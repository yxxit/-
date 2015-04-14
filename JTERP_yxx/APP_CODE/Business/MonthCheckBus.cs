using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.StorageManager;
using System.Data;

namespace XBase.Business.Office.StorageManager
{
    public class MonthCheckBus
    {
        //获取月结信息
        public static DataTable GetMonthcheck()
        {
            return MonthCheckDBHelper.GetMonthcheck();
        }
        //检查单据是否已处理
        public static DataTable checkit( string sdate, string edate)
        {
            return MonthCheckDBHelper.checkit( sdate, edate);
        }
        //执行结算
        public static bool close(string year, string month, string sdate, string edate)
        {
            return MonthCheckDBHelper.close(year, month, sdate, edate);
        }
        //取消结算
        public static bool UnClose(string year, string month, string sdate, string edate)
        {
            return MonthCheckDBHelper.UnClose(year, month, sdate, edate);
        }
        //查询月结
        public static DataTable MonthList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2, string prono, string proname)
        {
            return MonthCheckDBHelper.MonthList(pageIndex, pageCount, ord, ref totalCount, time1, time2, CheckCount1, CheckCount2, storageid, OutCount1, OutCount2, InCount1, InCount2, prono, proname);
        }
        //查询日结
        public static DataTable DayList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2,string  prono,string proname)
        {
            return MonthCheckDBHelper.DayList(pageIndex, pageCount, ord, ref totalCount, time1, time2, CheckCount1, CheckCount2, storageid, OutCount1, OutCount2, InCount1, InCount2,prono,proname);
        }
    }
}
