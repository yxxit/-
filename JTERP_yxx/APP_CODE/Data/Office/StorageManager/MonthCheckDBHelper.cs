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
    public class MonthCheckDBHelper
    {
        //获取月结信息
        public static DataTable GetMonthcheck()
        {
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.GetYearAndMonth", param);
            return dt;
        }
        //检查是否有未确认单据
        public static DataTable checkit( string sdate, string edate)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            
            param[1] = SqlHelper.GetParameter("@sdate", sdate);
            param[2] = SqlHelper.GetParameter("@edate", edate);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.CheckBillStatus", param);
            return dt;
        }

        //结算处理
        public static bool close(string year, string month, string sdate, string edate)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@year", year);
            param[2] = SqlHelper.GetParameter("@month", month);
           
            param[3] = SqlHelper.GetParameter("@checkman", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
            param[4] = SqlHelper.GetParameter("@sdate1", sdate);
            param[5] = SqlHelper.GetParameter("@edate1", edate);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.MonthAndDayClose", param);
            int num = int.Parse(dt.Rows[0][0].ToString());
            if (num == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //取消结算
        public static bool UnClose(string year, string month, string sdate, string edate)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@year", year);
            param[2] = SqlHelper.GetParameter("@month", month);
            param[3] = SqlHelper.GetParameter("@sdate1", sdate);
            param[4] = SqlHelper.GetParameter("@edate1", edate);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.UnClose", param);
            int num = int.Parse(dt.Rows[0][0].ToString());
            if (num == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //获取月结列表 
        public static DataTable MonthList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2, string prono, string proname)
        {
          

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
        //获取月结列表 
        public static DataTable DayList(int pageIndex, int pageCount, string ord, ref int totalCount, string time1, string time2, decimal CheckCount1, decimal CheckCount2, int storageid, decimal OutCount1, decimal OutCount2, decimal InCount1, decimal InCount2, string prono, string proname)
        {
            string sql = "";

            SqlCommand comm = new SqlCommand();
            sql = @"select convert(varchar(10),a.daytime,120)daytime,a.storageid,a.productid,a.prodno,a.productname,a.batchno,a.unitname,a.productcount,a.roadcount,a.ordercount,a.incount,a.outcount,isnull(i.storageincount,0)storageincount,isnull(o.storageincount,0) storageoutcount from 
(select a.companycd,a.daytime,a.storageid,a.productid,p.prodno,p.productname,isnull(a.batchno,'')batchno,isnull([dbo].[getCodename](p.unitid),'')unitname
,sum(productcount)productcount,sum(roadcount)roadcount,sum(ordercount)ordercount,sum(incount)incount,sum(outcount)outcount
 from officedba.daycheck a left join  officedba.productinfo p on p.id=a.productid group by  a.daytime,a.productid,p.prodno,p.productname,p.unitid,a.storageid,a.batchno,a.companycd) a left join 
(select a.companycd,a.daytime,a.productid,a.storageid,case when a.batchno is null then '' else a.batchno end batchno,isnull(sum(b.inoroutcount),0)storageincount from officedba.daycheck a left join  officedba.daycheckdetail b
	 on a.id=b.dayid where  billtype=1 and billname not in ('借货返还单','采购到货单','生产任务单') group by a.daytime,a.productid,a.storageid,a.batchno,a.companycd)i on 
	 a.productid=i.productid and a.storageid=i.storageid and a.batchno=i.batchno and a.daytime=i.daytime and a.companycd=i.companycd left join (select a.companycd,a.daytime,a.productid,a.storageid,case when a.batchno is null then '' else a.batchno end batchno,isnull(sum(b.inoroutcount),0)storageincount from officedba.daycheck a left join  officedba.daycheckdetail b
	 on a.id=b.dayid where  billtype=0 and billname not in ('借货申请单','销售发货单') group by a.daytime,a.productid,a.storageid,a.years,a.months,a.batchno,a.companycd)
	 o on   a.productid=o.productid and a.storageid=o.storageid and a.batchno=o.batchno and a.daytime=o.daytime and a.companycd=o.companycd where a.companycd='" + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD + "'";
            if (time1 != "" && time2 == "")
            {
                sql += " and a.daytime>='" + time1 + " 00:00:00'";
            }
            else if (time2 != "" && time1 == "")
            {
                sql += " and a.daytime<='" + time2 + " 23:59:59'";
            }
            else if (time1 != "" && time2 != "")
            {
                sql += " and a.daytime between '" + time1 + "  00:00:00' and '" + time2 + "  23:59:59' ";
            }
            if (CheckCount1 != 0 && CheckCount2 == 0)
            {
                sql += " and a.productcont >=" + CheckCount1 + "";
            }
            else if (CheckCount2 != 0 && CheckCount1 == 0)
            {
                sql += " and a.productcont <=" + CheckCount2 + "";
            }
            else if (CheckCount1 != 0 && CheckCount2 != 0)
            {
                sql += " and a.productcont between " + CheckCount1 + " and" + CheckCount2 + "";
            }

            if (InCount1 != 0 && InCount2 == 0)
            {
                sql += " and i.storageincount >= " + InCount1 + "";
            }
            else if (InCount2 != 0 && InCount1 == 0)
            {
                sql += " and i.storageincount <= " + InCount2 + "";
            }
            else if (InCount1 != 0 && InCount2 != 0)
            {
                sql += " and i.storageincount between " + InCount1 + " and " + InCount2 + "";
            }

            if (OutCount1 != 0 && OutCount2 == 0)
            {
                sql += " and o.storageincount >= " + OutCount1 + "";
            }
            else if (OutCount2 != 0 && OutCount1 == 0)
            {
                sql += " and o.storageincount <= " + OutCount2 + "";
            }
            else if (OutCount1 != 0 && OutCount2 != 0)
            {
                sql += " and o.storageincount between " + OutCount1 + " and " + OutCount2 + "";
            }
            if (prono != "")
            {
                sql+=" and a.prodno like '%"+prono+"%'";
            }
             if (proname != "")
            {
                sql += " and a.prodname like '%" + proname + "%'";
            }
             if (storageid > 0)
             {
                 sql += " and a.storageid =" + proname + "";
             }
             comm.CommandText = sql;
             return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
    }
}
