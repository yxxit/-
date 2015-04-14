using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Business
{
    public  class SendNotice
    {
        
        private static System.Threading.Timer timer = null;
        private static string  PostUrl
         {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessageURL"];
                if (val + "" == "")
                {
                    throw new Exception("短信发送页面地址未配置");
                }
                return val;
            }
        }

        private static long ScanTime
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["SendMessageScanTime"];
                if (val + "" == "")
                {
                    throw new Exception("短信发送后台扫描时间没有配置 未配置");
                }
                return Convert.ToInt64(val);
            }
        }
        private static string RemindScanTime
        {
            get
            {
                string val = System.Web.Configuration.WebConfigurationManager.AppSettings["RemindScanTime"];
                if (val + "" == "")
                {
                    throw new Exception("预警提醒设置扫描时间没有配置");
                }
                return val;
            }
        }

        public static void Start()
        {
            SendNotice Action = new SendNotice();
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(Action.DoSend));
            timer.Change(10, ScanTime);
        }
        /// <summary>
        /// 同步预警设置
        /// </summary>
        public void RemindSet() 
        {
            try
            {
                string hour = System.DateTime.Now.Hour.ToString();
                string minute = System.DateTime.Now.Minute.ToString();
                if (Convert.ToInt32(hour) < 10)
                    hour = "0" + hour;
                if (Convert.ToInt32(minute) < 10)
                    minute = "0" + minute;
                string Now = hour + ":" + minute + ":00";
                string val = RemindScanTime;// System.Web.Configuration.WebConfigurationManager.AppSettings["RemindScanTime"];
                
                if (Now == val)
                {
                    //定时同步
                    XBase.Data.SendNoticeDBHelper.InsertRemindSet("0","");
                }
            }
            catch(Exception ex)
            { }
        }
        public void DoSend(object state)
        {
            RemindSet();
            DataTable dt = new DataTable();
            dt = XBase.Data.SendNoticeDBHelper.GetSendTable();
            foreach (DataRow dr in dt.Rows) { 
                 DateTime date = DateTime.Now;
                 try{
                   date = DateTime.Parse(dr["PlanNoticeDate"].ToString()); 
                 } catch{
                    continue; 
                 }
                 if (date < DateTime.Now) {

                     string MobileNum = "";
                     string SendText="";
                     string CompanyCD = "";
                     string ReceiveUserName = "";
                     string ReceiveUserID = "";

                     GetSendString(dr["SourceFlag"].ToString(), dr["SourceID"].ToString(), out  MobileNum, out  SendText, out CompanyCD, out ReceiveUserName, out ReceiveUserID);

                     int messgeanum = XBase.Data.SendNoticeDBHelper.GetAutoMessageNum(CompanyCD);
                     if (messgeanum <= 0)
                         return ;
                     if (!string.IsNullOrEmpty(MobileNum))
                     {
                         if (XBase.Common.SMSender.InternalSendbackgroud(MobileNum, SendText))
                         {

                             string Sqlstr = "update  officedba.NoticeHistory  set RealNoticeDate ='" + DateTime.Now + "'  where SourceFlag= '" + dr["SourceFlag"] + "'   and  SourceID='" + dr["SourceID"] + "'";
                             SqlHelper.ExecuteSql(Sqlstr);
                             XBase.Data.SendNoticeDBHelper.UpdataAutoMessageNum(CompanyCD, messgeanum - 1, ReceiveUserName, ReceiveUserID, MobileNum, SendText);
                         }
                     }
                 }
            }
        }

        private void GetSendString(string SourceFlag, string SourceID, out string MobileNum,out  string SendText ,out string CompanyCD,out string ReceiveUserName, out string ReceiveUserID)
        {
            string RNum ="";
            string RText="";
            string RCompanyCD = "";
            string RReceiveUserName="";
            string RReceiveUserID = "";
            switch (SourceFlag.Trim()) {
                case "1": XBase.Data.SendNoticeDBHelper.GetAgendString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "2": XBase.Data.SendNoticeDBHelper.GetAimString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "3": XBase.Data.SendNoticeDBHelper.GetTaskString(SourceID, out  RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "4": XBase.Data.SendNoticeDBHelper.GetSellChanceString(SourceID, out RNum, out   RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break;
                case "5": XBase.Data.SendNoticeDBHelper.GetMsgProess(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //进度控制 
                case "6": XBase.Data.SendNoticeDBHelper.GetStorageRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //库存限量报警
                case "7": XBase.Data.SendNoticeDBHelper.GetCustRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //客户联络延期报警
                case "8": XBase.Data.SendNoticeDBHelper.GetPriRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //供应商联络延期报警
                case "9": XBase.Data.SendNoticeDBHelper.GetContrRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //即将到期的劳动合同报警
                // case "10": XBase.Data.SendNoticeDBHelper.GetOfficeRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //办公用品缺货报警
                case "11": XBase.Data.SendNoticeDBHelper.GetFeeRemindInfo(SourceID, out RNum, out RText, out RCompanyCD, out RReceiveUserName, out RReceiveUserID); break; //费用报销延期报警
            }
            MobileNum  = RNum;
            SendText  =RText;
            CompanyCD = RCompanyCD;
            ReceiveUserName =RReceiveUserName;
            ReceiveUserID = RReceiveUserID;
        }

    }
}
