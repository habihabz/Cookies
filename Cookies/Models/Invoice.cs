using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class Invoice
    {
        [Key]
        [Display(Name = "Inv No")]
        public int inv_id { get; set; }

        [Display(Name = "Customer")]
        public int? inv_customer { get; set;}

        [Display(Name = "Customer")]
        public string? inv_customer_name { get; set; }

        [Display(Name = "Price Type")]
        public string inv_price_type { get; set; }

        [Display(Name = "Order No")]
        public int? inv_order_no { get; set; }

        [Display(Name = "Is Posted Y/N")]
        public string inv_is_posted_yn { get; set; }

        [Display(Name = "Paid Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal? inv_paid_amt { get; set; }

        [Display(Name = "Created by")]
        public int? inv_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string? inv_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? inv_cre_date { get; set; }
    }
}
