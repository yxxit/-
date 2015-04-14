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
//using XBase.Business.Office.CustManager;
using XBase.Business.Office.ContractManage;
using XBase.Business.Common;

public partial class Pages_JTHY_ContractManage_SellContract_Add : BasePage
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidUpDateTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//上传附件时间
        if (!Page.IsPostBack)
        {

        

            FlowApply1.BillTypeFlag = "30";
            FlowApply1.BillTypeCode = "1";

          
            BindSettleType();
            BindTransportType();
            // BindCoalType(drpCoalType1);
            // BindUnitCode(drpComUnit1);
            ddlContractID.CodingType = ConstUtil.SELL_CODINGTYPE;
            ddlContractID.ItemTypeID = ConstUtil.SELL_ITEMTYPEID;
            this.txtSignDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            //this.txtPPersonID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            //this.txtPPerson.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            //this.DeptName.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptName;
            //this.hdDeptID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID.ToString();


            //this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            //this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.txtModifiedDates.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.txtModifiedUserIDs.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人

          
        
        
        
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
    /// 绑定煤种
    /// </summary>
    private void BindCoalType(HtmlSelect dtrcoal)
    {
        DataTable dt = ContractInfoBus.GetdrpCoalType();
        if (dt != null && dt.Rows.Count > 0)
        {
            ListItem item = new ListItem();
            item.Text = "--请选择--";
            item.Value = "0";
            dtrcoal.Items.Add(item);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem item1 = new ListItem();
                item1.Text = dt.Rows[i]["ProductName"].ToString();
                item1.Value = dt.Rows[i]["ID"].ToString();
                dtrcoal.Items.Add(item1);
            }
          
            //dtrcoal.DataSource = dt;
            //dtrcoal.DataTextField = "ProductName";
            //dtrcoal.DataValueField = "ID";
            //dtrcoal.DataBind();
        }
    }

    /// <summary>
    /// 绑定计量单位
    /// </summary>
    private void BindUnitCode(HtmlSelect dtrunit)
    {
        DataTable dt = ContractInfoBus.GetdrpUnitCode();
        if (dt != null && dt.Rows.Count > 0)
        {
            dtrunit.DataSource = dt;
            dtrunit.DataTextField = "CodeName";
            dtrunit.DataValueField = "ID";
            dtrunit.DataBind();
        }
    }
   
}
