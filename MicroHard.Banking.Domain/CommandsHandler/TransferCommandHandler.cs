using MediatR;
using MicroHard.Banking.Domain.Commands;
using MicroHard.Banking.Domain.Events;
using MicroHard.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroHard.Banking.Domain.CommandsHandler
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand, bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            //Publish to rabbitmq
            _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));


            return Task.FromResult(true);
        }
    }
}
