/**********************************************
 * 类作用：   新建帐套数据库层处理
 * 建立人：   钱锋锋
 * 创建时间：2010/10/11
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Data.SqlTypes;
using System.Collections;
using System.Security.Cryptography;
namespace XBase.Data.Office.SystemManager
{
    /// <summary>
    /// 类名：CompanyDBHelper
    /// 描述：新建帐套数据库层处理
    /// 
    /// 作者：钱锋锋
    /// 创建时间：2010/10/11
    /// 最后修改时间：2010/10/11
    /// </summary>
    ///
    public class CompanyDBHelper
    {
       
        /// <summary>
        /// 用户信息更新或者插入
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <param name="loginUserID">登陆系统的用户ID</param>
        /// <returns>更新成功与否</returns>
        public static bool InsertCompany(CompanyModel model, string loginUserID, string loginCompanyCD)
        {
            string sqlstr = "";
            bool result = false;
            try
            {
                #region 公司信息添加SQL语句
                StringBuilder sqlSoIn = new StringBuilder();
                sqlSoIn.AppendLine("INSERT INTO  pubdba.company");
                sqlSoIn.AppendLine(" (CompanyCD");
                sqlSoIn.AppendLine(" , NameCn");
                sqlSoIn.AppendLine("    , NameEn");
                sqlSoIn.AppendLine("  , NameShort");
                sqlSoIn.AppendLine("  , PYShort");
                sqlSoIn.AppendLine("  , ArtiPerson");
                sqlSoIn.AppendLine("   , SetupAddr");
                sqlSoIn.AppendLine("  , SetupDate");
                sqlSoIn.AppendLine("  , SetupMoney");
                sqlSoIn.AppendLine("  , CapitalScale");
                sqlSoIn.AppendLine("  , SaleroomY");
                sqlSoIn.AppendLine("  , ProfitY");
                sqlSoIn.AppendLine("  , TaxCD");
                sqlSoIn.AppendLine(" , BusiNumber");
                sqlSoIn.AppendLine("  , IsTax");
                sqlSoIn.AppendLine("  , Country");
                sqlSoIn.AppendLine(" , ProfessionType");
                sqlSoIn.AppendLine("  , Province");
                sqlSoIn.AppendLine("  , City");
                sqlSoIn.AppendLine(" , ContactName");
                sqlSoIn.AppendLine("  , Tel");
                sqlSoIn.AppendLine("  , Mobile");
                sqlSoIn.AppendLine("  , email");
                sqlSoIn.AppendLine("  , Fax");
                sqlSoIn.AppendLine("   , QQ");
                sqlSoIn.AppendLine("  , MSN");
                sqlSoIn.AppendLine("  , IM");
                sqlSoIn.AppendLine("   , Addr");
                sqlSoIn.AppendLine("  , Post");
                sqlSoIn.AppendLine("  , WebSite");
                sqlSoIn.AppendLine("   , SalesMan");
                sqlSoIn.AppendLine("  , Remark");
                sqlSoIn.AppendLine("  , ModifiedDate");
                sqlSoIn.AppendLine("  , ModifiedUserID)");
                sqlSoIn.AppendLine("     SELECT ");
                sqlSoIn.AppendLine("@CompanyCD");
                sqlSoIn.AppendLine(" ,@NameCn");
                if (model.NameEn == "")
                {
                    sqlSoIn.AppendLine("    , NameEn");
                }
                else
                {
                    sqlSoIn.AppendLine("    , @NameEn");
                }
                if (model.NameShort == "")
                {
                    sqlSoIn.AppendLine("  , NameShort");
                }
                else
                {
                    sqlSoIn.AppendLine("  , @NameShort");
                }
                sqlSoIn.AppendLine("  , @PYShort");
                sqlSoIn.AppendLine("  , ArtiPerson");
                sqlSoIn.AppendLine("   , SetupAddr");
                sqlSoIn.AppendLine("  , SetupDate");
                sqlSoIn.AppendLine("  , SetupMoney");
                sqlSoIn.AppendLine("  , CapitalScale");
                sqlSoIn.AppendLine("  , SaleroomY");
                sqlSoIn.AppendLine("  , ProfitY");
                sqlSoIn.AppendLine("  , TaxCD");
                sqlSoIn.AppendLine(" , BusiNumber");
                sqlSoIn.AppendLine("  , IsTax");
                sqlSoIn.AppendLine("  , Country");
                sqlSoIn.AppendLine(" , ProfessionType");
                sqlSoIn.AppendLine("  , Province");
                sqlSoIn.AppendLine("  , City");
                sqlSoIn.AppendLine(" , ContactName");
                sqlSoIn.AppendLine("  , Tel");
                sqlSoIn.AppendLine("  , Mobile");
                sqlSoIn.AppendLine("  , email");
                sqlSoIn.AppendLine("  , Fax");
                sqlSoIn.AppendLine("   , QQ");
                sqlSoIn.AppendLine("  , MSN");
                sqlSoIn.AppendLine("  , IM");
                sqlSoIn.AppendLine("   , Addr");
                sqlSoIn.AppendLine("  , Post");
                sqlSoIn.AppendLine("  , WebSite");
                sqlSoIn.AppendLine("   , SalesMan");
                sqlSoIn.AppendLine("  , Remark");
                sqlSoIn.AppendLine("  , @ModifiedDate");
                sqlSoIn.AppendLine("  , @ModifiedUserID");
                sqlSoIn.AppendLine("  FROM         pubdba.company");
                sqlSoIn.AppendLine("  where CompanyCD=@OldCompanyCD");
                SqlCommand comm = new SqlCommand();
                comm.CommandText = sqlSoIn.ToString();
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@OldCompanyCD", loginCompanyCD));
                comm.Parameters.Add(SqlHelper.GetParameter("@NameCn", model.NameCn));
                comm.Parameters.Add(SqlHelper.GetParameter("@NameEn", model.NameEn));
                comm.Parameters.Add(SqlHelper.GetParameter("@NameShort", model.NameShort));
                comm.Parameters.Add(SqlHelper.GetParameter("@PyShort", model.PyShort));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                result = SqlHelper.ExecuteTransWithCommand(comm);
                #endregion
                #region 公司服务添加SQL语句
                StringBuilder sqlSoIn1 = new StringBuilder();
                sqlSoIn1.AppendLine("INSERT INTO  pubdba.companyOpenServ");
                sqlSoIn1.AppendLine("(CompanyCD");
                sqlSoIn1.AppendLine(", MaxRoles");
                sqlSoIn1.AppendLine(" , MaxUers");
                sqlSoIn1.AppendLine(", MaxDocSize");
                sqlSoIn1.AppendLine(" , SingleDocSize");
                sqlSoIn1.AppendLine(" , MaxDocNum");
                sqlSoIn1.AppendLine(" , DocSavePath");
                sqlSoIn1.AppendLine(" , MaxKeywords");
                sqlSoIn1.AppendLine(" , MaxUserKeywords");
                sqlSoIn1.AppendLine(" , ManMsgNum");
                sqlSoIn1.AppendLine("  , AutoMsgNum");
                sqlSoIn1.AppendLine("  , OpenDate");
                sqlSoIn1.AppendLine("  , CloseDate");
                sqlSoIn1.AppendLine(" , ModifiedDate");
                sqlSoIn1.AppendLine("  , ModifiedUserID");
                sqlSoIn1.AppendLine(" , remark");
                sqlSoIn1.AppendLine(" , LogoImg");
                sqlSoIn1.AppendLine(" , enableUSBKEYLOGIN,ISStatus,UsedStatus)");
                sqlSoIn1.AppendLine("     SELECT ");
                sqlSoIn1.AppendLine("@CompanyCD");
                sqlSoIn1.AppendLine(", MaxRoles");
                sqlSoIn1.AppendLine(" , MaxUers");
                sqlSoIn1.AppendLine(", MaxDocSize");
                sqlSoIn1.AppendLine(" , SingleDocSize");
                sqlSoIn1.AppendLine(" , MaxDocNum");
                sqlSoIn1.AppendLine(" , @DocSavePath");
                sqlSoIn1.AppendLine(" , MaxKeywords");
                sqlSoIn1.AppendLine(" , MaxUserKeywords");
                sqlSoIn1.AppendLine(" , ManMsgNum");
                sqlSoIn1.AppendLine("  , AutoMsgNum");
                sqlSoIn1.AppendLine("  , OpenDate");
                sqlSoIn1.AppendLine("  , CloseDate");
                sqlSoIn1.AppendLine(" , @ModifiedDate");
                sqlSoIn1.AppendLine("  , @ModifiedUserID");
                sqlSoIn1.AppendLine(" , remark");
                sqlSoIn1.AppendLine(" , LogoImg");
                sqlSoIn1.AppendLine(" , enableUSBKEYLOGIN,'1',@UsedStatus");
                sqlSoIn1.AppendLine("  FROM        pubdba.companyOpenServ");
                sqlSoIn1.AppendLine("  where CompanyCD=@OldCompanyCD");
                SqlCommand comm1 = new SqlCommand();
                comm1.CommandText = sqlSoIn1.ToString();
                comm1.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm1.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
                comm1.Parameters.Add(SqlHelper.GetParameter("@DocSavePath", model.DocSavePath));
                comm1.Parameters.Add(SqlHelper.GetParameter("@OldCompanyCD", loginCompanyCD));
                comm1.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString()));
                comm1.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));

                result = SqlHelper.ExecuteTransWithCommand(comm1);
                #endregion
                #region 公司菜单添加SQL语句
                StringBuilder sqlSoIn2 = new StringBuilder();
                sqlSoIn2.AppendLine("INSERT INTO  pubdba.CompanyModule");
                sqlSoIn2.AppendLine(" (CompanyCD");
                sqlSoIn2.AppendLine(", ModuleID");
                sqlSoIn2.AppendLine(", OrderNO");
                sqlSoIn2.AppendLine(" ,ModifiedDate");
                sqlSoIn2.AppendLine(", ModifiedUserID)");
                sqlSoIn2.AppendLine("     SELECT ");
                sqlSoIn2.AppendLine(" @CompanyCD");
                sqlSoIn2.AppendLine(", ModuleID");
                sqlSoIn2.AppendLine(", OrderNO");
                sqlSoIn2.AppendLine(" ,@ModifiedDate");
                sqlSoIn2.AppendLine(", @ModifiedUserID");
                sqlSoIn2.AppendLine("  FROM  pubdba.CompanyModule");
                sqlSoIn2.AppendLine("  where CompanyCD=@OldCompanyCD");
                SqlCommand comm2 = new SqlCommand();
                comm2.CommandText = sqlSoIn2.ToString();
                comm2.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm2.Parameters.Add(SqlHelper.GetParameter("@OldCompanyCD", loginCompanyCD));
                comm2.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString()));
                comm2.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                result = SqlHelper.ExecuteTransWithCommand(comm2);
                #endregion
                #region 系统角色添加SQL语句
                StringBuilder sqlSoIn3 = new StringBuilder();

                sqlSoIn3.AppendLine("INSERT INTO  officedba.RoleInfo ");
                sqlSoIn3.AppendLine(" (RoleName");
                sqlSoIn3.AppendLine(", CompanyCD");
                sqlSoIn3.AppendLine(", ModifiedUserID");
                sqlSoIn3.AppendLine(" ,IsRoot)");
                sqlSoIn3.AppendLine("    values ");
                sqlSoIn3.AppendLine(" ('系统管理员角色'");
                sqlSoIn3.AppendLine(",@CompanyCD");
                sqlSoIn3.AppendLine(", @ModifiedUserID");
                sqlSoIn3.AppendLine(" ,'1')");
                sqlSoIn3.AppendLine("set @ID=@@IDENTITY                ");
                SqlCommand comm3 = new SqlCommand();
                comm3.CommandText = sqlSoIn3.ToString();
                comm3.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                comm3.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", loginUserID));
                comm3.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                result = SqlHelper.ExecuteTransWithCommand(comm3);
                int roleid = int.Parse(comm3.Parameters["@ID"].Value.ToString());
                #endregion

                #region 角色关联添加SQL语句
                comm.Parameters.Clear();
                comm.CommandText = "select * from pubdba.ModuleFunction where ModuleID like '219%' or ModuleID like '2011201' or ModuleID like '2011202' or ModuleID like '2011203' or ModuleID like '2011204'";
                DataTable dt = SqlHelper.ExecuteSearch(comm);
                sqlstr = " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','2','" + DateTime.Now.ToString() + "','" + loginUserID + "' )  ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','219','" + DateTime.Now.ToString() + "','" + loginUserID + "'  )  ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','201','" + DateTime.Now.ToString() + "','" + loginUserID + "' )  ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','20112','" + DateTime.Now.ToString() + "','" + loginUserID + "' )   ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','21911','" + DateTime.Now.ToString() + "','" + loginUserID + "' )   ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','21913','" + DateTime.Now.ToString() + "','" + loginUserID + "' )   ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','21914','" + DateTime.Now.ToString() + "','" + loginUserID + "' )   ";
                sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,ModifiedDate,ModifiedUserID )  values ('" + model.CompanyCD + "','" + roleid + "','21918','" + DateTime.Now.ToString() + "','" + loginUserID + "' )   ";
               
                foreach (DataRow dr in dt.Rows)
                {
                    sqlstr += " insert into officedba.RoleFunction ( CompanyCD,RoleID,ModuleID,FunctionID,ModifiedDate,ModifiedUserID )  ";
                    sqlstr += " values ( '" + model.CompanyCD + "','" + roleid + "','" + dr["ModuleID"].ToString() + "','" + dr["FunctionID"] + "','" + DateTime.Now.ToString() + "','" + loginUserID + "')   ";
                }
                comm.CommandText = sqlstr;
                result = SqlHelper.ExecuteTransWithCommand(comm);
                #endregion
                #region 用户添加SQL语句
                comm.Parameters.Clear();
                Random rd = new Random(DateTime.Now.Millisecond);
                int temp = rd.Next(1000, 9999);
                sqlstr = " insert into officedba.UserInfo ( CompanyCD,UserID,password,UsedStatus,LockFlag,OpenDate,CloseDate,IsRoot)  ";
                sqlstr += " values ( '" + model.CompanyCD + "','" + model.CompanyCD + temp + "','" + MD5(model.CompanyCD + temp.ToString()).ToUpper() + "','1','0','" + DateTime.Now + "','3999-12-30','1') ";
                comm.CommandText = sqlstr;
                result = SqlHelper.ExecuteTransWithCommand(comm);
                #endregion
                comm.Parameters.Clear();
                sqlstr = " insert into officedba.UserRole ( CompanyCD,UserID,RoleID,ModifiedDate,ModifiedUserID)  ";
                sqlstr += " values ('" + model.CompanyCD + "','" + model.CompanyCD + temp + "','" + roleid + "','" + DateTime.Now + "','" + loginUserID + "') ";
                comm.CommandText = sqlstr;
                
                result = SqlHelper.ExecuteTransWithCommand(comm);
                comm.Parameters.Clear();
                sqlstr = @"INSERT INTO [officedba].[CultureType] ([CompanyCD] ,[TypeName] ,[SupperTypeID],[Path]) VALUES ('" + model.CompanyCD + "','企业内刊',0,'') ";
                sqlstr += @"INSERT INTO [officedba].[CultureType] ([CompanyCD] ,[TypeName] ,[SupperTypeID],[Path]) VALUES ('" + model.CompanyCD + "','规章制度',0,'') ";
                sqlstr += @"INSERT INTO [officedba].[CultureType] ([CompanyCD] ,[TypeName] ,[SupperTypeID],[Path]) VALUES ('" + model.CompanyCD + "','信息共享',0,'') ";
                comm.CommandText = sqlstr;
                result = SqlHelper.ExecuteTransWithCommand(comm);
                comm.Parameters.Clear();
                DataTable iCompany = new DataTable();
                sqlstr = "Select * from  pubdba.company  where CompanyCD='" + model.CompanyCD + "' ";
                comm.CommandText = sqlstr;
                iCompany = SqlHelper.ExecuteSearch(comm);
                comm.Parameters.Clear();
                if (iCompany.Rows.Count > 0)
                {
                    sqlstr = @"INSERT INTO [officedba].[DeptInfo] ([CompanyCD],[DeptNO],[PYshort],[DeptName] ,[UsedStatus],[ModifiedDate],[ModifiedUserID],[saleflag],[subflag])  VALUES  ('" + iCompany.Rows[0]["CompanyCD"] + "'" + @",'000001' ,'" + iCompany.Rows[0]["PYShort"] + "'" + @",'" + iCompany.Rows[0]["NameCn"] + "'" + @",'1','" + DateTime.Now + "'" +@",'System' ,'0','0') ";
                    comm.CommandText = sqlstr;
                    result = SqlHelper.ExecuteTransWithCommand(comm);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
            
        }

          /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="companyCD"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static bool CheckCompanyCD(string codeValue)
        {
            string checkSql = " SELECT  CompanyCD FROM pubdba.company  WHERE  CompanyCD  = @ColumnValue";

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            int i = 0;
            //公司代码
            //编码类型
            param[i++] = SqlHelper.GetParameter("@ColumnValue", codeValue);

            //校验存在性
            DataTable data = SqlHelper.ExecuteSql(checkSql, param);
            //数据不存在时，返回true
            if (data == null || data.Rows.Count < 1)
            {
                return true;
            }
            //数据存在时，返回false
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="companyCD"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static string  GetUserID(string CompanyCD)
        {
            string getSql = " SELECT  UserID FROM officedba.UserInfo WHERE  CompanyCD  = @CompanyCD";

            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            int i = 0;
            
            param[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);

            
            DataTable data = SqlHelper.ExecuteSql(getSql, param);
            
            
                return data.Rows[0][0].ToString();
            
        }
        private static string MD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        public static  bool UpdateCompany(CompanyModel model)
        {
            #region 公司服务添加SQL语句
            StringBuilder sqlSoIn1 = new StringBuilder();
            sqlSoIn1.AppendLine("Update  pubdba.companyOpenServ");
            sqlSoIn1.AppendLine("set UsedStatus=@UsedStatus");
            sqlSoIn1.AppendLine("  FROM        pubdba.companyOpenServ");
            sqlSoIn1.AppendLine("  where CompanyCD=@CompanyCD");
            SqlCommand comm1 = new SqlCommand();
            comm1.CommandText = sqlSoIn1.ToString();
            comm1.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm1.Parameters.Add(SqlHelper.GetParameter("@UsedStatus", model.UsedStatus));
            return  SqlHelper.ExecuteTransWithCommand(comm1);
            #endregion
        }
        public static DataTable  GetCompany()
        {
            #region 公司服务添加SQL语句

            string str = "SELECT     a.CompanyCD, a.NameCn, a.NameEn, a.NameShort, a.PYShort, b.DocSavePath, c.UserID, c.UserID AS UserPWD,b.UsedStatus"
                        + "  FROM         pubdba.company AS a "
                        + "   LEFT OUTER JOIN officedba.UserInfo AS c ON a.CompanyCD = c.CompanyCD AND c.IsRoot = '1'"
                        + "   LEFT OUTER JOIN pubdba.companyOpenServ AS b ON a.CompanyCD = b.CompanyCD"
                        + "  WHERE     (a.CompanyCD IN"
                        +"             (SELECT     CompanyCD"
                        +"                FROM          pubdba.companyOpenServ"
                        +"                WHERE      (ISStatus = '1')))";

            try
            {
                return SqlHelper.ExecuteSql(str);
            }
            catch (Exception ex)
            {
                return null;
            }
            #endregion
        }
    }
}
