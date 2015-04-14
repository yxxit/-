using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.StorageManager
{
    public class DisassemblingModel
    {
        public DisassemblingModel()
		{}
		#region Model
		private int _id;
		private int? _creater;
		private int? _confirmer;
		private DateTime? _confirmdate;
		private int? _closer;
		private DateTime? _closedate;
		private int? _status;
		private string _updater;
		private DateTime? _updatedate;
		private int? _billtype;
		private string _remark;
		private string _companycd;
		private string _billno;
		private int? _bomid;
		private string _bomname;
		private DateTime? _creatdate;
		private decimal? _totalprice;
		private int? _departmentid;
		private int? _handsmanid;
		/// <summary>
		/// 自增
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 制单人
		/// </summary>
		public int? creater
		{
			set{ _creater=value;}
			get{return _creater;}
		}
		/// <summary>
		/// 确认人
		/// </summary>
		public int? Confirmer
		{
			set{ _confirmer=value;}
			get{return _confirmer;}
		}
		/// <summary>
		/// 确认时间
		/// </summary>
		public DateTime? ConfirmDate
		{
			set{ _confirmdate=value;}
			get{return _confirmdate;}
		}
		/// <summary>
		/// 结单人
		/// </summary>
		public int? Closer
		{
			set{ _closer=value;}
			get{return _closer;}
		}
		/// <summary>
		/// 结单时间
		/// </summary>
		public DateTime? CloseDate
		{
			set{ _closedate=value;}
			get{return _closedate;}
		}
		/// <summary>
		/// 单据状态
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 最后更新人
		/// </summary>
		public string Updater
		{
			set{ _updater=value;}
			get{return _updater;}
		}
		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? UpdateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 单据类型
		/// </summary>
		public int? BillType
		{
			set{ _billtype=value;}
			get{return _billtype;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public string BillNo
		{
			set{ _billno=value;}
			get{return _billno;}
		}
		/// <summary>
		/// bomid
		/// </summary>
		public int? BomID
		{
			set{ _bomid=value;}
			get{return _bomid;}
		}
		/// <summary>
		/// bom名称
		/// </summary>
		public string BomName
		{
			set{ _bomname=value;}
			get{return _bomname;}
		}
		/// <summary>
		/// 制单时间
		/// </summary>
		public DateTime? CreatDate
		{
			set{ _creatdate=value;}
			get{return _creatdate;}
		}
		/// <summary>
		/// 拆装费用
		/// </summary>
		public decimal? TotalPrice
		{
			set{ _totalprice=value;}
			get{return _totalprice;}
		}
		/// <summary>
		/// 部门
		/// </summary>
		public int? departmentID
		{
			set{ _departmentid=value;}
			get{return _departmentid;}
		}
		/// <summary>
		/// 经手人
		/// </summary>
		public int? HandsManID
		{
			set{ _handsmanid=value;}
			get{return _handsmanid;}
		}
		#endregion Model
    }
}
