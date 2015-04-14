/**********************************************
 * 类作用：   快捷菜单的添加与删除
 * 建立人：   刘朋
 * 建立时间： 2010/09/17
 ***********************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Text;
using System.Collections;
using XBase.Model.Common;


namespace XBase.Data.Common
{
    public class QukMenuDBHelper
    {
        #region 添加快捷菜单数据
        /// <summary>
        /// 添加快捷菜单数据
        /// </summary>
        
        public static bool QukMenuAdd(QukMenuModel model)
        {
            //定义插入SQL变量
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.AppendLine("INSERT INTO dbo.QuickMenu ");
            sqlInsert.AppendLine("           ( QukM_ID   ");
            sqlInsert.AppendLine("           , CompanyCD    ");
            sqlInsert.AppendLine("		   	  , UserID            ");
            sqlInsert.AppendLine("           , MenuAddTime  )               ");
            
            sqlInsert.AppendLine("     VALUES                      ");
            sqlInsert.AppendLine("           (@QukM_ID            ");
            sqlInsert.AppendLine("           ,@CompanyCD            ");
            sqlInsert.AppendLine("           ,@UserID           ");
            sqlInsert.AppendLine("           ,@MenuAddTime )           ");
            

            //设置参数
            SqlParameter[] param = new SqlParameter[4];
            int i = 0;
            param[i++] = SqlHelper.GetParameter("@QukM_ID", model.QukM_ID);
            //快捷菜单名称
            param[i++] = SqlHelper.GetParameter("@CompanyCD", model.CompanyCD);
            //链接
            param[i++] = SqlHelper.GetParameter("@UserID", model.UserID);
            //位置
            param[i++] = SqlHelper.GetParameter("@MenuAddTime", model.MenuAddTime);
            
            

            //执行插入
            return SqlHelper.ExecuteTransSql(sqlInsert.ToString(), param) > 0 ? true : false;
       
        }
        #endregion

        #region 获取快捷菜单数据
        /// <summary>
        /// 获取快捷菜单数据
        /// </summary>

        public static DataTable QukMenuSelect(string CompanyCD, string UserID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("select top 7 a.QukM_ID");
            selectSql.AppendLine(",a.MenuAddTime ");
            selectSql.AppendLine(",b.ModuleName,b.PropertyValue");
            selectSql.AppendLine(" from dbo.QuickMenu as a ");

            selectSql.AppendLine(" left join  pubdba.SysModule as b on a.QukM_ID=ModuleID");
            selectSql.AppendLine("where a.CompanyCD =@CompanyCD");
            selectSql.AppendLine("and a.UserID =@UserID ");
            selectSql.AppendLine("order by a.MenuAddTime desc");
            SqlParameter[] param = new SqlParameter[2];
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[1] = SqlHelper.GetParameter("@UserID", UserID);
            return SqlHelper.ExecuteSql(selectSql.ToString(),param);
        }
        #endregion
        public static bool QukMenuSelect(string QukM_ID, string CompanyCD, string UserID)
        {
            StringBuilder selectSql = new StringBuilder();
            selectSql.AppendLine("select ");
            selectSql.AppendLine(" *");
            selectSql.AppendLine(" from dbo.QuickMenu as a ");

            selectSql.AppendLine("where a.QukM_ID =@QukM_ID");
            selectSql.AppendLine("and a.CompanyCD =@CompanyCD");
            selectSql.AppendLine("and a.UserID =@UserID");

            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@QukM_ID", QukM_ID);
            param[1] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            param[2] = SqlHelper.GetParameter("@UserID", UserID);

            return SqlHelper.Exists(selectSql.ToString(), param);
        }
        

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuUpdate(QukMenuModel model)
        {

            #region 更新SQL拼写
            StringBuilder updateSql = new StringBuilder();
            updateSql.AppendLine(" UPDATE dbo.QuickMenu         ");
            updateSql.AppendLine(" SET                                ");
            updateSql.AppendLine(" 	 QukM_ID = @QukM_ID               ");
            updateSql.AppendLine(" 	,CompanyCD = @CompanyCD             ");
            updateSql.AppendLine(" 	,UserID = @UserID       ");
            updateSql.AppendLine(" 	,MenuAddTime = '" + model.MenuAddTime + " '            ");

            updateSql.AppendLine(" WHERE                              ");
            updateSql.AppendLine(" 	CompanyCD = @CompanyCD            ");
            updateSql.AppendLine(" 	AND UserID = @UserID              ");
            updateSql.AppendLine(" 	AND QukM_ID = @QukM_ID              ");
            #endregion

            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            comm.CommandText = updateSql.ToString();
            //设置保存的参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@QukM_ID", model.QukM_ID));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));            
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", model.UserID));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@MenuAddTime", model.MenuAddTime.ToString()));
            //执行插入操作并返回更新结果
            return SqlHelper.ExecuteTransWithCommand(comm);
        }

        #endregion
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuDel(string QukMenuName)
        {

            ArrayList listADD = new ArrayList();
            string[] arrID = QukMenuName.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 1; i < arrID.Length; i++)
                {
                    if (arrID[i] != "")
                    {
                        StringBuilder sqlDet = new StringBuilder();
                        sqlDet.AppendLine(" delete from dbo.QuickMenu where QukMenuName=@QukMenuName");
                        SqlCommand commDet = new SqlCommand();
                        commDet.Parameters.Add(SqlHelper.GetParameter("@QukMenuName", arrID[i].ToString()));
                        commDet.CommandText = sqlDet.ToString();

                        listADD.Add(commDet);

                    }
                }
            }
            
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
    }
}
