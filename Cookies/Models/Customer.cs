using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer Id")]
        public int c_id { get; set; }

        [Display(Name = "Customer Name")]
        public string c_name { get; set;}

        [Display(Name = "Active")]
        public string c_active_yn { get; set; }

        [Display(Name = "Price Type")]
        public string c_price_type { get; set; }

        [Display(Name = "Balance Payable")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal c_balance_payable { get; set; }

        [Display(Name = "Created by")]
        public int c_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string c_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime c_cre_date { get; set; }
    }
}
