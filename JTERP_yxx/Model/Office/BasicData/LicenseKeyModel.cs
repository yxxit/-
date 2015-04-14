using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    public class LicenseKeyModel
    {
        public LicenseKeyModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _type;
        private string _licensekey;
        private string _licensedate;
        private string _usedstatus;
        private string _remark;
        private int _creator;
        private DateTime _createdate;
        private DateTime _modifieddate;
        /// <summary>
        /// ID 自动生成
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
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 授权码
        /// </summary>
        public string LicenseKey
        {
            set { _licensekey = value; }
            get { return _licensekey; }
        }
        /// <summary>
        /// 授权日期
        /// </summary>
        public string LicenseDate
        {
            set { _licensedate = value; }
            get { return _licensedate; }
        }
        /// <summary>
        /// 启用状态 1：启用
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
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
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        #endregion Model
    }
}
