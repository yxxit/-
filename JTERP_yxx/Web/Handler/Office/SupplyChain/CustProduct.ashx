<%@ WebHandler Language="C#" Class="CustProduct" %>

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
using XBase.Business.Common;
public class CustProduct : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    CustProductModel model = new CustProductModel();
    JsonClass jc;
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.RequestType == "POST")
        {
            //设置行为参数
            string ActionFlag = context.Request.Form["ActionFlag"].ToString();
            string id = context.Request.QueryString["str"].ToString();

            switch (ActionFlag)
            {
                case "Search":
                    GetLsit(context);//加载数据
                    break;
                case "Add":
                    AddCustProduct(context);//添加记录
                    break;
                case "Del":
                    DelItem(context, id);//删除记录
                    break;
                case "Edit":
                    EditItem(context);//修改记录
                    break;
                case "GetProd":
                    GetProd(context);//修改记录
                    break;
            }

        }
    }
    private void GetLsit(HttpContext context)
    {
        //设置行为参数
        model.CustName = context.Request.Form["CustName"].ToString();
        model.ProdName = context.Request.Form["ProdName"].ToString();
        model.ProdAlias = context.Request.Form["ProdAlias"].ToString();
        if (context.Request.Form["ProdPrice"].ToString() != "")
        {
            model.ProdPrice = Int32.Parse(context.Request.Form["ProdPrice"].ToString());
        }
        else
        {
            model.ProdPrice = -1;
        }
        if (context.Request.Form["IsStop"].ToString() != "")
        {
            model.IsStop = Int32.Parse(context.Request.Form["IsStop"].ToString());
        }
        else
        {
            model.IsStop = -1;
        }
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string orderString = (context.Request.Form["orderby"].ToString());//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
        int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;
        DataTable dt = CustProductBus.GetDataTable(model, pageIndex, pageCount, ord, ref totalCount);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }


    private void GetProd(HttpContext context)
    {
        //设置行为参数
        string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string CustID = context.Request.Form["CustID"].ToString();
        if (CustID == "undefined")
            CustID = "0";
        string ProdNo = context.Request.Form["ProdNo"].ToString();
        int totalCount = 0;
        DataTable dt = CustProductBus.GetDataTableByCustProduct(companyCD, CustID, ProdNo);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("totalCount:");
        sb.Append(totalCount.ToString());
        sb.Append(",data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"ID\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");

        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }




    private void AddCustProduct(HttpContext context)
    {
       
        model.CustID =int.Parse(context.Request.Form["CustID"].ToString());
        model.ProdNo = context.Request.Form["ProdNo"].ToString();
        model.ProdAlias = context.Request.Form["ProdAlias"].ToString();

        model.ProdPrice = Convert.ToDecimal(context.Request.Form["ProdPrice"].ToString());
        model.IsStop = int.Parse(context.Request.Form["UseStatus"].ToString());       
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.Creator= ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        
        model.CreateDate = System.DateTime.Now;
        //判断是否存在
        bool isAlready = CustProductBus.CheckCustProduct(model);
            //存在的场合
            if (!isAlready)
            {
                jc = new JsonClass("已设置该客户与物品对照信息，请重新输入", "", 0);
                context.Response.Write(jc);
                return;
            }
            else
            {
                
                bool result_unit = CustProductBus.InsertCustProduct(model);
                {
                    if (result_unit)
                    {
                        jc = new JsonClass("保存成功！", "", 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败！", "", 0);
                    }
                    context.Response.Write(jc);
                    return;
                }
            }
    }


    private void EditItem(HttpContext context)
    {
        model.CustID = int.Parse(context.Request.Form["CustID"].ToString());
        model.ProdNo = context.Request.Form["ProdNo"].ToString();
        model.ProdAlias = context.Request.Form["ProdAlias"].ToString();

        model.ProdPrice = Convert.ToDecimal(context.Request.Form["ProdPrice"].ToString());
        model.IsStop = int.Parse(context.Request.Form["UseStatus"].ToString());
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        model.ID = int.Parse(context.Request.Form["ID"].ToString());
       
                bool result = CustProductBus.UpdateCustProduct(model);
                {
                    if (result)
                    {
                        jc = new JsonClass("保存成功！", "", 1);
                    }
                    else
                    {
                        jc = new JsonClass("保存失败！", "", 0);
                    }
                    context.Response.Write(jc);
                    return;
                }
           
        }       

    /// <summary>
    /// 删除对照信息
    /// </summary>
    private void DelItem(HttpContext context, string id)
    {
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        
        JsonClass jc;
        bool isDelete = CustProductBus.DeleteCustProduct(id);
        //删除成功时
        if (isDelete)
        {
            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败！", "", 0);
        }
        context.Response.Write(jc);
        return;
    }
    /// <summary>
    /// datatabletoxml
    /// </summary>
    /// <param name="xmlDS"></param>
    /// <returns></returns>
    private XElement ConvertDataTableToXML(DataTable xmlDS)
    {
        StringWriter sr = new StringWriter();
        xmlDS.TableName = "Data";
        xmlDS.WriteXml(sr, System.Data.XmlWriteMode.IgnoreSchema, true);
        string contents = sr.ToString();
        return XElement.Parse(contents);
    }

    public static string ToJSON(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    //数据源结构
    public class DataSourceModel
    {
        public string CodeName { get; set; }
        public string FeeSubjectsNo { get; set; }
        public string SubjectsName { get; set; }
        public string Flag { get; set; }
        public string Description { get; set; }
        public string UsedStatus { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedUserID { get; set; }
        public string ID { get; set; }
        public string Publicflag { get;set; }
        public string CodeSymbol { get; set; }
    }

}