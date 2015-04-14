using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class DisassemblingListModel
    {
        public DisassemblingListModel()
		{}
		#region Model
		private int _id;
		private decimal? _quota;
		private string _batch;
		private decimal? _usedcount; 
		private string _companycd;
		private string _billsno;
		private int? _types;
		private int? _storageid;
		private int? _productid;
		private int? _unitid;
		private decimal? _price;
		private decimal? _amount;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 定量
		/// </summary>
		public decimal? Quota
		{
			set{ _quota=value;}
			get{return _quota;}
		}
		/// <summary>
		/// 批次
		/// </summary>
		public string Batch
		{
			set{ _batch=value;}
			get{return _batch;}
		}
		/// <summary>
		/// 数量
		/// </summary>
		public decimal? UsedCount
		{
			set{ _usedcount=value;}
			get{return _usedcount;}
		}
		/// <summary>
		/// 公司编码
		/// </summary>
		public string companyCD
		{
			set{ _companycd=value;}
			get{return _companycd;}
		}
		/// <summary>
		/// 单据编号
		/// </summary>
		public string BillsNo
		{
			set{ _billsno=value;}
			get{return _billsno;}
		}
		/// <summary>
		/// 类型（套件/散件）
		/// </summary>
		public int? Types
		{
			set{ _types=value;}
			get{return _types;}
		}
		/// <summary>
		/// 仓库
		/// </summary>
		public int? Storageid
		{
			set{ _storageid=value;}
			get{return _storageid;}
		}
		/// <summary>
		/// 物品ID
		/// </summary>
		public int? Productid
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 单位ID
		/// </summary>
		public int? UnitID
		{
			set{ _unitid=value;}
			get{return _unitid;}
		}
		/// <summary>
		/// 价格
		/// </summary>
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal? Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		#endregion Model
    }
}
