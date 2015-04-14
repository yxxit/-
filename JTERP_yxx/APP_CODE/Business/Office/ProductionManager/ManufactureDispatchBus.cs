/**********************************************
 * 脚本作用： 生产派工的页面操作
 * 建立人：   沈健平
 * 建立时间： 2011/03/16
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.ProductionManager;
using XBase.Common;

namespace XBase.Business.Office.ProductionManager
{
    public class ManufactureDispatchBus
    {
        #region 查询最后一条派工记录
        public static DataTable GetManufactureDispatch(string taskno,string pageno)
        {
            try
            {
                return ManufactureDispatchDBHelper.GetManufactureDispatch(taskno,pageno);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
         #endregion
         #region 确认
        public static bool updatecomfirmor(int pgid)
        {
            try
            {
                return ManufactureDispatchDBHelper.updatecomfirmor(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 检查是否被引用
        public static bool checkpg(int pgid)
        {
            try
            {

                return ManufactureDispatchDBHelper.checkpg(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
         #region 取消确认
        public static bool unupdatecomfirmor(int pgid)
        {
            try
            {

                return ManufactureDispatchDBHelper.unupdatecomfirmor(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
         #region 结单
        public static bool close(int pgid)
        {
            try
            {
                return ManufactureDispatchDBHelper.close(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
         #region 取消结单
        public static bool unclose(int pgid)
        {
            try
            {
                return ManufactureDispatchDBHelper.unclose(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
         #region 保存
        public static bool save(int pgid, int rwid, string DetSortNo, string DetProductID, string DetStartDate, string DetEndDate, string DetFromType, string DetFromBillID, string DetFromBillNo, string DetFromLineNo)
        {
            try
            {
                return ManufactureDispatchDBHelper.save(pgid, rwid, DetSortNo, DetProductID, DetStartDate, DetEndDate, DetFromType, DetFromBillID, DetFromBillNo, DetFromLineNo);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion

         #region 取消派工
        public static bool delete(int pgid)
        {
            try
            {
                return ManufactureDispatchDBHelper.delete(pgid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region  检索生产派工单
        public static DataTable SelectManufactureDispatchList(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string Purchase, string TaskNo, string GoodsName, string WorkCenter, string StartLinkDate, string EndLinkDate)
        {
            try
            {
                return ManufactureDispatchDBHelper.SelectProviderManufactureDispatchList(pageIndex, pageCount, orderBy, ref TotalCount, Purchase, TaskNo, GoodsName, WorkCenter, StartLinkDate, EndLinkDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 弹出对话框得到派工单列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">一页显示条数</param>
        /// <param name="orderBy">排序</param>
        /// <param name="TotalCount">总条数</param>
        /// <param name="Purchase"></param>
        /// <param name="TaskNo"></param>
        /// <param name="GoodsName">商品名称</param>
        /// <param name="WorkCenter">工作中心</param>
        /// <param name="StartLinkDate">开工时间</param>
        /// <param name="EndLinkDate">完工时间</param>
        /// <returns></returns>
        public static DataTable GetManufactureDispatch(int pageIndex, int pageCount, string orderBy, ref int TotalCount, string Purchase, string TaskNo, string GoodsName, string WorkCenter, string StartLinkDate, string EndLinkDate)
        {
            try
            {
                DataTable dt = new DataTable();
                return ManufactureDispatchReportDBHelper.GetManufactureDispatchReport(pageIndex, pageCount, orderBy, ref  TotalCount, Purchase, TaskNo, GoodsName, WorkCenter, StartLinkDate, EndLinkDate);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        /// <summary>
        /// 弹出对话框：复选框，选择派工单
        /// </summary>
        /// <param name="strid">派工单号</param>
        /// <returns></returns>
        public static DataTable GetManufactureDispatchByCheck(string strid)
        {
            try
            {
                UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                string CompanyCD = userInfo.CompanyCD;
                return ManufactureDispatchReportDBHelper.GetManufactureDispatchReportByCheck(strid, CompanyCD);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

    }
}
