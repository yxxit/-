/**********************************************
 * 脚本作用： 生产派工的页面操作
 * 建立人：   沈健平
 * 建立时间： 2011/03/16
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using XBase.Model.Office.ProductionManager;

namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureDispatchDBHelper
    {

        public static int comfirmor = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        public static string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        public static string companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        #region 查询最新的派工单
        /// <summary>
        /// 查询最新的派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetManufactureDispatch(string taskno,string  pageno)
        {
            companycd = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            int pageindex = 0;
            pageindex = int.Parse(pageno);
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select *,case when 1=1 then 0 end as totallist from (select bid,isnull(confirmor,0)confirmor,isnull(closer,0)closer,a.taskno,convert(numeric(7,2),
productcount)productcount,convert(varchar(10),c.createdate,120)createdate,c.manutaskdetilid,
a.productid,a.ProdNo,a.Specification,a.CodeName,convert(varchar(10),
a.startdate,120)startdate,convert(varchar(10),a.enddate,120)enddate from
(select productcount,e.id,taskno,productid,ProdNo,
isnull(Specification,'')Specification,h.CodeName,e.startdate,e.enddate from officedba.ManufactureTaskDetail e left join 
officedba.ProductInfo f on e.productid=f.id inner join officedba.CodeUnitType h on f.UnitID=h.id)
 a inner join (
select b.id as bid,b.confirmor,b.closer,createdate,taskno,manutaskdetilid from officedba.Manufacturedispatching b inner join ");
            if (taskno != "" && taskno != "0")
            {
                searchSql.AppendLine("(  select taskid,manutaskdetilid from officedba.ManufacturedispatchingDetail where companycd='" + companycd + "' and taskid in(select taskid from officedba.ManufacturedispatchingDetail  where companycd='" + companycd + "' and id=" + taskno + " ) group by taskid,manutaskdetilid )");
            }
            else
            {
                searchSql.AppendLine(@"(select taskid,manutaskdetilid from officedba.ManufacturedispatchingDetail where  taskid in
(select TOP 1  detail.taskid from officedba.ManufacturedispatchingDetail detail right JOIN 
(select * from(select ROW_NUMBER() OVER (ORDER BY taskid desc)
 AS ROWID, taskid from officedba.ManufacturedispatchingDetail where companycd='" +companycd+@"'   group by taskid)as aa where rowid="+pageindex+@")
as tt on detail.taskid=tt.taskid where companycd='" + companycd + "') group by taskid,manutaskdetilid )");
            }
            //}
            searchSql.AppendLine(@"d on d.taskid=b.id) c 
on a.taskno=c.taskno and a.id=c.manutaskdetilid) y left join (select manutaskdetilid,q.companycd,q.chargeman,isnull(j.employeesname,'')employeesname,q.sequno as pgid,q.sequdes,q.taskid,q.id mid,w.sequno,w.sequname,r.wcname,convert(numeric(7,2),q.worktime)worktime,convert(varchar(10),q.StartDate,120)startdate1,convert(varchar(10),q.enddate,120)enddate1,q.RouteDes,q.remark from 
officedba.ManufacturedispatchingDetail q left join officedba.StandardSequ w on q.SequNo=w.id left join officedba.WorkCenter r on w.wcid=r.id left join (select a.ID,ISNULL(a.EmployeeName,'') AS EmployeesName,a.Flag,b.SuperDeptID,
isnull(cast (a.DeptID as varchar),'')  as  DeptID   from officedba.EmployeeInfo  as a left join officedba.deptinfo  as b on a.DeptID=b.ID
where a.CompanyCD='" + companycd + @"' and a.Flag!='2' and a.DeptID Is not null and a.Flag<>'3') as j on q.chargeman=j.id) u
on y.bid=u.taskid and y.manutaskdetilid=u.manutaskdetilid where u.companycd='" + companycd + "' ");
            //if (taskno != ""&&taskno!=null)
            //{
            //    searchSql.AppendLine(@" and taskno='" + taskno + "'");
            //}
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            #region 获取派工单主表总记录数
            string sql = "select count(1) from officedba.Manufacturedispatching where companycd='"+companycd+"'";
            comm.CommandText = sql;
            DataTable totalcount = SqlHelper.ExecuteSearch(comm);
            if (totalcount != null)
            {
                dt.Rows[0]["totallist"] = totalcount.Rows[0][0].ToString();
            }
            #endregion
            return dt;
        }
        #endregion
       
        #region 确认派工单
        /// <summary>
        /// 确认派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool updatecomfirmor(int pgid)
        {
            
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"update  officedba.Manufacturedispatching set billstatus=2,confirmor=" + comfirmor + ",ConfirmDate=getdate(),ModifiedDate=getdate(),ModifiedUserID='" + userid + "'  where id=" + pgid);
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            bool boolean = SqlHelper.ExecuteTransWithCommand(comm);
            return boolean;
        }
        #endregion
        #region 检查派工单是否被引用
        /// <summary>
        /// 检查派工单是否被引用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool checkpg(int pgid)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select count(1) from officedba.ManufactureProgressRptDetail where Morderno in (select id from   officedba.Manufacturedispatchingdetail where taskid="+pgid+") and companycd='"+companycd+"'");
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            
            DataTable dt=SqlHelper.ExecuteSearch(comm);
            if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                return false;
            else
                return true;
        }
        #endregion
        #region 取消确认派工单
        /// <summary>
        /// 取消确认派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool unupdatecomfirmor(int pgid)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"update  officedba.Manufacturedispatching set billstatus=1,confirmor=null,ConfirmDate=null,ModifiedDate=getdate(),ModifiedUserID='" + userid + "' where id=" + pgid);
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            bool boolean = SqlHelper.ExecuteTransWithCommand(comm);
            return boolean;
        }
        #endregion
        #region 结单派工单
        /// <summary>
        /// 结单派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool close(int pgid)
        {
            
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"update  officedba.Manufacturedispatching set billstatus=3,closer=" + comfirmor + ",closedate=getdate(),ModifiedDate=getdate(),ModifiedUserID='" + userid + "' where id=" + pgid);
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            bool boolean = SqlHelper.ExecuteTransWithCommand(comm);
            return boolean;
        }
        #endregion
        #region 取消结单派工单
        /// <summary>
        /// 取消结单派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool unclose(int pgid)
        {
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"update  officedba.Manufacturedispatching set billstatus=2,closer=null,closedate=null,ModifiedDate=getdate(),ModifiedUserID='" + userid + "' where id=" + pgid);
            #endregion
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            bool boolean = SqlHelper.ExecuteTransWithCommand(comm);
            return boolean;
        }
        #endregion
        #region 保存派工单
        /// <summary>
        /// 保存派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool save(int pgid, int rwid, string DetSortNo, string DetProductID, string DetStartDate, string DetEndDate, string DetFromType, string DetFromBillID, string DetFromBillNo, string DetFromLineNo)
        {
            ManufactureTaskDispatchModel model = new ManufactureTaskDispatchModel();
            #region 查询语句
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select top 1 * from officedba.ManufacturedispatchingDetail where  taskid="+pgid+" and manutaskdetilid="+rwid);

            #endregion 
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            model.IDs = int.Parse(dt.Rows[0]["id"].ToString());
            model.CompanyCD=((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            model.TaskID = pgid;
            model.ManuTaskDetilID = rwid;
            model.RouteID = int.Parse(dt.Rows[0]["RouteID"].ToString());
            model.cdepcode = dt.Rows[0]["cdepcode"].ToString();
            model.cdepname = dt.Rows[0]["cdepname"].ToString();
            model.Timeunit = dt.Rows[0]["Timeunit"].ToString();
            model.PreTime = decimal.Parse(dt.Rows[0]["PreTime"].ToString());
            string sql = "select id from officedba.ManufacturedispatchingDetail where taskid=" + pgid;
            comm.CommandText = sql;
            DataTable getid = SqlHelper.ExecuteSearch(comm);
            int[] ids=new int[getid.Rows.Count];
            if (getid.Rows.Count > 0)
            {
                for (int i = 0; i < getid.Rows.Count; i++)
                {
                    ids[i] = int.Parse(getid.Rows[i][0].ToString());
                }
            }
            comm.CommandText = "select max(id) from officedba.ManufacturedispatchingDetail";
            DataTable dtmaxid = SqlHelper.ExecuteSearch(comm);
            int maxid = int.Parse(dtmaxid.Rows[0][0].ToString());
            string[] gxid = DetSortNo.Split(',');
            string[] gxsm = DetProductID.Split(',');
            string[] kgsj = DetStartDate.Split(',');
            string[] wgsj = DetEndDate.Split(',');
            string[] fzr = DetFromType.Split(',');
            string[] zysj = DetFromBillID.Split(',');
            string[] gybz = DetFromBillNo.Split(',');
            string[] pgbz = DetFromLineNo.Split(',');
            string[] sqllist = new String[gxid.Length + 1];
            sqllist[0] = "delete from officedba.ManufacturedispatchingDetail where  taskid=" + pgid ;
            for (int i = 0; i < gxid.Length; i++)
            {
                model.SequNo = int.Parse(gxid[i]);
                model.sequDes = gxsm[i];
                model.StartDate = DateTime.Parse(kgsj[i]);
                model.EndDate =DateTime.Parse( wgsj[i]);
                if (fzr[i] != "")
                {
                    model.chargeman = int.Parse(fzr[i]);
                }
                else
                {
                    model.chargeman = 0;
                }
                model.Worktime = decimal.Parse(zysj[i]);
                model.Totaltime = model.Worktime + model.PreTime;
                model.Remarks = pgbz[i];
                model.RouteDes = gybz[i];
                if (i < getid.Rows.Count)
                {
                    sqllist[i + 1] = @"insert into officedba.ManufacturedispatchingDetail(ManuTaskDetilID,id,CompanyCD,TaskID,RouteID,RouteDes,SequNo,sequDes,StartDate,EndDate,cdepcode,cdepname,chargeman,timeunit,PreTime,worktime,totaltime,remark)
                                            values(" + model.ManuTaskDetilID + "," + ids[i] + ",'" + model.CompanyCD + "'," + model.TaskID + "," + model.RouteID + ",'" + model.RouteDes + "'," + model.SequNo + ",'" + model.sequDes + "','" + model.StartDate + "','" + model.EndDate + "','" + model.cdepcode + "','" + model.cdepname + "'," + model.chargeman + ",'" + model.Timeunit + "'," + model.PreTime + "," + model.Worktime + "," + model.Totaltime + ",'" + model.Remarks + "')";
                }
                else
                {
                    maxid++;
                    sqllist[i + 1] = @"insert into officedba.ManufacturedispatchingDetail(ManuTaskDetilID,id,CompanyCD,TaskID,RouteID,RouteDes,SequNo,sequDes,StartDate,EndDate,cdepcode,cdepname,chargeman,timeunit,PreTime,worktime,totaltime,remark)
                                            values(" + model.ManuTaskDetilID + "," + maxid + ",'" + model.CompanyCD + "'," + model.TaskID + "," + model.RouteID + ",'" + model.RouteDes + "'," + model.SequNo + ",'" + model.sequDes + "','" + model.StartDate + "','" + model.EndDate + "','" + model.cdepcode + "','" + model.cdepname + "'," + model.chargeman + ",'" + model.Timeunit + "'," + model.PreTime + "," + model.Worktime + "," + model.Totaltime + ",'" + model.Remarks + "')";
                }
                
            }
            bool boolean = SqlHelper.ExecuteTransForListWithSQL(sqllist);//执行事物
          
            return boolean;
        }
        #endregion


        #region 取消派工单
        /// <summary>
        /// 取消派工单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool delete(int pgid)
        {
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //指定命令的SQL文
            comm.CommandText = "select taskno from officedba.Manufacturedispatching where id=" + pgid;
            //执行查询
            DataTable dt = SqlHelper.ExecuteSearch(comm);
            string rwid="";
            if(dt!=null)
            {
                   if(dt.Rows.Count>0)
                   {
                       rwid = dt.Rows[0][0].ToString();
                   }
            }
            
            
            string[] sqllist = new String[3];
            sqllist[0] = "delete from officedba.Manufacturedispatching where  id=" + pgid ;
            sqllist[1] = "delete from officedba.ManufacturedispatchingDetail where  taskid=" + pgid;
            sqllist[2] = "update officedba.ManufactureTaskDetail set isdispatch=0 where taskno='" + rwid+"' ";
            bool boolean = SqlHelper.ExecuteTransForListWithSQL(sqllist);//执行事物

            return boolean;
        }
        #endregion



     #region 检索派工单
        public static DataTable SelectProviderManufactureDispatchList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string Purchase, string TaskNo, string GoodsName, string WorkCenter, string StartLinkDate, string EndLinkDate)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT G.ID as gid,D.ID as ID,F.WCName as WCName,isnull(D.TaskID,'') as DispatchNo,isnull(A.TaskNO,'') as TaskNo,isnull(C.ProductName,'') as ProductName ,convert(decimal(12,0),isnull(B.ProductedCount,0)) as ProductedCount       ");
            sql.AppendLine("      ,convert(decimal(12,0),isnull(B.ProductCount,0)) as ProductCount,isnull(convert(varchar(10),B.StartDate,120),'') as StartDate,isnull(convert(varchar(10),B.EndDate,120),'') as EndDate        ");
            sql.AppendLine("      ,isnull(convert(varchar(10),D.StartDate,120),'') as StartDate1,isnull(convert(varchar(10),D.EndDate,120),'') as EndDate1,isnull(E.SequName,'') as SequName       ");
            sql.AppendLine("      ,isnull(convert(varchar(10),D.worktime,120),0) as WorkTime, isnull(D.totaltime,0) as TotalTime    ");
            sql.AppendLine(" FROM officedba.Manufacturedispatching A                                 ");
            sql.AppendLine("LEFT OUTER JOIN officedba.ManufactureTaskDetail B on A.TaskNO = B.TaskNO and A.CompanyCD = B.CompanyCD ");
            sql.AppendLine("LEFT OUTER JOIN officedba.ManufactureTask G on G.TaskNo = B.TaskNo and G.companyCD = B.companyCD  ");
            sql.AppendLine("LEFT OUTER JOIN officedba.ProductInfo C on C.ID = B.ProductID and C.CompanyCD = B.CompanyCD ");
            sql.AppendLine("INNER JOIN officedba.ManufacturedispatchingDetail D on  D.TaskID = A.id and D.ManuTaskDetilID=B.ID and D.CompanyCD = A.CompanyCD");
            sql.AppendLine("LEFT OUTER JOIN officedba.StandardSequ E on E.ID = D.SequNo and E.CompanyCD = D.CompanyCD");
            sql.AppendLine("LEFT OUTER JOIN officedba.WorkCenter F on F.ID = E.WCID and F.CompanyCD = E.CompanyCD");
            sql.AppendLine(" WHERE 1=1");
            sql.AppendLine("AND A.CompanyCD = @CompanyCD");
            if (Purchase != "" && Purchase != null)
            {
                sql.AppendLine(" AND D.ID like @DispatchNo ");
            }
            if (TaskNo != null && TaskNo != "")
            {
                sql.AppendLine(" AND A.TaskNo like @TaskNo");
            }
            if (GoodsName != "" && GoodsName != null)
            {
                sql.AppendLine(" AND C.ProductName like  @productname ");
            }
            if (WorkCenter != null && WorkCenter != "")
            {
                sql.AppendLine(" AND F.WCName like  @WCName");
            }
            if (StartLinkDate != "" && StartLinkDate != null)
            {
                sql.AppendLine(" AND B.StartDate >= @StartLinkDate ");
            }
            if (EndLinkDate != null && EndLinkDate != "")
            {
                sql.AppendLine(" AND B.EndDate <= @EndLinkDate");
            }
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", companyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@DispatchNo", "%" + Purchase + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", "%" + TaskNo + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@productname", "%" + GoodsName + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@WCName", "%" + WorkCenter + "%"));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@StartLinkDate", StartLinkDate));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@EndLinkDate", EndLinkDate));

            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
        }
        #endregion
    }
       
}
