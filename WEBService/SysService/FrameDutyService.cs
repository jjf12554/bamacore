using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameDTO.SysDTO;
using WEBModel.FrameModel.SysModel;
using WEBTools;
using WEBTools.Convent;

namespace WEBService.SysService
{
    public class FrameDutyService
    {
        public FrameDutyDAO frameDutyDAO = new FrameDutyDAO();

        public List<FrameDutyModel> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<FrameDutyModel>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            return frameDutyDAO.Query(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public FrameDutyModel GetForm(string keyValue)
        {
            return frameDutyDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            frameDutyDAO.Delete(keyValue,userId);
        }
        public void SubmitForm(FrameDutyDTO dutyEntity, string keyValue, string userId)
        {
            FrameDutyModel frameDutyModel = new FrameDutyModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameDutyModel = frameDutyDAO.GetById(keyValue);
                frameDutyModel.F_OrganizeId = dutyEntity.F_OrganizeId;
                frameDutyModel.F_EnCode = dutyEntity.F_EnCode;
                frameDutyModel.F_FullName = dutyEntity.F_FullName;
                frameDutyModel.F_Type = dutyEntity.F_Type;
                frameDutyModel.F_AllowEdit = dutyEntity.F_AllowEdit;
                frameDutyModel.F_AllowDelete = dutyEntity.F_AllowDelete;
                frameDutyModel.F_SortCode = dutyEntity.F_SortCode;
                frameDutyModel.F_EnabledMark = dutyEntity.F_EnabledMark;
                frameDutyModel.F_Description = dutyEntity.F_Description;
                frameDutyDAO.Update(frameDutyModel, userId);
            }
            else
            {
                frameDutyModel = AutoMapperHelper.MapTo<FrameDutyModel>(dutyEntity);
                frameDutyDAO.Create(frameDutyModel, userId);
            }
        }
    }
}
