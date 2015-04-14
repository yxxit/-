<%@ WebHandler Language="C#" Class="Group" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class Group : BaseHandler
{
    private static XBase.Business.Personal.MessageBox.MyContact bll2 = new XBase.Business.Personal.MessageBox.MyContact();
    private static XBase.Business.Personal.MessageBox.MyContactGroup bll = new XBase.Business.Personal.MessageBox.MyContactGroup();
    protected override void ActionHandler(string action)
    {
        
        switch (action.ToLower())
        {
            case "addcontact":
                AddContact();
                break;
            case "delcontact":
                DelContact();
                break;
            case "delitem":
                DelItem();//删除记录
                break;
            case "additem":
                AddItem();//添加记录
                break;
            case "edititem":
                EditItem();//修改记录
                break; 
            case "loaddata":
                LoadData();
                break;                  
            default:
                DefaultAction(action);
                break;
        }
    }

    private void LoadData()
    {
        DataTable dt = bll.GetList("Creator = " + UserInfo.EmployeeID.ToString());
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow row in dt.Rows)
        {
            if (sb.ToString().EndsWith("}"))
            {
                sb.Append(",");
            }
            sb.Append("{text:\"" + row["GroupName"].ToString() + "\",value:" + row["ID"].ToString() + "}");
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private void DelItem()
    {
        string ID = GetParam("ID");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }

        int count1 = bll2.GetCount("GroupID=" + ID);   
        if (count1 > 0 )
        {
            OutputResult(false, "该分组内有联系人，不能删除");
            return;
        }

        bll.Delete(int.Parse(ID));

        OutputResult(true, "删除成功");
    }


    private void AddItem()
    {

        string GroupName = GetParam("GroupName");
        if (GroupName == string.Empty)
        {
            OutputResult(false, "未指定GroupName");
            return;
        }

        DataTable dt = new XBase.Business.Personal.MessageBox.MyContactGroup().GetList("GroupName='" + GroupName + "' and  CompanyCD ='"+UserInfo.CompanyCD+"'" );
        if (dt.Rows.Count > 0)
        {
            OutputResult(false, "已经存在相同名称的分组");
            return;
        }

        XBase.Model.Personal.MessageBox.MyContactGroup entity = new XBase.Model.Personal.MessageBox.MyContactGroup();

        entity.CompanyCD = UserInfo.CompanyCD;
        entity.CreateDate = DateTime.Now;
        entity.Creator = UserInfo.EmployeeID;
        entity.GroupName = GroupName;
        entity.ModifiedDate = DateTime.Now;
        entity.ModifiedUserID = UserInfo.UserID;

        bll.Add(entity);

        OutputResult(true, "添加成功");
    }

    private void EditItem()
    {
        string ID = GetParam("ID");
        if (ID == string.Empty)
        {
            OutputResult(false, "未指定ID");
            return;
        }

        string GroupName = GetParam("GroupName");
        if (GroupName == string.Empty)
        {
            OutputResult(false, "未指定GroupName");
            return;
        }

        DataTable dt = new XBase.Business.Personal.MessageBox.MyContactGroup().GetList("GroupName='" + GroupName + "' AND id<>"+ID);
        if (dt.Rows.Count > 0)
        {
            OutputResult(false, "已经存在相同名称的分组");
            return;
        }

        XBase.Model.Personal.MessageBox.MyContactGroup  entity = bll.GetModel(int.Parse(ID));
        entity.GroupName = GroupName;
        entity.ModifiedDate = DateTime.Now;
        entity.ModifiedUserID = UserInfo.UserID;
        
        bll.Update(entity);

        OutputResult(true, "修改成功");
    }

    private void AddContact()
    {
        string GroupID = GetParam("GroupID");
        if(GroupID == string.Empty)
        {
            OutputResult(false, "未指定GroupID");
            return;
        }

        string idList = GetParam("IDList");
        if (idList == string.Empty)
        {
            OutputResult(false, "未指定IDList");
            return;
        }

        DataTable dt = new XBase.Business.Personal.MessageBox.MyContact().GetList("GroupID=" + GroupID + " AND ContactID IN(" + idList + ")");
        if (dt.Rows.Count > 0)
        {
            //StringBuilder sb = new StringBuilder();
            //foreach (DataRow row in dt.Rows)
            //{
            //    sb.AppendLine("");
            //}
            
            OutputResult(false, "本次要添加的联系人中含有已经存在的联系人请重新选择");
            return;
        }

        XBase.Model.Personal.MessageBox.MyContact entity = new XBase.Model.Personal.MessageBox.MyContact();
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {  
            entity.ContactID = int.Parse(ids[i]);
            entity.GroupID = int.Parse(GroupID);

            entity.CompanyCD = UserInfo.CompanyCD;
            entity.CreateDate = DateTime.Now;
            entity.Creator = UserInfo.EmployeeID;
            entity.ModifiedDate = DateTime.Now;
            entity.ModifiedUserID = UserInfo.UserID;

            bll2.Add(entity);
        }

        OutputResult(true, "添加成功");
        
        
    }

    private void DelContact()
    {
        string idList = GetParam("IDList");
        if (idList == string.Empty)
        {
            OutputResult(false, "未指定IDList");
            return;
        }

        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            bll2.Delete(int.Parse(ids[i]));
        }
        
        OutputResult(true, "删除成功");
        
    }
    

}