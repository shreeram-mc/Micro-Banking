using Microhard.Transfer.Domain.Models;
using MicroHard.Transfer.Domain.Interfaces;
using MicroHard.Domain.Core.Bus;
using MicroHard.Transfer.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Transfer.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IEventBus _bus;

        public TransferService(ITransferRepository transferRepository, IEventBus bus)
        {
            _transferRepository = transferRepository;
            _bus = bus;
        }

      
        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _transferRepository.GetTransferLogs();
        }

        
    }
}
