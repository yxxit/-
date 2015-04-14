<%@ WebHandler Language="C#" Class="SaleOrderName_New" %>

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

public class SaleOrderName_New : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
           //设置行为参数
           
            string orderString = (context.Request.Form["OrderByUc_SaleOrder"].ToString());//排序
            string order = "ascending";//排序：升序
            string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "OrderNo";//要排序的字段，如果为空，默认为"ID"
            if (orderString.EndsWith("_d"))
            {
                order = "descending";//排序：降序
            }
                 
            int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
            int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
            
            int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
            string SaleOrderId = context.Request.Form["SaleOrderId"].ToString();
            string Date1 = context.Request.Form["Date1"].ToString();
            string Date2 = context.Request.Form["Date2"].ToString();
            string isinit = context.Request.Form["isinit"].ToString();
            string custname = context.Request.Form["custname"].ToString();
            if (isinit == "1")
            {
                Date1 = DateTime.Now.ToShortDateString();
                Date2 = DateTime.Now.ToShortDateString(); 
            }
            //获取数据
              
            //int CreateID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//建单人ID
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//用户公司代码 
            string CanUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();//当前用户ID
            DataTable CustDt = new DataTable();
            CustDt = dtGetSaleOrder(SaleOrderId,Date1,Date2,custname);
               
       
            XElement dsXML = ConvertDataTableToXML(CustDt);

            //linq排序
            var dsLinq =
                (order == "ascending") ?
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value ascending
                 select new DataSourceModel()
                 {
                     
                     id = x.Element("id").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     CustName = x.Element("CustName").Value,
                     LinkManName = x.Element("LinkManName").Value,
                     orderDate = x.Element("orderDate").Value,
                     ExpressOrderNum = x.Element("ExpressOrderNum").Value,
                     
                     cCusPerson = x.Element("cCusPerson").Value,
                     cCusPhone = x.Element("cCusPhone").Value,
                     cCusOAddress = x.Element("cCusOAddress").Value,
                     
                   
                 }):
                (from x in dsXML.Descendants("Data")
                 orderby x.Element(orderBy).Value descending
                 select new DataSourceModel()
                 {
                     id = x.Element("id").Value,
                     OrderNo = x.Element("OrderNo").Value,
                     CustName = x.Element("CustName").Value,
                     LinkManName = x.Element("LinkManName").Value,
                     orderDate = x.Element("orderDate").Value,
                     ExpressOrderNum = x.Element("ExpressOrderNum").Value,

                     cCusPerson = x.Element("cCusPerson").Value,
                     cCusPhone = x.Element("cCusPhone").Value,
                     cCusOAddress = x.Element("cCusOAddress").Value,
                    
                            
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

    private DataTable dtGetSaleOrder(string SaleOrderId,string Date1,string Date2,string custname)
    {
        string Hc_Database = System.Configuration.ConfigurationManager.AppSettings["HC_database"].ToString();
        string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string selSaleOrder = "select  isnull(b.cCusPerson,'')cCusPerson,isnull(cCusPhone,'')cCusPhone,isnull(cCusOAddress,'')cCusOAddress,"+
        "OrderNo id,OrderNo,CustName,isnull(LinkManName,'') LinkManName," +
        "convert(varchar(100),orderDate,23) orderDate,isnull(ExpressOrderNum,'')ExpressOrderNum from  officedba.YYSellOrder a "+
        " left join "+Hc_Database+".dbo.customer b on a.custid=b.ccuscode " +
        "  where 1=1 and companycd='" + companycd + "' ";
        if (SaleOrderId != "")
        {
            selSaleOrder += " and OrderNo like '%" + SaleOrderId + "%' ";
        }
        if (Date1 != "")
        {
            selSaleOrder += " and convert(varchar(100),orderDate,23) >= '" + Date1 + "' ";
        }
        if (Date2 != "")
        {
            selSaleOrder += " and convert(varchar(100),orderDate,23) <= '" + Date2 + "' ";
        }
        if (custname != "")
        {
            selSaleOrder += " and CustName like '%"+custname+"%'  "; 
        }
        
        DataTable dtSaleOrder = SqlHelper.ExecuteSql(selSaleOrder);
        return dtSaleOrder;
                     
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
        public string CustName { get; set; }
        public string LinkManName { get; set; }
        public string orderDate { get; set; }
        public string ExpressOrderNum { get; set; }
        public string OrderNo { get; set; }
        public string cCusPerson { get; set; }
        public string cCusPhone { get; set; }
        public string cCusOAddress { get; set; }
            
    }

}