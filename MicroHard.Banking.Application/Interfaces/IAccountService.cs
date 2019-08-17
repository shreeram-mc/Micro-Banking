using MicroHard.Banking.Application.Models;
using MicroHard.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Banking.Application.Interfaces
{
   public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();

        void TransferFunds(AccountTransfer accountTransfer);
    }
}
