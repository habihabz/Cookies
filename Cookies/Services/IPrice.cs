﻿using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IPrice
    {
       List<Price> GetActivePricesForAProduct(int prod_id);
        DbResult AddProductPrice(Price price);
    }
}
