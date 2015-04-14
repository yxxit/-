using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XBase.Common;
using XBase.Business.Office.FinanceManager;
public partial class Pages_JTHY_Expenses_PayBill_Add : BasePage
{
    private const string SUBJECTSNAME = "银行存款";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //初始化页面信息
            InitControlinfo();
            //初始化页面状态
            InitPageStatus();

            DataTable MasterCurrencydt = CurrTypeSettingBus.GetMasterCurrency();
            if (MasterCurrencydt.Rows.Count > 0 && MasterCurrencydt != null)
            {
                //this.hiddenCurryTypeID.Value = MasterCurrencydt.Rows[0]["ID"].ToString();
                //this.txtCurryType.Value = MasterCurrencydt.Rows[0]["CurrencyName"].ToString();
                //this.txtExchangeRate.Value = MasterCurrencydt.Rows[0]["ExchangeRate"].ToString();
            }
            ListItem Item1 = new ListItem("无来源", "8");
            BillingType.Items.Add(Item1);
            ListItem Item3 = new ListItem("销售订单", "1");
            BillingType.Items.Add(Item3);
            ListItem Item2 = new ListItem("代销结算单", "4");
            BillingType.Items.Add(Item2);
            ListItem Item4 = new ListItem("采购退货单", "5");
            BillingType.Items.Add(Item4);

            ListItem Item5 = new ListItem("销售发货单", "7");
            BillingType.Items.Add(Item5);
            Confirmname.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserName.ToString();
        }
    }

    #region initPage
    private void InitControlinfo()
    {
        txtSaveUserID.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        txtAcceDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        UsertxtExcutor.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        jiandangren.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;

        DataTable dt = VoucherBus.GetSubjectsInfo(SUBJECTSNAME);

        DataTable subjectsDT = dt.Clone();
        subjectsDT.Clear();
        if (subjectsDT == null||subjectsDT.Columns.Count<=0)
        {
            subjectsDT.Columns.Add("SubjectsName");
            subjectsDT.Columns.Add("SubjectsCD");
        }
        DataRow roww = subjectsDT.NewRow();
        roww["SubjectsName"] = "--请选择--";
        roww["SubjectsCD"] = "";
        subjectsDT.Rows.Add(roww);
       
            foreach (DataRow dr in dt.Rows)
            {
                DataRow row = subjectsDT.NewRow();
                row["SubjectsName"] = dr["SubjectsName"].ToString();
                row["SubjectsCD"] = dr["SubjectsName"].ToString();
                subjectsDT.Rows.Add(row);

            }


            if (dt.Rows.Count > 0)
            {
                //this.ddlBankName.DataTextField = "SubjectsName";
                //this.ddlBankName.DataValueField = "SubjectsCD";
                //this.ddlBankName.DataSource = subjectsDT;
                //this.ddlBankName.DataBind();
            }
        
    }
    #endregion

    #region  初始化页面
    private void InitPageStatus()
    {
        #region 新建、修改共通处理
        //收款单列表模块ID
        hidModuleID.Value = ConstUtil.MODULE_ID_INCOMEBILL_LIST;
        //this.hidCashModuleID.Value = ConstUtil.MODULE_ID_CASHACCOUNT;
        //this.hidBankModuleID.Value = ConstUtil.MODULE_ID_BANKACCOUNT;

        /*
             * 判断当前企业会计科目是否初始化，获取初始化期间，该企业凭证只能从他初始化期间后做凭证 Start
             */
        //this.HiddenIsSubjectsBgein.Value = SubjectsBeginDetailsBus.GetPeriodNum();
        /*
         * 判断当前企业会计科目是否初始化，获取初始化期间，该企业凭证只能从他初始化期间后做凭证 End
         */

        //获取请求参数
        string requestParam = Request.QueryString.ToString();
        //通过参数个数来判断是否从菜单过来
        int firstIndex = requestParam.IndexOf("&");
        //从列表过来时
        if (firstIndex > 0)
        {
            //返回按钮可见
            //btnBack.Visible = true;
            //获取列表的查询条件
            string searchCondition = requestParam.Substring(firstIndex);
            //设置检索条件
            hidSearchCondition.Value = searchCondition;
        }
        #endregion

        //获取ID
        string ID = Request.QueryString["ID"];
        
        if (!string.IsNullOrEmpty(ID))
        {
            //islistcome.Value = ID;
            islistcome.Value = ID;
            string BillingID="0";
            string TotalPrice = "0";


            DataTable dt = IncomeBillBus.GetIncomeBillInfoByID(ID);
            if (dt.Rows.Count > 0)
            {
                BillingID = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BillingID");
                TotalPrice = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TotalPrice");
            }

            //获取业务单ID
            txtSaveID.Value = BillingID;

            //获取收款单对应的业务单未付和已付金额
            decimal NAccounts = BillingBus.GetBillingNAccounts(BillingID);
            decimal YAccounts = BillingBus.GetBillingYAccounts(BillingID);

            //获取收款单金额
            txtIncomePrice.Value = (NAccounts + YAccounts).ToString();
          

            ////获取业务单未结金额
            //decimal NAccount = BillingBus.GetBillingNAccounts(BillingID);
            //if (NAccount > 0)
            //{
            //    txtNAccounts.Value = BillingBus.GetBillingNAccounts(BillingID).ToString();
            //}
            ////获取业务单总金额
            //decimal TotalPrice = BillingBus.GetBillingTotalAccounts(BillingID);
            //if (TotalPrice > 0)
            //{
            //    txtNAccounts.Value = TotalPrice.ToString();
            //}
          //  txtTotalPrice.Disabled = true;


            //设置标题
            divTitle.InnerText = "收款单信息";
            //操作ID
            txtOprtID.Value = ID;
            // 获取是收款单信息
            InitPageInfo(ID);
            //更改操作行为值
            txtAction.Value = "2";
            this.hidCashOrBankType.Value = Request.QueryString["CashOrBankType"] == null ? "" : Request.QueryString["CashOrBankType"].ToString();
        }
    }
    #endregion

    #region InitPageInfo
    private void InitPageInfo(string ID)
    {
       
        DataTable dt = IncomeBillBus.GetIncomeBillInfoByID(ID);

        //获取业务单ID
        string BillingID ="0";
        if (dt.Rows.Count > 0)
        {
            BillingID = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BillingID");
            txtSaveID.Value = BillingID;
        }
        decimal NAccounts = BillingBus.GetBillingNAccounts(BillingID);

        divCodeRule.Attributes.Add("style", "display:none;");
        divIncomeNo.Attributes.Add("style", "display:block;");

       
        if (dt != null && dt.Rows.Count > 0)
        {
            txtOldPrice.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TotalPrice");

            txtNAccounts.Value = (NAccounts + Convert.ToDecimal(GetSafeData.ValidateDataRow_String(dt.Rows[0], "TotalPrice"))).ToString();

            //基本信息
            divIncomeNo.InnerHtml = GetSafeData.ValidateDataRow_String(dt.Rows[0], "IncomeNo");
            txtAcceDate.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "AcceDate") == "" ? "" : Convert.ToDateTime(GetSafeData.ValidateDataRow_String(dt.Rows[0], "AcceDate")).ToString("yyyy-MM-dd");
            txtCustName.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CustName");
            txtOrder.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BillingNum");
            txtTotalPrice.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TotalPrice");
            price.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "TotalPrice");
            DrpAcceWay.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "AcceWay");
            //ddlBankName.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BankName");
            //txtBanlNo.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "AccountNo");
            //this.rblBlengding.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "BlendingType");
            if (DrpAcceWay.SelectedValue == "1")
            {
                //txtBankName.ReadOnly = false;
                //ddlBankName.Enabled = false;
             
                //txtBanlNo.ReadOnly = false;
            }
            txtSaveUserID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Executor");
            UsertxtExcutor.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ExecutorName");
            //txtRemark.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Summary");

            //this.txtCurryType.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CurrencyName");
            //this.hiddenCurryTypeID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CurrencyType");
            //this.txtExchangeRate.Value = Math.Round(Convert.ToDecimal(GetSafeData.ValidateDataRow_String(dt.Rows[0], "CurrencyRate")), 2).ToString();

            this.CustID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "CustID");
            this.FromTBName.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "FromTBName");
            this.FileName.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "FileName");
            //this.txtProject.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProjectName");
            //this.hidProjectID.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ProjectID");
            PaymentTypes.SelectedValue = GetSafeData.ValidateDataRow_String(dt.Rows[0], "PaymentTypes");
            //确认信息
           
            //txtConfirmStatus.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmStatus");
            if (GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmStatus") == "未确认")
            {
                Confirm.Value = "0";
            }
            else
            {
                Confirm.Value = "1";
            }
            //if (txtConfirmStatus.Value == ConstUtil.CONFIRM_STATUS_NAME_OK)
            //{
            //    //btnIncomeBill_Save.Visible = false;
            //    txtTotalPrice.Disabled = true;
            //}
            txtConfirmDate.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmDate") == "" ? "" : Convert.ToDateTime(GetSafeData.ValidateDataRow_String(dt.Rows[0], "ConfirmDate")).ToString("yyyy-MM-dd");
            txtConfirmor.Value = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Confirmor");
            //登记信息
            //txtIsAccount.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "IsAccount");
            //txtAccountDate.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "AccountDate") == "" ? "" : Convert.ToDateTime(GetSafeData.ValidateDataRow_String(dt.Rows[0], "AccountDate")).ToString("yyyy-MM-dd");
            //txtAccountor.Text = GetSafeData.ValidateDataRow_String(dt.Rows[0], "Accountor");
        }
    }
    #endregion

}
