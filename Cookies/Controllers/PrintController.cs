using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cookies.Models;
using Cookies.Services;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;

namespace Cookies.Controllers
{
    public class PrintController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly IWebHostEnvironment iwebHostEnvironment;
        private readonly ISalesOrder isalesOrder;
        private readonly IInvoice iinvoice;

        public PrintController(IRole _irole,IMenu _imenu, IWebHostEnvironment _iwebHostEnvironment,ISalesOrder _isalesOrder,IInvoice _iinvoice)
        {
            irole = _irole;
            imenu = _imenu;
            iwebHostEnvironment=_iwebHostEnvironment;
            isalesOrder = _isalesOrder;
            iinvoice = _iinvoice;
        }
        
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult PrintSalesOrder(int id)
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{iwebHostEnvironment.WebRootPath}\\Reports\\SalesOrder.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            SalesOrder salesOrders = isalesOrder.GetSalesOrder(id);
            var salesOrderDetail = isalesOrder.getSalesOrderDetail(id);

            parameters.Add("OrderId",id.ToString());
            parameters.Add("CreDate", salesOrders.so_cre_date.ToString());
            parameters.Add("CustomerName", salesOrders.so_customer_name);
            
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", salesOrderDetail);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public ActionResult PrintInvoice(int id)
        {
            string mimtype = "";
            int extension = 1;
            var path = $"{iwebHostEnvironment.WebRootPath}\\Reports\\Invoice.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Invoice invoice = iinvoice.GetInvoice(id);
            var invoicedetails = iinvoice.getInvoiceDetails(id);

            parameters.Add("InvNo", id.ToString());
            parameters.Add("OrderNo", invoice.inv_order_no.ToString());
            parameters.Add("DateTime", invoice.inv_cre_date.ToString());
            parameters.Add("CustomerName", invoice.inv_customer_name);
            parameters.Add("OldBalance", "0.00");
            parameters.Add("AmtPaid", invoice.inv_paid_amt.ToString());
            parameters.Add("NewBalance", "0.00");

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("InvoiceDataSet", invoicedetails);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


    }
}