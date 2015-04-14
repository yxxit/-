/********************************************** 
 * 类作用：   获取呆料列表
 * 建立人：   宋凯歌
 * 建立时间： 2011/01/14
 ***********************************************/

using System;
using XBase.Model.Office.StorageManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;

namespace XBase.Data.Office.StorageManager
{
    public class StayMaterialDBHelper
    {
        #region 查询：呆料查询
        /// <summary>
        /// 呆料查询
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStayMaterialInfo(StayMaterialModel model,  int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@CompanyCD",model.CompanyCD),
                new SqlParameter("@ProductNo",model.ProductNo),
                new SqlParameter("@TurnOverStart",model.TurnOver),
                new SqlParameter("@TurnOverEnd",model.TurnOverEnd),
                new SqlParameter("@OverTimeStart",model.OverTime),
                new SqlParameter("@OverTimeEnd",model.OverTimeEnd),
                new SqlParameter("@sltMoreThanType",model.BusiType),
                new SqlParameter("@MaxType",model.MoreThanType),
                new SqlParameter("@ProductName",model.ProductName),
                new SqlParameter("@SelPoint",int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint)),
                new SqlParameter("@Order",ord),
                new SqlParameter("@pageindex",pageIndex),
                new SqlParameter("@pagesize",pageCount)
               
            };
            DataSet ds = SqlHelper.ExecuteDataset("", "[officedba].[GetDailiao]", param);
            StringBuilder sql = new StringBuilder();
            SqlCommand comm = new SqlCommand();
            sql.AppendLine("select A.ProductID,A.ProductNo,A.ProductName,A.Specification,d.Remark ,d.Size,d.BarCode,d.FromAddr,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.StandardCost,0)) as StandardCost,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.StandardSell,0)) as StandardSell ");
            sql.AppendLine(" ,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.SellTax,0)) as SellTax,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.SellPrice,0)) as SellPrice,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.StandardBuy,0)) as StandardBuy,Convert(numeric(22," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ISNULL(d.TaxBuy,0)) as TaxBuy  ,A.UnitID,A.UnitName,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),A.MaxStockNum) AS MaxStockNum,A.Manufacturer,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),ProductCount) AS ProductCount,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),A.OverCount) AS OverCount,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),A.StayStandard) AS StayStandard,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),A.Turnover) AS Turnover ,convert(decimal(18," + ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint + "),A.Slip) AS Slip ,A.UsedStatus,case A.[UsedStatus] when '0' then '' when '1' then '积压' when '2' then '呆滞' end as UsedStatusName  ");
		    sql.AppendLine("from [officedba].[DailiaoInfo] A");
		     sql.AppendLine("left join officedba.ProductInfo D ON A.ProductID=D.ID");
             if (model.UsedStatus != "")
             {
                 sql.AppendLine(" where  A.UsedStatus = '"+model.UsedStatus+"'");
             }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
        #region 查询：呆料查询
        /// <summary>
        /// 呆料查询
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetStayMaterialInfo1(StayMaterialModel model, int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
           
            SqlCommand comm = new SqlCommand();
//            string sql = @" select ProductID,ProdNo,ProductName,CodeName,Specification,isnull(sum(productcount),0)ProductCount,isnull(sum(productcount),0)/" + model.Daycount + @" AvgCount,'呆料' UsedStatusName from
//(SELECT  a.ProductID,a.StorageID,isnull(a.HappenCount,0) as ProductCount,CONVERT(VARCHAR(10),a.HappenDate,21) as EnterDate ,a.BillType as typeflag ,a.BillNo,isnull(a.PageUrl,'') as PageUrl, 
// isnull(b.ProdNo,'') as ProdNo, 
// isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification, 
// c.StorageName,c.StorageNo,isnull(b.Size,'') as ProductSize,e.CodeName, 
// isnull(b.FromAddr,'') as FromAddr,isnull(a.BatchNo,'') as BatchNo,isnull(a.ProductCount,0) as NowProductCount,a.Price,a.Creator,a.ReMark,d.EmployeeName as CreatorName,f.TypeName as ColorName  
// from officedba.StorageAccount
//  a left outer join officedba.ProductInfo b 
// on a.ProductID=b.ID 
// left outer join officedba.StorageInfo c 
// on a.StorageID=c.ID  
//  left outer join officedba.EmployeeInfo d 
// on a.Creator=d.ID left outer join officedba.CodeUnitType e  on b.UnitID=e.ID 
//  left outer join officedba.CodePublicType f on b.ColorID=f.ID  where   a.CompanyCD='"+model.CompanyCD+@"'  and a.billtype in(7,8,9,12,17,24) and a.HappenDate >='" + model.OverTime + @" 00:00:00' and a.HappenDate<='" + model.OverTimeEnd + @" 23:59:59'
//)as t group by ProductID,ProdNo,ProductName,CodeName,Specification having isnull(sum(productcount),0)/" + model.Daycount + "<" + model.Productcount + " ";
//            if (model.ProductNo != "")
//            {
//                sql += " and ProdNo='" + model.ProductNo + "'";
//            }
//            if (model.ProductName != "")
//            {
//                sql += " and ProductName like '%" + model.ProductName + "%'";
//            }
//            sql += @"union select b.ID ProductID,ProdNo,ProductName,e.CodeName,isnull(Specification,'')Specification,0 ProductCount,0 AvgCount,'呆料' UsedStatusName
//from officedba.ProductInfo b left join officedba.CodeUnitType e  on b.UnitID=e.ID where b.companycd='" + model.CompanyCD + @"'  and b.id not in
//(select ProductID from (select ProductID,companycd from   officedba.StorageAccount a  where  companycd='" + model.CompanyCD + "'  and a.billtype in(7,8,9,12,17,24) and a.HappenDate >='" + model.OverTime + @" 00:00:00' and a.HappenDate<='" + model.OverTimeEnd + @" 23:59:59')k group by ProductID )";
//            if (model.ProductNo != "")
//            {
//                sql += " and ProdNo='" + model.ProductNo + "'";
//            }
//            if (model.ProductName != "")
//            {
//                sql += " and ProductName like '%" + model.ProductName + "%'";
//            }
            string sql = @"select b.ProdNo, b.ProductName,b.Specification,b.CodeName,c.productcount from (select b.id,b.companycd,isnull(b.ProdNo,'') as ProdNo, isnull(b.ProductName,'') as ProductName,isnull(b.Specification,'') as Specification,e.CodeName  from officedba.productinfo b left outer join officedba.CodeUnitType e  
on b.UnitID=e.ID  where b.companycd='" + model.CompanyCD + @"' and b.id not in (select productid from officedba.StorageAccount a where a.companycd='" + model.CompanyCD + @"' and a.billtype in(7,8,9,12,17,24) and a.HappenDate >='" + model.OverTime + @" 00:00:00' and a.HappenDate<='" + model.OverTimeEnd + @" 23:59:59' )) b right join officedba.storageproduct c on b.id=c.productid 
 where b.companycd='" + model.CompanyCD + @"' and c.productcount>0";
            if (model.ProductNo != "")
            {
                sql += " and b.ProdNo='" + model.ProductNo + "'";
            }
            if (model.ProductName != "")
            {
                sql += " and b.ProductName like '%" + model.ProductName + "%'";
            }
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion
    }
}
