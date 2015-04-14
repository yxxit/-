using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Caching;

using XBase.Data.DBHelper;

namespace XBase.Data.Common
{
    public class SubStore
    {

        public static ArrayList PubSubStoreList = new ArrayList();

        #region 验证当前用户是 最近父级是分公司或者分店
        public static bool IsSubStore(int DeptID, string CompanyCD)
        {
            //第一步 验证当前用户所属组织机构 是否为 分店或者 分公司 
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM officedba.DeptInfo ");
            sbSql.AppendLine(" WHERE ID=@ID");
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = SqlHelper.GetSqlParameter("@ID", DeptID, SqlDbType.Int);
            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt != null && dt.Rows.Count > 0)
            {
                //SuperDeptID 为空的是总公司
                if (dt.Rows[0]["SuperDeptID"] == null || dt.Rows[0]["SuperDeptID"].ToString() == "")
                {
                    return true;
                }
                else
                {
                    //如果是 返回true
                    if (dt.Rows[0]["SubFlag"].ToString() == "1" || dt.Rows[0]["SaleFlag"].ToString() == "1")
                    {
                        //是分公司或者分店
                        return true;
                    }
                    //否则递归求出最近的父级分店ID 或者分公司ID 
                    else
                    {
                        //递归
                        return IsSubStore(Convert.ToInt32(dt.Rows[0]["SuperDeptID"].ToString()), dt.Rows[0]["CompanyCD"].ToString());
                    }
                }
            }
            else
                return false;
        }
        #endregion

        //#region 获取分店列表
        //public static DataTable GetSubSotre(string CompanyCD,string DeptID)
        //{
        //    StringBuilder sbSql = new StringBuilder();
        //    sbSql.AppendLine("SELECT * FROM officedba.DeptInfo AS a ");
        //    sbSql.AppendLine(" WHERE a.CompanyCD=@CompanyCD AND a.SaleFalg='1' ");

        //    SqlParameter[] Params = new SqlParameter[1];
        //    Params[0] = XBase.Data.DBHelper.SqlHelper.GetSqlParameter("@CompanyCD", CompanyCD, SqlDbType.VarChar);
        //    DataTable dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(sbSql.ToString(), Params);

        //    return dt;

        //}
        //#endregion

        #region 获取从属于当前分店或者分公司下的所有分店ID和分公司ID列表
        public static ArrayList GetSubStoreIDList(string DeptID, ArrayList SubStoreList)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM officedba.DeptInfo ");
            sbSql.AppendLine(" WHERE SuperDeptID=@DeptID AND (SubFlag='1' OR SaleFlag='1' )");

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = SqlHelper.GetSqlParameter("@DeptID", DeptID, SqlDbType.Int);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);
            ArrayList tmpList = new ArrayList();
            foreach (DataRow row in dt.Rows)
            {
                //将是分店或者分公司的DeptID 加入到列表
                SubStoreList.Add(row["ID"].ToString());
                //递归
                SubStoreList = GetSubStoreIDList(row["ID"].ToString(), SubStoreList);
            }
            return SubStoreList;
        }
        #endregion



        #region 获取从属于当前分店或者分公司下的所有分店ID和分公司ID列表
        public static ArrayList GetPubSubStoreIDList(string DeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM officedba.DeptInfo ");
            sbSql.AppendLine(" WHERE SuperDeptID=@DeptID ");

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = SqlHelper.GetSqlParameter("@DeptID", DeptID, SqlDbType.Int);
            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            foreach (DataRow row in dt.Rows)
            {
                if (row["SubFlag"].ToString() == "1" || row["SaleFlag"].ToString() == "1")
                {
                    PubSubStoreList.Add(row["ID"].ToString());
                }

                GetPubSubStoreIDList(row["ID"].ToString());
            }

            return PubSubStoreList;
        }
        #endregion

        #region 查询某公司下所有的分店或者分公司
        public static ArrayList GetAllSubStore(string companyCD)
        {
            ArrayList list = new ArrayList();
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT * FROM officedba.DeptInfo WHERE CompanyCD=@CompanyCD AND (SubFlag=1 OR SaleFlag=1 OR SuperDeptID IS NULL)");
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["ID"].ToString());
            }
            return list;
        }
        #endregion



        #region 获取当前用户的分店ID或分公司ID
        public static string GetSubStoreID(string CurrentDeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM officedba.DeptInfo ");
            sbSql.AppendLine(" WHERE  ID=@ID ");

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = XBase.Data.DBHelper.SqlHelper.GetSqlParameter("@ID", CurrentDeptID, SqlDbType.Int);

            DataTable dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(sbSql.ToString(), Params);
            if (dt != null && dt.Rows.Count > 0)
            {

                //SuperDeptID 为空的是总公司
                if (dt.Rows[0]["SuperDeptID"] == null || dt.Rows[0]["SuperDeptID"].ToString() == "")
                {
                    return dt.Rows[0]["ID"].ToString();
                }
                else
                {
                    //验证是否为分公司或者分店
                    if (dt.Rows[0]["subFlag"].ToString() == "1" || dt.Rows[0]["saleflag"].ToString() == "1")
                    {
                        return dt.Rows[0]["ID"].ToString();
                    }
                    else
                    {
                        //递归
                        return GetSubStoreID(dt.Rows[0]["SuperDeptID"].ToString());
                    }
                }
            }
            else
                return "-1";


        }

        #endregion

        #region 判断是总店还是分店
        public static bool GetComOrSub(string CurrentDeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * FROM officedba.DeptInfo ");
            sbSql.AppendLine(" WHERE  ID=@ID ");

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = XBase.Data.DBHelper.SqlHelper.GetSqlParameter("@ID", CurrentDeptID, SqlDbType.Int);

            DataTable dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(sbSql.ToString(), Params);
            if (dt != null && dt.Rows.Count > 0)
            {

                //SuperDeptID 为空的是总公司
                if (dt.Rows[0]["SuperDeptID"] == null || dt.Rows[0]["SuperDeptID"].ToString() == "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return true;


        }

        #endregion

        #region 取顶级公司DeptID
        public static string GetTopDeptID(string companyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT ID FROM officedba.DeptInfo WHERE CompanyCD=@CompanyCD AND SuperDeptID IS NULL");
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), sqlParams);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["ID"].ToString();
            else
                return "0";
        }
        #endregion


        #region 获取分店名称
        public static string GetSubStoreName(string DeptID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT DeptName FROM officedba.DeptInfo WHERE ID=@DeptID ");
            SqlParameter[] Params = new SqlParameter[1];
            int index = 0;
            Params[index++] = SqlHelper.GetSqlParameter("@DeptID", DeptID, SqlDbType.Int);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["DeptName"].ToString();
            else
                return string.Empty;
        }
        #endregion

        #region 读取指定公司的组织机构
        public static DataTable GetAllDept(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT * FROM officedba.DeptInfo WHERE CompanyCD=@CompanyCD ");
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = SqlHelper.GetSqlParameter("@CompanyCD", CompanyCD, SqlDbType.VarChar);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion



    }
}
