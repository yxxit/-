using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureDispatchReportModel
    {
        public ManufactureDispatchReportModel() { }

        #region 生产进度汇报 Model

        private int _id;
        private string _companyCD;
        private string _reportNO;
        private int _deptID;
        private int _reporter;
        private DateTime _reportDate;
        private string _remark;
        private int _confirmor;
        private DateTime _confirmDate;
        private int _creator;
        private DateTime _createDate;
        private DateTime _modifiedDate;
        private int _billStatus;
        private string _modifiedUserID;

        /// <summary>
        /// 自动增长
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// 汇报编号
        /// </summary>
        public string ReportNO
        {
            set { _reportNO = value; }
            get { return _reportNO; }
        }
        /// <summary>
        /// 汇报部门ID
        /// </summary>
        public int DeptID
        {
            set { _deptID = value; }
            get { return _deptID; }
        }
        /// <summary>
        /// 汇报人
        /// </summary>
        public int Reporter
        {
            set { _reporter = value; }
            get { return _reporter; }
        }
        /// <summary>
        /// 汇报日期
        /// </summary>
        public DateTime ReportDate
        {
            set { _reportDate = value; }
            get { return _reportDate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 确认人
        /// </summary>
        public int Confirmor
        {
            set { _confirmor = value; }
            get { return _confirmor; }
        }
        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime ConfirmDate
        {
            set { _confirmDate = value; }
            get { return _confirmDate; }
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
            set { _createDate = value; }
            get { return _createDate; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifiedDate = value; }
            get { return _modifiedDate; }
        }
        /// <summary>
        /// 单据状态(1制单，2确认)
        /// </summary>
        public int BillStatus
        {
            set { _billStatus = value; }
            get { return _billStatus; }
        }
        /// <summary>
        /// 最后更新用户ID（对应操作用户表的UserID）
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifiedUserID = value; }
            get { return _modifiedUserID; }
        }

        #endregion

        #region 生产进度汇报明细 Model
        private string _detID;
        private string _detReportNo;
        private string _detMorderNo;
        private string _detWorkTime;
        private string _detFinishNum;
        private string _detPassNum;
        private string _detPassPercent;
        private string _detFromBillNo;
        private string _detFromBillID;
        private string _detFromLineNo;
        private string _detRealStartDate;
        private string  _detEndDate;
        private string _detOperator;
        private string _detRemark;

        /// <summary>
        /// 汇报详情ID
        /// </summary>
        public string DetID
        {
            set { _detID = value; }
            get { return _detID; }
        }
        /// <summary>
        /// 汇报单编号
        /// </summary>
        public string DetReportNo
        {
            set { _detReportNo = value; }
            get { return _detReportNo; }
        }
        /// <summary>
        /// 派工单编号
        /// </summary>
        public string DetMorderNo
        {
            set { _detMorderNo = value; }
            get { return _detMorderNo; }
        }
        /// <summary>
        /// 实际工时
        /// </summary>
        public string DetWorkTime
        {
            set { _detWorkTime = value; }
            get { return _detWorkTime; }
        }
        /// <summary>
        /// 本时段完成数
        /// </summary>
        public string DetFinishNum
        {
            set { _detFinishNum = value; }
            get { return _detFinishNum; }
        }
        /// <summary>
        /// 合格数
        /// </summary>
        public string DetPassNum
        {
            set { _detPassNum = value; }
            get { return _detPassNum; }
        }
        /// <summary>
        /// 合格率
        /// </summary>
        public string DetPassPercent
        {
            set { _detPassPercent = value; }
            get { return _detPassPercent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetFromBillNo
        {
            set { _detFromBillNo = value; }
            get { return _detFromBillNo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetFromBillID
        {
            set { _detFromBillID = value; }
            get { return _detFromBillID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DetFromLineNo
        {
            set { _detFromLineNo = value; }
            get { return _detFromLineNo; }
        }
        /// <summary>
        /// 实际开工日期
        /// </summary>
        public string DetRealStartDate
        {
            set { _detRealStartDate = value; }
            get { return _detRealStartDate; }
        }
        /// <summary>
        /// 实际完工日期
        /// </summary>
        public string DetEndDate
        {
            set { _detEndDate = value; }
            get { return _detEndDate; }
        }
        /// <summary>
        /// 操作人员
        /// </summary>
        public string DetOperator
        {
            set { _detOperator = value; }
            get { return _detOperator; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string DetRemark
        {
            set { _detRemark = value; }
            get { return _detRemark; }
        }
        #endregion
    }
}
