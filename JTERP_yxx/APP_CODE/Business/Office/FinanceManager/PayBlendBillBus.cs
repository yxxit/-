/**********************************************
 * 描述：     付款核销单业务处理
 * 建立人：   lcb
 * 建立时间： 2011/05/10
 ***********************************************/
using System;
using XBase.Data.Office.FinanceManager;
using XBase.Model.Office.FinanceManager;
using System.Data;
using XBase.Common;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.FinanceManager
{
    public class PayBlendBillBus
    {
        /// <summary>
        /// 根据查询条件获取付款单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetPayBlendBillInfo(string queryStr, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            try
            {
                return PayBlendBillDBHelper.GetPayBlendBillInfo(queryStr, pageIndex, pageSize, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据查询条件获取付款单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetPayBlendBillInfo(string CustID)
        {
            try
            {
                return PayBlendBillDBHelper.GetPayBlendBillInfo(CustID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据查询条件获取核销单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetBillingInfo(string CustID)
        {
            try
            {
                return PayBlendBillDBHelper.GetBillingInfo(CustID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新付款单、票据、核销明细
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static bool UpdatePayBill(string payBillID, string payNo, string payAmount, string nAccounts, string settleAmount,
                                        string billingID, string payAmountBilling, string nAccountsBilling, string settleAmountBilling)
        {
            try
            {
                return PayBlendBillDBHelper.UpdatePayBill(payBillID, payNo, payAmount, nAccounts, settleAmount, billingID, payAmountBilling, nAccountsBilling, settleAmountBilling);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
