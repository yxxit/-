using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    public class SteelInfo
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _steeltype;
        private string _steelname;
        private string _format;
        private int _steelmaterial;
        private decimal _length;
        private decimal _thickness;
        private decimal _width;
        private decimal _radius;
        private decimal _density;
        private decimal _convermodulus;
        private int _steelorigin;
        private decimal _maxstock;
        private decimal _minstock;
        private decimal _safestock;
        private int _creator;
        private DateTime _createdate;
        private string _usedstatus;
        private decimal _purchasebyton;
        private decimal _sellbyton;
        private decimal _purchasemaxbyton;
        private decimal _sellminbyton;
        private decimal _purchasebycount;
        private decimal _sellbycount;
        private decimal _purchasemaxbycount;
        private decimal _sellminbycount;
        private string _remark;
        private decimal _maxcountstock;
        private decimal _maxweightstock;
        private decimal _mincountstock;
        private decimal _minweightstock;
        private decimal _safecountstock;
        private decimal _safeweightstock;
        private string _ismaxton;
        private string _isminton;
        private string _ismaxcount;
        private string _ismincount;
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
        public int SteelType
        {
            set { _steeltype = value; }
            get { return _steeltype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SteelName
        {
            set { _steelname = value; }
            get { return _steelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Format
        {
            set { _format = value; }
            get { return _format; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SteelMaterial
        {
            set { _steelmaterial = value; }
            get { return _steelmaterial; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Length
        {
            set { _length = value; }
            get { return _length; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Thickness
        {
            set { _thickness = value; }
            get { return _thickness; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Width
        {
            set { _width = value; }
            get { return _width; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Radius
        {
            set { _radius = value; }
            get { return _radius; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Density
        {
            set { _density = value; }
            get { return _density; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ConverModulus
        {
            set { _convermodulus = value; }
            get { return _convermodulus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SteelOrigin
        {
            set { _steelorigin = value; }
            get { return _steelorigin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MaxStock
        {
            set { _maxstock = value; }
            get { return _maxstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MinStock
        {
            set { _minstock = value; }
            get { return _minstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SafeStock
        {
            set { _safestock = value; }
            get { return _safestock; }
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
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseByTon
        {
            set { _purchasebyton = value; }
            get { return _purchasebyton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SellByTon
        {
            set { _sellbyton = value; }
            get { return _sellbyton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseMaxByTon
        {
            set { _purchasemaxbyton = value; }
            get { return _purchasemaxbyton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SellMinByTon
        {
            set { _sellminbyton = value; }
            get { return _sellminbyton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseByCount
        {
            set { _purchasebycount = value; }
            get { return _purchasebycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SellByCount
        {
            set { _sellbycount = value; }
            get { return _sellbycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PurchaseMaxByCount
        {
            set { _purchasemaxbycount = value; }
            get { return _purchasemaxbycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SellMinByCount
        {
            set { _sellminbycount = value; }
            get { return _sellminbycount; }
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
        public decimal MaxCountStock
        {
            set { _maxcountstock = value; }
            get { return _maxcountstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MaxWeightStock
        {
            set { _maxweightstock = value; }
            get { return _maxweightstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MinCountStock
        {
            set { _mincountstock = value; }
            get { return _mincountstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal MinWeightStock
        {
            set { _minweightstock = value; }
            get { return _minweightstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SafeCountStock
        {
            set { _safecountstock = value; }
            get { return _safecountstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SafeWeightStock
        {
            set { _safeweightstock = value; }
            get { return _safeweightstock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsMaxTon
        {
            set { _ismaxton = value; }
            get { return _ismaxton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsMinTon
        {
            set { _isminton = value; }
            get { return _isminton; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsMaxCount
        {
            set { _ismaxcount = value; }
            get { return _ismaxcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsMinCount
        {
            set { _ismincount = value; }
            get { return _ismincount; }
        }
        #endregion Model
    }
}
