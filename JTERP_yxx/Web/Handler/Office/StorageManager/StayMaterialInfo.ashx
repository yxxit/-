<%@ WebHandler Language="C#" Class="StayMaterialInfo" %>

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
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class StayMaterialInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    string companyCD = string.Empty;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string action = context.Request.QueryString["action"].ToString();
            if (action == "Get")
            {
                //设置行为参数
                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProductNo";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                //获取数据
                StayMaterialModel model = new StayMaterialModel();
                model.CompanyCD = companyCD;
                string BusiType = context.Request.Params["BusiType"].ToString().Trim();
                string ProductNo = context.Request.Params["ProductNo"].ToString().Trim();
                string ProductName = context.Request.Params["ProductName"].ToString().Trim();
                string MoreThanType = context.Request.Params["MoreThanType"].ToString().Trim();
                string OverTime = context.Request.Params["OverTime"].ToString().Trim();
                string OverTimeEnd = context.Request.Params["OverTimeEnd"].ToString().Trim();
                string TurnOver = context.Request.Params["TurnOver"].ToString().Trim();
                string TurnOverEnd = context.Request.Params["TurnOverEnd"].ToString().Trim();
                string UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
                model.BusiType = BusiType;
                model.ProductNo = ProductNo;
                model.ProductName = ProductName;
                model.MoreThanType = MoreThanType;
                model.OverTime = OverTime;
                model.OverTimeEnd = OverTimeEnd;
                model.TurnOver = TurnOver;
                model.TurnOverEnd = TurnOverEnd;
                model.UsedStatus = UsedStatus;
                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StayMaterialBus.GetStayMaterialInfo(model,pageIndex, pageCount, ord, ref TotalCount);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            if (action == "new")
            {
                //设置行为参数
                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ProdNo";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                //获取数据
                StayMaterialModel model = new StayMaterialModel();
                model.CompanyCD = companyCD;
                
                string ProductNo = context.Request.Params["ProductNo"].ToString().Trim();
                string ProductName = context.Request.Params["ProductName"].ToString().Trim();
                string daycount = context.Request.Params["daycount"].ToString().Trim();
                string OverTime = context.Request.Params["OverTime"].ToString().Trim();
                string OverTimeEnd = context.Request.Params["OverTimeEnd"].ToString().Trim();
                string UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
               
                
                model.ProductNo = ProductNo;
                model.ProductName = ProductName;
               
                model.OverTime = OverTime;
                model.OverTimeEnd = OverTimeEnd;
                model.Daycount = daycount;
                model.UsedStatus = UsedStatus;
                string ord = orderBy + " " + order;
                int TotalCount = 0;
                DataTable dt = StayMaterialBus.GetStayMaterialInfo1(model, pageIndex, pageCount, ord, ref TotalCount);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
                sb.Append(",data:");
                if (dt.Rows.Count == 0)
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
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}