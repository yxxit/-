<%@ WebHandler Language="C#" Class="AdviceSend" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class AdviceSend : BaseHandler
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
            case "additem":
                AddItem();//读取记录
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

        XBase.Business.Personal.MessageBox.PersonalAdviceSend bll = new XBase.Business.Personal.MessageBox.PersonalAdviceSend();
        XBase.Model.Personal.MessageBox.PersonalAdviceSend entity = bll.GetModel(int.Parse(id));

        //System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

         XBase.Model.Office.HumanManager.EmployeeInfoModel entitys = XBase.Business.Office.HumanManager.EmployeeInfoBus.GetEmployeeInfoWithID(entity.DoUserID);
        string uname = "";
        if (entitys != null)
        {
            uname = entitys.EmployeeName;
        }
        else
        {
            uname = "";
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("{");

        sb.Append("ID:" + entity.ID.ToString());
        sb.Append(",Title:\"" + GetSafeJSONString(entity.Title) + "\"");
        sb.Append(",DoUserID:\"" + uname + "\"");
        sb.Append(",AdviceType:\"" + entity.AdviceType + "\"");
        sb.Append(",CreateDate:\"" + entity.CreateDate.ToString() + "\"");
        sb.Append(",Content:\"" + GetSafeJSONString(entity.Content) + "\"");

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

        XBase.Business.Personal.MessageBox.PersonalAdviceSend bll = new XBase.Business.Personal.MessageBox.PersonalAdviceSend();

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

        condition += " AND [FromUserID]='" + UserInfo.EmployeeID.ToString() + "'";

        DataTable dataList = new DataTable();

        int recCount = new XBase.Business.Personal.MessageBox.PersonalAdviceSend().GetPageData(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));

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
    
    
    private void AddItem()
    {
         string idList = GetParam("IDList");
        if (idList == string.Empty)
        {
            OutputResult(false, "未指定IDList");
            return;
        }

        string title = GetParam("title");
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(title);
        //byte[] bytes2 = System.Text.Encoding.Convert(System.Text.Encoding.Default, System.Text.Encoding.UTF8, bytes);
        //title = System.Text.Encoding.Default.GetString(bytes2);
        //string title2 = System.Text.Encoding.Default.GetString(bytes);
        
        if (title == string.Empty)
        {
            OutputResult(false, "未指定title");
            return;
        }

        string content = GetParam("content");
        if (content == string.Empty)
        {
            OutputResult(false, "未指定content");
            return;
        }
        
         string AdviceType = GetParam("AdviceType");
         string DisplayName = GetParam("DisplayName");


         XBase.Model.Personal.MessageBox.PersonalAdviceSend entitySend = new XBase.Model.Personal.MessageBox.PersonalAdviceSend();
         XBase.Business.Personal.MessageBox.PersonalAdviceSend bllSend = new XBase.Business.Personal.MessageBox.PersonalAdviceSend();
         XBase.Business.Personal.MessageBox.PersonalAdviceInput bllInput = new XBase.Business.Personal.MessageBox.PersonalAdviceInput();
         XBase.Model.Personal.MessageBox.PersonalAdviceInput entityInput = new XBase.Model.Personal.MessageBox.PersonalAdviceInput();
        
        
        //循环发送 
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            #region entitySend
            entitySend.CompanyCD = UserInfo.CompanyCD;
            entitySend.Content = content;
            entitySend.Title = title;            
            
            entitySend.FromUserID = UserInfo.EmployeeID;
            entitySend.DoUserID = int.Parse(ids[i]);    
                  
            entitySend.CreateDate = DateTime.Now;
            entitySend.AdviceType = AdviceType;
            entitySend.DisplayName = DisplayName;
                       
            #endregion
            

            int fromID = bllSend.Add(entitySend);//发送表

            #region entityInput
            entityInput.CompanyCD = UserInfo.CompanyCD;
            entityInput.Content = content;
            entityInput.Title = title;
                     
            entityInput.FromUserID = UserInfo.EmployeeID;
            entityInput.DoUserID = int.Parse(ids[i]);
         
            entityInput.CreateDate = DateTime.Now;
            entityInput.AdviceType = AdviceType;
            entityInput.DisplayName = DisplayName;
         
            #endregion
            
            bllInput.Add(entityInput);//写入接收表
        }

        OutputResult(true, "发送成功");
    }

}