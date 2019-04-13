using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameModel.SysModel;
using WEBTools.Convent;

namespace WEBService.SysService
{
    public class FrameModuleButtonService
    {
        public FrameModuleButtonDAO frameModuleButtonDAO { get; set; }
        public List<FrameModuleButtonModel> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<FrameModuleButtonModel>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_DeleteMark == false);
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return frameModuleButtonDAO.Query(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public FrameModuleButtonModel GetForm(string keyValue)
        {
            return frameModuleButtonDAO.GetById(keyValue);
        }

        public void DeleteForm(string keyValue,string userId)
        {
            if (frameModuleButtonDAO.QueryAll().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                frameModuleButtonDAO.Delete(keyValue, userId);
            }
        }

        public void SubmitForm(FrameModuleButtonModel moduleButtonEntity, string keyValue,string userId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                FrameModuleButtonModel frameModuleButtonModel = frameModuleButtonDAO.GetById(keyValue);
                frameModuleButtonModel.F_ModuleId = moduleButtonEntity.F_ModuleId;
                frameModuleButtonModel.F_ParentId = moduleButtonEntity.F_ParentId;
                frameModuleButtonModel.F_Layers = moduleButtonEntity.F_Layers;
                frameModuleButtonModel.F_EnCode = moduleButtonEntity.F_EnCode;
                frameModuleButtonModel.F_FullName = moduleButtonEntity.F_FullName;
                frameModuleButtonModel.F_Icon = moduleButtonEntity.F_Icon;
                frameModuleButtonModel.F_Location = moduleButtonEntity.F_Location;
                frameModuleButtonModel.F_JsEvent = moduleButtonEntity.F_JsEvent;
                frameModuleButtonModel.F_UrlAddress = moduleButtonEntity.F_UrlAddress;
                frameModuleButtonModel.F_Split = moduleButtonEntity.F_Split;
                frameModuleButtonModel.F_IsPublic = moduleButtonEntity.F_IsPublic;
                frameModuleButtonModel.F_AllowEdit = moduleButtonEntity.F_AllowEdit;
                frameModuleButtonModel.F_AllowDelete = moduleButtonEntity.F_AllowDelete;
                frameModuleButtonModel.F_SortCode = moduleButtonEntity.F_SortCode;
                frameModuleButtonDAO.Update(frameModuleButtonModel, userId);
            }
            else
            {
                frameModuleButtonDAO.Create(moduleButtonEntity, userId);
            }
        }

        public void SubmitCloneButton(string moduleId, string Ids,string userId)
        {
            string[] ArrayId = Ids.Split(',');
            var data = this.GetList();
            List<FrameModuleButtonModel> entitys = new List<FrameModuleButtonModel>();
            foreach (string item in ArrayId)
            {
                FrameModuleButtonModel moduleButtonEntity = data.Find(t => t.F_Id == item);
                moduleButtonEntity.F_Id = Guid.NewGuid().ToString();
                moduleButtonEntity.F_ModuleId = moduleId;
                entitys.Add(moduleButtonEntity);
            }
            frameModuleButtonDAO.Create(entitys, userId);
        }
    }
}
