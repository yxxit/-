<%@ WebHandler Language="C#" Class="DealContract" %>

using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Linq;
using XBase.Model.Office.SellManager;
using XBase.Common;
using XBase.Business.Office.ContractManage;
using System.Collections.Generic;
using XBase.Data.DBHelper;
using System.Data.SqlClient;
public class DealContract : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
        static string QualArea = "";
        public void ProcessRequest (HttpContext context){
            
            if (context.Request.RequestType == "POST")
            {
                string action = (context.Request.Params["action"].ToString());//操作
                           
                if (action == "insert")//编辑
                {
                     
                    InsertSellOrder(context);
                                        
                }
                else if (action == "DeleteQTestInfo")//删除
                {
                    DeleteQTestInfo(context); 
                }
                else if (action == "ConfirmQTest")  //确认 
                {
                    ConfirmQTest(context);
                }
                else if (action == "CancelConfirmQTest")  //反确认
                {
                    CancelConfirmQTest(context);
                }
            }
       }
       
   
    
    //编辑质检单
    private void InsertSellOrder(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        string jcQual = "true";

        string QTestType = context.Request.Params["QTestType"].ToString().Trim();  //编号类型
        string SourceBillID = context.Request.Params["SourceBillID"].ToString().Trim();
        string SourceBillNo = context.Request.Params["SourceBillNo"].ToString().Trim();
       // string ProviderID = context.Request.Params["ProviderID"].ToString().Trim();
     ////   string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
     //   string TranSportNo = context.Request.Params["TranSportNo"].ToString().Trim();
    //    string CoalID = context.Request.Params["CoalID"].ToString().Trim();
      //  string Quantity = context.Request.Params["Quantity"].ToString().Trim();
        string QTestBillNo = context.Request.Params["QTestBillNo"].ToString().Trim();
        string CheckDate = context.Request.Params["CheckDate"].ToString().Trim();
        string CheckType = context.Request.Params["CheckType"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string Description = context.Request.Params["Description"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
     
                    
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate=DateTime.Now.ToShortDateString();
        
                   
        ArrayList lstCmd = new ArrayList();
        JsonClass jc;
        if (jcQual == "true")
        {
            int ID = int.Parse(tempID);
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)
            {
               ////更新
                string strup = "   update   jt_cgzj set ReportNo='" + QTestBillNo + "',FromType='1',FromReportNo='"+SourceBillID+"'," +
                "CheckType='1',CheckMode='" + CheckType + "',CheckResult='" + Description + "',Checker='" + PPersonID + "',ChecDeptId='" + DeptID
                + "',CheckDate='" + CreatorDate + "' ,Remark='" + Remark + "',ModifiedUserID='" + userid + "',ModifiedDate='" + CreatorDate + "' where ID='" + ID.ToString() + "' ";
                SqlCommand command1 = new SqlCommand(strup);
                //SqlHelper.ExecuteSql(strup);
                lstCmd.Add(command1);
               
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = "   delete    from  jt_cgzj_mx  where ReportNo='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);


                for (int i = 1; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string CarNo = inseritems[0].ToString();
                        string CheckItem = inseritems[1].ToString();
                        string CheckTarget = inseritems[2].ToString();
                        string CheckValue = inseritems[3].ToString();

                        string strsql = "  " +
                        "  insert into jt_cgzj_mx(CompanyCD,SortNo,ReportNo,chexiangNo,CheckItem,CheckStandard,CheckValue)" +
                        "  values('" + CompanyCD + "','" + i.ToString() + "','" + ID.ToString() + "','" + CarNo + "','" + CheckItem + "','"
                        + CheckTarget + "','" + CheckValue +  "')";
                       
                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);

                    }
                }



                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77711','" + ID.ToString() + "','jt_cgzj，jt_cgzj_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
                
                
                                      
                
            }
            else
            {
                string tableName = "jt_cgzj";//费用申请表
                string columnName = "ReportNo";//单据编号

                if (QTestType != "")  //如果为自动编号，则获取编码
                    QTestBillNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(QTestType, tableName, columnName);
                else
                {
                    //判断是否已经存在
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, QTestBillNo);
                    if (!ishave)
                    {
                        jc = new JsonClass("该编号已被使用，请输入未使用的编号！","", 0);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
                
                                          
                //插入表头
                
                 
                
                
                
                string strsql = " insert into   jt_cgzj(CompanyCD,ReportNo,FromType,FromReportNo,CheckType,CheckMode," +
                "Checker,CheckResult,ChecDeptId,CheckDate,Remark,Creator,CreateDate,BillStatus,ModifiedDate,ModifiedUserID)" +
                "   values('" + CompanyCD + "','" + QTestBillNo + "','1','" + SourceBillID + "','1','" + CheckType
                + "','" + PPersonID + "','" + Description + "','" + DeptID + "','" + CreatorDate + "','" + Remark + "','" + Creator + "','" + CreatorDate + "','1','" + CreatorDate + "','" + userid + "')   set  @id=@@IDENTITY";

                commandd.CommandText=strsql;
                commandd.Parameters.Add(SqlHelper.GetOutputParameter("@id", SqlDbType.Int));
                 lstCmd.Add(commandd);
                
                              
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                for (int i = 1; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string CarNo = inseritems[0].ToString();
                        string CheckItem = inseritems[1].ToString();
                        string CheckTarget = inseritems[2].ToString();
                        string CheckValue = inseritems[3].ToString();


                        strsql = "  declare   @reportno varchar(20) set @reportno=(select max(ID) AS ID from jt_cgzj where reportno='" + QTestBillNo + "')" +

                        "   insert into jt_cgzj_mx(SortNo,ReportNo,CompanyCD,chexiangNo,CheckItem,CheckStandard,CheckValue)" +
                        "  values('" + i.ToString() + "',@reportno,'" + CompanyCD + "','" + CarNo + "','" + CheckItem + "','"
                        + CheckTarget + "','" + CheckValue + "')";
                        SqlCommand command = new SqlCommand(strsql);                       
                        lstCmd.Add(command);
                        
                    }
                }




                string strlog = "  declare   @reportno varchar(20) set @reportno=(select max(ID) AS ID from jt_cgzj where reportno='" + QTestBillNo + "')" +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77711',@reportno,'jt_cgzj，jt_cgzj_mx','新建','操作成功')";
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
                

            }

            try
            {
                if (ID > 0)
                {
                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);
                }
                else
                {
                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);
                    ID = (int)commandd.Parameters["@id"].Value;
                }

                
            }
            catch (Exception ex)
            {
                isSucc = false;
            }

           
            if (isSucc)
            {
                if (ID > 0)
                {
                    jc = new JsonClass("保存成功", QTestBillNo, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", QTestBillNo, int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }
        
        
         context.Response.Write(jc);
    }



    //确认质检单
    private void ConfirmQTest(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //采购质检单id

        bool isSucc = false;//是否确认成功
        try
        {
            string strconfirm = "update dbo.jt_cgzj set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";
            SqlHelper.ExecuteSql(strconfirm);
            isSucc = true;
        }
        catch (Exception ee)
        {
            isSucc = false;
        }
        if (isSucc)
        {

            jc = new JsonClass("确认成功", employeeName, int.Parse(employeeid));
            //写入日志
            string strEnsurelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77711','" + tempID + "','jt_cgzj','确认','操作成功')";
            SqlHelper.ExecuteSql(strEnsurelog);

        }
        else
        {
            jc = new JsonClass("确认失败", "", 0);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    //反确认质检单
    private void CancelConfirmQTest(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        bool isSucc = false;//是否保存成功

//        //先判断质检单是否已经被入库单引用
//        string IsUsed = @"select count(a.id)
//                        from jt_cgrk a 
//                        left join jt_cgzj b on b.FromReportNo=a.FromBillID
//                        where b.id='" + tempID + "' and b.CompanyCD='" + CompanyCD + "' ";
        
//        int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsUsed, null));
//        if (count == 0)  //如果没被引用
//        {
            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update dbo.jt_cgzj set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "' ";
            SqlCommand command1 = new SqlCommand(strconfirm);
            lstCmd.Add(command1);
            //写入日志
            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77711','" + tempID + "','jt_cgzj','取消确认','操作成功')";
            SqlCommand command2 = new SqlCommand(strCanclelog);
            lstCmd.Add(command2);

            try
            {

                isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

            }
            catch (Exception ex)
            {
                isSucc = false;
            }
        //}
        //else
        //{
        //    jc = new JsonClass("该质检单已经被入库单引用，无法取消确认！", "", 0);
        //    context.Response.Write(jc.ToString());
        //    context.Response.End();
        //}

        if (isSucc)
        {

            jc = new JsonClass("取消确认成功", "", int.Parse(tempID));

        }
        else
        {
            jc = new JsonClass("取消确认失败", "", 0);
        }
        context.Response.Write(jc);
        context.Response.End();
        

    }

    /// <summary>
    /// 删除质检单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteQTestInfo(HttpContext context)
    {


        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allQTestID"].ToString().Trim();
        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的采购质检单 
            string IsHasEnsure = @" select count(*) from jt_cgzj where id in (" + allid + ") and BillStatus='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除采购质检主表
                string delsql = " delete from jt_cgzj  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除采购质检明细
                delsql = "delete from jt_cgzj_mx where ReportNo in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);

                //记录删除日志
                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77711','" + allid + "','jt_cgzj,jt_cgzj_mx','删除','操作成功')";
                SqlCommand command2 = new SqlCommand(deleLogSql);
                lstCmd.Add(command2);

                try
                {

                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

                }
                catch (Exception ex)
                {
                    isSucc = false;
                }
            }
            else
            {
                jc = new JsonClass("已经确认单据无法删除，请先取消确认！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
        }
        if (isSucc)
        {

            jc = new JsonClass("删除成功", "", 1);
        }
        else
        {
            jc = new JsonClass("删除失败！", "", 0);
        }

        context.Response.Write(jc.ToString());
        context.Response.End();
    }


     
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}