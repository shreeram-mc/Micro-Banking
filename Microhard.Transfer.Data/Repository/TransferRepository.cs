using Microhard.Transfer.Domain.Models;
using MicroHard.Transfer.Domain.Interfaces;
using MicroHard.Transfer.Data.Context;
using System.Collections.Generic;

namespace MicroHard.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private TransferDbContext _ctx;

        public TransferRepository(TransferDbContext ctx)
        {
            _ctx = ctx;
        } 

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _ctx.TransferLogs;
        }
    }
}
