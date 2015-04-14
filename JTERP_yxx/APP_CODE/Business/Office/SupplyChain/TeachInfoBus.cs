using System;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Collections;
using XBase.Model.Office.BasicData;
using System.Collections.Generic;

namespace XBase.Business.Office.SupplyChain
{
    public class TeachInfoBus
    {
         #region 查询商品档案（公用控件）
        public static DataTable GetTeachInfoTableBycondition(TeachInfoModel model,  int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
               return TeachInfoDBHelper.GetTeachInfoTableBycondition(model, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion    
        
        

        #region 物品多选确定（公用控件）
        /// <summary>
        /// 物品多选确定
        /// </summary>
        /// <param name="strid"></param>
        /// <param name="StorageID"></param>
        /// <returns></returns>
        public static DataTable GetTeachInfoTableByCheckcondition(string strid)
        {
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string CompanyCD = userInfo.CompanyCD;
                return TeachInfoDBHelper.GetTeachInfoTableByCheckcondition(strid, CompanyCD);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        
        #endregion

        #region 查询获取商品信息
        /// <summary>
        /// 查询获取商品信息
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static DataTable GetTeachInfo(TeachInfoModel Model, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                if (OrderBy.Equals("ID asc"))
                {
                    OrderBy = "ID desc";
                }
                return TeachInfoDBHelper.GetTeachInfo(Model, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

    
        #region 插入商品档案信息
        /// <summary>
        /// 插入商品档案信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertTeachInfo(TeachInfoModel model, out string ID)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            ID = "0";
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.TeachNo);
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                succ = TeachInfoDBHelper.InsertTeachInfo(model, out ID);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }
        }
        #endregion

        #region 修改商品信息
        /// <summary>
        /// 修改商品档案
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateTeachInfo(TeachInfoModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            try
            {
                bool succ = false;
                LogInfoModel logModel = InitLogInfo(model.TeachNo);
                logModel.Element = ConstUtil.LOG_PROCESS_UPDATE;
                succ = TeachInfoDBHelper.UpdateTeachInfo(model);
                if (!succ)
                    logModel.Remark = ConstUtil.LOG_PROCESS_FAILED;
                else
                    logModel.Remark = ConstUtil.LOG_PROCESS_SUCCESS;
                LogDBHelper.InsertLog(logModel);
                return succ;
            }
            catch (Exception ex)
            {
                WriteSystemLog(userInfo, ex);
                return false;
            }

        }
        #endregion

        #region 根据ID获取物品信息
        /// <summary>
        /// 根据ID获取物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetTeachInfoByID(int ID)
        {
            if (ID == 0)
                return null;
            try
            {
                return TeachInfoDBHelper.GetTeachInfoByID(ID);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

      

        #region 删除商品信息
        /// <summary>
        /// 删除物品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteTeachInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return false;
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            //string CompanyCD = "AAAAAA";
            bool isSucc = TeachInfoDBHelper.DeleteTeachInfo(ID, CompanyCD);
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
                LogInfoModel logModel = InitLogInfo("工艺ID：" + no);
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
            logSys.ModuleID = "2081701";
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
        private static LogInfoModel InitLogInfo(string TeachNo)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = "2081701";
            //设置操作日志类型 修改
            logModel.ObjectName = "TeachInfo";
            //操作对象
            logModel.ObjectID = TeachNo;
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion       
       
       


        #region 读取指定公司所有的商品分类
        public static DataTable GetTeachType(string CompanyCD)
        {
            return Data.Office.SupplyChain.TeachInfoDBHelper.GetTeachType(CompanyCD);
        }
        #endregion        

      
        #region 验证商品编号是否重复
        public static bool IsRepeatTeachNo(string CompanyCD, string TeachNo)
        {
            return Data.Office.SupplyChain.TeachInfoDBHelper.IsRepeatTeachNo(CompanyCD, TeachNo);

        }

        #endregion
    }
    }

