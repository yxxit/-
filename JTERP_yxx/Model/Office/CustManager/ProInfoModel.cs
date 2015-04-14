/**********************************************
 * 类作用：  ProInfo表数据模板
 * 建立人：  包胜东
 * 建立时间：2013-10-11
 ***********************************************/
using System;
namespace XBase.Model.Office.CustManager
{
    public class ProInfoModel
    {
       
        private int _id;
        private string _companycd;
        private string _cvccode;
        private string _cvcname;
        private string _cvencode;
        private string _cvenname;
        private string _cvenabbname;
        private string _cvenaddress;
        private string _cvenperson;
        private string _free1;

        public string Free1
        {
            get { return _free1; }
            set { _free1 = value; }
        }
        private string _free2;

        public string Free2
        {
            get { return _free2; }
            set { _free2 = value; }
        }
        private string _free3;

        public string Free3
        {
            get { return _free3; }
            set { _free3 = value; }
        }

        public string Cvenperson
        {
            get { return _cvenperson; }
            set { _cvenperson = value; }
        }

        public string Cvccode
        {
            get { return _cvccode; }
            set { _cvccode = value; }
        }
      
        public string Cvcname
        {
            get { return _cvcname; }
            set { _cvcname = value; }
        }
      

        public string Cvencode
        {
            get { return _cvencode; }
            set { _cvencode = value; }
        }
      

        public string Cvenname
        {
            get { return _cvenname; }
            set { _cvenname = value; }
        }
      
        public string Cvenabbname
        {
            get { return _cvenabbname; }
            set { _cvenabbname = value; }
        }
     

        public string Cvenaddress
        {
            get { return _cvenaddress; }
            set { _cvenaddress = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
      
        public string Companycd
        {
            get { return _companycd; }
            set { _companycd = value; }
        }
        
       
        
        #region 附件
        private string _annfilename;
        private string _annremark;
        private string _annaddr;
        private string _updatetime;
        public string AnnFileName
        {
            set { _annfilename = value; }
            get { return _annfilename; }
        }
        public string AnnAddr
        {
            set { _annaddr = value; }
            get { return _annaddr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnnRemark
        {
            set { _annremark = value; }
            get { return _annremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        ///        
        public string UpDateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        #endregion
    }
}
