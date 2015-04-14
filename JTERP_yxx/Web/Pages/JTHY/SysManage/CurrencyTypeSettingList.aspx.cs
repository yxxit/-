using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Business.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using XBase.Common;
using XBase.Business.Common;

public partial class Pages_Office_FinanceManager_CurrencyTypeSettingList : BasePage
{
    #region 主键ID
    private int ID
    {
        set { ViewState["id"] = value; }
        get { return Convert.ToInt32(ViewState["id"]); }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCurrencyTypeSettingInfo();//加载币种信息
        }
    }
    #region 加载币种信息
    private void LoadCurrencyTypeSettingInfo()
    {
        DataTable dt = CurrTypeSettingBus.GetCurrTypeByCompanyCD();
        if (dt != null && dt.Rows.Count > 0)
        {
            WarningList.DataSource = dt;//绑定列表
            WarningList.DataBind();
        }
    }
    #endregion

    #region 判断选择框是否选择
    private bool Check()
    {
        bool result = false;
        if (WarningList.Items.Count > 0)
        {
            for (int i = 0; i < WarningList.Items.Count; i++)
            {
                CheckBox check = WarningList.Items[i].FindControl("CheckStatus") as CheckBox;
                if (check.Checked)
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    #endregion

    #region 检验控件值
    private bool CheckInput()
    {
        string ErrorMsg = "";
        bool result = true;
        if (this.txtCurrencyName.Text.Trim().Length<=0)
        {
            ErrorMsg += "币种名称不能为空,请输入";
            txtCurrencyName.Focus();
            result = false;
        }
        else if (this.txtCurrencySymbol.Text.Trim() == "")
        {
            ErrorMsg += "币种符号不能为空,请输入";
            txtCurrencySymbol.Focus();
            result = false;
        }
        else if (this.DdlConvertWay.SelectedValue=="0")
        {
            ErrorMsg += "请选择折算方式";
            DdlConvertWay.Focus();
            result = false;
        }
        else if (this.txtExchangeRate.Text.Trim().Length<=0)
        {
            ErrorMsg += "汇率不能为空，请输入";
            txtExchangeRate.Focus();
            result = false;
        }
        else if (!ValidateUtil.IsNumeric(this.txtExchangeRate.Text.Trim()))
        {
            ErrorMsg += "汇率格式错误必须为数字";
            txtExchangeRate.Focus();
            result = false;
        }
        else if (Convert.ToDecimal(this.txtExchangeRate.Text.Trim()) <= 0)
        {
            ErrorMsg += "汇率必须大于零";
            txtExchangeRate.Focus();
            result = false;
        }
        else if (!ValidateUtil.IsDateString(this.txtChangeTime.Text.Trim()))
        {
            ErrorMsg += "日期格式不正确，请重新输入";
            txtChangeTime.Focus();
            result = false;
        }
        if (ErrorMsg.Length > 0)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('" + ErrorMsg + "');</script>");
        }
        return result;
    }
    #endregion

    #region 保存币种类别信息
    protected void Save_FinanceWarning_Click(object sender, ImageClickEventArgs e)
    {
        if (!CheckInput()) return;
        CurrencyTypeSettingModel Model = new CurrencyTypeSettingModel();
        Model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        Model.CurrencyName = this.txtCurrencyName.Text;//币种名称
        Model.CurrencySymbol = this.txtCurrencySymbol.Text;//币种符号
        Model.isMaster = this.RdbisMaster.SelectedValue;
        Model.ConvertWay =Convert.ToInt32(this.DdlConvertWay.SelectedValue);
        Model.ChangeTime = Convert.ToDateTime(this.txtChangeTime.Text);
        Model.ExchangeRate = Convert.ToDecimal(this.txtExchangeRate.Text);
        Model.UsedStatus = this.RdbUsedStatus.SelectedValue;
        try
        {
            string idCode = string.Empty;
            if (txtAction.Value.Trim() == ActionUtil.Add.ToString())
            {
                idCode = "0";
            }
            else if (txtAction.Value.Trim() == ActionUtil.Edit.ToString())
            {
                idCode = ID.ToString();
            }
            bool IsMaster = false;
            if (Model.isMaster == "1")
            {
                IsMaster = true;
            }
            if (txtAction.Value.Trim() == ActionUtil.Add.ToString())
            {
               
                if (CurrTypeSettingBus.IsExitsMasterCurrency(idCode)&&IsMaster)
                {
                    
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('已存在本位币，不能重复添加本位币！');</script>");

                }
                else
                {
                    int nev = CurrTypeSettingBus.IsSame(Model.CurrencyName, Model.CurrencySymbol, idCode);
                    if (nev == 0)
                    {
                        CurrTypeSettingBus.InSertCurrTypeSetting(Model);
                        txtAction.Value = ActionUtil.Edit.ToString();
                        ID = IDIdentityUtil.GetIDIdentity(ConstUtil.TABLE_FINANCEWARNING_CODE);
                        LoadCurrencyTypeSettingInfo();
                    }
                    else if (nev == 1)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种名称已存在！请重新输入！');</script>");
                    }
                    else if (nev == 2)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种符已存在！请重新输入！');</script>");
                    }
                    else if (nev == 3)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种名称,币种符已存在！请重新输入！');</script>");
                    }

                }
            }
            else if (txtAction.Value.Trim() == ActionUtil.Edit.ToString())
            {
                Model.ID = ID;
                if (CurrTypeSettingBus.IsExitsMasterCurrency(idCode) && IsMaster)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('已存在本位币，不能重复添加本位币！');</script>");
                }
                else
                {
                    int nevv = CurrTypeSettingBus.IsSame(Model.CurrencyName, Model.CurrencySymbol, idCode);
                    if (nevv == 0)
                    {
                        CurrTypeSettingBus.UpdateCurrTypeSetting(Model);
                        LoadCurrencyTypeSettingInfo();
                    }
                    else if (nevv == 1)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种名称已存在！请重新输入！');</script>");
                    }
                    else if (nevv == 2)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种符已存在！请重新输入！');</script>");
                    }
                    else if (nevv == 3)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('币种名称,币种符已存在！请重新输入！');</script>");
                    }
                }
            }
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('保存成功');</script>");
        }
        catch
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('保存失败,请联系管理员');</script>");
        }
    }
    #endregion

    #region 删除币种信息
    protected void Delete_FinanceWarning_Click(object sender, ImageClickEventArgs e)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        if (Check())
        {
            try
            {
                if (WarningList.Items.Count > 0)
                {
                    string Id = string.Empty;
                    for (int i = 0; i < WarningList.Items.Count; i++)
                    {
                        CheckBox check = WarningList.Items[i].FindControl("CheckStatus") as CheckBox;
                        if (check.Checked)
                        {
                            Id += WarningList.DataKeys[i].ToString()+",";
                           
                        }
                    }

                    CurrTypeSettingBus.DeleteCurrTypeSetting(CompanyCD, Id.TrimEnd(new char[] {','}));
                    LoadCurrencyTypeSettingInfo();
                    this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('删除成功');</script>");
                }
            }
            catch
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('删除失败');</script>");
            }
        }
        else
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "info", "<script>popMsgObj.ShowMsg('请选择删除的行');</script>");
        }
    }
    #endregion

    #region 编辑行
    protected void WarningList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Trim() == ActionUtil.Edit.ToString())
        {
            this.txtCurrencyName.Text = ((Label)e.Item.FindControl("LabCurrencyName")).Text.Trim();
            this.txtCurrencySymbol.Text = ((Label)e.Item.FindControl("LabCurrencySymbol")).Text.Trim();
            this.RdbisMaster.SelectedValue = ((Label)e.Item.FindControl("LabisMaster")).Text.Trim()=="是"?"1":"0";
            this.txtChangeTime.Text = ((Label)e.Item.FindControl("LabChangeTime")).Text.Trim();
            this.txtExchangeRate.Text = ((Label)e.Item.FindControl("LabExchangeRate")).Text.Trim();
            this.RdbUsedStatus.SelectedValue = ((Label)e.Item.FindControl("LabUsedStatus")).Text.Trim()=="启用"?"1":"0";
           
            txtAction.Value = ActionUtil.Edit.ToString();
            ID = Convert.ToInt32(WarningList.DataKeys[e.Item.ItemIndex]);
        }
    }
    #endregion
}
