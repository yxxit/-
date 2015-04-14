/*************************************
 * 创建人：何小武
 * 创建日期：2009-12-12
 * 描述：销售出库
 ************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellManager;
using System.Web.UI.WebControls;

namespace XBase.Data.Office.SellManager
{
    public class SellOutStorageForeignDBHelper
    {
        #region 添加销售出库单
        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true保存成功，false保存失败</returns>
        public static bool SaveSellOutStorage(SellOutStorageForeignModel sellOutModel, List<SellOutStorageForeignDetailModel> sellOutDMList, Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellOutModel.OutNo, sellOutModel.CompanyCD))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellOutStorage(sellOutModel, tran, htExtAttr);//插入主表信息
                    InsertSellOutStorageDetail(sellOutDMList, tran);//插入明细表信息
                    tran.Commit();
                    isSucc = true;
                    strMsg = "保存成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "保存失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "该编号已被使用，请输入未使用的编号！";
            }
            return isSucc;
        }
        #endregion
        #region 修改销售出库单
        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true修改成功，false修改失败</returns>
        public static bool UpdataSellOutStorage(SellOutStorageForeignModel sellOutModel, List<SellOutStorageForeignDetailModel> sellOutDMList, Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOutModel.OutNo, sellOutModel.CompanyCD))
            {
                string strSql = "delete from officedba.SellOutStorageDetailForeign where  OutNo=@OutNo  and CompanyCD=@CompanyCD ";
                SqlParameter[] paras = { new SqlParameter("@OutNo", sellOutModel.OutNo), new SqlParameter("@CompanyCD", sellOutModel.CompanyCD) };

                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateSellOutStorage(sellOutModel, tran, htExtAttr);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertSellOutStorageDetail(sellOutDMList, tran);
                    tran.Commit();
                    isSucc = true;
                    strMsg = "修改成功！@";
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "修改失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
                strMsg = "非制单状态、已提交审批或审批中的单据不可修改！";
            }
            return isSucc;
        }
        #endregion

        #region 收款
        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true修改成功，false修改失败</returns>
        public static bool CollectionSellOutStorage(SellOutStorageForeignModel sellOutModel, Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            try
            {


                string strSQL = "update officedba.SellOutStorageForeign set Cashier='" + sellOutModel.CashierID + "',DeptID='" + sellOutModel.DeptID + "',ExchangeRate='" + sellOutModel.ExchangeRate + "',ExchangeDate='" + sellOutModel.ExchangeDate + "',ExchangeAmount='" + sellOutModel.ExchangeAmount + "',CommissionDate='" + sellOutModel.CommissionDate + "',BillStatus='3' where OutNo = '" + sellOutModel.OutNo + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                string[] strSqls = { strSQL };
                SqlHelper.ExecuteTransForListWithSQL(strSqls);
                //string strSQL2 = "update officedba.SellOrderForeign set BackSection="+"'"+"isnull(BackSection,0)"  + sellOutModel.ExchangeAmount + "',BackCommission="+"'"+"isnull(BackCommission,0)"+ sellOutModel.TotalFee + "' where ID = '" + sellOutModel.FromBillID + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                //string[] strSqls2 = { strSQL2 };
                //SqlHelper.ExecuteTransForListWithSQL(strSqls2);
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append(" update officedba.SellOrderForeign set BackSection=isnull(BackSection,0)+@ExchangeAmount,BackCommission=isnull(BackCommission,0)+@TotalFee ");
                strSql1.Append(" where ID = @FromBillID and CompanyCD= @CompanyCD ");

                SqlParameter[] p1 = {   new SqlParameter("@ExchangeAmount",sellOutModel.ExchangeAmount ),
                                        new SqlParameter("@TotalFee",sellOutModel.TotalFee ),
                                            new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                            new SqlParameter("@FromBillID",sellOutModel.FromBillID )                                           
                                           };
                SqlHelper.ExecuteNonQuery(strSql1.ToString(), p1);
                isSucc = true;
                strMsg = "收款成功！@";
            }
            catch (Exception ex)
            {
                strMsg = "收款失败，请联系系统管理员！";
                throw ex;
            }
            return isSucc;
        }
        #endregion
        #region 取消收款
        /// <summary>
        /// 取消收款
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true修改成功，false修改失败</returns>
        public static bool NoCollectionSellOutStorage(SellOutStorageForeignModel sellOutModel, Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            try
            {


                string strSQL = "update officedba.SellOutStorageForeign set Cashier=NULL,DeptID=NULL,ExchangeRate=NULL,ExchangeDate=NULL,ExchangeAmount=NULL,CommissionDate=NULL,BillStatus='2' where OutNo = '" + sellOutModel.OutNo + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                string[] strSqls = { strSQL };
                SqlHelper.ExecuteTransForListWithSQL(strSqls);
                //string strSQL2 = "update officedba.SellOrderForeign set BackSection=NULL,BackCommission=NULL where ID = '" + sellOutModel.FromBillID + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                //string[] strSqls2 = { strSQL2 };
                //SqlHelper.ExecuteTransForListWithSQL(strSqls2);
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append(" update officedba.SellOrderForeign set BackSection=isnull(BackSection,0)-@ExchangeAmount,BackCommission=isnull(BackCommission,0)-@TotalFee ");
                strSql1.Append(" where ID = @FromBillID and CompanyCD= @CompanyCD ");

                SqlParameter[] p1 = {   new SqlParameter("@ExchangeAmount",sellOutModel.ExchangeAmount ),
                                        new SqlParameter("@TotalFee",sellOutModel.TotalFee ),
                                            new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                            new SqlParameter("@FromBillID",sellOutModel.FromBillID )                                           
                                           };
                SqlHelper.ExecuteNonQuery(strSql1.ToString(), p1);
                isSucc = true;
                strMsg = "取消成功！@";
            }
            catch (Exception ex)
            {
                strMsg = "取消失败，请联系系统管理员！";
                throw ex;
            }
            return isSucc;
        }
        #endregion
        #region 删除销售出库单
        /// <summary>
        /// 删除销售出库单
        /// </summary>
        /// <param name="NoStr">编号序列</param>
        public static bool DelSellOutStorage(string NoStr, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            bool isSucc = false;
            string allSellOutNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();

            string[] strSellOutNos = null;
            strSellOutNos = NoStr.Split(',');

            for (int i = 0; i < strSellOutNos.Length; i++)
            {
                //if (!isHandle(strSellOutNos[i], "1", strCompanyCD))
                //{
                //    // strFieldText += "单据：" + strSellOutNos[i] + " ";
                //    //strMsg += "已确认或已作废的单据不允许删除！\n";
                //    strMsg += "单据：" + strSellOutNos[i] + " " + "已确认,已收款或已作废的单据不允许删除！";
                //    bTemp = true;
                //}
                //if (IsNotCurStore(strSellOutNos[i], branchID, strCompanyCD))
                //{
                //    //strFieldText += "单据：" + strSellOutNos[i] + " ";
                //    //strMsg += "不是本店的单据不允许删除！\n";
                //    strMsg += "单据：" + strSellOutNos[i] + " " + "不是本店的单据不允许删除！";
                //    bTemp = true;
                //}
                strSellOutNos[i] = "'" + strSellOutNos[i] + "'";
                sb.Append(strSellOutNos[i]);
            }

            allSellOutNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOutStorageForeign WHERE OutNo IN ( " + allSellOutNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOutStorageDetailForeign WHERE OutNo IN ( " + allSellOutNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

                    tran.Commit();
                    isSucc = true;
                    strMsg = "删除成功！";

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "删除失败，请联系系统管理员！";
                    isSucc = false;
                    throw ex;
                }
            }
            else
            {
                isSucc = false;
            }
            return isSucc;
        }
        #endregion

        #region 插入销售出库单主表信息
        /// <summary>
        /// 插入销售出库单主表信息
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="tran">事务</param>
        public static void InsertSellOutStorage(SellOutStorageForeignModel sellOutModel, TransactionManager tran, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into officedba.SellOutStorageForeign (CompanyCD,OutNo,Title,FromBillID,");
            strSql.Append(" CustID,UserID,ContainerNumber,Destination,");
            strSql.Append(" DestinationDate,InvoiceNo,YPayTime,MExchangeRate,OrderDate,OrderCurrencyRate,");
            strSql.Append(" InlandCosts,SeaFreight,TotalFreight,OrderCurrency,BillStatus,Remark,Creator,CreateDate,ShipComp  ");
            strSql.Append(" ,Cashier,DeptID,ExchangeRate,ExchangeDate,ExchangeAmount,CommissionDate ) ");
            strSql.Append(" values(@CompanyCD,@OutNo,@Title,@FromBillID,");
            strSql.Append(" @CustID,@UserID,@ContainerNumber,@Destination,");
            strSql.Append(" @DestinationDate,@InvoiceNo,@YPayTime,@MExchangeRate,@OrderDate,@OrderCurrencyRate,");
            strSql.Append(" @InlandCosts,@SeaFreight,@TotalFreight,@OrderCurrency,@BillStatus,@Remark,@Creator,getdate(),@ShipComp, ");
            strSql.Append(" @Cashier,@DeptID,@ExchangeRate,@ExchangeDate,@ExchangeAmount,@CommissionDate )");
            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellOutModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOutModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOutModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@UserID", sellOutModel.UserID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContainerNumber", sellOutModel.ContainerNumber.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Destination", sellOutModel.Destination.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DestinationDate", sellOutModel.DestinationDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@InvoiceNo", sellOutModel.InvoiceNo.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@YPayTime", sellOutModel.YPayTime.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MExchangeRate", sellOutModel.MExchangeRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderDate", sellOutModel.OrderDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrencyRate", sellOutModel.OrderCurrencyRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@InlandCosts", sellOutModel.InlandCosts.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SeaFreight", sellOutModel.SeaFreight.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFreight", sellOutModel.TotalFreight.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrency", sellOutModel.OrderCurrency));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOutModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellOutModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ShipComp", sellOutModel.ShipComp.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Cashier", sellOutModel.CashierID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeptID", sellOutModel.DeptID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeRate", sellOutModel.ExchangeRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeDate", sellOutModel.ExchangeDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeAmount", sellOutModel.ExchangeAmount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CommissionDate", sellOutModel.CommissionDate.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellOutStorageForeign set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and OutNo=@OutNo  ");//and BranchID=@BranchID
            }
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            #endregion

            foreach (SqlParameter para in param)
            {
                if (para.Value == null || para.Value.ToString() == "-1")
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion
        #region 插入销售出库单明细
        /// <summary>
        /// 插入销售出库单明细
        /// </summary>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="tran">事务</param>
        private static void InsertSellOutStorageDetail(List<SellOutStorageForeignDetailModel> sellOutDMList, TransactionManager tran)
        {
            foreach (SellOutStorageForeignDetailModel sellOutDetailModel in sellOutDMList)
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("insert into officedba.SellOutStorageDetailForeign(");
                strSql.Append("CompanyCD,OutNo,ProductID,DetailCount,PriceType,CostPrice,");
                strSql.Append(" SalesPrice,Difference,Ratio,DeclarationNumber,DeclarationPrice,Shipments)");
                strSql.Append(" values (");
                strSql.Append(" @CompanyCD,@OutNo,@ProductID,@DetailCount,@PriceType,@CostPrice,");
                strSql.Append(" @SalesPrice,@Difference,@Ratio,@DeclarationNumber,@DeclarationPrice,@Shipments)");
                #region 参数
                SqlParameter[] param = {
					                        new SqlParameter("@CompanyCD", sellOutDetailModel.CompanyCD ),
					                        new SqlParameter("@OutNo", sellOutDetailModel.OutNo ),
					                        new SqlParameter("@ProductID", sellOutDetailModel.ProductID ),
					                        new SqlParameter("@DetailCount", sellOutDetailModel.DetailCount ),
					                        new SqlParameter("@PriceType", sellOutDetailModel.PriceType ),
					                        new SqlParameter("@CostPrice", sellOutDetailModel.CostPrice ),
					                        new SqlParameter("@SalesPrice", sellOutDetailModel.SalesPrice),
					                        new SqlParameter("@Difference", sellOutDetailModel.Difference ),
					                        new SqlParameter("@Ratio", sellOutDetailModel.Ratio ),
					                        new SqlParameter("@DeclarationNumber", sellOutDetailModel.DeclarationNumber ) ,
                                            new SqlParameter("@DeclarationPrice", sellOutDetailModel.DeclarationPrice ) ,
                                             new SqlParameter("@Shipments", sellOutDetailModel.Shipments ) 
                                       };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null || para.Value.ToString() == "-1")
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
            }
        }
        #endregion
        #region 修改主表信息
        /// <summary>
        /// 修改主表信息
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="tran">事务</param>
        private static void UpdateSellOutStorage(SellOutStorageForeignModel sellOutModel, TransactionManager tran, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update officedba.SellOutStorageForeign set ");
            strSql.Append(" Title=@Title,FromBillID=@FromBillID,");
            strSql.Append(" CustID=@CustID,UserID=@UserID,");
            strSql.Append(" ContainerNumber=@ContainerNumber,Destination=@Destination,");
            strSql.Append(" DestinationDate=@DestinationDate,InvoiceNo=@InvoiceNo,YPayTime=@YPayTime,");
            strSql.Append(" MExchangeRate=@MExchangeRate,InlandCosts=@InlandCosts,");
            strSql.Append(" SeaFreight=@SeaFreight,TotalFreight=@TotalFreight,OrderCurrency=@OrderCurrency,OrderDate=@OrderDate,OrderCurrencyRate=@OrderCurrencyRate,Remark=@Remark,ShipComp=@ShipComp, ");
            strSql.Append(" Cashier=@Cashier,DeptID=@DeptID,ExchangeRate=@ExchangeRate,ExchangeDate=@ExchangeDate,ExchangeAmount=@ExchangeAmount,CommissionDate=@CommissionDate ");
            strSql.Append(" where CompanyCD=@CompanyCD and OutNo=@OutNo ");
            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            //lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
            //lcmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellOutModel.Title));
            //lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOutModel.FromBillID.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOutModel.CustID.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@UserID", sellOutModel.UserID.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ContainerNumber", sellOutModel.ContainerNumber.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@Destination", sellOutModel.Destination.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@DestinationDate", sellOutModel.DestinationDate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@InvoiceNo", sellOutModel.InvoiceNo.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@YPayTime", sellOutModel.YPayTime.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@MExchangeRate", sellOutModel.MExchangeRate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@InlandCosts", sellOutModel.InlandCosts.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@SeaFreight", sellOutModel.SeaFreight.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@TotalFreight", sellOutModel.TotalFreight.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrency", sellOutModel.OrderCurrency));
            //lcmd.Add(SqlHelper.GetParameterFromString("@OrderDate", sellOutModel.OrderDate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrencyRate", sellOutModel.OrderCurrencyRate.ToString()));
            //lcmd.Add(SqlHelper.GetParameterFromString("@ShipComp", sellOutModel.ShipComp.ToString()));

            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellOutModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOutModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOutModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@UserID", sellOutModel.UserID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContainerNumber", sellOutModel.ContainerNumber.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Destination", sellOutModel.Destination.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DestinationDate", sellOutModel.DestinationDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@InvoiceNo", sellOutModel.InvoiceNo.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@YPayTime", sellOutModel.YPayTime.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MExchangeRate", sellOutModel.MExchangeRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderDate", sellOutModel.OrderDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrencyRate", sellOutModel.OrderCurrencyRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@InlandCosts", sellOutModel.InlandCosts.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SeaFreight", sellOutModel.SeaFreight.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFreight", sellOutModel.TotalFreight.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@OrderCurrency", sellOutModel.OrderCurrency));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOutModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@ShipComp", sellOutModel.ShipComp.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Cashier", sellOutModel.CashierID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeptID", sellOutModel.DeptID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeRate", sellOutModel.ExchangeRate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeDate", sellOutModel.ExchangeDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExchangeAmount", sellOutModel.ExchangeAmount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CommissionDate", sellOutModel.CommissionDate.ToString()));
            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellOutStorage set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus where CompanyCD=@CompanyCD and OutNo=@OutNo and BranchID=@BranchID ");
            }
            //将list参数存入参数数组中
            if (lcmd != null && lcmd.Count > 0)
            {
                param = new SqlParameter[lcmd.Count];
                for (int i = 0; i < lcmd.Count; i++)
                {
                    param[i] = (SqlParameter)lcmd[i];
                }
            }
            #endregion

            foreach (SqlParameter para in param)
            {
                if (para.Value == null || para.Value.ToString() == "-1")
                {
                    para.Value = DBNull.Value;
                }
            }

            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion

        #region 确认销售出库单
        /// <summary>
        /// 确认销售出库单
        /// </summary>
        /// <param name="sellOutModel">sellOutModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true确认成功，false确认失败</returns>
        public static bool ConfirmSellOutStorage(SellOutStorageForeignModel sellOutModel, List<SellOutStorageForeignDetailModel> sellOutDMList, Hashtable htExtAttr, string EmployeeName, int EmployeeID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            if (isHandle(sellOutModel.OutNo, "1", sellOutModel.CompanyCD))
            {
                //string isEnoughStr = IsEnoughStorage(sellOutDMList, sellOutModel.StorageID, sellOutModel.BranchID);
                //if (isEnoughStr == "")
                //{
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] param2 ={
                                                new SqlParameter("@CompanyCD",sellOutModel.CompanyCD),
                                                new SqlParameter("@OutNo",sellOutModel.OutNo)
                                             };
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.SellOutStorageDetailForeign where  OutNo=@OutNo  and CompanyCD=@CompanyCD", param2);
                    UpdateSellOutStorage(sellOutModel, tran, htExtAttr);//修改主表
                    InsertSellOutStorageDetail(sellOutDMList, tran);//重新插入子表

                    //更新单据状态，确认人，确认时间
                    strSql.Append(" update officedba.SellOutStorageForeign set BillStatus=@BillStatus, ");
                    strSql.Append(" Confirmor=@Confirmor ,ConfirmDate=getdate()");
                    strSql.Append(" where CompanyCD= @CompanyCD and OutNo=@OutNo ");

                    SqlParameter[] p = { 
                                            new SqlParameter("@Confirmor",EmployeeID),
                                            new SqlParameter("@BillStatus","2"),
                                            new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                            new SqlParameter("@OutNo",sellOutModel.OutNo )
                                           // new SqlParameter("@BranchID",sellOutModel.BranchID )
                                           };
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), p);

                    foreach (SellOutStorageForeignDetailModel sellOutDetailModel in sellOutDMList)
                    {
                        StringBuilder strSql2 = new StringBuilder();
                        //附加更新销售订单明细中的出货数
                        strSql2.Append(" ;update officedba.SellOrderDetailForeign set NumberShipments=isnull(NumberShipments,0)+@Shipments ");
                        strSql2.Append(" where ProductID=@ProductID ");
                        strSql2.Append(" and CompanyCD=@CompanyCD  ");
                        strSql2.Append(" and OrderNo=(select OrderNo from officedba.SellOrderForeign where ID = @FromBillID and CompanyCD = @CompanyCD)  ");
                        SqlParameter[] param = {
                                                    new SqlParameter("@Shipments",sellOutDetailModel.Shipments),
                                                    new SqlParameter("@ProductID",sellOutDetailModel.ProductID ),
                                                    new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                                    new SqlParameter("@FromBillID",sellOutModel.FromBillID )
                                                   };

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql2.ToString(), param);
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    strMsg = "确认失败！请联系管理员";
                    isSuc = false;
                }


                string sql3 = "select * from (select case  when isnull(OrderCount,0)-isnull(NumberShipments,0)=0 then 2 when isnull(NumberShipments,0)=0 then 0 when isnull(OrderCount,0)-isnull(NumberShipments,0)>0 then 1 end   status  ";
                sql3 += "from officedba.SellOrderDetailForeign  where OrderNo =(select OrderNo from officedba.SellOrderForeign where ID = " + sellOutModel.FromBillID + " and CompanyCD = '" + sellOutModel.CompanyCD + "') )as aa  group by status";
                DataTable dt = SqlHelper.ExecuteSql(sql3);
                bool succ = false;
                string Shipments = "";
                if (dt.Rows.Count == 1)
                {
                    Shipments = dt.Rows[0]["status"].ToString();
                }
                if (dt.Rows.Count > 1)
                {
                    Shipments = "1";
                }
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append(" update officedba.SellOrderForeign set Shipments=@Shipments ");
                strSql1.Append(" where CompanyCD= @CompanyCD ");
                strSql1.Append(" and OrderNo=(select OrderNo from officedba.SellOrderForeign where ID = @FromBillID and CompanyCD = @CompanyCD)  ");

                SqlParameter[] p1 = {   new SqlParameter("@Shipments",Shipments ),
                                            new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                            new SqlParameter("@FromBillID",sellOutModel.FromBillID )                                           
                                           };
               int count= SqlHelper.ExecuteNonQuery(strSql1.ToString(), p1);


                isSuc = true;
                strMsg = "确认成功！" + "@" + EmployeeName + "@" + System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {//已经被其他人确认
                strMsg = "已经确认的单据不可再次确认！";
                isSuc = false;
            }
            return isSuc;
        }
        #endregion

        #region 作废销售出库单
        /// <summary>
        /// 作废销售出库单
        /// 作废前提：单据状态为执行状态，且未被其他单据（财务中和销售退货单中）引用
        /// </summary>
        /// <param name="sellOutModel">sellOutModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true作废成功，false作废失败</returns>
        public static bool ScrapSellOutStorage(SellOutStorageForeignModel sellOutModel, List<SellOutStorageForeignDetailModel> sellOutDMList, int EmployeeID, string EmployeeName, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            ArrayList lstCmd = new ArrayList();
            //if (isHandleTwo(sellOutModel.OutNo, sellOutModel.ID, sellOutModel.CompanyCD))//判断是否可被作废
            //{
            var strBillSQL = "select BillStatus from officedba.SellOutStorageForeign where CompanyCD='" + sellOutModel.CompanyCD + "' and OutNo='" + sellOutModel.OutNo + "'";
            DataTable DTBill = SqlHelper.ExecuteSql(strBillSQL);
            var strBillStatus = DTBill.Rows[0][0].ToString();
            foreach (SellOutStorageForeignDetailModel sellOutDetailModel in sellOutDMList)
            {
                //附加更新销售订单明细中的出货数
                string upSellOrder = string.Format("update officedba.SellOrderDetailForeign set NumberShipments=isnull(NumberShipments,0)-'{0}'  where ProductID='{1}' " +
                " and CompanyCD='{2}'  " +
                " and OrderNo=(select OrderNo from officedba.SellOrderForeign where ID = '{3}' and CompanyCD = '{2}')  ", sellOutDetailModel.Shipments, sellOutDetailModel.ProductID, sellOutModel.CompanyCD, sellOutModel.FromBillID);
                //SqlHelper.ExecuteSql(upSellOrder);
                string[] strSqlss = { upSellOrder };
                SqlHelper.ExecuteTransForListWithSQL(strSqlss);
            }
            string sql3 = "select ID from officedba.SellOutStorageForeign where FromBillID = " + sellOutModel.FromBillID + " and CompanyCD = '" + sellOutModel.CompanyCD + "'";
            DataTable dt = SqlHelper.ExecuteSql(sql3);
            string Shipments = "";
            if (dt.Rows.Count > 1)
            {
                Shipments = "1";
            }
            else
            {
                Shipments = "0";
            }
            string strSQL3 = "update officedba.SellOrderForeign set Shipments='" + Shipments + "' where ID = '" + sellOutModel.FromBillID + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                string[] strSqls3 = { strSQL3 };
                SqlHelper.ExecuteTransForListWithSQL(strSqls3);
            if (strBillStatus == "3")
            {

                string strSQL2 = "update officedba.SellOrderForeign set BackSection=NULL,BackCommission=NULL where ID = '" + sellOutModel.FromBillID + "' and CompanyCD='" + sellOutModel.CompanyCD + "'";
                string[] strSqls2 = { strSQL2 };
                SqlHelper.ExecuteTransForListWithSQL(strSqls2);
            }
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {

                strSql.Append(" update officedba.SellOutStorageForeign set BillStatus=@BillStatus, ");
                strSql.Append(" Invalidor=@Invalidor ,InvalidDate=getdate() ");
                strSql.Append(" where CompanyCD=@CompanyCD and OutNo=@OutNo ");

                lstCmd.Add(SqlHelper.GetParameterFromString("@Invalidor", EmployeeID.ToString()));
                lstCmd.Add(SqlHelper.GetParameterFromString("@BillStatus", "4"));
                lstCmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
                lstCmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
                // lstCmd.Add(SqlHelper.GetParameterFromString("@BranchID", sellOutModel.BranchID.ToString()));

                SqlParameter[] p = null;
                if (lstCmd != null && lstCmd.Count > 0)
                {
                    p = new SqlParameter[lstCmd.Count];
                    for (int i = 0; i < lstCmd.Count; i++)
                    {
                        p[i] = (SqlParameter)lstCmd[i];
                    }
                }
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), p);

                //附加更新分仓存量表语句(入库：增加库存量)
                //foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
                //{
                //    StringBuilder strSql2 = new StringBuilder();
                //    strSql2.Append(" ;update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)+@ProductCount ");
                //    strSql2.Append(" where StorageID=@StorageID");
                //    strSql2.Append(" and ProductID=@ProductID");
                //    strSql2.Append(" and BatchNo=@BatchNo ");
                //    strSql2.Append(" and CompanyCD=@CompanyCD and BranchID=@BranchID ");

                //    //更新库存结余表（删除记录）
                //    //获取当前明细的ID
                //    strSql2.Append(" ;select @BillDetailID=ID from officedba.SellOutStorageDetail where CompanyCD=@CompanyCD ");
                //    strSql2.Append(" and OutNo=@BillNo and ProductID=@ProductID and BatchNo=@BatchNo ");

                //    strSql2.Append(" ;delete from officedba.StorageSurplus where CompanyCD=@CompanyCD ");
                //    strSql2.Append(" and BranchID=@BranchID and BillNo=@BillNo and BillDetailID=@BillDetailID ");

                //    SqlParameter[] param = {
                //                                new SqlParameter("@ProductCount",sellOutDetailModel.DetailCount ),
                //                                new SqlParameter("@StorageID",sellOutModel.StorageID ),
                //                                new SqlParameter("@ProductID",sellOutDetailModel.ProductID ),
                //                                new SqlParameter("@BatchNo",sellOutDetailModel.BatchNo ),
                //                                new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                //                                new SqlParameter("@BranchID",sellOutModel.BranchID ),
                //                                new SqlParameter("@BillNo",sellOutModel.OutNo ),
                //                                new SqlParameter("@BillDetailID",0)
                //                               };

                //    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql2.ToString(), param);

                //}

                tran.Commit();
                isSuc = true;

                strMsg = "作废成功！" + "@" + EmployeeName + "@" + System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch
            {
                tran.Rollback();
                strMsg = "作废失败！请联系管理员";
                isSuc = false;
            }
            // }
            //else
            //{//已经被其他人作废
            //    strMsg = "已经作废或者被其它单据引用的单据，不可作废！";
            //    isSuc = false;
            //}
            return isSuc;
        }
        #endregion

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据ID获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByID(int BillID)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append(" select s.ID,s.BranchID,s.OutNo,s.CustID,s.UserID,s.StorageID,s.Contractor,s.ContractPhone,");
            //strSql.Append(" s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,s.FromBillID,s.AriveAddress,");
            //strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            //strSql.Append(" s.BillStatus,s.Remark,s.PreferPrice,s.TotalCount,s.TotalPrice,s.Creator,");
            //strSql.Append(" CONVERT(varchar(100),s.CreateDate,23) as CreateDate,CONVERT(varchar(100),s.ExpireDay,23) as ExpireDay, ");
            //strSql.Append(" s.Confirmor,s.Invalidor,s.IsBlend, s.IsUsedPromotion, ");
            //strSql.Append(" case s.IsUsedPromotion when 0 then '停用' when 1 then '启用' end as IsUsedPromotionText, ");
            //strSql.Append(" c.CustName as CustName,e1.EmployeeName as UserName,s2.StorageName as StorageName,s2.Admin as AdminName ,");
            //strSql.Append(" s3.OrderNo as FromBillNo,e2.EmployeeName as CreatorName,");
            //strSql.Append(" e3.EmployeeName as ConfirmorName,e4.EmployeeName as InvalidorName,");
            //strSql.Append(" CONVERT(varchar(100),s.ConfirmDate,23) as ConfirmDate,CONVERT(varchar(100),s.InvalidDate,23) as InvalidDate, ");
            //strSql.Append(" case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            //strSql.Append(" ,case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText ");
            //strSql.Append(" ,s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ");

            //strSql.Append(" from officedba.SellOutStorage as s ");
            //strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            //strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.UserID ");
            //strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            //strSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=s.Confirmor ");
            //strSql.Append(" left join officedba.EmployeeInfo as e4 on e4.ID=s.Invalidor ");
            //strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            //strSql.Append(" left join officedba.SellOrder as s3 on s3.ID=s.FromBillID ");
            //strSql.Append(" where s.ID=@BillID ");
            strSql.Append(" select s.ID,s.OutNo,s.CustID,s.UserID,s.Title,s.Cashier,s.DeptID,f.DeptName,s.InvoiceNo,s.ShipComp,g.CurrencyName,");
            strSql.Append(" s.FromBillID,s.BillStatus,s.ContainerNumber,s.Destination,s.MExchangeRate,s.InlandCosts,s.SeaFreight,s.TotalFreight,");//CONVERT(varchar(100),s.SellDate,23) as SellDate,
            strSql.Append(" s.Remark,s.OrderCurrency,s.Creator,");
            strSql.Append(" CONVERT(varchar(100),s.DestinationDate,23) as DestinationDate,CONVERT(varchar(100),s.YPayTime,23) as YPayTime,CONVERT(varchar(100),s.OrderDate,23) as OrderDate,CONVERT(varchar(100),s.CommissionDate,23) as CommissionDate, ");
            strSql.Append(" s.Confirmor,s.Invalidor,CONVERT(varchar(100),s.ExchangeDate,23) as ExchangeDate,s.ExchangeRate,s.ExchangeAmount,s.OrderCurrencyRate, ");//s.IsBlend, s.IsUsedPromotion, strSql.Append(" case s.IsUsedPromotion when 0 then '停用' when 1 then '启用' end as IsUsedPromotionText, ");
            strSql.Append(" c.CustName as CustName,e1.EmployeeName as UserName,");//s2.StorageName as StorageName,s2.Admin as AdminName ,");
            strSql.Append(" s3.OrderNo as FromBillNo,e2.EmployeeName as CreatorName,");
            strSql.Append(" e3.EmployeeName as ConfirmorName,e4.EmployeeName as InvalidorName,e5.EmployeeName as CashName,");
            strSql.Append(" CONVERT(varchar(100),s.ConfirmDate,23) as ConfirmDate,CONVERT(varchar(100),s.CreateDate,23) as CreateDate,CONVERT(varchar(100),s.InvalidDate,23) as InvalidDate, ");
            // strSql.Append(" case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '已收款' when 4 then '作废' end as BillStatusText ,");
            //strSql.Append(" s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ");
            strSql.Append(" (select Top 1 LinkManName from officedba.CustLinkMan as d1 where d1.CustNo = c.CustNo and d1.CompanyCD = c.CompanyCD) as LinkManName,");
            strSql.Append(" (select Top 1 WorkTel from officedba.CustLinkMan as d1 where d1.CustNo = c.CustNo and d1.CompanyCD = c.CompanyCD) as WorkTel,");
            strSql.Append(" (select Top 1 MSN from officedba.CustLinkMan as d1 where d1.CustNo = c.CustNo and d1.CompanyCD = c.CompanyCD) as QQ");
            strSql.Append(" from officedba.SellOutStorageForeign as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and c.CompanyCD=s.CompanyCD  ");//and (NatureType=2 or NatureType=3)
            //strSql.Append("  left join officedba.CustLinkMan d on d.CustID=c.ID and c.CompanyCD=d.CompanyCD");
            strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.UserID  and e1.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator  and e2.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=s.Confirmor  and e3.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.EmployeeInfo as e4 on e4.ID=s.Invalidor  and e4.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.EmployeeInfo as e5 on e5.ID=s.Cashier  and e5.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.DeptInfo as f on f.ID=s.DeptID  and f.CompanyCD=s.CompanyCD");
            strSql.Append(" left join officedba.CurrencyTypeSetting as g on g.ID=s.OrderCurrency  and g.CompanyCD=s.CompanyCD");
            //strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join officedba.SellOrderForeign as s3 on s3.ID=s.FromBillID  and s3.CompanyCD=s.CompanyCD");
            strSql.Append(" where s.ID=@BillID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",BillID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取销售出库单明细
        public static DataTable GetSellOutDetail(string BillNo, string strCompanyCD, int FromBillID)//, int storageID, int BranchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select s.ID,s.OutNo,s.ProductID,s.DetailCount,");
            strSql.Append(" s.CostPrice,s.SalesPrice,s.Difference,s.Ratio,s.DeclarationNumber,s.DeclarationPrice,s.Shipments,s.QuantityShipped");
            strSql.Append(" ,p.ProdNo ProductNo,p.ProductName,p.Specification,isnull(q.NumberShipments,0) as CDetailCount, ");
            strSql.Append(" case s.PriceType when 0 then 'CIF' when 1 then 'FOB'  end as PriceTypeName, ");
            ////strSql.Append(" (select UnitName from officedba. MeasureUnit m where p.UnitID=m.ID) as UnitName ");
            strSql.Append("(select CodeName from officedba.CodeUnitType m where m.ID=p.UnitID) as UnitName ");
            //strSql.Append(" ,(select ProductCount from officedba.StorageProduct s2 where s2.ProductID=s.ProductID and s2.StorageID=@storageID ");
            //strSql.Append(" and s2.CompanyCD=@CompanyCD and s2.BatchNo=s.BatchNo and s2.BranchID=@BranchID) as ProductCount ");
            //strSql.Append(" ,isBarCode='',isBatchNo='' ");//是否启用条码、批次
            strSql.Append(" from officedba.SellOutStorageDetailForeign as s ");
            strSql.Append(" left join officedba.ProductInfo p on p.ID=s.ProductID  and p.CompanyCD = s.CompanyCD");
            strSql.Append(" left join officedba.SellOrderDetailForeign q  on q.CompanyCD = s.CompanyCD and q.OrderNo = (select OrderNo from officedba.SellOrderForeign where ID = @FromBillID) and q.ProductID = s.ProductID ");
            strSql.Append(" where s.OutNo=@BillNo and s.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillNo",BillNo ),
                                    new SqlParameter("@CompanyCD",strCompanyCD ),
                                    new SqlParameter("@FromBillID",FromBillID )
                                    //new SqlParameter("@storageID",storageID ),
                                    //new SqlParameter("@BranchID",BranchID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取销售出库单历史单据列表
        /// <summary>
        /// 获取销售出库单历史单据列表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutHistoryList(SellOutStorageModel model, DateTime? SellDate1, string FromBillNo, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OutNo,s.CustID,s.StorageID,s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,");
            strSql.Append(" s.TotalCount,s.TotalPrice,s.BillStatus,c.CustName as CustName,s2.StorageName as StorageName, ");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText,BlendPrice,IsBlend ");
            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" where s.CompanyCD=@CompanyCD and s.BranchID=@BranchID ");
            strSql.Append("and s.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            strSql.Append("  INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            strSql.Append(" CROSS JOIN            officedba.UserInfo AS v  ");
            strSql.Append(" INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            strSql.Append("  INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            strSql.Append("                WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "')) ");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", model.BranchID.ToString()));
            if (model.OutNo != null)
            {
                string OutNoParam = "%" + model.OutNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
                strSql.Append(" and s.OutNo like @OutNo ");
            }
            if (model.StorageID != null && model.StorageID != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID.ToString()));
                strSql.Append(" and s.StorageID=@StorageID ");
            }
            if (model.SellType != null && model.SellType != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellType", model.SellType.ToString()));
                strSql.Append(" and s.SellType=@SellType ");
            }
            if (SellDate1 != null && model.SellDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
                strSql.Append(" and s.SellDate>=@SellDate and s.SellDate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (SellDate1 != null && model.SellDate == null)//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
                strSql.Append(" and s.SellDate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (SellDate1 == null && model.SellDate != null)//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
                strSql.Append(" and s.SellDate>=@SellDate  ");
            }

            if (FromBillNo != null)
            {
                string FromBillNoParam = "%" + FromBillNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNoParam));
                strSql.Append(" and s.FromBillID in( ");
                strSql.Append(" select ID from officedba.SellOrder where OrderNo like @FromBillNo and CompanyCD=@CompanyCD )");
            }
            if (model.BillStatus != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                strSql.Append(" and s.BillStatus=@BillStatus ");
            }
            //此处为收款核销单页面所用
            if (model.CustID != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
                strSql.Append(" and s.CustID=@CustID ");
            }
            if (model.IsBlend != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsBlend", model.IsBlend));
                strSql.Append(" and s.IsBlend !=@IsBlend ");
            }
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            //return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, lstCmd, ref totalCount);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 获取销售出库单列表
        /// <summary>
        /// 获取销售出库单列表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutList(SellOutStorageForeignModel model, string FromBillNo, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OutNo,s.CustID,s.InvoiceNo,s.ShipComp,");
            strSql.Append(" s.BillStatus,c.CustName as CustName,s.Destination,CONVERT(varchar(100),s.DestinationDate,23) as DestinationDate,s.InlandCosts,s.SeaFreight,s.TotalFreight,h.OrderNo, ");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '已收款' when 4 then '作废' end as BillStatusText ");
            strSql.Append(" from officedba.SellOutStorageForeign as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and c.CompanyCD = s.CompanyCD   ");//and (NatureType=2 or NatureType=3)
            strSql.Append(" left join officedba.SellOrderForeign as h on h.ID=s.FromBillID and h.CompanyCD = s.CompanyCD   ");
            //strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            //strSql.Append(" left join officedba.DeptInfo as s3 on s3.ID=s.BranchID ");
            strSql.Append(" where s.CompanyCD=@CompanyCD  ");//此处的Remark为保存BranchID串（逗号隔开）的时候用的
            strSql.Append("and ((s.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            strSql.Append("  INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            strSql.Append(" CROSS JOIN            officedba.UserInfo AS v  ");
            strSql.Append(" INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            strSql.Append("  INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            strSql.Append("                WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "')) )");
            strSql.AppendLine("or (s.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", model.Remark));
            if (model.OutNo != null)
            {
                string OutNoParam = "%" + model.OutNo + "%";
                strSql.Append(" and s.OutNo like @OutNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
            }
            if (model.InvoiceNo != null)
            {

                strSql.Append(" and s.InvoiceNo=@InvoiceNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@InvoiceNo", model.InvoiceNo.ToString()));
            }
            if (model.ShipComp != null)
            {

                strSql.Append(" and s.ShipComp=@ShipComp ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ShipComp", model.ShipComp.ToString()));
            }
            if (model.CustID != null && model.CustID != 0)
            {

                strSql.Append(" and s.CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            }
            //if (model.StorageID != null && model.StorageID != 0)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID.ToString()));
            //    strSql.Append(" and s.StorageID=@StorageID ");
            //}
            //if (model.SellType != null && model.SellType != 0)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellType", model.SellType.ToString()));
            //    strSql.Append(" and s.SellType=@SellType ");
            //}
            //if (SellDate1 != null && model.SellDate != null)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
            //    strSql.Append(" and s.SellDate>=@SellDate and s.SellDate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            //}
            //else if (SellDate1 != null && model.SellDate == null)//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
            //    strSql.Append(" and s.SellDate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            //}
            //else if (SellDate1 == null && model.SellDate != null)//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
            //    strSql.Append(" and s.SellDate>=@SellDate  ");
            //}

            if (FromBillNo != null)
            {
                string FromBillNoParam = "%" + FromBillNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FromBillNo", FromBillNoParam));
                strSql.Append(" and s.FromBillID in( ");
                strSql.Append(" select ID from officedba.SellOrderForeign where OrderNo like @FromBillNo and CompanyCD=@CompanyCD )");
            }
            if (model.BillStatus != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus));
                strSql.Append(" and s.BillStatus=@BillStatus ");
            }
            if (model.Destination != null && model.Destination != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Destination", model.Destination.ToString()));
                strSql.Append(" and s.Destination=@Destination ");
            }
            //if (model.DestinationDate != null )
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DestinationDate", model.DestinationDate.ToString()));
            //    strSql.Append(" and s.DestinationDate=@DestinationDate ");
            //}
            if (model.DestinationDateEnd != null && model.DestinationDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DestinationDate", model.DestinationDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DestinationDateEnd", model.DestinationDateEnd.ToString()));
                strSql.Append(" and s.DestinationDate>=@DestinationDate and s.DestinationDate<=@DestinationDateEnd ");
            }
            else if (model.DestinationDateEnd != null && model.DestinationDate == null)//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DestinationDateEnd", model.DestinationDateEnd.ToString()));
                strSql.Append(" and s.DestinationDate<=@DestinationDateEnd ");
            }
            else if (model.DestinationDateEnd == null && model.DestinationDate != null)//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DestinationDate", model.DestinationDate.ToString()));
                strSql.Append(" and s.DestinationDate>=@DestinationDate  ");
            }
            //if (model.IsBlend != null)
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsBlend", model.IsBlend));
            //    strSql.Append(" and s.IsBlend=@IsBlend ");
            //}
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            //return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, lstCmd, ref totalCount);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 根据单据编号和公司编码获取单据ID
        /// <summary>
        /// 根据单据编号和公司编码获取单据ID
        /// </summary>
        /// <param name="BillNo">单据编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>ID</returns>
        public static int GetSellOutID(string BillNo, string strCompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID from officedba.SellOutStorageForeign where OutNo=@BillNo and CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                       new SqlParameter("@BillNo",BillNo ),
                                       new SqlParameter("@CompanyCD",strCompanyCD )
                                   };
            return (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
        }
        #endregion

        #region 判断该公司该单据编号是否存在
        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="BillNo">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string BillNo, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(1) from officedba.SellOutStorageForeign ");
            strSql.Append(" WHERE  (OutNo = @OutNo) AND (CompanyCD = @CompanyCD) ");

            SqlParameter[] param = { 
                                       new SqlParameter("@OutNo", BillNo),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 判断单据是否可以被修改
        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="BillNo">发货单编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string BillNo, string strCompanyCD)
        {
            bool isSuc = true;
            int iCount = 0;
            StringBuilder strSql = new StringBuilder();

            SqlParameter[] param = { 
                                       new SqlParameter("@OutNo", BillNo),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@BillStatus","2")
                                   };

            strSql.Append(" select count(1) from officedba.SellOutStorageForeign ");
            strSql.Append(" where OutNo=@OutNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ");
            iCount = (int)SqlHelper.ExecuteScalar(strSql.ToString(), param);
            if (iCount > 0)
            {
                isSuc = false;
            }

            return isSuc;
        }
        #endregion

        #region 确认相关操作是否可以进行
        /// <summary>
        /// 根据单据状态判断该单据是否可以执行该操作
        /// </summary>
        /// <param name="BillNo">单据编号</param>
        /// <param name="BillStatus">单据状态</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool isHandle(string BillNo, string BillStatus, string strCompanyCD)
        {
            bool isSuc = false;
            int iCount = 0;
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.SellOutStorageForeign where OutNo = @OutNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            SqlParameter[] paras = { 
                                    new SqlParameter("@OutNo", BillNo),
                                    new SqlParameter("@CompanyCD", strCompanyCD),
                                    new SqlParameter("@BillStatus", BillStatus)
                                   };
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion
        #region 判断单据是否是当前用户店的单据
        /// <summary>
        /// 判断单据是否是当前用户店的单据
        /// </summary>
        /// <param name="billNo">单据编号</param>
        /// <param name="branchID">分店ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true时表示不是本店单据<</returns>
        private static bool IsNotCurStore(string billNo, int branchID, string strCompanyCD)
        {
            int iCount = 0;
            bool isSuc = true;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(1) from officedba.SellOutStorage ");
            strSql.Append(" where CompanyCD=@CompanyCD and BranchID=@BranchID and OutNo=@OutNo ");

            SqlParameter[] param = { 
                                    new SqlParameter("@CompanyCD",strCompanyCD ),
                                    new SqlParameter("@BranchID",branchID ),
                                    new SqlParameter("@OutNo",billNo )
                                   };
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
            if (iCount > 0)
            {
                isSuc = false;
            }
            return isSuc;
        }
        #endregion

        #region 判断单据是否可作废
        /// <summary>
        /// 判断单据是否可作废
        /// 若单据状态为执行状态，且未被销售退货和财务中引用时可作废；若被销售退货引用单据状态都为作废状态时可作废
        /// </summary>
        /// <param name="BillNo">单据编号</param>
        /// <param name="BillID">单据ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>返回true时表示可被作废，false时不可被作废</returns>
        private static bool isHandleTwo(string BillNo, int BillID, string strCompanyCD)
        {
            bool isSuc = true;
            int iCount = 0;
            int iCoutn2 = 0;
            int iCount3 = 0;
            string strSql = string.Empty;
            string strSql2 = string.Empty;
            string strSql3 = string.Empty;
            //是否被退货引用(排除退货单据状态为作废状态)
            strSql = "select count(1) from officedba.SellBack where FromBillID = @BillID and CompanyCD=@CompanyCD and (BillStatus=1 or BillStatus=2) ";
            SqlParameter[] paras = { 
                                    new SqlParameter("@BillNo", BillNo),
                                    new SqlParameter("@BillID", BillID),
                                    new SqlParameter("@CompanyCD", strCompanyCD)
                                   };
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            //是否被财务中引用()
            strSql2 = "select count(1) from officedba.SellOutStorage where IsBlend!=0 and OutNo=@BillNo and CompanyCD=@CompanyCD ";
            iCoutn2 = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql2.ToString(), paras));
            //出库单状态是否为执行状态
            strSql3 = "select count(1) from officedba.SellOutStorage where BillStatus=2 and OutNo=@BillNo and CompanyCD=@CompanyCD ";
            iCount3 = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql3.ToString(), paras));


            if (iCount != 0 || iCoutn2 != 0)
            {
                isSuc = false;
            }
            if (iCount3 < 0 || iCount3 == 0)
            {
                isSuc = false;
            }

            return isSuc;
        }
        #endregion

        #region 判断库存是否足够
        /// <summary>
        /// 判断库存是否足够
        /// </summary>
        /// <param name="sellOutDMList">销售出库单明细列表</param>
        /// <returns>返回不满足条件的行号，若都满足条件则返回空""</returns>
        private static string IsEnoughStorage(List<SellOutStorageForeignDetailModel> sellOutDMList, int? StorageID, int? BranchID)
        {
            string isEnoughStr = "";
            StringBuilder strSql = new StringBuilder();
            foreach (SellOutStorageForeignDetailModel sellOutDetailModel in sellOutDMList)
            {
                strSql.Append(" select count(1) from officedba.StorageProduct ");
                strSql.Append(" where ProductCount >= @ProductCount ");
                strSql.Append(" and StorageID=@StorageID ");
                strSql.Append(" and ProductID=@ProductID ");
                strSql.Append(" and BatchNo=@BatchNo ");
                strSql.Append(" and CompanyCD=@CompanyCD ");
                strSql.Append(" and BranchID=@BranchID ");
                SqlParameter[] param = { 
                                        new SqlParameter("@ProductCount",sellOutDetailModel.DetailCount),
                                        new SqlParameter("@StorageID",StorageID),
                                        new SqlParameter("@ProductID",sellOutDetailModel.ProductID),
                                        new SqlParameter("@BatchNo",sellOutDetailModel.BatchNo),
                                        new SqlParameter("@CompanyCD",sellOutDetailModel.CompanyCD),
                                        new SqlParameter("@BranchID",BranchID)
                                       };
                int count = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql.ToString(), param));
                if (count > 0)
                {
                    //isEnoughStr = sellOutDetailModel.FromDetailID + ",";
                }
                else
                {
                    isEnoughStr = sellOutDetailModel.ProductID + ",";
                }
            }
            return isEnoughStr;
        }
        #endregion

        #region 根据仓库，商品ID获取批次列表
        /// <summary>
        /// 根据仓库，商品ID获取批次列表
        /// </summary>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID，总店为0</param>
        /// <returns></returns>
        public static DataTable GetBatchNoList(int StorageID, int ProductID, string strCompanyCD, int branchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendLine(" select ID,BatchNo,ProductCount from officedba.StorageProduct ");
            strSql.AppendLine(" where ProductID=@ProductID ");
            strSql.AppendLine(" and CompanyCD=@CompanyCD and BranchID=@BranchID ");

            SqlCommand cmdsql = new SqlCommand();
            cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", strCompanyCD.ToString()));
            cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", branchID.ToString()));

            if (StorageID != -1)
            {
                strSql.AppendLine(" and StorageID=@StorageID  ");
                cmdsql.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", StorageID.ToString()));
            }
            //指定命令的SQL文
            cmdsql.CommandText = strSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(cmdsql);

            //SqlParameter[] param = { 
            //                        new SqlParameter("@StorageID",StorageID ),
            //                        new SqlParameter("@ProductID",ProductID ),
            //                        new SqlParameter("@CompanyCD",strCompanyCD ),
            //                        new SqlParameter("@BranchID",branchID )
            //                       };

            //return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 根据商品ID，销售日期获取对应的促销政策(零售)
        /// <summary>
        /// 根据商品ID，销售日期获取对应的促销政策
        /// </summary>
        /// <param name="rowid">行号</param>
        /// <param name="productID">商品ID串</param>
        /// <param name="sellDate">销售日期</param>
        /// <param name="batchNo">批次</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetPromotionByPdtIDS(string rowid, string productID, DateTime? sellDate, string batchNo, string strCompanyCD, int branchID)
        {
            StringBuilder strSql = new StringBuilder();
            string[] rowidArr = rowid.Split(',');
            string[] pdtidArr = productID.Split(',');
            string[] batchNoArr = batchNo.Split(',');
            strSql.AppendLine(" create table #tempSellProTable (rowIndex int,productID int,batchNo varchar(50)) ");

            for (int iCount = 0; iCount < rowidArr.Length; iCount++)
            {
                strSql.AppendLine(" insert into #tempSellProTable (rowIndex,productID,batchNo)");
                strSql.AppendLine(" values(" + rowidArr[iCount] + "," + pdtidArr[iCount] + ",'" + batchNoArr[iCount] + "')");
            }

            strSql.AppendLine(" select a.rowIndex,b.SpecialPrice,c.ID,c.PromoName,c.PromotionType from #tempSellProTable a ");
            strSql.AppendLine(" left join officedba.SalesPromotionPlanDetail b on  b.BatchNo=a.batchNo and b.productID=a.productID  ");
            strSql.AppendLine(" left join officedba.SalesPromotionPlan c on c.PlanNo=b.PlanNo and b.CompanyCD=c.CompanyCD   ");
            strSql.AppendLine(" and c.ProStatus=1 and c.branchID=@BranchID and c.CompanyCD=@CompanyCD ");
            strSql.AppendLine(" and @SellDate>=c.StartDate and @SellDate<dateadd(day,1,Convert(datetime,c.EndDate)) ");

            SqlParameter[] param = { 
                                    new SqlParameter("@SellDate",sellDate ),
                                    new SqlParameter("@CompanyCD",strCompanyCD ),
                                    new SqlParameter("@BranchID",branchID ),
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);

        }
        #endregion
        #region 根据商品ID，销售日期,客户获取对应的促销政策(批发)
        /// <summary>
        /// 根据商品ID，销售日期，客户获取对应的促销政策
        /// </summary>
        /// <param name="rowid">行号</param>
        /// <param name="productID">商品ID串</param>
        /// <param name="sellDate">销售日期</param>
        /// <param name="custID">客户ID</param>
        /// <param name="batchNo">批次</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetPromotionByPdtIDS(string rowid, string productID, DateTime? sellDate, int custID, string batchNo, string strCompanyCD, int branchID)
        {
            StringBuilder strSql = new StringBuilder();
            string[] rowidArr = rowid.Split(',');
            string[] pdtidArr = productID.Split(',');
            string[] batchNoArr = batchNo.Split(',');
            strSql.AppendLine(" create table #tempTable (rowIndex int,productID int,batchNo varchar(50)) ");

            for (int iCount = 0; iCount < rowidArr.Length; iCount++)
            {
                strSql.AppendLine(" insert into #tempTable (rowIndex,productID,batchNo)");
                strSql.AppendLine(" values(" + rowidArr[iCount] + "," + pdtidArr[iCount] + ",'" + batchNoArr[iCount] + "')");
            }

            strSql.AppendLine(" select a.rowIndex,b.SpecialPrice,c.ID,c.PromoName,c.CustID from #tempTable a ");
            strSql.AppendLine(" left join officedba.SalesWholesalePlanDetail b on  b.BatchNo=a.batchNo and b.productID=a.productID  ");
            strSql.AppendLine(" left join officedba.SalesWholesalePlan c on c.PlanNo=b.PlanNo and b.CompanyCD=c.CompanyCD   ");
            strSql.AppendLine(" and c.ProStatus=1 and c.branchID=@BranchID and c.CompanyCD=@CompanyCD ");
            strSql.AppendLine(" and @SellDate>=c.StartDate and @SellDate<dateadd(day,1,Convert(datetime,c.EndDate)) and c.CustID=@CustID ");

            SqlParameter[] param = { 
                                new SqlParameter("@SellDate",sellDate ),
                                new SqlParameter("@CompanyCD",strCompanyCD ),
                                new SqlParameter("@BranchID",branchID ),
                                new SqlParameter("@CustID",custID )
                               };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);

        }
        #endregion


        #region 报表
        /// <summary>
        /// 销售结算表
        /// </summary>
        /// <param name="list">1.CustID 客户、2.StartDate 开始时间 、3.EndDate 结束时间、4.IsBack 是否含退货、5.只显示未完全核销的记录、6.CompanyCD 、7.BranchID </param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="order">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns></returns>
        public static DataTable GetSalesBalanceSheet(List<string> list, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            ArrayList SqlParamList = new ArrayList();

            SqlParamList.Add(new SqlParameter("@CompanyCD", list[5]));
            SqlParamList.Add(new SqlParameter("@BranchID", list[6]));

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendLine(" select c.DeptName, CustID,CustNO,CustName,OutNo as OrderNO ,convert(varchar(100),SellDate,23) as OrderDate,'销售' as OrderType, ");
            sqlStr.AppendLine(" TotalPrice,isnull(BlendPrice,0) BlendPrice, ");
            sqlStr.AppendLine(" isnull(TotalPrice,0)-isnull(BlendPrice,0) AccountsPrice,a.Remark,a.ID from officedba.SellOutStorage as a ");
            sqlStr.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID  where a.BillStatus=2   ");
            if (!String.IsNullOrEmpty(list[0]))
            {
                sqlStr.AppendLine(" and CustID=@CustID ");
                SqlParamList.Add(new SqlParameter("@CustID", Convert.ToInt32(list[0])));
            }
            if (!String.IsNullOrEmpty(list[1]))
            {
                sqlStr.AppendLine(" and SellDate>=Convert(datetime,@StartDate) ");
                SqlParamList.Add(new SqlParameter("@StartDate", list[1]));
            }
            if (!String.IsNullOrEmpty(list[2]))
            {
                sqlStr.AppendLine(" and SellDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
                SqlParamList.Add(new SqlParameter("@EndDate", list[2]));
            }
            if (list[4] == "1")
            {
                sqlStr.AppendLine(" and a.IsBlend<>2 ");
            }
            sqlStr.AppendLine(" and a.CompanyCD=@CompanyCD ");
            sqlStr.AppendLine(" and a.BranchID in (" + list[6] + ") ");

            if (list[3] == "1")
            {
                sqlStr.AppendLine(" union all ");

                sqlStr.AppendLine(" select c.DeptName,CustID,CustNO,CustName,BackNo as OrderNO,convert(varchar(100),BackDate,23) as OrderDate,'退货' as OrderType, ");
                sqlStr.AppendLine("  TotalPrice,isnull(BlendPrice,0) BlendPrice, ");
                sqlStr.AppendLine(" isnull(TotalPrice,0)-isnull(BlendPrice,0) AccountsPrice,a.Remark,a.ID  from officedba.SellBack as a ");
                sqlStr.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID  where a.BillStatus=2 ");
                if (!String.IsNullOrEmpty(list[0]))
                {
                    sqlStr.AppendLine(" and CustID=@CustID ");
                }
                if (!String.IsNullOrEmpty(list[1]))
                {
                    sqlStr.AppendLine(" and BackDate>=Convert(datetime,@StartDate) ");
                }
                if (!String.IsNullOrEmpty(list[2]))
                {
                    sqlStr.AppendLine(" and BackDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
                }
                if (list[4] == "1")
                {
                    sqlStr.AppendLine(" and a.IsBlend<>2 ");
                }
                sqlStr.AppendLine(" and a.CompanyCD=@CompanyCD ");
                sqlStr.AppendLine(" and a.BranchID in (" + list[6] + ") ");
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(sqlStr.ToString(), pageIndex, pageCount, order, SqlParamList, ref totalCount);

        }

        public static DataTable GetBlendBill(string sourceId, string sourceType, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            StringBuilder sqlStr = new StringBuilder();
            ArrayList SqlParamList = new ArrayList();
            SqlParamList.Add(new SqlParameter("@sourceId", Convert.ToInt32(sourceId)));
            if (sourceType == "1")
            {
                sqlStr.AppendLine(" select IncomeNo as orderNo ,convert(varchar(100),payDate,23) as date,payPrice  as price,convert(varchar(100),BlendDate,23) as BlendDate,BlendPrice from officedba.IncomeBlendBill as a ");
                sqlStr.AppendLine(" left join officedba.IncomeBill as b on b.id=a.IncomeID ");
                sqlStr.AppendLine(" where BillType=1 ");
                if (!String.IsNullOrEmpty(sourceId))
                {
                    sqlStr.AppendLine(" and a.sourceId=@sourceId ");
                }
            }
            else
            {
                sqlStr.AppendLine(" select  PayNo as orderNo,convert(varchar(100),payDate,23) as date,payPrice as price,convert(varchar(100),BlendDate,23) as BlendDate,BlendPrice from officedba.PayBlendBill as a  ");
                sqlStr.AppendLine(" left join officedba.PayBill as b on b.id=a.PayID ");
                sqlStr.AppendLine(" where BillType=1 ");
                if (!String.IsNullOrEmpty(sourceId))
                {
                    sqlStr.AppendLine(" and a.sourceId=@sourceId ");
                }
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(sqlStr.ToString(), pageIndex, pageCount, order, SqlParamList, ref totalCount);
        }

        public static DataTable GetInStorageListBycondition(XBase.Model.Office.PurchaseManager.PurchaseOutStorageModel model, string CustID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("select a.ID,a.OutNo,a.ProviderID,b.CustName as ProviderReal,a.StorageID,c.StorageName,isnull(a.TotalPrice,0) as TotalPrice ,a.CreateDate,");
            searchSql.AppendLine("		isnull( CONVERT(CHAR(10), a.OutDate, 23),'') as PurchaseDate,a.BillStatus,");
            searchSql.AppendLine("      case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '作废' end as strBillStatus,");
            searchSql.AppendLine("		isnull(a.TotalCount ,0) as InCount,isnull(f.employeeName,'') as EmployeeName,IsBlend,BlendPrice");
            searchSql.AppendLine("from officedba.PurchaseOutStorage a");
            searchSql.AppendLine("left join officedba.CustInfo b on a.ProviderID=b.ID");
            searchSql.AppendLine("left join officedba.StorageInfo c on a.StorageID=c.ID");
            searchSql.AppendLine("left join officedba.EmployeeInfo as f on a.UserID=f.ID");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and a.BranchID=@BranchID and a.IsBlend<>2 ");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", model.BranchID.ToString()));


            //单据编号
            if (!string.IsNullOrEmpty(model.OutNo))
            {
                searchSql.AppendLine(" and a.OutNo like @OutNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", "%" + model.OutNo + "%"));
            }
            //供应商

            if (model.ProviderID > 0)
            {
                searchSql.AppendLine(" and a.ProviderID=@ProviderID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderID", model.ProviderID.ToString()));
            }
            //仓库
            if (model.StorageID > 0)
            {
                searchSql.AppendLine("and a.StorageID=@StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID.ToString()));
            }

            //单据状态

            if (model.BillStatus > 0)
            {
                searchSql.AppendLine("and a.BillStatus=@BillStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus.ToString()));
            }

            if (!String.IsNullOrEmpty(CustID))
            {
                searchSql.AppendLine("and b.ID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            }

            //指定命令的SQL文

            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }

        #endregion

        #region 销售业绩统计分析
        /// <summary>
        /// 获得销售业绩统计分析数据
        /// </summary>
        /// <param name="info">0.BranchID 分店ID、1.CustID 客户ID、
        /// 2.UserID 业务员ID、3.StorageID 仓库、4 .StartDate 开始时间、
        /// 5.EndDate 结束时间、6.IsBack 是否含退货、7.分组字符串、
        /// 8.是否查询分店、9.统计类型:True:数量，false：金额、10.CompanyCD 客户</param>
        /// <returns></returns>
        public static DataTable GetSellAchievement(List<object> list)
        {
            string sql1 = " FROM officedba.SellOutStorage sos LEFT JOIN officedba.SellOutStorageDetail sosd ON sos.OutNo=sosd.OutNo ";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" SELECT  COUNT({0}) AS AxisX ", Convert.ToBoolean(list[9]) ? "sosd.DetailCount" : "sosd.DetailTotalPrice");

            #region 分组字段
            string group = "di.DeptName";
            switch (Convert.ToString(list[7]))
            {
                case "1":// 按客户
                    group = "sos.CustID";
                    sb.AppendFormat(",{0} AS AxisY ", group);
                    sb.Append(sql1);
                    break;
                case "2":// 按品名
                    group = "sosd.ProductID";
                    sb.AppendFormat(",{0} AS AxisY ", group);
                    sb.Append(sql1);
                    break;
                case "3":// 按业务员
                    group = "sos.UserID";
                    sb.AppendFormat(",{0} AS AxisY ", group);
                    sb.Append(sql1);
                    break;
                case "4":// 按分店
                    group = "di.DeptName";
                    sb.AppendFormat(",{0} AS AxisY ", group);
                    sb.Append(sql1);
                    sb.Append(" LEFT JOIN officedba.DeptInfo di ON sos.CompanyCD=di.CompanyCD AND sos.BranchID=di.ID ");
                    break;
                case "5":// 按仓库
                    group = "sos.StorageID";
                    sb.AppendFormat(",{0} AS AxisY ", group);
                    sb.Append(sql1);
                    break;
            }
            #endregion

            sb.AppendFormat(" WHERE sos.CompanyCD='{0}' ", list[10]);

            // 开始时间
            if (list[4] != null)
            {
                sb.AppendFormat(" AND SellDate>=Convert(datetime,'{0}') ", list[4]);
            }
            // 结束时间
            if (list[5] != null)
            {
                sb.AppendFormat(" AND SellDate<dateadd(day,1,Convert(datetime,'{0}'))  ", list[5]);
            }
            // 仓库
            if (Convert.ToInt32(list[3]) > 0)
            {
                sb.AppendFormat(" AND sos.StorageID={0} ", Convert.ToInt32(list[3]));
            }
            // 业务员
            if (Convert.ToInt32(list[2]) > 0)
            {
                sb.AppendFormat(" AND sos.UserID={0} ", Convert.ToInt32(list[2]));
            }

            // 客户
            if (Convert.ToInt32(list[1]) > 0)
            {
                sb.AppendFormat(" AND sos.CustID={0} ", Convert.ToInt32(list[1]));
            }

            // 分组
            sb.AppendFormat(" GROUP BY {0} ", group);

            // 返回查询结果
            return SqlHelper.ExecuteSql(sb.ToString());
        }

        #endregion
    }
}
