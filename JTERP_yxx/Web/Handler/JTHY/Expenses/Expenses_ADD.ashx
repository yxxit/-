<%@ WebHandler Language="C#" Class="Expenses_ADD" %>

using System;
using System.Web;
using XBase.Common;
using System.Data;
using System.Data.SqlClient;
using XBase.Data.DBHelper;
using System.Web.SessionState;

public class Expenses_ADD : IHttpHandler, IRequiresSessionState
{

    bool isSucc = false;
    
    JsonClass jc = new JsonClass();
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";


        if (context.Request.RequestType == "POST")
        {

            string Action = context.Request.Params["action"].ToString().Trim();
            if (Action == "insert")
            {
                InsertData(context);
            }
            if (Action == "update")
            {
                UpdateDate(context);
            }
            if (Action == "confirm")
            {
                ConfirmDate(context);
            }
        }
            
    }


    private void InsertData(HttpContext context)
    {
        
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        
        HttpRequest request = context.Request;
        string ExpCodeType = request.Params["ExpCodeType"].ToString().Trim();
        string ExpCode = request.Params["ExpCode"].ToString().Trim();
        string tableName = "Jt_fysq";//费用申请表
        string columnName = "ExpCode";//单据编号

        if (ExpCodeType != "")  //如果为自动编号，则获取编码
            ExpCode = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ExpCodeType, tableName, columnName);
        else
        {
            //判断是否已经存在
            bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, ExpCode);
            if (!ishave)
            {
                jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                context.Response.Write(jc);
                context.Response.End();
            }
        }
        
        
        //当没有确认时，状态为“1”
        string Status = request.Params["Status"].ToString().Trim();
       
        string Applyor = request.Params["Applyor"].ToString().Trim();
        string DeptID = request.Params["DeptID"].ToString().Trim();
        string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
        string AriseDate = request.Params["AriseDate"].ToString().Trim();
        string ExpType = request.Params["ExpType"].ToString().Trim();
        string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
        string PayType = request.Params["PayType"].ToString().Trim();
        string TransactorID = request.Params["TransactorID"].ToString().Trim()==""?"0":request.Params["TransactorID"].ToString().Trim();
        string Reason = request.Params["Reason"].ToString().Trim();
        string CreateDate = request.Params["CreateDate"].ToString().Trim();
        string Creator = request.Params["Creator"].ToString().Trim();
        string ModifiedDate = request.Params["ModifiedDate"].ToString().Trim();
        string ModifiedUserID = request.Params["ModifiedUserID"].ToString().Trim();
        
        SqlCommand commandInsert = new SqlCommand();

        string sqlInsert = @"insert into jt_fysq
                (CustType,CompanyCD,ExpCode,Applyor,DeptID,TotalAmount,AriseDate,ExpType,CustID,
                    PayType,TransactorID,Reason,CreateDate,Creator,ModifiedDate,ModifiedUserID,Status)
                 values('1','" + CompanyCD + "','" + ExpCode + "'," + Applyor + "," + DeptID +
                     "," + TotalAmount + ",'" + AriseDate + "'," + ExpType + "," + CustID +
                     "," + PayType + "," + TransactorID + ",'" + Reason + "',getdate()"+
                     "," + Creator + ",getdate(),'" + ModifiedUserID + "','" + Status + "')  set @ID=@@identity";


        commandInsert.CommandText = sqlInsert;
        commandInsert.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
        
        int ID = 0;
        try
        {
            isSucc = SqlHelper.ExecuteTransWithCommand(commandInsert);
            ID = (int)commandInsert.Parameters["@ID"].Value;
                
        }
        catch (Exception ex)
        {
            isSucc = false;
        }

        if (isSucc)
        {
           
            jc = new JsonClass("保存成功", ExpCode, ID);
            
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
            
        context.Response.Write(jc);
        context.Response.End();


    }


    private void UpdateDate(HttpContext context)
    {
        
        HttpRequest request = context.Request;
        int ID = int.Parse(request.Params["ID"].ToString().Trim());
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        string ExpCode = request.Params["ExpCode"].ToString().Trim();
        string Applyor = request.Params["Applyor"].ToString().Trim();
        string DeptID = request.Params["DeptID"].ToString().Trim();
        string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
        string AriseDate = request.Params["AriseDate"].ToString().Trim();
        string ExpType = request.Params["ExpType"].ToString().Trim();
        string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
        string PayType = request.Params["PayType"].ToString().Trim();
        string TransactorID = request.Params["TransactorID"].ToString().Trim() == "" ? "0" : request.Params["TransactorID"].ToString().Trim();
        string Reason = request.Params["Reason"].ToString().Trim();
        string Creator = request.Params["Creator"].ToString().Trim();
        string ModifiedDate = request.Params["ModifiedDate"].ToString().Trim();
       

        SqlCommand commandUpdate = new SqlCommand();
        
        
        if (ID > 0)
        {
            
            string sqlUpdate = @"update jt_fysq set Applyor=" + Applyor + ",DeptID=" + DeptID + ",TotalAmount=" + TotalAmount + ",AriseDate='" + AriseDate +
                           "',ExpType=" + ExpType + ", CustID=" + CustID + ",PayType=" + PayType + ",TransactorID=" + TransactorID + ",Reason='" + Reason +
                           "', ModifiedDate=getdate(),ModifiedUserID='" + UserID + "' where ExpCode='" + ExpCode + "' and  id=" + ID;
            commandUpdate.CommandText = sqlUpdate;
            try
            {
                isSucc = SqlHelper.ExecuteTransWithCommand(commandUpdate);
            }
            catch (Exception ex)
            {
                isSucc = false;
            }

            if (isSucc) 
                jc = new JsonClass("保存成功", ExpCode, ID);
            else
                jc = new JsonClass("保存失败", "", 0);

            context.Response.Write(jc);
            context.Response.End();

        }
   
    }


    private void ConfirmDate(HttpContext context)
    {
        int EmployeeID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
        string EmployeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
        string UserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
        HttpRequest request = context.Request;
        int ID = int.Parse(request.Params["ID"].ToString().Trim());
        string Status = request.Params["Status"].ToString().Trim();
        string ExpCode = request.Params["ExpCode"].ToString().Trim();
        string Applyor = request.Params["Applyor"].ToString().Trim();
        string DeptID = request.Params["DeptID"].ToString().Trim();
        string TotalAmount = request.Params["TotalAmount"].ToString().Trim();
        string AriseDate = request.Params["AriseDate"].ToString().Trim();
        string ExpType = request.Params["ExpType"].ToString().Trim();
        string CustID = request.Params["CustID"].ToString().Trim() == "" ? "0" : request.Params["CustID"].ToString().Trim();
        string PayType = request.Params["PayType"].ToString().Trim();
        string TransactorID = request.Params["TransactorID"].ToString().Trim() == "" ? "0" : request.Params["TransactorID"].ToString().Trim();
        string Reason = request.Params["Reason"].ToString().Trim();
        string CreateDate = request.Params["CreateDate"].ToString().Trim();
        string ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd");
        string ModifiedUserID = request.Params["ModifiedUserID"].ToString().Trim();

        SqlCommand commandUpdate = new SqlCommand();
        

        if (ID > 0)
        {

            string sqlUpdate = @"update jt_fysq set Applyor=" + Applyor + ",DeptID=" + DeptID + ",TotalAmount=" + TotalAmount + ",AriseDate='" + AriseDate +
                           "',ExpType=" + ExpType + ", CustID=" + CustID + ",PayType=" + PayType + ",TransactorID=" + TransactorID + ",Reason='" + Reason +
                           "', ModifiedDate=getdate(),ModifiedUserID='" + UserID +
                           "',Status='" + Status + "',ConfirmDate=getDate(),Confirmor=" + EmployeeID + "  where ExpCode='" + ExpCode + "' and  id=" + ID;
            commandUpdate.CommandText = sqlUpdate;
            try
            {
                isSucc = SqlHelper.ExecuteTransWithCommand(commandUpdate);
            }
            catch (Exception ex)
            {
                isSucc = false;
            }

            if (isSucc)
                jc = new JsonClass("确认成功", ExpCode + "," + EmployeeName, ID);
            else
                jc = new JsonClass("确认失败", "", 0);

            context.Response.Write(jc);
            context.Response.End();
        }
    }




    public bool IsReusable {
        get {
            return false;
        }
    }

}