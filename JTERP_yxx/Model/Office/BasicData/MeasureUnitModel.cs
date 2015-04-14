using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
   public class MeasureUnitModel
    {
       public MeasureUnitModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _unitcode;
		private string _unitname;
		private string _isdefault;
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
		public string UnitCode
		{
		set{ _unitcode=value;}
		get{return _unitcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UnitName
		{
		set{ _unitname=value;}
		get{return _unitname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isDefault
		{
		set{ _isdefault=value;}
		get{return _isdefault;}
		}
		#endregion Model
    }
}
