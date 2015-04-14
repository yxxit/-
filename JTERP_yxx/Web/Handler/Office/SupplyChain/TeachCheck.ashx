<%@ WebHandler Language="C#" Class="TeachCheck" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;


public class TeachCheck : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            try
            {
                //设置行为参数
                DataTable dt = new DataTable();
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数                
                string strid = context.Request.QueryString["str"].ToString();         
               
                dt = TeachInfoBus.GetTeachInfoTableByCheckcondition(strid);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
              
                sb.Append("data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}