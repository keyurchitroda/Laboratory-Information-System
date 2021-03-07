using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class RecordController : Controller
    {
        private _IAllRepository<tblrecord> recordobj;
        private _IAllRepository<tblpatient> patientobj;
        private _IAllRepository<tbldoctor> doctorobj;
        private _IAllRepository<tblcollector> collectorobj;
        private _IAllRepository<tbltestmaster> testmasterobj;

        public RecordController()
        {
            recordobj = new ClassAllRepository<tblrecord>();
            patientobj = new ClassAllRepository<tblpatient>();
            doctorobj = new ClassAllRepository<tbldoctor>();
            collectorobj = new ClassAllRepository<tblcollector>();
            testmasterobj = new ClassAllRepository<tbltestmaster>();
        }

        // GET: Record
        public ActionResult Index()
        {
            //ViewBag.testObj = testmasterobj.GetAll();
            return View(recordobj.GetAll());
        }

        // GET: Record/Details/5
        public ActionResult Details(int id)
        {
            return View(recordobj.GetById(id));
        }

        // GET: Record/Create
        public ActionResult Create()
        {
            var patient_data = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patient_data, "patientid", "patientfirstname");
            var doctor_data = doctorobj.GetAll();
            ViewBag.list2 = new SelectList(doctor_data, "doctorid", "doctorname");
            var collector_data = collectorobj.GetAll();
            ViewBag.list3 = new SelectList(collector_data, "collectorid", "collectorname");
            var testmaster_data = testmasterobj.GetAll();
            ViewBag.list4 = new SelectList(testmaster_data, "testid", "testname");
            return View();
        }

        // POST: Record/Create
        [HttpPost]
        public ActionResult Create(tblrecord record_para)
        {
            try
            {
                record_para.testid = string.Join(",", record_para.testid_Array);

                recordobj.Insert(record_para);
                recordobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Record/Edit/5
        public ActionResult Edit(int id)
        {
            var patient_data = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patient_data, "patientid", "patientfirstname");
            var doctor_data = doctorobj.GetAll();
            ViewBag.list2 = new SelectList(doctor_data, "doctorid", "doctorname");
            var collector_data = collectorobj.GetAll();
            ViewBag.list3 = new SelectList(collector_data, "collectorid", "collectorname");

            var abc = recordobj.GetById(id);
            abc.testid_Array = abc.testid.Split(',').ToArray();

            var testmaster_data = testmasterobj.GetAll();
            ViewBag.list4 = new SelectList(testmaster_data, "testid", "testname");
            return View(recordobj.GetById(id));
        }

        // POST: Record/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblrecord record_para)
        {
            try
            {
                record_para.testid = string.Join(",", record_para.testid_Array);

                recordobj.Update(record_para);
                recordobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Record/Delete/5
        public ActionResult Delete(int id)
        {
            recordobj.Delete(id);
            recordobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Record/Delete/5
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
