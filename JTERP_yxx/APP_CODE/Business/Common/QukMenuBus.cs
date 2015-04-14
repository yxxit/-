using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Common;
using XBase.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace XBase.Business.Common
{
    public class QukMenuBus
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuAdd(QukMenuModel model)
        {
            return Data.Common.QukMenuDBHelper.QukMenuAdd(model);
        }
         #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuUpdate(QukMenuModel model)
        {
            return Data.Common.QukMenuDBHelper.QukMenuUpdate(model);
        }

        #endregion
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuDel(string QukMenuName)
        {
            return Data.Common.QukMenuDBHelper.QukMenuDel(QukMenuName);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool QukMenuSelect( string QukM_ID, string CompanyCD, string UserID)
        {
            return Data.Common.QukMenuDBHelper.QukMenuSelect( QukM_ID,  CompanyCD,  UserID);
        }
        public static DataTable QukMenuSelect(string CompanyCD, string UserID)
        {
            return Data.Common.QukMenuDBHelper.QukMenuSelect(CompanyCD, UserID);
        } 
    }
}
