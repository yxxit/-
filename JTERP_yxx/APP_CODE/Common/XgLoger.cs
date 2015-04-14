using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.SessionState;
using System.Web;
using System.Configuration;
using XBase.Common;

namespace XBase.Common
{
    public class XgLoger :  IRequiresSessionState

    {
        private static EasyLoger.Loger myloger = EasyLoger.Loger.GetInstance();
        public static void Log(EasyLoger.LogMessage msg)
        {
            myloger.Write(msg);
        }

        public static void Log(string msgtxt)
        {
            myloger.Write(new EasyLoger.LogMessage(msgtxt));
        }
      
        public static void Log(Exception ex)
        {
            
            UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string _CompanyCD;

            // liuch 20120427 调试状态下，userinfo 取值为空
            if (UserInfo == null)
            {
                _CompanyCD = "debug";
            }
            else { _CompanyCD = UserInfo.CompanyCD; }
            string filepath = "Web/Error";
            DirectoryInfo di = new DirectoryInfo(filepath);
            if (!di.Exists)
            {
                di.Create();
            }
            string[] fpath = filepath.Split('/');
            string fpath1 = fpath[fpath.Length - 1];
            string path = "~/" + fpath1 + "/" + _CompanyCD + "-" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            // string path = "~/" + fpath1 + "/" + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine(" Log Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                ". Error Message:" + ex;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }
        public static void Log(Exception ex, string url)
        {
            EasyLoger.LogMessage msg = new EasyLoger.LogMessage(ex);
            msg.Message += "\r\n fromUrl:" + url;
            myloger.Write(msg);

        }
    }
}
