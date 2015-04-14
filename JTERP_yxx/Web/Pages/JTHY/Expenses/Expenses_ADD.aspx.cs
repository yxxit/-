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
using XBase.Data.DBHelper;

public partial class Pages_JTHY_Expenses_Expenses_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ddlExpCode.CodingType = ConstUtil.EXPENSES_CODINGTYPE;
            this.ddlExpCode.ItemTypeID = ConstUtil.EXPENSES_ITEMTYPEID;

            if (this.txtCreateDate.Text == "") //创建日期
            {
                this.txtCreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (this.UserPrincipal.Text == "")  //建档人
            {
                this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            }
            if (txtCreator.Value == "")
            {
                txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            }
            if (this.txtModifiedDate.Text == "")  //最后更改日期
            {
                this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            //if (this.txtConfirmDate.Text == "")  //确认日期
            //{
            //    this.txtConfirmDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //}
            if (this.txtModifiedUserID.Text == "")  //最后更改日期
            {
                txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            }

            BindPayType();  //绑定支付方式
            BindExpType();  //绑定费用类别
        }

    }

    #region  绑定支付方式
    private void BindPayType()
    {

        DataTable dt = GetCodeTypeInfo(ConstUtil.MONEY_TYPE_FLAG, ConstUtil.MONEY_TYPE_CODE);
        if (dt.Rows.Count > 0)
        {
            ddlPayType.DataTextField = "TypeName";
            ddlPayType.DataValueField = "ID";
            ddlPayType.DataSource = dt;
            ddlPayType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlPayType.Items.Insert(0, Item);
    }

    
    #endregion



    #region   绑定费用类别

    private void BindExpType()
    {

        DataTable dt = GetCodeTypeInfo(ConstUtil.EXPENSES_TYPE_FLAG, ConstUtil.EXPENSES_TYPE_CODE);
        if (dt.Rows.Count > 0)
        {
            ddlExpType.DataTextField = "TypeName";
            ddlExpType.DataValueField = "ID";
            ddlExpType.DataSource = dt;
            ddlExpType.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlExpType.Items.Insert(0, Item);
    }
 
    

    #endregion



    private DataTable GetCodeTypeInfo(string type, string code)
    {

        if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(code))
        {
            string sql = "select ID , TypeName from officedba.CodePublicType where TypeFlag='" + type + "' and TypeCode='" + code + "'";
            return SqlHelper.ExecuteSql(sql);

        }
        else
            return null;

    }

}
