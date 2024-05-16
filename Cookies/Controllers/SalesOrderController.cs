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
    public class SalesOrderController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly IProduct iproduct;
        private readonly IPrice iprice;
        private readonly ISalesOrder isalesOrder;
        private readonly ICustomer icustomer;
        public SalesOrderController(IRole _irole,IMenu _imenu, IProduct _iproduct, IPrice _iprice, ISalesOrder _isalesOrder, ICustomer _icustomer)
        {
            irole = _irole;
            imenu = _imenu;
            iproduct = _iproduct;
            iprice = _iprice;
            isalesOrder = _isalesOrder;
            icustomer = _icustomer;
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
        public ActionResult getSalesOrders()
        {
            List<SalesOrder> salesOrders = new List<SalesOrder>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                salesOrders = isalesOrder.GetSalesOrders();
            }
            return View(salesOrders);
        }

      
        public ActionResult AddSalesOrderDetail(int sod_product,int so_customer,int sod_qty)
        {
            SalesOrderDetail salesOrder = new SalesOrderDetail();
            var user = getCurrentUser();
            if (user == null)
            { 

            }
            else
            {
                salesOrder = isalesOrder.AddSalesOrderDetail(sod_product, so_customer, sod_qty);
            }
            return View(salesOrder);
        }

        public DbResult createSalesOrder(int so_customer,string sod_data)
        {
            Trace.WriteLine(sod_data);

            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = isalesOrder.createSalesOrder(so_customer, sod_data, user.u_id);
            }

            return result;
        }

        public SalesOrder getSalesOrder(int id)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder = isalesOrder.GetSalesOrder(id);
            return salesOrder;
        }

        public DbResult removeSalesOrder(int id )
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = isalesOrder.removeSalesOrder(id);
            }

            return result;
        }  
        
        public DbResult convertSalesOrderToInvoice(int so_id)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = isalesOrder.convertSalesOrderToInvoice(so_id);
            }

            return result;
        }





        public ActionResult getSalesOrderDetail(int id)
        {
            List<List_SalesOrderDetail> salesOrderDetails = new List<List_SalesOrderDetail>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                salesOrderDetails = isalesOrder.getSalesOrderDetail(id);
            }
            return View(salesOrderDetails);
           
        }

        public ActionResult SalesOrderReport()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Products = iproduct.GetProducts();
                ViewBag.Customers = icustomer.getCustomers();
                return View();
            }
        }


        public ActionResult getSalesOrderReport(int customer, int product, string reportrange, string type, string fullhistory)
        {
            DataTable dataTable = new DataTable();

            if (!reportrange.Equals("undefined"))
            {
                String[] array = reportrange.Split('-');

                DateTime from = DateTime.Parse(array[0]);
                DateTime to = DateTime.Parse(array[1] + " 11:59:59 PM");

                dataTable = isalesOrder.getSalesOrderReport(customer, product,  from, to, type, fullhistory);
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