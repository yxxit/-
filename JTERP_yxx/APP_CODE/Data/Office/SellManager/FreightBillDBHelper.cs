/**********************************************
 * 类作用：   获取申请单相关信息
 * 建立人：   陈鑫铎
 * 建立时间： 2010/11/19
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.SellManager;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;

namespace XBase.Data.Office.SellManager
{
    public class FreightBillDBHelper
    {
        #region 申请单插入
        /// <summary>
        /// 申请单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertFreightBill(FreightModel model, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            ID = "0";
            bool result = false;
            string strSql = string.Format("INSERT INTO [officedba].[FreightForeign] ([CompanyCD]  ,[FillDate] ,[ContainerSp] ,[ShippedPort],[ObjectivePort] " +
                " ,[ShipCompanies]  ,[InlandCosts] ,[InlandCostsUS],[ExchangeRate],[SeaFreight]" +
          "  ,[WeightLimit] ,[VolumeLimit] ,[Remark],Topics,OutNo,Creator,CreateDate)     VALUES " +
          " ('{0}','{1}','{2}','{3}','{4}'" +
          ",'{5}','{6}','{7}','{8}','{9}' " +
          ",'{10}','{11}','{12}','{13}','{14}','{15}','{16}' ) select @@identity", model.CompanyCD, model.TxtBackDate, model.CboContainer, model.TxtOutOf, model.TxtObjective
          , model.TxtShipCompanies, model.TxtLumpFee, model.TxtLumpFeeM, model.TxtExchangeRate, model.TxtSeaFreight
          , model.WeightLimit, model.VolumeLimit, model.Remark, model.TxtTopics, model.InvNo, model.Creator, DateTime.Now.ToShortDateString());
            string[] strSqlList = { strSql };
            try
            {
                DataTable dt = SqlHelper.ExecuteSql(strSql);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        ID = dt.Rows[0][0].ToString();
                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;

        }
        #endregion

        #region 申请单修改
        /// <summary>
        /// 申请单修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateFreightBill(FreightModel model)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();
            SqlCommand comm = new SqlCommand();
            if (model.ID <= 0)
            {
                return false;
            }
            bool result = false;
            string strSql = string.Format("UPDATE [officedba].[FreightForeign]   SET " +
      " [FillDate] ='{0}' ,[ContainerSp] = '{1}' ,[ShippedPort] = '{2}',[ObjectivePort] = '{3}',[ShipCompanies] = '{4}' " +
     " ,[InlandCosts] = '{5}' ,[InlandCostsUS] = '{6}' ,[ExchangeRate] = '{7}' ,[SeaFreight] = '{8}' ,[WeightLimit] ='{9}' " +
      " ,[VolumeLimit] = '{10}',[Remark] = '{11}'   ,[Topics] = '{12}' WHERE id='{13}'"
, model.TxtBackDate, model.CboContainer, model.TxtOutOf, model.TxtObjective, model.TxtShipCompanies
         , model.TxtLumpFee, model.TxtLumpFeeM, model.TxtExchangeRate, model.TxtSeaFreight, model.WeightLimit
         , model.VolumeLimit, model.Remark, model.TxtTopics, model.ID);
            string[] strSqlList = { strSql };
            try
            {
                result = SqlHelper.ExecuteTransForListWithSQL(strSqlList);
            }
            catch
            {
                result = false;
            }
            return result;
            #region  合同单修改SQL语句 old
            //StringBuilder sqlEdit = new StringBuilder();
            //sqlEdit.AppendLine("UPDATE officedba.InvoiceBill              ");
            //sqlEdit.AppendLine("   SET             ");
            //sqlEdit.AppendLine("      CustID = @CustID                 ");
            //if (!string.IsNullOrEmpty(model.Tel))
            //{
            //    sqlEdit.AppendLine("      ,Tel = @Tel                 ");
            //}
            //if (!string.IsNullOrEmpty(model.Address))
            //{
            //    sqlEdit.AppendLine("      ,Address = @Address                 ");
            //}
            //if (!string.IsNullOrEmpty(model.Heading))
            //{
            //    sqlEdit.AppendLine("      ,Heading = @Heading           ");
            //}
            //if (!string.IsNullOrEmpty(model.AccountMan))
            //{
            //    sqlEdit.AppendLine("      ,AccountMan = @AccountMan   ");
            //}
            //if (!string.IsNullOrEmpty(model.AccountNum))
            //{
            //    sqlEdit.AppendLine("      ,AccountNum = @AccountNum   ");
            //}
            //sqlEdit.AppendLine("      ,FromBillID = @FromBillID   ");
            //if (!string.IsNullOrEmpty(model.Remark))
            //{
            //    sqlEdit.AppendLine("      ,Remark = @Remark   ");
            //}
            //sqlEdit.AppendLine("      ,ModifiedUserID = @ModifiedUserID   ");
            //sqlEdit.AppendLine("      ,ModifiedDate = @ModifiedDate   ");
            //sqlEdit.AppendLine(" WHERE ID=@ID");



            //comm.CommandText = sqlEdit.ToString();
            //comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            //comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            //if (!string.IsNullOrEmpty(model.Tel))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            //}
            //if (!string.IsNullOrEmpty(model.Address))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
            //}
            //if (!string.IsNullOrEmpty(model.Heading))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@Heading", model.Heading));
            //}
            //if (!string.IsNullOrEmpty(model.AccountMan))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            //}
            //if (!string.IsNullOrEmpty(model.AccountNum))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            //}
            //comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            //if (!string.IsNullOrEmpty(model.Remark))
            //{
            //    comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            //}
            //comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.ModifiedUserID));
            //comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd")));
            //listADD.Add(comm);
            // return SqlHelper.ExecuteTransWithArrayList(listADD);
            #endregion



        }
        #endregion

        #region 申请单删除
        /// <summary>
        /// 申请单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteFreightBill(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlMasterDet = new StringBuilder();
                    StringBuilder sqlMaster = new StringBuilder();
                    sqlMaster.AppendLine("delete from officedba.FreightForeign where ID=@ID");

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
        #region 确认
        public static bool ConfirmInvoiceBillBase(InvoiceBillModel model, int loginUserID, string loginUserName)
        {
            //DataTable dbIn = GetInvoiceBillInfo(model);
            //if (dbIn.Rows.Count > 0)
            //{
            //    int BillStatus = int.Parse(dbIn.Rows[0]["BillStatus"].ToString());      /*单据状态值*/
            //    //string createDate = dbIn.Rows[0]["CreateDate"].ToString();              /*创建日期*/
            //    if (BillStatus == 1)
            //    {
            //        ArrayList listADD = new ArrayList();
            //        SqlCommand comm = new SqlCommand();
            //        if (model.ID <= 0)
            //        {
            //            return false;
            //        }

            //        #region  合同单修改SQL语句
            //        StringBuilder sqlEdit = new StringBuilder();
            //        sqlEdit.AppendLine("UPDATE officedba.InvoiceBill              ");
            //        sqlEdit.AppendLine("   SET             ");
            //        sqlEdit.AppendLine("      CustID = @CustID                 ");
            //        if (!string.IsNullOrEmpty(model.Tel))
            //        {
            //            sqlEdit.AppendLine("      ,Tel = @Tel                 ");
            //        }
            //        if (!string.IsNullOrEmpty(model.Address))
            //        {
            //            sqlEdit.AppendLine("      ,Address = @Address                 ");
            //        }
            //        if (!string.IsNullOrEmpty(model.Heading))
            //        {
            //            sqlEdit.AppendLine("      ,Heading = @Heading           ");
            //        }
            //        if (!string.IsNullOrEmpty(model.AccountMan))
            //        {
            //            sqlEdit.AppendLine("      ,AccountMan = @AccountMan   ");
            //        }
            //        if (!string.IsNullOrEmpty(model.AccountNum))
            //        {
            //            sqlEdit.AppendLine("      ,AccountNum = @AccountNum   ");
            //        }
            //        sqlEdit.AppendLine("      ,FromBillID = @FromBillID   ");
            //        if (!string.IsNullOrEmpty(model.Remark))
            //        {
            //            sqlEdit.AppendLine("      ,Remark = @Remark   ");
            //        }
            //        sqlEdit.AppendLine("      ,BillStatus = @BillStatus   ");
            //        sqlEdit.AppendLine("      ,ConfirmUser = @ConfirmUser   ");
            //        sqlEdit.AppendLine("      ,ConfirmDate = @ConfirmDate   ");

            //        sqlEdit.AppendLine(" WHERE ID=@ID");
            //        comm.CommandText = sqlEdit.ToString();
            //        comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            //        comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            //        if (!string.IsNullOrEmpty(model.Tel))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            //        }
            //        if (!string.IsNullOrEmpty(model.Address))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
            //        }
            //        if (!string.IsNullOrEmpty(model.Heading))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@Heading", model.Heading));
            //        }
            //        if (!string.IsNullOrEmpty(model.AccountMan))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            //        }
            //        if (!string.IsNullOrEmpty(model.AccountNum))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            //        }
            //        comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            //        if (!string.IsNullOrEmpty(model.Remark))
            //        {
            //            comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            //        }
            //        comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "5"));
            //        comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmUser", model.ConfirmUser));
            //        comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", DateTime.Now.ToString("yyyy-MM-dd")));
            //        listADD.Add(comm);
            //        #endregion

            //        return SqlHelper.ExecuteTransWithArrayList(listADD);
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }
        #endregion

        #region 获取申请单列表
        public static DataTable GetFreightBillList(FreightModel model, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            string strWhere = " where 1=1";

            if (!string.IsNullOrEmpty(model.CompanyCD))
            {
                strWhere += " and a.CompanyCD = '" + model.CompanyCD + "'";
            }
            //convert(varchar(10),c.ConfirmDate,120)>
            if (model.TxtBackDateStart != null)
            {
                strWhere += " and  convert(varchar(10),a.FillDate,120) >= '" + model.TxtBackDateStart + "'";
            }
            if (model.TxtBackDateEnd != null)
            {
                strWhere += " and  convert(varchar(10),a.FillDate,120) <= '" + model.TxtBackDateEnd + "'";
            }
            //  /*主题*/
            if (!string.IsNullOrEmpty(model.TxtTopics))
            {
                strWhere += " and  a.Topics like '%" + model.TxtTopics.ToString().Trim() + "%'";
            }
            //  /*编号*/
            if (!string.IsNullOrEmpty(model.InvNo))
            {
                strWhere += " and  a.OutNo like '%" + model.InvNo.ToString().Trim() + "%'";
            }
            //  /*船公司*/
            if (!string.IsNullOrEmpty(model.TxtShipCompanies))
            {
                strWhere += " and  a.ShipCompanies like '%" + model.TxtShipCompanies.ToString().Trim() + "%'";
            }

            //  /*运出港*/
            if (!string.IsNullOrEmpty(model.TxtOutOf))
            {
                strWhere += " and  a.ShippedPort like '%" + model.TxtOutOf.ToString().Trim() + "%'";
            }
            //  /*目的港*/
            if (!string.IsNullOrEmpty(model.TxtObjective))
            {
                strWhere += " and  a.ObjectivePort like '%" + model.TxtObjective.ToString().Trim() + "%'";
            }
            //  /*集装箱类型*/
            if (!string.IsNullOrEmpty(model.CboContainer))
            {
                if (model.CboContainer.ToString().Trim() != "全部")
                {
                    strWhere += " and  a.ContainerSp = '" + model.CboContainer.ToString().Trim() + "'";
                }
            }

            string strSql = string.Format("  select	a.ID,a.CompanyCD,a.OutNo,a.ObjectivePort ,isnull(substring(CONVERT(varchar,a.FillDate,120),0,11),'') as FillDate,a.ContainerSp " +
          " ,a.ShippedPort,a.ShipCompanies,a.InlandCosts,a.InlandCostsUS " +
          " ,a.ExchangeRate ,a.SeaFreight,a.WeightLimit,a.VolumeLimit,a.Remark " +
          " ,a.Creator,e1.EmployeeName as CreatorN,isnull(substring(CONVERT(varchar,a.CreateDate,120),0,11),'') CreateDate  ,a.Topics " +
          "from  officedba.FreightForeign a " +
          "left join officedba.EmployeeInfo as e1 on e1.ID=a.Creator {0}", strWhere);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = strSql.ToString();
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
            //return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 导出需要
        public static DataTable GetOutInvoiceBillList(InvoiceBillModel model)
        {
            SqlCommand comm = new SqlCommand();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" SELECT a.[ID]                                    ");
            sql.AppendLine("       ,isnull(a.InvNo,'') as InvNo                          ");
            sql.AppendLine("       ,isnull(a.CustID,'') as CustID                          ");

            sql.AppendLine("       ,a.[Tel] as Tel    ");
            sql.AppendLine("       ,b.[CustName] as CustName    ");
            sql.AppendLine("       ,a.[Address]  ");
            sql.AppendLine("       ,a.[Remark]  ");
            sql.AppendLine("       ,case when a.BillStatus=1 then '制单' when a.BillStatus=5 then '执行' end as BillStatus ");
            sql.AppendLine("   FROM [officedba].[InvoiceBill] as a          ");
            sql.AppendLine("left join officedba.CustInfo AS b ON a.CustID = b.ID  ");
            sql.AppendLine(" where a.CompanyCD=@CompanyCD  ");


            if (model.CustID != 0)
            {
                sql.AppendLine(" and a.CustID = @CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            }
            if (model.InvNo != "")
            {
                sql.AppendLine(" and a.InvNo = @InvNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@InvNo", model.InvNo));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.ExecuteSearch(comm);
        }
        #endregion

        #region 申请单详细信息
        /// <summary>
        /// 合同单详细信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetFreightBillInfo(FreightModel model)
        {
            #region 旧代码
            //#region 查询语句
            ////查询SQL拼写
            //StringBuilder infoSql = new StringBuilder();
            //infoSql.AppendLine("select	a.ID,a.CompanyCD,a.InvNo,a.CustID,b.CustName,a.Tel,a.Address,a.Heading,a.AccountMan,a.AccountNum ");
            //infoSql.AppendLine("        ,a.FromBillID,s.OutNo as FromBillNo,s.StorageID,a.Remark,a.BillStatus,a.Creator,a.ModifiedUserID,a.ConfirmUser");
            //infoSql.AppendLine("        ,e1.EmployeeName as CreatorN,e2.EmployeeName as ModifiedUserIDN,e3.EmployeeName as ConfirmUserN");

            //infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.CreateDate,120),0,11),'') CreateDate                          ");
            //infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.ModifiedDate,120),0,11),'') ModifiedDate                          ");
            //infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.ConfirmDate,120),0,11),'') ConfirmDate                          ");

            //infoSql.AppendLine("from officedba.InvoiceBill a");
            //infoSql.AppendLine("left join officedba.CustInfo b on a.CustID=b.ID");
            //infoSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=a.Creator ");
            //infoSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=a.ModifiedUserID ");
            //infoSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=a.ConfirmUser ");
            //infoSql.Append(" left join officedba.SellOutStorage as s on s.ID=a.FromBillID ");
            //infoSql.AppendLine("where a.ID=@ID");

            //#endregion

            ////定义查询的命令
            //SqlCommand comm = new SqlCommand();
            //comm.Parameters.Add(SqlHelper.GetParameterFromString("@ID", model.ID.ToString()));


            ////指定命令的SQL文
            //comm.CommandText = infoSql.ToString();
            ////执行查询
            //return SqlHelper.ExecuteSearch(comm);
            #endregion

            string strSql = string.Format("  select	a.ID,a.CompanyCD,a.OutNo,a.ObjectivePort ,isnull(substring(CONVERT(varchar,a.FillDate,120),0,11),'') as FillDate,a.ContainerSp " +
            " ,a.ShippedPort,a.ObjectivePort,a.ShipCompanies,a.InlandCosts,a.InlandCostsUS " +
            " ,a.ExchangeRate ,a.SeaFreight,a.WeightLimit,a.VolumeLimit,a.Remark " +
            " ,a.Creator,e1.EmployeeName as CreatorN,isnull(substring(CONVERT(varchar,a.CreateDate,120),0,11),'') CreateDate  ,a.Topics " +
            "from  officedba.FreightForeign a " +
            "left join officedba.EmployeeInfo as e1 on e1.ID=a.Creator where a.ID='{0}'", model.ID.ToString());
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion

        #region 获取美元汇率
        /// <summary>
        /// 获取美元汇率
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static DataTable GetCurrencyInfo(FreightModel model)
        {
            string strSql = string.Format(" SELECT  top 1 ExchangeRate FROM  [officedba].[CurrencyTypeSetting]  where CurrencyName like '%美元%' and CompanyCD='{0}'", model.CompanyCD);
            return SqlHelper.ExecuteSql(strSql);
        }
        #endregion
    }
}
