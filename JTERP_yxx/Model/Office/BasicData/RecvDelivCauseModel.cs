using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    public class RecvDelivCauseModel
    {
        public RecvDelivCauseModel()
        {
        }
        #region 收发类别Model
        private int _id;
        private string _companyCD;
        private string _cause;
        private string _way;
        private string _remark;

        /// <summary>
        /// 自动增长
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companyCD = value; }
            get { return _companyCD; }
        }
        /// <summary>
        /// 收支原因名称
        /// </summary>
        public string Cause
        {
            set { _cause = value; }
            get { return _cause; }
        }
        /// <summary>
        /// 收发方向：1、收货，2、发货
        /// </summary>
        public string Way
        {
            set { _way = value; }
            get { return _way; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion
    }
}
