using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : BaseEvent
    {
        public Guid SaleId { get; }
        public string Reason { get; }
        public DateTime CancelledAt { get; }

        public SaleCancelledEvent(Guid saleId, string reason)
        {
            SaleId = saleId;
            Reason = reason;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
