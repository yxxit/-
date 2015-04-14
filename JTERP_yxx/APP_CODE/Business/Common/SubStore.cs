using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace XBase.Business.Common
{
    public class SubStore
    {

        #region 验证当前用户是分店用户还是总店
        /// <summary>
        ///  是否分店 分店 true   总店 false
        /// </summary>
        /// <param name="DeptID"></param>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static bool IsSubStore(int DeptID, string CompanyCD)
        {
            return Data.Common.SubStore.IsSubStore(DeptID, CompanyCD);
        }
        #endregion

        #region 获取当前分店ID
        public static string GetSubStoreID(string CurrentDeptID)
        {
            return Data.Common.SubStore.GetSubStoreID(CurrentDeptID);
        }
        #endregion

        #region 判断是总店还是分店
        public static bool GetComOrSub(string CurrentDeptID)
        {
            return Data.Common.SubStore.GetComOrSub(CurrentDeptID);
        }
        #endregion

        #region 获取从属于当前分店或者分公司下的所有分店ID和分公司ID列表
        public static ArrayList GetSubStoreIDList(string DeptID)
        {
            ArrayList DeptIDlist = new ArrayList();

            DeptIDlist = Data.Common.SubStore.GetPubSubStoreIDList(DeptID);
            return DeptIDlist;
        }
        #endregion

        #region 获取当前分店下所的分店ID 包含当前分店ID
        public static ArrayList GetSubStoreIDListIn(string DeptID)
        {
            Data.Common.SubStore.PubSubStoreList.Clear();
            ArrayList DeptIDlist = GetSubStoreIDList(DeptID);
            DeptIDlist.Add(DeptID);
            return DeptIDlist;
        }
        #endregion

        #region 查询某公司下所有的分店或者分公司
        public static ArrayList GetAllSubStore(string companyCD)
        {
            return Data.Common.SubStore.GetAllSubStore(companyCD);
        }
        #endregion

        #region 获取分店名称
        public static string GetSubStoreName(string DeptID)
        {
            return Data.Common.SubStore.GetSubStoreName(DeptID);
        }
        #endregion


        #region 取顶级公司DeptID
        public static string GetTopDeptID(string companyCD)
        {
            return Data.Common.SubStore.GetTopDeptID(companyCD);
        }
        #endregion

    }
}