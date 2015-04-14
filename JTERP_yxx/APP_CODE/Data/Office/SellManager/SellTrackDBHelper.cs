using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Model.Office.SellManager;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Data;
using XBase.Data.DBHelper;
using System.Data.SqlClient;

namespace XBase.Data.Office.SellManager
{
    public class SellTrackDBHelper
    {
        //public static string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        //public static string selpoint = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;
        #region 销售列表
        #region 销售单据
        public static DataTable XSorder(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt=SqlHelper.ExecuteStoredProcedure("officedba.Sell_sellorder", param);
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                SqlParameter[] param1= new SqlParameter[3];
                param1[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                param1[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                param1[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                dt1 = SqlHelper.ExecuteStoredProcedure("officedba.Sell_sellsend", param1);
                
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["BillType"] = dt1.Rows[i]["BillType"].ToString();
                        dr["OrderNo"] = dt1.Rows[i]["OrderNo"].ToString();
                        dr["CreateDate"] = dt1.Rows[i]["CreateDate"].ToString();
                        dr["CustName"] = dt1.Rows[i]["CustName"].ToString();
                        dr["ProdNo"] = dt1.Rows[i]["ProdNo"].ToString();
                        dr["ProductName"] = dt1.Rows[i]["ProductName"].ToString();
                        dr["Specification"] = dt1.Rows[i]["Specification"].ToString();
                        dr["UnitName"] = dt1.Rows[i]["UnitName"].ToString();
                        dr["ProductCount"] = dt1.Rows[i]["ProductCount"].ToString();
                        dr["TotalPrice"] = dt1.Rows[i]["TotalPrice"].ToString();
                        dt.Rows.Add(dr);
                    }

                    DataTable dt2 = new DataTable();
                    SqlParameter[] param2 = new SqlParameter[3];
                    param2[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param2[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param2[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Sell_sell_outstroage", param2);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = dt2.Rows[i]["ProductCount"].ToString();
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }
                    SqlParameter[] param3 = new SqlParameter[3];
                    param3[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param3[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param3[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Sell_sellback_outstroage", param3);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = dt2.Rows[i]["ProductCount"].ToString();
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }

                    SqlParameter[] param4 = new SqlParameter[3];
                    param4[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param4[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param4[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Sell_sellbill", param4);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = "0.00";
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }

                    SqlParameter[] param5 = new SqlParameter[3];
                    param5[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param5[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param5[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Sell_sell_paybill", param5);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = "0.00";
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }


                   
                 
                
            }
            return dt;
        }
        #endregion

        #region 生产单据
        public static DataTable SCorder(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Task_Taskorder", param);
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                SqlParameter[] param1 = new SqlParameter[3];
                param1[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                param1[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                param1[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                dt1 = SqlHelper.ExecuteStoredProcedure("officedba.Task_TaskInstroage", param1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["BillType"] = dt1.Rows[i]["BillType"].ToString();
                        dr["OrderNo"] = dt1.Rows[i]["OrderNo"].ToString();
                        dr["CreateDate"] = dt1.Rows[i]["CreateDate"].ToString();
                        dr["DeptName"] = dt1.Rows[i]["DeptName"].ToString();
                        dr["ProdNo"] = dt1.Rows[i]["ProdNo"].ToString();
                        dr["ProductName"] = dt1.Rows[i]["ProductName"].ToString();
                        dr["Specification"] = dt1.Rows[i]["Specification"].ToString();
                        dr["UnitName"] = dt1.Rows[i]["UnitName"].ToString();
                        dr["ProductCount"] = dt1.Rows[i]["ProductCount"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        #endregion

        #region 领料单据
        public static DataTable LLorder(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Task_Tasklingliao", param);
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                SqlParameter[] param1 = new SqlParameter[3];
                param1[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                param1[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                param1[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                dt1 = SqlHelper.ExecuteStoredProcedure("officedba.Task_backlingliao", param1);
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["BillType"] = dt1.Rows[i]["BillType"].ToString();
                        dr["OrderNo"] = dt1.Rows[i]["OrderNo"].ToString();
                        dr["CreateDate"] = dt1.Rows[i]["CreateDate"].ToString();
                        dr["DeptName"] = dt1.Rows[i]["DeptName"].ToString();
                        dr["Taker"] = dt1.Rows[i]["Taker"].ToString();
                        dr["Handout"] = dt1.Rows[i]["Handout"].ToString();
                        dr["ProdNo"] = dt1.Rows[i]["ProdNo"].ToString();
                        dr["ProductName"] = dt1.Rows[i]["ProductName"].ToString();
                        dr["Specification"] = dt1.Rows[i]["Specification"].ToString();
                        dr["UnitName"] = dt1.Rows[i]["UnitName"].ToString();
                        dr["ProductCount"] = dt1.Rows[i]["ProductCount"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        #endregion

        #region 派工单据
        public static DataTable PGorder(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Task_PaiGong", param);
            return dt;
        }
        #endregion

        #region 采购单据
        public static DataTable CGorder(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Purchase_PurchaseOrder", param);
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                SqlParameter[] param1 = new SqlParameter[3];
                param1[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                param1[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                param1[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                dt1 = SqlHelper.ExecuteStoredProcedure("officedba.Purchase_PurchaseArrive", param1);
                
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["BillType"] = dt1.Rows[i]["BillType"].ToString();
                        dr["OrderNo"] = dt1.Rows[i]["OrderNo"].ToString();
                        dr["CreateDate"] = dt1.Rows[i]["CreateDate"].ToString();
                        dr["CustName"] = dt1.Rows[i]["CustName"].ToString();
                        dr["ProdNo"] = dt1.Rows[i]["ProdNo"].ToString();
                        dr["ProductName"] = dt1.Rows[i]["ProductName"].ToString();
                        dr["Specification"] = dt1.Rows[i]["Specification"].ToString();
                        dr["UnitName"] = dt1.Rows[i]["UnitName"].ToString();
                        dr["ProductCount"] = dt1.Rows[i]["ProductCount"].ToString();
                        dr["TotalPrice"] = dt1.Rows[i]["TotalPrice"].ToString();
                        dt.Rows.Add(dr);
                    }

                    DataTable dt2 = new DataTable();
                    SqlParameter[] param2 = new SqlParameter[3];
                    param2[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param2[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param2[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Purchase_InStroage", param2);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = dt2.Rows[i]["ProductCount"].ToString();
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }
                    SqlParameter[] param3 = new SqlParameter[3];
                    param3[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param3[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param3[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Purchase_back_InStroage", param3);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = dt2.Rows[i]["ProductCount"].ToString();
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }

                    SqlParameter[] param4 = new SqlParameter[3];
                    param4[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param4[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param4[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("officedba.Purchase_paybill", param4);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = "0.00";
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }
                   
                    SqlParameter[] param5 = new SqlParameter[3];
                    param5[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                    param5[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
                    param5[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
                    dt2 = SqlHelper.ExecuteStoredProcedure("[officedba].[Purchase_paytoproiver]", param5);
                    if (dt2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["BillType"] = dt2.Rows[i]["BillType"].ToString();
                            dr["OrderNo"] = dt2.Rows[i]["OrderNo"].ToString();
                            dr["CreateDate"] = dt2.Rows[i]["CreateDate"].ToString();
                            dr["CustName"] = dt2.Rows[i]["CustName"].ToString();
                            dr["ProdNo"] = dt2.Rows[i]["ProdNo"].ToString();
                            dr["ProductName"] = dt2.Rows[i]["ProductName"].ToString();
                            dr["Specification"] = dt2.Rows[i]["Specification"].ToString();
                            dr["UnitName"] = dt2.Rows[i]["UnitName"].ToString();
                            dr["ProductCount"] = "0.00";
                            dr["TotalPrice"] = dt2.Rows[i]["TotalPrice"].ToString();
                            dt.Rows.Add(dr);
                        }

                    }
                
            }
            return dt;
        }
        #endregion
        #endregion
        #region 单据进程
        #region 销售进程
        public static DataTable XSjincheng(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Process_sellProcess", param);
            return dt;
        }
        #endregion

        #region 生产进程
        public static DataTable SCjincheng(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Process_TaskProcess", param);
            return dt;
        }
        #endregion

        #region 领料进程
        public static DataTable LLjincheng(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Process_lingliaoProcess", param);
            return dt;
        }
        #endregion

        #region 派工进程
        public static DataTable PGjincheng(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Process_paigongProcess", param);
            return dt;
        }
        #endregion

        #region 采购进程
        public static DataTable CGjincheng(string orderNo)
        {
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = SqlHelper.GetParameter("@companycd", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
            param[1] = SqlHelper.GetParameter("@OrderNo", orderNo);
            param[2] = SqlHelper.GetParameter("@selpoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
            dt = SqlHelper.ExecuteStoredProcedure("officedba.Process_PurchaseProcess", param);
            return dt;
        }
        #endregion
        #endregion
    }
}
