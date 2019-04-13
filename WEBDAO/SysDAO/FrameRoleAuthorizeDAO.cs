using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;

namespace WEBDAO.SysDAO
{
    public class FrameRoleAuthorizeDAO:BaseDAO_M<FrameRoleAuthorizeModel>
    {
        public void DeleteRole(string roleId)
        {
            sugarDB.Deleteable<FrameRoleAuthorizeModel>(t => t.F_ObjectId == roleId).ExecuteCommand();
        }
    }
}
