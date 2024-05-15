using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class CustomerLedger 
    {
        [Key]
        [Display(Name = "Inv No")]
        public int cl_id { get; set; }

        [Display(Name = "Customer")]
        public int? cl_customer { get; set;}

        [Display(Name = "Customer")]
        public string? cl_customer_name { get; set; }

        [Display(Name = "Account Type")]
        public string cl_acc_type { get; set; }

        [Display(Name = "Inv Id")]
        public int? cl_inv_id { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal cl_amount { get; set; }

        [Display(Name = "Created by")]
        public int? cl_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string? cl_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime? cl_cre_date { get; set; }
    }
}
