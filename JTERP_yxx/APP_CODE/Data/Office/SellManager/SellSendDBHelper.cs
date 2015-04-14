using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using System.Data.Sql;
using XBase.Model.Office.SellManager;
using XBase.Common;
using XBase.Data.Common;

namespace XBase.Data.Office.SellManager
{
    public class SellSendDBHelper
    {
        #region 添加、修改、删除相关操作
        
        #region 保存销售发货单
        /// <summary>
        /// 保存销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool SaveSellSend(Hashtable ht, SellSendModel sellSendModel, List<SellSendDetailModel> sellSendDetailModellList, out string strMsg, string[] strCustomdetail)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            //判断单据编号是否存在
            if (NoIsExist(sellSendModel.SendNo))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    InsertSellSend(ht,sellSendModel, tran);
                    InsertSellSendDetail(sellSendDetailModellList, tran,strCustomdetail);
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

        #region 更新销售发货单
        /// <summary>
        /// 更新销售发货单
        /// </summary>
        /// <returns></returns>
        public static bool UpdateSellSend(Hashtable ht, SellSendModel sellSendModel, List<SellSendDetailModel> sellSendDetailModellList, out string strMsg, string[] strCustomdetail)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellSendModel.SendNo))
            {
                string strSql = "delete from officedba.SellSendDetail where  SendNo=@SendNo  and CompanyCD=@CompanyCD";
                SqlParameter[] paras = { new SqlParameter("@SendNo", sellSendModel.SendNo), new SqlParameter("@CompanyCD", sellSendModel.CompanyCD) };
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {

                    UpdateSellSend(ht,sellSendModel, tran);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    InsertSellSendDetail(sellSendDetailModellList, tran, strCustomdetail);
                    tran.Commit();
                    strMsg = "保存成功！";
                    isSucc = true;
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
                strMsg = "非制单状态的未提交审批、审批未通过或撤销审批单据不可修改！";
            }
            return isSucc;
        }
        #endregion
        #region 更新主表数据
        /// <summary>
        /// 更新主表数据
        /// </summary>
        /// <param name="sellBackModel"></param>
        /// <param name="tran"></param>
        private static void UpdateSellSend(Hashtable htExtAttr, SellSendModel sellSendModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update officedba.SellSend set ");
            strSql.Append("CustID=@CustID,");
            strSql.Append("Title=@Title,");
            strSql.Append("FromType=@FromType,");
            strSql.Append("SellType=@SellType,");
            strSql.Append("BusiType=@BusiType,");
            strSql.Append("PayType=@PayType,");
            strSql.Append("MoneyType=@MoneyType,");
            strSql.Append("TakeType=@TakeType,");
            strSql.Append("CarryType=@CarryType,");
            strSql.Append("SendAddr=@SendAddr,");
            strSql.Append("Sender=@Sender,");
            strSql.Append("ReceiveAddr=@ReceiveAddr,");
            strSql.Append("Receiver=@Receiver,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Modile=@Modile,");
            strSql.Append("Post=@Post,");
            strSql.Append("IntendSendDate=@IntendSendDate,");
            strSql.Append("Transporter=@Transporter,");
            strSql.Append("TransportFee=@TransportFee,");
            strSql.Append("TransPayType=@TransPayType,");
            strSql.Append("CurrencyType=@CurrencyType,");
            strSql.Append("Rate=@Rate,");
            strSql.Append("TotalPrice=@TotalPrice,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("TotalFee=@TotalFee,");
            strSql.Append("Discount=@Discount,");
            strSql.Append("PayRemark=@PayRemark,");
            strSql.Append("DeliverRemark=@DeliverRemark,");
            strSql.Append("PackTransit=@PackTransit,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("BillStatus=@BillStatus,");
            strSql.Append("ModifiedDate=getdate(),");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("SellDeptId=@SellDeptId,");
            strSql.Append("DiscountTotal=@DiscountTotal,");
            strSql.Append("RealTotal=@RealTotal,");
            strSql.Append("isAddTax=@isAddTax,");
            strSql.Append("CountTotal=@CountTotal,");
            strSql.Append("Seller=@Seller,");
            strSql.Append("FromBillID=@FromBillID");
            strSql.Append(",ProjectID=@ProjectID");
            strSql.Append(",CanViewUser=@CanViewUser");
            strSql.Append(" where CompanyCD=@CompanyCD and SendNo=@SendNo ");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellSendModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellSendModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendNo", sellSendModel.SendNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellSendModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellSendModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellSendModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellSendModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellSendModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellSendModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TakeType", sellSendModel.TakeType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellSendModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendAddr", sellSendModel.SendAddr));
            lcmd.Add(SqlHelper.GetParameterFromString("@Sender", sellSendModel.Sender.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ReceiveAddr", sellSendModel.ReceiveAddr));
            lcmd.Add(SqlHelper.GetParameterFromString("@Receiver", sellSendModel.Receiver));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tel", sellSendModel.Tel));
            lcmd.Add(SqlHelper.GetParameterFromString("@Modile", sellSendModel.Modile));
            lcmd.Add(SqlHelper.GetParameterFromString("@Post", sellSendModel.Post));
            lcmd.Add(SqlHelper.GetParameterFromString("@IntendSendDate", sellSendModel.IntendSendDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Transporter", sellSendModel.Transporter.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TransportFee", sellSendModel.TransportFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TransPayType", sellSendModel.TransPayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellSendModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellSendModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellSendModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellSendModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellSendModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellSendModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayRemark", sellSendModel.PayRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeliverRemark", sellSendModel.DeliverRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PackTransit", sellSendModel.PackTransit));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellSendModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellSendModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellSendModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellSendModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellSendModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellSendModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellSendModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellSendModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellSendModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellSendModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellSendModel.ProjectID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CanViewUser", sellSendModel.CanViewUser));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellSend set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and SendNo=@SendNo ");
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
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }

            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion

        #region 获取当前单据的id
        /// <summary>
        /// 获取当前单据的id
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static int GetOrderID(string orderNo)
        {
            int OrderID = 0;
            string sql = string.Empty;
            string strCompanyCD = string.Empty;//单位编号

            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           

            sql = " select ID from officedba.SellSend where CompanyCD=@CompanyCD and SendNo=@SendNo ";
            SqlParameter[] paras = { new SqlParameter("@CompanyCD", strCompanyCD), new SqlParameter("@SendNo", orderNo) };
            OrderID = (int)SqlHelper.ExecuteScalar(sql, paras);
            return OrderID;
        }
        #endregion

        #region 为主表插入数据
        /// <summary>
        /// 为主表插入数据
        /// </summary>
        /// <param name="sellSendModel"></param>
        /// <param name="tran"></param>
        private static void InsertSellSend(Hashtable htExtAttr, SellSendModel sellSendModel, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.SellSend(");
            strSql.Append("CompanyCD,CustID,SendNo,FromBillID,Title,FromType,SellType,BusiType,PayType,MoneyType,TakeType,CarryType,SendAddr,Sender,ReceiveAddr,Receiver,Tel,Modile,Post,IntendSendDate,Transporter,TransportFee,TransPayType,CurrencyType,Rate,TotalPrice,Tax,TotalFee,Discount,PayRemark,DeliverRemark,PackTransit,Remark,BillStatus,Creator,CreateDate,Confirmor,ConfirmDate,Closer,CloseDate,ModifiedDate,ModifiedUserID,SellDeptId,DiscountTotal,RealTotal,isAddTax,CountTotal,Seller,ProjectID,CanViewUser)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@CustID,@SendNo,@FromBillID,@Title,@FromType,@SellType,@BusiType,@PayType,@MoneyType,@TakeType,@CarryType,@SendAddr,@Sender,@ReceiveAddr,@Receiver,@Tel,@Modile,@Post,@IntendSendDate,@Transporter,@TransportFee,@TransPayType,@CurrencyType,@Rate,@TotalPrice,@Tax,@TotalFee,@Discount,@PayRemark,@DeliverRemark,@PackTransit,@Remark,@BillStatus,@Creator,@CreateDate,@Confirmor,@ConfirmDate,@Closer,@CloseDate,@ModifiedDate,@ModifiedUserID,@SellDeptId,@DiscountTotal,@RealTotal,@isAddTax,@CountTotal,@Seller,@ProjectID,@CanViewUser)");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellSendModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellSendModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendNo", sellSendModel.SendNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@Title", sellSendModel.Title));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromType", sellSendModel.FromType));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellSendModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@BusiType", sellSendModel.BusiType));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayType", sellSendModel.PayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@MoneyType", sellSendModel.MoneyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TakeType", sellSendModel.TakeType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CarryType", sellSendModel.CarryType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SendAddr", sellSendModel.SendAddr));
            lcmd.Add(SqlHelper.GetParameterFromString("@Sender", sellSendModel.Sender.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ReceiveAddr", sellSendModel.ReceiveAddr));
            lcmd.Add(SqlHelper.GetParameterFromString("@Receiver", sellSendModel.Receiver));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tel", sellSendModel.Tel));
            lcmd.Add(SqlHelper.GetParameterFromString("@Modile", sellSendModel.Modile));
            lcmd.Add(SqlHelper.GetParameterFromString("@Post", sellSendModel.Post));
            lcmd.Add(SqlHelper.GetParameterFromString("@IntendSendDate", sellSendModel.IntendSendDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Transporter", sellSendModel.Transporter.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TransportFee", sellSendModel.TransportFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TransPayType", sellSendModel.TransPayType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CurrencyType", sellSendModel.CurrencyType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Rate", sellSendModel.Rate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellSendModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Tax", sellSendModel.Tax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalFee", sellSendModel.TotalFee.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Discount", sellSendModel.Discount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@PayRemark", sellSendModel.PayRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@DeliverRemark", sellSendModel.DeliverRemark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PackTransit", sellSendModel.PackTransit));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellSendModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellSendModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellSendModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CreateDate", sellSendModel.CreateDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Confirmor", sellSendModel.Confirmor.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ConfirmDate", sellSendModel.ConfirmDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Closer", sellSendModel.Closer.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CloseDate", sellSendModel.CloseDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedDate", sellSendModel.ModifiedDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ModifiedUserID", sellSendModel.ModifiedUserID));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDeptId", sellSendModel.SellDeptId.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@DiscountTotal", sellSendModel.DiscountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@RealTotal", sellSendModel.RealTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@isAddTax", sellSendModel.isAddTax.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CountTotal", sellSendModel.CountTotal.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Seller", sellSendModel.Seller.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellSendModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ProjectID", sellSendModel.ProjectID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CanViewUser", sellSendModel.CanViewUser));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellSend set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and SendNo=@SendNo ");
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
                if (para.Value == null)
                {
                    para.Value = DBNull.Value;
                }
            }
            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), param);
        }
        #endregion
        #region 为明细表插入数据
        /// <summary>
        /// 为明细表插入数据
        /// </summary>
        /// <param name="sellSendDetailModellList"></param>
        /// <param name="tran"></param>
        private static void InsertSellSendDetail(List<SellSendDetailModel> sellSendDetailModellList, TransactionManager tran, string[] strCustomdetail)
        {
            int j = 1;
            foreach (SellSendDetailModel sellSendDetailModel in sellSendDetailModellList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellSendDetail(");
                strSql.Append("CompanyCD,SendNo,SortNo,ProductID,ProductCount,UnitID,UnitPrice,TotalPrice,Discount,SendDate,Package,Remark,FromType,FromBillID,FromLineNo,BackCount,OutCount,ModifiedDate,ModifiedUserID,TaxPrice,TaxRate,TotalFee,TotalTax,UsedUnitID,UsedUnitCount,UsedPrice,ExRate,StorageID,PieceCount,TotalNumber,ProductAlias");
                //添加扩展项
                strSql.Append(GetBillTableCellsDBHelper.insertCustomFilder(strCustomdetail, j));
                strSql.Append(") values (");
                strSql.Append("@CompanyCD,@SendNo,@SortNo,@ProductID,@ProductCount,@UnitID,@UnitPrice,@TotalPrice,@Discount,@SendDate,@Package,@Remark,@FromType,@FromBillID,@FromLineNo,@BackCount,@OutCount,@ModifiedDate,@ModifiedUserID,@TaxPrice,@TaxRate,@TotalFee,@TotalTax,@UsedUnitID,@UsedUnitCount,@UsedPrice,@ExRate,@StorageID,@PieceCount,@TotalNumber,@ProductAlias");
                //扩展项值
                strSql.Append(GetBillTableCellsDBHelper.insertCustomvalue(strCustomdetail, j));
                strSql.Append(")");
                j++;
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@SendNo", SqlDbType.VarChar,50),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductCount", SqlDbType.Decimal,9),
					new SqlParameter("@UnitID", SqlDbType.Int,4),
					new SqlParameter("@UnitPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TotalPrice", SqlDbType.Decimal,9),
					new SqlParameter("@Discount", SqlDbType.Decimal,5),
					new SqlParameter("@SendDate", SqlDbType.DateTime),
					new SqlParameter("@Package", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,200),
					new SqlParameter("@FromType", SqlDbType.Char,1),
					new SqlParameter("@FromBillID", SqlDbType.Int,4),
					new SqlParameter("@FromLineNo", SqlDbType.Int,4),
					new SqlParameter("@BackCount", SqlDbType.Decimal,9),
					new SqlParameter("@OutCount", SqlDbType.Decimal,9),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,20),
					new SqlParameter("@TaxPrice", SqlDbType.Decimal,9),
					new SqlParameter("@TaxRate", SqlDbType.Decimal,5),
					new SqlParameter("@TotalFee", SqlDbType.Decimal,9),
					new SqlParameter("@TotalTax", SqlDbType.Decimal,9),
					new SqlParameter("@UsedUnitID", SqlDbType.Int,4),
					new SqlParameter("@UsedUnitCount", SqlDbType.Decimal,9),
					new SqlParameter("@UsedPrice", SqlDbType.Decimal,9),
					new SqlParameter("@ExRate", SqlDbType.Decimal,9),
                    new SqlParameter("@StorageID", SqlDbType.Int,4),
                    new SqlParameter("@PieceCount", SqlDbType.VarChar,50),
                    new SqlParameter("@TotalNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@ProductAlias", SqlDbType.VarChar,100)};
                parameters[0].Value = sellSendDetailModel.CompanyCD;
                parameters[1].Value = sellSendDetailModel.SendNo;
                parameters[2].Value = sellSendDetailModel.SortNo;
                parameters[3].Value = sellSendDetailModel.ProductID;
                parameters[4].Value = sellSendDetailModel.ProductCount;
                parameters[5].Value = sellSendDetailModel.UnitID;
                parameters[6].Value = sellSendDetailModel.UnitPrice;
                parameters[7].Value = sellSendDetailModel.TotalPrice;
                parameters[8].Value = sellSendDetailModel.Discount;
                parameters[9].Value = sellSendDetailModel.SendDate;
                parameters[10].Value = sellSendDetailModel.Package;
                parameters[11].Value = sellSendDetailModel.Remark;
                parameters[12].Value = sellSendDetailModel.FromType;
                parameters[13].Value = sellSendDetailModel.FromBillID;
                parameters[14].Value = sellSendDetailModel.FromLineNo;
                parameters[15].Value = sellSendDetailModel.BackCount;
                parameters[16].Value = sellSendDetailModel.OutCount;
                parameters[17].Value = sellSendDetailModel.ModifiedDate;
                parameters[18].Value = sellSendDetailModel.ModifiedUserID;
                parameters[19].Value = sellSendDetailModel.TaxPrice;
                parameters[20].Value = sellSendDetailModel.TaxRate;
                parameters[21].Value = sellSendDetailModel.TotalFee;
                parameters[22].Value = sellSendDetailModel.TotalTax;
                parameters[23].Value = sellSendDetailModel.UsedUnitID;
                parameters[24].Value = sellSendDetailModel.UsedUnitCount;
                parameters[25].Value = sellSendDetailModel.UsedPrice;
                parameters[26].Value = sellSendDetailModel.ExRate;
                parameters[27].Value = sellSendDetailModel.StorageID;
                parameters[28].Value = sellSendDetailModel.PieceCount;
                parameters[29].Value = sellSendDetailModel.TotalNumber;
                parameters[30].Value = sellSendDetailModel.ProductAlias;
                foreach (SqlParameter para in parameters)
                {
                    if (para.Value == null)
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }

        }
        #endregion

        #region 删除销售发货单
        /// <summary>
        /// 删除销售发货单
        /// </summary>
        /// <param name="orderNos"></param>
        /// <returns></returns>
        public static bool DelOrder(string orderNos, out string strMsg, out string strFieldText)
        {
            string strCompanyCD = string.Empty;//单位编号
            bool isSucc = false;
            string allOrderNo = "";
            strMsg = "";
            strFieldText = "";
            bool bTemp = false;//单据是否可以被删除
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            TransactionManager tran = new TransactionManager();
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
          
            string[] orderNoS = null;
            orderNoS = orderNos.Split(',');

            for (int i = 0; i < orderNoS.Length; i++)
            {
                if (!IsFlow(orderNoS[i]))
                {
                    strFieldText += "单据：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的单据不允许删除！|";
                    bTemp = true;
                }
                else if (!isHandle(orderNoS[i], "1"))
                {
                    strFieldText += "单据：" + orderNoS[i] + "|";
                    strMsg += "已提交审批或已确认后的单据不允许删除！|";
                    bTemp = true;
                }
                orderNoS[i] = "'" + orderNoS[i] + "'";
                sb.Append(orderNoS[i]);
            }

            allOrderNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellSend WHERE SendNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellSendDetail WHERE SendNo IN ( " + allOrderNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
        #endregion

        #region 获取发货单信息相关操作
        #region 获取发货单列表
        /// <summary>
        /// 获取发货单列表 
        /// </summary>
        /// <param name="sellContractModel">sellContractModel表实体</param>
        /// <param name="FlowStatus">审批状态</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SellSendModel sellSendModel, int? FlowStatus, string EFIndex, string EFDesc, int pageIndex, int pageCount, string ord, ref int TotalCount,string time1,string time2,string prodid,string custid,string EFIndexP,string EFDescP)
        {
            string strSql = string.Empty;
            string sellpoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
            strSql = "select * from (SELECT s.SendNo,p.productname,Convert(decimal(22," + sellpoint + "), isnull(de.productcount,0)) productcount,Convert(decimal(22," + sellpoint + "), isnull(de.unitprice,0)) unitprice,Convert(decimal(22," + sellpoint + "), isnull(de.totalprice,0)) totalprice, ";
            strSql += " s.ID,s.ModifiedDate,CONVERT(varchar(100), s.CreateDate, 23) as CreateDate, s.Title, ISNULL(so.OrderNo, '') AS OrderNo, s.Receiver, ";
            strSql += "ISNULL(e1.EmployeeName, '') AS SenderName, ISNULL(e2.EmployeeName,'') AS SellerName, ";
            strSql += "ISNULL(c.CustName, '') AS CustName, CASE s.FromType WHEN 0 THEN '无来源' WHEN 1 THEN '销售订单' END AS FromTypeText, ";
            strSql += "CASE s.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, ";
            strSql += "CASE WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc )IS NULL THEN '' ";
            strSql += "WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc )=1 THEN ";
            strSql += "'待审批' WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc ) = 2 THEN '审批中' ";
            strSql += " WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc ) = 3 THEN '审批通过' ";
            strSql += "WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += "where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc )=4 THEN '审批不通过' ";
            strSql += " WHEN (select top 1 FlowStatus from officedba.FlowInstance ";
            strSql += " where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc ) = 5 THEN ";
            strSql += "'撤销审批' END AS FlowInstanceText, isnull(CASE ((SELECT count(1) ";
            strSql += "FROM officedba.SellBack AS sb WHERE sb.FromType = '1' AND sb.FromBillID = s.ID) + ";
            strSql += "(SELECT count(1) FROM officedba.StorageOutSell AS soo ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID) + ";
            strSql += "(SELECT count(1) FROM officedba.SellChannelSttl AS scs ";
            strSql += "WHERE scs.FromType = '1' AND scs.FromBillID = s.ID) + ";
            strSql += "(SELECT count(1) FROM officedba.SellChannelSttlDetail AS scsd ";
            strSql += "WHERE scsd.FromType = '1' AND scsd.FromBillID = s.ID) + ";
            strSql += "(SELECT count(1)  FROM officedba.SellBackDetail AS sbd WHERE sbd.FromType = '1' AND sbd.FromBillID = s.ID)) ";
            strSql += "WHEN 0 THEN '无引用' END, '被引用') AS RefText, isnull(cb.TypeName, '') AS TypeName,(select top 1 FlowStatus from officedba.FlowInstance where BillID=s.ID  AND BillTypeFlag = 5 AND BillTypeCode = 4 order by ModifiedDate desc )as FlowStatus ";
            strSql += "FROM officedba.SellSend AS s left join officedba.SellSenddetail de on s.companycd=de.companycd and s.sendno=de.sendno left join officedba.productinfo p on p.id=de.productid  LEFT OUTER JOIN ";
            strSql += "officedba.CodePublicType AS cb ON s.TakeType = cb.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellOrder AS so ON s.FromBillID = so.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON s.Sender = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON s.Seller = e2.ID LEFT OUTER JOIN ";

            strSql += "officedba.CustInfo AS c ON s.CustID = c.ID where  s.CompanyCD=@CompanyCD     ";
            strSql += " and ( ";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dtt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dtt != null && dtt.Rows.Count > 0)
            {
               
                if (dtt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) OR  ";
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) ))  OR  ";
                }
                if (dtt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql += " (s.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID)))  OR  ";
                }
            }
            strSql += " (select COUNT(*) from officedba.FlowTaskHistory where FlowNo=(SELECT TOP 1 FlowNo FROM officedba.FlowInstance WHERE BillID = s.ID AND BillTypeFlag = 5 AND BillTypeCode = 4) AND BillID = s.ID  AND operateUserId = '" + userInfo.UserID + "')>0 ";

            strSql += "or (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.Seller IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.Sender IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "'))";

            strSql += " or ( charindex('," + sellSendModel.Creator + ",' , ','+s.CanViewUser+',')>0  )) ";

            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList arr = new ArrayList();

            //扩展属性
            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                strSql += " and s.ExtField" + EFIndex + " like @EFDesc ";
                arr.Add(new SqlParameter("@EFDesc", "%" + EFDesc + "%"));
            }
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            if (sellSendModel.ProjectID != null)
            {
                strSql += " and s.ProjectID=@ProjectID ";
                arr.Add(new SqlParameter("@ProjectID", sellSendModel.ProjectID));
            }
            if (sellSendModel.BillStatus != null)
            {
                strSql += " and s.BillStatus= @BillStatus";
                arr.Add(new SqlParameter("@BillStatus", sellSendModel.BillStatus));
            }
            if (sellSendModel.FromBillID != null)
            {
                strSql += " and s.FromBillID=@FromBillID";
                arr.Add(new SqlParameter("@FromBillID", sellSendModel.FromBillID)); ;
            }
            //if (sellSendModel.FromType != null)
            //{
            //    strSql += " and s.FromType=@FromType";
            //    arr.Add(new SqlParameter("@FromType", sellSendModel.FromType));
            //}

            if (sellSendModel.SendNo != null)
            {
                strSql += " and s.SendNo like @SendNo";
                arr.Add(new SqlParameter("@SendNo", "%" + sellSendModel.SendNo + "%"));
            }
            if (sellSendModel.Receiver != null)
            {
                if (FlowStatus != 0)
                {
                    strSql += " and s.Receiver like @Receiver";
                    arr.Add(new SqlParameter("@Receiver", "%" + sellSendModel.Receiver + "%"));
                }
                else
                {
                    strSql += " and f.Receiver is null ";
                }
            }
            if (sellSendModel.Sender != null)
            {
                strSql += " and s.Sender=@Sender";
                arr.Add(new SqlParameter("@Sender", sellSendModel.Sender));
            }
            if (sellSendModel.Title != null)
            {
                strSql += " and s.Title like @Title";
                arr.Add(new SqlParameter("@Title", "%" + sellSendModel.Title + "%"));
            }
            if (sellSendModel.TakeType != null)
            {
                strSql += " and s.TakeType=@TakeType";
                arr.Add(new SqlParameter("@TakeType", sellSendModel.TakeType));
            }
            if (sellSendModel.Seller != null)
            {
                strSql += " and s.Seller=@Seller";
                arr.Add(new SqlParameter("@Seller", sellSendModel.Seller));
            }
            if (time1 != "" && time2 != "")
            {
                strSql += " and s.CreateDate between '" + time1 + " 0:00:00 ' and '" + time2 + " 23:59:59 ' ";
            }
            else if (time1 == "" && time2 != "")
            {
                strSql += " and s.CreateDate<='" + time2 + " 23:59:59' ";
            }
            else if (time1 != "" && time2 == "")
            {
                strSql += " and s.CreateDate>='" + time1 + " 0:00:00' ";
            }

            if (prodid != "")
            {
                strSql += " and p.productname like'%" + prodid + "%'";
            }
            //扩展属性
            if (!string.IsNullOrEmpty(EFIndexP) && !string.IsNullOrEmpty(EFDescP))
            {
                strSql += " and p.ExtField" + EFIndexP + " like @EFDescP ";
                arr.Add(new SqlParameter("@EFDescP", "%" + EFDescP + "%"));
            }
            if (custid != "")
            {
                strSql += " and c.CustName='" + custid + "'";
            }
            strSql += " ) as f  where 1=1 ";
            if (FlowStatus != null)
            {
                if (FlowStatus != 0)
                {
                    strSql += " and f.FlowStatus=@FlowStatus";
                    arr.Add(new SqlParameter("@FlowStatus", FlowStatus));
                }
                else
                {
                    strSql += " and f.FlowStatus is null ";
                }

            }

            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arr, ref TotalCount);
        }
        #endregion

        #region 获取单据明细信息
        /// <summary>
        /// 获取单据明细信息
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static DataTable GetOrderDetail(string orderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                string strSql = @"SELECT isnull(ssd.Custom1,'')Custom1,isnull(ssd.Custom2,'')Custom2,isnull(ssd.Custom3,'')Custom3,isnull(ssd.Custom4,'')Custom4,isnull(ssd.Custom5,'')Custom5,isnull(ssd.Custom6,'')Custom6,isnull(ssd.Custom7,'')Custom7,isnull(ssd.Custom8,'')Custom8,isnull(ssd.Custom9,'')Custom9,isnull(ssd.Custom10,'')Custom10
                    ,p.Size,ssd.PieceCount,ssd.TotalNumber,ssd.StorageID,p.Specification, p.ProductName, c.CodeName, ISNULL(p.SellTax, 0) AS SellTax,                                                       ";
                strSql += "ISNULL(p.TaxRate, 0) AS TaxRate1, p.ProdNo, ssd.ProductID,ssd.ProductAlias,isnull(pc.TypeName,'') as ColorName,   ";
            strSql += "ssd.ProductCount, ssd.UnitID, ssd.UnitPrice, ssd.TotalPrice, ssd.Discount,                                                                ";
            strSql += "CONVERT(varchar(100), ssd.SendDate, 23) AS SendDate, ssd.Package,                                                                         ";
            strSql += "ssd.Remark, ssd.FromType, ssd.FromBillID, CASE ssd.FromType WHEN '0' THEN '无来源'                                                        ";
            strSql += "WHEN '1' THEN '销售订单' END AS FromTypeText,                                                                                             ";
            strSql += " ssd.UsedUnitID,isnull(ssd.UsedUnitCount,0) as UsedUnitCount,isnull(ssd.UsedPrice,0) as UsedPrice,isnull(ssd.ExRate,1) as ExRate,c2.CodeName as UsedUnitName, ";//单位，单价，数量，换算率
            strSql += "ssd.FromLineNo, ISNULL(ssd.BackCount, 0) AS BackCount, ISNULL(ssd.OutCount, 0)                                                            ";
            strSql += "AS OutCount, ssd.TaxPrice, ssd.TaxRate, ssd.TotalFee,    isnull(p.StandardCost,0) as StandardCost,             ";
            strSql += "ssd.TotalTax, ISNULL(ssd.SttlCount, 0) AS SttlCount,isnull( (SELECT SendCount FROM officedba.SellOrderDetail AS sod WHERE (OrderNo =      ";
            strSql += "(SELECT OrderNo FROM officedba.SellOrder AS so                                                                                            ";
            strSql += "WHERE (ID = ssd.FromBillID))) AND (SortNo = ssd.FromLineNo) AND (CompanyCD = ssd.CompanyCD)),0) AS transactCount,                         ";
            //订单数量（对应基本单位的基本数量）
            strSql += "(SELECT ProductCount FROM officedba.SellOrderDetail AS sod WHERE (OrderNo =   ";
            strSql += "(SELECT OrderNo FROM officedba.SellOrder AS so          ";
            strSql += "WHERE (ID = ssd.FromBillID))) AND (SortNo = ssd.FromLineNo) AND (CompanyCD = ssd.CompanyCD)) AS OrderCount ,   ";
            //订单数量（对应单位的数量）
            strSql += "(SELECT UsedUnitCount FROM officedba.SellOrderDetail AS sod WHERE (OrderNo =  ";
            strSql += "(SELECT OrderNo FROM officedba.SellOrder AS so   ";
            strSql += "WHERE (ID = ssd.FromBillID))) AND (SortNo = ssd.FromLineNo) AND (CompanyCD = ssd.CompanyCD)) AS UsedOrderCount , ";
            //订单数量（对应单位的数量)END
            strSql += "(SELECT OrderNo FROM officedba.SellOrder AS so                                                                                            ";
            strSql += "WHERE (ID = ssd.FromBillID) ) as OrderNo                                                                                                  ";
            strSql += "FROM officedba.SellSendDetail AS ssd LEFT OUTER JOIN                                                                                      ";
            strSql += "officedba.ProductInfo AS p ON ssd.ProductID = p.ID LEFT OUTER JOIN                                                                        ";
            strSql += " officedba.CodePublicType as pc on pc.ID=p.ColorID left join ";
            strSql += "officedba.CodeUnitType AS c ON ssd.UnitID = c.ID                                                                                          ";
            strSql += " left join officedba.CodeUnitType as c2 on ssd.UsedUnitID=c2.ID ";
            strSql += " left join (select a.id,b.SortNo,b.PieceCount,b.TotalNumber from officedba.SellOrder a left join officedba.SellOrderDetail b  on a.orderno=b.orderno and a.companycd=b.companycd";
            strSql += " where a.companycd=@CompanyCD) sell on sell.id=ssd.FromBillID and sell.SortNo=ssd.FromLineNo ";
            strSql += "WHERE (ssd.SendNo = @SendNo) AND (ssd.CompanyCD = @CompanyCD) ";
            strSql += "ORDER BY ssd.SortNo ";

            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@SendNo", orderNo);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion
        #region 获取发货单主表信息
        /// <summary>
        /// 获取发货单主表信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public static DataTable GetOrderInfo(int orderID)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                string strSql = "SELECT ct.CurrencyName, so.OrderNo, c.CustName, oc.CustName AS TransporterName, ";
            strSql += "d.DeptName, e5.EmployeeName AS SellerName, e1.EmployeeName AS SenderName, ";
            strSql += "e2.EmployeeName AS CreatorName, e3.EmployeeName AS ConfirmorName, e4.EmployeeName AS CloserName, ";
            strSql += "ss.ExtField1,ss.ExtField2,ss.ExtField3,ss.ExtField4,ss.ExtField5,";
            strSql += "ss.ExtField6,ss.ExtField7,ss.ExtField8,ss.ExtField9,ss.ExtField10, ";
            strSql += "ss.CustID, ss.Title, ss.FromType, ss.FromBillID, ss.BusiType, ss.SellType, ";
            strSql += "ss.PayType, ss.MoneyType, ss.TakeType, ss.CarryType, ss.SendAddr, ";
            strSql += "ss.Sender, ss.ReceiveAddr, ss.Receiver, ss.Tel, ss.Modile, ss.Post, ";
            strSql += "CONVERT(varchar(100), ss.IntendSendDate, 23) AS IntendSendDate, ";
            strSql += "ss.Transporter, ss.TransportFee, ss.TransPayType, ss.CurrencyType, ";
            strSql += "ss.Rate, ss.TotalPrice, ss.Tax, ss.TotalFee, ss.Discount, ss.PayRemark, ";
            strSql += "ss.DeliverRemark, ss.PackTransit, ss.Remark, ";
            strSql += "ss.Creator, ss.BillStatus, CONVERT(varchar(100), ss.CreateDate, 23) ";
            strSql += "AS CreateDate, CONVERT(varchar(100), ss.ConfirmDate, 23) AS ConfirmDate, ";
            strSql += "ss.Confirmor, ss.Closer, CONVERT(varchar(100), ss.CloseDate, 23) ";
            strSql += "AS CloseDate, CONVERT(varchar(100), ss.ModifiedDate, 23) AS ModifiedDate, ";
            strSql += "ss.ModifiedUserID, ss.SellDeptId, ss.DiscountTotal, ss.RealTotal, ";
            strSql += "ss.isAddTax, ss.CountTotal,  ss.Seller, ";
            strSql += " ss.ProjectID,p.ProjectName, ";
            strSql += "CASE ss.BillStatus WHEN 1 THEN '制单' WHEN 2 THEN '执行' WHEN 3 THEN '变更' ";
            strSql += "WHEN 4 THEN '手工结单' WHEN 5 THEN '自动结单' END AS BillStatusText, ss.SendNo,ss.ID ";
            strSql += ",ss.CanViewUser,[dbo].[getEmployeeNameString](ss.CanViewUser) as CanViewUserName,isnull(ss.isOpenbill,'0') as isOpenbill ";
            strSql += "FROM officedba.SellSend AS ss LEFT OUTER JOIN ";
            strSql += "officedba.CurrencyTypeSetting AS ct ON ss.CurrencyType = ct.ID LEFT OUTER JOIN ";
            strSql += "officedba.CustInfo AS c ON ss.CustID = c.ID LEFT OUTER JOIN ";
            strSql += "officedba.DeptInfo AS d ON ss.SellDeptId = d.ID LEFT OUTER JOIN ";
            strSql += "officedba.SellOrder AS so ON ss.FromBillID = so.ID LEFT OUTER JOIN ";
            strSql += "officedba.OtherCorpInfo AS oc ON ss.Transporter = oc.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e1 ON ss.Sender = e1.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e2 ON ss.Creator = e2.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e3 ON ss.Confirmor = e3.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e4 ON ss.Closer = e4.ID LEFT OUTER JOIN ";
            strSql += "officedba.EmployeeInfo AS e5 ON ss.Seller = e5.ID ";
            strSql += " left join officedba.ProjectInfo p on p.ID=ss.ProjectID ";


            strSql += " WHERE (ss.ID = @ID ) AND (ss.CompanyCD = @CompanyCD)";
            SqlParameter[] paras = new SqlParameter[2];
            ArrayList arr = new ArrayList();
            paras[0] = new SqlParameter("@ID", orderID);
            arr.Add(paras[0]);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            arr.Add(paras[1]);
            return SqlHelper.ExecuteSql(strSql, arr);
        }
        #endregion
        #endregion

        #region 确认、结单、取消确认、取消结单操作

        /// <summary>
        /// 确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool ConfirmOrder(string OrderNO, out string strMsg, out string strFieldText)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            strFieldText = "";
            //判断单据是够为制单状态，非制单状态不能确认
            if (isHandle(OrderNO, "1"))
            {
                if (IsConfirm(OrderNO, out strMsg, out strFieldText))
                {
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        SqlParameter[] paras = new SqlParameter[4];
                        strSq = "update  officedba.SellSend set BillStatus='2'  ";
                       
                            strSq += " , Confirmor=@EmployeeID, ConfirmDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                            paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                            paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                            paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                            paras[3] = new SqlParameter("@SendNo", OrderNO);
                       
                        strSq += " WHERE SendNo = @SendNo and CompanyCD=@CompanyCD";
                        
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                        UpdateSellOrder(1, OrderNO, tran);
                        UpdateOrdercount(1, OrderNO);
                        tran.Commit();
                        isSuc = true;
                        strMsg = "确认成功！";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "确认失败，请联系系统管理员！";
                        throw ex;
                    }
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户确认，不可再次确认！";
            }
            return isSuc;
        }

        /// <summary>
        /// 更新对应的物品在分仓存量表
        /// </summary>
        /// <param name="flag">flag=1表示确认单据，等于2表示取消确认</param>
        /// <param name="SendNo"></param>
        private static void UpdateOrdercount(int flag, string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            string strSql = " SELECT ProductID,ProductCount,StorageID FROM officedba.SellSendDetail ";
            strSql += " WHERE(sendno = @OrderNo) AND (CompanyCD = @CompanyCD) ";

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            SqlParameter[] paras = { new SqlParameter("@OrderNo", OrderNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            SqlParameter[] param = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (flag)
                {
                    case 1://确认
                        if (Exists(dt.Rows[i]["ProductID"].ToString(), strCompanyCD))
                        {
                            strSql = "UPDATE officedba.StorageProduct SET OrderCount = (isnull(OrderCount,0) + ";
                            strSql += dt.Rows[i]["ProductCount"].ToString();
                            strSql += ") where StorageID =" + dt.Rows[i]["StorageID"].ToString() + " and ProductID=@ProductID and CompanyCD=@CompanyCD  and  batchno is null";
                            param = new SqlParameter[] { new SqlParameter("@ProductID", dt.Rows[i]["ProductID"]), new SqlParameter("@CompanyCD", strCompanyCD) };
                            SqlHelper.ExecuteSql(strSql, param);
                        }
                        else
                        {
                            InsertStorageProduct(dt.Rows[i]["ProductID"].ToString(), dt.Rows[i]["ProductCount"].ToString(), strCompanyCD, dt.Rows[i]["StorageID"].ToString());
                        }
                        break;
                    case 2://取消确认
                        strSql = "UPDATE officedba.StorageProduct SET OrderCount = (isnull(OrderCount,0) - ";
                        strSql += dt.Rows[i]["ProductCount"].ToString();
                        strSql += ") where StorageID =" + dt.Rows[i]["StorageID"].ToString() + " and ProductID=@ProductID and CompanyCD=@CompanyCD  and batchno is null ";
                        param = new SqlParameter[] { new SqlParameter("@ProductID", dt.Rows[i]["ProductID"]), new SqlParameter("@CompanyCD", strCompanyCD) };
                        SqlHelper.ExecuteSql(strSql, param);
                        break;
                    default:
                        break;
                }



            }
        }

        /// <summary>
        /// 判断物品记录在分仓存量表中是否存在，不存在就插入新数据
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static bool Exists(string ProductID, string CompanyCD)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from officedba.StorageProduct");
            strSql.Append(" where storageID=( SELECT StorageID  FROM officedba.ProductInfo where  ID =@ProductID ) and ProductID=@ProductID and CompanyCD=@CompanyCD ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", ProductID),
                    new SqlParameter("@CompanyCD",CompanyCD)};
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 分仓存量表插入数据
        /// </summary>
        /// <param name="ProductID"></param>
        /// <param name="ProductNum"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        private static void InsertStorageProduct(string ProductID, string ProductNum, string CompanyCD,string StorageID)
        {
            StringBuilder strSql = new StringBuilder();
            string strSql2 = string.Empty;
            //string strStorageID = string.Empty;
            //strSql2 = "SELECT isnull(StorageID,0) as StorageID  FROM officedba.ProductInfo where  ID =@ProductID ";
            //SqlParameter[] para = { new SqlParameter("@ProductID", ProductID) };
            //strStorageID = SqlHelper.ExecuteScalar(strSql2, para).ToString();
            strSql.Append("insert into officedba.StorageProduct(");
            strSql.Append("CompanyCD,StorageID,ProductID,OrderCount)");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@StorageID,@ProductID,@OrderCount)");

            SqlParameter[] parameters = {
                    new SqlParameter("@ProductID", ProductID),
                    new SqlParameter("@CompanyCD",CompanyCD),
                      new SqlParameter("@OrderCount", ProductNum),new SqlParameter("@StorageID", StorageID)                    };
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
        /// 更新订单中已通知数量
        /// 2010-06-10 Modified by hexw；根据是否启用多计量单位来更新（用实际数量来更新）。（若启用多计量单位则用实际数量UsedUnitCount，不启用则ProductCount）
        /// </summary>
        /// <param name="flag">1表示确认，2表示取消确认</param>
        /// <param name="SendNo"></param>
        private static void UpdateSellOrder(int flag, string SendNo, TransactionManager tran)
        {
            string strCompanyCD = string.Empty;//单位编号
            string strSql = " SELECT FromLineNo, FromBillID,isnull(ProductCount,0) as ProductCount,isnull(UsedUnitCount,0) as UsedUnitCount FROM officedba.SellSendDetail ";
            strSql += " WHERE(SendNo = @SendNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')";
           
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            SqlParameter[] paras = { new SqlParameter("@SendNo", SendNo), new SqlParameter("@CompanyCD", strCompanyCD) };
            DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
            strSql = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (flag)
                {
                    case 1://确认
                        strSql = "UPDATE officedba.SellOrderDetail SET SendCount = (isnull(SendCount,0) + ";
                        break;
                    case 2://取消确认
                        strSql = "UPDATE officedba.SellOrderDetail SET SendCount = (isnull(SendCount,0) - ";
                        break;
                    default:
                        break;
                }
                //多计量单位（更新源单时用实际数量更新）
                if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
                {
                    strSql += dt.Rows[i]["UsedUnitCount"].ToString();
                }
                else
                { 
                    strSql += dt.Rows[i]["ProductCount"].ToString();
                }
                
                strSql += ") where OrderNo =( SELECT OrderNo  FROM officedba.SellOrder where  ID =@ID )  AND SortNo=@SortNo and (CompanyCD = @CompanyCD) ";
                SqlParameter[] param = { new SqlParameter("@ID", dt.Rows[i]["FromBillID"]),
                                           new SqlParameter("@SortNo", dt.Rows[i]["FromLineNo"]),
                                           new SqlParameter("@CompanyCD",strCompanyCD)
                                       };
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql, param);
            }
        }

        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool CloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为执行状态，非执行状态不能结单
            if (isHandle(OrderNO, "2"))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[4];
                    strSq = "update  officedba.SellSend set BillStatus='4'  ";
                    
                        strSq += " , Closer=@EmployeeID, CloseDate=getdate(), ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@SendNo", OrderNO);
                   
                    strSq += " WHERE SendNo = @SendNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                    UpdateOrdercount(2, OrderNO);
                    tran.Commit();
                    isSuc = true;
                    strMsg = "结单成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "结单失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户结单，不可再次结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消结单
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnCloseOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            //判断单据是否为手工结单状态，非手工结单状态不能结单
            if (isHandle(OrderNO, "4"))
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    SqlParameter[] paras = new SqlParameter[5];
                    strSq = "update  officedba.SellSend set BillStatus='2'  ";
                   
                        strSq += " , Closer=@EmployeeID, CloseDate=@CloseDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";

                        paras[0] = new SqlParameter("@EmployeeID", DBNull.Value);
                        paras[1] = new SqlParameter("@UserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID);
                        paras[2] = new SqlParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                        paras[3] = new SqlParameter("@SendNo", OrderNO);
                        paras[4] = new SqlParameter("@CloseDate", DBNull.Value);
                   
                    strSq += " WHERE SendNo = @SendNo and CompanyCD=@CompanyCD";

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                    UpdateOrdercount(1, OrderNO);
                    tran.Commit();
                    isSuc = true;
                    strMsg = "取消结单成功！";
                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    isSuc = false;
                    strMsg = "取消结单失败，请联系系统管理员！";
                    throw ex;
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户取消结单，不可再次取消结单！";
            }
            return isSuc;
        }

        /// <summary>
        /// 取消确认单据
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="strMsg">操作返回的信息</param>
        /// <returns></returns>
        public static bool UnConfirmOrder(string OrderNO, out string strMsg)
        {
            string strSq = string.Empty;
            bool isSuc = false;
            strMsg = "";
            int OrderId = GetOrderID(OrderNO);


            //判断单据是否为执行状态，非执行状态不能取消确认
            if (isHandle(OrderNO, "2"))
            {
                //判断单据是否被引用
                if (IsRef(OrderNO))
                {
                    int iEmployeeID = 0;//员工id
                    string strUserID = string.Empty;//用户id
                    string strCompanyCD = string.Empty;//单位编码
                    SqlParameter[] paras = new SqlParameter[6];
                    
                        iEmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
                        strUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                        strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                    strSq = "update  officedba.SellSend set BillStatus='1'   ";
                    strSq += " , Confirmor=@Confirmor, ConfirmDate=@ConfirmDate, ModifiedDate=getdate() ,ModifiedUserID=@UserID ";
                    strSq += " WHERE SendNo = @SendNo and CompanyCD=@CompanyCD and isOpenbill=@isOpenbill";

                    paras[0] = new SqlParameter("@Confirmor", DBNull.Value);
                    paras[1] = new SqlParameter("@UserID", strUserID);
                    paras[2] = new SqlParameter("@CompanyCD", strCompanyCD);
                    paras[3] = new SqlParameter("@SendNo", OrderNO);
                    paras[4] = new SqlParameter("@ConfirmDate", DBNull.Value);
                    paras[5] = new SqlParameter("@isOpenbill", '0');

                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        int iiCount = 0;
                        iiCount = SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSq, paras);
                        if (iiCount > 0)
                        {
                            FlowDBHelper.OperateCancelConfirm(strCompanyCD, 5, 4, OrderId, strUserID, tran);//撤销审批
                            UpdateSellOrder(2, OrderNO, tran);
                            UpdateOrdercount(2, OrderNO);
                            tran.Commit();
                            isSuc = true;
                            strMsg = "取消确认成功！";
                        }
                        else
                        {
                            isSuc = false;
                            strMsg = "取消确认失败！已开票的单据不可取消确认！";
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();

                        isSuc = false;
                        strMsg = "取消确认失败，请联系系统管理员！";
                        throw ex;
                    }
                }
                else
                {
                    isSuc = false;
                    strMsg = "该单据已被其他单据引用，不可取消确认！";
                }
            }
            else
            {
                isSuc = false;
                strMsg = "该单据已被其他用户取消确认，不可再次取消确认！";
            }
            return isSuc;
        }

        #endregion

        #region 确认相关操作是否可以进行

        /// <summary>
        /// 根据单据状态判断该单据是否可以执行该操作
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <param name="OrderStatus">单据状态</param>
        /// <returns>返回true时表示可以执行操作</returns>
        private static bool isHandle(string OrderNO, string OrderStatus)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
          
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;

            strSql = "select count(1) from officedba.SellSend where SendNo = @SendNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
            ArrayList arr = new ArrayList();
            SqlParameter[] paras = new SqlParameter[3];
            paras[0] = new SqlParameter("@SendNo", OrderNO);
            paras[1] = new SqlParameter("@CompanyCD", strCompanyCD);
            paras[2] = new SqlParameter("@BillStatus", OrderStatus);

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount != 0)
            {
                isSuc = true;
            }

            return isSuc;

        }

        /// <summary>
        /// 判断当前发货明细中的发货数量是否大于引用订单中的未执行数量，超出不允许确认
        /// 2010-06-10 Modified by hexw；根据是否启用多计量单位来判断（用实际数量来判断）。（若启用多计量单位则用实际数量UsedUnitCount，不启用则ProductCount）
        /// </summary>
        /// <param name="SendNo"></param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        /// <returns>返回true表示未超出，可以确认</returns>
        private static bool IsConfirm(string SendNo, out string strMsg, out string strFieldText)
        {
            bool isMoreUnit=((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;
            bool isOverOrder = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsOverOrder;//是否启用超订单发货
            string strCompanyCD = string.Empty;//单位编号
            bool isSuc = true;
            strMsg = "";
            strFieldText = "";
            //若启用超订单发货则不进行判断，直接返回true
            if (!isOverOrder)
            { 
                string strSql = "SELECT t.FromLineNo,s.SortNo, t.FromBillID,t.ProductCount,t.UsedUnitCount    ";
                strSql += "FROM officedba.SellSendDetail as s left join      ";
                strSql += "(SELECT FromLineNo, FromBillID, sum(isnull(ProductCount,0)) as ProductCount,sum(isnull(UsedUnitCount,0)) as UsedUnitCount FROM officedba.SellSendDetail ";
                strSql += "WHERE (SendNo = @SendNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')     ";
                strSql += "group by FromBillID,FromLineNo) as t on t.FromLineNo=s.FromLineNo and t.FromBillID=s.FromBillID ";
                strSql += "WHERE (SendNo = @SendNo) AND (CompanyCD = @CompanyCD) AND (FromType = '1')     ";
              
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
               
                SqlParameter[] paras = { new SqlParameter("@SendNo", SendNo), new SqlParameter("@CompanyCD", strCompanyCD) };
                DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
                strSql = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql = string.Empty;
                    //多计量单位
                    if (isMoreUnit)
                    {
                        strSql += " select isnull((UsedUnitCount-isnull(SendCount,0)),0) as pCount  from officedba.SellOrderDetail ";
                    }
                    else
                    { 
                       strSql += " select isnull((ProductCount-isnull(SendCount,0)),0) as pCount  from officedba.SellOrderDetail "; 
                    }
                    strSql += " where OrderNo =( SELECT OrderNo  FROM officedba.SellOrder where  ID =@ID )  AND SortNo=@SortNo and (CompanyCD = @CompanyCD) ";
                    SqlParameter[] param = { 
                                               new SqlParameter("@ID", dt.Rows[i]["FromBillID"]),
                                               new SqlParameter("@SortNo", dt.Rows[i]["FromLineNo"]),
                                               new SqlParameter("@CompanyCD",strCompanyCD)
                                           };
                    decimal pCount = Convert.ToDecimal(SqlHelper.ExecuteScalar(strSql, param));
                    //多计量单位
                    if (isMoreUnit)
                    {
                        if (Convert.ToDecimal(dt.Rows[i]["UsedUnitCount"].ToString()) > pCount)
                        {
                            strFieldText += "明细第" + dt.Rows[i]["SortNo"].ToString() + "行：|";
                            strMsg += "本次发货数量大于源单中的未执行订单数量，请修改！|";
                            isSuc = false;
                        }
                    }
                    else
                    { 
                        if (Convert.ToDecimal(dt.Rows[i]["ProductCount"].ToString()) > pCount)
                        {
                            strFieldText += "明细第" + dt.Rows[i]["SortNo"].ToString() + "行：|";
                            strMsg += "本次发货数量大于源单中的未执行订单数量，请修改！|";
                            isSuc = false;
                        }
                    }
                    
                }            
            }

            return isSuc;
        }

        /// <summary>
        /// 判断单据编号是否存在
        /// </summary>
        /// <param name="OrderNO">单据编号</param>
        /// <returns>返回true时表示不存在</returns>
        private static bool NoIsExist(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
         
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SendNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select count(1) from officedba.SellSend ";
            strSql += " WHERE  (SendNo = @SendNo) AND (CompanyCD = @CompanyCD) ";
            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 单据是否被引用
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示未被引用</returns>
        private static bool IsRef(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SendNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += "select  ((SELECT count(1)                                                                                             ";
            strSql += "FROM officedba.SellBack AS sb WHERE sb.FromType = '1' AND sb.FromBillID = s.ID) +                                     ";
            strSql += "(SELECT count(1) FROM officedba.StorageOutSell AS soo                                                                ";
            strSql += "WHERE soo.FromType = '1' AND soo.FromBillID = s.ID) +                                                                 ";
            strSql += "(SELECT count(1) FROM officedba.SellChannelSttl AS scs                                                                ";
            strSql += "WHERE scs.FromType = '1' AND scs.FromBillID = s.ID) +                                                                 ";
            strSql += "(SELECT count(1) FROM officedba.SellChannelSttlDetail AS scsd                                                         ";
            strSql += "WHERE scsd.FromType = '1' AND scsd.FromBillID = s.ID) +                                                               ";
            strSql += "(SELECT count(1)  FROM officedba.SellBackDetail AS sbd WHERE sbd.FromType = '1' AND sbd.FromBillID = s.ID)) as tt     ";
            strSql += "FROM officedba.SellSend AS s                                                                                         ";

            strSql += "WHERE (s.SendNo = @SendNo) AND (s.CompanyCD = @CompanyCD)               ";

            iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));


            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 单据是否提交审批
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示未提交</returns>
        private static bool IsFlow(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            int iCount = 0;
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SendNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@FlowStatus",'5')
                                   };
            strSql += " SELECT COUNT(1) FROM officedba.FlowInstance AS f LEFT OUTER JOIN officedba.SellSend AS s ON f.BillID = s.ID AND f.BillTypeFlag = 5 AND f.BillTypeCode = 4 and f.CompanyCD=s.CompanyCD";
            strSql += "  WHERE (s.SendNo = @SendNo) AND (f.CompanyCD = @CompanyCD) and f.flowstatus!=@FlowStatus  ";
            try
            {
                iCount = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            }
            catch (Exception ex)
            {
                isSuc = false;
            }

            if (iCount == 0)
            {
                isSuc = true;
            }

            return isSuc;
        }

        /// <summary>
        /// 根据单据状态判断单据是否可以被修改
        /// </summary>
        /// <param name="OrderNO"></param>
        /// <returns>返回true时表示可以修改</returns>
        private static bool IsUpdate(string OrderNO)
        {
            bool isSuc = false;
            string strCompanyCD = string.Empty;//单位编号
            string strStatus = string.Empty;
           
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
           
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@SendNo", OrderNO),
                                       new SqlParameter("@CompanyCD", strCompanyCD) 
                                   };
            strSql += " select TOP  1  FlowStatus  from officedba.FlowInstance ";
            strSql += " WHERE (BillID = (SELECT ID FROM officedba.SellSend WHERE (SendNo = @SendNo) AND (CompanyCD = @CompanyCD))) AND BillTypeFlag = 5 AND BillTypeCode = 4  ";
            strSql += " ORDER BY ModifiedDate DESC ";
            object obj = SqlHelper.ExecuteScalar(strSql, paras);
            if (obj != null)
            {
                strStatus = obj.ToString();
                switch (strStatus)
                {
                    case "4":
                        isSuc = true;
                        break;
                    case "5":
                        isSuc = true;
                        break;
                    default:
                        isSuc = false;
                        break;
                }
            }
            else
            {
                isSuc = true;
            }

            return isSuc;
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印主表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrder(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "select * from  officedba.sellmodule_report_SellSend WHERE (SendNo = @SendNo) AND (CompanyCD = @CompanyCD)";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SendNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }

        /// <summary>
        /// 打印子表数据
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public static DataTable GetRepOrderDetail(string OrderNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            
                strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            
            string strSql = "select  * from  officedba.sellmodule_report_SellSendDetail WHERE (SendNo = @SendNo) AND (CompanyCD = @CompanyCD) order by SortNo asc";
            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SendNo",OrderNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
        #endregion

        #region 获取发货单主表信息
        /// <summary>
        /// 获取发货单主表信息，用于打印
        /// </summary>
        /// <param name="sendNo"></param>
        /// <returns></returns>
        public static DataTable GetSellSend(string sendNo)
        {
            string strCompanyCD = string.Empty;//单位编号
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string sql = "SELECT c.Fax,c.Mobile,c.Contactname,c.CustName, cpt.TypeName AS SellTypeName, ct.CurrencyName, d.DeptName, cpt4.TypeName AS TakeTypeName,";
            sql+="e1.EmployeeName AS SenderName, cpt3.TypeName AS CarryTypeName, cpt2.TypeName AS MoneyTypeName, e.EmployeeName AS SellerName,";
            sql+="e2.EmployeeName AS ConfirmorName, e3.EmployeeName AS CloserName, e4.EmployeeName AS CreatorName, cpt1.TypeName AS PayTypeName,";
            sql+="cpt5.TypeName AS TransPayTypeName, so.OrderNo, o.CustName AS TransporterName, s.ID, s.CompanyCD, s.SendNo, s.Title, s.SendAddr, ";
            sql+="s.ReceiveAddr, s.Receiver, s.Tel, s.Modile, s.Post, s.IntendSendDate, s.TransportFee, s.Rate, CONVERT(numeric(22,2),round(s.TotalPrice,2))as TotalPrice, s.Tax, s.TotalFee, s.Discount,";
            sql+="s.PayRemark, s.DeliverRemark, s.PackTransit, s.Remark, CONVERT(varchar(100), s.CreateDate, 23) AS CreateDate, CONVERT(varchar(100), ";
            sql+="s.ConfirmDate, 23) AS ConfirmDate, CONVERT(varchar(100), s.CloseDate, 23) AS CloseDate, s.ModifiedUserID, CONVERT(varchar(100), s.ModifiedDate,";
            sql+="23) AS ModifiedDate, s.DiscountTotal, s.RealTotal, s.CountTotal, s.ProjectID, p.ProjectName, ";
            sql+="CASE s.FromType WHEN '0' THEN '无来源' WHEN '1' THEN '销售订单' END AS FromTypeText, ";
            sql+="CASE s.BusiType WHEN '1' THEN '普通销售' WHEN '2' THEN '委托代销' WHEN '3' THEN '直运' WHEN '4' THEN '零售' WHEN '5' THEN '销售调拨' END ";
            sql+="AS BusiTypeName, CASE s.isAddTax WHEN '0' THEN '否' WHEN '1' THEN '是' END AS isAddTaxName, ";
            sql+="CASE s.BillStatus WHEN '1' THEN '制单' WHEN '2' THEN '执行' WHEN '4' THEN '手工结单' WHEN '5' THEN '自动结单' END AS BillStatusText, ";
            sql+="c.Tel AS CustTel, s.ExtField1, s.ExtField2, s.ExtField3, s.ExtField4, s.ExtField5, s.ExtField6, s.ExtField7, s.ExtField8, s.ExtField9, s.ExtField10,";
            sql+="s.CanViewUser, dbo.getEmployeeNameString(s.CanViewUser) AS CanViewUserName ";
            sql+="FROM officedba.SellSend AS s LEFT OUTER JOIN ";
            sql+="officedba.OtherCorpInfo AS o ON s.Transporter = o.ID LEFT OUTER JOIN ";
            sql+="officedba.SellOrder AS so ON so.ID = s.FromBillID LEFT OUTER JOIN ";
            sql+="officedba.CustInfo AS c ON s.CustID = c.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt ON s.SellType = cpt.ID LEFT OUTER JOIN ";
            sql+="officedba.CurrencyTypeSetting AS ct ON s.CurrencyType = ct.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt1 ON s.PayType = cpt1.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt2 ON s.MoneyType = cpt2.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt3 ON s.CarryType = cpt3.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt4 ON s.TakeType = cpt4.ID LEFT OUTER JOIN ";
            sql+="officedba.CodePublicType AS cpt5 ON s.TransPayType = cpt5.ID LEFT OUTER JOIN ";
            sql+="officedba.DeptInfo AS d ON s.SellDeptId = d.ID LEFT OUTER JOIN ";
            sql+="officedba.EmployeeInfo AS e ON s.Seller = e.ID LEFT OUTER JOIN ";
            sql+="officedba.EmployeeInfo AS e1 ON s.Sender = e1.ID LEFT OUTER JOIN ";
            sql+="officedba.EmployeeInfo AS e2 ON s.Confirmor = e2.ID LEFT OUTER JOIN ";
            sql+="officedba.EmployeeInfo AS e3 ON s.Closer = e3.ID LEFT OUTER JOIN ";
            sql+="officedba.EmployeeInfo AS e4 ON s.Creator = e4.ID LEFT OUTER JOIN ";
            sql+="officedba.ProjectInfo AS p ON p.ID = s.ProjectID ";
            sql += "where s.CompanyCD=@CompanyCD and s.SendNo=@SendNo ";

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SendNo",sendNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(sql, param);
            return dt;
        }
        #endregion

        #region 获取发货单明细的仓库
        public static DataTable GetSellSendStorage(string sendNo)
        {
            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Sql = "select distinct s.ID, s.StorageName ";
            Sql += "from officedba.StorageInfo s ";
            Sql += " left join officedba.SellSendDetail sd on sd.StorageID=s.ID ";
            Sql += " left join officedba.SellSend ss on ss.SendNo=sd.SendNo ";
            Sql += "where ss.CompanyCD=@CompanyCD and ss.SendNo=@SendNo ";

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SendNo",sendNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(Sql, param);
            return dt;
        }
        #endregion

        #region 获取发货单明细表信息
        public static DataTable GetSellSendDetail(string sendNo)
        {
            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string strSql = "SELECT s.ID,isnull(s.productalias,'')ProductAlias,isnull(s.TotalNumber,0)TotalNumber,isnull(s.PieceCount,0)PieceCount,case when p.size is null then '' when p.size='**' then '' else p.size end as Size, s.CompanyCD, s.SendNo, s.SortNo, s.ProductCount, s.UnitPrice, s.TaxPrice, s.Discount, s.TaxRate, s.TotalFee, CONVERT(NUMERIC(22,2),round(s.TotalPrice,2)) as TotalPrice, s.TotalTax,";
            strSql += " isnull(s.Custom1,'')Custom1,isnull(s.Custom2,'')Custom2,isnull(s.Custom3,'')Custom3,isnull(s.Custom4,'')Custom4,isnull(s.Custom5,'')Custom5,isnull(s.Custom6,'')Custom6,isnull(s.Custom7,'')Custom7,isnull(s.Custom8,'')Custom8,isnull(s.Custom9,'')Custom9,isnull(s.Custom10,'')Custom10, ";
                 strSql += " CASE WHEN CP.ProdAlias IS NULL THEN p.ProductName ELSE CP.ProdAlias END AS ProductName, p.ProdNo, c.TypeName AS PackageName, ";
                 strSql += " s.FromLineNo, s.FromBillID, s.FromType, s.BackCount, s.OutCount, s.SttlCount, s.Remark, p.Specification, CONVERT(varchar(100), s.SendDate, 23) ";
                 strSql += " AS SendDate, d.CodeName AS UnitName, s.UsedUnitID, CONVERT(numeric(14,2),s.UsedUnitCount) as UsedUnitCount,CONVERT(numeric(14,2),s.UsedPrice) as UsedPrice, ";
            strSql +=" ISNULL(s.ExRate, 1) AS ExRate, ISNULL(b.CodeName, '') AS UsedUnitName, ISNULL(pc.TypeName, '') AS ColorName, si.StorageName ";
            strSql +=" FROM officedba.SellSendDetail AS s LEFT OUTER JOIN ";
            strSql +=" officedba.ProductInfo AS p ON s.ProductID = p.ID LEFT OUTER JOIN ";
            strSql +=" officedba.CodePublicType AS c ON s.Package = c.ID LEFT OUTER JOIN ";
            strSql +=" officedba.CodeUnitType AS d ON s.UnitID = d.ID LEFT OUTER JOIN ";
            strSql +=" officedba.CodeUnitType AS b ON b.ID = s.UsedUnitID LEFT OUTER JOIN ";
            strSql +=" officedba.CodePublicType AS pc ON pc.ID = p.ColorID LEFT OUTER JOIN ";
            strSql += " officedba.SellSend AS k ON k.SendNo = s.SendNo and k.CompanyCD=s.CompanyCD LEFT OUTER JOIN ";
            strSql +=" officedba.CustProdDetails AS CP ON CP.ProdNo = p.ProdNo AND CP.CustID = k.CustID";
            strSql += " left join officedba.StorageInfo as si on si.ID=s.StorageID ";
            strSql += "where k.CompanyCD=@CompanyCD and k.SendNo=@SendNo";

            SqlParameter[] param = {
                                       new SqlParameter("@CompanyCD", strCompanyCD),
                                       new SqlParameter("@SendNo",sendNo)
                                   };

            DataTable dt = SqlHelper.ExecuteSql(strSql, param);
            return dt;
        }
        #endregion

        #region 验证现有库存数量
        /// <summary>
        /// 验证现有库存数量
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="strMsg"></param>
        /// <param name="strFieldText"></param>
        public static void CheckProCount(Hashtable ht, out string strMsg, out string strFieldText)
        {          
            strFieldText = "";
            strMsg = "";
            string strSql = string.Empty;
            string strCompanyCD = string.Empty;
            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //strSql += " SELECT isnull(SUM(ProductCount),0) as proCount,(select ProductName from officedba.ProductInfo where ID=@ProductID) ";
            strSql += " SELECT isnull(sum(productcount),0)+isnull(sum(roadcount),0)+isnull(sum(incount),0)-isnull(sum(ordercount),0)-isnull(sum(outcount),0) as proCount,(select ProductName from officedba.ProductInfo where ID=@ProductID) ";
            strSql += " as ProName  FROM officedba.StorageProduct                                                                ";
            strSql += " WHERE (CompanyCD = @CompanyCD) AND (ProductID = @ProductID)                                              ";
            foreach (DictionaryEntry de in ht)// fileht为一个Hashtable实例
            {
                SqlParameter[] paras = { new SqlParameter("@ProductID", de.Key), new SqlParameter("@CompanyCD", strCompanyCD) };
                DataTable dt = SqlHelper.ExecuteSql(strSql, paras);
                if (dt.Rows.Count == 1)
                {
                    if (Convert.ToDecimal(de.Value) > Convert.ToDecimal(dt.Rows[0]["proCount"].ToString()))
                    {
                        strFieldText += "明细物品：" + dt.Rows[0]["ProName"].ToString() + "|";
                        strMsg += "本次总发货数量(" + de.Value.ToString() + ")大于现有库存数量(" + dt.Rows[0]["proCount"].ToString() + ")，您确定操作？|";
                    }
                }
            }
        }
        #endregion

        #region 销售执行统计
        public static DataTable GetSellStatistics(string OrderID,string Product, string CustID,string StartDate, string EndDate,
           string HzType,string CompanyCD, string SalesManID, string Dept,string SellType,string SaleType,int pageIndex,int pageCount, ref int totalCount)
        {
            string sql =@" EXEC	[officedba].[SellStatistics] ";
            sql+="@companycd = '"+CompanyCD+"', ";
            if(!string.IsNullOrEmpty(Dept))
            {
               sql+="@deptid = '"+Dept+"', ";
            }
            else
            {
                sql+="@deptid = 0,";
            }
            if(!string.IsNullOrEmpty(StartDate))
            {
               sql+="@time1 = '"+StartDate+"', ";
            }
            else
            {
                sql+="@time1 = NULL,";
            }
            if(!string.IsNullOrEmpty(EndDate))
            {
               sql+="@time2 = '"+EndDate+"', ";
            }
            else
            {
                sql+="@time2 = NULL,";
            }
            if(!string.IsNullOrEmpty(CustID))
            {
               sql+="@custid = '"+CustID+"', ";
            }
            else
            {
                sql+="@custid = 0,";
            }
            if(!string.IsNullOrEmpty(Product))
            {
               sql+="@prodname = '"+Product+"', ";
            }
            else
            {
                sql+="@prodname = NULL,";
            }
            if(!string.IsNullOrEmpty(SalesManID))
            {
               sql+="@seller = '"+SalesManID+"', ";
            }
            else
            {
                sql+="@seller = 0,";
            }
             if(!string.IsNullOrEmpty(OrderID))
            {
               sql+="@orderno = '"+OrderID+"', ";
            }
            else
            {
                sql+="@orderno = 0,";
            }
               if(!string.IsNullOrEmpty(SellType))
            {
               sql+="@selltype = '"+SellType+"', ";
            }
            else
            {
                sql+="@selltype = 0,";
            }
           if (!string.IsNullOrEmpty(SaleType))
           {
               sql += "@saletype = '" + SaleType + "', ";
           }
           else
           {
               sql += "@saletype = 0,";
           }
            if(!string.IsNullOrEmpty(HzType))
            {
               sql+="@grouptype = '"+HzType+"', ";
            }
            else
            {
                sql+="@grouptype = 0,";
            }
            sql+="@pagesize = '"+pageCount+"',";
            sql+="@pageindex = '"+pageIndex+"'";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dt.Rows[0]["totalcount"].ToString());
            }
            else
            {
                totalCount = 0;
            }
            return dt;
        }
        #endregion

        #region 采购执行统计
        public static DataTable GetPurchaseStatistics(string OrderID, string Product, string CustID, string StartDate, string EndDate,
           string HzType, string CompanyCD, string SalesManID, string Dept, string SellType, int pageIndex, int pageCount, ref int totalCount)
        {
            string sql = @" EXEC	[officedba].[PurchaseStatistics] ";
            sql += "@companycd = '" + CompanyCD + "', ";
            if (!string.IsNullOrEmpty(Dept))
            {
                sql += "@deptid = '" + Dept + "', ";
            }
            else
            {
                sql += "@deptid = 0,";
            }
            if (!string.IsNullOrEmpty(StartDate))
            {
                sql += "@time1 = '" + StartDate + "', ";
            }
            else
            {
                sql += "@time1 = NULL,";
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                sql += "@time2 = '" + EndDate + "', ";
            }
            else
            {
                sql += "@time2 = NULL,";
            }
            if (!string.IsNullOrEmpty(CustID))
            {
                sql += "@custid = '" + CustID + "', ";
            }
            else
            {
                sql += "@custid = 0,";
            }
            if (!string.IsNullOrEmpty(Product))
            {
                sql += "@prodname = '" + Product + "', ";
            }
            else
            {
                sql += "@prodname = NULL,";
            }
            if (!string.IsNullOrEmpty(SalesManID))
            {
                sql += "@seller = '" + SalesManID + "', ";
            }
            else
            {
                sql += "@seller = 0,";
            }
            if (!string.IsNullOrEmpty(OrderID))
            {
                sql += "@orderno = '" + OrderID + "', ";
            }
            else
            {
                sql += "@orderno = 0,";
            }
            if (!string.IsNullOrEmpty(SellType))
            {
                sql += "@selltype = '" + SellType + "', ";
            }
            else
            {
                sql += "@selltype = 0,";
            }
            if (!string.IsNullOrEmpty(HzType))
            {
                sql += "@grouptype = '" + HzType + "', ";
            }
            else
            {
                sql += "@grouptype = 0,";
            }
            sql += "@pagesize = '" + pageCount + "',";
            sql += "@pageindex = '" + pageIndex + "'";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                totalCount = Convert.ToInt32(dt.Rows[0]["totalcount"].ToString());
            }
            else
            {
                totalCount = 0;
            }
            return dt;
        }
        #endregion
    }
}
