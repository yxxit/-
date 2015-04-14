using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.FinanceManager;

namespace XBase.Business.Office.FinanceManager
{
    public class ReceiveVerifictionBus
    {
        #region 根据客户查询收款单
        /// <summary>
        /// 根据客户查询收款单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable getIncomeBillbyCust(string Custid)
        {
            try
            {
                return ReceiveVerifictionDBHelper.getIncomeBillbyCust(Custid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 根据客户查询票据
        /// <summary>
        /// 根据客户查询收票据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable getInvoiceBillbyCust(string Custid)
        {
            try
            {
                return ReceiveVerifictionDBHelper.getInvoiceBillbyCust(Custid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 更新付款单
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static bool UpdatePayBill(string payBillID, string payNo, string payAmount, string nAccounts, string settleAmount,
                                        string billingID, string payAmountBilling, string nAccountsBilling, string settleAmountBilling)
        {
            try
            {
                return ReceiveVerifictionDBHelper.UpdatePayBill(payBillID, payNo, payAmount, nAccounts, settleAmount, billingID, payAmountBilling, nAccountsBilling, settleAmountBilling);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
