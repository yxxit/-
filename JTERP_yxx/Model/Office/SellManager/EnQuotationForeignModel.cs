using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class EnQuotationForeignModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _quotano;
        private int _creator;
        private int _custid;
        private DateTime? _baojiadate;
        private string _title;
        private string _sourceno;
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
        public string QuotaNo
        {
            set { _quotano = value; }
            get { return _quotano; }
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

        public string SourceNo
        {
            set { _sourceno = value; }
            get { return _sourceno; }
        }
        #endregion Model

        #region Detail Model
        private int _did;
        private string _dcompanycd;
        private string _dquotano;
        private string _sortno;
        private int _dproductid;
        private decimal _dorderweight;
        private string _dsurfacee;
        private decimal _dfobprice;
        private decimal _dcifprice;
        private string _dxiangxing;
        private decimal _dcount;
        private string _dpicture;
        private string _dremark;
        private string _denproductname;
        private string _dcurrencytype;
        private string _dpaymentterm;
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

        public string DQuotaNo
        {
            set { _dquotano = value; }
            get { return _dquotano; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DSortNo
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
        public decimal DWeight
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
        public string DPicture
        {
            set { _dpicture = value; }
            get { return _dpicture; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DCount
        {
            set { _dcount = value; }
            get { return _dcount; }
        }


        public string DEnProductName
        {
            set { _denproductname = value; }
            get { return _denproductname; }
        }
        public string DCurrencyType
        {
            set { _dcurrencytype = value; }
            get { return _dcurrencytype; }
        }
        public string DPaymentTerm
        {
            set { _dpaymentterm = value; }
            get { return _dpaymentterm; }
        }

        public string DPyshort
        {
            set { _dpyshort = value; }
            get { return _dpyshort; }
        }
        #endregion Model
    }
}
