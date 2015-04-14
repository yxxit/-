using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using XBase.Common;

public partial class Pages_Personal_FileManage_FileList : System.Web.UI.Page
{
    private UserInfoUtil UserInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        DataTable dt1 = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(UserInfo.UserID);
        switch (dt1.Rows[0]["RoleRange"].ToString())
        {
            case "0": UserLookPower.Text = "本人"; break;
            case "1": UserLookPower.Text = "本人及下属部门"; break;
            case "2": UserLookPower.Text = "本部门"; break;
            case "3": UserLookPower.Text = "全部"; break;
        }
        if (!this.IsPostBack)
        {
            string typeid = (Request.QueryString["typeid"] == null) ? "" : Request.QueryString["typeid"].ToString();
            string typename = (Request.QueryString["TypeName"] == null) ? "" : Request.QueryString["TypeName"].ToString();
            hidtypeid.Value = typeid;
            hidtypename.Value = typename;

            DataTable dt = new XBase.Business.Personal.Culture.CultureType().GetList("CompanyCD='" + UserInfo.CompanyCD + "'");

            if (hidtypeid.Value != "")
            {
                this.inputCompuny.Value = hidtypeid.Value;
            }
            if (hidtypename.Value != "")
            {
                this.Compuny.Text = typename;
            }
        }
    }
}
