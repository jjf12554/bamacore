using DBBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBModel.FrameModel.SysModel
{
    [SugarTable("Sys_Duty")]
    public class FrameDutyModel :BaseModel
    {
        public string F_OrganizeId { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public string F_Type { get; set; }
        public bool? F_AllowEdit { get; set; }
        public bool? F_AllowDelete { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
    }
}
