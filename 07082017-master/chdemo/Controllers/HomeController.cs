using chdemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chdemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult loaddata()
        {
            using (chdemo.projectEntities dc = new chdemo.projectEntities())
            {
                var data = dc.record_estimate.OrderBy(a => a.Time).ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult GetChartData()
        {
            List<record_estimate> data = new List<record_estimate>();
            var dt = new VisualizationDataTable();
            var chart = new ChartViewModel
            {
                Title = "Events",
                Subtitle = "per date",
                DataTable = dt
            };




            //Here MyDatabaseEntities  is our dbContext
            using (projectEntities dc = new projectEntities())
            {
                        
                data = dc.record_estimate.ToList();

            }


            var sortbytime = data.OrderByDescending(x => x.Time).ToList();


            dt.AddColumn("Date", "string");
            dt.AddColumn("實際蒸汽壓力(kgf/cm2G)", "number");
            dt.AddColumn("預測蒸汽壓力(kgf/cm2G)", "number");

            int counter = 0;
            foreach (var item in sortbytime)
            {
                dt.NewRow(item.Time, item.ActualPressure, item.EstimatePressure);
                counter++;
                if (counter >= 12)
                    break;

            }






            return Content(JsonConvert.SerializeObject(chart), "application/json");
        }
    }
}