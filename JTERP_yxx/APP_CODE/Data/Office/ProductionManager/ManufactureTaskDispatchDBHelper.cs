/**********************************************
 * 作用：     处理生产派工的数据请求
 * 建立人：   沈健平
 * 建立时间： 2011/03/10
 ***********************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.ProductionManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Collections.Generic;

namespace XBase.Data.Office.ProductionManager
{
    public class ManufactureTaskDispatchDBHelper
    {
        #region 通过检索条件查询生产任务单信息
        /// <summary>
        /// 通过检索条件查询生产任务单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="BillTypeFlag"></param>
        /// <param name="BillTypeCode"></param>
        /// <returns></returns>
        public static DataTable Getdetails(ManufactureTaskModel model,  int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select rwid,e.ID,e.taskno,convert(varchar(10),e.createdate,120) as createdate,isnull(e.productname,'')as productname, convert(numeric(7,2),isnull(e.usedunitcount,0))as usedunitcount,f.deptname,convert(varchar(10),e.startdate,120) as startdate,convert(varchar(10),e.enddate,120) as enddate,isnull(e.isdispatch,0)as isdispatch from
(select rwid,d.id,d.deptid,d.taskno,d.createdate,c.productname,d.usedunitcount,d.startdate,d.enddate,d.isdispatch from
(select a.id as rwid,b.id,a.deptid,a.taskno,a.createdate,b.Productid,b.productcount usedunitcount,b.startdate,b.enddate,b.isdispatch from officedba.ManufactureTask a right join officedba.ManufactureTaskDetail b  on a.companycd=b.companycd and a.taskno=b.taskno 
where 1=1 and a.companycd=@companycd and confirmor is not null and closer is null )as d left join officedba.ProductInfo c on d.Productid=c.id) as e left join officedba.DeptInfo f on e.deptid=f.id");
            searchSql.AppendLine("where 1=1 ");


            if (model.Isdispatch == 1)
            {
                searchSql.AppendLine(" and isdispatch=1 ");
            }
            else
            {
                searchSql.AppendLine(" and isdispatch=0 ");
            }
          
            if (model.TaskNo != "")
            {
                searchSql.AppendLine(" and TaskNo=@TaskNo ");
            }
            if (model.Productname != "")
            {
                searchSql.AppendLine(" and Productname like @Productname ");
            }
            //searchSql.AppendLine("or (a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) OR (a.Principal IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            //BillTypeFlag
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", model.TaskNo));
            //BillTypeCode
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@Productname", '%' + model.Productname + '%'));

         
           
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion
        #region 生成派工单回写任务单
        /// <summary>
        /// 生成派工单回写任务单
        /// </summary>
        /// <param name="companycd"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static DataTable getcheck(string companycd,string ids, string tasknos,string pname)
        {
            int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string[] no = ids.Split(',');//id数组
            string[] taskno=tasknos.Split(',');//编码数组
            string[] goods = pname.Split(',');//物品数组
            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            searchSql.AppendLine(@"select g.taskno,mid,isnull(num,0)as num,g.productnam as productname,(case when mid<>0 then '' end)as tasknos  from
(select m.companycd,m.id as mid,m.RouteID,m.taskno,isnull(t.productname,'')as productnam from officedba.ManufactureTaskDetail m left join officedba.ProductInfo t on m.Productid=t.id where m.companycd=@companycd) g left join
(select c.id,d.SequID as num from officedba.TechnicsRouting c right join officedba.TechnicsRoutingDetail d on c.routeno=d.routeno and c.companycd=d.companycd where c.companycd=@companycd ) h on g.RouteID=h.id where taskno is not null and mid in(" + ids + ")");
//            searchSql.AppendLine(@"select m.taskno,m.id as mid,isnull(m.RouteID,0)as num, t.productname,(case when m.id<>0 then ''end)as tasknos from officedba.ManufactureTaskDetail m left join officedba.ProductInfo t on m.Productid=t.id where m.companycd=@companycd 
//and m.id in(" +ids+")");
            //searchSql.AppendLine(@"select taskno,mid,(case when id<>0 then '' end)as pname,(case when id<>0 then '' end)as tasknos from officedba.ManufactureTaskDetail where routeid<>0 and routeid is not null and companycd=@companycd and taskno=@taskno");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@companycd", companycd));
            
            //ids
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ids", ids));
            //指定命令的SQL文
            comm.CommandText = searchSql.ToString();
            //执行查询
            DataTable dt= SqlHelper.ExecuteSearch(comm);
            List<string> strlist = new List<string>();
             if (dt!=null&&dt.Rows.Count > 0)
            {
                int count = 0;
                List<int> list = new List<int>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int num=int.Parse(dt.Rows[i]["num"].ToString());
                    if (num > 0)
                    {
                        list.Add(int.Parse(dt.Rows[i]["mid"].ToString()));
                        count += 1;
                        string thisid = dt.Rows[i]["mid"].ToString();
                        if (strlist.Count > 0)
                        {
                            bool san = true;
                            for (int k = 0; k < strlist.Count; k++)
                            {
                                if (thisid == strlist[k])
                                {
                                    san = false;
                                }
                            }
                            if (san)
                            {
                                strlist.Add(thisid);
                            }
                        }
                        else
                        {
                                strlist.Add(thisid);
                        }
                    }
                }
                if (count > 0)
                {
                    string allmid = "";
                    for (int i = 0; i < strlist.Count; i++)
                    {
                        if (i == strlist.Count - 1)
                        {
                            allmid += strlist[i];
                        }
                        else
                        {
                            allmid += strlist[i] + ",";
                        }
                    }
                    #region 获取派工单和派工明细的相关数据
                    string sql1 = @"select a.RouteID--工艺路线id
,isnull(SequID,0)SequID --工序id
,a.id,taskno,isnull(remark,'')as remark--工艺路线说明
,isnull(readytime,0)readytime,isnull(runtime,0)runtime,startdate,enddate,deptno,deptname from
(select isnull(RouteID,0)as RouteID,id,taskno,startdate,enddate from officedba.ManufactureTaskDetail where companycd='"+companycd+ @"') a left join
(select c.id,c.remark,SequID,e.readytime,e.runtime,deptno,deptname from officedba.TechnicsRouting c left Join officedba.TechnicsRoutingDetail d on c.Routeno=d.Routeno and c.companycd=d.companycd 
left join officedba.StandardSequ e on e.id=d.sequid left join officedba.WorkCenter f on e.wcid=f.id left join officedba.DeptInfo h on f.deptid=h.id where c.companycd='" + companycd + "' ) b on a.RouteID=b.id where a.id in ("+allmid+")";
                    #endregion
                    comm.CommandText = sql1;
                    DataTable dt1 = SqlHelper.ExecuteSearch(comm);
                    ManufactureTaskDispatchModel model = new ManufactureTaskDispatchModel();
                    model.RouteID = int.Parse(dt1.Rows[0]["RouteID"].ToString());
                    model.RouteDes = dt1.Rows[0]["remark"].ToString();
                    model.SequNo = int.Parse(dt1.Rows[0]["SequID"].ToString());
                    model.StartDate = DateTime.Parse(dt1.Rows[0]["startdate"].ToString());
                    model.EndDate = DateTime.Parse(dt1.Rows[0]["enddate"].ToString());
                    model.cdepcode = dt1.Rows[0]["deptno"].ToString();
                    model.cdepname = dt1.Rows[0]["deptname"].ToString();
                    model.Timeunit = "天";
                    model.PreTime = decimal.Parse(dt1.Rows[0]["readytime"].ToString());
                    model.Worktime = decimal.Parse(dt1.Rows[0]["runtime"].ToString());
                    model.Totaltime = model.Worktime + model.PreTime;
                    model.Remarks = "";
                    string TaskNo = dt.Rows[0]["taskno"].ToString();
                    string ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
                    comm.CommandText = "select isnull(max(id),0)as id from officedba.ManufacturedispatchingDetail";
                    DataTable getids = SqlHelper.ExecuteSearch(comm);
                    
                    comm.CommandText = "select isnull(max(id),0) as id from officedba.Manufacturedispatching";
                    DataTable getid = SqlHelper.ExecuteSearch(comm);
                    model.ID = int.Parse(getid.Rows[0][0].ToString()) + 1;
                    model.IDs = int.Parse(getids.Rows[0][0].ToString()) + 1;
                    
                    int tasks=model.IDs;
                    int cou = 0;
                    string[] sqllist = new String[count*2+1];//生成派工单回写任务明细

                    sqllist[0] = @"insert into officedba.Manufacturedispatching (id,CompanyCD,TaskNo,BillStatus,Creator,CreateDate,Remark,ModifiedDate,ModifiedUserID)values(" + model.ID + ",'" + companycd + "','" + TaskNo + "',1," + EmployeeID + ",getdate(),'',getdate(),'" + ModifiedUserID + "')";
                    for (int i = 1; i < count+1; i++)
                    {
                        model.ManuTaskDetilID = list[cou];
                        sqllist[i] = @"insert into officedba.ManufacturedispatchingDetail(ManuTaskDetilID,id,CompanyCD,TaskID,RouteID,RouteDes,SequNo,sequDes,StartDate,EndDate,cdepcode,cdepname,chargeman,timeunit,PreTime,worktime,totaltime,remark)
                                            values("+model.ManuTaskDetilID+"," +model.IDs+",'" + companycd + "'," + model.ID + "," + model.RouteID + ",'" + model.RouteDes + "'," + model.SequNo + ",'','" + model.StartDate + "','" + model.EndDate + "','" + model.cdepcode + "','" + model.cdepname + "',0,'" + model.Timeunit + "'," + model.PreTime + "," + model.Worktime + "," + model.Totaltime + ",'')";
                        model.IDs += 1;
                        cou++;
                        if(cou<dt1.Rows.Count)
                        model.SequNo = int.Parse(dt1.Rows[cou]["SequID"].ToString());
                    }
//                    string insertdetail = @"insert into officedba.ManufacturedispatchingDetail(CompanyCD,TaskID,RouteID,RouteDes,SequNo,sequDes,StartDate,EndDate,cdepcode,cdepname,chargeman,timeunit,PreTime,worktime,totaltime,remark)
//                                            values('" + companycd + "'," + model.ID + "," + model.RouteID + ",'" + model.RouteDes + "'," + model.SequNo + ",'','" + model.StartDate + "','" + model.EndDate + "','" + model.cdepcode + "','" + model.cdepname + "','','" + model.Timeunit + "'," + model.PreTime + "," + model.Worktime + "," + model.Totaltime + ",'')";
                    int num = 0;
                    for (int i = count+1; i < sqllist.Length; i++)
                    {
                        sqllist[i] = "update officedba.ManufactureTaskDetail set isdispatch=1 where id=" + list[num];
                        num++;
                    }
                    
                    bool boolean = SqlHelper.ExecuteTransForListWithSQL(sqllist);//执行事物
                    if(boolean)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (int.Parse(dt.Rows[i]["num"].ToString()) > 0)
                            {
                                dt.Rows[i]["tasknos"] = tasks;
                                tasks++;
                            }
                        }
                    } 
                    
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["productname"].ToString() == "")
                        {
                            dt.Rows[i]["productname"] = goods[i];
                        }
                    }
                }
                
            }
            return dt;
        }
        #endregion
    }
}
