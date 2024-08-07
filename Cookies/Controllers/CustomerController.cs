﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cookies.Models;
using Cookies.Services;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Cookies.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly ICustomer icustomer;
        private readonly IConfiguration configuration;
     
        public CustomerController(IRole _irole,IMenu _imenu, ICustomer _icustomer, IConfiguration configuration)
        {
            irole = _irole;
            imenu = _imenu;
            icustomer = _icustomer;
            Configuration = configuration;
            
        }
        public IConfiguration Configuration { get; }
        // GET: Role
        public ActionResult Index()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        public ActionResult getCustomers()
        {
            List<Customer> customers = new List<Customer>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                customers = icustomer.getCustomers();
            }
            return View(customers);
        }

        public DbResult createOrEditCustomer(Customer customer)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                customer.c_cre_by = user.u_id;
                result = icustomer.createOrEditCustomer(customer);
            }

            return result;
        }

        public Customer getCustomer(int id)
        {
            Customer customer = new Customer();
            customer = icustomer.getCustomer(id);
            return customer;
        }

        public DbResult removeCustomer(int id )
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = icustomer.removeCustomer(id);
            }

            return result;
        }


        public ActionResult getCustomerTransactions(int c_id)
        {
            List<CustomerLedger> customerLedgers = new List<CustomerLedger>();

            customerLedgers = icustomer.getCustomerTransactions(c_id);
            var currency = Configuration.GetConnectionString("CurrencyFormat");
            var Purchased = customerLedgers.Where(x => x.cl_acc_type == "Debit").Sum(x => x.cl_amount);
            var Paid = customerLedgers.Where(x => x.cl_acc_type == "Credit").Sum(x => x.cl_amount);
            var ToBePaid = (Purchased-Paid);
            ViewBag.Purchased= Purchased;
            ViewBag.Paid= Paid;
            ViewBag.ToBePaid= ToBePaid;
            ViewBag.Currency = currency;
            return View(customerLedgers);
        }


        public ActionResult customerLedger()
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Customers = icustomer.getCustomers();
                return View();
            }
        }


        public ActionResult getCustomerLedger(int customer)
        {
            DataTable dataTable = new DataTable();

            dataTable = icustomer.getCustomerLedger(customer);

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