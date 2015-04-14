<%@ WebHandler Language="C#" Class="ContractInfo" %>

using System;
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

public class ContractInfo : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Action = context.Request.Params["Action"];

            if (Action == "SearchSellContract")
            {

                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                string headid = context.Request.Form["headid"].ToString();
                int TotalCount = 0;
                
                //string billtypeid = context.Request.Form["billtypeid"].ToString();
                //string billid = context.Request.Form["autoid"].ToString();
               
                ////获取数据

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();



                string strQuery = "select  a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
               "      isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName,"+
                "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
                "    a.TransPortType,h.typename as TransPortTypeName,"+
                "     a.ContractMoney,c.productname,c.unitid,d.codename as unitname,isnull(c.specification,'') specification," +
                "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus,"+
                "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
                "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney "+
                "    from ContractHead_Sale a"+
                "    left join ContractDetails_Sale b on a.id=b.contractid"+
                "    left join officedba.ProductInfo c on b.cinvccode=c.id"+
                "    left join officedba.CodeUnitType d on c.unitid=d.ID"+
                "    left join officedba.CustInfo e on a.cCusCode=e.id "+
                "    left join officedba.ProviderInfo f on a.cVenCode=f.id"+
                "    left join officedba.CodePublicType  g on a.SettleType=g.id"+
                "    left join officedba.CodePublicType h on  a.TransPortType=h.id"+
                "    left join officedba.EmployeeInfo i on a.creator=i.id" +
                "    where a.companycd='" + CompanyCD + "' and a.cVouchType='1' and a.BillType='1' and a.id='"+headid+"' order by a.id desc,b.autoid ";
                dt = SqlHelper.ExecuteSql(strQuery);
                TotalCount = dt.Rows.Count;


                string strAnn = "select a.ParentId as InqNo,a.annFileName as AnnFileName,a.AnnAddr,a.upDatetime as UpDateTime,a.annRemark as AnnRemark from officedba.Annex a" +
                    " where a.CompanyCD='" + CompanyCD + "'  and ModuleType='" + ConstUtil.MODULE_ID_SELLCONTRANCT_ADD + "' and a.ParentId='" + headid + "'";
                
                
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
                {
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));


                    DataTable dtAttach = SqlHelper.ExecuteSql(strAnn);
                    
                    if (dtAttach.Rows.Count > 0)
                    {
                        sb.Remove(sb.Length - 1,1);
                        sb.Append(",");
                        sb.Append("dataAttach:");
                        sb.Append(JsonClass.DataTable2Json(dtAttach));
                        sb.Append("}");
                    }
                    
                }
                context.Response.Write(sb.ToString());
                context.Response.End();
                
            }
            else if (Action == "SearchOutWareList")//查询销售出库列表
            {

                string orderString = context.Request.Form["orderby"].ToString();//排序
                string order = "desc";//排序：降序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "asc";//排序：升序
                }
                string ord = orderBy + " " + order;
                
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件
                string OutWareNo = context.Request.Form["OutWareNo"].ToString().Trim();  //出库单号
                string CustName = context.Request.Form["CustName"].ToString().Trim();   //客户
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //出库开始时间
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //出库结束时间
                string TransactorName = context.Request.Form["TransactorName"].ToString().Trim();   //出库人
                string StartPlace = context.Request.Form["StartPlace"].ToString().Trim();   //发站
                string SendNo = context.Request.Form["SendNo"].ToString().Trim();   //发货单编号
                
                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = "  select a.id,a.outno,isnull(a.custid,'') custid,isnull(c.custname,'') custname ,b.productid," +
                "     isnull(d.ProductName,'') productname,b.productcount,b.totalprice," +
                "    ISNULL(e.ship_place,'') as startstation, ISNULL(e.to_place,'') as endstation,  " +
                "     (case e.at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'    when '50' then '到货' " +
                "     else '未生效' end) transstate,(case a.billstatus when '1' then '未确认' when '2' then '已确认' end) as billstatus"+
                "    from jt_xsck  a" +
                "    left join jt_xsck_mx  b on a.id=b.OutNo" +
                "    left join officedba.custinfo c on a.custid=c.id" +
                "    left join officedba.ProductInfo d on b.productid=d.id" +
                "    left join jt_HuocheDiaoyun e on a.transportid=e.id_at   " +
                "    left join officedba.EmployeeInfo f on f.id=a.Transactor   " +
                "    left join jt_xsfh g on g.id=a.FromBillID   " +
                "  where 1=1 and a.companycd='" + CompanyCD + "' ";

                if (OutWareNo != "")
                    strQuery += " and a.outno like '%" + OutWareNo + "%'";
                if (CustName != "" )
                    strQuery += " and c.custname like '%" + CustName + "%'";
                if (BeginT != "")
                    strQuery += " and CONVERT(varchar(100),a.OutDate, 23)>='" + BeginT + "'";
                if (EndT != "")
                    strQuery += " and CONVERT(varchar(100),a.OutDate, 23)<='" + EndT + "'";
                if (TransactorName != "")
                    strQuery += " and f.EmployeeName like '%" + TransactorName + "%'";
                if (StartPlace != "")
                    strQuery += " and e.ship_place like '%" + StartPlace + "%'";
                if (SendNo != "")
                    strQuery += " and g.SendNo like '%" + SendNo + "%'";
                
                
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                //string orderBy = " id  desc  ";
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
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
            else if (Action == "SearchOutWareOne")//加载销售出库单据详细信息
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                string headid = context.Request.Form["headid"].ToString();
                int TotalCount = 0;
 

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = "select a.id,a.outno,a.frombillid,isnull(f.sendno,'') sendno,a.billStatus," +
                "    isnull(f.paytype,0) as settletype,isnull(f.carrytype,0) as transporttype,"+
                "    isnull(a.custid,0) custid,isnull(c.custname,'') custname,"+
                "    isnull(c.billunit,'') billunit,a.transactor as ppersonid,isnull(g.employeename,'') pperson,"+
                "    convert(varchar(12),a.outdate,23) outdate,isnull(a.counttotal,0) outcount,isnull(e.ship_quantity,0) transendcount," +
                "    isnull(a.transportfee,0) as transmoney,a.deptid,isnull(h.deptname,'') deptname,a.remark,"+
                "    isnull(e.id_at,0) as diaoyunid,isnull(e.diaoyunno,'') diaoyunno,isnull(e.motorcade,'') as carno,"+
                "    isnull(e.ship_place,'') as startystation,isnull(e.to_place,'') as endstation,"+
                "    isnull(e.vehicle_quantity,0) as carnum,"+
                "    (case e.at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途' "+
                "    when '50' then '到货' else '未生效' end) transstate ,"+
                " a.Confirmor,i.EmployeeName as ConfirmorName, CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate"+
                "    from  jt_xsck a left join jt_xsck_mx b on a.id=b.outno"+
                "    left join officedba.custinfo c on a.custid=c.id"+
                "    left join officedba.ProductInfo d on b.productid=d.id"+
                "    left join jt_HuocheDiaoyun e on a.transportid=e.id_at  "+
                "    left join jt_xsfh f on a.frombillid=f.id"+
                "    left join officedba.EmployeeInfo g on a.transactor=g.id"+
                "    left join officedba.DeptInfo h on a.deptid=h.id" +
                "    left join officedba.EmployeeInfo i on i.id=a.Confirmor" +
                "    where 1=1 and a.id='"+headid+"'";

                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                string orderBy = " id  desc  ";
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
            else if (Action == "SearchOutWareOneDetail")  //获取某个出库单的明细信息
            {
                string headid = context.Request.Form["headid"].ToString();

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = @"select a.xsfhmx_autoid as id, a.StorageID,b.StorageName,a.ProductID,c.ProdNo,c.ProductName,
                                    cast(a.UsedUnitCount as int) as ProCount,c.unitid,isnull(e.codename,'') as unitName,
                                    a.usedPrice as TaxPrice ,a.TaxRate,a.TotalTax,a.TotalPrice as TotalFee,
                                    d.OutCount,d.SttlCount,isnull(d.productCount,0) as TotalNum,(isnull(d.productCount,0)-isnull(d.OutCount,0)) as ProductCount


                                    from dbo.jt_xsck_mx a  
                                    left join officedba.StorageInfo b on b.id=a.storageID
                                    left join officedba.ProductInfo c on c.id=a.ProductID 
                                    left join dbo.jt_xsfh_mx d on a.xsfhmx_autoid=d.id  
                                    left join officedba.CodeUnitType e on c.unitid=e.ID                                                                      
                                    where  a.OutNo='" + headid+"'";

                try
                {
                    dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(strQuery);


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("{");
                    sb.Append("data:");
                    sb.Append(JsonClass.DataTable2Json(dt));
                    sb.Append("}");

                    context.Response.Write(sb.ToString());
                    context.Response.End();

                }
                catch (Exception ex)
                {

                }  
            }
             
            
                
          
        }
    }
    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    ////数据源结构
    //public class DataSourceModel
    //{
    //    public string ID { get; set; }
    //    public string CustNo { get; set; }
    //    public string CustName { get; set; }
    //    public string CustNam { get; set; }
    //    public string CustType { get; set; }
    //    public string CustTypeName { get; set; }
    //    public string PYShort { get; set; }
    //    public string CreditGrade { get; set; }
    //    public string CreditGradeName { get; set; }
    //    public string Manager { get; set; }
    //    public string ManagerName { get; set; }
    //    public string AreaID { get; set; }
    //    public string AreaName { get; set; }
    //    public string Creator { get; set; }
    //    public string CreatorName { get; set; }
    //    public string CreateDate { get; set; }
    //    public string CustClass { get; set; }
    //    public string CustClassName { get; set; }
    //}

}