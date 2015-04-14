<%@ WebHandler Language="C#" Class="CustContact" %>

using System;
using System.Web;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using System.Data;
using System.Xml.Linq;
using XBase.Common;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;
using System.Text;

public class CustContact : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        string Action = context.Request.Params["action"].ToString().Trim();
        UserInfoUtil UserInfo=(UserInfoUtil)SessionUtil.Session["UserInfo"];
        if (Action == "getcustcontact")
        {
            DataTable dataList = new DataTable();
            dataList = TalkBus.GetCustContact(UserInfo.CompanyCD);
            StringBuilder sb = new StringBuilder();

            sb.Append("{result:true,data:");
            sb.Append("{count:" + dataList.Rows.Count.ToString() + ",");
            sb.Append("list:" +JsonClass.DataTable2Json(dataList) + "}");
            sb.Append("}");
            context.Response.Write(sb.ToString());
        }
        else if(Action=="getcustservice")
        {
            DataTable dt = new DataTable();
            dt = TalkBus.GetCustService(UserInfo.CompanyCD);
            StringBuilder sb = new StringBuilder();
            sb.Append("{result:true,data:");
            sb.Append("{count:" + dt.Rows.Count.ToString() + ",");
            sb.Append("list:" + JsonClass.DataTable2Json(dt) + "}");
            sb.Append("}");
            context.Response.Write(sb.ToString());
        }
     }
        

 
    public bool IsReusable {
        get {
            return false;
        }
    }

}