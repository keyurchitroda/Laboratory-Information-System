using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{


    public class EquipmentController : Controller
    {
        private _IAllRepository<tblequipement> equipementobj;
        public EquipmentController()
        {
            equipementobj = new ClassAllRepository<tblequipement>();
        }
        // GET: Equipment
        public ActionResult AddSc()
        {
            if (Session["email"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "Login");
            }
        }
        public ActionResult Index()
        {
            return View(equipementobj.GetAll());
        }

        // GET: Equipment/Details/5
        public ActionResult Details(int id)
        {
            return View(equipementobj.GetById(id));
        }

        // GET: Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        public ActionResult Create(tblequipement equipement)
        {
            try
            {
                // TODO: Add insert logic here
                equipementobj.Insert(equipement);
                equipementobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Equipment/Edit/5
        public ActionResult Edit(int id)
        {
            var equipement = equipementobj.GetById(id);
            return View(equipement);
        }

        // POST: Equipment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblequipement equipement)
        {
            try
            {
                // TODO: Add update logic here
                equipementobj.Update(equipement);
                equipementobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Equipment/Delete/5
        public ActionResult Delete(int id)
        {
            equipementobj.Delete(id);
            equipementobj.Save();
            return RedirectToAction("Index");
        }

        //// POST: Equipment/Delete/5
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
