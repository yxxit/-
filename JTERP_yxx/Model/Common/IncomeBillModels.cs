/*************************
 * 创建人:yxx
 * 创建日期：2014-7-3
 * 描述：收款单 数据model
 *
 **************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBase.Model.Common
{
    /// <summary>
    /// 收款单 数据model
    /// 主要功能：新建收款单，收款单列表，新建付款单，付款单列表。 体现 供应商（客户）付款（收款）行为记录。
    /// </summary>
    public class IncomeBillModels
    {
        private string _FromBillType;

        public string FromBillType
        {
            get { return _FromBillType; }
            set { _FromBillType = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _CompanyCD;

        public string CompanyCD
        {
            get { return _CompanyCD; }
            set { _CompanyCD = value; }
        }
        private string _InComeNo;

        public string InComeNo
        {
            get { return _InComeNo; }
            set { _InComeNo = value; }
        }
        private string _AcceDate;

        public string AcceDate
        {
            get { return _AcceDate; }
            set { _AcceDate = value; }
        }
        private string _CustName;

        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }
        private int _BillingID;

        public int BillingID
        {
            get { return _BillingID; }
            set { _BillingID = value; }
        }
        private double _TotalPrice;

        public double TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        private string _AcceWay;

        public string AcceWay
        {
            get { return _AcceWay; }
            set { _AcceWay = value; }
        }
        private string _BankName;

        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        private int _Executor;

        public int Executor
        {
            get { return _Executor; }
            set { _Executor = value; }
        }
        private string _AccountNo;

        public string AccountNo
        {
            get { return _AccountNo; }
            set { _AccountNo = value; }
        }
        private string _ConfirmStatus;

        public string ConfirmStatus
        {
            get { return _ConfirmStatus; }
            set { _ConfirmStatus = value; }
        }
        private int _Confirmor;

        public int Confirmor
        {
            get { return _Confirmor; }
            set { _Confirmor = value; }
        }
        private string _ConfirmDate;

        public string ConfirmDate
        {
            get { return _ConfirmDate; }
            set { _ConfirmDate = value; }
        }
        private string _ModifiedDate;

        public string ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
        private string _ModifiedUserID;

        public string ModifiedUserID
        {
            get { return _ModifiedUserID; }
            set { _ModifiedUserID = value; }
        }
        private string _Summary;

        public string Summary
        {
            get { return _Summary; }
            set { _Summary = value; }
        }
        private string _IsAccount;

        public string IsAccount
        {
            get { return _IsAccount; }
            set { _IsAccount = value; }
        }
        private string _AccountDate;

        public string AccountDate
        {
            get { return _AccountDate; }
            set { _AccountDate = value; }
        }
        private int _Accountor;

        public int Accountor
        {
            get { return _Accountor; }
            set { _Accountor = value; }
        }
        private int _AttestBillID;

        public int AttestBillID
        {
            get { return _AttestBillID; }
            set { _AttestBillID = value; }
        }
        private string _BlendingType;

        public string BlendingType
        {
            get { return _BlendingType; }
            set { _BlendingType = value; }
        }
        private int _CurrencyType;

        public int CurrencyType
        {
            get { return _CurrencyType; }
            set { _CurrencyType = value; }
        }
        private double _CurrencyRate;

        public double CurrencyRate
        {
            get { return _CurrencyRate; }
            set { _CurrencyRate = value; }
        }
        private int _CustID;

        public int CustID
        {
            get { return _CustID; }
            set { _CustID = value; }
        }
        private string _FromTBName;

        public string FromTBName
        {
            get { return _FromTBName; }
            set { _FromTBName = value; }
        }
        private string _FileName;

        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        private int _ProjectID;

        public int ProjectID
        {
            get { return _ProjectID; }
            set { _ProjectID = value; }
        }
        private int _PaymentTypes;

        public int PaymentTypes
        {
            get { return _PaymentTypes; }
            set { _PaymentTypes = value; }
        }
        private double _NAccounts;

        public double NAccounts
        {
            get { return _NAccounts; }
            set { _NAccounts = value; }
        }
        private string _AccountsStatus;

        public string AccountsStatus
        {
            get { return _AccountsStatus; }
            set { _AccountsStatus = value; }
        }
     

        //public string FromBillType
        //{
        //    get { return _FromBillType; }
        //    set { _FromBillType = value; }
        //}

    }
}
