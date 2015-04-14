<%@ WebHandler Language="C#" Class="Expenses_List" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using XBase.Model.Office.SellManager;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Text;

public class Expenses_List : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        HttpRequest request = context.Request;      
        
        if (request.RequestType == "POST")
        {
            string Action = (context.Request.Form["action"].ToString());//
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (Action == "SearchAll")
            {
                DataTable dt = new DataTable();
                //设置行为参数
                string orderString = context.Request.Form["orderby"].ToString();//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_d"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(request.Form["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数



                string ExpCode = request.Form["ExpCode"].ToString().Trim();  //单据编号
                string Applyor = request.Form["Applyor"].ToString().Trim();   //申请人
                string TransactorName = request.Form["TransactorName"].ToString().Trim();   //经办人
                string BeginT = request.Form["BeginT"].ToString().Trim();   //申请开始时间
                string EndT = request.Form["EndT"].ToString().Trim();   //申请开始时间
                string CustName = request.Form["CustName"].ToString().Trim();   //来往单位

                string ord = orderBy + " " + order;
                int TotalCount = 0;




                string sql = @" select distinct a.id, ExpCode,Applyor,b.EmployeeName as ApplyorName,CONVERT(varchar(100),AriseDate, 23) as  AriseDate,
                            TotalAmount,a.CustID,d.CustName as CustName,TransactorID,c.EmployeeName as TransactorName,a.Confirmor,e.EmployeeName as ConfirmorName,CONVERT(varchar(100),a.ConfirmDate, 23) as  ConfirmDate 
                            from dbo.jt_fysq a
                            left join officedba.EmployeeInfo b on a.Applyor=b.id
                            left join officedba.EmployeeInfo c on a.TransactorID=c.id
                            left join officedba.EmployeeInfo e on isnull(a.Confirmor,0)=e.id                            
                            left join officedba.CustInfo d on a.CustID=d.id " +
                               " where a.CompanyCD = '" + CompanyCD + "'";


                if (ExpCode != "")
                    sql += " and a.ExpCode like '%" + ExpCode + "%'";
                if (Applyor != "")
                    sql += " and b.EmployeeName like '%" + Applyor + "%'";
                if (TransactorName != "")
                    sql += " and c.EmployeeName like '%" + TransactorName + "%'";
                if (BeginT != "")
                    sql += " and AriseDate>='" + BeginT + "'";
                if (EndT != "")
                    sql += " and AriseDate<='" + EndT + "'";
                if (CustName != "")
                    sql += " and d.CustName like '%" + CustName + "%'";
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

                string allid = context.Request.Params["AllId"].ToString().Trim(); //客户编号
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
            string sql = "select Status from jt_fysq where id=" + ExpIDs[0];
            object o=XBase.Data.DBHelper.SqlHelper.ExecuteScalar(sql,null);
            if ( Convert.ToInt32(o) == 2)
            {
                bo = true;
                return bo;
            }
            
            
        }
        return bo;
        

    }
    

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
        string sql = "delete from jt_fysq where id in (" + allExpID + ")";

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