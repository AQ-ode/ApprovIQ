using ApprovIQ.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovIQ.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Subdomain { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string PrimaryColor { get; set; } = "#4F46E5";
        public string? CustomCss { get; set; }
        public bool IsActive { get; set; } = true;
        public string? StripeCustomerId { get; set; }
        public string? StripeSubscriptionId { get; set; }

        // Navigation properties
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();
    }
}
