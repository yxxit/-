<%@ WebHandler Language="C#" Class="FlowReport" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using XBase.Common;

using XBase.Business.Personal.WorkFlow;

public class FlowReport : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        DataTable aimreportlist = new DataTable();
        int totalcount = 0;
        if (context.Request.QueryString["isPage"] == "true")
        {
            int pageindex = 1;
            int pagecount = 10;
            try
            {
                pageindex = int.Parse(context.Request.QueryString["pageindex"].ToString());
                pagecount = int.Parse(context.Request.QueryString["pagecount"].ToString());
            }
            catch { }
            string orderBy = context.Request.QueryString["orderby"].ToString().Trim();//排序;
            if (String.IsNullOrEmpty(orderBy))
            {
                orderBy = " ID ";
            }
            aimreportlist = FlowReportBus.StatFlow(SetSearchHashtable(context.Request), pageindex, pagecount, orderBy, ref totalcount);
            SessionUtil.Session.Add("CurrentListTable", FlowReportBus.StatFlow(SetSearchHashtable(context.Request), 1, 99999, orderBy, ref totalcount));
        }
        else
        {
            aimreportlist = FlowReportBus.StatFlow(SetSearchHashtable(context.Request));
        }
        string json = "";
        if (aimreportlist.Rows.Count > 0)
        {
            json = "{sta:1,data:" + JsonClass.DataTable2Json(aimreportlist) + ",info:" + totalcount + " }";
        }
        else
        {
            json = "{result:true,data:'' ,info:0}";
        }
        context.Response.Write(json);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private Hashtable SetSearchHashtable(HttpRequest request)
    {
        Hashtable hs = new Hashtable();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        hs.Add("GroupBy", request.QueryString["GroupBy"]);
        hs.Add("GroupByValue", request.QueryString["GroupByValue"]);
        hs.Add("StartDate", request.QueryString["StartDate"]);
        hs.Add("EndDate", request.QueryString["EndDate"]);
        if (request.QueryString["AimStatus"] + "" != "")
        {
            hs.Add("Status", request.QueryString["AimStatus"]);
        }
        hs.Add("CompanyCD", userInfo.CompanyCD);
        hs.Add("FromWitch", request.QueryString["FromWitch"]);

        if (request.QueryString["DeptID"] + "" != "")
        {
            hs.Add("DeptID", request.QueryString["DeptID"]);
        }
        if (request.QueryString["EmployeeID"] + "" != "")
        {
            hs.Add("EmployeeID", request.QueryString["EmployeeID"]);
        }


        return hs;
    }
}