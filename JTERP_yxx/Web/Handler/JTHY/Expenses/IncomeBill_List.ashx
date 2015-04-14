<%@ WebHandler Language="C#" Class="IncomeBill_List" %>
using System;
using System.Web;
using XBase.Common;
using System.Data;
using System.Linq;
using System.Collections;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Web.SessionState;
using XBase.Model.Common;
using XBase.Data.JTHY.Expenses;
using System.Text;


public class IncomeBill_List : IHttpHandler, IRequiresSessionState
{            
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        HttpRequest request = context.Request;
       
       //if (context.Request.ContentType == "POST")
        if(true)
        {
            string my = context.Request.ContentType;
            string Action = context.Request.Params["action"].ToString().Trim();//
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (Action == "Get")//查询所有
            {
                DataTable dt = new DataTable();
                //设置行为参数
                string orderString = context.Request.Params["orderby"].ToString();//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.Params["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Params["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string IncomeNo = context.Request.Params["IncomeNo"].ToString().Trim();  //单据编号                
                string Executor = context.Request.Params["Executor"].ToString().Trim();   //经办人
                string StartDate = context.Request.Params["StartDate"].ToString().Trim();   //申请开始时间
                string EndDate = context.Request.Params["EndDate"].ToString().Trim();   //申请开始时间

                string CustName = context.Request.Params["CustName"].ToString().Trim();   //来往单位
                string ConfirmStatus = context.Request.Params["ConfirmStatus"].ToString().Trim();//单据状态

                //string Applyor = request.Form["Applyor"].ToString().Trim();   //申请人
                //string ExpCode = request.Form["IncomeNo"].ToString().Trim();  //单据编号
                //string Applyor = request.Form["Applyor"].ToString().Trim();   //申请人
                //string TransactorName = request.Form["TransactorName"].ToString().Trim();   //经办人
                //string BeginT = request.Form["BeginT"].ToString().Trim();   //申请开始时间
                //string EndT = request.Form["EndT"].ToString().Trim();   //申请开始时间
                //string CustName = request.Form["CustName"].ToString().Trim();   //来往单位

                string ord = orderBy + " " + order;
                int TotalCount = 0;




                string sql = @"SELECT     a.ID, a.InComeNo, a.CustName, a.BillingID
                , REPLACE(CONVERT(varchar(100), a.AcceDate, 23), '1900-01-01', '') AS AcceDate, a.TotalPrice, 
                      b.EmployeeName AS Executor, c.EmployeeName AS Confirmor
                      , (CASE a.AcceWay WHEN 0 THEN '现金' WHEN 1 THEN '银行存款' ELSE '其他' END) 
                      AS AcceWay, REPLACE(CONVERT(varchar(100), a.ConfirmDate, 23), '1900-01-01', '') AS ConfirmDate
                      
                      FROM         officedba.IncomeBill AS a LEFT OUTER JOIN
                      officedba.EmployeeInfo AS b ON a.Executor = b.ID LEFT OUTER JOIN
                      officedba.EmployeeInfo AS c ON a.Confirmor = c.ID" +
                      " where a.CompanyCD = '" + CompanyCD + "'";


                if (IncomeNo != "")
                    sql += " and a.IncomeNo like '%" + IncomeNo + "%'";
                if (Executor != "")
                    sql += " and b.Executor like '%" + Executor + "%'";
                if (ConfirmStatus != "")
                    sql += " and a.ConfirmStatus like '%" + ConfirmStatus + "%'";
                if (StartDate != "")
                    sql += " and AcceDate>='" + StartDate + "'";
                if (EndDate != "")
                    sql += " and AcceDate<='" + EndDate + "'";
                if (CustName != "")
                    sql += " and a.CustName like '%" + CustName + "%'";
                try
                {
                    dt = XBase.Data.DBHelper.SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                }
                catch (Exception ex)
                {
                    dt = null;
                }


                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count == 0 || dt == null)
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
            else if (Action == "SearchOne")
            {
                string id = request.Form["id"].ToString().Trim() == "" ? "0" : request.Form["id"].ToString().Trim();
                string sqlOne = @"select distinct a.id,a.DeptID,a.Status,a.Confirmor,g.EmployeeName as ConfirmorName,CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate, ExpCode,Applyor,b.EmployeeName as ApplyorName,CONVERT(varchar(100),AriseDate, 23) as AriseDate,
                                    e.DeptName as DeptName,a.TotalAmount,ExpType,a.PayType,
                                    a.CustID,d.CustName as CustName,TransactorID,c.EmployeeName as TransactorName,
                                    a.Reason,a.ConfirmDate,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate,a.Creator,f.EmployeeName as CreatorName,a.ModifiedUserID,CONVERT(varchar(100),a.ModifiedDate, 23) as ModifiedDate
                                    from dbo.jt_fysq a
                                    left join officedba.EmployeeInfo b on a.Applyor=b.id
                                    left join officedba.EmployeeInfo c on a.TransactorID=c.id
                                    left join officedba.CustInfo d on a.CustID=d.id
                                    left join officedba.DeptInfo e on a.DeptID=e.id
                                    left join officedba.EmployeeInfo f on a.Creator=f.id
                                    left join officedba.EmployeeInfo g on a.Confirmor=g.id                                                                        
                                    where a.id=" + id + " and a.CompanyCD='" + CompanyCD + "'";
                try
                {
                    DataTable dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(sqlOne);


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
            else if (Action.Trim() == "Delete")
            {

                string allid = context.Request.Params["ID"].ToString().Trim(); //客户编号
                string[] ALLID1 = allid.Split(',');
                string[] ALLID2 = allid.Split(',');

                JsonClass jc;
                
                bool Issure = IsSure(ALLID1);
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

    /// <summary>
    /// 其功能的实现是？
    /// </summary>
    /// <param name="ALLID"></param>
    /// <returns></returns>
    private bool IsSure(string[] ALLID)
    {
        bool bo = false;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < ALLID.Length; i++)
        {
            ALLID[i] = "'" + ALLID[i] + "'";
            sb.Append(ALLID[i]);
        }

        string allExpIDs = sb.ToString().Replace("''", "','");
        allExpIDs = allExpIDs.Replace("'", "");
        string[] ExpIDs = allExpIDs.Split(',');
        for (int i = 0; i < ExpIDs.Length; i++)
        {
            string sql = "select ConfirmStatus from Officedba.IncomeBill where id=" + ExpIDs[0];
            object o=XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql,null);
            if(o==null||o==""){
                return false;
            }
            if ( Convert.ToInt32(o) == 1)
            {
                bo = true;
                return bo;
            }
            
            
        }
        return bo;
        

    }
    
    /// <summary>
    /// 删除功能
    /// </summary>
    /// <param name="ALLID"></param>
    /// <returns></returns>
    private bool DeleteDataByIds(string[] ALLID)
    {
        bool bo = false;
        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
        for (int i = 0; i < ALLID.Length; i++)
        {
            ALLID[i] = "'" + ALLID[i] + "'";
            sb1.Append(ALLID[i]);
        }

        string allExpID = sb1.ToString().Replace("''", "','");
        string sql = "delete from Officedba.IncomeBill where id in (" + allExpID + ")";

        try
        {
            XBase.Data.DBHelper.SqlHelper.ExecuteSql(sql);
            bo = true;
        }
        catch (Exception ex)
        {
            bo = false;
        }
        return bo;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}