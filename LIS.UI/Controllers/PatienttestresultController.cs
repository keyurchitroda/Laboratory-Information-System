using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class PatienttestresultController : Controller
    {
        private _IAllRepository<tblpatienttestresult> patienttestresultobj;

        private _IAllRepository<tblrecord> recordobj;

        private _IAllRepository<tblpatient> patientobj;
        private _IAllRepository<tbltestmaster> testmasterobj;
        public _IAllRepository<tblanalytic> analyticobj;

        public PatienttestresultController()
        {
            patienttestresultobj = new ClassAllRepository<tblpatienttestresult>();
                    //if using recordtable
            recordobj = new ClassAllRepository<tblrecord>();

            patientobj = new ClassAllRepository<tblpatient>();
            testmasterobj = new ClassAllRepository<tbltestmaster>();
            analyticobj = new ClassAllRepository<tblanalytic>();
        }

        // GET: Patienttestresult
        public ActionResult Index()
        {
            return View(patienttestresultobj.GetAll());
        }

        // GET: Patienttestresult/Details/5
        public ActionResult Details(int id, DateTime pdate)
        {
            //var d = Convert.ToDateTime(pdate);
            return View(patienttestresultobj.GetAll().Where(p => p.patientid == id).Where(p => p.date == pdate).ToList());
        }

        // GET: Patienttestresult/Create
        public ActionResult Create()
        {
            var patient_data = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patient_data, "patientid", "patientfirstname");
            //var testmaster_data = testmasterobj.GetAll();
            //ViewBag.list2 = new SelectList(testmaster_data, "testid", "testname");

            //if using recordtable
            ////var patient_data = recordobj.GetAll();
            ////ViewBag.list1 = new SelectList(patient_data, "recordid", "patientid");
            ////var testmaster_data = recordobj.GetAll();
            ////ViewBag.list2 = new SelectList(testmaster_data, "recordid", "testid");

            //var analytic_data = analyticobj.GetAll();
            //ViewBag.list3 = new SelectList(analytic_data, "analyticid", "diagnosisitem");
            return View();
        }

        // POST: Patienttestresult/Create
        [HttpPost]
        public ActionResult Create(tblpatienttestresult patienttestresult_pera, string testid , string []analytic , string []reading)
        {
            try
            {
                
                int[] analytiIntArray  = Array.ConvertAll(analytic, int.Parse);
                //int[] readingIntArray = Array.ConvertAll(reading, int.Parse);

                for(var i = 0; i < analytiIntArray.Length; i++)
                {
                    tblpatienttestresult tmpobj = new tblpatienttestresult();
                    tmpobj.patientid = patienttestresult_pera.patientid;
                    tmpobj.testid = Convert.ToInt32(testid);
                    tmpobj.analyticid = analytiIntArray[i];
                    tmpobj.reading = reading[i];
                    tmpobj.date = patienttestresult_pera.date;

                    patienttestresultobj.Insert(tmpobj);
                    patienttestresultobj.Save();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patienttestresult/Edit/5
        public ActionResult Edit(int id)
        {
            var patient_data = patientobj.GetAll();
            ViewBag.list1 = new SelectList(patient_data, "patientid", "patientfirstname");
            var testmaster_data = testmasterobj.GetAll();
            ViewBag.list2 = new SelectList(testmaster_data, "testid", "testname");
            var analytic_data = analyticobj.GetAll();
            ViewBag.list3 = new SelectList(analytic_data, "analyticid", "diagnosisitem");
            return View(patienttestresultobj.GetById(id));
        }

        // POST: Patienttestresult/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tblpatienttestresult patienttestresult_pera)
        {
            try
            {
                patienttestresultobj.Update(patienttestresult_pera);
                patienttestresultobj.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patienttestresult/Delete/5
        public ActionResult Delete(int id,DateTime pdate)
        {
            //patienttestresultobj.GetAll().Where(p => p.patientid == id).Where(p => p.date == pdate).ToList();
            var delete_data= patienttestresultobj.GetAll().Where(p => p.patientid == id).Where(p => p.date == pdate).ToList();
            var recordid_array = delete_data.Select(r => r.patienttestresultid).ToList();
            for (int i = 0; i < recordid_array.Count; i++)
            {
                patienttestresultobj.Delete(Convert.ToInt32(recordid_array[i]));
                patienttestresultobj.Save();
            }
            return RedirectToAction("Index");
        }

        //// POST: Patienttestresult/Delete/5
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


        public JsonResult getTestFromPatientId(int id)
        {
            var data = recordobj.GetAll().Where(d => d.patientid == id);
            var chk = data.Select(t => t.testid);
            var chkarrayString = chk.ToList();

            var anotherTmp = chkarrayString[0];

            String[] testidArray = anotherTmp.Split(',');

            int[] ints = Array.ConvertAll(testidArray, int.Parse);

            List<tbltestmaster> returnArray = new List<tbltestmaster>();

            for (var i = 0; i < ints.Length ; i++)
            {
                var tbltestmaster = testmasterobj.GetById(Convert.ToInt32(ints[i]));
                returnArray.Add(new tbltestmaster() { testid = Convert.ToInt32(ints[i]), testname = tbltestmaster.testname });
            }

            return Json(returnArray.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAnalyticsFromTest(int id)
        {
            var data = testmasterobj.GetById(id);
            string AnalyticString = data.analyticlist;
            string[] AnalyticStringArray = AnalyticString.Split(',');
            int[] AnalyticIntArray = Array.ConvertAll(AnalyticStringArray, int.Parse);

            List<tblanalytic> returnArray = new List<tblanalytic>();

            for (var i=0; i < AnalyticIntArray.Length; i++)
            {
                var tmpanalytic = analyticobj.GetById(AnalyticIntArray[i]);
                returnArray.Add( 
                    new tblanalytic() { 
                        analyticid = Convert.ToInt32(tmpanalytic.analyticid),
                        diagnosisitem = tmpanalytic.diagnosisitem,
                        value = tmpanalytic.value, 
                        minvalue = tmpanalytic.minvalue,
                        maxvalue = tmpanalytic.maxvalue,
                        measurementunit = tmpanalytic.measurementunit
                    }
                );
            }

            return Json(returnArray.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
