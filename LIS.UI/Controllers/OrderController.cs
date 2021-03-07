using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{

    public class OrderController : Controller
    {
        private _IAllRepository<tblorder> orderobj;
        private _IAllRepository<tblpatient> patientobj;
        private _IAllRepository<tblequipement> equipementobj;
        private _IAllRepository<tblorder> ordobj;

        public OrderController()
        {
            orderobj = new ClassAllRepository<tblorder>();
            patientobj = new ClassAllRepository<tblpatient>();
            equipementobj = new ClassAllRepository<tblequipement>();
            ordobj = new ClassAllRepository<tblorder>();


        }
        // GET: Order
        public ActionResult Index()
        {
            return View(orderobj.GetAll());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View(orderobj.GetById(id));
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var patientvar = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patientvar, "patientid", "patientfirstname");
            var equipementvar = equipementobj.GetAll();
            ViewBag.list = new SelectList(equipementvar, "equipementid", "equipementname");

            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(tblorder order)
        {
            try
            {
                int equipementid = Convert.ToInt32(order.equipementid);
                tblequipement equipobj = equipementobj.GetById(equipementid);
                // TODO: Add insert logic here
                if (order.quantity < equipobj.quantity)
                {
                    orderobj.Insert(order);
                    orderobj.Save();
                    equipobj.quantity -= order.quantity;
                    equipementobj.Update(equipobj);
                    equipementobj.Save();
                }
                else
                {
                    TempData["Message"] = "Out Of Stock";

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            var patientvar = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patientvar, "patientid", "patientfirstname");
            var equipementvar = equipementobj.GetAll();
            ViewBag.list = new SelectList(equipementvar, "equipementid", "equipementname");
            var order = orderobj.GetById(id);
            return View(order);

        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblorder order)
        {
            try
            {
                tblorder tblorderobj = ordobj.GetById(order.orderid);

                long quantity = Convert.ToInt64(tblorderobj.quantity);


                quantity = Convert.ToInt64(order.quantity) - quantity;
                ordobj.Save();
                // TODO: Add update logic here
                orderobj.Update(order);
                orderobj.Save();

                tblequipement equipobj = equipementobj.GetById(Convert.ToInt32(order.equipementid));
                equipobj.quantity = equipobj.quantity - quantity;
                equipementobj.Update(equipobj);
                equipementobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            orderobj.Delete(id);
            orderobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Order/Delete/5
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
