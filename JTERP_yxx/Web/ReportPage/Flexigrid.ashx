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
            if (Action == "SearchAll")
            {
                string today = context.Request.Params["today"];

                #region 报表获取参数，参考
                string rp = context.Request.Params["rp"];//每页显示数量11
                string page = context.Request.Params["page"];//页11
                string sortname = context.Request.Params["sortname"];//自定义排序
                string sortorder = context.Request.Params["sortorder"];//排序类型11

                #region 筛选条件 按顺序
                string cCusCode = "";
                string cCusName = "";
                string query = context.Request.Params["query"];//搜索查询的条件,将条件统一
                if (query != null && query != "")
                {
                    string[] list = query.Split('#');
                    cCusCode = list[0];
                    cCusName = list[1];
                }
                #endregion


                string qtype = context.Request.Params["qtype"]; //搜索查询的类别
                #endregion

                #region 我们项目中的获取方法,有用
                //设置行为参数
                string orderString = context.Request.Params["sortname"].ToString();//排序
                string order = context.Request.Params["sortorder"].ToString();//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString : "ID";//要排序的字段，如果为空，默认为"ID"
                //if (orderString.EndsWith("_d"))
                //{
                //    order = "asc";//排序：降序
                //}
                int pageCount = int.Parse(context.Request.Params["rp"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Params["page"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                string ord = orderBy + " " + order;
                int TotalCount = 0;//
                #endregion
                string sql = "select 1 as ID,cCusCode,cCusName,cCusAddress,dCusDevDate,cCusCode as ope from dbo.Customer  where 1=1  ";
                if (cCusCode != null && cCusCode != "")
                {
                    sql += "and cCusCode like '%" + cCusCode + "%' ";
                }
                if (cCusName != null && cCusName != "")
                {
                    sql += "and cCusName like '%" + cCusName + "%' ";
                }
                DataTable dt = SqlHelper.CreateSqlByPageExcuteSql(sql.ToString(), pageIndex, pageCount, ord, null, ref TotalCount);
                #region 在表中添加一个序列 ID 不再使用
                //DataColumn dc = new DataColumn("ID", System.Type.GetType("System.Int32"));
                //dt.Columns.Add(dc);
                //dt.Columns.Add("WD", System.Type.GetType("System.String"));
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    int a = i + 1;
                //    dt.Rows[i]["ID"] = a;
                //}
                #endregion
                //string res = turn(pageIndex.ToString(), TotalCount.ToString(), dt);
                //对dt中的数据进行处理
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int a = i + 1;
                    dt.Rows[i]["ID"] = a;
                    //为cCusCode添加A标签
                    dt.Rows[i]["cCusCode"] = "<a href='http://www.baidu.com'  class='opea'>" + dt.Rows[i]["cCusCode"].ToString() + "</a>"; ;
                    //为ope添加操作
                    dt.Rows[i]["ope"] = "<input type='button' value='查看' id='sss' onclick='sss(" + dt.Rows[i]["ope"].ToString() + ")' />";
                }

                string res = TurnJson(pageIndex.ToString(), TotalCount.ToString(), dt);
                context.Response.Write(res);
                context.Response.End();
            }
            else if (Action == "Delete")
            {
                Delete(context);
            }
        }
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