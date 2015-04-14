<%@ WebHandler Language="C#" Class="MedicineList" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using XBase.Model.Office.SellManager;
using XBase.Common;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using XBase.Business.Office.PurchaseManager;
using XBase.Model.Office.PurchaseManager;
using XBase.Business.Office.PurchaseManager;
using XBase.Data.Office.PurchaseManager;
using XBase.Data.DBHelper;
 

public class MedicineList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.Params["action"].ToString();//操作

            if(action == "getprodcutinfo")
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数
                string strid = "";
                try
                {
                    strid = context.Request.Params["str"].ToString();
                }
                catch (Exception)
                {
                    strid = context.Request.QueryString["str"].ToString();
                }
                // dt = MedicineRetailBus.GetProductInfoTableByCheckcondition(strid, companyCD);

                string strsql =
                "    select   a.id,a.prodno,a.pyshort,a.productname,isnull(a.storageId,'0') as storageId, a.unitid,isnull(b.codename,'') as unitname,isnull(a.specification,'') specification,isnull(a.HeatPower,'') as HeatPower, " +
                "    c.StorageName ,a.StandardBuy,a.StandardSell " +
                "     from  officedba.ProductInfo a " +
                "    left join officedba.CodeUnitType b on a.unitid=b.ID " +
                "    left join officedba.StorageInfo c on c.id=a.StorageID  " +
                "     where a.companycd='" + companyCD + "' and a.id='" + strid + "'   ";
                dt = SqlHelper.ExecuteSql(strsql);
                

                int totalCount = dt.Rows.Count;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (action == "getcoaldata")
            {
                DataTable dt = null;
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

                dt = XBase.Business.Office.ContractManage.ContractInfoBus.GetdrpCoalType();

                int totalCount = dt.Rows.Count;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");

                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End(); 
            }


        }

    }
      
    public bool IsReusable {
        get {
            return false;
        }
    }

}