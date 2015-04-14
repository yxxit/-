using System;
using System.Data;
using System.Collections.Generic;

namespace XBase.Business.Personal.Culture
{
	/// <summary>
	/// 业务逻辑类CultureDocs 的摘要说明。
	/// </summary>
	public class CultureDocs
	{
		private readonly XBase.Data.Personal.Culture.CultureDocs dal=new XBase.Data.Personal.Culture.CultureDocs();
		public CultureDocs()
		{}
		#region  成员方法

          /// <summary>
        /// 是否存在该记录
        /// </summary>
        public int Exists(string where)
        {
            return dal.Exists(where);
        }
		

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
		public int  Add(XBase.Model.Personal.Culture.CultureDocs model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(XBase.Model.Personal.Culture.CultureDocs model)
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
		public XBase.Model.Personal.Culture.CultureDocs GetModel(int ID)
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
        /// GetPageData
        /// </summary>    
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="orderExp"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public int GetPageData(out DataTable dt, string where, string fields, string orderExp, int pageindex, int pagesize)
        {
            return dal.GetPageData(out dt, where, fields, orderExp, pageindex, pagesize);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetCultureList(int pageIndex, int pageCount, string ord, ref int TotalCount, string time1, string time2, string strTitle, string content, string createname,string culturetype)
        {
            return dal.GetCultureList(pageIndex, pageCount, ord, ref TotalCount, time1, time2, strTitle, content, createname,culturetype);
        }

        ///<summary>
        ///保存页面文档内容到数据库
        ///</summary>
        public int addDoctent(string DocsID, string docType, byte[] docContent)
        {
            return dal.addDoctent(DocsID, docType, docContent);
        }

        ///<summary>
        ///新建时修改主ID
        ///</summary>
        public void UpdateMainID(int DocsID)
        {
             dal.UpdateMainID(DocsID);
        }
        ///<summary>
        ///获取文档表中某条企业文化关联的最大ID的一条记录（即最新的数据）
        ///</summary>
        public DataTable GetLastUpdate(int DocsID)
        {
           return dal.GetLastUpdate(DocsID);
        }
        ///<summary>
        ///获取文档表中某条记录，根据ID
        ///</summary>
        public DataTable GetCultureDocsFile(int DocsID)
        {
            return dal.GetCultureDocsFile(DocsID);
        }

        /// <summary>
        /// 得到一个企业文化关联的文档的修改次数
        /// </summary>
        public int GetModCount(int ID)
        {

            return dal.GetModCount(ID);
        }

        /// <summary>
        /// 获取某条企业文化记录的修改历史
        /// </summary>
        public DataTable GetCultureDocsFileList(string strDocsID, int pageIndex,int pageCount, string ord, ref int totalCount)
        {

            return dal.GetCultureDocsFileList(strDocsID, pageIndex, pageCount, ord, ref totalCount);
        }

        /// <summary>
        /// 删除一条子项数据
        /// </summary>
        public void DelSubItem(int ID)
        {

            dal.DelSubItem(ID);
        }

		#endregion  成员方法
	}
}

