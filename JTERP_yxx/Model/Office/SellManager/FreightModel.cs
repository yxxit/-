/**********************************************
 * 类作用：   运费明细Model
 * 建立人：   陈鑫铎
 * 建立时间： 2010/11/18
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class FreightModel
    {
        private int _id;                    //自动编号
        private string _invNo;                    //编号
        private string _companycd;          //公司代码
        private int _creator;               //制单人
        private DateTime _createdate;       //制单日期
        private string _txtTopics;          //主题
        private DateTime _txtBackDate;        //日期
        private string _txtOutOf;           //运出港
        private string _txtObjective;        //目的港
        private string _txtShipCompanies;   //船公司
        private string _txtLumpFee;         //包干费（人民币）
        private string _txtLumpFeeM;        //包干费（美元）
        private string _txtSeaFreight;      //海运费
        private string _txtExchangeRate;    //美元汇率
        private string _cboContainer;       //集装箱类型
        private string _remark;              //备注
        private string _weightLimit;         //限重
        private string _volumeLimit;         //限体积
        private string _txtBackDateStart;        //开始日期
        private string _txtBackDateEnd;        //结束日期

        /// <summary>
        /// 开始日期
        /// </summary>
        public string TxtBackDateStart
        {
            set { _txtBackDateStart = value; }
            get { return _txtBackDateStart; }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string TxtBackDateEnd
        {
            set { _txtBackDateEnd = value; }
            get { return _txtBackDateEnd; }
        }

        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 限重
        /// </summary>
        public string WeightLimit
        {
            set { _weightLimit = value; }
            get { return _weightLimit; }
        }
        /// <summary>
        /// 限体积
        /// </summary>
        public string VolumeLimit
        {
            set { _volumeLimit = value; }
            get { return _volumeLimit; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string InvNo
        {
            set { _invNo = value; }
            get { return _invNo; }
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
        /// 主题
        /// </summary>
        public string TxtTopics
        {
            set { _txtTopics = value; }
            get { return _txtTopics; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime TxtBackDate
        {
            set { _txtBackDate = value; }
            get { return _txtBackDate; }
        }

        /// <summary>
        /// 运出港
        /// </summary>
        public string TxtOutOf
        {
            set { _txtOutOf = value; }
            get { return _txtOutOf; }
        }
        /// <summary>
        /// 目的港
        /// </summary>
        public string TxtObjective
        {
            set { _txtObjective = value; }
            get { return _txtObjective; }
        }
        /// <summary>
        /// 船公司
        /// </summary>
        public string TxtShipCompanies
        {
            set { _txtShipCompanies = value; }
            get { return _txtShipCompanies; }
        }
        /// <summary>
        /// 包干费（人民币）
        /// </summary>
        public string TxtLumpFee
        {
            set { _txtLumpFee = value; }
            get { return _txtLumpFee; }
        }
        /// <summary>
        ///包干费（美元）
        /// </summary>
        public string TxtLumpFeeM
        {
            set { _txtLumpFeeM = value; }
            get { return _txtLumpFeeM; }
        }

        /// <summary>
        /// 海运费
        /// </summary>
        public string TxtSeaFreight
        {
            set { _txtSeaFreight = value; }
            get { return _txtSeaFreight; }
        }
        /// <summary>
        /// 美元汇率
        /// </summary>
        public string TxtExchangeRate
        {
            set { _txtExchangeRate = value; }
            get { return _txtExchangeRate; }
        }
        /// <summary>
        /// 集装箱类型
        /// </summary>
        public string CboContainer
        {
            set { _cboContainer = value; }
            get { return _cboContainer; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
