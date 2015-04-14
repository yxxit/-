<%@ WebHandler Language="C#" Class="WorkFlow" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.SessionState;
using XBase.Common;

public class WorkFlow :BaseHandler
{

    
    protected override void ActionHandler(string action)
    {


       // UserInfoUtil UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        
        /*
          var action="LoadFlowMyApply";
           var action="LoadFlowMyProcess";
           LoadFlowWaitProcess
         */
      
        switch (action.ToLower())
        {
         
            case "loadflowmyapply":
            case "loadflowmyprocess":
            case "loadflowwaitprocess":
                LoadData();//加载数据
                break;
            case "desktoploaddata":
               loadflowwLoad();
                break;    
                
            default:
                DefaultAction(action);
                break;
        }
    }

    private void LoadData()
    {
       // UserInfoUtil UserInfo = ((UserInfoUtil)SessionUtil.Session["UserInfo"]);
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
            orderExp = "ID ASC";

        string fields = GetParam("Fields");
        if (fields == string.Empty)
        {
            fields = "[ID]";
        }



        DataTable dataList = new DataTable();

        int recCount = 0;

        if (Action.ToLower() == "loadflowmyapply")
        {
            condition += " AND [ModifiedUserID]='" + UserInfo.UserID + "'";
            recCount = XBase.Business.Personal.WorkFlow.WorkFlowBus.GetVflowMyApplyList(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));
        }
        else if (Action.ToLower() == "loadflowmyprocess")
        {
            string ApplyUserID = GetParam("ApplyUser");
            if (!string.IsNullOrEmpty(ApplyUserID))
            {
                condition += " AND  ID IN (select FlowInstanceID from officedba.FlowTaskHistory where operateUserid='" + UserInfo.UserID + "' and EmployeeID in(" + ApplyUserID + "))"; 
            }
            else
            {
                condition += " AND  ID IN (select FlowInstanceID from officedba.FlowTaskHistory where operateUserid='" + UserInfo.UserID + "')";  
            }
            recCount = XBase.Business.Personal.WorkFlow.WorkFlowBus.GetVFlowMyProcessList(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));
        }
        else
        {
            string ApplyUserID = GetParam("ApplyUser");
            if (!string.IsNullOrEmpty(ApplyUserID))
            {
                condition += " AND Actor=" + UserInfo.EmployeeID.ToString() + " and ApplyUserEmpID in("+ApplyUserID+") ";
            }
            else
            {
                condition += " AND Actor=" + UserInfo.EmployeeID.ToString();
            }
            recCount = XBase.Business.Personal.WorkFlow.WorkFlowBus.GetVFlowWaitProcessList(out dataList, condition, fields, orderExp, int.Parse(pageIndex), int.Parse(pageSize));
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("{result:true,data:");
        sb.Append("{count:" + recCount.ToString() + ",");
        sb.Append("list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        Output(sb.ToString());
        
    }

    
    
    
    
    //加载首页
    public void  loadflowwLoad()
    {

        DataTable dataList = new DataTable();
        int recCount = XBase.Business.Personal.WorkFlow.WorkFlowBus.GetDeskTopVFlowWaitProcessList(out dataList,UserInfo.EmployeeID);
   

        StringBuilder sb = new StringBuilder();

        sb.Append("{result:true,data:");
        sb.Append("{list:" + DataTable2Json(dataList) + "}");
        sb.Append("}");
        //OutputResult(true, sb.ToString());
        Output(sb.ToString());
        



    }
    
    

}