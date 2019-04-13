using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameDTO.ConfigDTO
{
    public class KBDPersonInfoDTO
    {
        public string personId { get; set; }
        public string personNo { get; set; }
        public string personName { get; set; }
        public string workCenter { get; set; }
        public string lastLeader { get; set; }
        public string lastLeaderPhone { get; set; }
        public string sex { get; set; }
        public string organize { get; set; }
        public byte[] personImage { get; set; }
    }
}
