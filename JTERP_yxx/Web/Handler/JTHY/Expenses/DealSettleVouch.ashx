<%@ WebHandler Language="C#" Class="DealSettleVouch" %>

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

public class DealSettleVouch : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Params["action"].ToString());//操作

            if (action == "insert")//编辑
            {

                InsertSettleVouch(context);

            }
            else if (action == "DeleteSettleVouch")//删除
            {
                DeleteSettleVouch(context);
            }
            else if (action == "ConfirmSettleVouch")  //确认 
            {
                ConfirmSettleVouch(context);
            }
            else if (action == "CancelConfirmSettleVouch")  //反确认
            {
                CancelConfirmSettleVouch(context);
            }
        }
        
        
    }

    //编辑结算
    private void InsertSettleVouch(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        string SettleCode = context.Request.Params["SettleCode"].ToString().Trim();  //单据编号
        string sel_cBusTtype = context.Request.Params["sel_cBusTtype"].ToString().Trim();  //来源类型
        string SourceBillID = context.Request.Params["SourceBillID"].ToString().Trim();  //来源订单ID
        string SourceBillNo = context.Request.Params["SourceBillNo"].ToString().Trim();  //来源订单编码
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();  //经办人id
        string CustomerID = context.Request.Params["CustomerID"].ToString().Trim();  //客户id
        string S_SettelTotalPrice = context.Request.Params["S_SettelTotalPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["S_SettelTotalPrice"].ToString().Trim();  //销售本次结算
        string ProviderID = context.Request.Params["ProviderID"].ToString().Trim();  //供应商ID
        string P_SettleTotalPrice = context.Request.Params["P_SettleTotalPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["P_SettleTotalPrice"].ToString().Trim();  //采购本次结算
        string Reason = context.Request.Params["Reason"].ToString().Trim();  //备注


        UserInfoUtil UserInfo = (SessionUtil.Session["UserInfo"]) as UserInfoUtil;
        string CompanyCD = UserInfo.CompanyCD;
        string Creator = UserInfo.EmployeeID.ToString();
        string userid = UserInfo.UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();


        ArrayList lstCmd = new ArrayList();
        JsonClass jc;
        
        int ID = int.Parse(tempID);
        SqlCommand commandd = new SqlCommand();
        if (ID > 0)
        {
            //更新
            string strup = "   update   jt_settlevouch set cBusTtype='" + sel_cBusTtype + "',SettleCode='" + SettleCode + "'," +
            "SourceBillID='" + SourceBillID + "',cPersonCode='" + PPersonID + "',CustID='" + CustomerID + "',ProviderID='" + ProviderID
            + "',cMemo='" + Reason + "' ,P_SettleTotalPrice='" + P_SettleTotalPrice + "' ,S_SettelTotalPrice='" + S_SettelTotalPrice + "',ModifiedUserID='" + userid + "',ModifiedDate='" + CreatorDate + "' where ID='" + ID.ToString() + "' ";
            SqlCommand command1 = new SqlCommand(strup);
            lstCmd.Add(command1);

           
            string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
            "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8883','" + ID.ToString() + "','jt_settlevouch','修改','操作成功')";
            //SqlHelper.ExecuteSql(strlog);
            SqlCommand command2 = new SqlCommand(strlog);
            lstCmd.Add(command2);

        }
        else
        {
            string tableName = "dbo.jt_settlevouch";//结算单主表表
            string columnName = "SettleCode";//结算单号

            //判断是否已经存在
            bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, SettleCode);
            if (!ishave)
            {
                jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 0);
                context.Response.Write(jc);
                context.Response.End();
            }

            //插入表头
            string strsql = @" insert into dbo.jt_settlevouch(CompanyCD,cBusTtype,SettleCode,
                SourceBillID,cPersonCode,CustID,ProviderID,cMemo,
                P_SettleTotalPrice,S_SettelTotalPrice,Status,Creator,CreateDate,
                ModifiedUserID,ModifiedDate )
                values('" + CompanyCD + "','" + sel_cBusTtype + "','" + SettleCode + "','" + SourceBillID
            + "','" + PPersonID + "','" + CustomerID + "','" + ProviderID + "','"
            + Reason + "','" + P_SettleTotalPrice + "','" + S_SettelTotalPrice + "','1','"
            + Creator + "','" + CreatorDate + "','" + userid + "','" + CreatorDate + "')   set  @id=@@IDENTITY";

            commandd.CommandText = strsql;
            commandd.Parameters.Add(SqlHelper.GetOutputParameter("@id", SqlDbType.Int));
            lstCmd.Add(commandd);

            string strlog = "  declare   @reportno varchar(20) set @reportno=(select max(ID) AS ID from jt_settlevouch where SettleCode='" + SettleCode + "')" +
            "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8883',@reportno,'jt_settlevouch','新建','操作成功')";
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
                jc = new JsonClass("保存成功", SettleCode, ID);
            }
            else
            {
                jc = new JsonClass("保存成功", SettleCode, int.Parse(tempID));
            }
        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
        }

        context.Response.Write(jc);
        context.Response.End();
    }



    //确认结算单
    private void ConfirmSettleVouch(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //结算单id
        string P_SPrice = context.Request.Params["P_SPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["P_SPrice"].ToString().Trim();  //采购结算金额
        string S_SPrice = context.Request.Params["S_SPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["S_SPrice"].ToString().Trim();  //销售结算金额
        string hidSourceID = context.Request.Params["hidSourceID"].ToString().Trim(); //源单id
        string sourceType = context.Request.Params["sourceType"].ToString();  //来源类型
        
        ArrayList lstCmd = new ArrayList();
        bool isSucc = false;//是否确认成功

        
        DataTable dt = null;
        string searchSql1 = "";
        if (sourceType == "1")  //直销
        {
            searchSql1 = @"select a.id, isnull(b.CustJsFee,0) as CustJsFee,isnull(b.TotalFee,0) as SellMoney, 0 as ProJsFee,
                                0 as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_xsfh b on b.id=a.SourceBillID
                                where b.BusiType='1' and b.id='" + hidSourceID + "' and a.id='" + tempID + "' ";
        }
        else if (sourceType == "2")//采购到货
        {
            searchSql1 = @"select a.id, 0 as CustJsFee,0 as SellMoney, isnull(b.ProJsFee,0) as ProJsFee,
                                isnull(b.TotalFee,0) as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_cgdh b on b.id=a.SourceBillID
                                where  b.id='" + hidSourceID + "' and a.id='" + tempID + "' ";
        }
        else if (sourceType == "3")//采购直销
        {
            searchSql1 = @"select a.id, isnull(b.CustJsFee,0) as CustJsFee,isnull(b.TotalFee,0) as SellMoney, isnull(b.ProJsFee,0) as ProJsFee,
                                isnull(b.SupplyAmount,0) as ProMoney 
                                from dbo.jt_settlevouch a
                                left join dbo.jt_xsfh b on b.id=a.SourceBillID
                                where b.BusiType='2' and b.id='" + hidSourceID + "' and a.id='" + tempID + "' ";
        }
        dt = SqlHelper.ExecuteSql(searchSql1);
        Double CustJsFee = 0.0;
        Double SellMoney = 0.0;
        Double ProJsFee = 0.0;
        Double ProMoney = 0.0;
        if (dt != null && dt.Rows.Count > 0)
        {
            CustJsFee = Convert.ToDouble(dt.Rows[0]["CustJsFee"].ToString());
            SellMoney = Convert.ToDouble(dt.Rows[0]["SellMoney"].ToString());
            ProJsFee = Convert.ToDouble(dt.Rows[0]["ProJsFee"].ToString());
            ProMoney = Convert.ToDouble(dt.Rows[0]["ProMoney"].ToString());
        }

        if (Convert.ToDouble(P_SPrice) > (ProMoney - ProJsFee))
        {
            jc = new JsonClass("采购结算金额不能大于" + (ProMoney - ProJsFee) + ",请重新保存！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }
        else if (Convert.ToDouble(S_SPrice) > (SellMoney - CustJsFee))
        {
            jc = new JsonClass("销售结算金额不能大于" + (SellMoney - CustJsFee) + ",请重新保存！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }
        else 
        {
            string strUpdatefh="";
            if (sourceType == "1")
            {
                strUpdatefh = " update dbo.jt_xsfh set CustJsFee=isnull(CustJsFee,0)+" + S_SPrice +
                            "  where CompanyCD='" + CompanyCD + "' and BusiType='1' and id='" + hidSourceID + "' ";
            }
            else if (sourceType == "2")
            {
                strUpdatefh = @"update dbo.jt_cgdh set ProJsFee=isnull(ProJsFee,0) +"+ P_SPrice+ 
                               " where CompanyCD='" + CompanyCD + "'  and id='" + hidSourceID + "' ";
            }
            else if (sourceType == "3")
            {
                strUpdatefh = @"update dbo.jt_xsfh set CustJsFee=isnull(CustJsFee,0)+" + S_SPrice + ",ProJsFee=isnull(ProJsFee,0)+" + P_SPrice +
                            "  where CompanyCD='" + CompanyCD + "' and BusiType='2' and id='" + hidSourceID + "' ";
            }

            SqlCommand commandUpp = new SqlCommand(strUpdatefh);
            lstCmd.Add(commandUpp);
        }


        string strconfirm = "update dbo.jt_settlevouch  set status='2',confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";


        SqlCommand command3 = new SqlCommand(strconfirm);
        lstCmd.Add(command3);

        //写入日志

        string strEnsurelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                          "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8883','" + tempID + "','jt_settlevouch','确认','操作成功')";
        SqlCommand commandlog = new SqlCommand(strEnsurelog);
        lstCmd.Add(commandlog);

        try
        {
            isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

        }
        catch (Exception e)
        {
            isSucc = false;
        }
        if (isSucc)
        {

            jc = new JsonClass("确认成功", employeeName, int.Parse(employeeid));
            

        }
        else
        {
            jc = new JsonClass("确认失败", "", 0);
        }

        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    //反确认结算单
    private void CancelConfirmSettleVouch(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        string P_SPrice = context.Request.Params["P_SPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["P_SPrice"].ToString().Trim();  //采购结算金额
        string S_SPrice = context.Request.Params["S_SPrice"].ToString().Trim() == "" ? "0" : context.Request.Params["S_SPrice"].ToString().Trim();  //销售结算金额
        string hidSourceID = context.Request.Params["hidSourceID"].ToString().Trim(); //源单id
        string sourceType = context.Request.Params["sourceType"].ToString();  //来源类型

        ArrayList lstCmd = new ArrayList();
        bool isSucc = false;//是否取消确认成功

        string strconfirm = "update dbo.jt_settlevouch  set status='1',confirmor=null,confirmdate=null  where id='" + tempID + "' ";

        SqlCommand command3 = new SqlCommand(strconfirm);
        lstCmd.Add(command3);

        string strUpdatefh = "";
        if (sourceType == "1")
        {
            strUpdatefh = " update dbo.jt_xsfh set CustJsFee=isnull(CustJsFee,0)-" + S_SPrice +
                        "  where CompanyCD='" + CompanyCD + "' and BusiType='1' and id='" + hidSourceID + "' ";
        }
        else if (sourceType == "2")
        {
            strUpdatefh = @"update dbo.jt_cgdh set ProJsFee=isnull(ProJsFee,0) -" + P_SPrice +
                           " where CompanyCD='" + CompanyCD + "'  and id='" + hidSourceID + "' ";
        }
        else if (sourceType == "3")
        {
            strUpdatefh = @"update dbo.jt_xsfh set CustJsFee=isnull(CustJsFee,0)-" + S_SPrice + ",ProJsFee=isnull(ProJsFee,0)-" + P_SPrice +
                        "  where CompanyCD='" + CompanyCD + "' and BusiType='2' and id='" + hidSourceID + "' ";
        }

        SqlCommand commandUpp = new SqlCommand(strUpdatefh);
        lstCmd.Add(commandUpp);

        //写入日志

        string strEnsurelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                          "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8883','" + tempID + "','jt_settlevouch','取消确认','操作成功')";
        SqlCommand commandlog = new SqlCommand(strEnsurelog);
        lstCmd.Add(commandlog);

        try
        {
            isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

        }
        catch (Exception e)
        {
            isSucc = false;
        }
        if (isSucc)
        {

            jc = new JsonClass("取消确认成功", "", int.Parse(tempID));


        }
        else
        {
            jc = new JsonClass("取消确认失败", "", 0);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(jc.ToString());
        context.Response.End();


    }

    /// <summary>
    /// 删除结算单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteSettleVouch(HttpContext context)
    {


        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allSettleVouchId"].ToString().Trim();
        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的结算单 
            string IsHasEnsure = @" select count(*) from dbo.jt_settlevouch   where id in (" + allid + ") and status='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除结算单主表
                string delsql = " delete from dbo.jt_settlevouch  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);



                //记录删除日志
                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8883','" + allid + "','jt_settlevouch','删除','操作成功')";
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