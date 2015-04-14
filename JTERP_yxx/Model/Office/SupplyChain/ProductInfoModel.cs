using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
 public class ProductInfoModel
    {
        public ProductInfoModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _prodno;
        private string _oldno;
        private string _pyshort;
        private string _productname;
        private string _shortnam;
        private string _barcode;
        private string _typeid;
        private string _bigtype;
        private string _gradeid;
        private string _source;
        private string _unitid;
        private string _brand;
        private string _specification;
        private string _colorid;
        private string _size;
        private string _density;
        private string _pieceCount;
        private string _stockis;
        private string _abctype;
        private string _remark;
        private string _creator;
        private string _createdate;
        private string _checkstatus;
        private string _checkuser;
        private string _checkdate;
        private string _usedstatus;
        private string _modifieddate;
        private string _modifieduserid;
        private string _fromaddr;
        private string _drawingnum;
        private string _imgurl;
        private string _fileno;
        private string _pricepolicy;
        private string _params;
        private string _questions;
        private string _replacename;
        private string _description;
        private string _minusis;
        private string _storageid;
        private string _safestocknum;
        private string _staystandard;
        private string _minstocknum;
        private string _maxstocknum;
        private string _calcpriceways;
        private string _standardcost;
        private string _plancost;
        private string _standardsell;
        private string _sellmin;
        private string _sellmax;
        private string _taxrate;
        private string _intaxrate;
        private string _selltax;
        private string _sellprice;
        private string _pageprice;
        private string _transfreprice;
        private string _oldstandardsell;
        private string _discount;
        private string _standardbuy;
        private string _taxbuy;
        private string _buymax;
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
        private string _extfield11;
        private string _extfield12;
        private string _extfield13;
        private string _extfield14;
        private string _extfield15;
        private string _extfield16;
        private string _extfield17;
        private string _extfield18;
        private string _extfield19;
        private string _extfield20;
        private string _extfield21;
        private string _extfield22;
        private string _extfield23;
        private string _extfield24;
        private string _extfield25;
        private string _extfield26;
        private string _extfield27;
        private string _extfield28;
        private string _extfield29;
        private string _extfield30;
        private string _Manufacturer;
        private string _Material;
        private string _StartStorage;
        private string _IsBatchNo;
        private string _StorageUnit;
        private string _SellUnit;
        private string _PurchseUnit;
        private string _ProductUnit;
        private string _GroupNo;
        private string _MaxImportPrice;
        private string _MinSalePrice;
        private string _bsale;
        private string _bpurchase;
        private string _bconsume;
        private string _baccessary;
        private string _bself;
        private string _bproducing;
        private string _bservice;
        private string _Pnumber;
        private string _AbrasionResist;
        private string _BalancePaper;
        private string _BaseMaterial;
        private string _SurfaceTreatment;
        private string _BackBottomPlate;
        private string _BuckleType;
        private string _Pnumberid;
        private string _AbrasionResistid;
        private string _BalancePaperid;
        private string _BaseMaterialid;
        private string _casregi;
        private string _Qualitystandard;
        private string _Capability;
        private DateTime?_Validity;
        private DateTime?_MedFileDate;
        private DateTime?_MedCheckDate;
        private string _MedCheckNo;//药品检验报告编号
        private int _SearchArea;//1109 添加检索范围变量 edit by dyg   

        #region   20140401新增煤炭字段
        public string HeatPower { set; get; }
        public string VolaPercent { set; get; }
        public string AshPercent { set; get; }
        public string SulfurPercent { set; get; }
        public string WaterPercent { set; get; }
        public string CarbonPercent { set; get; }

        #endregion

        public int SearchArea  // 1109 新增检索范围属性
        {
            set { _SearchArea = value; }
            get { return _SearchArea; }
        }


        private string _engname;

        public string Pnumberid
        {
            set { _Pnumberid = value; }
            get { return _Pnumberid; }
        }
        public string AbrasionResistid
        {
            set { _AbrasionResistid = value; }
            get { return _AbrasionResistid; }
        }
        public string BalancePaperid
        {
            set { _BalancePaperid = value; }
            get { return _BalancePaperid; }
        }
        public string BaseMaterialid
        {
            set { _BaseMaterialid = value; }
            get { return _BaseMaterialid; }
        }

        public string Pnumber
        {
            set { _Pnumber = value; }
            get { return _Pnumber; }
        }
        public string AbrasionResist
        {
            set { _AbrasionResist = value; }
            get { return _AbrasionResist; }
        }
        public string BalancePaper
        {
            set { _BalancePaper = value; }
            get { return _BalancePaper; }
        }
        public string BaseMaterial
        {
            set { _BaseMaterial = value; }
            get { return _BaseMaterial; }
        }
        public string SurfaceTreatment
        {
            set { _SurfaceTreatment = value; }
            get { return _SurfaceTreatment; }
        }
        public string BackBottomPlate
        {
            set { _BackBottomPlate = value; }
            get { return _BackBottomPlate; }
        }
        public string BuckleType
        {
            set { _BuckleType = value; }
            get { return _BuckleType; }
        }
        public string MaxImportPrice
        {
            set { _MaxImportPrice = value; }
            get { return _MaxImportPrice; }
        }
        public string MinSalePrice
        {
            set { _MinSalePrice = value; }
            get { return _MinSalePrice; }
        }
        public string bsale
        {
            set { _bsale = value; }
            get { return _bsale; }
        }
        public string bpurchase
        {
            set { _bpurchase = value; }
            get { return _bpurchase; }
        }
        public string bconsume
        {
            set { _bconsume = value; }
            get { return _bconsume; }
        }
        public string baccessary
        {
            set { _baccessary = value; }
            get { return _baccessary; }
        }
        public string bself
        {
            set { _bself = value; }
            get { return _bself; }
        }
        public string bproducing
        {
            set { _bproducing = value; }
            get { return _bproducing; }
        }
        public string bservice
        {
            set { _bservice = value; }
            get { return _bservice; }
        }
        public string GroupNo
        {
            set { _GroupNo = value; }
            get { return _GroupNo; }
        }
        public string ProductUnit
        {
            set { _ProductUnit = value; }
            get { return _ProductUnit; }
        }
        public string PurchseUnit
        {
            set { _PurchseUnit = value ; }
            get { return _PurchseUnit; }
        }
        public string SellUnit
        {
            set { _SellUnit = value; }
            get { return _SellUnit; }
        }
        public string StorageUnit
        {
            set { _StorageUnit = value; }
            get { return _StorageUnit; }
        }
        public string IsBatchNo
        {
            set { _IsBatchNo = value; }
            get { return _IsBatchNo; }
        }
        public string StartStorage
        {
            set { _StartStorage = value; }
            get { return _StartStorage; }
        }
        private string _EndStorage;
        public string EndStorage
        {
            set { _EndStorage = value; }
            get { return _EndStorage; }
        }
        
        public string Material
        {
            set { _Material = value; }
            get { return _Material; }
        }
        public string Manufacturer
        {
            set { _Manufacturer = value; }
            get { return _Manufacturer; }
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
        public string ProdNo
        {
            set { _prodno = value; }
            get { return _prodno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldNo
        {
            set { _oldno = value; }
            get { return _oldno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PYShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShortNam
        {
            set { _shortnam = value; }
            get { return _shortnam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BarCode
        {
            set { _barcode = value; }
            get { return _barcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BigType
        {
            set { _bigtype = value; }
            get { return _bigtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Source
        {
            set { _source = value; }
            get { return _source; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Brand
        {
            set { _brand = value; }
            get { return _brand; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ColorID
        {
            set { _colorid = value; }
            get { return _colorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Size
        {
            set { _size = value; }
            get { return _size; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Density
        {
            set { _density = value; }
            get { return _density; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PieceCount
        {
            set { _pieceCount = value; }
            get { return _pieceCount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StockIs
        {
            set { _stockis = value; }
            get { return _stockis; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ABCType
        {
            set { _abctype = value; }
            get { return _abctype; }
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
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckStatus
        {
            set { _checkstatus = value; }
            get { return _checkstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckUser
        {
            set { _checkuser = value; }
            get { return _checkuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
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
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FromAddr
        {
            set { _fromaddr = value; }
            get { return _fromaddr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DrawingNum
        {
            set { _drawingnum = value; }
            get { return _drawingnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FileNo
        {
            set { _fileno = value; }
            get { return _fileno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PricePolicy
        {
            set { _pricepolicy = value; }
            get { return _pricepolicy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Params
        {
            set { _params = value; }
            get { return _params; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Questions
        {
            set { _questions = value; }
            get { return _questions; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplaceName
        {
            set { _replacename = value; }
            get { return _replacename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MinusIs
        {
            set { _minusis = value; }
            get { return _minusis; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StorageID
        {
            set { _storageid = value; }
            get { return _storageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SafeStockNum
        {
            set { _safestocknum = value; }
            get { return _safestocknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StayStandard
        {
            set { _staystandard = value; }
            get { return _staystandard; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MinStockNum
        {
            set { _minstocknum = value; }
            get { return _minstocknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaxStockNum
        {
            set { _maxstocknum = value; }
            get { return _maxstocknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CalcPriceWays
        {
            set { _calcpriceways = value; }
            get { return _calcpriceways; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StandardCost
        {
            set { _standardcost = value; }
            get { return _standardcost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PlanCost
        {
            set { _plancost = value; }
            get { return _plancost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StandardSell
        {
            set { _standardsell = value; }
            get { return _standardsell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellMin
        {
            set { _sellmin = value; }
            get { return _sellmin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellMax
        {
            set { _sellmax = value; }
            get { return _sellmax; }
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
        public string InTaxRate
        {
            set { _intaxrate = value; }
            get { return _intaxrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellTax
        {
            set { _selltax = value; }
            get { return _selltax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PagePrice
        {
            set { _pageprice = value; }
            get { return _pageprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TransferPrice
        {
            set { _transfreprice = value; }
            get { return _transfreprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldStandardSell
        {
            set { _oldstandardsell = value; }
            get { return _oldstandardsell; }
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
        public string StandardBuy
        {
            set { _standardbuy = value; }
            get { return _standardbuy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TaxBuy
        {
            set { _taxbuy = value; }
            get { return _taxbuy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuyMax
        {
            set { _buymax = value; }
            get { return _buymax; }
        }
        /// <summary>
        /// 
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
        /// <summary>
        /// 
        /// </summary>
        public string ExtField11
        {
            set { _extfield11 = value; }
            get { return _extfield11; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField12
        {
            set { _extfield12 = value; }
            get { return _extfield12; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField13
        {
            set { _extfield13 = value; }
            get { return _extfield13; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField14
        {
            set { _extfield14 = value; }
            get { return _extfield14; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField15
        {
            set { _extfield15 = value; }
            get { return _extfield15; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField16
        {
            set { _extfield16 = value; }
            get { return _extfield16; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField17
        {
            set { _extfield17 = value; }
            get { return _extfield17; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField18
        {
            set { _extfield18 = value; }
            get { return _extfield18; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField19
        {
            set { _extfield19 = value; }
            get { return _extfield19; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField20
        {
            set { _extfield20 = value; }
            get { return _extfield20; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField21
        {
            set { _extfield21 = value; }
            get { return _extfield21; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField22
        {
            set { _extfield22 = value; }
            get { return _extfield22; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField23
        {
            set { _extfield23 = value; }
            get { return _extfield23; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField24
        {
            set { _extfield24 = value; }
            get { return _extfield24; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField25
        {
            set { _extfield25 = value; }
            get { return _extfield25; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField26
        {
            set { _extfield26 = value; }
            get { return _extfield26; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField27
        {
            set { _extfield27 = value; }
            get { return _extfield27; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField28
        {
            set { _extfield28 = value; }
            get { return _extfield28; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField29
        {
            set { _extfield29 = value; }
            get { return _extfield29; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExtField30
        {
            set { _extfield30 = value; }
            get { return _extfield30; }
        }

        public string CASRegi
        {
            set { _casregi = value; }
            get { return _casregi; }
        }

        public string EngName
        {
            set { _engname = value; }
            get { return _engname; }
        }
        public string Qualitystandard
        {
            set { _Qualitystandard= value; }
            get { return _Qualitystandard; }
        }
        public string Capability
        {
            set { _Capability = value; }
            get { return _Capability; }
        }
        public DateTime? Validity
        {
            set { _Validity = value; }
            get { return _Validity; }
        }
        public DateTime? MedFileDate
        {
            set { _MedFileDate = value; }
            get { return _MedFileDate; }
        }
        public DateTime? MedCheckDate
        {
            set { _MedCheckDate = value; }
            get { return _MedCheckDate; }
        }
        /// <summary>
        /// 药品检验报告编号
        /// </summary>
        public string MedCheckNo
        {
            get { return _MedCheckNo; }
            set { _MedCheckNo = value; }
        }

        #endregion Model

    }
}
