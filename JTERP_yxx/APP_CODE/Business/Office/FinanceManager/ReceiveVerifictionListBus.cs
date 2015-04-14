using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.FinanceManager;

namespace XBase.Business.Office.FinanceManager
{
    public class ReceiveVerifictionListBus
    {
        //通过检索条件检索核销明细
        public static DataTable Getdetails(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string hbno, string Deptment, string startdate1, string startdate2, string enddate1, string enddate2)
        {
            try
            {
                return ReceiveVerifictionListDbHelper.Getdetails(pageIndex, pageCount, OrderBy, ref totalCount, TaskNo, Productname, hbno, Deptment, startdate1, startdate2, enddate1, enddate2);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
