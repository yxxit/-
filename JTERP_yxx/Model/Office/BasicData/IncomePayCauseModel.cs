
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    public class IncomePayCauseModel
    {
        #region Model
        private int _id;
        private string _companycd;
        //private int _branchid;
        private string _cause;
        private string _way;
        private string _classify;
        private string _contactunit;
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
        ///// <summary>
        ///// 分店ID
        ///// </summary>
        //public int BranchID
        //{
        //    set { _branchid = value; }
        //    get { return _branchid; }
        //}
        /// <summary>
        /// 收支原因名称
        /// </summary>
        public string Cause
        {
            set { _cause = value; }
            get { return _cause; }
        }
        /// <summary>
        /// 收支方向：1收款，2付款
        /// </summary>
        public string Way
        {
            set { _way = value; }
            get { return _way; }
        }
        /// <summary>
        /// 收支分类：1销售，2采购，3费用
        /// </summary>
        public string classify
        {
            set { _classify = value; }
            get { return _classify; }
        }
        /// <summary>
        /// 往来单位：1供应商，2客户，3供应商与客户，4单个员工，5所有员工，6其他
        /// </summary>
        public string ContactUnit
        {
            set { _contactunit = value; }
            get { return _contactunit; }
        }
        #endregion Model
    }
}
