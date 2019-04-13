using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;

namespace WEBDAO.SysDAO
{
    public class FrameItemDetailDAO:BaseDAO_M<FrameItemsDetailModel>
    {
        public List<FrameItemsDetailModel> GetItemList(string enCode)
        {
            var sql = sugarDB.Queryable<FrameItemsDetailModel, FrameItemModel>((id, i) => id.F_ItemId == i.F_Id)
                .Where((id, i)=>i.F_EnCode == enCode)  
                .Where((id, i) => id.F_EnabledMark == true)
                .Where((id, i) => id.F_DeleteMark == false)
                .Select((id, i)=>id);
            return sql.ToList();
        }
    }
}
