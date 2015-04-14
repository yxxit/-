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
using XBase.Business.Common;
using System.Text;

using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Top : BasePage
{


    public string Zxl100Path
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {






        //获得用户页面控制权限
        UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];


        hidCompanCD.Value = UserInfo.CompanyCD;
        hidEmployeeID.Value = UserInfo.EmployeeID.ToString();


    
     //lblCompanyName.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;
   
       
     //   lblUserInfo.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName;
      
        if (!IsPostBack)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
            {
                case XBase.Common.ConstUtil.Ver_XGoss_Guid://生产版                    
                    Zxl100Path = string.Empty;
                    //workModelInfo.InnerHtml = "执行模式";
                    //spanCurrentType.Visible = true;
                    break;
                case XBase.Common.ConstUtil.Ver_Zxl100_Guid://执行力 
                    
                    Zxl100Path = "ZxlDefault/";
                    //workModelInfo.InnerHtml = "执行力";
                    //spanCurrentType.Visible = false;
                    break;
                default://未匹配到
                    break;
            }

            //初始化Top菜单
            InitTopMenu(UserInfo);

            //backSiteDomain
            string backSiteDomain = (string)ConfigurationManager.AppSettings["backSiteDomain"];
            if (!backSiteDomain.EndsWith("/"))
            {
                backSiteDomain = backSiteDomain + "/";
            }


            XBase.Model.SystemManager.CompanyOpenServModel comInfo = XBase.Business.SystemManager.CompanyOpenServBus.GetCompanyOpenServInfo(UserInfo.CompanyCD);
            if (comInfo != null)
            {
                if (comInfo.LogoImg.Trim() + "" != "")
                {
                    img_logo.Src = backSiteDomain + "/CompanyLogo/" + comInfo.LogoImg;
                }
                else
                {
                    img_logo.Src = "images/" + Zxl100Path + "LOGO.png";
                }
            }

        }

    }

    /// <summary>
    /// 初始化Top菜单
    /// </summary>
    private void InitTopMenu(UserInfoUtil UserInfo)
    {
        //从用户信息中获取菜单信息
        //DataTable menuInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).MenuInfo;
        DataTable menuInfo = UserInfo.MenuInfo;
        //从菜单信息中获取Top信息
        DataRow[] rows = menuInfo.Select("ParentID is null");
        if (rows.Length < 1)
        {
            return;
        }
        try
        {
            DataTable DT = QukMenuBus.QukMenuSelect(UserInfo.CompanyCD, UserInfo.UserID.ToString());
            if (DT != null)
            {
                StringBuilder sb = new StringBuilder();
                if(DT.Rows.Count>0)
                    sb.Append("<font color=\"#5A5A5A\">快捷键：</font>");
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    string name = DT.Rows[i][2].ToString();
                    string url = DT.Rows[i][3].ToString();
                    string ModuleID = DT.Rows[i][0].ToString();
                    var result = url.IndexOf("?");
                    if (result > 0)
                    {
                        sb.Append(" <a href='#' onclick='parent.addTab(this);' tabid='" + ModuleID + "' tabTitle='" + name + "' dataLink='" + url + "&ModuleID=" + ModuleID + "'  >");
                        
                    }
                    else
                    {
                        sb.Append(" <a href='#' onclick='parent.addTab(this);' tabid='" + ModuleID + "' tabTitle='" + name + "' dataLink='" + url + "?ModuleID=" + ModuleID + "'  >");
                                            }
                    sb.Append("&nbsp;<font color=\"#63B8FF\">&nbsp;" + name + "&nbsp;</font>&nbsp;");
                    sb.Append("</a>");
                }
                Labelqm.Text = sb.ToString();
            }
        }
        catch (Exception e)
        { }
        for (int i = 0; i < rows.Length; i++)
        {
            DataRow data = rows[i];
            //获取Top菜单的模块ID
            int moduleID = int.Parse((string)data["ModuleID"]);
            switch (moduleID)
            {
                case 1:
                    //显示个人桌面
                    //TopMenuCell1.Visible = true;
                    //this.Label1.Visible = true;
                    continue;
                case 2:
                    //显示办公模式
                    //TopMenuCell2.Visible = true;
                    //this.Label2.Visible = true;
                    continue;
                case 3:
                    //显示运营模式
                    //TopMenuCell3.Visible = true;
                    //this.Label3.Visible = true;
                    continue;
                case 4:
                    //显示决策模式
                    //TopMenuCell4.Visible = true;
                    //this.Label4.Visible = true;
                    continue;
                case 5:
                    //显示知识中心
                    //TopMenuCell5.Visible = true;
                    //this.Label5.Visible = true;
                    continue;
                case 6:
                    //显示监控中心
                    //TopMenuCell6.Visible = true;
                    //his.Label6.Visible = true;
                    continue;
                default:
                    continue;
            }
        }
    }



}
