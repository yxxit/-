/**********************************************
 * 类作用：   帮助
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/18
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XBase.Data;
namespace XBase.Business
{
    /// <summary>
    /// 类名：LoginBus
    /// 描述：获取登陆用户的信息
    /// 
    /// 作者：吴志强
    /// 创建时间：2008/12/30
    /// 最后修改时间：2008/12/30
    /// </summary>
    ///
    public class HelpBus
    {

       


        #region 获取帮助信息
        /// <summary>
        /// 获取登陆用户的信息
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="companyCD">公司代码</param>
        /// <returns>DataTable 用户信息</returns>
        public static DataTable HelpInfo(string ModuleID)
        {
            return HelpDBHelper.HelpInfo(ModuleID);
        }

        #endregion

      
    }
}
