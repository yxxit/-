using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class MonthAndDayCheckListDBHelper
    {
        //获取月结列表 
        public static DataTable MonthList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2,string prono,string proname)
        {
            string sql = "";

            SqlCommand comm = new SqlCommand();
            SqlParameter[] param = new SqlParameter[16];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@time1", time1);
            param[2] = SqlHelper.GetParameter("@time2", time2);
            param[3] = SqlHelper.GetParameter("@count1", CheckCount1);
            param[4] = SqlHelper.GetParameter("@count2", CheckCount2);
            param[5] = SqlHelper.GetParameter("@incount1", InCount1);
            param[6] = SqlHelper.GetParameter("@incount2", InCount2);
            param[7] = SqlHelper.GetParameter("@outcount1", OutCount1);
            param[8] = SqlHelper.GetParameter("@outcount2", OutCount2);
            param[9] = SqlHelper.GetParameter("@Storageid", storageid);
            param[10] = SqlHelper.GetParameter("@proNo", prono);
            param[11] = SqlHelper.GetParameter("@proName", proname);
            param[12] = SqlHelper.GetParameter("@point", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            param[13] = SqlHelper.GetParameter("@pagesize", pageCount);
            param[14] = SqlHelper.GetParameter("@pageindex", pageIndex);
            param[15] = SqlHelper.GetParameter("@orderby", ord);
            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteStoredProcedure("[officedba].[GetMonthList]", param);
            if (dt.Rows.Count > 0)
            {
                totalCount = int.Parse(dt.Rows[0]["totalcount"].ToString());
            }
            else
            {
                totalCount = 0;
            }
            return dt;
        }
    }
}
