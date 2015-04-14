/**********************************************
 * 类作用：   生产进度汇报事务层处理
 * 建立人：   宋杰
 * 建立时间： 2010/03/21
 ***********************************************/
using System;
using System.Collections.Generic;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.ProductionManager
{
    public class ManufactureProgressRptBus
    {
        #region 生产进度汇报插入
        /// <summary>
        /// 生产进度汇报插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertManufactureProgressRpt(ManufactureDispatchReportModel model, Hashtable ht, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.ReportNO, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = ManufactureDispatchReportDBHelper.InsertManufactureProgressRpt(model, ht, loginUserID, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }
        }
        #endregion

        #region 更新生产进度汇报信息
        /// <summary>
        /// 更新生产进度汇报信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UpdateID"></param>
        /// <returns></returns>
        public static bool UpdateManufactureProgressRpt(ManufactureDispatchReportModel model, Hashtable ht, string UpdateID)
        {

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.ReportNO, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = ManufactureDispatchReportDBHelper.UpdateManufactureProgressRpt(model, ht, loginUserID, UpdateID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, 0, ex);
                return false;
            }
        }

        #endregion

        #region 生产进度汇报详细信息
        /// <summary>
        /// 生产进度汇报详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureProgressRptInfo(string ReportNo)
        {
            try
            {
                return ManufactureDispatchReportDBHelper.GetProgressRptInfo(ReportNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 生产进度汇报单明细详细信息
        /// <summary>
        /// 生产进度汇报单明细详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetManufactureProgressRptDetail(string ReportNo)
        {
            try
            {
                return ManufactureDispatchReportDBHelper.GetProgressRptDetailInfo(ReportNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo, int ModuleType, Exception ex)
        {
            /* 
             * 出现异常时，输出系统日志到文本文件 
             * 考虑出现异常情况比较少，尽管一个方法可能多次异常，
             *      但还是考虑将异常日志的变量定义放在catch里面
             */
            //定义变量
            LogInfo logSys = new LogInfo();
            //设置日志类型 需要指定为系统日志
            logSys.Type = LogInfo.LogType.SYSTEM;
            //指定系统日志类型 出错信息
            logSys.SystemKind = LogInfo.SystemLogKind.SYSTEM_ERROR;
            //指定登陆用户信息
            logSys.UserInfo = userInfo;
            //设定模块ID
            if (ModuleType == 0)
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_EDIT;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_LIST;
            }
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion

        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string wcno, int ModuleType)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.MODULE_ID_BOM_LIST;
            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_EDIT;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_MANUFACTURETASK_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_MANUFACTURETASK;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion

    }
}
