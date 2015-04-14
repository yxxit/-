<%@ WebHandler Language="C#" Class="SettleVouchInfo" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.SellManager;
using System.Web.SessionState;
using System.Text;
using XBase.Business.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

public class SettleVouchInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Action = context.Request.Params["action"];

            if (Action == "getSettleMoney")
            {

                DataTable dt = null;
                string ID = context.Request.Params["ID"].ToString();  //来源单据id
                string sourceType = context.Request.Params["sourceType"].ToString();  //来源类型
                string searchSql1 = "";
                if (sourceType == "1")  //直销
                {
                    searchSql1 = @"select a.id, b.CustJsFee,b.TotalFee as SellMoney, '' as ProJsFee,
                                '' as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_xsfh b on b.id=a.SourceBillID
                                where b.BusiType='1' and b.id='" + ID + "' ";
                }
                else if (sourceType == "2")//采购到货
                {
                    searchSql1 = @"select a.id, '' as CustJsFee,'' as SellMoney, b.ProJsFee,
                                b.TotalFee as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_cgdh b on b.id=a.SourceBillID
                                where  b.id='" + ID + "' ";
                }
                else if (sourceType == "3")//采购直销
                {
                    searchSql1 = @"select a.id, b.CustJsFee,b.TotalFee as SellMoney, b.ProJsFee,
                                b.SupplyAmount as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_xsfh b on b.id=a.SourceBillID
                                where b.BusiType='2' and b.id='" + ID + "' ";
                }
                dt = SqlHelper.ExecuteSql(searchSql1);

                int TotalCount = 0;
                StringBuilder sb = new StringBuilder();

                if (dt == null || dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
                context.Response.Write(sb.ToString());
                context.Response.End();

            }
            else if (Action == "SearchSettleVouchList")
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件
                string SettleCode = context.Request.Form["SettleCode"].ToString().Trim();  //单据编号
                string BusTtype = context.Request.Form["BusTtype"].ToString().Trim();   //来源类型
                string SourceBillNo = context.Request.Form["SourceBillNo"].ToString().Trim();   //来源单号
                string CustName = context.Request.Form["CustName"].ToString().Trim();   //客户名称
                string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供应商名称
                string cPersonName = context.Request.Form["cPersonName"].ToString().Trim();   //经办人


                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = "";
                strQuery = @" select a.id,a.SettleCode,
                                (case a.cBusTtype when '1' then '直销' when '2' then '采购到货' when '3' then '采购直销' end) as BusTtype,
                                a.cBusTtype,a.P_SettleTotalPrice,a.S_SettelTotalPrice,
                                a.cPersonCode,c.EmployeeName,
                                (case a.Status when '1' then '未确认' when '2' then '已确认' end) as BillStatus, ";
                if(BusTtype=="2")
                {
                     strQuery+=@" b.ArriveNo as SourceBillNo,b.TotalFee as P_Money,'' as S_Money, 
                                    d.custname as providername ,'' as custname  ";
                }
                else 
                {
                      strQuery+=@"  b.SendNo as SourceBillNo,b.TotalFee as S_Money,b.SupplyAmount as P_Money ,
                                      d.custname as providername ,e.custname ";
                }


                strQuery += @" from dbo.jt_settlevouch a
                            left join officedba.EmployeeInfo c on c.id=a.cPersonCode ";

                if(BusTtype=="2")
                {
                      strQuery += @"  left join dbo.jt_cgdh b on a.SourceBillID=b.id 
                                      left join officedba.providerinfo d on a.ProviderID=d.id ";
                }
                else
                {
                      strQuery += @" left join dbo.jt_xsfh b on a.SourceBillID=b.id 
                                     left join officedba.providerinfo d on a.ProviderID=d.id 
                                    left join officedba.CustInfo e on a.CustID=e.id  ";
                }
                strQuery += @" where a.CompanyCD = '" + CompanyCD + "' and a.cBusTtype='" + BusTtype + "' ";


                if (SettleCode != "")
                    strQuery += " and a.SettleCode like '%" + SettleCode + "%'";
                if (SourceBillNo != "")
                {
                    if (BusTtype == "2")
                    {
                        strQuery += " and b.ArriveNo like '%" + SourceBillNo + "%'";
                    }
                    else
                    {
                        strQuery += " and b.SendNo like '%" + SourceBillNo + "%'";
                    }
                }
                if (CustName != "")
                    strQuery += " and e.custname like '%" + CustName + "%'";
                if (ProviderName != "")
                    strQuery += " and d.custname like '%" + ProviderName + "%'";
                if (cPersonName != "")
                    strQuery += " and c.EmployeeName like '%" + cPersonName + "%'";


                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                string orderBy = " id  desc  ";
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
                StringBuilder sb = new StringBuilder();

                if (dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
                context.Response.Write(sb.ToString());
                context.Response.End();


            }
            else if (Action == "SearchSettleVouchOne")  //从列表中查看结算单详细信息
            {
                string headid = context.Request.Form["headid"].ToString();
                string BusTtype = context.Request.Form["BusTtype"].ToString().Trim();


                DataTable dt = new DataTable();
                string strQuery="";
                strQuery = @"select  a.id,a.SettleCode,a.cBusTtype,a.SourceBillID,
                                    a.cPersonCode as PersonId,b.EmployeeName as PersonName,
                                    a.P_SettleTotalPrice,a.S_SettelTotalPrice,a.cMemo,a.Status as billStatus,
                                    a.Creator,g.EmployeeName as CreatorName,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,
                                    a.Confirmor,h.EmployeeName as ConfirmorName,CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,
                                    a.ModifiedUserID,CONVERT(varchar(100),a.ModifiedDate, 23) as ModifiedDate, ";
                if(BusTtype=="2")
                {
                      strQuery += @"  d.ArriveNo as SourceBillNo,
                                    a.ProviderID,isnull(e.CustName,'') as ProviderName,
                                    0 as CustID, '' as CustName,d.TotalFee as P_Money,
                                    0 as S_Money,isnull(d.ProJsFee,0) as ProJsFee,0 as CustJsFee ";
                }
                else
                {
                      strQuery += @" d.SendNo as SourceBillNo,
                                    a.ProviderID,isnull(e.CustName,'') as ProviderName,
                                    a.CustID, isnull(f.CustName,'') as CustName,d.TotalFee as S_Money,
                                    d.SupplyAmount as P_Money,isnull(d.ProJsFee,0) as ProJsFee,isnull(d.CustJsFee,0) as CustJsFee ";
                }

                strQuery += @" from  dbo.jt_settlevouch a
                                    left join officedba.EmployeeInfo b on b.id=a.cPersonCode
                                    left join officedba.EmployeeInfo g on g.id=a.Creator
                                    left join officedba.EmployeeInfo h on h.id=a.Confirmor ";
                if(BusTtype=="2")
                {
                      strQuery += @" left join dbo.jt_cgdh d on d.id=a.SourceBillID
                                    left join officedba.providerinfo e on a.ProviderID=e.id ";
                }
                else
                { 
                      strQuery += @" left join dbo.jt_xsfh d on d.id=a.SourceBillID
                                    left join officedba.providerinfo e on a.ProviderID=e.id 
                                    left join  officedba.CustInfo f on f.id=a.CustID ";    
                }
                strQuery += @" where  a.id='" + headid + "'";

                try
                {
                    dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(strQuery);


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("{");
                    sb.Append("data:");
                    sb.Append(JsonClass.DataTable2Json(dt));
                    sb.Append("}");

                    context.Response.Write(sb.ToString());
                    context.Response.End();

                }
                catch (Exception ex)
                {

                }
            }
        }
           
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}