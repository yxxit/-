<%@ WebHandler Language="C#" Class="SearchUser" %>

using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Model.Office.SellManager;
using System.Web.SessionState;
using System.Text;
using XBase.Business.Common;
using XBase.Data.DBHelper;
public class SearchUser : IHttpHandler,IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
      
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {

            string Action = context.Request.Params["Action"];
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            if (Action == "SearchUser")
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件
                string userId = context.Request.Form["UserInfo"].ToString().Trim();  //用户编号

                int TotalCount = 0;

            

                DataTable dt = new DataTable();

                string strQuery = " select  isnull(a.UserID,'')as UserId, isnull(a.UserId,'') as id, ISNULL(b.EmployeeName,'') AS EmployeeName " +
              "from officedba.UserInfo  a Left Join officedba.EmployeeInfo b on b.ID = a.EmployeeID" +
                " where a.CompanyCD = '" + CompanyCD + "' and a.IsRoot!='1'";


                if (userId != "")
                    strQuery += " and a.UserID like '%" + userId + "%'";

                // dt = SqlHelper.ExecuteSql(strQuery);
                // TotalCount = dt.Rows.Count;
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                string orderBy = " UserId  desc  ";
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
                StringBuilder sb = new StringBuilder();

                if (dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
               sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
                context.Response.Write(sb.ToString());
                context.Response.End();

            }





        }
    }
 
    
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}