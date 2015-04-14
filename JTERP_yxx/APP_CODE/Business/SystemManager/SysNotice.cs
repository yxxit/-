using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

namespace XBase.Business.SystemManager
{
    /// <summary>
    /// 业务逻辑类SysNotice 的摘要说明。
    /// </summary>
    public class SysNotice
    {
        private readonly XBase.Data.SystemManager.SysNotice dal = new XBase.Data.SystemManager.SysNotice();
        public SysNotice()
        { }
        #region  成员方法

       
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(XBase.Model.SystemManager.SysNotice model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(XBase.Model.SystemManager.SysNotice model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public XBase.Model.SystemManager.SysNotice GetModel(int ID)
        {

            return dal.GetModel(ID);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere,int topN)
        {
            return dal.GetList(strWhere, topN);
        }
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  成员方法
    }
}
