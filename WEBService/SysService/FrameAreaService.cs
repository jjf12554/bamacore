using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBDAO.SysDAO;
using WEBModel.FrameModel.SysModel;

namespace WEBService.SysService
{
    public class FrameAreaService
    {
        public FrameAreaDAO frameAreaDAO { get; set; }
        public List<FrameAreaModel> GetList()
        {
            return frameAreaDAO.QueryAll().ToList();
        }
        public FrameAreaModel GetForm(string keyValue)
        {
            return frameAreaDAO.GetById(keyValue);
        }
        public void DeleteForm(string keyValue,string userId)
        {
            if (frameAreaDAO.QueryAll().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                frameAreaDAO.Delete(keyValue, userId);
            }
        }
        public void SubmitForm(FrameAreaModel areaEntity, string keyValue,string userId)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                frameAreaDAO.Update(areaEntity, userId);
            }
            else
            {
                frameAreaDAO.Create(areaEntity, userId);
            }
        }
    }
}
