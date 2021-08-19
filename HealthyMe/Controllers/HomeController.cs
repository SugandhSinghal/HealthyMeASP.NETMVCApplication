using HealthyMe.Models;
using HealthyMe.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;


namespace HealthyMe.Controllers
{

    public class HomeController : Controller
    {
        private HeathyMe_Entities db = new HeathyMe_Entities();
        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }
        public ActionResult Calendar()
        {
            return View();
        }

        // This is for sending buld email
        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail1 = model.ToEmail1;
                    String toEmail2 = model.ToEmail2;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail1, toEmail2, subject, contents);
                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        // This is used to get the chart


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetChartData()
        {

            List<Rating> data = new List<Rating>();
            //Here MyDatabaseEntities  is our dbContext
            using (HeathyMe_Entities db = new HeathyMe_Entities())
            {
                data = db.Ratings.ToList();
            }

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]{
                    "AppointmentId",
                    "Rating1"
                };
            int j = 0;
            foreach (var i in data)
            {
                j++;
                chartData[j] = new object[] { i.AppointmentId, i.Rating1 };
            }

            return chartData;

        }






    }

}