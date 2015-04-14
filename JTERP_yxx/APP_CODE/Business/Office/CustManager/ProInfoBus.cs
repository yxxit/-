using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Data.Office.CustManager;
using XBase.Model.Office.CustManager;
using XBase.Business.Common;
using XBase.Model.Common;
using XBase.Common;
using XBase.Data.Common;
using System.Collections;

namespace XBase.Business.Office.CustManager
{
    public class ProInfoBus
    {
       

        #region 根据创建人获取供应商ID、编号、简称的方法
        /// <summary>
        /// 根据创建人获取供应商ID、编号、简称的方法
        /// </summary>
        /// <param name="Creator">创建人</param>
        /// <param name="CompanyCD">公司代码</param>
        /// <returns>供应商列表结果集</returns>
        public static DataTable GetProName(ProInfoModel ProModel, string CanUserID, string CompanyCD)
        {
            try
            {
                return ProInfoDBHelper.GetProName(ProModel, CanUserID, CompanyCD);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        

        

        #region 获取附件列表
        public static DataTable GetCustAttachInfoBycondition(string CompanyCD,string CustName,string Attachment,string remark,int pageIndex,int pageCount,string ord,ref int TotalCount)
        {
            return CustInfoDBHelper.GetCustAttachInfoBycondition(CompanyCD,CustName,Attachment,remark,pageIndex,pageCount,ord,ref TotalCount);
        }
        #endregion

        #region 删除附件
        public static bool  DelAttachment(string CompanyCD, string filename, string realname)
        {
            bool isSuc = false;
            try
            {
              int result= CustInfoDBHelper.DelAttachment(CompanyCD,filename,realname);
              if (result > 0)
              {
                  isSuc = true;
              }
              else
              {
                  isSuc = false;
              }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return isSuc;
        }
        #endregion

        

    }
}
