/**********************************************
 * 类作用：   申请单信息
 * 建立人：   宋凯歌
 * 建立时间： 2010/09/21
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XBase.Model.Office.SellManager
{
    public class InvoiceBillModel
    {
        private int _id;
        private string _companycd;
        private string _invno;
        private int _custid;
        private string _remark;
        private string _tel;
        private string _address;
        private string _accountman;
        private string _accountnum;
        private string _heading;
        private int _frombillid;
        private int _creator;
        private int _modifieduserid;
        private int _confirmuser;
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
        public string InvNo
        {
            set { _invno = value; }
            get { return _invno; }
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
        public int CustID
        {
            set { _custid = value; }
            get { return _custid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountMan
        {
            set { _accountman = value; }
            get { return _accountman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AccountNum
        {
            set { _accountnum = value; }
            get { return _accountnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Heading
        {
            set { _heading = value; }
            get { return _heading; }
        }
        /// <summary>
        /// 
        /// </summary>

        public int FromBillID
        {
            set { _frombillid = value; }
            get { return _frombillid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModifiedUserID
        {
            set { _modifieduserid = value; }
            get { return _modifieduserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ConfirmUser
        {
            set { _confirmuser = value; }
            get { return _confirmuser; }
        }
        /// <summary>
        /// 
        /// </summary>
    }
}
