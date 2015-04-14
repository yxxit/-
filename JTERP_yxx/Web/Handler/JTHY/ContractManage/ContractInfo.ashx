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
            else if (Action == "uc_SellContractDetail")  //查询某个销售发货单明细信息
            {
                uc_SellContractDetail(context, User, CompanyCD);                
            }
            else if (Action == "SearchSellContractList")  // 查询销售合同列表
            {
                SearchSellContractList(context, User, CompanyCD);                 
            }
            else if (Action == "uc_SearchSellContractList")
            {
                uc_SearchSellContractList(context, User, CompanyCD); 
            }
            else if (Action == "SearchPurContract")
            {
                SearchPurContract(context, User, CompanyCD); 
            }
            else if (Action == "SearchPurContractList")    // 查询采购合同列表
            {
                SearchPurContractList(context, User, CompanyCD); 
            }
            else if (Action == "SearchPurContractFromUserControl") //从自定义控件采购合同
            {
                string ord = " id desc";
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                int TotalCount = 0;
                //检索条件
                string OrderNo = context.Request.Form["PurContractId"].ToString().Trim();  //合同号

                string ProviderName = context.Request.Form["PurContractName"].ToString().Trim();   //供应商名称
                context.Response.ContentType = "text/plain";
                DataTable dt = new DataTable();
                string strQuery = "select distinct  a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode as providerid,a.cCusCode," +
                "   isnull(f.custname,'') as providerName,f.contactname as linkman, j.employeename as managerName,j.id as managerid,k.deptName, a.deptid, " +
              "      isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
               "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
               "    a.TransPortType,h.typename as TransPortTypeName," +
               "     a.ContractMoney," +
               "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus,a.billstatus as billstatusid," +
               "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark " +
               "     ,m.iQuantity as ContactSum" +
                "    from ContractHead_Pur a" +
                "    inner join ContractDetails_Pur b on a.id=b.contractid" +
                "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
                "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
                "    left join officedba.CustInfo e on a.cCusCode=e.id " +
                "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
                "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
                "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
                "    left join officedba.EmployeeInfo i on a.creator=i.id" +
                "    left join officedba.EmployeeInfo j on a.PPersonID=j.id" +
                "    left join officedba.DeptInfo k on a.deptid=k.id" +
                "    left join (select sum(iQuantity) as iQuantity,b.contractid as id from ContractDetails_Pur b group by b.contractid) m on m.id=a.id" +
                "    where a.companycd='" + CompanyCD + "' and a.cVouchType='2' and a.BillType='2' and a.billstatus='2' ";

                if (OrderNo != "")
                    strQuery += " and a.Contractid like '%" + OrderNo + "%'";
                if (ProviderName != "")
                    strQuery += " and f.custname like '%" + ProviderName + "%'";

                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
                // TotalCount = dt.Rows.Count;
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
            else if (Action == "SearchPurContract_Zhixiao") // 采购直销获取物品价格
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                string headid = context.Request.Form["headid"].ToString();
                int TotalCount = 0;

                ////获取数据
                context.Response.ContentType = "text/plain";
                DataTable dt = new DataTable();
                string strQuery ="SELECT     cInvCCode, cCounitCode, iQuantity, iUnitCost, iMoney " +
                                 "FROM   ContractDetails_Pur " +
                                 "where ContractID="+headid+" " ;
                
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
                {
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));                   
                }
                context.Response.Write(sb.ToString());
                context.Response.End();
            }          
        }
    }

    private void SearchPurContract(HttpContext context, int User, string CompanyCD)
    {
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        string headid = context.Request.Form["headid"].ToString();
        int TotalCount = 0;

        ////获取数据
        context.Response.ContentType = "text/plain";
        DataTable dt = new DataTable();
        string strQuery = "select  a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
       "      isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
        "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
        "    a.TransPortType,h.typename as TransPortTypeName," +
        "     a.ContractMoney,c.productname,c.storageid,c.unitid,d.codename as unitname,isnull(c.specification,'') specification,isnull(c.HeatPower,'') HeatPower," +
        "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus,a.billstatus as billstatusid," +
        "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
        "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney, " +
        "isnull(a.PPersonID,0) PPersonID,isnull(j.employeename,'') as PPerson,isnull(a.DeptID,0) DeptID,isnull(k.deptname,'') DeptName " +
        ",ISNULL(m.EmployeeName, '') AS Confirmor,ISNULL(n.EmployeeName, '') AS ModifiedUserID" +
        ",REPLACE(CONVERT(varchar(12), a.ConfirmDate, 23), '1900-01-01', '') AS ConfirmDate" +
        ",REPLACE(CONVERT(varchar(12), a.ModifiedDate, 23), '1900-01-01', '') AS ModifiedDate" +

        "    from ContractHead_Pur a" +
        "    inner join ContractDetails_Pur b on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    left join officedba.EmployeeInfo j on a.PPersonID=j.id" +
        "    left join officedba.EmployeeInfo m on a.Confirmor = m.id" +
        "    left join officedba.EmployeeInfo n on a.ModifiedUserID = n.id" +
        "    left join officedba.DeptInfo k on a.DeptID=k.id" +
        "    where a.companycd='" + CompanyCD + "' and a.cVouchType='2' and a.BillType='2' and a.id='" + headid + "'   order by a.id desc,b.autoid   ";
        dt = SqlHelper.ExecuteSql(strQuery);
        TotalCount = dt.Rows.Count;
        string strAnn = "select a.ParentId as InqNo,a.annFileName as AnnFileName,a.AnnAddr,a.upDatetime as UpDateTime,a.annRemark as AnnRemark from officedba.Annex a" +
            " where a.CompanyCD='" + CompanyCD + "'  and ModuleType='" + ConstUtil.MODULE_ID_PurchaseContract_Add + "' and a.ParentId='" + headid + "'";

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
          
    // 查询采购合同列表
    private void SearchPurContractList(HttpContext context, int User, string CompanyCD)
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
        string OrderNo = context.Request.Form["OrderNo"].ToString().Trim();  //合同号
        string ProductName = context.Request.Form["ProductName"].ToString().Trim();  //煤种
        string Specification = context.Request.Form["Specification"].ToString().Trim();  //质量(热卡)
        string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //创建开始时间
        string EndT = context.Request.Form["EndT"].ToString().Trim();   //创建结束时间
        string BillStatus = context.Request.Form["BillStatus"].ToString().Trim();   //合同状态
        string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供应商名称

        context.Response.ContentType = "text/plain";
        DataTable dt = new DataTable();
        string strQuery = "select a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
        "   isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
        "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
        "   a.TransPortType,h.typename as TransPortTypeName," +
        "   a.ContractMoney," +
        "   (Case  a.billstatus when 1 then '制单' when 2 then '已生效' when 3 then '审核' when 9 then '关闭' else '其他' end) billstatus,a.billstatus as billstatusid," +
        "   a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark, " +
        "   b.iQuantity as ProductCount , b.iMoney as totalmoney" +
        "    from ContractDetails_Pur b " +
        "    left join ContractHead_Pur a on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    where a.companycd='" + CompanyCD + "' and a.cVouchType='2' and a.BillType='2' ";

        if (OrderNo != "")
            strQuery += " and a.Contractid like '%" + OrderNo + "%'";
        if (ProductName != "")
            strQuery += " and c.ProductName like '%" + ProductName + "%'";
        if (Specification != "")
            strQuery += " and b.quals like '%" + Specification + "%'";
        if (BeginT != "")
            strQuery += " and convert(varchar(12),a.createdate,23)>='" + BeginT + "'";
        if (EndT != "")
            strQuery += " and convert(varchar(12),a.createdate,23)<='" + EndT + "'";
        if (BillStatus != "0")
            strQuery += " and a.billstatus ='" + BillStatus + "'";
        if (ProviderName != "")
            strQuery += " and f.custname like '%" + ProviderName + "%'";

        DataTable dtTtl = new DataTable();
        string strQueryTtl = "SELECT   SUM(ProductCount) AS ttlCount, SUM(totalmoney) AS ttlFee FROM (" + strQuery +
                 ") AS tempTable";

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
            // sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
        context.Response.Write(sb.ToString());
        context.Response.End();
        
    }
    private void uc_SearchSellContractList(HttpContext context, int User, string CompanyCD)
    {
        string orderString = context.Request.Form["OrderByUc_SelContract"].ToString();//排序
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
        string SelContractNo = context.Request.Form["SelContractNo"].ToString().Trim();  //合同号
        string SelCustName = context.Request.Form["SelCustName"].ToString().Trim();  //煤种

        context.Response.ContentType = "text/plain";
        DataTable dt = new DataTable();
        string strQuery = "select distinct  a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
       "      isnull(e.custname,'') custname,isnull(e.billunit,'') billunit,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
        "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
        "    a.TransPortType,h.typename as TransPortTypeName, c.storageId, l.storageName ," +
        "     a.ContractMoney,a.PPersonID as ManagerId,j.employeename as ManagerName,a.deptId,k.deptName," +
        "    (Case  a.billstatus when 1 then '制单' when 2 then '已生效' when 3 then '审核' else '其他' end) billstatus,a.billstatus as billstatusid," +
        "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark " +
        "     ,m.iQuantity as ContactSum" +
        "    from ContractHead_Sale a" +
        "    inner join ContractDetails_Sale b on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    left join officedba.EmployeeInfo j on a.PPersonID=j.id" +   //业务员id
        "    left join officedba.DeptInfo k on k.id=a.deptid" +   //部门id
        "    left join officedba.StorageInfo l on l.id=c.storageId" +   //仓库id
        "    left join (select sum(iQuantity) as iQuantity,b.contractid as id from ContractDetails_Sale b group by b.contractid) m on m.id=a.id" +
        "    where a.companycd='" + CompanyCD + "' and a.cVouchType='1' and a.BillType='1' and a.billstatus='2'   ";
        
        if (SelContractNo != "")
            strQuery += " and a.Contractid like '%" + SelContractNo + "%'";
        if (SelCustName != "")
            strQuery += " and e.custname like '%" + SelCustName + "%'";

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

    // 查询销售合同列表
    private void SearchSellContractList(HttpContext context, int User, string CompanyCD)
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
        string OrderNo = context.Request.Form["OrderNo"].ToString().Trim();  //合同号
        string ProductName = context.Request.Form["ProductName"].ToString().Trim();  //煤种
        string Specification = context.Request.Form["Specification"].ToString().Trim();  //质量(热卡)
        string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //创建开始时间
        string EndT = context.Request.Form["EndT"].ToString().Trim();   //创建结束时间
        string BillStatus = context.Request.Form["BillStatus"].ToString().Trim();   //合同状态
        string CustName = context.Request.Form["CustName"].ToString().Trim();   //客户名称

        context.Response.ContentType = "text/plain";
        DataTable dt = new DataTable();
        string strQuery = "select a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
        "   isnull(e.custname,'') custname,isnull(e.billunit,'') billunit,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
        "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
        "   a.TransPortType,h.typename as TransPortTypeName," +
        "   a.ContractMoney," +
        "   (Case  a.billstatus when 1 then '制单' when 2 then '已生效' when 3 then '审核' when 9 then '关闭' else '其他' end) billstatus,a.billstatus as billstatusid," +
        "   a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark, " +
        "   b.iQuantity as ProductCount, b.iMoney as totalmoney" +
        "    from  ContractDetails_Sale b " +
        "    left join ContractHead_Sale a on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    where a.companycd='" + CompanyCD + "' and a.cVouchType='1' and a.BillType='1'   ";

        if (OrderNo != "")
            strQuery += " and a.Contractid like '%" + OrderNo + "%'";
        if (ProductName != "")
            strQuery += " and c.ProductName like '%" + ProductName + "%'";
        if (Specification != "")
            strQuery += " and b.quals like '%" + Specification + "%'";
        if (BeginT != "")
            strQuery += " and convert(varchar(12),a.ConfirmDate,23)>='" + BeginT + "'";
        if (EndT != "")
            strQuery += " and convert(varchar(12),a.ConfirmDate,23)<='" + EndT + "'";
        if (BillStatus != "0")
            strQuery += " and a.billstatus= '" + BillStatus + "'";
        if (CustName != "")
            strQuery += " and e.custname like '%" + CustName + "%'";

        DataTable dtTtl = new DataTable();
        string strQueryTtl = "SELECT   SUM(ProductCount) AS ttlCount, SUM(totalmoney) AS ttlFee FROM (" + strQuery +
                 ") AS tempTable";        

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
           //  sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
        context.Response.Write(sb.ToString());
        context.Response.End();        
    }

    // 
    private void uc_SellContractDetail(HttpContext context, int User, string CompanyCD)
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
        string strQuery = "select  a.id, " +
        "    c.StorageID,c.ProdNo,c.ProductName,c.unitID,isnull(d.codeName,'') as unitName, " +
        " '' as InCost,'' as TotalInCost,'17' as TaxRate,'' as TotalTax, '0.00' as OutCount,'0.00' as SttlCount, " +
        "    b.cinvccode as ProductID,b.iquantity as ProductCount ,b.iunitcost as TaxPrice,b.imoney as TotalFee " +
        "    from ContractHead_Sale a" +
        "    inner join ContractDetails_Sale b on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on d.id=c.unitID" +
        "    where a.companycd='" + CompanyCD + "' and a.id='" + headid + "' order by a.id desc,b.autoid ";
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
        {
            sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
        }
        context.Response.Write(sb.ToString());
        context.Response.End();       
    }
    // 
    private void SearchSellContract(HttpContext context, int User, string CompanyCD)
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
        "    a.TransPortType,h.typename as TransPortTypeName,c.storageId, l.storageName ,c.prodno," +
        "     a.ContractMoney,c.productname,c.unitid,d.codename as unitname,isnull(c.specification,'') specification," +
        "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus,a.billstatus as billstatusid," +
        "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
        "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney," +
        "isnull(a.PPersonID,0) PPersonID,isnull(j.employeename,'') as PPerson,isnull(a.DeptID,0) DeptID,isnull(k.deptname,'') DeptName " +

        ",ISNULL(m.EmployeeName, '') AS Confirmor,ISNULL(n.EmployeeName, '') AS ModifiedUserID" +
        ",REPLACE(CONVERT(varchar(12), a.ConfirmDate, 23), '1900-01-01', '') AS ConfirmDate" +
        ",REPLACE(CONVERT(varchar(12), a.ModifiedDate, 23), '1900-01-01', '') AS ModifiedDate" +
        "    from ContractHead_Sale a" +
        "    inner join ContractDetails_Sale b on a.id=b.contractid" +
        "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
        "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
        "    left join officedba.CustInfo e on a.cCusCode=e.id " +
        "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
        "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
        "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
        "    left join officedba.EmployeeInfo i on a.creator=i.id" +
        "    left join officedba.EmployeeInfo j on a.PPersonID=j.id" +
        "    left join officedba.EmployeeInfo m on a.Confirmor = m.id" +
        "    left join officedba.EmployeeInfo n on a.ModifiedUserID = n.id" +
        "    left join officedba.DeptInfo k on a.DeptID=k.id" +
        "    left join officedba.StorageInfo l on l.id=c.storageId" +   //仓库id
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