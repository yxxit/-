/*******************************************
 * 创建人：宋凯歌
 * 创建日期：2010-11-20
 * 描述：销售出库明细model
 * ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellOutStorageForeignDetailModel
    {

        #region Model
        private int _id;
        private string _companycd;
        private string _outno;
        private int? _productid;
        private string _batchno;
        private decimal _detailcount;
        private decimal _detailprice;
        private decimal _detailtotalprice;
        private decimal _backcount;
        private int? _fromdetailid;
        private int? _promotionID;
        private int? _pricetype;
        private decimal _costprice;
        private decimal _salesprice;
        private decimal _difference;
        private decimal _ratio;
        private decimal _declarationnumber;
        private decimal _declarationprice;
        private decimal _shipments;

        /// <summary>
        /// ID,自动生成
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
        /// 商品ID
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string BatchNo
        {
            set { _batchno = value; }
            get { return _batchno; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal DetailCount
        {
            set { _detailcount = value; }
            get { return _detailcount; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DetailPrice
        {
            set { _detailprice = value; }
            get { return _detailprice; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal DetailTotalPrice
        {
            set { _detailtotalprice = value; }
            get { return _detailtotalprice; }
        }
        /// <summary>
        /// 退货数量
        /// </summary>
        public decimal BackCount
        {
            set { _backcount = value; }
            get { return _backcount; }
        }
        /// <summary>
        /// 源单明细ID
        /// </summary>
        public int? FromDetailID
        {
            set { _fromdetailid = value; }
            get { return _fromdetailid; }
        }
        /// <summary>
        /// 促销方案ID
        /// </summary>
        public int? PromotionID
        {
            set { _promotionID = value; }
            get { return _promotionID; }
        }
        /// <summary>
        /// 价格类别
        /// </summary>
        public int? PriceType
        {
            set { _pricetype = value; }
            get { return _pricetype; }
        }
        /// <summary>
        /// 比例
        /// </summary>
        public decimal Ratio
        {
            set { _ratio = value; }
            get { return _ratio; }
        }
        /// <summary>
        /// 报关数量
        /// </summary>
        public decimal DeclarationNumber
        {
            set { _declarationnumber = value; }
            get { return _declarationnumber; }
        }
        /// <summary>
        /// 出货数量
        /// </summary>
        public decimal Shipments
        {
            set { _shipments = value; }
            get { return _shipments; }
        }
        /// <summary>
        /// 成本价格
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal DeclarationPrice
        {
            set { _declarationprice = value; }
            get { return _declarationprice; }
        }
        /// <summary>
        /// 报关价格
        /// </summary>
        public decimal SalesPrice
        {
            set { _salesprice = value; }
            get { return _salesprice; }
        }
        /// <summary>
        /// 差额 
        /// </summary>
        public decimal Difference
        {
            set { _difference = value; }
            get { return _difference; }
        }
        #endregion Model
    }
}
