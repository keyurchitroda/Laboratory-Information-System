using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIS.UI.Helper;
namespace LIS.UI.Controllers
{

    public class RegisteruserController : Controller
    {
        private _IAllRepository<tbluser> userobj;
        private _IAllRepository<tblrole> roleobj;
        public RegisteruserController()
        {
            userobj = new ClassAllRepository<tbluser>();
            roleobj = new ClassAllRepository<tblrole>();
        }
        // GET: Register
        public ActionResult Index()
        {
            return View(userobj.GetAll());
        }

        // GET: Register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            var rolevar = roleobj.GetAll();
            ViewBag.list = new SelectList(rolevar, "roleid", "rolename");
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Create(tbluser user)
        {
            try
            {
                // TODO: Add insert logic here
                Encryption encyption = new Encryption();
                user.password = encyption.Encrypt(user.password);
                userobj.Insert(user);
                userobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int id)
        {
            var rolevar = roleobj.GetAll();
            ViewBag.list = new SelectList(rolevar, "roleid", "rolename");
            var user = userobj.GetById(id);

            return View(user);
        }

        // POST: Register/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbluser user)
        {
            try
            {
                // TODO: Add update logic here
                userobj.Update(user);
                userobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int id)
        {
            userobj.Delete(id);
            userobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Register/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
