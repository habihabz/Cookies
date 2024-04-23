using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class Price
    {
        [Key]
        [Display(Name = "Price id")]
        public int pr_id { get; set; }

        [Display(Name = "Product Id")]
        public int? pr_prod_id { get; set;}

        [Display(Name = "Price Type")]
        public string? pr_price_type { get; set; }

        [Display(Name = "Product Price")]
        [DataType(DataType.Currency)]
        public decimal? pr_price { get; set; }

        [Display(Name = "Price start from")]
        [DataType(DataType.Date)]
        public DateTime? pr_start_date { get; set; }
    
        [Display(Name = "Price end date")]
        [DataType(DataType.Date)]
        public DateTime? pr_end_date { get; set; }

        [Display(Name = "Created by")]
        public int? pr_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string? pr_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? pr_cre_date { get; set; }
    }
}
