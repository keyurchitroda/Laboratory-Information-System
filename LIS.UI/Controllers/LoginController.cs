using LIS.Model.Models;
using LIS.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LIS.Model.Repository;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace LIS.UI.Controllers
{
    public class LoginController : Controller
    {
        private _IAllRepository<tbluser> userobj;
        public LoginController()
        {
            userobj = new ClassAllRepository<tbluser>();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(tbluser objUser)
        {
            if (ModelState.IsValid)
            {
                using (LissystemEntities db = new LissystemEntities())
                {
                    Encryption encyption = new Encryption();
                    //string passencry= encyption.Encrypt(objUser.password);
                    objUser.password = encyption.Encrypt(objUser.password);
                    var obj = db.tblusers.Where(a => a.email.Equals(objUser.email) && a.password.Equals(objUser.password)).FirstOrDefault();
                    if (obj != null && obj.isactive != null && obj.isactive == true)
                    {
                        Session["userid"] = obj.userid.ToString();
                        Session["email"] = obj.email.ToString();
                        return RedirectToAction("Index", "Equipment", new { area = "" });
                    }
                    else
                    {
                        ViewBag.message = "Invalid username or password";
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["userid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            return RedirectToAction("Index");
        }

        public ActionResult resetpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resetpassword(tbluser model)
        {
            using (var context = new LissystemEntities())
            {
                Encryption encyption = new Encryption();
                String mail = model.email;



                String pass = context.tblusers.Where(x => x.email.Equals(model.email)).Select(x => x.password).FirstOrDefault();
                String depass = encyption.Decrypt(pass);
                ViewBag.msg = "We Send Your Register Password in your Registered Email Address";

                var fromMail = new MailAddress("lisprojectsystem@gmail.com", "LIS System"); // set your email  
                var fromEmailpassword = "Lis@1234"; // Set your password   
                var toEmail = new MailAddress(mail);

                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

                var Message = new MailMessage(fromMail, toEmail);
                Message.Subject = "Password Forgot Request";
                Message.Body = "<br/> We Got Your email and Password." +
                               "<br/> please Remember this Login info and Login." +
                               "<br/><br/> Email : " + mail + "<br>" + "Password :" + depass;
                Message.IsBodyHtml = true;
                smtp.Send(Message);

            }
            return RedirectToAction("Index");
        }

    }
}
