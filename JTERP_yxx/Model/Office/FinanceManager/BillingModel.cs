/**********************************************
 * 类作用：   开票表数据模板
 * 建立人：   江贻明
 * 建立时间： 2009/04/15
 ***********************************************/
using System;
namespace XBase.Model.Office.FinanceManager
{
  public class BillingModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _billcd;
        private string _billingnum;
        private string _billingtype;
        private string _invoicetype;
        private DateTime _createdate;
        private string _contactunits;
        private string _acceway;
        private decimal _totalprice;
        private decimal _yaccounts;
        private decimal _naccounts;
        private int _executor;
        private int _deptid;
        private string _isaudit;
        private int _auditor;
        private DateTime _auditdate;
        private int _register;
        private string _isvoucher;
        private string _sourcedt;
        private string _sourceid;
        private string _columnname;
        private string _accountsstatus;
        private string _remark;
        private int _currencytype;
        private decimal _currencyrate;
        private int _custid;
        private string _fromtbname;
        private string _filename;
        private string _Accountsorcope;
        /// <summary>
        /// 主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编码
        /// </summary>
        public string BillCD
        {
            set { _billcd = value; }
            get { return _billcd; }
        }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string BillingNum
        {
            set { _billingnum = value; }
            get { return _billingnum; }
        }
        /// <summary>
        /// 开票类型
        /// </summary>
        public string BillingType
        {
            set { _billingtype = value; }
            get { return _billingtype; }
        }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 往来单位
        /// </summary>
        public string ContactUnits
        {
            set { _contactunits = value; }
            get { return _contactunits; }
        }
        /// <summary>
        /// 接收方式
        /// </summary>
        public string AcceWay
        {
            set { _acceway = value; }
            get { return _acceway; }
        }
        /// <summary>
        /// 开票总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 已结金额
        /// </summary>
        public decimal YAccounts
        {
            set { _yaccounts = value; }
            get { return _yaccounts; }
        }
        /// <summary>
        /// 未结金额
        /// </summary>
        public decimal NAccounts
        {
            set { _naccounts = value; }
            get { return _naccounts; }
        }
        /// <summary>
        /// 执行人
        /// </summary>
        public int Executor
        {
            set { _executor = value; }
            get { return _executor; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public int DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 是否审核
        /// </summary>
        public string IsAudit
        {
            set { _isaudit = value; }
            get { return _isaudit; }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public int Auditor
        {
            set { _auditor = value; }
            get { return _auditor; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 登记人
        /// </summary>
        public int Register
        {
            set { _register = value; }
            get { return _register; }
        }
        /// <summary>
        /// 是否登记凭证
        /// </summary>
        public string IsVoucher
        {
            set { _isvoucher = value; }
            get { return _isvoucher; }
        }
        /// <summary>
        /// 来源表
        /// </summary>
        public string SourceDt
        {
            set { _sourcedt = value; }
            get { return _sourcedt; }
        }
        /// <summary>
        /// 来源表主键
        /// </summary>
        public string SourceID
        {
            set { _sourceid = value; }
            get { return _sourceid; }
        }
        /// <summary>
        /// 源单表关联的字段
        /// </summary>
        public string ColumnName
        {
            set { _columnname = value; }
            get { return _columnname; }
        }
        /// <summary>
        /// 结算状态
        /// </summary>
        public string AccountsStatus
        {
            set { _accountsstatus = value; }
            get { return _accountsstatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 币种
        /// </summary>
        public int CurrencyType
        {
            set { _currencytype = value; }
            get { return _currencytype; }
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal CurrencyRate
        {
            set { _currencyrate = value; }
            get { return _currencyrate; }
        }


        /// <summary>
        /// 往来客户主键
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 往来客户来源表
        /// </summary>
        public string FromTBName
        {
            set { _fromtbname = value; }
            get { return _fromtbname; }
        }
        /// <summary>
        /// 往来客户来源表名称字段
        /// </summary>
        public string FileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 收付款类型：1为收款2为付款
        /// </summary>
        public string Accountsorcope
        {
            set { _Accountsorcope = value; }
            get { return _Accountsorcope; }
        }
        #endregion Model

        #region Model
        private string _detailid;
        private string _discount;
        private string _distotalprice;
        private string _realtotalprice;
        private string _fromlineno;
        private string _detailbillingnum;
        private string _productid;
        private string _productcount;
        private string _price;
        private string _taxrate;
        private string _detailtotalprice;
        private string _DetailRemark;
        public string DetailRemark
        {
            set { _DetailRemark = value; }
            get { return _DetailRemark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Distotalprice
        {
            set { _distotalprice = value; }
            get { return _distotalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Realtotalprice
        {
            set { _realtotalprice = value; }
            get { return _realtotalprice; }
        }
      
        /// <summary>
        /// 
        /// </summary>
        public string FromLineno
        {
            set { _fromlineno = value; }
            get { return _fromlineno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailBillingNum
        {
            set { _detailbillingnum = value; }
            get { return _detailbillingnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Productid
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductCount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetailTotalprice
        {
            set { _detailtotalprice = value; }
            get { return _detailtotalprice; }
        }
        #endregion Model
    }
}
