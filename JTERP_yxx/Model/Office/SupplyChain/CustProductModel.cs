using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SupplyChain
{
    public class CustProductModel
    {
        public CustProductModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
        private int _custid;
        private string _prodno;
        private string _prodalias;
        private decimal _prodprice;
        private int _isstop;
        private DateTime _createdate;
        private string  _creator;
        private string _custName;
        private string _prodName;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
		set{ _id=value;}
		get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyCD
		{
		set{ _companycd=value;}
		get{return _companycd;}
		}
       /// <summary>
		/// 
		/// </summary>
        public int CustID
		{
            set { _custid= value; }
            get { return _custid; }
		}
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustName
        {
            set { _custName = value; }
            get { return _custName; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string ProdName
        {
            set { _prodName = value; }
            get { return _prodName; }
        }
		/// <summary>
		/// 
		/// </summary>
        public string ProdNo
		{
            set { _prodno = value; }
            get { return _prodno; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string  ProdAlias
		{
		    set{ _prodalias=value;}
		    get{return _prodalias;}
		}
		/// <summary>
		/// 
		/// </summary>
        public decimal ProdPrice
		{
            set { _prodprice = value; }
            get { return _prodprice; }
		}
		/// <summary>
		/// 
		/// </summary>
        public int IsStop
		{
            set { _isstop = value; }
            get { return _isstop; }
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
        public string Creator
		{
            set { _creator = value; }
            get { return _creator; }
		}
      
		#endregion Model
    }
}
