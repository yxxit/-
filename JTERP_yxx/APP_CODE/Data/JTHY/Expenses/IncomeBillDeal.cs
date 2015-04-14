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
   public  class IncomeBillDeal
   {
       #region 修改
       /// <summary>
       /// 修改
       /// </summary>
       /// <param name="ibm"></param>
       /// <returns></returns>
       public static int IncomeBillEdit(IncomeBillModels ibm)
       {
           bool isSucc = false;
           int ID = 0;
           SqlCommand commandInsert = new SqlCommand();

           string sqlInsert = @"update Officedba.IncomeBill set 
AcceDate='" + ibm.AcceDate.ToString() + "',CustName='" + ibm.CustName + "',FromBillType='" + ibm.FromBillType + "',ConfirmStatus=" + 1 + ",BillingID=" + ibm.BillingID + ",TotalPrice="
            + ibm.TotalPrice
            + ",PaymentTypes=" + ibm.PaymentTypes + ",AcceWay='" + ibm.AcceWay + "',Executor=" + ibm.Executor +
            "where IncomeNo='" + ibm.InComeNo + "' and ID=" + ibm.Id + " and companyCD='" + ibm.CompanyCD + "'";

           commandInsert.CommandText = sqlInsert;
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
               if(isSucc){
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
       /// <summary>
       /// 保存
       /// </summary>
       /// <param name="ibm"></param>
       /// <returns></returns>
       public static int IncomeBillAdd(IncomeBillModels ibm) {
           bool isSucc = false;
           int ID = 0;
           SqlCommand commandInsert = new SqlCommand();

           string sqlInsert = @"insert into Officedba.IncomeBill
                (IncomeNo,AcceDate,CustName,FromBillType,BillingID,TotalPrice,PaymentTypes,AcceWay,Executor,
AccountDate,Accountor,ConfirmDate,Confirmor,CompanyCD,ConfirmStatus)
                 values('" + ibm.InComeNo + "','" + ibm.AcceDate + "','" + ibm.CustName + "'," + ibm.FromBillType + "," + ibm.BillingID +
                              "," + ibm.TotalPrice + "," + ibm.PaymentTypes + "," + ibm.AcceWay + "," + ibm.Executor +
                              ",'" + ibm.AccountDate + "'," + ibm.Accountor + ",'" + ibm.ConfirmDate + "'," + ibm.Confirmor + ",'" 
                              + ibm.CompanyCD + "',0) set @ID=@@identity";

           commandInsert.CommandText = sqlInsert;           
           commandInsert.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
           try
           {
               isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
               if(isSucc){
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
       public static bool ConfirmIncomeBill(IncomeBillModels ibm)
       {
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();


           string sqlInsert = @"update Officedba.IncomeBill set 
AcceDate='" + ibm.AcceDate.ToString() + "',CustName='" + ibm.CustName + "',FromBillType='" + ibm.FromBillType + "',ConfirmStatus="+1+",BillingID=" + ibm.BillingID + ",TotalPrice=" + ibm.TotalPrice
           +",PaymentTypes="+ibm.PaymentTypes+",AcceWay='"+ibm.AcceWay+"',Executor="+ibm.Executor+
           ",AccountDate='"+ibm.AccountDate.ToString()+"',Accountor="+ibm.Accountor+",ConfirmDate='"+ibm.ConfirmDate.ToString()+"',Confirmor="+ibm.Confirmor+
           "where IncomeNo='" + ibm.InComeNo + "' and ID=" + ibm.Id + " and companyCD='" + ibm.CompanyCD + "'";

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
       public static bool CloseConfirmIncomeBill(IncomeBillModels ibm)
       {
           bool isSucc = false;
           SqlCommand commandInsert = new SqlCommand();


           string sqlInsert = "update Officedba.IncomeBill set ConfirmStatus='0',confirmor=null,confirmdate='' where ID=" + ibm.Id;

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

       #region 根据字符获取id
       public static int getIdByExecutor(string EmployeeName, string CompanyCD)
       {
           int isSucc = 0;
           SqlCommand commandInsert = new SqlCommand();

           string sqlInsert = "select ID from officedba.EmployeeInfo where CompanyCD = '" + CompanyCD + "' and EmployeeName='" + EmployeeName + "'";

           commandInsert.CommandText = sqlInsert;
           try
           {
               //isSucc = SqlHelper.ExecuteSql(sqlInsert);
           }
           catch (Exception ex)
           {
               isSucc = 0;
           }

           return isSucc;

       }
       #endregion
       
   

   }
}
