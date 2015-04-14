using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.DBHelper;

namespace XBase.Data.JTHY.Expenses
{
   /// <summary>  
   /// 类名：IncomeSettleDBHelper
   /// 
   /// 作者：刘群
   /// 创建时间：2014/7/3
   /// </summary>
    
   public  class IncomeSettleDBHelper
   {

       #region 根据供应商名称和时间查询
       /// <summary>
       /// 根据供应商名称和时间查询采购信息
       /// </summary>
       /// <param name="ProviderId">供应商的Id</param>
       /// <param name="IncomeDateBegin">开始时间</param>
       /// <param name="IncomeDateEnd">结束时间</param>
       /// <returns></returns>
       public static DataTable SearchIncomeSetle(string ProviderName, string DateBegin, string DateEnd, int iscount)
       {
           XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
           string CompanyCD = userInfo.CompanyCD;
           try
           {
               string sql1 = "";
               sql1 = "select c.id,a.ArriveNo,convert(varchar(12), a.CreateDate,23) CreateDate,c.SttlTotalPrice as sttlTotalPrice,c.SttlCount as sttlCounts, a.ProviderID,c.ProductId,c.TaxPrice,c.ProductCount ,";
               sql1 += " p.CustName,jt_cg=0 from jt_cgdh a";
               sql1 += " left join jt_cgdh_mx  c on c.ArriveNo=a.Id ";
               sql1 += " left join officedba.ProviderInfo p on p.Id=a.ProviderId where a.BillStatus='2'";

               string sql2 = @"select  c.id,a.SendNo as ArriveNo,convert(varchar(12), a.CreateDate,23) CreateDate,c.GysTotalPrice as sttlTotalPrice,c.GysCount as sttlCounts,
                            a.ProviderID,c.ProductId,c.InCost as TaxPrice,c.ProductCount ,p.CustName ,jt_cg=1
                            from  jt_xsfh a  
                            left join jt_xsfh_mx c on a.id=c.sendno 
                            left join officedba.ProviderInfo p on a.providerid=p.id 
                            where a.BillStatus='2' and a.BusiType='2'";

               if (iscount == 1)  //超数量结算检索所有的数据
               {
                   sql1 += "  and  a.CompanyCD = '" + CompanyCD + "'";
                   sql2 += "  and  a.CompanyCD = '" + CompanyCD + "'";
               }
               else
               {
                   sql1 += "  and  c.SttlCount<c.ProductCount and   a.CompanyCD = '" + CompanyCD + "'";
                   sql2 += "  and  c.GysCount<c.ProductCount and   a.CompanyCD = '" + CompanyCD + "'";
               }
               if (ProviderName != "")
               {
                   sql1 += " and p.CustName ='" + ProviderName + "'";
                   sql2 += " and p.CustName ='" + ProviderName + "'";
               }
               if (DateBegin != "")
               {
                   sql1 += " and a.CreateDate >= '" + DateBegin.ToString() + "'";
                   sql2 += " and a.CreateDate >= '" + DateBegin.ToString() + "'";
               }

               if (DateEnd != "")
               {
                   sql1 += " and a.CreateDate <= '" + DateEnd.ToString() + "'";
                   sql2 += " and a.CreateDate <= '" + DateEnd.ToString() + "'";
               }
               string sql = sql1 + "  union  " + sql2;
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
