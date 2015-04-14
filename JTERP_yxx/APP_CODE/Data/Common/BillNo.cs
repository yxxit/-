using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace XBase.Data.Common
{
    public class BillNo
    {
        /// <summary>
        /// 自动生成单据编号 格式 前缀+yyMMdd+4位流水号
        /// 每日的流水号重0001开始统计
        /// </summary>
        /// <param name="prefixStr">前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名 即单据编号列</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns>dt</returns>
        public static DataTable Create(string prefixStr, string tableName, string colName, string CompanyCD, int BranchID)
        {
            string tmpNo = prefixStr + DateTime.Now.ToString("yyMMdd");
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT TOP 1 " + colName + " From " + tableName);
            // sbSql.AppendLine(" WHERE  CompanyCD=@CompanyCD AND BranchID=@BranchID  AND  " + colName + " LIKE @TmpNo  ORDER BY ID DESC ");
            sbSql.AppendLine(" WHERE  CompanyCD=@CompanyCD AND  " + colName + " LIKE @TmpNo  ORDER BY ID DESC ");
            SqlParameter[] Params = new SqlParameter[2];
            int index = 0;
            Params[index++] = Data.DBHelper.SqlHelper.GetSqlParameter("@CompanyCD", CompanyCD, SqlDbType.VarChar);
            Params[index++] = Data.DBHelper.SqlHelper.GetSqlParameter("@TmpNo", tmpNo + "%", SqlDbType.VarChar);
            //Params[index++] = Data.DBHelper.SqlHelper.GetSqlParameter("@BranchID", BranchID, SqlDbType.Int);

            DataTable dt = Data.DBHelper.SqlHelper.ExecuteSql(sbSql.ToString(), Params);
            return dt;
        }

        /// <summary>
        /// 验证单据编号 是否重复
        /// </summary>
        /// <param name="No">新单据百年好</param>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名</param>
        /// <param name="CompanyCD">公司编码</param>
        /// <returns></returns>
        public static bool Validate(string No, string tableName, string colName, string CompanyCD, int BranchID)
        {
            StringBuilder sbSql = new StringBuilder();
            //sbSql.AppendLine("SELECT TOP 1 * FROM " + tableName + " WHERE " + colName + "='" + No + "' AND BranchID="+BranchID.ToString()+" AND CompanyCD='" + CompanyCD + "'");
            sbSql.AppendLine("SELECT TOP 1 * FROM " + tableName + " WHERE " + colName + "='" + No + "' AND CompanyCD='" + CompanyCD + "'");
            DataTable dt = Data.DBHelper.SqlHelper.ExecuteSql(sbSql.ToString());

            if (dt == null || dt.Rows.Count <= 0)
                return true;
            else
                return false;

        }
    }
}
