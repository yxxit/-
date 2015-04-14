/*************************************
 * 创建人：宋凯歌
 * 创建日期：2010-11-26
 * 描述：销售出库
 ************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellManager;
using System.Web.UI.WebControls;

namespace XBase.Data.Office.SellManager
{
    public class CostsForeignDBHelper
    {
        #region 获取销售出库单列表
        /// <summary>
        /// 获取销售出库单列表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetCostsForeignList(SellOutStorageForeignModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OutNo,s.CustID,");
            strSql.Append(" s.BillStatus,c.CustName as CustName,s.InvoiceNo,CONVERT(varchar(100),s.ExchangeDate,23) as ExchangeDate,h.OrderNo,k.ProductName,g.SalesPrice,g.DetailCount, ");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '收款' when 4 then '作废' end as BillStatusText ");
            strSql.Append(" from officedba.SellOutStorageForeign as s ");
            strSql.Append(" right join officedba.SellOutStorageDetailForeign as g on g.OutNo=s.OutNo and g.CompanyCD = s.CompanyCD   ");
            strSql.Append(" right join officedba.ProductInfo as k on k.ID=g.ProductID and k.CompanyCD = g.CompanyCD   ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and c.CompanyCD = s.CompanyCD   ");//and (NatureType=2 or NatureType=3)
            strSql.Append(" left join officedba.SellOrderForeign as h on h.ID=s.FromBillID and h.CompanyCD = s.CompanyCD   ");
            strSql.Append(" left join officedba.CurrencyTypeSetting as j on j.ID=h.Currency and j.CompanyCD = h.CompanyCD   ");
            strSql.Append(" where s.CompanyCD=@CompanyCD  ");//此处的Remark为保存BranchID串（逗号隔开）的时候用的

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (model.InvoiceNo != null)
            {
                string InvoiceNoParam = "%" + model.InvoiceNo + "%";
                strSql.Append(" and s.InvoiceNo like @InvoiceNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InvoiceNo", InvoiceNoParam));
            }
            if (model.CustID != null && model.CustID != 0)
            {

                strSql.Append(" and s.CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            }
            if (model.OrderCurrency != null && model.OrderCurrency != "0")
            {

                strSql.Append(" and j.ID=@OrderCurrency ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderCurrency", model.OrderCurrency.ToString()));
            }
            //if (model.OrderNo != null && model.OrderNo != "")
            //{
            //    string OrderNoParam = "%" + model.OrderNo + "%";
            //    strSql.Append(" and s.OrderNo like @OrderNo ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", OrderNoParam));
            //}
            if (model.OrderNo != null && model.OrderNo != "")
            {
                string FromBillNoParam = "%" + model.OrderNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", FromBillNoParam));
                strSql.Append(" and s.FromBillID in( ");
                strSql.Append(" select ID from officedba.SellOrderForeign where OrderNo like @OrderNo and CompanyCD=@CompanyCD )");
            }
            if (model.OutNo != null)
            {
                string OutNoParam = "%" + model.OutNo + "%";
                strSql.Append(" and s.OutNo like @OutNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
            }
            if (model.BillStatus != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                strSql.Append(" and s.BillStatus=@BillStatus ");
            }
            //if (model.Destination != null && model.Destination != "")
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@Destination", model.Destination.ToString()));
            //    strSql.Append(" and s.Destination=@Destination ");
            //}
            if (model.ExchangeDateEnd != null && model.ExchangeDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExchangeDate", model.ExchangeDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExchangeDateEnd", model.ExchangeDateEnd.ToString()));
                strSql.Append(" and s.ExchangeDate>=@ExchangeDate and s.ExchangeDate<@ExchangeDateEnd ");
            }
            else if (model.ExchangeDateEnd != null && model.ExchangeDate == null)//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExchangeDateEnd", model.ExchangeDateEnd.ToString()));
                strSql.Append(" and s.ExchangeDate<@ExchangeDateEnd ");
            }
            else if (model.ExchangeDateEnd == null && model.ExchangeDate != null)//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ExchangeDate", model.ExchangeDate.ToString()));
                strSql.Append(" and s.ExchangeDate>=@ExchangeDate  ");
            }
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region  获取出货明细
        public static DataTable GetSellOutReportList(SellOutStorageForeignModel model, string myOrder)
        {
            string strWhere = " where 1=1 ";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            if (!string.IsNullOrEmpty(model.CompanyCD.Trim()))
            {
                strWhere += " and a.[CompanyCD]='" + model.CompanyCD.Trim() + "' ";
            }

            if (!string.IsNullOrEmpty(model.InvoiceNo))
            {
                strWhere += " and b.[InvoiceNo] like '%" + model.InvoiceNo + "%' ";//发票
            }
            if (!string.IsNullOrEmpty(model.CustID.ToString()))
            {
                strWhere += " and b.[CustID]='" + model.CustID.ToString().Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(model.OrderNo))
            {
                strWhere += " and d.[OrderNo]  like '%" + model.OrderNo.ToString().Trim() + "%' ";//销售订单编号
            }
            if (!string.IsNullOrEmpty(model.OutNo))
            {
                strWhere += " and a.[OutNo]  like '%" + model.OutNo.ToString().Trim() + "%' ";//出库单编号
            }
            //if (!string.IsNullOrEmpty(model.OrderNo))
            //{
            //    strWhere += " and b.[OrderNo]  like '%" + model.OrderNo.ToString().Trim() + "%' ";
            //}
            if (model.ExchangeDate != null)
            {
                strWhere += " and  convert(varchar(10),b.ExchangeDate,120) >=convert(varchar(10),'" + model.ExchangeDate + "',120) ";
            }
            if (model.ExchangeDateEnd != null)
            {
                strWhere += " and convert(varchar(10),b.ExchangeDate,120) <=convert(varchar(10),'" + model.ExchangeDateEnd + "',120) ";
            }

            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                strWhere += " and b.[BillStatus]='" + model.BillStatus.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(model.OrderCurrency) && model.OrderCurrency != "0")
            {
                strWhere += " and g.[ID]='" + model.OrderCurrency + "' ";
            }
            strWhere +="   and ((b.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ";
            strWhere +="     INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ";
            strWhere +="   CROSS JOIN            officedba.UserInfo AS v  ";
            strWhere +="   INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ";
            strWhere +="   INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID";
            strWhere +="                   WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "'))) ";
            strWhere +="  or (b.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
            
            string strSql = string.Format("select row_number() over (order by {1}) as rowid,vv.* ,(convert(decimal(18,2),((isnull(ActualSalesTotal,0)-isnull(CommissionTotal,0)*isnull(OrderCurrencyRate,0)-isnull(TotalFreight,0)))) ) as OperatingTotal " +
               ",(convert(decimal(18,2),(isnull(ActualSalesTotal,0)-isnull(CommissionTotal,0)-isnull(TotalFreight,0))*isnull(CustomersRatio,0))) as Operating  " +
                "  from (SELECT a.[ID],a.[CompanyCD],b.InvoiceNo,d.OrderNo,e.CustName,g.CurrencyName,b.BillStatus ,a.[OutNo] ,c.[ProductName] ,a.[SalesPrice],a.DetailCount " +
                " ,b.ExchangeRate,convert(varchar(10),b.ExchangeDate,120) as ExchangeDate,(convert(decimal(18,2),isnull(a.[SalesPrice],0)*isnull(a.DetailCount,0))) as ActualSalesTotal " +
                " ,a.[Difference],convert(decimal(18,2),isnull(a.Ratio,0)/100) as  Ratio,isnull(b.OrderCurrencyRate,1) as OrderCurrencyRate,(convert(decimal(18,2),isnull(a.[Difference],0)*isnull(a.DetailCount,0))) as CommissionTotal " +
                ",(convert(decimal(18,2),isnull(b.TotalFreight,0)/isnull(b.OrderCurrencyRate,1))) as TotalFreight " +
                " ,isnull(h.Rate,0) as EmployeeRate ,isnull((case when e.CustType='0'  then f.CustomersRatio else f.NewcustomersRatio end),0) as CustomersRatio " +
                " FROM  [officedba].[SellOutStorageDetailForeign] as a  " +
                " left join [officedba].[SellOutStorageForeign] as b on b.OutNo=a.OutNo and a.CompanyCD=b.CompanyCD    " +
                " left join [officedba].[ProductInfo] as c on c.id=a.ProductID and a.CompanyCD=c.CompanyCD   " +
                " left join officedba.SellOrderForeign  as d on d.id=b.FromBillID  and a.CompanyCD=d.CompanyCD   " +
                " left join officedba.CustInfo  as e on e.id=b.CustID  and a.CompanyCD=e.CompanyCD " +
                " left join officedba.EmployeeInfo as f on f.id=d.UserID and a.CompanyCD=f.CompanyCD " +
               // " left join officedba.EmployeeDeduct as h on e.CustCategory=h.CodeID and a.CompanyCD=h.CompanyCD " + 
               " left join officedba.EmployeeDeduct as h on e.CustType=h.CodeID and a.CompanyCD=h.CompanyCD " + 
                " left join officedba.CurrencyTypeSetting as g on g.id=d.Currency and a.CompanyCD=g.CompanyCD  {0} ) as vv ", strWhere, myOrder);
            //        string strSql = string.Format("select row_number() over (order by {1}) as rowid,vv.* ,(convert(decimal(18,2),(isnull(ActualSalesTotal,0)-isnull(CommissionTotal,0)-isnull(TotalFreight,0)))) as OperatingTotal" +
            //",(convert(decimal(18,2),(isnull(ActualSalesTotal,0)-isnull(CommissionTotal,0)-isnull(TotalFreight,0)))) as Operating  from ( " +
            //" SELECT a.[ID],a.[CompanyCD],b.InvoiceNo,d.OrderNo,e.CustName,g.CurrencyName,b.BillStatus ,a.[OutNo] ,c.[ProductName] ,a.[SalesPrice],a.DetailCount " +
            //" ,b.ExchangeRate,convert(varchar(10),b.ExchangeDate,120) as ExchangeDate,(convert(decimal(18,2),isnull(a.[SalesPrice],0)*isnull(a.DetailCount,0))) as ActualSalesTotal " +
            //" ,a.[Difference],convert(decimal(18,2),isnull(a.Ratio,0)) as  Ratio,(convert(decimal(18,2),isnull(a.[Difference],0)*isnull(a.DetailCount,0))) as CommissionTotal " +
            //",(convert(decimal(18,2),isnull(b.TotalFreight,0)*isnull(b.OrderCurrencyRate,0))) as TotalFreight " +
            //",isnull((case when LinkCycle=0  then f.CustomersRatio else f.NewcustomersRatio end),0) as CustomersRatio " +
            //" FROM  [officedba].[SellOutStorageDetailForeign] as a  " +
            //" left join [officedba].[SellOutStorageForeign] as b on b.OutNo=a.OutNo and b.CompanyCD=a.CompanyCD    " +
            //" left join [officedba].[ProductInfo] as c on c.id=a.ProductID and b.CompanyCD=a.CompanyCD   " +
            //" left join officedba.SellOrderForeign  as d on d.id=b.FromBillID  and b.CompanyCD=a.CompanyCD   " +
            //" left join officedba.CustInfo  as e on e.id=b.CustID  and b.CompanyCD=a.CompanyCD " +
            //" left join officedba.EmployeeInfo as f on f.id=d.UserID and b.CompanyCD=a.CompanyCD " +  //
            //" left join officedba.CurrencyTypeSetting as g on g.id=d.Currency and g.CompanyCD=a.CompanyCD  {0} ) as vv ", strWhere, myOrder);
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion
    }
}
