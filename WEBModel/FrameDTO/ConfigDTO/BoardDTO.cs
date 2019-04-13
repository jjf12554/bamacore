using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameDTO.ConfigDTO
{
    public class BoardDTO
    {
        public string F_Id { get; set; }
        public string F_MAC { get; set; }
        public string F_IP { get; set; }
        public string F_URL { get; set; }
        public string F_Remark { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime F_LastModifyTime { get; set; }

    }
}
