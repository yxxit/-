<%@ WebHandler Language="C#" Class="CustLinkMan" %>

using System;
using System.Web;
using XBase.Business.Office.CustManager;
using System.Data;
using XBase.Common;


public class CustLinkMan : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string CustNo = context.Request.Params["CustNo"].ToString().Trim();
        //string LinkManName = context.Request.Params["LinkManName"].ToString().Trim();
        //string Appellation = context.Request.Params["Appellation"].ToString().Trim();
        //string Department = context.Request.Params["Department"].ToString().Trim();

        DataTable dt = LinkManBus.GetLinkManByCustNo1(CompanyCD,CustNo);

        JsonClass jc;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        //sb.Append(dt.Rows.Count);
        if (dt.Rows.Count == 0)
            sb.Append(dt.Rows.Count);
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}