<%@ WebHandler Language="C#" Class="SendInfo" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class SendInfo : BaseHandler
{

    private static XBase.Business.Personal.MessageBox.GetUserDepList bll = new XBase.Business.Personal.MessageBox.GetUserDepList();
    private static XBase.Business.Personal.MessageBox.MyContact bll2 = new XBase.Business.Personal.MessageBox.MyContact();
    private static XBase.Business.Personal.MessageBox.MyContactGroup bll3 = new XBase.Business.Personal.MessageBox.MyContactGroup();
    protected override void ActionHandler(string action)
    {
        /*
          var action="LoadFlowMyApply";
           var action="LoadFlowMyProcess";
           LoadFlowWaitProcess
         */
        switch (action.ToLower())
        {
            case "sendinfo":
                SendBatInfo();
                break;
            case "loaduserlist":
                LoadUserList();
                break;
            case "loaduserlistwithdepartment":
                LoadUserListWithDepartment();
                break;
            case "loaduserlistwithgroup":
                LoadUserListWithGroup();
                break;
            case "loaddepartmentlist":
                LoadDepartmentList();
                break;
            case "sendtalkinfo":
                SendTalkInfo();
                break;
            default:
                DefaultAction(action);
                break;
        }
    }
    private void SendTalkInfo()
     {
         string telphone=GetParam("telphone");
         if (telphone == string.Empty)
         {
             OutputResult(false, "未指定手机号");
             return;
         }
         string name = GetParam("name");
        
         string content = GetParam("content");

         XBase.Common.SMSender.SendBatch(telphone, name+"您好!" + content + " 短信源于:" + UserInfo.EmployeeName);
         OutputResult(true, "发送成功");
    }
    private void LoadDepartmentList()
    {
        depts = bll.GetDeptInfo(UserInfo.CompanyCD);
        //SuperDeptID ,ID

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = depts.Select("SuperDeptID IS NULL");

        sb.Append("[");
        foreach (DataRow row in rows)
        {
            LoadSubDept2(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");       
        
        
    }

    private void LoadSubDept2(DataRow p, StringBuilder sb)
    {
        //do self
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        sb.Append("{");
        sb.Append("text:\"" + p["DeptName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",SubNodes:[");
        //do users of this dept    
        DataRow[] rows = depts.Select("SuperDeptID=" + p["ID"].ToString());
        foreach (DataRow row in rows)
        {
            LoadSubDept2(row, sb);
        }

        sb.Append("]}");
    }

    private static System.Text.RegularExpressions.Regex mobileNo = new System.Text.RegularExpressions.Regex(@"^\d{7,12}$", System.Text.RegularExpressions.RegexOptions.Compiled);
    
    private void SendBatSm(string idlist,string content)
    {

      //DataTable userInfos =   XBase.Business.Office.HumanManager.EmployeeInfoBus.GetContact(idlist);

      //DataSet ds = new XBase.Business.KnowledgeCenter.MyKeyWord().GetCompanyOpenServ(UserInfo.CompanyCD);
      //int reCount = int.Parse(ds.Tables[0].Rows[0]["ManMsgNum"].ToString()) ;
        
      //  //循环发送 
      //  string[] ids = idlist.Split(',');

      //  if (ids.Length > reCount)
      //  {
      //      OutputResult(false, "超过可以发送数量限制");
      //      return;
      //  }
        
      //  string phoneList = "";
      //  XBase.Model.Personal.MessageBox.MobileMsgMonitor entity;
      //  XBase.Business.Personal.MessageBox.MobileMsgMonitor bll = new XBase.Business.Personal.MessageBox.MobileMsgMonitor();
      //  for (int i = 0; i < ids.Length; i++)
      //  {
      //      DataRow[] urows = userInfos.Select("ID="+ids[i]);
      //      if (urows.Length == 0)
      //          continue;
      //      if (urows[0]["Mobile"].ToString().Trim() + "" == "")
      //      {
      //          continue;
      //      }
      //      if( !mobileNo.IsMatch( urows[0]["Mobile"].ToString().Trim()) )
      //      {
      //          continue;
      //      }
            
      //      entity = new XBase.Model.Personal.MessageBox.MobileMsgMonitor();
      //      entity.CompanyCD = UserInfo.CompanyCD;
      //      entity.Content = content;
      //      entity.CreateDate = DateTime.Now;
      //      entity.ReceiveMobile = urows[0]["Mobile"].ToString().Trim();
      //      entity.ReceiveUserID = int.Parse(ids[i]);
      //      entity.ReceiveUserName = urows[0]["EmployeeName"].ToString();
      //      entity.SendDate = DateTime.Now;
      //      entity.SendUserID = UserInfo.EmployeeID;
      //      entity.SendUserName = UserInfo.EmployeeName;
      //      entity.Status = "1";
      //      entity.MsgType = "0";

      //      bll.Add(entity);

      //      if (phoneList != "")
      //          phoneList += ",";
      //      phoneList += entity.ReceiveMobile;            
      //  }


      //  reCount -= phoneList.Split(',').Length;
        
      //  //
      //  XBase.Business.SystemManager.CompanyOpenServBus.UpdateCompanyManMsgNum(UserInfo.CompanyCD, reCount);

      //  if (phoneList == "")
      //  {
      //      OutputResult(true, "发送成功");
      //      return;
      //  }
      //  XBase.Common.SMSender.SendBatch(phoneList, content+" from:"+UserInfo.EmployeeName);
      //  OutputResult(true, "发送成功");
    }

    private void SendBatInfo()
    {
        string idList = GetParam("IDList");
        if (idList == string.Empty)
        {
            OutputResult(false, "未指定IDList");
            return;
        }

        string title = GetParam("title");
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

        string smFlag = GetParam("smFlag");
        if (smFlag == "1")
        {
            SendBatSm(idList, title +":"+ content);
            return;
        }
        if (smFlag == "3")
        {
            SendBatSm(idList, title + ":" + content);
            //return;
        }
        //smFlag == "2"

        //Entity
        XBase.Model.Personal.MessageBox.MessageSendBox entitySend = new XBase.Model.Personal.MessageBox.MessageSendBox();
        XBase.Model.Personal.MessageBox.MessageInputBox entityInput = new XBase.Model.Personal.MessageBox.MessageInputBox();     
        
        //Bll
        XBase.Business.Personal.MessageBox.MessageSendBox bllSend = new XBase.Business.Personal.MessageBox.MessageSendBox();
        XBase.Business.Personal.MessageBox.MessageInputBox bllInput = new XBase.Business.Personal.MessageBox.MessageInputBox();

        //循环发送 
        string[] ids = idList.Split(',');
        for (int i = 0; i < ids.Length; i++)
        {
            #region entitySend
            entitySend.CompanyCD = UserInfo.CompanyCD;
            entitySend.Content = content;
            entitySend.Title = title;            
            
            entitySend.SendUserID = UserInfo.EmployeeID;
            entitySend.ReceiveUserID = int.Parse(ids[i]);
            entitySend.Status = "0";
            entitySend.ReadDate = DateTime.Now;
            
            entitySend.CreateDate = DateTime.Now;
            entitySend.ModifiedDate = DateTime.Now;
            entitySend.ModifiedUserID = UserInfo.UserID;
            entitySend.MsgURL = "";
            #endregion
            

            int fromID = bllSend.Add(entitySend);//写入发件夹

            #region entityInput
            entityInput.CompanyCD = UserInfo.CompanyCD;
            entityInput.Content = content;
            entityInput.Title = title;

            entityInput.FromID = fromID;
            entityInput.SendUserID = UserInfo.EmployeeID;
            entityInput.ReceiveUserID = int.Parse(ids[i]);
            entityInput.Status = "0";
            entityInput.ReadDate = DateTime.Now;

            entityInput.CreateDate = DateTime.Now;
            entityInput.ModifiedDate = DateTime.Now;
            entityInput.ModifiedUserID = UserInfo.UserID;
            entityInput.MsgURL = "";
            #endregion
            
            bllInput.Add(entityInput);//写入收件夹
        }
        
        if (smFlag != "3")
        {
            OutputResult(true, "发送成功");
        }
        
    }

    private void LoadUserList()
    {
        DataTable users = bll.GetUserInfo(UserInfo.CompanyCD);
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow row in users.Rows)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("isUser:true,");
            sb.Append("text:\"" + row["EmployeesName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",SubNodes:[]}");
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
    }

    private DataTable userlist = new DataTable();
    private DataTable depts = new DataTable();
    private void LoadUserListWithDepartment()
    {
        userlist = bll.GetUserInfo(UserInfo.CompanyCD);        
        depts = bll.GetDeptInfo(UserInfo.CompanyCD);        
        //SuperDeptID ,ID

        StringBuilder sb = new StringBuilder();
        DataRow[] rows = depts.Select("SuperDeptID IS NULL");
        
        sb.Append("[");
        foreach (DataRow row in rows)
        {
            LoadSubDept(row, sb);
        }
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");       
    }

    private void LoadSubDept(DataRow p,StringBuilder sb)
    { 
        //do self
        if (sb[sb.Length - 1] == '}')
            sb.Append(",");

        sb.Append("{");
        sb.Append("text:\"" + p["DeptName"].ToString() + "\"");
        sb.Append(",value:\"" + p["ID"].ToString() + "\"");
        sb.Append(",SubNodes:[");
        
        //do users of this dept
        DataRow[] users = userlist.Select("DeptID="+p["ID"].ToString());
        foreach (DataRow row in users)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("isUser:true,");
            sb.Append("text:\"" + row["EmployeesName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",SubNodes:[]}");
        }
        
        DataRow[] rows = depts.Select("SuperDeptID=" + p["ID"].ToString());
        foreach (DataRow row in rows)
        {
            LoadSubDept(row, sb);
        }

        sb.Append("]}");       
    }

    private void LoadUserListWithGroup()
    {
        DataTable contacts = bll2.GetListEx(UserInfo.CompanyCD,UserInfo.EmployeeID);
        DataTable group = bll3.GetList("creator = "+UserInfo.EmployeeID.ToString());

        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow row in group.Rows)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");
            
            sb.Append("{");
            sb.Append("text:\"" + row["GroupName"].ToString() + "\"");
            sb.Append(",value:\"" + row["ID"].ToString() + "\"");
            sb.Append(",SubNodes:[");
            DataRow[] rows = contacts.Select("GroupID="+row["ID"].ToString());
            foreach (DataRow row2 in rows)
            {
                if (sb[sb.Length - 1] == '}')
                    sb.Append(",");
                
                sb.Append("{");
                sb.Append("isUser:true,");
                sb.Append("text:\"" + row2["EmployeeName"].ToString() + "\"");
                sb.Append(",value2:\"" + row2["ID"].ToString() + "\"");
                sb.Append(",value:\"" + row2["ContactID"].ToString() + "\"");
                sb.Append(",SubNodes:[]}");
            }
            sb.Append("]}");
        }

        //未分组联系人
        DataRow[] rows2 = contacts.Select("GroupID=0");
        foreach (DataRow row in rows2)
        {
            if (sb[sb.Length - 1] == '}')
                sb.Append(",");

            sb.Append("{");
            sb.Append("text:\"" + row["EmployeeName"].ToString() + "\"");
            sb.Append(",value2:\"" + row["ID"].ToString() + "\"");
            sb.Append(",value:\"" + row["ContactID"].ToString() + "\"");
            sb.Append(",SubNodes:[]}");
        }
        
        
        sb.Append("]");

        Output("{result:true,data:" + sb.ToString() + "}");
        
    }

    
}