using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Data.Common;

namespace Cookies.Repository
{
    public class CustomerRepository : ICustomer
    {
        private readonly DBContext db;

        public CustomerRepository(DBContext _db)
        {
            db = _db;
        }

        public DbResult createOrEditCustomer(Customer customer)
        {
            var c_id = new SqlParameter("c_id", customer.c_id + "");
            var c_name = new SqlParameter("c_name", customer.c_name + "");
            var c_price_type = new SqlParameter("c_price_type", customer.c_price_type + "");
            var c_active_yn = new SqlParameter("c_active_yn", customer.c_active_yn + "");
            var c_cre_by = new SqlParameter("c_cre_by", customer.c_cre_by + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.createOrEditCustomer @c_id,@c_name,@c_price_type,@c_active_yn,@c_cre_by", c_id, c_name, c_price_type,c_active_yn, c_cre_by).ToList().FirstOrDefault();
            return result;
        }

        public List<Customer> getCustomers()
        {
         
            var customers = db.Customers.FromSqlRaw<Customer>("EXECUTE dbo.getCustomers").ToList();
            return customers;
        }

        public Customer getCustomer(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var customer = db.Customers.FromSqlRaw<Customer>("EXECUTE dbo.getCustomer @id", _id).ToList().FirstOrDefault();
            return customer;
        }

        public DbResult removeCustomer(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.removeCustomer @id", _id).ToList().FirstOrDefault();
            return result;
        }

        public List<CustomerLedger> getCustomerTransactions(int c_id)
        {
            var _c_id = new SqlParameter("c_id", c_id + "");
            var customerLedgers = db.CustomerLedgers.FromSqlRaw<CustomerLedger>("EXECUTE dbo.getCustomerTransactions @c_id", _c_id).ToList();
            return customerLedgers;
        }

        public DataTable getCustomerLedger(int customer)
        {
            DataTable dt = new DataTable();
            var conn = db.Database.GetDbConnection();
            try
            {

                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"[dbo].[getCustomerLedger]";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("customer", customer + ""));
                    command.CommandTimeout = 250;
                    DbDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Dispose();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message + " " + ex.InnerException);
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}
