/*******************************************
 * 创建人：何小武
 * 创建日期：2009-12-11
 * 描述：销售出库model
 * ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellOutStorageModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _outno;
        private int? _branchid;
        private int? _custid;
        private int? _userid;
        private int? _storageid;
        private string _contractor;
        private string _contractphone;
        private int? _invoicetype;
        private int? _selltype;
        private DateTime? _selldate;
        private int? _frombillid;
        private string _ariveaddress;
        private string _billstatus;
        private string _remark;
        private decimal _preferprice;
        private decimal _totalcount;
        private decimal _totalprice;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _invalidor;
        private DateTime _invaliddate;
        private string _isBlend;
        private string _FlowStatus;
        private DateTime? _expireDay;
        private string _custname;
        private string _isUsedPromotion;
        private string _extfield1;
        private string _extfield2;
        private string _extfield3;
        private string _extfield4;
        private string _extfield5;
        private string _extfield6;
        private string _extfield7;
        private string _extfield8;
        private string _extfield9;
        private string _extfield10;
        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 出库单编号
        /// </summary>
        public string OutNo
        {
            set { _outno = value; }
            get { return _outno; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 业务员ID
        /// </summary>
        public int? UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int? StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contractor
        {
            set { _contractor = value; }
            get { return _contractor; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContractPhone
        {
            set { _contractphone = value; }
            get { return _contractphone; }
        }
        /// <summary>
        /// 发票类型1 有发票2 无发票
        /// </summary>
        public int? InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 销售类型1 零售 2 批发 
        /// </summary>
        public int? SellType
        {
            set { _selltype = value; }
            get { return _selltype; }
        }
        /// <summary>
        /// 销售日期
        /// </summary>
        public DateTime? SellDate
        {
            set { _selldate = value; }
            get { return _selldate; }
        }
        /// <summary>
        /// 来源销售订单ID
        /// </summary>
        public int? FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string AriveAddress
        {
            set { _ariveaddress = value; }
            get { return _ariveaddress; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }

        public string FlowStatus
        {
            set { _FlowStatus = value; }
            get { return _FlowStatus; }
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
        /// 优惠
        /// </summary>
        public decimal PreferPrice
        {
            set { _preferprice = value; }
            get { return _preferprice; }
        }
        /// <summary>
        /// 数量合计
        /// </summary>
        public decimal TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        /// 作废人
        /// </summary>
        public int Invalidor
        {
            set { _invalidor = value; }
            get { return _invalidor; }
        }
        /// <summary>
        /// 作废日期
        /// </summary>
        public DateTime InvalidDate
        {
            set { _invaliddate = value; }
            get { return _invaliddate; }
        }
        /// <summary>
        /// 核销状态 0未核销，1核销中，2核销完成
        /// </summary>
        public string IsBlend
        {
            set { _isBlend = value; }
            get { return _isBlend; }
        }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime? ExpireDay
        {
            set { _expireDay = value; }
            get { return _expireDay; }
        }
        /// <summary>
        /// 是否使用促销政策（0不使用，1使用）
        /// </summary>
        public string IsUsedPromotion
        {
            set { _isUsedPromotion = value; }
            get { return _isUsedPromotion; }
        }
        /// <summary>
        /// 扩展属性1
        /// </summary>
        public string ExtField1
        {
            set { _extfield1 = value; }
            get { return _extfield1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField2
        {
            set { _extfield2 = value; }
            get { return _extfield2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField3
        {
            set { _extfield3 = value; }
            get { return _extfield3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField4
        {
            set { _extfield4 = value; }
            get { return _extfield4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField5
        {
            set { _extfield5 = value; }
            get { return _extfield5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField6
        {
            set { _extfield6 = value; }
            get { return _extfield6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField7
        {
            set { _extfield7 = value; }
            get { return _extfield7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField8
        {
            set { _extfield8 = value; }
            get { return _extfield8; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField9
        {
            set { _extfield9 = value; }
            get { return _extfield9; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField10
        {
            set { _extfield10 = value; }
            get { return _extfield10; }
        }

        private decimal _ForkliftTruck;
        /// <summary>
        /// 铲车费
        /// </summary>
        public decimal ForkliftTruck
        {
            set { _ForkliftTruck = value; }
            get { return _ForkliftTruck; }
        }
        private int _LogisticsID;
        /// <summary>
        /// 物流商
        /// </summary>
        public int LogisticsID
        {
            set { _LogisticsID = value; }
            get { return _LogisticsID; }
        }
        private decimal _LogisticsPrice;
        /// <summary>
        /// 物流费
        /// </summary>
        public decimal LogisticsPrice
        {
            set { _LogisticsPrice = value; }
            get { return _LogisticsPrice; }
        }
        #endregion Model
        
    }
}
