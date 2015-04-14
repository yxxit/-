/***********************************************
 * 类作用：   合同管理事务层处理               *
 * 建立人：   包胜东                             *
 * 建立时间： 2014/03/21                       *
 ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using XBase.Common;
using System.Data.SqlTypes;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Data.Common;
using XBase.Data.Office.ContractInfoDBHelper;

namespace XBase.Business.Office.ContractManage
{
    // <summary>
    /// 类名：ContractInfoBus
    /// 描述：合同管理事务层处理
    /// 
    /// 作者：包胜东
    /// 创建时间：2014/03/21
    /// </summary>
    public class ContractInfoBus
    {
        #region 绑定采购供应商类别
        /// <summary>
        /// 绑定采购供应商类别
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCustType()
        {
            DataTable dt = ContractInfoDBHelper.GetdrpCustType();
            return dt;
        }
        #endregion

        #region 绑定采购供应商分类
        /// <summary>
        /// 绑定采购供应商分类
        /// </summary>
        /// <returns>DataTable</returns>

        public static DataTable GetdrpCustClass()
        {
            DataTable dt = ContractInfoDBHelper.GetdrpCustClass();
            return dt;
        }
        #endregion

        /// <summary>
        /// 绑定结算方式
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpSettleType()
        {
            DataTable dt = ContractInfoDBHelper.GetdrpSettleType();
            return dt;
        }
        /// <summary>
        /// 绑定运输类型
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpTranSportType()
        {
            DataTable dt = ContractInfoDBHelper.GetdrpTranSportType();
            return dt;
        }

        /// <summary>
        /// 绑定煤种
        /// </summary>
        /// <returns></returns>
        public static DataTable GetdrpCoalType()
        {
            DataTable dt = ContractInfoDBHelper.GetdrpCoalType();
            return dt;
        }

        // 绑定单位
        public static DataTable GetdrpUnitCode()
        {
            DataTable dt = ContractInfoDBHelper.GetUnitCode();
            return dt;
        }

        //绑定 始发站 (参数 3)、原到站 (参数 5)、终到站 (参数 7)  liuch add
        public static DataTable GetTransStation(int intTypeID)
        {
            DataTable dt = ContractInfoDBHelper.GetTransStation(intTypeID);
            return dt;
        }

    }
}
