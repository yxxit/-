<%@ WebHandler Language="C#" Class="Flexigrid" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Text;
using System.IO;
using Newtonsoft.Json;


/// <summary>
/// Flexigrid 返回报表数据
/// </summary>
public class Flexigrid : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        HttpRequest request = context.Request;
        if (context.Request.RequestType == "POST")
        {
            string Action = context.Request.Params["Action"];
            if (Action == "SearchAll")//报表查询
            {
                SearchAll(context);                
            }
            else if (Action == "Delete")
            {
                Delete(context);
            }
        }
    }
    /// <summary>
    /// 报表查询
    /// 1,将相关参数进行一个整合，排除用不到的参数，
    /// 2,将代码进行优化。
    /// </summary>
    /// <param name="context"></param>
    public void SearchAll(HttpContext context){
        #region 筛选条件 按顺序
        string CompanyCD = "jthy4";//通过隐藏控件设置
        string CustName = "";//供应商名称
        string Contractid = "";//合同
        string StartTime = "";//开始时间
        string EndTime = "";//结束时间。
        if (StartTime == "")
            StartTime = "1900-01-01 00:00:00";
        if (EndTime == "")
            EndTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59");
        string Dept = "";

        string query = context.Request.Params["query"];//搜索查询的条件,将条件统一
        if (query != null && query != "")
        {
            string[] list = query.Split('#');
            if (list[0] != "")
            {
                CompanyCD = list[0];//当参数为空的时候，给以个默认值    
            }
            if (list[1] != "")
            {
                CustName = list[1];
            }
            if (list[2] != "")
            {
                Contractid = list[2];
            }
            if (list[3] != "")
            {
                Dept = list[3];
            }
            if (list[4] != "")
            {
                StartTime = list[4];
            }
            if (list[5] != "")
            {
                EndTime = list[5];
            }
        }
        #endregion
        string qtype = context.Request.Params["qtype"]; //搜索查询的类别,暂时不知道什么作用。

        #region 我们项目中的获取方法,有用
        //设置行为参数
        string orderString = context.Request.Params["sortname"].ToString();//排序
        string order = context.Request.Params["sortorder"].ToString();//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString : "ID";//要排序的字段，如果为空，默认为"ID"                
        int pageCount = int.Parse(context.Request.Params["rp"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Params["page"].ToString());//当前页
        //int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数在此报表中，用不到
        string ord = orderBy + " " + order;
        int TotalCount = 0;//总数量
        #endregion
        #region sql 业务编辑区
        string sqltop1 = @"SELECT DISTINCT
      t1.Contractid ,
      t4.CustName,
      t5.ProductName,
      t3.iQuantity,
      t3.iMoney,
      t9.ProductCount,
      t9.SttlCount,
      (t9.ProductCount-t9.SttlCount) as wCount,
      t9.totalfee,
      t9.SttlTotalPrice,
      (t9.totalfee- t9.SttlTotalPrice) as wFee,
      replace( convert(varchar(12),t1.SignDate,23),'1900-01-01','') SignDate ,
      replace( convert(varchar(12),t1.EffectiveDate,23),'1900-01-01','') EffectiveDate ,
      replace( convert(varchar(12),t1.EndDate,23),'1900-01-01','') EndDate ,
      t6.DeptName,
      t2.EmployeeName  
FROM    dbo.ContractHead_Pur t1
        JOIN officedba.EmployeeInfo t2 ON t1.PPersonID=t2.ID
        JOIN dbo.ContractDetails_Pur t3 ON t1.id = t3.ContractID
        JOIN officedba.ProductInfo t5 ON t3.cInvCCode = t5.ID
        JOIN officedba.ProviderInfo t4 ON  t1.cVenCode=t4.ID
        JOIN officedba.DeptInfo t6 ON t1.DeptID=t6.ID
        JOIN dbo.jt_cgdh t8 ON t8.SourceBillID=t1.ID
        JOIN dbo.jt_cgdh_mx t9 ON t8.ID=t9.ArriveNo and t9.ProductID=t3.cInvCCode
where  t1.BillStatus in (2,9) and t8.BillStatus in (2,9)
and  t1.CompanyCD='" + CompanyCD + @"'";
        string sqltop2 = @"select 
    t7.ContractId ,
	t9.custname,
	t3.productname,
    t8.iQuantity,
    t8.iMoney,
	t2.productcount as productcount,
    t2.SttlCount,
   (t2.ProductCount-t2.SttlCount) as wCount,
    t2.totalfee as totalfee,
    t2.SttlTotalPrice,
   (t2.totalfee - t2.SttlTotalPrice) as wFee,
    replace( convert(varchar(12),t7.SignDate,23),'1900-01-01','') SignDate ,
    replace( convert(varchar(12),t7.EffectiveDate,23),'1900-01-01','') EffectiveDate ,
    replace( convert(varchar(12),t7.EndDate,23),'1900-01-01','') EndDate ,
	t6.DeptName,
    t5.EmployeeName  
from  jt_xsfh t1  
 left join jt_xsfh_mx t2 on t1.id=t2.sendno
 	  left join officedba.ProductInfo t3 on t2.productid=t3.id  
      left join officedba.ProviderInfo t9 on t1.providerid=t9.id
	  left join officedba.CustInfo t4 on t1.custid=t4.id
	  left join officedba.EmployeeInfo t5 on t1.Seller=t5.id     
	  left join officedba.DeptInfo t6 on t6.id=t1.sellDeptId 
 	  left join ContractHead_pur t7 on t7.id=t1.PurContractID      
      JOIN dbo.ContractDetails_Pur t8 ON t7.id = t8.ContractID       
where t1.BillStatus=2 and t7.BillStatus in (2,9) and  t1.BusiType=2
and  t1.CompanyCD='" + CompanyCD + "'"; //这个是必须要有的

        if (CustName != null && CustName != "")//这样方便定制
        {
            sqltop1+=" and t4.CustName like '% " + CustName + "%'";            
            sqltop2+=" and  t9.CustName like '%" + CustName + "%'  ";            
        }
        if (Contractid != null && Contractid != "")
        {
            sqltop1 += " AND t1.Contractid  like '%" + Contractid + "%'";
            sqltop2 += " and  t7.ContractId like '%" + Contractid + "%'";
        }
        if (StartTime != null && StartTime != "")
        {
            sqltop1 += " and  t1.ConfirmDate>= '" + StartTime + "'";
            sqltop2 += " and  t7.ConfirmDate>= '" + StartTime + "'";
        }
        if (EndTime != null && EndTime != "")
        {
            sqltop1 += " and  t1.ConfirmDate<= '" + EndTime + "'";
            sqltop2 += " and  t7.ConfirmDate<= '" + EndTime + "'";
        }
        if (Dept != null && Dept != "")
        {
            sqltop1 += " AND  t6.DeptName = '" + Dept + "' ";
            sqltop2 += " and  t6.DeptName = '" + Dept + "' ";
        }
        
        string sql1 = @"select 
	'1' as ID,CustName,
	sum(iQuantity) iQuantity,
	sum(iMoney) iMoney,
	sum(ProductCount) ProductCount,
	sum(SttlCount) SttlCount,
	sum(wCount) wCount,
	sum(totalfee) totalfee,
	sum(SttlTotalPrice) SttlTotalPrice,
	sum(wFee) wFee
from (
select 
	CustName,
	iQuantity,
	iMoney,
	sum(ProductCount) ProductCount,
	sum(SttlCount) SttlCount,
	sum(wCount) wCount,
	sum(totalfee) totalfee,
	sum(SttlTotalPrice) SttlTotalPrice,
	sum(wFee) wFee
from (" + sqltop1 + " union all " + sqltop2 + @") as tempTable
group by CustName,ContractId,iQuantity,iMoney
)as result
group by CustName";
       
        #endregion
        DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql1.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
        #region dt数据处理区
        //对dt中的数据进行处理
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["ID"] = dt.Rows[i]["RowNumber"].ToString();
            ////为cCusCode添加A标签
            //dt.Rows[i]["cCusCode"] = "<a href='http://www.baidu.com'  class='opea'>" + dt.Rows[i]["cCusCode"].ToString() + "</a>"; ;            
        }
        //添加合计一列
        string sqlAllsun = @"select sum(iQuantity) iQuantity,
	sum(iMoney) iMoney,
	sum(ProductCount) ProductCount,
	sum(SttlCount) SttlCount,
	sum(wCount) wCount,
	sum(totalfee) totalfee,
	sum(SttlTotalPrice) SttlTotalPrice,
	sum(wFee) wFee from (" + sql1 + ") as Sunall";
        DataTable SummaryDT = SqlHelper.ExecuteSql(sqlAllsun);//汇总信息数据
        
        DataRow newRow = dt.NewRow();//在dt的基础上添加一行
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            for (int j = 0; j < SummaryDT.Columns.Count; j++)//需要统计的数据
            {
                if (dt.Columns[i].ColumnName == SummaryDT.Columns[j].ColumnName)
                {
                    newRow[i] = SummaryDT.Rows[0][j].ToString();
                }
            }
            if (dt.Columns[i].ColumnName == "ID")//显示的合计信息
            {
                newRow[i] = "合计";//这个事确定的
            }//没有的隐藏。
        }
        dt.Rows.Add(newRow);
        #endregion
        string res = TurnJson(pageIndex.ToString(), TotalCount.ToString(), dt);
        context.Response.Write(res);
        context.Response.End();        
    }

    
    
    /// <summary>
    /// 删除功能
    /// </summary>
    /// <param name="context"></param>
    public void Delete(HttpContext context)
    {
        string cCusCode = context.Request.Params["cCusCode"].ToString();
        string sql = "delete dbo.Customer where cCusCode='" + cCusCode + "'";
        string bo = "";
        try
        {
            SqlHelper.ExecuteSql(sql);
            bo = "success";
        }
        catch (Exception)
        {
            bo = "该数据被调用，无法删除";
        }
        context.Response.Write(bo);
        context.Response.End();
    }


    #region 将数据转换为flexigrid支持的格式
    /**
         * 1,必须有ID,默认从1开始
         * */

    /// <summary>
    /// 将数据转换为flexigrid支持的格式
    /// </summary>
    /// <param name="page">页</param>
    /// <param name="total">总数</param>
    /// <param name="dt"></param>
    /// <returns></returns>
    private string turn(string page, string total, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"page\":");
        sb.Append("\"" + page + "\",");
        sb.Append("\"total\":");
        sb.Append("\"" + total + "\"");
        sb.Append(",\"rows\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == (dt.Rows.Count - 1))
            {
                sb.Append("{\"cell\": {");
                for (int j = 0; j < dt.Columns.Count; j++)//对最后一个数据判断操作
                {
                    if (j == (dt.Columns.Count - 1))
                    {
                        string btn = "<input type='button' value='查看' id='sss' onclick='sss(" + dt.Rows[i][j].ToString() + ")' />";
                        string a = "<a href='http://www.baidu.com'  class='opea' onclick='sss(" + dt.Rows[i][j].ToString() + ")'>" + dt.Rows[i][j].ToString() + "</a>";
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + btn + "\"");
                    }
                    else if (dt.Columns[j].ColumnName == "cCusCode")
                    {
                        string a = "<a href='http://www.baidu.com'  class='opea' onclick='sss(" + dt.Rows[i][j].ToString() + ")'>" + dt.Rows[i][j].ToString() + "</a>";
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + a + "\",");
                    }
                    else
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j] + "\",");
                    }
                }
                sb.Append("}}");
            }
            else
            {
                sb.Append("{\"cell\": {");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == (dt.Columns.Count - 1))//对最后一个数据判断操作
                    {
                        //使用单引号比较适合,符合条件
                        string btn = "<input type='button' value='查看' id='sss' onclick='sss(" + dt.Rows[i][j].ToString() + ")' />";
                        string a = "<a href='http://www.baidu.com' class='opea' onclick='sss(" + dt.Rows[i][j].ToString() + ")'>" + dt.Rows[i][j].ToString() + "</a>";
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + btn + "\"");
                    }
                    else if (dt.Columns[j].ColumnName == "cCusCode")
                    {
                        //string a = "<a href=\'http://www.baidu.com\'  class=\'opea\' onclick=\'sss(" + dt.Rows[i][j].ToString() + ")\'>" + dt.Rows[i][j].ToString() + "</a>";
                        string a = "<a href='http://www.baidu.com'  class='opea' onclick='sss(" + dt.Rows[i][j].ToString() + ")'>" + dt.Rows[i][j].ToString() + "</a>";
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + a + "\",");
                    }
                    else
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j] + "\",");
                    }
                }
                sb.Append("}},");
            }
        }
        //sb.Append("[{\"cell\": {\"cCusCode\": \"a\",\"cCusName\": \"aa\"}}]");
        sb.Append("]}");
        return sb.ToString();
    }

    /// <summary>
    /// 将数据转换为flexigrid支持的格式,提取出来的公共方法可以调用
    /// </summary>
    /// <param name="page">页</param>
    /// <param name="total">总数</param>
    /// <param name="dt"></param>
    /// <returns></returns>
    private string TurnJson(string page, string total, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.Append("\"page\":");
        sb.Append("\"" + page + "\",");
        sb.Append("\"total\":");
        sb.Append("\"" + total + "\"");
        sb.Append(",\"rows\":[");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == (dt.Rows.Count - 1))
            {
                sb.Append("{\"cell\": {");
                for (int j = 0; j < dt.Columns.Count; j++)//对最后一个数据判断操作
                {
                    if (j == (dt.Columns.Count - 1))
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j].ToString() + "\"");
                    }
                    else
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j] + "\",");
                    }
                }
                sb.Append("}}");
            }
            else
            {
                sb.Append("{\"cell\": {");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == (dt.Columns.Count - 1))//对最后一个数据判断操作
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j].ToString() + "\"");
                    }
                    else
                    {
                        sb.Append("\"" + dt.Columns[j].ColumnName + "\": \"" + dt.Rows[i][j] + "\",");
                    }
                }
                sb.Append("}},");
            }
        }
        //sb.Append("[{\"cell\": {\"cCusCode\": \"a\",\"cCusName\": \"aa\"}}]");
        sb.Append("]}");
        return sb.ToString();
    }

    #endregion

   

    #region json转换  网上获取，有待修改。主要是返回的结构有问题数据无法对应
    /// <summary>
    /// JSON格式转换
    /// </summary>
    /// <param name="dt">DataTable表</param>
    /// <param name="page">当前页</param>
    /// <param name="total">总计多少行</param>
    /// <returns></returns>

    public static string DtToSON2(DataTable dt, string page, string total)
    {
        StringBuilder jsonString = new StringBuilder();
        //jsonString.AppendLine("{");
        jsonString.AppendFormat("\"page\": \"{0}\",\n", page);
        jsonString.AppendFormat("\"total\": \"{0}\",\n", total);
        jsonString.AppendLine("\"rows\": [");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            jsonString.Append("{");
            jsonString.AppendFormat("\"id\":\"{0}\",\"cell\":[", dt.Rows[i][0].ToString());
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (j == dt.Columns.Count - 1)
                {
                    jsonString.AppendFormat("\"{0}\"", dt.Rows[i][j].ToString());
                }
                else
                {
                    jsonString.AppendFormat("\"{0}\",", dt.Rows[i][j].ToString());
                }
                if (j == dt.Columns.Count - 1)
                {
                    jsonString.AppendFormat(",\"{0}\"", "<input type=\'button\' value=\'查看\' id=\'sss\' onclick=\'sss(" + dt.Rows[i][0].ToString() + ")\' />");
                }
            }
            jsonString.Append("]");
            if (i == dt.Rows.Count - 1)
            {
                jsonString.AppendLine("}");
            }
            else
            {
                jsonString.AppendLine("},");
            }
        }
        jsonString.Append("]");
        //jsonString.AppendLine("}");
        return jsonString.ToString();
    }

    #endregion


    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="context"></param>
    public void Search(HttpContext context)
    {
        string sql = "select cCusCode,cCusName,cCusAddress,cCusLPerson,dCusDevDate from dbo.Customer";
        DataTable dt = SqlHelper.ExecuteSql(sql);
        string res = dtToJson.DataTable2Json(dt);
        context.Response.Write(res);
        context.Response.End();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}