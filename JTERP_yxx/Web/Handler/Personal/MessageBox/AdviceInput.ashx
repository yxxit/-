<%@ WebHandler Language="C#" Class="AdviceInput" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class AdviceInput : BaseHandler
{
    protected override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "getitem":
                GetItem();//读取记录
                break;
            case "delitem":
                DelItem();//删除记录
                break;
            case "loaddata":
                LoadData();
                break;        
            default:
                DefaultAction(action);
                break;
        }
    }

    private void GetItem()
    {
        string id = GetParam("id");
        if (id == string.Empty)
        {
            OutputResult(false, "id未指定");
            return;
        }

        XBase.Business.Personal.MessageBox.PersonalAdviceInput bll = new XBase.Business.Personal.MessageBox.PersonalAdviceInput();
        XBase.Model.Personal.MessageBox.PersonalAdviceInput entity = bll.GetModel(int.Parse(id));

        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
         XBase.Model.Office.HumanManager.EmployeeInfoModel entitys = XBase.Business.Office.HumanManager.EmployeeInfoBus.GetEmployeeInfoWithID(entity.FromUserID);
         string uname = "";
         if (entitys != null)
         {
              uname = entitys.EmployeeName;
         }
         else {
             uname = "";
         }
        
        StringBuilder sb = new StringBuilder();
        sb.Append("{");

        sb.Append("ID:" + entity.ID.ToString());
        sb.Append(",Title:\"" + GetSafeJSONString(entity.Title) + "\"");
        sb.Append(",FromUserID:\"" + uname + "\"");
        sb.Append(",AdviceType:\"" + entity.AdviceType + "\"");
        sb.Append(",CreateDate:\"" + entity.CreateDate.ToString() + "\"");
        sb.Append(",Content:\"" + GetSafeJSONString(entity.Content) + "\"");
        sb.Append(",DisplayName:\"" +entity.DisplayName + "\"");
        sb.Append("}");

        OutputData(sb.ToString());
    }

    private void DelItem()
    {
        string idList = GetParam("idList");
        if (idList == string.Empty)
        {
            OutputResult(false, "idList未指定");
            return;
        }

        XBase.Business.Personal.MessageBox.PersonalAdviceInput bll = new XBase.Business.Personal.MessageBox.PersonalAdviceInput();

        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            bll.Delete(int.Parse(ids[i]));
        }

        OutputResult(true, "操作成功");
    }

    private void LoadData()
    {
        string condition = GetParam("Condition");
        if (condition == string.Empty)
            condition = "1=1";
        string pageIndex = GetParam("PageIndex");
        if (pageIndex == string.Empty)
            pageIndex = "1";
        string pageSize = GetParam("PageSize");
        if (pageSize == string.Empty)
            pageSize = "10";
        string orderExp = GetParam("OrderExp");
        if (orderExp == string.Empty)
            orderExp = "ID DESC";

        string fields = GetParam("Fields");
        if (fields == string.Empty)
        {
            fields = "[ID]";
        }

        condition += " AND [DoUserID]='" + UserInfo.EmployeeID.ToString() + "'";

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.MessageBox.PersonalAdviceInput().GetPageData(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));

        //foreach (DataRow row in dataList.Rows)
        //{
        //    string tt = row["Content"].ToString();
        //    if (tt.Length > 16)
        //    {
        //        tt = tt.Substring(0, 16);
        //        row["Content"] = tt;
        //    }
        //}

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());

    }
}