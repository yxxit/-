using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
   public class CashBankInfoModel
    {
       public CashBankInfoModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private int _branchid;
		private string _accounttype;
		private string _openbankname;
		private string _accountname;
		private string _accountname1;
		private string _accountno;
        private decimal _initAmount;
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
		public int BranchID
		{
		set{ _branchid=value;}
		get{return _branchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountType
		{
		set{ _accounttype=value;}
		get{return _accounttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OpenBankName
		{
		set{ _openbankname=value;}
		get{return _openbankname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountName
		{
		set{ _accountname=value;}
		get{return _accountname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountName1
		{
		set{ _accountname1=value;}
		get{return _accountname1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountNo
		{
		set{ _accountno=value;}
		get{return _accountno;}
		}
       /// <summary>
       /// 期初余额（默认为0）
       /// </summary>
        public decimal InitAmount
        {
            set { _initAmount = value; }
            get { return _initAmount; }
        }
		#endregion Model
    }
}
