using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SIR.Controllers
{
    [Authorize]
    public class TIncidentReportingsController : Controller
    {
        private SIRModel db = new SIRModel();


        // GET: TIncidentReportings
        public ActionResult Index(string startDate, string endDate, string location, string injuryType, string department, string position)
        {
            var tIncidentReportings = db.TIncidentReportings.Include(t => t.TEmployee);
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                DateTime tempStartDate;
                if (DateTime.TryParse(startDate, out tempStartDate))
                {
                    tIncidentReportings = tIncidentReportings.Where(x => x.Date >= tempStartDate);
                }
            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                DateTime tempEndDate;
                if (DateTime.TryParse(endDate, out tempEndDate))
                {
                    tIncidentReportings = tIncidentReportings.Where(x => x.Date <= tempEndDate);
                }
            }
            if (!string.IsNullOrWhiteSpace(location))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.IncidentLocation == location);
            }
            if (!string.IsNullOrWhiteSpace(injuryType))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.InjuryType == injuryType);
            }
            if (!string.IsNullOrWhiteSpace(department))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.Department == department);
            }
            if (!string.IsNullOrWhiteSpace(position))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.Position == position);
            }
            return View(tIncidentReportings.ToList());
        }

        // GET: TIncidentReportings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIncidentReporting tIncidentReporting = db.TIncidentReportings.Find(id);
            if (tIncidentReporting == null)
            {
                return HttpNotFound();
            }
            return View(tIncidentReporting);
        }

        // GET: TIncidentReportings/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.TEmployees, "EmployeeID", "FirstName");
            return View();
        }

        // POST: TIncidentReportings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentID,EmployeeID,Shift,Position,ExpirienceMonth,IncidentLocation,Date,Time,Department,Description,Equipment,InjuryType,IncidentType,FirstAid,Investigator,ImageName,ImageData")] TIncidentReporting tIncidentReporting, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var imagedata = new TIncidentReporting
                    {
                        ImageName = System.IO.Path.GetFileName(upload.FileName)
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        imagedata.ImageData = reader.ReadBytes(upload.ContentLength);
                    }
                    tIncidentReporting.ImageData = imagedata.ImageData;
                    tIncidentReporting.ImageName = imagedata.ImageName;
                }
                db.TIncidentReportings.Add(tIncidentReporting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.TEmployees, "EmployeeID", "FirstName", tIncidentReporting.EmployeeID);
            return View(tIncidentReporting);
        }

        // GET: TIncidentReportings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIncidentReporting tIncidentReporting = db.TIncidentReportings.Find(id);

            if (tIncidentReporting == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmployeeID = new SelectList(db.TEmployees, "EmployeeID", "EmployeeID", tIncidentReporting.EmployeeID);
            return View(tIncidentReporting);
        }

        // POST: TIncidentReportings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentID,EmployeeID,Shift,Position,ExpirienceMonth,IncidentLocation,Date,Time,Department,Description,Equipment,InjuryType,IncidentType,FirstAid,Investigator,ImageName,ImageData")] TIncidentReporting tIncidentReporting, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var imagedata = new TIncidentReporting
                    {
                        ImageName = System.IO.Path.GetFileName(upload.FileName)
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        imagedata.ImageData = reader.ReadBytes(upload.ContentLength);
                    }
                    tIncidentReporting.ImageData = imagedata.ImageData;
                    tIncidentReporting.ImageName = imagedata.ImageName;
                }
                db.Entry(tIncidentReporting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.TEmployees, "EmployeeID", "EmployeeID", tIncidentReporting.EmployeeID);
            return View(tIncidentReporting);
        }

        // GET: TIncidentReportings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIncidentReporting tIncidentReporting = db.TIncidentReportings.Find(id);
            if (tIncidentReporting == null)
            {
                return HttpNotFound();
            }
            return View(tIncidentReporting);
        }

        // POST: TIncidentReportings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIncidentReporting tIncidentReporting = db.TIncidentReportings.Find(id);
            db.TIncidentReportings.Remove(tIncidentReporting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SendEmail(int id, SIR.Models.EmailFromModels model)
        {
            var result = User.Identity.GetUserId();
            AspNetUser aspNetuser = db.AspNetUsers.Find(result);

            model.IncidentID = id;
            model.FromEmail = aspNetuser.Email;
            model.FromName = aspNetuser.FirstName + " " + aspNetuser.LastName;
          
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(SIR.Models.EmailFromModels model)
        {
            if (ModelState.IsValid)
            {
                TIncidentReporting tIncidentReporting = db.TIncidentReportings.Find(model.IncidentID);
                if (tIncidentReporting == null)
                {
                    return HttpNotFound();
                }

                //var report = new SelectList(db.TEmployees, "EmployeeID", "EmployeeID", tIncidentReporting.EmployeeID);

                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                //var email1 = model.FromEmail;
                //message.To.Add(email1);
                message.To.Add(new MailAddress("trantn@mail.uc.edu"));  // replace with valid value 
                message.To.Add(new MailAddress("hiratssr@mail.uc.edu"));
                message.To.Add(new MailAddress("shakhusy@mail.uc.edu"));
                message.From = new MailAddress("sshakhurdin@outlook.com");  // replace with valid value         
                message.Subject = "Incident Report";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message = "IncidentID: " + tIncidentReporting.IncidentID + "<br />" +
                                                                                                    "FirstName: " + tIncidentReporting.TEmployee.FirstName + "<br />" +
                                                                                                    "LastName: " + tIncidentReporting.TEmployee.LastName + "<br />" +
                                                                                                    "IncidentLocation: " + tIncidentReporting.IncidentLocation + "<br />" +
                                                                                                    "Shift: " + tIncidentReporting.Shift + "<br />" +
                                                                                                    "Position: " + tIncidentReporting.Position + "<br />" +
                                                                                                    "ExpirienceMonth: " + tIncidentReporting.ExpirienceMonth + "<br />" +
                                                                                                    "Date: " + tIncidentReporting.Date + "<br />" +
                                                                                                    "Time: " + tIncidentReporting.Time + "<br />" +
                                                                                                    "Department: " + tIncidentReporting.Department + "<br />" +
                                                                                                    "Description: " + tIncidentReporting.Description + "<br />" +
                                                                                                    "FirstAid: " + tIncidentReporting.FirstAid + "<br />" +
                                                                                                    "Investigator: " + tIncidentReporting.Investigator + "<br />" +
                                                                                                    "InjuryType :" + tIncidentReporting.InjuryType + "<br />" +
                                                                                                    "Image :" + "<img style='width:30px; height:30px; ' src=\"data:image/jpeg;base64,"
                                                                                                                    + Convert.ToBase64String(tIncidentReporting.ImageData) + "\" />" + "<br />");



                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "sshakhurdin@outlook.com",  // replace with valid value
                        Password = "password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
