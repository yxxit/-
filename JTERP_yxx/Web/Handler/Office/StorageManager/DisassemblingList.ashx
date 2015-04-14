<%@ WebHandler Language="C#" Class="DisassemblingList" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;

public class DisassemblingList : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string action = context.Request.QueryString["action"].ToString();
            if (action == "getlist")
            {
                string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                //设置行为参数
                string orderString = (context.Request.QueryString["orderby"].ToString());//排序
                string order = "desc";//排序：升序
                string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "ID";//要排序的字段，如果为空，默认为"ID"
                if (orderString.EndsWith("_a"))
                {
                    order = "asc";//排序：降序
                }
                int pageCount = int.Parse(context.Request.QueryString["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.QueryString["pageIndex"].ToString());//当前页
                int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数

                //获取数据
                DisassemblingModel model = new DisassemblingModel();
                model.companyCD = companyCD;
                string EnterDateStart = string.Empty;
                string EnterDateEnd = string.Empty;
                model.BillNo = context.Request.QueryString["InNo"].Trim();

                if (context.Request.QueryString["txtDeptID"].Trim() != "undefined" && context.Request.QueryString["txtDeptID"].Trim() != "")
                {
                    model.departmentID = int.Parse(context.Request.QueryString["txtDeptID"].ToString());
                }
                if (context.Request.QueryString["BomID"].Trim() != "undefined" && context.Request.QueryString["BomID"].Trim() != "")
                {
                    model.BomID = int.Parse(context.Request.QueryString["BomID"].ToString());
                }
                model.status = int.Parse(context.Request.QueryString["BillStatus"].ToString());
                if (context.Request.QueryString["txtExecutorID"].Trim() != "undefined" && context.Request.QueryString["txtExecutorID"].Trim() != "")
                {
                    model.HandsManID = int.Parse(context.Request.QueryString["txtExecutorID"].ToString());
                }
                EnterDateStart = context.Request.QueryString["createDate1"].ToString();
                EnterDateEnd = context.Request.QueryString["createDate2"].ToString();
                model.BillType = int.Parse(context.Request.QueryString["billtype"].ToString());

                string ord = orderBy + " " + order;
                int TotalCount = 0;

                DataTable dt = DisassemblingListBus.GetStorageInOtherTableBycondition(model, EnterDateStart, EnterDateEnd, pageIndex, pageCount, ord,false, ref TotalCount);


                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("totalCount:");
                sb.Append(TotalCount.ToString());
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
            else if(action=="Del")
            {
                JsonClass jc = new JsonClass();
                string strID = context.Request.QueryString["strID"].ToString();
                string[] IDArray = null;
                IDArray = strID.Split(',');
                bool ifdele = true;
                int delcout = 0;
                for (int i = 0; i < IDArray.Length; i++)
                {
                    if (!DisassemblingBus.isdel(IDArray[i]))
                    {
                        ifdele = false;
                        break;
                    }
                }

                if (ifdele == true)
                {
                    for (int i = 0; i < IDArray.Length; i++)
                    {

                        if (DisassemblingBus.DeleteIt(IDArray[i]))
                        {
                            delcout += 1;
                        }
                    }
                    if (delcout > 0)
                    {
                        jc = new JsonClass("删除成功", "", 1);
                    }
                    else
                    {
                        jc = new JsonClass("删除失败", "", 0);
                    }
                }
                else
                {
                    jc = new JsonClass("已确认后的单据不允许删除！", "", 0);

                }
                context.Response.Write(jc);
            }

           
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}