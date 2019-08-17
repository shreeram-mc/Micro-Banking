using Microhard.Transfer.Domain.Models;
using System.Collections.Generic;

namespace MicroHard.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogs();
        void Add(TransferLog transferLog);
    }
}
