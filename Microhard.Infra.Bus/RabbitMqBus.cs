using MediatR;
using MicroHard.Domain.Core.Bus;
using MicroHard.Domain.Core.Commands;
using MicroHard.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microhard.Infra.Bus
{
    public sealed class RabbitMqBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        const string RabbitHostName = "rabbit";

        public RabbitMqBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitHostName
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var eventName = @event.GetType().Name;

                    channel.QueueDeclare(eventName, false, false, false, null);

                    var message = JsonConvert.SerializeObject(@event);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", eventName, null, body);

                }
            }
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);


            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException(
                    $"Handler type {handlerType.Name} is already registred for '{eventName}' ", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StratBasicConsume<T>();
        }

        private void StratBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = RabbitHostName,
                DispatchConsumersAsync = true
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Recieved;

            channel.BasicConsume(eventName, true, consumer);

        }

        private async Task Consumer_Recieved(object sender, BasicDeliverEventArgs e)
        {
            var evenName = e.RoutingKey;

            var message = Encoding.UTF8.GetString(e.Body);

            try
            {
                await ProcessEvents(evenName, message).ConfigureAwait(false);
            }
            catch
            {

            }
        }

        private async Task ProcessEvents(string evenName, string message)
        {
            if (_handlers.ContainsKey(evenName))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var subscriptions = _handlers[evenName];

                    foreach (var subscription in subscriptions)
                    {
                        //var handler = Activator.CreateInstance(subscription);
                        var handler = scope.ServiceProvider.GetService(subscription);

                        if (handler == null)
                            continue;

                        var eventType = _eventTypes.SingleOrDefault(a => a.Name == evenName);

                        var @event = JsonConvert.DeserializeObject(message, eventType);

                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }


            }
        }
    }
}
