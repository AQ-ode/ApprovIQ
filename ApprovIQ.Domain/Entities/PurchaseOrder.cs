using ApprovIQ.Domain.Common;
using ApprovIQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovIQ.Domain.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        public string PONumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public POStatus Status { get; set; } = POStatus.Draft;
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = "USD";
        public DateTime? RequiredByDate { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public Guid TenantId { get; set; }
        public Guid RequestedById { get; set; }
        public Guid? ApprovedById { get; set; }
        public Guid? VendorId { get; set; }
        public Guid? BudgetId { get; set; }

        // Navigation properties
        public ICollection<POLineItem> LineItems { get; set; } = new List<POLineItem>();
    }
}
