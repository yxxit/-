using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
    public class TeachInfoModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private string _teachno;
        private string _teachname;
        private int _teachtype;
        private string _unitid;
        private string _specification;
        private string _creator;
        private string _createdate;
        private string _usedstatus;
        private string _refercostprice;
        private string _Remark;
        private string _Quality;
        
        public string Quality
        {
            set { _Quality = value; }
            get { return _Quality; }
        }
        public int TeachType
        {
            set { _teachtype = value; }
            get { return _teachtype; }
        }
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
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
        /// 
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TeachNo
        {
            set { _teachno = value; }
            get { return _teachno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TeachName
        {
            set { _teachname = value; }
            get { return _teachname; }
        }
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Specification
        {
            set { _specification = value; }
            get { return _specification; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UsedStatus
        {
            set { _usedstatus = value; }
            get { return _usedstatus; }
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        public string referCostPrice
        {
            set { _refercostprice = value; }
            get { return _refercostprice; }
        }
        #endregion Model
    }
}
