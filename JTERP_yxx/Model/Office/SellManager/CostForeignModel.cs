using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class CostForeignModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _costno;
        private string _title;
        private int _branchid;
        private string _containersp;
        private decimal _shippedport;
        private decimal _objectiveport;
        private decimal _totalprice;
        private decimal _fobprice;
        private decimal _cifprice;
        private decimal _ototalprice;
        private decimal _ofobprice;
        private decimal _ocifprice;
        private decimal _totalpriceW;
        private decimal _fobpriceW;
        private decimal _cifpriceW;
        private decimal _ototalpriceW;
        private decimal _ofobpriceW;
        private decimal _ocifpriceW;
        private int _creator;
        private DateTime _createdate;
        private int _laster;
        private DateTime _lastdate;
        private string _productID;
        private string _Container20Info;
        private string _Container40Info;
        private string _Container41Info;
        private string _CostDate;
        private string _Remarks;
        private string _CostDateStart;
        private string _CostDateEnd;

        /// <summary>
        /// 日期开始
        /// </summary>
        public string CostDateStart
        {
            set { _CostDateStart = value; }
            get { return _CostDateStart; }
        }
        /// <summary>
        /// 日期结束
        /// </summary>
        public string CostDateEnd
        {
            set { _CostDateEnd = value; }
            get { return _CostDateEnd; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _Remarks = value; }
            get { return _Remarks; }
        }

        /// <summary>
        /// 日期
        /// </summary>
        public string CostDate
        {
            set { _CostDate = value; }
            get { return _CostDate; }
        }
        /// <summary>
        /// 集装箱20信息
        /// </summary>
        public string Container20Info
        {
            set { _Container20Info = value; }
            get { return _Container20Info; }
        }

        /// <summary>
        /// 集装箱40信息
        /// </summary>
        public string Container40Info
        {
            set { _Container40Info = value; }
            get { return _Container40Info; }
        }

        /// <summary>
        /// 集装箱41信息
        /// </summary>
        public string Container41Info
        {
            set { _Container41Info = value; }
            get { return _Container41Info; }
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
        public string CostNo
        {
            set { _costno = value; }
            get { return _costno; }
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
        public string ProductID
        {
            set { _productID = value; }
            get { return _productID; }
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
        public string ContainerSp
        {
            set { _containersp = value; }
            get { return _containersp; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ShippedPort
        {
            set { _shippedport = value; }
            get { return _shippedport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ObjectivePort
        {
            set { _objectiveport = value; }
            get { return _objectiveport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FOBPrice
        {
            set { _fobprice = value; }
            get { return _fobprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CIFPrice
        {
            set { _cifprice = value; }
            get { return _cifprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OTotalPrice
        {
            set { _ototalprice = value; }
            get { return _ototalprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OFOBPrice
        {
            set { _ofobprice = value; }
            get { return _ofobprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OCIFPrice
        {
            set { _ocifprice = value; }
            get { return _ocifprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPriceW
        {
            set { _totalpriceW = value; }
            get { return _totalpriceW; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FOBPriceW
        {
            set { _fobpriceW = value; }
            get { return _fobpriceW; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CIFPriceW
        {
            set { _cifpriceW = value; }
            get { return _cifpriceW; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OTotalPriceW
        {
            set { _ototalpriceW = value; }
            get { return _ototalpriceW; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OFOBPriceW
        {
            set { _ofobpriceW = value; }
            get { return _ofobpriceW; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OCIFPriceW
        {
            set { _ocifpriceW = value; }
            get { return _ocifpriceW; }
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
        #endregion Model


        #region Detail Model
        private int _did;
        private string _dcompanycd;
        private string _dproductid;
        private string _ddetaicount;
        private string _dtotalcount;
        private string _dsingleweight;
        private string _dtotalweight;
        private string _dwholeprice;
        private string _dtotalprice;
        private string _dprodtype;

        /// <summary>
        /// 外协采购价   
        /// </summary>
        private string _detailTotalPriceW;
        /// <summary>
        /// 备注   
        /// </summary>
        private string _detailRemarks;
        /// <summary>
        /// 明细
        /// </summary>
        private string _detailSpecifications;



        /// <summary>
        /// 物品ID
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
        /// 商品编号
        /// </summary>
        public string DProductID
        {
            set { _dproductid = value; }
            get { return _dproductid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string DDetailCount
        {
            set { _dtotalcount = value; }
            get { return _dtotalcount; }
        }
        /// <summary>
        /// 总数量
        /// </summary>
        public string DTotalCount
        {
            set { _ddetaicount = value; }
            get { return _ddetaicount; }
        }
        /// <summary>
        /// 单重
        /// </summary>
        public string DSingleWeight
        {
            set { _dsingleweight = value; }
            get { return _dsingleweight; }
        }
        /// <summary>
        /// 总重
        /// </summary>
        public string DTotalWeight
        {
            set { _dtotalweight = value; }
            get { return _dtotalweight; }
        }
        /// <summary>
        /// 材料单价
        /// </summary>
        public string DWholePrice
        {
            set { _dwholeprice = value; }
            get { return _dwholeprice; }
        }
        /// <summary>
        /// 材料单价
        /// </summary>
        public string DTotalPrice
        {
            set { _dtotalprice = value; }
            get { return _dtotalprice; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string DProdType
        {
            set { _dprodtype = value; }
            get { return _dprodtype; }
        }
        /// <summary>
        /// 明细
        /// </summary>
        public string DetailSpecifications
        {
            set { _detailSpecifications = value; }
            get { return _detailSpecifications; }
        }

        /// <summary>
        /// 备注   
        /// </summary>
        public string DRemarks
        {
            set { _detailRemarks = value; }
            get { return _detailRemarks; }
        }

        /// <summary>
        /// 外协采购价   
        /// </summary>
        public string DetailTotalPriceW
        {
            set { _detailTotalPriceW = value; }
            get { return _detailTotalPriceW; }
        }

        #endregion Model
    }
}
