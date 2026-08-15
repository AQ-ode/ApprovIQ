using ApprovIQ.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovIQ.Domain.Entities
{
    public class Budget : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal SpentAmount { get; set; } = 0;
        public decimal RemainingAmount => TotalAmount - SpentAmount;
        public decimal AlertThresholdPercent { get; set; } = 80;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid TenantId { get; set; }
        public Guid DepartmentId { get; set; }

        // Navigation properties
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();
    }
}
