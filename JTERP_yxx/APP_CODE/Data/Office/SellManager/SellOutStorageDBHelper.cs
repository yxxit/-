/*************************************
 * 创建人：
 * 创建日期：2009-12-12
 * 描述：销售出库
 ************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Common;
using XBase.Data.Common;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellManager;
using System.Web.UI.WebControls;



namespace XBase.Data.Office.SellManager
{
    public class SellOutStorageDBHelper
    {
        #region 添加销售出库单
        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true保存成功，false保存失败</returns>
        public static bool SaveSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable htExtAttr, out string strMsg)
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

        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="sellOutFDMList">SellOutStorageFeeDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true保存成功，false保存失败</returns>
        public static bool SaveSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList,List<SellOutStorageFeeDetailModel> sellOutFDMList, Hashtable htExtAttr, out string strMsg)
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
                    InsertSellOutFeeDetail(sellOutFDMList,tran);
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
        public static bool UpdataSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOutModel.OutNo, sellOutModel.CompanyCD))
            {
                string strSql = "delete from officedba.SellOutStorageDetail where  OutNo=@OutNo  and CompanyCD=@CompanyCD ";
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

        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        ///  <param name="sellOutFDMList">SellOutStorageFeeDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true修改成功，false修改失败</returns>
        public static bool UpdataSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, List<SellOutStorageFeeDetailModel> sellOutFDMList,Hashtable htExtAttr, out string strMsg)
        {
            bool isSucc = false;//是否添加成功
            strMsg = "";
            if (IsUpdate(sellOutModel.OutNo, sellOutModel.CompanyCD))
            {
                string strSql = "delete from officedba.SellOutStorageDetail where  OutNo=@OutNo  and CompanyCD=@CompanyCD ";
                SqlParameter[] paras = { new SqlParameter("@OutNo", sellOutModel.OutNo), new SqlParameter("@CompanyCD", sellOutModel.CompanyCD) };
                string strSql1 = "delete from officedba.SellOutStorageFeeDetail where   OutNo=@OutNo  and CompanyCD=@CompanyCD";


                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    UpdateSellOutStorage(sellOutModel, tran, htExtAttr);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql1.ToString(), paras);
                    InsertSellOutStorageDetail(sellOutDMList, tran);
                    InsertSellOutFeeDetail(sellOutFDMList,tran);
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
        #region 删除销售出库单
        /// <summary>
        /// 删除销售出库单
        /// </summary>
        /// <param name="NoStr">编号序列</param>
        public static bool DelSellOutStorage(string NoStr, int branchID, string strCompanyCD, out string strMsg, out string strFieldText)
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
                if (!isHandle(strSellOutNos[i], "1", strCompanyCD))
                {
                    
                    // strFieldText += "单据：" + strSellOutNos[i] + " ";
                    //strMsg += "已确认或已作废的单据不允许删除！\n";
                    strMsg += "单据：" + strSellOutNos[i] + " " + "已确认或已作废的单据不允许删除！";
                    bTemp = true;
                }
                if (IsNotCurStore(strSellOutNos[i], branchID, strCompanyCD))
                {
                    //strFieldText += "单据：" + strSellOutNos[i] + " ";
                    //strMsg += "不是本店的单据不允许删除！\n";
                    strMsg += "单据：" + strSellOutNos[i] + " " + "不是本店的单据不允许删除！";
                    bTemp = true;
                }
                strSellOutNos[i] = "'" + strSellOutNos[i] + "'";
                sb.Append(strSellOutNos[i]);
            }

            allSellOutNo = sb.ToString().Replace("''", "','");
            if (!bTemp)
            {
                tran.BeginTransaction();
                try
                {
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOutStorage WHERE OutNo IN ( " + allSellOutNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOutStorageDetail WHERE OutNo IN ( " + allSellOutNo + " ) and CompanyCD='" + strCompanyCD + "'", null);
                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "DELETE FROM officedba.SellOutStorageFeeDetail WHERE OutNo IN ( " + allSellOutNo + " ) and CompanyCD='" + strCompanyCD + "'", null);

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
        public static void InsertSellOutStorage(SellOutStorageModel sellOutModel, TransactionManager tran, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" insert into officedba.SellOutStorage (CompanyCD,OutNo,BranchID,CustID,");
            strSql.Append(" UserID,StorageID,Contractor,ContractPhone,");
            strSql.Append(" SellType,SellDate,FromBillID,AriveAddress,");
            strSql.Append(" BillStatus,Remark,PreferPrice,TotalCount,TotalPrice,Creator,CreateDate,ExpireDay,IsUsedPromotion,ForkliftTruck,LogisticsID,LogisticsPrice) ");
            strSql.Append(" values(@CompanyCD,@OutNo,@BranchID,@CustID,");
            strSql.Append(" @UserID,@StorageID,@Contractor,@ContractPhone,");
            strSql.Append(" @SellType,@SellDate,@FromBillID,@AriveAddress,");
            strSql.Append(" @BillStatus,@Remark,@PreferPrice,@TotalCount,@TotalPrice,@Creator,getdate(),@ExpireDay,@IsUsedPromotion,@ForkliftTruck,@LogisticsID,@LogisticsPrice)");
            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            #region 参数
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@BranchID", sellOutModel.BranchID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOutModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@UserID", sellOutModel.UserID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@StorageID", sellOutModel.StorageID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Contractor", sellOutModel.Contractor));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContractPhone", sellOutModel.ContractPhone));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellOutModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDate", sellOutModel.SellDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOutModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@AriveAddress", sellOutModel.AriveAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellOutModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOutModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PreferPrice", sellOutModel.PreferPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalCount", sellOutModel.TotalCount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOutModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Creator", sellOutModel.Creator.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExpireDay", sellOutModel.ExpireDay.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@IsUsedPromotion", sellOutModel.IsUsedPromotion));
            lcmd.Add(SqlHelper.GetParameterFromString("@ForkliftTruck", sellOutModel.ForkliftTruck.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@LogisticsID", sellOutModel.LogisticsID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@LogisticsPrice", sellOutModel.LogisticsPrice.ToString()));
            #endregion

            #region 拓展属性

            if (htExtAttr != null && htExtAttr.Count != 0)
            {
                strSql.Append(" ;UPDATE officedba.SellOutStorage set ");
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql.Append(de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",");
                    lcmd.Add(SqlHelper.GetParameterFromString("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim()));
                }
                strSql.Append(" BillStatus=@BillStatus  where CompanyCD=@CompanyCD and OutNo=@OutNo and BranchID=@BranchID ");
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
        private static void InsertSellOutStorageDetail(List<SellOutStorageDetailModel> sellOutDMList, TransactionManager tran)
        {
            foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("insert into officedba.SellOutStorageDetail(");
                strSql.Append("CompanyCD,OutNo,ProductID,BatchNo,DetailCount,DetailPrice,");
                strSql.Append(" DetailTotalPrice,BackCount,FromDetailID,PromotionID");
                //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == 2)
                //{
                //    strSql.Append(" ,LogisticsID,LogisticsPrice,Remark ");
                //}
                strSql.Append(") values (");
                strSql.Append(" @CompanyCD,@OutNo,@ProductID,@BatchNo,@DetailCount,@DetailPrice,");
                strSql.Append(" @DetailTotalPrice,@BackCount,@FromDetailID,@PromotionID");
                //if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == 2)
                //{
                //    strSql.Append(" ,"+sellOutDetailModel.Logistic+","+sellOutDetailModel.Logisticsprice+",'"+sellOutDetailModel.Remark+"' ");
                //}
                strSql.Append(" )");
                #region 参数
                SqlParameter[] param = {
					                        new SqlParameter("@CompanyCD", sellOutDetailModel.CompanyCD ),
					                        new SqlParameter("@OutNo", sellOutDetailModel.OutNo ),
					                        new SqlParameter("@ProductID", sellOutDetailModel.ProductID ),
					                        new SqlParameter("@BatchNo", sellOutDetailModel.BatchNo ),
					                        new SqlParameter("@DetailCount", sellOutDetailModel.DetailCount ),
					                        new SqlParameter("@DetailPrice", sellOutDetailModel.DetailPrice ),
					                        new SqlParameter("@DetailTotalPrice", sellOutDetailModel.DetailTotalPrice ),
					                        new SqlParameter("@BackCount", sellOutDetailModel.BackCount ),
					                        new SqlParameter("@FromDetailID", sellOutDetailModel.FromDetailID ),
					                        new SqlParameter("@PromotionID", sellOutDetailModel.PromotionID ) 
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
        #region 插入销售出库费用明细
        /// <summary>
        /// 为费用表插入数据
        /// </summary>
        /// <param name="sellOutStorageFDMList"></param>
        /// <param name="tran"></param>
        private static void InsertSellOutFeeDetail(List<SellOutStorageFeeDetailModel> sellOutStorageFDMList, TransactionManager tran)
        {
            foreach (SellOutStorageFeeDetailModel sellOutFeeDetailModel in sellOutStorageFDMList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into officedba.SellOutStorageFeeDetail (");
                strSql.Append("OutNo,FeeID,FeeTotal,Remark,CompanyCD)");
                strSql.Append(" values (");
                strSql.Append("@OutNo,@FeeID,@FeeTotal,@Remark,@CompanyCD)");
                #region 参数
                SqlParameter[] parameters = {
					new SqlParameter("@OutNo", SqlDbType.VarChar,50),
					new SqlParameter("@FeeID", SqlDbType.Int,4),
					new SqlParameter("@FeeTotal", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,800),
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8)};
                parameters[0].Value = sellOutFeeDetailModel.OutNo;
                parameters[1].Value = sellOutFeeDetailModel.FeeID;
                parameters[2].Value = sellOutFeeDetailModel.FeeTotal;
                parameters[3].Value = sellOutFeeDetailModel.Remark;
                parameters[4].Value = sellOutFeeDetailModel.CompanyCD;
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
        #region 修改主表信息
        /// <summary>
        /// 修改主表信息
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="tran">事务</param>
        private static void UpdateSellOutStorage(SellOutStorageModel sellOutModel, TransactionManager tran, Hashtable htExtAttr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update officedba.SellOutStorage set ");
            strSql.Append(" CustID=@CustID,UserID=@UserID,");
            strSql.Append(" StorageID=@StorageID,Contractor=@Contractor,");
            strSql.Append(" ContractPhone=@ContractPhone,");
            strSql.Append(" SellType=@SellType,SellDate=@SellDate,");
            strSql.Append(" FromBillID=@FromBillID,AriveAddress=@AriveAddress,BillStatus=@BillStatus,");
            strSql.Append(" Remark=@Remark,PreferPrice=@PreferPrice,");
            strSql.Append(" TotalCount=@TotalCount,TotalPrice=@TotalPrice,ExpireDay=@ExpireDay,IsUsedPromotion=@IsUsedPromotion,ForkliftTruck=@ForkliftTruck,LogisticsID=@LogisticsID,LogisticsPrice=@LogisticsPrice ");
            strSql.Append(" where CompanyCD=@CompanyCD and OutNo=@OutNo and BranchID=@BranchID ");

            SqlParameter[] param = null;
            ArrayList lcmd = new ArrayList();
            lcmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
            lcmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
            lcmd.Add(SqlHelper.GetParameterFromString("@BranchID", sellOutModel.BranchID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@CustID", sellOutModel.CustID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@UserID", sellOutModel.UserID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@StorageID", sellOutModel.StorageID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@Contractor", sellOutModel.Contractor));
            lcmd.Add(SqlHelper.GetParameterFromString("@ContractPhone", sellOutModel.ContractPhone));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellType", sellOutModel.SellType.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@SellDate", sellOutModel.SellDate.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@FromBillID", sellOutModel.FromBillID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@AriveAddress", sellOutModel.AriveAddress));
            lcmd.Add(SqlHelper.GetParameterFromString("@BillStatus", sellOutModel.BillStatus));
            lcmd.Add(SqlHelper.GetParameterFromString("@Remark", sellOutModel.Remark));
            lcmd.Add(SqlHelper.GetParameterFromString("@PreferPrice", sellOutModel.PreferPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalCount", sellOutModel.TotalCount.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@TotalPrice", sellOutModel.TotalPrice.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@ExpireDay", sellOutModel.ExpireDay.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@IsUsedPromotion", sellOutModel.IsUsedPromotion));
            lcmd.Add(SqlHelper.GetParameterFromString("@ForkliftTruck", sellOutModel.ForkliftTruck.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@LogisticsID", sellOutModel.LogisticsID.ToString()));
            lcmd.Add(SqlHelper.GetParameterFromString("@LogisticsPrice", sellOutModel.LogisticsPrice.ToString()));

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
        public static bool ConfirmSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable htExtAttr, string EmployeeName, int EmployeeID, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            if (isHandle(sellOutModel.OutNo, "1", sellOutModel.CompanyCD))
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string isEnoughStr = "";

                //if (userInfo.IsNegStock) // 启用 超可用量发货，不需要进行可用量检测
                //{
                //    isEnoughStr = "";
                //}
                //else
                //{
                //    isEnoughStr = IsEnoughStorage(sellOutDMList, sellOutModel.StorageID, sellOutModel.BranchID);
                //}  
                
                if (isEnoughStr == "")
                {
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        SqlParameter[] param2 ={
                                                new SqlParameter("@CompanyCD",sellOutModel.CompanyCD),
                                                new SqlParameter("@OutNo",sellOutModel.OutNo)
                                             };
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, "delete from officedba.SellOutStorageDetail where  OutNo=@OutNo  and CompanyCD=@CompanyCD", param2);
                        UpdateSellOutStorage(sellOutModel, tran, htExtAttr);//修改主表
                        InsertSellOutStorageDetail(sellOutDMList, tran);//重新插入子表

                        //更新单据状态，确认人，确认时间
                        strSql.Append(" update officedba.SellOutStorage set BillStatus=@BillStatus, ");
                        strSql.Append(" Confirmor=@Confirmor ,ConfirmDate=getdate()");
                        strSql.Append(" where CompanyCD= @CompanyCD and OutNo=@OutNo ");

                        SqlParameter[] p = { 
                                            new SqlParameter("@Confirmor",EmployeeID),
                                            new SqlParameter("@BillStatus","2"),
                                            new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                            new SqlParameter("@OutNo",sellOutModel.OutNo ),
                                            new SqlParameter("@BranchID",sellOutModel.BranchID )
                                           };
                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), p);
                         bool isbig = false;
                         foreach (SellOutStorageDetailModel Model in sellOutDMList)
                         {
                             if (Model.FromDetailID > 0)
                             {
                                 string sql = "select count(1) from officedba.sellorderdetail where outcount+"+Model.DetailCount+">ordercount and id="+Model.FromDetailID+" ";
                                 if (int.Parse(SqlHelper.ExecuteSql(sql).Rows[0][0].ToString()) > 0)
                                 {
                                     isbig = true;
                                     break;
                                 }
                             }
                         }
                         if (!isbig)
                         {
                             tran.Commit();  // liuch add 20110306

                             //更新分仓存量表和更新库存结余表
                             foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
                             {
                                 StringBuilder strSql2 = new StringBuilder();
                                 //回写销售订单
                                 if (sellOutDetailModel.FromDetailID > 0 && ((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == "2")
                                 {
                                     strSql2.Append(" ;update officedba.sellorderdetail set outcount=isnull(outcount,0)+" + sellOutDetailModel.DetailCount + " where id=" + sellOutDetailModel.FromDetailID + " ");
                                 }

                                 //附加更新分仓存量表语句(出库：减少库存量)
                                 strSql2.Append(" ;update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)-@ProductCount ");
                                 strSql2.Append(" where StorageID=@StorageID ");
                                 strSql2.Append(" and ProductID=@ProductID ");
                                 strSql2.Append(" and BatchNo=@BatchNo ");
                                 strSql2.Append(" and CompanyCD=@CompanyCD and BranchID=@BranchID ");

                                 //更新库存结余表（插入记录）
                                 //获取现有存量：根据CompanyCD，BranchID，ProductID检索分存量表获取该商品的库存
                                 // strSql2.Append("; select @NowCount= CONVERT(decimal(19, 2), ISNULL(SUM(ProductCount), 0))  from officedba.StorageProduct  where ProductID=@ProductID ");
                                 // strSql2.Append(" and CompanyCD=@CompanyCD and BranchID=@BranchID  ");
                                 //获取当前明细的ID
                                 strSql2.Append(" ;select @BillDetailID=ID from officedba.SellOutStorageDetail where CompanyCD=@CompanyCD ");
                                 strSql2.Append(" and OutNo=@BillNo and ProductID=@ProductID and BatchNo=@BatchNo ");

                                 strSql2.Append(" ;insert into officedba.StorageSurplus (CompanyCD,BillType,BranchID,");
                                 strSql2.Append(" BatchNo,BillNo,BillDetailID,NowCount)");
                                 strSql2.Append(" values(@CompanyCD,'5',@BranchID,@BatchNo,");
                                 strSql2.Append(" @BillNo,@BillDetailID,@NowCount)");
                                 string strSql3 = "select isnull(sum(ProductCount),0)  as NowCount from officedba.StorageProduct  where ProductID=" + sellOutDetailModel.ProductID + " and CompanyCD='" + sellOutModel.CompanyCD + "' ";
                                 DataTable dtt = SqlHelper.ExecuteSql(strSql3);
                                 SqlParameter[] param = {
                                                    new SqlParameter("@ProductCount",sellOutDetailModel.DetailCount ),
                                                    new SqlParameter("@StorageID",sellOutModel.StorageID ),
                                                    new SqlParameter("@ProductID",sellOutDetailModel.ProductID ),
                                                    new SqlParameter("@BatchNo",sellOutDetailModel.BatchNo ),
                                                    new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                                    new SqlParameter("@BranchID",sellOutModel.BranchID ),
                                                    new SqlParameter("@BillNo",sellOutModel.OutNo ),
                                                    new SqlParameter("@BillDetailID",0),
                                                    new SqlParameter("@NowCount",(decimal.Parse(dtt.Rows[0][0].ToString())-sellOutDetailModel.DetailCount))
                                                   };
                                 tran.BeginTransaction();  // liuch add 20110306
                                 SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql2.ToString(), param);
                                 tran.Commit(); // liuch add 20110306
                             }

                             //tran.Commit();
                             isSuc = true;
                             strMsg = "确认成功！" + "@" + EmployeeName + "@" + System.DateTime.Now.ToString("yyyy-MM-dd");
                         }
                         else
                         {
                             strMsg = "确认失败！物品数量大于源单未出库数量";
                             isSuc = false;
                         }
                    }
                    catch
                    {
                        tran.Rollback();
                        strMsg = "确认失败！请联系管理员";
                        isSuc = false;
                    }
                }
                else
                {
                    strMsg = "确认失败，库存不足！";
                    isSuc = false;
                }
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
        public static bool ScrapSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, int EmployeeID, string EmployeeName, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            ArrayList lstCmd = new ArrayList();
            if (isHandleTwo(sellOutModel.OutNo, sellOutModel.ID, sellOutModel.CompanyCD))//判断是否可被作废
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.Append(" update officedba.SellOutStorage set BillStatus=@BillStatus, ");
                    strSql.Append(" Invalidor=@Invalidor ,InvalidDate=getdate() ");
                    strSql.Append(" where CompanyCD=@CompanyCD and OutNo=@OutNo ");

                    lstCmd.Add(SqlHelper.GetParameterFromString("@Invalidor", EmployeeID.ToString()));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@BillStatus", "3"));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@BranchID", sellOutModel.BranchID.ToString()));

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
                    foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
                    {
                        StringBuilder strSql2 = new StringBuilder();
                        //回写销售订单
                        if (sellOutDetailModel.FromDetailID > 0 && ((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == "2")
                        {
                            strSql2.Append(" ;update officedba.sellorderdetail set outcount=isnull(outcount,0)-" + sellOutDetailModel.DetailCount + " where id=" + sellOutDetailModel.FromDetailID + " ");
                        }
                        strSql2.Append(" ;update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)+@ProductCount ");
                        strSql2.Append(" where StorageID=@StorageID");
                        strSql2.Append(" and ProductID=@ProductID");
                        strSql2.Append(" and BatchNo=@BatchNo ");
                        strSql2.Append(" and CompanyCD=@CompanyCD and BranchID=@BranchID ");

                        //更新库存结余表（删除记录）
                        //获取当前明细的ID
                        strSql2.Append(" ;select @BillDetailID=ID from officedba.SellOutStorageDetail where CompanyCD=@CompanyCD ");
                        strSql2.Append(" and OutNo=@BillNo and ProductID=@ProductID and BatchNo=@BatchNo ");

                        strSql2.Append(" ;delete from officedba.StorageSurplus where CompanyCD=@CompanyCD ");
                        strSql2.Append(" and BranchID=@BranchID and BillNo=@BillNo and BillDetailID=@BillDetailID ");

                        SqlParameter[] param = {
                                                    new SqlParameter("@ProductCount",sellOutDetailModel.DetailCount ),
                                                    new SqlParameter("@StorageID",sellOutModel.StorageID ),
                                                    new SqlParameter("@ProductID",sellOutDetailModel.ProductID ),
                                                    new SqlParameter("@BatchNo",sellOutDetailModel.BatchNo ),
                                                    new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                                    new SqlParameter("@BranchID",sellOutModel.BranchID ),
                                                    new SqlParameter("@BillNo",sellOutModel.OutNo ),
                                                    new SqlParameter("@BillDetailID",0)
                                                   };

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql2.ToString(), param);

                    }

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
            }
            else
            {//已经被其他人作废
                strMsg = "已经作废或者被其它单据引用的单据，不可作废！";
                isSuc = false;
            }
            return isSuc;
        }
        #endregion

        #region 取消确认销售出库单
        /// <summary>
        /// 取消确认销售出库单
        /// 取消确认前提：单据状态为执行状态，且未被其他单据（财务中和销售退货单中）引用
        /// </summary>
        /// <param name="sellOutModel">sellOutModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true取消确认成功，false取消确认失败</returns>
        public static bool CancelSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, int EmployeeID, string EmployeeName, out string strMsg)
        {
            strMsg = "";
            bool isSuc = false;
            StringBuilder strSql = new StringBuilder();
            ArrayList lstCmd = new ArrayList();
            if (isHandleTwo(sellOutModel.OutNo, sellOutModel.ID, sellOutModel.CompanyCD))//判断是否可被作废
            {
                TransactionManager tran = new TransactionManager();
                tran.BeginTransaction();
                try
                {
                    strSql.Append(" update officedba.SellOutStorage set BillStatus=@BillStatus,");
                    // strSql.Append(" Invalidor=@Invalidor ,InvalidDate=getdate() ");
                    strSql.Append(" Confirmor=NULL ,ConfirmDate=NULL ");
                    strSql.Append(" where CompanyCD=@CompanyCD and OutNo=@OutNo ");

                    // lstCmd.Add(SqlHelper.GetParameterFromString("@Invalidor", EmployeeID.ToString()));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@BillStatus", "1"));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@CompanyCD", sellOutModel.CompanyCD));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@OutNo", sellOutModel.OutNo));
                    lstCmd.Add(SqlHelper.GetParameterFromString("@BranchID", sellOutModel.BranchID.ToString()));

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
                    foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
                    {
                        StringBuilder strSql2 = new StringBuilder();
                        //回写销售订单
                        if (sellOutDetailModel.FromDetailID > 0 && ((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version=="2")
                        {
                            strSql2.Append(" ;update officedba.sellorderdetail set outcount=isnull(outcount,0)-" + sellOutDetailModel.DetailCount + " where id=" + sellOutDetailModel.FromDetailID + " ");
                        }
                        strSql2.Append(" ;update officedba.StorageProduct set ProductCount=isnull(ProductCount,0)+@ProductCount ");
                        strSql2.Append(" where StorageID=@StorageID");
                        strSql2.Append(" and ProductID=@ProductID");
                        strSql2.Append(" and BatchNo=@BatchNo ");
                        strSql2.Append(" and CompanyCD=@CompanyCD and BranchID=@BranchID ");

                        //更新库存结余表（删除记录）
                        //获取当前明细的ID
                        strSql2.Append(" ;select @BillDetailID=ID from officedba.SellOutStorageDetail where CompanyCD=@CompanyCD ");
                        strSql2.Append(" and OutNo=@BillNo and ProductID=@ProductID and BatchNo=@BatchNo ");

                        strSql2.Append(" ;delete from officedba.StorageSurplus where CompanyCD=@CompanyCD ");
                        strSql2.Append(" and BranchID=@BranchID and BillNo=@BillNo and BillDetailID=@BillDetailID ");

                        SqlParameter[] param = {
                                                    new SqlParameter("@ProductCount",sellOutDetailModel.DetailCount ),
                                                    new SqlParameter("@StorageID",sellOutModel.StorageID ),
                                                    new SqlParameter("@ProductID",sellOutDetailModel.ProductID ),
                                                    new SqlParameter("@BatchNo",sellOutDetailModel.BatchNo ),
                                                    new SqlParameter("@CompanyCD",sellOutModel.CompanyCD ),
                                                    new SqlParameter("@BranchID",sellOutModel.BranchID ),
                                                    new SqlParameter("@BillNo",sellOutModel.OutNo ),
                                                    new SqlParameter("@BillDetailID",0)
                                                   };

                        SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql2.ToString(), param);

                    }

                    tran.Commit();
                    isSuc = true;
                    strMsg = "取消确认成功！" + "@" + EmployeeName + "@" + System.DateTime.Now.ToString("yyyy-MM-dd");
                }
                catch
                {
                    tran.Rollback();
                    strMsg = "取消确认失败！请联系管理员";
                    isSuc = false;
                }
            }
            else
            {//已经被其他人作废
                strMsg = "已经取消确认或者被其它单据引用的单据，不可取消确认！";
                isSuc = false;
            }
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
            strSql.Append(" select s.ID,isnull(s.ForkliftTruck,0.00)ForkliftTruck,dbo.getCustNameById(isnull(s.LogisticsID,0))Logistics,isnull(s.LogisticsID,0)LogisticsID,isnull(s.LogisticsPrice,0.00)LogisticsPrice,s.BranchID,s.OutNo,s.CustID,s.UserID,s.StorageID,s.Contractor,s.ContractPhone,");
            strSql.Append(" s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,s.FromBillID,s.AriveAddress,");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" s.BillStatus,s.Remark,s.PreferPrice,s.TotalCount,s.TotalPrice,s.Creator,");
            strSql.Append(" CONVERT(varchar(100),s.CreateDate,23) as CreateDate,CONVERT(varchar(100),s.ExpireDay,23) as ExpireDay, ");
            strSql.Append(" s.Confirmor,s.Invalidor,s.IsBlend, s.IsUsedPromotion, ");
            strSql.Append(" case s.IsUsedPromotion when 0 then '停用' when 1 then '启用' end as IsUsedPromotionText, ");
            strSql.Append(" c.CustName as CustName,e1.EmployeeName as UserName,s2.StorageName as StorageName,s2.Admin as AdminName ,");
            strSql.Append(" s3.OrderNo as FromBillNo,e2.EmployeeName as CreatorName,");
            strSql.Append(" e3.EmployeeName as ConfirmorName,e4.EmployeeName as InvalidorName,");
            strSql.Append(" CONVERT(varchar(100),s.ConfirmDate,23) as ConfirmDate,CONVERT(varchar(100),s.InvalidDate,23) as InvalidDate,f.FlowStatus as FlowStatus, ");
            strSql.Append(" case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(" ,case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText ");
            strSql.Append(" ,s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ");

            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            strSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=s.Confirmor ");
            strSql.Append(" left join officedba.EmployeeInfo as e4 on e4.ID=s.Invalidor ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join officedba.SellOrder as s3 on s3.ID=s.FromBillID ");
            strSql.Append(" left join officedba.FlowInstance f on s.CompanyCD=f.CompanyCD and f.BillTypeFlag=5 ");
            strSql.Append(" and f.BillTypeCode=1 and f.BillNo=s.OutNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where s.ID=f2.BillID and f2.BillTypeFlag=5 and f2.BillTypeCode=1 )");
            strSql.Append(" where s.ID=@BillID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",BillID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据ID获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByIDR(int BillID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select s.ID,s.BranchID,s.OutNo,s.CustID,s.UserID,s.StorageID,s.Contractor,s.ContractPhone,");
            strSql.Append(" s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,s.FromBillID,s.AriveAddress,");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" s.BillStatus,s.Remark,s.PreferPrice,s.TotalCount,s.TotalPrice,s.Creator,");
            strSql.Append(" CONVERT(varchar(100),s.CreateDate,23) as CreateDate,CONVERT(varchar(100),s.ExpireDay,23) as ExpireDay, ");
            strSql.Append(" s.Confirmor,s.Invalidor,s.IsBlend, s.IsUsedPromotion, ");
            strSql.Append(" case s.IsUsedPromotion when 0 then '停用' when 1 then '启用' end as IsUsedPromotionText, ");
            strSql.Append(" c.CustName as CustName,e1.EmployeeName as UserName,s2.StorageName as StorageName,s2.Admin as AdminName ,");
            strSql.Append(" s3.OrderNo as FromBillNo,e2.EmployeeName as CreatorName,");
            strSql.Append(" e3.EmployeeName as ConfirmorName,e4.EmployeeName as InvalidorName,");
            strSql.Append(" CONVERT(varchar(100),s.ConfirmDate,23) as ConfirmDate,CONVERT(varchar(100),s.InvalidDate,23) as InvalidDate,f.FlowStatus as FlowStatus, ");
            strSql.Append(" case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(" ,case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText ");
            strSql.Append(" ,s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10, ");
            strSql.Append("		c.Tel,c.Mobile,c.Fax,c.email,c.QQ,c.MSN,c.WebSite,c.Post,c.ContactName,c.ReceiveAddress,c.Tel,  ");
            strSql.Append("		e1.Telephone,e1.Mobile ");
            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            strSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=s.Confirmor ");
            strSql.Append(" left join officedba.EmployeeInfo as e4 on e4.ID=s.Invalidor ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join officedba.SellOrder as s3 on s3.ID=s.FromBillID ");
            strSql.Append(" left join officedba.FlowInstance f on s.CompanyCD=f.CompanyCD and f.BillTypeFlag=5 ");
            strSql.Append(" and f.BillTypeCode=1 and f.BillNo=s.OutNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where s.ID=f2.BillID and f2.BillTypeFlag=5 and f2.BillTypeCode=1 )");
            strSql.Append(" where s.ID=@BillID ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillID",BillID)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据ID获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByNo(string BillNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select s.ID,isnull(s.ForkliftTruck,0.00)ForkliftTruck,s.BranchID,s.OutNo,s.CustID,s.UserID,s.StorageID,s.Contractor,s.ContractPhone,");
            strSql.Append(" s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,s.FromBillID,s.AriveAddress,");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" s.BillStatus,s.Remark,s.PreferPrice,s.TotalCount,s.TotalPrice,s.Creator,");
            strSql.Append(" CONVERT(varchar(100),s.CreateDate,23) as CreateDate,CONVERT(varchar(100),s.ExpireDay,23) as ExpireDay, ");
            strSql.Append(" s.Confirmor,s.Invalidor,s.IsBlend, s.IsUsedPromotion, ");
            strSql.Append(" case s.IsUsedPromotion when 0 then '停用' when 1 then '启用' end as IsUsedPromotionText, ");
            strSql.Append(" c.CustName as CustName,e1.EmployeeName as UserName,s2.StorageName as StorageName,s2.Admin as AdminName ,");
            strSql.Append(" s3.OrderNo as FromBillNo,e2.EmployeeName as CreatorName,");
            strSql.Append(" e3.EmployeeName as ConfirmorName,e4.EmployeeName as InvalidorName,");
            strSql.Append(" CONVERT(varchar(100),s.ConfirmDate,23) as ConfirmDate,CONVERT(varchar(100),s.InvalidDate,23) as InvalidDate, ");
            strSql.Append(" case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(" ,case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText ");
            strSql.Append(" ,s.ExtField1,s.ExtField2,s.ExtField3,s.ExtField4,s.ExtField5,s.ExtField6,s.ExtField7,s.ExtField8,s.ExtField9,s.ExtField10 ");

            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Creator ");
            strSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=s.Confirmor ");
            strSql.Append(" left join officedba.EmployeeInfo as e4 on e4.ID=s.Invalidor ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join officedba.SellOrder as s3 on s3.ID=s.FromBillID ");
            strSql.Append(" where s.OutNo=@BillNo ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillNo",BillNo)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion

        #region 获取销售出库单明细
        public static DataTable GetSellOutDetail(string BillNo, string strCompanyCD, int storageID, int BranchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select s.ID,s.OutNo,s.ProductID,s.BatchNo,s.DetailCount,s.DetailPrice,");
            strSql.Append(" s.DetailTotalPrice,s.BackCount,s.FromDetailID,s.PromotionID ");
            strSql.Append(" ,p.BarCode,p.ProductNo,p.ProductName,p.Specification,p.Sell,p.wholesalePrice, ");
            strSql.Append(" (select UnitName from officedba. MeasureUnit m where p.UnitID=m.ID) as UnitName ");
            strSql.Append(" ,(select ProductCount from officedba.StorageProduct s2 where s2.ProductID=s.ProductID and s2.StorageID=@storageID ");
            strSql.Append(" and s2.CompanyCD=@CompanyCD and s2.BatchNo=s.BatchNo and s2.BranchID=@BranchID) as ProductCount ");
            strSql.Append(" ,isBarCode='',isBatchNo='' ");//是否启用条码、批次
            strSql.Append(" from officedba.SellOutStorageDetail as s ");
            strSql.Append(" left join officedba.ProductInfo p on p.ID=s.ProductID ");
            strSql.Append(" where s.OutNo=@BillNo and s.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@BillNo",BillNo ),
                                    new SqlParameter("@CompanyCD",strCompanyCD ),
                                    new SqlParameter("@storageID",storageID ),
                                    new SqlParameter("@BranchID",BranchID)
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
            strSql.Append(" where s.CompanyCD=@CompanyCD  ");
            strSql.AppendLine("and (  ");
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            strSql.AppendLine(" (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));           
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
        public static DataTable GetSellOutList(SellOutStorageModel model, DateTime? SellDate1, string FromBillNo,string ProductID, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OutNo,sell.OrderNo,s.CustID,s.StorageID,s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,");
            strSql.Append(" s.TotalCount,s.TotalPrice,s.BillStatus,c.CustName as CustName,s2.StorageName as StorageName, ");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText,s.BlendPrice, ");
            strSql.Append(" s.IsBlend,case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(", f.FlowStatus,case f.FlowStatus  when 1 then '待审批' when 2 then '审批中' when 3 then '审批通过' when 4 then '审批不通过'  when 5 then '撤销审批' end as FlowStatusText ");
            strSql.Append(" ,s.BranchID,s3.DeptName as SubStoreName,ssd.id as DetailID,isnull(ssd.ProductID,'') ProductID,");
            strSql.Append(" isnull(p.ProductNo,'') ProductNo,isnull(p.productname,'') ProductName,p.Specification,p.unitid,m.UnitName, ");
            strSql.Append(" isnull(ssd.detailCount,0) DetailCount,isnull(ssd.detailprice,0) DetailPrice, ");
            strSql.Append(" isnull(ssd.DetailTotalPrice,0) DetailTotalPrice,isnull(ssd.BackCount,0) BackCount ");
            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join  [officedba].[SellOrder] as sell on sell.id=s.frombillid ");
            strSql.Append(" left join officedba.DeptInfo as s3 on s3.ID=s.BranchID ");
            strSql.Append(" left join officedba.FlowInstance f on s.CompanyCD=f.CompanyCD and f.BillTypeFlag=5 ");
            strSql.Append(" and f.BillTypeCode=1 and f.BillNo=s.OutNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where s.ID=f2.BillID and f2.BillTypeFlag=5 and f2.BillTypeCode=1 )");
            strSql.Append(" left join officedba.SellOutStorageDetail as ssd on ssd.OutNo=s.OutNo and ssd.CompanyCD=s.CompanyCD ");
            strSql.Append(" left join officedba.ProductInfo as p on p.id=ssd.productid ");
            strSql.Append(" left join officedba.MeasureUnit as m on m.id=p.unitID ");
            //strSql.Append("left join officedba.FlowTaskHistory as F1 on F.FlowNo=F1.FlowNo");
            strSql.Append(" where s.CompanyCD=@CompanyCD   ");//此处的Remark为保存BranchID串（逗号隔开）的时候用的
            strSql.AppendLine("and (  ");
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                
                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " ))  OR ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) ))  OR ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID)))  OR ");
                }
            }
            strSql.AppendLine(" (select COUNT(*) from officedba.FlowTaskHistory where FlowNo=F.FlowNo AND BillID=S.ID AND operateUserId = '" + userInfo.UserID + "')>0 ");
            strSql.AppendLine("or (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", model.Remark));
            if (model.OutNo != null)
            {
                string OutNoParam = "%" + model.OutNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
                strSql.Append(" and s.OutNo like @OutNo ");
            }
            if (model.FlowStatus != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FlowStatus", model.FlowStatus));
                strSql.Append(" and f.FlowStatus=@FlowStatus ");
            }
            if (model.StorageID != null && model.StorageID != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", model.StorageID.ToString()));
                strSql.Append(" and s.StorageID=@StorageID ");
            }
            if (model.SellType != null && model.SellType != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellType", model.SellType.ToString()));
                strSql.Append(" and s.custid=@SellType ");
            }
            if (SellDate1 != null && model.SellDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
                strSql.Append(" and s.Confirmdate>=@SellDate and s.Confirmdate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (SellDate1 != null && model.SellDate == null)//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
                strSql.Append(" and s.Confirmdate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (SellDate1 == null && model.SellDate != null)//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
                strSql.Append(" and s.Confirmdate>=@SellDate  ");
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
            if (model.IsBlend != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsBlend", model.IsBlend));
                strSql.Append(" and s.IsBlend=@IsBlend ");
            }
            if (model.BranchID != 0)
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID", model.BranchID.ToString()));
                strSql.Append(" and s.BranchID=@BranchID ");
            }
            if (!string.IsNullOrEmpty(ProductID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
                strSql.Append(" and ssd.ProductID=@ProductID");
            }
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
            strSql.Append("select ID from officedba.SellOutStorage where OutNo=@BillNo and CompanyCD=@CompanyCD ");
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
            strSql.Append(" select count(1) from officedba.SellOutStorage ");
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

            strSql.Append(" select count(1) from officedba.SellOutStorage ");
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

            strSql = "select count(1) from officedba.SellOutStorage where OutNo = @OutNo and CompanyCD=@CompanyCD and BillStatus=@BillStatus ";
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
        private static string IsEnoughStorage(List<SellOutStorageDetailModel> sellOutDMList, int? StorageID, int? BranchID)
        {
            string isEnoughStr = "";
            StringBuilder strSql = new StringBuilder();
            foreach (SellOutStorageDetailModel sellOutDetailModel in sellOutDMList)
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

            for (int iCount = 0; iCount < rowidArr.Length;iCount++ )
            {
                strSql.AppendLine(" insert into #tempTable (rowIndex,productID,batchNo)");
                strSql.AppendLine(" values("+rowidArr[iCount]+","+pdtidArr[iCount]+",'"+batchNoArr[iCount]+"')");
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
            //ArrayList SqlParamList = new ArrayList();

            //SqlParamList.Add(new SqlParameter("@CompanyCD", list[5]));
            //SqlParamList.Add(new SqlParameter("@BranchID", list[6]));

            //StringBuilder sqlStr = new StringBuilder();
            //sqlStr.AppendLine(" select c.DeptName, CustID,CustNO,CustName,OutNo as OrderNO ,convert(varchar(100),SellDate,23) as OrderDate,'销售' as OrderType, ");
            //sqlStr.AppendLine(" TotalPrice,isnull(BlendPrice,0) BlendPrice, ");
            //sqlStr.AppendLine(" isnull(TotalPrice,0)-isnull(BlendPrice,0) AccountsPrice,a.Remark,a.ID from officedba.SellOutStorage as a ");
            //sqlStr.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID  where a.BillStatus=2   ");
            //if (!String.IsNullOrEmpty(list[0]))
            //{
            //    sqlStr.AppendLine(" and CustID=@CustID ");
            //    SqlParamList.Add(new SqlParameter("@CustID", Convert.ToInt32(list[0])));
            //}
            //if (!String.IsNullOrEmpty(list[1]))
            //{
            //    sqlStr.AppendLine(" and SellDate>=Convert(datetime,@StartDate) ");
            //    SqlParamList.Add(new SqlParameter("@StartDate", list[1]));
            //}
            //if (!String.IsNullOrEmpty(list[2]))
            //{
            //    sqlStr.AppendLine(" and SellDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
            //    SqlParamList.Add(new SqlParameter("@EndDate", list[2]));
            //}
            //if (list[4] == "1")
            //{
            //    sqlStr.AppendLine(" and a.IsBlend<>2 ");
            //}
            //sqlStr.AppendLine(" and a.CompanyCD=@CompanyCD ");
            //sqlStr.AppendLine(" and a.BranchID in ("+list[6]+") ");

            //if (list[3] == "1")
            //{
            //    sqlStr.AppendLine(" union all ");

            //    sqlStr.AppendLine(" select c.DeptName,CustID,CustNO,CustName,BackNo as OrderNO,convert(varchar(100),BackDate,23) as OrderDate,'退货' as OrderType, ");
            //    sqlStr.AppendLine(" (0-TotalPrice)TotalPrice,(0-isnull(BlendPrice,0)) BlendPrice, ");
            //    sqlStr.AppendLine(" 0-(isnull(TotalPrice,0)-isnull(BlendPrice,0)) AccountsPrice ,a.Remark,a.ID  from officedba.SellBack as a ");
            //    sqlStr.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID  where a.BillStatus=2 ");
            //    if (!String.IsNullOrEmpty(list[0]))
            //    {
            //        sqlStr.AppendLine(" and CustID=@CustID ");
            //    }
            //    if (!String.IsNullOrEmpty(list[1]))
            //    {
            //        sqlStr.AppendLine(" and BackDate>=Convert(datetime,@StartDate) ");
            //    }
            //    if (!String.IsNullOrEmpty(list[2]))
            //    {
            //        sqlStr.AppendLine(" and BackDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
            //    }
            //    if (list[4] == "1")
            //    {
            //        sqlStr.AppendLine(" and a.IsBlend<>2 ");
            //    }
            //    sqlStr.AppendLine(" and a.CompanyCD=@CompanyCD ");
            //    sqlStr.AppendLine(" and a.BranchID in ("+list[6]+") ");
            //}
            //return SqlHelper.CreateSqlByPageExcuteSqlArr(sqlStr.ToString(), pageIndex, pageCount, order, SqlParamList, ref totalCount);
            ArrayList paramList = new ArrayList();
            paramList.Add(new SqlParameter("@CompanyCD", list[5]));
            paramList.Add(new SqlParameter("@BranchID", list[6]));
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(" select c.DeptName, CustID,CustNO,CustName,OutNo as OrderNO ,convert(varchar(100),SellDate,23) as OrderDate,'销售' as OrderType, ");
            builder.AppendLine(" TotalPrice,isnull(BlendPrice,0) BlendPrice, ");
            builder.AppendLine(" isnull(TotalPrice,0)-isnull(BlendPrice,0) AccountsPrice,a.Remark,a.ID,tt.acceway from officedba.SellOutStorage as a ");
            builder.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID left join (select sourceid,case when acceway=0 then '现金' when acceway=1 then '银行转账' when acceway=2 then '支票'      when acceway=3 then '承兑' when acceway=4 then '其他' else '' end acceway\r\nfrom officedba.IncomeBlendBill b  left join officedba.IncomeBill a   on a.id=b.incomeid where sourcedt='officedba.SellOutStorage') tt on tt.sourceid=a.id  where a.BillStatus=2   ");
            if (!string.IsNullOrEmpty(list[0]))
            {
                builder.AppendLine(" and CustID=@CustID ");
                paramList.Add(new SqlParameter("@CustID", Convert.ToInt32(list[0])));
            }
            if (!string.IsNullOrEmpty(list[1]))
            {
                builder.AppendLine(" and SellDate>=Convert(datetime,@StartDate) ");
                paramList.Add(new SqlParameter("@StartDate", list[1]));
            }
            if (!string.IsNullOrEmpty(list[2]))
            {
                builder.AppendLine(" and SellDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
                paramList.Add(new SqlParameter("@EndDate", list[2]));
            }
            if (list[4] == "1")
            {
                builder.AppendLine(" and a.IsBlend<>2 ");
            }
            builder.AppendLine(" and a.CompanyCD=@CompanyCD ");
            builder.AppendLine(" and a.BranchID in (" + list[6] + ") ");
            if (list[3] == "1")
            {
                builder.AppendLine(" union all ");
                builder.AppendLine(" select c.DeptName,CustID,CustNO,CustName,BackNo as OrderNO,convert(varchar(100),BackDate,23) as OrderDate,'退货' as OrderType, ");
                builder.AppendLine(" (0-TotalPrice)TotalPrice,(0-isnull(BlendPrice,0)) BlendPrice, ");
                builder.AppendLine(" 0-(isnull(TotalPrice,0)-isnull(BlendPrice,0)) AccountsPrice ,a.Remark,a.ID,tt.acceway  from officedba.SellBack as a ");
                builder.AppendLine(" inner join officedba.custinfo as b on a.custID=B.id left join officedba.deptinfo as c on a.BranchID=c.ID                 left join (select sourceid,case when acceway=0 then '现金' when acceway=1 then '银行转账' when acceway=2 then '支票'      when acceway=3 then '承兑' when acceway=4 then '其他' else '' end acceway\r\nfrom officedba.PayBlendBill b  left join officedba.PayBill a   on a.id=b.payid where sourcedt='SellBack')tt on tt.sourceid=a.id where a.BillStatus=2 ");
                if (!string.IsNullOrEmpty(list[0]))
                {
                    builder.AppendLine(" and CustID=@CustID ");
                }
                if (!string.IsNullOrEmpty(list[1]))
                {
                    builder.AppendLine(" and BackDate>=Convert(datetime,@StartDate) ");
                }
                if (!string.IsNullOrEmpty(list[2]))
                {
                    builder.AppendLine(" and BackDate<dateadd(day,1,Convert(datetime,@EndDate)) ");
                }
                if (list[4] == "1")
                {
                    builder.AppendLine(" and a.IsBlend<>2 ");
                }
                builder.AppendLine(" and a.CompanyCD=@CompanyCD ");
                builder.AppendLine(" and a.BranchID in (" + list[6] + ") ");
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(builder.ToString(), pageIndex, pageCount, order, paramList, ref totalCount);


        }

        public static DataTable GetBlendPrice(string type, string branchid, string custid, string date1, string date2, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            SqlCommand cmd = new SqlCommand();
            string str = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string str2 = " select a.id,a.payno,convert(char(10),a.paydate,120)paydate,payprice,convert(char(10),b.blenddate,120)blenddate,";
            if (type == "1")
            {
                str2 = str2 + "blendprice ";
            }
            else
            {
                str2 = str2 + "0-blendprice as blendprice ";
            }
            string str3 = str2;
            str2 = str3 + "from officedba.PayBill a left join officedba.PayBlendBill b on a.id=b.PayID                              where a.companycd='" + str + "' and branchid=" + branchid + "  and custid=" + custid + " and contactunit=" + type + " and blenddate is not null ";
            if ((date1 != "") && (date2 != ""))
            {
                str3 = str2;
                str2 = str3 + " and paydate between '" + date1 + " 00:00:00' and '" + date2 + " 23:59:59' ";
            }
            else if ((date1 != "") && (date2 == ""))
            {
                str2 = str2 + " and paydate>='" + date1 + " 00:00:00'";
            }
            else if ((date1 == "") && (date2 != ""))
            {
                str2 = str2 + " and paydate<='" + date2 + " 23:59:59'";
            }
            str2 = str2 + "union select a.id,a.incomeno payno,convert(char(10),a.paydate,120)paydate,payprice,convert(char(10),b.blenddate,120)blenddate,";
            if (type == "2")
            {
                str2 = str2 + " blendprice ";
            }
            else
            {
                str2 = str2 + " 0-blendprice as blendprice ";
            }
            str3 = str2;
            str2 = str3 + " from officedba.IncomeBill a  left join officedba.IncomeBlendBill b on a.id=b.incomeid                        where a.companycd='" + str + "' and branchid=" + branchid + "  and custid=" + custid + " and contactunit=" + type + " and blenddate is not null  ";
            if ((date1 != "") && (date2 != ""))
            {
                str3 = str2;
                str2 = str3 + " and paydate between '" + date1 + " 00:00:00' and '" + date2 + " 23:59:59' ";
            }
            else if ((date1 != "") && (date2 == ""))
            {
                str2 = str2 + " and paydate>='" + date1 + " 00:00:00'";
            }
            else if ((date1 == "") && (date2 != ""))
            {
                str2 = str2 + " and paydate<='" + date2 + " 23:59:59'";
            }
            cmd.CommandText = str2;
            return SqlHelper.PagerWithCommand(cmd, pageIndex, pageCount, order, ref totalCount);
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

        public static DataTable GetTransFeeList(SellOutStorageModel model, string ConfirmDate,string ConfirmDate1, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.OutNo,sell.OrderNo,s.CustID,s.StorageID,s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,");
            strSql.Append(" s.TotalCount,s.TotalPrice,s.BillStatus,c.CustName as CustName,s2.StorageName as StorageName, ");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText,s.BlendPrice, ");
            strSql.Append(" s.IsBlend,case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(", f.FlowStatus,case f.FlowStatus  when 1 then '待审批' when 2 then '审批中' when 3 then '审批通过' when 4 then '审批不通过'  when 5 then '撤销审批' end as FlowStatusText ");
            strSql.Append(" ,s.BranchID,s3.DeptName as SubStoreName ");
            strSql.Append(" ,s.LogisticsID,isnull(s.LogisticsPrice,0) as TransFee, ci.CustName as TransName, e.EmployeeName, e1.EmployeeName as CreateUserName,e2.EmployeeName as Confirmer ");
            strSql.Append(" ,isnull(substring(CONVERT(varchar,s.CreateDate,120),0,11),'') CreateDate,isnull(substring(CONVERT(varchar,s.ConfirmDate,120),0,11),'') ConfirmDate,s.Remark ");
            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.CustInfo as ci on ci.ID=s.LogisticsID ");
            strSql.Append(" left join officedba.EmployeeInfo as e on e.ID=s.UserID ");
            strSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=s.Creator ");
            strSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=s.Confirmor ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join  [officedba].[SellOrder] as sell on sell.id=s.frombillid ");
            strSql.Append(" left join officedba.DeptInfo as s3 on s3.ID=s.BranchID ");
            strSql.Append(" left join officedba.FlowInstance f on s.CompanyCD=f.CompanyCD and f.BillTypeFlag=5 ");
            strSql.Append(" and f.BillTypeCode=1 and f.BillNo=s.OutNo ");
            strSql.Append(" and f.ID=(select max(ID) from officedba.FlowInstance as f2 where s.ID=f2.BillID and f2.BillTypeFlag=5 and f2.BillTypeCode=1 )");
            //strSql.Append("left join officedba.FlowTaskHistory as F1 on F.FlowNo=F1.FlowNo");
            strSql.Append(" where s.CompanyCD=@CompanyCD  and isnull(s.LogisticsPrice,0)<>0  ");//此处的Remark为保存BranchID串（逗号隔开）的时候用的
            strSql.AppendLine("and (  ");
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " ))  OR ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID) ))  OR ");
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql.AppendLine("(s.Creator IN  ");
                    strSql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    strSql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    strSql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    strSql.AppendLine("  WHERE a.ID=b.ID)))  OR ");
                }
            }
            strSql.AppendLine(" (select COUNT(*) from officedba.FlowTaskHistory where FlowNo=F.FlowNo AND BillID=S.ID AND operateUserId = '" + userInfo.UserID + "')>0 ");
            strSql.AppendLine("or (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (s.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userInfo.CompanyCD));
            if (model.OutNo!="")
            {
                string OutNoParam = "%" + model.OutNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
                strSql.Append(" and s.OutNo like @OutNo ");
            }
            if (ConfirmDate1 != "" && ConfirmDate != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", ConfirmDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate1", ConfirmDate1));
                strSql.Append(" and s.ConfirmDate>=DATEADD(day,0,Convert(datetime,@ConfirmDate)) and s.ConfirmDate<DATEADD(day,1,Convert(datetime,@ConfirmDate1)) ");
            }
            else if (ConfirmDate1 != "" && ConfirmDate == "")//开始时间为空，按结束时间检索（检索出结束时间及之前的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate1", ConfirmDate1));
                strSql.Append(" and s.ConfirmDate<DATEADD(day,1,Convert(datetime,@ConfirmDate1)) ");
            }
            else if (ConfirmDate1 == "" && ConfirmDate != "")//结束时间为空，按开始时间检索（检索出开始时间及之后的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ConfirmDate", ConfirmDate));
                strSql.Append(" and s.ConfirmDate>=DATEADD(day,0,Convert(datetime,@ConfirmDate)) ");
            }
            if (model.CustID != 0)
            {
                strSql.AppendLine(" and s.CustID = @CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            }
            if (model.LogisticsID != 0)
            {
                strSql.AppendLine(" and s.LogisticsID = @LogisticsID");
                comm.Parameters.Add(SqlHelper.GetParameter("@LogisticsID", model.LogisticsID));
            }
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }


        #region 选择销售出库单明细
        ///<summary>
        ///选择销售出库单
        ///</summary>
        ///
        public static DataTable GetSellOutListByCondition1(string OrderNo, string CustID,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            string strSql = "";
            strSql = "SELECT A.ID,A.CompanyCD,A.OutNo as OrderNo,A.BranchID ,A.CustID,A.UserID ,A.StorageID, "
                        + " A.SellType,case A.SellType when '1' then '零售' when '2' then '批发' end as SellTypeText, "
                        + " A.SellDate,A.FromBillID,A.BillStatus,case a.BillStatus when '1' then '制单' when '2' then '执行' "
                        + " when '3' then '作废' end as BillStatusText,A.TotalCount,A.TotalPrice PreferedPrice,"
                       + " A.Remark ,A.Creator ,A.CreateDate, ci.CustName,ei.EmployeeName,B.EmployeeName CreatorName,"
                       + " sd.ProductID,p.ProductNo, sd.ID as DetailID, p.Productname,p.Specification,p.unitid,c.UnitName,"
                       + " isnull(sd.DetailCount,0) OrderCount, isnull(sd.detailprice,0) Price,isnull(sd.detailtotalprice,0) TotalPrice,"
                       + " isnull(sd.BackCount,0) OutCount, isnull(sd.DetailCount,0)-isnull(sd.BackCount,0) unBackCount,isnull(sd.MakedBillCount,0) MakedBillCount,"
                       + " isnull(sd.DetailCount,0)-isnull(sd.BackCount,0)-isnull(sd.MakedBillCount,0) UnMakedBillCount, "
                       + " (isnull(sd.DetailCount,0)-isnull(sd.BackCount,0)-isnull(sd.MakedBillCount,0))*isnull(sd.detailprice,0) CanMakeBill, isnull(sd.makedbill,0) MakedBill, "
                       + " (isnull(sd.DetailCount,0)-isnull(sd.BackCount,0))*isnull(sd.detailprice,0)-isnull(sd.makedbill,0)unMakedBill "
                        + " FROM  officedba.SellOutStorage AS A "
                        + " LEFT JOIN officedba.CustInfo  as ci on A.CustID=ci.ID   "
                        + " left join officedba.EmployeeInfo as ei on A.UserID=ei.ID  "
                        + " left join officedba.EmployeeInfo as B  on A.Creator=B.ID "
                        + " left join officedba.SellOutStorageDetail as sd on sd.OutNo=A.OutNo "
                        + " left join officedba.ProductInfo as p on p.id=sd.productid "
                        + " left join officedba.MeasureUnit c on p.UnitID=c.ID "
                       + "where A.CompanyCD= @CompanyCD and A.BillStatus='2'and A.CustID=@CustID and "
                       +" (isnull(sd.DetailCount,0)-isnull(sd.BackCount,0))*isnull(sd.detailprice,0)-isnull(sd.makedbill,0)>0 and (";
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    strSql += "(a.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    strSql += "(a.Creator IN  ";
                    strSql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID) )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    strSql += "(a.Creator IN  ";
                    strSql += "( SELECT ID FROM  officedba.EmployeeInfo ";
                    strSql += "  WHERE DeptID IN (SELECT a.ID  ";
                    strSql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    strSql += "  WHERE a.ID=b.ID))) or ";
                }
            }
            strSql += "(a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (a.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", userInfo.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", CustID));
            //单据编号
            if (!string.IsNullOrEmpty(OrderNo))
            {
                strSql += " and A.OutNo like @OrderNo ";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + OrderNo + "%"));
            }

            comm.CommandText = strSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 获取销售发货单明细信息（以填充发票明细列表）
        /// <summary>
        /// 获取销售发货单明细信息（以填充发票明细列表）
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select b.ID,b.CompanyCD,b.OutNo as OrderNo,b.BranchID,b.CustID,r.CustName,b.UserID,t.EmployeeName ");
            sql.AppendLine(",b.PreferPrice,b.TotalCount,b.TotalPrice as PreferedPrice,b.Remark,b.BillStatus ");
            sql.AppendLine(",(isnull(r.TotalPay,0)-isnull(r.MakedBill,0)) as CustUnMakeBill                                                          ");
            sql.AppendLine(",a.ID as DetailID,a.ProductID,isnull(p.ProductNo,'')ProductNo,isnull(p.ProductName,'') ProductName,ISNULL(p.Specification,'') as Specification ");
            sql.AppendLine(",p.UnitID,q.UnitName,a.DetailCount as OrderCount,a.DetailPrice as Price,a.DetailTotalPrice as TotalPrice                                           ");
            sql.AppendLine(",isnull(a.DetailCount,0)-isnull(a.backcount,0)-isnull(a.MakedBillCount,0) UnMakedBillCount          ");
            sql.AppendLine(",convert(numeric(20,2),(isnull(a.DetailCount,0)-isnull(a.backcount,0))*isnull(a.DetailPrice,0)-isnull(a.MakedBill,0)) unMakedBill ");
            sql.AppendLine(" from officedba.SellOutStorageDetail a                                                         ");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID                                            ");
            sql.AppendLine(" left join officedba.SellOutStorage b on b.OutNo=a.OutNo and a.CompanyCD=b.CompanyCD                ");
            sql.AppendLine(" left join officedba.MeasureUnit q on q.ID=p.UnitID                                              ");
            sql.AppendLine(" left join officedba.CustInfo r on r.ID=b.CustID                                                  ");
            sql.AppendLine(" left join officedba.EmployeeInfo t on t.id=b.UserID											  ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")";
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 获取费用明细
        public static DataTable GetSellOutStorageFeeDetail(string OutNo)
        {
            string strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select a.ID as FeeDetailID,a.CompanyCD,a.OutNo,a.FeeID,b.CodeName,convert(numeric(14,2),a.FeeTotal)FeeTotal,a.Remark,a.ModifiedDate,a.ModifiedUserID ");
            detSql.AppendLine("from officedba.SellOutStorageFeeDetail a ");
            detSql.AppendLine("left join officedba.CodeFeeType b on a.FeeID=b.ID ");
            detSql.AppendLine(" where a.CompanyCD=@CompanyCD");
            detSql.AppendLine("  and a.OutNo=@OutNo");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",strCompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNo));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion 

        #region 获取销售出库费用列表
        public static DataTable GetSellOutFeeList(string OutNo, string ConfirmDate, string ConfirmDate1, string FeeID, string BillStatus,int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select ID,OutNo,SellDate,TotalCount,TotalPrice,CustName,StorageName,BillStatusText,SubStoreName, ");
            strSql.Append(" ConfirmDate,FeeDetailID,FeeID, FeeTotal,Remark,CodeName ");
            strSql.Append("  from (select s.ID,s.OutNo,s.CustID,s.StorageID,s.SellType,CONVERT(varchar(100),s.SellDate,23) as SellDate,");
            strSql.Append(" s.TotalCount,s.TotalPrice,s.BillStatus,c.CustName as CustName,s2.StorageName as StorageName, ");
            strSql.Append(" case s.SellType when 1 then '零售' when 2 then '批发' end as SellTypeName,");
            strSql.Append(" case s.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '作废' end as BillStatusText,s.BlendPrice, ");
            strSql.Append(" s.IsBlend,case s.IsBlend when 0 then '未核销' when 1 then '核销中' when 2 then '核销完成' end as IsBlendText ");
            strSql.Append(" ,s.BranchID,s3.DeptName as SubStoreName,sf.ID as FeeDetailID,isnull(substring(CONVERT(varchar,s.ConfirmDate,120),0,11),'') ConfirmDate, ");
            strSql.Append(" sf.FeeID,convert(numeric(14,2),isnull(sf.FeeTotal,0)) FeeTotal,isnull(sf.Remark,'')Remark,isnull(cp.CodeName,'')CodeName ");
            strSql.Append(" from officedba.SellOutStorage as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID and (NatureType=2 or NatureType=3) ");
            strSql.Append(" left join officedba.StorageInfo as s2 on s2.ID=s.StorageID ");
            strSql.Append(" left join officedba.DeptInfo as s3 on s3.ID=s.BranchID ");
            strSql.Append(" left join officedba.SellOutStorageFeeDetail as sf on sf.OutNo=s.OutNo ");
            strSql.Append("left join officedba.CodeFeeType as cp on cp.id=sf.FeeID ");
            strSql.Append(" where s.CompanyCD=@CompanyCD  ");//此处的Remark为保存BranchID串（逗号隔开）的时候用的
           
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD",userInfo.CompanyCD));
            if (!string.IsNullOrEmpty(OutNo))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", "%" + OutNo + "%"));
                strSql.Append(" and s.OutNo like @OutNo ");
            }
            if (!string.IsNullOrEmpty(ConfirmDate1) &&!string.IsNullOrEmpty(ConfirmDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", ConfirmDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", ConfirmDate1));
                strSql.Append(" and s.Confirmdate>=@SellDate and s.Confirmdate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (!string.IsNullOrEmpty(ConfirmDate1) && string.IsNullOrEmpty(ConfirmDate))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate1", ConfirmDate1));
                strSql.Append(" and s.Confirmdate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
            }
            else if (string.IsNullOrEmpty(ConfirmDate1) && !string.IsNullOrEmpty(ConfirmDate))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", ConfirmDate));
                strSql.Append(" and s.Confirmdate>=@SellDate  ");
            }
            if (!string.IsNullOrEmpty(FeeID))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@FeeID", FeeID));
                strSql.Append(" and sf.FeeID=@FeeID ");
            }
            if (!string.IsNullOrEmpty(BillStatus))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus",BillStatus));
                strSql.Append(" and s.BillStatus=@BillStatus ");
            }
            strSql.Append(") a where a.FeeDetailID is not null");
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            //return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, lstCmd, ref totalCount);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion 

    }
}
