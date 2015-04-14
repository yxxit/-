<%@ WebHandler Language="C#" Class="Intellisense" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.SellManager;
using XBase.Model.Office.SellManager;
using XBase.Data.DBHelper;
//using XBase.Data.SqlHelper;

public class Intellisense : IHttpHandler,System.Web.SessionState.IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            context.Request.ContentEncoding = Encoding.GetEncoding("utf-8");
            string action = (context.Request.Form["action"].ToString());//操作
            string txtValue = (context.Request.Form["txtValue"].ToString());//操作
            
            if (action == "CustInfo")
            {
                GetCustInfo(txtValue, context);
            }
            else if (action == "ProviderInfo")
            {
                GetProviderInfo(txtValue, context);
            }
            else if (action == "YYCustInfo")
            {
                GetYYCustInfo(txtValue,context);
            }
        }
    }

    /// <summary>
    /// 获取供应商数据列表
    /// </summary>
    private void GetProviderInfo(string txt, HttpContext context)
    {
        DataTable dt = new DataTable();
        DataTable dtFilter = new DataTable();
        if (SessionUtil.Session["ProviderInfoIntellisense"] != null)
        {
            dt = (DataTable)SessionUtil.Session["ProviderInfoIntellisense"];
        }
        else
        {
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql = @"select a.ID,a.CustNo,a.CustName,a.TakeType as TakeType,isnull(b.TypeName,'') as TakeTypeName,a.CarryType as CarryType,isnull(c.TypeName,'') as CarryTypeName,
            a.PayType as PayType,isnull(d.TypeName,'') as PayTypeName from officedba.ProviderInfo as a  left join officedba.codepublictype as b on b.id = a.TakeType 
            and b.CompanyCD=a.CompanyCD left join officedba.codepublictype as c on c.id = a.CarryType left join officedba.codepublictype as d on d.id = a.PayType 
            where a.UsedStatus =1 AND a.CompanyCD=@CompanyCD ";
            
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            //strSql += " and (CustNo like  '%" + txt.Trim() + "%'";
            //strSql += " or CustName like  '%" + txt.Trim() + "%')";
            //strSql += " and  UsedStatus = '1' ";

            dt.TableName = "ProviderInfo";
            dt = SqlHelper.ExecuteSql(strSql, arr);
            SessionUtil.Session.Add("ProviderInfoIntellisense", dt);
        }

        dtFilter = dt.Clone();
        DataRow[] mRow = dt.Select("CustNo like  '%" + txt.Trim() + "%' or CustName like '%" + txt.Trim() + "%'");
        if (mRow != null && mRow.Length > 0)
        {
            foreach (DataRow r in mRow)
            {
                dtFilter.ImportRow(r);
            }
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(dtFilter.Rows.Count.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dtFilter));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 获取客户数据列表
    /// </summary>
    private void GetCustInfo(string txt, HttpContext context)
    {
        DataTable dt = new DataTable();
        //DataTable dtFilter = new DataTable();
        //if (SessionUtil.Session["CustInfoIntellisense"] != null)
        //{
        //    dt = (DataTable)SessionUtil.Session["CustInfoIntellisense"];
        //}
        //else
        //{
            string strCompanyCD = string.Empty;//单位编号

            strCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            string strSql = string.Empty;
            strSql = "SELECT top 7 ID, CustNo, CustName, ArtiPerson, CustNote, Relation" +
                     " FROM officedba.CustInfo " +
                     " WHERE  CompanyCD=@CompanyCD ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", strCompanyCD));
            strSql += " and (CustNo like  '%" + txt.Trim() + "%'";
            strSql += " or CustName like  '%" + txt.Trim() + "%')";
            strSql += " and  UsedStatus = '1' ";

            dt.TableName = "CustInfo";
            dt = SqlHelper.ExecuteSql(strSql, arr);
        //    SessionUtil.Session.Add("CustInfoIntellisense", dt);
        //}

        //dtFilter = dt.Clone();
        //DataRow[] mRow = dt.Select("CustNo like  '%" + txt.Trim() + "%' or CustName like '%" + txt.Trim() + "%'");
        //if (mRow != null && mRow.Length > 0)
        //{
        //    foreach (DataRow r in mRow)
        //    {
        //        dtFilter.ImportRow(r);
        //    }
        //}

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(dt.Rows.Count.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    /// <summary>
    /// 获取YY客户数据列表
    /// </summary>
    private void GetYYCustInfo(string txt, HttpContext context)
    {
        DataTable dt = new DataTable();
        string strSql = string.Empty;
        strSql = "SELECT top 10 [cCusCode] as  CustNo, [cCusName] as CustName, [cCusAbbName],isnull(cCusAddress,'') CustAddr " +
                 " FROM [dbo].[Customer] " +
                 " WHERE  cCusCode like @cCusCode or cCusName like @cCusCode ";
        ArrayList arr = new ArrayList();
        arr.Add(new SqlParameter("@cCusCode", "%" + txt.Trim() + "%"));

        dt.TableName = "Customer";
        dt = YYSqlHelper.ExecuteSql(strSql, arr);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(dt.Rows.Count.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}