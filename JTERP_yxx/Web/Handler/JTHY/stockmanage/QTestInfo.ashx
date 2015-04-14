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
using System.Data.SqlClient;

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
            else if (Action == "SearchInBusList")
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = "  select  a.id,a.arriveno,providerid,c.custname as provider,productid,d.productname,"+
                "    productcount,isnull(totalmoney,0) totalmoney,ISNULL(e.ship_place,'') as startstation,"+
                "    ISNULL(e.to_place,'') as endstation,"+
                "    (case e.at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'"+
                "     when '50' then '到货' else '未生效' end) transstate "+
                "     from jt_cgdh a inner join jt_cgdh_mx b on a.id=b.arriveno "+
                "    left join officedba.ProviderInfo c on a.providerid=c.id "+
                "    left join officedba.ProductInfo d on b.productid=d.id "+
               "   left join jt_HuocheDiaoyun e on a.TransPortNo=e.id_at ";
               // dt = SqlHelper.ExecuteSql(strQuery);
               // TotalCount = dt.Rows.Count;
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
            else if (Action == "SearchInBusListToTest")
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = " select a.ID,a.ArriveNo,a.TransPortNo as diaoyunid,isnull(c.diaoyunno,'') diaoyunno,"+
                "    a.ProviderID,d.custname as providername,"+
                "    a.FromType,b.ProductID,e.productname,b.ProductCount,e.UnitID "+
                "    from jt_cgdh a  inner join jt_cgdh_mx b on a.id=b.ArriveNo "+
                "    left join jt_HuocheDiaoyun  c on a.TransPortNo=c.id_at "+
                "    left join officedba.providerinfo d on a.ProviderID=d.id"+
               "     left join officedba.ProductInfo e on b.ProductID=e.id ";
                // dt = SqlHelper.ExecuteSql(strQuery);
                // TotalCount = dt.Rows.Count;
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                string orderBy = " ID  desc  ";
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
            else if (Action == "SearchQTestList")  //检索质检单列表
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
                string ReportNo = context.Request.Form["ReportNo"].ToString().Trim();  //质检单号
                string OtherCorpName = context.Request.Form["OtherCorpName"].ToString().Trim();   //供货方
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //申请开始时间
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //申请开始时间
                string ProductName = context.Request.Form["ProductName"].ToString().Trim();   //煤种
                string StartPlace = context.Request.Form["StartPlace"].ToString().Trim();   //发站
                string InBusNo = context.Request.Form["InBusNo"].ToString().Trim();   //到货单编号
                
                
                int TotalCount = 0;

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery =
               "     SELECT  a.ID, a.ReportNo as reportno, ISNULL(b.CheckNum, 0) AS checknum," +
               "     ISNULL(a.CheckResult, '') AS checkresult, " +
               "     (CASE a.billstatus WHEN '1' THEN '未确认' WHEN '2' THEN '已确认' END) AS billstatus, " +
               "       b.CheckValue, b.CheckStandard, e.EmployeeName as employeeName ,f.employeeName as createorName " +
               "   FROM  jt_cgzj AS a LEFT OUTER JOIN " +
               "       jt_cgzj_mx AS b ON a.ID = b.ReportNo" +
                "    left join officedba.EmployeeInfo  e on a.checker=e.id   " +
                  "    left join officedba.EmployeeInfo  f on a.creator=f.id  " +
                 " where a.CompanyCD = '" + CompanyCD + "'";


                if (ReportNo != "")
                    strQuery += " and a.ReportNo like '%" + ReportNo + "%'";
                if (OtherCorpName != "")
                    strQuery += " and d.custname like '%" + OtherCorpName + "%'";
                if (BeginT != "")
                    strQuery += " and a.CheckDate>='" + BeginT + "'";
                if (EndT != "")
                    strQuery += " and a.CheckDate<='" + EndT + "'";
             
                
                
                // dt = SqlHelper.ExecuteSql(strQuery);
                // TotalCount = dt.Rows.Count;
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;

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
            else if (Action == "SearchQTestInfo") //查询质检单详细信息
            {
                string ID = context.Request.Form["headid"].ToString().Trim();  //入库单ID


                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id ,a.ReportNo,a.FromReportNo as SourceBillID,CONVERT(varchar(100),a.CheckDate, 23) as CheckDate,a.CheckMode,
                                     g.id as CoalID ,g.productname as CoalName,a.BillStatus, f.CheckNum,a.ChecDeptId,h.DeptName,a.Checker as PPersonID,i.EmployeeName as PPerson,
                                 
                                    f.CheckNum,a.ChecDeptId,h.DeptName,a.Checker as PPersonID,i.EmployeeName as PPerson,
                                        a.CheckResult,a.Remark,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,a.Creator,j.EmployeeName as CreatorName,
                                    a.Confirmor,k.EmployeeName as ConfirmorName,CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,CONVERT(varchar(100),a.ModifiedDate, 23) as ModifiedDate,
                                  a.ModifiedUserID



                                    from dbo.jt_cgzj a
                                
                                     left join jt_cgzj_mx f on f.reportno=a.id
                                    left join officedba.ProductInfo g on f.CoalType=g.id 
                                    left join officedba.DeptInfo h on h.id=a.ChecDeptId
                                    left join officedba.EmployeeInfo i on i.id=a.Checker
                                    left join officedba.EmployeeInfo j on j.id=a.Creator
                                    left join officedba.EmployeeInfo k on k.id=a.Confirmor
                                    where a.id=" + ID + " and a.CompanyCD='" + CompanyCD + "'";
           
                
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
            else if (Action == "SearchQTestDetail") //查询质检单明细
            {
                string ID = context.Request.Form["headid"].ToString().Trim();  //入库单ID


                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id, a.chexiangNo,a.CheckItem,a.CheckStandard,a.CheckValue
                                    from jt_cgzj_mx a
                                    where a.ReportNo=" + ID + " and a.CompanyCD='" + CompanyCD + "'";
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
            else if (Action == "Delete") //删除记录
            {
                string allid = context.Request.Params["AllId"].ToString().Trim(); //客户编号
                string[] ALLID1 = allid.Split(',');
                string[] ALLID2 = allid.Split(',');

                JsonClass jc;

                bool Issure = IsSure(ALLID1);  //判断是否已经确认
                if (Issure)
                {
                    jc = new JsonClass("faile", "", 2);
                }
                else
                {



                    if (DeleteDataByIds(ALLID2))
                        jc = new JsonClass("success", "", 1);
                    else
                        jc = new JsonClass("faile", "", 0);
                }


                context.Response.Write(jc);
                context.Response.End();
            }

             
                
          
        }
    }
    
    //删除记录
    private bool DeleteDataByIds(string[] ALLID)
    {
        ArrayList lstCmd = new ArrayList();

        
        bool bo = false;
        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
        for (int i = 0; i < ALLID.Length; i++)
        {
            ALLID[i] = "'" + ALLID[i] + "'";
            sb1.Append(ALLID[i]);
        }

        string allIDs = sb1.ToString().Replace("''", "','");
        string sql = "delete from jt_cgzj where id in (" + allIDs + ")";
        SqlCommand command= new SqlCommand(sql);
        lstCmd.Add(command);

        string sqlDtail = "delete from jt_cgzj_mx where ReportNo in (" + allIDs + ")";
        SqlCommand commandDetail = new SqlCommand(sqlDtail);
        lstCmd.Add(commandDetail);
        
        try
        {
            bo=SqlHelper.ExecuteTransWithArrayList(lstCmd);
        }
        catch (Exception ex)
        {
            bo = false;
        }
        return bo;
    }

    //判断质检单是否已经确认
    private bool IsSure(string[] ALLID)
    {
        bool bo = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < ALLID.Length; i++)
        {
            ALLID[i] = "'" + ALLID[i] + "'";
            sb.Append(ALLID[i]);
        }

        string allIDs = sb.ToString().Replace("''", "','");
        allIDs = allIDs.Replace("'", "");
        string[] ExpIDs = allIDs.Split(',');
        for (int i = 0; i < allIDs.Length; i++)
        {
            string sql = "select BillStatus from jt_cgzj where id=" + allIDs[0];
            object o = XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql, null);
            if (Convert.ToInt32(o) == 2)
            {
                bo = true;
                return bo;
            }


        }
        return bo;


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