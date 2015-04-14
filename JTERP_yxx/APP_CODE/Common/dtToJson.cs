/**********************************************
 * 类作用：   Data数据转换为json数据
 *            或者json转换为Data数据
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace XBase.Common
{
    /// <summary>
    /// Data数据转换为json数据
    /// </summary>
    public class dtToJson
    {
        int orderid;
        string data1;
        int issuc;
        string msg;
        string orderno;
        public int ID
        {
            get { return orderid; }
            set { this.orderid = value; }
        }
        public string NO
        {
            get { return orderno; }
            set { this.orderno = value; }
        }
        public int Sta1
        {
            get { return issuc; }
            set { this.issuc = value; }
        }
        public string Data1
        {
            get { return data1; }
            set { this.data1 = value; }
        }
        public string Msg
        {
            get { return msg; }
            set { this.msg = value; }
        }

        public  string ToJosnString()
        {
            return "{\"data\":\"" + data1 + "\",\"id\":" + orderid + ",\"sta\":" + issuc + ",\"Msg\":\"" + msg + "\",\"no\":\"" + orderno + "\"}";
        }



        //手动输出JSON格式的数据 
        public dtToJson(string info, string data, int sta)
        {
            this.info = info;
            this.data = data;
            this.sta = sta;
        }

        string info;
        string data;
        int sta;
        public string Info
        {
            get { return info; }
            set { this.info = value; }
        }
        public string Data
        {
            get { return data; }
            set { this.data = value; }
        }
        public int Sta
        {
            get { return sta; }
            set { this.sta = value; }
        }
        //重写ToString()方法，以便输出格式是标准的JSON格式 
        public override string ToString()
        {
            return "{\"data\":\"" + data + "\",\"info\":\"" + info + "\",\"sta\":" + sta + "}";
        }


        /// <summary>
        /// DataTable转换为json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTable2Json(DataTable dt)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();
            jsonBuilder.Append("[");
            if (dt.Rows.Count == 0)
            {
                jsonBuilder.Append("]");
                return jsonBuilder.ToString();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    try
                    {
                        jsonBuilder.Append((dt.Rows[i][j].ToString().Replace("\"", "\\\"")).Replace("\n", "\\r\\n"));
                    }
                    catch
                    {
                        jsonBuilder.Append("");
                    }
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }
        public static string ConvertTextToHtml(string chr)
        {
            if (chr == null)
                return "";
            chr = chr.Replace("\n", "<Enter>");
            return (chr);
        }

        public static string FormatDataTableToJson(DataTable dt, int TotalCount)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{totalCount:'" + TotalCount.ToString() + "',data:");
            sb.Append(DataTable2Json(dt));
            sb.Append("}");

            return sb.ToString();
        }

        /*包含格式化日期参数*/
        public static string DataTableToJson(DataTable dt, int TotalCount)
        {
            System.Text.StringBuilder jsonBuilder = new System.Text.StringBuilder();


            jsonBuilder.Append("{totalCount:'" + TotalCount.ToString() + "',data:");
            
            jsonBuilder.Append("[");
            if (dt.Rows.Count == 0)
            {
                jsonBuilder.Append("]");
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append(":\"");
                    try
                    {
                        /*根据datatable列的数据格式 来格式化字符*/
                        string tmp = string.Empty;
                        if (dt.Columns[j].DataType.ToString() == "System.Int16" || dt.Columns[j].DataType.ToString() == "System.Int32" || dt.Columns[j].DataType.ToString() == "System.Int64")
                            tmp = dt.Rows[i][j].ToString();
                        else if (dt.Columns[j].DataType.ToString() == "System.Decimal")
                            tmp = dt.Rows[i][j].ToString();
                        else if (dt.Columns[j].DataType.ToString() == "System.DateTime")
                        {
                            if (dt.Rows[i][j] != null && dt.Rows[i][j].ToString() != "")
                            {
                                tmp = (Convert.ToDateTime(dt.Rows[i][j].ToString())).ToString("yyyy-MM-dd");
                            }
                        }
                        else
                            tmp = dt.Rows[i][j].ToString();
                        jsonBuilder.Append(GetSafeJSONString(tmp));//.Replace("\"","\\\""));
                    }
                    catch
                    {
                        jsonBuilder.Append("");
                    }
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");



            jsonBuilder.Append("}");

            return jsonBuilder.ToString();
        }


        private static System.Text.RegularExpressions.Regex safeJSON = new System.Text.RegularExpressions.Regex("[\\n\\r]");
        protected static string GetSafeJSONString(string input)
        {
            string output = input.Replace("\"", "\\\"");
            output = safeJSON.Replace(output, "<br>");

            return output;

        }

        // <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        public static  DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "△\"").Replace("\":", "\"#").ToString();//修改一下*转换为△2014-12-15
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            //string strName = rg.Match(strJson).Value;
            string strName = "tbtmp";
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('△');//*转换为△2014-12-15

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

    }
    

    
     
  
   

    

    
}
