using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

using rovnicaMVC.Models;
using rovnicaMVC.Domain;

namespace rovnicaMVC.Controllers
{
    public class NeedsController : Controller
    {
        //
        // GET: /Admin/

        //public ActionResult Index(OrderState? status)
        //{
        //    status = status ?? OrderState.All;

        //    var res = CategoryItemRepository.GetOrders(status.Value);

        //    ViewData["status"] = status.Value;

        //    return View(res);
        //}

        public ActionResult Index()
        {

            //System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
            //cn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["cn_mssql"].ConnectionString;
            //cn.Open();
            var res = new p_needs();

            res.hq_needs = DataStorage.getNeeds();
            res.test = "bbb";

              return View(res);
        }

           

        public ActionResult Login(string userName, string password, string ReturnUrl)
        {
            if (this.IsValidLoginArgument(userName, password))
            {
                var passwordEncrypted = FormsAuthentication.HashPasswordForStoringInConfigFile(userName + password, "MD5");
                if (CategoryItemRepository.ValidateUser(userName, passwordEncrypted))
                    this.RedirectFromLoginPage(userName, ReturnUrl);
                else
                    this.ViewData["LoginFaild"] = "Login faild! Make sure you have entered the right user name and password!";
            }


            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Response.Redirect(FormsAuthentication.DefaultUrl);


            return View();
        }

        private void RedirectFromLoginPage(string userName, string ReturnUrl)
        {
            FormsAuthentication.SetAuthCookie(userName, false);

            if (!string.IsNullOrEmpty(ReturnUrl))
                Response.Redirect(ReturnUrl);
            else
                Response.Redirect(FormsAuthentication.DefaultUrl);
        }

        private bool IsValidLoginArgument(string userName, string password)
        {
            return !(string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password));
        }

     
       

    }
}
