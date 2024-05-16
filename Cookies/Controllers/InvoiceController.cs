using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cookies.Models;
using Cookies.Services;
using System.Diagnostics;
using System.Data;

namespace Cookies.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly IProduct iproduct;
        private readonly IPrice iprice;
        private readonly ISalesOrder isalesOrder;
        private readonly ICustomer icustomer;
        private readonly IInvoice iinvoice;
  
        public InvoiceController(IRole _irole,IMenu _imenu, IProduct _iproduct, IPrice _iprice, ISalesOrder _isalesOrder, ICustomer _icustomer, IInvoice _iinvoice)
        {
            irole = _irole;
            imenu = _imenu;
            iproduct = _iproduct;
            iprice = _iprice;
            isalesOrder = _isalesOrder;
            icustomer = _icustomer;
            iinvoice = _iinvoice;
        }
        // GET: Role
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Customers = icustomer.getCustomers();
                ViewBag.Products = iproduct.GetProducts();
                return View();
            }
        }
        public ActionResult getInvoices(int customer,string posted,string dateRequired,string reportrange)
        {
            List<Invoice> invoices = new List<Invoice>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                if (!reportrange.Equals("undefined"))
                {
                    String[] array = reportrange.Split('-');

                    DateTime from = DateTime.Parse(array[0]);
                    DateTime to = DateTime.Parse(array[1] + " 11:59:59 PM");
                    invoices = iinvoice.GetInvoices(customer, posted, dateRequired, from, to);
                }


            }
            return View(invoices);
        }

        public DbResult postInvoice(int inv_id,decimal inv_amount_received)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = iinvoice.postInvoice(inv_id, inv_amount_received, user.u_id);
            }

            return result;
        }

        public ActionResult getInvoiceDetails(int inv_id,int inv_customer)
        {
            List<InvoiceDetail> invoicedetails = new List<InvoiceDetail>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                Invoice invoice = iinvoice.GetInvoice(inv_id);
                invoicedetails = iinvoice.getInvoiceDetails(inv_id);
                Customer customer = icustomer.getCustomer(inv_customer);
                ViewBag.Customers = icustomer.getCustomers();
                ViewBag.Products = iproduct.GetProducts();
                ViewBag.CBP = customer.c_balance_payable;
                ViewBag.TotalPrice = invoicedetails.Select(x => x.id_total_price).Sum();
                ViewBag.Invoice = invoice.inv_is_posted_yn ?? "N";
                ViewBag.AmountPaid = invoice.inv_paid_amt;
            }
            return View(invoicedetails);
        }

        public InvoiceDetail getInvDetail(int id_id)
        {
            InvoiceDetail invoiceDetail = new InvoiceDetail();

            invoiceDetail= iinvoice.getInvoiceDetail(id_id);

            return invoiceDetail;
        }


        public DbResult AddOrUpdateInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = iinvoice.AddOrUpdateInvoiceDetail(invoiceDetail);
            }

            return result;
        }


        public ActionResult InvoiceReport()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Customers = icustomer.getCustomers();
                ViewBag.Products = iproduct.GetProducts();
                return View();
            }
        }


        public ActionResult getInvoiceReport(int customer, int product, string reportrange, string type, string fullhistory)
        {
            DataTable dataTable = new DataTable();

            if (!reportrange.Equals("undefined"))
            {
                String[] array = reportrange.Split('-');

                DateTime from = DateTime.Parse(array[0]);
                DateTime to = DateTime.Parse(array[1] + " 11:59:59 PM");

                dataTable = iinvoice.getInvoiceReport(customer, product, from, to, type, fullhistory);
            }

            return View(dataTable);

        }


        private User getCurrentUser()
        {
            try
            {
                if (HttpContext.Session.GetString("User") == null)
                {
                    return null;
                }
                else
                {
                    User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User"));
                    ViewBag.Name = user.u_full_name;
                    ViewBag.isAdmin = user.u_is_admin;

                    List<MenuItems> menulist = new List<MenuItems>();

                    IEnumerable<Menu> menus = imenu.getMenulistByRoleAndType(user.u_role_id, "Menu");

                    foreach (var menu in menus)
                    {
                        MenuItems menuItems = new MenuItems();
                        menuItems.m_id = menu.m_id;
                        menuItems.m_description = menu.m_description;
                        menuItems.m_desc_to_show = menu.m_desc_to_show;
                        menuItems.m_link = menu.m_link;
                        menuItems.m_parrent_id = menu.m_parrent_id;
                        menuItems.m_type = menu.m_type;
                        menuItems.m_cre_by = menu.m_cre_by;
                        menuItems.m_active_yn = menu.m_active_yn;
                        menuItems.m_cre_date = menu.m_cre_date;
                        menuItems.menuItem = imenu.getMenulistByRoleAndTypeAndParrent(user.u_role_id, "MenuItem", menu.m_id);
                        menulist.Add(menuItems);
                    }

                    ViewBag.MenuList = menulist;
                  
                    if (user.u_role_description.Equals("Monitor")) ViewBag.isMonitor = "Y";
                    else
                    {
                        ViewBag.isMonitor = "N";
                    }
                    return user;
                }

            }
            catch
            {
                return null;
            }
        }
    }
}