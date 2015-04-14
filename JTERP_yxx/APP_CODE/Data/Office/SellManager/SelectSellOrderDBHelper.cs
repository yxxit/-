/*************************************
 * 创建人：何小武
 * 创建日期：2009-12-21
 * 描述：销售订单选择控件业务处理
 ************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Data.Office.SellManager
{
    public class SelectSellOrderDBHelper
    {
        #region 获取销售销售订单列表
        /// <summary>
        /// 获取销售销售订单列表
        /// </summary>        
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="BranchID">分店ID，总店0</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOrderList(string strCompanyCD,int BranchID,int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OrderNo,s.CustID,s.UserID,convert(varchar(100),s.OrderDate,23) as OrderDate,");
            strSql.Append(" convert(varchar(100),s.DeliveryDate,23) as DeliveryDate,s.PreferPrice,s.TotalCount,s.PreferedPrice,");
            strSql.Append(" s.Remark,s.Creator,convert(varchar(100),s.CreateDate,23) as CreateDate ");
            strSql.Append(" ,c.CustName as CustName ,e.EmployeeName as UserName,e2.EmployeeName as CreatorName ");
            strSql.Append(" from officedba.SellOrder as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID ");
            strSql.Append(" left join officedba.EmployeeInfo as e on e.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            strSql.Append(" where s.CompanyCD=@CompanyCD and s.billstatus=2 and s.orderno in(select orderno from officedba.sellorderdetail de where companycd=@CompanyCD and  (de.ordercount-isnull(de.outcount,0))>0)  ");
            strSql.AppendLine("and (  ");
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            strSql.AppendLine("(s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");


            SqlParameter[] param = { 
                                   
                                    new SqlParameter("@CompanyCD",strCompanyCD)
                                   };

            return SqlHelper.CreateSqlByPageExcuteSql(strSql.ToString(), pageIndex, pageCount, ord, param, ref totalCount);
        } 
        #endregion

        #region 获取销售订单明细列表信息
        /// <summary>
        /// 获取销售订单明细列表信息
        /// </summary>
        /// <param name="sellOrderNo">销售订单编号</param>
        /// <param name="sellOrderID">销售订单ID号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOrderDetail(string sellOrderNo,int sellOrderID, string strCompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" SELECT a.ID as DetailID,a.CompanyCD,a.OrderNo ,a.ProductID,isnull(b.ProductName,'') as ProductName,");
            strSql.AppendLine("  isnull(b.ProductNo,'') as ProductNo,isnull(b.Specification,'') as Specification, b.BarCode,");
            strSql.AppendLine("  b.Sell,b.wholesalePrice,d.SellType,");
            strSql.AppendLine("  isnull(c.UnitName,'') as UnitName,a.OrderCount ,a.Price,a.TotalPrice,a.Discount,");
            strSql.AppendLine("  d.CustID,e.CustName as CustName,d.UserID,f.EmployeeName as UserName ");
            strSql.AppendLine("  ,convert(varchar(100),d.DeliveryDate,23) as DeliveryDate,d.PreferPrice,convert(varchar(100),d.OrderDate,23) as OrderDate ");
            //strSql.AppendLine(" ,(select sum(isnull(DetailCount,0)) from officedba.SellOutStorageDetail where FromDetailID=a.ID ");
            //strSql.AppendLine(" and id in(select ID from officedba.selloutstorage where billstatus=2 and companycd='123ch')group by FromDetailID ");
           // strSql.AppendLine(" )as SellOutCount ");
            strSql.AppendLine(" ,isnull(a.OutCount,0) as  SellOutCount");
            strSql.AppendLine("  FROM officedba.SellOrderDetail   as a ");
            strSql.AppendLine("  left join officedba.ProductInfo as b on b.ID=a.ProductID ");
            strSql.AppendLine("  left join officedba.MeasureUnit as c on c.ID=b.UnitID ");
            strSql.AppendLine("  left join officedba.SellOrder d on a.OrderNo=d.OrderNo and a.CompanyCD=d.CompanyCD ");
            strSql.AppendLine("  left join officedba.CustInfo e on d.CustID=e.ID ");
            strSql.AppendLine("  left join officedba.EmployeeInfo f on d.UserID=f.ID ");
            //strSql.AppendLine("  left join ( ");
            //strSql.AppendLine("    select sum(isnull(a2.DetailCount,0)) as SellOutCount,a2.FromDetailID from officedba.SellOutStorageDetail a2 ");
            //strSql.AppendLine("    left join officedba.SellOutStorage b2 on b2.OutNo=a2.OutNo and a2.CompanyCD=b2.CompanyCD ");
            //strSql.AppendLine("    where b2.BillStatus=2 and b2.FromBillID!='' and a2.companyCD=@CompanyCD group by a2.FromDetailID ");
            //strSql.AppendLine("  ) as g on g.FromDetailID=a.ID  ");
            strSql.AppendLine("  where a.CompanyCD=@CompanyCD and (a.ordercount-isnull(a.outcount,0))>0 and a.OrderNo=(select Top 1 OrderNo from officedba.SellOrder where ID=@ID )  ");
            
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", strCompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", sellOrderID));
            comm.CommandText = strSql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        #region 获取销售订单
        /// <summary>
        /// 获取销售订单
        /// </summary>
        /// <param name="sellOrderNo">销售订单编号</param>
        /// <param name="sellOrderID">销售订单ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOrder(string sellOrderNo, int sellOrderID)
        {
            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string strSql = string.Empty;

            strSql = "SELECT A.ID,A.CompanyCD,A.OrderNo,A.BranchID ,A.CustID, "
                         + " A.UserID ,A.DeliveryDate ,A.OrderDate ,A.PreferPrice ,A.TotalCount, "
                         + " A.PreferedPrice  ,A.Remark  ,A.BillStatus  ,A.Creator ,A.CreateDate, "
                         + " A.SellType,ci.CustName,ei.EmployeeName,B.EmployeeName CreatorName,(A.PreferedPrice-isnull(A.MakedBill,0)) as UnMakeBill, "
                         + " (isnull(ci.TotalPay,0)-isnull(ci.MakedBill,0))+ci.CreditLimit as CustUnMakeBill"
                         + " FROM  officedba.SellOrder AS A "
                         + " LEFT JOIN officedba.CustInfo  as ci on A.CustID=ci.ID   "
                         + " left join officedba.EmployeeInfo as ei on A.UserID=ei.ID  "
                         + " left join officedba.EmployeeInfo as B  on A.Creator=B.ID "
                          + "where  A.ID = '" + sellOrderID + "' and A.CompanyCD='" + strCompanyCD + "'  ";

            return SqlHelper.ExecuteSql(strSql);

        }
        #endregion
    }
}
