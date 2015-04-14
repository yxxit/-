<%@ WebHandler Language="C#" Class="PaperPlateSel" %>

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
using XBase.Business.Common;

public class PaperPlateSel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Form["action"].ToString();
            if (action == "getfloor")
            {

                //设置行为参数
                string orderString = (context.Request.Form["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }

                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;

                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
                string Name = context.Request.Params["Name"].ToString().Trim();
                string Remark = context.Request.Params["Remark"].ToString().Trim();
                string TypeID = context.Request.Params["TypeID"].ToString().Trim();

                string ord = orderBy + " " + order;

                DataTable dt = ProductInfoBus.GetPnumber(TypeID, Name, Remark, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();

            }
            else if (action == "codepublic")
            {
                //设置行为参数
                string orderString = (context.Request.Form["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }

                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;

               
                string TypeID = context.Request.Params["TypeID"].ToString().Trim();

                string ord = orderBy + " " + order;

                DataTable dt = ProductInfoBus.GetCodePublic(TypeID, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
                sb.Append(",data:");
                if (dt == null)
                    sb.Append("[{\"ID\":\"\"}]");
                else
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