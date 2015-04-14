<%@ WebHandler Language="C#" Class="CustInfoGet" %>

using System;
using System.Web;
using System.Data;
using XBase.Common;
using System.Data.SqlClient;
using XBase.Data;
using XBase.Data.DBHelper;
using System.Collections;

public class CustInfoGet : IHttpHandler ,System.Web.SessionState.IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context)
    {
        string CustNo = context.Request.Params["CustNo"].ToString();
        string CustName = context.Request.Params["CustName"].ToString();
        string CreatedDate = context.Request.Params["CreateDate"].ToString();
        try
        {
            context.Response.Write("{sta:1,info:'',ID:'" + GetCustInfo(CustNo, CustName, CreatedDate).Rows[0]["ID"].ToString() + "'}");
        }
        catch (Exception ee)
        { }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private DataTable GetCustInfo(string CustNo, string CustName, string CreatedDate)//检测已经过期的证书
    {
        //string table = "CustInfo";
       //UserInfoUtil userInfo = (UserInfoUtil).Session["UserInfo"];
        string sql = "select ID from officedba.CustInfo where CustNo='" + CustNo + "' and companycd='HCYY1' and CustName='"+CustName+"' and CreatedDate='"+CreatedDate+"'";
       // SqlParameter[] paras = new SqlParameter[1];
      //  paras[0] = new SqlParameter("@CompanyCD", userInfo.CompanyCD);
        //ArrayList arr = new ArrayList();
        //arr.Add(new SqlParameter("@CompanyCD", "HCYY1"));
        DataTable  DT= SqlHelper.ExecuteSql(sql);
        return DT;
    }

}