using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovIQ.Domain.Enums
{
    public enum POStatus
    {
        Draft = 0,
        Submitted = 1,
        Approved = 2,
        Rejected = 3,
        Delivered = 4,
        Paid = 5,
        Closed = 6,
        Cancelled = 7
    }
}
