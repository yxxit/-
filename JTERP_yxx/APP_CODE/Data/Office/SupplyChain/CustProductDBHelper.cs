using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SupplyChain;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Data.SqlTypes;
using System.Data;
using XBase.Common;
using System.Collections;


namespace XBase.Data.Office.SupplyChain
{
    public class CustProductDBHelper
    {
        #region 【添加客户商品别名】
        /// <summary>
        /// 添加客户商品别名
        /// </summary>
        /// <returns>true 成功 false 失败</returns>
        public static bool InsertCustProduct(CustProductModel CustProdModel)
        {
            bool bState = false;
            string strSql = string.Format("INSERT INTO [officedba].[CustProdDetails]" +
           " ([CompanyCD]  ,[CustID]  ,[ProdNo],[ProdAlias]  ,[ProdPrice],[IsStop]  ,[Creator] ,[CreateDate])" +
         " VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}', '{7}')"
         , CustProdModel.CompanyCD, CustProdModel.CustID, CustProdModel.ProdNo, CustProdModel.ProdAlias,
         CustProdModel.ProdPrice, CustProdModel.IsStop, CustProdModel.Creator, DateTime.Now.ToString());
            try
            {
                if (SqlHelper.ExecuteWithSQL(strSql))
                {
                    bState = true;
                }
                else
                {
                    bState = false;
                }
            }
            catch (Exception ex)
            {
                bState = false;
            }

            return bState;
        }
        #endregion

        #region 【判断该客户某商品别名是否存在】
        /// <summary>
        /// 判断该客户某商品别名是否存在
        /// </summary>
        /// <param name="CustProdModel"></param>
        /// <returns>true 可以插入 false 已近存在不可以插入</returns>
        public static bool CheckCustProduct(CustProductModel CustProdModel)
        {
            bool bState = false;
            string strSql = string.Format("select CustID from [officedba].[CustProdDetails] where " +
           " [CompanyCD]='{0}'  and [CustID]='{1}'  and [ProdNo]='{2}'"
         , CustProdModel.CompanyCD, CustProdModel.CustID, CustProdModel.ProdNo);
            try
            {
                if (SqlHelper.ExecuteSql(strSql).Rows.Count == 0)
                {
                    bState = true;
                }
                else
                {
                    bState = false;
                }
            }
            catch (Exception ex)
            {
                bState = false;
            }

            return bState;
        }
        #endregion

        #region 【修改客户商品别名】
        /// <summary>
        /// 修改客户商品别名
        /// </summary>
        /// <returns>true 成功 false 失败</returns>
        public static bool UpdateCustProduct(CustProductModel CustProdModel)
        {
            bool bState = false;
            string strSql = string.Format("UPDATE [officedba].[CustProdDetails]" +
           "  set [ProdAlias]='{0}'  ,[ProdPrice]='{1}',[IsStop]='{2}',ModifiedDate='{3}',ModifiedUserID='{4}' " +
         "  where [CompanyCD]='{5}'  and  [ID]='{6}'"
         , CustProdModel.ProdAlias,
         CustProdModel.ProdPrice, CustProdModel.IsStop, DateTime.Now.ToString(), CustProdModel.Creator, CustProdModel.CompanyCD, CustProdModel.ID);
            try
            {

                if (SqlHelper.ExecuteWithSQL(strSql))
                {
                    bState = true;
                }
                else
                {
                    bState = false;
                }
            }
            catch (Exception ex)
            {
                bState = false;
            }

            return bState;
        }
        #endregion

        #region 【删除客户商品别名】
        /// <summary>
        /// 删除客户商品别名
        /// </summary>
        /// <returns>true 成功 false 失败</returns>
        public static bool DeleteCustProduct(string id)
        {
            bool bState = false;
            string strSql = string.Format("DELETE FROM [officedba].[CustProdDetails]" +
         "  where   [ID] in ( {0})"
        , id);
            try
            {
                if (SqlHelper.ExecuteWithSQL(strSql))
                {
                    bState = true;
                }
                else
                {
                    bState = false;
                }
            }
            catch (Exception ex)
            {
                bState = false;
            }

            return bState;
        }
        #endregion

        #region 【列表检索】
        /// <summary>
        /// 列表检索
        /// </summary>
        /// <param name="CustProdModel"></param>
        /// <returns>datatable</returns>
        public static DataTable GetDataTable(CustProductModel CustProdModel, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            bool bState = false;
            string StrWhere = "";
            try
            {

                if (!string.IsNullOrEmpty(CustProdModel.CustName.ToString().Trim()))
                {
                    StrWhere = " and  b.CustName like '%" + CustProdModel.CustName + "%' ";
                }
                if (!string.IsNullOrEmpty(CustProdModel.ProdName.ToString().Trim()))
                {
                    StrWhere += " and  c.ProductName like '%" + CustProdModel.ProdName + "%' ";
                }
                if (!string.IsNullOrEmpty(CustProdModel.ProdAlias.ToString().Trim()))
                {
                    StrWhere += " and  a.ProdAlias like '%" + CustProdModel.ProdAlias + "%' ";
                }
                if (CustProdModel.ProdPrice > -1)
                {
                    StrWhere += " and  a.ProdPrice= " + CustProdModel.ProdPrice;
                }
                if (CustProdModel.IsStop > -1)
                {
                    StrWhere += " and  a.IsStop= " + CustProdModel.IsStop;
                }

                string strSql = string.Format("SELECT a.[ID],b.CustName,c.ProductName ,a.[CustID],a.[ProdNo],a.[ProdAlias],a.[ProdPrice] ,a.[IsStop]   " +
                 " FROM [officedba].[CustProdDetails] as a " +
                " left join officedba.CustInfo  as b on a.CustID = b.ID " +
                " left join officedba.ProductInfo as c  ON a.ProdNo = c.ProdNo " +
                " where  a.CompanyCD='{0}'and b.CompanyCD='{0}'and c.CompanyCD='{0}' {1}"
             , CustProdModel.CompanyCD, StrWhere);
                SqlCommand comm = new SqlCommand();
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(strSql);

                comm.CommandText = sql.ToString();
                return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 列表检索
        /// </summary>
        /// <param name="CustProdModel"></param>
        /// <returns>datatable</returns>
        public static DataTable GetDataTableByCompanyCD(string CompanyCD)
        {
            bool bState = false;
           
            try
            {

                

                string strSql = string.Format("SELECT a.[ID],b.CustName,c.ProductName ,a.[CustID],a.[ProdNo],a.[ProdAlias],a.[ProdPrice] ,case when a.[IsStop]=1 then '启用' when a.[IsStop]=0 then '停用' end as IsStop   " +
                 " FROM [officedba].[CustProdDetails] as a " +
                " left join officedba.CustInfo  as b on b.ID=a.CustID and b.CompanyCD='{0}'" +
                " left join officedba.ProductInfo as c on c.ProdNo=a.ProdNo and b.CompanyCD='{0}'" +
                " where  a.CompanyCD='{0}' "
                 , CompanyCD);
                SqlCommand comm = new SqlCommand();
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(strSql);

                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetAlias(string companycd,string custid,string prodno)
        {
            try
            {

                string strSql = string.Format("select CustID,[ProdAlias],[ProdPrice]  from [officedba].[CustProdDetails] where " +
                                  " [CustID]='{0}'  and [ProdNo]='{1}' and CompanyCD='{2}' and isstop>0"
                                 , custid, prodno,companycd);
                SqlCommand comm = new SqlCommand();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(strSql);
                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetProd(string CustID,string ProdNo)
        {
            bool bState = false;

            try
            {

                string strSql = string.Format("select CustID,[ProdAlias],[ProdPrice]  from [officedba].[CustProdDetails] where " +
           " [CustID]='{0}'  and [ProdNo]='{1}' and isstop>0"
         ,  CustID, ProdNo);

               
                SqlCommand comm = new SqlCommand();
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(strSql);

                comm.CommandText = sql.ToString();
                return SqlHelper.ExecuteSearch(comm);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }

}