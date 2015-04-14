<%@ WebHandler Language="C#" Class="ProviderInfo" %>

using System;
using System.Web;
using System.Xml.Linq;
using XBase.Common;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;

public class ProviderInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "id";

            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            int totalCount = 0;

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
            string CustNo = context.Request.Params["CustNo"].ToString().Trim();
            string CustName = context.Request.Params["CustName"].ToString().Trim();
            string CustNam = context.Request.Params["CustNam"].ToString().Trim();

            DataTable dt = ProviderInfoBus.GetProviderInfoByCustNo(CompanyCD, CustName, CustNo, CustNam, pageIndex, pageCount, order, ref totalCount);

            JsonClass jc;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");

            if (dt.Rows.Count == 0)
                sb.Append("[{\"id\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
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