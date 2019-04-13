using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameDTO.SysDTO
{
    public class FrameItemsDetailDTO
    {
        public string F_Id { get; set; }
        public string F_ItemId { get; set; }
        public string F_ParentId { get; set; }
        public string F_ItemCode { get; set; }
        public string F_ItemName { get; set; }
        public string F_SimpleSpelling { get; set; }
        public bool? F_IsDefault { get; set; }
        public int? F_Layers { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
    }
}
