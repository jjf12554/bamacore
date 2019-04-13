using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{
    public class BaseDAO_M<T>: BaseDAO<T> where T : BaseModel ,new()
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public T Create(T t,string userId)
        {
            if (string.IsNullOrEmpty(t.F_Id))
            {
                t.F_Id = Guid.NewGuid().ToString();
            }
            t.F_CreatorTime = DateTime.Now;
            t.F_CreatorUserId = userId;
            t.F_LastModifyTime = DateTime.Now;
            t.F_LastModifyUserId = userId;
            t.F_DeleteMark = false;
            return base.Create(t);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Create(List<T> list, string userId)
        {
            foreach (var t in list)
            {
                t.F_Id = Guid.NewGuid().ToString();
                t.F_CreatorTime = DateTime.Now;
                t.F_CreatorUserId = userId;
                t.F_LastModifyTime = DateTime.Now;
                t.F_LastModifyUserId = userId;
                t.F_DeleteMark = false;
            }
            
            return base.Create(list);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Update(T t,string userId)
        {
            t.F_LastModifyTime = DateTime.Now;
            t.F_LastModifyUserId = userId;
            return base.Update(t);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Delete(string id,string userId)
        {
            T t = base.GetById(id);
            t.F_DeleteMark = true;
            t.F_DeleteTime = DateTime.Now;
            t.F_DeleteUserId = userId;
            return base.Update(t);
        }

        public override List<T> QueryAll()
        {
            return sugarDB.Queryable<T>().Where(t=>t.F_DeleteMark == false).ToList();
        }
    }
}
