/*************************************
 * 创建人：何小武
 * 创建日期：2009-12-12
 * 描述：销售出库
 ************************************/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XBase.Data.Office.SellManager;
using XBase.Model.Office.SellManager;

namespace XBase.Business.Office.SellManager
{
    public class SellOutStorageBus
    {
        #region 添加销售出库单
        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        public static bool SaveSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable ht, out string strMsg)
        {
            return SellOutStorageDBHelper.SaveSellOutStorage(sellOutModel, sellOutDMList, ht, out strMsg);
        }

        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="sellOutFDMList">SellOutStorageFeeDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        public static bool SaveSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList,List<SellOutStorageFeeDetailModel> sellOutFDMList, Hashtable ht, out string strMsg)
        {
            return SellOutStorageDBHelper.SaveSellOutStorage(sellOutModel, sellOutDMList,sellOutFDMList, ht, out strMsg);
        }
        #endregion

        #region 修改销售出库单
        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        public static bool UpdataSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable htExtAttr, out string strMsg)
        {
            return SellOutStorageDBHelper.UpdataSellOutStorage(sellOutModel, sellOutDMList, htExtAttr, out strMsg);
        }

        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体列表</param>
        /// <param name="sellOutFDMList">SellOutStorageFeeDetailModel实体列表</param>
        /// <param name="strMsg">返回信息</param>
        public static bool UpdataSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList,List<SellOutStorageFeeDetailModel> sellOutFDMList, Hashtable htExtAttr, out string strMsg)
        {
            return SellOutStorageDBHelper.UpdataSellOutStorage(sellOutModel, sellOutDMList,sellOutFDMList, htExtAttr, out strMsg);
        }
        #endregion

        #region 删除销售出库单
        /// <summary>
        /// 删除销售出库单
        /// </summary>
        /// <param name="NoStr">编号序列</param>
        public static bool DelSellOutStorage(string NoStr, int branchID, string strCompanyCD, out string strMsg, out string strFieldText)
        {
            return SellOutStorageDBHelper.DelSellOutStorage(NoStr, branchID, strCompanyCD, out strMsg, out strFieldText);
        }
        #endregion

        #region 确认销售出库单
        /// <summary>
        /// 确认销售出库单
        /// </summary>
        /// <param name="sellOutModel">SellOutStorageModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true确认成功，false确认失败</returns>
        public static bool ConfirmSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, Hashtable htExtAttr, string EmployeeName, int EmployeeID, out string strMsg)
        {
            return SellOutStorageDBHelper.ConfirmSellOutStorage(sellOutModel, sellOutDMList, htExtAttr, EmployeeName, EmployeeID, out strMsg);
        }
        #endregion

        #region 作废销售出库单
        /// <summary>
        /// 作废销售出库单
        /// </summary>
        /// <param name="sellOutModel">sellOutModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true作废成功，false作废失败</returns>
        public static bool CancelSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, int EmployeeID, string EmployeeName, out string strMsg)
        {
            return SellOutStorageDBHelper.CancelSellOutStorage(sellOutModel, sellOutDMList, EmployeeID, EmployeeName, out strMsg);
        }
        #endregion

        #region 取消确认销售出库单
        /// <summary>
        /// 取消确认销售出库单
        /// </summary>
        /// <param name="sellOutModel">sellOutModel实体</param>
        /// <param name="sellOutDMList">SellOutStorageDetailModel实体List</param>
        /// <param name="EmployeeID">当前用户ID</param>
        /// <param name="EmployeeName">当前用户名称</param>
        /// <param name="strMsg">返回信息</param>
        /// <returns>true取消确认成功，false取消确认失败</returns>
        public static bool ScrapSellOutStorage(SellOutStorageModel sellOutModel, List<SellOutStorageDetailModel> sellOutDMList, int EmployeeID, string EmployeeName, out string strMsg)
        {
            return SellOutStorageDBHelper.ScrapSellOutStorage(sellOutModel, sellOutDMList, EmployeeID, EmployeeName, out strMsg);
        }
        #endregion

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据ID获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByID(int BillID)
        {
            return SellOutStorageDBHelper.GetSellOutInfoByID(BillID);
        }
        #endregion

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据ID获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByIDR(int BillID)
        {
            return SellOutStorageDBHelper.GetSellOutInfoByIDR(BillID);
        }
        #endregion

       

        #region  根据ID获取单据详细信息
        /// <summary>
        /// 根据No获取单据详细信息
        /// </summary>
        /// <param name="BillID">单据No</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutInfoByNo(string BillNo)
        {
            return SellOutStorageDBHelper.GetSellOutInfoByNo(BillNo);
        }
        #endregion

        #region 根据单据编号，公司编码，仓库ID
        public static DataTable GetSellOutDetail(string BillNo, string strCompanyCD, int storageID, int BranchID)
        {
            return SellOutStorageDBHelper.GetSellOutDetail(BillNo, strCompanyCD, storageID, BranchID);
        }
        #endregion

        #region 获取销售出库单历史单据列表
        /// <summary>
        /// 获取销售出库单历史单据列表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutHistoryList(SellOutStorageModel model, DateTime? SellDate1, string FromBillNo, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetSellOutHistoryList(model, SellDate1, FromBillNo, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 获取销售出库单列表
        /// <summary>
        /// 获取销售出库单列表
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="SellDate1">销售日期</param>
        /// <param name="FromBillNo">源单编号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutList(SellOutStorageModel model, DateTime? SellDate1, string FromBillNo, string ProductID,int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetSellOutList(model, SellDate1, FromBillNo,ProductID, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion


        #region 获取销售出库单费用列表
        public static DataTable GetSellOutFeeList(string OutNo, string ConfirmDate, string ConfirmDate1, string FeeID,string BillStatus, int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetSellOutFeeList(OutNo, ConfirmDate, ConfirmDate1, FeeID,BillStatus, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion

        #region 获取销售出库单列表(用于物流费用统计)
        /// <summary>
        /// 获取销售出库单列表（用于物流费用统计）
        /// </summary>
        /// <param name="model">SellOutStorageModel实体</param>
        /// <param name="Confirmdate1">确认日期</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="ord">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns>datatable</returns>
        public static DataTable GetTransFeeList(SellOutStorageModel model, string ConfirmDate,string  ConfirmDate1,int pageIndex, int pageCount, string ord, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetTransFeeList(model,ConfirmDate, ConfirmDate1, pageIndex, pageCount, ord, ref totalCount);
        }
        #endregion
        #region 根据单据编号和公司编码获取单据ID
        /// <summary>
        /// 根据单据编号和公司编码获取单据ID
        /// </summary>
        /// <param name="BillNo">单据编号</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <returns>ID</returns>
        public static int GetSellOutID(string BillNo, string strCompanyCD)
        {
            return SellOutStorageDBHelper.GetSellOutID(BillNo, strCompanyCD);
        }
        #endregion

        #region 根据仓库，商品ID获取批次列表
        /// <summary>
        /// 根据仓库，商品ID获取批次列表
        /// </summary>
        /// <param name="StorageID">仓库ID</param>
        /// <param name="ProductID">商品ID</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID，总店为0</param>
        /// <returns></returns>
        public static DataTable GetBatchNoList(int StorageID, int ProductID, string strCompanyCD, int branchID)
        {
            return SellOutStorageDBHelper.GetBatchNoList(StorageID, ProductID, strCompanyCD, branchID);
        }
        #endregion

        #region 根据商品ID，销售日期获取对应的促销政策(零售)
        /// <summary>
        /// 根据商品ID，销售日期获取对应的促销政策
        /// </summary>
        /// <param name="rowid">行号</param>
        /// <param name="productID">商品ID串</param>
        /// <param name="sellDate">销售日期</param>
        /// <param name="batchNo">批次</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetPromotionByPdtIDS(string rowid, string productID, DateTime? sellDate, string batchNo, string strCompanyCD, int branchID)
        {
            return SellOutStorageDBHelper.GetPromotionByPdtIDS(rowid,productID, sellDate, batchNo, strCompanyCD, branchID);
        }
        #endregion
        #region 根据商品ID，销售日期,客户获取对应的促销政策(批发)
        /// <summary>
        /// 根据商品ID，销售日期，客户获取对应的促销政策
        /// </summary>
        /// <param name="rowid">行号</param>
        /// <param name="productID">商品ID串</param>
        /// <param name="sellDate">销售日期</param>
        /// <param name="custID">客户ID</param>
        /// <param name="batchNo">批次</param>
        /// <param name="strCompanyCD">公司编码</param>
        /// <param name="branchID">分店ID</param>
        /// <returns>datatable</returns>
        public static DataTable GetPromotionByPdtIDS(string rowid,string productID, DateTime? sellDate, int custID, string batchNo, string strCompanyCD, int branchID)
        {
            return SellOutStorageDBHelper.GetPromotionByPdtIDS(rowid,productID, sellDate, custID, batchNo, strCompanyCD, branchID);
        }
        #endregion

        #region 报表
        /// <summary>
        /// 销售结算表
        /// </summary>
        /// <param name="list">1.CustID 客户、2.StartDate 开始时间 、3.EndDate 结束时间、4.IsBack 是否含退货、5.只显示未完全核销的记录、6.CompanyCD 、7.BranchID </param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageCount">页数</param>
        /// <param name="order">排序</param>
        /// <param name="totalCount">记录数</param>
        /// <returns></returns>
        public static DataTable GetSalesBalanceSheet(List<string> list, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetSalesBalanceSheet(list, pageIndex, pageCount, order, ref totalCount);
        }

        public static DataTable GetBlendBill(string sourceId, string sourceType, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetBlendBill(sourceId, sourceType, pageIndex, pageCount, order, ref totalCount);
        }
        public static DataTable GetBlendPrice(string type, string branchid, string custid, string date1, string date2, int pageIndex, int pageCount, string order, ref int totalCount)
        {
            return SellOutStorageDBHelper.GetBlendPrice(type, branchid, custid, date1, date2, pageIndex, pageCount, order, ref totalCount);
        }

        public static DataTable GetInStorageListBycondition(XBase.Model.Office.PurchaseManager.PurchaseOutStorageModel model, string CustID, int pageIndex, int pageCount, string OrderBy, ref int totalCount)
        {
            try
            {
                return SellOutStorageDBHelper.GetInStorageListBycondition(model, CustID, pageIndex, pageCount, OrderBy, ref totalCount);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 销售业绩统计分析
        /// <summary>
        /// 获得销售业绩统计分析数据
        /// </summary>
        /// <param name="info">0.BranchID 分店ID、1.CustID 客户ID、
        /// 2.UserID 业务员ID、3.StorageID 仓库、4 .StartDate 开始时间、
        /// 5.EndDate 结束时间、6.IsBack 是否含退货、7.分组字符串、
        /// 8.是否查询分店、9.统计类型:True:数量，false：金额、10.CompanyCD 客户</param>
        /// <returns></returns>
        public static DataTable GetSellAchievement(List<object> list)
        {
            return SellOutStorageDBHelper.GetSellAchievement(list);
        }
        #endregion

        #region  选择销售出库单
        /// <summary>
        /// 选择销售出库单
        /// </summary>
        /// <returns>datatable</returns>
        public static DataTable GetSellOutListByCondition1(string OrderNo, string CustID,int pageIndex, int pageCount, string ord, ref int TotalCount)
        {
            return SellOutStorageDBHelper.GetSellOutListByCondition1(OrderNo,CustID, pageIndex, pageCount, ord, ref TotalCount);
        }
        #endregion

        #region 获取销售发货单明细信息（以填充发票明细列表）
        /// <summary>
        /// 获取销售发货单明细信息（以填充发票明细列表）
        /// </summary>
        /// <param name="strDetailIDList"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetInfoByDetalIDList(string strDetailIDList, string CompanyCD)
        {
            return SellOutStorageDBHelper.GetInfoByDetalIDList(strDetailIDList, CompanyCD);
        }
        #endregion

        #region 销售出库单费用明细信息
        /// <summary>
        /// 销售出库单费用明细信息
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetSellOutStorageFeeDetail(string OutNo)
        {
            try
            {
                return SellOutStorageDBHelper.GetSellOutStorageFeeDetail(OutNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
