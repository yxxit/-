using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data;


namespace XBase.Data.Office.SellManager
{
    public class ArrearsAnalysisDBHelper
    {
        #region 查询客户欠款
        /// <summary>
        ///  查询客户欠款
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable getcust(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string CurrencyType, string Deptment, string enddate1, string enddate2)
        {

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            string date="";
            string date1="";
            if(Productname!="")
            {
                date=" and auditdate<='"+Productname+" 23:59:59' ";
                date1=" and confirmdate<='"+Productname+" 23:59:59' ";
            }
            string sql = string.Format(@"select * from (select ROW_NUMBER() OVER (ORDER BY a.custid )id,a.custid,a.CurrencyType,a.companycd,e.custno,e.custname,isnull(a.totalprice,0.00)-isnull(d.totalprice,0.00)-isnull(c.totalprice,0.00) as qprice,
isnull(e.MaxCredit,0.00)MaxCredit,isnull(e.MaxCredit,0.00)-(isnull(a.totalprice,0.00)-isnull(d.totalprice,0.00)-isnull(c.totalprice,0.00)) xindu, 
isnull(b.totalprice,0.00)-isnull(c.totalprice,0.00)goodsprice,isnull(a.totalprice,0.00)-isnull(b.totalprice,0.00)naccounts,convert(numeric(8,2),isnull(d.naccounts,0.00))yprice
 from (select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (0,1,4,7) and accountsorcope=1 and custid is not null {0} group by companycd,CurrencyType,custid) as a
left join (select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (1,4,7) and accountsorcope=1 and custid is not null {0} group by companycd,CurrencyType,custid)as b
on a.custid=b.custid and a.companycd=b.companycd and a.CurrencyType=b.CurrencyType left join 
(select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (3) and accountsorcope=2 and custid is not null {0} group by companycd,CurrencyType,custid)as c
on a.custid=c.custid and a.companycd=c.companycd and a.CurrencyType=c.CurrencyType left join 
(select companycd,CurrencyType,custid,sum(isnull(totalprice,0))totalprice,sum(isnull(naccounts,totalprice))naccounts from Officedba.IncomeBill
where Confirmor is not null and (AccountsStatus<>2 or AccountsStatus is null) and  fromtbname='officedba.CustInfo' {1} group by companycd,CurrencyType,custid)as d
on a.custid=d.custid and a.companycd=d.companycd and a.CurrencyType=d.CurrencyType left join officedba.CustInfo e on a.companycd=e.companycd and a.custid=e.id 
)ArrearsAnalysis where  companycd='{2}' and (qprice<>0 or naccounts<>0 or yprice<>0 or goodsprice<>0 ) and custno is not null ", date, date1, companyCD);
            searchSql.AppendLine(sql);
            if (TaskNo != "")
            {
                searchSql.AppendLine(" and custid=@TaskNo ");
            }
            if (Deptment == "1")
            {
                searchSql.AppendLine(" and  xindu<0 ");
            }
            else if (Deptment=="2")
            {
                searchSql.AppendLine(" and  xindu>0 ");
            }
            if (enddate1 != "" && enddate2 != "")
            {
                searchSql.AppendLine(" and qprice between @enddate1 and @enddate2 ");
            }
            else if (enddate1 != "")
            {
                searchSql.AppendLine(" and qprice>=@enddate1 ");
            }
            else if (enddate2 != "")
            {
                searchSql.AppendLine(" and qprice<=@enddate2 ");
            }
            if (CurrencyType != "")
            {
                searchSql.AppendLine(" and CurrencyType=" + CurrencyType + " ");
            }
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate1", enddate1));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate2", enddate2));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            DataTable dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            return dt;
        }
        #endregion


        #region 查询供应商欠款
        /// <summary>
        ///  查询供应商欠款
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable getpro(int pageIndex, int pageCount, string OrderBy, ref int totalCount, string TaskNo, string Productname, string CurrencyType, string Deptment, string enddate1, string enddate2)
        {

            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            string date = "";
            string date1 = "";
            if (Productname != "")
            {
                date = " and auditdate<='" + Productname + " 23:59:59' ";
                date1 = " and confirmdate<='" + Productname + " 23:59:59' ";
            }
            string sql = string.Format(@"select * from (select ROW_NUMBER() OVER (ORDER BY a.custid )id,a.custid,a.CurrencyType,a.companycd,e.custno,e.custname,isnull(a.totalprice,0.00)-isnull(d.totalprice,0.00)-isnull(c.totalprice,0.00) as qprice,
'0.00' MaxCredit,0-(isnull(a.totalprice,0.00)-isnull(d.totalprice,0.00)-isnull(c.totalprice,0.00)) xindu, 
isnull(b.totalprice,0.00)-isnull(c.totalprice,0.00)goodsprice,isnull(a.totalprice,0.00)-isnull(b.totalprice,0.00)naccounts,convert(numeric(8,2),isnull(d.naccounts,0.00))yprice
 from (select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (0,2,6) and accountsorcope=2 and custid is not null {0} group by companycd,CurrencyType,custid) as a
left join (select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (2,6) and accountsorcope=2 and custid is not null {0} group by companycd,CurrencyType,custid)as b
on a.custid=b.custid and a.companycd=b.companycd and a.CurrencyType=b.CurrencyType left join 
(select companycd,CurrencyType,custid,sum(isnull(naccounts,totalprice))totalprice from  officedba.Billing where auditor is not null and 
(AccountsStatus<>1 or AccountsStatus is null) and BillingType in (5) and accountsorcope=1 and custid is not null {0} group by companycd,CurrencyType,custid)as c
on a.custid=c.custid and a.companycd=c.companycd and a.CurrencyType=c.CurrencyType left join 
(select companycd,CurrencyType,custid,sum(isnull(payamount,0))totalprice,sum(isnull(naccounts,payamount))naccounts from officedba.paybill
where Confirmor is not null and (AccountsStatus<>2 or AccountsStatus is null) and  fromtbname='officedba.ProviderInfo' {1} group by companycd,CurrencyType,custid)as d
on a.custid=d.custid and a.companycd=d.companycd and a.CurrencyType=d.CurrencyType left join officedba.ProviderInfo e on a.companycd=e.companycd and a.custid=e.id 
)ArrearsAnalysis where  companycd='{2}' and custid is not null and (qprice<>0 or naccounts<>0 or yprice<>0 or goodsprice<>0 ) and custno is not null ", date, date1, companyCD);
            searchSql.AppendLine(sql);
            if (TaskNo != "")
            {
                searchSql.AppendLine(" and custid=@TaskNo ");
            }
            if (Deptment == "1")
            {
                searchSql.AppendLine(" and  xindu<0 ");
            }
            else if (Deptment == "2")
            {
                searchSql.AppendLine(" and  xindu>0 ");
            }
            if (enddate1 != "" && enddate2 != "")
            {
                searchSql.AppendLine(" and qprice between @enddate1 and @enddate2 ");
            }
            else if (enddate1 != "")
            {
                searchSql.AppendLine(" and qprice>=@enddate1 ");
            }
            else if (enddate2 != "")
            {
                searchSql.AppendLine(" and qprice<=@enddate2 ");
            }
            if (CurrencyType != "")
            {
                searchSql.AppendLine(" and CurrencyType=" + CurrencyType + " ");
            }
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate1", enddate1));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate2", enddate2));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", Productname + " 23:59:59"));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            DataTable dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
            return dt;
        }
        #endregion
    }
}
