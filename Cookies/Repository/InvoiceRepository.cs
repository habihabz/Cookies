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
    public class InvoiceRepository : IInvoice
    {
        private readonly DBContext db;

        public InvoiceRepository(DBContext _db)
        {
            db = _db;
        }

        public Invoice AddInvoiceDetail(int inv_product, int inv_customer, int inv_qty)
        {
            throw new NotImplementedException();
        }

        public DbResult createInvoice(int inv_customer, string inv_data, int u_id)
        {
            throw new NotImplementedException();
        }

        public Invoice GetInvoice(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var invoice = db.Invoices.FromSqlRaw<Invoice>("EXECUTE dbo.GetInvoice @id", _id).ToList().FirstOrDefault();
            return invoice;
        }

        public List<InvoiceDetail> getInvoiceDetails(int inv_id)
        {
            var _inv_id = new SqlParameter("inv_id", inv_id + "");
            var invoiceDetails = db.InvoiceDetails.FromSqlRaw<InvoiceDetail>("EXECUTE dbo.getInvoiceDetails @inv_id", _inv_id).ToList();
            return invoiceDetails;
        }

        public List<Invoice> GetInvoices(int customer,string posted,string dateRequired, DateTime from, DateTime to)
        {
            var _customer = new SqlParameter("customer", customer + "");
            var _posted = new SqlParameter("posted", posted + "");
            var _dateRequired = new SqlParameter("dateRequired", dateRequired + "");
            var _from = new SqlParameter("from", from + "");
            var _to = new SqlParameter("to", to + "");
            var invoices = db.Invoices.FromSqlRaw<Invoice>("EXECUTE dbo.GetInvoices @customer,@posted,@dateRequired,@from,@to", _customer, _posted, _dateRequired, _from, _to).ToList();
            return invoices;
        }

        public DbResult postInvoice(int inv_id,decimal inv_amount_received,int user)
        {
            var _inv_id = new SqlParameter("inv_id", inv_id + "");
            var _inv_amount_received = new SqlParameter("inv_amount_received", inv_amount_received + "");
            var _user = new SqlParameter("user", user + "");
           
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.postInvoice @inv_id,@inv_amount_received,@user", _inv_id, _inv_amount_received, _user).ToList().FirstOrDefault();
            return result;
           
        }

        public DbResult removeInvoice(int id)
        {
            var _id = new SqlParameter("id", id + "");
            var result = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.removeInvoice @id", _id).ToList().FirstOrDefault();
            return result;
        }

        public InvoiceDetail getInvoiceDetail(int id_id)
        {
            var _id_id = new SqlParameter("id_id", id_id + "");
            var invoiceDetail = db.InvoiceDetails.FromSqlRaw<InvoiceDetail>("EXECUTE dbo.getInvoiceDetail @id_id", _id_id).ToList().FirstOrDefault();
            return invoiceDetail;
        }

        public DbResult AddOrUpdateInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            var id_id = new SqlParameter("id_id", invoiceDetail.id_id + "");
            var id_inv_id = new SqlParameter("id_inv_id", invoiceDetail.id_inv_id + "");
            var id_prod_id = new SqlParameter("id_prod_id", invoiceDetail.id_prod_id + "");
            var id_qty = new SqlParameter("id_qty", invoiceDetail.id_qty + "");
            var id_modify_by = new SqlParameter("id_modify_by", invoiceDetail.id_modify_by + "");

            var dbresult = db.DbResult.FromSqlRaw<DbResult>("EXECUTE dbo.AddOrUpdateInvoiceDetail @id_id,@id_inv_id,@id_prod_id,@id_qty,@id_modify_by",
                id_id, id_inv_id, id_prod_id, id_qty, id_modify_by).ToList().FirstOrDefault();
            return dbresult;
        }

        public DataTable getInvoiceReport(int customer, int product, DateTime from, DateTime to, string type, string fullhistory)
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
                    string query = @"exec [dbo].[getInvoiceReport] @customer='" + customer + "',@product='" + product + "'," +
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
