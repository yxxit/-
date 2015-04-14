using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.BasicData
{
  public  class MatrixingUnitModel
    {
      public MatrixingUnitModel()
		{}
		#region Model
		private int _id;
		private string _companycd;
		private string _unitcode;
		private string _matrixingunicode;
		private string _unitname;
		private string _unittype;
		private decimal _matrixingratio;
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
		public string MatrixingUniCode
		{
		set{ _matrixingunicode=value;}
		get{return _matrixingunicode;}
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
		public string UnitType
		{
		set{ _unittype=value;}
		get{return _unittype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal MatrixingRatio
		{
		set{ _matrixingratio=value;}
		get{return _matrixingratio;}
		}
		#endregion Model
    }
}
