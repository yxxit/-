using System;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using XBase.Common;
using XBase.Business.Office.FinanceManager;
public partial class Pages_JTHY_Expenses_IncomeBillList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            InitPage();

            DataTable menuInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).MenuInfo;

            DataRow[] rows = menuInfo.Select("ModuleID = '" + ConstUtil.MODULE_ID_VOUCHER_ADD + "'");

            if (rows.Length <= 0)
            {
                this.btnRegi.Visible = false;
            }
            point.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
        }
    }

    #region 初始化页面
    private void InitPage()
    {
        //新建收款单模块ID
        hidModuleID.Value = ConstUtil.MODULE_ID_INCOMEBILL_ADD;

        //新建凭证模块ID
        VoucherModuleID.Value = ConstUtil.MODULE_ID_VOUCHER_ADD;

        //获取请求参数
        string requestParam = Request.QueryString.ToString();
        //从列表过来时
        int firstIndex = requestParam.IndexOf("&");
        //返回回来时
        if (firstIndex > 0)
        {
            //获取是否查询的标识
            string flag = Request.QueryString["Flag"];
            //点击查询时，设置查询的条件，并执行查询
            if ("1".Equals(flag))
            {
                //收款单号
                txtIncomeNo.Text = Request.QueryString["IncomeNo"];
                //收款方式
                IncomeBillType.SelectedValue = Request.QueryString["AcceWay"];
                //收款金额
                txtTotalPrice.Text = Request.QueryString["TotalPrice"];
                //往来客户
                txtCustName.Text = Request.QueryString["CustName"];
                //确认状态
                DrpConfirmStatus.SelectedValue = Request.QueryString["ConfirmStatus"];
                //登证状态
                DrpIsAccount.SelectedValue = Request.QueryString["IsAccount"];
                //开始时间
                txtStartDate.Text = Request.QueryString["StartDate"];
                //结束时间
                txtEndDate.Text = Request.QueryString["EndDate"];

                this.hidProjectID.Value = Request.QueryString["ProjectID"];

                this.txtProject.Text = Request.QueryString["ProjectName"];

                string pageIndex = Request.QueryString["PageIndex"];
                //获取每页显示记录数 
                string pageCount = Request.QueryString["PageCount"];
                //执行查询
                ClientScript.RegisterStartupScript(this.GetType(), "SearchIncomeBillInfo"
                        , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");SearchIncomeBillInfo('" + pageIndex + "');</script>");
            }
        }
    }
    #endregion

    protected void btnImport_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        int TotalCount = 0;
        //设置查询条件
        string IncomeNo, AcceWay, TotalPrice, CustName, ConfirmStatus, IsAccount, StartDate, EndDate, ProjectID = "";
        //收款单号
        IncomeNo = txtIncomeNo.Text.Trim();
        //收款方式
        //AcceWay = IncomeBillType.SelectedValue.Trim();
        //收款金额
        //TotalPrice = txtTotalPrice.Text.Trim();
        //往来客户
        CustName = txtCustName.Text.Trim();
        //确认状态
        ConfirmStatus = DrpConfirmStatus.SelectedValue.Trim();
        //开始时间
        StartDate = txtStartDate.Text.Trim();
        //结束时间
        EndDate = txtEndDate.Text.Trim();
        //登记凭证状态
        //IsAccount = DrpIsAccount.SelectedValue.Trim();
        //ProjectID = this.hidProjectID.Value;

        ////查询数据
        //Pages_JTHY_Expenses_IncomeBillList
        
        //DataTable dt = XBase.Business.Office.FinanceManager.IncomeBillBus.SearchIncomeInfo(ProjectID, IncomeNo, CustName, ConfirmStatus, StartDate, EndDate, 1, 10000000, "InComeNo", ref TotalCount);
        //DataTable dt = XBase.Business.Office.FinanceManager.IncomeBillBus.SearchIncomeInfo(ProjectID, IncomeNo, CustName, AcceWay, TotalPrice, ConfirmStatus, IsAccount, StartDate, EndDate, 1, 10000000, "InComeNo", ref TotalCount);

        ////导出标题
        string headerTitle = "收款单号|所属项目|收款日期|往来客户|业务单号|确认状态|确认人|确认时间|登记人";
        string[] header = headerTitle.Split('|');

        //导出标题所对应的列字段名称
        string columnFiled = "InComeNo|ProjectName|AcceDate|CustName|BillingNum|ConfirmStatus|Confirmor|ConfirmDate|Accountor";
        string[] field = columnFiled.Split('|');

        //XBase.Common.OutputToExecl.ExportToTable(this.Page, dt, header, field, "收款单列表");
    }
}
