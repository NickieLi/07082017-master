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
        private er_modelEntities3 db = new er_modelEntities3();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string Date,string Hours)
        {
            ViewBag.Hours = new SelectList(db.historypressure, "Hours", "Hours");
            var date = from d in db.historypressure
                       select d;
            if (!string.IsNullOrEmpty(Date))
            {
                date = date.Where(c => c.Date.Contains(Date));
            }
            if (!string.IsNullOrEmpty(Hours))
            {
                string hr = Convert.ToString(Hours);
                date = date.Where(c => c.Hours.Contains(Hours));
                return View(date);
            }
            else
            {
                return View(date);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult GetChartData()
        {
            List<historypressure> data = new List<historypressure>();
            var dt = new VisualizationDataTable();
            var chart = new ChartViewModel
            {
                Title = "Events",
                Subtitle = "per date",
                DataTable = dt
            };




            //Here MyDatabaseEntities  is our dbContext
            using (er_modelEntities3 dc = new er_modelEntities3())
            {

                data = dc.historypressure.ToList();

            }


            var sortbytime = data.OrderByDescending(x => x.Minutes).ToList();


            dt.AddColumn("Date", "string");
            dt.AddColumn("實際蒸汽壓力(kgf/cm2G)", "number");
            dt.AddColumn("預測蒸汽壓力(kgf/cm2G)", "number");

            int counter = 0;
            foreach (var item in sortbytime)
            {
                dt.NewRow(item.Minutes, item.ActualPressure, item.EstimatePressure);
                counter++;
                if (counter >= 13)
                    break;

            }

            return Content(JsonConvert.SerializeObject(chart), "application/json");
        }

        //public ActionResult SearchChartData(string Date, string Hours)
        //{
            //ViewBag.Hours = new SelectList(db.historypressure, "Hours", "Hours");
            //var date = from d in db.historypressure
            //           select d;
            //if (!string.IsNullOrEmpty(Date))
            //{
            //    date = date.Where(c => c.Date.Contains(Date));
            //}
            //return View(date);
            //if (!string.IsNullOrEmpty(Hours))
            //{
            //    string hr = Convert.ToString(Hours);
            //    return View(date.Where(s => s.Hours == hr));
            //}
            //else
            //{
            //    return View(date);
            //}
        //}

    }
}