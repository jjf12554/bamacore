using DBBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameModel.Config
{
    [SugarTable("MESProblemSummary")]
    public class MESProblemSummaryModel : BaseModel
    {
        public string F_Problem { get; set; }
        public string F_Reason { get; set; }
        public string F_Solution { get; set; }
        public string F_Remark { get; set; }
    }
}
