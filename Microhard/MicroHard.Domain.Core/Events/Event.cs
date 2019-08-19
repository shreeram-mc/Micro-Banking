using System;
using System.Collections.Generic;
using System.Text;

namespace MicroHard.Domain.Core.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; } = DateTime.Now;


    }
}
