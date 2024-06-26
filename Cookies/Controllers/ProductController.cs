﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cookies.Models;
using Cookies.Services;

namespace Cookies.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRole irole;
        private readonly IMenu imenu;
        private readonly IProduct iproduct;
        private readonly IPrice iprice;
        public ProductController(IRole _irole,IMenu _imenu, IProduct _iproduct, IPrice _iprice)
        {
            irole = _irole;
            imenu = _imenu;
            iproduct = _iproduct;
            iprice = _iprice;
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
                return View();
            }
        }
        public ActionResult getProducts()
        {
            List<Product> products = new List<Product>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                products = iproduct.GetProducts();
            }
            return View(products);
        }

        public ActionResult GetActivePricesForAProduct(int prod_id)
        {
            List<Price> prices = new List<Price>();
            var user = getCurrentUser();
            if (user == null)
            {
                ViewBag.Message = "Session Expired !!";
            }
            else
            {
                prices = iprice.GetActivePricesForAProduct(prod_id);
            }

            return View(prices);
        }

        public DbResult AddProductPrice(Price price)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                price.pr_cre_by = user.u_id;
                result = iprice.AddProductPrice(price);
            }

            return result;
        }

        public DbResult createOrEditProduct(Product product)
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                product.p_cre_by = user.u_id;
                result = iproduct.createOrEditProduct(product);
            }

            return result;
        }

        public Product getProduct(int id)
        {
            Product product = new Product();
            product = iproduct.GetProduct(id);
            return product;
        }

        public DbResult removeProduct(int id )
        {
            DbResult result = new DbResult();
            var user = getCurrentUser();
            if (user == null)
            {
                result.Message = "Session is Expired !!";
            }
            else
            {
                result = iproduct.removeProduct(id);
            }

            return result;
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