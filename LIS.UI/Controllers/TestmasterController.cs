using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class TestmasterController : Controller
    {
        private _IAllRepository<tbltestmaster> testmasterobj;
        public _IAllRepository<tblanalytic> analyticobj;

        public TestmasterController()
        {
            testmasterobj = new ClassAllRepository<tbltestmaster>();
            analyticobj = new ClassAllRepository<tblanalytic>();
        }

        // GET: Testmaster
        public ActionResult Index()
        {
            return View(testmasterobj.GetAll());
        }

        // GET: Testmaster/Details/5
        public ActionResult Details(int id)
        {
            return View(testmasterobj.GetById(id));
        }

        // GET: Testmaster/Create
        public ActionResult Create()
        {
            var analytic_data = analyticobj.GetAll();
            ViewBag.list1 = new SelectList(analytic_data, "analyticid", "diagnosisitem");
            return View();
        }

        
        // POST: Testmaster/Create
        [HttpPost]
        public ActionResult Create(tbltestmaster testmaster_pera)
        {
            try
            {
                testmaster_pera.analyticlist = string.Join(",", testmaster_pera.analytic_idArr);

                testmasterobj.Insert(testmaster_pera);
                testmasterobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Testmaster/Edit/5
        public ActionResult Edit(int id)
        {
            var abc = testmasterobj.GetById(id);
            abc.analytic_idArr = abc.analyticlist.Split(',').ToArray();

            var analytic_data = analyticobj.GetAll();
            ViewBag.list1 = new SelectList(analytic_data, "analyticid", "diagnosisitem");
            return View(testmasterobj.GetById(id));
        }

        // POST: Testmaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbltestmaster testmaster_pera)
        {
            try
            {
                testmaster_pera.analyticlist = string.Join(",", testmaster_pera.analytic_idArr);

                testmasterobj.Update(testmaster_pera);
                testmasterobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Testmaster/Delete/5
        public ActionResult Delete(int id)
        {
            testmasterobj.Delete(id);
            testmasterobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Testmaster/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        testmasterobj.Delete(id);
        //        testmasterobj.Save();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
