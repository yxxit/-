<%@ WebHandler Language="C#" Class="InitSystemData" %>
/**********************************************
 * 作用：   系统初始化添加基本数据
 * 建立人：   宋凯歌
 * 建立时间： 2010/11/12
 ***********************************************/
using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using System.Data;
using XBase.Model.Office.SystemManager;
public class InitSystemData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        JsonClass jc;
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码  
        InitSystemDataModel model = new InitSystemDataModel();
        model.CompanyCD = CompanyCD;
        model.UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//更新用户
        model.DeptName = context.Request.QueryString["DeptName"].ToString();//组织结构名拼音缩写
        model.HidDeptName = context.Request.QueryString["HidDeptName"].ToString();//组织结构名称
        model.EmployeeName = context.Request.QueryString["EmployeeName"].ToString();//人员档案名称
        model.HidEmployeeName = context.Request.QueryString["HidEmployeeName"].ToString();//人员档案名称拼音缩写
        model.StorageName = context.Request.QueryString["StorageName"].ToString();//仓库名称
        model.CustName = context.Request.QueryString["CustName"].ToString();//客户名称
        model.HidCustName = context.Request.QueryString["HidCustName"].ToString();//客户名称拼音缩写 
        model.LinkManName = context.Request.QueryString["LinkManName"].ToString();//联系人  
        model.WorkTel = context.Request.QueryString["WorkTel"].ToString();//联系人电话 
        model.ProName = context.Request.QueryString["ProName"].ToString();//供应商名称   
        model.TypeName = context.Request.QueryString["TypeName"].ToString();//供应商分类
        model.User = context.Request.QueryString["User"].ToString().Trim();
        string password = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["Password"].ToString().Trim());
        model.PassWord = password;
        bool isTure = InitSystemDataBus.AddInitSystemUserData(model);
        if (!isTure)
        {
            jc = new JsonClass("已存在此用户", "", 3);
            context.Response.Write(jc);
            return;
        }
        bool iss = InitSystemDataBus.AddInitSystemData(model);
        if (iss)
        {
            jc = new JsonClass("添加成功", "", 1);
        }
        else
        {
            jc = new JsonClass("添加失败", "", 2);
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