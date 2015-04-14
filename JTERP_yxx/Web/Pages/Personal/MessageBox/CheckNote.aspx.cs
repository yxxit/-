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

using XBase.Common;

public partial class Pages_Personal_MessageBox_CheckNote : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
         this.UserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID+"";
    }
}