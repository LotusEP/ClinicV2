using clinic.Models;

using System;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using ClinicV2.Models;

/// <summary>
///----------------------------- For sign up-----------------------------------------------------
/// the function you are looking for it test(), it is use for signup for now
/// test fuction will load up the signup form, one the submit button is click than the controller will look for the signup() 
/// if everything go well it will call back test() to load back up the page
/// -------------------------- For requirement -----------------------------------------------
/// look for getrequirement(), it send a -10 value to the criteria model to load all criteria
/// CreateReq() create new criteria, not done yet
/// 
/// -----------------for clinic-------------------------
/// look for listoclinic() and clinicadd(), it self explain
/// 
/// --------------Model------------------
/// clinicmodel for clinic
/// patient model for patient
/// criteria model for criteria
/// the rest of the model is uselesss
/// 
/// -------------views-------------------------
/// 
/// the main view you want it clinicadd, createreq, index,listofclinc,test,getrequirement, any thing with 'x' in front is useless
/// 
/// -----------server--------------
/// 
/// hostname : clinicsystemdb.cfkpw0ap0abf.us-east-1.rds.amazonaws.com
/// username: Lotusep5ep
/// port: 3306
/// password: Pat123forsell
/// </summary>



namespace ClinicV2.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }

        //2nd version of email ---------------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public ActionResult Signup()
        {
            List<clinicModel> listofClinic = clinicModel.GetClinicList();


            ViewBag.Collection = listofClinic;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupModel newSignee, string [] clinicName)
        {

            //foreach (var mail in clinicT)
            //{
            //    if (mail)
            //}
            //string result = collection["clinicT"];

            Patient patient = newSignee.newPatient;
            string message = "Patient Email: " + patient.Email + "\n" + "Patient Phone Number: " + patient.CellPhone + "\n" + "Patient Address: " + patient.Street
           + "\n" + patient.City + "\n" + patient.State + "\n" + patient.Zip;



            try
            {
                if (ModelState.IsValid)
                {
                    var mess = new MailMessage();
                    var senderEmail = new MailAddress("--Email that will be sending the mail", "the name of the sender--");
                    var receiverEmail = new MailAddress(patient.Email, "Receiver");
                    var password = "--Password of the email--";
                    var sub = "--the subject--";
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp-mail.outlook.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };

                    mess.From = senderEmail;
                    mess.To.Add(receiverEmail);
                    //for (int i = 0; i < clinicName.Length; i++)
                    //{
                    //    if (clinicName != null)
                    //    {
                    //        mess.Bcc.Add(clinicName[i]);
                    //    }

                    //}
                    mess.Subject = sub;
                    mess.Body = message;



                    using (smtp)
                    {
                        smtp.Send(mess);
                    }

                    Patient.AddPatient(patient);


                    return RedirectToAction("test");
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("test");

        }

        [HttpGet]
        public ActionResult listofClinic()
        {
            List<clinicModel> listofClinic = clinicModel.GetClinicList();


            return View(listofClinic);
        }


        [HttpGet]
        public ActionResult ClinicAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ClinicAdd(clinicModel clinic)
        {

            return View();
        }
        [HttpGet]
        public ActionResult GetRequirement()
        {
            List<Criteria>Req = Criteria.GetReqList(-10);
        
            return View(Req);
        }

        [HttpGet]
        public ActionResult CreateReq()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateReq(Requirement req)
        {
            req.CreateReq(req);
            return RedirectToAction("GetRequirement");
        }

        public ActionResult test()
        {
            SignupModel tModel = new SignupModel();
            tModel.listofClinic = clinicModel.GetClinicList();
            tModel.newPatient = new Patient();


            return View(tModel);
        }

     
    }
}