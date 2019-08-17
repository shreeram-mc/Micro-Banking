using MicroHard.Banking.Domain.Models;
using System;
using System.Collections.Generic;



namespace MicroHard.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
        
    }
}
