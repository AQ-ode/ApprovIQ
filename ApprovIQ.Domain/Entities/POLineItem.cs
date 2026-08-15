using ApprovIQ.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovIQ.Domain.Entities
{
    public class POLineItem : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public string? Unit { get; set; }
        public Guid PurchaseOrderId { get; set; }

        // Navigation property
        public PurchaseOrder PurchaseOrder { get; set; } = null!;
        
    }
}
