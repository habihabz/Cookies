using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Cookies.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly DBContext db;

        public ProductRepository(DBContext _db)
        {
            db = _db;
        }

        public DbResult createOrEditProduct(Product product)
        {
            var p_id = new SqlParameter("p_id", product.p_id + "");
            var p_name = new SqlParameter("p_name", product.p_name + "");
            var p_active_yn = new SqlParameter("p_active_yn", product.p_active_yn + "");
            var p_cre_by = new SqlParameter("p_cre_by", product.p_cre_by + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.createOrEditProduct @p_id,@p_name,@p_active_yn,@p_cre_by", p_id, p_name, p_active_yn, p_cre_by).ToList().FirstOrDefault();
            return result;
        }

        public List<Product> GetProducts()
        {
         
            var products = db.Products.FromSqlRaw<Product>("EXECUTE dbo.GetProducts").ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var products = db.Products.FromSqlRaw<Product>("EXECUTE dbo.GetProduct @id", _id).ToList().FirstOrDefault();
            return products;
        }

        public DbResult removeProduct(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.removeProduct @id", _id).ToList().FirstOrDefault();
            return result;
        }
    }
}
