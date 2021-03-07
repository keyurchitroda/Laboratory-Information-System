using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class DoctorController : Controller
    {
        private _IAllRepository<tbldoctor> doctorobj;
        public DoctorController()
        {
            doctorobj = new ClassAllRepository<tbldoctor>();
        }

        // GET: Doctor
        public ActionResult Index()
        {
            return View(doctorobj.GetAll());
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            return View(doctorobj.GetById(id));
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(tbldoctor doctor_pera)
        {
            try
            {
                doctorobj.Insert(doctor_pera);
                doctorobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(doctorobj.GetById(id));
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tbldoctor doctor_pera)
        {
            try
            {
                doctorobj.Update(doctor_pera);
                doctorobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Delete/5
        public ActionResult Delete(int id)
        {
            doctorobj.Delete(id);
            doctorobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Doctor/Delete/5
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
