using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;

namespace XBase.Data.JTHY.Expenses
{
    public  class PaySettleDBHelper
    {
   
        /// <summary>
        /// 类名：IncomeSettleDBHelper
        /// 
        /// 作者：刘群
        /// 创建时间：2014/7/3
        /// </summary>

        #region 根据供应商名称和时间查询
        /// <summary>
        /// 根据客户名称和时间查询销售结算信息
        /// </summary>
        /// <param name="ProviderId">供应商的Id</param>
        /// <param name="IncomeDateBegin">开始时间</param>
        /// <param name="IncomeDateEnd">结束时间</param>
        /// <returns></returns>
        public static DataTable SearchPaySettle(string CustName, string DateBegin, string DateEnd,int iscount)
        {
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            string CompanyCD = userInfo.CompanyCD;
            try
            {
                string sql = "";
                    sql = "select c.id,a.SendNo,convert(varchar(12), CreateDate,23) CreateDate , c.SttlCount as sttlCounts, c.SttlTotalPrice as sttlTotalPrice, a.custId,c.TaxPrice,c.ProductCount,c.ProductId,p.CustName ";
                    sql += "  from jt_xsfh  a";
                    sql += " left join dbo.jt_xsfh_mx c on c.SendNo=a.Id ";
                    sql += " left join officedba.CustInfo p on p.Id=a.CustID  where a.BillStatus='2' ";
                    if (iscount == 1)
                    {
                        sql += "  and  a.CompanyCD = '" + CompanyCD + "'";
                    }
                    else
                    {
                        sql += "  and   c.SttlCount<c.ProductCount and  a.CompanyCD = '" + CompanyCD + "'";
                    }
                if (CustName != "")
                {
                    sql += " and p.CustName ='" + CustName + "'";
                }

                if (DateBegin != "")
                    sql += " and a.CreateDate >= '" + DateBegin.ToString() + "'";
                if (DateEnd != "")
                    sql += " and a.CreateDate <= '" + DateEnd.ToString() + "'";

                DataTable dt1 = SqlHelper.ExecuteSql(sql);
                return dt1;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion

    }
}
