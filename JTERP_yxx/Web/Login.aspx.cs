using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Microsoft.Win32;
using System.Xml;

using System.Runtime.InteropServices;
using System.Text;
using System.Security.Cryptography;
public partial class Login : System.Web.UI.Page
{

    protected string CustomPath
    {
        get;
        set;
    }

    protected string CustomSysName
    {
        get;
        set;
    }

    //[DllImport("SYUNEW3D.dll")]
    ////查找指定的加密锁
    //public static extern int FindPort(int start, StringBuilder OutKeyPath);

    //[DllImport("SYUNEW3D.dll")]
    ////从加密锁中读字符串
    //public static extern int YReadString(StringBuilder outstring, short Address, short mylen, string HKey, string LKey, string KeyPath);

    //public static string KeyPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        string USBKeyFlag = string.Empty;
        //try
        //{
        //    USBKeyFlag = ConfigurationSettings.AppSettings["USBKeyFlag"].ToString();
        //}
        //catch
        //{
        //    USBKeyFlag = "true";
        //}
        //if (USBKeyFlag == "true")
        //{
        //    #region/*读加密锁*/
        //    StringBuilder DevicePath = new StringBuilder("", 256);
        //    if (FindPort(0, DevicePath) != 0)
        //    {
        //        HidKey.Value = "2"; //加密锁不存在
        //    }
        //    else
        //    {
        //        KeyPath = DevicePath.ToString();
        //        //当加密锁存在时判断
        //        //从加密锁中读取字符串,使用默认的读密码(FFFFFFFF-FFFFFFFF)
        //        StringBuilder outstring;
        //        short mylen;
        //        //长度和写入的串的长度必须一致
        //        mylen = 8;
        //        outstring = new StringBuilder("", mylen);
        //        //初始化数据为0
        //        for (int i = 0; i < mylen; i++) { outstring.Append(0); }
        //        int num = YReadString(outstring, 0, mylen, "ffffffff", "ffffffff", KeyPath);
        //        if (YReadString(outstring, 0, mylen, "FFFFFFFF", "FFFFFFFF", KeyPath) != 0) { return; }
        //        string result = outstring.ToString().Substring(0, 8);

        //        MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
        //        string MDStr = BitConverter.ToString(MD5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(result))).Replace("-", "");

        //        string usbkey = string.Empty;
        //        #region 加密锁验证
        //        try
        //        {
        //            usbkey = ConfigurationSettings.AppSettings["USBKey"].ToString();

        //            if (usbkey != "" && usbkey.Length > 8)
        //            {
        //                usbkey = usbkey.Substring(8);
        //                if (usbkey == MDStr)
        //                {
        //                    HidKey.Value = "1";//加密锁验证通过
        //                }
        //                else
        //                {
        //                    HidKey.Value = "3"; //加密锁非法，或加密锁秘钥配置不正确
        //                }
        //            }
        //            else
        //            {
        //                HidKey.Value = "4";//加密锁秘钥未配置
        //            }
        //        }
        //        catch
        //        {
        //            HidKey.Value = "0"; //未找到加密锁的秘钥配置
        //        }
        //        #endregion
        //    }
        //    #endregion
        //}
        //else
        //{
        //    HidKey.Value = "1";
        //}

        if (!Page.IsPostBack)
        {

            /*验证版本 
             * 原版GUID:025AA3C8-FE57-45B5-A995-A61C92C8AB74
             * 执行力GUID:88CB3C6D-2CC5-4680-8BEE-F7DD01A7D1B7
             * 当该配置节不存在时，直接抛出异常，不做任何操作。
             */
            if (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"] == null)
            {
                divDefault.InnerHtml = "<br /></br><br/><center><span style=\"font-size:18px;color:red;\"> 对不起，版本GUID配置节不存在，请联系客服。</span></center>";
                divVerConert.Visible = false;
               // divZxl100.InnerHtml = "";
                Page.Title = "错误信息";                
                return;
            }
            else 
            {
                switch (System.Configuration.ConfigurationManager.AppSettings["VerNameGuid"].ToString())
                {
                    case XBase.Common.ConstUtil.Ver_XGoss_Guid://生产版
                   //     divZxl100.InnerHtml = "";
                        Page.Title = "煤炭进销存管理信息系统";
                        CustomPath = "LoginXGoss";
                        CustomSysName = "T6企业信息化管理平台，更多产品描述和帮助信息请参考产品服务网站";
                        break;
                    case XBase.Common.ConstUtil.Ver_Zxl100_Guid://执行力 
                      //  divDefault.InnerHtml = "";
                        Page.Title = "执行力100%";
                        CustomPath = "LoginZxl100";
                        CustomSysName = "执行力100%管理系统";
                        break;
                    default://未匹配到
                        divDefault.InnerHtml = "<br /></br><br/><center><span style=\"font-size:18px;color:red;\"> 对不起，版本GUID配置节不存在，请联系客服。</span></center>";
                        divVerConert.Visible = false;

                     //   divZxl100.InnerHtml = "";
                        Page.Title = "错误信息";
                        break;
                }
                    
            }




            //判断是否是退出
            try
            {
                if (Request.QueryString["flag"].ToString() == "1")
                {
                    XBase.Common.SessionUtil.Session.Clear();
                    XBase.Common.UserSessionManager.Remove(Session.SessionID);
                   
                   // HidKey.Value = "0";
                }
            }
            catch (Exception ex)
            { }
            //if (SetLicenseDisabled())
            //{
            //    string cpuid = XBase.Common.LicenseValidator.GetCUPInfo();
            //    string[] t = XBase.Common.LicenseValidator.getlicense();
            //    if (t[0] == "false")
            //    {
            //        HttpContext.Current.Response.Clear();
            //        HttpContext.Current.Response.Write("授权信息验证失败,请联系管理员！");
            //        HttpContext.Current.Response.End();
            //        return;
            //    }
            //    else
            //    {

            //        for (int i = 0; i < t.Length - 1; i++)
            //        {
            //            //测试是本机是否是授权的服务器
            //            if (i == 1)
            //            {
            //                if (t[i] != cpuid)
            //                {
            //                    // MessageBox.Show("您的服务器没有得到授权！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //                    HttpContext.Current.Response.Clear();
            //                    HttpContext.Current.Response.Write("您的服务器没有得到授权,请联系管理员！");
            //                    HttpContext.Current.Response.End();
            //                    return;
            //                }
            //            }
            //            //测试开始的时间
            //            if (i == 3)
            //            {
            //                try
            //                {
            //                    if (DateTime.Now <= Convert.ToDateTime(t[i]))
            //                    {
            //                        //  MessageBox.Show("你的授权还未开始！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //                        HttpContext.Current.Response.Clear();
            //                        HttpContext.Current.Response.Write("您的服务器授权还未开始,请联系管理员！");
            //                        HttpContext.Current.Response.End();
            //                        return;
            //                    }
            //                }
            //                catch
            //                {

            //                    HttpContext.Current.Response.Clear();
            //                    HttpContext.Current.Response.Write("授权信息验证失败,请联系管理员！");
            //                    HttpContext.Current.Response.End();
            //                    return;
            //                }
            //            }
            //            //测试结束的时间
            //            if (i == 5)
            //            {
            //                try
            //                {
            //                    if (DateTime.Now >= Convert.ToDateTime(t[i]))
            //                    {
            //                        // MessageBox.Show("你的授权以到期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);

            //                        HttpContext.Current.Response.Clear();
            //                        HttpContext.Current.Response.Write("您的服务器授权已到期,请联系管理员！");
            //                        HttpContext.Current.Response.End();
            //                        return;
            //                    }
            //                    else
            //                    {
            //                        if (DateTime.Now.AddMonths(1) >= Convert.ToDateTime(t[i]))
            //                        {
            //                            HttpContext.Current.Response.Write(" <script type='text/javascript' language='javascript'> alert('您的授权马上到期，请联系管理员！')</script>");
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                    // MessageBox.Show("你的授权有误请重新填写！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //                    HttpContext.Current.Response.Clear();
            //                    HttpContext.Current.Response.Write("授权信息验证失败,请联系管理员！");
            //                    HttpContext.Current.Response.End();
            //                    return;
            //                }
            //            }
            //        }
            //    }

            //    return;
            //}
        }
        
        if (!this.IsPostBack)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (Cache["login_SysNotice"] == null)
            {               
                string fpath = Server.MapPath("App_Data/SysNotice.xml");
                if( !System.IO.File.Exists(fpath))
                {
                 //   panelNotice.Visible = false;
                    //if (panelNotice != null)
                   //     panelNotice.Visible = false;

                    //if (panelNoticeZXL != null)
                    //    panelNoticeZXL.Visible = false;
                    return;
                }
                xmlDoc.Load(fpath);
                Cache.Insert("login_SysNotice", xmlDoc, new System.Web.Caching.CacheDependency(fpath));
            }
            else {
                xmlDoc = (XmlDocument)Cache["login_SysNotice"];
            }

            string pubDate = xmlDoc.SelectSingleNode("/notice/pubdate").InnerText;
            string overDate = xmlDoc.SelectSingleNode("/notice/overdate").InnerText;

            if (DateTime.Parse(overDate) <= DateTime.Now)
            {
                return;
            }
            
           // this.lblSysNoticeContent.Text = xmlDoc.SelectSingleNode("/notice/content").InnerText;
           // lblSysNoticePubDate.Text = "&nbsp;&nbsp;" + pubDate;

        }
        
    }
    //private bool SetLicenseDisabled()
    //{
    //    string aa = System.Configuration.ConfigurationManager.AppSettings["SetLicenseDisabledForSaaS"];
    //    if (System.Configuration.ConfigurationManager.AppSettings["SetLicenseDisabledForSaaS"] == "true")
    //        return true;
    //    else
    //        return false;
    //}
   
}
