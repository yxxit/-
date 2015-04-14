using System;
using XBase.Model.Office.SellManager;
using System.Text;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace XBase.Data.Office.SellManager
{
    public class SellOrderForeignDBHelper
    {
        #region 销售订单插入
        /// <summary>
        /// 销售订单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertSellOrder(SellOrderForeignModel model, Hashtable htExtAttr, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region 采购进货单添加SQL语句
            StringBuilder sqlSoIn = new StringBuilder();
            sqlSoIn.AppendLine("INSERT INTO officedba.SellOrderForeign");
            sqlSoIn.AppendLine("           (CompanyCD");
            sqlSoIn.AppendLine("           ,OrderNo");
            sqlSoIn.AppendLine("           ,BranchID");
            sqlSoIn.AppendLine("           ,CustID");
            sqlSoIn.AppendLine("           ,UserID");
            sqlSoIn.AppendLine("           ,DeliveryDate");
            sqlSoIn.AppendLine("           ,OrderDate");
            sqlSoIn.AppendLine("           ,Contractor");
            sqlSoIn.AppendLine("           ,ExpectedQTY");
            sqlSoIn.AppendLine("           ,Title");
            sqlSoIn.AppendLine("           ,Currency");
            sqlSoIn.AppendLine("           ,OrderRate");
            sqlSoIn.AppendLine("           ,IsInquiry");
            sqlSoIn.AppendLine("           ,TotalOrders");
            sqlSoIn.AppendLine("           ,TotalCost");
            sqlSoIn.AppendLine("           ,TotalSales");
            sqlSoIn.AppendLine("           ,TotalCommission");
            sqlSoIn.AppendLine("           ,Remark");
            sqlSoIn.AppendLine("           ,BillStatus");
            sqlSoIn.AppendLine("           ,Shipments");
            sqlSoIn.AppendLine("           ,Creator");
            sqlSoIn.AppendLine("           ,CreateDate)");
            sqlSoIn.AppendLine("     VALUES");
            sqlSoIn.AppendLine("           (@CompanyCD");
            sqlSoIn.AppendLine("           ,@OrderNo");
            sqlSoIn.AppendLine("           ,@BranchID");
            sqlSoIn.AppendLine("           ,@CustID");
            sqlSoIn.AppendLine("           ,@UserID");
            if (model.DeliveryDate != null)
            {
                sqlSoIn.AppendLine("           ,@DeliveryDate");
            }
            else
            {
                sqlSoIn.AppendLine("           ,null");
            }
            sqlSoIn.AppendLine("           ,@OrderDate");
            sqlSoIn.AppendLine("           ,@Contractor");
            sqlSoIn.AppendLine("           ,@ExpectedQTY");
            sqlSoIn.AppendLine("           ,@Title");
            sqlSoIn.AppendLine("           ,@Currency");
            sqlSoIn.AppendLine("           ,@OrderRate");
            sqlSoIn.AppendLine("           ,@IsInquiry");
            sqlSoIn.AppendLine("           ,@TotalOrders");
            sqlSoIn.AppendLine("           ,@TotalCost");
            sqlSoIn.AppendLine("           ,@TotalSales");
            sqlSoIn.AppendLine("           ,@TotalCommission");
            sqlSoIn.AppendLine("           ,@Remark");
            sqlSoIn.AppendLine("           ,1");
            sqlSoIn.AppendLine("           ,0");
            sqlSoIn.AppendLine("           ,@Creator");
            sqlSoIn.AppendLine("           ,getdate())");
            sqlSoIn.AppendLine("set @ID=@@IDENTITY                ");

            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlSoIn.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@BranchID", model.BranchID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UserID", model.UserID));
            if (model.DeliveryDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@DeliveryDate", model.DeliveryDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate", model.OrderDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contractor", model.Contractor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ExpectedQTY", model.ExpectedQTY));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@Currency", model.Currency));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderRate", model.OrderRate));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsInquiry", model.IsInquiry));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalOrders", model.TotalOrders));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalCost", model.TotalCost));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalSales", model.TotalSales));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalCommission", model.TotalCommission));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));


            listADD.Add(comm);
            #endregion

            try
            {

                #region 拓展属性
                SqlCommand cmd = new SqlCommand();
                GetExtAttrCmd(model, htExtAttr, cmd);
                if (htExtAttr.Count > 0)
                    listADD.Add(cmd);
                #endregion


                #region 销售订单明细处理
                if (!String.IsNullOrEmpty(model.DProductID))
                {
                    string[] dProductID = model.DProductID.Split(',');
                    string[] dOrderCount = model.DOrderCount.Split(',');
                    string[] dPriceType = model.DPriceType.Split(',');
                    string[] dCostPrice = model.DCostPrice.Split(',');
                    string[] dTotalCost = model.DTotalCost.Split(',');
                    string[] dSalesPrice = model.DSalesPrice.Split(',');
                    string[] dSaleTotal = model.DSaleTotal.Split(',');
                    string[] dDifference = model.DDifference.Split(',');
                    string[] dRatio = model.DRatio.Split(',');
                    string[] dSurface = model.DSurface.Split(',');

                    if (dProductID.Length >= 1)
                    {
                        for (int i = 0; i < dProductID.Length; i++)
                        {
                            System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                            cmdsql.AppendLine("INSERT INTO officedba.SellOrderDetailForeign   ");
                            cmdsql.AppendLine("           (CompanyCD                    ");
                            cmdsql.AppendLine("           ,ProductID                    ");
                            cmdsql.AppendLine("           ,OrderNo                      ");
                            cmdsql.AppendLine("           ,OrderCount                   ");
                            cmdsql.AppendLine("           ,PriceType                    ");
                            cmdsql.AppendLine("           ,CostPrice                    ");
                            cmdsql.AppendLine("           ,TotalCost                    ");
                            cmdsql.AppendLine("           ,SalesPrice                   ");
                            cmdsql.AppendLine("           ,SaleTotal                    ");
                            cmdsql.AppendLine("           ,Difference                   ");
                            cmdsql.AppendLine("           ,Ratio                       ");
                            cmdsql.AppendLine("           ,Surface     )                   ");
                            cmdsql.AppendLine("     VALUES                              ");
                            cmdsql.AppendLine("           (@CompanyCD                   ");
                            cmdsql.AppendLine("           ,@ProductID                   ");
                            cmdsql.AppendLine("           ,@OrderNo                     ");
                            cmdsql.AppendLine("           ,@OrderCount                  ");
                            cmdsql.AppendLine("           ,@PriceType                    ");
                            cmdsql.AppendLine("           ,@CostPrice                    ");
                            cmdsql.AppendLine("           ,@TotalCost                    ");
                            cmdsql.AppendLine("           ,@SalesPrice                   ");
                            cmdsql.AppendLine("           ,@SaleTotal                    ");
                            cmdsql.AppendLine("           ,@Difference                   ");
                            cmdsql.AppendLine("           ,@Ratio                        ");
                            cmdsql.AppendLine("           ,@Surface  )                      ");

                            SqlCommand comms = new SqlCommand();
                            comms.CommandText = cmdsql.ToString();
                            comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                            comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dProductID[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
                            comms.Parameters.Add(SqlHelper.GetParameter("@OrderCount", dOrderCount[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@PriceType", dPriceType[i].ToString()));
                            comms.Parameters.Add(SqlHelper.GetParameter("@CostPrice", decimal.Parse(dCostPrice[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@TotalCost", decimal.Parse(dTotalCost[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@SalesPrice", decimal.Parse(dSalesPrice[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@SaleTotal", decimal.Parse(dSaleTotal[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@Difference", decimal.Parse(dDifference[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@Ratio", decimal.Parse(dRatio[i].ToString())));
                            comms.Parameters.Add(SqlHelper.GetParameter("@Surface", dSurface[i].ToString()));
                            listADD.Add(comms);
                        }
                    }
                }

                #endregion

                if (SqlHelper.ExecuteTransWithArrayList(listADD))
                {
                    ID = comm.Parameters["@ID"].Value.ToString();
                    result = true;
                }
                else
                {
                    ID = "0";
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region 销售订单修改
        /// <summary>
        /// 销售订单修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateSellOrder(SellOrderForeignModel model, Hashtable htExtAttr)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();

            if (model.ID <= 0)
            {
                return false;
            }

            #region  销售订单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.SellOrderForeign              ");
            sqlEdit.AppendLine("   SET BranchID = @BranchID             ");
            sqlEdit.AppendLine("      ,CustID = @CustID                 ");
            sqlEdit.AppendLine("      ,UserID = @UserID                 ");
            if (model.DeliveryDate != null)
            {
                sqlEdit.AppendLine("      ,DeliveryDate = @DeliveryDate     ");
            }
            else
            {
                sqlEdit.AppendLine("      ,DeliveryDate =null     ");
            }
            sqlEdit.AppendLine("      ,OrderDate = @OrderDate           ");
            sqlEdit.AppendLine("           ,Contractor = @Contractor");
            sqlEdit.AppendLine("           ,ExpectedQTY = @ExpectedQTY");
            sqlEdit.AppendLine("           ,Title = @Title");
            sqlEdit.AppendLine("           ,Currency = @Currency");
            sqlEdit.AppendLine("           ,OrderRate = @OrderRate");
            sqlEdit.AppendLine("           ,IsInquiry = @IsInquiry");
            sqlEdit.AppendLine("           ,TotalOrders = @TotalOrders");
            sqlEdit.AppendLine("           ,TotalCost = @TotalCost");
            sqlEdit.AppendLine("           ,TotalSales = @TotalSales");
            sqlEdit.AppendLine("           ,TotalCommission = @TotalCommission");
            sqlEdit.AppendLine("           ,Laster = @Laster");
            sqlEdit.AppendLine("           ,LastDate =getdate()");

            sqlEdit.AppendLine("      ,Remark = @Remark  ");
            sqlEdit.AppendLine(" WHERE ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@BranchID", model.BranchID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            comm.Parameters.Add(SqlHelper.GetParameter("@UserID", model.UserID));
            if (model.DeliveryDate != null)
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@DeliveryDate", model.DeliveryDate));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderDate", model.OrderDate));
            comm.Parameters.Add(SqlHelper.GetParameter("@Contractor", model.Contractor));
            comm.Parameters.Add(SqlHelper.GetParameter("@ExpectedQTY", model.ExpectedQTY));
            comm.Parameters.Add(SqlHelper.GetParameter("@Title", model.Title));
            comm.Parameters.Add(SqlHelper.GetParameter("@Currency", model.Currency));
            comm.Parameters.Add(SqlHelper.GetParameter("@OrderRate", model.OrderRate));
            comm.Parameters.Add(SqlHelper.GetParameter("@IsInquiry", model.IsInquiry));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalOrders", model.TotalOrders));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalCost", model.TotalCost));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalSales", model.TotalSales));
            comm.Parameters.Add(SqlHelper.GetParameter("@TotalCommission", model.TotalCommission));
            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            comm.Parameters.Add(SqlHelper.GetParameter("@Laster", model.Creator));



            listADD.Add(comm);
            #endregion

            #region 拓展属性
            SqlCommand cmd = new SqlCommand();
            GetExtAttrCmd(model, htExtAttr, cmd);
            if (htExtAttr.Count > 0)
                listADD.Add(cmd);
            #endregion


            #region 先删除不在销售订单明细中的ID
            StringBuilder sqlDel = new StringBuilder();
            sqlDel.AppendLine("Delete From officedba.SellOrderDetailForeign where CompanyCD=@CompanyCD and OrderNo=@OrderNo");

            SqlCommand commDel = new SqlCommand();
            commDel.CommandText = sqlDel.ToString();
            commDel.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            commDel.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
            listADD.Add(commDel);
            #endregion

            #region 销售订单明细处理
            if (!String.IsNullOrEmpty(model.DProductID))
            {
                string[] dProductID = model.DProductID.Split(',');
                string[] dOrderCount = model.DOrderCount.Split(',');
                string[] dPriceType = model.DPriceType.Split(',');
                string[] dCostPrice = model.DCostPrice.Split(',');
                string[] dTotalCost = model.DTotalCost.Split(',');
                string[] dSalesPrice = model.DSalesPrice.Split(',');
                string[] dSaleTotal = model.DSaleTotal.Split(',');
                string[] dDifference = model.DDifference.Split(',');
                string[] dRatio = model.DRatio.Split(',');
                string[] dSurface = model.DSurface.Split(',');

                if (dProductID.Length >= 1)
                {
                    for (int i = 0; i < dProductID.Length; i++)
                    {
                        System.Text.StringBuilder cmdsql = new System.Text.StringBuilder();
                        cmdsql.AppendLine("INSERT INTO officedba.SellOrderDetailForeign    ");
                        cmdsql.AppendLine("           (CompanyCD                    ");
                        cmdsql.AppendLine("           ,ProductID                    ");
                        cmdsql.AppendLine("           ,OrderNo                      ");
                        cmdsql.AppendLine("           ,OrderCount                   ");
                        cmdsql.AppendLine("           ,PriceType                    ");
                        cmdsql.AppendLine("           ,CostPrice                    ");
                        cmdsql.AppendLine("           ,TotalCost                    ");
                        cmdsql.AppendLine("           ,SalesPrice                   ");
                        cmdsql.AppendLine("           ,SaleTotal                    ");
                        cmdsql.AppendLine("           ,Difference                   ");
                        cmdsql.AppendLine("           ,Ratio                       ");
                        cmdsql.AppendLine("           ,Surface     )                   ");
                        cmdsql.AppendLine("     VALUES                              ");
                        cmdsql.AppendLine("           (@CompanyCD                   ");
                        cmdsql.AppendLine("           ,@ProductID                   ");
                        cmdsql.AppendLine("           ,@OrderNo                     ");
                        cmdsql.AppendLine("           ,@OrderCount                  ");
                        cmdsql.AppendLine("           ,@PriceType                    ");
                        cmdsql.AppendLine("           ,@CostPrice                    ");
                        cmdsql.AppendLine("           ,@TotalCost                    ");
                        cmdsql.AppendLine("           ,@SalesPrice                   ");
                        cmdsql.AppendLine("           ,@SaleTotal                    ");
                        cmdsql.AppendLine("           ,@Difference                   ");
                        cmdsql.AppendLine("           ,@Ratio                        ");
                        cmdsql.AppendLine("           ,@Surface  )                      ");

                        SqlCommand comms = new SqlCommand();
                        comms.CommandText = cmdsql.ToString();
                        comms.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
                        comms.Parameters.Add(SqlHelper.GetParameter("@ProductID", dProductID[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@OrderNo", model.OrderNo));
                        comms.Parameters.Add(SqlHelper.GetParameter("@OrderCount", dOrderCount[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@PriceType", dPriceType[i].ToString()));
                        comms.Parameters.Add(SqlHelper.GetParameter("@CostPrice", decimal.Parse(dCostPrice[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@TotalCost", decimal.Parse(dTotalCost[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SalesPrice", decimal.Parse(dSalesPrice[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@SaleTotal", decimal.Parse(dSaleTotal[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Difference", decimal.Parse(dDifference[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Ratio", decimal.Parse(dRatio[i].ToString())));
                        comms.Parameters.Add(SqlHelper.GetParameter("@Surface", dSurface[i].ToString()));
                        listADD.Add(comms);
                    }
                }
            }

            #endregion

            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 通过检索条件查询销售订单信息
        /// <summary>
        /// 通过检索条件查询销售订单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="orderDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="OrderBy"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderListBycondition(SellOrderForeignModel model, string UserID, string OrderDate, string DeliveryDate, string OrderDateEnd, string DeliveryDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            searchSql.AppendLine("select a.ID,a.BranchID,c.DeptName as BranchName,a.OrderNo,a.CustID,b.CustName,isnull( CONVERT(CHAR(10), a.DeliveryDate, 23),'') as DeliveryDate,");
            searchSql.AppendLine("	   isnull( CONVERT(CHAR(10), a.OrderDate, 23),'') as OrderDate,a.ExpectedQTY,a.Title,a.Currency,a.OrderRate,a.IsInquiry,a.TotalOrders,a.TotalCost,a.TotalSales,a.TotalCommission,a.BillStatus,a.CreateDate,");
            searchSql.AppendLine("		case when a.BillStatus=1 then '制单' when a.BillStatus=2 then '执行' when a.BillStatus=3 then '作废' when a.BillStatus=4 then '结单' end as strBillStatus");
            searchSql.AppendLine("from officedba.SellOrderForeign a ");
            searchSql.AppendLine("left join officedba.CustInfo b on a.CustID=b.ID ");
            searchSql.AppendLine("left join officedba.DeptInfo c on a.BranchID=c.ID ");
            searchSql.AppendLine("where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("and ((a.Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            searchSql.AppendLine("  INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            searchSql.AppendLine(" CROSS JOIN            officedba.UserInfo AS v  ");
            searchSql.AppendLine(" INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            searchSql.AppendLine("  INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            searchSql.AppendLine("                WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "'))) ");
            searchSql.AppendLine("or (a.UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", UserID));
            //单据编号
            if (!string.IsNullOrEmpty(model.OrderNo))
            {
                searchSql.AppendLine(" and a.OrderNo like @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + model.OrderNo + "%"));
            }
            //供应商
            if (model.CustID > 0)
            {
                searchSql.AppendLine(" and a.CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            }
            //签单日期
            //if (!string.IsNullOrEmpty(OrderDate))
            //{
            //    searchSql.AppendLine("and a.OrderDate=@OrderDate ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate));
            //}
            //if (!string.IsNullOrEmpty(DeliveryDate))
            //{
            //    searchSql.AppendLine("and a.DeliveryDate=@DeliveryDate ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate));
            //}
            if (!string.IsNullOrEmpty(OrderDateEnd) && !string.IsNullOrEmpty(OrderDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDateEnd", OrderDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.OrderDate, 23)>=@OrderDate and CONVERT(CHAR(10), a.OrderDate, 23)<=@OrderDateEnd ");
            }
            else if (!string.IsNullOrEmpty(OrderDateEnd) && string.IsNullOrEmpty(OrderDate))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDateEnd", OrderDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.OrderDate, 23)<=@OrderDateEnd ");
            }
            else if (string.IsNullOrEmpty(OrderDateEnd) && !string.IsNullOrEmpty(OrderDate))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.OrderDate, 23)>=@OrderDate  ");
            }

            if (!string.IsNullOrEmpty(DeliveryDateEnd) && !string.IsNullOrEmpty(DeliveryDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDateEnd", DeliveryDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.DeliveryDate, 23)>=@DeliveryDate and CONVERT(CHAR(10), a.DeliveryDate, 23)<=@DeliveryDateEnd ");
            }
            else if (!string.IsNullOrEmpty(DeliveryDateEnd) && string.IsNullOrEmpty(DeliveryDate))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDateEnd", DeliveryDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.DeliveryDate, 23)<=@DeliveryDateEnd ");
            }
            else if (string.IsNullOrEmpty(DeliveryDateEnd) && !string.IsNullOrEmpty(DeliveryDate))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), a.DeliveryDate, 23)>=@DeliveryDate  ");
            }
            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and a.BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(model.IsInquiry.ToString()) && model.IsInquiry.ToString() != "2")
            {
                searchSql.AppendLine("and a.IsInquiry=@IsInquiry ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsInquiry", model.IsInquiry.ToString()));
            }
            //指定命令的SQL文

            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        public static DataTable GetSellOrderListBycondition1(SellOrderForeignModel model, string UserID, string OrderDate, string DeliveryDate, string OrderDateEnd, string DeliveryDateEnd, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder searchSql = new StringBuilder();
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            searchSql.AppendLine("select a.ID,a.CompanyCD, a.ProductID, a.OrderNo,a.Surface, a.OrderCount, a.PriceType, a.CostPrice, a.TotalCost, a.SalesPrice, a.SaleTotal, a.Difference, a.Ratio, a.NumberShipments,(a.OrderCount-a.NumberShipments) as uNumberShipments,");
            searchSql.AppendLine(" b.ProductName,b.ProdNo ProductNo,case a.PriceType when '0' then a.SalesPrice END as PriceTypeName ,case a.PriceType when '1' then a.SalesPrice END as PriceTypeName1");
            searchSql.AppendLine("	,	case when c.BillStatus=1 then '制单' when c.BillStatus=2 then '执行' when c.BillStatus=3 then '作废' when c.BillStatus=4 then '结单' end as strBillStatus");
            searchSql.AppendLine(",e.CustName,b.Specification,c.ExpectedQTY,isnull( CONVERT(CHAR(10), c.DeliveryDate, 23),'') as DeliveryDate,isnull( CONVERT(CHAR(10), c.OrderDate, 23),'') as OrderDate,");
            searchSql.AppendLine("f.CurrencyName,case a.PriceType when '0' then a.CostPrice END as PriceTypeName2 ,case a.PriceType when '1' then a.CostPrice END as PriceTypeName3");
            searchSql.AppendLine(" ,c.Remark,C.OrderRate,c.BillStatus  ");
            searchSql.AppendLine("from officedba.SellOrderDetailForeign a");
            searchSql.AppendLine("left join officedba.ProductInfo b  on a.ProductID=b.ID");
            searchSql.AppendLine("left join officedba.SellOrderForeign c on a.OrderNo=c.OrderNo ");
            searchSql.AppendLine("left join officedba.CustInfo e on c.CustID=e.ID ");
            searchSql.AppendLine("left join officedba.CurrencyTypeSetting f on c.Currency=f.ID");

            searchSql.AppendLine("where a.CompanyCD=@CompanyCD and a.OrderNo in (select  OrderNo from officedba.SellOrderForeign ");
            searchSql.AppendLine(" where a.CompanyCD=@CompanyCD ");
            searchSql.AppendLine("   and ((Creator IN    (SELECT     v.EmployeeID   FROM  officedba.UserRole AS z ");
            searchSql.AppendLine("     INNER JOIN    officedba.RoleInfo AS y ON z.RoleID = y.RoleID ");
            searchSql.AppendLine("   CROSS JOIN            officedba.UserInfo AS v  ");
            searchSql.AppendLine("   INNER JOIN     officedba.UserRole AS w ON v.UserID = w.UserID ");
            searchSql.AppendLine("   INNER JOIN        officedba.RoleInfo AS x ON w.RoleID = x.RoleID");
            searchSql.AppendLine("                   WHERE      (CHARINDEX(' ' + CONVERT(varchar(20), y.RoleID) + ' ', x.SuperRoleID + ' ', 1) > 0 or (y.RoleID = x.RoleID) ) AND (z.UserID = '" + userInfo.UserID + "'))) ");
            searchSql.AppendLine("  or (UserID IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')))");
            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@UserID", UserID));
            //单据编号
            if (!string.IsNullOrEmpty(model.OrderNo))
            {
                searchSql.AppendLine(" and OrderNo like @OrderNo ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderNo", "%" + model.OrderNo + "%"));
            }
            if (!string.IsNullOrEmpty(model.OrderCurrency) && model.OrderCurrency != "0")
            {
                searchSql.AppendLine(" and f.ID=@Currency ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@Currency", model.OrderCurrency));
            }
            //供应商
            if (model.CustID > 0)
            {
                searchSql.AppendLine(" and CustID=@CustID ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@CustID", model.CustID.ToString()));
            }
            //签单日期
            //if (!string.IsNullOrEmpty(OrderDate))
            //{
            //    searchSql.AppendLine("and OrderDate=@OrderDate ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate));
            //}
            //if (!string.IsNullOrEmpty(DeliveryDate))
            //{
            //    searchSql.AppendLine("and DeliveryDate=@DeliveryDate ");
            //    comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate));
            //}
            if (!string.IsNullOrEmpty(OrderDateEnd) && !string.IsNullOrEmpty(OrderDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDateEnd", OrderDateEnd.ToString()));
                searchSql.Append(" and  CONVERT(CHAR(10), c.OrderDate, 23)>=@OrderDate and  CONVERT(CHAR(10), c.OrderDate, 23)<=@OrderDateEnd ");
            }
            else if (!string.IsNullOrEmpty(OrderDateEnd) && !string.IsNullOrEmpty(OrderDate))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDateEnd", OrderDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), c.OrderDate, 23)<=@OrderDateEnd ");
            }
            else if (!string.IsNullOrEmpty(OrderDateEnd) && !string.IsNullOrEmpty(OrderDate))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@OrderDate", OrderDate.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), c.OrderDate, 23)>=@OrderDate  ");
            }

            if (!string.IsNullOrEmpty(DeliveryDateEnd) && !string.IsNullOrEmpty(DeliveryDate))
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate.ToString()));
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDateEnd", DeliveryDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), c.DeliveryDate, 23)>=@DeliveryDate and CONVERT(CHAR(10), c.DeliveryDate, 23)<=@DeliveryDateEnd ");
            }
            else if (!string.IsNullOrEmpty(DeliveryDateEnd) && !string.IsNullOrEmpty(DeliveryDate))//开始时间为空，按结束时间检索（检索出结束时间及之前的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDateEnd", DeliveryDateEnd.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), c.DeliveryDate, 23)<=@DeliveryDateEnd ");
            }
            else if (!string.IsNullOrEmpty(DeliveryDateEnd) && !string.IsNullOrEmpty(DeliveryDate))//结束时间为空，按开始时间检索（检索出开始时间及之后的所以记录）
            {
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@DeliveryDate", DeliveryDate.ToString()));
                searchSql.Append(" and CONVERT(CHAR(10), c.DeliveryDate, 23)>=@DeliveryDate  ");
            }


            //单据状态
            if (!string.IsNullOrEmpty(model.BillStatus))
            {
                if (int.Parse(model.BillStatus) > 0)
                {
                    searchSql.AppendLine("and c.BillStatus=@BillStatus ");
                    comm.Parameters.Add(SqlHelper.GetParameterFromString("@BillStatus", model.BillStatus.ToString()));
                }
            }
            if (!string.IsNullOrEmpty(model.IsInquiry.ToString()) && model.IsInquiry.ToString() != "2")
            {
                searchSql.AppendLine("and c.IsInquiry=@IsInquiry ");
                comm.Parameters.Add(SqlHelper.GetParameterFromString("@IsInquiry", model.IsInquiry.ToString()));
            }
            //指定命令的SQL文
            searchSql.AppendLine(") ");
            comm.CommandText = searchSql.ToString();
            //执行查询
            return SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, OrderBy, ref totalCount);
        }
        #endregion

        #region 销售订单详细信息
        /// <summary>
        /// 销售订单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderInfo(SellOrderForeignModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select	a.ID,a.CompanyCD,a.BranchID,a.OrderNo,a.BranchID,j.CodeName,k.TypeName as  CustCategory,i.DeptName,a.Currency,h.CurrencyName,a.OrderRate,a.Contractor,l.LinkManName,l.WorkTel,l.MailAddress,l.MSN,a.CustID,b.CustName,a.UserID,c.EmployeeName as UserName,");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.DeliveryDate, 23),'') as DeliveryDate,isnull( CONVERT(CHAR(10), a.OrderDate, 23),'') as OrderDate,");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.CreateDate, 23),'') as CreateDate,isnull( CONVERT(CHAR(10), a.ConfirmDate, 23),'') as ConfirmDate,isnull( CONVERT(CHAR(10), a.InvalidDate, 23),'') as InvalidDate,isnull( CONVERT(CHAR(10), a.LastDate, 23),'') as LastDate,a.ExpectedQTY,a.Title,a.Currency,a.OrderRate,a.IsInquiry,a.TotalOrders,a.TotalCost,a.TotalSales,a.TotalCommission,a.Remark,a.BillStatus,");
            infoSql.AppendLine("		a.Creator,d.EmployeeName as CreatorReal,g.EmployeeName as Lastername,e.EmployeeName as Confirmorname,f.EmployeeName as Invalidorname,a.ExtField1,a.ExtField2,case a.BillStatus when '1' then '制单' when '2' then '执行' when '3' then '作废'end as BillStatusName,");
            infoSql.AppendLine("		a.ExtField3,a.ExtField4,a.ExtField5,a.ExtField6,a.ExtField7,a.ExtField8,a.ExtField9,a.ExtField10 ,");
            infoSql.AppendLine("		a.Shipments,a.BackSection,a.BackCommission,(CASE a.Shipments WHEN '1' THEN '部分发货' WHEN '2' THEN '已发货' else '未发货'  END) AS  ShipmentsName,");
            infoSql.AppendLine("		isnull( CONVERT(CHAR(10), a.StatementDate, 23),'') as StatementDate,a.Statementor,m.EmployeeName as StatementName");

            infoSql.AppendLine("from officedba.SellOrderForeign a");
            infoSql.AppendLine("left join officedba.CustInfo b on a.CustID=b.ID");
            infoSql.AppendLine("left join officedba.CustLinkMan l on a.Contractor=l.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo c on a.UserID=c.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo d on a.Creator=d.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo e on a.Confirmor=e.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo f on a.Invalidor=f.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo g on a.Laster=g.ID");
            infoSql.AppendLine("left join officedba.EmployeeInfo m on a.Statementor=m.ID");
            infoSql.AppendLine("left join officedba.CurrencyTypeSetting h on a.Currency=h.ID");
            infoSql.AppendLine("left join officedba.DeptInfo i on c.DeptID=i.ID");
            infoSql.AppendLine("left join officedba.CodeCompanyType j on b.CustClass=j.ID");
            infoSql.AppendLine("left join officedba.CodePublicType k on b.CustType=k.ID");
            infoSql.AppendLine("where a.ID=@ID");

            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = infoSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 销售订单明细详细信息
        /// <summary>
        /// 销售订单明细详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetSellOrderDetailInfoList(SellOrderForeignModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder detSql = new StringBuilder();
            detSql.AppendLine("select a.ID as DetailID,a.CompanyCD, a.ProductID, a.OrderNo,a.Surface, a.OrderCount, a.PriceType, a.CostPrice, a.TotalCost, a.SalesPrice, a.SaleTotal, a.Difference, a.Ratio, isnull(a.NumberShipments,0) as NumberShipments,(a.OrderCount-isnull(a.NumberShipments,0)) as uNumberShipments,");
            detSql.AppendLine("	   b.ProductName,b.BarCode,b.ProdNo ProductNo,b.Specification,c.CodeName UnitName,isBarCode='', isBatchNo='',case a.PriceType when '0' then 'CIF' when '1' then 'FOB' END as PriceTypeName ");
            //detSql.AppendLine(" ,isnull(b.Sell,0) as SellPrice,isnull(b.wholesalePrice,0) as WholePrice  ");//零售价，批发价
            detSql.AppendLine("from officedba.SellOrderDetailForeign a");
            detSql.AppendLine("left join officedba.ProductInfo b  on a.ProductID=b.ID");
            //detSql.AppendLine("left join officedba.MeasureUnit c on b.UnitID=c.ID");
            detSql.AppendLine("left join officedba.CodeUnitType c on b.UnitID=c.ID");

            detSql.AppendLine("where a.CompanyCD=@CompanyCD and a.OrderNo=(select top 1 OrderNo from officedba.SellOrderForeign where ID=@ID)");


            #endregion

            //定义查询的命令
            SqlCommand comm = new SqlCommand();
            //添加公司代码参数
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            //指定命令的SQL文
            comm.CommandText = detSql.ToString();
            //执行查询
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 扩展属性保存操作
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(SellOrderForeignModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.SellOrderForeign set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.Add("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND OrderNo = @OrderNo";
                cmd.Parameters.Add("@CompanyCD", model.CompanyCD);
                cmd.Parameters.Add("@OrderNo", model.OrderNo);
                cmd.CommandText = strSql;
            }
            catch (Exception)
            { }


        }
        #endregion

        #region 销售订单删除
        /// <summary>
        /// 销售订单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteSellOrder(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlMasterDet = new StringBuilder();
                    StringBuilder sqlMaster = new StringBuilder();
                    sqlMasterDet.AppendLine("delete from officedba.SellOrderDetailForeign where CompanyCD=@CompanyCD and OrderNo=(select OrderNo from officedba.SellOrderForeign where ID=@ID)");
                    sqlMaster.AppendLine("delete from officedba.SellOrderForeign where ID=@ID");

                    SqlCommand commDet = new SqlCommand();
                    commDet.CommandText = sqlMasterDet.ToString();
                    commDet.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    commDet.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(commDet);

                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = sqlMaster.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", CompanyCD));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", arrID[i].ToString()));
                    listADD.Add(comm);
                }
            }
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion
        #region 销售订单确认
        /// <summary>
        /// 销售订单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool ConfirmBill(SellOrderForeignModel model)
        {

            #region  销售订单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.SellOrderForeign              ");
            sqlEdit.AppendLine("   SET BillStatus = '2'             ");
            sqlEdit.AppendLine("           ,Confirmor = @Confirmor");
            sqlEdit.AppendLine("           ,ConfirmDate =getdate()");
            sqlEdit.AppendLine(" WHERE ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Confirmor", model.Creator));




            #endregion
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion
        #region 销售订单作废
        /// <summary>
        /// 销售订单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool InvalidBill(SellOrderForeignModel model)
        {

            #region  销售订单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.SellOrderForeign              ");
            sqlEdit.AppendLine("   SET BillStatus = '3'             ");
            sqlEdit.AppendLine("           ,Invalidor = @Invalidor");
            sqlEdit.AppendLine("           ,InvalidDate =getdate()");
            sqlEdit.AppendLine(" WHERE ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Invalidor", model.Creator));




            #endregion
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion
        #region 销售订单结单
        /// <summary>
        /// 销售订单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool StatementBill(SellOrderForeignModel model)
        {

            #region  销售订单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.SellOrderForeign              ");
            sqlEdit.AppendLine("   SET BillStatus = '4'             ");
            sqlEdit.AppendLine("           ,Statementor = @Statementor");
            sqlEdit.AppendLine("           ,StatementDate =getdate()");
            sqlEdit.AppendLine(" WHERE ID=@ID");


            SqlCommand comm = new SqlCommand();
            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@Statementor", model.Creator));




            #endregion
            return SqlHelper.ExecuteTransWithCommand(comm);
        }
        #endregion

        #region 单据是否被销售出库单引用
        /// <summary>
        /// 单据是否被销售出库单引用
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int CountRefrenceByOutStorage(string companyCD, string branchID, string ID)
        {

            string sql = "select count(*) as RefCount from officedba.SellOutStorage where CompanyCD=@CompanyCD and BranchID=@BranchID and FromBillID in (" + ID + ")";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = SqlHelper.GetParameter("@CompanyCD", companyCD);
            parms[1] = SqlHelper.GetParameter("@BranchID", branchID);
            object obj = SqlHelper.ExecuteScalar(sql, parms);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
    }
}
