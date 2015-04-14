using System;
using XBase.Model.Office.LocalAlertManager;
using XBase.Data.Office.LocalAlertManager;
using XBase.Common;
using System.Data;

namespace XBase.Business.Office.LocalAlertManager
{
   public class LocalAlertSetBus
   {
       #region 保存预警提醒设置
       /// <summary>
       /// 保存预警提醒设置
       /// </summary>
       /// <param name="LocalAlertSet_M"></param>
       /// <returns></returns>
       public static bool SaveLocalAlertSet(LocalAlertSetModel LocalAlertSet_M)
        {
            return LocalAlertSetDBHelper.SaveLocalAlertSet(LocalAlertSet_M);
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
          return LocalAlertSetDBHelper.GetLocalAlertSetByCompanyCD(CompanyCD);
       }
       #endregion
   }
}
