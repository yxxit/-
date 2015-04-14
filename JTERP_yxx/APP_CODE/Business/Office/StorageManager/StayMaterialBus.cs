/********************************************** 
 * 类作用：   获取呆料列表
 * 建立人：   宋凯歌
 * 建立时间： 2011/01/14
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Collections.Generic;
using XBase.Common;

namespace XBase.Business.Office.StorageManager
{
    public class StayMaterialBus
    {
        #region 查询：库存呆料列表
        /// <summary>
        /// 呆料列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStayMaterialInfo(StayMaterialModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StayMaterialDBHelper.GetStayMaterialInfo(model, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 查询：库存呆料列表 new
        /// <summary>
        /// 呆料列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStayMaterialInfo1(StayMaterialModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return StayMaterialDBHelper.GetStayMaterialInfo1(model, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
    }
}
