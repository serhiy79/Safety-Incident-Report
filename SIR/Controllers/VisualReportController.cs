using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections;
using System.Web.UI.WebControls;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Helpers;

namespace SIR.Controllers
{
    [Authorize]
   
    public class VisualReportController : Controller
    {
        private SIRModel db = new SIRModel();

        //GET: VisualReport
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Graph(string location, string department, string rca)
        {
            
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
           
            List<string> allMonths = new List<string>();
            allMonths.Add("Jan");
            allMonths.Add("Feb");
            allMonths.Add("Mar");
            allMonths.Add("Apr");
            allMonths.Add("May");
            allMonths.Add("Jun");
            allMonths.Add("Jul");
            allMonths.Add("Aug");
            allMonths.Add("Sep");
            allMonths.Add("Oct");
            allMonths.Add("Nov");
            allMonths.Add("Dec");

            var tIncidentReportings = db.TIncidentReportings.Include(x => x.TRootCauses);
            //var tIncidentReportings = from i in db.TIncidentReportings.Include("TRootCauses")
            //select i;
           // var tIncidentReportings1 = from i in db.TIncidentReportings
                                      // join r in db.TRootCauses on i.IncidentID equals r.IncidentID
                                      // select new { i, r };

            //var tIncidentReportings = db.TIncidentReportings.Join(db.TRootCauses, i => i.IncidentID, r => r.IncidentID, (i, r) => new { i=i, r=r });
            if (!string.IsNullOrWhiteSpace(location))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.IncidentLocation == location);
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.Department == department);
            }
          
            //if (!string.IsNullOrWhiteSpace(rca))
            //{
            //    tIncidentReportings1 = tIncidentReportings1.Where(x => x.r.RootCauseName == rca);
            //}

            #region another query
            //var result = tIncidentReportings
            //.AsEnumerable()
            //.GroupBy(c => c.Date.ToString("MMM"))
            ////.GroupBy(c => c.Date.Month)
            //.Select(g => new { Month = g.Key, Count = g.Count() })
            //.OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMM", CultureInfo.InvariantCulture));


            //var result = tIncidentReportings
            //    .AsEnumerable()
            //    .GroupBy(c => c.Date.ToString("MMMM"))
            //    .Select(g => new { Month = g.Key, Count = g.Count() })
            //    .OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMMM", CultureInfo.InvariantCulture));
            //var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            //months.ForEach(m =>
            //{
            //    if (!result.Select(r => r.Month).Contains(m))
            //    {
            //        result.ToList().Add(new { Month = m, Count = 0 });
            //    }
            //});

            //var months =  System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            #endregion

            //get data from database
            var result = allMonths.Select(m =>
                new
                {
                    Month = m,
                    Count = tIncidentReportings.AsEnumerable().Where(d => d.Date.ToString("MMM") == m).Count()
                }
            ).OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMM", CultureInfo.InvariantCulture)).ToList();

            //write data for x and y axis
            result.ToList().ForEach(rs => xValue.Add(rs.Month));
            result.ToList().ForEach(rs => yValue.Add(rs.Count));
             

            string customTheme = @"<Chart >
                                    <ChartAreas>
                                        <ChartArea Name=""Default"" _Template_=""All"" BackColor=""64, 165, 191, 228"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"" >
                                            <AxisY>
                                       <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
                                      </AxisY>
                                      <AxisX LineColor=""64, 64, 64, 64""   Interval=""1"" >
                                       <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
                                      </AxisX>
                                        </ChartArea>
                                    </ChartAreas>    
                                    <Legends>
                                        <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 8.25pt, style=Bold"" IsTextAutoFit=""False"" /> 
                                    </Legends> 
                                   </Chart>";

            var graph = new System.Web.Helpers.Chart(1080, 250, theme: customTheme)
            .AddSeries("Incidents",chartType: "column", xValue: xValue, yValues: yValue)
            .AddTitle("Total Incidents")
            .AddLegend("Legend")
            .Save("../Content/Chart.png");
            //.Write();
            return View("Index");

            //.GetBytes("png");
            //return File(graph, "image/png");
        }
        
        public ActionResult Graph1(string location, string department, string rca)
        {
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            List<string> allMonths = new List<string>();
            allMonths.Add("Jan");
            allMonths.Add("Feb");
            allMonths.Add("Mar");
            allMonths.Add("Apr");
            allMonths.Add("May");
            allMonths.Add("Jun");
            allMonths.Add("Jul");
            allMonths.Add("Aug");
            allMonths.Add("Sep");
            allMonths.Add("Oct");
            allMonths.Add("Nov");
            allMonths.Add("Dec");

            //var tIncidentReportings = db.TRootCauses.Include(x => x.TIncidentReportings);
            //var tIncidentReportings = from i in db.TIncidentReportings.Include("TRootCauses")
            //select i;
            var tIncidentReportings = from i in db.TIncidentReportings
                                      join r in db.TRootCauses on i.IncidentID equals r.IncidentID
                                      select new { i, r };

            //var tIncidentReportings = db.TIncidentReportings.Join(db.TRootCauses, i => i.IncidentID, r => r.IncidentID, (i, r) => new { i=i, r=r });
            if (!string.IsNullOrWhiteSpace(location))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.i.IncidentLocation == location);
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.i.Department == department);
            }

            if (!string.IsNullOrWhiteSpace(rca))
            {
                tIncidentReportings = tIncidentReportings.Where(x => x.r.RootCauseName == rca);
            }

            #region another query
            //var result = tIncidentReportings
            //.AsEnumerable()
            //.GroupBy(c => c.Date.ToString("MMM"))
            ////.GroupBy(c => c.Date.Month)
            //.Select(g => new { Month = g.Key, Count = g.Count() })
            //.OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMM", CultureInfo.InvariantCulture));


            //var result = tIncidentReportings
            //    .AsEnumerable()
            //    .GroupBy(c => c.Date.ToString("MMMM"))
            //    .Select(g => new { Month = g.Key, Count = g.Count() })
            //    .OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMMM", CultureInfo.InvariantCulture));
            //var months = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            //months.ForEach(m =>
            //{
            //    if (!result.Select(r => r.Month).Contains(m))
            //    {
            //        result.ToList().Add(new { Month = m, Count = 0 });
            //    }
            //});

            //var months =  System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            #endregion

            //get data from database
            var result = allMonths.Select(m =>
                new
                {
                    Month = m,
                    Count = tIncidentReportings.AsEnumerable().Where(d => d.i.Date.ToString("MMM") == m).Count()
                }
            ).OrderBy(x => DateTime.ParseExact((x.Month).ToString(), "MMM", CultureInfo.InvariantCulture)).ToList();

            //write data for x and y axis
            result.ToList().ForEach(rs => xValue.Add(rs.Month));
            result.ToList().ForEach(rs => yValue.Add(rs.Count));

            string customTheme = @"<Chart Palette=""None"" PaletteCustomColors=""#00CC00"" >
                                    <ChartAreas>
                                        <ChartArea Name=""Default"" _Template_=""All"" BackColor=""64, 165, 191, 228"" BackGradientStyle=""TopBottom"" BackSecondaryColor=""White"" BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" ShadowColor=""Transparent"" >
                                            <AxisY>
                                       <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
                                      </AxisY>
                                      <AxisX LineColor=""64, 64, 64, 64""   Interval=""1"" >
                                       <LabelStyle Font=""Trebuchet MS, 8.25pt, style=Bold"" />
                                      </AxisX>
                                        </ChartArea>
                                    </ChartAreas>    
                                    <Legends>
                                        <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Trebuchet MS, 8.25pt, style=Bold"" IsTextAutoFit=""False"" /> 
                                    </Legends> 
                                   </Chart>";

        var graph = new System.Web.Helpers.Chart(1100, 250, theme: customTheme)
            .AddSeries("Root Causes", chartType: "column", xValue: xValue, yValues: yValue)
            .AddLegend("Legend")
            .AddTitle("Root Causes")
            .Save("../Content/Chart1.png");
            //.Write();
            return View("Index");

            //.GetBytes("png");
            //return File(graph, "image/png");
        }
        public FilePathResult GetPdf()
        {
            var doc = new Document();
            var pdf = Server.MapPath("../Chart.pdf");

            PdfWriter.GetInstance(doc, new FileStream(pdf, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("Incident Report Chart"));
            string path = HttpContext.Server.MapPath("~/Content/Chart.png");
            var image = iTextSharp.text.Image.GetInstance(path);
            image.ScalePercent(50f);
            doc.Add(image);
            doc.Close();

            return File(pdf, "application/pdf", "Chart.pdf");
        }

        public FilePathResult GetPdf1()
        {
            var doc = new Document();
            var pdf = Server.MapPath("../Chart1.pdf");

            PdfWriter.GetInstance(doc, new FileStream(pdf, FileMode.Create));
            doc.Open();

            doc.Add(new Paragraph("Incident Report Chart"));
            string path = HttpContext.Server.MapPath("~/Content/Chart1.png");
            var image = iTextSharp.text.Image.GetInstance(path);
            image.ScalePercent(50f);
            doc.Add(image);
            doc.Close();

            return File(pdf, "application/pdf", "Char1t.pdf");
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
