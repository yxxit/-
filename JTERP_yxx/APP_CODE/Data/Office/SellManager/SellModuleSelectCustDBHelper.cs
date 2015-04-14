using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SellManager;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellModuleSelectCustDBHelper
    {

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCustList(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            strSql = "SELECT ID, CustNo, CustName, ArtiPerson, CustNote, Relation" +
                     " FROM officedba.CustInfo " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            //过滤单据：显示当前用户拥有权限查看的单据
            int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            strSql += " AND  (  ";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql += " (Creator IN  ";
                    strSql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql += " (Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql += " (Creator IN  ";
                    strSql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID))) or ";
                }
            }


            strSql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+CanViewUser+',')>0 )";
            strSql += " or (Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
              

            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";

            }
            if (model != "all")
            {
                strSql += " and  UsedStatus = '1' ";
            }
           
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
            
        }

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProviderList(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号


            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql = "SELECT ID, CustNo, CustName, ArtiPerson, CustNote" +
                     " FROM officedba.ProviderInfo " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            ////过滤单据：显示当前用户拥有权限查看的单据
            //int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //strSql += " AND  (  ";
            //XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            //DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            //if (dt != null && dt.Rows.Count > 0)
            //{

            //    if (dt.Rows[0]["RoleRange"].ToString() == "1")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
            //    }
            //    if (dt.Rows[0]["RoleRange"].ToString() == "2")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID) )) or ";
            //    }
            //    if (dt.Rows[0]["RoleRange"].ToString() == "3")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID))) or ";
            //    }
            //}


            //strSql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+CanViewUser+',')>0 )";
            //strSql += " or (Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";


            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";
            }
            if (model != "all")
            {
                strSql += " and  UsedStatus = '1' ";
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);

        }

        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable getother(string OrderNo, string Title, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strCompanyCD = string.Empty;//单位编号


            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql = "SELECT ID, CustNo, CustName, isnull(ArtiPerson,0)ArtiPerson, isnull(CustNote,0)CustNote" +
                     " FROM [officedba].[OtherCorpInfo] " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));

            ////过滤单据：显示当前用户拥有权限查看的单据
            //int empid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            //strSql += " AND  (  ";
            //XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            //DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            //if (dt != null && dt.Rows.Count > 0)
            //{

            //    if (dt.Rows[0]["RoleRange"].ToString() == "1")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
            //    }
            //    if (dt.Rows[0]["RoleRange"].ToString() == "2")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID) )) or ";
            //    }
            //    if (dt.Rows[0]["RoleRange"].ToString() == "3")
            //    {
            //        strSql += " (Creator IN  ";
            //        strSql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
            //        strSql += "  WHERE DeptID IN (SELECT a.ID  ";
            //        strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
            //        strSql += "  WHERE a.ID=b.ID))) or ";
            //    }
            //}


            //strSql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+CanViewUser+',')>0 )";
            //strSql += " or (Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (Manager IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";


            if (OrderNo != null)
            {
                strSql += " and CustNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and CustName like  '%" + Title + "%'";
            }
            if (model != "all")
            {
                strSql += " and  UsedStatus = '1' ";
            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);

        }
        /// <summary>
        /// 获取客户详细信息
        /// </summary>
        /// <param name="strID">客户编号</param>
        /// <returns></returns>
        public static DataTable GetCustInfo(string strID)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           

            strSql = "SELECT officedba.CustInfo.ID,isnull(officedba.CustInfo.CustNo,'') CustNo,isnull(officedba.CustInfo.CustType,'') as CustType , " +
                     " officedba.CustInfo.CurrencyType, officedba.CustInfo.TakeType, " +
                     " officedba.CustInfo.PayType,officedba.CustInfo.Tel,officedba.CustInfo.MoneyType, officedba.CustInfo.BusiType," +
                     " officedba.CustInfo.CarryType, officedba.CustInfo.CustName, " +
                     " officedba.CodePublicType.TypeName, officedba.CurrencyTypeSetting.ExchangeRate, officedba.CurrencyTypeSetting.CurrencyName " +
                     " FROM officedba.CustInfo LEFT OUTER JOIN " +
                     " officedba.CodePublicType ON officedba.CustInfo.CustType = officedba.CodePublicType.ID LEFT OUTER JOIN " +
                     " officedba.CurrencyTypeSetting ON officedba.CustInfo.CurrencyType = officedba.CurrencyTypeSetting.ID";
            strSql += " where officedba.CustInfo.ID='" + strID + "' and officedba.CustInfo.CompanyCD='" + strCompanyCD + "'";
            return SqlHelper.ExecuteSql(strSql);
        }
    }
}
