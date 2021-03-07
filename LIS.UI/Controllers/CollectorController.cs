using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class CollectorController : Controller
    {
        private _IAllRepository<tblcollector> collectorobj;
        public CollectorController()
        {
            collectorobj = new ClassAllRepository<tblcollector>();
        }

        // GET: Collector
        public ActionResult Index()
        {
            return View(collectorobj.GetAll());
        }

        // GET: Collector/Details/5
        public ActionResult Details(int id)
        {
            return View(collectorobj.GetById(id));
        }

        // GET: Collector/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collector/Create
        [HttpPost]
        public ActionResult Create(tblcollector collector_pera)
        {
            try
            {
                collectorobj.Insert(collector_pera);
                collectorobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Collector/Edit/5
        public ActionResult Edit(int id)
        {
            return View(collectorobj.GetById(id));
        }

        // POST: Collector/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblcollector collector_pera)
        {
            try
            {
                collectorobj.Update(collector_pera);
                collectorobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Collector/Delete/5
        public ActionResult Delete(int id)
        {
            collectorobj.Delete(id);
            collectorobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Collector/Delete/5
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
