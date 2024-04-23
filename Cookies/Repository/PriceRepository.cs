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
    public class PriceRepository : IPrice
    {
        private readonly DBContext db;

        public PriceRepository(DBContext _db)
        {
            db = _db;
        }

        public DbResult AddProductPrice(Price price)
        {
            var _pr_prod_id = new SqlParameter("pr_prod_id", price.pr_prod_id + "");
            var _pr_price_type = new SqlParameter("pr_price_type", price.pr_price_type + "");
            var _pr_price = new SqlParameter("pr_price", price.pr_price + "");
            var _pr_start_date = new SqlParameter("pr_start_date", price.pr_start_date + "");
            var _pr_cre_by = new SqlParameter("pr_cre_by", price.pr_cre_by + "");
            
            var dbResult = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.AddProductPrice @pr_prod_id,@pr_price_type,@pr_price,@pr_start_date,@pr_cre_by", 
                _pr_prod_id, _pr_price_type, _pr_price, _pr_start_date, _pr_cre_by).ToList().FirstOrDefault();

            return dbResult;
        }

        public List<Price> GetActivePricesForAProduct(int prod_id)
        {
            var _prod_id = new SqlParameter("prod_id", prod_id + "");
            var price = db.Prices.FromSqlRaw<Price>("EXECUTE dbo.GetActivePricesForAProduct @prod_id", _prod_id).ToList();
            return price;
        }
    }
}
