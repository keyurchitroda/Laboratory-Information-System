using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class AnalyticController : Controller
    {

        private _IAllRepository<tblanalytic> analyticobj;
        public AnalyticController()
        {
            analyticobj = new ClassAllRepository<tblanalytic>();
        }

        // GET: Analytic
        public ActionResult Index()
        {
            return View(analyticobj.GetAll());
        }

        // GET: Analytic/Details/5
        public ActionResult Details(int id)
        {
            return View(analyticobj.GetById(id));
        }

        // GET: Analytic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Analytic/Create
        [HttpPost]
        public ActionResult Create(tblanalytic analytic_pera)
        {
            try
            {
                analyticobj.Insert(analytic_pera);
                analyticobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analytic/Edit/5
        public ActionResult Edit(int id)
        {
            return View(analyticobj.GetById(id));
        }

        // POST: Analytic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblanalytic analytic_pera)
        {
            try
            {
                analyticobj.Update(analytic_pera);
                analyticobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Analytic/Delete/5
        public ActionResult Delete(int id)
        {
            analyticobj.Delete(id);
            analyticobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Analytic/Delete/5
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
