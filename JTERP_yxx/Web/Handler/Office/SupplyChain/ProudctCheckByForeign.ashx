<%@ WebHandler Language="C#" Class="ProudctCheckByForeign" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;


public class ProudctCheckByForeign : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            try
            {
                //设置行为参数
                DataTable dt = new DataTable();
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数
                string StorageID = "";
                string BranchID = "";
                string strDept = "";
                string DeptID = "";
                string strid = context.Request.QueryString["str"].ToString();
                if (context.Request.QueryString["StorageID"] != null)
                {
                     StorageID = context.Request.QueryString["StorageID"].ToString();
                     if ("0".Equals(StorageID)) StorageID = "";
                }
                if (context.Request.QueryString["DeptID"] != null)
                {
                    DeptID = context.Request.QueryString["DeptID"].ToString();
                    if ("0".Equals(StorageID)) StorageID = "";
                }
                if (context.Request.QueryString["SubStor"] != null)
                {
                    BranchID = context.Request.QueryString["SubStor"].ToString();
                }
                else
                {
                    BranchID= ((UserInfoUtil)SessionUtil.Session["UserInfo"]).BranchID.ToString();
                }
                if (!string.IsNullOrEmpty(StorageID))
                {
                    if (StorageID == "NoStorageID")
                    {
                        ArrayList al = XBase.Business.Common.SubStore.GetSubStoreIDListIn(((UserInfoUtil)SessionUtil.Session["UserInfo"]).BranchID.ToString());
                        for (int i = 0; i < al.Count; i++)
                        {
                            strDept = strDept + al[i].ToString() + ",";
                        }
                       BranchID = strDept.TrimEnd(',');
                    }
                }
                string issc = "";
                try
                {
                    issc = context.Request.Form["issc"].ToString();
                }
                catch (Exception)
                {
                    issc = "";
                }
                //dt = ProductInfoBus.GetProductInfoTableByCheckcondition(strid,StorageID,BranchID,DeptID);
                dt = ProductInfoBus.GetProductInfoTableByCheckcondition(strid, issc);
                dt.Columns.Add("TotalPrice");
                dt.Columns.Add("FOBPrice");
                dt.Columns.Add("CIFPrice");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable dt1 = XBase.Business.Office.SellManager.CostForeignBus.GetCostForeignInfo(dt.Rows[i]["ID"].ToString());
                    if (dt1.Rows.Count > 0)
                    {
                        dt.Rows[i]["TotalPrice"] = dt1.Rows[0]["TotalPrice"];
                        dt.Rows[i]["FOBPrice"] = dt1.Rows[0]["FOBPrice"];
                        dt.Rows[i]["CIFPrice"] = dt1.Rows[0]["CIFPrice"];
                    }
                    else
                    {
                        dt.Rows[i]["TotalPrice"] = "0.00";
                        dt.Rows[i]["FOBPrice"] = "0.00";
                        dt.Rows[i]["CIFPrice"] = "0.00";
                    }
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
              
                sb.Append("data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            catch (Exception ex)
            {

            }
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}