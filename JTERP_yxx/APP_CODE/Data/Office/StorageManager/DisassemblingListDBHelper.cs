using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.StorageManager;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Data;
using XBase.Common;

namespace XBase.Data.Office.StorageManager
{
    public class DisassemblingListDBHelper
    {
        public static DataTable GetStorageInOtherTableBycondition( DisassemblingModel model, string timeStart, string timeEnd, int pageIndex, int pageCount, string ord,bool isgetlist, ref int TotalCount)
        {
            //入库单编号、入库单主题、交货人、验收人、人库人、入库时间、入库原因、入库数量、入库金额、摘要、单据状态。
            string CanUser = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine(@"select a.id,a.billno,isnull(convert(varchar(10),a.CreatDate,120),'')CreatDate,isnull(dbo.getEmployeeName(a.handsmanid),'') handsman,
g.productname,c.deptname,case when status=1 then '制单' when status=2 then '执行' else '结单' end status,isnull(convert(numeric(12,2),a.totalprice),0)totalprice
 from officedba.Disassembling a left join officedba.bom f on f.companycd=a.companycd and f.id=a.bomid left join officedba.ProductInfo g on g.companycd=f.companycd and g.id=f.productid 
left join officedba.DeptInfo c on a.departmentid=c.id where a.companycd='"+model.companyCD+"' and BillType="+model.BillType+" ");
            
            if (!string.IsNullOrEmpty(model.BillNo))
            {
                sql.AppendLine(" and a.BillNo like  '%"+model.BillNo+"%'");
                
            }
            if (model.BomID>0)
            {
                sql.AppendLine(" and a.BomID="+model.BomID+"");
               
            }
            if (model.departmentID>0)
            {
                sql.AppendLine(" and a.departmentID="+model.departmentID+"");
               
            }
            if (model.HandsManID>0)
            {
                sql.AppendLine(" and a.HandsManID="+model.HandsManID+"");
               
            }
            if (!string.IsNullOrEmpty(timeStart) && string.IsNullOrEmpty(timeEnd))
            {
                sql.AppendLine(" and a.CreatDate>=@timeStart");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeStart", timeStart));
            }

            if (!string.IsNullOrEmpty(timeEnd)&&string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.CreatDate<=@timeEnd");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (!string.IsNullOrEmpty(timeEnd) && !string.IsNullOrEmpty(timeStart))
            {
                sql.AppendLine(" and a.CreatDate<=@timeEnd and a.CreatDate>='"+timeStart+"'");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@timeEnd", timeEnd));
            }
            if (model.status > 0)
            {
                sql.AppendLine(" and status=" + model.status + "");
            }
            if (isgetlist && !string.IsNullOrEmpty(ord))
            {
                sql.AppendLine(" order by  " +ord );
            }
            comm.CommandText = sql.ToString();
            if (isgetlist)
            {
                TotalCount = 0;
            }
            if (!isgetlist)
                return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
            else
                return SqlHelper.ExecuteSearch(comm);
        }
    }
}
