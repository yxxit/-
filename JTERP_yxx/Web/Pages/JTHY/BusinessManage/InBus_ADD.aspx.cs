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
using XBase.Business.Office.ContractManage;
using XBase.Common;

public partial class Pages_JTHY_BusinessManage_InBus_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.ddlArriveCode.CodingType = "2";
            this.ddlArriveCode.ItemTypeID = "3";
            BindSettleType();
            // BindTransportType();
            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            //txtSendTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    /// <summary>
    /// 绑定结算方式
    /// </summary>
    private void BindSettleType()
    {
        DataTable dt = ContractInfoBus.GetdrpSettleType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpSettleType.DataSource = dt;
            drpSettleType.DataTextField = "TypeName";
            drpSettleType.DataValueField = "ID";
            drpSettleType.DataBind();
        }

    }
    /// <summary>
    /// 绑定运输类型
    /// </summary>
    //private void BindTransportType()
    //{
    //    DataTable dt = ContractInfoBus.GetdrpTranSportType();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        drpTransPortType.DataSource = dt;
    //        drpTransPortType.DataTextField = "TypeName";
    //        drpTransPortType.DataValueField = "ID";
    //        drpTransPortType.DataBind();
    //    }
    //}
}
