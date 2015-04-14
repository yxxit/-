<%@ WebHandler Language="C#" Class="DeptName" %>

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

public class DeptName : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["OrderByUc_Dept"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "DeptId";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string DeptId = context.Request.Form["DeptId"].ToString();
            string DeptName = context.Request.Form["DeptName"].ToString();
            
            //获取数据
              
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = new DataTable ();
            CustDt = dtGetDept(DeptId,DeptName);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     DeptId = x.Element("DeptId").Value,
                     DeptName = x.Element("DeptName").Value,
                     cDepPerson = x.Element("cDepPerson").Value,
                     iDepGrade = x.Element("iDepGrade").Value,
                     bDepEnd = x.Element("bDepEnd").Value,
                      
                   // 
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     DeptId = x.Element("DeptId").Value,
                     DeptName = x.Element("DeptName").Value,
                     cDepPerson = x.Element("cDepPerson").Value,
                     iDepGrade = x.Element("iDepGrade").Value,
                     bDepEnd = x.Element("bDepEnd").Value,      
                            
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

    private DataTable dtGetDept(string DeptId, string DeptName)
    {
        string Hc_Database = System.Configuration.ConfigurationManager.AppSettings["HC_database"].ToString();
        string selDept = "select (Department.cDepCode) as id,(Department.cDepCode) as DeptId,cDepName DeptName,isnull(cDepPerson,'') cDepPerson,iDepGrade,"+
        " ( CASE bDepEnd when 1 then '是' else '否' end) bDepEnd"+
        " , isnull(cDepProp,'') cDepProp,cDepPhone,cDepAddress,cDepMemo,iCreLine,cCreGrade,"+
        " iCreDate  from "+Hc_Database+".dbo.Department where ((bdepend=1) ) "+
        " and (1=1) ";
        if (DeptId != "")
        {
            selDept += " and Department.cDepCode like '%" + DeptId + "%' ";
        }
        if (DeptName != "")
        {
            selDept += " and cDepName like '%" + DeptName + "%'";
        }
        selDept += " Order by cDepCode ASC   ";
        DataTable dtDept = SqlHelper.ExecuteSql(selDept);
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
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string cDepPerson { get; set; }
        public string iDepGrade { get; set; }
        public string bDepEnd { get; set; }
        
             
    }

}