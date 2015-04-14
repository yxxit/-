using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;

namespace XBase.Data.Office.CustManager
{
    public class TalkDBHelper
    {
        #region 新建洽谈信息的方法
        /// <summary>
        /// 新建洽谈信息的方法
        /// </summary>
        /// <param name="CustTalkM">洽谈信息</param>
        /// <returns>返回洽谈ID</returns>
        public static int CustTalkAdd(CustTalkModel CustTalkM)
        {
            try
            {
                #region 设置参数
                SqlParameter[] param = new SqlParameter[24];
                param[0] = SqlHelper.GetParameter("@CompanyCD     ",CustTalkM.CompanyCD     );
                param[1] = SqlHelper.GetParameter("@CustID        ",CustTalkM.CustID        );
                param[2] = SqlHelper.GetParameter("@CustLinkMan   ",CustTalkM.CustLinkMan   );
                param[3] = SqlHelper.GetParameter("@TalkNo        ",CustTalkM.TalkNo        );
                param[4] = SqlHelper.GetParameter("@Title         ",CustTalkM.Title         );
                param[5] = SqlHelper.GetParameter("@Priority      ",CustTalkM.Priority      );
                param[6] = SqlHelper.GetParameter("@TalkType      ",CustTalkM.TalkType      );
                param[7] = SqlHelper.GetParameter("@Linker        ",CustTalkM.Linker        );
                param[8] = SqlHelper.GetParameter("@CompleteDate  ",CustTalkM.CompleteDate  );
                param[9] = SqlHelper.GetParameter("@Status        ",CustTalkM.Status        );
                param[10] = SqlHelper.GetParameter("@Contents      ",CustTalkM.Contents      );
                param[11] = SqlHelper.GetParameter("@Feedback      ",CustTalkM.Feedback      );
                param[12] = SqlHelper.GetParameter("@Result        ",CustTalkM.Result        );
                param[13] = SqlHelper.GetParameter("@remark        ",CustTalkM.remark        );
                param[14] = SqlHelper.GetParameter("@Creator       ",CustTalkM.Creator       );
                param[15] = SqlHelper.GetParameter("@CreatedDate   ",CustTalkM.CreatedDate   );
                param[16] = SqlHelper.GetParameter("@ModifiedDate  ",CustTalkM.ModifiedDate  );
                param[17] = SqlHelper.GetParameter("@ModifiedUserID",CustTalkM.ModifiedUserID);
                param[18] = SqlHelper.GetParameter("@CanViewUser", CustTalkM.CanViewUser);
                param[19] = SqlHelper.GetParameter("@CanViewUserName", CustTalkM.CanViewUserName);

                SqlParameter paramid = new SqlParameter("@id", SqlDbType.Int);
                paramid.Direction = ParameterDirection.Output;
                param[20] = paramid;
                param[21] = SqlHelper.GetParameter("@NextLinkDate",CustTalkM.NextLinkDate==null?SqlDateTime.Null:SqlDateTime.Parse(CustTalkM.NextLinkDate.ToString()));
                param[22] = SqlHelper.GetParameter("@JoinUser", CustTalkM.JoinUser);
                param[23] = SqlHelper.GetParameter("@JoinUserName", CustTalkM.JoinUserName);
                #endregion

                //创建命令
                SqlCommand comm = new SqlCommand();
                SqlHelper.ExecuteTransStoredProcedure("officedba.insertCustTalk", comm, param);
                int Talkid = Convert.ToInt32(comm.Parameters["@id"].Value);

                return Talkid;
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return 0;
            }
        }
        #endregion

        #region 修改客户洽谈信息
        /// <summary>
        /// 修改客户洽谈信息
        /// </summary>
        /// <param name="CustTalkM">客户洽谈信息</param>
        /// <returns>bool值</returns>
        public static bool UpdateTalk(CustTalkModel CustTalkM)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE officedba.CustTalk set ");
                sql.AppendLine("CompanyCD     =@CompanyCD     ,");
                sql.AppendLine("CustID        =@CustID        ,");
                sql.AppendLine("CustLinkMan   =@CustLinkMan   ,");
                sql.AppendLine("Title         =@Title         ,");
                sql.AppendLine("Priority      =@Priority      ,");
                sql.AppendLine("TalkType      =@TalkType      ,");
                sql.AppendLine("Linker        =@Linker        ,");
                sql.AppendLine("CompleteDate  =@CompleteDate  ,");
                sql.AppendLine("Status        =@Status        ,");
                sql.AppendLine("Contents      =@Contents      ,");
                sql.AppendLine("Feedback      =@Feedback      ,");
                sql.AppendLine("Result        =@Result        ,");
                sql.AppendLine("remark        =@remark        ,");
                sql.AppendLine("CanViewUser = @CanViewUser,    ");
                sql.AppendLine("CanViewUserName = @CanViewUserName, ");
                sql.AppendLine("ModifiedDate  =@ModifiedDate  ,");
                sql.AppendLine("ModifiedUserID=@ModifiedUserID,");
                sql.AppendLine("NextLinkDate=@NextLinkDate,");
                sql.AppendLine("JoinUser = @JoinUser,    ");
                sql.AppendLine("JoinUserName = @JoinUserName ");
                sql.AppendLine(" WHERE ");
                sql.AppendLine("ID = @ID ");

                SqlParameter[] param = new SqlParameter[21];
                param[0] = SqlHelper.GetParameter("@ID      ", CustTalkM.ID);
                param[1] = SqlHelper.GetParameter("@CompanyCD     ",CustTalkM.CompanyCD    );
                param[2] = SqlHelper.GetParameter("@CustID        ",CustTalkM.CustID        );
                param[3] = SqlHelper.GetParameter("@CustLinkMan   ",CustTalkM.CustLinkMan   );                
                param[4] = SqlHelper.GetParameter("@Title         ",CustTalkM.Title         );
                param[5] = SqlHelper.GetParameter("@Priority      ",CustTalkM.Priority      );
                param[6] = SqlHelper.GetParameter("@TalkType      ",CustTalkM.TalkType      );
                param[7] = SqlHelper.GetParameter("@Linker        ",CustTalkM.Linker        );
                param[8] = SqlHelper.GetParameter("@CompleteDate", CustTalkM.CompleteDate == null
                                        ? SqlDateTime.Null
                                        : SqlDateTime.Parse(CustTalkM.CompleteDate.ToString()));
                param[9] = SqlHelper.GetParameter("@Status        ",CustTalkM.Status        );
                param[10] = SqlHelper.GetParameter("@Contents      ",CustTalkM.Contents      );
                param[11] = SqlHelper.GetParameter("@Feedback      ",CustTalkM.Feedback      );
                param[12] = SqlHelper.GetParameter("@Result        ",CustTalkM.Result        );
                param[13] = SqlHelper.GetParameter("@remark        ",CustTalkM.remark        );
                param[14] = SqlHelper.GetParameter("@ModifiedDate  ",CustTalkM.ModifiedDate  );
                param[15] = SqlHelper.GetParameter("@ModifiedUserID",CustTalkM.ModifiedUserID);
                param[16] = SqlHelper.GetParameter("@CanViewUser", CustTalkM.CanViewUser);
                param[17] = SqlHelper.GetParameter("@CanViewUserName", CustTalkM.CanViewUserName);
                param[18] = SqlHelper.GetParameter("@NextLinkDate",CustTalkM.NextLinkDate==null
                    ?SqlDateTime.Null:SqlDateTime.Parse(CustTalkM.NextLinkDate.ToString()));
                param[19] = SqlHelper.GetParameter("@JoinUser", CustTalkM.JoinUser);
                param[20] = SqlHelper.GetParameter("@JoinUserName", CustTalkM.JoinUserName);

                SqlHelper.ExecuteTransSql(sql.ToString(), param);
                return SqlHelper.Result.OprateCount > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 根据条件查询客户洽谈信息的方法
        /// <summary>
        /// 根据条件查询客户洽谈信息的方法
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CustTalkM">客户洽谈信息</param>
        /// <param name="TalkBegin">开始时间</param>
        /// <param name="TalkEnd">结束时间</param>
        /// <returns>返回查询结果</returns>
        public static DataTable GetTalkInfoBycondition(string CanUserID,string CustName, CustTalkModel CustTalkM, string TalkBegin, string TalkEnd, int pageIndex, int pageCount, string ord, string IsOverTime, ref int TotalCount)
        {
            try
            {
                #region sql语句
                string sql = "select  ct.id,ct.TalkNo,ct.title,ct.custid,ci.custname custnam," +
                               " ct.custlinkman,cl.linkmanname,ci.CustNo,ci.CustBig,ci.CanViewUser,ci.Manager,ci.Creator CustCreator, " +
                               " ct.Priority,  ct.talktype,(case ct.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' when '5' then '会晤拜访' when '6' then '综合' end) typename," +
                               " CONVERT(varchar(100), ct.CompleteDate, 20) CompleteDate,ct.Linker,ct.Status," +
                                 " ct.Creator,ct.Contents,ct.Feedback,ei.EmployeeName," +
                               " CONVERT(varchar(100), ct.CreatedDate, 23) CreatedDate, CONVERT(varchar(100), ct.ModifiedDate, 23) ModifiedDate" +
                               ", isnull(CONVERT(varchar(100), ct.NextLinkDate, 23),'') NextLinkDate" +
                                 " from " +
                                 " officedba.custtalk ct  " +
                               " left join officedba.custinfo ci on ci.id = ct.custid  " +
                               " left join officedba.custlinkman cl on cl.id = ct.custlinkman " +
                    //" left join officedba.CodePublicType cp on cp.id = ct.talktype " +
                               " left join officedba.EmployeeInfo ei on ei.id = ct.creator  " +
                           " where " +
                           " ct.CompanyCD = '" + CustTalkM.CompanyCD + "'";
                sql += " AND  (  ";
                 XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
                DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    if (dt.Rows[0]["RoleRange"].ToString() == "1")
                    {
                        sql += " (ct.Creator IN  ";
                        sql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "2")
                    {
                        sql += " (ct.Creator IN  ";
                        sql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) ))  or ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "3")
                    {
                        sql += " (ct.Creator IN  ";
                        sql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID)))  or ";
                    }
                }


                sql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+ct.CanViewUser+',')>0 )";
                sql += " or (ct.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (ct.Linker IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
                                                     
                if (CustName != "")
                    sql += " and ci.id = '" + CustName + "'";
                if (CustTalkM.Linker != "0")
                    sql += " and ct.Linker = '" + CustTalkM.Linker + "'"; // 执行人
                if (CustTalkM.Creator != 0)
                    sql += " and ct.Creator = '" + CustTalkM.Creator + "'"; // 创建人
                if (CustTalkM.TalkType != 0)
                    sql += " and ct.TalkType = " + CustTalkM.TalkType + "";
                if (TalkBegin != "")
                    sql += " and ct.CompleteDate >= '" + TalkBegin.ToString() + " 00:00:00"+ "'";
                if (TalkEnd != "")
                    sql += " and ct.CompleteDate <= '" + TalkEnd.ToString() + " 23:59:59"+ "'";
                if (CustTalkM.Priority != "0")
                    sql += " and ct.Priority = " + CustTalkM.Priority + "";
                if (CustTalkM.Title != "")
                    sql += " and ct.title like '%" + CustTalkM.Title + "%'";
                if (CustTalkM.Status != "0")
                    sql += " and ct.Status = " + CustTalkM.Status + "";
                if (IsOverTime == "1")
                    sql += " and ct.CompleteDate<'" + DateTime.Now.ToString() + "'" +
                         " and ct.Status='1'";
                #endregion

                //return SqlHelper.ExecuteSql(sql);
                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 根据客户查看其洽谈信息
        /// <summary>
        /// 根据客户查询其洽谈信息
        /// </summary>
        /// <param name="TalkID">洽谈编号</param>
        /// <returns>返回查询结果</returns>
        public static DataTable GetTalkInfoByCustNo(string CompanyCD, string CustNo, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                string sql = "select  ct.id,ct.TalkNo,isnull(ct.title,'') title,ct.custid,isnull(ci.custname,'') custnam," +
                             " ct.custlinkman,isnull(cl.linkmanname,'') linkmanname,ci.CustNo, ct.Contents, " +
                             " CONVERT(varchar(100), ct.CompleteDate, 20) CompleteDate,ct.Linker,isnull(ct.Status,'') Status," +
                               " ct.Creator,isnull(ei.EmployeeName,'') EmployeeName ," +
                             " CONVERT(varchar(100), ct.CreatedDate, 23) CreatedDate" +
                               " from " +
                               " officedba.custtalk ct  " +
                             " left join officedba.custinfo ci on ci.id = ct.custid  " +
                             " left join officedba.custlinkman cl on cl.id = ct.custlinkman " +
                             " left join officedba.EmployeeInfo ei on ei.id = ct.creator  " +
                         " where " +
                         " ct.CompanyCD = '" + CompanyCD + "'"+
                         "and  "+
                             "cl.CustNo = '"+ CustNo + "'";
              
                SqlCommand comm = new SqlCommand();
                comm.CommandText = sql;
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion



        #region 根据洽谈ID获取洽谈信息的方法
        /// <summary>
        /// 根据洽谈ID获取洽谈信息的方法
        /// </summary>
        /// <param name="CompanyCD">公司代码</param>
        /// <param name="TalkID">洽谈ID</param>
        /// <returns>返回洽谈信息</returns>
        public static DataTable GetTalkInfoByID(string CompanyCD, int TalkID)
        {
            try
            {
                string sql = "select ct.ID,ct.CompanyCD,ct.CustID,ci.custname custnam,ci.custno,ct.CustLinkMan,cl.linkmanname,cl.HandSet,cl.WorkTel,cl.HomeAddress " +
                                  " ,ct.TalkNo,ct.Title,ct.Priority,ct.TalkType" +
                                  " ,ct.Linker,CONVERT(varchar(100), ct.CompleteDate, 20) CompleteDate,ct.Status,ct.Contents" +
                                  " ,ct.Feedback,ct.Result,ct.remark,ct.Creator,ei.EmployeeName" +
                                  " ,CONVERT(varchar(100), ct.CreatedDate, 23) CreatedDate," +
                                  " CONVERT(varchar(100), ct.ModifiedDate, 23) ModifiedDate,ct.ModifiedUserID" +
                                  " ,ct.CanViewUser,ct.CanViewUserName,isnull(CONVERT(varchar(100), ct.NextLinkDate, 23),'') NextLinkDate,ct.JoinUser,ct.JoinUserName " +
                              " from" +
                                  " officedba.custtalk ct " +
                                  " left join officedba.custinfo ci on ci.id = ct.custid " +
                                  " left join officedba.custlinkman cl on cl.id = ct.CustLinkMan " +
                                  " left join officedba.EmployeeInfo ei on ei.id = ct.Creator " +
                              " where" +
                                  " ct.id = @id" +
                              " and ct.CompanyCD = @CompanyCD"; 

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", TalkID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 洽谈一览表_报表
        /// <summary>
        /// 洽谈一览表_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">洽谈方式</param>
        /// <param name="TypeId">优先级别</param>
        /// <param name="TypeId">状态</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkList(string CustName, string TalkType, string Priority,string Status,string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.id,b.CustBig,b.CustName,c.EmployeeName,a.TalkNO,a.id as TalkID,a.Title, ");
                sql.Append(" (case a.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' ");
                sql.Append("  when '5' then '会晤拜访' when '6' then '综合'  else '' end) TalkType, ");
                sql.Append(" (case a.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' else '' end) Priority, ");

                sql.Append(" substring(a.contents,0,10) Contents, ");
                sql.Append(" (case a.Status when '1' then '未开始' when '2' then '进行中' when '3' then '已完成' else '' end) Status, ");
                sql.Append(" (d.EmployeeName) linker,a.CompleteDate ");
                sql.Append(" from officedba.custTalk a inner join ");
                sql.Append(" officedba.custinfo b on a.CustId=b.Id left join ");
                sql.Append(" officedba.employeeInfo c on b.manager=c.Id left join ");
                sql.Append(" officedba.employeeInfo d on a.linker=d.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TalkType != "")
                {
                    sql.Append(" and a.TalkType= ");
                    sql.Append(TalkType);
                }

                if (Priority != "") 
                {
                    sql.Append(" and a.Priority='");
                    sql.Append(Priority);
                    sql.Append("'");
                }

                if (Status != "") 
                {
                    sql.Append(" and a.Status='");
                    sql.Append(Status);
                    sql.Append("'");
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetTalkListPrint(string CustName, string TalkType, string Priority, string Status, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select b.CustNO,b.CustName,c.EmployeeName,a.TalkNO,a.Title, ");
                sql.Append(" (case a.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' ");
                sql.Append("  when '5' then '会晤拜访' when '6' then '综合'  else '' end) TalkType, ");
                sql.Append(" (case a.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' else '' end) Priority, ");

                sql.Append(" substring(a.contents,0,10) Contents, ");
                sql.Append(" (case a.Status when '1' then '未开始' when '2' then '进行中' when '3' then '已完成' else '' end) Status, ");
                sql.Append(" (d.EmployeeName) linker,a.CompleteDate ");
                sql.Append(" from officedba.custTalk a inner join ");
                sql.Append(" officedba.custinfo b on a.CustId=b.Id left join ");
                sql.Append(" officedba.employeeInfo c on b.manager=c.Id left join ");
                sql.Append(" officedba.employeeInfo d on a.linker=d.Id ");

                sql.Append(" where 1=1");
                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }
                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }
                if (TalkType != "")
                {
                    sql.Append(" and a.TalkType= ");
                    sql.Append(TalkType);
                }

                if (Priority != "")
                {
                    sql.Append(" and a.Priority='");
                    sql.Append(Priority);
                    sql.Append("'");
                }

                if (Status != "")
                {
                    sql.Append(" and a.Status='");
                    sql.Append(Status);
                    sql.Append("'");
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and a.CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and a.CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按洽谈次数统计_报表
        /// <summary>
        /// 按洽谈次数统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustTalkCount(string CustName,string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,b.talkCount from officedba.custInfo a inner join  ");
                sql.Append(" (select count(1) talkCount,CustId from  officedba.custtalk where 1=1  ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append("  group by CustId) b on a.Id=b.custId  ");

                sql.Append(" where 1=1");

                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustTalkCountPrint(string CustName,string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,b.talkCount,b.CustID from officedba.custInfo a inner join  ");
                sql.Append(" (select count(1) talkCount,CustId from  officedba.custtalk where 1=1 and Status!=1  ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CreatedDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CreatedDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append("  group by CustId) b on a.Id=b.custId  ");

                sql.Append(" where 1=1");

                if (CompanyCD != "")
                {
                    sql.Append(" and a.CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (CustName != "")
                {
                    sql.Append(" and a.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按洽谈方式统计_报表
        /// <summary>
        /// 按洽谈方式统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TalkType">洽谈方式</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustTalkByType(string CustName,string TalkType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  b.CustNO,b.CustName, ");
                sql.Append(" (case a.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' ");
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合'  else '' end) TalkType,a.TalkCount from ");
                sql.Append(" (select count(1) TalkCount,TalkType,CustId from officedba.CustTalk where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }
                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by TalkType,CustId ) a inner join ");
                sql.Append(" officedba.custInfo b on a.custId=b.Id where 1=1 ");

                if (CustName != "") 
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%' ");
                }

                if (TalkType != "")
                {
                    sql.Append(" and a.TalkType=");
                    sql.Append(TalkType);
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustTalkByTypePrint(string CustName,string TalkType, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  b.CustNO,b.CustName, ");
                sql.Append(" (case a.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' ");
                sql.Append(" when '5' then '会晤拜访' when '6' then '综合'  else '' end) TalkType,a.TalkCount from ");
                sql.Append(" (select count(1) TalkCount,TalkType,CustId from officedba.CustTalk where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by TalkType,CustId ) a inner join ");
                sql.Append(" officedba.custInfo b on a.custId=b.Id where 1=1 ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%' ");
                }

                if (TalkType != "")
                {
                    sql.Append(" and a.TalkType=");
                    sql.Append(TalkType);
                }

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按优先级别统计_报表
        /// <summary>
        /// 按优先级别统计_报表
        /// </summary>
        /// <param name="TypeId">客户名称</param>
        /// <param name="Priority">优先级别</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetCustTalkByPriority(string CustName, string Priority, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  b.CustNO,b.CustName, ");
                sql.Append(" (case a.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' else '' end) Priority ");
                sql.Append(" ,a.TalkCount from ");
                sql.Append(" (select count(1) TalkCount,Priority,CustId from officedba.CustTalk where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }
                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by Priority,CustId ) a inner join ");
                sql.Append(" officedba.custInfo b on a.custId=b.Id where 1=1 ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%' ");
                }

                if (Priority != "")
                {
                    sql.Append(" and a.Priority=");
                    sql.Append(Priority);
                }


                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetCustTalkByPriorityPrint(string CustName, string Priority, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select  b.CustNO,b.CustName, ");
                sql.Append(" (case a.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' else '' end) Priority ");
                sql.Append(" ,a.TalkCount from ");
                sql.Append(" (select count(1) TalkCount,Priority,CustId from officedba.CustTalk where 1=1 ");

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }



                sql.Append(" group by Priority,CustId ) a inner join ");
                sql.Append(" officedba.custInfo b on a.custId=b.Id where 1=1 ");

                if (CustName != "")
                {
                    sql.Append(" and b.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%' ");
                }

                if (Priority != "")
                {
                    sql.Append(" and a.Priority=");
                    sql.Append(Priority);
                }


                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 按洽谈人统计_报表
        /// <summary>
        /// 按洽谈人统计_报表
        /// </summary>
        /// <param name="CustName">客户名称</param>
        /// <param name="TypeId">执行人ID</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <param name="LinkDateBegin">开始时间</param>
        /// <param name="LinkDateEnd">结束时间</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkByMan(string CustName,string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.TalkCount from  ");
                sql.Append(" (select count(*) TalkCount,Linker,CustId from officedba.CustTalk where 1=1 ");

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Linker != "")
                {
                    sql.Append(" and Linker=");
                    sql.Append(Linker.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by Linker,CustId) a Inner join ");
                sql.Append(" officedba.EmployeeInfo b on a.Linker=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id where 1=1 ");
                if (CustName != "") 
                {
                    sql.Append(" and c.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetTalkByManPrint(string CustName, string Linker, string CompanyCD, string LinkDateBegin, string LinkDateEnd, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select c.CustNO,c.CustName,b.EmployeeName,a.TalkCount from  ");
                sql.Append(" (select count(*) TalkCount,Linker,CustId from officedba.CustTalk where 1=1 ");

                if (CompanyCD != "")
                {
                    sql.Append(" and CompanyCD='");
                    sql.Append(CompanyCD);
                    sql.Append("' ");
                }

                if (Linker != "")
                {
                    sql.Append(" and Linker=");
                    sql.Append(Linker.ToString());
                }

                if (LinkDateBegin.ToString() != "")
                {
                    sql.Append(" and  CompleteDate >= ' ");
                    sql.Append(LinkDateBegin.ToString());
                    sql.Append("'");
                }
                if (LinkDateEnd.ToString() != "")
                {
                    sql.Append(" and  CompleteDate <dateadd(dd,1,'");
                    sql.Append(LinkDateEnd.ToString());
                    sql.Append("')");
                }

                sql.Append(" group by Linker,CustId) a Inner join ");
                sql.Append(" officedba.EmployeeInfo b on a.Linker=b.Id Inner join  ");
                sql.Append(" officedba.CustInfo c on a.CustId=c.Id where 1=1 ");
                if (CustName != "")
                {
                    sql.Append(" and c.CustName like '%");
                    sql.Append(CustName);
                    sql.Append("%'");
                }


                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 未洽谈客户统计_报表
        /// <summary>
        /// 未洽谈客户统计_报表
        /// </summary>
        /// <param name="Days">天数</param>
        /// <param name="CompanyCD">公司ID</param>
        /// <returns>记录集</returns>
        public static DataTable GetTalkByDays(string Days, string CompanyCD, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.id,a.CustBig,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustTalk where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and CompleteDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                return SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }

        public static DataTable GetTalkByDaysPrint(string Days, string CompanyCD, string ord)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select a.CustNO,a.CustName,isnull(b.EmployeeName,'') EmployeeName from ");
                sql.Append(" officedba.CustInfo a left join officedba.EmployeeInfo b on a.Manager=b.Id ");
                sql.Append(" where a.Id not in(select CustId from officedba.CustTalk where 1=1 ");

                if (Days != "")
                {
                    sql.Append(" and CompleteDate >=dateadd(dd,-" + Days.ToString() + ",getdate()) ");
                }
                sql.Append(" ) ");


                sql.Append(" and a.CompanyCD='");
                sql.Append(CompanyCD);
                sql.Append("' ");

                sql.Append("Order by ");
                sql.Append(ord);

                return SqlHelper.ExecuteSql(sql.ToString());
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 客户洽谈打印
        /// <summary>
        /// 客户洽谈打印
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="TalkID"></param>
        /// <returns></returns>
        public static DataTable PrintTalk(string CompanyCD, string TalkID)
        {
            try
            {
                string sql = "select ct.ID,ct.CompanyCD,ct.CustID,ci.custname custnam,ci.custno,ct.CustLinkMan,cl.linkmanname" +
                                   " ,ct.TalkNo,ct.Title,(case ct.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' end)Priority,"+
                                   "(case ct.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' when '5' then '会晤拜访' when '6' then '综合' end) TalkType," +
                                   " ct.Linker,CONVERT(varchar(100), ct.CompleteDate, 20) CompleteDate,(case ct.Status when '1' then '未开始' when '2' then '进行中' when '3' then '已完成' end)Status,ct.Contents" +
                                   " ,ct.Feedback,ct.Result,ct.remark,ct.Creator,ei.EmployeeName" +
                                   " ,CONVERT(varchar(100), ct.CreatedDate, 23) CreatedDate," +
                                   " CONVERT(varchar(100), ct.ModifiedDate, 23) ModifiedDate,ct.ModifiedUserID,ct.CanViewUserName " +
                               " from" +
                                   " officedba.custtalk ct " +
                                   " left join officedba.custinfo ci on ci.id = ct.custid " +
                                   " left join officedba.custlinkman cl on cl.id = ct.CustLinkMan " +
                                   " left join officedba.EmployeeInfo ei on ei.id = ct.Creator " +
                               " where" +
                                   " ct.id = @id" +
                               " and ct.CompanyCD = @CompanyCD";
                               

                SqlParameter[] param = new SqlParameter[2];
                param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
                param[1] = SqlHelper.GetParameter("@id", TalkID);
                return SqlHelper.ExecuteSql(sql, param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 打印客户洽谈信息
        /// <summary>
        /// 打印客户洽谈信息
        /// </summary>
        /// <param name="CustID"></param>
        /// <param name="CustTalkM"></param>
        /// <param name="TalkBegin"></param>
        /// <param name="TalkEnd"></param>
        /// <param name="ord"></param>
        /// <returns></returns>
        public static DataTable ExportTalkInfo(string CanUserID,string CustID, CustTalkModel CustTalkM, string TalkBegin, string TalkEnd, string ord)
        {
            try
            {
                #region sql语句
                string sql = "select  ct.id,ct.TalkNo,ct.title,ct.custid,ci.custname custnam," +
                               " ct.custlinkman,cl.linkmanname," +
                               " (case ct.Priority when '1' then '暂缓' when '2' then '普通' when '3' then '尽快' when '4' then '立即' end) PriorityName," +
                               "  ct.talktype,(case ct.TalkType when '1' then '电话' when '2' then '传真' when '3' then '邮件' when '4' then '远程在线' when '5' then '会晤拜访' when '6' then '综合' end) typename," +
                               " CONVERT(varchar(100), ct.CompleteDate, 20) CompleteDate,ct.Linker,(case ct.Status when '1' then '未开始' when '2' then '进行中' when '3' then '已完成' end) StatusName," +
                                 " ct.Creator,ei.EmployeeName," +
                               " CONVERT(varchar(100), ct.CreatedDate, 23) CreatedDate" +
                                 " from " +
                                 " officedba.custtalk ct  " +
                               " left join officedba.custinfo ci on ci.id = ct.custid  " +
                               " left join officedba.custlinkman cl on cl.id = ct.custlinkman " +
                               " left join officedba.EmployeeInfo ei on ei.id = ct.creator  " +
                           " where " +
                           " ct.CompanyCD = '" + CustTalkM.CompanyCD + "'";
                sql += " AND  (  ";
                           XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
                DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    if (dt.Rows[0]["RoleRange"].ToString() == "1")
                    {
                        sql += " (ct.Creator IN  ";

                        sql += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " ))  or ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "2")
                    {
                        sql += " (ct.Creator IN  ";
                        sql += " (SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID) ))  or  ";
                    }
                    if (dt.Rows[0]["RoleRange"].ToString() == "3")
                    {
                        sql += " (ct.Creator IN  ";
                        sql += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                        sql += "  WHERE DeptID IN (SELECT a.ID  ";
                        sql += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                        sql += "  WHERE a.ID=b.ID)))  or ";
                    }
                }


                sql += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+ct.CanViewUser+',')>0 )";
                sql += " or (ct.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (ct.Linker IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
                  
                if (CustID != "")
                    sql += " and ci.id = '" + CustID + "'";
                if (CustTalkM.TalkType != 0)
                    sql += " and ct.TalkType = " + CustTalkM.TalkType + "";
                if (TalkBegin != "")
                    sql += " and ct.CompleteDate >= '" + TalkBegin.ToString() + "'";
                if (TalkEnd != "")
                    sql += " and ct.CompleteDate <= '" + TalkEnd.ToString() + "'";
                if (CustTalkM.Priority != "0")
                    sql += " and ct.Priority = " + CustTalkM.Priority + "";
                if (CustTalkM.Title != "")
                    sql += " and ct.title like '%" + CustTalkM.Title + "%'";
                if (CustTalkM.Status != "0")
                    sql += " and ct.Status = " + CustTalkM.Status + "";
                #endregion

                return SqlHelper.ExecuteSql(sql);               
            }
            catch (Exception ex)
            {
                string smeg = ex.Message;
                return null;
            }
        }
        #endregion

        #region 获取最近7天待联系的客户
        public static DataTable GetCustContact(string CompanyCD)
        {
            string sqlcontact = "";
            sqlcontact = "SELECT c.[ID],c.[CompanyCD],c.[CustID],c.[CustLinkMan],c.[TalkNo],c.[Title],c.[Priority],c.[TalkType],c.[Linker],c.[CompleteDate],c.[Status]";
            sqlcontact += ",c.[Contents],c.[Feedback],c.[Result],c.[remark],c.[Creator] ,c.[CreatedDate] ";
            sqlcontact+=",c.[ModifiedDate],c.[ModifiedUserID],c.[CanViewUser],c.[CanViewUserName],isnull(convert(varchar(100),c.[NextLinkDate],23),'')NextLinkDate,datediff(day,getdate(),c.nextlinkdate) as delaydays";
            sqlcontact += ",ci.CustName,cl.LinkManName,ei.EmployeeName";
            sqlcontact += "  FROM (select * from [officedba].[CustTalk] where companycd='" + CompanyCD + "' and id in (select max(id) from [officedba].[CustTalk] group by custid ) and NextLinkDate is not null) c";
            sqlcontact += " left join officedba.CustInfo ci on ci.id=c.custid ";
            sqlcontact += " left join officedba.custlinkman cl on cl.id=c.custlinkman ";
            sqlcontact += " left join officedba.employeeinfo ei on ei.id=c.creator ";
            sqlcontact += "where c.CompanyCD='" + CompanyCD + "' and datediff(day,getdate(),c.nextlinkdate)<=7 and datediff(day,getdate(),c.nextlinkdate)>=0 ";
            sqlcontact += " and (";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sqlcontact += " (c.Creator IN  ";
                    sqlcontact += " (  SELECT ID FROM  officedba.EmployeeInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sqlcontact += " (c.Creator IN  ";
                    sqlcontact += " (SELECT ID FROM  officedba.EmployeeInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID) ))  or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sqlcontact += " (c.Creator IN  ";
                    sqlcontact += " ( SELECT ID FROM  officedba.EmployeeInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID)))  or ";
                }
            }


            sqlcontact += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+c.CanViewUser+',')>0 )";
            sqlcontact += " or (c.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (c.Linker IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
            sqlcontact += " order by c.NextLinkDate";
            return SqlHelper.ExecuteSql(sqlcontact);
        }
        #endregion
        #region 获取最近7天待服务的客户
        public static DataTable GetCustService(string CompanyCD)
        {
            string sqlcontact = "";
            sqlcontact = "SELECT cs.[ID],cs.[CompanyCD],cs.[ServeNO],cs.[CustID],cs.[CustLinkMan],cs.[CustLinkTel],cs.[Title] ,cs.[ServeType],cs.[Fashion] ,cs.[State] ,cs.[BeginDate] ,cs.[DateUnit]";
            sqlcontact += ",cs.[SpendTime],cs.[OurLinkMan],cs.[Executant],cs.[Contents],cs.[Feedback],cs.[LinkQA],cs.[Remark],cs.[ModifiedDate],cs.[ModifiedUserID],cs.[CanViewUser],cs.[CanViewUserName]";
            sqlcontact += ",isnull(convert(varchar(100),cs.[NextLinkDate],23),'') NextLinkDate,datediff(day,getdate(),cs.nextlinkdate) as delaydays";
            sqlcontact += ",c.CustName,e.EmployeeName,l.LinkManName ";
            sqlcontact += "  FROM (select * from [officedba].[CustService] where companycd='" + CompanyCD + "' and id in (select max(id) from [officedba].[CustService] group by custid )  and NextLinkDate is not null ) cs ";
            sqlcontact += " left join officedba.custinfo c on  c.id = cs.custid ";
            sqlcontact += " left join  officedba.CodePublicType p on p.id = cs.ServeType";
            sqlcontact += " left join officedba.EmployeeInfo e on e.id = cs.Executant";
            sqlcontact += " left join officedba.CustLinkMan l on l.id = cs.CustLinkMan";
            sqlcontact += " where cs.CompanyCD='" + CompanyCD + "' and datediff(day,getdate(),cs.nextlinkdate)>=0 and datediff(day,getdate(),cs.nextlinkdate)<=7 ";
            sqlcontact += " and (";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            DataTable dt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sqlcontact += " (cs.ModifiedUserID IN  ";
                    sqlcontact += " (  SELECT UserID FROM  officedba.UserInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sqlcontact += " (cs.ModifiedUserID IN  ";
                    sqlcontact += " (SELECT UserID FROM  officedba.UserInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID) )) or  ";
                }
                if (dt.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sqlcontact += " (cs.ModifiedUserID IN  ";
                    sqlcontact += " ( SELECT UserID FROM  officedba.UserInfo ";
                    sqlcontact += "  WHERE DeptID IN (SELECT a.ID  ";
                    sqlcontact += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                    sqlcontact += "  WHERE a.ID=b.ID)))  or ";
                }
            }


            sqlcontact += " (CHARINDEX('," + userInfo.EmployeeID + ",',','+cs.CanViewUser+',')>0 ) or (cs.ModifiedUserID IN (SELECT UserID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) ";
            sqlcontact += " or (cs.Executant IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) or (cs.OurLinkMan IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";
            sqlcontact += " order by cs.NextLinkDate";
            return SqlHelper.ExecuteSql(sqlcontact);
        }
        #endregion

    }
}
