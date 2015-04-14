using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data;



namespace XBase.Data.Office.FinanceManager
{
    public class ReceiveVerifictionDBHelper
    {
         #region 根据客户查询收款单
        /// <summary>
        /// 根据客户查询收款单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable getIncomeBillbyCust(string custid)
        {
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select a.id,convert(varchar(10),accedate,120)AcceDate,InComeNo,a.custname,a.totalprice,c.currencyname,
case when paymenttypes=0 then '应收' when paymenttypes=1 then '预收' when paymenttypes=2 then '其他费用' else  '' end as  paymenttypes 
,case when AcceWay=0 then '现金' when AcceWay=1 then '银行转账'end as acceway,isnull(a.naccounts,a.totalprice)naccounts from 
 Officedba.IncomeBill a  left join 
officedba.CurrencyTypeSetting c on a.CurrencyType=c.id   where (naccounts>0 or naccounts is null ) and Confirmor is not null and a.custid='" + custid + "' and a.companycd='" + companycd + "'");
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
        }
        #endregion



        #region 根据客户查询票据
        /// <summary>
        /// 根据客户查询票据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable getInvoiceBillbyCust(string Custid)
        {
            string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select a.id,convert(varchar(10),createdate,120)createdate,case when InvoiceType=1 then '增值税发票' when InvoiceType=2 then '普通地税' when InvoiceType=3 then '普通国税' when InvoiceType=4 then '收据' end as InvoiceType,
BillingNum,a.contactunits as custname,billcd,c.currencyname,a.TotalPrice,isnull(a.NAccounts,a.TotalPrice)NAccounts
 from officedba.Billing a left join officedba.CustInfo b on a.custid=b.id left join 
officedba.CurrencyTypeSetting c on a.CurrencyType=c.id where  
(NAccounts>0 or NAccounts is null) and AccountsStatus <>1 and BillingType in (0,1,4,5,7) and Accountsorcope=1 and   Auditor is not null and a.custid='" + Custid + "' and a.companycd='" + companycd + "'");
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            return dt;
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
                        sql.AppendLine("Update officedba.IncomeBill set NAccounts=" + (float.Parse(nAccountsCols[i].ToString()) - float.Parse(settleAmountCols[i].ToString())).ToString() + ",AccountsStatus='" + accountsStatus + "',ModifiedUserID=" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID + ",ModifiedDate=getdate()" + str + " where ID=" + payBillIDCols[i].ToString() + ";");
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
            string[] pIndex, bIndex, rtnAmount;
            GetIndex(settleAmountCols, settleAmountBillingCols, out pIndex, out bIndex, out rtnAmount);
            double totalprice = 0;
            string bill_id = "";
            for (int i = 0; i < pIndex.Length; i++)
            {
                string pID = payBillIDCols[int.Parse(pIndex[i])];
                string pNo = payNoCols[int.Parse(pIndex[i])];
                string bID = billingIDCols[int.Parse(bIndex[i])];
                if (bill_id != "")
                {
                    if (bill_id != bID)
                    {
                        totalprice = 0;
                        bill_id = bID;
                    }
                }
                else
                {
                    bill_id = bID;
                }
                string price=rtnAmount[i];
                totalprice += double.Parse(price);
                sql.AppendLine(GetBlendSql(pID, pNo, bID,price,totalprice));
            }
            SqlParameter[] parms = new SqlParameter[0];
            SqlHelper.ExecuteTransSql(sql.ToString(), parms);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }

        public static void GetIndex(string[] pay, string[] bill, out string[] pIndex, out string[] bIndex, out string[] rtnAmount)
        {
            string[] payClone = pay;
            string[] billClone = bill;
            string payIndex = "";
            string billIndex ="";
            string amount = "";

            
                //float payAmount = 0;
                //float billAmount = float.Parse(billClone[i]);
                //for (int j = 0; j < payClone.Length; j++)
                //{
                //    if (float.Parse(payClone[j]) != 0)
                //    {
                //        payAmount += float.Parse(payClone[j]);
                //        if (payAmount > billAmount)
                //        {
                //            payClone[j] = payAmount - billAmount;
                //            payIndex.Append(j);
                //            billIndex.Append(i);
                //            amount.Append(billAmount);
                //            break;
                //        }
                //        else
                //        {
                //            payIndex.Append(j);
                //            billIndex.Append(i);
                //            amount.Append(payAmount);
                //            payClone[j] = 0;
                //        }
                //    }
                //} 
                for (int i = 0; i < billClone.Length; i++)
                {
                    int j = 0;
                    int s = 0;
                    while (true)
                    {
                        
                        s++;
                        if (float.Parse(payClone[j]) > 0)
                        {
                            if (float.Parse(billClone[i]) >= float.Parse(payClone[j]))
                            {
                                billClone[i] = (float.Parse(billClone[i]) - float.Parse(payClone[j])).ToString();
                                payIndex += s - 1 + ",";
                                billIndex += i + ",";
                                amount += payClone[j] + ",";
                                payClone[j] = "0";
                                j++;
                                if (j == payClone.Length || billClone[i] == "0")
                                {
                                    break;
                                }
                            }
                            else
                            {
                                payIndex += s - 1 + ",";
                                billIndex += i + ",";
                                amount += billClone[i] + ",";
                                payClone[j] = (float.Parse(payClone[j]) - float.Parse(billClone[i])).ToString();
                                billClone[i] = "0";
                                break;
                            }
                        }
                        else
                        {
                            j++;

                        }
                    }
                }

            payIndex=payIndex.Substring(0, payIndex.Length - 1);
            billIndex = billIndex.Substring(0, billIndex.Length - 1);
            amount=amount.Substring(0, amount.Length - 1);
            pIndex = payIndex.ToString().Split(',');
            bIndex = billIndex.ToString().Split(',');
            rtnAmount = amount.ToString().Split(',');
        }

        public static string GetBlendSql(string payBillID, string payNo, string billingID,string price,double totalprice)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int userID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            StringBuilder strSql = new StringBuilder();
            DataTable dt = GetBillingInfoByID(billingID);
            
            strSql.Append("insert into Officedba.BlendingDetails(");
            strSql.Append("CompanyCD,PayOrInComeType,BillingID,SourceDt,SourceID,BillCD,BillingType,InvoiceType,TotalPrice,YAccounts,NAccounts,CreateDate,ContactUnits,Executor,CurrencyType,CurrencyRate,ShouFuKuanID,ShouFuKuanNO)");
            strSql.Append(" values(");
            string vals = "'{0}','2',{1},'{2}','{3}','{4}','{5}','{6}',{7},{8},{9},'{10}','{11}',{12},{13},{14},{15},'{16}');";
            strSql.Append(string.Format(vals, CompanyCD, billingID, dt.Rows[0]["SourceDt"].ToString(),
                                dt.Rows[0]["SourceID"].ToString(),
                                dt.Rows[0]["BillCD"].ToString(),
                                dt.Rows[0]["BillingType"].ToString(),
                                dt.Rows[0]["InvoiceType"].ToString(),
                                dt.Rows[0]["TotalPrice"].ToString()==""? "0.00":dt.Rows[0]["TotalPrice"].ToString(),
                                price==""? "0.00":price,
                                dt.Rows[0]["NAccounts"].ToString() == "" ? "0.00" : (double.Parse(dt.Rows[0]["NAccounts"].ToString()) - totalprice).ToString(),
                                DateTime.Now,
                                dt.Rows[0]["ContactUnits"].ToString(),
                                userID,
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
