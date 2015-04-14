using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.StorageManager;

namespace XBase.Business.Office.StorageManager
{
    public class MonthAndDayCheckListBus
    {
        //查询月结
        public static DataTable MonthList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2,string prono,string proname)
        {
            return MonthAndDayCheckListDBHelper.MonthList(pageIndex, pageCount, ord, ref totalCount, time1, time2, CheckCount1, CheckCount2, storageid, OutCount1, OutCount2, InCount1, InCount2,prono,proname);
        }
    }
}
