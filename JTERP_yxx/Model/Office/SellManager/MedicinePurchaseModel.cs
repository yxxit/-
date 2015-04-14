using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Office.SellManager
{
    public class MedicinePurchaseModel
   {
       #region model
       private int _id;
       private string _companycd;
       private string _orderno;
       private string _cashier;

       public string Cashier
       {
           get { return _cashier; }
           set { _cashier = value; }
       }
       private string _companyid;

       public string Companyid
       {
           get { return _companyid; }
           set { _companyid = value; }
       }
       private string _companyname;

       public string Companyname
       {
           get { return _companyname; }
           set { _companyname = value; }
       }
       private string _saledeptid;

       public string Saledeptid
       {
           get { return _saledeptid; }
           set { _saledeptid = value; }
       }
       private string _saledept;

       public string Saledept
       {
           get { return _saledept; }
           set { _saledept = value; }
       }


       private string  _totalprice;

       public string Totalprice
       {
           get { return _totalprice; }
           set { _totalprice = value; }
       }
       private string _counttotal;

       public string Counttotal
       {
           get { return _counttotal; }
           set { _counttotal = value; }
       }
       private string _orderdate;
       private string _status;
       private string _billstatus;

       private string _remark;

       public string Remark
       {
           get { return _remark; }
           set { _remark = value; }
       }
       private int _checker;
       private string _checkdate;
       private int _creator;
       private string _createdate;
      

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
        /// 单据编号
        /// </summary>
        public string OrderNo
        {
            set { _orderno = value; }
            get { return _orderno; }
        }




       
       
        /// <summary>
        /// 单据状态（1制单，2确认）
        /// </summary>
        public string BillStatus
        {
            set { _billstatus = value; }
            get { return _billstatus; }
        }
        /// <summary>
        /// 订单状态（0处理中，1已处理）
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
   
        /// <summary>
        /// 制单人ID
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单日期
        /// </summary>
        public string CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrderDate
        {
            set { _orderdate = value; }
            get { return _orderdate; }
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public int Checker
        {
            set { _checker = value; }
            get { return _checker; }
        }
        /// <summary>
        /// 审核人日期
        /// </summary>
        public string CheckDate
        {
            set { _checkdate = value; }
            get { return _checkdate; }
        }



       #endregion

        #region Detail Model
        private int _did;
        private string _productno;

        public string Productno
        {
            get { return _productno; }
            set { _productno = value; }
        }
        private string _productcount;

        public string Productcount
        {
            get { return _productcount; }
            set { _productcount = value; }
        }
        private string _supplyprice;

        public string Supplyprice
        {
            get { return _supplyprice; }
            set { _supplyprice = value; }
        }
     
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private string _tax;

        public string Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DID
        {
            set { _did = value; }
            get { return _did; }
        }
        
        #endregion DetailModel
   }
}
