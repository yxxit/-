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

            if (Action == "SearchTransPort")
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

                string strQuery = " select isnull(a.diaoyunno,'') as transportid,a.id_at as id,(case at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
               "  when '50' then '到货' else '制单' end ) as states,a.ship_time as StartDate," +
                " a.ship_place as StartStation,to_place as ArriveStation,a.motorcade as CarNo,a.billStatus," +
                " a.trans_type as TransPortType,a.ship_quantity as SendNum,a.vehicle_quantity as CarNum,"+
                " a.num_first_vehicle as StartCarCode,a.num_last_vehicle as EndCarCode,"+
                " convert(varchar(12), isnull(a.ModifiedDate,''),23) ModifiedDate,isnull(a.ModifiedUserID,'') ModifiedUserID,"+
                " isnull(d.EmployeeName,'') as ModifiedName,"+
                " a.Jh_ReceMan,a.Ss_ReceMan,a.Ss_quan,a.Ss_VeQuan,a.Jh_place,a.Remark," +
                " a.Creator,c.EmployeeName as CreatorName,convert(varchar(12), CreateDate,23) CreateDate,"+
                " isnull(PPersonID,'') PPersonID,isnull(e.EmployeeName,'') as PPersonName,  at_department as DeptID,isnull(DeptName,'') DeptName," +
                " b.id_at as detailsid,b.num_vehicle as CarCode,b.gw as GrossWeight,b.pw as TareWeight,"+
                " b.nw as NetWeight,b.sumnum as SumWeight"+
                " from  jt_HuocheDiaoyun a "+
                " left join  jt_HuocheDiaoyun_mx b on a.id_at=b.id_at_state "+
                " left join officedba.employeeinfo c on a.Creator=c.id "+
                " left join officedba.employeeinfo d on a.ModifiedUserID=d.id "+
                " left join officedba.employeeinfo e on a.PPersonID=e.id " +
                " left join officedba.DeptInfo g on a.at_department=g.id"+
                
               "    where a.companycd='" + CompanyCD + "'  and a.Ship_type='1' and a.id_at='" + headid + "' order by a.id_at desc,b.id_at ";
                dt = SqlHelper.ExecuteSql(strQuery);
                TotalCount = dt.Rows.Count;
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
            else if (Action == "SearchTransPortList")
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
                int TotalCount = 0;
                //检索条件
                string motorcade = context.Request.Form["motorcade"].ToString().Trim();  //车次
                string ship_place = context.Request.Form["ship_place"].ToString().Trim();  //发站
                string to_place = context.Request.Form["to_place"].ToString().Trim();  //到站
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //发运开始时间
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //发运结束时间

               // BeginT= String.Format("yyyy-MM-dd",BeginT);

               // EndT = Convert.ToDateTime(EndT).ToString("YYYY-MM-DD");
                
                
                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = "select  distinct isnull(diaoyunno,'') as transportid,a.id_at as id,convert(varchar(12),ship_time,23) as StartDate,motorcade as CarNo,"+
                "ship_place as StartStation,to_place as ArriveStation,(case a.billstatus when '2' then '已确认' else '未确认' end) as billStatus," +
               " ship_quantity as SendNum,convert(varchar(12),CreateDate,23) CreateDate,(case at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
               "   when '50' then '到货' else '制单' end) transstate,isnull(at_state,10) transtatevalue  from  jt_HuocheDiaoyun a " +
               "  left join  jt_HuocheDiaoyun_mx b on a.id_at=b.id_at_state  ";

                strQuery += " WHERE  (a.CompanyCD = '" + CompanyCD + "')";
               
                if (motorcade != "")
                    strQuery += " and a.motorcade like '%" + motorcade + "%'";
                
                if (ship_place != "")
                    strQuery += " and a.ship_place like '%" + ship_place + "%'";
                
                if (to_place != "")
                    strQuery += " and a.to_place like '%" + to_place + "%'";
                
                if (BeginT != "")
                    strQuery += " and convert(varchar(12),a.ship_time,23)>='" + BeginT + "'";
               
                if (EndT != "")
                    strQuery += " and convert(varchar(12),a.ship_time,23)<='" + EndT + "'";
                
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
            else if (Action == "uc_SearchTransPortList")  //自定义控件，调用调运单列表
            {
                // string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string orderString = context.Request.Form["OrderByUc_TranSport"].ToString();//排序
                string order = "desc";//排序：降序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "asc";//排序：升序
                }
                string ord = orderBy + " " + order;
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;

                //检索条件
                string TranSportNo = context.Request.Form["TranSportNo"].ToString().Trim();  //调运单号
                string motorcade = context.Request.Form["motorcade"].ToString().Trim();  //车次
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //发运开始时间
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //发运结束时间
                int iscount = Convert.ToInt32(context.Request.Form["iscount"].ToString());
                string StartStation = context.Request.Form["StartStation"].ToString().Trim();  //调运单号
                string ArriveStation = context.Request.Form["ArriveStation"].ToString().Trim();  //调运单号

                context.Response.ContentType = "text/plain";
                DataTable dt = new DataTable();
                string strQuery = "  select  distinct isnull(diaoyunno,'') as transportid,a.id_at as id,convert(varchar(12),ship_time,23) as StartDate,motorcade as CarNo," +
                                   " ship_place as StartStation,to_place as ArriveStation,a.vehicle_quantity as carNum," +
                                   " ship_quantity as SendNum,convert(varchar(12),CreateDate,23) CreateDate,(case at_state when 10 then '未生效'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
                                   " when '50' then '到货' else '制单' end) transstate,isnull(at_state,10) transtatevalue,a.Ss_quan as GetNum" + 
                                   " from  jt_HuocheDiaoyun a " +
                                   " left join  jt_HuocheDiaoyun_mx b on a.id_at=b.id_at_state  ";

                strQuery += " WHERE  (a.CompanyCD = '" + CompanyCD + "')";
                if (iscount == 0)
                    strQuery += "and a.Ss_quan<a.ship_quantity";
                
                if (TranSportNo != "")
                    strQuery += " and a.diaoyunno like '%" + TranSportNo + "%'";

                 //发站
                if (StartStation != "")
                    strQuery += " and ship_place like '%" + StartStation + "%'";
                //到站
                if (ArriveStation != "")
                    strQuery += " and to_place like '%" + ArriveStation + "%'";

                if (motorcade != "")
                    strQuery += " and a.motorcade like '%" + motorcade + "%'";

                if (BeginT != "" && BeginT != "undefined")
                    strQuery += " and convert(varchar(12),a.ship_time,23)>='" + BeginT + "'";
                if (EndT != "" && BeginT != "undefined")
                    strQuery += " and convert(varchar(12),a.ship_time,23)<='" + EndT + "'";
                
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