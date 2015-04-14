using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Model.Office.SupplyChain;
using System.Data.SqlClient;

namespace XBase.Data.Office.SystemManager
{
   public class BusiLogicSetDBHelper
    {
        #region 设置指定的业务规则配置
       public static bool SetBus(string CompanyCD, string LogicType, string LogicID, string LogicName, string LogicSet, string Description)
        {
            StringBuilder sbSql = new StringBuilder();
            //验证是否存在指定配置项
            if (ValidateKey(CompanyCD, LogicType, LogicID))
            {
                //不存在 则按照指定状态插入一条数据
                sbSql.AppendLine(" INSERT INTO officedba.BusiLogicSet ");
                sbSql.AppendLine(" (CompanyCD,LogicType,LogicID,LogicName,LogicSet,Description) ");
                sbSql.AppendLine(" VALUES ");
                sbSql.AppendLine(" (@CompanyCD,@LogicType,@LogicID,@LogicName,@LogicSet,@Description )");
            }
            else
            {
                //已经存在 则更新指定项的状态
                sbSql.AppendLine(" UPDATE officedba.BusiLogicSet SET ");
                sbSql.AppendLine(" LogicSet=@LogicSet ");
                sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND LogicType=@LogicType AND LogicID=@LogicID ");
            }

            //构造参数
            SqlParameter[] Params = new SqlParameter[6];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@LogicSet", LogicSet);
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@LogicType", LogicType);
            Params[index++] = SqlHelper.GetParameter("@LogicID", LogicID);
            Params[index++] = SqlHelper.GetParameter("@LogicName", LogicName);
            Params[index++] = SqlHelper.GetParameter("@Description", Description);


            //执行SQL
            if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Params) > 0)
                return true;
            else
                return false;
        }

        #endregion

        #region 验证是否已经存在指定配置项
        /// <summary>
        /// 验证是否已经存在指定配置项
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="LogicType"></param>
        /// <param name="LogicID"></param>
        /// <returns></returns>
        private static bool ValidateKey(string CompanyCD, string LogicType, string LogicID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  TOP 1 * ");
            sbSql.AppendLine(" FROM officedba.BusiLogicSet ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD  AND LogicType=@LogicType AND LogicID=@LogicID ");

            SqlParameter[] Params = new SqlParameter[3];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@LogicType", LogicType);
            Params[index++] = SqlHelper.GetParameter("@LogicID", LogicID);


            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt == null || dt.Rows.Count <= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 读取参数配置
        public static DataTable GetBus(string CompanyCD, string LogicType, string LogicID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * ");
            sbSql.AppendLine(" FROM officedba.BusiLogicSet ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND LogicType=@LogicType AND LogicID=@LogicID ");
            SqlParameter[] Params = new SqlParameter[3];

            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@LogicType", LogicType);
            Params[index++] = SqlHelper.GetParameter("@LogicID", LogicID);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion

        #region 设置提醒内容
        public static bool SetAlertContent(string CompanyCD, string Type, string UserID, string status,string iDays,string Lock)
        {
            StringBuilder sbSql = new StringBuilder();
            //验证是否存在指定配置项
            if (Type == "10" || Type == "11" || Type == "12" || Type == "13" || Type == "14")
            {
                string strdel = "delete from officedba.AlertSetting where CompanyCD='"+CompanyCD+"' and functionType='"+Type+"' ";
                SqlHelper.ExecuteTransSql(strdel);

                sbSql.AppendLine(" INSERT INTO officedba.AlertSetting ");
                sbSql.AppendLine(" (CompanyCD,UserID,functionType,usedStatus,iDays,Lock) ");
                sbSql.AppendLine(" VALUES ");
                sbSql.AppendLine(" (@CompanyCD,@UserID,@functionType,@usedStatus,@iDays,@Lock )");
            }
            else
            {
                if (IsValidateKey(CompanyCD, UserID, Type))
                {
                    //不存在 则按照指定状态插入一条数据
                    sbSql.AppendLine(" INSERT INTO officedba.AlertSetting ");
                    sbSql.AppendLine(" (CompanyCD,UserID,functionType,usedStatus,iDays,Lock) ");
                    sbSql.AppendLine(" VALUES ");
                    sbSql.AppendLine(" (@CompanyCD,@UserID,@functionType,@usedStatus,@iDays,@Lock )");
                }
                else
                {
                    //已经存在 则更新指定项的状态
                    sbSql.AppendLine(" UPDATE officedba.AlertSetting SET ");
                    sbSql.AppendLine(" usedStatus=@usedStatus,iDays=@iDays,Lock=@Lock ");
                    sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND UserID=@UserID AND functionType=@functionType ");
                }
            }
            //构造参数
            SqlParameter[] Params = new SqlParameter[6];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@UserID", UserID);
            Params[index++] = SqlHelper.GetParameter("@functionType", Type);
            Params[index++] = SqlHelper.GetParameter("@usedStatus", status);
            Params[index++] = SqlHelper.GetParameter("@iDays", iDays);
            Params[index++] = SqlHelper.GetParameter("@Lock", Lock);


            //执行SQL
            if (SqlHelper.ExecuteTransSql(sbSql.ToString(), Params) > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 验证是否已经存在指定配置项
        /// <summary>
        /// 验证是否已经存在指定配置项
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name ="UserID"></param>
        /// <param name="FunctionType"></param>
        /// <returns></returns>
        private static bool IsValidateKey(string CompanyCD, string UserID,string FunctionType)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT  TOP 1 * ");
            sbSql.AppendLine(" FROM officedba.AlertSetting ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD and UserID=@UserID AND FunctionType=@FunctionType ");

            SqlParameter[] Params = new SqlParameter[3];
            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);
            Params[index++] = SqlHelper.GetParameter("@UserID", UserID);

            DataTable dt = SqlHelper.ExecuteSql(sbSql.ToString(), Params);

            if (dt == null || dt.Rows.Count <= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region 读取提醒内容配置
        public static DataTable GetAlertContent(string CompanyCD, string FunctionType,string UserID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * ");
            sbSql.AppendLine(" FROM officedba.AlertSetting ");
            //sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType and UserID=@UserID ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType  ");

            SqlParameter[] Params = new SqlParameter[2];

            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);
           // Params[index++] = SqlHelper.GetParameter("@UserID", UserID);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion

        #region 读取提醒内容配置--不限定用户
        public static DataTable GetAlertContentNoUser(string CompanyCD, string FunctionType, string UserID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * ");
            sbSql.AppendLine(" FROM officedba.AlertSetting ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType   ");
            SqlParameter[] Params = new SqlParameter[2];

            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);
          
            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }
        #endregion

       /// <summary>
       /// 超期锁定
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="FunctionType"></param>
       /// <param name="UserID"></param>
       /// <returns></returns>
        public static DataTable GetLockNoUser(string CompanyCD, string FunctionType, string UserID)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT * ");
            sbSql.AppendLine(" FROM officedba.AlertSetting ");
            sbSql.AppendLine(" WHERE CompanyCD=@CompanyCD AND FunctionType=@FunctionType   ");
            SqlParameter[] Params = new SqlParameter[2];

            int index = 0;
            Params[index++] = SqlHelper.GetParameter("@CompanyCD", CompanyCD);
            Params[index++] = SqlHelper.GetParameter("@FunctionType", FunctionType);

            return SqlHelper.ExecuteSql(sbSql.ToString(), Params);
        }

    }
}
