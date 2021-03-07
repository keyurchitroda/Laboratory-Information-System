using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{

    public class SupplierController : Controller
    {
        private _IAllRepository<tblsupplier> supplierobj;

        public SupplierController()
        {
            supplierobj = new ClassAllRepository<tblsupplier>();
        }
        // GET: Supplier
        public ActionResult Index()
        {
            return View(supplierobj.GetAll());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View(supplierobj.GetById(id));
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(tblsupplier supplier)
        {
            try
            {
                // TODO: Add insert logic here
                supplierobj.Insert(supplier);
                supplierobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var supplier = supplierobj.GetById(id);
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblsupplier supplier)
        {
            try
            {
                // TODO: Add update logic here
                supplierobj.Update(supplier);
                supplierobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            supplierobj.Delete(id);
            supplierobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Supplier/Delete/5
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
