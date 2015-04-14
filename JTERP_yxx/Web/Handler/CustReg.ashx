<%@ WebHandler Language="C#" Class="CustReg" %>

using System;
using System.Web;
using XBase.Common;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Business.Common;
using System.Collections;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Model.Office.HumanManager;
using XBase.Business.Office.HumanManager;

public class CustReg : IHttpHandler, System.Web.SessionState.IRequiresSessionState
 {
    
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        string CompanyCD = "HCYY1";
        string CustName = context.Request.Params["CustName"].ToString().Trim();//YY客户名称
        string CorrYYCode = context.Request.Params["CustNo"].ToString().Trim();//YY客户编号
        string CustAddress = context.Request.Params["CustAddr"].ToString().Trim();//YY客户地址
        string[] arr = new string[] { "CustName", "CompanyCD" };
        string[] CustNames = new string[] { CustName,CompanyCD };
        bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNames);
        string CustNo = "";
        if (NameHas)
        {
            CustNo = CustInfoBus.GetCustNo(CompanyCD,CustName);
        }
        else
        {
            CustNo = "KH" ;//生成客户编号
        }
         
        string LinkManName = context.Request.Params["LinkName"].ToString().Trim();//联系人姓名
        string Sex = context.Request.Params["Sex"].ToString().Trim();//联系人性别
        string HomeAddress = context.Request.Params["LinkAddr"].ToString().Trim();//联系人地址
        string WorkTel = context.Request.Params["WorkTel"].ToString().Trim();//工作电话
        string Mobile = context.Request.Params["Mobile"].ToString().Trim();//手机
        string MailAddress = context.Request.Params["Email"].ToString().Trim();//Email
        string EmployeeNo = "RY";//生成员工编号
        string UserID = context.Request.Params["UserName"].ToString().Trim();//用户名
        string password = StringUtil.EncryptPasswordWitdhMD5(context.Request.QueryString["Password"].ToString().Trim());//密码
        CustInfoModel CustInfoM = new CustInfoModel();
        LinkManModel LinkManM = new LinkManModel();
        EmployeeInfoModel emploModel = new EmployeeInfoModel();
        UserInfoModel userModel = new UserInfoModel();
        
        CustInfoM.CompanyCD = CompanyCD;
        CustInfoM.CustNo = CustNo; //客户编号
        CustInfoM.CustName = CustName;//客户名称
        CustInfoM.ReceiveAddress = CustAddress;//客户地址
        CustInfoM.UsedStatus = "1";
        CustInfoM.CorrYYCode = CorrYYCode;//YY客户编号，关联用友数据库

        LinkManM.CompanyCD = CompanyCD;
        LinkManM.CustNo = CustNo;
        LinkManM.LinkManName = LinkManName;
        LinkManM.HomeAddress = HomeAddress;
        LinkManM.WorkTel = WorkTel;
        LinkManM.Handset = Mobile;
        LinkManM.MailAddress = MailAddress;
        LinkManM.Sex = Sex;

        emploModel.CompanyCD = CompanyCD;
        emploModel.EmployeeNo = EmployeeNo;
        emploModel.EmployeeName = LinkManName;
        emploModel.Sex = Sex;
        emploModel.Mobile = Mobile;

        userModel.CompanyCD = CompanyCD;
        userModel.UserID = UserID;
        userModel.Password = password;
        userModel.UsedStatus = "1";
        userModel.LockFlag = "0";
        userModel.IsCust = "1";
        userModel.OpenDate = Convert.ToDateTime(DateTime.Now.ToString());
        userModel.CloseDate = Convert.ToDateTime(DateTime.Now.AddYears(3).ToString());

        if (UserInfoBus.InsertRegInfo(CustInfoM, LinkManM, emploModel,userModel))
            jc = new JsonClass("success", "注册成功", 1);
        else
            jc = new JsonClass("faile", "注册失败", 0);
        context.Response.Write(jc);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}