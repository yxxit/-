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
                string billtype = (context.Request.Params["billtype"].ToString()); //发票类型：1.销售合同 2.采购合同
                           
                if (action == "insert")
                {
                    if (billtype == "1")//编辑销售合同
                    {
                        InsertSellOrder(context);
                    }
                    if (billtype == "2")//编辑采购合同
                    {
                        InsertPurOrder(context);
                    }
                }
                if (action == "DeleteSelContract")//删除销售合同
                {
                     DelSelContract(context); 
                }
                if (action == "DeletePurContract")//删除采购合同---
                {
                     DelPurContract(context); 
                }
                if (action == "ConfirmSell")//确认销售合同
                {
                    ConfirmSell(context);
                }
                if (action == "CancelConfirmSell")//取消确认销售合同
                {
                    CancelConfirmSell(context);
                    
                }
                if (action == "ConfirmPur")//确认采购合同
                {
                    ConfirmPur(context);
                }
                if (action == "CancelConfirmPur")//取消确认采购合同
                {
                    CancelConfirmPur(context);
                }

                if (action == "ClosePur")  //关闭采购合同
                {


                    ClosePur(context);
                }
                if (action == "UnClosePur")  //取消关闭采购合同
                {

                    UnClosePur(context);

                }

                if (action == "ClosePurSeller")  //关闭销售合同
                {
                   
                   ClosePurSeller(context);
                }
                
                
                 if(action=="UnClosePurSeller")//取消关闭销售合同
                 {
                     UnClosePurSeller(context);
                     
                 } 
                
                
            }
       }

    
    
      //确认销售合同
        private void ConfirmSell(HttpContext context)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate=DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功

            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Sale set billstatus=2,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "' ";
            SqlCommand commandDel = new SqlCommand(strconfirm);
            lstCmd.Add(commandDel);

            //记录删除日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + tempID + "','ContractHead_Sale','确认','操作成功')";
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
            

            if (isSucc)
            {

                jc = new JsonClass("确认成功", "销售合同", int.Parse(tempID));

            }
            else
            {
                jc = new JsonClass("确认失败", "销售合同", 0);
            }
            context.Response.Write(jc);
        }
    
       
    
    
    
    
    
      //取消
        private void CancelConfirmSell(HttpContext context)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功

            //先判断销售合同是否已经被销售发货单引用
            string IsHasEnsure = " select count(id) from jt_xsfh where FromBillID='" + tempID + "'  ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  
            {
                ArrayList lstCmd = new ArrayList();
                string strconfirm = "update ContractHead_Sale set billstatus=1,confirmor=null,confirmdate=null where id='" + tempID + "' ";
                SqlCommand commandDel = new SqlCommand(strconfirm);
                lstCmd.Add(commandDel);
                
                //记录日志

                string InsertLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + tempID + "','ContractHead_Sale','取消确认','操作成功')";
                SqlCommand command2 = new SqlCommand(InsertLogSql);
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
                jc = new JsonClass("该销售合同已经被销售发货单引用，无法取消确认！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
                
                
                
                
                

            if (isSucc)
            {
               
                jc = new JsonClass("取消确认成功", "销售合同", int.Parse(tempID));
                
            }
            else
            {
                jc = new JsonClass("取消确认失败", "销售合同", 0);
            }
            context.Response.Write(jc);
        }

       // 关闭销售合同
        private void ClosePurSeller(HttpContext context)
        {

            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功

            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Sale set billstatus=9,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "' ";
            SqlCommand commandDel = new SqlCommand(strconfirm);
            lstCmd.Add(commandDel);

            //记录删除日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + tempID + "','ContractHead_Sale','关闭','操作成功')";
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


            if (isSucc)
            {

                jc = new JsonClass("关闭成功", "销售合同", int.Parse(tempID));

            }
            else
            {
                jc = new JsonClass("关闭失败", "销售合同", 0);
            }
            context.Response.Write(jc);



        }

      //取消关闭销售合同
        private void UnClosePurSeller(HttpContext context)
        {


            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;

            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Sale set billstatus=2,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "' ";
            SqlCommand commandDel = new SqlCommand(strconfirm);
            lstCmd.Add(commandDel);

            //记录删除日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + tempID + "','ContractHead_Sale','取消关闭','操作成功')";
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


            if (isSucc)
            {

                jc = new JsonClass("取消关闭成功", "销售合同", int.Parse(tempID));

            }
            else
            {
                jc = new JsonClass("取消关闭失败", "销售合同", 0);
            }
            context.Response.Write(jc);


        }
    
    
    
    
    
      //采购合同关闭
        private void ClosePur(HttpContext context)
        {
            
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功



            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Pur set billstatus=9,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "'  ";
            SqlCommand command = new SqlCommand(strconfirm);
            lstCmd.Add(command);

            //记录日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + tempID + "','ContractHead_Pur','关闭','操作成功')";
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



            if (isSucc)
            {
                int ID = int.Parse(tempID);
                if (ID > 0)
                {
                    jc = new JsonClass("关闭成功", "采购合同", ID);
                }
                else
                {
                    jc = new JsonClass("关闭成功", "采购合同", int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("关闭成功", "采购合同", 0);
            }
            context.Response.Write(jc);



        }


    
    
       // 取消采购合同关闭
        private void UnClosePur(HttpContext context)
        {
            
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功



            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Pur set billstatus=2,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "'  ";
            SqlCommand command = new SqlCommand(strconfirm);
            lstCmd.Add(command);

            //记录日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + tempID + "','ContractHead_Pur','取消关闭','操作成功')";
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



            if (isSucc)
            {
                int ID = int.Parse(tempID);
                if (ID > 0)
                {
                    jc = new JsonClass("取消关闭成功", "采购合同", ID);
                }
                else
                {
                    jc = new JsonClass("取消关闭成功", "采购合同", int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("取消关闭成功", "采购合同", 0);
            }
            context.Response.Write(jc);

            
        }
    
    
      //采购合同确认
        private void ConfirmPur(HttpContext context)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功



            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update ContractHead_Pur set billstatus=2,confirmor='" + Creator + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "'  ";
            SqlCommand command = new SqlCommand(strconfirm);
            lstCmd.Add(command);

            //记录日志

            string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + tempID + "','ContractHead_Pur','确认','操作成功')";
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
            
            
           
            if (isSucc)
            {
                int ID = int.Parse(tempID);
                if (ID > 0)
                {
                    jc = new JsonClass("确认成功", "采购合同", ID);
                }
                else
                {
                    jc = new JsonClass("确认成功", "采购合同", int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("确认失败", "采购合同", 0);
            }
            context.Response.Write(jc);

        }
    
      
    
    
    
    
    
    
      //取消确认
        private void CancelConfirmPur(HttpContext context)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
            string CreatorDate = DateTime.Now.ToShortDateString();
            JsonClass jc;
            string tempID = "0";
            tempID = context.Request.Params["headid"].ToString().Trim();

            bool isSucc = false;//是否保存成功


            //先判断采购合同是否已经被采购到货单引用
            string IsHasEnsure = " select count(id) from (select id from dbo.jt_xsfh where PurContractID='" + tempID + "' union select id from jt_cgdh where SourceBillID='" + tempID + "') as a  ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)
            {
                ArrayList lstCmd = new ArrayList();
                string strconfirm = "update ContractHead_Pur set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "'";
                SqlCommand command = new SqlCommand(strconfirm);
                lstCmd.Add(command);

                //记录日志

                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + tempID + "','ContractHead_Pur','取消确认','操作成功')";
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
                jc = new JsonClass("该采购合同已经被采购到货单或采购直销单引用，无法取消确认！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
            
            
           
            if (isSucc)
            {
                int ID = int.Parse(tempID);
                if (ID > 0)
                {
                    jc = new JsonClass("取消确认成功", "采购合同", ID);
                }
                else
                {
                    jc = new JsonClass("取消确认成功", "采购合同", int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("取消确认失败", "采购合同", 0);
            }
            context.Response.Write(jc);
        }
    
    //编辑销售合同
    private void InsertSellOrder(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        string jcQual = "true";
        string cVouchType = "1";
        string Contractid = context.Request.Params["ContractID"].ToString().Trim();
        string BillType = "1";
        string cVenCode = "";
        string cCusCode = context.Request.Params["CustomerID"].ToString().Trim();
        string DeliveryAddress = context.Request.Params["DeliveryAddress"].ToString().Trim();
        string SettleType = context.Request.Params["SettleType"].ToString().Trim();
        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        string ContractMoney = context.Request.Params["ContractMoney"].ToString().Trim();
        string SignDate = context.Request.Params["SignDate"].ToString().Trim();
        string EffectiveDate = context.Request.Params["EffectiveDate"].ToString().Trim();
        string EndDate = context.Request.Params["EndDate"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate=DateTime.Now.ToShortDateString();
        //-----------------------------附件---------------------------------//
        string AnnFileName = context.Request.Params["AnnFileName"].ToString().Trim();
        string AnnRemark=context.Request.Params["AnnRemark"].ToString().Trim();
        string UpDateTime=context.Request.Params["UpDateTime"].ToString().Trim();
        string AnnAddr = context.Request.Params["AnnAddr"].ToString().Trim();
        
        
        //------------------------------------------------------------------//
        
        
        
        
       //表体
        ArrayList lstCmd = new ArrayList();
        JsonClass jc;
        if (jcQual == "true")
        {
            int ID = int.Parse(tempID);
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)
            {
               //插入记录都表单审批流管理表，用于提交、确认权限控制
                string strdelflow = "delete from jt_BillFlow where CompanyCD='"+CompanyCD+"' and BillTypeFlag='30' and BillTypeCode='1' and billid='"+ID+"' ";
                SqlCommand commdelflow = new SqlCommand(strdelflow);
                lstCmd.Add(commdelflow);
                string strinflow = "insert into jt_BillFlow(CompanyCD,BillTypeFlag,BillTypeCode,BillID,BillNo,UserID)" +
                " values('"+CompanyCD+"','30','1','"+ID+"','"+Contractid+"','"+userid+"')";
                SqlCommand comminflow = new SqlCommand(strinflow);
                lstCmd.Add(comminflow);
               //更新
                string strup = "update ContractHead_Sale set cVouchType='" + cVouchType + "',Contractid='"+Contractid+"',BillType='"+BillType
                    +"',cVenCode='"+cVenCode+"',cCusCode='"+cCusCode+"'," +
                 "DeliveryAddress='"+DeliveryAddress+"',SettleType='"+SettleType+"',TransPortType='"+TransPortType
                 + "',ContractMoney='" + ContractMoney + "',SignDate='" + SignDate + "',EffectiveDate='" + EffectiveDate + "',EndDate='" + EndDate + "',Remark='" + Remark + "',PPersonID='"+PPersonID+"',DeptID='"+DeptID+"' " +
                 " where id='"+ID.ToString()+"' ";
                SqlCommand command1 = new SqlCommand(strup);
                //SqlHelper.ExecuteSql(strup);
                lstCmd.Add(command1);

                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = " delete   from  ContractDetails_Sale where contractid='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);


                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string coaltype = inseritems[0].ToString();
                        string SpecialName = inseritems[1].ToString();
                        string UnitID = inseritems[2].ToString();
                        string Quantity = inseritems[3].ToString();
                        string UnitCost = inseritems[4].ToString();
                        string Money = inseritems[5].ToString();
                        string strsql = 
                        " insert into ContractDetails_Sale(contractid,cinvccode,ccounitcode,quals,iquantity,iunitcost,imoney) " +
                       "values('"+ID.ToString()+"','" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);

                    }
                }
                
               

                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + ID.ToString() + "','ContractHead_Sale，ContractDetails_Sale','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
                
                //-------------------------附件----------------------------------//
                #region 先删除附件信息
                
                string Delsql=@"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='"+Contractid+"' ) "+
                    "Delete From officedba.Annex where CompanyCD='" + CompanyCD +
                    "' and ParentId=@contractid and ModuleType='" + ConstUtil.MODULE_ID_SELLCONTRANCT_ADD + "'";

                SqlCommand commDel = new SqlCommand(Delsql);
                lstCmd.Add(commDel);
                #endregion

                #region 插入附件
                if (!String.IsNullOrEmpty(AnnFileName))
                {
                    string[] annFileName = AnnFileName.Split(',');
                    string[] annRemark = AnnRemark.Split(',');
                    string[] annUpDateTime = UpDateTime.Split(',');
                    string[] annAddr = AnnAddr.Split(',');
                    if (annFileName.Length >= 1)
                    {
                        for (int i = 0; i < annFileName.Length - 1; i++)
                        {
                            string sqlcmd = @"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='" + Contractid + "' ) " +
                                            "INSERT INTO officedba.Annex (CompanyCD,ParentId,AnnAddr,annFileName,upDatetime,annRemark,ModuleType) " +
                                            "VALUES('" + CompanyCD + "',@contractid,'" + annAddr[i].ToString() + "','" + annFileName[i].ToString() +
                                            "','" + annUpDateTime[i].ToString() + "','" + annRemark[i].ToString() + "','" + ConstUtil.MODULE_ID_SELLCONTRANCT_ADD + "')";

                            SqlCommand comms = new SqlCommand(sqlcmd);
                            lstCmd.Add(comms);
                        }
                    }
                }
                #endregion
                
                
                
                
                //----------------------------------------------------------------//
                
                
            }
            else
            {
                string ContractIDType = context.Request.Params["ContractIDType"].ToString().Trim();

                string tableName = "ContractHead_Sale";//合同表
                string columnName = "Contractid";//合同编号


                if (ContractIDType != "")
                    Contractid = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ContractIDType, tableName, columnName);

                else
                {
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, Contractid);
                    if (!ishave)
                    {
                        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
                
                
                
                //插入表头
                string strsql = "insert into ContractHead_Sale(cVouchType,Contractid,BillType,cVenCode,cCusCode," +
                 "DeliveryAddress,SettleType,TransPortType,ContractMoney,SignDate,EffectiveDate,EndDate,Remark," +
                 "CompanyCD,Creator,CreateDate,BillStatus,PPersonID,DeptID)" +
                 " values('" + cVouchType + "','" + Contractid + "','" + BillType + "','" + cVenCode + "','" + cCusCode +
                 "','" + DeliveryAddress + "','" + SettleType + "','" + TransPortType + "','" + ContractMoney + "','" + SignDate + "','" + EffectiveDate + "','" + EndDate + "','" + Remark +
                 "','" + CompanyCD + "','" + Creator + "','" + CreatorDate + "','1','"+PPersonID+"','"+DeptID+"') set @ID=@@IDENTITY";

                commandd.CommandText=strsql;
                commandd.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                lstCmd.Add(commandd);
                //插入表体
                //string strheadid = "select id from ContractHead_Sale where cVouchType=1 and Contractid='"+Contractid+"' ";
                //DataTable dtid=SqlHelper.ExecuteSql(strheadid);
                //if (dtid.Rows.Count > 0)
                //{
          
                //ID = int.Parse(dtid.Rows[0]["id"].ToString());
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {                           
                        string coaltype = inseritems[0].ToString();
                        string SpecialName = inseritems[1].ToString();
                        string UnitID = inseritems[2].ToString();
                        string Quantity = inseritems[3].ToString();
                        string UnitCost = inseritems[4].ToString();
                        string Money = inseritems[5].ToString();
                        strsql = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='"+Contractid+"' )"+
                        " insert into ContractDetails_Sale(contractid,cinvccode,ccounitcode,quals,iquantity,iunitcost,imoney) " +
                       //"values('" + dtid.Rows[0]["id"].ToString() + "','" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                       "values(@Contractid,'" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                        SqlCommand command = new SqlCommand(strsql);
                        //commandd.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                        
                        lstCmd.Add(command);
                        
                    }
                }
                    
                    
                //}

                string strlog = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='" + Contractid + "' )" +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011',@contractid,'ContractHead_Sale，ContractDetails_Sale','新建','操作成功')";
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);

                
                //插入记录都表单审批流管理表，用于提交、确认权限控制
                string strdelflow = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='" + Contractid + "' )" +
                "delete from jt_BillFlow where CompanyCD='" + CompanyCD + "' and BillTypeFlag='30' and BillTypeCode='1' and billid=@contractid ";
                SqlCommand commdelflow = new SqlCommand(strdelflow);
                lstCmd.Add(commdelflow);
                string strinflow = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='" + Contractid + "' )" +
                "insert into jt_BillFlow(CompanyCD,BillTypeFlag,BillTypeCode,BillID,BillNo,UserID)" +
                " values('" + CompanyCD + "','30','1',@contractid,'" + Contractid + "','" + userid + "')";
                SqlCommand comminflow = new SqlCommand(strinflow);
                lstCmd.Add(comminflow);


                //-------------------------附件----------------------------------//
                #region 附件

                if (!String.IsNullOrEmpty(AnnFileName))
                {
                    string[] annFileName = AnnFileName.Split(',');
                    string[] annRemark = AnnRemark.Split(',');
                    string[] annUpDateTime =UpDateTime.Split(',');
                    string[] annAddr = AnnAddr.Split(',');
                    if (annFileName.Length >= 1)
                    {
                        for (int i = 0; i < annFileName.Length - 1; i++)
                        {
                            string sqlcmd = @"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='"+Contractid+"' ) "+
                                            "INSERT INTO officedba.Annex (CompanyCD,ParentId,AnnAddr,annFileName,upDatetime,annRemark,ModuleType) "+
                                            "VALUES('" + CompanyCD + "',@contractid,'" + annAddr[i].ToString() + "','" + annFileName[i].ToString() +
                                            "','" + annUpDateTime[i].ToString() + "','" + annRemark[i].ToString() + "','" + ConstUtil.MODULE_ID_SELLCONTRANCT_ADD + "')";

                            SqlCommand comms = new SqlCommand(sqlcmd);
                            lstCmd.Add(comms);
                        }
                    }
                }
                #endregion

                //----------------------------------------------------------------//

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
                    ID = (int)commandd.Parameters["@ID"].Value;
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
                    jc = new JsonClass("保存成功", Contractid, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", Contractid, int.Parse(tempID));
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


    //编辑采购合同
    private void InsertPurOrder(HttpContext context)
     {
           
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        string jcQual = "true";
        string cVouchType = "2";
        string Contractid = context.Request.Params["ContractID"].ToString().Trim();  //合同号
        string BillType = "2";
        string cVenCode =  context.Request.Params["ProviderID"].ToString().Trim();
        string cCusCode = "";
        string DeliveryAddress = context.Request.Params["DeliveryAddress"].ToString().Trim();
        string SettleType = context.Request.Params["SettleType"].ToString().Trim();
        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        string ContractMoney = context.Request.Params["ContractMoney"].ToString().Trim();
        string SignDate = context.Request.Params["SignDate"].ToString().Trim();
        string EffectiveDate = context.Request.Params["EffectiveDate"].ToString().Trim();
        string EndDate = context.Request.Params["EndDate"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();

        //-----------------------------附件---------------------------------//
        string AnnFileName = context.Request.Params["AnnFileName"].ToString().Trim();
        string AnnRemark = context.Request.Params["AnnRemark"].ToString().Trim();
        string UpDateTime = context.Request.Params["UpDateTime"].ToString().Trim();
        string AnnAddr = context.Request.Params["AnnAddr"].ToString().Trim();


        //------------------------------------------------------------------//
        
        //表体
        ArrayList lstCmd = new ArrayList();
        JsonClass jc;
        if (jcQual == "true")
        {
            int ID = int.Parse(tempID);
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)
            {
                //插入记录都表单审批流管理表，用于提交、确认权限控制
                string strdelflow = "delete from jt_BillFlow where CompanyCD='" + CompanyCD + "' and BillTypeFlag='30' and BillTypeCode='2' and billid='" + ID + "' ";
                SqlCommand commdelflow = new SqlCommand(strdelflow);
                lstCmd.Add(commdelflow);
                string strinflow = "insert into jt_BillFlow(CompanyCD,BillTypeFlag,BillTypeCode,BillID,BillNo,UserID)" +
                " values('" + CompanyCD + "','30','2','" + ID + "','" + Contractid + "','" + userid + "')";
                SqlCommand comminflow = new SqlCommand(strinflow);
                lstCmd.Add(comminflow);
                //更新
                string strup = "update ContractHead_Pur set cVouchType='" + cVouchType + "',Contractid='" + Contractid + "',BillType='" + BillType
                   + "',cVenCode='" + cVenCode + "',cCusCode='" + cCusCode + "'," +
                "DeliveryAddress='" + DeliveryAddress + "',SettleType='" + SettleType + "',TransPortType='" + TransPortType
                + "',ContractMoney='" + ContractMoney + "',SignDate='" + SignDate + "',EffectiveDate='" + EffectiveDate + "',EndDate='" + EndDate + "',Remark='" + Remark + "'," +
                " PPersonID='"+PPersonID+"',DeptID='"+DeptID+"'  where id='" + ID.ToString() + "' ";
                
                //SqlHelper.ExecuteSql(strup);
                SqlCommand cmdUp1 = new SqlCommand(strup);
                lstCmd.Add(cmdUp1);

                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = " delete   from  ContractDetails_Pur where contractid='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);


                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string coaltype = inseritems[0].ToString();
                        string SpecialName = inseritems[1].ToString();
                        string UnitID = inseritems[2].ToString();
                        string Quantity = inseritems[3].ToString();
                        string UnitCost = inseritems[4].ToString();
                        string Money = inseritems[5].ToString();
                        string strsql =
                        " insert into ContractDetails_Pur(contractid,cinvccode,ccounitcode,quals,iquantity,iunitcost,imoney) " +
                       "values('" + ID.ToString() + "','" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);

                    }
                }
                //isSucc = true;
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + ID.ToString() + "','ContractHead_Pur，ContractDetails_Pur','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand cmdLog = new SqlCommand(strlog);
                lstCmd.Add(cmdLog);

                //-------------------------附件----------------------------------//
                #region 先删除附件信息

                string Delsql = @"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                    "Delete From officedba.Annex where CompanyCD='" + CompanyCD +
                    "' and ParentId=@contractid and ModuleType='" + ConstUtil.MODULE_ID_PurchaseContract_Add + "'";

                SqlCommand commDel = new SqlCommand(Delsql);
                lstCmd.Add(commDel);
                #endregion

                #region 插入附件
                if (!String.IsNullOrEmpty(AnnFileName))
                {
                    string[] annFileName = AnnFileName.Split(',');
                    string[] annRemark = AnnRemark.Split(',');
                    string[] annUpDateTime = UpDateTime.Split(',');
                    string[] annAddr = AnnAddr.Split(',');
                    if (annFileName.Length >= 1)
                    {
                        for (int i = 0; i < annFileName.Length - 1; i++)
                        {
                            string sqlcmd = @"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                                            "INSERT INTO officedba.Annex (CompanyCD,ParentId,AnnAddr,annFileName,upDatetime,annRemark,ModuleType) " +
                                            "VALUES('" + CompanyCD + "',@contractid,'" + annAddr[i].ToString() + "','" + annFileName[i].ToString() +
                                            "','" + annUpDateTime[i].ToString() + "','" + annRemark[i].ToString() + "','" + ConstUtil.MODULE_ID_PurchaseContract_Add + "')";

                            SqlCommand comms = new SqlCommand(sqlcmd);
                            lstCmd.Add(comms);
                        }
                    }
                }
                #endregion




                //----------------------------------------------------------------//
                
            }
            else //新建
            {
              
                string ContractIDType = context.Request.Params["ContractIDType"].ToString().Trim(); 
                
                string tableName = "ContractHead_Pur";//采购合同表
                string columnName = "Contractid";//合同编号
                

                if (ContractIDType != "")
                    Contractid = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ContractIDType, tableName, columnName);

                else
                {
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, Contractid);
                    if (!ishave)
                    {
                        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }
            
                
                //插入表头
                string strsql = "insert into ContractHead_Pur(cVouchType,Contractid,BillType,cVenCode,cCusCode," +
                 "DeliveryAddress,SettleType,TransPortType,ContractMoney,SignDate,EffectiveDate,EndDate,Remark," +
                 "CompanyCD,Creator,CreateDate,BillStatus,PPersonID,DeptID)" +
                 " values('" + cVouchType + "','" + Contractid + "','" + BillType + "','" + cVenCode + "','" + cCusCode +
                 "','" + DeliveryAddress + "','" + SettleType + "','" + TransPortType + "','" + ContractMoney + "','" + SignDate + "','" + EffectiveDate + "','" + EndDate + "','" + Remark +
                 "','" + CompanyCD + "','" + Creator + "','" + CreatorDate + "','1','"+PPersonID+"','"+DeptID+"') set @ID=@@IDENTITY";
                //SqlHelper.ExecuteSql(strsql);
                commandd.CommandText=strsql;
                commandd.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                lstCmd.Add(commandd);
                //插入表体
                //string strheadid = "select id from ContractHead_Pur where cVouchType=2 and Contractid='" + Contractid + "' ";
                //DataTable dtid = SqlHelper.ExecuteSql(strheadid);
                //if (dtid.Rows.Count > 0)
                //{
                    //ID = int.Parse(dtid.Rows[0]["id"].ToString());
                    string[] strarray = null;
                    string recorditems = "";
                    string[] inseritems = null;
                    string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                    strarray = strfitinfo.Split('|');

                    for (int i = 0; i < strarray.Length; i++)
                    {
                        recorditems = strarray[i];
                        inseritems = recorditems.Split(',');
                        if (recorditems.Length != 0)
                        {
                            string coaltype = inseritems[0].ToString();
                            string SpecialName = inseritems[1].ToString();
                            string UnitID = inseritems[2].ToString();
                            string Quantity = inseritems[3].ToString();
                            string UnitCost = inseritems[4].ToString();
                            string Money = inseritems[5].ToString();
                            strsql = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='"+Contractid+"' )"+
                                " insert into ContractDetails_Pur(contractid,cinvccode,ccounitcode,quals,iquantity,iunitcost,imoney) " +
                           "values(@contractid,'" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                            //SqlHelper.ExecuteSql(strsql);
                            SqlCommand cmdStr2 = new SqlCommand(strsql);
                            lstCmd.Add(cmdStr2);

                        }
                    }
                //}

                //isSucc = true;

                    string strlog = " declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021',@contractid,'ContractHead_Pur，ContractDetails_Pur','新建','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand cmdLog = new SqlCommand(strlog);
                lstCmd.Add(cmdLog);


                //插入记录都表单审批流管理表，用于提交、确认权限控制
                string strdelflow = " declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                "delete from jt_BillFlow where CompanyCD='" + CompanyCD + "' and BillTypeFlag='30' and BillTypeCode='2' and billid=@contractid ";
                SqlCommand commdelflow = new SqlCommand(strdelflow);
                lstCmd.Add(commdelflow);
                string strinflow = " declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                "insert into jt_BillFlow(CompanyCD,BillTypeFlag,BillTypeCode,BillID,BillNo,UserID)" +
                " values('" + CompanyCD + "','30','2',@contractid,'" + Contractid + "','" + userid + "')";
                SqlCommand comminflow = new SqlCommand(strinflow);
                lstCmd.Add(comminflow);
                
            
                //---------------------------附件---------------------------------//
                #region 插入附件
                if (!String.IsNullOrEmpty(AnnFileName))
                {
                    string[] annFileName = AnnFileName.Split(',');
                    string[] annRemark = AnnRemark.Split(',');
                    string[] annUpDateTime = UpDateTime.Split(',');
                    string[] annAddr = AnnAddr.Split(',');
                    if (annFileName.Length >= 1)
                    {
                        for (int i = 0; i < annFileName.Length - 1; i++)
                        {
                            string sqlcmd = @"declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Pur where cVouchType='" + cVouchType + "' and Contractid='" + Contractid + "' ) " +
                                            "INSERT INTO officedba.Annex (CompanyCD,ParentId,AnnAddr,annFileName,upDatetime,annRemark,ModuleType) " +
                                            "VALUES('" + CompanyCD + "',@contractid,'" + annAddr[i].ToString() + "','" + annFileName[i].ToString() +
                                            "','" + annUpDateTime[i].ToString() + "','" + annRemark[i].ToString() + "','" + ConstUtil.MODULE_ID_PurchaseContract_Add + "')";

                            SqlCommand comms = new SqlCommand(sqlcmd);
                            lstCmd.Add(comms);
                        }
                    }
                }
                #endregion
                //------------------------------------------------------------------//
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
                    ID = (int)commandd.Parameters["@ID"].Value;
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
                    jc = new JsonClass("保存成功", Contractid, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", Contractid, int.Parse(tempID));
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

    /// <summary>
    /// 删除销售合同
    /// </summary>
    /// <param name="context"></param>
    private void DelSelContract(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allSelContractID"].ToString().Trim();
        
        bool isSucc = false;//是否删除成功
        JsonClass jc;
        context.Response.ContentType = "text/plain";
        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的采购到货单 
            string IsHasEnsure = " select count(*) from ContractHead_Sale where id in (" + allid + ") and BillStatus='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)
            {
                ArrayList lstCmd = new ArrayList();
                //删除销售主表
                string delsql = " delete from ContractHead_Sale where id in (" + allid + ")";
                SqlCommand commandHead = new SqlCommand(delsql);
                lstCmd.Add(commandHead);
                //删除销售明细表
                delsql = "delete from ContractDetails_Sale where contractid in (" + allid + ")";
                SqlCommand commandDetail = new SqlCommand(delsql);
                lstCmd.Add(commandDetail);

                //记录操作日志
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80011','" + allid + "','ContractHead_Sale，ContractDetails_Sale','删除','操作成功')";
                SqlCommand cmdLog = new SqlCommand(strlog);
                lstCmd.Add(cmdLog);

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
            jc = new JsonClass("删除失败", "", 0);
        }


        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    private void DelPurContract(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allPurContractID"].ToString().Trim();
        
        bool isSucc = false;//是否删除成功
        JsonClass jc;
        context.Response.ContentType = "text/plain";
        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的采购到货单 
            string IsHasEnsure = " select count(*) from ContractHead_Pur where id in (" + allid + ") and BillStatus='2' ";
            string IsHasEnsure1 = " select count(*) from ContractHead_Pur where id in (" + allid + ") and BillStatus='9' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            int count1 = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure1,null));
            if (count == 0&&count1==0)
            {
                ArrayList lstCmd = new ArrayList();
                //删除采购主表
                string delsql = " delete from ContractHead_Pur where id in (" + allid + ")";
                SqlCommand commandHead = new SqlCommand(delsql);
                lstCmd.Add(commandHead);
                //删除采购明细表
                delsql = " delete from ContractDetails_Pur where contractid in (" + allid + ")";
                SqlCommand commandDetail = new SqlCommand(delsql);
                lstCmd.Add(commandDetail);

                //记录操作日志
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                  "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','80021','" + allid + "','ContractHead_Pur，ContractDetails_Pur','删除','操作成功')";
                SqlCommand cmdLog = new SqlCommand(strlog);
                lstCmd.Add(cmdLog);

                try
                {

                    isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

                }
                catch (Exception ex)
                {
                    isSucc = false;
                }
            }
            else if(count!=0&&count1==0)
            {
                jc = new JsonClass("已经确认单据无法删除，请先取消确认！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
            else
            {
                jc = new JsonClass("已经关闭单据无法删除，请先取消确认！", "", 0);
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
            jc = new JsonClass("删除失败", "", 0);
        }



        context.Response.Write(jc);
        context.Response.End();
    }
    
   
    public bool IsReusable {
        get {
            return false;
        }
    }

}