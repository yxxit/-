<%@ WebHandler Language="C#" Class="UFEmployeeName" %>

using System;
using System.Web;
using System.Xml.Linq;
using XBase.Common;
using XBase.Business.Office.CustManager;
using System.Data;
using System.IO;
using XBase.Model.Office.CustManager;
using System.Web.Script.Serialization;
using System.Linq;
using XBase.Data.DBHelper;

public class UFEmployeeName : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["OrderByUc_Employee"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "cEmployeecode";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string EmployeeId = context.Request.Form["EmployeeId"].ToString();
            string EmployeeName = context.Request.Form["EmployeeName"].ToString();
            
            //获取数据
              
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = new DataTable ();

            CustDt = dtGetEmployee(EmployeeId, EmployeeName);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     cEmployeecode = x.Element("cEmployeecode").Value,
                     cEmployeename = x.Element("cEmployeename").Value,
                     cdepcode = x.Element("cdepcode").Value,
                     cdepname = x.Element("cdepname").Value,
                      
                   // 
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     cEmployeecode = x.Element("cEmployeecode").Value,
                     cEmployeename = x.Element("cEmployeename").Value,
                     cdepcode = x.Element("cdepcode").Value,
                     cdepname = x.Element("cdepname").Value,    
                            
                 });
            int totalCount = dsLinq.Count();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            sb.Append(ToJSON(dsLinq.Skip(skipRecord).Take(pageCount).ToList()));
            sb.Append("}");

               /*   int totalCount = CustDt.Rows.Count;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(totalCount.ToString());
            sb.Append(",data:");
            sb.Append(JsonClass.DataTable2Json(CustDt));
            sb.Append("}");*/
             
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }


    private DataTable dtGetEmployee(string EmployeeId, string EmployeeName)
    {
        string Hc_Database = System.Configuration.ConfigurationManager.AppSettings["HC_database"].ToString();
        string selEmployee = "select cpersoncode id,cpersoncode cEmployeecode,cpersonname cEmployeename,a.cdepcode cdepcode," +
       " cdepname from  " + Hc_Database + ".dbo.person a left join "+Hc_Database+".dbo.department b on a.cdepcode=b.cdepcode  where 1=1 ";
        if (EmployeeId != "")
        {
            selEmployee += " and cpersoncode='" + EmployeeId + "' ";
        }
        if (EmployeeName != "")
        {
            selEmployee += " and cpersonname like '%" + EmployeeName + "%'";
        }
        selEmployee += "  order by cpersoncode   ";

        DataTable dtDept = SqlHelper.ExecuteSql(selEmployee);
        return dtDept;
                     
    }
    //把DataTable转换为XML流
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        //DiffGram
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public class DataSourceModel
    {
        public string id { get; set; }
        public string cEmployeecode { get; set; }
        public string cEmployeename { get; set; }
        public string cdepcode { get; set; }
        public string cdepname { get; set; }
           
    }

}