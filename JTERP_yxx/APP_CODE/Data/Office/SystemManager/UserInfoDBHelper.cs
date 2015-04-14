/**********************************************
 * 类作用：   用户管理数据库层处理
 * 建立人：   吴志强
 * 建立时间： 2009/01/10
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
using XBase.Model.Office.CustManager;
using XBase.Model.Office.HumanManager;
using XBase.Data.Office.CustManager;
using XBase.Data.Office.HumanManager;

namespace XBase.Data.Office.SystemManager
{
    /// <summary>
    /// 类名：UserInfoDBHelper
    /// 描述：用户管理数据库层处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/01/10
    /// 最后修改时间：2009/01/10
    /// </summary>
    ///
    public class UserInfoDBHelper
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfo(UserInfoModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //SQL拼写
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string sql = "select   ISNULL(a.IsHardValidate,1) AS IsHardValidate, isnull(a.UserID,'')as UserID,'' as UserName,isnull(a.IsRoot,0) as IsRoot,isnull(a.LastLoginTime,'') as LastLoginTime,";
                sql+= "b.EmployeeName, c.DeptName,e.RoleName,Case e.RoleRange  WHEN '0' THEN '本人'WHEN '1' THEN '本人及下属'   WHEN '2' THEN '本部门'    WHEN '3' THEN '全部'   ELSE '未设置权限' END AS RoleRangeName,";
            sql+= "isnull( CONVERT(CHAR(19), a.OpenDate, 120),'') as OpenDate ,";
            sql+= "isnull( CONVERT(CHAR(10), a.CloseDate, 23),'') as CloseDate ,";
            sql+= "Case WHEN a.LockFlag = '1' THEN '是' ELSE '否' END AS LockFlag ,Case WHEN a.UsedStatus = '1' THEN '启用' ELSE '停用' END AS UsedStatus,isnull( CONVERT(CHAR(19), a.ModifiedDate, 120),'') as ModifiedDate,a.ModifiedUserID,isnull(a.remark,'')as remark ";
                sql+= " FROM officedba.UserInfo a ";
            sql+= " Left Join officedba.EmployeeInfo b on b.ID = a.EmployeeID ";
            sql+= " Left Join officedba.DeptInfo c on c.ID = b.DeptID ";
            sql+= " Left Join officedba.UserRole d on d.UserID = a.UserID ";
            sql+= " Left Join officedba.RoleInfo e on e.RoleID = d.RoleID ";
                sql+= " WHERE a.CompanyCD = '" + companyCD + "' and a.IsRoot!='1'";
            if (!string.IsNullOrEmpty(model.UserID))
            {
                sql += " AND a.UserID LIKE @UserID";
            }
            if (!string.IsNullOrEmpty(model.LockFlag))
            {
                if (model.LockFlag == "1")
                {
                    sql += " AND a.LockFlag = '1'";
                }
                else
                {
                    sql += " AND a.LockFlag = '0'";
                }
            }

            if (model.EmployeeID != 0)
            {
                sql += " AND a.EmployeeID = @EmployeeID";
            }
            //开始时间输入的场合，添加为条件
            if (model.OpenDate != null && model.CloseDate != null)
            {
                sql += " AND a.OpenDate >= @OpenDate  AND a.CloseDate<=@CloseDate ";
            }
            else
            {
                if (model.OpenDate != null)
                {
                    sql += " AND a.OpenDate=@OpenDate";
                }
                //结束时间输入的场合，添加为条件 
                if (model.CloseDate != null)
                {
                    sql += " AND a.CloseDate=@CloseDate";
                }
            }
            if (model.IsHardValidate != null)
            {
                if (model.IsHardValidate == "1")
                    sql += " AND ( a.IsHardValidate='1' OR a.IsHardValidate IS NULL  )";
                else
                    sql += "AND a.IsHardValidate='0' "; 
            }

           
            SqlParameter[] param = new SqlParameter[5];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UserID", "%" + model.UserID + "%");
            param[2] = SqlHelper.GetParameter("@EmployeeID", model.EmployeeID);
            param[3] = SqlHelper.GetParameter("@OpenDate", model.OpenDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.OpenDate.ToString()));
            param[4] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.CloseDate.ToString()));
            DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, OrderBy, param, ref totalCount);
            return dt;
        }
        /// <summary>
        /// 获取公司用户总数
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户数目</returns>
        public static int GetUserNum(string companyCD)
        {
            //查询语句
            string select = "SELECT COUNT(*) AS UserNum FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD ";
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                return (int)userCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ComPanyCD"></param>
        /// <returns></returns>
        public static DataTable GetUserInfoByID(string UserId, string ComPanyCD)
        {
            //string sql = "SELECT  ISNULL(IsHardValidate,1) AS IsHardValidate,CompanyCD,UserID,'' as UserName,EmployeeID,password,UsedStatus,LockFlag,OpenDate,CloseDate,ModifiedDate,ModifiedUserID,isnull(IsRoot,0) as IsRoot,";
            //    sql+=" remark FROM officedba.UserInfo where UserID=@UserID and CompanyCD=@CompanyCD";
            //zyy修改查询语句
            string sql = " SELECT  ISNULL(ui.IsHardValidate,1) AS IsHardValidate,ui.CompanyCD,ui.UserID,'' as UserName,";
            sql += " ui.EmployeeID,ui.password,ui.UsedStatus,ui.LockFlag,ui.OpenDate,ui.CloseDate,ui.ModifiedDate, ";
            sql += " ui.ModifiedUserID,isnull(ui.IsRoot,0) as IsRoot,ui.remark,ci.custno,ci.custname,ei.EmployeeName,";
            sql += " case when ci.custname is null then ei.EmployeeName+'_本公司' else ei.EmployeeName+'_'+ci.custname end  as EmployeesName ";
            sql += " FROM officedba.UserInfo ui ";
            sql += " left join officedba.EmployeeInfo ei on ei.companycd=ui.companycd  and ei.ID=ui.EmployeeID ";
            sql += " left join officedba.CustLinkMan cl on cl.companycd=ei.companycd and cl.id=ei.corrlinkman ";
            sql += " left join officedba.CustInfo ci on ci.companycd=cl.companycd and ci.custno=cl.custno ";
            sql += " where ui.UserID=@UserID and ui.CompanyCD=@CompanyCD ";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@UserID", UserId);
                param[1] = SqlHelper.GetParameter("@ComPanyCD", ComPanyCD);
                return SqlHelper.ExecuteSql(sql,param);
        }

        /// <summary>
        /// 获取用户数
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetCompanyUserCount(string companyCD)
        {
            //查询语句
            string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD and IsRoot!='1' ";
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                return (int)userCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 判断用户ID是否已经存在
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetUserCount(string companyCD,string UserId)
        {
            //查询语句
            string select = "SELECT COUNT(*) AS UserCount FROM [officedba].[UserInfo] WHERE CompanyCD = @CompanyCD and UserID=@UserID";
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@UserID", UserId);
            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                return (int)userCount.Rows[0][0];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static int GetCompanyMaxUserNum(string companyCD)
        {
            //查询语句
            string select = "SELECT MaxUers FROM [pubdba].[companyOpenServ] WHERE CompanyCD = @CompanyCD";
            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable userCount = SqlHelper.ExecuteSql(select, param);
            if (userCount != null && userCount.Rows.Count > 0)
            {
                if (userCount.Rows[0][0] != null)
                {
                    return (int)userCount.Rows[0][0];
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 用户信息更新或者插入
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <param name="loginUserID">登陆系统的用户ID</param>
        /// <returns>更新成功与否</returns>
        public static bool ModifyUserInfo(UserInfoModel model, string loginUserID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            //追加的场合
            if (model.IsInsert)
            {
                sql.AppendLine("INSERT INTO officedba.UserInfo");
                sql.AppendLine("		(CompanyCD      ");
                sql.AppendLine("		,UserID         ");
              // sql.AppendLine("		,UserName       ");
                sql.AppendLine("		,password       ");
                sql.AppendLine("		,LockFlag       ");
                sql.AppendLine("		,EmployeeID       ");
                sql.AppendLine("		,UsedStatus     ");
                sql.AppendLine("		,OpenDate       ");
                sql.AppendLine("		,CloseDate      ");
                sql.AppendLine("		,ModifiedDate   ");
                sql.AppendLine("		,ModifiedUserID ");
                sql.AppendLine("		,remark      ");
                sql.AppendLine(",IsHardValidate)");
                sql.AppendLine("VALUES                  ");
                sql.AppendLine("		(@CompanyCD     ");
                sql.AppendLine("		,@UserID        ");
           //     sql.AppendLine("		,@UserName      ");
                sql.AppendLine("		,@password      ");
                sql.AppendLine("		,@LockFlag      ");
                sql.AppendLine("		,@EmployeeID      ");
                sql.AppendLine("		,@UsedStatus      ");
                sql.AppendLine("		,@OpenDate      ");
                sql.AppendLine("		,@CloseDate     ");
                sql.AppendLine("		,@ModifiedDate  ");
                sql.AppendLine("		,@ModifiedUserID");
                sql.AppendLine("		,@remark       ");
                sql.AppendLine(",@IsHardValidate)");
            }
            //更新的场合
            else
            {
                sql.AppendLine("UPDATE officedba.UserInfo		     ");
                sql.AppendLine("SET                                      ");
             //   sql.AppendLine("		UserName = @UserName             ");
                sql.AppendLine("		LockFlag = @LockFlag            ");
                sql.AppendLine("		,EmployeeID = @EmployeeID        ");
                sql.AppendLine("		,UsedStatus = @UsedStatus        ");
                sql.AppendLine("		,OpenDate = @OpenDate            ");
                sql.AppendLine("		,CloseDate = @CloseDate          ");
                sql.AppendLine("		,ModifiedDate = @ModifiedDate    ");
                sql.AppendLine("		,ModifiedUserID = @ModifiedUserID");
                sql.AppendLine("		,remark = @remark ,IsHardValidate=@IsHardValidate               ");
                sql.AppendLine("WHERE                                    ");
                sql.AppendLine("		UserID = @UserID                 ");
                sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
            }
            //设置参数
            SqlParameter[] param;
            if (model.IsInsert){
                param = new SqlParameter[12];
            }
            else
            {
                param = new SqlParameter[11];
            }
            
            param[0] = SqlHelper.GetParameter("@UserID", model.UserID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
           // param[2] = SqlHelper.GetParameter("@UserName", model.UserName);
            param[2] = SqlHelper.GetParameter("@LockFlag", model.LockFlag);
            param[3] = SqlHelper.GetParameter("@OpenDate", model.OpenDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.OpenDate.ToString()));
            param[4] = SqlHelper.GetParameter("@CloseDate", model.CloseDate == null
                                         ? SqlDateTime.Null
                                         : SqlDateTime.Parse(model.CloseDate.ToString()));
            param[5] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            param[6] = SqlHelper.GetParameter("@ModifiedUserID", loginUserID);
            param[7] = SqlHelper.GetParameter("@remark", model.Remark);
            param[8] = SqlHelper.GetParameter("@UsedStatus", model.UsedStatus);
            param[9] = SqlHelper.GetParameter("@EmployeeID", model.EmployeeID==null
                                           ?SqlInt32.Null
                                           :SqlInt32.Parse(model.EmployeeID .ToString()));
            param[10] = SqlHelper.GetParameter("@IsHardValidate", model.IsHardValidate);
            if (model.IsInsert)
            {
                param[11] = SqlHelper.GetParameter("@password", model.Password);
            }            

            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="psd"></param>
        /// <returns></returns>
        public static bool ModifyUserInfoPwd(string userID, string psd, string CompanyCD)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            //追加的场合
          
                sql.AppendLine("UPDATE officedba.UserInfo		     ");
                sql.AppendLine("SET                                      ");
                sql.AppendLine("		password = @password             ");
                sql.AppendLine("WHERE                                    ");
                sql.AppendLine("		UserID = @UserID                 ");
                sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
                SqlParameter[] param = param = new SqlParameter[3];
                param[0] = SqlHelper.GetParameter("@password", psd);
                param[1] = SqlHelper.GetParameter("@UserID", userID);
                param[2] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        public static bool ModifyUserInfoPwdLog(string userID, string psd, string CompanyCD,string SessionUserID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.UserInfo		     ");
            sql.AppendLine("SET                                      ");
            sql.AppendLine("		ModifiedDate = @ModifiedDate    ");
            sql.AppendLine("		,ModifiedUserID = @ModifiedUserID");
            sql.AppendLine("WHERE                                    ");
            sql.AppendLine("		UserID = @UserID                 ");
            sql.AppendLine("		AND CompanyCD = @CompanyCD       ");
            SqlParameter[] param = param = new SqlParameter[4];
            param[0] = SqlHelper.GetParameter("@ModifiedDate", System.DateTime.Now);
            param[1] = SqlHelper.GetParameter("@ModifiedUserID", SessionUserID);
            param[2] = SqlHelper.GetParameter("@UserID", userID);
            param[3] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            SqlHelper.ExecuteTransSql(sql.ToString(), param);
            return SqlHelper.Result.OprateCount > 0 ? true : false;
        }
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static bool DeleteUserInfo(string userID, string companyCD)
        {
           string allUserID = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();
           string[] Delsql = new string[1];
           try
           {
               string[] UserIdS = null;
               userID = userID.Substring(0, userID.Length);
               UserIdS = userID.Split(',');

               for (int i = 0; i < UserIdS.Length; i++)
               {
                   UserIdS[i] = "'" + UserIdS[i] + "'";
                   sb.Append(UserIdS[i]);
               }
               allUserID = sb.ToString().Replace("''", "','");
               Delsql[0] = "DELETE FROM officedba.UserInfo WHERE UserID IN (" + allUserID + ") and CompanyCD = @CompanyCD";
               SqlCommand comm = new SqlCommand();
               comm.CommandText = Delsql[0].ToString();
               //设置参数
               comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", companyCD));
               ArrayList lstDelete = new ArrayList();
               lstDelete.Add(comm);

               SqlCommand sqlcomm = new SqlCommand();
               sqlcomm.CommandText = "DELETE FROM officedba.UserRole WHERE UserID IN (" + allUserID + ") and CompanyCD = @CompanyID";
               sqlcomm.Parameters.Add(SqlHelper.GetParameter("@CompanyID", companyCD));
               lstDelete.Add(sqlcomm);
               //添加基本信息更新命令
               return SqlHelper.ExecuteTransWithArrayList(lstDelete);
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }
        /// <summary>
        /// 获取员工姓名
        /// </summary>
        /// <param name="companyCD"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeInfo(string companyCD)
        {
            string sql = "select b.ID,b.EmployeeName,b.EmployeeNo from officedba.EmployeeInfo as b where b.CompanyCD=@companyCD and b.Flag='1' ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@companyCD", companyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }

        #region 获取员工信息
        ///<summary>
        ///zyy添加获取员工信息,以区分是本公司员工还是客户
        ///</summary>
        ///<param name="CompanyCD"></param>
        ///<returns></returns>
        public static DataTable GetEmployeeInfoDiff(string CompanyCD)
        {
            string sql = "select b.ID,b.EmployeeName,b.EmployeeNo,b.CorrLinkMan,c.CustID,isnull(c.CustNo,'')CustNo,isnull(c.CustName,'')CustName ";
            sql += "  from officedba.EmployeeInfo as b ";
            sql += " left join ( select a.ID CustID,a.CustNo,a.CustName,a.CompanyCD,cl.ID as CorrID ";
            sql += " from officedba.CustInfo  a ";
            sql += " left join officedba.CustLinkMan cl on cl.CompanyCD=a.CompanyCD and cl.CustNo=a.CustNo ";
            sql += "  where a.CompanyCD=@companyCD  and cl.ID in (select CorrLinkMan from officedba.EmployeeInfo  where CompanyCD=@companyCD  and Flag='1' )";
            sql += " ) c on c.companycd=b.companycd and c.CorrID=b.CorrLinkMan  where b.CompanyCD=@companyCD  and b.Flag='1'  ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@companyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }
        #endregion 

        ///<summary>
        ///获取公司名称
        ///</summary>
        public static DataTable GetCompanyName(string CompanyCD)
        {
            string sql = "SELECT [CompanyCD] ,[NameCn] ,[NameEn],[NameShort] ";
            sql += "  FROM [pubdba].[company]  ";
            sql += " where CompanyCD=@companyCD ";
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = SqlHelper.GetParameter("@companyCD", CompanyCD);
            return SqlHelper.ExecuteSql(sql, parms);
        }

        /// <summary>
        /// 获取用户信息 
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>用户信息</returns>
        public static DataTable GetUserInfo(string CompanyCD, string UserID)
        {
            //SQL拼写
            string sql = "select UserID,'' as UserName from officedba.UserInfo where CompanyCD=@CompanyCD and IsRoot!='1'";
            DataTable dt=null;
            if (!string.IsNullOrEmpty(UserID))
            {
                 sql += " and UserID=@UserID ";
                SqlParameter[] parms = new SqlParameter[2];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                parms[1] = SqlHelper.GetParameter("@UserID", UserID);
                dt=SqlHelper.ExecuteSql(sql, parms);
                
            }
            else if (string.IsNullOrEmpty(UserID))
            {
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                dt= SqlHelper.ExecuteSql(sql, parms);
            }
            return dt;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static DataTable GetUserList()
        {
            string sql = "select * from officedba.UserInfo";
            return SqlHelper.ExecuteSql(sql,new SqlParameter[0]);
        }

        public static DataTable GetUserList(bool flag)
        {
            string sql = string.Empty;
            sql = "select * from officedba.UserInfo where isroot=0";
            return SqlHelper.ExecuteSql(sql, new SqlParameter[0]);
        }

        public static bool InsertPasswordHistory(string CompanyCD, string UserID,string password,string ModifiedUserID)
        {
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine("INSERT INTO officedba.PasswordHistory ");
            sqlInsert.AppendLine("           (CompanyCD, UserID    ");
            sqlInsert.AppendLine("		   	  , password            ");
            sqlInsert.AppendLine("           , ModifiedDate, ModifiedUserID)");
            sqlInsert.AppendLine("     VALUES                      ");
            sqlInsert.AppendLine("           (@CompanyCD           ");
            sqlInsert.AppendLine("           ,@UserID              ");
            sqlInsert.AppendLine("           ,@password            ");
            sqlInsert.AppendLine("           ,@ModifiedDate          ");
            sqlInsert.AppendLine("           ,@ModifiedUserID)            ");

            //设置参数
            SqlParameter[] param = new SqlParameter[5];
            int i = 0;
            //公司代码
            param[i++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //操作用户ID
            param[i++] = SqlHelper.GetParameter("@UserID", UserID);
            //操作模块ID
            param[i++] = SqlHelper.GetParameter("@password", password);
            //操作单据编号
            param[i++] = SqlHelper.GetParameter("@ModifiedDate", DateTime.Now);
            //操作对象
            param[i++] = SqlHelper.GetParameter("@ModifiedUserID", ModifiedUserID);
            //执行插入
            return SqlHelper.ExecuteTransSql(sqlInsert.ToString(), param) > 0 ? true : false;
        }

        /// <summary>
        /// 验证公司是否启用USBKEY设备
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsOpenValidateByCompany(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT ISNULL(EnableUSBKEYLOGIN,1) AS EnableUSBKEYLOGIN FROM pubdba.CompanyOpenServ WHERE CompanyCD='" + CompanyCD+"'");

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString());
            if (dt == null || dt.Rows.Count < 0)
                return true;
            else
            {
                if (dt.Rows[0]["EnableUSBKEYLOGIN"].ToString() == "True")
                    return true;
                else
                    return false;
            }
        }


        #region 插入注册信息
        public static bool InsertRegInfo(CustInfoModel CustInfoM, LinkManModel LinkManM, EmployeeInfoModel EmpLoM, UserInfoModel UserInfoM)
        {
            bool isSucc = false;//是否添加成功
            int linkid = 0;
            int empID = 0;
            string[] arr = new string[] { "CustName", "CompanyCD" };
            string[] CustNames = new string[] { CustInfoM.CustName, CustInfoM.CompanyCD };
            bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNames);
            int deptID = getSuperDeptID(CustInfoM.CompanyCD); 
            TransactionManager tran = new TransactionManager();
            tran.BeginTransaction();
            try
            {
                if (!NameHas)
                {
                    CustInfoM.CustBig = "1";
                    CustInfoDBHelper.CustInfoAddReg(CustInfoM, tran);
                }
                 linkid=LinkManDBHelper.LinkManAddReg(LinkManM,tran);

                 if (linkid > 0)
                 {
                     EmpLoM.CorrLinkMan = linkid;
                     empID = EmployeeInfoDBHelper.InsertEmployeeInfoReg(EmpLoM,tran);
                     if (empID>0)
                     {
                         EmployeeInfoDBHelper.UpdateEmployeeInfo(empID, deptID, CustInfoM.CompanyCD,tran);
                         LinkManDBHelper.UpdateLinkMan(linkid, empID, CustInfoM.CompanyCD,tran);
                         if (!NameHas)
                         {
                             CustInfoDBHelper.UpdateCustInfo(LinkManM, empID, tran);
                         }
                         UserInfoM.EmployeeID = empID;
                         InsertUserInfoReg(UserInfoM, tran);
                     }
                 }

                tran.Commit();
                isSucc = true;
                DataTable dt = EmployeeInfoDBHelper.GetEmployeeInfoWithCustEmpID(CustInfoM.CompanyCD,empID);
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Mobile"].ToString()))
                    {
                        XBase.Common.SMSender.SendBatch(dt.Rows[0]["Mobile"].ToString(), dt.Rows[0]["EmployeeName"].ToString() + "您好!有客户注册,客户名为" + CustInfoM.CustName + "用户名为" + UserInfoM.UserID);
                    }
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                isSucc = false;
                throw ex;
            }
            return isSucc;
        }
        #endregion
        
        //GETSUPERDEPTID
        private static int  getSuperDeptID(string CompanyCD)
        {
            int deptid = 0;
            string sql = " select ID from  officedba.DeptInfo where  CompanyCD = '"+CompanyCD+"' AND  SuperDeptID is   NULL  ";
            DataTable dt = SqlHelper.ExecuteSql(sql);
            if (dt.Rows.Count > 0)
            {
                deptid =Convert.ToInt32(dt.Rows[0]["ID"].ToString());
            }
            return deptid;
        }
        #region insertuserinfo
        public static void InsertUserInfoReg(UserInfoModel UserInfoM, TransactionManager tran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into officedba.UserInfo (");
            strSql.Append("CompanyCD,UserID,Password,LockFlag,EmployeeID,UsedStatus,OpenDate,CloseDate,isCust,IsRoot,IsHardValidate )");
            strSql.Append(" values (");
            strSql.Append("@CompanyCD,@UserID,@Password,@LockFlag,@EmployeeID,@UsedStatus,@OpenDate,@CloseDate,@isCust,'0','0' )");
            #region 参数
            SqlParameter[] parameters = {
					new SqlParameter("@CompanyCD", SqlDbType.VarChar,8),
					new SqlParameter("@UserID", SqlDbType.VarChar,10),
					new SqlParameter("@Password", SqlDbType.VarChar,100),
					new SqlParameter("@LockFlag", SqlDbType.Char,1),
                    new SqlParameter("@EmployeeID",SqlDbType.Int,4),
                    new SqlParameter("@UsedStatus",SqlDbType.Char,1),
                     new SqlParameter("@OpenDate",SqlDbType.DateTime),
                     new SqlParameter("@CloseDate",SqlDbType.DateTime),
                     new SqlParameter("@isCust",SqlDbType.Char,1)
					};
            parameters[0].Value = UserInfoM.CompanyCD;
            parameters[1].Value = UserInfoM.UserID;
            parameters[2].Value = UserInfoM.Password;
            parameters[3].Value = UserInfoM.LockFlag;
            parameters[4].Value = UserInfoM.EmployeeID;
            parameters[5].Value = UserInfoM.UsedStatus;
            parameters[6].Value = UserInfoM.OpenDate;
            parameters[7].Value = UserInfoM.CloseDate;
            parameters[8].Value = UserInfoM.IsCust;

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
        #endregion
    }
}
