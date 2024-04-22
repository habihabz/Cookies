using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cookies.Models;
using Cookies.Repository;
using Cookies.Services;

namespace Cookies.Controllers
{
    public class WorkflowController : Controller
    {
        private readonly IWorkflow iworkflow;
        private readonly IRole irole;
        private readonly IWorkflowDetail iworkflowDetail;
        private readonly IMenu imenu;

        public WorkflowController(IWorkflow _iworkflow, IRole _irole, IWorkflowDetail _iworkflowDetail,IMenu _imenu)
        {

            iworkflow = _iworkflow;
            irole = _irole;
            iworkflowDetail = _iworkflowDetail;
            imenu=_imenu;
        }

        public ActionResult Index(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.RoleList = (irole.GetRoles);
                return View(iworkflow.GetWorkflows);
            }
        }
        // GET: Workflow/Details/5
        public ActionResult Details(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(iworkflowDetail.GetWorkFlowDetailsByWorkFlow(id));
            }
        }

        // GET: Workflow/Create
        public ActionResult Create()
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

        // POST: Workflow/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    workflow.w_active_yn = "Y";
                    workflow.w_cre_by = getCurrentUser().u_id;
                    workflow.w_cre_date = DateTime.Now;
                    iworkflow.Add(workflow);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Workflow/Edit/5
        public ActionResult Edit(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iworkflow.GetWorkflow(id));
            }
        }

        // POST: Workflow/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    Workflow workflow1 = iworkflow.GetWorkflow(id);
                    workflow1.w_description = workflow.w_description;
                    iworkflow.Update(workflow1);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Workflow/Delete/5
        public ActionResult Delete(int id)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                return View(iworkflow.GetWorkflow(id));
            }
        }

        // POST: Workflow/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Workflow workflow)
        {
            if (getCurrentUser() == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {

                try
                {
                    iworkflow.Remove(id);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
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