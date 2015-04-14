using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Common;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SystemManager;

namespace XBase.Business.Office.SystemManager
{
   public class BusiLogicSetBus
    {
        ///<summary>
        ///设置业务规则
        ///</summary>
        public static bool SetBus(string  CompanyCD, string LogicType, string LogicID,string LogicName,string LogicSet,string Description)
        {
            return BusiLogicSetDBHelper.SetBus(CompanyCD, LogicType, LogicID, LogicName, LogicSet, Description);
        }

        ///<summary>
        ///获取业务规则
        ///</summary>
        public static bool GetBus(string CompanyCD, string LogicType, string LogicID, bool isset)
        {
            DataTable dt = BusiLogicSetDBHelper.GetBus(CompanyCD, LogicType, LogicID);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return isset;
            }
            else
            {
                if (dt.Rows[0]["LogicSet"].ToString() == "1")
                    return true;
                else
                    return false;
            }
        }

        #region 读取配置
        public static DataTable GetValue(string CompanyCD, string LogicType, string LogicID)
        {
            DataTable dt = BusiLogicSetDBHelper.GetBus(CompanyCD, LogicType, LogicID);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        #endregion

        #region 设置提醒内容
        public static bool SetAlertContent(string CompanyCD,string FunctionType,string UserID,string status,string iDays,string Lock)
        {
            return BusiLogicSetDBHelper.SetAlertContent(CompanyCD, FunctionType, UserID, status,iDays,Lock);
        }
        #endregion

       /// <summary>
       /// 读取证照预警天数
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="FunctionType"></param>
       /// <param name="UserID"></param>
       /// <param name="flag"></param>
       /// <returns></returns>
        public static string GetWaringDays(string CompanyCD, string FunctionType, string UserID, bool flag)
        {
            DataTable dt = BusiLogicSetDBHelper.GetAlertContentNoUser(CompanyCD, FunctionType, UserID);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "0";
            }
            else
            {
                try
                {
                    string iDays = dt.Rows[0]["iDays"].ToString();
                    if (iDays == "")
                    {
                        iDays = "0";
                    }
                    return iDays;
                }
                catch (Exception ee)
                {
                    return "0";
                }
            }
        }

       /// <summary>
       /// 是否超期锁定
       /// </summary>
       /// <param name="CompanyCD"></param>
       /// <param name="FunctionType"></param>
       /// <param name="UserID"></param>
       /// <param name="flag"></param>
       /// <returns></returns>
        public static string GetLock(string CompanyCD, string FunctionType, string UserID, bool flag)
        {
            DataTable dt = BusiLogicSetDBHelper.GetLockNoUser(CompanyCD, FunctionType, UserID);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return "是";
            }
            else
            {
                try
                {
                    string Lock = dt.Rows[0]["Lock"].ToString();
                    if (Lock == "")
                    {
                        Lock = "是";
                    }
                    return Lock;
                }
                catch (Exception ee)
                {
                    return "是";
                }
            }
        }


        #region 读取提醒内容

        public static bool GetAlertContent(string CompanyCD, string FunctionType, string UserID, bool flag)
        {
            DataTable dt = BusiLogicSetDBHelper.GetAlertContent(CompanyCD, FunctionType, UserID);

            if (dt == null || dt.Rows.Count <= 0)
            {
                return flag;
            }
            else
            {
                if (dt.Rows[0]["usedStatus"].ToString() == "1")
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region 读取提醒内容
        public static DataTable GetAlertContent(string CompanyCD,string FunctionType,string UserID)
        {
            DataTable dt = BusiLogicSetDBHelper.GetAlertContent(CompanyCD, FunctionType, UserID);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return dt;
            }
        }
        #endregion

    }
}
