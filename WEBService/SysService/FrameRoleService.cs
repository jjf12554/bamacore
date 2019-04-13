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
    public class FrameRoleService
    {
        public FrameRoleDAO frameRoleDAO = new FrameRoleDAO();
        public FrameRoleAuthorizeDAO frameRoleAuthorizeDAO = new FrameRoleAuthorizeDAO();
        public FrameModuleDAO frameModuleDAO = new FrameModuleDAO();
        public FrameModuleButtonDAO frameModuleButtonDAO = new FrameModuleButtonDAO();
        public List<FrameRoleModel> GetList()
        {
            return frameRoleDAO.QueryAll();
        }

        public List<FrameRoleModel> GetList(string keyword = "")
        {
            var expression = ExtLinq.True<FrameRoleModel>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_FullName.Contains(keyword));
                expression = expression.Or(t => t.F_EnCode.Contains(keyword));
            }
            expression = expression.And(t => t.F_DeleteMark == false);
            return frameRoleDAO.Query(expression).OrderBy(t => t.F_SortCode).ToList();
        }
        public FrameRoleModel GetForm(string keyValue)
        {
            return frameRoleDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            frameRoleDAO.Delete(keyValue, userId);
        }
        public void SubmitForm(FrameRoleDTO roleEntity, string[] permissionIds, string keyValue,string userId)
        {
            FrameRoleModel frameRoleModel = new FrameRoleModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.F_Id = keyValue;
            }
            else
            {
                roleEntity.F_Id = Guid.NewGuid().ToString();
            }

            var moduledata = frameModuleDAO.GetList();
            var buttondata = frameModuleButtonDAO.GetList();
            List<FrameRoleAuthorizeModel> roleAuthorizeEntitys = new List<FrameRoleAuthorizeModel>();
            foreach (var itemId in permissionIds)
            {
                FrameRoleAuthorizeModel roleAuthorizeEntity = new FrameRoleAuthorizeModel();
                roleAuthorizeEntity.F_Id = Guid.NewGuid().ToString();
                roleAuthorizeEntity.F_ObjectType = 1;
                roleAuthorizeEntity.F_ObjectId = roleEntity.F_Id;
                roleAuthorizeEntity.F_ItemId = itemId;
                if (moduledata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 1;
                }
                if (buttondata.Find(t => t.F_Id == itemId) != null)
                {
                    roleAuthorizeEntity.F_ItemType = 2;
                }
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameRoleModel = frameRoleDAO.GetById(keyValue);
                frameRoleModel.F_OrganizeId = roleEntity.F_OrganizeId;
                frameRoleModel.F_Category = roleEntity.F_Category;
                frameRoleModel.F_EnCode = roleEntity.F_EnCode;
                frameRoleModel.F_FullName = roleEntity.F_FullName;
                frameRoleModel.F_Type = roleEntity.F_Type;
                frameRoleModel.F_AllowEdit = roleEntity.F_AllowEdit;
                frameRoleModel.F_AllowDelete = roleEntity.F_AllowDelete;
                frameRoleModel.F_SortCode = roleEntity.F_SortCode;
                frameRoleModel.F_EnabledMark = roleEntity.F_EnabledMark;
                frameRoleModel.F_Description = roleEntity.F_Description;
                frameRoleDAO.Update(frameRoleModel, userId);
            }
            else
            {
                //roleEntity.F_Category = 1;
                frameRoleModel = AutoMapperHelper.MapTo<FrameRoleModel>(roleEntity);
                frameRoleDAO.Create(frameRoleModel, userId);
            }
            frameRoleAuthorizeDAO.DeleteRole(roleEntity.F_Id);
            frameRoleAuthorizeDAO.Create(roleAuthorizeEntitys, userId);
        }
    }
}
