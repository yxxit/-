<%@ WebHandler Language="C#" Class="InitSystem" %>
/**********************************************
 * 作用：   系统初始化
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/22
 ***********************************************/
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using System.Data;
public class InitSystem : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        JsonClass jc;
        
        string flag= context.Request.QueryString["flag"].ToString();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        bool iss = InitSystemBus.DeleteInfo(flag, CompanyCD);
        if (iss)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败", "", 2);
        }
        context.Response.Write(jc);
        
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}