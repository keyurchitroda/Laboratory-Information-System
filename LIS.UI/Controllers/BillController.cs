using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{

    public class BillController : Controller
    {
        private _IAllRepository<tblbill> billobj;
        private _IAllRepository<tblpatient> patientobj;
        private _IAllRepository<tblrecord> recordobj;
        private _IAllRepository<tbltestmaster> testmasterobj;
        public BillController()
        {
            billobj = new ClassAllRepository<tblbill>();
            patientobj = new ClassAllRepository<tblpatient>();
            recordobj = new ClassAllRepository<tblrecord>();
            testmasterobj = new ClassAllRepository<tbltestmaster>();

        }
        // GET: Bill
        public ActionResult Index()
        {
            return View(billobj.GetAll());
        }

        // GET: Bill/Details/5
        public ActionResult Details(int id)
        {
            //    


            return View(billobj.GetById(id));
        }

        // GET: Bill/Create
        public ActionResult Create()
        {
            LissystemEntities db = new LissystemEntities();
            var patientvar = patientobj.GetAll();
            ViewBag.list = new SelectList(patientvar, "patientid", "patientfirstname");

            return View();
        }

        // POST: Bill/Create
        [HttpPost]
        public ActionResult Create(tblbill bill)
        {
            try
            {
                // TODO: Add insert logic here
                billobj.Insert(bill);
                billobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Edit/5
        public ActionResult Edit(int id)
        {
            var patientvar = patientobj.GetAll();
            ViewBag.list = new SelectList(patientvar, "patientid", "patientfirstname");
            var bill = billobj.GetById(id);
            return View(bill);

        }

        // POST: Bill/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblbill bill)
        {
            try
            {
                // TODO: Add update logic here
                billobj.Update(bill);
                billobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bill/Delete/5
        public ActionResult Delete(int id)
        {
            billobj.Delete(id);
            billobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Bill/Delete/5
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

        public JsonResult getRecordIdFromPatient(int id)
        {
            var data = recordobj.GetAll().Where(d => d.patientid == id);
            var chk = data.Select(t => t.testid);
            var chkarrayString = chk.ToList();

            var anotherTmp = chkarrayString[0];

            String[] testidArray = anotherTmp.Split(',');

            int[] ints = Array.ConvertAll(testidArray, int.Parse);

            List<tbltestmaster> returnArray = new List<tbltestmaster>();



            for (var i = 0; i < ints.Length; i++)
            {
                var tbltestmaster = testmasterobj.GetById(Convert.ToInt32(ints[i]));
                returnArray.Add(new tbltestmaster() { testid = Convert.ToInt32(ints[i]), testname = tbltestmaster.testname, testprice = tbltestmaster.testprice });
            }

            return Json(returnArray.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}

