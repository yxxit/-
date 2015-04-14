using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
  public  class ProductCompanyInfoModel
    {
        public ProductCompanyInfoModel()
        { }
        #region Model
        private int _id;
        private string _companycd;
        private string _companyname;
        private string _productadd;
        private string _quality;
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
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductAdd
        {
            set { _productadd = value; }
            get { return _productadd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Quality
        {
            set { _quality = value; }
            get { return _quality; }
        }
        #endregion Model
    }
}
