using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Main :System.Web.UI.Page
{

    public string Zxl100Path
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string UserSessionMinLife = System.Configuration.ConfigurationManager.AppSettings["UserSessionMinLife"];
        inpMessageTipTimerSpan.Value = System.Configuration.ConfigurationManager.AppSettings["DeskTipFrameTimeSpan"] == null ? "300" : System.Configuration.ConfigurationManager.AppSettings["DeskTipFrameTimeSpan"];
        ClientScript.RegisterClientScriptBlock(this.GetType(), "fdsfs", "var UserSessionMinLife=" + UserSessionMinLife,true);


        if (!IsPostBack)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
            {
                case XBase.Common.ConstUtil.Ver_XGoss_Guid://生产版
                    Zxl100Path = "LoginXgoss";
                    // Title = "莱特科技企业信息化管理系统";
                   // Title = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName+"-企业管理平台";
                    Title ="金泰恒业进销存管理系统";
                    break;
                case XBase.Common.ConstUtil.Ver_Zxl100_Guid://执行力  
                    Zxl100Path = "LoginZxl100";
                    Title = "执行力100%";
                    break;
                default://未匹配到
                    break;
            }
            //2014-9-30 修改 去除注释 开始
            //this.txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;

            this.txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            lblCompanyName.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;
            //lblVersion.Text = System.Configuration.ConfigurationManager.AppSettings["VersionName"].ToString();
            //lblVersionCom.Text = System.Configuration.ConfigurationManager.AppSettings["VersionCom"].ToString();
            //lblVersionTel.Text = System.Configuration.ConfigurationManager.AppSettings["VersionTel"].ToString();
            DataTable dt = UserInfoBus.GetUserInfoByID(txt_User.Text, companyCD);
            if (dt.Rows.Count > 0)
            {
                hf_psd.Value = dt.Rows[0]["password"].ToString();
                hfcommanycd.Value = dt.Rows[0]["CompanyCD"].ToString();
                // lblUserInfo.Text = this.txt_User.Text;
                lblUserInfo.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
            }

            //结束

           lblCompanyName.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;


           lblUserInfo.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
           txt_User.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
        }

         //获得用户页面控制权限
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        //btn_czkhlxr
        string[] AuthInfo = XBase.Business.Common.SafeUtil.GetPageAuthorityFromDB("2021202", UserInfo);
             

        //有权限操作页面
        if (AuthInfo != null && AuthInfo.Length > 0)
        {
            //可操作的控件显示
            for (int i = 0; i < AuthInfo.Length; i++)
            {
                if (AuthInfo[i].Trim() == "btn_search")
                {
                    this.btn_czkhlxr.Visible = true;
                    break;
                }
            }
        }

        GetAlertSetting();
        CompanyCD.Text = UserInfo.CompanyCD.ToString();
    }
    protected void GetAlertSetting()
    {
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        DataTable dt1 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "1", UserInfo.UserID);
        if (dt1 == null || dt1.Rows.Count <= 0)
        {
            IsALertStorage.Value = "1";
        }
        else
        {
            if (dt1.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertStorage.Value = "1";
            }
            else
            {
                IsALertStorage.Value = "0";
            }
        }

        DataTable dt2 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "2", UserInfo.UserID);
        if (dt2 == null || dt2.Rows.Count <= 0)
        {
            IsALertCust.Value = "1";
        }
        else
        {
            if (dt2.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertCust.Value = "1";
            }
            else
            {
                IsALertCust.Value = "0";
            }
        }

        DataTable dt3 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "3", UserInfo.UserID);
        if (dt3 == null || dt3.Rows.Count <= 0)
        {
            IsALertProvider.Value = "1";
        }
        else
        {
            if (dt3.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertProvider.Value = "1";
            }
            else
            {
                IsALertProvider.Value = "0";
            }
        }

        DataTable dt4 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "4", UserInfo.UserID);
        if (dt4 == null || dt4.Rows.Count <= 0)
        {
            IsALertFlow.Value = "1";
        }
        else
        {
            if (dt4.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertFlow.Value = "1";
            }
            else
            {
                IsALertFlow.Value = "0";
            }
        }

        DataTable dt5 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "5", UserInfo.UserID);
        if (dt5 == null || dt5.Rows.Count <= 0)
        {
            IsALertTask.Value = "1";
        }
        else
        {
            if (dt5.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertTask.Value = "1";
            }
            else
            {
                IsALertTask.Value = "0";
            }
        }

        DataTable dt6 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "6", UserInfo.UserID);
        if (dt6 == null || dt6.Rows.Count <= 0)
        {
            IsALertContract.Value = "1";
        }
        else
        {
            if (dt6.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertContract.Value = "1";
            }
            else
            {
                IsALertContract.Value = "0";
            }
        }

        DataTable dt7 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "7", UserInfo.UserID);
        if (dt7 == null || dt7.Rows.Count <= 0)
        {
            IsALertMemo.Value = "1";
        }
        else
        {
            if (dt7.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertMemo.Value = "1";
            }
            else
            {
                IsALertMemo.Value = "0";
            }
        }

        DataTable dt8 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "8", UserInfo.UserID);
        if (dt8 == null || dt8.Rows.Count <= 0)
        {
            IsALertMsg.Value = "1";
        }
        else
        {
            if (dt8.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertMsg.Value = "1";
            }
            else
            {
                IsALertMsg.Value = "0";
            }
        }

        DataTable dt9 = BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "9", UserInfo.UserID);
        if (dt9 == null || dt9.Rows.Count <= 0)
        {
            IsALertMeet.Value = "1";
        }
        else
        {
            if (dt9.Rows[0]["usedStatus"].ToString() == "1")
            {
                IsALertMeet.Value = "1";
            }
            else
            {
                IsALertMeet.Value = "0";
            }
        }
    }
}
