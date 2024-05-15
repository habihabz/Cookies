using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class SaleOrderDetailType
    {
      
        [Display(Name = "Product Id")]
        public int ProductID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set;}

        [Display(Name = "Created by")]
        public decimal Qty { get; set; }

        [Display(Name = "Created by")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Created by")]
        public decimal NetPrice { get; set; }

        [Display(Name = "Created by")]
        public string Remarks { get; set; }

        [Display(Name = "Action")]
        public string Action { get; set; }


    }
}
