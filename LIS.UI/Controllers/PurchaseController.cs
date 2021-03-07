using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{

    public class PurchaseController : Controller
    {
        private _IAllRepository<tblpurchase> purchaseobj;
        private _IAllRepository<tblequipement> equipementobj;
        private _IAllRepository<tblsupplier> supplierobj;
        private _IAllRepository<tblpurchase> purobj;

        public PurchaseController()
        {
            purchaseobj = new ClassAllRepository<tblpurchase>();
            equipementobj = new ClassAllRepository<tblequipement>();
            supplierobj = new ClassAllRepository<tblsupplier>();
            purobj = new ClassAllRepository<tblpurchase>();


        }
        // GET: Purchase
        public ActionResult Index()
        {
            return View(purchaseobj.GetAll());
        }

        // GET: Purchase/Details/5
        public ActionResult Details(int id)
        {
            return View(purchaseobj.GetById(id));
        }

        // GET: Purchase/Create
        public ActionResult Create()
        {
            var equipementvar = equipementobj.GetAll();
            ViewBag.list = new SelectList(equipementvar, "equipementid", "equipementname");
            var suppliervar = supplierobj.GetAll();
            ViewBag.list1 = new SelectList(suppliervar, "supplierid", "suppliername");

            return View();
        }

        // POST: Purchase/Create
        [HttpPost]
        public ActionResult Create(tblpurchase purchase)
        {
            try
            {
                tblpurchase purch = new tblpurchase();

                // TODO: Add insert logic here
                purchaseobj.Insert(purchase);
                purchaseobj.Save();
                int equipementid = Convert.ToInt32(purchase.equipementid);
                tblequipement equipobj = equipementobj.GetById(equipementid);
                equipobj.quantity += purchase.quantity;
                equipementobj.Update(equipobj);
                equipementobj.Save();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(int id)
        {
            var equipementvar = equipementobj.GetAll();
            ViewBag.list = new SelectList(equipementvar, "equipementid", "equipementname");
            var suppliervar = supplierobj.GetAll();
            ViewBag.list1 = new SelectList(suppliervar, "supplierid", "suppliername");
            var purchase = purchaseobj.GetById(id);
            return View(purchase);
        }

        // POST: Purchase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblpurchase purchase)
        {
            try
            {
                tblpurchase tblpurchaseobj = purobj.GetById(purchase.purchaseid);

                long quantity = Convert.ToInt64(tblpurchaseobj.quantity);


                quantity = Convert.ToInt64(purchase.quantity) - quantity;
                purobj.Save();

                // TODO: Add update logic here
                purchaseobj.Update(purchase);
                purchaseobj.Save();

                tblequipement equipobj = equipementobj.GetById(Convert.ToInt32(purchase.equipementid));
                equipobj.quantity = equipobj.quantity + quantity;
                equipementobj.Update(equipobj);
                equipementobj.Save();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Purchase/Delete/5
        public ActionResult Delete(int id)
        {
            purchaseobj.Delete(id);
            purchaseobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Purchase/Delete/5
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
