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
    public class SelectSellOfferUCDBHelper
    {
        /// <summary>
        /// 选择执行状态的单据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSellOfferList(string OrderNo, string Title, string CustName, int? CurrencyType, string model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            strSql = "SELECT     s.ID, s.Title, e.EmployeeName, s.Rate, cts.CurrencyName, c.CustName, CONVERT(varchar(100), s.OfferDate, 23) AS OfferDate, s.QuoteTime,  s.OfferNo ";
            strSql += "FROM         officedba.SellOffer AS s LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS cts ON s.CurrencyType = cts.ID LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID ";
            strSql += "WHERE s.CompanyCD=@CompanyCD ";
            strSql += "and (  ";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dtt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dtt != null && dtt.Rows.Count > 0)
            {
               
                if (dtt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) OR ";
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) ))  OR ";
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID)))  OR ";
                }
            }
            strSql += "  (select COUNT(*) from officedba.FlowTaskHistory where FlowNo=(SELECT TOP 1 FlowNo FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 1) AND BillID = s.ID  AND operateUserId = '" + userInfo.UserID + "')>0 ";

            strSql += "or (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.Seller IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "'))";


            strSql += " or ( charindex('," + userInfo.EmployeeID + ",' , ','+s.CanViewUser+',')>0 )) ";
           
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (OrderNo != null)
            {
                strSql += " and s.OfferNo like  '%" + OrderNo + "%'";
            }
            if (Title != null)
            {
                strSql += " and s.Title like '%" + Title + "%'";

            }
            if (CustName != null)
            {
                strSql += " and c.CustName like '%" + CustName + "%'";

            }
            if (CurrencyType != null)
            {
                strSql += " and s.CurrencyType = " + CurrencyType;

            }
            if (model != "all")
            {
                strSql += " and  s.BillStatus = '2' ";
            }
            else
            {
                strSql += " and  s.BillStatus <> '1' ";
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }

        /// <summary>
        /// 获取报价单详细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        public static DataTable GetSellOffer( string strOffNo)
        {
            string strSql = string.Empty;//查询报价单信息
            string strCompanyCD = string.Empty;//单位编号

           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          

            strSql = "SELECT s.CustID, s.OfferNo, s.ID, s.CustTel, s.Seller, s.SellDeptId, s.SellType, ";
            strSql += "s.BusiType, s.PayType, s.CarryType, s.TakeType, s.MoneyType, e.EmployeeName, ";
            strSql += "s.CurrencyType, s.Rate, s.isAddTax, d.DeptName, c.CustName, ct.CurrencyName,s.Discount ";
            strSql += "FROM officedba.SellOffer AS s LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON s.SellDeptId = d.ID LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS ct ON s.CurrencyType = ct.ID ";

            strSql += " WHERE (s.OfferNo = @OfferNo ) and s.CompanyCD=@CompanyCD  ORDER BY s.CreateDate DESC";
            SqlParameter[] paras = { new SqlParameter("@OfferNo", strOffNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            return SqlHelper.ExecuteSql(strSql, paras);
            
        }

        /// <summary>
        /// 获取报价单明细信息
        /// </summary>
        /// <param name="strOffNo"></param>
        /// <returns></returns>
        public static DataTable GetSellOfferInfo(string strOffNo)
        {
            string strSql = string.Empty;//查询报价单明细信息
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          
            strSql = "SELECT s.OfferNo, s.SortNo, s.ProductID, isnull(s.ProductCount,0) as ProductCount, s.UnitID, ";
            strSql += "s.UnitPrice, s.Discount, s.TaxPrice, s.TaxRate, s.TotalFee, s.TotalPrice, s.TotalTax,isnull(pc.TypeName,'') as ColorName, ";
            strSql += "s.Package,isnull(p.StandardCost,0) as StandardCost, p.ProdNo, p.ProductName, p.Specification, c.CodeName,s.SendTime ";
            strSql += " ,isnull(p.StandardSell,0) as StandardSell,s.UsedUnitID,isnull(s.UsedUnitCount,0) as UsedUnitCount,isnull(s.UsedPrice,0) as UsedPrice,isnull(s.ExRate,1) as ExRate ";
            strSql += " ,isnull(p.ExtField1,'') ExtField1,isnull(p.ExtField2,'') ExtField2,isnull(p.ExtField3,'') ExtField3,isnull(p.ExtField4,'') ExtField4,";
            strSql += " isnull(p.ExtField5,'') ExtField5,isnull(p.ExtField6,'') ExtField6,isnull(p.ExtField7,'') ExtField7,";
            strSql += " isnull(p.ExtField8,'') ExtField8,isnull(p.ExtField9,'') ExtField9,isnull(p.ExtField10,'') ExtField10 ";
            strSql += "FROM officedba.SellOfferDetail AS s LEFT OUTER JOIN ";
            strSql += "officedba.ProductInfo AS p ON s.ProductID = p.ID LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType as pc on pc.ID=p.ColorID left join ";
            strSql += "officedba.CodeUnitType AS c ON s.UnitID = c.ID ";


            strSql += " WHERE (s.OfferNo = @OfferNo ) and s.CompanyCD=@CompanyCD  order by s.SortNo asc";

            SqlParameter[] paras = { new SqlParameter("@OfferNo", strOffNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            return SqlHelper.ExecuteSql(strSql,paras);
        }
    }
}
