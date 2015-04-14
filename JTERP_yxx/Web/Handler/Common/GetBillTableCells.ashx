<%@ WebHandler Language="C#" Class="GetBillTableCells" %>

using System;
using System.Web;
using System.Data;
using XBase.Business.Common;

public class GetBillTableCells : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Params["action"].ToString();
            if (action == "all")
            {
                string TabName = context.Request.Params["TableName"].ToString();
                DataTable dt = GetBillTableCellsBus.GetAllCustom(TabName);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(dt.Rows.Count.ToString());
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
            else if (action == "getfield")
            {
                string moduleid = context.Request.Params["moduleid"].ToString();
                //string type = context.Request.Params["type"].ToString();
                DataTable dt = GetBillTableCellsBus.GetAllField(moduleid);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(dt.Rows.Count.ToString());
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
            else if (action == "savepagesetup")
            {
                JsonClass jc;
                string jinbenxinxi = context.Request.Params["jinbenxinxi"].ToString();
                string dindandetail = context.Request.Params["dindandetail"].ToString();
                string hejixinxi = context.Request.Params["hejixinxi"].ToString();
                string beizhuxinxi = context.Request.Params["beizhuxinxi"].ToString();
                string danjuzhuangtai = context.Request.Params["danjuzhuangtai"].ToString();
                string feiyongxinxi = context.Request.Params["feiyongxinxi"].ToString();
                string isdis = context.Request.Params["isdis"].ToString();
                string moduleid = context.Request.Params["moduleid"].ToString();
                string isenable = context.Request.Params["isenable"].ToString();
                if (GetBillTableCellsBus.InsertPageSetUp(jinbenxinxi,dindandetail,feiyongxinxi,hejixinxi,beizhuxinxi,danjuzhuangtai,isdis,moduleid,isenable))
                {
                    jc = new JsonClass("保存成功", "", 1);
                    context.Response.Write(jc);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                    context.Response.Write(jc);
                }
            }
            else if (action == "clearpagesetup")
            {
                JsonClass jc;
                string moduleid = context.Request.Params["moduleid"].ToString();
                if (GetBillTableCellsBus.ClearPageSetUp(moduleid))
                {
                    jc = new JsonClass("清除设置成功", "", 1);
                    context.Response.Write(jc);
                }
                else
                {
                    jc = new JsonClass("清除设置成功", "", 0);
                    context.Response.Write(jc);
                }
            }
                
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}