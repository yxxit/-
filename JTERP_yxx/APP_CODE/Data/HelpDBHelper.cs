/**********************************************
 * 类作用：   帮助
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/18
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
namespace XBase.Data
{
    
    public class HelpDBHelper
    {
        #region 获取帮助信息

        /// <summary>
        /// 获取帮助信息
        /// </summary>
        /// <param name="ModuleID">ModuleID</param>
        /// <returns>DataTable 帮助信息</returns>
        public static DataTable HelpInfo(string ModuleID)
        {//检索菜单项SQL文

            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT								");
            searchSql.AppendLine("  Description	");
            searchSql.AppendLine("FROM                                  ");
            searchSql.AppendLine("	pubdba.HelpFeld       ");
            searchSql.AppendLine("WHERE                                 ");
            searchSql.AppendLine("	ModuleID =@ModuleID        ");

            //设置参数
            SqlParameter[] p = new SqlParameter[1];
            p[0] = SqlHelper.GetParameter("@ModuleID", ModuleID);

            return SqlHelper.ExecuteSql(searchSql.ToString(), p);
        }

        #endregion

       
    }
}
