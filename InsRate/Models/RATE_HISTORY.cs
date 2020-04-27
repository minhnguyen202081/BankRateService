using System;
using System.Collections.Generic;

namespace BR.Models
{
    public partial class RATE_HISTORY
    {
        public int ID { get; set; }
        public string GroupCode { get; set; }
        public string BankCode { get; set; }
        public string TenorCode { get; set; }
        public string InsRate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
