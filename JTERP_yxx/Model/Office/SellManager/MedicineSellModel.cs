using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
   public class MedicineSellModel
   {
       #region model
       private int _id;
       private string _companycd;
       private string _orderno;
       //private int? _custid;
       private string _custid;
       private string custno;
       private string cashier;
       private string _expressstyle;
      
       /// <summary>
       /// 快递公司类型
       /// </summary>
       public string Expressstyle
       {
           get { return _expressstyle; }
           set { _expressstyle = value; }
       }
       private string _expressordernum;

       /// <summary>
       /// 快递单号
       /// </summary>
       public string Expressordernum
       {
           get { return _expressordernum; }
           set { _expressordernum = value; }
       }

       public string Cashier
       {
           get { return cashier; }
           set { cashier = value; }
       }

       public string Custno
       {
           get { return custno; }
           set { custno = value; }
       }
       private string _custname;
       // private int? _linkmanid;
       private string _linkmanid;
       private string _linkmanname;
       private string _handset;
       private string _receiveaddress;
       private int _seller;
       private decimal? _totalprice;
       private decimal? _counttotal;
       private DateTime? _orderdate;
       private string _status;
       private string _billstatus;
       private string _sendstatus;
       private string _remark;
       private int _checker;
       private DateTime? _checkdate;
       private int _creator;
       private DateTime? _createdate;
       private string _isselforder;
       private string _tell;
       private string _paytypeuc;
       private string _carrytype;
       private string _area;
       private string _discount;
       private string _discountamount;
       private string _reviewercomments;
       private string _cbustype;
       private string _cstcode;
       private string _cexchname;
       private string _iexchrate;
       private string _saledepartment;
       /// <summary>
       /// 销售部门
       /// 2013-10-04 bsd
       /// </summary>
       public string Saledepartment
       {
           get { return _saledepartment; }
           set { _saledepartment = value; }
       }
       /// <summary>
       /// 汇率
       /// 2013-10-04 bsd
       /// </summary>
       public string Iexchrate
       {
           get { return _iexchrate; }
           set { _iexchrate = value; }
       }
       /// <summary>
       /// 币种
       /// 2013-10-04 bsd
       /// </summary>
       public string Cexchname
       {
           get { return _cexchname; }
           set { _cexchname = value; }
       }

       /// <summary>
       /// 销售类型
       /// 2013-10-04 bsd
       /// </summary>
       public string Cstcode
       {
           get { return _cstcode; }
           set { _cstcode = value; }
       }
       /// <summary>
       /// 业务类型
       /// 2013-10-04
       /// bsd
       /// </summary>
       public string Cbustype
       {
           get { return _cbustype; }
           set { _cbustype = value; }
       }  
       /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 客户ID（关联客户信息表）
        /// </summary>
        //public int? CustID
        public string CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustName
        {
            set { _custname = value; }
            get { return _custname; }
        }

        /// <summary>
        /// 联系人ID（关联联系人信息表）
        /// </summary>
        //public int? LinkManID
        public string LinkManID
        {
            set { _linkmanid = value; }
            get { return _linkmanid; }
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkManName
        {
            set { _linkmanname = value; }
            get { return _linkmanname; }
        }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string HandSet
        {
            set { _handset = value; }
            get { return _handset; }
        }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string ReceiveAddress
        {
            set { _receiveaddress = value; }
            get { return _receiveaddress; }
        }
        /// <summary>
        /// 业务员(对应员工表ID)
        /// </summary>
        public int Seller
        {
            set { _seller = value; }
            get { return _seller; }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        public decimal? TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 数量合计
        /// </summary>
        public decimal? CountTotal
        {
            set { _counttotal = value; }
            get { return _counttotal; }
        }
        /// <summary>
        /// 单据状态（1制单，2确认）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 订单状态（0处理中，1已处理）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 发货状态（0未发货，1已发货）
        /// </summary>
        public string SendStatus
        {
            set { _sendstatus = value; }
            get { return _sendstatus; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime? OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public int Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 审核人日期
        /// </summary>
        public DateTime? CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 是否自己客户下单
        /// </summary>
        public string isSelfOrder
        {
            set { _isselforder = value; }
            get { return _isselforder; }
        }

        ///<summary>
        ///电话
        ///</summary>
        public string Tell
        {
            set { _tell = value; }
            get { return _tell; }
        }

        ///<summary>
        ///开票方式
        ///</summary>
        public string PayTypeUC
        {
            set { _paytypeuc = value; }
            get { return _paytypeuc; }
        }

        ///<summary>
        ///发货方式
        ///</summary>
        public string CarryType
        {
            set { _carrytype = value; }
            get { return _carrytype; }
        }

        ///<summary>
        ///销售地区
        ///</summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }

        ///<summary>
        ///整单折扣
        ///</summary>
        public string discount
        {
            set { _discount = value; }
            get { return _discount; }
        }

        ///<summary>
        ///折扣后金额
        ///</summary>
        public string DiscountAmount
        {
            set { _discountamount = value; }
            get { return _discountamount; }
        }

        ///<summary>
        ///审核人备注
        ///</summary>
        public string ReviewerComments
        {
            set { _reviewercomments = value; }
            get { return _reviewercomments; }
        }


       #endregion

        #region Detail Model
        private int _did;
        private string _dcompanycd;
        private string _dorderno;
        private string _dproductno;
        private string _dremark;
        private decimal _dunitprice;
        private decimal _dtotalprice;
        private decimal _dcount;
        private decimal _dguidprice;//8-25
        private string _limitsellarea;

        private decimal _dsupplyprice;

        private decimal _ddiscountprice;


        /// <summary>
        /// 
        /// </summary>
        public int DID
        {
            set { _did = value; }
            get { return _did; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DCompanyCD
        {
            set { _dcompanycd = value; }
            get { return _dcompanycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOrderNo
        {
            set { _dorderno = value; }
            get { return _dorderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DProductNo
        {
            set { _dproductno = value; }
            get { return _dproductno; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DRemark
        {
            set { _dremark = value; }
            get { return _dremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DUnitPrice
        {
            set { _dunitprice = value; }
            get { return _dunitprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DTotalPrice
        {
            set { _dtotalprice = value; }
            get { return _dtotalprice; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal DCount
        {
            set { _dcount = value; }
            get { return _dcount; }
        }       

        public decimal DGuidPrice
        {
            set { _dguidprice = value; }
            get { return _dguidprice; }
        }
        public decimal DSupplyPrice
        {
            set { _dsupplyprice = value; }
            get { return _dsupplyprice; }
        }
        public decimal DDiscountPrice
        {
            set { _ddiscountprice = value; }
            get { return _ddiscountprice; }
        }
        public string LimitSellArea
        {
            set { _limitsellarea = value; }
            get { return _limitsellarea; }
        }
        #endregion DetailModel
   }
}
