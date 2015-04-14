using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class StayMaterialModel
    {
        #region Model
        private string _id;
        private string _companycd;
        private string _overtime;
        private string _overtimeend;
        private string _turnover;
        private string _turnoverend;
        private string _busitype;
        private string _productno;
        private string _productname;
        private string _morethantype;
        private string _UsedStatus;
        private string _daycount;
        private string _productcount;
        /// <summary>
        /// 
        /// </summary>
        public string Productcount
        {
            set { _productcount = value; }
            get { return _productcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Daycount
        {
            set { _daycount = value; }
            get { return _daycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ID
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
        /// 起始日期
        /// </summary>
        public string OverTime
        {
            set { _overtime = value; }
            get { return _overtime; }
        }
        /// <summary>
        /// 终止日期
        /// </summary>
        public string OverTimeEnd
        {
            set { _overtimeend = value; }
            get { return _overtimeend; }
        }
        /// <summary>
        /// 起始周转率
        /// </summary>
        public string TurnOver
        {
            set { _turnover = value; }
            get { return _turnover; }
        }
        /// <summary>
        /// 终止周转率
        /// </summary>
        public string TurnOverEnd
        {
            set { _turnoverend = value; }
            get { return _turnoverend; }
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusiType
        {
            set { _busitype = value; }
            get { return _busitype; }
        }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string ProductNo
        {
            set { _productno = value; }
            get { return _productno; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 超储判断
        /// </summary>
        public string MoreThanType
        {
            set { _morethantype = value; }
            get { return _morethantype; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string UsedStatus
        {
            set { _UsedStatus = value; }
            get { return _UsedStatus; }
        }
        #endregion Model
    }
}
