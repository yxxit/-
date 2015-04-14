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
public partial class UserControl_LimitArea : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = XBase.Business.Common.CodePublicTypeBus.GetLimitAreaInfoForDrp();

        string str = "    <select id='Select1' name='D1'>";
        str += " <option value=''></option>";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str += " <option value='" + dt.Rows[i][0] + "'>" + dt.Rows[i][0] + "</option>";
        }

        str += " </select>";
        Literal1.Text = str;
    }
}
