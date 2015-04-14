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
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Params["action"].ToString());//操作


            if (action == "insert")
            {

                InsertSellOrder(context);

            }
            else if (action == "DeleteInBusInfo")//删除到货单
            {
                DeleteInBusInfo(context);
            }
            else if (action == "ConfirmInBus")
            {
                ConfirmInBus(context);
            }
            else if (action == "CancelConfirmInBus")  //  取消确认到货单
            {
                CancelConfirmInBus(context);
            }
            else if (action == "upStatus")
            {

                UpStatus(context);

            }
        }
    }


    //更新调运状态
    private void UpStatus(HttpContext context)
    {
        string tranSportNo = context.Request.Form["tranSportNo"];//调运单号
        string UPTranSportState = context.Request.Form["status"];//状态

        string statusId = context.Request.Form["statusId"];//调运单号id
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;

        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        bool isSucc = false;//是否确认成功

        ArrayList lstCmd = new ArrayList();

        //更新调运单状态
        string strstate = "update jt_HuocheDiaoyun set at_state ='" + UPTranSportState + "' where id_at='" + statusId + "' ";
        SqlCommand commstate = new SqlCommand(strstate);
        lstCmd.Add(commstate);


        //记录调运单的更新日志
        string strlogDiaoYun = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
            "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + statusId + "','jt_HuocheDiaoyun','修改','操作成功')";
        //SqlHelper.ExecuteSql(strlog);
        SqlCommand command3 = new SqlCommand(strlogDiaoYun);
        lstCmd.Add(command3);
        try
        {
            isSucc = isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

        }
        catch (Exception ex)
        {
            isSucc = false;
        }
        JsonClass jc;
        if (isSucc)
        {
            jc = new JsonClass("成功！", "", 1);
        }
        else
        {
            jc = new JsonClass("失败！", "", 0);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    //编辑到货单
    private void InsertSellOrder(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        int ID = int.Parse(tempID);
        string jcQual = "true";

        JsonClass jc;
        string ArriveType = context.Request.Params["ArriveType"].ToString().Trim();
        string ArriveBillNo = context.Request.Params["ArriveBillNo"].ToString().Trim();
        string tableName = "jt_cgdh";//费用申请表
        string columnName = "ArriveNo";//单据编号

        if (ID == 0)  //如果是新增采购入库单
        {
            if (ArriveType != "")  //如果为自动编号，则获取编码
                ArriveBillNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(ArriveType, tableName, columnName);
            else
            {
                //判断是否已经存在
                bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, ArriveBillNo);
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
        string ProviderID = context.Request.Params["ProviderID"].ToString().Trim();
        string ProviderName = context.Request.Params["ProviderName"].ToString().Trim();
        string LinkMan = context.Request.Params["LinkMan"].ToString().Trim();
        string SendTime = context.Request.Params["SendTime"].ToString().Trim();
        string SendNum = context.Request.Params["SendNum"].ToString().Trim() == "" ? "0" : context.Request.Params["SendNum"].ToString().Trim();
        string SumMoney = context.Request.Params["SumMoney"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string PPerson = context.Request.Params["PPerson"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string DeptName = context.Request.Params["DeptName"].ToString().Trim();

        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
        string TranSportNo = context.Request.Params["TranSportNo"].ToString().Trim();
        string TranSportState = context.Request.Params["TranSportState"].ToString().Trim();
        string CarNo = context.Request.Params["CarNo"].ToString().Trim();
        string StartStation = context.Request.Params["StartStation"].ToString().Trim();
        string EndStation = context.Request.Params["EndStation"].ToString().Trim();
        string CarNum = context.Request.Params["CarNum"].ToString().Trim() == "" ? "0" : context.Request.Params["CarNum"].ToString().Trim();
        string UPTranSportState = context.Request.Params["UPTranSportState"].ToString().Trim();
        string Freight = context.Request.Params["Freight"].ToString().Trim();  //运费
        string ServicesId = context.Request.Params["ServicesId"].ToString().Trim();

        //string serviceId = context.Request.Params["serviceId"].ToString().Trim();
        //string serviceName = context.Request.Params["servcieName"].ToString().Trim();         
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();

        ArrayList lstCmd = new ArrayList();
        if (jcQual == "true")
        {
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)
            {
                ////更新
                string strup = " update jt_cgdh set FromType='1',TransPortNo='" + TranSportID + "',ProviderID='" + ProviderID + "',Purchaser='" + PPersonID + "',DeptID='" + DeptID + "'," +
                "PayType='" + SettleType + "',CarryType='" + TransPortType + "',OtherTotal='" + Freight + "',TotalFee='" + SumMoney + "',SendNum='" + SendNum + "',CarNum='" +
                CarNum + "',SendTime='" + SendTime + "',SourceBillID='" + SourceBillID + "',ModifiedUserID='" + userid + "',ServicesID='" + ServicesId + "'  where ID='" + ID.ToString() + "' ";
                SqlCommand command1 = new SqlCommand(strup);
                //SqlHelper.ExecuteSql(strup);
                lstCmd.Add(command1);

                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = " delete   from  jt_cgdh_mx where ArriveNo='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);

                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string Ware = inseritems[0].ToString();
                        string CoalType = inseritems[1].ToString();
                        string Quantity = inseritems[2].ToString();
                        string UnitCost = inseritems[3].ToString();
                        string Money = inseritems[4].ToString();
                        string TaxRate = inseritems[5].ToString();
                        string ISQTest = inseritems[6].ToString();

                        string NetUnitCost = Convert.ToString(Convert.ToDouble(UnitCost) / (1 + Convert.ToDouble(TaxRate) / 100));//无税单价
                        string NetMoney = Convert.ToString(Convert.ToDouble(Quantity) * Convert.ToDouble(NetUnitCost));//无税金额
                        string TotalTax = Convert.ToString(Convert.ToDouble(Money) - Convert.ToDouble(NetMoney));//税额
                        string strsql = "  " +
                        "  insert into jt_cgdh_mx(SortNo,ArriveNo,ProductID,ProductCount,UnitPrice,TaxPrice,TaxRate,TotalFee,TotalPrice,TotalTax,StorageID,ISQTest)" +
                        "  values('" + (i + 1).ToString() + "','" + ID.ToString() + "','" + CoalType + "','" + Quantity + "','"
                        + NetUnitCost + "','" + UnitCost + "','" + TaxRate + "','" + Money + "','" + NetMoney + "','" + TotalTax + "','" + Ware + "','" + ISQTest + "')";

                        SqlCommand command_updetails = new SqlCommand(strsql);
                        lstCmd.Add(command_updetails);

                    }
                }



                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','90021','" + ID.ToString() + "','jt_cgdh，jt_cgdh_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);

                ////记录调运单的更新日志
                //string strlogDiaoYun ="  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                //    "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + TranSportID + "','jt_HuocheDiaoyun','修改','操作成功')";
                ////SqlHelper.ExecuteSql(strlog);
                //SqlCommand command3 = new SqlCommand(strlogDiaoYun);
                //lstCmd.Add(command3);



            }
            else
            {

                //插入表头
                string strsql = "insert into   jt_cgdh(CompanyCD,ArriveNo,FromType,TransPortNo,ProviderID,ServicesId, Purchaser,DeptID," +
                "PayType,CarryType,TotalFee,OtherTotal,BillStatus,Creator,CreateDate,SendNum,CarNum,SendTime,SourceBillID,ModifiedUserID)" +
                "   values('" + CompanyCD + "','" + ArriveBillNo + "','1','" + TranSportID + "','" + ProviderID + "','" + ServicesId + "','" + PPersonID + "','" + DeptID
                + "','" + SettleType + "','" + TransPortType + "','" + SumMoney + "','" + Freight + "','1','" + Creator + "','" + CreatorDate + "','" +
                SendNum + "','" + CarNum + "','" + SendTime + "','" + SourceBillID + "','" + userid + "')  set @ID=@@IDENTITY";

                commandd.CommandText = strsql;
                commandd.Parameters.Add(SqlHelper.GetOutputParameter("@ID", SqlDbType.Int));
                lstCmd.Add(commandd);

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
                        string CoalType = inseritems[1].ToString();
                        string Quantity = inseritems[2].ToString();
                        string UnitCost = inseritems[3].ToString();
                        string Money = inseritems[4].ToString();
                        string TaxRate = inseritems[5].ToString();
                        string ISQTest = inseritems[6].ToString();
                        string NetUnitCost = Convert.ToString(Convert.ToDouble(UnitCost) / (1 + Convert.ToDouble(TaxRate) / 100));//无税单价
                        string NetMoney = Convert.ToString(Convert.ToDouble(Quantity) * Convert.ToDouble(NetUnitCost));//无税金额
                        string TotalTax = Convert.ToString(Convert.ToDouble(Money) - Convert.ToDouble(NetMoney));//税额
                        // strsql = "declare @contractid varchar(50)  set @contractid=(select distinct id from ContractHead_Sale where cVouchType=1 and Contractid='"+Contractid+"' )"+
                        // " insert into ContractDetails_Sale(contractid,cinvccode,ccounitcode,quals,iquantity,iunitcost,imoney) " +                     
                        //"values(@Contractid,'" + coaltype + "','" + UnitID + "','" + SpecialName + "','" + Quantity + "','" + UnitCost + "','" + Money + "')";
                        strsql = "  declare   @ArriveNo varchar(20) set @ArriveNo=(select max(ID) AS ID from jt_cgdh where ArriveNo='" + ArriveBillNo + "'  )" +
                        "  insert into jt_cgdh_mx(SortNo,ArriveNo,ProductID,ProductCount,UnitPrice,TaxPrice,TaxRate,TotalFee,TotalPrice,TotalTax,StorageID,ISQTest)" +
                        "  values('" + (i + 1).ToString() + "',@ArriveNo,'" + CoalType + "','" + Quantity + "','"
                        + NetUnitCost + "','" + UnitCost + "','" + TaxRate + "','" + Money + "','" + NetMoney + "','" + TotalTax + "','" + Ware + "','" + ISQTest + "')";
                        SqlCommand command = new SqlCommand(strsql);
                        lstCmd.Add(command);

                    }
                }

                string strlog = "  declare  @ArriveNo varchar(20) set @ArriveNo=(select max(ID) AS ID from jt_cgdh where ArriveNo='" + ArriveBillNo + "'  ) " +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','90021',@ArriveNo,'jt_cgdh，jt_cgdh_mx','新建','操作成功')";
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
                    ID = (int)commandd.Parameters["@ID"].Value;
                }

                //更新主表的总除税金额，总税额，总数量
                double totalMoney = 0.0;
                double totalTaxMoney = 0.0;
                double totalCount = 0.0;
                string searchSql = @"select sum(isnull(totalPrice,0)) as totalMoney,sum(isnull(TotalTax,0)) as totalTax,
                                    sum(isnull(ProductCount,0)) as totalCount
                                     from dbo.jt_cgdh_mx where ArriveNo='" + ID + "' ";

                DataTable dts = SqlHelper.ExecuteSql(searchSql);
                if (dts != null && dts.Rows.Count > 0)
                {
                    totalMoney = Convert.ToDouble(dts.Rows[0]["totalMoney"].ToString());
                    totalTaxMoney = Convert.ToDouble(dts.Rows[0]["totalTax"].ToString());
                    totalCount = Convert.ToDouble(dts.Rows[0]["totalCount"].ToString());

                }
                string updateSql = "update dbo.jt_cgdh set TotalMoney='" + totalMoney + "', TotalTax='" + totalTaxMoney + "' , CountTotal='" + totalCount +
                                    "' where id='" + ID + "' and companyCD='" + CompanyCD + "' ";
                SqlHelper.ExecuteSql(updateSql);


            }
            catch (Exception ex)
            {
                isSucc = false;
            }


            if (isSucc)
            {
                if (ID > 0)
                {
                    jc = new JsonClass("保存成功", ArriveBillNo, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", ArriveBillNo, int.Parse(tempID));
                }
            }
            else
            {
                jc = new JsonClass("保存失败!", "", 0);
            }

        }
        else
        {
            jc = new JsonClass("保存失败!", "", 0);
        }

        context.Response.Write(jc);
    }

    /// <summary>
    /// 删除采购到货单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteInBusInfo(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allInBusID"].ToString().Trim();
        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的采购到货单 
            string IsHasEnsure = " select count(*) from jt_cgdh where id in (" + allid + ") and BillStatus='2' ";
            string IsHasEnsureFP = @" select count(a.id)  from Jt_cgfp a    
                                 left join Jt_cgfp_mx c on  a.id=c.cgfpid  
                                 left join jt_cgdh d on d.ArriveNo=c.sendNO    
                                 left join jt_cgdh_mx f on f.ArriveNo = d.id and f.id = c.cgdhId 
                                 where d.id in (" + allid + ") ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            int countFP = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsureFP, null));
            if (countFP != 0)
            {
                jc = new JsonClass("该订单已经被调用，请先去除调用！", "", 0);
                context.Response.Write(jc.ToString());
                context.Response.End();
            }
            
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除采购到货主表
                string delsql = " delete from jt_cgdh  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除采购到货明细
                delsql = "delete from jt_cgdh_mx where ArriveNo in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);
                //记录删除日志
                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','90021','" + allid + "','jt_cgdh,jt_cgdh_mx','删除','操作成功')";
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


    //确认采购订货单
    private void ConfirmInBus(HttpContext context)
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


        ArrayList lstCmd = new ArrayList();
        string strconfirm = "update dbo.jt_cgdh set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";
        SqlCommand command1 = new SqlCommand(strconfirm);
        lstCmd.Add(command1);
        //写入日志


        string strCanclelog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','90021','" + tempID + "','jt_cgdh','确认','操作成功')";
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
        if (isSucc)
        {
            jc = new JsonClass("确认成功", employeeName, int.Parse(employeeid));
        }
        else
        {
            jc = new JsonClass("确认失败", "采购到货单", 0);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    //取消确认采购到货单
    private void CancelConfirmInBus(HttpContext context)
    {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        bool isSucc = false;//是否取消确认成功

        //判断到货单是否被质检单引用

        string IsUsed = @"select count(id) from (select id from jt_cgzj a where a.FromReportNo='" + tempID + "' union select id from jt_cgrk where FromBillID='" + tempID + "') as a ";

        int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsUsed, null));
        //判断采购到货订单是否被调用
        string IsHasEnsureFP = @" select count(a.id)  from Jt_cgfp a    
                                 left join Jt_cgfp_mx c on  a.id=c.cgfpid  
                                 left join jt_cgdh d on d.ArriveNo=c.sendNO    
                                 left join jt_cgdh_mx f on f.ArriveNo = d.id and f.id = c.cgdhId 
                                 where d.id ="+tempID+" ";
        int countFP = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsureFP, null));
        if (countFP != 0)
        {
            jc = new JsonClass("该单据已经被结算单引用，无法取消确认！", "", 0);
            context.Response.Write(jc.ToString());
            context.Response.End();
        }
        if (count == 0)  //如果没被引用
        {
            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update dbo.jt_cgdh set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "' ";
            SqlCommand command1 = new SqlCommand(strconfirm);
            lstCmd.Add(command1);
            //写入日志
            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','90021','" + tempID + "','jt_cgdh','取消确认','操作成功')";
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
            jc = new JsonClass("该到货单已经被质检单或入库单引用，无法取消确认！", "", 0);
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}