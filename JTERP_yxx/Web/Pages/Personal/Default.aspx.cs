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

public partial class Pages_Personal_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
            {
                case XBase.Common.ConstUtil.Ver_XGoss_Guid://生产版
                    divZxl100.InnerHtml = "";
                    break;
                case XBase.Common.ConstUtil.Ver_Zxl100_Guid://执行力 
                    divDefault.InnerHtml = "";
                    break;
                default://未匹配到
                    break;
            }
        }
    }
}
