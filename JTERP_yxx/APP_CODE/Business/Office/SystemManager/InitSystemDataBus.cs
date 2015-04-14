/**********************************************
 * 类作用：   系统初始化添加数据
 * 建立人：   宋凯歌
 * 建立时间： 2010/11/12
 ***********************************************/
using System;
using XBase.Data.Office.SystemManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Business.Common;
using System.Data;
using XBase.Model.Office.SystemManager;


namespace XBase.Business.Office.SystemManager
{
    public class InitSystemDataBus
    {
        #region 添加基础数据
        /// <summary>
        /// 添加基础数据
        /// </summary>        
        /// <returns></returns>
        public static bool AddInitSystemUserData(InitSystemDataModel model)
        {

            bool isSucc = InitSystemDataDBHelper.AddInitSystemUserData(model);

            return isSucc;
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加基础数据
        /// </summary>        
        /// <returns></returns>
        public static bool AddInitSystemData(InitSystemDataModel model)
        {

            bool isSucc = InitSystemDataDBHelper.AddInitSystemData(model);

            return isSucc;
        }
        #endregion
    }
}
