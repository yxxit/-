/*************************************
 * 创建人：宋凯歌
 * 创建日期：2010-11-26
 * 描述：销售出库
 ************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.SellManager;
using XBase.Model.Office.SellManager;

namespace XBase.Business.Office.SellManager
{
    public class CostsForeignBus
    {
        #region 获取业务员统计表
        /// <summary>
        /// 获取业务员统计表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetCostsForeignList(SellOutStorageForeignModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return CostsForeignDBHelper.GetCostsForeignList(model, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 获取业务员统计表
        /// <summary>
        /// 获取业务员统计表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="myOrder">排序方式</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutReportList(SellOutStorageForeignModel model, string myOrder)
        {
            return CostsForeignDBHelper.GetSellOutReportList(model, myOrder);
        }
        #endregion
    }
}
