<%@ WebHandler Language="C#" Class="UploadfileDel" %>

using System;
using System.Web;
using System.IO;
using XBase.Common;
using System.Data;
using XBase.Business.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;

public class UploadfileDel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string filename = context.Request.Form["filename"].ToString();
        string realname = context.Request.Form["realname"].ToString();
        UserInfoUtil userInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
        JsonClass jc;
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        bool ishave = CustInfoBus.GetAttachment(userInfo.CompanyCD,filename,realname);
        if (ishave)
        {
            if (CustInfoBus.DelAttachment(userInfo.CompanyCD, filename, realname))
            {
                jc = new JsonClass("success", "", 1);
            }
            else
            {
                jc = new JsonClass("faile", "", 0);
            }
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