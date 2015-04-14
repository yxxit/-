<%@ WebHandler Language="C#" Class="MessageSend" %>

using System;
using System.Web;

public class MessageSend : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string textcontent = context.Server.UrlDecode(context.Request.QueryString["SendText"].ToString());
        //byte[] temp = System.Text.Encoding.GetEncoding("gb2312").GetBytes(textcontent);
        //textcontent = System.Text.Encoding.GetEncoding("gb2312").GetString(temp);
        XBase.Common.SMSender.InternalSend(context.Request.QueryString["Number"].ToString(), textcontent);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}