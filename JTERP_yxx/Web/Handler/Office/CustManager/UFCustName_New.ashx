<%@ WebHandler Language="C#" Class="UFCustName_New" %>

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

public class UFCustName_New : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["OrderByUc_UFCust"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "cCusCode";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string UFCustId = context.Request.Form["UFCustId"].ToString();
            string UFCustName = context.Request.Form["UFCustName"].ToString();
            
            //获取数据
              
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = new DataTable ();
            CustDt = dtGetUFCust(UFCustId,UFCustName);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     
                     id = x.Element("id").Value,
                     cCusCode = x.Element("cCusCode").Value,
                     cCusName = x.Element("cCusName").Value,
                     cCusAbbName = x.Element("cCusAbbName").Value,
                     cCusAddress = x.Element("cCusAddress").Value,
                     cCusPerson = x.Element("cCusPerson").Value,
                     cCusPPerson = x.Element("cCusPPerson").Value,
                     cpersonname = x.Element("cpersonname").Value,

                     cCusOAddress = x.Element("cCusOAddress").Value,
                     cCusPhone = x.Element("cCusPhone").Value,
                     cCusHand = x.Element("cCusHand").Value, 
                   
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     cCusCode = x.Element("cCusCode").Value,
                     cCusName = x.Element("cCusName").Value,
                     cCusAbbName = x.Element("cCusAbbName").Value,
                     cCusAddress = x.Element("cCusAddress").Value,
                     cCusPerson = x.Element("cCusPerson").Value,
                     cCusPPerson = x.Element("cCusPPerson").Value,
                     cpersonname = x.Element("cpersonname").Value,

                     cCusOAddress = x.Element("cCusOAddress").Value,
                     cCusPhone = x.Element("cCusPhone").Value,
                     cCusHand = x.Element("cCusHand").Value,     
                     
                           
                            
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

    private DataTable dtGetUFCust(string UFCustId, string UFCustName)
    {
        string Hc_Database = System.Configuration.ConfigurationManager.AppSettings["HC_database"].ToString();
        string selUFCust = "SELECT a.cCusCode id,a.cCusCode,a.cCusName,a.cCusAbbName,"+
            "isnull(a.cCusAddress,'') cCusAddress,isnull(a.cCusPerson,'') cCusPerson,isnull(a.cCusOAddress,'') cCusOAddress,isnull(a.cCusPhone,'') cCusPhone,isnull(a.cCusHand,'') cCusHand," +
        "    isnull(a.cCusPPerson,'') cCusPPerson,isnull(b.cpersonname,'') cpersonname FROM " + Hc_Database + ".dbo.customer a" +
        "    left join  "+Hc_Database+".dbo.person b on a.cCusPPerson=b.cpersoncode where 1=1 ";
        if (UFCustId != "")
        {
            selUFCust += " and a.cCusCode like '%" + UFCustId + "%' ";
        }
        if (UFCustName != "")
        {
            selUFCust += " and a.cCusName like '%" + UFCustName + "%'";
        }
        DataTable dtUFCust = SqlHelper.ExecuteSql(selUFCust);
        return dtUFCust;
                     
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
        public string cCusCode { get; set; }
        public string cCusName { get; set; }
        public string cCusAbbName { get; set; }
        public string cCusAddress { get; set; }
      
        public string cCusPPerson { get; set; }
        public string cpersonname { get; set; }


        public string cCusOAddress { get; set; }
        public string cCusPerson { get; set; }
        public string cCusPhone { get; set; }
        public string cCusHand { get; set; }
            
    }

}