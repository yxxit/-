using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.StorageManager;
using XBase.Data.Office.StorageManager;


namespace XBase.Business.Office.StorageManager
{
    public class DisassemblingListBus
    {
        //检索
        public static DataTable GetStorageInOtherTableBycondition(DisassemblingModel model,  string timeStart, string timeEnd, int pageIndex, int pageCount, string ord,bool isgetlist, ref int TotalCount)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DisassemblingListDBHelper.GetStorageInOtherTableBycondition(model, timeStart, timeEnd, pageIndex, pageCount, ord,isgetlist, ref TotalCount);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            return dt;
        }
    }
}
