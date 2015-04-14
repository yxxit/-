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
                SearchSellContract(context, User, CompanyCD);

            }
            else if (Action == "SearchInBusList")  //检索采购到货列表
            {
                SearchInBusList(context, User, CompanyCD);
            }
            else if (Action == "SearchInBusListToTest")//质检单参照到货单
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;

                //检索条件
                string InBusNo = context.Request.Form["InBusNo"].ToString().Trim();  //到货单编号
                string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供应商
                string ColName = context.Request.Form["ColName"].ToString().Trim();   //煤种


                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = " select a.ID,a.ArriveNo,a.TransPortNo as diaoyunid,isnull(c.diaoyunno,'') diaoyunno," +
                "    a.ProviderID,d.custname as providername,isnull(a.TotalFee,0) as TotalFee,isnull(a.ProJsFee,0) as ProJsFee," +
                "    a.FromType,b.ProductID,e.productname,b.ProductCount,e.UnitID " +
                "    from jt_cgdh a  inner join jt_cgdh_mx b on a.id=b.ArriveNo " +
                "    left join jt_HuocheDiaoyun  c on a.TransPortNo=c.id_at " +
                "    left join officedba.providerinfo d on a.ProviderID=d.id" +
               "     left join officedba.ProductInfo e on b.ProductID=e.id " +

                 "  where a.billstatus='2'and  a.CompanyCD = '" + CompanyCD + "'";
                if (InBusNo != "")
                    strQuery += " and a.ArriveNo like '%" + InBusNo + "%'";
                if (ProviderName != "")
                    strQuery += " and d.custname like '%" + ProviderName + "%'";
                if (ColName != "")
                    strQuery += " and e.productname like '%" + ColName + "%'";

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
            else if (Action == "SearchInBusListToInWare")//入库单参照到货单(自定义控件InWareInfo)
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;

                //检索条件
                string InBusNo = context.Request.Form["InBusNo"].ToString().Trim();  //到货单编号
                string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供应商
                string ColName = context.Request.Form["ColName"].ToString().Trim();   //煤种


                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = "      select a.ID,a.ArriveNo,a.TransPortNo as diaoyunid,isnull(c.diaoyunno,'') diaoyunno, " +
                "   a.ProviderID,d.custname as providername, " +
                "   a.FromType,b.ProductID,e.productname,b.ProductCount," +
                "   isnull(c.ship_quantity,0) as SendNum," +
                "   c.ship_place ,c.to_place,c.motorcade as CarNo,c.vehicle_quantity as CarNum," +
                "   (case c.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
                "     when '50' then '到货' else '制单' end) transstate, c.at_state," +
                    //"   ISNULL(g.ID,0) AS ZJID,ISNULL(G.ReportNo,'') AS ZJNO,a.Purchaser as ReceivePersonID"+  // liuch 20140624
                "   ISNULL(a.ID,0) AS ZJID,ISNULL(a.TransPortNo,'') AS ZJNO,a.Purchaser as ReceivePersonID" +
                "   from jt_cgdh a  inner join jt_cgdh_mx b on a.id=b.ArriveNo  " +
                "   left join jt_HuocheDiaoyun  c on a.TransPortNo=c.id_at   " +
                "   left join officedba.providerinfo d on a.ProviderID=d.id  " +
                "   left join officedba.ProductInfo e on b.ProductID=e.id " +
                    //"   left join (select * from jt_cgzj where billstatus='2') g on a.id=g.FromReportNo "+ // liuch 20140624
                "   where 1=1 and a.billstatus='2' and a.CompanyCD = '" + CompanyCD + "'";

                if (InBusNo != "")
                    strQuery += " and a.ArriveNo like '%" + InBusNo + "%'";
                if (ProviderName != "")
                    strQuery += " and d.custname like '%" + ProviderName + "%'";
                if (ColName != "")
                    strQuery += " and e.productname like '%" + ColName + "%'";

                // strQuery += " WHERE  (a.CompanyCD = '" + CompanyCD + "')";

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
            else if (Action == "uc_SearchInBusDetail")  //获取采购到货明细的信息，（自定义控件InWareInfo）
            {
                string headid = context.Request.Form["headid"].ToString();
                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id, a.StorageID,a.ProductID,c.CodeName as unitName,
                                    '0' as ProCount,a.ProductCount as TotalNum,'0' as CarNum,
                                    (isnull(a.ProductCount,0)-isnull(a.InCount,0)) as ProductCount


                                    from dbo.jt_cgdh_mx a
                                    left join officedba.ProductInfo b on b.id=a.ProductID
                                    left join officedba.CodeUnitType c on c.id=b.unitID 
                                    where a.ArriveNo=" + headid;

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
            else if (Action == "SearchInBusOne")//从列表查询某个采购到货单的信息
            {
                string headid = context.Request.Form["headid"].ToString();
                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id, a.ArriveNo,a.SourceBillID,b.Contractid as  SourceBillNo,
                                    a.PayType,a.providerid,c.custname as providerName,a.billStatus, 
                                    c.ContactName,a.OtherTotal as Freight,CONVERT(varchar(100),a.SendTime, 23) as SendTime,
                                    Cast(a.SendNum As Int) as SendNum,a.TotalFee,a.Purchaser as PPersonID,
                                    e.EmployeeName as PPerson,a.DeptID,f.DeptName,a.CarryType as transporttype,a.TransPortNo,g.DiaoyunNO,
                                    (case g.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'
                                    when '50' then '到货' else '制单' end) transstate,
                                     g.motorCade,g.ship_place , g.to_place,Cast(a.CarNum As Int) as CarNum,
                                    g.at_state , CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,a.Creator,h.EmployeeName as CreatorName,
                                    a.Confirmor,i.EmployeeName as ConfirmorName,CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,
                                    a.ModifiedUserID,CONVERT(varchar(100),a.ModifiedDate, 23) as ModifiedDate, j.CustName AS ServicesName, j.ID AS ServicesId
                                    ,g.Ss_quan as GetNum
                                    from dbo.jt_cgdh a
                                    left join dbo.ContractHead_Pur b on b.id=a.SourceBillID
                                    left join officedba.ProviderInfo c on c.id=a.providerid
                                    left join officedba.EmployeeInfo e on e.id=a.Purchaser
                                    left join officedba.DeptInfo f on f.id=a.DeptID
                                    left join jt_HuocheDiaoyun g on g.id_at=a.TransPortNo
                                    left join officedba.EmployeeInfo h on h.id=a.Creator
                                    left join officedba.EmployeeInfo i on i.id=a.Confirmor
                                    left join officedba.ProviderInfo AS j ON j.ID = a.ServicesID                                    
                                    where a.id=" + headid + " and a.CompanyCD='" + CompanyCD + "' ";

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
            else if (Action == "SearchInBusDetail")//从列表查询某个采购到货单的煤种信息
            {
                string headid = context.Request.Form["headid"].ToString();
                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = @"select a.id,a.StorageID,b.storageName,a.ProductID,
                                    c.ProductName,c.Specification,c.HeatPower,c.UnitID,d.CodeName as UnitName,
                                    a.ProductCount,a.TaxPrice,a.TotalFee,a.TaxRate,a.ISQTest,
                                    isnull(a.CheckedCount,0.00) as CheckedCount,isnull(a.InCount,0.00) as InCount


                                    from dbo.jt_cgdh_mx a
                                    left join officedba.StorageInfo b on b.id=a.StorageID
                                    left join officedba.ProductInfo c on c.id=a.ProductID
                                    left join officedba.CodeUnitType d on d.id=c.UnitID
                                    where a.ArriveNo=" + headid + "  ";

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

    // 到货单列表查询
    private void SearchInBusList(HttpContext context, int User, string CompanyCD)
    {
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        //检索条件
        string ArriveNo = context.Request.Form["ArriveNo"].ToString().Trim();  //单据编号
        string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供货方
        string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //发货开始时间
        string EndT = context.Request.Form["EndT"].ToString().Trim();   //发货结束时间
        string PurchaserName = context.Request.Form["PurchaserName"].ToString().Trim();   //采购员
        string TranSNo = context.Request.Form["TranSNo"].ToString().Trim();   //调运单号
        string State = context.Request.Form["State"].ToString().Trim();   //运送状态
        string strSqlCondtion = "";
        string PurContractNo = context.Request.Form["PurContractNo"].ToString().Trim();
        //string TranSNo = context.Request.Form["TranSNo"].ToString().Trim();
        string DeptID = context.Request.Form["DeptID"].ToString().Trim();
        
        
        StringBuilder sb = new StringBuilder();

        int TotalCount = 0;

        context.Response.ContentType = "text/plain";
        
          

        DataTable dt = new DataTable();
        string strQuery = "  select  a.id,a.arriveno,providerid,c.custname as provider,productid,d.productname," +
        " productcount as productcount,Taxprice as TaxPrice,isnull(totalmoney,0) totalmoney,ISNULL(e.ship_place,'') as startstation," +
        " ISNULL(e.to_place,'') as endstation," +
        " (case e.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
        " when '50' then '到货' else '制单' end) transstate,(case a.billstatus when '1' then '未确认' else '已确认' end) billstatus,  " +
        " i.DeptNO as DeptNO,i.ID as DeptID,i.DeptName,  h.DiaoyunNO,h.id_at as DiaoyunId,g.id as CtrId,g.Contractid  "+
        " from jt_cgdh a left join jt_cgdh_mx b on a.id=b.arriveno " +
        " left join officedba.ProviderInfo c on a.providerid=c.id " +
        " left join officedba.ProductInfo d on b.productid=d.id " +
        " left join jt_HuocheDiaoyun e on a.TransPortNo=e.id_at " +
        " left join officedba.EmployeeInfo f on f.id=a.Purchaser " +
        " left join contractHead_Pur g on g.id=a.SourceBillID "+
        " left join jt_HuocheDiaoyun h on h.id_at=a.TransPortNo "+
        " left join officedba.DeptInfo i on i.id=a.DeptId "+
        " where a.CompanyCD = '" + CompanyCD + "'";

        if (ArriveNo != "")
            strSqlCondtion += " and a.ArriveNo like '%" + ArriveNo + "%'";
        if (ProviderName != "")
            strSqlCondtion += " and c.custname like '%" + ProviderName + "%'";
        if (BeginT != "")
            strSqlCondtion += " and a.SendTime>='" + BeginT + "'";
        if (EndT != "")
            strSqlCondtion += " and a.SendTime<='" + EndT + "'";
        if (PurchaserName != "")
            strSqlCondtion += " and f.EmployeeName like '%" + PurchaserName + "%'";
        if (TranSNo != "")
            strSqlCondtion += " and e.diaoyunNo like '%" + TranSNo + "%'";
        if (State != "")
            strSqlCondtion += " and (case e.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
        "     when '50' then '到货' else '制单' end) like '%" + State + "%'";

        if (PurContractNo != "")
        { strSqlCondtion += " and g.Contractid like'%" + PurContractNo + "%'"; }       
        if (DeptID != "")
        { strSqlCondtion += " and i.ID ='" + DeptID + "'"; }
        
        // 权限限制代码        
        XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
        DataTable dtt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
        if (dtt != null && dtt.Rows.Count > 0)
        {
            if (dtt.Rows[0]["RoleRange"].ToString() == "1")
            {
                strSqlCondtion += " and (a.Creator IN  ";
                strSqlCondtion += "( SELECT ID FROM  officedba.EmployeeInfo ";
                strSqlCondtion += "  WHERE DeptID IN (SELECT a.ID  ";
                strSqlCondtion += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                strSqlCondtion += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) ";
            }
            if (dtt.Rows[0]["RoleRange"].ToString() == "2")
            {
                strSqlCondtion += " and (a.Creator IN  ";
                strSqlCondtion += " (SELECT ID FROM  officedba.EmployeeInfo ";
                strSqlCondtion += "  WHERE DeptID IN (SELECT a.ID  ";
                strSqlCondtion += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                strSqlCondtion += "  WHERE a.ID=b.ID) ))   ";
            }
            if (dtt.Rows[0]["RoleRange"].ToString() == "3")
            {
                strSqlCondtion += " and (a.Creator IN  ";
                strSqlCondtion += "( SELECT ID FROM  officedba.EmployeeInfo ";
                strSqlCondtion += "  WHERE DeptID IN (SELECT a.ID  ";
                strSqlCondtion += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                strSqlCondtion += "  WHERE a.ID=b.ID)))   ";
            }
        } 
        // end 权限限制 结束
        
        strQuery += strSqlCondtion;
        
        // 求数量及金额总和
        DataTable dtTtl = new DataTable();      
        string strQueryTtl = "SELECT   SUM(ProductCount) AS ttlCount, SUM(totalmoney) AS ttlFee FROM (" + strQuery +
                 ") AS tempTable";
        
        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
        comm.CommandText = strQuery;
        string orderBy = " id  desc  ";
        dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

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
            dtTtl = SqlHelper.ExecuteSql(strQueryTtl);
            if (dtTtl.Rows.Count > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append(",");
                sb.Append("ttlCount:");
                sb.Append(dtTtl.Rows[0][0].ToString());
                sb.Append(",");
                sb.Append("ttlFee:");
                sb.Append(dtTtl.Rows[0][1].ToString());
                sb.Append("}");
            }
        }
        context.Response.Write(sb.ToString());
        context.Response.End();
    }

    // 
    private void SearchSellContract(HttpContext context, int user, string CompanyCD)
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
       "      isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
        "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
        "    a.TransPortType,h.typename as TransPortTypeName," +
        "     a.ContractMoney,c.productname,c.unitid,d.codename as unitname,isnull(c.specification,'') specification," +
        "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus," +
        "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
        "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney " +
        "    from ContractHead_Sale a" +
        "    inner join ContractDetails_Sale b on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    where a.companycd='" + CompanyCD + "' and a.cVouchType='1' and a.BillType='1' and a.id='" + headid + "' order by a.id desc,b.autoid ";
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
                sb.Remove(sb.Length - 1, 1);
                sb.Append(",");
                sb.Append("dataAttach:");
                sb.Append(JsonClass.DataTable2Json(dtAttach));
                sb.Append("}");
            }

        }
        context.Response.Write(sb.ToString());
        context.Response.End();

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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}