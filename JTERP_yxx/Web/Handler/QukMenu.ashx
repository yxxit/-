<%@ WebHandler Language="C#" Class="QukMenu" %>



/**********************************************
 * 类作用：   合同金额统计管理
 * 建立人：   刘朋
 * 建立时间： 2010/09/08
 ***********************************************/
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
using XBase.Model.Common;
using XBase.Business.Common;

public class QukMenu : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    

    public void ProcessRequest(HttpContext context)
    {

              
       
        string CompanyCD = context.Request.Params["companyCD"].ToString().Trim(); 
        string UserID = context.Request.Params["UserID"].ToString().Trim(); 
        string ModuleID = context.Request.Params["ModuleID"].ToString().Trim();

        QukMenuModel model = new QukMenuModel();
        model.CompanyCD = CompanyCD;
        model.MenuAddTime = DateTime.Now;
        model.QukM_ID = ModuleID;
        model.UserID = UserID;
        if(QukMenuBus.QukMenuSelect(model.QukM_ID,model.CompanyCD,model.UserID))
        {
            QukMenuBus.QukMenuUpdate(model);
        }
        else
           QukMenuBus.QukMenuAdd(model);
        context.Response.ContentType = "text/plain";
        //context.Response.Write();
        context.Response.End();

       
        //    context.Response.ContentType = "text/plain";
        //    context.Response.Write(sb.ToString());
        //    context.Response.End();
        }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
