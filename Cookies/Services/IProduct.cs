﻿using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IProduct
    {
       List<Product> GetProducts();
       DbResult createOrEditProduct(Product product);
       Product GetProduct(int id);
       DbResult removeProduct(int id);
    }
}
