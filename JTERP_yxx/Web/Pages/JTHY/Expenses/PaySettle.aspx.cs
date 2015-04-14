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

public partial class Pages_JTHY_Expenses_PaySettle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {


           this.txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");//制单日期

           this.txtCreatePerson.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//制单人


            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//制单人的id



        }



    }
}
