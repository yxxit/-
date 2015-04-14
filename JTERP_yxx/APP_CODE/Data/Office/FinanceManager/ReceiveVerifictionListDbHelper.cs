using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Data.Office.FinanceManager
{
    public class ReceiveVerifictionListDbHelper
    {
        #region 查询核销明细
        /// <summary>
        ///  查询核销明细
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable Getdetails(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string hbno, string Deptment, string startdate1, string startdate2, string enddate1, string enddate2)
        {

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select ROW_NUMBER() OVER (ORDER BY billingid )id, billingid from officedba.BlendingDetails a left join officedba.Billing b on a.BillingID=b.id and a.companycd=b.companycd where a.companycd='"+companyCD+"' and PayOrInComeType=" + hbno + "  ");

            if (TaskNo != "")
            {
                searchSql.AppendLine(" and a.contactUnits=@TaskNo ");
            }
            if (Productname != "")
            {
                searchSql.AppendLine(" and b.billingnum like @Productname ");
            }
            if (Deptment != "0")
            {
                searchSql.AppendLine(" and a.InvoiceType=@Deptment ");
            }
            if (startdate1 != "" && startdate2 != "")
            {
                searchSql.AppendLine(" and b.CreateDate between @startdate1 and @startdate2 ");
            }
            else if (startdate1 != "")
            {
                searchSql.AppendLine(" and b.CreateDate>=@startdate1 ");
            }
            else if (startdate2 != "")
            {
                searchSql.AppendLine(" and b.CreateDat<=@startdate2 ");
            }
            if (enddate1 != "" && enddate2 != "")
            {
                searchSql.AppendLine(" and a.totalprice between @enddate1 and @enddate2 ");
            }
            else if (enddate1 != "")
            {
                searchSql.AppendLine(" and a.totalprice>=@enddate1 ");
            }
            else if (enddate2 != "")
            {
                searchSql.AppendLine(" and a.totalprice<=@enddate2 ");
            }
            //searchSql.AppendLine("or (a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (a.Principal IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

            searchSql.AppendLine("group by billingid ");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Deptment", Deptment));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startdate1", startdate1 + " 00:00:00"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startdate2", startdate2 + " 23:59:59"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate1", enddate1  ));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate2", enddate2 ));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo",   TaskNo ));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", '%' + Productname + '%'));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();

            DataTable dt1 = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            #region 获取开票id
            string billingid = "";
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (i == dt1.Rows.Count - 1)
                        billingid += dt1.Rows[i]["billingid"].ToString();
                    else
                        billingid += dt1.Rows[i]["billingid"].ToString() + ",";
                }
            }
            
            if (billingid == "")
            {
                billingid = "''";
            }

            #endregion
            #region 查询核销明细
            comm.CommandText = @"select a.id,a.billingid,convert(char(10),b.CreateDate,120)bCreateDate,a.contactUnits,case when a.InvoiceType=1 then '增值税发票' when a.InvoiceType=2 then '普通地税' when a.InvoiceType=3 then '普通国税' when a.InvoiceType=4 then '收据' end as InvoiceType,
b.BillingNum,c.currencyname,c.exchangerate,a.totalprice,a.yaccounts,a.naccounts,case when b.AcceWay=0 then '现金' when b.AcceWay=1 then '银行转账' end as AcceWay,
convert(char(10),a.createdate,120)acreatedate,case when a.PayOrInComeType=2 then '收款单' when a.PayOrInComeType=1 then '付款单' end as PayOrInComeType,ShouFuKuanNO
from officedba.BlendingDetails a left join officedba.Billing b on a.BillingID=b.id and a.companycd=b.companycd
left join officedba.CurrencyTypeSetting c on a.CurrencyType=c.id and  a.companycd=c.companycd
where a.companycd='"+companyCD+"' and PayOrInComeType=" + hbno + " and a.billingid in (" + billingid + ") order by a.id ";
            #endregion
            //执行查询
            DataTable dt2 = SqlHelper.ExecuteSearch(comm);
            #region 新建DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("bCreateDate");
            dt.Columns.Add("contactUnits");
            dt.Columns.Add("InvoiceType");
            dt.Columns.Add("BillingNum");
            dt.Columns.Add("currencyname");
            dt.Columns.Add("exchangerate");
            dt.Columns.Add("totalprice");
            dt.Columns.Add("yaccounts");
            dt.Columns.Add("naccounts");
            dt.Columns.Add("AcceWay");
            dt.Columns.Add("acreatedate");
            dt.Columns.Add("PayOrInComeType");
            dt.Columns.Add("ShouFuKuanNO");
            #endregion
            int row=0;
            #region 获取DT
            if (dt2.Rows.Count > 0)
            {
                if (billingid != "''")
                {
                    string[] str = billingid.Split(',');
                    for (int i = 0; i < str.Length; i++)
                    {
                        int a = 0;
                        row=0;
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            
                            if (str[i] == dt2.Rows[j]["billingid"].ToString())
                            {
                                row++;
                                if (a == 0)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["id"] = dt2.Rows[j]["id"].ToString();
                                    dr["bCreateDate"] = dt2.Rows[j]["bCreateDate"].ToString();
                                    dr["contactUnits"] = dt2.Rows[j]["contactUnits"].ToString();
                                    dr["InvoiceType"] = dt2.Rows[j]["InvoiceType"].ToString();
                                    dr["BillingNum"] = dt2.Rows[j]["BillingNum"].ToString();
                                    dr["currencyname"] = dt2.Rows[j]["currencyname"].ToString();
                                    dr["exchangerate"] = dt2.Rows[j]["exchangerate"].ToString();
                                    dr["totalprice"] = dt2.Rows[j]["totalprice"].ToString();
                                    dr["yaccounts"] = "";
                                    dr["naccounts"] = "";
                                    dr["AcceWay"] = "";
                                    dr["acreatedate"] = "";
                                    dr["PayOrInComeType"] = "";
                                    dr["ShouFuKuanNO"] = "";
                                    dt.Rows.Add(dr);
                                    a++;
                                    row++;
                                }
                                DataRow drow = dt.NewRow();

                                drow["id"] = dt2.Rows[j]["id"].ToString();
                                drow["bCreateDate"] = "";
                                drow["contactUnits"] = "";
                                drow["InvoiceType"] = "";
                                drow["BillingNum"] = "";
                                drow["currencyname"] = "";
                                drow["exchangerate"] = "";
                                drow["totalprice"] = "";
                                drow["yaccounts"] = dt2.Rows[j]["yaccounts"].ToString();
                                drow["naccounts"] = dt2.Rows[j]["naccounts"].ToString();
                                drow["AcceWay"] = dt2.Rows[j]["AcceWay"].ToString();
                                drow["acreatedate"] = dt2.Rows[j]["acreatedate"].ToString();
                                drow["PayOrInComeType"] = dt2.Rows[j]["PayOrInComeType"].ToString();
                                drow["ShouFuKuanNO"] = dt2.Rows[j]["ShouFuKuanNO"].ToString();
                                dt.Rows.Add(drow);
                            }
                        }
                    }
                }
            }
            #endregion
            return dt;
        }
        #endregion
    }
}
