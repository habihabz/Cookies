using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly DBContext db;

        public ProductRepository(DBContext _db)
        {
            db = _db;
        }

        public List<Product> GetProducts()
        {
         
            var paintqc = db.Products.FromSqlRaw<Product>("EXECUTE dbo.GetProducts").ToList();
            return paintqc;
        }
    }
}
