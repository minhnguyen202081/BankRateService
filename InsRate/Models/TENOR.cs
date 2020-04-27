using System;
using System.Collections.Generic;

namespace BR.Models
{
    public partial class TENOR
    {
        public System.Guid TenorID { get; set; }
        public string TenorCode { get; set; }
        public string TenorDesc { get; set; }
        public string TenorIndex { get; set; }
    }
}
