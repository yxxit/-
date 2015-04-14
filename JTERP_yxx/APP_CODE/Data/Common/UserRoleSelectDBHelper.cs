/**********************************************
 * 类名：UserRoleSelectDBHelper
 * 描述：处理角色选择页面的业务处理
 * 作者：钱锋锋
 * 创建时间：2010/08/19
 ***********************************************/
using System.Data;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：UserRoleSelectDBHelper
    /// 描述：处理角色选择页面的业务处理
    /// 
    /// 作者：钱锋锋
    /// 创建时间：2010/08/19    
    /// </summary>
    ///
    public class UserRoleSelectDBHelper
    {

        

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetRoleInfo(string companyCD)
        {
           StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(" SELECT                    "); 
            searchSql.AppendLine(" 	RoleID                    ");
            searchSql.AppendLine(" 	,ltrim(SUBSTRING(SuperRoleID,2,CHARINDEX(' ',SuperRoleID+' ',2)-1 ))  as SuperRoleID       ");
            searchSql.AppendLine(" 	,RoleName              ");
            searchSql.AppendLine(" 		FROM                 ");
            searchSql.AppendLine(" 		officedba.RoleInfo ");
            searchSql.AppendLine(" 		WHERE                ");
            searchSql.AppendLine(" 	CompanyCD = @CompanyCD ");
           

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        
    }
}
