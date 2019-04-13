using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;
using WEBTools;

namespace WEBDAO.SysDAO
{
    public class FrameUserDAO:BaseDAO_M<FrameUserModel>
    {
        public List<FrameUserModel> FindList(string keyword, Pagination pagination)
        {
            int total = 0;
            var sql = sugarDB.Queryable<FrameUserModel>()
                .WhereIF(!string.IsNullOrEmpty(keyword), t => t.F_Account.Contains(keyword))
                .Where(t=>t.F_Account!="bamaadmin")
                .Where(t=>t.F_DeleteMark==false)
                //.OrderByIF(!string.IsNullOrEmpty(pagination.sidx),t=> pagination.sidx,)
                .OrderBy(t=>t.F_LastModifyTime,SqlSugar.OrderByType.Desc)
                .OrderBy(t => t.F_Id)
                .ToPageList(pagination.page, pagination.rows, ref total).ToList();
            pagination.records = total;
            return sql;
        }
    }
}
