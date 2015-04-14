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
using XBase.Business.Office.CustManager;
using XBase.Business.Common;
using XBase.Common;

public partial class UserControl_addCustLinkMan : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        BindLinkManLinkType();
    }
    #region 绑定联系人类型
    private void BindLinkManLinkType()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_LINK_LINKTYPE);
        if (dt.Rows.Count > 0)
        {
            ddlLinkType.DataTextField = "TypeName";
            ddlLinkType.DataValueField = "ID";
            ddlLinkType.DataSource = dt;
            ddlLinkType.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlLinkType.Items.Insert(0, Item);
    }
    #endregion
}
