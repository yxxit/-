using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Collections;

namespace XBase.Data
{
    public  class SendNoticeDBHelper
    {
        public static DataTable GetSendTable() {
            string Sqlstr = "select * from  officedba.NoticeHistory where  PlanNoticeDate>@NowTime  and RealNoticeDate is  NULL ";

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            comm.Parameters.Add("@NowTime", SqlDbType.DateTime);
            comm.Parameters["@NowTime"].Value = DateTime.Now.AddHours(-1.5);
            return SqlHelper.ExecuteSearch(comm);
        }

        public static void GetAgendString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select *,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName from officedba.PersonalDateArrange as  pda  inner join officedba.EmployeeInfo as ei on pda.Creator = ei.ID  where pda.ID = " + SourceID;

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt =  SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                SendText = "您有新的日程安排，" + dt.Rows[0]["Content"] + ",请登陆系统查看";
            }
            else {
                CompanyCD = "";
                MobileNum = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
                SendText = "";
            }
        }
        public static void GetAimString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select * ,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName ,bt.TypeName    from officedba.PlanAim pa  inner join officedba.EmployeeInfo as ei on pa.PrincipalID = ei.ID   left join  officedba.CodePublicType  bt on  pa.AimTypeID=bt.id  where pa.ID = " + SourceID;

            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                switch (dt.Rows[0]["AimFlag"] + "")
                {
                    case "1": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "日" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "2": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年第"+dt.Rows[0]["AimNum"]+"周" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "3": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年"+dt.Rows[0]["AimNum"]+"月" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "4": SendText = "请尽快登录系统查看" +dt.Rows[0]["AimDate"]  + "年第"+dt.Rows[0]["AimNum"]+"季度" + dt.Rows[0]["ReceiveUserName"] + "的"+dt.Rows[0]["TypeName"]+"。"; break;
                    case "5": SendText = "请尽快登录系统查看" + dt.Rows[0]["AimDate"] + "年" + dt.Rows[0]["ReceiveUserName"] + "的" + dt.Rows[0]["TypeName"] + "。"; break;
                    default: SendText = ""; break;
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName =  "";
                ReceiveUserID =  "";
            }
        }


        public static void GetTaskString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select *,ei.ID as ReceiveUserID  ,ei.EmployeeName as ReceiveUserName,bt.TypeName from officedba.Task  ta  inner join officedba.EmployeeInfo as ei on ta.Principal = ei.ID    left join  officedba.CodePublicType  bt on  ta.TaskTypeID=bt.id   where ta.ID = " + SourceID;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiveUserID"] + "";
                SendText = "您有" + dt.Rows[0]["TypeName"] + "(" + dt.Rows[0]["Title"] + ")需要完成,请尽快登录系统查看详情";
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID =  "";
            }
        }
        #region 获取销售机会发送信息串
        /// <summary>
        /// 获取销售机会发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetSellChanceString(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            string Sqlstr = "select sc.*,e.EmployeeName as ReceiveUserName ";
            Sqlstr += " from officedba.SellChance  sc  ";
            Sqlstr += "left join officedba.EmployeeInfo as e on sc.ReceiverID = e.ID  ";
            Sqlstr += "where sc.ID = " + SourceID;
            SqlCommand comm = new SqlCommand();
            comm.CommandText = Sqlstr;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["RemindMTel"] + "";
                ReceiveUserName = dt.Rows[0]["ReceiveUserName"] + "";
                ReceiveUserID = dt.Rows[0]["ReceiverID"] + "";
                //SendText = "您有" + dt.Rows[0]["ChanceNo"] + "(" + dt.Rows[0]["Title"] + ")需要完成,请尽快登录系统查看详情";
                SendText = dt.Rows[0]["RemindContent"].ToString()+" 来自编号为"+dt.Rows[0]["ChanceNo"]+"的销售机会！";
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion

        public static int GetAutoMessageNum(string CompanyCD) {
            string sql = " select AutoMsgNum from pubdba.companyOpenServ  where CompanyCD='" + CompanyCD + "' ";
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            int num = 0;
            try
            {
               num =  Convert.ToInt32(dt.Rows[0][0]);
            }
            catch { 
            }

            return num;
        }


        public static bool UpdataAutoMessageNum(string CompanyCD, int num, string ReceiveUserName, string ReceiveUserID, string MobileNum, string SendText)
        {
            string sql = " update pubdba.companyOpenServ set  AutoMsgNum= " + num + "  where   CompanyCD='" + CompanyCD + "' ";
            sql += @"INSERT INTO [officedba].[MobileMsgMonitor]
           ([CompanyCD]
           ,[MsgType]
           ,[SendUserID]
           ,[SendUserName]
           ,[ReceiveUserID]
           ,[ReceiveUserName]
           ,[ReceiveMobile]
           ,[Content]
           ,[Status]
           ,[CreateDate]
           ,[SendDate])"+
         "    VALUES  ( '" + CompanyCD + "' ,'0',0,'系统短信'," + ReceiveUserID + ",'" + ReceiveUserName + "','" + MobileNum + "','" + SendText + "','1','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' )";
            
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql;
            try
            {
              return   SqlHelper.ExecuteTransWithCommand(comm);
            }
            catch {
                return false;
            }
        }

        #region 获取进度模块发送信息串
        /// <summary>
        /// 获取进度模块发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetMsgProess(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select A.*,B.Mobile,B.EmployeeName from officedba.MsgSendList A left join officedba.Employeeinfo B ");
            sb.AppendLine("on A.empid=B.ID where A.ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                MobileNum = dt.Rows[0]["Mobile"] + "";
                ReceiveUserName = dt.Rows[0]["EmployeeName"] + "";
                ReceiveUserID = dt.Rows[0]["Empid"] + "";
                SendText = dt.Rows[0]["msgContent"].ToString();
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion
        #region 同步预警提醒设置信息到notice
        public static bool InsertRemindSet(string type,string CompanyCD)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (type.Trim()=="0")
                    sb.AppendLine("select * from officedba.RemindSet ");
                else
                    sb.AppendLine("select * from officedba.RemindSet where CompanyCD='"+CompanyCD+"' and RemindType='" + type + "'");
                DataTable dt = SqlHelper.ExecuteSql(sb.ToString());
                DateTime SendDate = new DateTime();
                ArrayList ArrayRemindComm = new ArrayList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        StringBuilder strSqlDetail = new StringBuilder();
                        SqlCommand commDetail = new SqlCommand();
                        if (dt.Rows[i]["IsMobileNotice"].ToString() == "1")
                        {
                            if (dt.Rows[i]["RemindType"].ToString() == "11")//当类型为费用报销时单独处理
                            {
                                SendDate = System.DateTime.Now.AddMinutes(5);
                            }
                            else
                            {
                                if (dt.Rows[i]["RemindPeriod"].ToString() == "1") //周期是天
                                {
                                    SendDate = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");
                                }
                                else if (dt.Rows[i]["RemindPeriod"].ToString() == "2")//周期是周
                                {
                                    if ((int)DateTime.Today.DayOfWeek == Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString())) //今天等于设置周几
                                    {
                                        if (DateTime.Now < Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00"))//今天时间点小于设置的时间点，存本周设置的周几时间
                                            SendDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");
                                        else
                                            SendDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek + Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString())).ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");//今天时间点大于设置的时间点，存下周设置的周几时间
                                    }
                                    else if ((int)DateTime.Today.DayOfWeek > Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()))//今天大于设置的时间
                                    {
                                        SendDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - (int)DateTime.Today.DayOfWeek + Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString())).ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");//存下周设置的周几时间
                                    }
                                    else if ((int)DateTime.Today.DayOfWeek < Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()))//今天小于设置的时间
                                    {
                                        SendDate = Convert.ToDateTime(DateTime.Today.AddDays(Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()) - (int)DateTime.Today.DayOfWeek).ToShortDateString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");//存本周设置的周几时间
                                    }
                                }
                                else if (dt.Rows[i]["RemindPeriod"].ToString() == "3")//周期为月
                                {
                                    if (DateTime.Now.Day < Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()))//今天小于设置的每月的几号，存本月的几号
                                    {
                                        SendDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + dt.Rows[i]["SubPeriod"].ToString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");
                                    }
                                    else if (DateTime.Now.Day > Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()))
                                    {
                                        SendDate = Convert.ToDateTime(DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-" + dt.Rows[i]["SubPeriod"].ToString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");//今天大于设置的每月的几号，存下月的几号
                                    }
                                    if (DateTime.Now.Day == Convert.ToInt32(dt.Rows[i]["SubPeriod"].ToString()))//今天等于设置的每月的几号，存本月的几号
                                    {
                                        if (DateTime.Now < Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + dt.Rows[i]["SubPeriod"].ToString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00"))//当天时间小于设置的时间，存当月设置的时间
                                        {
                                            SendDate = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + dt.Rows[i]["SubPeriod"].ToString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");
                                        }
                                        else
                                        {
                                            SendDate = Convert.ToDateTime(DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-" + dt.Rows[i]["SubPeriod"].ToString() + " " + dt.Rows[i]["RemindTime"].ToString() + ":00");//当天时间大于设置的时间，存下月设置的时间
                                        }
                                    }
                                }
                            }
                            StringBuilder noticesb = new StringBuilder();
                            noticesb.AppendLine("select * from officedba.NoticeHistory where CompanyCD='" + dt.Rows[i]["CompanyCD"].ToString() + "' and SourceFlag='" + dt.Rows[i]["RemindType"].ToString() + "'  AND RealNoticeDate IS NULL");//AND Convert(varchar(10),REPLACE(Convert(char(10),datepart(yy,plannoticedate)),' ','')+'-'+REPLACE(Convert(char(10),datepart(mm,plannoticedate)),' ','')+'-'+REPLACE(Convert(char(10),datepart(dd,plannoticedate)),' ',''))='" + SendDate.ToShortDateString() + "'
                            DataTable noticedt = SqlHelper.ExecuteSql(noticesb.ToString());

                            StringBuilder ifsendsb = new StringBuilder();
                            ifsendsb.AppendLine("select * from officedba.NoticeHistory where CompanyCD='" + dt.Rows[i]["CompanyCD"].ToString() + "' and SourceFlag='" + dt.Rows[i]["RemindType"].ToString() + "' AND Convert(varchar(10),REPLACE(Convert(char(10),datepart(yy,plannoticedate)),' ','')+'-'+REPLACE(Convert(char(10),datepart(mm,plannoticedate)),' ','')+'-'+REPLACE(Convert(char(10),datepart(dd,plannoticedate)),' ',''))='" + SendDate.ToShortDateString() + "' AND RealNoticeDate IS NOT NULL");
                            DataTable ifsenddt = SqlHelper.ExecuteSql(ifsendsb.ToString());
                            if (ifsenddt != null && ifsenddt.Rows.Count > 0) { }
                            else
                            {
                                if (noticedt != null && noticedt.Rows.Count > 0)
                                {
                                    strSqlDetail.Append("UPDATE officedba.NoticeHistory ");
                                    strSqlDetail.Append("SET PlanNoticeDate=@PlanNoticeDate");
                                    strSqlDetail.Append(" WHERE CompanyCD=@CompanyCD AND SourceFlag=@SourceFlag AND RealNoticeDate IS NULL");
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNoticeDate ", SendDate.ToString()));
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", dt.Rows[i]["CompanyCD"].ToString()));
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@SourceFlag ", dt.Rows[i]["RemindType"].ToString()));
                                }
                                else
                                {
                                    strSqlDetail.Append("INSERT INTO  officedba.NoticeHistory ( ");
                                    strSqlDetail.Append("CompanyCD,SourceFlag,SourceID,PlanNoticeDate ) Values ");
                                    strSqlDetail.Append("(@CompanyCD,@SourceFlag,@SourceID,@PlanNoticeDate )");
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", dt.Rows[i]["CompanyCD"].ToString()));
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@SourceFlag ", dt.Rows[i]["RemindType"].ToString()));
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@SourceID ", dt.Rows[i]["ID"].ToString()));
                                    commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@PlanNoticeDate ", SendDate.ToString()));
                                }
                                commDetail.CommandText = strSqlDetail.ToString();
                                ArrayRemindComm.Add(commDetail);
                            }
                        }
                        else 
                        {
                            strSqlDetail.Append("DELETE officedba.NoticeHistory ");
                            strSqlDetail.Append("WHERE CompanyCD=@CompanyCD AND SourceFlag=@SourceFlag AND RealNoticeDate IS NULL");
                            commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", dt.Rows[i]["CompanyCD"].ToString()));
                            commDetail.Parameters.Add(SqlHelper.GetParameterFromString("@SourceFlag ", dt.Rows[i]["RemindType"].ToString()));
                            commDetail.CommandText = strSqlDetail.ToString();
                            ArrayRemindComm.Add(commDetail);
                        }
                    }//for结束
                    bool result = SqlHelper.ExecuteTransWithArrayList(ArrayRemindComm);
                    return result;
                }
                else
                    return false;
            }
            catch (Exception ex) { return false; }
            //
        }
        #endregion
        #region 获取库存限量报警发送信息串
        /// <summary>
        /// 获取库存限量报警发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetStorageRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            int TotalCount = 0;
            XBase.Model.Office.StorageManager.StockAccountModel _M = new XBase.Model.Office.StorageManager.StockAccountModel();
            _M.CompanyCD = dt.Rows[0]["CompanyCD"].ToString();
            DataTable dtcount = new DataTable();
            dtcount = XBase.Data.Office.StorageManager.StorageProductAlarmDBHelper.GetStorageProductAlarmForRemind("0", _M, "", 1, 10000, "ProductNo", ref TotalCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (TotalCount > 0)
                {
                    CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                    MobileNum = dt.Rows[0]["Mobile"] + "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                    SendText = "有新的库存限量报警" + TotalCount.ToString() + "条,请登陆系统查看";
                }
                else
                {
                    CompanyCD = "";
                    MobileNum = "";
                    SendText = "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion
        #region 获取客户联络延期报警发送信息串
        /// <summary>
        /// 获取客户联络延期报警发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetCustRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            int TotalCount = 0;
            DataTable dtcount = new DataTable();
            dtcount=XBase.Data.Office.CustManager.ContactHistoryDBHelper.GetContactDefer(dt.Rows[0]["CompanyCD"].ToString(), 1, 10000, "linkdate", ref TotalCount);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (TotalCount > 0)
                {
                    CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                    MobileNum = dt.Rows[0]["Mobile"] + "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                    SendText = "有新的客户联络延期预警" + TotalCount.ToString() + "条,请登陆系统查看";
                }
                else
                {
                    CompanyCD = "";
                    MobileNum = "";
                    SendText = "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion
        #region 获取供应商联络延期报警发送信息串
        /// <summary>
        /// 获取供应商联络延期报警发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetPriRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            int TotalCount = 0;
            DataTable dtcount = new DataTable();
            dtcount=XBase.Data.Office.PurchaseManager.ProviderContactHistoryDBHelper.SelectProviderContactDelay(1, 10000, "ID", ref TotalCount, dt.Rows[0]["CompanyCD"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                if (TotalCount > 0)
                {
                    CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                    MobileNum = dt.Rows[0]["Mobile"] + "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                    SendText = "有新的供应商联络延期预警" + TotalCount.ToString() + "条,请登陆系统查看";
                }
                else
                {
                    CompanyCD = "";
                    MobileNum = "";
                    SendText = "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion
        #region 获取即将到期的劳动合同报警发送信息串
        /// <summary>
        /// 获取即将到期的劳动合同报警发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetContrRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
            int TotalCount = 0;
            string sql = " select  count(*) from officedba.EmployeeContract where  CompanyCD=@CompanyCD and    DATEADD(day, -AheadDay, EndDate) <@day ";
            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@CompanyCD", dt.Rows[0]["CompanyCD"].ToString());
            paras[1] = new SqlParameter("@day", DateTime.Now.ToString("yyyy-MM-dd"));

            TotalCount= Convert.ToInt32(XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, paras));
            if (dt != null && dt.Rows.Count > 0)
            {
                if (TotalCount > 0)
                {
                    CompanyCD = dt.Rows[0]["CompanyCD"] + "";
                    MobileNum = dt.Rows[0]["Mobile"] + "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                    SendText = "有新的即将到期的劳动合同预警" + TotalCount.ToString() + "条,请登陆系统查看";
                }
                else
                {
                    CompanyCD = "";
                    MobileNum = "";
                    SendText = "";
                    ReceiveUserName = "";
                    ReceiveUserID = "";
                }
            }
            else
            {
                CompanyCD = "";
                MobileNum = "";
                SendText = "";
                ReceiveUserName = "";
                ReceiveUserID = "";
            }
        }
        #endregion
        #region 获取办公用品缺货报警发送信息串
        /// <summary>
        /// 获取办公用品缺货报警发送信息串
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        //public static void GetOfficeRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        //{

        //    //StringBuilder sb = new StringBuilder();
        //    //sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
        //    //SqlParameter[] param = {
        //    //                           new SqlParameter("@id",SqlDbType.VarChar,50)
        //    //                       };
        //    //param[0].Value = SourceID;
        //    //DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);
        //    //int TotalCount = 0;
        //    //DataTable dtcount = new DataTable();
        //    //dtcount=XBase.Data.Office.AdminManager.OfficeThingsInfoDBHelper.SearchAlarmInfoList("", "", dt.Rows[0]["CompanyCD"].ToString(), 1, 10000, "ID", ref TotalCount);
        //    //if (dt != null && dt.Rows.Count > 0)
        //    //{
        //    //    if (TotalCount > 0)
        //    //    {
        //    //        CompanyCD = dt.Rows[0]["CompanyCD"] + "";
        //    //        MobileNum = dt.Rows[0]["Mobile"] + "";
        //    //        ReceiveUserName = "";
        //    //        ReceiveUserID = "";
        //    //        SendText = "有新的办公用品缺货预警" + TotalCount.ToString() + "条,请登陆系统查看";
        //    //    }
        //    //    else
        //    //    {
        //    //        CompanyCD = "";
        //    //        MobileNum = "";
        //    //        SendText = "";
        //    //        ReceiveUserName = "";
        //    //        ReceiveUserID = "";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    CompanyCD = "";
        //    //    MobileNum = "";
        //    //    SendText = "";
        //    //    ReceiveUserName = "";
        //    //    ReceiveUserID = "";
        //    //}
        //}
        #endregion
        #region 获取费用报销报警发送信息串（需单独处理）
        /// <summary>
        /// 获取费用报销报警发送信息串（需单独处理）
        /// </summary>
        /// <param name="SourceID"></param>
        /// <param name="MobileNum"></param>
        /// <param name="SendText"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="ReceiveUserName"></param>
        /// <param name="ReceiveUserID"></param>
        public static void GetFeeRemindInfo(string SourceID, out string MobileNum, out string SendText, out string CompanyCD, out string ReceiveUserName, out string ReceiveUserID)
        {
            CompanyCD = "";
            MobileNum = "";
            SendText = "";
            ReceiveUserName = "";
            ReceiveUserID = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM officedba.RemindSet WHERE ID=@id");
            SqlParameter[] param = {
                                       new SqlParameter("@id",SqlDbType.VarChar,50)
                                   };
            param[0].Value = SourceID;
            DataTable dt = SqlHelper.ExecuteSql(sb.ToString(), param);//获取公司编码


            StringBuilder sbFee = new StringBuilder();//查询某公司下申请人的所有报销延期的条数
            sbFee.AppendLine("select Count(*)TotalCount,Applyor from officedba.FeeApply");
            sbFee.AppendLine("where CompanyCD=@CompanyCD AND IsReimburse='0' AND Status='2' AND EndReimbTime<getdate()");
            sbFee.AppendLine("group by Applyor");
            SqlParameter[] paramFee = {
                                       new SqlParameter("@CompanyCD",SqlDbType.VarChar,50)
                                   };
            paramFee[0].Value = dt.Rows[0]["CompanyCD"].ToString();
            DataTable dtFee = SqlHelper.ExecuteSql(sbFee.ToString(), paramFee);
            if (dtFee != null && dtFee.Rows.Count>0) 
            {
                for (int i = 0; i < dtFee.Rows.Count; i++)
                {
                    int messgeanum = XBase.Data.SendNoticeDBHelper.GetAutoMessageNum(dt.Rows[0]["CompanyCD"].ToString());
                    if (messgeanum <= 0)
                        return;
                    else
                    {
                        StringBuilder sbTel = new StringBuilder();//查询申请人对应的手机号码
                        sbTel.AppendLine("select Mobile from officedba.EmployeeInfo where ID=@ID");
                        SqlParameter[] paramTel = {
                                       new SqlParameter("@ID",SqlDbType.VarChar,50)
                                   };
                        paramTel[0].Value = dtFee.Rows[i]["Applyor"].ToString();
                        DataTable dtTel = SqlHelper.ExecuteSql(sbTel.ToString(), paramTel);

                        if (!string.IsNullOrEmpty(dtTel.Rows[0]["Mobile"].ToString()))
                        {
                            string Mobiles = dtTel.Rows[0]["Mobile"].ToString();
                            string SendContent = "有新的报销延期预警" + dtFee.Rows[i]["TotalCount"].ToString() + "条,请登录系统查看";
                            if (XBase.Common.SMSender.InternalSendbackgroud(Mobiles, SendContent))
                            {

                                string Sqlstr = "update  officedba.NoticeHistory  set RealNoticeDate ='" + DateTime.Now + "'  where SourceFlag= '" + dt.Rows[0]["RemindType"].ToString() + "'   and  SourceID='" + SourceID + "'";
                                SqlHelper.ExecuteSql(Sqlstr);
                                XBase.Data.SendNoticeDBHelper.UpdataAutoMessageNum(dt.Rows[0]["CompanyCD"].ToString(), messgeanum - 1, ReceiveUserName, ReceiveUserID, Mobiles, SendContent);
                               
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
