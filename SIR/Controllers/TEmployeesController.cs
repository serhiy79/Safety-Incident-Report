using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIR;
using PagedList;

namespace SIR.Controllers
{
    [Authorize]
    public class TEmployeesController : Controller
    {
        private SIRModel db = new SIRModel();

        // GET: TEmployees
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //var tEmployees = db.TEmployees.Include(t => t.TWorkLocation);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var tEmployees = from s in db.TEmployees
                         select s;

            switch (sortOrder)
            {
                case "name_desc":
                    tEmployees = tEmployees.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    tEmployees = tEmployees.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    tEmployees = tEmployees.OrderByDescending(s => s.StartDate);
                    break;
                default:
                    tEmployees = tEmployees.OrderBy(s => s.LastName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                tEmployees = db.TEmployees.Where(s => s.LastName.Contains(searchString)
                                      || s.FirstName.Contains(searchString));
            }
            //return View(tEmployees.ToList());
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(tEmployees.ToPagedList(pageNumber, pageSize));
        }

        // GET: TEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tEmployee);
        }

        // GET: TEmployees/Create
        public ActionResult Create()
        {
            ViewBag.WorkLocationID = new SelectList(db.TWorkLocations, "WorkLocationID", "LocationName");
            return View();
        }

        // POST: TEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,MiddleName,LastName,PhoneNumber,Address,City,State,Zip,UserName,Password,UserGroup,Email,WorkLocationID,Position,StartDate,Shift,SafetyRecord,IPMScore,Active,ManagerName")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                db.TEmployees.Add(tEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkLocationID = new SelectList(db.TWorkLocations, "WorkLocationID", "LocationName", tEmployee.WorkLocationID);
            return View(tEmployee);
        }

        // GET: TEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkLocationID = new SelectList(db.TWorkLocations, "WorkLocationID", "LocationName", tEmployee.WorkLocationID);
            return View(tEmployee);
        }

        // POST: TEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FirstName,MiddleName,LastName,PhoneNumber,Address,City,State,Zip,UserName,Password,UserGroup,Email,WorkLocationID,Position,StartDate,Shift,SafetyRecord,IPMScore,Active,ManagerName")] TEmployee tEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkLocationID = new SelectList(db.TWorkLocations, "WorkLocationID", "LocationName", tEmployee.WorkLocationID);
            return View(tEmployee);
        }

        // GET: TEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEmployee tEmployee = db.TEmployees.Find(id);
            if (tEmployee == null)
            {
                return HttpNotFound();
            }
            return View(tEmployee);
        }

        // POST: TEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TEmployee tEmployee = db.TEmployees.Find(id);
            db.TEmployees.Remove(tEmployee);
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
