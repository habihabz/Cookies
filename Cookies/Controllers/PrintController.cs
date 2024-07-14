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
using System.Diagnostics;
using System.IO;

namespace Cookies.Controllers
{
    public class PrintController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly IWebHostEnvironment iwebHostEnvironment;
        private readonly ISalesOrder isalesOrder;
        private readonly IInvoice iinvoice;
        private readonly ICustomer icustomer;

        public PrintController(IRole _irole,IMenu _imenu, IWebHostEnvironment _iwebHostEnvironment,ISalesOrder _isalesOrder,IInvoice _iinvoice, ICustomer _icustomer)
        {
            irole = _irole;
            imenu = _imenu;
            iwebHostEnvironment=_iwebHostEnvironment;
            isalesOrder = _isalesOrder;
            iinvoice = _iinvoice;
            icustomer = _icustomer;
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
            Customer customer = icustomer.getCustomer(invoice.inv_customer??0);

            parameters.Add("InvNo", id.ToString());
            parameters.Add("OrderNo", invoice.inv_order_no.ToString());
            parameters.Add("DateTime", invoice.inv_cre_date.ToString());
            parameters.Add("CustomerName", invoice.inv_customer_name);
            parameters.Add("OldBalance", customer.c_balance_payable.ToString());
            parameters.Add("AmtPaid", invoice.inv_paid_amt.ToString());

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("InvoiceDataSet", invoicedetails);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);

            return File(result.MainStream, "application/pdf");
        }


        public string ReprintInvoice(int id)
        {
            string mimetype = "text/html"; // Set the mimetype to HTML
            int extension = 1;
            var path = $"{iwebHostEnvironment.WebRootPath}\\Reports\\ReprintInvoice.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Invoice invoice = iinvoice.GetInvoice(id);
            var invoicedetails = iinvoice.getInvoiceDetails(id);
            Customer customer = icustomer.getCustomer(invoice.inv_customer ?? 0);

            parameters.Add("InvNo", id.ToString());
            parameters.Add("OrderNo", invoice.inv_order_no.ToString());
            parameters.Add("DateTime", invoice.inv_cre_date.ToString());
            parameters.Add("CustomerName", invoice.inv_customer_name);
            parameters.Add("OldBalance", customer.c_balance_payable.ToString());
            parameters.Add("AmtPaid", invoice.inv_paid_amt.ToString());

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("InvoiceDataSet", invoicedetails);
            var result = localReport.Execute(RenderType.Html, extension, parameters, mimetype); // Render as HTML

            // Convert the byte array to a string
            string htmlString = System.Text.Encoding.UTF8.GetString(result.MainStream);

            //Trace.WriteLine(htmlString);

            string str= ExecuteProgram(id.ToString());


            return str;
        }
        public string ExecuteProgram(string id)
        {
            string exePath = @"C:\Users\Habeeb\source\repos\Cookies\ReportDesigning\bin\Debug\app.publish\ReportDesigning.exe";
            string parameters = id;

            ProcessStartInfo startInfo = new ProcessStartInfo(exePath, parameters)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = Path.GetDirectoryName(exePath)
            };

            try
            {
                using (Process process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        throw new InvalidOperationException("Process could not be started.");
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    int exitCode = process.ExitCode;
                    if (exitCode != 0)
                    {
                        Trace.WriteLine($"Process exited with code {exitCode}");
                    }

                    Trace.WriteLine("Output:");
                    Trace.WriteLine(output);
                    Trace.WriteLine("Error:");
                    Trace.WriteLine(error);

                    return string.IsNullOrEmpty(error) ? output : error;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception:");
                Trace.WriteLine(ex.Message);
                Trace.WriteLine("Stack Trace:");
                Trace.WriteLine(ex.StackTrace);

                if (ex.InnerException != null)
                {
                    Trace.WriteLine("Inner Exception:");
                    Trace.WriteLine(ex.InnerException.Message);
                    Trace.WriteLine("Inner Stack Trace:");
                    Trace.WriteLine(ex.InnerException.StackTrace);
                }

                return $"Error: {ex.Message}";
            }
        }
    
    }
}