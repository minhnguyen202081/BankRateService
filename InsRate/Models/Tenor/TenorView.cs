using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ModelLib.BankModel
{
    public class TenorView
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Tenor ID")]
        public Guid TenorId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Tenor Code")]
        public string TenorCode { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Tenor Description")]
        public string TenorDesc { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Tenor index")]
        public string TenorIndex { get; set; }

    }
}
