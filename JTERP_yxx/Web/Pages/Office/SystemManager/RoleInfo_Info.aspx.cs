/**********************************************
 * 类作用：   角色
 * 建立人：   钱锋锋
 * 建立时间： 2010/08/16
 ***********************************************/
using System;
using XBase.Common;
using System.Data;
using System.Collections;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.SystemManager;
using System.Text;
using XBase.Business.Common;

public partial class Pages_Office_SystemManager_RoleInfo_Info : BasePage
{

    /// <summary>
    /// 类名：Role_Info
    /// 描述：角色
    /// 
    /// 作者：钱锋锋
    /// 创建时间：2010/08/16
    /// 最后修改时间：2010/08/16
    /// </summary>
    ///
    protected void Page_Load(object sender, EventArgs e)
    {
        //页面初期设值
        if (!IsPostBack)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            divCompany.InnerHtml = userInfo.CompanyName  ;           
        }
    }

}
