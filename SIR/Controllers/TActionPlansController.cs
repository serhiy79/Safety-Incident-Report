using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SIR.Controllers
{
    [Authorize]
    public class TActionPlansController : Controller
    {
        private SIRModel db = new SIRModel();

        public ActionResult Index(int? incidentId, int? rootCauseId)
        {
            // Drill from Incident, to Root Cause, to Action Plan
            if (rootCauseId == null)
            {
                // No Root Cause; do we have incident?
                if (incidentId == null)
                {
                    // Choose Incident
                    ViewBag.Incidents = db.TIncidentReportings;
                    return View((object)null);
                }

                // Choose Root Cause
                ViewBag.Incident = db.TIncidentReportings.Find(incidentId);
                ViewBag.RootCauses = db.TRootCauses.Where(t => t.IncidentID == incidentId);
                return View((object)null);
            }

            ViewBag.RootCause = db.TRootCauses.Include("TIncidentReporting")
                .Where(r => r.RootCauseID == rootCauseId).Single();
            var tActionPlans = db.TActionPlans.Where(t => t.RootCauseID == rootCauseId)
                .Include(t => t.TRootCause);
            return View(tActionPlans.ToList());
        }

        // GET: TActionPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TActionPlan tActionPlan = db.TActionPlans.Find(id);
            if (tActionPlan == null)
            {
                return HttpNotFound();
            }
            return View(tActionPlan);
        }

        // GET: TActionPlans/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            TActionPlan actionPlan = new TActionPlan() { RootCauseID = id.Value };
            return View(actionPlan);
        }

        // POST: TActionPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActionPlanID,RootCauseID,Owner,DeadLine,Status,Description")] TActionPlan tActionPlan)
        {
            if (ModelState.IsValid)
            {
                db.TActionPlans.Add(tActionPlan);
                db.SaveChanges();
                return RedirectToAction("Index", new { rootCauseId = tActionPlan.RootCauseID });
            }

            ViewBag.RootCauseId = tActionPlan.RootCauseID;
            return View(tActionPlan);
        }

        // GET: TActionPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TActionPlan tActionPlan = db.TActionPlans.Find(id);
            if (tActionPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.RootCauseId = tActionPlan.RootCauseID;
            return View(tActionPlan);
        }

        // POST: TActionPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActionPlanID,RootCauseID,Owner,DeadLine,Status,Description")] TActionPlan tActionPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tActionPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { rootCauseId = tActionPlan.RootCauseID });
            }
            ViewBag.RootCauseId = tActionPlan.RootCauseID;
            return View(tActionPlan);
        }

        // GET: TActionPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TActionPlan tActionPlan = db.TActionPlans.Find(id);
            if (tActionPlan == null)
            {
                return HttpNotFound();
            }
            return View(tActionPlan);
        }

        // POST: TActionPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TActionPlan tActionPlan = db.TActionPlans.Find(id);
            db.TActionPlans.Remove(tActionPlan);
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