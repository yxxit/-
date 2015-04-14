using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XBase.Model.Office.SupplyChain;
using XBase.Data.Office.SupplyChain;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Common;

using XBase.Model.Common;
namespace XBase.Business.Office.SupplyChain
{
    public class CustProductBus
    { 
        
       /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(CustProductModel CustProdModel, int PageIndex, int PageCount, string OrderBy, ref int TotalCount)
        {
            
            try
            {
                return CustProductDBHelper.GetDataTable(CustProdModel, PageIndex, PageCount, OrderBy, ref TotalCount);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetProd(string CustID,string ProdNo)
        {

            try
            {
                return CustProductDBHelper.GetProd(CustID,ProdNo);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="CompanyCD"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByCompanyCD(string CompanyCD )
        {

            try
            {
                return CustProductDBHelper.GetDataTableByCompanyCD(CompanyCD );
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }
        /// <summary>
        /// 通过客户和商品获取商品别名
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataTableByCustProduct(string companyCD, string custid,string prodno)
        {
            try
            {
                return CustProductDBHelper.GetAlias(companyCD,custid,prodno);
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
      
        /// <summary>
        /// 插入对照信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool InsertCustProduct(CustProductModel model)
        {
                 
           try
           {
              bool  succ = CustProductDBHelper.InsertCustProduct(model);
                
                return succ;
            }
            catch (Exception ex)
            {
               
                return false;
            }

        }

        /// <summary>
        /// 修改文档种类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool UpdateCustProduct(CustProductModel model)
        {
            
            try
            {
                bool succ = false;
                
                succ = CustProductDBHelper.UpdateCustProduct(model);
                
                return succ;
            }
            catch (Exception ex)
            {
                
                return false;
            }

        }

   
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCustProduct(string id)
        {


            bool isSucc = CustProductDBHelper.DeleteCustProduct(id);
           
            return isSucc;



        }
     


    
        /// <summary>
        /// 校验重复值
        /// </summary>      
        /// <returns>bool 是否已经存在 true 不存在 false 存在</returns>
        public static bool CheckCustProduct(CustProductModel model)
        {
           

            //校验存在性
            return CustProductDBHelper.CheckCustProduct(model);
        }
        
    }
}
