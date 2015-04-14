using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;
using System.Data.SqlTypes;

namespace XBase.Data.Office.ContractInfoDBHelper
{
    /// <summary>
    /// 类名：ContractInfoDBHelper
    /// 描述： 
    /// 
    /// 作者：包胜东
    /// 创建时间：2014/03/21
    /// </summary>
    ///
    public class ContractInfoDBHelper
    {
        #region 绑定采购供应商类别
        public static DataTable GetdrpCustType()
        {
            string sql = "select ID,TypeName from officedba.CodePublicType where typeflag =7 and typecode =1 and usedstatus=1 AND CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion

        #region 绑定采购供应商分类
        public static DataTable GetdrpCustClass()
        {
            string sql = "select ID,CodeName from officedba.CodeCompanytype where BigType =2  and usedstatus=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql,param );
            return data;
        }
        #endregion
        /// <summary>
        /// 绑定结算方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpSettleType()
        {
            string sql = "select top 1 0 as ID ,'--请选择--' as TypeName from officedba.Codepublictype union all" +
           " select ID,TypeName from  officedba.Codepublictype where typeflag=16 and typecode=1  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }

        /// <summary>
        /// 绑定运输类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpTranSportType()
        {
            string sql = " select top 1 0 as ID ,'--请选择--' as TypeName from officedba.Codepublictype union all  "+
            "select ID,TypeName from  officedba.Codepublictype where typeflag=18 and typecode=1   AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
        }

        /// <summary>
        /// 绑定煤种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpCoalType()
        {
            string sql = "select top 1 0 as ID,'--请选择--' as ProductName,'' as ProdNo,'' as specification,0 as UnitID from officedba.ProductInfo union all  " +
           " select    ID,ProductName,ProdNo,isnull(specification,'')specification,UnitID  from  officedba.ProductInfo  where usedstatus=1 and  CompanyCD=@CompanyCD   " +
            "order by  id ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
 
        }
        /// <summary>
        /// 绑定计量单位
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUnitCode()
        {
            string sql = "select c.ID,c.CodeName,isnull(c.Flag,'')Flag,isnull(CodeSymbol,'')as CodeSymbol, "+
            "isnull(Description,'')as Description,c.UsedStatus,isnull(c.ModifiedDate,'') as ModifiedDate ,"+
           " isnull(c.ModifiedUserID,'')as ModifiedUserID,c.Flag as Publicflag from officedba.CodeUnitType as c "+
            " where usedstatus=1 and  c.CompanyCD=@CompanyCD";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;
 
        }

        // //绑定 始发站 (参数 3)、原到站 (参数 5)、终到站 (参数 7)  liuch add
        public static DataTable GetTransStation(int intTypeID)
        {
            string sql = " select top 1 0 as ID ,'--请选择--' as TypeName from officedba.Codepublictype union all  " +
            "select ID,TypeName from  officedba.Codepublictype where typeflag=18 and typecode=" + intTypeID + "  AND CompanyCD=@CompanyCD ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            DataTable data = SqlHelper.ExecuteSql(sql, param);
            return data;

        }
         

    }
}
