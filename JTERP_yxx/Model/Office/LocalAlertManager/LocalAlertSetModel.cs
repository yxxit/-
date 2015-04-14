using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.LocalAlertManager
{
   public class LocalAlertSetModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _remindtype;
        private string _ismobilenotice;
        private string _mobile;
        private string _remindperiod;
        private string _subperiod;
        private string _remindtime;
        private string _modifieddate;
        private string _modifieduserid;
        /// <summary>
        /// 
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
        /// 提醒类型(1： 库存限量报警;2：客户联络延期告警；3：供应商联络延期告警;4：即将到期的劳动合同；5：办公用品缺货预警；6：费用报销延期预警)
        /// </summary>
        public string RemindType
        {
            set { _remindtype = value; }
            get { return _remindtype; }
        }
        /// <summary>
        /// 是否手机短信提醒（0否，1是）
        /// </summary>
        public string IsMobileNotice
        {
            set { _ismobilenotice = value; }
            get { return _ismobilenotice; }
        }
        /// <summary>
        /// 手机号(多个号码用逗号分开,最多可设4个号码)
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 提醒周期(1:天 2:周 3:月)
        /// </summary>
        public string RemindPeriod
        {
            set { _remindperiod = value; }
            get { return _remindperiod; }
        }
        /// <summary>
        /// 按天：第几天(隔几天发一次)按周：星期几(每周星期几发送一次)按月：第几天(每月的第几天发送一次)
        /// </summary>
        public string SubPeriod
        {
            set { _subperiod = value; }
            get { return _subperiod; }
        }
        /// <summary>
        /// 提醒时间点
        /// </summary>
        public string RemindTime
        {
            set { _remindtime = value; }
            get { return _remindtime; }
        }
 
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public string ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        #endregion Model

    }
}
