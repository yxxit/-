/**********************************************
 * 类作用：   销售订单事务层处理
 * 建立人：   王玉贞
 * 建立时间： 2009/12/19
 ***********************************************/

using System;
using System.Collections.Generic;
using XBase.Model.Office.SellManager;
using XBase.Data.Office.SellManager;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
using System.Data;
using XBase.Business.Common;
using System.Text;
using System.Collections;
using XBase.Model.Common;
using XBase.Data.Common;

namespace XBase.Business.Office.SellManager
{
    public class ChQuotationForeigBus
    {

        #region 中文报价单插入
       public static bool InsertSellOrder(ChQuotationForeignModel model,List<ChQuotationForeignModel> modellist, out string ID)
        {
            return ChQuotationForeignDBHelper.InsertSellOrder(model, modellist, out ID);

        }
        #endregion

       #region 中文报价单修改
       public static bool UpdateInvoice(ChQuotationForeignModel InvoiceM, List<ChQuotationForeignModel> InvoiceDMList)
        {
            return ChQuotationForeignDBHelper.UpdateInvoice(InvoiceM, InvoiceDMList);
        }
       #endregion

       #region 英文报价单插入
       public static bool InsertEnQuotation(EnQuotationForeignModel model, List<EnQuotationForeignModel> modellist, out string ID)
       {
           return ChQuotationForeignDBHelper.InsertEnQuotation(model, modellist, out ID);

       }
       #endregion

       #region 英文报价单修改
       public static bool UpdateEnQuotation(EnQuotationForeignModel InvoiceM, List<EnQuotationForeignModel> InvoiceDMList)
       {
           return ChQuotationForeignDBHelper.UpdateEnQuotation(InvoiceM, InvoiceDMList);
       }
       #endregion

       #region 删除中文报价单
       /// <summary>
       /// 删除中文报价单
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public static bool DeleteInvoice(string IDS)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            try
            {
                isSucc = ChQuotationForeignDBHelper.DeleteInvoice(IDS);
            }
            catch (Exception ex)
            {
                isSucc = false;
            }
            return isSucc;
        }
        #endregion

        #region 删除英文报价单
        /// <summary>
        /// 删除英文报价单
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public static bool DeleteEnQuotation(string IDS)
        {
            bool isSucc = false;//是否添加成功
            //定义变量
            string remark = string.Empty;
            try
            {
                isSucc = ChQuotationForeignDBHelper.DeleteEnQuotation(IDS);
            }
            catch (Exception ex)
            {
                isSucc = false;
            }
            return isSucc;
        }
        #endregion


       #region 通过检索条件查询中文报价单信息
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
        public static DataTable GetInvoiceList(ChQuotationForeignModel model, string CreateDate, string CreateDate1, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            return ChQuotationForeignDBHelper.GetInvoiceList(model, CreateDate, CreateDate1, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region 通过检索条件查询英文报价单信息
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
        public static DataTable GetEnQuotationList(EnQuotationForeignModel model, string CreateDate, string CreateDate1, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            return ChQuotationForeignDBHelper.GetEnQuotationList(model, CreateDate, CreateDate1, PageIndex, PageCount, OrderBy, ref TotalCount);
        }
        #endregion

        #region  根据编号获取中文报价详细信息
        /// <summary>
        /// 根据编号获取中文报价详细信息
        /// </summary>
        /// <param name="QuotaNo">报价单No</param>
        /// <returns>datatable</returns>
        public static DataTable GetInvoiceDetailByNo(string CompanyCD,string QuotaNo)
        {
            return ChQuotationForeignDBHelper.GetInvoiceDetailByNo(CompanyCD,QuotaNo);
        }
        #endregion
        #region  根据编号获取英文报价详细信息
        /// <summary>
        /// 根据编号获取英文报价详细信息
        /// </summary>
        /// <param name="QuotaNo">报价单No</param>
        /// <returns>datatable</returns>
        public static DataTable GetEnQuotationByNo(string CompanyCD, string QuotaNo)
        {
            return ChQuotationForeignDBHelper.GetEnQuotationByNo(CompanyCD, QuotaNo);
        }
        #endregion

        public static DataTable GetSellOutHistoryList(ChQuotationForeignModel model, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return ChQuotationForeignDBHelper.GetSellOutHistoryList(model, pageIndex, pageCount, ord, ref totalCount);
        }


        #region 获取信息BYID
        /// <summary>
        /// 获取信息BYID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetInvoiceDataByID(string ID)
        {
            return ChQuotationForeignDBHelper.GetInvoiceDataByID(ID);
        }
        #endregion

        #region 获取英文报价单BYID
        /// <summary>
        /// 获取英文报价单BYID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetEnQuotationByID(string ID)
        {
            return ChQuotationForeignDBHelper.GetEnQuotationByID(ID);
        }
        #endregion

    }
}
