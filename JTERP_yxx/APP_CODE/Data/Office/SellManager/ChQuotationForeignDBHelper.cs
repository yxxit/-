using System;
using XBase.Model.Office.SellManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Common;
using System.Collections.Generic;

namespace XBase.Data.Office.SellManager
{
    public class ChQuotationForeignDBHelper
    {
        #region 中文报价订单插入
        public static bool InsertSellOrder(ChQuotationForeignModel model,List<ChQuotationForeignModel> modellist, out string ID)
        {
            bool isSucc = false;//是否添加成功
            ID = "";
            string strID = string.Empty;
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                if (SaveInvoice(model, out strID)) //插入主表信息
                {
                    ID = strID;
                }
                InsertInvoiceDetail(modellist, tran);//插入明细表信息
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return isSucc;

        }
        #endregion
        #region 中文报价单主表插入
        public static bool SaveInvoice(ChQuotationForeignModel model, out string ID)
        {
            bool isSucc = false;//是否添加成功
            ArrayList listADD = new ArrayList();
            StringBuilder sqlSoIn = new StringBuilder();
            sqlSoIn.AppendLine("INSERT INTO officedba.ChQuotationForeign");
            sqlSoIn.AppendLine("           (CompanyCD");
            sqlSoIn.AppendLine("           ,QuotaNo");
            sqlSoIn.AppendLine("           ,Title");
            sqlSoIn.AppendLine("           ,CustID");
            sqlSoIn.AppendLine("           ,QuoteDate");
            sqlSoIn.AppendLine("           ,Creator");
            sqlSoIn.AppendLine("           ,CreateDate ");
            sqlSoIn.AppendLine("           ,ModifiedDate )");
            sqlSoIn.AppendLine("     VALUES");
            sqlSoIn.AppendLine("           (@CompanyCD");
            sqlSoIn.AppendLine("           ,@OrderNo");
            sqlSoIn.AppendLine("           ,@Title");
            sqlSoIn.AppendLine("           ,@CustID");
            sqlSoIn.AppendLine("           ,@DeliveryDate");
            sqlSoIn.AppendLine("           ,@Creator");
            sqlSoIn.AppendLine("           ,getdate(),NULL )");
            sqlSoIn.AppendLine("set @ID=@@IDENTITY                ");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlSoIn.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeliveryDate", model.BaoJiaDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

            listADD.Add(comm);

            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    isSucc = true;
                }
                else
                {
                    ID = "0";
                }
                return isSucc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


      #region 修改中文报价单及明细    
        public static bool UpdateInvoice(ChQuotationForeignModel InvoiceM, List<ChQuotationForeignModel> InvoiceDMList)
        {
            bool isSucc = false;//是否修改成功
            string strSql = "delete from officedba.ChQuotationForeignDetail where  QuotaNo=@BillingNo  and CompanyCD=@CompanyCD ";
            SqlParameter[] paras = { new SqlParameter("@BillingNo", InvoiceM.OrderNo), new SqlParameter("@CompanyCD", InvoiceM.CompanyCD) };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                UpdateInvoice(InvoiceM); //插入主表信息
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                InsertInvoiceDetail(InvoiceDMList, tran);//插入明细表信息
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return isSucc;
        }
      #endregion

        #region 修改中文报价单主表
        public static bool UpdateInvoice(ChQuotationForeignModel InvoiceM)
        {
            bool isSucc = false;//是否添加成功
            ArrayList listADD = new ArrayList();
            SqlCommand comm = new SqlCommand();
            if (InvoiceM.ID < 0)
                return false;
            #region  发票修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.ChQuotationForeign             ");
            sqlEdit.AppendLine("   SET             ");
            sqlEdit.AppendLine("      Title = @Title                 ");
            sqlEdit.AppendLine("      ,CustID = @CustID                 ");
            sqlEdit.AppendLine("      ,QuoteDate = @QuoteDate                 ");
            sqlEdit.AppendLine("      ,ModifiedUserID = @ModifiedUserID           ");
            sqlEdit.AppendLine(" ,ModifiedDate=@ModifiedDate   ");
            sqlEdit.AppendLine(" WHERE ID=@ID");

            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", InvoiceM.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", InvoiceM.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", InvoiceM.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@QuoteDate", InvoiceM.BaoJiaDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate",DateTime.Now.ToShortDateString()));
            listADD.Add(comm);
            #endregion


            isSucc = SqlHelper.ExecuteTransWithArrayList(listADD);

            return isSucc;
        }
        #endregion
        #region 插入中文报价单明细
        private static void InsertInvoiceDetail(List<ChQuotationForeignModel> modellist, TransactionManager tran)
        {
            foreach (ChQuotationForeignModel InVDetailModel in modellist)
            {
                System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                cmdsql.AppendLine("INSERT INTO officedba.ChQuotationForeignDetail    ");
                cmdsql.AppendLine("           (SortNo                      ");
                cmdsql.AppendLine("           ,CompanyCD                    ");
                cmdsql.AppendLine("           ,ProductID                    ");
                cmdsql.AppendLine("           ,QuotaNo                      ");
                cmdsql.AppendLine("           ,Weight                       ");
                cmdsql.AppendLine("           ,SurfaceTreatment             ");
                cmdsql.AppendLine("           ,ExchangeRate                 ");
                cmdsql.AppendLine("           ,FOBprice                     ");
                cmdsql.AppendLine("           ,CIFprice                     ");
                cmdsql.AppendLine("           ,ContainerSize                ");
                cmdsql.AppendLine("           ,BaoGuangPrice                ");
                cmdsql.AppendLine("           ,CountPerCS,Remark)                  ");
                cmdsql.AppendLine("     VALUES                              ");
                cmdsql.AppendLine("           (@SortNo                      ");
                cmdsql.AppendLine("           ,@CompanyCD                   ");
                cmdsql.AppendLine("           ,@ProductID                   ");
                cmdsql.AppendLine("           ,@OrderNo                     ");
                cmdsql.AppendLine("           ,@Weight                      ");
                cmdsql.AppendLine("           ,@SurfaceTreatment            ");
                cmdsql.AppendLine("           ,@ExchangeRate                ");
                cmdsql.AppendLine("           ,@FOBprice                    ");
                cmdsql.AppendLine("           ,@CIFprice                    ");
                cmdsql.AppendLine("           ,@ContainerSize               ");
                cmdsql.AppendLine("           ,@BaoGuangPrice               ");
                cmdsql.AppendLine("           ,@CountPerCS,@Remark)                 ");
                #region 参数
                SqlParameter[] param = {
					                        new SqlParameter("@SortNo", InVDetailModel.SortNo ),
					                        new SqlParameter("@CompanyCD", InVDetailModel.CompanyCD ),
					                        new SqlParameter("@ProductID", InVDetailModel.DProductID ),
					                        new SqlParameter("@OrderNo", InVDetailModel.OrderNo ),
					                        new SqlParameter("@Weight", InVDetailModel.DOrderWeight ),
					                        new SqlParameter("@SurfaceTreatment", InVDetailModel.DSurface ),
					                        new SqlParameter("@ExchangeRate", InVDetailModel.DHuiLv ),
					                        new SqlParameter("@FOBprice", InVDetailModel.DFOBPrice ),
					                        new SqlParameter("@CIFprice", InVDetailModel.DCIFPrice ),
                                            new SqlParameter("@ContainerSize",InVDetailModel.DXiangXing),
                                            new SqlParameter("@BaoGuangPrice", InVDetailModel.DBgPrice ),
                                            new SqlParameter("@CountPerCS",InVDetailModel.DCount),
                                             new SqlParameter("@Remark",InVDetailModel.DRemark)

                                       };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null || para.Value.ToString() == "-1")
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);
            }
        }
        #endregion 

        #region 英文报价订单插入
        public static bool InsertEnQuotation(EnQuotationForeignModel model, List<EnQuotationForeignModel> modellist, out string ID)
        {
            bool isSucc = false;//是否添加成功
            ID = "";
            string strID = string.Empty;
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                if (SaveEnQuotation(model, out strID,tran)) //插入主表信息
                {
                    ID = strID;
                }
                InsertEnQuotationDetail(modellist, tran);//插入明细表信息
                string sql = " update officedba.chquotationforeign  set RefCount=isnull(RefCount,0)+1,LastRefDate=getdate() ";
                sql += " where companycd=@companycd and quotano=@quotano ";
                SqlParameter[] param = { new SqlParameter("@companycd",model.CompanyCD),
                                             new SqlParameter("@quotano", model.SourceNo)
                                       };
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql, param);
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return isSucc;

        }
        #endregion
        #region 英文报价单主表插入
        public static bool SaveEnQuotation(EnQuotationForeignModel model, out string ID,TransactionManager tran)
        {
            bool isSucc = false;//是否添加成功
            ArrayList listADD = new ArrayList();
            StringBuilder sqlSoIn = new StringBuilder();
            sqlSoIn.AppendLine("INSERT INTO officedba.EnQuotationForeign");
            sqlSoIn.AppendLine("           (CompanyCD");
            sqlSoIn.AppendLine("           ,QuotaNo");
            sqlSoIn.AppendLine("           ,Title");
            sqlSoIn.AppendLine("           ,CustID");
            sqlSoIn.AppendLine("           ,QuoteDate");
            sqlSoIn.AppendLine("           ,Creator");
            sqlSoIn.AppendLine("           ,CreateDate ");
            sqlSoIn.AppendLine("           ,SourceNo,ModifiedDate )");
            sqlSoIn.AppendLine("     VALUES");
            sqlSoIn.AppendLine("           (@CompanyCD");
            sqlSoIn.AppendLine("           ,@OrderNo");
            sqlSoIn.AppendLine("           ,@Title");
            sqlSoIn.AppendLine("           ,@CustID");
            sqlSoIn.AppendLine("           ,@DeliveryDate");
            sqlSoIn.AppendLine("           ,@Creator");
            sqlSoIn.AppendLine("           ,getdate() ");
            sqlSoIn.AppendLine("           ,@SourceNo,NULL  )");
            sqlSoIn.AppendLine("set @ID=@@IDENTITY                ");
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlSoIn.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.QuotaNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@DeliveryDate", model.BaoJiaDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@SourceNo", model.SourceNo));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

            listADD.Add(comm);

            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    isSucc = true;
                }
                else
                {
                    ID = "0";
                }
                return isSucc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region 插入英文报价单明细
        private static void InsertEnQuotationDetail(List<EnQuotationForeignModel> modellist, TransactionManager tran)
        {
            foreach (EnQuotationForeignModel InVDetailModel in modellist)
            {
                System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                cmdsql.AppendLine("INSERT INTO officedba.EnQuotationForeignDetail    ");
                cmdsql.AppendLine("           (SortNo                      ");
                cmdsql.AppendLine("           ,CompanyCD                    ");
                cmdsql.AppendLine("           ,ProductID                    ");
                cmdsql.AppendLine("           ,QuotaNo                      ");
                cmdsql.AppendLine("           ,Weight                       ");
                cmdsql.AppendLine("           ,SurfaceTreatment             ");
                cmdsql.AppendLine("           ,FOBprice                     ");
                cmdsql.AppendLine("           ,CIFprice                     ");
                cmdsql.AppendLine("           ,ContainerSize                ");
                cmdsql.AppendLine("           ,CountPerCS,Picture, Remark,EnProductName,CurrencyType,PaymentTerm  )      ");
                cmdsql.AppendLine("     VALUES                              ");
                cmdsql.AppendLine("           (@SortNo                      ");
                cmdsql.AppendLine("           ,@CompanyCD                   ");
                cmdsql.AppendLine("           ,@ProductID                   ");
                cmdsql.AppendLine("           ,@OrderNo                     ");
                cmdsql.AppendLine("           ,@Weight                      ");
                cmdsql.AppendLine("           ,@SurfaceTreatment            ");
                cmdsql.AppendLine("           ,@FOBprice                    ");
                cmdsql.AppendLine("           ,@CIFprice                    ");
                cmdsql.AppendLine("           ,@ContainerSize               ");
                cmdsql.AppendLine("           ,@CountPerCS,@Picture,@Remark,@EnProductName,@CurrencyType,@PaymentTerm )  ");
                #region 参数
                SqlParameter[] param = {
					                        new SqlParameter("@SortNo", InVDetailModel.DSortNo ),
					                        new SqlParameter("@CompanyCD", InVDetailModel.DCompanyCD ),
					                        new SqlParameter("@ProductID", InVDetailModel.DProductID ),
					                        new SqlParameter("@OrderNo", InVDetailModel.DQuotaNo ),
					                        new SqlParameter("@Weight", InVDetailModel.DWeight ),
					                        new SqlParameter("@SurfaceTreatment", InVDetailModel.DSurface ),
					                        new SqlParameter("@FOBprice", InVDetailModel.DFOBPrice ),
					                        new SqlParameter("@CIFprice", InVDetailModel.DCIFPrice ),
                                            new SqlParameter("@ContainerSize",InVDetailModel.DXiangXing),
                                            new SqlParameter("@CountPerCS",InVDetailModel.DCount),
                                            new SqlParameter("@Picture",InVDetailModel.DPicture),
                                             new SqlParameter("@Remark",InVDetailModel.DRemark),
                                             new SqlParameter("@EnProductName",InVDetailModel.DEnProductName),
                                             new SqlParameter("@CurrencyType",InVDetailModel.DCurrencyType),
                                             new SqlParameter("@PaymentTerm",InVDetailModel.DPaymentTerm)

                                       };
                foreach (SqlParameter para in param)
                {
                    if (para.Value == null || para.Value.ToString() == "-1")
                    {
                        para.Value = DBNull.Value;
                    }
                }
                #endregion
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, cmdsql.ToString(), param);
            }
        }
        #endregion 

        #region 修改英文报价单及明细
        public static bool UpdateEnQuotation(EnQuotationForeignModel InvoiceM, List<EnQuotationForeignModel> InvoiceDMList)
        {
            bool isSucc = false;//是否修改成功
            string strSql = "delete from officedba.EnQuotationForeignDetail where  QuotaNo=@BillingNo  and CompanyCD=@CompanyCD ";
            SqlParameter[] paras = { new SqlParameter("@BillingNo", InvoiceM.QuotaNo), new SqlParameter("@CompanyCD", InvoiceM.CompanyCD) };
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                UpdateEnQuotation(InvoiceM); //插入主表信息
                SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), paras);
                InsertEnQuotationDetail(InvoiceDMList, tran);//插入明细表信息
                tran.Commit();
                isSucc = true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            return isSucc;
        }
        #endregion
        #region 修改英文报价单主表
        public static bool UpdateEnQuotation(EnQuotationForeignModel InvoiceM)
        {
            bool isSucc = false;//是否添加成功
            ArrayList listADD = new ArrayList();
            SqlCommand comm = new SqlCommand();
            if (InvoiceM.ID < 0)
                return false;
            #region  修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.EnQuotationForeign             ");
            sqlEdit.AppendLine("   SET             ");
            sqlEdit.AppendLine("      Title = @Title                 ");
            sqlEdit.AppendLine("      ,CustID = @CustID                 ");
            sqlEdit.AppendLine("      ,QuoteDate = @QuoteDate                 ");
            sqlEdit.AppendLine("      ,ModifiedUserID = @ModifiedUserID           ");
            sqlEdit.AppendLine(" ,ModifiedDate=@ModifiedDate   ");
            sqlEdit.AppendLine(" ,SourceNo=@SourceNo   ");
            sqlEdit.AppendLine(" WHERE ID=@ID");

            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", InvoiceM.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", InvoiceM.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", InvoiceM.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@QuoteDate", InvoiceM.BaoJiaDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToShortDateString()));
            comm.Parameters.Add(SqlHelper.GetParameter("@SourceNo",InvoiceM.SourceNo));
            listADD.Add(comm);
            #endregion


            isSucc = SqlHelper.ExecuteTransWithArrayList(listADD);

            return isSucc;
        }
        #endregion

        #region 删除中文报价单
        /// <summary>
        /// 删除中文报价单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteInvoice(string IDS)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList listADD = new ArrayList();
            ArrayList listADD1 = new ArrayList();
            bool isSuc = false;
            string[] arrID = IDS.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    DataTable dt = GetInvoiceDataByID(arrID[i].ToString());
                    try
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("delete from officedba.ChQuotationForeignDetail where CompanyCD=@CompanyCD and QuotaNo=@QuotaNo ");
                        SqlCommand com = new SqlCommand();
                        com.CommandText = sql.ToString();
                        com.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        com.Parameters.Add(SqlHelper.GetParameter("@QuotaNo", dt.Rows[0]["QuotaNo"].ToString()));
                        listADD1.Add(com);
                        isSuc = SqlHelper.ExecuteTransWithArrayList(listADD1);

                        StringBuilder sqlMaster = new StringBuilder();
                        sqlMaster.AppendLine("delete from officedba.ChQuotationForeign where CompanyCD=@CompanyCD and ID=@ID");
                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = sqlMaster.ToString();
                        comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                        listADD.Add(comm);
                        isSuc = SqlHelper.ExecuteTransWithArrayList(listADD);
                    }
                    catch
                    {
                        isSuc = false;
                    }
                }
            }
            return isSuc;
        }
        #endregion

        #region 删除英文报价单
        /// <summary>
        /// 删除英文报价单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteEnQuotation(string IDS)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            ArrayList listADD = new ArrayList();
            ArrayList listADD1 = new ArrayList();
            bool isSuc = false;
            string[] arrID = IDS.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    DataTable dt = GetEnQuotationByID(arrID[i].ToString());
                    TransactionManager tran = new TransactionManager();
                    tran.BeginTransaction();
                    try
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("delete from officedba.EnQuotationForeignDetail where CompanyCD=@CompanyCD and QuotaNo=@QuotaNo ");
                        SqlCommand com = new SqlCommand();
                        com.CommandText = sql.ToString();
                        com.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        com.Parameters.Add(SqlHelper.GetParameter("@QuotaNo", dt.Rows[0]["QuotaNo"].ToString()));
                        listADD1.Add(com);
                        isSuc = SqlHelper.ExecuteTransWithArrayList(listADD1);

                        StringBuilder sqlMaster = new StringBuilder();
                        sqlMaster.AppendLine("delete from officedba.EnQuotationForeign where CompanyCD=@CompanyCD and ID=@ID");
                        SqlCommand comm = new SqlCommand();
                        comm.CommandText = sqlMaster.ToString();
                        comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                        comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                        listADD.Add(comm);

                        string sql_sel = " select * from officedba.ChQuotationForeign where  CompanyCD=@CompanyCD and QuotaNo=@QuotaNo ";
                        SqlParameter[] paras = { new SqlParameter("@QuotaNo", dt.Rows[0]["SourceNo"].ToString()), new SqlParameter("@CompanyCD", CompanyCD) };
                        DataTable dt_sel = SqlHelper.ExecuteSql(sql_sel.ToString(), paras);
                        if (Convert.ToInt32(dt_sel.Rows[0]["RefCount"].ToString()) == 1)
                        {
                            string sql_u1 = " update officedba.chquotationforeign  set RefCount=0,LastRefDate=null ";
                            sql_u1 += " where companycd=@companycd and quotano=@quotano ";
                            SqlParameter[] param = { new SqlParameter("@companycd",CompanyCD),
                                             new SqlParameter("@quotano",dt.Rows[0]["SourceNo"].ToString())
                                       };
                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql_u1, param);
                        }
                        if (Convert.ToInt32(dt_sel.Rows[0]["RefCount"].ToString()) > 1)
                        {
                            string sql_en = "select * from officedba.EnQuotationForeign ";
                            sql_en += "where id in (select max(id) id  from officedba.EnQuotationForeign  where companycd=@companycd and sourceno=@quotano)";
                            SqlParameter[] paras1 = { new SqlParameter("@quotano", dt.Rows[0]["SourceNo"].ToString()), new SqlParameter("@companycd", CompanyCD) };
                            DataTable dt_enref = SqlHelper.ExecuteSql(sql_en.ToString(),paras1);
                            string sql_u2 = " update officedba.chquotationforeign  set RefCount=RefCount-1,LastRefDate=@lastrefdate ";
                            sql_u2 += " where companycd=@companycd and quotano=@quotano ";
                            SqlParameter[] param1 = { new SqlParameter("@companycd",CompanyCD),
                                             new SqlParameter("@quotano",dt.Rows[0]["SourceNo"].ToString()),
                                             new SqlParameter("@lastrefdate",dt_enref.Rows[0]["createdate"].ToString())
                                       };
                            SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, sql_u2, param1);
                        }

                        tran.Commit();
                        isSuc = SqlHelper.ExecuteTransWithArrayList(listADD);
                    }
                    catch
                    {
                        isSuc = false;
                    }
                }
            }
            return isSuc;
        }
        #endregion
        #region  根据编号获取中文报价单详细信息
        /// <summary>
        /// 根据编号获取中文报价单详细信息
        /// </summary>
        /// <param name="QuotaNo">报价单编号</param>
        /// <returns>datatable</returns>
        public static DataTable GetInvoiceDetailByNo(string CompanyCD,string QuotaNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.ID,a.CompanyCD,a.QuotaNo,a.SortNo,a.ProductID,a.Weight ");
            strSql.Append(" ,a.SurfaceTreatment,a.ExchangeRate,a.FOBprice,a.CIFprice,a.ContainerSize,a.CountPerCS,a.BaoGuangPrice,a.Remark ");
            strSql.Append(" ,p.ProdNo ProductNo,p.ProductName ");
            strSql.Append(" from officedba.ChQuotationForeignDetail as a ");
            strSql.Append(" left join officedba.productinfo as p on p.ID=a.ProductID ");
            strSql.Append(" where a.QuotaNo=@QuotaNo and a.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@QuotaNo",QuotaNo),
                                    new SqlParameter("@CompanyCD",CompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion
        #region  根据编号获取英文报价单详细信息
        /// <summary>
        /// 根据编号获取英文报价单详细信息
        /// </summary>
        /// <param name="QuotaNo">报价单编号</param>
        /// <returns>datatable</returns>
        public static DataTable GetEnQuotationByNo(string CompanyCD, string QuotaNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select a.ID,a.CompanyCD,a.QuotaNo,a.SortNo,a.ProductID,a.Weight ");
            strSql.Append(" ,a.SurfaceTreatment,a.FOBprice,a.CIFprice,a.ContainerSize,a.CountPerCS,a.Picture,a.Remark ");
            strSql.Append(" ,case when a.EnProductName='' then p.ProductName when a.EnProductName is null then p.ProductName else a.EnProductName end as EnProductName ");
            strSql.Append(" ,a.CurrencyType,a.PaymentTerm,p.ProdNo ProductNo,p.ProductName ");
            strSql.Append(" from officedba.EnQuotationForeignDetail as a ");
            strSql.Append(" left join officedba.productinfo as p on p.ID=a.ProductID ");
            strSql.Append(" where a.QuotaNo=@QuotaNo and a.CompanyCD=@CompanyCD ");
            SqlParameter[] param = { 
                                    new SqlParameter("@QuotaNo",QuotaNo),
                                    new SqlParameter("@CompanyCD",CompanyCD)
                                   };
            return SqlHelper.ExecuteSql(strSql.ToString(), param);
        }
        #endregion


        #region 通过检索条件查询中文报价单信息
        /// <summary>
        /// 通过检索条件查询中文报价单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetInvoiceList(ChQuotationForeignModel model, string CreateDate, string CreateDate1, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT a.ID,a.QuotaNo,a.Title,a.CustID,b.CustName ");
            sql.AppendLine("       ,CONVERT(varchar(100),a.QuoteDate,23) QuoteDate,a.Creator,CONVERT(varchar(100),a.CreateDate,23) CreateDate,c.EmployeeName  ");
            sql.AppendLine("       ,a.ModifiedUserID,d.EmployeeName as ModifiedUserName,CONVERT(varchar(100),a.ModifiedDate,23) ModifiedDate  ");
            sql.AppendLine(" ,cqd.ProductID,p.ProdNo,p.ProductName,cqd.Weight,cqd.SurfaceTreatment,cqd.ExchangeRate,cqd.FOBprice,cqd.CIFprice ");
            sql.AppendLine(" ,cqd.ContainerSize,cqd.CountPerCS,cqd.BaoGuangPrice,cqd.Remark ");
            sql.AppendLine("   FROM officedba.ChQuotationForeign as a          ");
            sql.AppendLine(" left join officedba.ChQuotationForeignDetail as cqd on cqd.companycd=a.companycd and cqd.QuotaNo=a.QuotaNo ");
            sql.AppendLine("  left join officedba.ProductInfo as p on p.companycd=cqd.companycd and p.id=cqd.productid  ");
            sql.AppendLine("left join officedba.CustInfo AS b ON a.CustID = b.ID  ");
            sql.AppendLine("left join officedba.EmployeeInfo AS c ON a.Creator = c.ID  ");
            sql.AppendLine(" left join officedba.EmployeeInfo as d on d.ID=a.ModifiedUserID ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("and (  ");
            DataTable dt1 = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                if (dt1.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            sql.AppendLine("(a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

            if (model.CustID != 0)
            {
                sql.AppendLine(" and a.CustID = @CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            }
            if (CreateDate1 != "" && CreateDate != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate1", CreateDate1));
                sql.Append(" and a.CreateDate>=DATEADD(day,0,Convert(datetime,@CreateDate)) and a.CreateDate<DATEADD(day,1,Convert(datetime,@CreateDate1)) ");
            }
            else if (CreateDate1 != "" && CreateDate == "")//开始时间为空，按结束时间检索（检索出结束时间及之前的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate1", CreateDate1));
                sql.Append(" and a.CreateDate<DATEADD(day,1,Convert(datetime,@CreateDate1)) ");
            }
            else if (CreateDate1 == "" && CreateDate != "")//结束时间为空，按开始时间检索（检索出开始时间及之后的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
                sql.Append(" and a.CreateDate>=DATEADD(day,0,Convert(datetime,@CreateDate))  ");
            }
            if (!string.IsNullOrEmpty(model.OrderNo))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillingNo", "%"+model.OrderNo+"%"));
                sql.AppendLine(" and a.QuotaNo Like @BillingNo ");
            }
            if (!string.IsNullOrEmpty(model.DProductName))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@productname", "%" + model.DProductName + "%"));
                sql.AppendLine(" and p.productname like @productname ");
            }
            if (!string.IsNullOrEmpty(model.DPyShort))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@pyshort", "%" + model.DPyShort + "%"));
                sql.AppendLine(" and p.PYShort like @pyshort ");
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            //sql.Append(" order by a.QuoteDate desc ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region 通过检索条件查询英文报价单信息
        /// <summary>
        /// 通过检索条件查询英文报价单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetEnQuotationList(EnQuotationForeignModel model, string CreateDate, string CreateDate1, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT a.ID,a.QuotaNo,a.Title,a.CustID,b.CustName ");
            sql.AppendLine("       ,CONVERT(varchar(100),a.QuoteDate,23) QuoteDate,a.Creator,CONVERT(varchar(100),a.CreateDate,23) CreateDate,c.EmployeeName  ");
            sql.AppendLine("       ,a.ModifiedUserID,d.EmployeeName as ModifiedUserName,CONVERT(varchar(100),a.ModifiedDate,23) ModifiedDate,a.SourceNo  ");
            sql.AppendLine("  ,eqd.ProductID,p.ProductName,eqd.EnProductName,p.ProdNo,eqd.Weight,eqd.SurfaceTreatment,eqd.FOBprice,eqd.CIFprice ");
            sql.AppendLine(", eqd.ContainerSize,eqd.CountPerCS,eqd.Picture,eqd.Remark,eqd.PaymentTerm,eqd.CurrencyType ");
            sql.AppendLine("   FROM officedba.EnQuotationForeign as a          ");
            sql.AppendLine("left join officedba.CustInfo AS b ON a.CustID = b.ID  ");
            sql.AppendLine(" left join officedba.EnQuotationForeignDetail as eqd on eqd.companycd=a.companycd and eqd.quotano=a.quotano   ");
            sql.AppendLine(" left join officedba.ProductInfo as p on p.companycd=eqd.companycd and  p.id=eqd.productid   ");
            sql.AppendLine("left join officedba.EmployeeInfo AS c ON a.Creator = c.ID  ");
            sql.AppendLine(" left join officedba.EmployeeInfo as d on d.ID=a.ModifiedUserID ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  ");
            sql.AppendLine("and (  ");
            DataTable dt1 = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                if (dt1.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            sql.AppendLine("(a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

            if (model.CustID != 0)
            {
                sql.AppendLine(" and a.CustID = @CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            }
            if (CreateDate1 != "" && CreateDate != "")
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate1", CreateDate1));
                sql.Append(" and a.CreateDate>=DATEADD(day,0,Convert(datetime,@CreateDate)) and a.CreateDate<DATEADD(day,1,Convert(datetime,@CreateDate1)) ");
            }
            else if (CreateDate1 != "" && CreateDate == "")//开始时间为空，按结束时间检索（检索出结束时间及之前的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate1", CreateDate1));
                sql.Append(" and a.CreateDate<DATEADD(day,1,Convert(datetime,@CreateDate1)) ");
            }
            else if (CreateDate1 == "" && CreateDate != "")//结束时间为空，按开始时间检索（检索出开始时间及之后的所有记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", CreateDate));
                sql.Append(" and a.CreateDate>=DATEADD(day,0,Convert(datetime,@CreateDate))  ");
            }
            if (!string.IsNullOrEmpty(model.QuotaNo))
            {
                sql.Append(" and a.QuotaNo like @QuotaNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@QuotaNo","%"+model.QuotaNo+"%"));
            }
            if (!string.IsNullOrEmpty(model.DEnProductName))
            {
                sql.Append(" and p.ProductName like @productname ");
                sql.Append(" or eqd.EnProductName like @productname ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@productname", "%" + model.DEnProductName + "%"));
            }
            if (!string.IsNullOrEmpty(model.DPyshort))
            {
                sql.Append(" and p.PYShort like @pyshort ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@pyshort", "%" + model.DPyshort + "%"));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            //sql.Append(" order by a.QuoteDate desc ");
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region 获取中文报价单BYID
        /// <summary>
        /// 获取信息BYID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetInvoiceDataByID(string ID)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  a.ID,a.QuotaNo,a.Title,a.CustID,b.CustName,");
            sb.Append(" CONVERT(varchar(100),a.QuoteDate,23) QuoteDate,a.Creator,CONVERT(varchar(100),a.CreateDate,23) CreateDate,c.EmployeeName,");
            sb.Append("a.ModifiedUserID,d.EmployeeName as ModifiedUserName,CONVERT(varchar(100),a.ModifiedDate,23) ModifiedDate ");
            sb.Append("FROM officedba.ChQuotationForeign a ");
            sb.Append("left join officedba.CustInfo AS b ON a.CustID = b.ID ");
            sb.Append("left join officedba.EmployeeInfo AS c ON a.Creator = c.ID   ");
            sb.Append(" left join officedba.EmployeeInfo as d on d.ID=a.ModifiedUserID ");
            sb.Append(" where A.ID=@ID ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@ID", ID));
            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }
        #endregion

        #region 获取英文报价单by ID
        /// <summary>
        /// 获取英文报价单by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetEnQuotationByID(string ID)
        {
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  a.ID,a.QuotaNo,a.Title,a.CustID,b.CustName,");
            sb.Append(" CONVERT(varchar(100),a.QuoteDate,23) QuoteDate,a.Creator,CONVERT(varchar(100),a.CreateDate,23) CreateDate,c.EmployeeName,");
            sb.Append("a.ModifiedUserID,d.EmployeeName as ModifiedUserName,CONVERT(varchar(100),a.ModifiedDate,23) ModifiedDate,a.SourceNo ");
            sb.Append("FROM officedba.EnQuotationForeign a ");
            sb.Append("left join officedba.CustInfo AS b ON a.CustID = b.ID ");
            sb.Append("left join officedba.EmployeeInfo AS c ON a.Creator = c.ID   ");
            sb.Append(" left join officedba.EmployeeInfo as d on d.ID=a.ModifiedUserID ");
            sb.Append(" where A.ID=@ID ");
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@ID", ID));
            return SqlHelper.ExecuteSql(sb.ToString(), arr);
        }
        #endregion


        #region 获取中文报价单历史单据
        public static DataTable GetSellOutHistoryList(ChQuotationForeignModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            ArrayList lstCmd = new ArrayList();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            strSql.Append(" select s.ID,s.QuotaNo,s.CustID,s.Title,CONVERT(varchar(100),s.QuoteDate,23) QuoteDate ,");
            strSql.Append(" s.Creator,CONVERT(varchar(100),s.CreateDate,23) CreateDate ,c.CustName as CustName ");
            strSql.Append(" ,isnull(s.RefCount,0) RefCount,CONVERT(varchar(100),s.LastRefDate,23) LastRefDate ");
            strSql.Append(" from officedba.ChQuotationForeign as s ");
            strSql.Append(" left join officedba.CustInfo as c on c.ID=s.CustID ");
            //strSql.Append(" and (NatureType=2 or NatureType=3) ");
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
            strSql.AppendLine(" (s.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (model.OrderNo != null)
            {
                string OutNoParam = "%" + model.OrderNo + "%";
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OutNo", OutNoParam));
                strSql.Append(" and s.QuotaNo like @OutNo ");
            }
            
            comm.CommandText = strSql.ToString();
            lstCmd.Add(comm);
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

    }
}
