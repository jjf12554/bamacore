using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameDTO.ConfigDTO
{
    public class MESProblemSummaryDTO
    {
        public string F_Id { get; set; }
        public string F_Problem { get; set; }
        public string F_Reason { get; set; }
        public string F_Solution { get; set; }
        public string F_Remark { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime F_LastModifyTime { get; set; }

    }
}
