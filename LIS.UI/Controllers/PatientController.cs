using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class PatientController : Controller
    {
        private _IAllRepository<tblpatient> patientobj;
        public PatientController()
        {
            patientobj = new ClassAllRepository<tblpatient>();
        }

        // GET: Patient
        public ActionResult Index()
        {
            return View(patientobj.GetAll());
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            return View(patientobj.GetById(id));
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(tblpatient patient_para)
        {
            try
            {
                patientobj.Insert(patient_para);
                patientobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            var patient_data = patientobj.GetById(id);
            return View(patient_data);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblpatient patient_para)
        {
            try
            {
                patientobj.Update(patient_para);
                patientobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            patientobj.Delete(id);
            patientobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Patient/Delete/5
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
