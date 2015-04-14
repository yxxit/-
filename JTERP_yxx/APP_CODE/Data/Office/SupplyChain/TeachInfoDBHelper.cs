using System;
using XBase.Model.Office.SupplyChain;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using XBase.Model.Office.BasicData;
using System.Collections.Generic;
using System.IO;
using XBase.Common;

namespace XBase.Data.Office.SupplyChain
{
    public class TeachInfoDBHelper
    {

        #region 查询物品档案，物品控件， 没有部门树
        /// <summary>
        /// 物品档案
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetTeachInfoTableBycondition(TeachInfoModel model,  int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a. ID                                                         ");
            sql.AppendLine("      ,a. TeachNo                                                  ");
            sql.AppendLine("      ,isnull(g.QualityName,'') QualityName                     ");
            sql.AppendLine("      ,a. TeachName                                                ");
            sql.AppendLine("      ,isnull(a. UnitID,0) UnitID                                    ");
            sql.AppendLine("      ,isnull(c. UnitName,'') UnitName                               ");
            sql.AppendLine("      ,a. Specification                                              ");
            sql.AppendLine("      ,a. referCostPrice                                             ");
            sql.AppendLine("  FROM   officedba.TeachInfo  a                                  ");
            sql.AppendLine(" left join officedba. MeasureUnit c on c.ID=a.UnitID and c.CompanyCD=@CompanyCD");
            sql.AppendLine("left join officedba.Quality as g on a.Quality=g.ID and a.CompanyCD=g.CompanyCD");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD                 ");                    
             sql.AppendLine(" and a.UsedStatus='1'                                      ");           
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.CommandText = sql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
      
        #region 物品多选确定
        public static DataTable GetTeachInfoTableByCheckcondition(string strID, string CompanyCD)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                                                 ");
            searchSql.AppendLine("      ,a.TeachNo                                             ");
            searchSql.AppendLine("      ,a.TeachName                                        ");
            searchSql.AppendLine("      ,isnull(a.TeachType,'') as TeachType                        ");
            searchSql.AppendLine("      ,isnull(a.UnitID,'')as   UnitID                       ");
            searchSql.AppendLine("      ,isnull(a.Specification,'')as Specification           ");
            searchSql.AppendLine("      ,isnull(a.Creator,'')  as Creator                     ");
            searchSql.AppendLine("      ,isnull(b.EmployeeName,'') as   CreatorName          ");
            searchSql.AppendLine("      ,isnull( CONVERT(CHAR(10),a.CreateDate, 23),'')CreateDate                 ");
            searchSql.AppendLine("      ,isnull(a.UsedStatus,'')as UsedStatus                 ");
            searchSql.AppendLine("      ,isnull(c.UnitName,'') as UnitName                    ");
            searchSql.AppendLine("      ,isnull(a.Quality,'') Quality                     ");
            searchSql.AppendLine("      ,isnull(g.QualityName,'')QualityName                     ");
            searchSql.AppendLine("     ,isnull(d.TypeName,'')as TypeName                      ");
            searchSql.AppendLine("  FROM officedba.TeachInfo as a                        ");
            searchSql.AppendLine("left join officedba.EmployeeInfo as b on a.Creator=b.ID  and a.CompanyCD=b.CompanyCD    ");
            searchSql.AppendLine("left join officedba. MeasureUnit  as c on a.UnitID=c.ID and a.CompanyCD=c.CompanyCD     ");
            searchSql.AppendLine("left join officedba.TeachType as d on a.TeachType=d.ID and a.CompanyCD=d.CompanyCD       ");
            searchSql.AppendLine("left join officedba.Quality as g on a.Quality=g.ID and a.CompanyCD=g.CompanyCD        ");
            searchSql.AppendLine("        where   a.CompanyCD=@CompanyCD   and a.ID in (" + strID + ")               ");
            comm.CommandText = searchSql.ToString();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", CompanyCD));
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@strID", strID));
            //执行查询
            return SqlHelper.ExecuteSearch(comm);

        }

        #endregion

   
        #region 列表查询商品信息
        /// <summary>
        /// 列表查询商品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static DataTable GetTeachInfo(TeachInfoModel Model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine("SELECT a.ID                                                 ");
            searchSql.AppendLine("      ,a.TeachNo                                             ");
            searchSql.AppendLine("      ,a.TeachName                                        ");
            searchSql.AppendLine("      ,isnull(a.TeachType,'') as TeachType                        ");
            searchSql.AppendLine("      ,isnull(a.UnitID,'')as   UnitID                       ");
            searchSql.AppendLine("      ,isnull(a.Specification,'')as Specification           ");
            searchSql.AppendLine("      ,isnull(a.Creator,'')  as Creator                     ");
            searchSql.AppendLine("      ,isnull(b.EmployeeName,'') as   CreatorName          ");
            searchSql.AppendLine("      ,isnull( CONVERT(CHAR(10),a.CreateDate, 23),'')CreateDate                 ");
            searchSql.AppendLine("      ,isnull(a.UsedStatus,'')as UsedStatus                 ");
            //searchSql.AppendLine("      ,isnull(c.UnitName,'') as UnitName                    ");
            searchSql.AppendLine("      ,isnull(c.CodeName,'') as UnitName                    ");
            searchSql.AppendLine("      ,isnull(a.Quality,'') Quality                     ");
            searchSql.AppendLine("      ,isnull(g.QualityName,'')QualityName                     ");
            searchSql.AppendLine("     ,isnull(d.TypeName,'')as TypeName                      ");
            searchSql.AppendLine("  FROM officedba.TeachInfo as a                        ");
            searchSql.AppendLine("left join officedba.EmployeeInfo as b on a.Creator=b.ID  and a.CompanyCD=b.CompanyCD    ");
            //searchSql.AppendLine("left join officedba. MeasureUnit  as c on a.UnitID=c.ID and a.CompanyCD=c.CompanyCD     ");
            searchSql.AppendLine("left join officedba.CodeUnitType  as c on a.UnitID=c.ID and a.CompanyCD=c.CompanyCD     ");
            searchSql.AppendLine("left join officedba.TeachType as d on a.TeachType=d.ID and a.CompanyCD=d.CompanyCD       ");
            searchSql.AppendLine("left join officedba.Quality as g on a.Quality=g.ID and a.CompanyCD=g.CompanyCD        ");         
            searchSql.AppendLine("        where   a.CompanyCD=@CompanyCD                  ");

            //#endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            ////公司代码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", Model.CompanyCD));
            //编号
            if (!string.IsNullOrEmpty(Model.TeachNo))
            {
                searchSql.AppendLine("	and a.TeachNo LIKE @TeachNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachNo", "%" + Model.TeachNo + "%"));
            }
            //名称
            if (!string.IsNullOrEmpty(Model.TeachName))
            {
                searchSql.AppendLine("	AND a.TeachName LIKE @TeachName ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachName", "%" + Model.TeachName + "%"));
            }
          
            if (!string.IsNullOrEmpty(Model.Specification))
            {
                searchSql.AppendLine("	AND a.Specification LIKE @Specification ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", "%" + Model.Specification + "%"));
            }
            if (!string.IsNullOrEmpty(Model.TeachType.ToString()) && Model.TeachType != 0)
            {
                searchSql.AppendLine("	AND a.TeachType=@TeachType ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachType", Model.TeachType.ToString()));
            }
            if (!string.IsNullOrEmpty(Model.UsedStatus))
            {
                searchSql.AppendLine("	AND a.UsedStatus=@UsedStatus ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", Model.UsedStatus.ToString()));
            }
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 根据物品ID加载信息

        public static DataTable GetTeachInfoByID(int ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT a.ID                             ");
            sql.AppendLine("      ,a.CompanyCD                      ");
            sql.AppendLine("      ,a.TeachNo                      ");
            sql.AppendLine("      ,a.TeachName                    ");
            sql.AppendLine("      ,a.TeachType                         ");
            sql.AppendLine("      ,a.UnitID                         ");
            sql.AppendLine("      ,a.Specification                  ");
            sql.AppendLine("      ,a.Creator                        ");
            sql.AppendLine(" 	,CONVERT(VARCHAR(10),a.CreateDate  ,21) AS CreateDate ");
            sql.AppendLine("      ,a.UsedStatus                     ");
            sql.AppendLine("      ,g.UnitName                      ");
            sql.AppendLine("      ,a.referCostPrice                    ");
            sql.AppendLine("      ,a.Quality                  ");
            sql.AppendLine("      ,a.Remark                       ");
            sql.AppendLine("      ,b.TypeName                      ");
            sql.AppendLine("      ,c.EmployeeName as CreatorName                        ");
            sql.AppendLine("  FROM officedba.TeachInfo as a");
            sql.AppendLine("left join officedba.EmployeeInfo as c on a.Creator=c.ID     ");
            sql.AppendLine("left join officedba.MeasureUnit as g on a.UnitID=g.ID ");
            sql.AppendLine(" left join officedba.TeachType as b on a.TeachType=b.ID where a.ID=@ID");
            SqlParameter[] param = new SqlParameter[1];
            //人员ID
            param[0] = SqlHelper.GetParameter("@ID", ID);
            //执行查询
            DataTable data = SqlHelper.ExecuteSql(sql.ToString(), param);
            return data;
        }
        #endregion
    
        #region 删除商品信息
        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="TypeFlag"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteTeachInfo(string ID, string CompanyCD)
        {
            string allID = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string[] Delsql = new string[1];
            try
            {
                string[] IdS = null;
                ID = ID.Substring(0, ID.Length);
                IdS = ID.Split(',');

                for (int i = 0; i < IdS.Length; i++)
                {
                    IdS[i] = "'" + IdS[i] + "'";
                    sb.Append(IdS[i]);
                }
                allID = sb.ToString().Replace("''", "','");

                Delsql[0] = "delete from  officedba.TeachInfo where ID IN (" + allID + ") and CompanyCD = @CompanyCD and UsedStatus='0'";

                SqlCommand comm = new SqlCommand();
                comm.CommandText = Delsql[0].ToString();

                //设置参数
                comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                ArrayList lstDelete = new ArrayList();
                comm.CommandText = Delsql[0].ToString();
                //添加基本信息更新命令
                lstDelete.Add(comm);
                return SqlHelper.ExecuteTransWithArrayList(lstDelete);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 插入物品信息
        /// <summary>
        /// 插入商品档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertTeachInfo(TeachInfoModel model, out string ID)
        {
            //SQL拼写
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("INSERT INTO officedba.TeachInfo     ");
            sql.AppendLine(" (  TeachNo                         ");
            sql.AppendLine("    ,TeachName                      ");
            sql.AppendLine("    ,Specification                  ");
            sql.AppendLine("    ,Quality                        ");
            sql.AppendLine("    ,UnitID                         ");
            sql.AppendLine("    ,referCostPrice                 ");
            sql.AppendLine("    ,TeachType                      ");
            sql.AppendLine("    ,CompanyCD                      ");
            sql.AppendLine("    ,Remark                         ");
            sql.AppendLine("    ,UsedStatus                         ");
            sql.AppendLine("    ,Creator                        ");
            sql.AppendLine("    ,CreateDate)                    ");
            sql.AppendLine("     VALUES                         "); 
            sql.AppendLine(" (  @TeachNo                        ");
            sql.AppendLine("    ,@TeachName                     ");
            sql.AppendLine("    ,@Specification                 ");
            sql.AppendLine("    ,@Quality                       ");
            sql.AppendLine("    ,@UnitID                        ");
            sql.AppendLine("    ,@referCostPrice                ");
            sql.AppendLine("    ,@TeachType                     ");
            sql.AppendLine("    ,@CompanyCD                     ");
            sql.AppendLine("    ,@Remark                        ");
            sql.AppendLine("    ,@UsedStatus                        ");
            sql.AppendLine("    ,@Creator                       ");
            sql.AppendLine("    ,@CreateDate)                   ");
            sql.AppendLine("   SET @ID= @@IDENTITY  ");
            //定义更新基本信息的命令
            SqlCommand comm = new SqlCommand();
            //设置存储过程名
            comm.CommandText = sql.ToString();
            //设置保存的参数
            SetSaveParameter(comm, model);
            //添加返回参数
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
            ArrayList lstCmd = new ArrayList();
            SqlCommand cmd = new SqlCommand();
            lstCmd.Add(comm);
            //执行登陆操作
            bool isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);
            //设置ID
            //model.ID = int.Parse(comm.Parameters["@ProdID"].Value);
            ID = comm.Parameters["@ID"].Value.ToString();
            return isSucc;
        }

        #endregion

        #region 保存物品配置参数
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="model"></param>
        private static void SetSaveParameter(SqlCommand comm, TeachInfoModel model)
        {
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));//公司编码                                
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachName", model.TeachName));//商品名称                         
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachNo", model.TeachNo));//商品编号                                 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UnitID", model.UnitID));//计量单位 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TeachType", model.TeachType.ToString()));                                   
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Specification", model.Specification));//规格                         
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UsedStatus", model.UsedStatus));//启用状态（0停用，1启用）     
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@referCostPrice", model.referCostPrice));//参考成本价                 
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Creator", model.Creator));//创建人       
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Remark", model.Remark));//创建人                     
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CreateDate", model.CreateDate));//创建时间  
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Quality", model.Quality));
        }
        #endregion

        #region 修改商品档案
        /// <summary>
        /// 修改商品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateTeachInfo(TeachInfoModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE officedba.TeachInfo                        ");
            sql.AppendLine("   SET                                              ");
            sql.AppendLine("    TeachName=@TeachName                      ");
            sql.AppendLine("    ,Specification=@Specification                   ");
            sql.AppendLine("    ,Quality=@Quality                          ");
            sql.AppendLine("    ,UnitID=@UnitID                         ");
            sql.AppendLine("    ,referCostPrice=@referCostPrice                   ");
            sql.AppendLine("    ,TeachType=@TeachType                        ");
            sql.AppendLine("    ,CompanyCD=@CompanyCD                        ");
            sql.AppendLine("    ,Remark=@Remark                           ");
            sql.AppendLine("    ,UsedStatus=@UsedStatus                           ");
            sql.AppendLine("      where                                         ");
            sql.AppendLine(" 	CompanyCD = @CompanyCD                          ");
            sql.AppendLine(" 	AND TeachNo = @TeachNo                      ");

            //定义更新基本信息的命令  
            SqlCommand comm = new SqlCommand();
            comm.CommandText = sql.ToString();
            SetSaveParameter(comm, model);//其他参数
            //执行更新并设置更新结果
            bool result = false;
            ArrayList lstCmd = new ArrayList();
            SqlCommand cmd = new SqlCommand();
           
            lstCmd.Add(comm);
         
            result = SqlHelper.ExecuteTransWithArrayList(lstCmd);
            return result;

        }


        #endregion  

     
        #region 读取指定公司所有的商品分类
        public static DataTable GetTeachType(string CompanyCD)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT * FROM officedba.TeachType ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND UsedStatus='1' ");
            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = SqlHelper.GetNewSqlParameter("@CompanyCD", CompanyCD, SqlDbType.VarChar);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion



        #region 验证商品编号是否重复
        public static bool IsRepeatTeachNo(string CompanyCD, string TeachNo)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("SELECT TOP 1 * FROM officedba.TeachInfo ");
            sbSql.AppendLine("WHERE CompanyCD=@CompanyCD AND TeachNo=@TeachNo ");
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = SqlHelper.GetNewSqlParameter("@CompanyCD", CompanyCD, SqlDbType.VarChar);
            Params[1] = SqlHelper.GetNewSqlParameter("@TeachNo", TeachNo, SqlDbType.VarChar);

            DataTable dtRes = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dtRes == null || dtRes.Rows.Count <= 0)
            {
                return true;
            }
            else
                return false;
        }
        #endregion 
    }
}
