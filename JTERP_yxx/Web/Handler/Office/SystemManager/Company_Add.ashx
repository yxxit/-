<%@ WebHandler Language="C#" Class="Company_Add" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using XBase.Business.Common;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;

public class Company_Add : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    int InID = 0;
    string strMsg = string.Empty;
    bool isSucc = false;

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string action = context.Request.QueryString["action"].ToString();
             string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            switch (action)
            {
                case "Search":
                    LoadData(context);//加载数据
                    break;
                case "Add":
                     InsertCompany(context);//添加记录
                    break;
              
                case "Update":
                    UpdateCompany(context);//修改记录
                    break;
                case "CheckCompanyCD":
                    CheckCompanyCD(context);
                    break;
            }

        }
    }

    //protected override void ActionHandler(string action)
    //{
    //    ProcessRequestHandler(_context, action);
    //}

    //public void ProcessRequestHandler(HttpContext context, string action)
    //{
        
    //    string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
       
    //    try
    //    {
    //        if (action == "Add")
    //        {

    //            InsertCompany(context);
    //            return;
                
    //        }
    //        if (action == "Update")
    //        {

    //            UpdateCompany(context);
    //            return;

    //        }
    //        if (action == "Search")
    //        {

    //            LoadData(context);
    //            return;
    //        }
    //        else
    //        {
    //            CheckCompanyCD(context);
    //            return;
               
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    //根据ID获取其它入库单信息
    private void CheckCompanyCD(HttpContext context)
    {
        string Code = context.Request.QueryString["CompanyCD"];//获取新公司代码
        
        bool result = CompanyBus.CheckCompanyCD( Code);
        //唯一性字段是否存在存在
        JsonClass jc;
        jc = new JsonClass("success", "", 1);
        if (!result)
        {
            jc = new JsonClass("faile", "公司代码已经存在，请重新输入！", 3);
        }


        context.Response.Write(jc);
    }
    private void InsertCompany(HttpContext context)
    {
        CompanyModel Model = new CompanyModel();
        string Code = context.Request.QueryString["CompanyCD"];//获取新公司代码
        Model.CompanyCD = context.Request.QueryString["CompanyCD"].ToString().Trim();
        Model.NameCn = context.Request.QueryString["NameCn"].ToString().Trim();
        Model.NameEn = context.Request.QueryString["NameEn"].ToString().Trim();
        Model.NameShort = context.Request.QueryString["NameShort"].ToString().Trim();
        Model.PyShort = context.Request.QueryString["PyShort"].ToString().Trim();
        Model.DocSavePath = context.Request.QueryString["DocSavePath"].ToString().Trim();
        Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString().Trim();
        JsonClass jc;

        if (CompanyBus.InsertCompany(Model))
        {
            
            jc = new JsonClass("保存成功！超级用户名为"+CompanyBus.GetUserID(Model.CompanyCD)+"，密码为"+CompanyBus.GetUserID(Model.CompanyCD)+"！", "", 1);
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
        context.Response.Write(jc);

    }
    private void UpdateCompany(HttpContext context)
    {
        CompanyModel Model = new CompanyModel();
        Model.CompanyCD = context.Request.QueryString["CompanyCD"].ToString().Trim();
        Model.UsedStatus = context.Request.QueryString["UsedStatus"].ToString().Trim();
        JsonClass jc;

        if (CompanyBus.UpdateCompany(Model))
        {

            jc = new JsonClass("保存成功", "", 1);
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
        context.Response.Write(jc);

    }
    private void LoadData(HttpContext context)
    {
        int totalCount = 0;
        DataTable dt = CompanyBus.GetCompany();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["DocSavePath"] = dt.Rows[i]["DocSavePath"].ToString().Replace("\\","\\\\") ;
        }
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ItemTypeID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}