<%@ WebHandler Language="C#" Class="ProviderFileAdd" %>

using System;
using System.Web;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Common;
using System.Web.SessionState;
using System.Web.UI;
using XBase.Business.Common;

public class ProviderFileAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
   public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        //获得登录页面POST过来的参数//按照JS中从页面上顺序取
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码
        string action = context.Request.Params["action"].ToString().Trim();
        int ID = int.Parse(context.Request.Params["ID"].ToString());
      
        JsonClass jc;            
        string CustNo = context.Request.Params["custNo"].Trim();
        //判断是否存在
        bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProviderInfo"
                            , "CustNo", CustNo);
       
        //存在的场合
        if (!isAlready && action == "Add")
        {
            jc = new JsonClass("该编号已被使用，请输入未使用的编号!", "", 0);
            context.Response.Write(jc);
        }
        else
        {
            string CodeType = context.Request.Params["CodeType"].ToString().Trim();
            ProviderInfoModel model = new ProviderInfoModel();
            model.CompanyCD = CompanyCD;

            //基本信息

            if (action == "Add")
            {
                string aaa = context.Request.Params["custNo"];
                if (context.Request.Params["custNo"].Length == 0)
                {
                    //model.CustNo = ItemCodingRuleBus.GetCodeValue(CodeType);//编号
                    model.CustNo = ItemCodingRuleBus.GetCodeValue(CodeType, "ProviderInfo", "CustNo");//编号

                }
                else
                {
                    model.CustNo = context.Request.Params["custNo"].ToString().Trim();//编号  
                }
            }
            else
            {
                model.CustNo = context.Request.Params["custNo"].ToString().Trim();//编号 
            }
            model.CustName = context.Request.Params["custName"].ToString().Trim();
            model.CustNam = context.Request.Params["custNam"].ToString().Trim();
            model.PYShort = context.Request.Params["pYShort"].ToString().Trim();
            model.CustType = int.Parse(context.Request.Params["CustType"].ToString());
            //-------------------------附件 刘锦旗 20140402-------------------------------------//
            string strAnnFileName = context.Request.Params["AnnFileName"].ToString().Trim();
            string strAnnAddr = context.Request.Params["AnnAddr"].ToString().Trim();
            string strAnnRemark = context.Request.Params["AnnRemark"].ToString().Trim();
            string strUpDateTime = context.Request.Params["UpDateTime"].ToString().Trim();
            if (!string.IsNullOrEmpty(strAnnFileName))
            {
                model.AnnFileName = strAnnFileName;/*附件名称*/
                model.AnnAddr = strAnnAddr;         /*附件地址*/
            }
            if (!string.IsNullOrEmpty(strUpDateTime))
            {
                model.AnnRemark = strAnnRemark;/*附件说明*/
                model.UpDateTime = strUpDateTime;         /*上传时间*/
            }
            //----------------------------------------------------------------//
            

            //辅助信息
            //if (context.Request.Params["creditGrade"].Trim() != null && context.Request.Params["creditGrade"].Trim() != "")
            //{
            //    model.CreditGrade = Convert.ToInt32(context.Request.Params["creditGrade"].ToString().Trim());
            //}
            if (context.Request.Params["manager"] != null && context.Request.Params["manager"] != "")
            {
                model.Manager = Convert.ToInt32(context.Request.Params["manager"].ToString().Trim());
            }
            if (context.Request.Params["areaID"].Trim() != null && context.Request.Params["areaID"].Trim() != "")
            {
                model.AreaID = Convert.ToInt32(context.Request.Params["areaID"].ToString().Trim());
            }
            //if (context.Request.Params["linkCycle"] != null && context.Request.Params["linkCycle"] != "")
            //{
            //    model.LinkCycle = Convert.ToInt32(context.Request.Params["linkCycle"].ToString().Trim());
            //}
            //if (context.Request.Params["hotIs"] != null && context.Request.Params["hotIs"] != "")
            //{
            //    model.HotIs = context.Request.Params["hotIs"].ToString().Trim();
            //}
            //if (context.Request.Params["hotHow"] != null && context.Request.Params["hotHow"] != "")
            //{
            //    model.HotHow = context.Request.Params["hotHow"].ToString().Trim();
            //}
            //if (context.Request.Params["meritGrade"] != null && context.Request.Params["meritGrade"] != "")
            //{
            //    model.MeritGrade = context.Request.Params["meritGrade"].ToString().Trim();
            //}
            //if (context.Request.Params["companyType"] != null && context.Request.Params["companyType"] != "")
            //{
            //    model.CompanyType = context.Request.Params["companyType"].ToString().Trim();
            //}
            //if (context.Request.Params["staffCount"] != null && context.Request.Params["staffCount"] != "")
            //{
            //    model.StaffCount = Convert.ToInt32(context.Request.Params["staffCount"].ToString().Trim());
            //}
            model.SetupAddress = context.Request.Params["setupAddress"].ToString().Trim();
            model.CustNote = context.Request.Params["custNote"].ToString().Trim();
            //try
            //{
            //    model.AllowDefaultDays = Convert.ToInt32(context.Request.Params["AllowDefaultDays"]);
            //}
            //catch
            //{
            //    model.AllowDefaultDays = 0;
            //}

            //业务信息
            //if (context.Request.Params["countryID"].Trim() != null && context.Request.Params["countryID"].Trim() != "")
            //{
            //    model.CountryID = Convert.ToInt32(context.Request.Params["countryID"].ToString().Trim());
            //}
            //model.Province = context.Request.Params["province"].ToString().Trim();
            //model.City = context.Request.Params["city"].ToString().Trim();
            //model.Post = context.Request.Params["post"].ToString().Trim();
            model.ContactName = context.Request.Params["contactName"].ToString().Trim();
            model.Tel = context.Request.Params["tel"].ToString().Trim();
            //model.Fax = context.Request.Params["fax"].ToString().Trim();
            model.Mobile = context.Request.Params["mobile"].ToString().Trim();

            model.OpenBank = context.Request.Params["openBank"].ToString().Trim();
            model.AccountMan = context.Request.Params["accountMan"].ToString().Trim();
            model.AccountNum = context.Request.Params["accountNum"].ToString().Trim();
            //model.BillUnit = context.Request.Params["BillUnit"].ToString().Trim();  //开票单位
            if (context.Request.Params["usedStatus"] != null && context.Request.Params["usedStatus"] != "")
            {
                model.UsedStatus = context.Request.Params["usedStatus"].ToString().Trim();
            }
            if (context.Request.Params["creator"] != null && context.Request.Params["creator"] != "")
            {
                model.Creator = Convert.ToInt32(context.Request.Params["creator"].ToString().Trim());
            }
            if (context.Request.Params["createDate"].Trim() != null && context.Request.Params["createDate"].Trim() != "")
            {
                model.CreateDate = Convert.ToDateTime(context.Request.Params["createDate"].ToString().Trim());
            }
            model.ModifiedUserID = context.Request.Params["modifiedUserID"].ToString().Trim();
            model.SendAddress = context.Request.Params["sendAddress"].ToString().Trim();
            if (ID > 0)
            {
                model.ID = ID;
            }





            if (action == "Add")
            {
                string tempID = "0";
                if (ProviderInfoBus.InsertProviderInfo(model, out tempID))
                {
                    //string ContractDetailID = IDIdentityUtil.GetIDIdentity("officedba.PurchaseContractDetail").ToString();
                    jc = new JsonClass("保存成功", model.CustNo, int.Parse(tempID));
                }
                else
                    jc = new JsonClass("保存失败", "", 0);
                context.Response.Write(jc);
            }
            else if (action == "Update")
            {
                if (ProviderInfoBus.UpdateProviderInfo(model))
                    jc = new JsonClass("保存成功", model.CustNo, model.ID);
                else
                    jc = new JsonClass("保存失败", "", 0);
                context.Response.Write(jc);
            }
            
        }


    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}