using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class RoleController : Controller
    {
        private _IAllRepository<tblrole> roleobj;

        public RoleController()
        {
            roleobj = new ClassAllRepository<tblrole>();
        }
        // GET: Role
        public ActionResult Index()
        {
            return View(roleobj.GetAll());
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View(roleobj.GetById(id));
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(tblrole role)
        {
            try
            {
                // TODO: Add insert logic here
                roleobj.Insert(role);
                roleobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            var role = roleobj.GetById(id);
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblrole role)
        {
            try
            {
                // TODO: Add update logic here
                roleobj.Update(role);
                roleobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            roleobj.Delete(id);
            roleobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Role/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
