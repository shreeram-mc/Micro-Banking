using MicroHard.Banking.Application.Interfaces;
using MicroHard.Banking.Application.Models;
using MicroHard.Banking.Domain.Commands;
using MicroHard.Banking.Domain.Interfaces;
using MicroHard.Banking.Domain.Models;
using MicroHard.Domain.Core.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountrepository;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus bus)
        {
            _accountrepository = accountRepository;
            _bus = bus;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountrepository.GetAccounts();
        }

        public void TransferFunds(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand
                (
                    accountTransfer.FromAccount,
                    accountTransfer.ToAccount,
                    accountTransfer.TransferAmount
                 );

            _bus.SendCommand(createTransferCommand);

        }
    }
}
