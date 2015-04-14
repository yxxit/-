<%@ WebHandler Language="C#" Class="PayBill_Add" %>


using System;
using System.Web;
using XBase.Common;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Web.SessionState;
using XBase.Model.Common;
using XBase.Data.JTHY.Expenses;
using System.Text;
public class PayBill_Add : IHttpHandler, IRequiresSessionState
{

    bool isSucc = false;
    
    JsonClass jc = new JsonClass();
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";


        if (context.Request.RequestType == "POST")
        {

            string Action = context.Request.Params["action"].ToString().Trim();
            if (Action == "insert")
            {
                InsertData(context);//context传递的是上下文，让其都可以使用response
            }
            if (Action == "update")//反确认
            {
                UpdateDate(context);
            }
            if (Action == "Edit")
            {
                EditDate(context);
            }
            if (Action == "confirm")//确认
            {
                ConfirmDate(context);
            }
            if (Action == "LoadIncomBill")
            {
                LoadIncomBill(context);
            }
        }
            
    }
    /// <summary>
    /// 数据的加载
    /// </summary>
    /// <param name="context"></param>
    private void LoadIncomBill(HttpContext context)
    {
        HttpRequest request = context.Request;//获取request
        PayBillModels ibm = new PayBillModels();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//获取公司的名称
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//获取用户的id
        
        
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        
        string headid = context.Request.Form["headid"].ToString();//获取数据的id
        int TotalCount = 0;

        //string billtypeid = context.Request.Form["billtypeid"].ToString();
        //string billid = context.Request.Form["autoid"].ToString();

        ////获取数据


        DataTable dt = new DataTable();

        string strQuery = @"SELECT     a.ConfirmStatus, a.id, a.PayNo, REPLACE(CONVERT(varchar(12), a.PayDate, 23), '1900-01-01', '') AS PayDate, a.CustName, a.FromBillType, a.BillingID,
                       a.PayAmount, a.PaymentType, a.AcceWay, b.EmployeeName AS Executor, REPLACE(CONVERT(varchar(12), a.AccountDate, 23), '1900-01-01', '') 
                      AS AccountDate, d.EmployeeName AS Accountor, REPLACE(CONVERT(varchar(12), a.ConfirmDate, 23), '1900-01-01', '') AS ConfirmDate, 
                      c.EmployeeName AS Confirmor
FROM         officedba.PayBill AS a LEFT OUTER JOIN
                      officedba.EmployeeInfo AS b ON a.Executor = b.ID LEFT OUTER JOIN
                      officedba.EmployeeInfo AS c ON a.Confirmor = c.ID LEFT OUTER JOIN
                      officedba.EmployeeInfo AS d ON a.Accountor = d.ID
where a.id=" +headid+"";
        
        

       // string strQuery = "select  a.id,a.cVouchType,a.Contractid,a.BillType,a.cVenCode,isnull(f.custname,'') as cvenname,f.contactname as linkman,a.cCusCode," +
       //"      isnull(e.custname,'') custname,a.DeliveryAddress,a.SettleType,g.typename as SettleTypeName," +
       // "   replace( convert(varchar(12),a.signdate,23),'1900-01-01','') signdate,replace( convert(varchar(12),a.effectivedate,23),'1900-01-01','') effectivedate,replace( convert(varchar(12),a.enddate,23),'1900-01-01','') enddate," +
       // "    a.TransPortType,h.typename as TransPortTypeName,c.storageId, l.storageName ,c.prodno," +
       // "     a.ContractMoney,c.productname,c.unitid,d.codename as unitname,isnull(c.specification,'') specification," +
       // "    (Case  a.billstatus when 1 then '制单' when 2 then '确认' when 3 then '审核' else '其他' end) billstatus,a.billstatus as billstatusid," +
       // "    a.creator,replace( convert(varchar(12),a.createdate,23),'1900-01-01','') createdate,i.employeename as createname,'' as checkor,'' as checkdate,a.remark," +
       // "    b.autoid as detailsid,b.cinvccode,b.iquantity,b.iunitcost,b.imoney," +
       // "isnull(a.PPersonID,0) PPersonID,isnull(j.employeename,'') as PPerson,isnull(a.DeptID,0) DeptID,isnull(k.deptname,'') DeptName " +
       // "    from ContractHead_Sale a" +
       // "    inner join ContractDetails_Sale b on a.id=b.contractid" +
       // "    left join officedba.ProductInfo c on b.cinvccode=c.id" +
       // "    left join officedba.CodeUnitType d on c.unitid=d.ID" +
       // "    left join officedba.CustInfo e on a.cCusCode=e.id " +
       // "    left join officedba.ProviderInfo f on a.cVenCode=f.id" +
       // "    left join officedba.CodePublicType  g on a.SettleType=g.id" +
       // "    left join officedba.CodePublicType h on  a.TransPortType=h.id" +
       // "    left join officedba.EmployeeInfo i on a.creator=i.id" +
       // "    left join officedba.EmployeeInfo j on a.PPersonID=j.id" +
       // "    left join officedba.DeptInfo k on a.DeptID=k.id" +
       // "    left join officedba.StorageInfo l on l.id=c.storageId" +   //仓库id
       // "    where a.companycd='" + CompanyCD + "' and a.cVouchType='1' and a.BillType='1' and a.id='" + headid + "' order by a.id desc,b.autoid ";
        dt = SqlHelper.ExecuteSql(strQuery);
        TotalCount = dt.Rows.Count;


        //string strAnn = "select a.ParentId as InqNo,a.annFileName as AnnFileName,a.AnnAddr,a.upDatetime as UpDateTime,a.annRemark as AnnRemark from officedba.Annex a" +
        //    " where a.CompanyCD='" + CompanyCD + "'  and ModuleType='" + ConstUtil.MODULE_ID_SELLCONTRANCT_ADD + "' and a.ParentId='" + headid + "'";


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
    /// <summary>
    /// 编辑收款单
    /// </summary>
    /// <param name="context"></param>
    private void EditDate(HttpContext context)
    {
        HttpRequest request = context.Request;//获取request
        PayBillModels ibm = new PayBillModels();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//获取公司的名称
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//获取用户的id
        //--------------------------------------------------------------------------------------------------------修改
        int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//获取用户的编号


        ibm.ID = Convert.ToInt32(request.Params["ID"].ToString().Trim());//----------------------------->update 2014-7-14
        ibm.PayNo = request.Params["PayNo"].ToString().Trim();
        ibm.PayDate = request.Params["PayDate"].ToString().Trim(); ;//收款日期
        ibm.CustName = request.Params["CustName"].ToString().Trim();//往来顾客
        ibm.FromBillType = request.Params["FromBillType"].ToString().Trim() == "" ? "0" : request.Params["FromBillType"].ToString().Trim();//业务单类型
        ibm.BillingID = Convert.ToInt32(request.Params["BillingID"].ToString().Trim());//来源单编号,现在不要去去做
        ibm.PayAmount = Convert.ToDouble(request.Params["PayAmount"].ToString().Trim());//收款金额
        ibm.PaymentType = Convert.ToInt32(request.Params["PaymentType"].ToString().Trim());//款项类型
        ibm.AcceWay = request.Params["AcceWay"].ToString().Trim();//付款方式
        string Executor = request.Params["Executor"].ToString().Trim();//执行人
       
        ibm.Executor = Convert.ToInt32(Executor);

        ibm.CompanyCD = CompanyCD;
        int isSucc = PayBillDeal.PayBillEdit(ibm);
        if (isSucc != 0)
        {
            jc = new JsonClass("修改成功", "修改成功", isSucc);
        }
        else
        {
            jc = new JsonClass("修改失败", "", 0);
        }
        context.Response.Write(jc);
        context.Response.End();
    }

    
    

    /// <summary>
    /// 添加收款单
    /// </summary>
    /// <param name="context"></param>
    private void InsertData(HttpContext context)
    {
        HttpRequest request = context.Request;//获取request
        PayBillModels ibm = new PayBillModels();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//获取公司的名称
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//获取用户的id
        //--------------------------------------------------------------------------------------------------------修改
        int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//获取用户的编号
        
        //string ExpCodeType = request.Params["ExpCodeType"].ToString().Trim();
        //string ExpCode = request.Params["ExpCode"].ToString().Trim();
        //string tableName = "Jt_fysq";//费用申请表
        //string columnName = "ExpCode";//单据编号

        //if (ExpCodeType != "")  //如果为自动编号，则获取编码
        //    ExpCode = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ExpCodeType, tableName, columnName);
        //else
        //{
        //    //判断是否已经存在
        //    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, ExpCode);
        //    if (!ishave)
        //    {
        //        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
        //        context.Response.Write(jc);
        //        context.Response.End();
        //    }
        //}
        bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt("Officedba.PayBill", "PayNo", request.Params["PayNo"].ToString().Trim());
        if (!ishave)
        {
            jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
            context.Response.Write(jc);
            context.Response.End();
        }




        //ibm.ID = Convert.ToInt32(request.Params["ID"].ToString().Trim());//----------------------------->update 2014-7-14
        ibm.PayNo = request.Params["PayNo"].ToString().Trim();
        ibm.PayDate = request.Params["PayDate"].ToString().Trim(); ;//收款日期
        ibm.CustName= request.Params["CustName"].ToString().Trim();//往来顾客
        ibm.FromBillType = request.Params["FromBillType"].ToString().Trim() == "" ? "0" : request.Params["FromBillType"].ToString().Trim();//业务单类型
        ibm.BillingID = Convert.ToInt32(request.Params["BillingID"].ToString().Trim());//来源单编号,现在不要去去做
        ibm.PayAmount = Convert.ToDouble(request.Params["PayAmount"].ToString().Trim());//收款金额
        ibm.PaymentType = Convert.ToInt32(request.Params["PaymentType"].ToString().Trim());//款项类型
        ibm.AcceWay = request.Params["AcceWay"].ToString().Trim();//付款方式
        string Executor = request.Params["Executor"].ToString().Trim();//执行人
        ibm.Executor = Convert.ToInt32(Executor);
            
        //附加信息
        ibm.AccountDate = DateTime.Now.ToString("yyyy-MM-dd");// request.Params["AccountDate"].ToString().Trim();//建档日期
        ibm.Accountor = EmployeeID;// Convert.ToInt32(request.Params["Accountor"].ToString().Trim() == "" ? "0" : request.Params["Accountor"].ToString().Trim());
        //ibm.ConfirmDate = "";// ibm.PayDate;
        //ibm.Confirmor = Convert.ToInt32(request.Params["Confirmor"].ToString().Trim() == "" ? "0" : request.Params["Confirmor"].ToString().Trim());

        ibm.CompanyCD = CompanyCD;
        int isSucc = PayBillDeal.PayBillAdd(ibm);
        if (isSucc!=0)
        {
           
            jc = new JsonClass("保存成功", "保存成功", isSucc);
            
        }
        else
        {
            jc = new JsonClass("保存失败11", "", 0);
        }
            
        context.Response.Write(jc);
        context.Response.End();


    }


    /// <summary>
    /// 确认
    /// </summary>
    /// <param name="context"></param>
    private void ConfirmDate(HttpContext context)
    {
        PayBillModels ibm = new PayBillModels();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//获取公司的名称
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//获取用户的id
        //--------------------------------------------------------------------------------------------------------修改
        int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//获取用户的编号
        
        HttpRequest request = context.Request;//获取request

        ibm.ID = Convert.ToInt32(request.Params["ID"].ToString().Trim());//----------------------------->update 2014-7-14
        ibm.PayNo = request.Params["PayNo"].ToString().Trim();
        ibm.PayDate = request.Params["PayDate"].ToString().Trim(); ;//收款日期
        ibm.CustName = request.Params["CustName"].ToString().Trim();//往来顾客
        ibm.FromBillType = request.Params["FromBillType"].ToString().Trim() == "" ? "0" : request.Params["FromBillType"].ToString().Trim();//业务单类型
        ibm.BillingID = Convert.ToInt32(request.Params["BillingID"].ToString().Trim());//来源单编号,现在不要去去做
        ibm.PayAmount = Convert.ToDouble(request.Params["PayAmount"].ToString().Trim());//收款金额
        ibm.PaymentType = Convert.ToInt32(request.Params["PaymentType"].ToString().Trim());//款项类型
        ibm.AcceWay = request.Params["AcceWay"].ToString().Trim();//付款方式
        string Executor = request.Params["Executor"].ToString().Trim();//执行人
        //if (Executor == "管理员")
        //{
        //    Executor = "905";//管理员在什么地方？
        //}
        ibm.Executor = Convert.ToInt32(Executor);

        //附加信息
        //ibm.AccountDate = ibm.PayDate;
        //ibm.Accountor = 1;// Convert.ToInt32(request.Params["Accountor"].ToString().Trim() == "" ? "0" : request.Params["Accountor"].ToString().Trim());
        ibm.ConfirmDate = DateTime.Now.ToString("yyyy-MM-dd");
        ibm.Confirmor = EmployeeID;// Convert.ToInt32(request.Params["Confirmor"].ToString().Trim() == "" ? "0" : request.Params["Confirmor"].ToString().Trim());

        ibm.CompanyCD = CompanyCD;
        bool isSucc = PayBillDeal.ConfirmIncomeBill(ibm);
        if (isSucc)
        {
            jc = new JsonClass("确认成功", "确认成功", 1);
        }
        else
        {
            jc = new JsonClass("确认失败", "", 0);
        }
        context.Response.Write(jc);
        context.Response.End();
        //string ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd");//js中是这样使用当前时间的
     
    }
    
    /// <summary>
    /// 取消确认
    /// </summary>
    /// <param name="context"></param>
    private void UpdateDate(HttpContext context)
    {
        PayBillModels ibm = new PayBillModels();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//获取公司的名称
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//获取用户的id

        //--------------------------------------------------------------------------------------------------------修改
        int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;//获取用户的编号
        
        HttpRequest request = context.Request;//获取request

        ibm.ID = Convert.ToInt32(request.Params["ID"].ToString().Trim());//----------------------------->update 2014-7-14
        ibm.PayNo = request.Params["PayNo"].ToString().Trim();
        ibm.PayDate = request.Params["PayDate"].ToString().Trim(); ;//收款日期
        ibm.CustName = request.Params["CustName"].ToString().Trim();//往来顾客
        ibm.FromBillType = request.Params["FromBillType"].ToString().Trim() == "" ? "0" : request.Params["FromBillType"].ToString().Trim();//业务单类型
        ibm.BillingID = Convert.ToInt32(request.Params["BillingID"].ToString().Trim());//来源单编号,现在不要去去做
        ibm.PayAmount = Convert.ToDouble(request.Params["PayAmount"].ToString().Trim());//收款金额
        ibm.PaymentType = Convert.ToInt32(request.Params["PaymentType"].ToString().Trim());//款项类型
        ibm.AcceWay = request.Params["AcceWay"].ToString().Trim();//付款方式
        string Executor = request.Params["Executor"].ToString().Trim();//执行人
        
        ibm.Executor = Convert.ToInt32(Executor);

        //附加信息
        //ibm.AccountDate = ibm.PayDate;
        //ibm.Accountor = 1;// Convert.ToInt32(request.Params["Accountor"].ToString().Trim() == "" ? "0" : request.Params["Accountor"].ToString().Trim());
        //ibm.ConfirmDate = DateTime.Now.ToString("yyyy-MM-dd");
        //ibm.Confirmor = 1;// Convert.ToInt32(request.Params["Confirmor"].ToString().Trim() == "" ? "0" : request.Params["Confirmor"].ToString().Trim());

        ibm.CompanyCD = CompanyCD;
        bool isSucc = PayBillDeal.CloseConfirmIncomeBill(ibm);
        if (isSucc)
        {
            jc = new JsonClass("取消确认成功", "取消确认成功", 1);
        }
        else
        {
            jc = new JsonClass("取消确认失败", "", 0);
        }
        context.Response.Write(jc);
        context.Response.End();
        
    }
    
    


    /// <summary>
    /// 添加收款单
    /// </summary>
    /// <param name="context"></param>
//    private void InsertData(HttpContext context)
//    {


//        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
//        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

//        HttpRequest request = context.Request;
//        //
//        string ExpCodeType = request.Params["ExpCodeType"].ToString().Trim();
//        string ExpCode = request.Params["ExpCode"].ToString().Trim();
//        string tableName = "Jt_fysq";//费用申请表
//        string columnName = "ExpCode";//单据编号

//        if (ExpCodeType != "")  //如果为自动编号，则获取编码
//            ExpCode = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ExpCodeType, tableName, columnName);
//        else
//        {
//            //判断是否已经存在
//            bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, ExpCode);
//            if (!ishave)
//            {
//                jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
//                context.Response.Write(jc);
//                context.Response.End();
//            }
//        }


//        //当没有确认时，状态为“1”
//        string Status = request.Params["Status"].ToString().Trim();

//        string Applyor = request.Params["Applyor"].ToString().Trim();
//        string DeptID = request.Params["DeptID"].ToString().Trim();
//        string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
//        string AriseDate = request.Params["AriseDate"].ToString().Trim();
//        string ExpType = request.Params["ExpType"].ToString().Trim();
//        string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
//        string PayType = request.Params["PayType"].ToString().Trim();
//        string TransactorID = request.Params["TransactorID"].ToString().Trim() == "" ? "0" : request.Params["TransactorID"].ToString().Trim();
//        string Reason = request.Params["Reason"].ToString().Trim();
//        string CreateDate = request.Params["CreateDate"].ToString().Trim();
//        string Creator = request.Params["Creator"].ToString().Trim();
//        string ModifiedDate = request.Params["ModifiedDate"].ToString().Trim();
//        string ModifiedUserID = request.Params["ModifiedUserID"].ToString().Trim();

//        SqlCommand commandInsert = new SqlCommand();

//        string sqlInsert = @"insert into jt_fysq
//                (CustType,CompanyCD,ExpCode,Applyor,DeptID,TotalAmount,AriseDate,ExpType,CustID,
//                    PayType,TransactorID,Reason,CreateDate,Creator,ModifiedDate,ModifiedUserID,Status)
//                 values('1','" + CompanyCD + "','" + ExpCode + "'," + Applyor + "," + DeptID +
//                     "," + TotalAmount + ",'" + AriseDate + "'," + ExpType + "," + CustID +
//                     "," + PayType + "," + TransactorID + ",'" + Reason + "',getdate()" +
//                     "," + Creator + ",getdate(),'" + ModifiedUserID + "','" + Status + "')  set @ID=@@identity";


//        commandInsert.CommandText = sqlInsert;
//        commandInsert.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

//        int ID = 0;
//        try
//        {
//            isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
//            ID = (int)commandInsert.Parameters["@ID"].Value;

//        }
//        catch (Exception ex)
//        {
//            isSucc = false;
//        }

//        if (isSucc)
//        {

//            jc = new JsonClass("保存成功", ExpCode, ID);

//        }
//        else
//        {
//            jc = new JsonClass("保存失败", "", 0);
//        }

//        context.Response.Write(jc);
//        context.Response.End();


//    }
    //private void UpdateDate(HttpContext context)
    //{

    //    HttpRequest request = context.Request;
    //    int ID = int.Parse(request.Params["ID"].ToString().Trim());
    //    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
    //    string ExpCode = request.Params["ExpCode"].ToString().Trim();
    //    string Applyor = request.Params["Applyor"].ToString().Trim();
    //    string DeptID = request.Params["DeptID"].ToString().Trim();
    //    string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
    //    string AriseDate = request.Params["AriseDate"].ToString().Trim();
    //    string ExpType = request.Params["ExpType"].ToString().Trim();
    //    string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
    //    string PayType = request.Params["PayType"].ToString().Trim();
    //    string TransactorID = request.Params["TransactorID"].ToString().Trim() == "" ? "0" : request.Params["TransactorID"].ToString().Trim();
    //    string Reason = request.Params["Reason"].ToString().Trim();
    //    string Creator = request.Params["Creator"].ToString().Trim();
    //    string ModifiedDate = request.Params["ModifiedDate"].ToString().Trim();


    //    SqlCommand commandUpdate = new SqlCommand();


    //    if (ID > 0)
    //    {

    //        string sqlUpdate = @"update jt_fysq set Applyor=" + Applyor + ",DeptID=" + DeptID + ",TotalAmount=" + TotalAmount + ",AriseDate='" + AriseDate +
    //                       "',ExpType=" + ExpType + ", CustID=" + CustID + ",PayType=" + PayType + ",TransactorID=" + TransactorID + ",Reason='" + Reason +
    //                       "', ModifiedDate=getdate(),ModifiedUserID='" + UserID + "' where ExpCode='" + ExpCode + "' and  id=" + ID;
    //        commandUpdate.CommandText = sqlUpdate;
    //        try
    //        {
    //            isSucc = SqlHelper.ExecuteTransWithCommand(commandUpdate);
    //        }
    //        catch (Exception ex)
    //        {
    //            isSucc = false;
    //        }

    //        if (isSucc)
    //            jc = new JsonClass("保存成功", ExpCode, ID);
    //        else
    //            jc = new JsonClass("保存失败", "", 0);

    //        context.Response.Write(jc);
    //        context.Response.End();

    //    }

    //}

    ///// <summary>
    ///// 确认
    ///// </summary>
    ///// <param name="context"></param>
    //private void ConfirmDate(HttpContext context)
    //{
    //    int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
    //    string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
    //    string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
    //    HttpRequest request = context.Request;
        
    //    int ID = int.Parse(request.Params["ID"].ToString().Trim());
    //    string Status = request.Params["Status"].ToString().Trim();
    //    string ExpCode = request.Params["ExpCode"].ToString().Trim();
    //    string Applyor = request.Params["Applyor"].ToString().Trim();
    //    string DeptID = request.Params["DeptID"].ToString().Trim();
    //    string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
    //    string AriseDate = request.Params["AriseDate"].ToString().Trim();
    //    string ExpType = request.Params["ExpType"].ToString().Trim();
    //    string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
    //    string PayType = request.Params["PayType"].ToString().Trim();
    //    string TransactorID = request.Params["TransactorID"].ToString().Trim() == "" ? "0" : request.Params["TransactorID"].ToString().Trim();
    //    string Reason = request.Params["Reason"].ToString().Trim();
    //    string CreateDate = request.Params["CreateDate"].ToString().Trim();
    //    string ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd");//js中是这样使用当前时间的
    //    string ModifiedUserID = request.Params["ModifiedUserID"].ToString().Trim();

    //    SqlCommand commandUpdate = new SqlCommand();
        

    //    if (ID > 0)
    //    {

    //        string sqlUpdate = @"update jt_fysq set Applyor=" + Applyor + ",DeptID=" + DeptID + ",TotalAmount=" + TotalAmount + ",AriseDate='" + AriseDate +
    //                       "',ExpType=" + ExpType + ", CustID=" + CustID + ",PayType=" + PayType + ",TransactorID=" + TransactorID + ",Reason='" + Reason +
    //                       "', ModifiedDate=getdate(),ModifiedUserID='" + UserID +
    //                       "',Status='" + Status + "',ConfirmDate=getDate(),Confirmor=" + EmployeeID + "  where ExpCode='" + ExpCode + "' and  id=" + ID;
    //        commandUpdate.CommandText = sqlUpdate;
    //        try
    //        {
    //            isSucc = SqlHelper.ExecuteTransWithCommand(commandUpdate);
    //        }
    //        catch (Exception ex)
    //        {
    //            isSucc = false;
    //        }

    //        if (isSucc)
    //            jc = new JsonClass("确认成功", ExpCode + "," + EmployeeName, ID);
    //        else
    //            jc = new JsonClass("确认失败", "", 0);

    //        context.Response.Write(jc);
    //        context.Response.End();
    //    }
    //}




    public bool IsReusable {
        get {
            return false;
        }
    }

}