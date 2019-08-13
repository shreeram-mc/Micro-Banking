using MicroHard.Banking.Data.Context;
using MicroHard.Banking.Domain.Interfaces;
using MicroHard.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts; 
        }
    }
}
