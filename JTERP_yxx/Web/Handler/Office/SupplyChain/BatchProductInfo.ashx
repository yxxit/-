<%@ WebHandler Language="C#" Class="BatchProductInfo" %>
/**********************************************
 * 类作用：   匹配物品信息Handler层处理
 * 建立人：   王玉贞
 * 建立时间： 2010/06/12
 ***********************************************/
using System;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using XBase.Business.Office.SupplyChain;
using System.Data;
using XBase.Common;

public class BatchProductInfo : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
         string action = "";
         try
         {
             action = context.Request["action"].ToString();
         }
         catch (Exception)
         {
         }

         if (context.Request.RequestType == "POST")
        {
            if (action == "getBatch")
            {
                string ProductID = context.Request["ProductID"].ToString();
                string ProdName = context.Request["ProdName"].ToString();
                string batch = context.Request["PYShort"].ToString();
                int pageCount = int.Parse(context.Request["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
                int totalCount = 0;
                string ord = context.Request["orderByP"].ToString();
                if (ord == "")
                {
                    ord = "ID";
                }
                string ProdNo = context.Request["ProdNo"].ToString();
                DataTable dt = ProductInfoBus.GetBatch(ProdNo,ProductID,ProdName,batch, pageIndex, pageCount, ord, ref totalCount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(totalCount.ToString());
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
            else
            {
                string ProdNo = context.Request.QueryString["ProdNo"].ToString();


                DataTable dbBatch = ProductInfoBus.GetMatchProductInfo(companyCD, ProdNo);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("dataBatch:");
                sb.Append(JsonClass.DataTable2Json(dbBatch));
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