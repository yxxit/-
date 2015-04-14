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

public partial class Pages_JTHY_Expenses_SettleVouch_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //this.ddlExpCode.CodingType = ConstUtil.EXPENSES_CODINGTYPE;
            //this.ddlExpCode.ItemTypeID = ConstUtil.EXPENSES_ITEMTYPEID;


            this.txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();

            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人

            //经办人
            this.txtPPersonID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            this.txtPPerson.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;

        }
    }
}
