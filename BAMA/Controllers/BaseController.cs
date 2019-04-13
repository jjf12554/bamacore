using BAMA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBTools;

namespace BAMA.Controllers
{
    public class BaseController: Controller
    {
        public string userId
        {
            get
            {
                var LoginInfo = OperatorProvider.Provider.GetCurrent();
                if (LoginInfo != null)
                {
                    return LoginInfo.UserId;
                }
                else
                {
                    return "";
                }
            }
        }
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Details()
        {
            return View();
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
