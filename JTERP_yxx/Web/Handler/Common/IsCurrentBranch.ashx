<%@ WebHandler Language="C#" Class="IsCurrentBranch" %>

using System;
using System.Web;
using XBase.Common;
using System.Web.SessionState;

public class IsCurrentBranch : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        string _companycd = context.Request.Form["CompanyCD"].Trim();//公司编号
        string _branchid = context.Request.Form["BranchID"].Trim();//分店ID
        JsonClass JC;
        try
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CurrCompanyID = userInfo.CompanyCD;//公司代码
            string BranchID = userInfo.BranchID.ToString();//
            if(_companycd==CurrCompanyID && _branchid==BranchID)
                JC = new JsonClass("", "", 1);
            else
                JC = new JsonClass("", "", 0);            
        }
        catch (System.Exception ex)
        {
            JC = new JsonClass("", "", 0);
        }
        context.Response.Write(JC);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}