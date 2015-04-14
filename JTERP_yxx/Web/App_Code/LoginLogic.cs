using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// LoginLogic 的摘要说明
/// </summary>
public class LoginLogic
{
	public LoginLogic()
	{

	}
    /// <summary>
    /// 判断后登录
    /// </summary>
    /// <param name="TopRootStr">输入路径</param>
    /// <param name="ClassName">类名</param>
    public static void MatchLoad(string TopRootStr,string ClassName)
    {
        //SessionInclude.SessionId = "admin";//临时值
        //SessionInclude.Id = "1";//临时值
        if (!string.IsNullOrEmpty(CookieInclude.CookieId))
        {
            SessionInclude.SessionId = CookieInclude.CookieId;
            SessionInclude.Id = CookieInclude.Id;
        }
     
        object userObj = SessionInclude.SessionId;
        if (userObj == null)
        {
            HttpContext.Current.Response.Redirect(TopRootStr + "Login_Cust.aspx");
        }
        if (CannotUse(ClassName))
        {
            HttpContext.Current.Response.Redirect(TopRootStr + "SystemError.aspx");
        }
    }
    public static bool CannotUse(string ClassName)
    {
        bool IsCannotUse = false;
    //Model.Users MU=new BLL.Users().GetModel(int.Parse(SessionInclude.Id));
    //Model.User_Priv MUP = new BLL.User_Priv().GetModel(MU.PrivId);
    //string[] QXStr = MUP.FuncIdStr.Split(new string[]{","},StringSplitOptions.RemoveEmptyEntries);
    //foreach (string QX in QXStr)
    //{
    //    if (QX == ClassName)
    //   {
    //       IsCannotUse = true;
    //       break;
    //   }
    //}
    return IsCannotUse;



    }
}
/// <summary>
/// Cookies属性操作类
/// </summary>
public class CookieInclude
{
    public CookieInclude()
    {
    }
    /// <summary>
    /// 用户的CookiesID属性
    /// </summary>
    public static string CookieId
    {
        set
        {
            HttpContext.Current.Response.Cookies["oa_cookiename"].Value = HttpContext.Current.Server.UrlEncode(value);
            HttpContext.Current.Response.Cookies["oa_cookiename"].Expires = DateTime.Now.AddDays(365);
        }
        get
        {
            try
            {
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies["oa_cookiename"].Value);//通过索引来获取值
            }
            catch (Exception exp)
            {
                if (exp is System.NullReferenceException)
                {
                    return "";
                }
                else
                {
                    throw exp;
                }
            }
        }
    }
    /// <summary>
    /// 用户的CookiesID属性
    /// </summary>
    public static string Id
    {
        set
        {
            HttpContext.Current.Response.Cookies["oa_cookieId"].Value = value;
            HttpContext.Current.Response.Cookies["oa_cookieId"].Expires = DateTime.Now.AddDays(365);
        }
        get
        {
            try
            {
                return HttpContext.Current.Request.Cookies["oa_cookieId"].Value;//通过索引来获取值
            }
            catch (Exception exp)
            {
                if (exp is System.NullReferenceException)
                {
                    return "";
                }
                else
                {
                    throw exp;
                }
            }
        }
    }

    /// <summary>
    /// 清楚Cookies的ID
    /// </summary>
    public static void Clear()
    {
        HttpContext.Current.Response.Cookies["oa_cookiename"].Expires = DateTime.Now.AddDays(-1);
    }


}
/// <summary>
/// Session属性操作类
/// </summary>
public class SessionInclude
{
    /// <summary>
    /// 用户的用户ID属性
    /// </summary>
    public static string SessionId
    {
        set
        {
            HttpContext.Current.Session["AdminUser"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["AdminUser"] != null)
            {
                return HttpContext.Current.Session["AdminUser"].ToString();
            }
            else
            {
                return null;
            }
        }
    }
    /// <summary>
    /// 用户的ID属性
    /// </summary>
    public static string Id
    {
        set
        {
            HttpContext.Current.Session["AdminUserId"] = value;
        }
        get
        {
            if (HttpContext.Current.Session["AdminUserId"] != null)
            {
                return HttpContext.Current.Session["AdminUserId"].ToString();
            }
            else
            {
                return null;
            }
        }
    }

}
