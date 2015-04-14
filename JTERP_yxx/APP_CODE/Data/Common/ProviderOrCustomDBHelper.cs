using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using XBase.Data.DBHelper;
namespace XBase.Data.Common
{
    public class ProviderOrCustomDBHelper
    {

        //获取往来单位列表
        public static DataTable GetProviderCustomList(Hashtable htParams, int PageSize, int PageIndex, string OrderBy, ref int TotalCount)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" SELECT   a.ID, a.CompanyCD, a.BranchID, a.CustClass, a.CustNo, a.CustName, a.CustNam, a.CustShort, a.CustNote, a.Salesman, a.Tel, a.Fax, a.WebSite,  ");
            sbSql.AppendLine("         a.Post, a.HotHow, a.MeritGrade, a.RelaGrade, a.Relation, a.CompanyType, a.Trade, a.ReceiveAddress, a.ContactName, a.Mobile, a.email, a.Remark, ");
            sbSql.AppendLine("         a.UsedStatus, a.Creator, a.CreatedDate, a.MaxCreditDate, a.OpenBank, a.AccountMan, a.AccountNum, a.ExtField1, a.ExtField2, a.ExtField3,  ");
            sbSql.AppendLine("        a.ExtField4, a.ExtField5, a.ExtField6, a.ExtField7, a.ExtField8, a.ExtField9, a.ExtField10, a.ExtField11, a.ExtField12, a.ExtField13, a.ExtField14,  ");
            sbSql.AppendLine("         a.ExtField15, a.ExtField16, a.ExtField17, a.ExtField18, a.ExtField19, a.ExtField20, a.ExtField21, a.ExtField22, a.ExtField23, a.ExtField24, a.ExtField25, ");
            sbSql.AppendLine("         a.ExtField26, a.ExtField27, a.ExtField28, a.ExtField29, a.ExtField30, a.PrepayAmount, a.NatureType, a.QQ, a.MSN, a.ReceivablePrice, a.PayablePrice, ");
            sbSql.AppendLine("         a.CreditLimit, a.LinkCycle, a.CustBig ,c.CodeName, b.TypeName as  CustCategory,");
            sbSql.AppendLine(" (CASE a.NatureType WHEN '1' THEN '供应商' WHEN '2' THEN '客户' WHEN '3' THEN '客户与供应商' WHEN '4' THEN '银行' WHEN '5' THEN '物流' WHEN '6' THEN '其他' END) AS  NatureTypeName");
            sbSql.AppendLine(" FROM officedba.CustInfo AS a ");
            sbSql.AppendLine("left join officedba.CodePublicType b on a.CustCategory=b.ID");
            sbSql.AppendLine("LEFT JOIN officedba.CodeCompanyType AS c ON a.CustClass = c.ID AND c.BigType = 2 ");
            sbSql.AppendLine(" WHERE a.CompanyCD=@CompanyCD AND a.UsedStatus='1' AND a.BranchID=@BranchID ");



            int length = htParams.Count;
            if (htParams.ContainsKey("NatureType"))
            {
                sbSql.AppendLine(" AND a.NatureType IN (" + htParams["NatureType"] + ")");
                length--;
            }

            SqlParameter[] Params = new SqlParameter[length];
            int index = 0;
            Params[index++] = SqlHelper.GetSqlParameter("@CompanyCD", htParams["CompanyCD"], SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@BranchID", htParams["BranchID"], SqlDbType.Int);

            if (htParams.ContainsKey("CustNo"))
            {
                sbSql.AppendLine(" AND a.CustNo LIKE @CustNo ");
                Params[index++] = SqlHelper.GetSqlParameter("@CustNo", htParams["CustNo"], SqlDbType.VarChar);
            }
            if (htParams.ContainsKey("CustName"))
            {
                sbSql.AppendLine(" AND a.CustName LIKE @CustName ");
                Params[index++] = SqlHelper.GetSqlParameter("@CustName", "%" + htParams["CustName"] + "%", SqlDbType.VarChar);
            }
            if (htParams.ContainsKey("Contactor"))
            {
                sbSql.AppendLine(" AND a.ContactName LIKE @ContactName ");
                Params[index++] = SqlHelper.GetSqlParameter("@ContactName", "%" + htParams["Contactor"] + "%", SqlDbType.VarChar);
            }


            return SqlHelper.CreateSqlByPageExcuteSql(sbSql.ToString(), PageIndex, PageSize, OrderBy, Params, ref TotalCount);
        }



        #region 添加往来单位
        public static string AddProviderCustom(Model.Office.CustManager.CustInfoModel model)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine(" INSERT INTO  officedba.CustInfo ");
            sbSql.AppendLine(" (CustNo,CustName,ContactName,Tel,Mobile,Fax,WebSite,email,ReceiveAddress,Post,CompanyCD,UsedStatus,CreatedDate,Creator,OpenBank,AccountMan,AccountNum) ");
            sbSql.AppendLine("  VALUES ");
            sbSql.AppendLine(" (@CustNo,@CustName,@ContactName,@ContactorTel,@Mobile,@Fax,@WebSite,@email,@ReceiveAddress,@Post,@CompanyCD,@UsedStatus,@CreateDate,@Creator,@OpenBank,@AccountMan,@AccountNum) ");
            sbSql.AppendLine(" set @ID=@@IDENTITY   ");
            SqlParameter[] Params = new SqlParameter[18];
            int index = 0;
            Params[index++] = SqlHelper.GetSqlParameter("@CustNo", model.CustNo, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@CustName", model.CustName, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@ContactName", model.ContactName, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@ContactorTel", model.Tel, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@Mobile", model.Mobile, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@Fax", model.Fax, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@WebSite", model.WebSite, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@email", model.email, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@ReceiveAddress", model.ReceiveAddress, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@Post", model.Post, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetOutputParameter("@ID", SqlDbType.Int);
            Params[index++] = SqlHelper.GetSqlParameter("@CompanyCD", model.CompanyCD, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@UsedStatus", "1", SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@CreateDate", model.CreatedDate, SqlDbType.DateTime);
            Params[index++] = SqlHelper.GetSqlParameter("@Creator", model.Creator, SqlDbType.Int);
            Params[index++] = SqlHelper.GetSqlParameter("@OpenBank", model.OpenBank, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@AccountMan", model.AccountMan, SqlDbType.VarChar);
            Params[index++] = SqlHelper.GetSqlParameter("@AccountNum", model.AccountNum, SqlDbType.VarChar);


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sbSql.ToString();
            cmd.Parameters.AddRange(Params);

            List<SqlCommand> cmdList = new List<SqlCommand>();
            cmdList.Add(cmd);
            if (SqlHelper.ExecuteTransWithCollections(cmdList))
            {
                return cmd.Parameters["@ID"].Value.ToString();
            }
            else
            {
                return string.Empty;
            }


        }
        #endregion

    }
}
