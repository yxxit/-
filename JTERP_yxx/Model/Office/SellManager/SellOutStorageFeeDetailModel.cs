using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class SellOutStorageFeeDetailModel
    {
        private int _id;
        private string _companycd;
        private string _outno;
        private int? _feeid;
        private decimal? _feetotal;
        private string _remark;
        private DateTime? _modifieddate;
        private string _modifieduserid;

        /// <summary>
        /// ID，自动生成
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 入库单编号
        /// </summary>
        public string OutNo
        {
            set { _outno = value; }
            get { return _outno; }
        }

        /// <summary>
        /// 费用类别ID（对应费用代码表ID）
        /// </summary>
        public int? FeeID
        {
            set { _feeid = value; }
            get { return _feeid; }
        }
        /// <summary>
        /// 费用金额
        /// </summary>
        public decimal? FeeTotal
        {
            set { _feetotal = value; }
            get { return _feetotal; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? ModifiedDate
        {
            set { _modifieddate = value; }
            get { return _modifieddate; }
        }
        /// <summary>
        /// 最后更新用户ID(对应操作用户UserID)
        /// </summary>
        public string ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCD
        {
            set { _companycd = value; }
            get { return _companycd; }
        }
    }
}
