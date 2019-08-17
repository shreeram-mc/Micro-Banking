using Microhard.Transfer.Domain.Models;
using MicroHard.Banking.Domain.Events;
using MicroHard.Domain.Core.Bus;
using MicroHard.Transfer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microhard.Transfer.Domain.EventsHandler
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepoisitory)
        {
            _transferRepository = transferRepoisitory;
        }
        public Task Handle(TransferCreatedEvent @event)
        {
            _transferRepository.Add(new TransferLog()
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            });

            return Task.CompletedTask;
        }
    }
}
