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
    public class FrameItemDetailService
    {
        public FrameItemDetailDAO frameItemsDetailDAO = new FrameItemDetailDAO();
        public List<FrameItemsDetailModel> GetList(string itemId = "", string keyword = "")
        {
            var expression = ExtLinq.True<FrameItemsDetailModel>();
            if (!string.IsNullOrEmpty(itemId))
            {
                expression = expression.And(t => t.F_ItemId == itemId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_ItemName.Contains(keyword));
                expression = expression.Or(t => t.F_ItemCode.Contains(keyword));
            }
            expression = expression.And(testc => testc.F_DeleteMark == false);
            return frameItemsDetailDAO.Query(expression).OrderBy(t => t.F_SortCode).ToList();
        }

        public List<FrameItemsDetailModel> GetItemList(string enCode)
        {
            return frameItemsDetailDAO.GetItemList(enCode);
        }
        public FrameItemsDetailModel GetForm(string keyValue)
        {
            return frameItemsDetailDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            frameItemsDetailDAO.Delete(keyValue,userId);
        }
        public void SubmitForm(FrameItemsDetailDTO itemsDetailEntity, string keyValue,string userId)
        {
            FrameItemsDetailModel frameItemsDetailModel = new FrameItemsDetailModel();
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameItemsDetailModel = frameItemsDetailDAO.GetById(keyValue);
                frameItemsDetailModel.F_ParentId = itemsDetailEntity.F_ParentId;
                frameItemsDetailModel.F_ItemCode = itemsDetailEntity.F_ItemCode;
                frameItemsDetailModel.F_ItemName = itemsDetailEntity.F_ItemName;
                frameItemsDetailModel.F_SimpleSpelling = itemsDetailEntity.F_SimpleSpelling;
                frameItemsDetailModel.F_IsDefault = itemsDetailEntity.F_IsDefault;
                frameItemsDetailModel.F_Layers = itemsDetailEntity.F_Layers;
                frameItemsDetailModel.F_SortCode = itemsDetailEntity.F_SortCode;
                frameItemsDetailModel.F_EnabledMark = itemsDetailEntity.F_EnabledMark;
                frameItemsDetailModel.F_Description = itemsDetailEntity.F_Description;
                frameItemsDetailDAO.Update(frameItemsDetailModel, userId);
            }
            else
            {
                frameItemsDetailModel = AutoMapperHelper.MapTo<FrameItemsDetailModel>(itemsDetailEntity);
                frameItemsDetailDAO.Create(frameItemsDetailModel, userId);
            }
        }
    }
}
