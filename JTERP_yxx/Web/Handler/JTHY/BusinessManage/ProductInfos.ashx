<%@ WebHandler Language="C#" Class="ProductInfos" %>


using System;
using System.Web;
using System.Web.SessionState;
using XBase.Common;
using XBase.Data.DBHelper;
using System.Data;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;

public class ProductInfos : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
      

        HttpRequest request = context.Request;
      

        if (context.Request.RequestType == "POST")
        {
            
            string Action = context.Request.Params["Action"];

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            StringBuilder sb = new StringBuilder();
            
            if (Action == "SearchProduct")  //自定义控件UserControl/ProductInfoSelect
            {
                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件

                string productNo = context.Request.Form["UproductNo"].ToString().Trim(); //商品编号
                string prodcutName = context.Request.Form["UproductName"].ToString().Trim(); //商品名称
                string UcProductShort = context.Request.Form["UcProductShort"].ToString().Trim(); //拼音
                int TotalCount = 0;



                DataTable dt = new DataTable();

                string strQuery = "  select a.id,a.prodNo,a.PYShort,a.ProductName,c.CodeName as codeName,a.Specification,a.Size " +
              "  from  officedba.ProductInfo a  left join officedba.CodeUnitType c on c.ID=a.UnitID " +
                " where a.CompanyCD = '" + CompanyCD + "'";


                if (productNo != "")
                    strQuery += " and a.prodNo like '%" + productNo + "%'";
                if (prodcutName != "")
                    strQuery += " and a.ProductName like '%" + prodcutName + "%'";
                if (UcProductShort != "")
                    strQuery += " and a.PYShort like '%" + UcProductShort + "%'";



                // dt = SqlHelper.ExecuteSql(strQuery);
                // TotalCount = dt.Rows.Count;
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                string orderBy = " id asc  ";
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);
                

                if (dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
                

            }
            else  if (Action == "getprodcutinfoByIds")   //采购合同添加明细
            {

                int TotalCount = 0;
                string ids = context.Request.Form["str"].ToString().Trim(); //商品编号



                DataTable dt = new DataTable();

                string strQuery = @"  select a.id,a.prodNo,a.ProductName,a.UnitID,c.CodeName as UnitName,a.StandardBuy
                                from  officedba.ProductInfo a  left join officedba.CodeUnitType c on c.ID=a.UnitID 
                                 where a.CompanyCD = '" + CompanyCD + "' and a.id in (" + ids + ") ";


                



                dt = SqlHelper.ExecuteSql(strQuery);
                TotalCount = dt.Rows.Count;
                


                if (dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
            }


            context.Response.Write(sb.ToString());
            context.Response.End();

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}