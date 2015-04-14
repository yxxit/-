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
using XBase.Data.DBHelper;
using XBase.Common;

public partial class Pages_JTHY_StockManage_OutWare_ADD : System.Web.UI.Page
{
    string companycd = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (!Page.IsPostBack)
        {
            BindSettleType();//绑定结算方式
            BindTransportType();//绑定运输类型
            //this.BindStorage();//绑定仓库
            ddlOutWareID.CodingType = "2";
            ddlOutWareID.ItemTypeID = "9";
            txtOprName.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            txtOutWareTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
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
    private void BindTransportType()
    {
        DataTable dt = ContractInfoBus.GetdrpTranSportType();
        if (dt != null && dt.Rows.Count > 0)
        {
            drpTransPortType.DataSource = dt;
            drpTransPortType.DataTextField = "TypeName";
            drpTransPortType.DataValueField = "ID";
            drpTransPortType.DataBind();
        }
    }
    /// <summary>
    ///绑定仓库 
    /// </summary>
    //private void BindStorage()
    //{
    //    string strsql = "select ID,StorageName from officedba.storageinfo where usedstatus=1 and companycd='" + companycd + "' ";
    //    DataTable dt = SqlHelper.ExecuteSql(strsql);
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        drpWare.DataSource = dt;
    //        drpWare.DataTextField = "StorageName";
    //        drpWare.DataValueField = "ID";
    //        drpWare.DataBind();
    //    }

    //}
}
