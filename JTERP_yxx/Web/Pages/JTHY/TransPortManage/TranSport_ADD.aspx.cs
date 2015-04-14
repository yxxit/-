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

public partial class Pages_JTHY_TransPortManage_TranSport_ADD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //BindTransportType();
            ddlTranSportID.CodingType = "2";
            ddlTranSportID.ItemTypeID = "10";
            this.txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");  //发运的默认时间
            if (this.txt_CreateDate.Text == "") //创建日期
            {
                this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (this.UserPrincipal.Text == "")  //建档人
            {
                this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            }

            if (txtCreator.Value == "")
            {
                txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            }
            if (this.txtModifiedDate.Text == "")  //最后更改日期
            {
                this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (this.txtModifiedUserID.Text == "")  //最后更改日期
            {
                txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            }

            BindTransportStart();
            BindTransportPlan();
            BindTransportFinal();
            
        }
    }

    /// <summary>
    /// 绑定始发站
    /// </summary>
    private void BindTransportStart()
    {
        DataTable dt = ContractInfoBus.GetTransStation(3);
        if (dt != null && dt.Rows.Count > 0)
        {
            drpStartStation.DataSource = dt;
            drpStartStation.DataTextField = "TypeName";
            //drpTransPortType.DataValueField = "ID";
            drpStartStation.DataValueField = "TypeName";
            drpStartStation.DataBind();
        }
    }

    /// <summary>
    /// 绑定原计划到站
    /// </summary>
    private void BindTransportPlan()
    {
        DataTable dt = ContractInfoBus.GetTransStation(5);
        if (dt != null && dt.Rows.Count > 0)
        {
            drpJh_place.DataSource = dt;
            drpJh_place.DataTextField = "TypeName";
            //drpTransPortType.DataValueField = "ID";
            drpJh_place.DataValueField = "TypeName";
            drpJh_place.DataBind();
        }
    }

    /// <summary>
    /// 绑定终到站
    /// </summary>
    private void BindTransportFinal()
    {
        DataTable dt = ContractInfoBus.GetTransStation(7); 
        if (dt != null && dt.Rows.Count > 0)
        {
            drpArriveStation.DataSource = dt;
            drpArriveStation.DataTextField = "TypeName";
            //drpTransPortType.DataValueField = "ID";
            drpArriveStation.DataValueField = "TypeName";
            drpArriveStation.DataBind();
        }
    }
}
