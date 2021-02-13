using AuthNAuthZ.Attributes;
using System;
using System.Web.Mvc;

namespace AuthNAuthZSimpleExampleWeb.Controllers
{
    [AuthenticationFilter]
    [ExceptionFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [AuthenticationFilter(false)]
        public ActionResult Logout(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        [AuthenticationFilter(false)]
        public ActionResult Unauthorized(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        [AuthenticationFilter(false)]
        public PartialViewResult UnauthorizedPartial(string Message)
        {
            ViewBag.Message = Message;
            return PartialView();
        }
        [AuthenticationFilter(false)]
        public ActionResult Error(string Message, string StackTrace, string InnerExceptionMessage, string InnerExceptionStackTrace)
        {
            ViewBag.Message = Message;
            return View();
        }
        [AuthenticationFilter(false)]
        public PartialViewResult ErrorPartial(string Message, string StackTrace, string InnerExceptionMessage, string InnerExceptionStackTrace)
        {
            ViewBag.Message = Message;
            return PartialView();
        }

        public ActionResult ForbiddenTest()
        {
            return View();
        }
        [IsThree]
        public ActionResult UnauthorizedTest()
        {
            return View();
        }
        [IsThree]
        public PartialViewResult UnauthorizedPartialTest()
        {
            return PartialView();
        }

        public ActionResult ErrorTest()
        {
            throw new Exception("Testing Error Handler");
        }

        public PartialViewResult ErrorPartialTest()
        {
            throw new Exception("Testing partial Error Handler");
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