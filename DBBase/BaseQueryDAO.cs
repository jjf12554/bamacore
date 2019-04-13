using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{
    public class BaseQueryDAO<T> where T : class, new()
    {
        public SqlSugarClients.SqlSugarClientsList clients { get; set; }
        protected virtual String SugarclientName { get => "bama"; }
        public SqlSugarClient sugarDB2 { get { return clients[SugarclientName]; } }
        public SqlSugarClient sugarDB = new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
        {
            DbType = SqlSugar.DbType.SqlServer,
            InitKeyType = InitKeyType.Attribute,
            ConnectionString = "Data Source=JX-1804004-ITFB;Initial Catalog=bama;User ID=jjf;Pwd=11111111",
            IsAutoCloseConnection = true,
        });

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual List<T> Query(Expression<Func<T, bool>> expression)
        {
            return sugarDB.Queryable<T>().Where(expression).ToList();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual List<T> QueryAll()
        {
            return sugarDB.Queryable<T>().ToList();
        }

        /// <summary>
        /// 根据ID查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(string id)
        {
            var t = sugarDB.Queryable<T>().InSingle(id);
            if (t == null)
            {
                t = new T();
            }
            return t;
        }

        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool IsExit(Expression<Func<T, bool>> expression)
        {
            return sugarDB.Queryable<T>().Where(expression).Any();
        }
    }
}
