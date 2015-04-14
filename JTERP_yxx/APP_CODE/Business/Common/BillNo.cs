using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace XBase.Business.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class BillNo
    {

        /// <summary>
        /// 自动生成单据编号 格式 前缀+yyMMdd+4位流水号
        /// 每日的流水号从0001开始统计
        /// </summary>
        /// <param name="prefixStr">前缀 为具体 单据拼音缩写 2位，例如采购退货单为CT  前缀请看文档</param>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名 即单据编号列</param>
        /// <param name="CommpanyCD">公司编码</param>
        /// <param name="BrachID">分店ID 总店为0</param>
        /// <returns>新单据编号</returns>
        public static string Create(string prefixStr, string tableName, string colName, string CompanyCD, int BrachID)
        {
            DataTable dt = Data.Common.BillNo.Create(prefixStr, tableName, colName, CompanyCD, BrachID);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return prefixStr + DateTime.Now.ToString("yyMMdd") + "0001";
            }
            else
            {
                string LastNo = dt.Rows[0][colName].ToString();

                string NewNo = prefixStr + DateTime.Now.ToString("yyMMdd") + GetLastNo(int.Parse(LastNo.Substring(LastNo.Length - 4, 4)));
                if (Data.Common.BillNo.Validate(NewNo, tableName, colName, CompanyCD, BrachID))
                    return NewNo;
                else
                    return Create(prefixStr, tableName, colName, CompanyCD, BrachID);
            }
        }


        /// <summary>
        /// 验证单据编号是否重复
        /// </summary>
        /// <param name="No"></param>
        /// <param name="tableName"></param>
        /// <param name="colName"></param>
        /// <param name="CompanyCD"></param>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public static bool Validate(string No, string tableName, string colName, string CompanyCD, int BranchID)
        {
            return XBase.Data.Common.BillNo.Validate(No, tableName, colName, CompanyCD, BranchID);
        }

        private static string GetLastNo(int no)
        {
            no++;
            switch (no.ToString().Length)
            {
                case 1:
                    return "000" + no.ToString();
                case 2:
                    return "00" + no.ToString();
                case 3:
                    return "0" + no.ToString();
                default:
                    return no.ToString();
            }
        }




    }
}
