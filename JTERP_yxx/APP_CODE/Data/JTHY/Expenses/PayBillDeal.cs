/**********************************************
 * 类作用   收款单数据处理层
 * 创建人   yxx
 * 创建时间 2014-7-13  
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Common;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Data;

namespace XBase.Data.JTHY.Expenses
{
    /// <summary>
    /// 收款单：收款信息的添加
    /// 注释：新建收款单，收款单列表，新建付款单，付款单列表。 体现 供应商（客户）付款（收款）行为记录。
    /// yxx
    /// 2014-7-3
    /// </summary>
   public  class PayBillDeal
   {
       #region 修改
       public static int PayBillEdit(PayBillModels ibm)
       {
           int ID = 0;
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();

           string sqlInsert = @"update Officedba.PayBill set 
PayDate='" + ibm.PayDate.ToString() + "',CustName='" + ibm.CustName + "',FromBillType='" + ibm.FromBillType + "',ConfirmStatus=" + 1 + ",BillingID=" + ibm.BillingID + ",PayAmount=" + ibm.PayAmount
           + ",PaymentType=" + ibm.PaymentType + ",AcceWay='" + ibm.AcceWay + "',Executor=" + ibm.Executor +
           "where ID=" + ibm.ID;

           commandInsert.CommandText = sqlInsert;
          
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
               if (isSucc)
               {
                   ID = -2;
               }
           }
           catch (Exception ex)
           {
               isSucc = false;
               ID = 0;
           }

           return ID;

       }
       #endregion
       #region 保存
       public static int PayBillAdd(PayBillModels ibm)
       {
           int ID = 0;
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();

           string sqlInsert = @"insert into Officedba.PayBill
                (PayNo,PayDate,CustName,FromBillType,BillingID,PayAmount,PaymentType,AcceWay,Executor,
AccountDate,Accountor,ConfirmDate,Confirmor,CompanyCD,ConfirmStatus)
                 values('" + ibm.PayNo + "','" + ibm.PayDate + "','" + ibm.CustName + "'," + ibm.FromBillType + "," + ibm.BillingID +
                              "," + ibm.PayAmount + "," + ibm.PaymentType + "," + ibm.AcceWay + "," + ibm.Executor +
                              ",'" + ibm.AccountDate + "'," + ibm.Accountor + ",'" + ibm.ConfirmDate + "'," + ibm.Confirmor + ",'"
                              + ibm.CompanyCD + "',0) set @ID=@@identity";

           commandInsert.CommandText = sqlInsert;
           commandInsert.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
               if (isSucc)
               {
                   ID = (int)commandInsert.Parameters["@ID"].Value;
               }
           }
           catch (Exception ex)
           {
               isSucc = false;
               ID = 0;
           }

           return ID;

       }
       #endregion
       #region 确认操作
       /// <summary>
       /// 确认操作
       /// </summary>
       /// <param name="ibm"></param>
       /// <returns></returns>
       public static bool ConfirmIncomeBill(PayBillModels ibm)
       {
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();


           string sqlInsert = @"update Officedba.PayBill set 
PayDate='" + ibm.PayDate.ToString() + "',CustName='" + ibm.CustName + "',FromBillType='" + ibm.FromBillType + "',ConfirmStatus=" + 1 + ",BillingID=" + ibm.BillingID + ",PayAmount=" + ibm.PayAmount
           +",PaymentType="+ibm.PaymentType+",AcceWay='"+ibm.AcceWay+"',Executor="+ibm.Executor+
           ",ConfirmDate='"+ibm.ConfirmDate.ToString()+"',Confirmor="+ibm.Confirmor+
           "where ID=" + ibm.ID ;

           commandInsert.CommandText = sqlInsert;
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
           }
           catch (Exception ex)
           {
               isSucc = false;
           }

           return isSucc;

       }

       #endregion

       #region 取消确认操作
       /// <summary>
       /// 取消确认操作
       /// </summary>
       /// <param name="ibm"></param>
       /// <returns></returns>
       public static bool CloseConfirmIncomeBill(PayBillModels ibm)
       {
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();


           string sqlInsert = "update Officedba.PayBill set ConfirmStatus='0',confirmor=null,confirmdate='' where ID=" + ibm.ID;

           commandInsert.CommandText = sqlInsert;
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
           }
           catch (Exception)
           {
               isSucc = false;
           }

           return isSucc;

       }

       #endregion

       
   

   }
}
