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
    public class FrameOrganizeService
    {
        public FrameOrganizeDAO frameOrganizeDAO = new FrameOrganizeDAO();
        public List<FrameOrganizeModel> GetList()
        {
            return frameOrganizeDAO.QueryAll();
        }

        public FrameOrganizeModel GetForm(string keyValue)
        {
            return frameOrganizeDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            if (frameOrganizeDAO.QueryAll().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                frameOrganizeDAO.Delete(keyValue, userId);
            }
        }
        public void SubmitForm(FrameOrganizeDTO organizeEntity, string keyValue,string userId)
        {
            FrameOrganizeModel frameOrganizeModel = new FrameOrganizeModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameOrganizeModel = frameOrganizeDAO.GetById(keyValue);
                frameOrganizeModel.F_Layers = organizeEntity.F_Layers;
                frameOrganizeModel.F_EnCode = organizeEntity.F_EnCode;
                frameOrganizeModel.F_FullName = organizeEntity.F_FullName;
                frameOrganizeModel.F_ShortName = organizeEntity.F_ShortName;
                frameOrganizeModel.F_CategoryId = organizeEntity.F_CategoryId;
                frameOrganizeModel.F_ManagerId = organizeEntity.F_ManagerId;
                frameOrganizeModel.F_TelePhone = organizeEntity.F_TelePhone;
                frameOrganizeModel.F_MobilePhone = organizeEntity.F_MobilePhone;
                frameOrganizeModel.F_WeChat = organizeEntity.F_WeChat;
                frameOrganizeModel.F_Fax = organizeEntity.F_Fax;
                frameOrganizeModel.F_Email = organizeEntity.F_Email;
                frameOrganizeModel.F_AreaId = organizeEntity.F_AreaId;
                frameOrganizeModel.F_Address = organizeEntity.F_Address;
                frameOrganizeModel.F_AllowEdit = organizeEntity.F_AllowEdit;
                frameOrganizeModel.F_AllowDelete = organizeEntity.F_AllowDelete;
                frameOrganizeModel.F_SortCode = organizeEntity.F_SortCode;
                frameOrganizeModel.F_EnabledMark = organizeEntity.F_EnabledMark;
                frameOrganizeModel.F_Description = organizeEntity.F_Description;
                frameOrganizeDAO.Update(frameOrganizeModel, userId);
            }
            else
            {
                frameOrganizeModel = AutoMapperHelper.MapTo<FrameOrganizeModel>(organizeEntity);
                frameOrganizeDAO.Create(frameOrganizeModel, userId);
            }
        }
    }
}
