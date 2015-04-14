/**********************************************
 * 类作用：   获取申请单相关信息
 * 建立人：   宋凯歌
 * 建立时间： 2010/09/21
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
    public class InvoiceBillDBHelper
    {
        #region 申请单插入
        /// <summary>
        /// 申请单插入
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUserID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool InsertInvoiceBill(InvoiceBillModel model, string loginUserID, out string ID)
        {
            ArrayList listADD = new ArrayList();
            bool result = false;

            #region 询价单添加SQL语句
            SqlCommand comm = new SqlCommand();
            StringBuilder sqlSoIn = new StringBuilder();
            sqlSoIn.AppendLine("INSERT INTO officedba.InvoiceBill");
            sqlSoIn.AppendLine("           (CompanyCD");
            sqlSoIn.AppendLine("           ,InvNo");
            sqlSoIn.AppendLine("           ,CustID");
            if (!string.IsNullOrEmpty(model.Tel))
            {
                sqlSoIn.AppendLine("           ,Tel");
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                sqlSoIn.AppendLine("           ,Address");
            }
            if (!string.IsNullOrEmpty(model.Heading))
            {
                sqlSoIn.AppendLine("           ,Heading");
            }
            if (!string.IsNullOrEmpty(model.AccountMan))
            {
                sqlSoIn.AppendLine("           ,AccountMan");
            }
            if (!string.IsNullOrEmpty(model.AccountNum))
            {
                sqlSoIn.AppendLine("           ,AccountNum");
            }
            sqlSoIn.AppendLine("           ,FromBillID");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sqlSoIn.AppendLine("           ,Remark");
            }
            sqlSoIn.AppendLine("           ,Creator");
            sqlSoIn.AppendLine("           ,CreateDate");
            //sqlSoIn.AppendLine("           ,ModifiedUserID");
            //sqlSoIn.AppendLine("           ,ModifiedDate");
            sqlSoIn.AppendLine("           ,BillStatus)");
            sqlSoIn.AppendLine("     VALUES");
            sqlSoIn.AppendLine("           (@CompanyCD");
            sqlSoIn.AppendLine("           ,@InvNo");
            sqlSoIn.AppendLine("           ,@CustID");
            if (!string.IsNullOrEmpty(model.Tel))
            {
                sqlSoIn.AppendLine("           ,@Tel");
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                sqlSoIn.AppendLine("           ,@Address");
            }
            if (!string.IsNullOrEmpty(model.Heading))
            {
                sqlSoIn.AppendLine("           ,@Heading");
            }
            if (!string.IsNullOrEmpty(model.AccountMan))
            {
                sqlSoIn.AppendLine("           ,@AccountMan");
            }
            if (!string.IsNullOrEmpty(model.AccountNum))
            {
                sqlSoIn.AppendLine("           ,@AccountNum");
            }
           
            sqlSoIn.AppendLine("           ,@FromBillID");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sqlSoIn.AppendLine("           ,@Remark");
            }
            sqlSoIn.AppendLine("           ,@Creator");
            sqlSoIn.AppendLine("           ,@CreateDate");
            //sqlSoIn.AppendLine("           ,@ModifiedUserID");
            //sqlSoIn.AppendLine("           ,@ModifiedDate");
            sqlSoIn.AppendLine("           ,@BillStatus)");

            sqlSoIn.AppendLine("set @ID=@@IDENTITY                ");


            comm.CommandText = sqlSoIn.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.Parameters.Add(SqlHelper.GetParameter("@InvNo", model.InvNo));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            if (!string.IsNullOrEmpty(model.Tel))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
            }
            if (!string.IsNullOrEmpty(model.Heading))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Heading", model.Heading));
            }
            if (!string.IsNullOrEmpty(model.AccountMan))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            }
            if (!string.IsNullOrEmpty(model.AccountNum))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@Creator", model.Creator));
              comm.Parameters.Add(SqlHelper.GetParameter("@CreateDate", DateTime.Now.ToString("yyyy-MM-dd")));
              //comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.Creator));
              //comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd")));
              comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "1"));
            comm.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));

            listADD.Add(comm);
            #endregion

            try
            {
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

        #region 申请单修改
        /// <summary>
        /// 申请单修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateInvoiceBill(InvoiceBillModel model)
        {
            //获取登陆用户ID
            ArrayList listADD = new ArrayList();
            SqlCommand comm = new SqlCommand();
            if (model.ID <= 0)
            {
                return false;
            }

            #region  合同单修改SQL语句
            StringBuilder sqlEdit = new StringBuilder();
            sqlEdit.AppendLine("UPDATE officedba.InvoiceBill              ");
            sqlEdit.AppendLine("   SET             ");
            sqlEdit.AppendLine("      CustID = @CustID                 ");
            if (!string.IsNullOrEmpty(model.Tel))
            {
                sqlEdit.AppendLine("      ,Tel = @Tel                 ");
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                sqlEdit.AppendLine("      ,Address = @Address                 ");
            }
            if (!string.IsNullOrEmpty(model.Heading))
            {
                sqlEdit.AppendLine("      ,Heading = @Heading           ");
            }
            if (!string.IsNullOrEmpty(model.AccountMan))
            {
                sqlEdit.AppendLine("      ,AccountMan = @AccountMan   ");
            }
            if (!string.IsNullOrEmpty(model.AccountNum))
            {
                sqlEdit.AppendLine("      ,AccountNum = @AccountNum   ");
            }
            sqlEdit.AppendLine("      ,FromBillID = @FromBillID   ");
            if (!string.IsNullOrEmpty(model.Remark))
            {
                sqlEdit.AppendLine("      ,Remark = @Remark   ");
            }
            sqlEdit.AppendLine("      ,ModifiedUserID = @ModifiedUserID   ");
            sqlEdit.AppendLine("      ,ModifiedDate = @ModifiedDate   ");
            sqlEdit.AppendLine(" WHERE ID=@ID");



            comm.CommandText = sqlEdit.ToString();
            comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
            comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            if (!string.IsNullOrEmpty(model.Tel))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
            }
            if (!string.IsNullOrEmpty(model.Heading))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Heading", model.Heading));
            }
            if (!string.IsNullOrEmpty(model.AccountMan))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
            }
            if (!string.IsNullOrEmpty(model.AccountNum))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
            if (!string.IsNullOrEmpty(model.Remark))
            {
                comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedUserID", model.Creator));
            comm.Parameters.Add(SqlHelper.GetParameter("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd")));
            listADD.Add(comm);
            #endregion

       
            return SqlHelper.ExecuteTransWithArrayList(listADD);
        }
        #endregion

        #region 申请单删除
        /// <summary>
        /// 申请单删除（单选 || 多选）
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool DeleteInvoiceBill(string ID, string CompanyCD)
        {
            ArrayList listADD = new ArrayList();
            string[] arrID = ID.Split(',');
            if (arrID.Length > 0)
            {
                for (int i = 0; i < arrID.Length; i++)
                {
                    StringBuilder sqlMasterDet = new StringBuilder();
                    StringBuilder sqlMaster = new StringBuilder();
                    sqlMaster.AppendLine("delete from officedba.InvoiceBill where ID=@ID");

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
            DataTable dbIn = GetInvoiceBillInfo(model);
            if (dbIn.Rows.Count > 0)
            {
                int BillStatus = int.Parse(dbIn.Rows[0]["BillStatus"].ToString());      /*单据状态值*/
                //string createDate = dbIn.Rows[0]["CreateDate"].ToString();              /*创建日期*/
                if (BillStatus == 1)
                {
                    ArrayList listADD = new ArrayList();
                    SqlCommand comm = new SqlCommand();
                    if (model.ID <= 0)
                    {
                        return false;
                    }

                    #region  合同单修改SQL语句
                    StringBuilder sqlEdit = new StringBuilder();
                    sqlEdit.AppendLine("UPDATE officedba.InvoiceBill              ");
                    sqlEdit.AppendLine("   SET             ");
                    sqlEdit.AppendLine("      CustID = @CustID                 ");
                    if (!string.IsNullOrEmpty(model.Tel))
                    {
                        sqlEdit.AppendLine("      ,Tel = @Tel                 ");
                    }
                    if (!string.IsNullOrEmpty(model.Address))
                    {
                        sqlEdit.AppendLine("      ,Address = @Address                 ");
                    }
                    if (!string.IsNullOrEmpty(model.Heading))
                    {
                        sqlEdit.AppendLine("      ,Heading = @Heading           ");
                    }
                    if (!string.IsNullOrEmpty(model.AccountMan))
                    {
                        sqlEdit.AppendLine("      ,AccountMan = @AccountMan   ");
                    }
                    if (!string.IsNullOrEmpty(model.AccountNum))
                    {
                        sqlEdit.AppendLine("      ,AccountNum = @AccountNum   ");
                    }
                    sqlEdit.AppendLine("      ,FromBillID = @FromBillID   ");
                    if (!string.IsNullOrEmpty(model.Remark))
                    {
                        sqlEdit.AppendLine("      ,Remark = @Remark   ");
                    }
                    sqlEdit.AppendLine("      ,BillStatus = @BillStatus   ");
                    sqlEdit.AppendLine("      ,ConfirmUser = @ConfirmUser   ");
                    sqlEdit.AppendLine("      ,ConfirmDate = @ConfirmDate   ");

                    sqlEdit.AppendLine(" WHERE ID=@ID");
                    comm.CommandText = sqlEdit.ToString();
                    comm.Parameters.Add(SqlHelper.GetParameter("@ID", model.ID));
                    comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
                    if (!string.IsNullOrEmpty(model.Tel))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@Tel", model.Tel));
                    }
                    if (!string.IsNullOrEmpty(model.Address))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@Address", model.Address));
                    }
                    if (!string.IsNullOrEmpty(model.Heading))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@Heading", model.Heading));
                    }
                    if (!string.IsNullOrEmpty(model.AccountMan))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@AccountMan", model.AccountMan));
                    }
                    if (!string.IsNullOrEmpty(model.AccountNum))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@AccountNum", model.AccountNum));
                    }
                    comm.Parameters.Add(SqlHelper.GetParameter("@FromBillID", model.FromBillID));
                    if (!string.IsNullOrEmpty(model.Remark))
                    {
                        comm.Parameters.Add(SqlHelper.GetParameter("@Remark", model.Remark));
                    }
                    comm.Parameters.Add(SqlHelper.GetParameter("@BillStatus", "5"));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmUser", model.ConfirmUser));
                    comm.Parameters.Add(SqlHelper.GetParameter("@ConfirmDate", DateTime.Now.ToString("yyyy-MM-dd")));
                    listADD.Add(comm);
                    #endregion

                    return SqlHelper.ExecuteTransWithArrayList(listADD);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 获取申请单列表
        public static DataTable GetInvoiceBillList(InvoiceBillModel model, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            SqlCommand comm = new SqlCommand();
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
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
            sql.AppendLine("and (  ");
            DataTable dt1 = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                
                if (dt1.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            sql.AppendLine("(a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

            if (model.InvNo != "")
            {
                sql.AppendLine(" and a.InvNo = @InvNo");
                comm.Parameters.Add(SqlHelper.GetParameter("@InvNo", model.InvNo));
            }
            if (model.CustID != 0)
            {
                sql.AppendLine(" and a.CustID = @CustID");
                comm.Parameters.Add(SqlHelper.GetParameter("@CustID", model.CustID));
            }
            comm.Parameters.Add(SqlHelper.GetParameter("@CompanyCD", model.CompanyCD));
            comm.CommandText = sql.ToString();
            return SqlHelper.PagerWithCommand(comm, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region 导出需要
        public static DataTable GetOutInvoiceBillList(InvoiceBillModel model)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
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
            sql.AppendLine("and (  ");
            DataTable dt1 = XBase.Data.Office.SystemManager.RoleInfoDBHelper.GetRoleInfoByUserID(userInfo.UserID);
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                if (dt1.Rows[0]["RoleRange"].ToString() == "1")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) AND DeptID<>" + userInfo.DeptID.ToString() + " )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "2")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine(" (SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.DeptID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID) )) or ");
                }
                if (dt1.Rows[0]["RoleRange"].ToString() == "3")
                {
                    sql.AppendLine("(a.Creator IN  ");
                    sql.AppendLine("( SELECT ID FROM  officedba.EmployeeInfo ");
                    sql.AppendLine("  WHERE DeptID IN (SELECT a.ID  ");
                    sql.AppendLine(" FROM officedba.DeptInfo a,DepartInfo_Children(" + userInfo.BranchID.ToString() + ") b  ");
                    sql.AppendLine("  WHERE a.ID=b.ID))) or ");
                }
            }
            sql.AppendLine("(a.Creator IN (SELECT EmployeeID from  officedba.UserInfo where UserID = '" + userInfo.UserID + "')) )");

 
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
        public static DataTable GetInvoiceBillInfo(InvoiceBillModel model)
        {

            #region 查询语句
            //查询SQL拼写
            StringBuilder infoSql = new StringBuilder();
            infoSql.AppendLine("select	a.ID,a.CompanyCD,a.InvNo,a.CustID,b.CustName,a.Tel,a.Address,a.Heading,a.AccountMan,a.AccountNum ");
            infoSql.AppendLine("        ,a.FromBillID,s.OutNo as FromBillNo,s.StorageID,a.Remark,a.BillStatus,a.Creator,a.ModifiedUserID,a.ConfirmUser");
            infoSql.AppendLine("        ,e1.EmployeeName as CreatorN,e2.EmployeeName as ModifiedUserIDN,e3.EmployeeName as ConfirmUserN");

            infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.CreateDate,120),0,11),'') CreateDate                          ");
            infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.ModifiedDate,120),0,11),'') ModifiedDate                          ");
            infoSql.AppendLine("       ,isnull(substring(CONVERT(varchar,a.ConfirmDate,120),0,11),'') ConfirmDate                          ");

            infoSql.AppendLine("from officedba.InvoiceBill a");
            infoSql.AppendLine("left join officedba.CustInfo b on a.CustID=b.ID");
            infoSql.Append(" left join officedba.EmployeeInfo as e1 on e1.ID=a.Creator ");
            infoSql.Append(" left join officedba.EmployeeInfo as e2 on e2.ID=a.ModifiedUserID ");
            infoSql.Append(" left join officedba.EmployeeInfo as e3 on e3.ID=a.ConfirmUser ");
            infoSql.Append(" left join officedba.SellOutStorage as s on s.ID=a.FromBillID ");
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
    }


}
