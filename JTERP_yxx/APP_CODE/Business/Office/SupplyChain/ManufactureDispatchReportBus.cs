using System;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Model.Common;
using XBase.Business.Common;
using XBase.Data.Common;
using System.Collections;

namespace XBase.Business.Office.SupplyChain
{
    public class ManufactureDispatchReportBus
    {
        public ManufactureDispatchReportBus() { }
        /// <summary>
        /// 得到派工单列表
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
        /// 复选框，选择派工单
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
