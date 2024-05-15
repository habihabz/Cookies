using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class SalesOrderDetail
    {
        [Key]
        [Display(Name = "SOD ID")]
        public int sod_id { get; set; }

        [Display(Name = "Product")]
        public int? sod_prod_id { get; set;}

        [Display(Name = "Product")]
        public string? sod_prod_name { get; set; }

        [Display(Name = "Price Type")]
        public string sod_price_type { get; set; }

        [Display(Name = "Qty")]
        public decimal sod_qty { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal sod_unit_price { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal sod_total_price { get; set; }
        
 
    }
}
