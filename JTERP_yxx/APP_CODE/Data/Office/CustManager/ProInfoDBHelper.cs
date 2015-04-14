/**********************************************
 * 类作用：   供应商信息数据层处理
 * 建立人：   包胜东
 * 建立时间： 2013/10/11
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Model.Office.CustManager;
using System.Data.SqlTypes;
using XBase.Common;
using System.Collections;
using System.Configuration;


namespace XBase.Data.Office.CustManager
{
    public class ProInfoDBHelper
    {
       
        #region 获取扩展属性
        public static DataTable GetExtAttrValue(string strKey, string CustNo, string CompanyCD)
        {
            //DataTable dt = new DataTable();
            string strSql = "select " + strKey + " from officedba.CustInfo where CompanyCD = @CompanyCD AND CustNo = @CustNo ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            arr.Add(new SqlParameter("@CustNo", CustNo));
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }
        /// <summary>
        /// 打印需要
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="CustNo"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetExtAttrValueReport(string strKey, string CustNo, string CompanyCD)
        {
            //DataTable dt = new DataTable();
            string strSql = "select " + strKey + "  from officedba.CustInfo where CompanyCD = @CompanyCD AND CustNo = @CustNo ";
            ArrayList arr = new ArrayList();
            arr.Add(new SqlParameter("@CompanyCD", CompanyCD));
            arr.Add(new SqlParameter("@CustNo", CustNo));
            return SqlHelper.ExecuteSql(strSql.ToString(), arr);
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 扩展属性保存操作
        /// </summary>
        /// <returns></returns>
        private static void GetExtAttrCmd(CustInfoModel model, Hashtable htExtAttr, SqlCommand cmd)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "UPDATE officedba.CustInfo set ";
                foreach (DictionaryEntry de in htExtAttr)// fileht为一个Hashtable实例
                {
                    strSql += de.Key.ToString().Trim() + "=@" + de.Key.ToString().Trim() + ",";
                    cmd.Parameters.Add("@" + de.Key.ToString().Trim(), de.Value.ToString().Trim());
                }
                int iLength = strSql.Length - 1;
                strSql = strSql.Substring(0, iLength);
                strSql += " where CompanyCD = @CompanyCD  AND CustNo = @CustNo";
                cmd.Parameters.Add("@CompanyCD", model.CompanyCD);
                cmd.Parameters.Add("@CustNo", model.CustNo);
                cmd.CommandText = strSql;
            }
            catch (Exception d)
            { }
        }
        #endregion 

      
        #region 根据创建人获取供应商ID、名称的方法
        /// <summary>
        /// 根据创建人获取供应商ID、编号、简称的方法
        /// 
        /// </summary>
        /// <param name="Creator">创建人</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>供应商列表结果集</returns>
        public static DataTable GetProName(ProInfoModel ProModel, string CanUserID, string CompanyCD)
        {
            XBase.Common.UserInfoUtil userInfo = (XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"];
            string sql = "";
            try
            {
                if (userInfo.Version == "medicine")  // 医药行业，直接从 T6 ERP中取数据
                {
                    string Hc_Database = ConfigurationManager.AppSettings["HC_database"].ToString();
                    sql = "SELECT top 200 a.cvencode id,a.cvencode CompanyId,a.cvenname CompanyName,a.cvenabbname,a.cvccode,b.cvcname,isnull(a.cvenaddress,'')cvenaddress," +
                    "isnull(a.cvenpostcode,'')cvenpostcode,isnull(a.cvenfax,'')cvenfax,isnull(a.cvenperson,'')cvenperson,"+
                    "isnull(a.cvenphone,'')cvenphone,isnull(a.cvenemail,'')cvenemail,isnull(a.cVenLPerson,'')cVenLPerson,"+
                    "isnull(a.ctrade,'')ctrade,isnull(a.cvendefine6,'') as Xukezheng,isnull(a.cvendefine5,'') as yyzzNo from " + Hc_Database + ".dbo.vendor a inner join " + Hc_Database + ".dbo.vendorclass b" +
                     " on a.cvccode=b.cvccode ";
                    sql += " where 1=1 and a.dEndDate is null ";
                    if (ProModel.Cvencode != "")
                        sql += " and a.cvencode like '%" + ProModel.Cvencode + "%'";
                    if (ProModel.Cvenname != "")
                        sql += " and a.cvenname like '%" + ProModel.Cvenname + "%'";

                    if (ProModel.Free1 == "首营药品")
                    {
                        sql = "";
                        sql = "SELECT top 200 a.cvencode id,a.cvencode CompanyId,a.cvenname CompanyName,a.cvenabbname,a.cvccode,b.cvcname,isnull(a.cvenaddress,'')cvenaddress," +
                      "isnull(a.cvenpostcode,'')cvenpostcode,isnull(a.cvenfax,'')cvenfax,isnull(a.cvenperson,'')cvenperson," +
                      "isnull(a.cvenphone,'')cvenphone,isnull(a.cvenemail,'')cvenemail,isnull(a.cVenLPerson,'')cVenLPerson," +
                      "isnull(a.ctrade,'')ctrade,isnull(a.cvendefine6,'') Xukezheng,c.xkzEndDate,isnull(a.cvendefine5,'') yyzzNo,c.yyzzEndDate,c.RzsEndDate," +
                      "c.RenZhengShuNo from " + Hc_Database + ".dbo.vendor a inner join " + Hc_Database + ".dbo.vendorclass b" +
                       " on a.cvccode=b.cvccode    left join officedba.ProviderExpand c on a.cvencode=c.id  ";
                        sql += " where 1=1 and a.dEndDate is null ";
                        if (ProModel.Cvencode != "")
                            sql += " and a.cvencode like '%" + ProModel.Cvencode + "%'";
                        if (ProModel.Cvenname != "")
                            sql += " and a.cvenname like '%" + ProModel.Cvenname + "%'";


                    }
                   
                }
           
                return SqlHelper.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                string sss = ex.Message;
                return null;
            }
        }
        #endregion

       

    

       
       
    }
}

