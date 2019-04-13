using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameDTO.SysDTO;
using WEBModel.FrameModel.SysModel;
using WEBTools;

namespace WEBService.SysService
{
    public class FrameModuleService
    {
        public FrameModuleDAO frameModuleDAO { get; set; }

        public List<FrameModuleModel> GetList()
        {
            return frameModuleDAO.QueryAll().OrderBy(t => t.F_SortCode).ToList();
        }

        public FrameModuleModel GetForm(string keyValue)
        {
            return frameModuleDAO.GetById(keyValue);
        }

        public void DeleteForm(string keyValue,string userId)
        {
            if (frameModuleDAO.QueryAll().Where(p => p.F_ParentId == keyValue).Count() > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                frameModuleDAO.Delete(keyValue, userId);
            }
        }

        public void SubmitForm(FrameModuleDTO moduleEntity, string keyValue,string userId)
        {
            FrameModuleModel frameModuleModel = new FrameModuleModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameModuleModel = frameModuleDAO.GetById(keyValue);
                frameModuleModel.F_ParentId = moduleEntity.F_ParentId;
                frameModuleModel.F_Layers = moduleEntity.F_Layers;
                frameModuleModel.F_EnCode = moduleEntity.F_EnCode;
                frameModuleModel.F_FullName = moduleEntity.F_FullName;
                frameModuleModel.F_Icon = moduleEntity.F_Icon;
                frameModuleModel.F_UrlAddress = moduleEntity.F_UrlAddress;
                frameModuleModel.F_Target = moduleEntity.F_Target;
                frameModuleModel.F_IsCS = moduleEntity.F_IsCS;
                frameModuleModel.F_IsMenu = moduleEntity.F_IsMenu;
                frameModuleModel.F_IsExpand = moduleEntity.F_IsExpand;
                frameModuleModel.F_IsPublic = moduleEntity.F_IsPublic;
                frameModuleModel.F_AllowEdit = moduleEntity.F_AllowEdit;
                frameModuleModel.F_AllowDelete = moduleEntity.F_AllowDelete;
                frameModuleModel.F_SortCode = moduleEntity.F_SortCode;
                frameModuleModel.F_EnabledMark = moduleEntity.F_EnabledMark;
                frameModuleModel.F_Description = moduleEntity.F_Description;
                frameModuleDAO.Update(frameModuleModel, userId);
            }
            else
            {
                frameModuleModel = AutoMapperHelper.MapTo<FrameModuleModel>(moduleEntity);
                frameModuleDAO.Create(frameModuleModel, userId);
            }
        }
    }
}
