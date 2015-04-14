/**********************************************
 * 作    者： 吴志强
 * 创建日期： 2009.03.04
 * 描    述： 员工以及部门选择
 * 修改日期： 2009.03.04
 * 版    本： 0.5.0
 ***********************************************/
using System.Data;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.Common
{
    /// <summary>
    /// 类名：UserDeptSelectDBHelper
    /// 描述：处理员工部门选择页面的业务处理
    /// 
    /// 作者：吴志强
    /// 创建时间：2009/03/04
    /// 最后修改时间：2009/03/04
    /// </summary>
    ///
    public class UserDeptSelectDBHelper
    {

        #region 获取分公司信息
        public static DataTable GetSubCompanyinfo(string CompanyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID ");
            selectSql.AppendLine(" , SuperDeptID, ");
            selectSql.AppendLine("  DeptName");
            selectSql.AppendLine(" FROM officedba.DeptInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND UsedStatus = @UsedStatus and subflag !=0   ");

            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);



        }
        #endregion

        #region 获取部门信息
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 部门信息</returns>
        public static DataTable GetDeptInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.AppendLine(" SELECT ID, CompanyCD, DeptNO ");
            selectSql.AppendLine(" , SuperDeptID, PYShort ");
            selectSql.AppendLine(" , DeptName, AccountFlag,'' as isFlag,subflag ");
            selectSql.AppendLine(" FROM officedba.DeptInfo ");
            selectSql.AppendLine(" WHERE ");
            selectSql.AppendLine(" CompanyCD = @CompanyCD ");
            selectSql.AppendLine(" AND UsedStatus = @UsedStatus ");
            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //启用状态
            param[1] = SqlHelper.GetParameter("@UsedStatus", "1");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        #region 获取员工信息
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            //selectSql.AppendLine(" SELECT A.EmployeesID AS EmployeesID ");
            //selectSql.AppendLine(" , ISNULL(B.EmployeeName,'') AS EmployeesName ");
            //selectSql.AppendLine(" , A.NowDeptID AS DeptID ");
            //selectSql.AppendLine(" FROM officedba.EmployeeJob AS A LEFT JOIN ");
            //selectSql.AppendLine(" officedba.EmployeeInfo AS B ON ");
            //selectSql.AppendLine(" A.CompanyCD = B.CompanyCD AND A.EmployeesID = B.ID ");
            //selectSql.AppendLine(" WHERE ");
            //selectSql.AppendLine(" A.CompanyCD = @CompanyCD ");
            //selectSql.AppendLine(" AND A.Flag <> @Flag ");


            //selectSql.AppendLine("select ID,ISNULL(EmployeeName,'') AS EmployeesName,Flag,");
            //selectSql.AppendLine("isnull(cast (DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo ");
            //selectSql.AppendLine("where CompanyCD=@CompanyCD and Flag!='2' and DeptID Is not null  ");

            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID,isnull(b.DeptName,'') as DeptName    from officedba.EmployeeInfo  as a");
            selectSql.AppendLine("left join officedba.deptinfo  as b on a.DeptID=b.ID");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null  ");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
          //  param[1] = SqlHelper.GetParameter("@Flag", "3");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        //add by zyy  ---start
        #region 获取员工信息,区分显示本公司还是客户员工
        /// <summary>
        /// 获取员工信息，区分显示本公司还是客户员工
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfoDiff(string companyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句

            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,a.CorrLinkMan, ");
            selectSql.AppendLine(" c.CustID,isnull(c.CustNo,'')CustNo,isnull(c.CustName,'')CustName, ");
            selectSql.AppendLine(" isnull(cast (a.DeptID as varchar),'')  as  DeptID,isnull(b.DeptName,'') as DeptName ");
            selectSql.AppendLine(" from officedba.EmployeeInfo  as a");
            selectSql.AppendLine(" left join officedba.deptinfo  as b on a.DeptID=b.ID ");
            selectSql.AppendLine(" left join ( select ci.ID CustID,ci.CustNo,ci.CustName,ci.CompanyCD,cl.ID as CorrID from officedba.CustInfo  ci ");
            selectSql.AppendLine(" left join officedba.CustLinkMan cl on cl.CompanyCD=ci.CompanyCD and cl.CustNo=ci.CustNo ");
            selectSql.AppendLine(" where ci.CompanyCD=@CompanyCD  and cl.ID in (select CorrLinkMan from officedba.EmployeeInfo  where CompanyCD=@CompanyCD  and Flag!='2' ) ) c ");
            selectSql.AppendLine(" on c.companycd=a.companycd and c.CorrID=a.CorrLinkMan where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null  ");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion
        #region 获取员工信息--去除已离职人员
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfoDiff(string companyCD, string flag)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,a.CorrLinkMan,");
            selectSql.AppendLine(" c.CustID,isnull(c.CustNo,'')CustNo,isnull(c.CustName,'')CustName, ");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID,isnull(b.DeptName,'') as DeptName ");
            selectSql.AppendLine(" from officedba.EmployeeInfo  as a ");
            selectSql.AppendLine(" left join officedba.deptinfo  as b on a.DeptID=b.ID");
            selectSql.AppendLine(" left join ( select ci.ID CustID,ci.CustNo,ci.CustName,ci.CompanyCD,cl.ID as CorrID from officedba.CustInfo  ci ");
            selectSql.AppendLine(" left join officedba.CustLinkMan cl on cl.CompanyCD=ci.CompanyCD and cl.CustNo=ci.CustNo ");
            selectSql.AppendLine(" where ci.CompanyCD=@CompanyCD  and cl.ID in (select CorrLinkMan from officedba.EmployeeInfo  where CompanyCD=@CompanyCD  and Flag!='2' and Flag<>'3' ) ) c ");
            selectSql.AppendLine(" on c.companycd=a.companycd and c.CorrID=a.CorrLinkMan where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3'  ");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
 
            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion
        //add by zyy---end

        #region 获取员工信息--去除已离职人员
        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 员工信息</returns>
        public static DataTable GetUserInfo(string companyCD,string flag)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            //selectSql.AppendLine(" SELECT A.EmployeesID AS EmployeesID ");
            //selectSql.AppendLine(" , ISNULL(B.EmployeeName,'') AS EmployeesName ");
            //selectSql.AppendLine(" , A.NowDeptID AS DeptID ");
            //selectSql.AppendLine(" FROM officedba.EmployeeJob AS A LEFT JOIN ");
            //selectSql.AppendLine(" officedba.EmployeeInfo AS B ON ");
            //selectSql.AppendLine(" A.CompanyCD = B.CompanyCD AND A.EmployeesID = B.ID ");
            //selectSql.AppendLine(" WHERE ");
            //selectSql.AppendLine(" A.CompanyCD = @CompanyCD ");
            //selectSql.AppendLine(" AND A.Flag <> @Flag ");


            //selectSql.AppendLine("select ID,ISNULL(EmployeeName,'') AS EmployeesName,Flag,");
            //selectSql.AppendLine("isnull(cast (DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo ");
            //selectSql.AppendLine("where CompanyCD=@CompanyCD and Flag!='2' and DeptID Is not null  ");

            selectSql.AppendLine("select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,");
            selectSql.AppendLine("isnull(cast (a.DeptID as varchar),'')  as  DeptID,isnull(b.DeptName,'') as DeptName   from officedba.EmployeeInfo  as a");
            selectSql.AppendLine("left join officedba.deptinfo  as b on a.DeptID=b.ID");
            selectSql.AppendLine("where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3'  ");


            //设置参数
            SqlParameter[] param = new SqlParameter[1];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            //在职区分标识
            //  param[1] = SqlHelper.GetParameter("@Flag", "3");

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
        #endregion

        //by:zyy  --culture big type
        public static DataTable GetCulture(string CompanyCD)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            //定义查询语句
            selectSql.Append("select ID,[path] as path_ID,SupperTypeID as SuperDeptID,TypeName as DeptName  ");
            selectSql.Append(" from [officedba].CultureType ");
            if (CompanyCD.Trim() != "")
            {
                selectSql.Append(" where SupperTypeID=0 and CompanyCD ='" + CompanyCD + "'");
            }

            //执行查询并返回查询到的CultureType
            return SqlHelper.ExecuteSql(selectSql.ToString());
        }

        //by:zyy
        public static DataTable GetCultureInfo(string companyCD, string SupperTypeID)
        {
            //定义查询SQL变量
            StringBuilder selectSql = new StringBuilder();
            ////定义查询语句
            selectSql.AppendLine("select ID,[path] as path_ID,SupperTypeID as SuperDeptID,TypeName as EmployeesName ");
            selectSql.AppendLine("from [officedba].CultureType ");
            selectSql.AppendLine(" where CompanyCD =@CompanyCD and SupperTypeID=@SupperTypeID ");


            //设置参数
            SqlParameter[] param = new SqlParameter[2];
            //公司代码
            param[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            param[1] = SqlHelper.GetParameter("@SupperTypeID", SupperTypeID);

            //执行查询并返回的查询到的部门信息
            return SqlHelper.ExecuteSql(selectSql.ToString(), param);
        }
    }
}
