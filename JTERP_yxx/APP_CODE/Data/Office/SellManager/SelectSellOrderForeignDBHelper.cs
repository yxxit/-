/*************************************
 * 创建人：宋凯歌
 * 创建日期：2010-11-19
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

namespace XBase.Data.Office.SellManager
{
    public class SelectSellOrderForeignDBHelper
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
        public static DataTable GetSellOrderList(string strCompanyCD, int BranchID, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OrderNo,s.CustID,s.UserID,convert(varchar(100),s.OrderDate,23) as OrderDate,s.TotalOrders,s.TotalCost,");
            strSql.Append(" convert(varchar(100),s.DeliveryDate,23) as DeliveryDate,");//s.PreferPrice,s.TotalCount,s.PreferedPrice,
            strSql.Append(" s.Remark,s.Creator,convert(varchar(100),s.CreateDate,23) as CreateDate ");
            strSql.Append(" ,c.CustName as CustName ,e.EmployeeName as UserName,e2.EmployeeName as CreatorName ");
            strSql.Append(" from officedba.SellOrderForeign as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID ");
            strSql.Append(" left join officedba.EmployeeInfo as e on e.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            strSql.Append(" where s.CompanyCD=@CompanyCD and s.BranchID=@BranchID and s.BillStatus = '2' and s.IsInquiry <> '1' and (s.Shipments <> '2' or s.Shipments is null  ) ");
            strSql.Append("and s.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            strSql.Append("  INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            strSql.Append(" CROSS JOIN            officedba.UserInfo AS v  ");
            strSql.Append(" INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            strSql.Append("  INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            strSql.Append("                WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "')) ");


            SqlParameter[] param = { 
                                    new SqlParameter("@BranchID",BranchID),
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
        public static DataTable GetSellOrderDetail(string sellOrderNo, int sellOrderID, string strCompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" SELECT a.ID as DetailID,a.CompanyCD,a.OrderNo ,a.ProductID,isnull(b.ProductName,'') as ProductName,a.PriceType,a.CostPrice,a.SalesPrice,a.Difference,a.Ratio,");
            strSql.AppendLine("  isnull(b.ProdNo,'') as ProductNo,isnull(b.Specification,'') as Specification, b.BarCode,");
            //strSql.AppendLine("  b.Sell,b.wholesalePrice,");
            strSql.AppendLine("  d.SellType,isnull(c.CodeName,'') as UnitName,a.OrderCount ,");//a.Price,a.TotalPrice,a.Discount,
            strSql.AppendLine("  d.CustID,e.CustName as CustName,d.UserID,f.EmployeeName as UserName ,d.OrderRate,d.Currency");
            strSql.AppendLine("  ,convert(varchar(100),d.DeliveryDate,23) as DeliveryDate,convert(varchar(100),d.OrderDate,23) as OrderDate,");//d.PreferPrice,
            strSql.Append(" (select Top 1 LinkManName from officedba.CustLinkMan as d1 where d1.CustNo = e.CustNo and d1.CompanyCD = e.CompanyCD) as LinkManName,");
            strSql.Append(" (select Top 1 WorkTel from officedba.CustLinkMan as d1 where d1.CustNo = e.CustNo and d1.CompanyCD = e.CompanyCD) as WorkTel,");
            strSql.Append(" (select Top 1 MSN from officedba.CustLinkMan as d1 where d1.CustNo = e.CustNo and d1.CompanyCD = e.CompanyCD) as QQ");
            //strSql.AppendLine(" ,(select sum(isnull(DetailCount,0)) from officedba.SellOutStorageDetail where FromDetailID=a.ID ");
            //strSql.AppendLine(" and id in(select ID from officedba.selloutstorage where billstatus=2 and companycd='123ch')group by FromDetailID ");
            // strSql.AppendLine(" )as SellOutCount ");
            strSql.AppendLine(" ,isnull(a.NumberShipments,0) as  SellOutCount");
            strSql.AppendLine("  FROM officedba.SellOrderDetailForeign   as a ");
            strSql.AppendLine("  left join officedba.ProductInfo as b on b.ID=a.ProductID and a.CompanyCD=b.CompanyCD");
            //strSql.AppendLine("  left join officedba.MeasureUnit as c on c.ID=b.UnitID and c.CompanyCD=b.CompanyCD");
            strSql.AppendLine("  left join officedba.CodeUnitType as c on c.ID=b.UnitID and c.CompanyCD=b.CompanyCD");
            strSql.AppendLine("  left join officedba.SellOrderForeign d on a.OrderNo=d.OrderNo and a.CompanyCD=d.CompanyCD ");
            strSql.AppendLine("  left join officedba.CustInfo e on d.CustID=e.ID and e.CompanyCD=d.CompanyCD");
            // strSql.AppendLine("  left join officedba.CustLinkMan h on h.CustID=e.ID and e.CompanyCD=h.CompanyCD");
            strSql.AppendLine("  left join officedba.EmployeeInfo f on d.UserID=f.ID and d.CompanyCD=f.CompanyCD");
            //strSql.AppendLine("  left join ( ");
            //strSql.AppendLine("    select a2.NumberShipments as SellOutCount,a2.OrderNo from officedba.SellOrderDetailForeign a2 ");
            //strSql.AppendLine("    left join officedba.SellOrderForeign b2 on b2.OutNo=a2.OutNo and a2.CompanyCD=b2.CompanyCD ");
            //strSql.AppendLine("    where b2.BillStatus=2  and a2.companyCD=@CompanyCD ");
            //strSql.AppendLine("  ) as g on g.OrderNo=@sellOrderNo ");
            //strSql.AppendLine("  left join ( ");
            //strSql.AppendLine("    select sum(isnull(a2.Shipments,0)) as SellOutCount,a2.FromDetailID from officedba.SellOutStorageDetailForeign a2 ");
            //strSql.AppendLine("    left join officedba.SellOutStorageForeign b2 on b2.OutNo=a2.OutNo and a2.CompanyCD=b2.CompanyCD ");
            //strSql.AppendLine("    where b2.BillStatus=2 and b2.FromBillID!='' and a2.companyCD=@CompanyCD group by a2.FromDetailID ");
            //strSql.AppendLine("  ) as g on g.FromDetailID=a.ID ");
            strSql.AppendLine("  where a.CompanyCD=@CompanyCD and a.OrderNo=@sellOrderNo ");//(select Top 1 OrderNo from officedba.SellOrderForeign where ID=@ID )

            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", strCompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", sellOrderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@sellOrderNo", sellOrderNo));
            comm.CommandText = strSql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion
    }
}
