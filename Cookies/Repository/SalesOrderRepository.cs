using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Cookies.Models;
using Cookies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;
using System.Data.Common;
using System.Diagnostics;

namespace Cookies.Repository
{
    public class SalesOrderRepository : ISalesOrder
    {
        private readonly DBContext db;

        public SalesOrderRepository(DBContext _db)
        {
            db = _db;
        }

        public DbResult createSalesOrder(int so_customer,string sod_data,int  user)
        {

            List<SaleOrderDetailType> silverSoldOr = JsonConvert.DeserializeObject<List<SaleOrderDetailType>>(sod_data).ToList();


            var Dttable = ToDataTable(silverSoldOr);

            var _so_customer = new SqlParameter("so_customer", so_customer + "");
            var _TempTbl = new SqlParameter("TempTbl", SqlDbType.Structured) { Value = Dttable, TypeName = "dbo.SaleOrderDetailType" };
            var _user = new SqlParameter("user", user + "");
       
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.createSalesOrder @so_customer,@TempTbl,@user", _so_customer, _TempTbl, _user).ToList().FirstOrDefault();
            return result;

        }

        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        public List<SalesOrder> GetSalesOrders()
        {
         
            var salesOrders = db.SalesOrders.FromSqlRaw<SalesOrder>("EXECUTE dbo.GetSalesOrders").ToList();
            return salesOrders;
        }

        public SalesOrder GetSalesOrder(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var salesOrder = db.SalesOrders.FromSqlRaw<SalesOrder>("EXECUTE dbo.GetSalesOrder @id", _id).ToList().FirstOrDefault();
            return salesOrder;
        }

        public DbResult removeSalesOrder(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.removeSalesOrder @id", _id).ToList().FirstOrDefault();
            return result;
        }

        public SalesOrderDetail AddSalesOrderDetail(int sod_product, int so_customer, int sod_qty)
        {
            var _sod_product = new SqlParameter("sod_product", sod_product + "");
            var _so_customer = new SqlParameter("so_customer", so_customer + "");
            var _sod_qty = new SqlParameter("sod_qty", sod_qty + "");
            var salesOrderDetail = db.SalesOrderDetails.FromSqlRaw<SalesOrderDetail>("EXECUTE dbo.AddSalesOrderDetail @sod_product,@so_customer,@sod_qty", _sod_product, _so_customer, _sod_qty).ToList().FirstOrDefault();

            return salesOrderDetail;
        }

        public List<List_SalesOrderDetail> getSalesOrderDetail(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var salesOrderDetails = db.List_SalesOrderDetails.FromSqlRaw<List_SalesOrderDetail>("EXECUTE dbo.getSalesOrderDetail @id", _id).ToList();
            return salesOrderDetails;
        }

        public DbResult convertSalesOrderToInvoice(int so_id)
        {
            var _so_id = new SqlParameter("so_id", so_id + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.convertSalesOrderToInvoice @so_id", _so_id).ToList().FirstOrDefault();
            return result;
        }

        public DataTable getSalesOrderReport(int customer, int product, DateTime from, DateTime to, string type, string fullhistory)
        {
            DataTable dt = new DataTable();
            var conn = db.Database.GetDbConnection();
            string from2 = "";
            string to2 = "";
            if (fullhistory == "true")
            {
                from2 = ""; to2 = "";
            }
            else
            {
                from2 = "" + from; to2 = "" + to;
            }
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    string query = @"exec [dbo].[getSalesOrderReport] @customer='" + customer  +"',@product='" + product + "'," +
                        "@from='" + from2 + "',@to='" + to2 + "',@type='" + type + "',@fullhistory='" + fullhistory + "'";
                    command.CommandText = query;
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
