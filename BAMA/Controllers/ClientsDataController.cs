using BAMA.Filtter;
using BAMA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WEBModel.FrameModel.SysModel;
using WEBService.SysService;
using WEBTools;
using WEBTools.Convent;

namespace BAMA.Controllers
{
    public class ClientsDataController : Controller
    {
        public FrameItemDetailService frameItemDetailService = new FrameItemDetailService();
        public FrameItemService frameItemService = new FrameItemService();
        public FrameOrganizeService frameOrganizeService = new FrameOrganizeService();
        public FrameRoleService frameRoleService = new FrameRoleService();
        public FrameRoleAuthorizeService frameRoleAuthorizeService = new FrameRoleAuthorizeService();
        public FrameDutyService frameDutyService = new FrameDutyService();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            HttpContext.Request.Cookies.TryGetValue("jxkbd20180822", out string value);
            var operatorModel = EncryptTool.DESDecrypt(value).ToObject<OperatorModel>();
            var data = new
            {
                dataItems = this.GetDataItemList(),
                organize = this.GetOrganizeList(),
                role = this.GetRoleList(),
                duty = this.GetDutyList(),
                user = "",
                authorizeMenu = this.GetMenuList(),
                authorizeButton = this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }


        private object GetDataItemList()
        {
            var itemdata = frameItemDetailService.GetList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in frameItemService.GetList())
            {
                var dataItemList = itemdata.FindAll(t => t.F_ItemId.Equals(item.F_Id));
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.F_ItemCode, itemList.F_ItemName);
                }
                dictionaryItem.Add(item.F_EnCode, dictionaryItemList);
            }
            return dictionaryItem;
        }
        private object GetOrganizeList()
        {
            
            var data = frameOrganizeService.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (FrameOrganizeModel item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetRoleList()
        {
            
            var data = frameRoleService.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (FrameRoleModel item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetDutyList()
        {
            //FrameRoleService frameDutyService = new FrameRoleService();
            var data = frameDutyService.GetList();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (FrameDutyModel item in data)
            {
                var fieldItem = new
                {
                    encode = item.F_EnCode,
                    fullname = item.F_FullName
                };
                dictionary.Add(item.F_Id, fieldItem);
            }
            return dictionary;
        }
        private object GetMenuList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            return ToMenuJson(frameRoleAuthorizeService.GetMenuList(roleId, OperatorProvider.Provider.GetCurrent().IsSystem), "0");
        }
        private string ToMenuJson(List<FrameModuleModel> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<FrameModuleModel> entitys = data.FindAll(t => t.F_ParentId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.F_Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        private object GetMenuButtonList()
        {
            var roleId = OperatorProvider.Provider.GetCurrent().RoleId;
            var data = frameRoleAuthorizeService.GetButtonList(roleId, OperatorProvider.Provider.GetCurrent().IsSystem);
            var dataModuleId = data.Distinct(new ExtList<FrameModuleButtonModel>("F_ModuleId"));
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (FrameModuleButtonModel item in dataModuleId)
            {
                var buttonList = data.Where(t => t.F_ModuleId.Equals(item.F_ModuleId));
                dictionary.Add(item.F_ModuleId, buttonList);
            }
            return dictionary;
        }
    }
}