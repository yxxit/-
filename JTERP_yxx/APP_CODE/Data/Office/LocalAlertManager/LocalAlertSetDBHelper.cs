
using System;
using XBase.Model.Office.LocalAlertManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;

namespace XBase.Data.Office.LocalAlertManager
{
    public class LocalAlertSetDBHelper
    {
        #region 保存预警提醒设置
        /// <summary>
        /// 保存预警提醒设置
        /// </summary>
        /// <param name="LocalAlertSet_M"></param>
        /// <returns></returns>
        public static bool SaveLocalAlertSet(LocalAlertSetModel LocalAlertSet_M)
        {
            try
            {
                StringBuilder SQL = new StringBuilder();
                int IntVal=IsExist(LocalAlertSet_M.CompanyCD,LocalAlertSet_M.RemindType);
                if (IntVal == 0)
                {
                    SQL.Append("INSERT INTO officedba.RemindSet( ");
                    SQL.Append("CompanyCD,RemindType,IsMobileNotice,Mobile,RemindPeriod,SubPeriod,RemindTime,ModifiedDate,ModifiedUserID) ");
                    SQL.Append(" values (");
                    SQL.Append("@CompanyCD,@RemindType,@IsMobileNotice,@Mobile,@RemindPeriod,@SubPeriod,@RemindTime,@ModifiedDate,@ModifiedUserID)");
                }
                else 
                {
                    SQL.Append("UPDATE officedba.RemindSet ");
                    SQL.Append("SET IsMobileNotice=@IsMobileNotice,Mobile=@Mobile,RemindPeriod=@RemindPeriod,SubPeriod=@SubPeriod,RemindTime=@RemindTime,ModifiedDate=@ModifiedDate,ModifiedUserID=@ModifiedUserID ");
                    SQL.Append(" WHERE CompanyCD=@CompanyCD AND RemindType=@RemindType ");
                }
                SqlCommand comm = new SqlCommand();
                comm.CommandText = SQL.ToString();
                SetParameter(comm, LocalAlertSet_M);
                ArrayList ArrayComm = new ArrayList();
                ArrayComm.Add(comm);//数组加入插入基表的command
                bool result = SqlHelper.ExecuteTransWithArrayList(ArrayComm);
                SendNoticeDBHelper.InsertRemindSet(LocalAlertSet_M.RemindType,LocalAlertSet_M.CompanyCD);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 保存时基本信息参数设置
        /// <summary>
        /// 保存时基本信息参数设置
        /// </summary>
        /// <param name="comm"></param>
        /// <param name="LocalAlertSet_M"></param>
        private static void SetParameter(SqlCommand comm, LocalAlertSetModel LocalAlertSet_M)
        {

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD ", LocalAlertSet_M.CompanyCD));//公司编码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RemindType ", LocalAlertSet_M.RemindType));//提醒类型
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsMobileNotice ", LocalAlertSet_M.IsMobileNotice));//是否手机短信提醒
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Mobile ", LocalAlertSet_M.Mobile));//手机号码
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RemindPeriod ", LocalAlertSet_M.RemindPeriod));//提醒周期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@SubPeriod ", LocalAlertSet_M.SubPeriod));//提醒周期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@RemindTime ", LocalAlertSet_M.RemindTime));//提醒时间点
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedDate ", LocalAlertSet_M.ModifiedDate));//最后更新日期
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ModifiedUserID ", LocalAlertSet_M.ModifiedUserID));//最后更新用户
        }
        #endregion
        #region 是否设置过此类型的提醒
        /// <summary>
        /// 是否设置过此类型的提醒
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <param name="RemindType"></param>
        /// <returns></returns>
        public static int IsExist(string CompanyCD, string RemindType)
        {
            string sql = "select * from officedba.RemindSet "
                     + "where CompanyCD='" + CompanyCD + "' AND RemindType='"+RemindType+"' ";
            DataTable IsExist = SqlHelper.ExecuteSql(sql);
            if (IsExist != null)
            {
                if (IsExist.Rows.Count > 0)
                    return IsExist.Rows.Count;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region 获取预警设置信息
        /// <summary>
        /// 获取预警设置信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetLocalAlertSetByCompanyCD(string CompanyCD)
        {
            try
            {
                string sql = "select * from officedba.RemindSet where CompanyCD='" + CompanyCD + "'";
                return SqlHelper.ExecuteSql(sql);
            }
            catch(Exception ex)
            { 
                throw ex;
                return null;
            }
        }
        #endregion
    }
}
