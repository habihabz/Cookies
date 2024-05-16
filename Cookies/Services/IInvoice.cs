using Cookies.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cookies.Services
{
    public interface IInvoice
    {
        List<Invoice> GetInvoices(int customer,string posted,string dateRequired,DateTime from,DateTime to);
        Invoice GetInvoice(int id);
        DbResult removeInvoice(int id);
        Invoice AddInvoiceDetail(int inv_product, int inv_customer, int inv_qty);
        DbResult createInvoice(int inv_customer, string inv_data, int u_id);
        DbResult postInvoice(int inv_id,decimal inv_amount_received,int user);
        List<InvoiceDetail> getInvoiceDetails(int inv_id);
        InvoiceDetail getInvoiceDetail(int id_id);
        DbResult AddOrUpdateInvoiceDetail(InvoiceDetail invoiceDetail);
        DataTable getInvoiceReport(int customer, int product, DateTime from, DateTime to, string type, string fullhistory);
    }
}
