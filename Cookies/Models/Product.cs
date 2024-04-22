using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product Id")]
        public int p_id { get; set; }

        [Display(Name = "Product Name")]
        public string p_name { get; set;}

        [Display(Name = "Active")]
        public string p_active_yn { get; set; }

        [Display(Name = "Created by")]
        public int p_cre_by { get; set; }

        [Display(Name = "Created by")]
        public string p_cre_by_name { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime p_cre_date { get; set; }
    }
}
