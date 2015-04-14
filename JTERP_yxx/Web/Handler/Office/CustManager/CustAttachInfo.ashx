<%@ WebHandler Language="C#" Class="CustAttachInfo" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using System.Data;
using XBase.Business.Office.CustManager;
using XBase.Common;

public class CustAttachInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string orderString = (context.Request.Form["orderby"].ToString());//排序
            string order = "desc";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "upDatetime";//要排序的字段，如果为空，默认为"ID"
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
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string CustName = context.Request.Form["CustNam"].ToString().Trim();
            string Attachment = context.Request.Form["Attachment"].ToString().Trim();
            string Remark = context.Request.Form["Remark"].ToString().Trim();

            DataTable dt = CustInfoBus.GetCustAttachInfoBycondition(CompanyCD,CustName, Attachment, Remark, pageIndex, pageCount, ord, ref totalCount);


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