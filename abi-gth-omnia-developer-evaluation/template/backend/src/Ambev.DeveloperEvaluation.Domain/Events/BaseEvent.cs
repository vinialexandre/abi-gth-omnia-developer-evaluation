using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public abstract class BaseEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
