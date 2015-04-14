using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using XBase.Model.Office.CustManager;
namespace XBase.Business.Common
{
    public class ProviderOrCustomBus
    {
        //获取往来单位列表
        public static DataTable GetProviderCustomList(Hashtable htParams, int PageSize, int PageIndex, string OrderBy, ref int TotalCount)
        {
            return Data.Common.ProviderOrCustomDBHelper.GetProviderCustomList(htParams, PageSize, PageIndex, OrderBy, ref TotalCount);
        }


        #region 添加往来单位
        public static string AddProviderCustom(Model.Office.CustManager.CustInfoModel model)
        {
            return Data.Common.ProviderOrCustomDBHelper.AddProviderCustom(model);
        }
        #endregion
    }
}
