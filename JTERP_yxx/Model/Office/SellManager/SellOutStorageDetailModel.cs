/*******************************************
 * 创建人：何小武
 * 创建日期：2009-12-11
 * 描述：销售出库明细model
 * ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellOutStorageDetailModel
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


        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private decimal logisticsprice;
        /// <summary>
        /// 物流费
        /// </summary>
        public decimal Logisticsprice
        {
            set { logisticsprice = value; }
            get { return logisticsprice; }
        }
        private int logistic;
        /// <summary>
        /// 物流商id
        /// </summary>
        public int Logistic
        {
            set { logistic = value; }
            get { return logistic; }
        }
        #endregion Model
    }
}
