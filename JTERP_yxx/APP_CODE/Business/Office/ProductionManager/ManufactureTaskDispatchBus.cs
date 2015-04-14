
/**********************************************
 * 类作用：   生产派工事务层处理
 * 建立人：   沈健平
 * 建立时间： 2011/03/09
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.ProductionManager;
using XBase.Data.Office.ProductionManager;

namespace XBase.Business.Office.ProductionManager
{
    public class ManufactureTaskDispatchBus
    {
        #region 任务单列表
        /// <summary>
        /// 任务单列表
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable Getdetails(ManufactureTaskModel model,  int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return ManufactureTaskDispatchDBHelper.Getdetails(model, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
             #region 验证任务单是否有相关工序
        /// <summary>
        /// 验证任务单是否有相关工序
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable getcheck(string companycd,string ids,string tasknos,string pname)
        {
            try
            {
                return ManufactureTaskDispatchDBHelper.getcheck(companycd,ids,tasknos,pname);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
