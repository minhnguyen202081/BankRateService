using BR.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ModelLib.BankModel
{
    public class BankRuleView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "BankCode")]
        public string BankCode { get; set; }
      
        [Display(Name = "Tenor")]
        public string TenorDesc { get; set; }


        [Display(Name = "TenorCode")]
        public string TenorCode { get; set; }

         [StringLength(500)]
         
         [Display(Name = "Bank Rule")]
         public string BankRule { get; set; }

         [Display(Name = "Result")]
         public string Result { get; set; }

        [Display(Name = "TenorIndex")]
        public string TenorIndex { get; set; }

    }
}
