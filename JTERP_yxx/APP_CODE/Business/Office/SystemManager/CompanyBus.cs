/**********************************************
 * 类作用：   新建帐套事务层处理
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/11
 ***********************************************/

using System;
using XBase.Model.Office.SystemManager;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using System.Data;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using XBase.Model.Common;

namespace XBase.Business.Office.SystemManager
{
    /// <summary>
    /// 类名：CompanyBus
    /// 描述：新建帐套事务层处理
    /// 
    /// 作者：钱锋锋
    /// 创建时间：2010/10/11
    /// 最后修改时间：2010/10/11
    /// </summary>
    ///
    public class CompanyBus
    {
        /// <summary>
        /// 用户信息更新或者插入
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>更新成功与否</returns>
        public static bool InsertCompany(CompanyModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码           
            //定义返回变量
            bool isSucc = false;
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //待修改
            string loginCompanyCD=((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD; 
            //string loginUserID = "admin";
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //操作日志
            LogInfoModel logModel = InitLogInfo(loginUserID);
           
                try
                {
                    //设置操作日志类型 新建
                    logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;//操作对象
                    logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                    //执行插入操作
                    isSucc = CompanyDBHelper.InsertCompany(model, loginUserID,loginCompanyCD);
                }
                catch (Exception ex)
                {
                    //输出日志
                    WriteSystemLog(userInfo, ex);
                }
            

      
            return isSucc;
        }
        public static bool UpdateCompany(CompanyModel model)
        {

            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码           
            //定义返回变量
            bool isSucc = false;
            //获取登陆用户ID
            string loginUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID; //待修改
            string loginCompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            //string loginUserID = "admin";
            /* 
             * 定义日志内容变量 
             * 增删改相关的日志，需要输出操作日志，该类型日志插入到数据库
             * 其他的 如出现异常时，需要输出系统日志，该类型日志保存到日志文件
             */
            //操作日志
            LogInfoModel logModel = InitLogInfo(loginUserID);

            try
            {
                //设置操作日志类型 新建
                logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_UserInfo;//操作对象
                logModel.Element = ConstUtil.LOG_PROCESS_INSERT;
                //执行插入操作
                isSucc = CompanyDBHelper.UpdateCompany(model);
            }
            catch (Exception ex)
            {
                //输出日志
                WriteSystemLog(userInfo, ex);
            }



            return isSucc;
        }
        #region 设置操作日志内容
        /// <summary>
        /// 设置操作日志内容
        /// </summary>
        /// <returns></returns>
        private static LogInfoModel InitLogInfo(string roleList)
        {
            LogInfoModel logModel = new LogInfoModel();
            //获取登陆用户信息
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            //设置公司代码
            logModel.CompanyCD = userInfo.CompanyCD;
            //设置登陆用户ID
            logModel.UserID = userInfo.UserID;
            //设置模块ID 模块ID请在ConstUtil中定义，以便维护
            logModel.ModuleID = ConstUtil.Menu_AddCompany;
            //设置操作日志类型 修改
            logModel.ObjectName = ConstUtil.CODING_RULE_TABLE_Company;
            //操作对象
            //涉及关键元素 这个需要根据每个页面具体设置，本页面暂时设置为空
            logModel.Element = string.Empty;

            return logModel;

        }
        #endregion
        #region 输出系统日志
        /// <summary>
        /// 输出系统日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="ex">异常信息</param>
        private static void WriteSystemLog(UserInfoUtil userInfo,  Exception ex)
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
            
                logSys.ModuleID = ConstUtil.Menu_AddCompany;
            
           
            //描述
            logSys.Description = ex.ToString();

            //输出日志
            LogUtil.WriteLog(logSys);
        }
        #endregion
        /// <summary>
        /// 验证唯一
        /// </summary>        
        /// <param name="codeValue"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static bool CheckCompanyCD(string codeValue)
        {
            return CompanyDBHelper.CheckCompanyCD(codeValue);
        }
        /// <summary>
        /// 获取超级管理员账号信息
        /// </summary>        
        /// <param name="codeValue"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static string GetUserID(string CompanyCD)
        {
            return CompanyDBHelper.GetUserID(CompanyCD);
        }
        public static DataTable  GetCompany()
        {

            

            try
            {
               return  CompanyDBHelper.GetCompany();
            }
            catch (Exception ex)
            {
                return null;
            }



        }
    }
}
