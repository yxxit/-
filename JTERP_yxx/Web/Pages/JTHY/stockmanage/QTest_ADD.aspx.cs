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
using XBase.Data.DBHelper;
using XBase.Common;
public partial class Pages_JTHY_StockManage_QTest_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ddlQTestBillNo.CodingType ="2";
            this.ddlQTestBillNo.ItemTypeID ="4";
            txtCheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
           // BindCheckItem();
        }

    }
    private void BindCheckItem()
    {
        //string strCheckItem = "select ID,TypeName from officedba.CodePublicType where typeflag=10 and typecode=1";
        //DataTable dt = SqlHelper.ExecuteSql(strCheckItem);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    drpCheckItem.DataSource = dt;
        //    drpCheckItem.DataTextField = "TypeName";
        //    drpCheckItem.DataValueField = "ID";
        //    drpCheckItem.DataBind();
        //}
    }
    protected void txtQTestBillNo_TextChanged(object sender, EventArgs e)
    {
        
    }
}
