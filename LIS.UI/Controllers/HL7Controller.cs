using LIS.Model.Models;
using LIS.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LIS.UI.Controllers
{
    public class HL7Controller : Controller
    {

        private _IAllRepository<tblpatient> patientobj;
        private _IAllRepository<tblpatienttestresult> patienttestresultobj;
        private _IAllRepository<tblanalytic> analyticobj;
        private _IAllRepository<tblrecord> recordobj;
        private _IAllRepository<tbldoctor> doctorobj;

        public HL7Controller()
        {
            patientobj = new ClassAllRepository<tblpatient>();
            patienttestresultobj = new ClassAllRepository<tblpatienttestresult>();
            analyticobj = new ClassAllRepository<tblanalytic>();
            recordobj= new ClassAllRepository<tblrecord>();
            doctorobj= new ClassAllRepository<tbldoctor>();
        }

        // GET: HL7
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetHL7()
        {
            
            return View();
        }

        public JsonResult GetAllPatient()
        {
            var patientData = patientobj.GetAll();
            var sendData = patientData.Select(s => new { s.patientid, s.patientfirstname,s.patientmiddelname,s.patientlastname });
            return Json(sendData.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcessHL7(int id)
        {
            string defaultHl7 = $"MSH|^~&|LAB|MYFAC|LAB||201411130917|| ORU^R01|3216598|D|2.3|||AL|NE|<br>";



            var rec = recordobj.GetAll().Where(p => p.patientid == id);
            var did = rec.Select(d => d.doctorid).ToList()[0];

            var dname = doctorobj.GetById(Convert.ToInt32(did)).doctorname;
            var dd = doctorobj.GetById(Convert.ToInt32(did)).designation;
            var phno = doctorobj.GetById(Convert.ToInt32(did)).doctormoblienumber;

            var patientData = patientobj.GetById(id);
            defaultHl7 += $"PID|{patientData.patientid}|{patientData.patientid}|{patientData.patientid}|{patientData.patientid}|{patientData.patientlastname}^{patientData.patientfirstname}^{patientData.patientmiddelname}||19670202|No|||4505 21 st^^LAKE COUNTRY^BC^V4V 2S7||{patientData.patientmobllienumber}|||||MF0050356/15|<br>";
            defaultHl7 += $"PV1|1|O|India||||^{dname}^{dd}^^^^^-{phno}|||||||||||REF||SELF|||||||||||||||||||MYFAC||REG|||201411071440||||||||23390^PV1_52Surname^PV1_52Given^H^^Dr^^PV1_52Mnemonic|<br>";
            defaultHl7 += $"ORC|RE|PT103933301.0100|||CM|N|||201411130917|^Kyle^Andra^J.^^^^KYLA||^Xavarie^Sonna^^^^^XAVS|MYFAC|<br>";
            defaultHl7 += $"OBR | 1 | PT1311:H00001R301.0100 | PT1311:H00001R | 301.0100 ^ Complete Blood Count(CBC)^00065227 ^ 57021 - 8 ^ CBC Auto Differential^pCLOCD | R || 201411130914 ||| KYLA |||| 201411130914 || ^Xavarie ^ Sonna ^ ^^^^XAVS || 00065227 |||| 201411130915 || LAB | F || ^^^^^R | ^Xavarie ^ Sonna ^ ^^^^XAVS |<br>";

            var resultData = patienttestresultobj.GetAll().Where(p => p.patientid == id);

            var chk = resultData.ToList();

            for(int i=0; i < chk.Count; i++)
            {
                var aID = Convert.ToInt32(chk[i].analyticid);
                var analyticName = analyticobj.GetById(aID).diagnosisitem;
                var patienttestid = Convert.ToInt32(chk[i].patienttestresultid);
                var testid = Convert.ToInt32(chk[i].testid);
                var analyticRange = analyticobj.GetById(aID).minvalue +" - "+ analyticobj.GetById(aID).maxvalue;
                defaultHl7 += $"OBX|1|NM|{testid}^{analyticName}^-^-^ - ^pCLOCD|1|{chk[i].reading}|{patienttestid}|{analyticRange}|H||A~S|Yes|||201411130916|MYFAC^MyFake Hospital^L|<br>";
            }
            return Json(defaultHl7, JsonRequestBehavior.AllowGet);
        }
    }
}