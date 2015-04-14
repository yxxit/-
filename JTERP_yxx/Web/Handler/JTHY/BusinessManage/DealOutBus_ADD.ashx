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
                string billtype = (context.Request.Params["billtype"].ToString()); //销售类型：0.普通销售 1.采购直销
                           
                if (action == "insert")
                {
                  
                     InsertSellOrder(context,billtype);
                                     
                }
                else if (action == "ConfirmOutBus")  //确认销售单
                {
                    ConfirmOutBus(context,billtype);
                }
                else if (action == "CancelConfirmOutBus")  //取消确认销售单
                {
                    CancelConfirmOutBus(context, billtype);
                }
                else if (action == "DeleteOutBus")  //删除销售单
                {
                    DeleteOutBus(context, billtype);
                }
                
            }
       }
    
    //编辑销售发货单
    private void InsertSellOrder(HttpContext context,string BillType)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        int ID = int.Parse(tempID);
        string jcQual = "true";
        //表头
        JsonClass jc;
        string CodeType = context.Request.Params["CodeType"].ToString().Trim();
        string SendNo = context.Request.Params["SendNo"].ToString().Trim();
        string tableName = "jt_xsfh";//费用申请表
        string columnName = "SendNo";//单据编号

        if (ID == 0)
        {
            if (CodeType != "" && CodeType!=null)  //如果为自动编号，则获取编码
                SendNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(CodeType, tableName, columnName);
            else
            {
                //判断是否已经存在
                bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, SendNo);
                if (!ishave)
                {
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 0);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
        }
       
        string SourceBillID = context.Request.Params["SourceBillID"].ToString().Trim();
        string SourceBillNo = context.Request.Params["SourceBillNo"].ToString().Trim();
        string SettleType = context.Request.Params["SettleType"].ToString().Trim();
        string CustomerID = context.Request.Params["CustomerID"].ToString().Trim();
        string CustomerName = context.Request.Params["CustomerName"].ToString().Trim();
        string InvoiceUnit = context.Request.Params["InvoiceUnit"].ToString().Trim();
        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        string SendTime = context.Request.Params["SendTime"].ToString().Trim();
        string SendNum = context.Request.Params["SendNum"].ToString().Trim();//
        string GetNum = context.Request.Params["GetNum"].ToString().Trim();//实收吨数
        
        string SumMoney = context.Request.Params["SumMoney"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string PPerson = context.Request.Params["PPerson"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string DeptName = context.Request.Params["DeptName"].ToString().Trim();
        string BusiType = context.Request.Params["BusiType"].ToString().Trim();
        string PurContractID = context.Request.Params["PurContractID"].ToString().Trim();
        string PurContractNo = context.Request.Params["PurContractNo"].ToString().Trim();
        string ProviderID = context.Request.Params["ProviderID"].ToString().Trim();
        string ProviderName = context.Request.Params["ProviderName"].ToString().Trim();
        string SupplyAmount = context.Request.Params["SupplyAmount"].ToString().Trim();
        string TransportFee = context.Request.Params["TransportFee"].ToString().Trim() == "" ? "0" : context.Request.Params["TransportFee"].ToString().Trim(); //运费
        string ServiceId = context.Request.Params["serviceId"].ToString().Trim();
        string ServcieName = context.Request.Params["servcieName"].ToString().Trim();
        
        if (PurContractID == "")
        {
            PurContractID = "0"; 
        }
        if (ProviderID == "")
        {
            ProviderID = "0"; 
        }
        if (SupplyAmount == "")
        {
            SupplyAmount = "0"; 
        }
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        
        string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
        string TranSportNo = context.Request.Params["TranSportNo"].ToString().Trim();
        string UPTranSportState = context.Request.Params["UPTranSportState"].ToString().Trim();  
               
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate=DateTime.Now.ToShortDateString();
        
                   
        ArrayList lstCmd = new ArrayList();
        
        if (jcQual == "true")//其作用？
        {
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)//id大于0的时候，表示，已经添加过了，再次就是修改！
            {
               ////更新
                string strup = " update jt_xsfh  set custid='"+CustomerID+"',sendno='"+SendNo+"',frombillid='"+SourceBillID+"',busitype='"+BusiType+"',"+
                "    paytype='" + SettleType + "',seller='" + PPersonID + "',selldeptid='" + DeptID + "',carrytype='" + TransPortType + "',sendaddr=''," +
                "    sender='',receiveaddr='',receiver='',tel='',intendsenddate='',TotalFee='" + SumMoney + "', " +
                "    transportfee='" + TransportFee + "',remark='" + Remark + "',Transporter='" + TranSportID +
                "',PurContractID='"+PurContractID+"',ProviderID='"+ProviderID+"',SupplyAmount='"+SupplyAmount+"'," +
                "    modifieddate='" + CreatorDate + "',modifieduserid='" + userid + "',ServicesID='"+ServiceId+"' where ID='" + ID.ToString() + "' ";
                SqlCommand command1 = new SqlCommand(strup);
                //SqlHelper.ExecuteSql(strup);
                lstCmd.Add(command1);

                ////更新调运单状态
                //string strstate = "update jt_HuocheDiaoyun set at_state ='" + UPTranSportState + "' where id_at='" + TranSportID + "' ";
                //SqlCommand commstate = new SqlCommand(strstate);
                //lstCmd.Add(commstate);

                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');
                
                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = "  delete   from  jt_xsfh_mx  where sendno='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);

                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {                        
                        string Ware = inseritems[0].ToString();
                        string CoalID = inseritems[1].ToString();
                        string CoalName = inseritems[2].ToString();
                        string Quantity = inseritems[3].ToString();
                        string InCost = inseritems[4].ToString() == "" ? "0" : inseritems[4].ToString();
                        string SaleCost = inseritems[5].ToString();
                        string TaxRate = inseritems[6].ToString() == "" ? "17" : inseritems[6].ToString();
                        string Tax = inseritems[7].ToString() == "" ? "0" : inseritems[7].ToString();
                        string Money = inseritems[8].ToString();
                        string TotalInCost = inseritems[9].ToString() == "" ? "0" : inseritems[9].ToString();
                        string strsql = "  insert into jt_xsfh_mx(companycd,sendno,sortno,productid,productcount,TaxPrice,TaxRate,TotalFee,TotalTax,InCost,storageid,TotalInCost)" +
                             " values('" + CompanyCD + "'," + ID + ",'1','" + CoalID + "','" + Quantity + "','" + SaleCost + "','" + TaxRate + "','" + Money + "','" + Tax + "','" + InCost + "','" + Ware + "','" + TotalInCost + "')";
                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);
                    }
                }

                string moduleid = "";
                if (BillType == "0")
                {
                    moduleid = "90031";
                }
                if (BillType == "1")
                {
                    moduleid = "90011";
                }
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','" + moduleid + "','" + ID.ToString() + "','jt_xsfh，jt_xsfh_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);                
            }
            else
            {
                                          
                //插入表头
                string strsql = "insert into   jt_xsfh(companycd,custid,sendno,frombillid,busitype,paytype,seller,selldeptid,carrytype,remark,Transporter,BillStatus,Creator,CreateDate,TransportFee,PurContractID,ProviderID,SupplyAmount,ModifiedUserID,TotalFee,ServicesID)" +
                "   values('"+CompanyCD+"','"+CustomerID+"','"+SendNo+"','"+SourceBillID+"','"+BusiType+"','"+SettleType+"','"+PPersonID
                + "','" + DeptID + "','" + TransPortType + "','" + Remark + "','" + TranSportID + "','1','" + Creator + "','" + CreatorDate + "','" + TransportFee + "','"
                + PurContractID + "','" + ProviderID + "','" + SupplyAmount + "','" + userid + "','" + SumMoney + "','"+ServiceId+"')  set @id=@@IDENTITY";

                commandd.CommandText=strsql;
                commandd.Parameters.Add(SqlHelper.GetOutputParameter("@id", SqlDbType.Int));
                lstCmd.Add(commandd);
                
                //更新调运单状态
                //string strstate = "update jt_HuocheDiaoyun set at_state ='"+UPTranSportState+"' where id_at='"+TranSportID+"' ";
                //SqlCommand commstate = new SqlCommand(strstate);
                //lstCmd.Add(commstate);
                
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
                        string Ware = inseritems[0].ToString();
                        string CoalID = inseritems[1].ToString();
                        string CoalName = inseritems[2].ToString();
                        string Quantity = inseritems[3].ToString();
                        string InCost = inseritems[4].ToString() == "" ? "0" : inseritems[4].ToString();
                        string SaleCost = inseritems[5].ToString();
                        string TaxRate = inseritems[6].ToString() == "" ? "17" : inseritems[6].ToString();
                        string Tax = inseritems[7].ToString() == "" ? "0" : inseritems[7].ToString();
                        string Money = inseritems[8].ToString();
                        string TotalInCost = inseritems[9].ToString() == "" ? "0" : inseritems[9].ToString();

                        strsql = "  declare   @SendNo varchar(20) set @SendNo=(select max(ID) AS ID from jt_xsfh where SendNo='" + SendNo + "'  )" +
                            "  insert into jt_xsfh_mx(companycd,sendno,sortno,productid,productcount,TaxPrice,TaxRate,TotalFee,TotalTax,InCost,storageid,TotalInCost)" +
                             " values('" + CompanyCD + "',@SendNo,'1','" + CoalID + "','" + Quantity + "','" + SaleCost + "','" + TaxRate + "','" + Money + "','" + Tax + "','" + InCost + "','" + Ware + "','" + TotalInCost + "')";
                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);
                    }
                } 

                string moduleid = "";
                if (BillType == "0")
                {
                    moduleid = "90031";
                }
                if (BillType == "1")
                {
                    moduleid = "90011";
                }
                string strlog = "  declare  @SendNo varchar(20) set @SendNo=(select max(ID) AS ID from jt_xsfh where SendNo='" + SendNo + "'  ) " +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','" + moduleid + "',@SendNo,'jt_xsfh，jt_xsfh_mx','新建','操作成功')";
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
                    jc = new JsonClass("保存成功", SendNo, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", SendNo, int.Parse(tempID));
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


    //确认销售单
    private void ConfirmOutBus(HttpContext context, string BillType)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //采购到货单id

        bool isSucc = false;//是否确认成功
        try
        {
            string strconfirm = "update dbo.jt_xsfh set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";
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
            string moduleid = "";
            if (BillType == "0")
            {
                moduleid = "90031";
            }
            if (BillType == "1")
            {
                moduleid = "90011";
            }
            string strEnsurelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','" + moduleid + "','" + tempID + "','jt_xsfh','确认','操作成功')";
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


    //取消确认销售单操作
    private void CancelConfirmOutBus(HttpContext context, string BillType)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        //string allid = context.Request.Params["allOutBusID"].ToString().Trim();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        bool isSucc = false;//是否取消确认成功

        int count=0;
        //先判断该发货单是否已经被出库单引用
        if (BillType == "0")
        {
            string IsUsed = @"select count(id)
                        from jt_xsck  
                        where FromBillID='" + tempID + "' and CompanyCD='" + CompanyCD + "' ";
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsUsed, null));
        }
        //判断所选项目章是否包含已经调用的发票单
        string IsHasEnsureFP = @" select count(a.id)
                                     from jt_xsfp a       
                                     left join Jt_xsfp_mx c on  a.id=c.xsfpId    
                                     left join jt_xsfh d on d.sendNO=c.sendNO    
                                     left join jt_xsfh_mx f on f.sendNO = d.id and f.id = c.payId    
                                     where  d.id ="+tempID+@"";
        int countFP = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsureFP, null));
        if (countFP != 0)
        {
            jc = new JsonClass("该单据已经被销售结算单引用，无法取消确认！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }
        //判断采购到货订单是否被调用
        string IsHasEnsureFP1 = @" select count(a.id)  from Jt_cgfp a    
                                left join Jt_cgfp_mx c on  a.id=c.cgfpid  
                                where sendno in (select sendNo from jt_xsfh  where id ="+tempID+")";
        int countFP1 = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsureFP1, null));
        if (countFP1 != 0)
        {
            jc = new JsonClass("该单据已经被采购结算单引用，无法取消确认！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }
        if (count == 0)  //如果没被引用
        {
            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update dbo.jt_xsfh set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "' ";
            SqlCommand command1 = new SqlCommand(strconfirm);
            lstCmd.Add(command1);
            //写入日志
            string moduleid = "";
            if (BillType == "0")    //销售出库
            {
                moduleid = "90031";
            }
            if (BillType == "1")  //采购直销
            {
                moduleid = "90011";
            }
            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','" + moduleid + "','" + tempID + "','jt_xsfh','取消确认','操作成功')";
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
        }
        else
        {
            jc = new JsonClass("该发货单已经被出库单引用，无法取消确认！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }

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
    /// 删除销售单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteOutBus(HttpContext context, string BillType)
    {
        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allOutBusID"].ToString().Trim();

        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的销售单 
            string IsHasEnsure = " select count(*) from jt_xsfh where id in (" + allid + ") and BillStatus='2' ";
            //判断所选项目章是否包含已经调用的发票单
            string IsHasEnsureFP = @" select count(a.id)
                                     from jt_xsfp a       
                                     left join Jt_xsfp_mx c on  a.id=c.xsfpId    
                                     left join jt_xsfh d on d.sendNO=c.sendNO    
                                     left join jt_xsfh_mx f on f.sendNO = d.id and f.id = c.payId    
                                     where  d.id in ("+allid+@")";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            int countFP = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsureFP, null));
            if (countFP!=0)
            {
                jc = new JsonClass("该订单已经被调用，请先去除调用！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除销售单主表
                string delsql = " delete from jt_xsfh  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除销售单明细
                delsql = "delete from jt_xsfh_mx where SendNo in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);

                //记录删除日志
                string moduleid = "";
                if (BillType == "0")    //销售出库
                {
                    moduleid = "90031";
                }
                if (BillType == "1")  //采购直销
                {
                    moduleid = "90011";
                }
                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','" + moduleid + "','" + allid + "','jt_xsfh,jt_xsfh_mx','删除','操作成功')";
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