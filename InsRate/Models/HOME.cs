using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BR.Models
{
     public partial class HOME
    {
        public string BankCode { get; set; }
           [Column("1M")]
        public string oneMonth { get; set; }
            [Column("2M")]
        public string twoMonth { get; set; }
            [Column("3M")]
        public string threeMonth { get; set; }
            [Column("6M")]
        public string sixMonth { get; set; }
           [Column("9M")]
         public string nineMonth { get; set; }

           [Column("12M")]
           public string twelveMonth { get; set; }


           [Column("24M")]
           public string twentyfourMonth { get; set; }

           [Column("36M")]
           public string thirtysixMonth { get; set; }

           [Column("48M")]
           public string fortyeightMonth { get; set; }

           [Column("60M")]
           public string sixtyMonth { get; set; }
    }
}
