/**********************************************
 * 类作用：   为获取申请单信息提供接口
 * 建立人：   陈鑫铎
 * 建立时间： 2010/11/19
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using XBase.Data.DBHelper;
using XBase.Data.Office.SellManager;
using XBase.Model.Office.SellManager;
using XBase.Business.Office.SystemManager;
using XBase.Model.Office.StorageManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class FreightBillBus
    {
        #region 申请单插入
        /// <summary>
        /// 申请单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertFreightBill(FreightModel model, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.InvNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;

                succ = FreightBillDBHelper.InsertFreightBill(model, loginUserID, out  ID);

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

        #region 申请单修改
        /// <summary>
        /// 合同单修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateFreightBill(FreightModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;

                LogInfoModel logModel = InitLogInfo(model.InvNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;

                succ = FreightBillDBHelper.UpdateFreightBill(model);
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
        #region 申请单删除
        /// <summary>
        /// 申请单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteFreightBill(string ID, string CompanyCD)
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

            bool isSucc = FreightBillDBHelper.DeleteFreightBill(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("申请单ID：" + no, 1);
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

        #region 确认
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeCode"></param>
        /// <param name="failReasons"></param>
        /// <returns></returns>
        public static bool ConfirmInvoiceBill(InvoiceBillModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                string loginUserName = userInfo.UserID;
                int loginUserID = userInfo.EmployeeID;

                LogInfoModel logModel = InitLogInfo(model.InvNo, 0);
                logModel.Element = ConstUtil.LOG_PROCESS_CONFIRM;

                succ = InvoiceBillDBHelper.ConfirmInvoiceBillBase(model, loginUserID, loginUserName);
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
        /// 获取运费单列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BeginOrderDate"></param>
        /// <param name="EndOrderDate"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public static DataTable GetFreightBillList(FreightModel model, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            return FreightBillDBHelper.GetFreightBillList(model, PageIndex, PageCount, OrderBy, ref TotalCount);
        }


        //导出EXCEL
        public static DataTable GetOutInvoiceBillList(InvoiceBillModel model)
        {
            return InvoiceBillDBHelper.GetOutInvoiceBillList(model);
        }
        #region 申请单详细信息
        /// <summary>
        /// 申请单详细信息
        /// <returns>DataTable</returns>
        public static DataTable GetFreightBillInfo(FreightModel model)
        {
            try
            {
                return FreightBillDBHelper.GetFreightBillInfo(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取美元汇率
        /// <summary>
        /// 获取美元汇率
        /// <returns>DataTable</returns>
        public static DataTable GetCurrencyInfo(FreightModel model)
        {
            try
            {
                return FreightBillDBHelper.GetCurrencyInfo(model);
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
                logSys.ModuleID = ConstUtil.MODULE_ID_INVOICEBill_ADD;
            }
            else
            {
                logSys.ModuleID = ConstUtil.MODULE_ID_INVOICEBill_INFO;
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
                logModel.ModuleID = ConstUtil.MODULE_ID_INVOICEBill_ADD;
            }
            else
            {
                logModel.ModuleID = ConstUtil.MODULE_ID_INCOMEBILL_LIST;
            }
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_INVOICEBill;
            //操作对象
            logModel.ObjectID = wcno;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
    }
}
