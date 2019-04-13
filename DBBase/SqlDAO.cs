using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{
    public class SqlDAO
    {
        public SqlSugarClients.SqlSugarClientsList clients { get; set; }
        protected virtual String SugarclientName { get => "mes"; }
        public SqlSugarClient sugarDB { get { return clients[SugarclientName]; } }
        public DataTable GetDataTableBySql(string sql, params SugarParameter[] SugarParameters)
        {
            return sugarDB.Ado.GetDataTable(sql, SugarParameters);
        }
    }
}
