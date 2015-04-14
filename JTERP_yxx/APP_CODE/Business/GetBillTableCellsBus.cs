using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Common;

namespace XBase.Business.Common
{
    public class GetBillTableCellsBus
    {
        //获取扩展项
        public static DataTable GetAllCustom(string TabName)
        {
            try
            {
                return GetBillTableCellsDBHelper.GetAllCustom(TabName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //获取单据明细扩展项对应物品扩展项的索引
        public static string GetSingleCustom(string TabName)
        {
            try
            {
                return GetBillTableCellsDBHelper.GetSingleCustom(TabName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //获取用户自定义显示数据
        public static DataTable GetAllField(string moduleid)
        {
            try
            {
                return GetBillTableCellsDBHelper.GetAllField(moduleid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //保存页面设置
        public static bool InsertPageSetUp(string jinbenxinxi, string dingdandetial, string feiyongxinxi, string hejixinxi, string beizuxinxi, string danjuzhuangtai, string isdis, string moduleid,string isenable)
        {
            try
            {
                return GetBillTableCellsDBHelper.InsertPageSetUp(jinbenxinxi, dingdandetial, feiyongxinxi, hejixinxi, beizuxinxi, danjuzhuangtai, isdis, moduleid,isenable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //清除页面设置
        public static bool ClearPageSetUp(string moduleid)
        {
            try
            {
                return GetBillTableCellsDBHelper.ClearPageSetUp(moduleid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable TypeChange(DataTable data)
        {
            return GetBillTableCellsDBHelper.TypeChange(data);
        }
    }
}
