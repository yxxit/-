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
public partial class UserControl_AddLimitArea : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = XBase.Business.Common.CodePublicTypeBus.GetLimitAreaInfoForDrp();

        string str = "";

        for (int i = 1; i <dt.Rows.Count+1; i++)
        {
            str += "  <input id=" + i + " onclick=showHTML('" + i + "')" + " type='checkbox' name='check'  value='" + dt.Rows[i - 1][0] + "'" + "/><span>" + dt.Rows[i - 1][0] + "</span>" + "     ";
            // str+= "<a>2123</a>  ";
            if (i%6==0)
                str +="<br/>";
        }


        Literal1.Text = str;
    }
}
