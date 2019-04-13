using DBBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBModel.FrameModel.SysModel;

namespace WEBDAO.SysDAO
{
    public class FrameRoleDAO:BaseDAO_M<FrameRoleModel>
    {
        public void SubmitForm(FrameRoleModel roleEntity, List<FrameRoleAuthorizeModel> roleAuthorizeEntitys, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                sugarDB.Insertable(roleEntity);
            }
            else
            {
                roleEntity.F_Category = 1;
                sugarDB.Insertable(roleEntity);
            }
            sugarDB.Deleteable<FrameRoleAuthorizeModel>(t => t.F_ObjectId == roleEntity.F_Id);
            sugarDB.Insertable(roleAuthorizeEntitys);
        }
    }
}
