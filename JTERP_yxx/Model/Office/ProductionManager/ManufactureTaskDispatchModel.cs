using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.ProductionManager
{
    public class ManufactureTaskDispatchModel
    {
        #region 生产派工表
        private int _id;
        private int? _closer;
        private DateTime? _closedate;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;
        private string _companycd;
        private string _taskno;
        private string _billstatus;
        private int _creator;
        private DateTime _createdate;
        private int _confirmor;
        private DateTime? _confirmdate;
        /// <summary>
        /// 自增
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 结单人
        /// </summary>
        public int? Closer
        {
            set { _closer = value; }
            get { return _closer; }
        }
        /// <summary>
        /// 结单时间
        /// </summary>
        public DateTime? CloseDate
        {
            set { _closedate = value; }
            get { return _closedate; }
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
        /// 最后更新时间
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新人id
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
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
        /// 任务单编码
        /// </summary>
        public string TaskNo
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
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
        /// 制单时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
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
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmDate
        {
            set { _confirmdate = value; }
            get { return _confirmdate; }
        }
        #endregion Model
         
        #region 生产派工明细
        private int _ids;
        private string _cdepcode;
        private string _cdepname;
        private int _chargeman;
        private string _timeunit;
        private decimal? _pretime;
        private decimal? _worktime;
        private decimal? _totaltime;
        private string _remarks;
        private string _companycds;
        private int? _routeid;
        private string _routedes;
        private int? _sequno;
        private string _sequdes;
        private DateTime? _startdate;
        private DateTime? _enddate;
        private int? _TaskID;
        private int? _ManuTaskDetilID;
        /// <summary>
        /// 自增
        /// </summary>
        public int IDs
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 隶属部门编码
        /// </summary>
        public string cdepcode
        {
            set { _cdepcode = value; }
            get { return _cdepcode; }
        }
        /// <summary>
        /// 隶属部门
        /// </summary>
        public string cdepname
        {
            set { _cdepname = value; }
            get { return _cdepname; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public int chargeman
        {
            set { _chargeman = value; }
            get { return _chargeman; }
        }
        /// <summary>
        /// 时间计量单位(小时、天等)
        /// </summary>
        public string Timeunit
        {
            set { _timeunit = value; }
            get { return _timeunit; }
        }
        /// <summary>
        /// 准备工时
        /// </summary>
        public decimal? PreTime
        {
            set { _pretime = value; }
            get { return _pretime; }
        }
        /// <summary>
        /// 作业时间
        /// </summary>
        public decimal? Worktime
        {
            set { _worktime = value; }
            get { return _worktime; }
        }
        /// <summary>
        /// 总作业时间
        /// </summary>
        public decimal? Totaltime
        {
            set { _totaltime = value; }
            get { return _totaltime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 公司编码
        /// </summary>
        public string CompanyCDs
        {
            set { _companycds = value; }
            get { return _companycds; }
        }
   
        /// <summary>
        /// 工艺路线ID
        /// </summary>
        public int? RouteID
        {
            set { _routeid = value; }
            get { return _routeid; }
        }
        /// <summary>
        /// 工艺说明
        /// </summary>
        public string RouteDes
        {
            set { _routedes = value; }
            get { return _routedes; }
        }
        /// <summary>
        /// 工序代码
        /// </summary>
        public int? SequNo
        {
            set { _sequno = value; }
            get { return _sequno; }
        }
        /// <summary>
        /// 工序说明
        /// </summary>
        public string sequDes
        {
            set { _sequdes = value; }
            get { return _sequdes; }
        }
        /// <summary>
        /// 计划开工时间
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 计划完工时间
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 派工单id
        /// </summary>
        public int? TaskID
        {
            set { _TaskID = value; }
            get { return _TaskID; }
        }

        /// <summary>
        /// 任务单明细id
        /// </summary>
        public int? ManuTaskDetilID
        {
            set { _ManuTaskDetilID = value; }
            get { return _ManuTaskDetilID; }
        }
        #endregion Model
    }
}
