using Microhard.Infra.Bus;
using MicroHard.Banking.Application.Interfaces;
using MicroHard.Banking.Application.Services;
using MicroHard.Banking.Data.Context;
using MicroHard.Banking.Data.Repository;
using MicroHard.Banking.Domain.Interfaces;
using MicroHard.Domain.Core.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Infra.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddTransient<IEventBus, RabbitMqBus>();

            //Application Services
            services.AddTransient<IAccountService, AccountService>();

            //Data
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<BankingDbContext>();

        }
    }
}
