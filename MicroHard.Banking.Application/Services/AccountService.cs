using MicroHard.Banking.Application.Interfaces;
using MicroHard.Banking.Domain.Interfaces;
using MicroHard.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountrepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountrepository = accountRepository;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountrepository.GetAccounts();
        }
    }
}
