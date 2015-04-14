using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
    [Serializable]
   public class StorageInfoModel
    {
        public StorageInfoModel()
		{}
		#region Model
		private int _id;
        private string _storageNo;
		private string _companycd;
		private int _branchid;
		private string _storagename;
		private string _admin;
		private string _isdefault;
		private string _address;
		private string _remark;
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
        public string StorageNo
        {
            get { return _storageNo; }
            set { _storageNo=value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string StorageName
		{
		set{ _storagename=value;}
		get{return _storagename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin
		{
		set{ _admin=value;}
		get{return _admin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isDefault
		{
		set{ _isdefault=value;}
		get{return _isdefault;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
		set{ _address=value;}
		get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
		set{ _remark=value;}
		get{return _remark;}
		}
		#endregion Model


    }
}
