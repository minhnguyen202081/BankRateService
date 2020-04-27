using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ModelLib.BankModel
{
  public class HomeView
    {
      
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        [Display(Name = "Bank Code")]
        public string BankCode { get; set; }

        [StringLength(300)]
        [Display(Name = "1M")]
        [Column("1M")]
        public string oneMonth { get; set; }

        [StringLength(300)]
        [Display(Name = "2M")]
        [Column("2M")]
        public string twoMonth { get; set; }

        [StringLength(300)]
        [Display(Name = "3M")]
        [Column("3M")]
        public string threeMonth { get; set; }

        [StringLength(300)]
        [Display(Name = "6M")]
        [Column("6M")]
        public string sixMonth { get; set; }


        [StringLength(300)]
        [Display(Name = "9M")]
        [Column("9M")]
        public string nineMonth { get; set; }

        [StringLength(300)]
        [Display(Name = "12M")]
        [Column("12M")]
        public string twelveMonth { get; set; }

      [StringLength(300)]
        [Display(Name = "24M")]
        [Column("24M")]
        public string twentyfourMonth { get; set; }

      [StringLength(300)]
      [Display(Name = "36M")]
      [Column("36M")]
      public string thirtysixMonth { get; set; }

      [StringLength(300)]
      [Display(Name = "48M")]
      [Column("48M")]
      public string fortyeightMonth { get; set; }

      [StringLength(300)]
      [Display(Name = "60M")]
      [Column("60M")]
      public string sixtyMonth { get; set; }

        public List <string> SelectedBankId { get; set; }
    }

    
}
