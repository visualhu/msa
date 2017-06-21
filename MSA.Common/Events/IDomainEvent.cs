using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        object AggregateRootKey { get; set; }
        string AggregateRootType { get; set; }
        DateTime Timestamp { get; }
    }
}
