using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellOrderForeignModel
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _orderno;
        private string _ordercurrency;
        private int _branchid;
        private int _custid;
        private int _userid;
        private DateTime? _deliverydate;
        private DateTime _orderdate;
        private string _remark;
        private string _billstatus;
        private int _creator;
        private DateTime _createdate;
        private string _sellType;
        private string _contractor;
        private int _expectedqty;
        private string _title;
        private string _currency;
        private decimal _orderrate;
        private int _isinquiry;
        private decimal _totalorders;
        private decimal _totalcost;
        private decimal _totalsales;
        private decimal _totalcommission;
        private int _shipments;
        private decimal _backsection;
        private decimal _backcommission;
        private int _confirmor;
        private DateTime _confirmdate;
        private int _invalidor;
        private DateTime _invaliddate;
        private int _laster;
        private DateTime _lastdate;
        private DateTime _orderdateend;
        private DateTime _deliverydateend;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderCurrency
        {
            set { _ordercurrency = value; }
            get { return _ordercurrency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeliveryDate
        {
            set { _deliverydate = value; }
            get { return _deliverydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
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
        /// 
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Contractor
        {
            set { _contractor = value; }
            get { return _contractor; }
        }
        /// <summary>
        ///
        /// </summary>
        public int ExpectedQTY
        {
            set { _expectedqty = value; }
            get { return _expectedqty; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        ///
        /// </summary>        
        public string Currency
        {
            set { _currency = value; }
            get { return _currency; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal OrderRate
        {
            set { _orderrate = value; }
            get { return _orderrate; }
        }
        /// <summary>
        ///
        /// </summary>        
        public int IsInquiry
        {
            set { _isinquiry = value; }
            get { return _isinquiry; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal TotalOrders
        {
            set { _totalorders = value; }
            get { return _totalorders; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal TotalCost
        {
            set { _totalcost = value; }
            get { return _totalcost; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal TotalSales
        {
            set { _totalsales = value; }
            get { return _totalsales; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal TotalCommission
        {
            set { _totalcommission = value; }
            get { return _totalcommission; }
        }
        /// <summary>
        ///
        /// </summary>        
        public int Shipments
        {
            set { _shipments = value; }
            get { return _shipments; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal BackSection
        {
            set { _backsection = value; }
            get { return _backsection; }
        }
        /// <summary>
        ///
        /// </summary>        
        public decimal BackCommission
        {
            set { _backcommission = value; }
            get { return _backcommission; }
        }
        /// <summary>
        ///
        /// </summary>        
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        ///
        /// </summary>        
        public DateTime ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        /// <summary>
        ///
        /// </summary>        
        public int Invalidor
        {
            set { _invalidor = value; }
            get { return _invalidor; }
        }
        /// <summary>
        ///
        /// </summary>        
        public DateTime InvalidDate
        {
            set { _invaliddate = value; }
            get { return _invaliddate; }
        }
        /// <summary>
        ///
        /// </summary>        
        public int Laster
        {
            set { _laster = value; }
            get { return _laster; }
        }
        /// <summary>
        ///
        /// </summary>        
        public DateTime LastDate
        {
            set { _lastdate = value; }
            get { return _lastdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderDateEnd
        {
            set { _orderdateend = value; }
            get { return _orderdateend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DeliveryDateEnd
        {
            set { _deliverydateend = value; }
            get { return _deliverydateend; }
        }
        #endregion Model


        #region Detail Model
        private int _did;
        private string _dcompanycd;
        private string _dproductid;
        private string _dbatchno;
        private string _dordercount;
        private string _dpricetype;
        private string _dcostprice;
        private string _dtotalcost;
        private string _dsalesprice;
        private string _dsaletotal;
        private string _ddifference;
        private string _dsurfacee;
        private string _dratio;
        private string _dnumbershipments;
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
        public string DProductID
        {
            set { _dproductid = value; }
            get { return _dproductid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DBatchNo
        {
            set { _dbatchno = value; }
            get { return _dbatchno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DOrderCount
        {
            set { _dordercount = value; }
            get { return _dordercount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DPriceType
        {
            set { _dpricetype = value; }
            get { return _dpricetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DSurface
        {
            set { _dsurfacee = value; }
            get { return _dsurfacee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DCostPrice
        {
            set { _dcostprice = value; }
            get { return _dcostprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DTotalCost
        {
            set { _dtotalcost = value; }
            get { return _dtotalcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DSalesPrice
        {
            set { _dsalesprice = value; }
            get { return _dsalesprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DSaleTotal
        {
            set { _dsaletotal = value; }
            get { return _dsaletotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DDifference
        {
            set { _ddifference = value; }
            get { return _ddifference; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DRatio
        {
            set { _dratio = value; }
            get { return _dratio; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DNumberShipments
        {
            set { _dnumbershipments = value; }
            get { return _dnumbershipments; }
        }
        #endregion Model
    }
}
