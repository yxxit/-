<%@ WebHandler Language="C#" Class="MedicineList" %>

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
 

public class MedicineList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Params["action"].ToString();//操作

            if (action == "getcheckdesc")
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数
                string strid = "";
                try
                {
                    strid = context.Request.Params["str"].ToString();
                }
                catch (Exception)
                {
                    strid = context.Request.QueryString["str"].ToString();
                }
                // dt = MedicineRetailBus.GetProductInfoTableByCheckcondition(strid, companyCD);

                string strsql =
                "select ID,TypeName,Description from  officedba.CodePublicType where typeflag=10 and typecode=1 and  ID='" + strid + "' and CompanyCD = '"+companyCD+"' ";
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
            else if (action == "getcheckdata")
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                string strsql = " select TOP 1 0 as  ID,'--请选择--' as  TypeName,'' as  Description from   officedba.CodePublicType union all "
                + "select ID,TypeName,Description from  officedba.CodePublicType where typeflag=10 and typecode=1 and CompanyCD='"+companyCD+"'";
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


        }

    }
      
    public bool IsReusable {
        get {
            return false;
        }
    }

}