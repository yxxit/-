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
using XBase.Business.Office.CustManager;
using XBase.Business.Common;
using XBase.Data.Personal.Culture;

public partial class Pages_Office_CustManager_Cust_Add :BasePage
{
    string CompanyCD = "";//((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
    //string CompanyCD = "AAAAAA";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.hidUpDateTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//上传附件时间
        if (!Page.IsPostBack)
        {
            CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            ddlCustNo.CodingType = ConstUtil.CUST_CODINGTYPE;
            ddlCustNo.ItemTypeID = ConstUtil.CUST_ITEMTYPEID;

            txtCreator.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;//建单人姓名
            txtCreatedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            txtModifiedUser.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//最后修改人
            txtModifiedDate.Value = DateTime.Now.ToString("yyyy-MM-dd");




            //#region 绑定下拉列表
            //BindDropDownList(this.ddlCustType, ConstUtil.CUST_TYPE_FLAG, ConstUtil.CUST_TYPE_CODE);     //绑定客户类别
            BindDropDownList(this.ddlArea, ConstUtil.AREA_TYPE_FLAG, ConstUtil.AREA_TYPE_CODE);         //绑定区域
            //BindDropDownList(this.seleBusiType, ConstUtil.BUSI_TYPE_FLAG, ConstUtil.BUSI_TYPE_CODE);    //绑定业务类型
            //BindDropDownList(this.ddlCarryType, ConstUtil.CARRY_TYPE_FLAG, ConstUtil.CARRY_TYPE_CODE);  //绑定运输类型
            //BindDropDownList(this.ddlTakeType, ConstUtil.TAKE_TYPE_FLAG, ConstUtil.TAKE_TYPE_CODE);     //绑定交货方式
            //BindDropDownList(this.ddlLinkCycle, ConstUtil.LINK_CYCLE_FLAG, ConstUtil.LINK_CYCLE_CODE);  //绑定联络期限（天）
            //BindDropDownList(this.ddlMoneyType, ConstUtil.MONEY_TYPE_FLAG, ConstUtil.MONEY_TYPE_CODE);  //绑定支付方式
            //BindDropDownList(this.ddlPayType, ConstUtil.PAY_TYPE_FLAG, ConstUtil.PAY_TYPE_CODE);        //绑定结算方式
            //BindDropDownList(this.ddlCurrencyType, ConstUtil.CURRENCY_TYPE_FLAG, ConstUtil.CURRENCY_TYPE_CODE);  //绑定结算币种
            //BindDropDownList(this.seleBillType, ConstUtil.BILL_TYPE_FLAG, ConstUtil.BILL_TYPE_CODE);            //绑定发票类型
            //BindDropDownList(this.txtCompanyType, ConstUtil.COMPANY_TYPE_FLAG, ConstUtil.COMPANY_TYPE_CODE);  //绑定单位性质
            //#endregion

            ////民族
            //CodeTypeDrpControl1.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //CodeTypeDrpControl1.TypeCode = ConstUtil.CODE_TYPE_NATIONAL;
            //CodeTypeDrpControl1.IsInsertSelect = true;

            ////学历
            //CodeTypeDrpControl2.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //CodeTypeDrpControl2.TypeCode = ConstUtil.CODE_TYPE_CULTURE;
            //CodeTypeDrpControl2.IsInsertSelect = true;

            //专业
            //CodeTypeDrpControl3.TypeFlag = ConstUtil.CODE_TYPE_HUMAN;
            //CodeTypeDrpControl3.TypeCode = ConstUtil.CODE_TYPE_PROFESSIONAL;
            //CodeTypeDrpControl3.IsInsertSelect = true;
        }
        //if (Request.QueryString["custid"] != null)
        //    TextBox1.Text = Request.QueryString["custid"].ToString();//获取客户ID号
        //else
        //    TextBox1.Text = Request["hideCustID"];//获取客户ID号
        //if (TextBox1.Text != "")
        //{
        //    CultureDocs doc = new CultureDocs();
        //    DataTable dt = doc.getDocByCustNo(TextBox1.Text);//根据客户ID号查询客户文档
        //    string str = "";
        //    if (dt.Rows.Count > 0)
        //    {
        //        str += "<table width='99%' border='0'align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'>";
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            str += "<tr>";
        //            //str += "<td><input id='Checkbox1' onclick=\"freshChkState(this)\" name='Checkbox1'  value=" + dt.Rows[0]["ID"] + "|typename=客户文档 type='checkbox'/></td>";
        //            str += "<td height='22'  class='td_list_fields' align='right' style='width: 10%;'>文档名称</td>";
        //            str += "<td height='22' bgcolor='#FFFFFF' style='width: 20%'><a href='../../Personal/Culture/CultureEdit.aspx?ModuleID=112&action=read&id=" + dt.Rows[i]["ID"] + "&typename=" + dt.Rows[i]["TypeName"] + "' target='_blank'>" + dt.Rows[i]["Title"].ToString() + "</a></td>";
        //            str += "<td height='22'  class='td_list_fields' align='right' style='width: 10%;'>文档说明</td>";
        //            str += "<td height='22' bgcolor='#FFFFFF' style='width: 20%'>" + dt.Rows[i]["Culturetent"].ToString() + "</td>";
        //            str += "<td height='22'  class='td_list_fields' align='right' style='width: 10%;'>修改时间</td>";
        //            str += "<td height='22' bgcolor='#FFFFFF' style='width: 20%'>" + dt.Rows[i]["ModifiedDate"].ToString() + "</td>";
        //            str += "<td height='22' bgcolor='#FFFFFF'><a href='../../Personal/Culture/CultureEdit.aspx?ModuleID=112&action=edit&id=" + dt.Rows[i]["ID"] + "&typename=" + dt.Rows[i]["TypeName"] + "' target='_blank'>编辑</a>";
        //            str += "<a style='color:red;cursor:pointer;' onclick='deleteDoc(" + dt.Rows[i]["ID"] + ")'>删除</a></td>";
        //            str += "</tr>";
        //        }
        //            str += "</table>";
        //    }
        //    Literal1.Text = str;
        //       // Repeater1.DataSource = dt;
        //   // Repeater1.DataBind();
        //}
    }

    #region 绑定下拉列表
    private void BindDropDownList(DropDownList ddl, string flag, string code)
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(flag, code);
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = "TypeName";
            ddl.DataValueField = "ID";
            ddl.DataSource = dt;
            ddl.DataBind();

        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddl.Items.Insert(0, Item);
    }
    #endregion

    //#region 绑定客户类型
    //private void BindCustType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_FLAG, ConstUtil.CUST_TYPE_CODE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        this.ddlCustType.DataTextField = "TypeName";
    //        this.ddlCustType.DataValueField = "ID";
    //        this.ddlCustType.DataSource = dt;
    //        this.ddlCustType.DataBind();

    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    this.ddlCustType.Items.Insert(0, Item);
    //}
    //#endregion

    #region 绑定联系人类型
    //private void BindLinkManLinkType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_LINK_LINKTYPE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlLinkType.DataTextField = "TypeName";
    //        ddlLinkType.DataValueField = "ID";
    //        ddlLinkType.DataSource = dt;
    //        ddlLinkType.DataBind();

    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlLinkType.Items.Insert(0, Item);
    //}
    #endregion

    #region 绑定客户地区的方法
    private void BindCustArea()
    {
        DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.AREA_TYPE_FLAG, ConstUtil.AREA_TYPE_CODE);
        if (dt.Rows.Count > 0)
        {
            ddlArea.DataTextField = "TypeName";
            ddlArea.DataValueField = "ID";
            ddlArea.DataSource = dt;
            ddlArea.DataBind();
            
        }
        ListItem Item = new ListItem();
        Item.Value = "0";
        Item.Text = "--请选择--";
        ddlArea.Items.Insert(0, Item);
    }
    #endregion    

    #region 绑定客户优质级别的方法
    //private void BindCustCreditGrade()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_CREDITGRADE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlCreditGrade.DataTextField = "TypeName";
    //        ddlCreditGrade.DataValueField = "ID";
    //        ddlCreditGrade.DataSource = dt;
    //        ddlCreditGrade.DataBind();
            
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlCreditGrade.Items.Insert(0, Item);
    //}
    #endregion

    //#region 绑定客户联络期限的方法
    //private void BindCustLinkCycle()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_LINKCYCLE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlLinkCycle.DataTextField = "TypeName";
    //        ddlLinkCycle.DataValueField = "ID";
    //        ddlLinkCycle.DataSource = dt;
    //        ddlLinkCycle.DataBind();
           
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlLinkCycle.Items.Insert(0, Item);
    //}
    //#endregion

    //#region  绑定国家地区的方法
    //private void BindCustCountry()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CODE_TYPE_HUMAN, ConstUtil.CODE_TYPE_COUNTRY);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlCountry.DataTextField = "TypeName";
    //        ddlCountry.DataValueField = "ID";
    //        ddlCountry.DataSource = dt;
    //        ddlCountry.DataBind();

    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlCountry.Items.Insert(0, Item);
    //}
    //#endregion

    //#region 绑定交货方式的方法
    //private void BindCustTakeType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.SELL_TYPE_SELL, ConstUtil.SELL_TYPE_TAKETYPE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlTakeType.DataTextField = "TypeName";
    //        ddlTakeType.DataValueField = "ID";
    //        ddlTakeType.DataSource = dt;
    //        ddlTakeType.DataBind();
           
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlTakeType.Items.Insert(0, Item);
    //}
    //#endregion

    //#region 绑定运货方式的方法
    //private void BindCustCarryType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.SELL_TYPE_SELL, ConstUtil.SELL_TYPE_CARRYTYPE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlCarryType.DataTextField = "TypeName";
    //        ddlCarryType.DataValueField = "ID";
    //        ddlCarryType.DataSource = dt;
    //        ddlCarryType.DataBind();
            
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlCarryType.Items.Insert(0, Item);
    //}
    //#endregion

    //#region 绑定结算方式的方法
    //private void BindCustPayType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_PAYTYPE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlPayType.DataTextField = "TypeName";
    //        ddlPayType.DataValueField = "ID";
    //        ddlPayType.DataSource = dt;
    //        ddlPayType.DataBind();
            
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlPayType.Items.Insert(0, Item);
    //}
    //#endregion

    //#region 绑定支付方式的方法 
    //private void BindMoneyType()
    //{
    //    DataTable dt = CodePublicTypeBus.GetCodeTypeInfo(ConstUtil.CUST_TYPE_CUST, ConstUtil.CUST_INFO_MONEYTYPE);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlMoneyType.DataTextField = "TypeName";
    //        ddlMoneyType.DataValueField = "ID";
    //        ddlMoneyType.DataSource = dt;
    //        ddlMoneyType.DataBind();
            
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlMoneyType.Items.Insert(0, Item);
    //}
    //#endregion

    //#region 绑定币种的方法
    //private void BindCustCurrencyType()
    //{
    //    DataTable dt = CustInfoBus.GetCustCurrencyType(CompanyCD);
    //    if (dt.Rows.Count > 0)
    //    {
    //        ddlCurrencyType.DataTextField = "CurrencyName";
    //        ddlCurrencyType.DataValueField = "ID";
    //        ddlCurrencyType.DataSource = dt;
    //        ddlCurrencyType.DataBind();
            
    //    }
    //    ListItem Item = new ListItem();
    //    Item.Value = "0";
    //    Item.Text = "--请选择--";
    //    ddlCurrencyType.Items.Insert(0, Item);
    //}
    //#endregion

   
}
