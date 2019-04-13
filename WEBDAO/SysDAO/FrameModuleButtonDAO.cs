using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;
using WEBTools.Convent;

namespace WEBDAO.SysDAO
{
    public class FrameModuleButtonDAO:BaseDAO_M<FrameModuleButtonModel>
    {
        public List<FrameModuleButtonModel> GetList(string moduleId = "")
        {
            var expression = ExtLinq.True<FrameModuleButtonModel>();
            if (!string.IsNullOrEmpty(moduleId))
            {
                expression = expression.And(t => t.F_ModuleId == moduleId);
            }
            return sugarDB.Queryable<FrameModuleButtonModel>().Where(expression).OrderBy(t => t.F_SortCode).ToList();
        }
    }
}
