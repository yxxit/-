/*************************************
 * 创建人：何小武
 * 创建日期：2009-12-26
 * 描述：销售出库单明细表报表
 ************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellManager;

namespace XBase.Data.Office.SellManager
{
    public class SellOutStorageDetailDBHelper
    {
        #region 根据检索条件获取销售出库明细表
        /// <summary>
        /// 根据检索条件获取销售出库明细表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="BatchNo">批次</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>datatable销售出库明细列表</returns>
        public static DataTable GetGetSellOutList(SellOutStorageModel model, DateTime? SellDate1, int? ProductID, string BatchNo, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.AppendLine(" select distinct s.ID as OutID,convert(varchar(100),s.SellDate,23) as SellDate, s.StorageID,s.CustID,s.OutNo,");
            strSql.AppendLine(" p.ProductNo,p.ProductName,p.Specification,p.UnitID,");
            strSql.AppendLine(" s2.ProductID,s2.DetailCount,s2.DetailPrice,s2.DetailTotalPrice,s2.BatchNo,");
            strSql.AppendLine(" c.CustName as CustName ");
            strSql.AppendLine(" ,(select StorageName from officedba.StorageInfo where ID=s.StorageID and CompanyCD=s.CompanyCD) as StorageName ");
            strSql.AppendLine(" ,(select UnitName from officedba.MeasureUnit where ID=p.UnitID and CompanyCD=p.CompanyCD) as UnitName");
            strSql.AppendLine(" ,(select DeptName from officedba.DeptInfo where ID=s.BranchID) as SubStoreName ");
            strSql.AppendLine(" ,(select top(1) StocksPrice from officedba.StorageCostAdjust where ProductID=s2.ProductID ");
            strSql.AppendLine(" and CompanyCD=s2.CompanyCD and BranchID=s.BranchID order by CreateDate desc) as CostPrice ");
            strSql.AppendLine(" from officedba.SellOutStorageDetail s2 ");
            strSql.AppendLine(" left join officedba.SellOutStorage s on s.OutNo=s2.OutNo and s.CompanyCD=s2.CompanyCD ");
            strSql.AppendLine(" left join officedba.CustInfo c on s.CustID=c.ID and s.CompanyCD=c.CompanyCD ");
            strSql.AppendLine(" left join officedba.ProductInfo p on p.ID=s2.ProductID and p.CompanyCD=s2.CompanyCD ");
            strSql.AppendLine(" where s2.CompanyCD=@CompanyCD and s.BillStatus=2 and s.BranchID in( " + model.Remark + ")");//此处的Remark中是临时存的BranchID串

            //检索条件：品名分类、客户、快捷查询、开始日期、结束日期、有毛利、无毛利。仓库、业务员、批次。
            //结果：日期、仓库、客户、销售出库单号、品名编号、品名、规格、计量单位、数量、单价、金额、成本价。
            ArrayList arrListParam = new ArrayList();

            arrListParam.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            if (ProductID != null)
            {
                strSql.AppendLine(" and s2.ProductID=@ProductID ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@ProductID", ProductID.ToString()));
            }
            if (model.CustID != null)
            {
                strSql.AppendLine(" and s.CustID=@CustID ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            }
            if (model.SellDate != null)
            {
                strSql.AppendLine(" and s.SellDate >=@SellDate ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@SellDate", model.SellDate.ToString()));
            }
            if (SellDate1 != null)
            {
                strSql.AppendLine(" and s.SellDate<DATEADD(day,1,Convert(datetime,@SellDate1)) ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@SellDate1", SellDate1.ToString()));
            }
            if (model.UserID != null)
            {
                strSql.AppendLine(" and s.UserID=@UserID ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@UserID", model.UserID.ToString()));
            }
            if (BatchNo != null)
            {
                string batchStr = "%" + BatchNo + "%";
                strSql.AppendLine(" and s2.BatchNo like @BatchNo ");
                arrListParam.Add(SqlHelper.GetParameterFromString("@BatchNo", batchStr));
            }
            return SqlHelper.CreateSqlByPageExcuteSqlArr(strSql.ToString(), pageIndex, pageCount, ord, arrListParam, ref totalCount);
        }
        #endregion

        #region 销售业绩统计分析
        /// <summary>
        /// 获得销售业绩统计分析数据
        /// </summary>
        /// <param name="list">0.BranchID 分店ID、1.CustID 客户ID、
        /// 2.UserID 业务员ID、3.StorageID 仓库、4 .StartDate 开始时间、
        /// 5.EndDate 结束时间、6.IsBack 是否含退货、7.分组字符串、
        /// 8.是否查询分店、9.统计类型:True:数量，false：金额、10.CompanyCD 客户、11.ProductID 商品ID</param>
        /// <returns></returns>
        public static DataTable GetSellAchievement(List<object> list)
        {
            bool isBack = Convert.ToBoolean(list[6]);
            const string sqlSellOut = " FROM officedba.SellOutStorage sos LEFT JOIN officedba.SellOutStorageDetail sosd ON sos.CompanyCD=sosd.CompanyCD AND sos.OutNo=sosd.OutNo ";
            const string sqlSellBack = " FROM officedba.SellBack sb LEFT JOIN officedba.SellBackDetail sbd ON sb.CompanyCD=sbd.CompanyCD AND sb.BackNo=sbd.BackNo";

            StringBuilder sbOut = new StringBuilder();
            StringBuilder sbBack = new StringBuilder();

            #region 分组字段

            sbOut.AppendFormat(" SELECT SUM({0}) AS AxisY ", Convert.ToBoolean(list[9]) ? "sosd.DetailCount" : "sosd.DetailTotalPrice");
            if (isBack)
            {// 包含退货
                sbBack.AppendFormat(" SELECT -SUM({0}) AS AxisY ", Convert.ToBoolean(list[9]) ? "sbd.BackCount" : "sbd.DetailTtoalPrice");
            }

            string group = "";
            switch (Convert.ToString(list[7]))
            {
                case "1":// 按客户
                    group = "ci.ID,ci.CustName";
                    sbOut.AppendFormat(",{0} AS AxisX ", group);
                    sbOut.Append(sqlSellOut);
                    sbOut.Append(" INNER JOIN officedba.CustInfo ci ON sos.CompanyCD=ci.CompanyCD AND sos.CustID=ci.ID ");
                    if (isBack)
                    {// 包含退货
                        sbBack.AppendFormat(",{0} AS AxisX ", group);
                        sbBack.Append(sqlSellBack);
                        sbBack.Append(" INNER JOIN officedba.CustInfo ci ON sb.CompanyCD=ci.CompanyCD AND sb.CustID=ci.ID ");
                    }
                    break;
                case "2":// 按品名
                    group = "pi1.ID,pi1.ProductName";
                    sbOut.AppendFormat(",{0} AS AxisX ", group);
                    sbOut.Append(sqlSellOut);
                    sbOut.Append(" INNER JOIN officedba.ProductInfo pi1 ON sos.CompanyCD=pi1.CompanyCD AND sosd.ProductID=pi1.ID ");
                    if (isBack)
                    {// 包含退货
                        sbBack.AppendFormat(",{0} AS AxisX ", group);
                        sbBack.Append(sqlSellBack);
                        sbBack.Append(" INNER JOIN officedba.ProductInfo pi1 ON sb.CompanyCD=pi1.CompanyCD AND sbd.ProductID=pi1.ID ");
                    }
                    break;
                case "3":// 按业务员
                    group = "ei.ID,ei.EmployeeName";
                    sbOut.AppendFormat(",{0} AS AxisX ", group);
                    sbOut.Append(sqlSellOut);
                    sbOut.Append(" INNER JOIN officedba.EmployeeInfo ei ON sos.CompanyCD=ei.CompanyCD AND sos.UserID=ei.ID ");
                    if (isBack)
                    {// 包含退货
                        sbBack.AppendFormat(",{0} AS AxisX ", group);
                        sbBack.Append(sqlSellBack);
                        sbBack.Append(" INNER JOIN officedba.EmployeeInfo ei ON sb.CompanyCD=ei.CompanyCD AND sb.UserID=ei.ID ");
                    }
                    break;
                case "4":// 按分店
                    group = "di.ID,di.DeptName";
                    sbOut.AppendFormat(",{0} AS AxisX ", group);
                    sbOut.Append(sqlSellOut);
                    sbOut.Append(" INNER JOIN officedba.DeptInfo di ON sos.CompanyCD=di.CompanyCD AND sos.BranchID=di.ID ");
                    if (isBack)
                    {// 包含退货
                        sbBack.AppendFormat(",{0} AS AxisX ", group);
                        sbBack.Append(sqlSellBack);
                        sbBack.Append(" INNER JOIN officedba.DeptInfo di ON sb.CompanyCD=di.CompanyCD AND sb.BranchID=di.ID ");
                    }
                    break;
                case "5":// 按仓库
                    group = "si.ID,si.StorageName";
                    sbOut.AppendFormat(",{0} AS AxisX ", group);
                    sbOut.Append(sqlSellOut);
                    sbOut.Append(" INNER JOIN officedba.StorageInfo si ON sos.CompanyCD=si.CompanyCD AND sos.StorageID=si.ID ");
                    if (isBack)
                    {// 包含退货
                        sbBack.AppendFormat(",{0} AS AxisX ", group);
                        sbBack.Append(sqlSellBack);
                        sbBack.Append(" INNER JOIN officedba.StorageInfo si ON sb.CompanyCD=si.CompanyCD AND sb.StorageID=si.ID ");
                    }
                    break;
            }
            #endregion

            #region 条件
            sbOut.AppendFormat(" WHERE sos.BillStatus='2' AND sos.CompanyCD='{0}' ", list[10]);
            sbBack.AppendFormat(" WHERE sb.BillStatus='2' AND sb.CompanyCD='{0}' ", list[10]);

            // 开始时间
            if (list[4] != null)
            {
                sbOut.AppendFormat(" AND sos.ConfirmDate>=Convert(datetime,'{0}') ", list[4]);
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.ConfirmDate>=Convert(datetime,'{0}') ", list[4]);
                }
            }
            // 结束时间
            if (list[5] != null)
            {
                sbOut.AppendFormat(" AND sos.ConfirmDate<dateadd(day,1,Convert(datetime,'{0}'))  ", list[5]);
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.ConfirmDate<dateadd(day,1,Convert(datetime,'{0}'))  ", list[5]);
                }
            }
            // 仓库
            if (!String.IsNullOrEmpty(Convert.ToString(list[3])) && Convert.ToInt32(list[3]) > 0)
            {
                sbOut.AppendFormat(" AND sos.StorageID={0} ", Convert.ToInt32(list[3]));
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.StorageID={0} ", Convert.ToInt32(list[3]));
                }
            }
            // 业务员
            if (!String.IsNullOrEmpty(Convert.ToString(list[2])) && Convert.ToInt32(list[2]) > 0)
            {
                sbOut.AppendFormat(" AND sos.UserID={0} ", Convert.ToInt32(list[2]));
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.UserID={0} ", Convert.ToInt32(list[2]));
                }
            }

            // 客户
            if (!String.IsNullOrEmpty(Convert.ToString(list[1])) && Convert.ToInt32(list[1]) > 0)
            {
                sbOut.AppendFormat(" AND sos.CustID={0} ", Convert.ToInt32(list[1]));
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.CustID={0} ", Convert.ToInt32(list[1]));
                }
            }
            // 品名
            if (!String.IsNullOrEmpty(Convert.ToString(list[11])) && Convert.ToInt32(list[11]) > 0)
            {
                sbOut.AppendFormat(" AND sosd.ProductID={0} ", Convert.ToInt32(list[11]));
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sbd.ProductID={0} ", Convert.ToInt32(list[11]));
                }
            }

            // 分店
            if (!String.IsNullOrEmpty(Convert.ToString(list[0])))
            {
                sbOut.AppendFormat(" AND sos.BranchID IN ({0}) ", Convert.ToString(list[0]));
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.BranchID IN ({0})  ", Convert.ToString(list[0]));
                }
            }
            #endregion

            #region 排序

            string sql = "";
            // 分组、排序
            sbOut.AppendFormat(" GROUP BY {0} ", group);
            if (isBack)
            {// 包含退货
                sbBack.AppendFormat(" GROUP BY {0} ", group);
                sql = String.Format("SELECT AxisX, SUM(AxisY) AS AxisY,ID FROM ( {0} UNION ALL {1} ) AS temp GROUP BY temp.AxisX,temp.ID", sbOut.ToString(), sbBack.ToString());
            }
            else
            {
                sql = String.Format("SELECT AxisX, AxisY,ID FROM ({0}) AS TEMP ", sbOut.ToString());
            }

            sql += " ORDER BY ID";
            #endregion

            // 返回查询结果
            DataTable dt = SqlHelper.ExecuteSql(sql);

            return dt;
        }

        /// <summary>
        /// 获取销售出库单历史单据列表
        /// </summary>
        /// <param name="list">0.BranchID 分店ID、1.CustID 客户ID、
        /// 2.UserID 业务员ID、3.StorageID 仓库、4 .StartDate 开始时间、
        /// 5.EndDate 结束时间、6.IsBack 是否含退货、7.分组字符串、
        /// 8.是否查询分店、9.统计类型:True:数量，false：金额、10.CompanyCD 客户、11.ProductID 商品ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellAchievementDetail(List<object> list, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            bool isBack = Convert.ToBoolean(list[6]);

            StringBuilder sbOut = new StringBuilder();
            StringBuilder sbBack = new StringBuilder();
            SqlCommand comm = new SqlCommand();

            #region 查询语句

            sbOut.Append(@" SELECT sos.ID,sos.OutNo,di.DeptName,si.StorageName,ci.CustName,ei.EmployeeName,pi1.ProductNo,pi1.ProductName,pi1.Specification,mu.UnitName,sosd.DetailCount,sosd.DetailTotalPrice
,sos.BranchID,sos.CustID,sos.UserID,sosd.ProductID,pi1.UnitID,sos.StorageID  FROM officedba.SellOutStorage sos
LEFT JOIN officedba.SellOutStorageDetail sosd ON sos.CompanyCD=sosd.CompanyCD AND sos.OutNo=sosd.OutNo
LEFT JOIN officedba.DeptInfo di ON sos.CompanyCD=di.CompanyCD AND sos.BranchID=di.ID
LEFT JOIN officedba.CustInfo ci ON sos.CompanyCD=ci.CompanyCD AND sos.CustID=ci.ID
LEFT JOIN officedba.EmployeeInfo ei ON sos.CompanyCD=ei.CompanyCD AND sos.UserID=ei.ID
LEFT JOIN officedba.ProductInfo pi1 ON sos.CompanyCD=pi1.CompanyCD AND  sosd.ProductID=pi1.ID
LEFT JOIN officedba.MeasureUnit mu ON sos.CompanyCD=mu.CompanyCD AND pi1.UnitID=mu.ID
LEFT JOIN officedba.StorageInfo si ON sos.CompanyCD=si.CompanyCD AND sos.StorageID=si.ID ");

            if (isBack)
            {// 包含退货
                sbBack.Append(@"SELECT sb.ID,sb.BackNo,di.DeptName,si.StorageName,ci.CustName,ei.EmployeeName,pi1.ProductNo,pi1.ProductName,pi1.Specification,mu.UnitName,sbd.BackCount,sbd.DetailTtoalPrice 
,sb.BranchID,sb.CustID,sb.UserID,sbd.ProductID,pi1.UnitID,sb.StorageID FROM officedba.SellBack sb
LEFT JOIN officedba.SellBackDetail sbd ON sb.CompanyCD=sbd.CompanyCD AND sb.BackNo=sbd.BackNo
LEFT JOIN officedba.DeptInfo di ON sb.CompanyCD=di.CompanyCD AND sb.BranchID=di.ID
LEFT JOIN officedba.CustInfo ci ON sb.CompanyCD=ci.CompanyCD AND sb.CustID=ci.ID
LEFT JOIN officedba.EmployeeInfo ei ON sb.CompanyCD=ei.CompanyCD AND sb.UserID=ei.ID
LEFT JOIN officedba.ProductInfo pi1 ON sb.CompanyCD=pi1.CompanyCD AND  sbd.ProductID=pi1.ID
LEFT JOIN officedba.MeasureUnit mu ON sb.CompanyCD=mu.CompanyCD AND pi1.UnitID=mu.ID
LEFT JOIN officedba.StorageInfo si ON sb.CompanyCD=si.CompanyCD AND sb.StorageID=si.ID ");
            }

            #endregion

            #region 条件

            sbOut.Append(" WHERE sos.BillStatus='2' AND sos.CompanyCD=@CompanyCD ");
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", Convert.ToString(list[10])));
            if (isBack)
            {// 包含退货
                sbBack.Append(" WHERE sb.BillStatus='2' AND sb.CompanyCD=@CompanyCD ");
            }

            // 分店
            if (!String.IsNullOrEmpty(Convert.ToString(list[0])))
            {
                sbOut.AppendFormat(" AND sos.BranchID IN ({0}) ", list[0]);
                if (isBack)
                {// 包含退货
                    sbBack.AppendFormat(" AND sb.BranchID IN ({0}) ", list[0]);
                }
            }

            if (list[1] != null && !string.IsNullOrEmpty(list[1].ToString()))
            {
                sbOut.Append(" AND sos.CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", Convert.ToString(list[1])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.CustID=@CustID ");
                }
            }
            if (list[2] != null && !string.IsNullOrEmpty(list[2].ToString()))
            {
                sbOut.Append(" AND sos.UserID=@UserID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", Convert.ToString(list[2])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.UserID=@UserID ");
                }
            }
            if (list[3] != null && !string.IsNullOrEmpty(list[3].ToString()))
            {
                sbOut.Append(" AND sos.StorageID=@StorageID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID", Convert.ToString(list[3])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.StorageID=@StorageID ");
                }
            }
            if (list[4] != null && !string.IsNullOrEmpty(list[4].ToString()))
            {
                sbOut.Append(" AND sos.SellDate>=Convert(datetime,@SellDate) ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDate", Convert.ToString(list[4])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.BackDate>=Convert(datetime,@SellDate) ");
                }
            }

            if (list[5] != null && !string.IsNullOrEmpty(list[5].ToString()))
            {
                sbOut.Append(" AND sos.SellDate<DATEADD(day,1,Convert(datetime,@SellDateEnd)) ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@SellDateEnd", Convert.ToString(list[5])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.BackDate<DATEADD(day,1,Convert(datetime,@SellDateEnd)) ");
                }
            }

            if (list[11] != null && !string.IsNullOrEmpty(list[11].ToString()))
            {
                sbOut.Append(" AND sosd.ProductID=@ProductID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID", Convert.ToString(list[11])));
                if (isBack)
                {// 包含退货
                    sbBack.Append(" AND sb.ProductID=@ProductID ");
                }
            }
            #endregion

            #region 分组关键字
            switch (Convert.ToString(list[7]))
            {
                case "1":// 按客户
                    sbOut.Append(" AND sos.CustID=@CustID1 ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID1", Convert.ToString(list[12])));
                    if (isBack)
                    {// 包含退货
                        sbBack.Append(" AND sb.CustID=@CustID1 ");
                    }
                    break;
                case "2":// 按品名
                    sbOut.Append(" AND sosd.ProductID=@ProductID1 ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@ProductID1", Convert.ToString(list[12])));
                    if (isBack)
                    {// 包含退货
                        sbBack.Append(" AND sbd.ProductID=@ProductID1 ");
                    }
                    break;
                case "3":// 按业务员
                    sbOut.Append(" AND sos.UserID=@UserID1 ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID1", Convert.ToString(list[12])));
                    if (isBack)
                    {// 包含退货
                        sbBack.Append(" AND sb.UserID=@UserID1 ");
                    }
                    break;
                case "4":// 按分店
                    sbOut.Append(" AND sos.BranchID=@BranchID1 ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BranchID1", Convert.ToString(list[12])));
                    if (isBack)
                    {// 包含退货
                        sbBack.Append(" AND sb.BranchID=@BranchID1 ");
                    }
                    break;
                case "5":// 按仓库
                    sbOut.Append(" AND sos.StorageID=@StorageID1 ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@StorageID1", Convert.ToString(list[12])));
                    if (isBack)
                    {// 包含退货
                        sbBack.Append(" AND sb.StorageID=@StorageID1 ");
                    }
                    break;
            }
            #endregion

            string sql = "";
            if (isBack)
            {// 包含退货
                sql = String.Format(" {0} UNION ALL {1} ", sbOut.ToString(), sbBack.ToString());
            }
            else
            {
                sql = sbOut.ToString();
            }

            comm.CommandText = sql;

            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }

        #endregion

        #region  获取出货明细
        public static DataTable GetSellOutReportList(SellOutStorageForeignModel model, string FromBillNo, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            string strWhere = " where 1=1 ";
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            if (!string.IsNullOrEmpty(model.CompanyCD.Trim()))
            {
                strWhere += " and a.[CompanyCD]='" + model.CompanyCD.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(FromBillNo))
            {
                strWhere += " and d.[OrderNo]='" + FromBillNo + "' ";
            }
            if (!string.IsNullOrEmpty(model.CustID.ToString()))
            {
                strWhere += " and b.[CustID]='" + model.CustID.ToString().Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(model.InvoiceNo))
            {
                strWhere += " and b.[InvoiceNo]='" + model.InvoiceNo + "' ";
            }
            if (!string.IsNullOrEmpty(model.OrderCurrency) && model.OrderCurrency != "0")
            {
                strWhere += " and f.[ID]='" + model.OrderCurrency + "' ";
            }
            if (!string.IsNullOrEmpty(model.TxtBackDateStart))
            {
                strWhere += " and  convert(varchar(10),b.DestinationDate,120) >='" + model.TxtBackDateStart.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(model.TxtBackDateEnd))
            {
                strWhere += " and convert(varchar(10),b.DestinationDate,120) <='" + model.TxtBackDateEnd.Trim() + "' ";
            }
            if (!string.IsNullOrEmpty(model.Destination))
            {
                strWhere += " and b.[Destination] like '%" + model.Destination.Trim() + "%' ";
            }
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                strWhere += " and b.[BillStatus]='" + model.BillStatus.Trim() + "' ";
            }
            strWhere += "   and ((b.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ";
            strWhere += "     INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ";
            strWhere += "   CROSS JOIN            officedba.UserInfo AS v  ";
            strWhere += "   INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ";
            strWhere += "   INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID";
            strWhere += "                   WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "'))) ";
            strWhere += "  or (b.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))";

            string strSql = string.Format("select row_number() over (order by {1}) as rowid,vv.* from (SELECT a.[ID],a.[CompanyCD],d.OrderNo,e.CustName,b.Destination,b.TotalFreight,b.BillStatus,b.ContainerNumber,b.InvoiceNo,a.[OutNo] ,c.[ProductName],c.Specification ,a.[SalesPrice],a.DetailCount,f.CurrencyName " +
            ",a.DeclarationPrice,a.DeclarationNumber,(convert(decimal(18,2),isnull(a.DeclarationPrice,0)*isnull(a.DeclarationNumber,0))) as DeclarationTotal " +
            "  , case b.BillStatus when 1 then '制单' when 2 then '执行' when 3 then '收款' when 4 then '作废' end as BillStatusText " +
            " ,d.OrderRate,(convert(decimal(18,2),isnull(a.[SalesPrice],0)*isnull(a.DeclarationNumber,0))) as CifSalesTotal " +
            " ,(convert(decimal(18,2),isnull(a.[SalesPrice],0)*isnull(a.DetailCount,0))) as ActualSalesTotal ,isnull(b.OrderCurrencyRate,0) OrderCurrencyRate " +
            ",a.[Difference],(convert(decimal(18,2),isnull(a.[Difference],0)*isnull(a.DetailCount,0))) as DiffTotal,isnull(substring(CONVERT(varchar,b.CommissionDate ,120),0,11),'') as CommissionDate" +
            ",isnull(substring(CONVERT(varchar,b.orderDate,120),0,11),'') as orderDate,isnull(substring(CONVERT(varchar,b.DestinationDate,120),0,11),'') as DestinationDate,b.ExchangeRate,isnull(substring(CONVERT(varchar,b.ExchangeDate,120),0,11),'') as ExchangeDate " +
            //",(convert(decimal(18,2),isnull(b.OrderCurrencyRate,0)*isnull(a.SalesPrice,0)*isnull(a.DetailCount,0))) as FreightTotal " +
            "FROM  [officedba].[SellOutStorageDetailForeign] as a " +
            " left join [officedba].[SellOutStorageForeign] as b on b.OutNo=a.OutNo and b.CompanyCD=a.CompanyCD " +
            " left join [officedba].[ProductInfo] as c on c.id=a.ProductID and b.CompanyCD=a.CompanyCD " +
            " left join officedba.SellOrderForeign  as d on d.id=b.FromBillID  and b.CompanyCD=a.CompanyCD " +
            " left join officedba.CurrencyTypeSetting  as f on d.Currency=f.ID  and f.CompanyCD=a.CompanyCD " +
            " left join officedba.CustInfo  as e on e.id=b.CustID  and b.CompanyCD=a.CompanyCD  {0} ) as vv ", strWhere, ord);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion
    }
}
