using MediatR;
using Microhard.Infra.Bus;
using Microhard.Transfer.Domain.EventsHandler;
using MicroHard.Banking.Application.Interfaces;
using MicroHard.Banking.Application.Services;
using MicroHard.Banking.Data.Context;
using MicroHard.Banking.Data.Repository;
using MicroHard.Banking.Domain.Commands;
using MicroHard.Banking.Domain.CommandsHandler;
using MicroHard.Banking.Domain.Events;
using MicroHard.Banking.Domain.Interfaces;
using MicroHard.Domain.Core.Bus;
using MicroHard.Transfer.Application.Interfaces;
using MicroHard.Transfer.Application.Services;
using MicroHard.Transfer.Data.Context;
using MicroHard.Transfer.Data.Repository;
using MicroHard.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroHard.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMqBus>();

            //Domain Banking Commands
            services.AddTransient<IRequestHandler<CreateTransferCommand,bool>, TransferCommandHandler>();

            //Domain events
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<TransferDbContext>();

        }
    }
}
