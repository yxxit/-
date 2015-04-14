<%@ WebHandler Language="C#" Class="KuaiDiName_New" %>

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

public class KuaiDiName_New : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["OrderByUc_KuaiDi"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CompanyID";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
                 
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string KuaiDiId = context.Request.Form["KuaiDiId"].ToString();
            string KuaiDiName = context.Request.Form["KuaiDiName"].ToString();
            
            //获取数据
              
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = new DataTable();
            CustDt = dtGetKuaiDi(KuaiDiId,KuaiDiName);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     
                     id = x.Element("id").Value,
                     CompanyID = x.Element("CompanyID").Value,
                     CompanyName = x.Element("CompanyName").Value,
                     CompanyType = x.Element("CompanyType").Value,
                     CompanyAdCode = x.Element("CompanyAdCode").Value,
                     Personer = x.Element("Personer").Value,
                     P_Tel = x.Element("P_Tel").Value,
                     P_Mobile = x.Element("P_Mobile").Value,
                     CarCode = x.Element("CarCode").Value,
                     CarType = x.Element("CarType").Value,
                     Remark = x.Element("Remark").Value,
                    
                     
                   
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     CompanyID = x.Element("CompanyID").Value,
                     CompanyName = x.Element("CompanyName").Value,
                     CompanyType = x.Element("CompanyType").Value,
                     CompanyAdCode = x.Element("CompanyAdCode").Value,
                     Personer = x.Element("Personer").Value,
                     P_Tel = x.Element("P_Tel").Value,
                     P_Mobile = x.Element("P_Mobile").Value,
                     CarCode = x.Element("CarCode").Value,
                     CarType = x.Element("CarType").Value,
                     Remark = x.Element("Remark").Value,
                            
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

    private DataTable dtGetKuaiDi(string KuaiDiId, string KuaiDiName)
    {
        string Hc_Database = System.Configuration.ConfigurationManager.AppSettings["HC_database"].ToString();
        string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
      
        string selKuaiDi = "select CompanyID id,CompanyID,CompanyName,CompanyType,"+
        "CompanyAdCode,Personer,P_Tel,P_Mobile,CarCode,CarType,Remark from officedba.Delivery where 1=1 and companycd='" + companycd + "' ";
        if (KuaiDiId != "")
        {
            selKuaiDi += " and CompanyID like '%" + KuaiDiId + "%' ";
        }
        if (KuaiDiName != "")
        {
            selKuaiDi += " and CompanyName like '%" + KuaiDiName + "%'";
        }
        DataTable dtKuaiDi = SqlHelper.ExecuteSql(selKuaiDi);
        return dtKuaiDi;
                     
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
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string CompanyAdCode { get; set; }
        public string Personer { get; set; }
        public string P_Tel { get; set; }
        public string P_Mobile { get; set; }
        public string CarCode { get; set; }
        public string CarType { get; set; }
        public string Remark { get; set; }
        
            
    }

}