using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.StorageManager;
using System.Data;
using XBase.Model.Office.StorageManager;
using XBase.Common;
using XBase.Data.Common;
using XBase.Model.Common;
using XBase.Business.Common;

namespace XBase.Business.Office.StorageManager
{
    public class DisassemblingBus
    {
        #region 获取套件信息
        public static DataTable GetBOM(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DisassemblingDBHelper.GetBOM(ID);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dt;
        }
        #endregion 
        #region 获取套件信息
        public static DataTable GetBOMID(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DisassemblingDBHelper.GetBOMID(ID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }
        #endregion
        #region 获取套件信息
        public static DataTable GetBOMinfo(string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DisassemblingDBHelper.GetBOMinfo(ID);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }
        #endregion


        #region 检查唯一性
        public static bool exsist(string billno)
        {
            bool exsi = false;
            try
            {
                exsi = DisassemblingDBHelper.exsist(billno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return exsi;
        }
        #endregion 

          public static bool Add(DisassemblingModel model, List<DisassemblingListModel> modelList, out int IndexIDentity)
        {

            IndexIDentity = 0;
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            model.companyCD = userInfo.CompanyCD;
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = DisassemblingDBHelper.Add(model, modelList,  out IndexIDentity);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }
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
            //操作日志
            LogInfoModel logModel = InitLogInfo(model.BillNo);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;

        }
          #region 更新其他入库及其他入库明细
          /// <summary>
          /// 更新其他入库及其他入库明细
          /// </summary>
          /// <param name="model"></param>
          /// <param name="modelList"></param>
          /// <returns></returns>
          public static bool Update(DisassemblingModel model, List<DisassemblingListModel> modelList)
          {
              //获取登陆用户信息
              UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
              //设置公司代码
              model.companyCD = userInfo.CompanyCD;
              //定义返回变量
              bool isSucc = false;
              /* 
               * 定义日志内容变量 
               * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
               * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
               */
              //获取公司代码
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

              //执行删除操作
              try
              {
                  //执行更新
                  isSucc = DisassemblingDBHelper.Update(model, modelList);
              }
              catch (Exception ex)
              {
                  //输出日志
                  WriteSystemLog(userInfo, ex);
              }
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
              //操作日志
              LogInfoModel logModel = InitLogInfo(model.ID.ToString());
              //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
              logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
              //设置操作成功标识
              logModel.Remark = remark;

              //登陆日志
              LogDBHelper.InsertLog(logModel);
              return isSucc;
          }
          #endregion

          #region 输出系统日志
          /// <summary>
          /// 输出系统日志
          /// </summary>
          /// <param name="userInfo">用户信息</param>
          /// <param name="ex">异常信息</param>
          private static void WriteSystemLog(UserInfoUtil userInfo, Exception ex)
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
              logSys.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINOTHER_ADD;
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
          private static LogInfoModel InitLogInfo(string ID)
          {
              LogInfoModel logModel = new LogInfoModel();
              //获取登陆用户信息
              UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
              //设置公司代码
              logModel.CompanyCD = userInfo.CompanyCD;
              //设置登陆用户ID
              logModel.UserID = userInfo.UserID;
              //设置模块ID 模块ID请在ConstUtil中定义，以便维护
              logModel.ModuleID = ConstUtil.MODULE_ID_STORAGE_STORAGEINOTHER_ADD;
              //设置操作日志类型 修改
              logModel.ObjectName = "StorageInOther";
              //操作对象
              logModel.ObjectID = ID;
              //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
              logModel.Element = string.Empty;

              return logModel;

          }
          #endregion

          #region 是否可以被确认
          /// <summary>
          /// 
          /// </summary>
          /// <param name="model">CompanyCD,ID</param>
          /// <returns></returns>
          public static bool ISConfirmBill(DisassemblingModel model,bool type,out string proname)
          {
              bool isscuss = false;
              try
              {
                  isscuss=DisassemblingDBHelper.ISConfirmBill(model,type,out proname);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return isscuss;
          }
          #endregion

          #region 确认
          /// <summary>
          /// 确认
          /// </summary>
          /// <param name="model"></param>
          /// <returns></returns>
          public static bool ConfirmBill(DisassemblingModel model, out string Msg)
          {

              //获取登陆用户信息
              UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
              //设置公司代码
              model.companyCD = userInfo.CompanyCD;
              //定义返回变量
              bool isSucc = false;
              Msg = "";
              /* 
               * 定义日志内容变量 
               * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
               * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
               */
              //获取公司代码
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

              //执行删除操作
              try
              {
                  //执行更新
                  isSucc = DisassemblingDBHelper.ConfirmBill(model, out Msg);
              }
              catch (Exception ex)
              {
                  //输出日志
                  WriteSystemLog(userInfo, ex);
              }

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
              //操作日志
              LogInfoModel logModel = InitLogInfo(model.ID.ToString());
              //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
              logModel.Element = "确认";
              //设置操作成功标识
              logModel.Remark = remark;

              //登陆日志
              LogDBHelper.InsertLog(logModel);
              return isSucc;
          }
          #endregion

          #region 反确认
          /// <summary>
          /// 反确认
          /// </summary>
          /// <param name="model"></param>
          /// <returns></returns>
          public static bool UnConfirmBill(DisassemblingModel model, out string Msg)
          {

              //获取登陆用户信息
              UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
              //设置公司代码
              model.companyCD = userInfo.CompanyCD;
              //定义返回变量
              bool isSucc = false;
              Msg = "";
              /* 
               * 定义日志内容变量 
               * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
               * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
               */
              //获取公司代码
              string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

              //执行删除操作
              try
              {
                  //执行更新
                  isSucc = DisassemblingDBHelper.UnConfirmBill(model, out Msg);
              }
              catch (Exception ex)
              {
                  //输出日志
                  WriteSystemLog(userInfo, ex);
              }

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
              //操作日志
              LogInfoModel logModel = InitLogInfo(model.ID.ToString());
              //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
              logModel.Element = "取消确认";
              //设置操作成功标识
              logModel.Remark = remark;

              //登陆日志
              LogDBHelper.InsertLog(logModel);
              return isSucc;
          }
          #endregion

          #region 获取单据信息
          public static DataTable GetBillByID(string ID)
          {
              DataTable dt = new DataTable();
              try
              {
                  dt = DisassemblingDBHelper.GetBillByID(ID);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dt;
          }
          #endregion

          #region

        /// <summary>
        /// 结单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public static bool CloseBill(string id)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
           
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = DisassemblingDBHelper.CloseBill(id);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            //操作日志
            LogInfoModel logModel = InitLogInfo(id);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = "结单";
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }


        public static bool CancelCloseBill(string id)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
          
            //定义返回变量
            bool isSucc = false;
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //获取公司代码
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

            //执行删除操作
            try
            {
                //执行更新
                isSucc = DisassemblingDBHelper.CancelCloseBill(id);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }

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
            //操作日志
            LogInfoModel logModel = InitLogInfo(id);
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = "取消结单";
            //设置操作成功标识
            logModel.Remark = remark;

            //登陆日志
            LogDBHelper.InsertLog(logModel);
            return isSucc;
        }
        #endregion
        #region 是否可删除
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool isdel(string id)
        {
            bool isscuss = false;
            try
            {
                isscuss = DisassemblingDBHelper.isdel(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isscuss;
        }
        #endregion

        #region 执行删除
        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="model">CompanyCD,ID</param>
        /// <returns></returns>
        public static bool DeleteIt(string id)
        {
            bool isscuss = false;
            try
            {
                isscuss = DisassemblingDBHelper.DeleteIt(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isscuss;
        }
        #endregion

        #region 根据单据编号获取单据信息
        public static DataTable GetBillByNo(string No)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DisassemblingDBHelper.GetBillByNo(No);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion

    }
}
