<%@ WebHandler Language="C#" Class="DrugName22" %>

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


public class DrugName22 : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
         if (context.Request.RequestType == "POST")
        {
           //设置行为参数
            string orderString = (context.Request.Form["orderbyuc"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CompanyId";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

            //获取数据
           
            ProInfoModel ProInfoM = new ProInfoModel();
            ProInfoM.Cvencode = context.Request.Form["CompanyId"].ToString().Trim();
            ProInfoM.Cvenname = context.Request.Form["CompanyName"].ToString().Trim();
            ProInfoM.Free1 = context.Request.Form["free1"].ToString().Trim();
                        
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = ProInfoBus.GetProName(ProInfoM, CanUserID, CompanyCD);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     CompanyId = x.Element("CompanyId").Value,
                     CompanyName = x.Element("CompanyName").Value,
                     cvenperson = x.Element("cvenperson").Value,
                     cvenaddress = x.Element("cvenaddress").Value,
                     cvenpostcode = x.Element("cvenpostcode").Value,
                     cvenfax = x.Element("cvenfax").Value,
                     cVenLPerson = x.Element("cVenLPerson").Value,
                     cvenphone = x.Element("cvenphone").Value,
                     yyzzNo = x.Element("yyzzNo").Value,
                     Xukezheng = x.Element("Xukezheng").Value,
                      
                   // 
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     CompanyId = x.Element("CompanyId").Value,
                     CompanyName = x.Element("CompanyName").Value,
                     cvenperson = x.Element("cvenperson").Value,
                     cvenaddress = x.Element("cvenaddress").Value,
                     cvenpostcode = x.Element("cvenpostcode").Value,
                     cvenfax = x.Element("cvenfax").Value,
                     cVenLPerson = x.Element("cVenLPerson").Value,
                     cvenphone = x.Element("cvenphone").Value,
                     yyzzNo = x.Element("yyzzNo").Value,
                     Xukezheng = x.Element("Xukezheng").Value,               
                            
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
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string cvenperson { get; set; }
        public string cvenaddress { get; set; }
        public string cvenpostcode { get; set; }
        public string cvenfax { get; set; }
        public string cVenLPerson { get; set; }
        public string cvenphone { get; set; }
        public string yyzzNo { get; set; }
        public string Xukezheng { get; set; }
        
    }

}