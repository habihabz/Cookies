using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Models
{
    public class MenuWrapper
    {
        public IEnumerable<Menu> allMenu;
        public IEnumerable<Menu> userMenu;
    }
}
