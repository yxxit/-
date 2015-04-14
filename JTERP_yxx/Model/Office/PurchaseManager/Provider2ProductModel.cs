using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.PurchaseManager
{
   public class Provider2ProductModel
    {
        #region Model
        private int _id;
        private string _companycd;
        private int _providerid;
        private int _linkmanid;
        private string _linkmanname;
        private string _linktel;
        private int _sortno;
        // private int _productid;
        private string _productid;
        private string _productno;
        private string _productname;
        private string _remark;
        private DateTime _modifieddate;
        private string _modifieduserid;
        private int _deptype;
        /// <summary>
        /// 自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public int ProviderID
        {
            set { _providerid = value; }
            get { return _providerid; }
        }
        /// <summary>
        /// 供应商联系人ID
        /// </summary>
        public int LinkManID
        {
            set { _linkmanid = value; }
            get { return _linkmanid; }
        }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkManName
        {
            set { _linkmanname = value; }
            get { return _linkmanname; }
        }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LinkerTel
        {
            set { _linktel = value; }
            get { return _linktel; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo
        {
            set { _sortno = value; }
            get { return _sortno; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 物品编号
        /// </summary>
        public string ProductNo
        {
            set { _productno = value; }
            get { return _productno; }
        }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string ProductName
        {
            set { _productname = value; }
            get { return _productname; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 单位类型 0供应商 1客户
        /// </summary>
        public int Deptype
        {
            set { _deptype = value; }
            get { return _deptype; }
        }
        #endregion Model
    }
}
