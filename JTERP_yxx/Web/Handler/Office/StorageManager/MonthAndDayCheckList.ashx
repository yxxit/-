<%@ WebHandler Language="C#" Class="MonthAndDayCheckList" %>

using System;
using System.Web;
using XBase.Business.Office.StorageManager;
using System.Data;

public class MonthAndDayCheckList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string action = context.Request.Params["action"].ToString();
        //switch (action)
        //{
        //    //case "monthcheck":
        //    //    monthcheck(context);
        //    //    break;
        //    //case "close":
        //    //    close(context);
        //    //    break;
        //    //case "unclose":
        //    //    unclose(context);
        //    //    break;
        //    //case "checkit":
        //    //    checkit(context);
        //    //    break;
        //    case "monthlist":
        //        monthlist(context);
        //        break;
        //    //case "daylist":
        //    //    daylist(context);
        //    //    break;
        //    //case "getbill":
        //    //    getbill(context);
        //    //    break;
        //}
    }

    //月结查询
    public void monthlist(HttpContext context)
    {
        //string time1 = context.Request.Params["time1"].ToString();
        //string time2 = context.Request.Params["time2"].ToString();
        //decimal CheckCount1 = 0;
        //decimal CheckCount2 = 0;
        //int storageid = 0;
        //string prono = "";
        //string proname = "";
        //decimal OutCount1 = 0;
        //decimal OutCount2 = 0;
        //decimal InCount2 = 0;
        //decimal InCount1 = 0;
        //if (context.Request.Params["CheckCount1"].ToString() != "")
        //{
        //    CheckCount1 = decimal.Parse(context.Request.Params["CheckCount1"].ToString());
        //}
        //if (context.Request.Params["CheckCount2"].ToString() != "")
        //{
        //    CheckCount2 = decimal.Parse(context.Request.Params["CheckCount2"].ToString());
        //}
        //if (context.Request.Params["storageid"].ToString() != "")
        //{
        //    storageid = int.Parse(context.Request.Params["storageid"].ToString());
        //}
      
        //if (context.Request.Params["OutCount1"].ToString() != "")
        //{
        //    OutCount1 = decimal.Parse(context.Request.Params["OutCount1"].ToString());
        //}
        //if (context.Request.Params["OutCount2"].ToString() != "")
        //{
        //    OutCount2 = decimal.Parse(context.Request.Params["OutCount2"].ToString());
        //}
        //if (context.Request.Params["InCount1"].ToString() != "")
        //{
        //    InCount1 = decimal.Parse(context.Request.Params["InCount1"].ToString());
        //}
        //if (context.Request.Params["InCount2"].ToString() != "")
        //{
        //    InCount2 = decimal.Parse(context.Request.Params["InCount2"].ToString());
        //}
        //if (context.Request.Params["prono"].ToString() != "")
        //{
        //    prono = context.Request.Params["prono"].ToString();
            
        //}
        //if (context.Request.Params["proname"].ToString() != "")
        //{
        //    proname = context.Request.Params["proname"].ToString();

        //}
        //int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());
        //int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());
        ////string orderby = context.Request.Params["orderby"].ToString();
        //string orderString = (context.Request.Form["orderby"].ToString());//排序
        //string order = "asc";//排序：升序
        //string orderby = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        //int totalcount = 0;
        //if (orderby == "")
        //{
        //    orderby = "years";
        //}
        //DataTable dt = MonthAndDayCheckListBus.MonthList(pageIndex, pageCount, orderby, ref totalcount, time1, time2, CheckCount1, CheckCount2, storageid, OutCount1, OutCount2, InCount1, InCount2,prono,proname);
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("{");
        //sb.Append("totalCount:");
        //sb.Append(totalcount.ToString());
        //sb.Append(",data:");
        //if (dt.Rows.Count == 0)
        //    sb.Append("[{\"years\":\"\"}]");
        //else
        //    sb.Append(JsonClass.DataTable2Json(dt));
        //sb.Append("}");
        //context.Response.ContentType = "text/plain";
        //context.Response.Write(sb.ToString());
        //context.Response.End();
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}