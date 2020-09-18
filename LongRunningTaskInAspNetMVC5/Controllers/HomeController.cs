using LongRunningTaskInAspNetMVC5.MyTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LongRunningTaskInAspNetMVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Guid requestId = Guid.NewGuid();
            ProgressTracker.add(requestId, "Starting Long running task!!!");

            //Call Long running task
            MyLongRunningTask myTask = new MyLongRunningTask();
            myTask.RunMyTask(requestId);

            return RedirectToAction("TaskProgress", new { requestId = requestId.ToString() });
        }

        public ActionResult TaskProgress(string requestId)
        {
            var statusMessage = string.Empty;

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                statusMessage = ProgressTracker.getValue(Guid.Parse(requestId)).ToString();

                //The processing  has not yet finished 
                //Add a refresh header, to refresh the page in 5 seconds.
                Response.Headers.Add("Refresh", "5");
                return View("TaskProgress", (object)statusMessage);
            }

            statusMessage = "Error: Something went wrong with process";
            return View("TaskProgress", (object)statusMessage);
        }

        public ActionResult Error()
        {
            return View();
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
    }
}