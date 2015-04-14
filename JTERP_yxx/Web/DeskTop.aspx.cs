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
using System.Text;
using System.Globalization;

using XBase.Common;
using XBase.Business.Office.AdminManager;
using XBase.Model.Office.AdminManager;
using XBase.Data.Office.AdminManager;

public partial class DeskTop : BasePage, System.Web.SessionState.IRequiresSessionState
{
    private  ChineseLunisolarCalendar chineseDate = new ChineseLunisolarCalendar();
    string CompanyCD = "";
    int Employeeid;
    string date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
            //this.inputTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //this.lblshowyear.InnerHtml = DateTime.Now.Year + "年";
            //this.lblmonth.InnerHtml = DateTime.Now.Month + "月";
            //this.spanday.InnerHtml = DateTime.Now.ToString("dd");
            //this.lblweek.InnerHtml = ReturnWeek(DateTime.Now.DayOfWeek.ToString());            
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
             if (userInfo.IsRoot == "1") {
               
             }
           
        }
    }  
      
}
