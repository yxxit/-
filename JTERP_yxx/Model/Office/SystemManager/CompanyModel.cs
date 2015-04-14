using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SystemManager
{
    public class CompanyModel
    {
        #region Model
        private string _companycd;
        private string _namecn;
        private string _nameen;
        private string _nameshort;
        private string _pyshort;
        private string  _docsavepath;
        private string _usedstatus;
        private int _id;
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string NameCn
        {
            set { _namecn = value; }
            get { return _namecn; }
        }
       
        /// <summary>
        /// 公司名称
        /// </summary>
        public string NameEn
        {
            set { _nameen = value; }
            get { return _nameen; }
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        public string NameShort
        {
            set { _nameshort = value; }
            get { return _nameshort; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string PyShort
        {
            set { _pyshort = value; }
            get { return _pyshort; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string DocSavePath
        {
            set { _docsavepath = value; }
            get { return _docsavepath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        #endregion Model
    }
}
