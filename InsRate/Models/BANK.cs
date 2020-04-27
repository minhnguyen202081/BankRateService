using System;
using System.Collections.Generic;

namespace BR.Models
{
    public partial class BANK
    {
        public System.Guid BankID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankLink { get; set; }
        public string DataExtractor { get; set; }
        public List<string> SubmittedBanks { get; set; }
    }
}
