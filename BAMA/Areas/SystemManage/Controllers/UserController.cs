using BAMA.Controllers;
using BAMA.Filtter;
using BAMA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBModel.FrameDTO.SysDTO;
using WEBModel.FrameModel.SysModel;
using WEBService.SysService;
using WEBTools;

namespace BAMA.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class UserController : BaseController
    {
        public FrameUserService frameUserService = new FrameUserService();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            HttpContext.Request.Cookies.TryGetValue("jxkbd20180822", out string value);
            var operatorModel = EncryptTool.DESDecrypt(value).ToObject<OperatorModel>();
            var data = new
            {
                rows = frameUserService.GetList(keyword, pagination),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                keyValue = userId;
            }
            var data = frameUserService.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(FrameUserDTO userEntity, string keyValue)
        {
            frameUserService.SubmitForm(userEntity, keyValue,userId);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            frameUserService.DeleteForm(keyValue,userId);
            return Success("删除成功。");
        }
        [HttpGet]
        public ActionResult RevisePassword()
        {
            return View();
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitRevisePassword(string userPassword, string keyValue)
        {
            frameUserService.RevisePassword(userPassword, keyValue,userId);
            return Success("重置密码成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            frameUserService.UpdateForm(false, keyValue,userId);
            return Success("账户禁用成功。");
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            frameUserService.UpdateForm(true, keyValue, userId);
            return Success("账户启用成功。");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}