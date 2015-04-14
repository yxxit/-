<%@ WebHandler Language="C#" Class="FileList" %>

using System;
using System.Web;
using System.Data;
using System.Text;


public class FileList : BaseHandler
{
    private static XBase.Business.Personal.Culture.CultureDocs bll = new XBase.Business.Personal.Culture.CultureDocs();
    protected override void ActionHandler(string action)
    {        
        switch (action.ToLower())
        {
            case "loadalldata":
                LoadAllData();
                break;
            case "delfile":
                DelFile(); ;
                break;
            default:
                DefaultAction(action);
                break;
        }
    }

    private void LoadAllData()
    {
        //设置行为参数
        string orderString = GetParam("orderby");//排序
        string order = "desc";//排序：升序
        string orderBy = (!string.IsNullOrEmpty(orderString)) ? orderString.Substring(0, orderString.Length - 2) : "CreateDate";//要排序的字段，如果为空，默认为"ID"
        if (orderString.EndsWith("_a"))
        {
            order = "asc";//排序：降序
        }
        int pageCount = int.Parse(GetParam("pageSize"));//每页显示记录数
        int pageIndex = int.Parse(GetParam("pageIndex"));//当前页
        int skipRecord = (pageIndex - 1) * pageCount;//跳过记录数
        int totalCount = 0;
        string ord = orderBy + " " + order;

        DataTable dt = GetDataTable(pageIndex, pageCount, ord, ref totalCount);

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + totalCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dt) + "}");
        sb.Append("}");
        Output(sb.ToString());
    }

    private DataTable GetDataTable(int pageIndex, int pageCount, string ord, ref int TotalCount)
    {
        DataTable dt = new DataTable();
        string strTitle = GetParam("Title");
        string time1 = GetParam("createdate1");
        string time2 = GetParam("createdate2");
        string content = GetParam("Content");
        string createname = GetParam("createname");
        string culturetype = GetParam("CultureType");


        dt = bll.GetCultureList(pageIndex, pageCount, ord, ref TotalCount, time1, time2, strTitle, content, createname, culturetype);
        return dt;
    }

    private void DelFile()
    {
        string idList = GetParam("FileIDs");
        if (idList == string.Empty)
        {
            OutputResult(false, "idList未指定");
            return;
        }

        XBase.Business.Personal.Culture.CultureDocs bll = new XBase.Business.Personal.Culture.CultureDocs();
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            bll.Delete(int.Parse(ids[i]));
        }

        OutputResult(true, "操作成功");
        
        
       /* string expID = GetParam["FileIDs"].ToString().Trim();
        expID = expID.Remove(expID.Length - 1, 1);
        StringBuilder sb = new StringBuilder();
        if (bll.DelFileByCheck(expID))
        {
            sb.Append("{result:true,data:");
            sb.Append("}");
            Output(sb.ToString());
        }
        Output(sb.ToString());*/
    }     
}