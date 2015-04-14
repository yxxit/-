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
        public void ProcessRequest (HttpContext context){
            
            if (context.Request.RequestType == "POST")
            {
                string action = (context.Request.Params["action"].ToString());//操作

                           
                if (action == "insert")//编辑
                {
                     
                    InsertSellOrder(context);
                                        
                }
                if (action == "DeleteOutWare")//删除
                {
                    DeleteOutWare(context); 
                }
                if (action == "ConfirmOutWare")
                {
                    ConfirmOutWare(context); //确认
                }
                if (action == "CancelConfirmOutWare")
                {
                    CancelConfirmOutWare(context);  //反确认
                }
                
            }
       }
       
   
    
    //编辑销售出库单
    private void InsertSellOrder(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        int ID = int.Parse(tempID);
        string jcQual = "true";
        JsonClass jc;
        string OutCodeType = context.Request.Params["OutCodeType"].ToString().Trim();
        string OutWareNo = context.Request.Params["OutWareNo"].ToString().Trim();
        string tableName = "jt_xsck";//销售出库表
        string columnName = "OutNo";//单据编号

        if (ID == 0)  //如果是新增采购入库单
        {
            if (OutCodeType != "")  //如果为自动编号，则获取编码
                OutWareNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(OutCodeType, tableName, columnName);
            else
            {
                //判断是否已经存在
                bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, OutWareNo);
                if (!ishave)
                {
                    jc = new JsonClass("该编号已被使用，请输入未使用的编号！","" , 0);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
        }

        

        string SourceBillID = context.Request.Params["SourceBillID"].ToString().Trim();
        string SourceBillNo = context.Request.Params["SourceBillNo"].ToString().Trim();
        string SettleType = context.Request.Params["SettleType"].ToString().Trim();
        string CustomerID = context.Request.Params["CustomerID"].ToString().Trim();
        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        string OprID = context.Request.Params["OprID"].ToString().Trim();
        string OutWareTime = context.Request.Params["OutWareTime"].ToString().Trim();
        string OutNum = context.Request.Params["OutNum"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string SendNum = context.Request.Params["SendNum"].ToString().Trim();  //收货人ID
        string TransMoney = context.Request.Params["TransMoney"].ToString().Trim();


        string Remark = context.Request.Params["Remark"].ToString().Trim();
        //string WareID= context.Request.Params["WareID"].ToString().Trim();
        //string CoalID = context.Request.Params["CoalID"].ToString().Trim();
        //string Quantity = context.Request.Params["Quantity"].ToString().Trim();
        //string SaleCost = context.Request.Params["SaleCost"].ToString().Trim();
        //string TaxRate = context.Request.Params["TaxRate"].ToString().Trim();
        //string Tax = context.Request.Params["Tax"].ToString().Trim();
        //string TaxMoney = context.Request.Params["TaxMoney"].ToString().Trim();
        string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
        string TranSportNo = context.Request.Params["TranSportNo"].ToString().Trim();
        //string TranSportState = context.Request.Params["TranSportState"].ToString().Trim();

        string CarNo = context.Request.Params["CarNo"].ToString().Trim();
        string StartStation = context.Request.Params["StartStation"].ToString().Trim();
        string EndStation = context.Request.Params["EndStation"].ToString().Trim();
        string CarNum = context.Request.Params["CarNum"].ToString().Trim();
        //string Remark = context.Request.Params["Remark"].ToString().Trim();
                        
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator=((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate=DateTime.Now.ToShortDateString();
        
                   
        ArrayList lstCmd = new ArrayList();
        if (jcQual == "true")
        {
            
            SqlCommand commandd = new SqlCommand();
            if (ID > 0)
            {
               ////更新
                string strup = "    update jt_xsck set FromType='1',FromBillID='"+SourceBillID+"',Transactor='"+PPersonID+"',DeptID='"+DeptID
                + "',TransportFee='" + TransMoney + "',OutDate='" + OutWareTime + "',CountTotal='" + OutNum + "',Remark='" + Remark + "',modifieduserid='" + userid + "',TransPortID='" + TranSportID + "',custid='" + CustomerID + "'  " +
                "   where id='"+ID.ToString()+"' ";
                SqlCommand command1 = new SqlCommand(strup);
               
                lstCmd.Add(command1);
               

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = "   delete    from  jt_xsck_mx  where OutNo='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);


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


                        string WareID = inseritems[0].ToString();
                        string CoalID = inseritems[1].ToString();

                        string Quantity = inseritems[2].ToString() == "" ? "0" : inseritems[2].ToString();
                        string SaleCost = inseritems[3].ToString() == "" ? "0" : inseritems[3].ToString();
                        string TaxRate = inseritems[4].ToString() == "" ? "17" : inseritems[4].ToString();
                        string Tax = inseritems[5].ToString() == "" ? "0" : inseritems[5].ToString();
                        string Money = inseritems[6].ToString() == "" ? "0" : inseritems[6].ToString();
                        string hidOutBusMxId = inseritems[7].ToString();  //销售发货单明细ID
                        //string hidPreQuantity = inseritems[8].ToString();  //上次保存时的煤种数量
                        //if (Convert.ToDouble(Quantity) <= 0) continue;  //如果煤种数量为0，则跳过
                        string strsql = " insert into jt_xsck_mx(outno,CompanyCD,sortno,storageid,productid,unitprice,ProductCount,totalprice,UsedUnitCount,usedPrice,TaxRate,TotalTax,xsfhmx_autoid) " +
                                "  values('" + ID.ToString() + "','" + CompanyCD + "','" + (i + 1).ToString() + "','" + WareID + "','" + CoalID + "','" + SaleCost + "','" + Quantity + "','" + Money + "','" + Quantity + "','" + SaleCost + "','" + TaxRate + "','" + Tax + "','" + hidOutBusMxId + "') ";
                        SqlCommand command3 = new SqlCommand(strsql);
                        lstCmd.Add(command3);
                        
                        ////更新销售发货单的已出库数量
                        //string strUpdatefh = " update dbo.jt_xsfh_mx set OutCount=isnull(OutCount,0)-" + hidPreQuantity + "+" + Quantity + "  ,ProductCount=ProductCount+" + hidPreQuantity + "-" + Quantity +
                        //                "  where CompanyCD='" + CompanyCD + "' and ProductID='" + CoalID + "' and id='" + hidOutBusMxId + "'";

                        //SqlCommand commandUpp = new SqlCommand(strUpdatefh);
                        //lstCmd.Add(commandUpp);

                    }
                }
                
                
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77731','" + ID.ToString() + "','jt_xsck，jt_xsck_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
                
                
                                      
                
            }
            else
            {
                                          
                //插入表头
                string strsql = "  insert  into   jt_xsck(CompanyCD,OutNo,FromType,FromBillID,Creator,CreateDate,Transactor,DeptID,TransportFee,OutDate,CountTotal,Remark,TransPortID,custid,billstatus)" +
                "  values('"+CompanyCD+"','"+OutWareNo+"','1','"+SourceBillID+"','"+Creator+"','"+CreatorDate+"','"+PPersonID+"','"
                + DeptID + "','" + TransMoney + "','" + OutWareTime + "','" + OutNum + "','" + Remark + "','" + TranSportID + "','" + CustomerID + "','1')   set  @id=@@IDENTITY";

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


                        string WareID = inseritems[0].ToString();
                        string CoalID = inseritems[1].ToString();

                        string Quantity = inseritems[2].ToString() == "" ? "0" : inseritems[2].ToString();
                        string SaleCost = inseritems[3].ToString() == "" ? "0" : inseritems[3].ToString();
                        string TaxRate = inseritems[4].ToString() == "" ? "17" : inseritems[4].ToString();
                        string Tax = inseritems[5].ToString() == "" ? "0" : inseritems[5].ToString();
                        string Money = inseritems[6].ToString() == "" ? "0" : inseritems[6].ToString();
                        string hidOutBusMxId = inseritems[7].ToString();  //销售发货单明细ID
                        //if (Convert.ToDouble(Quantity) <= 0) continue;  //如果煤种数量为0，则跳过
                        string strsqlDetail = "  declare   @outno varchar(20) set @outno=(select max(ID) AS ID from jt_xsck where outno='" + OutWareNo + "')" +
                                     " insert into jt_xsck_mx(outno,CompanyCD,sortno,storageid,productid,unitprice,ProductCount,totalprice,UsedUnitCount,usedPrice,TaxRate,TotalTax,xsfhmx_autoid) " +
                                    "  values(@outno,'" + CompanyCD + "','" + (i + 1).ToString() + "','" + WareID + "','" + CoalID + "','" + SaleCost + "','" + Quantity + "','" + Money + "','" + Quantity + "','" + SaleCost + "','" + TaxRate + "','" + Tax + "','" + hidOutBusMxId + "') ";
                        SqlCommand command3 = new SqlCommand(strsqlDetail);
                        lstCmd.Add(command3);


                    }
                }

                string strlog = "  declare   @outno varchar(20) set @outno=(select max(ID) AS ID from jt_xsck where outno='" + OutWareNo + "')" +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77731',@outno,'jt_xsck，jt_xsck_mx','新建','操作成功')";
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
                    jc = new JsonClass("保存成功", OutWareNo, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", OutWareNo, ID);
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


    //确认出库单
    private void ConfirmOutWare(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //销售出库单id

        ArrayList lstCmd = new ArrayList();
        bool isSucc = false;//是否确认成功
        context.Response.ContentType = "text/plain";
        
        
        
        
        
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


                string Quantity = inseritems[0].ToString() == "" ? "0" : inseritems[0].ToString();  //出库的数量
                string hidOutBusMxId = inseritems[1].ToString();  //销售发货单明细id
                
                //先判断出库的数量是否多于发货单的数量
                string strIsMore = @" select  (isnull(ProductCount,0)-isnull(OutCount,0)) as count from dbo.jt_xsfh_mx where CompanyCD='" + CompanyCD + "' and id='" + hidOutBusMxId + "' ";
                Double ProductCount= Convert.ToDouble( SqlHelper.ExecuteScalar(strIsMore));
                if (ProductCount < Convert.ToDouble(Quantity))
                {
                    jc = new JsonClass("第" + i + "行的煤种数量不能多于" + ProductCount + ",请重新保存！", "", 0);
                    context.Response.Write(jc.ToString());
                    context.Response.End();
                }
                else
                {

                    //更新销售发货单的已出库数量
                    string strUpdatefh = " update dbo.jt_xsfh_mx set OutCount=isnull(OutCount,0)+" + Quantity +
                                    "  where CompanyCD='" + CompanyCD + "' and id='" + hidOutBusMxId + "' ";

                    SqlCommand commandUpp = new SqlCommand(strUpdatefh);
                    lstCmd.Add(commandUpp);
                }
            }
        }

        string strconfirm = "update dbo.jt_xsck  set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";


        SqlCommand command3 = new SqlCommand(strconfirm);
        lstCmd.Add(command3);

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
            //写入日志

            string strEnsurelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77731','" + tempID + "','jt_xsck','确认','操作成功')";
            SqlHelper.ExecuteSql(strEnsurelog);

        }
        else
        {
            jc = new JsonClass("确认失败", "", 0);
        }
        
        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    //反确认出库单
    private void CancelConfirmOutWare(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        ArrayList lstCmd = new ArrayList();
        bool isSucc = false;//是否取消确认成功
       
        string strconfirm = "update dbo.jt_xsck set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "' ";

        SqlCommand command3 = new SqlCommand(strconfirm);
        lstCmd.Add(command3);



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


                string Quantity = inseritems[0].ToString() == "" ? "0" : inseritems[0].ToString();  //出库的数量
                string hidOutBusMxId = inseritems[1].ToString();  //销售发货单明细id






                //更新销售发货单的已出库数量
                string strUpdatefh = " update dbo.jt_xsfh_mx set OutCount=isnull(OutCount,0)-" + Quantity +
                                "  where CompanyCD='" + CompanyCD + "' and id='" + hidOutBusMxId + "' ";

                SqlCommand commandUpp = new SqlCommand(strUpdatefh);
                lstCmd.Add(commandUpp);
            }
        }

        try
        {
            isSucc = SqlHelper.ExecuteTransWithArrayList(lstCmd);

        }
        catch (Exception ee)
        {
            isSucc = false;
        }
        if (isSucc)
        {

            jc = new JsonClass("取消确认成功", "", int.Parse(tempID));
            //写入日志

            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77731','" + tempID + "','jt_xsck','取消确认','操作成功')";
            SqlHelper.ExecuteSql(strCanclelog);

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
    /// 删除出库单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteOutWare(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allOutWareID"].ToString().Trim();

        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的销售单 
            string IsHasEnsure = " select count(*) from jt_xsck where id in (" + allid + ") and BillStatus='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除出库单主表
                string delsql = " delete from jt_xsck  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除出库单明细
                delsql = "delete from jt_xsck_mx where OutNo in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);

                //记录删除日志

                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77731','" + allid + "','jt_xsck,jt_xsck_mx','删除','操作成功')";
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