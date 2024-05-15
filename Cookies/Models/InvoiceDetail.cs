using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class InvoiceDetail
    {
        [Key]
        [Display(Name = "Id")]
        public int id_id { get; set; }

        [Display(Name = "Inv Id")]
        public int? id_inv_id { get; set; }

        [Display(Name = "Product")]
        public int? id_prod_id { get; set;}

        [Display(Name = "Product")]
        public string? id_prod_name { get; set; }

        [Display(Name = "Qty")]
        public decimal id_qty { get; set; }

        [Display(Name = "Unit Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal id_unit_price { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "₹{0:N}")]
        public decimal id_total_price { get; set; }

        [Display(Name = "Modified by")]
        public int? id_modify_by { get; set; }

        [Display(Name = "Modified by")]
        public string? id_modify_by_name { get; set; }

        [Display(Name = "Modify Date")]
        [DataType(DataType.Date)]
        public DateTime? id_modify_date { get; set; }
    }
}
