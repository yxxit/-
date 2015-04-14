/**********************************************
 * 类作用：   新建帐套
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/11
 ***********************************************/

using System;
using System.Web;
using XBase.Common;
using XBase.Model.Office.SystemManager;
using XBase.Business.Office.SystemManager;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data.SqlTypes;

public partial class Pages_Office_SystemManager_Company_Add : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hidModuleID.Value = ConstUtil.Menu_SerchUserInfo;
    }
}
