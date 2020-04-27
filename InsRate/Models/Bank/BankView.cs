using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelLib.BankModel
{
    public class BankView   
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Bank ID")]
        public Guid BankId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Bank Code")]
        public string BankCode { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [StringLength(300)]
        [Display(Name = "URL")]
        public string URL { get; set; }

        [StringLength(200)]
        [Display(Name = "DataExtractor")]
        public string DataExtractor { get; set; }
    }
}