/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.03.19
 * 描    述： 字段唯一性验证
 * 修改日期： 2009.03.19
 * 版    本： 0.1.0
 ***********************************************/
using System.Data;
using XBase.Data.Common;
using XBase.Common;
using System.Text;

namespace XBase.Business.Common
{
    /// <summary>
    /// 类名：PrimekeyVerifyBus
    /// 描述：字段唯一性验证
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/19
    /// 最后修改时间：2009/03/19
    /// </summary>
    ///
    public class PrimekeyVerifyBus
    {
        #region 校验编号的唯一性
        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue)
        {
            string companyCD = string.Empty;

            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;



            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq(tableName, columnName, codeValue, companyCD);            
        }


        /// <summary>
        /// 校验编号的唯一性-金泰恒业
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq_jt(string tableName, string columnName, string codeValue)
        {
            string companyCD = string.Empty;

            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;



            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq_jt(tableName, columnName, codeValue, companyCD);
        }


        /// <summary>
        /// 校验编号的唯一性
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCodeUniq1(string tableName, string columnName, string codeValue, string Flag)
        {
            string companyCD = string.Empty;

            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;



            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq1(tableName, columnName, codeValue, companyCD, Flag);
        }
        public static bool CheckCodeUniq(string tableName, string columnName, string codeValue, string Condition)
        {
            string companyCD = string.Empty;

            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;



            //校验存在性
            return PrimekeyVerifyDBHelper.CheckCodeUniq(tableName, columnName, codeValue, companyCD, Condition);
        }
        /// <summary>
        /// 不根据企业编码验证唯一
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="codeValue"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static bool CheckUserUniq(string tableName, string columnName, string codeValue)
        {
            return PrimekeyVerifyDBHelper.CheckUserUniq(tableName, columnName, codeValue);
        }
        public static bool CheckUserUniq1(string tableName, string columnName, string codeValue, string CompanyCD)
        {
            return PrimekeyVerifyDBHelper.CheckUserUniq1(tableName, columnName, codeValue, CompanyCD);
        }
        #endregion

        #region 检验是否被引用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">列名</param>
        /// <param name="codeValue">输入的编码值</param>
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static DataTable CheckQuote(string tableName, string columnName, string codeValue)
        {
            string companyCD = string.Empty;

            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;



            //校验存在性
            return PrimekeyVerifyDBHelper.CheckQuote(tableName, columnName, codeValue, companyCD);
        }
        #endregion
        //检查供应商是否能被删除
        public static bool isdel(string[] id)
        {
            return PrimekeyVerifyDBHelper.isdel(id);
        }

        //检验YY客户是否已存在
        public static bool CheckYYcustExist(string tableName, string columnName, string codeValue)
        {
            return PrimekeyVerifyDBHelper.CheckYYcustExist(tableName, columnName, codeValue);
        }
    }
}
