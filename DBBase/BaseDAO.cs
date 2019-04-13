using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{
    public class BaseDAO<T> : BaseQueryDAO<T> where T: class,new()
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        //public virtual int Create(T t)
        //{
        //    return sugarDB.Insertable(t).ExecuteCommand();
        //}
        public virtual T Create(T t)
        {
            return sugarDB.Insertable(t).ExecuteReturnEntity();
        }
        public virtual int Create(List<T> list)
        {
            return sugarDB.Insertable(list).ExecuteCommand();
        }
        #region 删除数据
        public virtual int Delete(T t)
        {
            return sugarDB.Deleteable<T>().Where(t).ExecuteCommand();
        }
        public virtual int Delete(List<T> list)
        {
            return sugarDB.Deleteable<T>().Where(list).ExecuteCommand();
        }
        public virtual int Delete(string id)
        {
            return sugarDB.Deleteable<T>().In(id).ExecuteCommand();
        }
        public virtual int Delete(List<string> list)
        {
            return sugarDB.Deleteable<T>().In(list).ExecuteCommand();
        }
        #endregion

        #region 更新数据
        public virtual int Update(T t)
        {
            return sugarDB.Updateable(t).ExecuteCommand();
        }
        public virtual int Update(List<T> t)
        {
            return sugarDB.Updateable(t).ExecuteCommand();
        }
        #endregion
    }
}
