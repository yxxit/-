<%@ WebHandler Language="C#" Class="WareInfo" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using XBase.Model.Office.SellManager;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Data.DBHelper;
using System.Text;


public class WareInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Params["action"].ToString();//操作

            if (action == "getwaredata")
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数
                string strid = "";
                string strsql = "select top 1  '0' as ID,'' as StorageNo,'--请选择--' as StorageName from  Officedba.StorageInfo union all " +
                " select ID,StorageNo,StorageName from  Officedba.StorageInfo where companycd='"+companyCD+"'   ";
                dt = SqlHelper.ExecuteSql(strsql);
                
                

                int totalCount = dt.Rows.Count;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "getOutCount")  //获取销售发货单的未出库数量
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string ID = context.Request.Params["ID"].ToString();  //销售发货单明细id
                string searchSql = @"select id, (isnull(ProductCount,0)-isnull(OutCount,0)) as ProductCount  from dbo.jt_xsfh_mx where SendNo=" + ID + " and CompanyCD='" + companyCD + "'";

                dt = SqlHelper.ExecuteSql(searchSql);
                
                int TotalCount=0;
                StringBuilder sb = new StringBuilder();
                
                if (dt!=null && dt.Rows.Count == 0 )
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
            else if (action == "getInCount")   //获取采购到货单的已入库数量
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string ID = context.Request.Params["ID"].ToString();  //销售发货单明细id
                string searchSql = @"select id, (isnull(ProductCount,0)-isnull(InCount,0)) as ProductCount  from dbo.jt_cgdh_mx where ArriveNo=" + ID + " ";

                dt = SqlHelper.ExecuteSql(searchSql);

                int TotalCount = 0;
                StringBuilder sb = new StringBuilder();

                if (dt != null && dt.Rows.Count == 0)
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



        }
        

    }
      
    public bool IsReusable {
        get {
            return false;
        }
    }

}