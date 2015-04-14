using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
    /// <summary>
    /// 采购退货主表实体类
    /// </summary>
   public  class PurchaseOutStorageModel
    {
        #region Model
        private  int  _id;
        private string _companycd;
        private string _outno;
        private  string   _branchid;
        private  int   _providerid;
        private  int   _userid;
        private  string   _invoicetype;
        private  int   _frombillid;
        private  string   _outdate;
        private string _remark;
        private  string   _totalcount;
        private  string   _totalprice;
        private  string   _creator;
        private  string   _createdate;
        private  int   _confirmor;
        private  DateTime   _confirmdate;
        private  int   _invalidor;
        private DateTime _invaliddate;
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
        private int _storageid;
        private int _billstatus;
        private string _FlowStatus;
        private int _IsBlend;
        /// <summary>
        /// 核销状态0、	未核销1、	核销中2、	核销完成
        /// </summary>
        public int IsBlend
        {
            set { _IsBlend = value; }
            get { return _IsBlend; }
        }
        /// <summary>
        /// 1 制单2 执行3 作废
        /// </summary>
        public int BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// ID，自动生成

        /// </summary>
        public  int  ID
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
        public string FlowStatus
        {
            set { _FlowStatus = value; }
            get { return _FlowStatus; }
        }
        /// <summary>
        /// 退货单编号
        /// </summary>
        public string OutNo
        {
            set { _outno = value; }
            get { return _outno; }
        }
        /// <summary>
        /// 分店ID
        /// </summary>
        public  string   BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public  int   ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 业务员ID
        /// </summary>
        public  int   UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 发票类型 1 有发票2 无发票 默认无发票

        /// </summary>
        public  string   InvoiceType
        {
            set { _invoicetype = value; }
            get { return _invoicetype; }
        }
        /// <summary>
        /// 源单ID
        /// </summary>
        public  int   FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 退货日期

        /// </summary>
        public  string   OutDate
        {
            set { _outdate = value; }
            get { return _outdate; }
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
        /// 数量合计
        /// </summary>
        public  string   TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public  string   TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 制单人

        /// </summary>
        public  string   Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public  string   CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 确认人

        /// </summary>
        public  int   Confirmor
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
        public  int   Invalidor
        {
            set { _invalidor = value; }
            get { return _invalidor; }
        }
        /// <summary>
        /// 作废日期
        /// </summary>
        public  DateTime    InvalidDate
        {
            set { _invaliddate = value; }
            get { return _invaliddate; }
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
        /// 扩展属性2
        /// </summary>
        public string ExtField2
        {
            set { _extfield2 = value; }
            get { return _extfield2; }
        }
        /// <summary>
        /// 扩展属性3
        /// </summary>
        public string ExtField3
        {
            set { _extfield3 = value; }
            get { return _extfield3; }
        }
        /// <summary>
        /// 扩展属性4
        /// </summary>
        public string ExtField4
        {
            set { _extfield4 = value; }
            get { return _extfield4; }
        }
        /// <summary>
        /// 扩展属性5
        /// </summary>
        public string ExtField5
        {
            set { _extfield5 = value; }
            get { return _extfield5; }
        }
        /// <summary>
        /// 扩展属性6
        /// </summary>
        public string ExtField6
        {
            set { _extfield6 = value; }
            get { return _extfield6; }
        }
        /// <summary>
        /// 扩展属性7
        /// </summary>
        public string ExtField7
        {
            set { _extfield7 = value; }
            get { return _extfield7; }
        }
        /// <summary>
        /// 扩展属性8
        /// </summary>
        public string ExtField8
        {
            set { _extfield8 = value; }
            get { return _extfield8; }
        }
        /// <summary>
        /// 扩展属性9
        /// </summary>
        public string ExtField9
        {
            set { _extfield9 = value; }
            get { return _extfield9; }
        }
        /// <summary>
        /// 扩展属性10
        /// </summary>
        public string ExtField10
        {
            set { _extfield10 = value; }
            get { return _extfield10; }
        }
        #endregion Model



        #region Detail Model
        private string _did;
        private string _dproductid;
        private string _dcompanycd;
        private string _dinno;
        private string _dincount;
        private string _dinprice;
        private string _ddetailntotalprice;
        private string _dbackcount;
        private string _dfromdetailid;
        private string _dBatchNo;
        private string _dDetailID;

        /// <summary>
        /// 明细ID
        /// </summary>
        public string DDetailID
        {
            set { _dDetailID = value; }
            get { return _dDetailID; }
        }
        /// <summary>
        /// 明细批次
        /// </summary>
        public string DBatchNo
        {
            set { _dBatchNo = value; }
            get { return _dBatchNo; }
        }

        /// <summary>
        /// id
        /// </summary>
        public string DID
        {
            set { _did = value; }
            get { return _did; }
        }
        /// <summary>
        /// 物品id
        /// </summary>
        public string DProductID
        {
            set { _dproductid = value; }
            get { return _dproductid; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string DCompanyCD
        {
            set { _dcompanycd = value; }
            get { return _dcompanycd; }
        }
        /// <summary>
        /// 退货单编号
        /// </summary>
        public string DInNo
        {
            set { _dinno = value; }
            get { return _dinno; }
        }
        /// <summary>
        /// 退货数量

        /// </summary>
        public string DInCount
        {
            set { _dincount = value; }
            get { return _dincount; }
        }
        /// <summary>
        /// 退货单价

        /// </summary>
        public string DInPrice
        {
            set { _dinprice = value; }
            get { return _dinprice; }
        }
        /// <summary>
        /// 退货金额

        /// </summary>
        public string DDetailnTotalPrice
        {
            set { _ddetailntotalprice = value; }
            get { return _ddetailntotalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DBackCount
        {
            set { _dbackcount = value; }
            get { return _dbackcount; }
        }
        /// <summary>
        /// 源单明细id
        /// </summary>
        public string DFromDetailID
        {
            set { _dfromdetailid = value; }
            get { return _dfromdetailid; }
        }
        #endregion Model
    }
}
