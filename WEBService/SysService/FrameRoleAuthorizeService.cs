using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameModel.SysModel;

namespace WEBService.SysService
{
    public class FrameRoleAuthorizeService
    {
        public FrameRoleAuthorizeDAO frameRoleAuthorizeDAO = new FrameRoleAuthorizeDAO();
        public FrameModuleDAO frameModuleDAO = new FrameModuleDAO();
        public FrameModuleButtonDAO frameModuleButtonDAO = new FrameModuleButtonDAO();


        public List<FrameRoleAuthorizeModel> GetList(string ObjectId)
        {
            return frameRoleAuthorizeDAO.Query(t => t.F_ObjectId == ObjectId).ToList();
        }
        public List<FrameModuleModel> GetMenuList(string roleId,bool isSystem)
        {
            var data = new List<FrameModuleModel>();
            if (isSystem)
            {
                data = frameModuleDAO.QueryAll();
            }
            else
            {
                var moduledata = frameModuleDAO.QueryAll();
                var authorizedata = frameRoleAuthorizeDAO.Query(t => t.F_ObjectId == roleId && t.F_ItemType == 1).ToList();
                foreach (var item in authorizedata)
                {
                    FrameModuleModel moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleEntity != null)
                    {
                        data.Add(moduleEntity);
                    }
                }
            }
            return data.Where(p => p.F_IsCS == false).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<FrameModuleButtonModel> GetButtonList(string roleId,bool isSystem)
        {
            var data = new List<FrameModuleButtonModel>();
            if (isSystem)
            {
                data = frameModuleButtonDAO.QueryAll();
            }
            else
            {
                var buttondata = frameModuleButtonDAO.QueryAll();
                var authorizedata = frameRoleAuthorizeDAO.Query(t => t.F_ObjectId == roleId && t.F_ItemType == 2).ToList();
                foreach (var item in authorizedata)
                {
                    FrameModuleButtonModel moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
                    if (moduleButtonEntity != null)
                    {
                        data.Add(moduleButtonEntity);
                    }
                }
            }
            return data.OrderBy(t => t.F_SortCode).ToList();
        }

        public bool ActionValidate(string roleId, string moduleId, string action)
        {
            //var authorizeurldata = new List<AuthorizeActionModel>();
            //var cachedata = CacheFactory.Cache().GetCache<List<AuthorizeActionModel>>("authorizeurldata_" + roleId);
            //if (cachedata == null)
            //{
            //    var moduledata = moduleApp.GetList();
            //    var buttondata = moduleButtonApp.GetList();
            //    var authorizedata = service.IQueryable(t => t.F_ObjectId == roleId).ToList();
            //    foreach (var item in authorizedata)
            //    {
            //        if (item.F_ItemType == 1)
            //        {
            //            ModuleEntity moduleEntity = moduledata.Find(t => t.F_Id == item.F_ItemId);
            //            authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleEntity.F_Id, F_UrlAddress = moduleEntity.F_UrlAddress });
            //        }
            //        else if (item.F_ItemType == 2)
            //        {
            //            ModuleButtonEntity moduleButtonEntity = buttondata.Find(t => t.F_Id == item.F_ItemId);
            //            authorizeurldata.Add(new AuthorizeActionModel { F_Id = moduleButtonEntity.F_ModuleId, F_UrlAddress = moduleButtonEntity.F_UrlAddress });
            //        }
            //    }
            //    CacheFactory.Cache().WriteCache(authorizeurldata, "authorizeurldata_" + roleId, DateTime.Now.AddMinutes(5));
            //}
            //else
            //{
            //    authorizeurldata = cachedata;
            //}
            //authorizeurldata = authorizeurldata.FindAll(t => t.F_Id.Equals(moduleId));
            //foreach (var item in authorizeurldata)
            //{
            //    if (!string.IsNullOrEmpty(item.F_UrlAddress))
            //    {
            //        string[] url = item.F_UrlAddress.Split('?');
            //        if (item.F_Id == moduleId && url[0] == action)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;
            return true;
        }
    }
}
