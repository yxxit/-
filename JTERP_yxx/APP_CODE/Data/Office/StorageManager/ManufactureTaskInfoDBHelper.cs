/**********************************************
 * 类作用：   期初库存数据库层处理
 * 建立人：   肖合明
 * 建立时间： 2009/04/01
 ***********************************************/
using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class ManufactureTaskInfoDBHelper
    {
        #region 生产任务单及其明细信息列表(弹出层显示)
        public static DataTable GetMTDetailInfo(string CompanyCD, string TaskNo, string Subject,string Prodname,string Deptname)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendLine("select a.ID,a.TaskNo,a.Subject,a.Principal,");
            sql.AppendLine("ISNULL(s.EmployeeName,'') as PrincipalName,a.DeptID,");
            sql.AppendLine("ISNULL(h.DeptName,'') as DeptName");
            sql.AppendLine(",case a.ManufactureType when '0' then '普通' when '1' then '返修' when '2' then '拆件' else cp.TypeName end as ManufactureType");
            sql.AppendLine(",CONVERT(varchar(10),a.CreateDate, 23) as CreateDate");
            sql.AppendLine(",b.ID as DetailID,b.ProductID,ISNULL(c1.CodeName,'') as UnitName,isnull(Convert(numeric(10,2),b.ProductCount),0) as  JiBenCount,");
            sql.AppendLine("ISNULL(i.ProdNo,'') as ProdNo,ISNULL(Convert(numeric(10,2),i.StandardCost),0) as UnitPrice,");
            sql.AppendLine("ISNULL(i.ProductName,'') as ProductName");
            sql.AppendLine(",ISNULL(Convert(numeric(10,2),b.usedunitcount),0) as ProductCount,ISNULL(Convert(numeric(10,2),b.InCount),0) as InCount");
            sql.AppendLine(" from officedba.ManufactureTaskDetail b");
            sql.AppendLine(" left outer join officedba.ManufactureTask a on b.TaskNo=a.TaskNo and a.CompanyCD=b.CompanyCD ");
            sql.AppendLine(" left join officedba.DeptInfo h on a.DeptID=h.ID");
            sql.AppendLine(" left join officedba.ProductInfo i on b.ProductID=i.ID");
            sql.AppendLine(" left join officedba.EmployeeInfo s on a.Principal=s.ID");
            sql.AppendLine(" left join officedba.CodeUnitType c1 on c1.ID = i.UnitID ");
            sql.AppendLine(" left join officedba.CodePublicType cp on cp.ID=a.ManufactureType and cp.CompanyCD=a.CompanyCD ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.BillStatus=2");
            sql.AppendLine(" and (ISNULL(b.ProductCount,0)-ISNULL(b.InCount,0))>0");
            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            if (!string.IsNullOrEmpty(TaskNo))
            {
                sql.AppendLine(" and a.TaskNo like '%'+ @TaskNo +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@TaskNo", TaskNo));
            }
            if (!string.IsNullOrEmpty(Subject))
            {
                sql.AppendLine(" and a.Subject like '%'+ @Subject +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Subject", Subject));
            }

            if (!string.IsNullOrEmpty(Prodname))
            {
                sql.AppendLine(" and i.ProductName like '%'+ @prodname +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@prodname", Prodname));
            }
            if (!string.IsNullOrEmpty(Deptname))
            {
                sql.AppendLine(" and h.DeptName like '%'+ @deptname +'%'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@deptname", Deptname));
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);

        }
        #endregion

        /// <summary>
        /// 根据生产任务单编号，获取基本信息
        /// </summary>
        /// <param name="TaskNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetMTInfo(string TaskNo, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select a.ID,a.TaskNo,a.DeptID,i.DeptName,a.ManufactureType,");
            sql.AppendLine("a.Principal,h.EmployeeName as PrincipalName");
            sql.AppendLine("from officedba.ManufactureTask a left join officedba.DeptInfo i on a.DeptID=i.ID ");
            sql.AppendLine("left join officedba.EmployeeInfo h on h.ID=a.Principal");
            sql.AppendLine("where a.CompanyCD='" + CompanyCD + "' and TaskNo='" + TaskNo + "'");
            return SqlHelper.ExecuteSql(sql.ToString());
        }


        /// <summary>
        /// 根据传过来的明细ID数组来获取明细列表
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select isnull(p.Size,'')Size,isnull(a.PieceCount,0)PieceCount,isnull(a.TotalNumber,0)TotalNumber,isnull(Pakeages,0)pakeageid,isnull(dbo.getCodeublic(a.pakeages),'')pakeages,isnull(a.FromBillNo,'')FromBillNo,isnull(a.usedunitcount,0) jibencount,a.ID,a.TaskNo,p.StorageID,p.IsBatchNo,a.UsedUnitID   ");
            sql.AppendLine(",a.ProductID,ISNULL(p.ProdNo,'') as ProdNo,ISNULL(p.ProductName,'') as ProductName  ");
            sql.AppendLine(" ,ISNULL(q.CodeName,'') as UnitID                                                   ");
            sql.AppendLine(" ,ISNULL(p.Specification,'') as Specification                                       ");
            sql.AppendLine(" ,ISNULL(a.ProductCount,0) as FromBillCount                                       ");
            sql.AppendLine(" ,ISNULL(a.InCount,0) as InCount");
            sql.AppendLine("  ,ISNULL(a.ProductCount,0)-ISNULL(a.InCount,0) as ProductCount                     ");
            sql.AppendLine(",ISNULL(p.StandardCost,0) as UnitPrice,a.FromType                                    ");
            sql.AppendLine(",case a.FromType when '0' then '无来源' else '生产任务知单' end as FromTypeName     ");
            sql.AppendLine(",a.SortNo as FromLineNo from officedba.ManufactureTaskDetail a                      ");
            sql.AppendLine(" left join officedba.ProductInfo p on p.ID=a.ProductID                              ");
            sql.AppendLine(" left join officedba.CodeUnitType q on q.ID=p.UnitID ");
            sql.AppendLine(" where a.CompanyCD='" + CompanyCD + "' and a.ID in ( ");
            for (int i = 0; i < strDetailIDList.Split(',').Length - 1; i++)
            {
                sql.AppendLine("'" + strDetailIDList.Split(',')[i] + "', ");
            }
            string strSql = sql.ToString().Remove(sql.ToString().LastIndexOf(','));
            strSql += ")"; 
            DataTable dt= SqlHelper.ExecuteSql(strSql);
            if (dt.Rows.Count > 0)
            {
                string sql1 = @"select * from (select a.productid,b.NotPassNum,a.fromreportno from officedba.QualityCheckReport a left join officedba.CheckReportDetail b
on a.reportno=b.reportno and a.companycd=b.companycd where a.companycd='"+CompanyCD+@"' 
and fromtype=3 and Confirmor is not null union
select a.productid,b.NotPassNum,n.taskno fromreportno from officedba.QualityCheckReport a left join officedba.CheckReportDetail b
on a.reportno=b.reportno and a.companycd=b.companycd left join (select c.id,d.FromBillID,c.companycd from officedba.QualityCheckApplay c left join officedba.QualityCheckApplyDetail d
on d.applyno=c.applyno and c.companycd=d.companycd where c.companycd='"+CompanyCD+@"' and c.fromtype=2) m on a.ReportID=m.id and a.companycd=m.companycd
left join officedba.ManufactureTaskDetail n on n.id=m.FromBillID    where a.companycd='"+CompanyCD+@"' 
and a.fromtype=1 and a.Confirmor is not null and n.taskno is not null) model where fromreportno='" + dt.Rows[0]["TaskNo"].ToString() + "'";
                DataTable dt1 = SqlHelper.ExecuteSql(sql1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            if (dt.Rows[i]["ProductID"].ToString() == dt1.Rows[j]["productid"].ToString())
                            {
                                //dt.Rows[i]["FromBillCount"] = double.Parse(dt.Rows[i]["FromBillCount"].ToString()) - double.Parse(dt1.Rows[j]["NotPassNum"].ToString());
                                dt.Rows[i]["ProductCount"] = double.Parse(dt.Rows[i]["ProductCount"].ToString()) - double.Parse(dt1.Rows[j]["NotPassNum"].ToString());
                            }
                        }
                    }
                }
            }
            return dt;
        }

    }
}
