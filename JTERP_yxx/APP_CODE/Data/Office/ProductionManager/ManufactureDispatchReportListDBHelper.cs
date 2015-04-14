/**********************************************
 * 作用：     处理生产进度汇报的数据请求
 * 建立人：   沈健平
 * 建立时间： 2011/03/21
 ***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;

namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureDispatchReportListDBHelper
    {
        #region 通过检索条件查询生产进度汇报信息
        /// <summary>
        /// 通过检索条件查询生产进度汇报信息
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
            searchSql.AppendLine(@"select a.id,b.id as did,taskno,a.ReportNo,j.deptname,convert(varchar(10),reportdate,120)reportdate,i.Sequname,i.wcname,isnull(i.employeesname,'')employeesname,k.employeesname as operators,convert(numeric(7,2),isnull(i.productcount,0))productcount,convert(numeric(7,2),isnull(b.FinishNum,0))FinishNum,convert(numeric(7,2),isnull(PassNum,0))PassNum,convert(numeric(7,2),isnull(b.FinishNum,0)-isnull(PassNum,0)) as nopassnum,i.timeunit,convert(numeric(7,2),isnull(b.WorkTime,0))WorkTime,convert(varchar(10),RealstartDate,120)RealstartDate,convert(varchar(10),RealEndDate,120)RealEndDate,isnull(i.productname,'')productname
from  officedba.ManufactureProgressRpt a left join  officedba.ManufactureProgressRptDetail b on a.reportno=b.reportno left join officedba.deptinfo j on j.id=a.deptid left join (select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,
isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a left join officedba.deptinfo  as b on a.DeptID=b.ID where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3') k on k.id=b.operator  left join
(select c.taskno,p.productname,h.productcount,d.id as pgid,e.Sequname,chargeman,d.timeunit,f.wcname,g.employeesname from  officedba.Manufacturedispatching c left join officedba.ManufacturedispatchingDetail d on c.id=d.taskid left join officedba.StandardSequ e on e.id=d.SequNo left join officedba.WorkCenter f on e.wcid=f.id left join 
(select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,
isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a left join officedba.deptinfo  as b on a.DeptID=b.ID where a.CompanyCD=@CompanyCD and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3') g on g.id=d.chargeman left join officedba.ManufactureTaskDetail h on h.taskno=c.taskno and d.manutaskdetilid=h.id
 left join officedba.Productinfo p on p.id=h.Productid)i 
on b.Morderno=i.pgid where a.companycd=@CompanyCD");


            if (hbno != "") 
            {
                searchSql.AppendLine(" and a.ReportNo like @hbno ");
            }
           
            if (TaskNo != "")
            {
                searchSql.AppendLine(" and taskno like @TaskNo ");
            }
            if (Productname != "")
            {
                searchSql.AppendLine(" and Productname like @Productname ");
            }
            if (Deptment != "")
            {
                searchSql.AppendLine(" and deptname=@Deptment ");
            }
            if (startdate1 != ""&&enddate1!="")
            {
                searchSql.AppendLine(" and RealstartDate between @startdate1 and @startdate2 or RealEndDate between @enddate1 and @enddate2 ");
            }
            else if (startdate1 != "")
            {
                searchSql.AppendLine(" and RealstartDate between @startdate1 and @startdate2 ");
            }
            else if(enddate1!="")
            {
                searchSql.AppendLine(" and RealEndDate between @enddate1 and @enddate2 ");
            }

            //searchSql.AppendLine("or (a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (a.Principal IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Deptment", Deptment));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startdate1", startdate1+" 00:00:00"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@startdate2", startdate2 + " 23:59:59"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate1", enddate1 + " 00:00:00"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@enddate2", enddate2 + " 23:59:59"));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", '%' +TaskNo + '%'));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", '%' + Productname + '%'));

            comm.Parameters.Add(SqlHelper.GetParameterFromString("@hbno", '%' + hbno + '%'));
            

            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
    }
}
