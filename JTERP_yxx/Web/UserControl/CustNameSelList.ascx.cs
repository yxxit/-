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

public partial class UserControl_CustNameSelList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 客户ID
    /// </summary>
    public string CustIDContrl
    {
        get
        {
            return hfCustID_Ser.Value;
        }
    }
    //客户编号
    public string CustNoContrl
    {
        set
        {
            //hfCustNoControl.Value = value;
        }
    }
}
