/*************************************
 * 创建人：钱锋锋
 * 创建日期：2010-09-18
 * 描述：帮助文档
 ************************************/
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

public partial class help : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ModuleID = Request.QueryString["ModuleID"].ToString();//客户编号

        

        DataTable dt = XBase.Business.HelpBus.HelpInfo(ModuleID);
        try
        {
            Lbhelp.Text = dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            Lbhelp.Text = "无帮助信息！";
        }

    }
}
