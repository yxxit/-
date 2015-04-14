using System;
using System.Collections.Generic;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class CostForeignBus
    {
        #region 成本核算插入
        /// <summary>
        /// 成本核算插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertCostForeign(CostForeignModel model, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.CostNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = CostForeignDBHelper.InsertCostForeign(model, loginUserID, out ID);
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

        #region 成本核算修改
        /// <summary>
        /// 成本核算修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateCostForeign(CostForeignModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.CostNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = CostForeignDBHelper.UpdateCostForeign(model);
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

        #region 成本核算明细详细信息
        /// <summary>
        /// 成本核算明细详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCostForeignDetailInfoList(CostForeignModel model)
        {
            try
            {
                return CostForeignDBHelper.GetCostForeignDetailInfoList(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 成本核算详细信息
        /// <summary>
        /// 成本核算详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCostForeignInfo(CostForeignModel model)
        {
            try
            {
                return CostForeignDBHelper.GetCostForeignInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 成本核算详细信息
        /// <summary>
        /// 成本核算详细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetCostForeignInfo(string ProductID)
        {
            try
            {
                return CostForeignDBHelper.GetCostForeignInfo(ProductID);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 通过检索条件查询成本核算信息
        /// <summary>
        /// 通过检索条件查询成本核算信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetCostForeignListBycondition(CostForeignModel model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                return CostForeignDBHelper.GetCostForeignListBycondition(model, loginUserID, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 成本核算删除
        /// <summary>
        /// 成本核算删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteCostForeign(string ID, string CompanyCD)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return false;
            }
            if (string.IsNullOrEmpty(CompanyCD))
            {
                CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            }

            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

            bool isSucc = CostForeignDBHelper.DeleteCostForeign(ID, CompanyCD);
            //定义变量
            string remark;
            //成功时
            if (isSucc)
            {
                //设置操作成功标识
                remark = ConstUtil.LOG_PROCESS_SUCCESS;
            }
            else
            {
                //设置操作成功标识 
                remark = ConstUtil.LOG_PROCESS_FAILED;
            }
            //获取删除的编号列表
            string[] noList = ID.Split(',');
            //遍历所有编号，登陆操作日志
            for (int i = 0; i < noList.Length; i++)
            {
                //获取编号
                string no = noList[i];
                //替换两边的 '
                no = no.Replace("'", string.Empty);

                //操作日志
                LogInfoModel logModel = InitLogInfo("成本核算ID：" + no, 1);
                //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
                logModel.Element = ConstUtil.LOG_PROCESS_DELETE;
                //设置操作成功标识
                logModel.Remark = remark;

                //登陆日志
                LogDBHelper.InsertLog(logModel);
            }
            return isSucc;
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
                logSys.ModuleID = ConstUtil.MODULE_ID_SELLORDER_ADD;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_SELLORDER_INFO;
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

            if (ModuleType == 0)
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_SELLORDER_ADD;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_SELLORDER_INFO;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_SELLORDER;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
