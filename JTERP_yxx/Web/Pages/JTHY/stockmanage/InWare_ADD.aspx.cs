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

public partial class Pages_JTHY_StockManage_InWare_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ddlInWareNoCode.CodingType = ConstUtil.INWARE_CODINGTYPE;
            this.ddlInWareNoCode.ItemTypeID = ConstUtil.INWARE_ITEMTYPEID;
            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            this.txtReceiveTime.Text = DateTime.Now.ToString("yyyy-MM-dd");//默认的入库时间

            this.txtPPersonID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            this.txtPPerson.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            this.DeptName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;
            this.hdDeptID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();
        }


    }
}
