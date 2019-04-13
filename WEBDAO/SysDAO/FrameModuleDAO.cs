using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;

namespace WEBDAO.SysDAO
{
    public class FrameModuleDAO:BaseDAO_M<FrameModuleModel>
    {
        public List<FrameModuleModel> GetList()
        {
            return sugarDB.Queryable<FrameModuleModel>().OrderBy(t => t.F_SortCode).ToList();
        }
    }
}
