using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class ChQuotationForeignModel
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _orderno;
        private int _branchid;
        private int _creator;
        private int _custid;
        private DateTime? _baojiadate;
        private string _title;
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
        public int BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
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
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BaoJiaDate
        {
            set { _baojiadate = value; }
            get { return _baojiadate; }
        }
        /// <summary>
        ///
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        #endregion Model


        #region Detail Model
        private int _did;
        private string _dcompanycd;
        private int _dproductid;
        private string _dbatchno;
        private decimal _dorderweight;
        private string _dremark;
        private decimal _dfobprice;
        private decimal _dhuilv;
        private decimal _dcifprice;
        private string _dxiangxing;
        private decimal _dbgprice;
        private string _dsurfacee;
        private decimal _dcount;
        private string _sortno;
        private string _dproductname;
        private string _dpyshort;
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
        public string SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DProductID
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
        public decimal DOrderWeight
        {
            set { _dorderweight = value; }
            get { return _dorderweight; }
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
        public string DSurface
        {
            set { _dsurfacee = value; }
            get { return _dsurfacee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DFOBPrice
        {
            set { _dfobprice = value; }
            get { return _dfobprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DHuiLv
        {
            set { _dhuilv = value; }
            get { return _dhuilv; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DCIFPrice
        {
            set { _dcifprice = value; }
            get { return _dcifprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DXiangXing
        {
            set { _dxiangxing = value; }
            get { return _dxiangxing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DBgPrice
        {
            set { _dbgprice = value; }
            get { return _dbgprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DCount
        {
            set { _dcount= value; }
            get { return _dcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DProductName
        {
            set { _dproductname = value; }
            get { return _dproductname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DPyShort
        {
            set { _dpyshort = value; }
            get { return _dpyshort; }
        }
        #endregion Model
    }
}
