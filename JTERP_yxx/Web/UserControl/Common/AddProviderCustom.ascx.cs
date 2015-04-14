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
using XBase.Business.Office.CustManager;
using XBase.Business.Common;


public partial class UserControl_Common_AddProviderCustom : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCustNo.CodingType = ConstUtil.CUST_CODINGTYPE;
        ddlCustNo.ItemTypeID = ConstUtil.CUST_ITEMTYPEID;
    }
}
