<%@ WebHandler Language="C#" Class="ProviderInfoEdit" %>

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
using XBase.Data.Office.PurchaseManager;
using System.Web.SessionState;

public class ProviderInfoEdit : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //获取该界面有没有被引用标识
            int ID = Convert.ToInt32(context.Request.Params["ID"].ToString().Trim());

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            DataTable dtp = ProviderInfoBus.SelectProviderInfo(ID);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();  
            DataTable dtAttach = ProviderInfoBus.GetAdvisoryPriceAttachInfo(ID);
            
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(dtp));
            if (dtAttach.Rows.Count > 0)
            {
                sb.Append(",");
                sb.Append("dataAttach:");
                sb.Append(JsonClass.DataTable2Json(dtAttach));
            }
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}