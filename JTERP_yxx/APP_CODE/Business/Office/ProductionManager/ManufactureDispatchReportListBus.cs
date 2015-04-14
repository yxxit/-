/**********************************************
 * 作用：     处理生产进度汇报的数据请求
 * 建立人：   沈健平
 * 建立时间： 2011/03/21
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.ProductionManager;
using System.Data;

namespace XBase.Business.Office.ProductionManager
{
    public class ManufactureDispatchReportListBus
    {
        //通过检索条件查询生产进度汇报信息
        public static DataTable Getdetails(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string hbno, string Deptment, string startdate1, string startdate2, string enddate1, string enddate2)
        {
            try
            {
                return ManufactureDispatchReportListDBHelper.Getdetails( pageIndex, pageCount, OrderBy, ref totalCount,TaskNo,Productname,hbno,Deptment,startdate1,startdate2,enddate1,enddate2);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
