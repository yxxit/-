/****************************************************** 
FileName:事务操作类
******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using XBase.Common;

namespace XBase.Data.DBHelper
{

    public class YYTransactionManager
    {
        private SqlTransaction _trans;//事务变量
        private SqlConnection _conn;//数据连接
        //  private string strConn = string.Empty;//链接字符串

        private static string strConn = string.Empty;//链接字符串

        #region 公共属性

        /// <summary>
        /// 获得事务实体
        /// </summary>
        public SqlTransaction Trans
        {
            get
            {
                return _trans;
            }
        }

        #endregion 公共属性

        #region 公共方法

        /// <summary>
        /// 构造函数，初始化连接字符串和数据库连接。
        /// </summary>
        public YYTransactionManager()
        {
            //strConn = SqlHelper.GetConnection();
            if (strConn == null || strConn == "")
            {
                strConn = ConfigurationManager.ConnectionStrings["ConnectionStringYY"].ToString();
                if (ConfigurationManager.AppSettings["enableEncryptConnectionString"] == "1")
                {
                    strConn = XBase.Common.SecurityUtil.DecryptDES(strConn);
                }

            }
            _conn = new SqlConnection(strConn);
        }

        /// <summary>
        /// 开始事务,默认的事务锁定行为。
        /// </summary>
        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 开始事务。如果读取时要锁定行，请用枚举：IsolationLevel.Serializable。
        /// </summary>
        /// <param name="isolationLevel">事务锁定行为</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            try
            {
                _trans = _conn.BeginTransaction(isolationLevel);
            }
            catch
            {
                _conn.Close();
                _trans.Dispose();
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            try
            {
                _trans.Commit();
            }
            finally
            {
                _conn.Close();
                _trans.Dispose();
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            try
            {
                _trans.Rollback();
            }
            finally
            {
                _conn.Close();
                _trans.Dispose();
            }
        }

        #endregion 公共方法

    }//类结束

}//命名空间结束
