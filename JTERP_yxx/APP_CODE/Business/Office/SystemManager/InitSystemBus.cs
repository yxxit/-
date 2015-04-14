/**********************************************
 * 类作用：   系统初始化
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/22
 ***********************************************/
using System;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;

namespace XBase.Business.Office.SystemManager
{

    public class InitSystemBus
    {
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>        
        /// <returns></returns>
        public static bool DeleteInfo(string flag, string CompanyCD)
        {
            
            bool isSucc = InitSystemDBHelper.DeleteInfo(flag, CompanyCD);          
            
            return isSucc;
        }
        #endregion
    }
}