<%@ WebHandler Language="C#" Class="CustCompanyCheck" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.AdminManager;
using XBase.Business.Common;

public class CustCompanyCheck : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string CName = context.Request.QueryString["strname"];
        string CommonTable = context.Request.QueryString["TableName"];
        string ColName = context.Request.QueryString["colname"];
        bool result = PrimekeyVerifyBus.CheckYYcustExist(CommonTable, ColName, CName);
        JsonClass jc;
        if (result)
        {
            jc = new JsonClass("faile", "公司名称不存在，请重新输入！", 3);
        }
        else
        {
            jc = new JsonClass("success", "", 1);
        }

        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}