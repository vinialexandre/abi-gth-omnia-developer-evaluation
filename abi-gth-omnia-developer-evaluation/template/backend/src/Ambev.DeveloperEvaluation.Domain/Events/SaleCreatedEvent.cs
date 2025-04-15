using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : BaseEvent
    {
        public Guid SaleId { get; }
        public DateTime SaleDate { get; }
        public Guid CustomerId { get; }
        public decimal TotalAmount { get; }

        public SaleCreatedEvent(Guid saleId, DateTime saleDate, Guid customerId, decimal totalAmount)
        {
            SaleId = saleId;
            SaleDate = saleDate;
            CustomerId = customerId;
            TotalAmount = totalAmount;
        }
    }
}
