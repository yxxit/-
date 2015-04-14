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
using XBase.Business.Common;
using XBase.Business.Office.ContractManage;
using XBase.Data.DBHelper;

public partial class Pages_JTHY_BusinessManage_OutBus_ADD : System.Web.UI.Page
{
    string companycd = "";
    protected void Page_Load(object sender, EventArgs e)
    {    
        companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        if (!IsPostBack)
        {
            string typeflag = "0";
            if (Request.QueryString["typeflag"] != null)
                typeflag = Request.QueryString["typeflag"].ToString();
        
            if (typeflag == "0")  //销售出库
            {
                this.ddlSendNo.CodingType = "2";
                this.ddlSendNo.ItemTypeID = "9";
            }
            if (typeflag == "1") //采购直销
            {
                this.ddlSendNo.CodingType = "2";
                this.ddlSendNo.ItemTypeID = "8";
            }
            //SendNo.CodingType = ConstUtil.SELLOUT_CODINGTYPE;
            //SendNo.ItemTypeID = ConstUtil.SELLOUT_ITEMTYPEID;
            this.BindSettleType();   //绑定结算方式
            // this.BindTransportType();//绑定调运类型
            //this.BindStorage();//绑定仓库

            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;


            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            //txtSendTime.Text = DateTime.Now.ToString("yyyy-MM-dd");



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
            //drpTransPortType.DataSource = dt;
            //drpTransPortType.DataTextField = "TypeName";
            //drpTransPortType.DataValueField = "ID";
            //drpTransPortType.DataBind();
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
    ///绑定仓库 
    /// </summary>
    //private void BindStorage()
    //{
    //    string strsql = "select ID,StorageName from officedba.storageinfo where usedstatus=1 and companycd='"+companycd+"' ";
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
