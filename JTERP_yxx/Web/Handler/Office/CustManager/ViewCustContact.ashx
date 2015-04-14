<%@ WebHandler Language="C#" Class="ViewCustContact" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using System.Xml.Linq;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class ViewCustContact : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {

        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "desc";
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreatedDate";//要排序的字段，如果为空，默认为"linkmanname"
            if (orderString.EndsWith("_d"))
            {
                order = "asc";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string ord = orderBy + " " + order;
            int totalCount = 0;

            //获取数据           
            string CustNo = context.Request.Form["CustNo"].ToString().Trim();
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 

            DataTable dt = TalkBus.GetTalkInfoByCustNo(CompanyCD, CustNo, pageIndex, pageCount, ord, ref totalCount);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            if (dt == null)
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