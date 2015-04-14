/**********************************************
 * 类作用：   付款核销单数据库层处理
 * 建立人：   lcb
 * 建立时间： 2011/05/10
 ***********************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Model.Office.FinanceManager;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
    public class PayBlendBillDBHelper
    {
        /// <summary>
        /// 根据查询条件获取付款单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetPayBlendBillInfo(string CustID, int pageIndex, int pageSize, string OrderBy, ref int totalCount)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.PayNo,convert(varchar(10),a.PayDate,120) as PayDate,");
            sql.AppendLine("a.CustName,convert(varchar,convert(money,a.PayAmount),1) as PayAmount,case when a.AcceWay='0' then '现金' when");
            sql.AppendLine("a.AcceWay='1' then '银行转账' end as AcceWay ,a.AcceWay as AcceWayID,isnull(a.CustID,0) as CustID,");
            sql.AppendLine("case when a.PaymentType='0' then '应付' when a.PaymentType='1' then '预付' when a.PaymentType='2' then '其他费用' else then '' end as PaymentType,");
            sql.AppendLine("a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,b.CurrencyName,c.YAccounts,c.NAccounts ");
            sql.AppendLine("from officedba.PayBill as a left join ");
            sql.AppendLine("officedba.CurrencyTypeSetting as b on a.CurrencyType=b.ID left join officedba.BlendingDetails as c on a.BillingID=c.ID ");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.CustID=" + CustID + " and a.ConfirmStatus='1' ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageSize, OrderBy, ref totalCount);
        }


        /// <summary>
        /// 根据查询条件获取付款单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetPayBlendBillInfo(string CustID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.PayNo,convert(varchar(10),a.PayDate,120) as PayDate,");
            sql.AppendLine("a.CustName,convert(varchar,a.PayAmount,1) as PayAmount,case when a.AcceWay='0' then '现金' when");
            sql.AppendLine("a.AcceWay='1' then '银行转账' end as AcceWay ,a.AcceWay as AcceWayID,isnull(a.CustID,0) as CustID,");
            sql.AppendLine("case when a.PaymentType='0' then '应付' when a.PaymentType='1' then '预付' when a.PaymentType='2' then '其他费用' else '' end as PaymentType,");
            sql.AppendLine("a.CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,b.CurrencyName,isnull(a.NAccounts,a.PayAmount) as NAccounts ");
            sql.AppendLine("from officedba.PayBill as a left join ");
            sql.AppendLine("officedba.CurrencyTypeSetting as b on a.CurrencyType=b.ID ");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.CustID=" + CustID + " and a.ConfirmStatus='1' and Confirmor is not null and (naccounts>0 or  naccounts is null) ");            
            return SqlHelper.ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 根据查询条件获取应付单表信息
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static DataTable GetBillingInfo(string CustID)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  a.ID,a.BillCD, a.BillingNum, ");           
            sql.AppendLine("(CASE a.InvoiceType WHEN '1' THEN '增值税发票' WHEN '2' THEN '普通地税' WHEN '3' THEN '普通国税' WHEN '4' THEN '收据' ELSE '' END) ");
            sql.AppendLine(" AS InvoiceType, CONVERT(varchar(10),a.CreateDate, 120) AS CreateDate, ");
            sql.AppendLine(" a.ContactUnits, a.TotalPrice, a.YAccounts, isnull(a.NAccounts,a.TotalPrice) as NAccounts,isnull(a.CustID,0) as CustID, ");            
            sql.AppendLine(" isnull(a.CurrencyType,'') as CurrencyType,isnull(a.CurrencyRate,1) as CurrencyRate,CASE WHEN g.CurrencyName IS NOT NULL THEN g.CurrencyName WHEN g.CurrencyName IS NULL THEN '' END AS CurrencyName ");            
            sql.AppendLine(" FROM         officedba.Billing AS a LEFT OUTER JOIN ");            
            sql.AppendLine("officedba.CurrencyTypeSetting AS g ON a.CurrencyType = g.ID");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and a.CustID='" + CustID + "' and a.Auditor IS NOT NULL and isnull(a.AccountsStatus,'0')<>'1' ");
            sql.AppendLine("and a.BillingType in (0,2,3,6) and Accountsorcope=2 ");
            // sql.AppendLine("and (a.BillingType='2' or a.BillingType='3' or a.BillingType='6')");

            return SqlHelper.ExecuteSql(sql.ToString());

        }

        /// <summary>
        /// 更新付款单、票据、核销明细
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static bool UpdatePayBill(string payBillID, string payNo, string payAmount, string nAccounts, string settleAmount,
                                        string billingID, string payAmountBilling, string nAccountsBilling, string settleAmountBilling)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string accountsStatus = "0";
            string[] payBillIDCols = payBillID.Split(',');
            string[] payNoCols = payNo.Split(',');
            string[] payAmountCols = payAmount.Split(',');
            string[] nAccountsCols = nAccounts.Split(',');
            string[] settleAmountCols = settleAmount.Split(',');

            string[] billingIDCols = billingID.Split(',');
            string[] payAmountBillingCols = payAmountBilling.Split(',');
            string[] nAccountsBillingCols = nAccountsBilling.Split(',');
            string[] settleAmountBillingCols = settleAmountBilling.Split(',');
            StringBuilder sql = new StringBuilder();
            
            //付款单
            if (payBillIDCols != null && payBillIDCols.Length > 0)
            {
                for (int i = 0; i < payBillIDCols.Length; i++)
                {
                    if (payBillIDCols[i].ToString() != "")
                    {
                        if (float.Parse(nAccountsCols[i].ToString()) - float.Parse(settleAmountCols[i].ToString()) > 0)
                        {
                            accountsStatus = "1";
                        }
                        else
                        {
                            accountsStatus = "2";
                        }
                        string str = "";
                        if (accountsStatus == "2")
                        {
                            str = ",ConfirmStatus=1,Confirmor=" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID + ",ConfirmDate=getdate()";
                        }
                        sql.AppendLine("Update officedba.PayBill set NAccounts=" + (float.Parse(nAccountsCols[i].ToString()) - float.Parse(settleAmountCols[i].ToString())).ToString() + ",AccountsStatus='" + accountsStatus + "',ModifiedUserID=" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID + ",ModifiedDate=getdate()" + str + " where ID=" + payBillIDCols[i].ToString() + ";");
                    }
                }
            }

            //票据
            if (billingIDCols != null && billingIDCols.Length > 0)
            {
                for (int i = 0; i < billingIDCols.Length; i++)
                {
                    if (billingIDCols[i].ToString() != "")
                    {
                        if (float.Parse(nAccountsBillingCols[i].ToString()) - float.Parse(settleAmountBillingCols[i].ToString()) > 0)
                        {
                            accountsStatus = "2";
                        }
                        else
                        {
                            accountsStatus = "1";
                        }
                        sql.AppendLine("Update officedba.Billing set YAccounts=" + (float.Parse(payAmountBillingCols[i].ToString()) - float.Parse(nAccountsBillingCols[i].ToString()) + float.Parse(settleAmountBillingCols[i].ToString())).ToString() + ",NAccounts=" + (float.Parse(nAccountsBillingCols[i].ToString()) - float.Parse(settleAmountBillingCols[i].ToString())).ToString() + ",AccountsStatus='" + accountsStatus + "' where ID=" + billingIDCols[i].ToString() + ";");
                    }
                }
            }

            //核销明细
            string[] pIndex, bIndex, rtnAmount, amountTotal;
            GetIndex(settleAmountCols, settleAmountBillingCols, out pIndex, out bIndex, out rtnAmount, out amountTotal);
            for (int i = 0; i < pIndex.Length - 1; i++)
            {
                string pID = payBillIDCols[int.Parse(pIndex[i])];
                string pNo = payNoCols[int.Parse(pIndex[i])];
                string bID = billingIDCols[int.Parse(bIndex[i])];                
                sql.AppendLine(GetBlendSql(pID, pNo, bID, rtnAmount[i], amountTotal[i]));
            }

            SqlParameter[] parms = new SqlParameter[0];
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        
        /// <summary>
        /// 核销明细算法
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static void GetIndex(string[] pay, string[] bill, out string[] pIndex, out string[] bIndex, out string[] rtnAmount, out string[] rtnTotal)
        {
            string[] payClone = (string[])pay.Clone();
            string[] billClone = (string[])bill.Clone();
            StringBuilder payIndex = new StringBuilder();//付款单
            StringBuilder billIndex = new StringBuilder();//票据
            StringBuilder amount = new StringBuilder();//核销金额
            StringBuilder amountTotal = new StringBuilder();//票据核销金额累计

            for (int i = 0; i < billClone.Length; i++)
            {
                float payAmount = 0;
                float aTotal = 0;
                float billAmount = float.Parse(billClone[i]);
                for (int j = 0; j < payClone.Length; j++)
                {
                    if (float.Parse(payClone[j]) != 0)
                    {
                        payAmount += float.Parse(payClone[j]);
                        if (payAmount >= billAmount)
                        {
                            payIndex.Append(j + ",");
                            billIndex.Append(i + ",");
                            amount.Append(float.Parse(payClone[j]) + billAmount - payAmount + ",");
                            aTotal += float.Parse(payClone[j]) + billAmount - payAmount;
                            amountTotal.Append(aTotal + ",");
                            payClone[j] = (payAmount - billAmount).ToString();
                            break;
                        }
                        else
                        {
                            payIndex.Append(j + ",");
                            billIndex.Append(i + ",");
                            amount.Append(payClone[j] + ",");
                            aTotal += float.Parse(payClone[j]);
                            amountTotal.Append(aTotal + ",");
                            payClone[j] = "0";
                        }
                    }                    
                }                
            }

            pIndex = payIndex.ToString().Split(',');
            bIndex = billIndex.ToString().Split(',');
            rtnAmount = amount.ToString().Split(',');
            rtnTotal = amountTotal.ToString().Split(',');
        }

        /// <summary>
        /// 构建核销明细SQL语句
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        public static string GetBlendSql(string payBillID, string payNo, string billingID, string amount, string amountTotal)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            StringBuilder strSql = new StringBuilder();
            DataTable dt = GetBillingInfoByID(billingID);

            strSql.Append("insert into Officedba.BlendingDetails(");
            strSql.Append("CompanyCD,PayOrInComeType,BillingID,SourceDt,SourceID,BillCD,BillingType,InvoiceType,TotalPrice,YAccounts,NAccounts,CreateDate,ContactUnits,Executor,CurrencyType,CurrencyRate,ShouFuKuanID,ShouFuKuanNO)");
            strSql.Append(" values(");
            string vals = "'{0}','1',{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}',{12},{13},{14},{15},'{16}');";
            strSql.Append(string.Format(vals, CompanyCD, billingID, dt.Rows[0]["SourceDt"].ToString(),
                                dt.Rows[0]["SourceID"].ToString(),
                                dt.Rows[0]["BillCD"].ToString(),
                                dt.Rows[0]["BillingType"].ToString(),
                                dt.Rows[0]["InvoiceType"].ToString(),
                                dt.Rows[0]["TotalPrice"].ToString() == "" ? "0.00" : dt.Rows[0]["TotalPrice"].ToString(),//票据的总额
                                amount == "" ? "0.00" : amount,//每次勾兑的金额
                                float.Parse(dt.Rows[0]["NAccounts"].ToString() == "" ? "0.00" : dt.Rows[0]["NAccounts"].ToString()) - float.Parse(amountTotal),//票据的余额
                                DateTime.Now,
                                dt.Rows[0]["ContactUnits"].ToString(),
                                userID.ToString(),
                                dt.Rows[0]["CurrencyType"].ToString(),
                                dt.Rows[0]["CurrencyRate"].ToString(),
                                payBillID, payNo));

            return strSql.ToString();
        }

        #region 根据编码获取开票信息
        /// <summary>
        /// 根据编码获取开票信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetBillingInfoByID(string id)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select CompanyCD, BillCD,BillingType,InvoiceType,ContactUnits,TotalPrice,YAccounts,NAccounts,Executor,SourceDt,");
            sql.AppendLine("SourceID,CurrencyType,CurrencyRate ");                        
            sql.AppendLine("from officedba.billing ");
            sql.AppendLine("where ID=@ID");
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@ID", id);
            return SqlHelper.ExecuteSql(sql.ToString(), parms);
        }
        #endregion
    }
}
