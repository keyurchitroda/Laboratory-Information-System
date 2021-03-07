using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIS.Model.Models;
using LIS.Model.Repository;

namespace LIS.UI.Controllers
{

    public class MachineController : Controller
    {
        private _IAllRepository<tblmachine> machineobj;

        public MachineController()
        {
            machineobj = new ClassAllRepository<tblmachine>();
        }
        // GET: Machine
        public ActionResult Index()
        {
            return View(machineobj.GetAll());
        }

        // GET: Machine/Details/5
        public ActionResult Details(int id)
        {
            return View(machineobj.GetById(id));
        }

        // GET: Machine/Create
        public ActionResult Create()
        {
            return View(new tblmachine());
        }

        // POST: Machine/Create
        [HttpPost]
        public ActionResult Create(tblmachine machine, HttpPostedFileBase template)
        {
            try
            {
                // TODO: Add insert logic here
                if (template.ContentLength > 0)
                {

                    string ext = Path.GetExtension(template.FileName);
                    string filename = Guid.NewGuid() + ext;
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    machine.template = "~/UploadedFiles/" + filename;
                    template.SaveAs(_path);
                }
                machineobj.Insert(machine);
                machineobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public FileResult Download(string filename)
        {
            string ext = Path.GetExtension(filename);
            filename = Path.GetFileName(filename);

            string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/", filename);
            byte[] fileByte = System.IO.File.ReadAllBytes(_path);
            return File(fileByte, System.Net.Mime.MediaTypeNames.Application.Octet, "Machine" + ext);

        }

        // GET: Machine/Edit/5
        public ActionResult Edit(int id)
        {
            var machine = machineobj.GetById(id);
            return View(machine);
        }

        // POST: Machine/Edit/5
        [HttpPost]
        public ActionResult Edit(tblmachine machine, HttpPostedFileBase template)
        {
            try
            {
                // TODO: Add update logic here
                if (template != null && template.ContentLength > 0)
                {

                    string ext = Path.GetExtension(template.FileName);
                    string filename = Guid.NewGuid() + ext;
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    machine.template = "~/UploadedFiles/" + filename;
                    template.SaveAs(_path);
                }


                machineobj.Update(machine);
                machineobj.Save();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Machine/Delete/5
        public ActionResult Delete(int id)
        {
            machineobj.Delete(id);
            machineobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Machine/Delete/5
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
