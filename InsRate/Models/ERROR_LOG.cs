using System;
using System.Collections.Generic;

namespace BR.Models
{
    public partial class ERROR_LOG
    {
        public System.Guid ID { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
    }
}
