<%@ WebHandler Language="C#" Class="LocalAlertSet" %>

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
using XBase.Business.Office.LocalAlertManager;
using XBase.Model.Office.LocalAlertManager;
using System.Collections.Generic;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;

public class LocalAlertSet : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        string action = (context.Request.Form["action"].ToString().Trim());//动作
        if (action == "LocalAlertSet") { SaveLocalAlertSetData(context); }//保存预警提醒设置
        else if (action == "GetLocalAlertSetByCompanyCD") { GetLocalAlertSetByCompanyCD(context); }//获取预警设置信息
    }
    /// <summary>
    /// 获取预警设置信息
    /// </summary>
    /// <param name="context"></param>
    private void GetLocalAlertSetByCompanyCD(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        DataTable DT = LocalAlertSetBus.GetLocalAlertSetByCompanyCD(CompanyCD);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        sb.Append(JsonClass.DataTable2Json(DT));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    /// <summary>
    /// 保存预警提醒设置
    /// </summary>
    private void SaveLocalAlertSetData(HttpContext context)
    {
        //获得登录页面POST过来的参数 
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];//获取登陆用户信息    

        string userID = userInfo.UserID;//用户ID
        string CompanyID = userInfo.CompanyCD;//公司代码
        LocalAlertSetModel LocalAlertSet_M = new LocalAlertSetModel();

        string RemindType = context.Request.Params["RemindType"].ToString().Trim(); //提醒类型
        string IsMobileNotice = context.Request.Params["IsMobileNotice"].ToString().Trim(); //是否手机短信提醒
        string RemindPeriod = context.Request.Params["RemindPeriod"].ToString().Trim(); //提醒周期
        string SubPeriod = context.Request.Params["SubPeriod"].ToString().Trim(); //提醒周期内哪天或周几
        string RemindTime = context.Request.Params["RemindTime"].ToString().Trim(); //提醒时间点
        string Mobiles = context.Request.Params["Mobiles"].ToString().Trim(); //提醒手机号码
        if (Mobiles.EndsWith(",")) Mobiles = Mobiles.Remove(Mobiles.Length - 1,1);
       
        LocalAlertSet_M.CompanyCD = CompanyID;
        LocalAlertSet_M.IsMobileNotice = IsMobileNotice;
        LocalAlertSet_M.Mobile = Mobiles;
        LocalAlertSet_M.ModifiedDate = System.DateTime.Now.ToString();
        LocalAlertSet_M.ModifiedUserID = userID;
        LocalAlertSet_M.RemindPeriod = RemindPeriod;
        LocalAlertSet_M.SubPeriod = SubPeriod;
        LocalAlertSet_M.RemindTime = RemindTime;
        LocalAlertSet_M.RemindType = RemindType;        


        JsonClass jc;
        try
        {
            if (LocalAlertSetBus.SaveLocalAlertSet(LocalAlertSet_M))
            {
                jc = new JsonClass("suss","",1);
            }
            else
            {
                jc = new JsonClass("faile","",0);
            }
            context.Response.Write(jc);
        }
        catch (System.Exception ex)
        {
            jc = new JsonClass("faile","",0);
            context.Response.Write(jc);
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}