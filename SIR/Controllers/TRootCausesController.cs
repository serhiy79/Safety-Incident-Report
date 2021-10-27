using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIR;
using System.Diagnostics;
using AutoMapper;

namespace SIR.Controllers
{
    [Authorize]
    public class TRootCausesController : Controller
    {
        private SIRModel db = new SIRModel();
       
        // GET: TRootCauses
        public ActionResult Index()
        {
            var tRootCause = db.TRootCauses.Include(t => t.TIncidentReporting);
            return View(tRootCause.ToList());
        }

        // GET: TRootCauses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRootCause tRootCause = db.TRootCauses.Find(id);
            if (tRootCause == null)
            {
                return HttpNotFound();
            }
            return View(tRootCause);
        }

        // GET: TRootCauses/Create
        public ActionResult Create()
        {
            ViewBag.Incidents = db.TIncidentReportings;

            TRootCause tRootCause = new TRootCause();
            tRootCause.TWhies = new List<TWhy>();
            for (int i = 0; i < 5; i++)
            {
                tRootCause.TWhies.Add(new TWhy { Ordering = i + 1 });
            }
            return View(tRootCause);
        }

        // POST: TRootCauses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RootCauseID,IncidentID,TWhies,Description,RootCauseName,Verified,VerifiedNote")] TRootCause tRootCause)
        {
            //db.Configuration.AutoDetectChangesEnabled = false;
            if (ModelState.IsValid)
            {
                tRootCause = db.TRootCauses.Add(tRootCause);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //else // for Debugging
            //{
            //    var errors = ModelState.Values.SelectMany(v => v.Errors);
            //    foreach (var item in errors)
            //    {
            //        Debug.WriteLine(item.ErrorMessage);
            //    }
            //}

            ViewBag.Incidents = db.TIncidentReportings;
            return View(tRootCause);
        }
    
     
        // GET: TRootCauses/Edit/5
        public ActionResult Edit(int? id)
        {
            TempData["ID"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TRootCause tRootCause = db.TRootCauses.Find(id);
            //TRootCause tRootCause = db.TRootCauses.AsNoTracking().Where(x => x.RootCauseID == id).FirstOrDefault();
            
            if (tRootCause == null)
            {
                return HttpNotFound();
            }
            ViewBag.Incidents = db.TIncidentReportings;

            return View(tRootCause);
        }

        // POST: TRootCauses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RootCauseID,IncidentID,Description,TWhies,RootCauseName,Verified,VerifiedNote")] TRootCause tRootCause)
        {
            if (ModelState.IsValid)
            {
                //db.Configuration.AutoDetectChangesEnabled = false;
                // db.TRootCauses.AsNoTracking().Where(x => x.RootCauseID == 0);
                db.Entry(tRootCause).State = EntityState.Modified;
              
                //db.Entry(tRootCause).State = tRootCause.RootCauseID == 0 ?
                //                   EntityState.Added :
                //                   EntityState.Modified;
                db.SaveChanges();
                var id = TempData["ID"];
                tRootCause = db.TRootCauses.Find(id);
                db.TRootCauses.Remove(tRootCause);
                db.SaveChanges();
                return RedirectToAction("Index");
                //return RedirectToAction("Delete", new { id = TempData["ID"] });
            }

            ViewBag.Incidents = db.TIncidentReportings;
            return View(tRootCause);
        }

        // GET: TRootCauses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRootCause tRootCause = db.TRootCauses.Find(id);
            if (tRootCause == null)
            {
                return HttpNotFound();
            }
            return View(tRootCause);
        }

        // POST: TRootCauses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRootCause tRootCause = db.TRootCauses.Find(id);
            db.TRootCauses.Remove(tRootCause);
            db.SaveChanges();
            return RedirectToAction("Index");
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
