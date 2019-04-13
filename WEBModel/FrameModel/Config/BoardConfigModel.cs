using DBBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameModel.Config
{
    [SugarTable("MESBoardInfo")]
    public class BoardConfigModel : BaseModel
    {
        public string F_MAC { get; set; }
        public string F_IP { get; set; }
        public string F_URL { get; set; }
        public string F_Remark { get; set; }
    }
}
