using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class DbResult
    {
        [Key]
        public int id { get; set; }
        public string Message { get; set; }
    }
}
