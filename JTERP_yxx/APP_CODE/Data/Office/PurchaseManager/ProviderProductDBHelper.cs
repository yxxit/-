/***********************************************
 * 类作用：   采购管理事务层处理               *
 * 建立人：   宋飞                          *
 * 修改人：   王保军                          *
 * 建立时间： 2009/04/27                       *
 * 修改时间： 2009/08/27                       *
 ***********************************************/
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.ProductionManager;
using XBase.Model.Office.SellManager;
using System.Collections;
using XBase.Common;
using System.Data.SqlTypes;

namespace XBase.Data.Office.PurchaseManager
{
    /// <summary>
    /// 类名：ProviderProductDBHelper
    /// 描述：采购供应商物品推荐数据库层处理
    /// 
    /// 作者：宋飞
    /// 创建时间：2009/04/28
    /// 最后修改时间：2009/04/28
    /// </summary>
    ///
    public class ProviderProductDBHelper
    {
        #region 插入供应商物品推荐
        public static bool InsertProviderProduct(ProviderProductModel model, out string ID)
        {

            ArrayList listADD = new ArrayList();
            bool result = false;
            ID = "0";

            #region  采购供应商联系人添加SQL语句
            StringBuilder sqlArrive = new StringBuilder();


            sqlArrive.AppendLine("INSERT INTO officedba.ProviderProduct");
            sqlArrive.AppendLine("(CompanyCD,CustNo,ProductID,Grade,Remark,JoinDate,Joiner)");
            sqlArrive.AppendLine("VALUES (@CompanyCD,@CustNo,@ProductID,@Grade,@Remark,@JoinDate,@Joiner)");
            sqlArrive.AppendLine("set @ID=@@IDENTITY");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Grade", model.Grade));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@JoinDate", model.JoinDate == null
                                                        ? SqlDateTime.Null
                                                        :SqlDateTime.Parse(model.JoinDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Joiner", model.Joiner));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion


            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 更新供应商联系人
        public static bool UpdateProviderProduct(ProviderProductModel model)
        {
            if (model.ID <= 0)
            {
                return false;
            }
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region  修改供应商联系人
            StringBuilder sqlArrive = new StringBuilder();

            sqlArrive.AppendLine("Update  Officedba.ProviderProduct set CompanyCD=@CompanyCD,");
            sqlArrive.AppendLine("CustNo=@CustNo,ProductID=@ProductID,Grade=@Grade,Remark=@Remark,");
            sqlArrive.AppendLine("JoinDate=@JoinDate,Joiner=@Joiner where CompanyCD=@CompanyCD and ID=@ID");
            

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustNo", model.CustNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", model.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Grade", model.Grade));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@JoinDate", model.JoinDate == null
                                                        ? SqlDateTime.Null
                                                        : SqlDateTime.Parse(model.JoinDate.ToString())));
            comm.Parameters.Add(SqlHelper.GetParameter("@Joiner", model.Joiner));
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.CommandText = sqlArrive.ToString();


            listADD.Add(comm);
            #endregion

            try
            {
                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region 查询采购供应商物品推荐列表所需数据
        public static DataTable SelectProviderProductList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string CustNo, string ProductID, string Grade, string Joiner, string StartJoinDate, string EndJoinDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,isnull(B.CustName,'') AS CustName,A.ProductID,isnull(C.ProductName,'') AS  ProductName,A.Grade ");
            sql.AppendLine("      ,case A.Grade when '1' then '低' when '2' then '中' ");
            sql.AppendLine("      when '3' then '高' end AS  GradeName");
            sql.AppendLine("   ,isnull(A.Joiner,0) AS Joiner ,isnull(D.EmployeeName,'') AS JoinerName,isnull(Convert(varchar(100),A.JoinDate,23),'')  AS JoinDate ");
            sql.AppendLine(" FROM officedba.ProviderProduct AS A                                   ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.CustNo=B.CustNo");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.ProductID=C.ID");
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND  A.Joiner=D.ID");


            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD =@CompanyCD ");
            if (CustNo != "" && CustNo != null)
            {
                sql.AppendLine(" AND A.CustNo=@CustNo ");
            }
            if (ProductID != null && ProductID != "")
            {
                sql.AppendLine(" AND A.ProductID =@ProductID");
            }
            if (Grade != "" && Grade != null)
            {
                sql.AppendLine(" AND A.Grade=@Grade ");
            }
            if (Joiner != null && Joiner != "")
            {
                sql.AppendLine(" AND A.Joiner =@Joiner");
            }
            if (StartJoinDate != null && StartJoinDate != "")
            {
                sql.AppendLine(" AND A.JoinDate >= @StartJoinDate");
            }
            if (EndJoinDate != "" && EndJoinDate != "")
            {
                sql.AppendLine(" AND A.JoinDate <= @EndJoinDate ");
            }
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", CustNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Grade", Grade));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Joiner", Joiner));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartJoinDate", StartJoinDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndJoinDate", EndJoinDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载单个供应商物品推荐
        public static DataTable SelectProviderProduct(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID ,A.CustNo ,B.CustName AS CustName ,A.ProductID ,C.ProductName AS  ProductName,A.Grade  ");
            sql.AppendLine("     ,A.Remark, isnull(A.Joiner,0) AS Joiner,isnull(D.EmployeeName,'') AS JoinerName,Convert(varchar(100),A.JoinDate,23) AS JoinDate ");

            sql.AppendLine(" FROM officedba.ProviderProduct AS A                                                                  ");
            sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND A.CustNo=B.CustNo              ");
            sql.AppendLine("LEFT JOIN officedba.ProductInfo AS C ON A.CompanyCD = C.CompanyCD AND  A.ProductID=C.ID"               );
            sql.AppendLine("LEFT JOIN officedba.EmployeeInfo AS D ON A.CompanyCD = D.CompanyCD AND  A.Joiner=D.ID");

            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            sql.AppendLine(" AND A.ID =@ID");

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID", Convert .ToString (ID ));
            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 删除供应商物品推荐
        public static bool DeleteProviderProduct(string ID, string CompanyCD)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                //allUserID = sb.ToString();
                allID = sb.ToString().Replace("''", "','");
                Delsql[0] = "delete from  officedba.ProviderProduct where ID IN (" + allID + ") and CompanyCD = @CompanyCD ";
                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 查找加载合同明细
        public static DataTable ISCunzaiProviderProduct(int ProductID, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT A.ID");
            sql.AppendLine(" FROM officedba.ProviderProduct AS A     ");
            sql.AppendLine("where A.CompanyCD =@CompanyCD AND A.ProductID =@ProductID ");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ProductID", Convert .ToString ( ProductID ));
            return SqlHelper.ExecuteSql(sql.ToString(),param );
        }
        #endregion

        #region 插入供应商物品关联
        public static void SaveProvider2Product(List<Provider2ProductModel> Provider2ProductMList,TransactionManager tran)
        {
            foreach (Provider2ProductModel Provider2ProductM in Provider2ProductMList)
            {
                    StringBuilder strSql = new StringBuilder();
                    //SQL语句
                    strSql.AppendLine(" insert into officedba.Provider2Product ( ");
                    //strSql.AppendLine(@" CompanyCD,Deptype,ProviderID,LinkManID,LinkManName,LinkerTel,SortNo,ProductID,ProductNo,ProductName,Remark,ModifiedUserID,ModifiedDate ");
                    strSql.AppendLine(@" CompanyCD,Deptype,ProviderID,SortNo,ProductID,ProductNo,ProductName,Remark,ModifiedUserID,ModifiedDate ");
                    strSql.AppendLine(") values (");
                    //strSql.AppendLine(@" @CompanyCD,@Deptype,@ProviderID,@LinkManID,@LinkManName,@LinkerTel,@SortNo,@ProductID,@ProductNo,@ProductName,@Remark,@ModifiedUserID,getdate() )");
                    strSql.AppendLine(@" @CompanyCD,@Deptype,@ProviderID,@SortNo,@ProductID,@ProductNo,@ProductName,@Remark,@ModifiedUserID,getdate() )");

                    //参数
                    SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
                    new SqlParameter("@Deptype", SqlDbType.Int,4),
					new SqlParameter("@ProviderID", SqlDbType.Int,4),
                    //new SqlParameter("@LinkManID",SqlDbType.Int,4),
                    //new SqlParameter("@LinkManName",SqlDbType.VarChar,30),
                    //new SqlParameter("@LinkerTel",SqlDbType.VarChar,30),
					new SqlParameter("@SortNo", SqlDbType.Int,4),
					new SqlParameter("@ProductID", SqlDbType.Int,4),
					new SqlParameter("@ProductNo", SqlDbType.VarChar,50),
					new SqlParameter("@ProductName", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.VarChar,256),
					new SqlParameter("@ModifiedUserID", SqlDbType.VarChar,50)
                                             };

                    parameters[0].Value = Provider2ProductM.CompanyCD;
                    parameters[1].Value = Provider2ProductM.Deptype;
                    parameters[2].Value = Provider2ProductM.ProviderID;
                    //parameters[3].Value = Provider2ProductM.LinkManID;
                    //parameters[4].Value = Provider2ProductM.LinkManName;
                    //parameters[5].Value = Provider2ProductM.LinkerTel;
                    parameters[3].Value = Provider2ProductM.SortNo;
                    parameters[4].Value = Provider2ProductM.ProductID;
                    parameters[5].Value = Provider2ProductM.ProductNo;
                    parameters[6].Value = Provider2ProductM.ProductName;
                    parameters[7].Value = Provider2ProductM.Remark;
                    parameters[8].Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                    foreach (SqlParameter para in parameters)
                    {
                        if (para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                    }

                    SqlHelper.ExecuteNonQuery(tran.Trans, CommandType.Text, strSql.ToString(), parameters);
            }
        }
        #endregion

        #region 插入物品关联
        public static void SaveProvider2Product(Provider2ProductModel Provider2ProductM)
        {
            ArrayList listADD = new ArrayList();
            StringBuilder strSql = new StringBuilder();
            //SQL语句
            strSql.AppendLine(" insert into officedba.Provider2Product ( ");
            strSql.AppendLine(@" CompanyCD,Deptype,ProviderID,SortNo,ProductID,ProductNo,ProductName,Remark,ModifiedUserID,ModifiedDate ");
            strSql.AppendLine(") values (");
            strSql.AppendLine(@" @CompanyCD,@Deptype,@ProviderID,@SortNo,@ProductID,@ProductNo,@ProductName,@Remark,@ModifiedUserID,getdate() )");

            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Provider2ProductM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Deptype", Provider2ProductM.Deptype));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Provider2ProductM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", Provider2ProductM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", Provider2ProductM.Remark == null ? "" :Provider2ProductM.Remark.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameter("@SortNo", Provider2ProductM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductNo", Provider2ProductM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", Provider2ProductM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.CommandText = strSql.ToString();

            listADD.Add(comm);
            SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 修改物品关联
        public static void UpdateProvider2Product(Provider2ProductModel Provider2ProductM)
        {
            ArrayList listADD = new ArrayList();
            StringBuilder sqlUpdate = new StringBuilder();

            sqlUpdate.AppendLine("Update  Officedba.Provider2Product set CompanyCD=@CompanyCD,");
            sqlUpdate.AppendLine("Deptype=@Deptype,ProviderID=@ProviderID,SortNo=@SortNo,ProductID=@ProductID,ProductNo=@ProductNo,");
            sqlUpdate.AppendLine(" ProductName=@ProductName,Remark=@Remark,ModifiedUserID=@ModifiedUserID,ModifiedDate=getdate() ");
            sqlUpdate.AppendLine(" where CompanyCD=@CompanyCD and ProviderID=@ProviderID and ProductID=@ProductID and Deptype=@Deptype ");


            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", Provider2ProductM.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@Deptype", Provider2ProductM.Deptype));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductID", Provider2ProductM.ProductID));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProviderID", Provider2ProductM.ProviderID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", Provider2ProductM.Remark == null ? "" : Provider2ProductM.Remark.ToString()));
            comm.Parameters.Add(SqlHelper.GetParameter("@SortNo", Provider2ProductM.SortNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductNo", Provider2ProductM.ProductNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@ProductName", Provider2ProductM.ProductName));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID));
            comm.CommandText = sqlUpdate.ToString();


            listADD.Add(comm);

            SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion
        #region 判断供应商物品关联是否存在
        public static int getCount(string CompanyCD, int ProviderID)
        {
            int count = 0;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProviderID", ProviderID),
                                       new SqlParameter("@CompanyCD", CompanyCD) 
                                   };
            strSql += " select count(*) from officedba.Provider2Product ";
            strSql += " WHERE  ( ProviderID= @ProviderID) AND (CompanyCD = @CompanyCD) ";
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            return count;
        }
        #endregion

        #region 获取已经存在的关联
        // public static int GetExistCount(string CompanyCD,int Deptype,int ProviderID,int ProductID)
        public static int GetExistCount(string CompanyCD, int Deptype, int ProviderID, string ProductID)
        {
            int count = 0;
            string strSql = string.Empty;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProviderID", ProviderID),
                                       new SqlParameter("@CompanyCD", CompanyCD),
                                        new SqlParameter("@Deptype", Deptype),
                                       new SqlParameter("@ProductID", ProductID)
                                   };
            strSql += " select count(*) as ExistCount from officedba.Provider2Product ";
            strSql += " WHERE  ProviderID=@ProviderID AND CompanyCD =@CompanyCD and Deptype=@Deptype and ProductID=@ProductID ";
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(strSql, paras));
            return count;
        }
        #endregion

        #region 供应商物品关联列表
        public static DataTable SelectProvider2Product(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string Deptype, string ProviderID, string ProviderName, string ProductName, string ProductNo, string EFIndex, string EFDesc, string Remark)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            //sql.AppendLine("select  s.Deptype,case s.Deptype when '0' then '供应商' when '1' then '客户' else '' end as Deptypetext, ");
            //sql.AppendLine(" s.CompanyCD,s.ProviderID,s.ProviderNo,s.ProviderName,s.ProductID,s.ProductNo,s.ProductName,");

            //sql.AppendLine("specification,colorID ");// 获取物品属性

            //sql.AppendLine(",isnull(ExtField1,'')as  ExtField1    ");
            //sql.AppendLine(",isnull(ExtField2,'')as  ExtField2    ");
            //sql.AppendLine(",isnull(ExtField3,'')as  ExtField3    ");
            //sql.AppendLine(",isnull(ExtField4,'')as  ExtField4    ");
            //sql.AppendLine(",isnull(ExtField5,'')as  ExtField5    ");
            //sql.AppendLine(",isnull(ExtField6,'')as  ExtField6    ");
            //sql.AppendLine(",isnull(ExtField7,'')as  ExtField7    ");
            //sql.AppendLine(",isnull(ExtField8,'')as  ExtField8    ");
            //sql.AppendLine(",isnull(ExtField9,'')as  ExtField9    ");
            //sql.AppendLine(",isnull(ExtField10,'')as ExtField10,   ");
       

            //sql.AppendLine(" s.Remark,s.ModifiedUserID,s.ModifiedUserName,s.ModifiedDate from (");
            //sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel, ");
            //sql.AppendLine("   A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            //sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            //sql.AppendLine("   isnull(B.CustNo,'') ProviderNo,isnull(B.CustName,'') ProviderName ");


            //sql.AppendLine(",pro.specification,pro.colorID ");// 获取物品属性      

            //sql.AppendLine(",isnull(pro.ExtField1,'')as  ExtField1    ");
            //sql.AppendLine(",isnull(pro.ExtField2,'')as  ExtField2    ");
            //sql.AppendLine(",isnull(pro.ExtField3,'')as  ExtField3    ");
            //sql.AppendLine(",isnull(pro.ExtField4,'')as  ExtField4    ");
            //sql.AppendLine(",isnull(pro.ExtField5,'')as  ExtField5    ");
            //sql.AppendLine(",isnull(pro.ExtField6,'')as  ExtField6    ");
            //sql.AppendLine(",isnull(pro.ExtField7,'')as  ExtField7    ");
            //sql.AppendLine(",isnull(pro.ExtField8,'')as  ExtField8    ");
            //sql.AppendLine(",isnull(pro.ExtField9,'')as  ExtField9    ");
            //sql.AppendLine(",isnull(pro.ExtField10,'')as ExtField10   ");

            //sql.AppendLine(" FROM officedba.Provider2Product AS A                                   ");
            //sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID ");
            //sql.AppendLine(" left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            //sql.AppendLine(" left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID ");
            //sql.AppendLine("LEFT OUTER JOIN   officedba.ProductInfo AS pro ON A.ProductID = pro.id");
            //sql.AppendLine(" where A.Deptype='0' ");
            //sql.AppendLine(" union ");
            //sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel,");
            //sql.AppendLine("   A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            //sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            //sql.AppendLine("   isnull(B.CustNo,'') ProviderNo,isnull(B.CustName,'') ProviderName ");

            //sql.AppendLine(",pro.specification,pro.colorID ");// 获取物品属性

            //sql.AppendLine(",isnull(pro.ExtField1,'')as  ExtField1    ");
            //sql.AppendLine(",isnull(pro.ExtField2,'')as  ExtField2    ");
            //sql.AppendLine(",isnull(pro.ExtField3,'')as  ExtField3    ");
            //sql.AppendLine(",isnull(pro.ExtField4,'')as  ExtField4    ");
            //sql.AppendLine(",isnull(pro.ExtField5,'')as  ExtField5    ");
            //sql.AppendLine(",isnull(pro.ExtField6,'')as  ExtField6    ");
            //sql.AppendLine(",isnull(pro.ExtField7,'')as  ExtField7    ");
            //sql.AppendLine(",isnull(pro.ExtField8,'')as  ExtField8    ");
            //sql.AppendLine(",isnull(pro.ExtField9,'')as  ExtField9    ");
            //sql.AppendLine(",isnull(pro.ExtField10,'')as ExtField10   ");

            //sql.AppendLine(" FROM officedba.Provider2Product AS A                                   ");
            //sql.AppendLine(" LEFT JOIN officedba.CustInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID ");
            //sql.AppendLine(" left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            //sql.AppendLine(" left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID ");
            //sql.AppendLine("LEFT OUTER JOIN   officedba.ProductInfo AS pro ON A.ProductID = pro.id");
            //sql.AppendLine(" where A.Deptype='1' ");
            //sql.AppendLine(")  s ");

            sql.AppendLine("select  s.Deptype,case s.Deptype when '0' then '供应商' when '1' then '客户' when '2' then '业务员' else '' end as Deptypetext, ");
            sql.AppendLine(" s.CompanyCD,s.ProviderID,s.ProviderNo,s.ProviderName,s.ProductID,s.ProductNo,s.ProductName,");

            sql.AppendLine("specification,colorID ");// 获取物品属性

            sql.AppendLine(",isnull(ExtField1,'')as  ExtField1    ");
            sql.AppendLine(",isnull(ExtField2,'')as  ExtField2    ");
            sql.AppendLine(",isnull(ExtField3,'')as  ExtField3    ");
            sql.AppendLine(",isnull(ExtField4,'')as  ExtField4    ");
            sql.AppendLine(",isnull(ExtField5,'')as  ExtField5    ");
            sql.AppendLine(",isnull(ExtField6,'')as  ExtField6    ");
            sql.AppendLine(",isnull(ExtField7,'')as  ExtField7    ");
            sql.AppendLine(",isnull(ExtField8,'')as  ExtField8    ");
            sql.AppendLine(",isnull(ExtField9,'')as  ExtField9    ");
            sql.AppendLine(",isnull(ExtField10,'')as ExtField10,   ");


            sql.AppendLine(" s.Remark,s.ModifiedUserID,s.ModifiedUserName,s.ModifiedDate from (");
            sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel, ");
            sql.AppendLine("   A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            sql.AppendLine("   isnull(B.CustNo,'') ProviderNo,isnull(B.CustName,'') ProviderName ");


            sql.AppendLine(",pro.specification,pro.colorID ");// 获取物品属性      

            sql.AppendLine(",isnull(pro.ExtField1,'')as  ExtField1    ");
            sql.AppendLine(",isnull(pro.ExtField2,'')as  ExtField2    ");
            sql.AppendLine(",isnull(pro.ExtField3,'')as  ExtField3    ");
            sql.AppendLine(",isnull(pro.ExtField4,'')as  ExtField4    ");
            sql.AppendLine(",isnull(pro.ExtField5,'')as  ExtField5    ");
            sql.AppendLine(",isnull(pro.ExtField6,'')as  ExtField6    ");
            sql.AppendLine(",isnull(pro.ExtField7,'')as  ExtField7    ");
            sql.AppendLine(",isnull(pro.ExtField8,'')as  ExtField8    ");
            sql.AppendLine(",isnull(pro.ExtField9,'')as  ExtField9    ");
            sql.AppendLine(",isnull(pro.ExtField10,'')as ExtField10   ");

            sql.AppendLine(" FROM officedba.Provider2Product AS A                                   ");
            sql.AppendLine(" LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID ");
            sql.AppendLine(" left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            sql.AppendLine(" left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID ");
            sql.AppendLine("LEFT OUTER JOIN   officedba.ProductInfo AS pro ON A.ProductID = pro.id");
            sql.AppendLine(" where A.Deptype='0' ");
            sql.AppendLine(" union ");
            sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel,");
            sql.AppendLine("   A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            sql.AppendLine("   isnull(B.CustNo,'') ProviderNo,isnull(B.CustName,'') ProviderName ");

            sql.AppendLine(",pro.specification,pro.colorID ");// 获取物品属性

            sql.AppendLine(",isnull(pro.ExtField1,'')as  ExtField1    ");
            sql.AppendLine(",isnull(pro.ExtField2,'')as  ExtField2    ");
            sql.AppendLine(",isnull(pro.ExtField3,'')as  ExtField3    ");
            sql.AppendLine(",isnull(pro.ExtField4,'')as  ExtField4    ");
            sql.AppendLine(",isnull(pro.ExtField5,'')as  ExtField5    ");
            sql.AppendLine(",isnull(pro.ExtField6,'')as  ExtField6    ");
            sql.AppendLine(",isnull(pro.ExtField7,'')as  ExtField7    ");
            sql.AppendLine(",isnull(pro.ExtField8,'')as  ExtField8    ");
            sql.AppendLine(",isnull(pro.ExtField9,'')as  ExtField9    ");
            sql.AppendLine(",isnull(pro.ExtField10,'')as ExtField10   ");

            sql.AppendLine(" FROM officedba.Provider2Product AS A                                   ");
            sql.AppendLine(" LEFT JOIN officedba.CustInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID ");
            sql.AppendLine(" left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            sql.AppendLine(" left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID ");
            sql.AppendLine("LEFT OUTER JOIN   officedba.ProductInfo AS pro ON A.ProductID = pro.id");
            sql.AppendLine(" where A.Deptype='1' ");
           

            sql.AppendLine(" union ");
            sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel,");
            sql.AppendLine("   A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            sql.AppendLine("   isnull(e2.EmployeeNo,'') ProviderNo,isnull(e2.EmployeeName,'') ProviderName ");

            sql.AppendLine(",pro.specification,pro.colorID ");// 获取物品属性

            sql.AppendLine(",isnull(pro.ExtField1,'')as  ExtField1    ");
            sql.AppendLine(",isnull(pro.ExtField2,'')as  ExtField2    ");
            sql.AppendLine(",isnull(pro.ExtField3,'')as  ExtField3    ");
            sql.AppendLine(",isnull(pro.ExtField4,'')as  ExtField4    ");
            sql.AppendLine(",isnull(pro.ExtField5,'')as  ExtField5    ");
            sql.AppendLine(",isnull(pro.ExtField6,'')as  ExtField6    ");
            sql.AppendLine(",isnull(pro.ExtField7,'')as  ExtField7    ");
            sql.AppendLine(",isnull(pro.ExtField8,'')as  ExtField8    ");
            sql.AppendLine(",isnull(pro.ExtField9,'')as  ExtField9    ");
            sql.AppendLine(",isnull(pro.ExtField10,'')as ExtField10   ");

            sql.AppendLine(" FROM officedba.Provider2Product AS A                                   ");
            sql.AppendLine(" LEFT JOIN officedba.CustInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID ");
            sql.AppendLine(" left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            sql.AppendLine(" left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID ");
            sql.AppendLine(" left join officedba.EmployeeInfo e2 on e2.CompanyCD=a.CompanyCD and e2.id=A.ProviderID ");

            sql.AppendLine("LEFT OUTER JOIN   officedba.ProductInfo AS pro ON A.ProductID = pro.id");
            sql.AppendLine(" where A.Deptype='2' ");
            sql.AppendLine(")  s ");

            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND s.CompanyCD =@CompanyCD ");
            if (Deptype != "")
            {
                sql.AppendLine(" and s.Deptype =@Deptype ");
            }
            if (ProductNo != "" && ProductNo != null)
            {
                sql.AppendLine(" AND s.ProductNo like @ProductNo ");
            }
            if (ProductName != null && ProductName != "")
            {
                sql.AppendLine(" AND s.ProductName like @ProductName");
            }
            if (Remark != "" && Remark != null)
            {
                sql.AppendLine(" AND s.Remark like @Remark ");
            }
            //if (ProviderID != "" && ProviderID != null)
            //{
            //    sql.AppendLine(" and s.ProviderNo like @ProviderNo ");
            //}
            if (ProviderName != "" && ProviderName != null)
            {
                sql.AppendLine(" and s.ProviderName like @ProviderName ");
            }

            if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
            {
                sql.AppendLine("	AND s.ExtField" + EFIndex + " LIKE '%" + EFDesc + "%' ");
            }

            //if (CustNo != "" && CustNo != null)
            //{
            //    if ((ProviderID != "" && ProviderID != null) || (ProviderName != "" && ProviderName != null))
            //    {
            //        sql.AppendLine(" or s.ProviderNo like @CustNo ");
            //    }
            //    else
            //    {
            //        sql.AppendLine(" and s.ProviderNo like @CustNo");
            //    }
            //}
            //if (CustName != "" && CustName != null)
            //{
            //    if ((ProviderID != "" && ProviderID != null) || (ProviderName != "" && ProviderName != null))
            //    {
            //        sql.AppendLine(" or s.ProviderName like @CustName ");
            //    }
            //    else
            //    {
            //        sql.AppendLine(" and s.ProviderName like @CustName ");
            //    }
            //}
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Deptype", Deptype));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderNo", "%"+ProviderID+"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProviderName", "%"+ProviderName+"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustNo", "%"+ EFIndex +"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustName", "%"+ EFDesc +"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductName", "%"+ProductName+"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductNo", "%"+ ProductNo +"%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", "%"+Remark+"%"));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion

        #region 查找加载供应商物品关联
        public static DataTable GetProvider2Product(string ID,string Deptype)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT A.Deptype,A.CompanyCD,A.ProviderID,A.LinkManID,isnull(A.LinkManName,'') LinkManName,isnull(A.LinkerTel,'') LinkerTel, ");
            sql.AppendLine("  A.SortNo,A.ProductID,A.ProductNo,isnull(A.ProductName,'') ProductName,isnull(A.Remark,'') Remark,A.ModifiedUserID, ");
            sql.AppendLine("  ei.EmployeeName ModifiedUserName,CONVERT(varchar(100), A.ModifiedDate, 23) AS ModifiedDate, ");
            if (Deptype == "2")//20121109 添加if语句块，用于根据当前的检测的角色类型加载不同的sql检索语句
            {
                sql.AppendLine("    isnull(e2.EmployeeNo,'') ProviderNo,isnull(e2.EmployeeName,'') ProviderName ");
            }//----end-------//
            else
            {
                sql.AppendLine("   isnull(B.CustNo,'') ProviderNo,isnull(B.CustName,'') ProviderName ");
            }
            sql.AppendLine(" FROM officedba.Provider2Product AS A                                                                  ");
            if (Deptype == "0")
            {
                sql.AppendLine("LEFT JOIN officedba.ProviderInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID               ");
            }
            else
            {
                sql.AppendLine("LEFT JOIN officedba.CustInfo AS B ON A.CompanyCD = B.CompanyCD AND  A.ProviderID=B.ID               ");
            }
            sql.AppendLine("left join officedba.UserInfo ui on ui.UserID=a.ModifiedUserID and ui.CompanyCD=a.CompanyCD ");
            sql.AppendLine("left join officedba.EmployeeInfo ei on ei.CompanyCD=a.CompanyCD and ei.id=ui.EmployeeID  ");
            sql.AppendLine("  left join officedba.EmployeeInfo e2 on e2.CompanyCD=a.CompanyCD and e2.id=A.ProviderID ");//20121109添加 对于类型为2的角色的信息关联检索
            sql.AppendLine("WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            sql.AppendLine(" AND A.ProviderID =@ID and A.Deptype=@Deptype ");

            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@ID",ID);
            param[2] = SqlHelper.GetParameter("@Deptype",Deptype);
            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion

        #region 删除供应商物品关联
        public static bool DeleteProvider2Product(string ID, string CompanyCD)
        {
            bool isSuc = false;
            string sql="";
            try{
            string[] IDs = ID.Split(',');
                for (int i = 0; i < IDs.Length; i++)
                {
                    string[] ppIDs=IDs[i].Split('|');
                    sql="delete from officedba.Provider2Product where CompanyCD=@CompanyCD and ProductID='"+ppIDs[1]+"' and ProviderID='"+ppIDs[0]+"' and Deptype='"+ppIDs[2]+"' ";
                    SqlCommand comm=new SqlCommand();
                    comm.CommandText=sql.ToString();

                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    ArrayList lstDelete = new ArrayList();
                    lstDelete.Add(comm);
                    isSuc= SqlHelper.ExecuteTransWithArrayList(lstDelete);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuc;
        }
        #endregion

        #region 获取询价ID
        public static DataTable GetAskPriceID(string providerID, string productID,string deptype)
        {
            StringBuilder sql = new StringBuilder();
            if (deptype == "1")
            {
                sql.AppendLine(" SELECT a.[ID],a.BillStatus  FROM [officedba].[SellOffer] a ");
                sql.AppendLine("  left join (select id,offerno,companycd from officedba.SellOfferdetail ");
                sql.AppendLine("  where id in (select max(id) from officedba.SellOfferdetail where companycd=@CompanyCD and productid=@productID )  ");
                sql.AppendLine("  ) b on b.companycd=a.companycd and b.offerno=a.offerno ");
                sql.AppendLine(" WHERE a.companycd=@CompanyCD and a.offerno=b.offerno and a.CustID=@providerID ");
            }
            else
            {
                sql.AppendLine(" SELECT a.[ID],a.BillStatus  FROM [officedba].[PurchaseAskPrice] a ");
                sql.AppendLine("  left join (select id,askno,companycd from officedba.purchaseAskpricedetail ");
                sql.AppendLine("  where id in (select max(id) from officedba.purchaseAskpricedetail where companycd=@CompanyCD and productid=@productID )  ");
                sql.AppendLine("  ) b on b.companycd=a.companycd and b.askno=a.askno ");
                sql.AppendLine(" WHERE a.companycd=@CompanyCD and a.askno=b.askno and a.providerid=@providerID ");
            }
            
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@productID", productID);
            param[2] = SqlHelper.GetParameter("@providerID", providerID);
            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion
        #region 获取客户或供应商信息
        public static DataTable GetCustInfo(string providerID, string deptype)
        {
            StringBuilder sql = new StringBuilder();
            if (deptype == "1")
            {
                sql.AppendLine(" select ID,CustNo,CustName from officedba.CustInfo  ");
                sql.AppendLine(" WHERE companycd=@CompanyCD and ID=@providerID ");
            }
            else
            {
                sql.AppendLine(" select ID,CustNo,CustName from officedba.ProviderInfo  ");
                sql.AppendLine(" WHERE  companycd=@CompanyCD and ID=@providerID ");
            }

            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@providerID", providerID);
            return SqlHelper.ExecuteSql(sql.ToString(), param);
        }
        #endregion
    }
}
