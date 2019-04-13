using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAMA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBTools;

namespace BAMA.Controllers
{
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult susu(string str,string str1)
        {
            var model = OperatorProvider.Provider.GetCurrent();
            return Content(model.ToJson());
        }
    }
}