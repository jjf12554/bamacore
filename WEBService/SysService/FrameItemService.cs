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
    public class FrameItemService
    {
        public FrameItemDAO frameItemsDAO = new FrameItemDAO();

        public List<FrameItemModel> GetList()
        {
            return frameItemsDAO.QueryAll();
        }
        public FrameItemModel GetForm(string keyValue)
        {
            return frameItemsDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            if (frameItemsDAO.QueryAll().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                frameItemsDAO.Delete(keyValue,userId);
            }
        }
        public void SubmitForm(FrameItemDTO itemsEntity, string keyValue,string userId)
        {
            FrameItemModel frameItemModel = new FrameItemModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameItemModel = frameItemsDAO.GetById(keyValue);
                frameItemModel.F_ParentId = itemsEntity.F_ParentId;
                frameItemModel.F_EnCode = itemsEntity.F_EnCode;
                frameItemModel.F_FullName = itemsEntity.F_FullName;
                frameItemModel.F_IsTree = itemsEntity.F_IsTree;
                frameItemModel.F_Layers = itemsEntity.F_Layers;
                frameItemModel.F_SortCode = itemsEntity.F_SortCode;
                frameItemModel.F_EnabledMark = itemsEntity.F_EnabledMark;
                frameItemModel.F_Description = itemsEntity.F_Description;
                frameItemsDAO.Update(frameItemModel, userId);
            }
            else
            {
                frameItemModel = AutoMapperHelper.MapTo<FrameItemModel>(itemsEntity);
                frameItemsDAO.Create(frameItemModel, userId);
            }
        }
    }
}
