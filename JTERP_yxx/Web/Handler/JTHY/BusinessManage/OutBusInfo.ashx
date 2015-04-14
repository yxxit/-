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

            if (Action == "SearchSellContract")//查询销售合同
            {
                SearchSellContract(context, User, CompanyCD);                
            }
            else if (Action == "SearchOutBusList")  //检索销售发货单列表
            {
                SearchOutBusList(context,User,CompanyCD);                 
            }
            else if (Action == "SearchOutBusOne")  //查询某个销售发货单详细信息
            {
                SearchOutBusOne(context, User, CompanyCD); 
            }
            else if (Action == "uc_SearchOutBusDetail")  //自定义控件查询某个销售发货单明细信息
            {
                string headid = context.Request.Form["headid"].ToString();
 
                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = @"select a.id, a.StorageID,b.StorageName,a.ProductID,c.ProdNo,c.ProductName,
                                    cast(a.ProductCount as int) as TotalNum,'0' as ProCount,
                                    a.InCost,a.TaxPrice ,a.TaxRate,'0' as TotalTax,'0' as TotalFee,c.unitID,isnull(e.codeName,'') as unitName,
                                    a.OutCount,a.SttlCount,(isnull(a.ProductCount,0)-isnull(a.OutCount,0)) as ProductCount,a.TotalInCost as TotalInCost 
                                    from dbo.jt_xsfh_mx a
                                    left join officedba.StorageInfo b on b.id=a.storageID
                                    left join officedba.ProductInfo c on c.id=a.ProductID 
                                    left join officedba.CodeUnitType e on e.id=c.unitID                                               
                                    where a.sendNo=" + headid;

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
            else if (Action == "SearchOutBusDetail")  //查询某个销售发货单明细信息
            {
                string headid = context.Request.Form["headid"].ToString();
                context.Response.ContentType = "text/plain";
                DataTable dt = new DataTable();
                string strQuery = @"select a.id, a.StorageID,b.StorageName,a.ProductID,c.ProdNo,c.ProductName,
                                    a.ProductCount as ProductCount,c.unitID,isnull(e.codeName,'') as unitName,
                                    a.InCost,a.TaxPrice ,a.TaxRate,a.TotalTax,a.TotalFee,
                                    isnull(a.OutCount,'0.0000') as OutCount,isnull(a.SttlCount,'0.0000') as SttlCount,a.TotalInCost as TotalInCost 
                                    from dbo.jt_xsfh_mx a
                                    left join officedba.StorageInfo b on b.id=a.storageID
                                    left join officedba.ProductInfo c on c.id=a.ProductID 
                                    left join officedba.CodeUnitType e on e.id=c.unitID                                              
                                    where a.sendNo=" + headid;
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
            else if (Action == "SearchOutBusToOutWare")   //自定义控件：获取销售发货单信息
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                string BusiType = context.Request.Form["BusiType"].ToString();//判断是普通销售还是采购直销
                int TotalCount = 0;

                //检索条件
                string OutBusNo = context.Request.Form["OutBusNo"].ToString().Trim();   //销售单号
                string CustName = context.Request.Form["CustName"].ToString().Trim();   //客户名称
                string ProName = context.Request.Form["ProName"].ToString().Trim();   //煤种

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();

                string strQuery = "  select a.id,a.sendno,a.custid,f.custname,isnull(f.billunit,'') billunit,isnull(b.storageid,'') storageid," +
               "  isnull(g.storagename,'') storagename,a.providerid,c.CustName as providername," +
               "  isnull(a.CustJsFee,0) as CustJsFee,isnull(a.ProJsFee,0) as ProJsFee ,isnull(a.TotalFee,0) as SellMoney,isnull(a.SupplyAmount,0) as ProMoney," +
               "   b.productid ,d.productname,productcount," +
               "    isnull(b.UnitPrice,0) as unitcost,isnull(b.TaxPrice,0) as taxunitcost,b.taxrate," +
               "    isnull(b.totalfee,0) as taxmoney,isnull(b.TotalPrice,0) as imoney,isnull(b.totaltax,0) as itax,e.id_at as diaoyunid,e.diaoyunno," +
               "    isnull(e.motorcade,'') as carno,isnull(e.vehicle_quantity,0) as carnum," +
               "    a.CarryType as transporttype,a.PayType as settletype,a.Seller as ppersonid,h.employeename as ppersonname,a.SellDeptId as deptid,i.deptname" +
               "    ,isnull(a.TransportFee,0)  as transmoney,e.ship_quantity as sendnum," +
               "    ISNULL(e.ship_place,'') as startstation, ISNULL(e.to_place,'') as endstation, " +
               "    (case e.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'    when '50' then '到货' " +
               "    else '制单' end) transstate,a.BusiType,e.at_state   " +
               "    from  jt_xsfh a  left join jt_xsfh_mx b on a.id=b.sendno " +
               "    left join officedba.ProviderInfo c on a.providerid=c.id   " +
               "    left join officedba.ProductInfo d on b.productid=d.id   " +
               "    left join jt_HuocheDiaoyun e on a.Transporter=e.id_at    " +
               "    left join officedba.CustInfo f on a.custid=f.id   " +
               "    left join officedba.storageinfo g on b.storageid=g.id" +
               "    left join officedba.employeeinfo  h on a.Seller=h.id" +
               "    left join officedba.deptinfo i on a.SellDeptId=i.id" +
              "     where 1=1 and a.BusiType=" + BusiType + " and a.billstatus='2' and a.CompanyCD = '" + CompanyCD + "'";

                if (OutBusNo != "")
                    strQuery += " and a.sendno like '%" + OutBusNo + "%'";
                if (CustName != "")
                    strQuery += " and f.custname like '%" + CustName + "%'";
                if (ProName != "")
                    strQuery += " and d.productname like '%" + ProName + "%'";                  
                            
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
        }
    }

    //根据订单ID号查询详细
    private void SearchOutBusOne(HttpContext context, int User, string CompanyCD)
    {
        string headid = context.Request.Form["headid"].ToString();
                string typeflag = context.Request.Form["typeflag"].ToString();//"0"为普通销售"1"为采购直销
                string BusiType="0";
                if (typeflag == "0")
                    BusiType = "1";
                if (typeflag == "1")
                    BusiType = "2";
                context.Response.ContentType = "text/plain";
                DataTable dt = new DataTable();
                string  strQuery = @"select a.id, a.SendNo,a.FromBillID,b.Contractid,
                                    a.PayType,a.CustID,c.CustName,c.BillUnit,a.billStatus,
                                    a.CarryType,a.CountTotal,d.ship_time,a.TotalFee,a.Seller,
                                    e.EmployeeName,a.SellDeptid,f.DeptName,
                                    a.PurContractID,g.Contractid as PurContractNo,
                                    a.ProviderID,h.CustName as ProviderName,a.TransportFee,
                                    a.SupplyAmount,a.Remark,a.Transporter,d.DiaoyunNO,
                                    (case d.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'
                                    when '50' then '到货' else '制单' end) transstate,
                                    d.motorcade as CarNo,d.ship_place as StartStation,
                                    d.to_place as EndStation,d.vehicle_quantity as CarNum,d.ship_quantity as SendNum,
                                    d.at_state,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,a.Creator,i.EmployeeName as CreatorName,
                                    convert(varchar(100),a.ModifiedDate,23) as ModifiedDate,a.ModifiedUserID,
                                    a.Confirmor,j.EmployeeName as ConfirmorName, CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,
                                    ISNULL( k.CustName,'')  AS ServicesName, isnull( k.ID,'') AS ServicesId,d.Ss_quan as GetNum
                                    from dbo.jt_xsfh a
                                    left join dbo.ContractHead_Sale b on b.id=a.FromBillID
                                    left join officedba.CustInfo c on c.id=a.CustID
                                    left join jt_HuocheDiaoyun d on d.id_at=a.Transporter
                                    left join officedba.EmployeeInfo e on e.id=a.Seller
                                    left join  officedba.ProviderInfo AS k ON k.ID = a.ServicesID                                    
                                    left join officedba.DeptInfo f on f.id=a.SellDeptid
                                    left join dbo.ContractHead_Pur g on g.id=a.PurContractID
                                    left join officedba.ProviderInfo h on h.id=a.ProviderID
                                    left join officedba.EmployeeInfo i on i.id=a.Creator
                                    left join officedba.EmployeeInfo j on j.id=a.Confirmor                                    
                                    where a.BusiType='" + BusiType + "' and a.id=" + headid;
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
        
    private void SearchSellContract(HttpContext context, int User, string CompanyCD)
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
        "     a.ContractMoney,c.productname,c.unitid,d.codename as unitname,isnull(c.specification,'') specification," +
        "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus," +
        "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
        "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney " +
        "    from ContractHead_Sale a" +
        "    left join ContractDetails_Sale b on a.id=b.contractid" +
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
    
    
    // 获取列表信息
    private void SearchOutBusList(HttpContext context, int User, string CompanyCD)
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
        string BusiType = context.Request.Form["BusiType"].ToString();//判断是普通销售还是采购直销
        //检索条件
        string SendNo = context.Request.Form["SendNo"].ToString().Trim();  //发货单号
        string ProviderName = context.Request.Form["ProviderName"].ToString().Trim();   //供货方
        string BeginT = context.Request.Form["BeginT"].ToString().Trim();   //发货开始时间
        string EndT = context.Request.Form["EndT"].ToString().Trim();   //发货结束时间
        string SellerName = context.Request.Form["SellerName"].ToString().Trim();   //业务员
        string TranSNo = context.Request.Form["TranSNo"].ToString().Trim();   //调运单号
        string SendState = context.Request.Form["SendState"].ToString().Trim();   //运送状态
        
        string PurContractNo = context.Request.Form["PurContractNo"].ToString().Trim();   //运送状态
        string DeptID = context.Request.Form["DeptID"].ToString().Trim();   //运送状态
        string ContractNo = context.Request.Form["ContractNo"].ToString().Trim();   //运送状态   
        string CustName = context.Request.Form["CustName"].ToString().Trim();   
               
        int TotalCount = 0;

        context.Response.ContentType = "text/plain";

        DataTable dt = new DataTable();

        string strQuery = "   select a.id,a.sendno,a.custid,f.custname,b.productid ,d.productname,productcount as productcount ,b.totalfee as totalfee,b.TaxPrice," +
        "  ISNULL(e.ship_place,'') as startstation,e.ship_time, " +
        "  ISNULL(e.to_place,'') as endstation,c.custname as providerName, " +
        " (case e.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
        " when '50' then '到货' else '制单' end) transstate,(case a.BillStatus when '1' then '未确认' else '已确认' end) BillStatus,a.BusiType, " +
        " i.DeptNO as DeptNO,i.ID as DeptID,i.DeptName,  isnull(e.DiaoyunNO,'')as DiaoyunNo,e.id_at as DiaoyunId, " +
        " h.id as CtrId,h.ContractId ,j.id as SaleContractId,j.ContractId as SaleContractNo " +
        " from  jt_xsfh a  left join jt_xsfh_mx b on a.id=b.sendno" +
        " left join officedba.ProviderInfo c on a.providerid=c.id" +
        " left join officedba.ProductInfo d on b.productid=d.id  " +
        " left join jt_HuocheDiaoyun e on a.Transporter=e.id_at  " +
        " left join officedba.CustInfo f on a.custid=f.id" +
        " left join officedba.EmployeeInfo g on a.Seller=g.id" +        
        " left join officedba.DeptInfo i on i.id=a.sellDeptId " +
        " left join ContractHead_pur h on h.id=a.PurContractID" +
        " left join ContractHead_Sale j on j.id=a.FromBillID " +              
        " where a.CompanyCD = '" + CompanyCD + "' and a.BusiType='" + BusiType + "'";

        if (SendNo != "")
            strQuery += " and a.sendno like '%" + SendNo + "%'";
        if (ProviderName != "" && ProviderName != "不可用")
            strQuery += " and c.custname like '%" + ProviderName + "%'";
        if (BeginT != "")
            strQuery += " and e.ship_time>='" + BeginT + "'";
        if (EndT != "")
            strQuery += " and e.ship_time<='" + EndT + "'";
        if (SellerName != "")
            strQuery += " and g.EmployeeName like '%" + SellerName + "%'";
        if (TranSNo != "")
            strQuery += " and e.diaoyunNo like '%" + TranSNo + "%'";
        //if (SendState != "")
        //    strQuery += " and (case e.at_state when 10 then '制单'  when '20'  then '装车' when '30' then '发货' when '40' then '在途'" +
        // " when '50' then '到货' else '制单' end) like '%" + SendState + "%'";

        if (PurContractNo != "")
            strQuery += " and h.ContractID like '%" + PurContractNo + "%'";
        if (ContractNo != "")
            strQuery += " and j.ContractID like '%" + ContractNo + "%'";
        if (DeptID != "")
            strQuery += " and a.sellDeptId = '" + DeptID + "'";
        if (CustName != "")
            strQuery += " and f.custname like '%" + CustName + "%' ";

        // 权限限制代码
        XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
        DataTable dtt = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
        if (dtt != null && dtt.Rows.Count > 0)
        {
            if (dtt.Rows[0]["RoleRange"].ToString() == "1")
            {
                strQuery += " and (a.Creator IN  ";
                strQuery += "( SELECT ID FROM  officedba.EmployeeInfo ";
                strQuery += "  WHERE DeptID IN (SELECT a.ID  ";
                strQuery += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                strQuery += "  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) ";
            }
            if (dtt.Rows[0]["RoleRange"].ToString() == "2")
            {
                strQuery += " and (a.Creator IN  ";
                strQuery += " (SELECT ID FROM  officedba.EmployeeInfo ";
                strQuery += "  WHERE DeptID IN (SELECT a.ID  ";
                strQuery += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ";
                strQuery += "  WHERE a.ID=b.ID) ))   ";
            }
            if (dtt.Rows[0]["RoleRange"].ToString() == "3")
            {
                strQuery += " and (a.Creator IN  ";
                strQuery += "( SELECT ID FROM  officedba.EmployeeInfo ";
                strQuery += "  WHERE DeptID IN (SELECT a.ID  ";
                strQuery += " FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ";
                strQuery += "  WHERE a.ID=b.ID)))   ";
            }
        }
        // end 权限限制 结束
        
       
        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
        comm.CommandText = strQuery;

        // 求数量及金额总和
        DataTable dtTtl = new DataTable();
        string strQueryTtl = "SELECT   SUM(productcount) AS ttlCount, SUM(totalfee) AS ttlFee FROM (" + strQuery +
                 ") AS tempTable";
        
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
}