using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class SalesOrder
    {
        [Key]
        [Display(Name = "SO ID")]
        public int so_id { get; set; }

        [Display(Name = "Customer")]
        public int? so_customer { get; set;}

        [Display(Name = "Customer")]
        public string? so_customer_name { get; set; }

        [Display(Name = "Price Type")]
        public string so_price_type { get; set; }

        [Display(Name = "Is INV Converted Y/N")]
        public string so_is_inv_converved { get; set; }

        [Display(Name = "Is Finished Y/N")]
        public string so_is_finished_yn { get; set; }

        [Display(Name = "Created by")]
        public int? so_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string? so_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime? so_cre_date { get; set; }
    }
}
