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
                "    inner join ContractDetails_Sale b on a.id=b.contractid"+
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
            else if (Action == "SearchInWareList")
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件
                string InNo = context.Request.Form["InNo"].ToString().Trim();  //入库单号
                string FromName = context.Request.Form["FromName"].ToString().Trim();   //供货方
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //申请开始时间
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //申请开始时间
                string Executor = context.Request.Form["Executor"].ToString().Trim();   //收货人
                string TransPortNo = context.Request.Form["TransPortNo"].ToString().Trim();   //调运单号
                string QTestNo = context.Request.Form["QTestNo"].ToString().Trim();   //质检单号
                
                
                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = " select a.id,a.inno,a.fromtype,a.frombillid,a.transportno, "+
                "    b.productid,b.productcount,c.ProviderID,e.custname as providername,CONVERT(varchar(100),c.createdate, 23)  as providerdate," +
                "    f.productname,isnull(d.ship_place,'') as StartStation,isnull(d.to_place,'') as EndStation,"+
                "    (case d.at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'"+
                "     when '50' then '到货' else '未生效' end) transstate,(case a.billstatus when '1' then '未确认' when '2' then '已确认' end ) as billstatus " +
                "      from  jt_cgrk a"+
                "    left join jt_cgrk_mx b on a.id=b.inno"+
                "    left join jt_cgdh c on a.frombillid=c.id"+
                "    left join jt_HuocheDiaoyun  d on a.TransPortNo=d.id_at "+
                "    left join officedba.providerinfo e on c.ProviderID=e.id "+
                "    left join officedba.ProductInfo f on b.ProductID=f.id"+
                "    left join officedba.EmployeeInfo g on a.Executor=g.id   "+
              
                 " where a.CompanyCD = '" + CompanyCD + "'";
                

                if (InNo != "")
                    strQuery += " and a.inno like '%" + InNo + "%'";
                if (FromName != "")
                    strQuery += " and e.custname like '%" + FromName + "%'";
                if (BeginT != "")
                    strQuery += " and a.EnterDate>='" + BeginT + "'";
                if (EndT != "")
                    strQuery += " and a.EnterDate<='" + EndT + "'";
                if (Executor != "")
                    strQuery += " and g.EmployeeName like '%" + Executor + "%'";
                if (TransPortNo != "")
                    strQuery += " and d.DiaoyunNO like '%" + TransPortNo + "%'";
                if (QTestNo != "")
                    strQuery += " and h.ReportNo like '%" + QTestNo + "%'";
                
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
            else if (Action == "SearchInWareInfo")//获取某个采购入库单详细信息
            {
                string ID = context.Request.Form["headid"].ToString().Trim();  //入库单ID
                

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id, a.InNo,a.FromBillID,b.ArriveNo as SourceBillNo,
                                    b.ProviderID,c.CustName as ProviderName,
                                    a.TransPortNo as TranSportID,g.DiaoyunNO as TranSportNo,
                                    a.Executor ,h.EmployeeName as PPerson,CONVERT(varchar(100),a.EnterDate, 23) as EnterDate,
                                    g.ship_place as StartStation,g.ship_quantity as SendNum,
                                    g.vehicle_quantity as CarNum,g.to_place as EndStation,
                                    
                                    b.ID as QTestID ,  b.ArriveNo  as QTestNo,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,                                    
                                    a.Creator as CreatorId,k.EmployeeName as CreatorName,
                                    a.Confirmor,l.EmployeeName as ConfirmorName,
                                    CONVERT(varchar(100),a.ModifiedDate, 23) as ModifiedDate,a.ModifiedUserID,a.billStatus,
                                    a.DeptID,m.DeptName,a.Remark,CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,a.CountTotal,(case g.at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'
                                     when '50' then '到货' else '未生效' end) transstate,g.at_state,g.motorcade

                                    from dbo.jt_cgrk a
                                    left join dbo.jt_cgdh b on b.ID=a.FromBillID 
                                    left join officedba.ProviderInfo c on c.id=b.ProviderID
                                    left join dbo.jt_HuocheDiaoyun g on g.id_at=a.TransPortNo
                                    left join officedba.EmployeeInfo h on a.Executor=h.id

                                    left join officedba.EmployeeInfo k on a.Creator=k.id
                                    left join officedba.EmployeeInfo l on a.Confirmor=l.id
                                    left join officedba.DeptInfo m on a.DeptID=m.id
                                    where a.id=" + ID + " and a.CompanyCD='" + CompanyCD + "'";
                //                                    left join dbo.jt_cgzj j on j.FromReportNo=b.id
                //j.ID as QTestID , j.ReportNo as QTestNo,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,
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
            else if (Action == "SearchInWareOneDetail")   //从列表进入查看界面，获取入库单的明细信息 
            {
                string headid = context.Request.Form["headid"].ToString();

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = @"select a.cgdhmx_autoid as id, a.StorageID,b.StorageName,a.ProductID,c.ProdNo,c.ProductName,
                                    cast(a.ProductCount as int) as ProCount,c.unitid,isnull(e.codename,'') as unitName,
                                    a.CarNum,
                                    d.InCount,isnull(d.productCount,0) as TotalNum,
                                    (isnull(d.productCount,0)-isnull(d.InCount,0)) as ProductCount

                                    from dbo.jt_cgrk_mx a  
                                    left join officedba.StorageInfo b on b.id=a.storageID
                                    left join officedba.ProductInfo c on c.id=a.ProductID 
                                    left join dbo.jt_cgdh_mx d on a.cgdhmx_autoid=d.id  
                                    left join officedba.CodeUnitType e on c.unitid=e.ID                                                                      
                                    where  a.InNo='" + headid + "'";

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