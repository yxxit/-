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
                string action = (context.Request.Params["action"].ToString());//操作//
                           
                if (action == "insert")//编辑
                {
                     
                    InsertSellOrder(context);
                                        
                }
                else if (action == "ConfirmInWare") //确认采购入库单
                {
                    ConfirmInWare(context);
                }
                else if (action == "CancelConfirmInWare") //反确认采购入库单
                {
                    CancelConfirmInWare(context);
                }
                if (action == "DeleteInWare")//删除采购入库单
                {
                    DeleteInWare(context); 
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
        int ID = int.Parse(tempID);
        string jcQual = "true";
        JsonClass jc;
        string InCodeType = context.Request.Params["InCodeType"].ToString().Trim();
        string InWareNo = context.Request.Params["InWareNo"].ToString().Trim();
        string tableName = "jt_cgrk";//费用申请表
        string columnName = "InNo";//单据编号

        if (ID == 0)  //如果是新增采购入库单
        {
            if (InCodeType != "")  //如果为自动编号，则获取编码
                InWareNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(InCodeType, tableName, columnName);
            else
            {
                //判断是否已经存在
                bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, InWareNo);
                if (!ishave)
                {
                    jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
        }

        

        string SourceBillID = context.Request.Params["SourceBillID"].ToString().Trim();
        string SourceBillNo = context.Request.Params["SourceBillNo"].ToString().Trim();
        string ProviderID = context.Request.Params["ProviderID"].ToString().Trim();
        string ProviderName = context.Request.Params["ProviderName"].ToString().Trim();
        //string CoalID = context.Request.Params["CoalID"].ToString().Trim();
        //string CoalName = context.Request.Params["CoalName"].ToString().Trim();
        //string WareID = context.Request.Params["WareID"].ToString().Trim();
        //string WareName = context.Request.Params["WareName"].ToString().Trim();
        string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
        string TranSportNo = context.Request.Params["TranSportNo"].ToString().Trim();
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();  //收货人ID
        string PPerson = context.Request.Params["PPerson"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim() == "" ? "0" : context.Request.Params["DeptID"].ToString().Trim();  //收货人部门ID

        string DeptName = context.Request.Params["DeptName"].ToString().Trim();
        string ReceiveTime = context.Request.Params["ReceiveTime"].ToString().Trim();
        string StartStation = context.Request.Params["StartStation"].ToString().Trim();
        string SendNum = context.Request.Params["SendNum"].ToString().Trim();
        string CarNum = context.Request.Params["CarNum"].ToString().Trim() == "" ? "0" : context.Request.Params["CarNum"].ToString().Trim();  //发车数
        string EndStation = context.Request.Params["EndStation"].ToString().Trim();
        string RealSendNum = context.Request.Params["RealSendNum"].ToString().Trim();   //入库总数量
        //string RealCarNum = context.Request.Params["RealCarNum"].ToString().Trim();
        string QTestID = context.Request.Params["QTestID"].ToString().Trim();
        string QTestNo = context.Request.Params["QTestNo"].ToString().Trim();
        string Remark = context.Request.Params["Remark"].ToString().Trim();
        string CarNo = context.Request.Params["CarNo"].ToString().Trim();   //车次
                        
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
                string strup = "    update jt_cgrk set  CountTotal='"+RealSendNum+"', DeptID='"+DeptID+"',FromType='1',FromBillID='"+SourceBillID+"',TransPortNo='"+TranSportID+"',Checker='"+PPersonID+"',Remark='"+Remark+"',"+
                "    ModifiedUserID='" + userid + "' where ID='" + ID.ToString() + "' ";
                SqlCommand command1 = new SqlCommand(strup);
               
                lstCmd.Add(command1);
               


                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = "   delete    from  jt_cgrk_mx  where InNo='" + ID.ToString() + "' ";
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
                        string hidInBusMxId = inseritems[3].ToString();  //采购入库单明细ID
                        string RealCarNum = inseritems[4].ToString() == "" ? "0" : inseritems[4].ToString();
                        //if (Convert.ToDouble(Quantity) <= 0) continue;  //如果煤种数量为0，则跳过
                        string strsql = @" insert into jt_cgrk_mx(CompanyCD,InNo,ProductID,ProductCount,StorageID,ModifiedDate,ModifiedUserID,cgdhmx_autoid,CarNum) " +
                                       " values('" + CompanyCD + "','" + ID.ToString() + "','" + CoalID + "','" + Quantity + "','" + WareID + "','" + CreatorDate + "','" + userid + "','" + hidInBusMxId + "','" + RealCarNum + "') ";
                        SqlCommand command3 = new SqlCommand(strsql);
                        lstCmd.Add(command3);

                    }
                }

                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77721','" + ID.ToString() + "','jt_cgrk，jt_cgrk_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
                
                
                                      
                
            }
            else
            {
                                          
                //插入表头
                string strsql = " insert into   jt_cgrk(CompanyCD,InNo,FromType,FromBillID,TransPortNo,Checker,Remark,Creator,CreateDate,BillStatus,Executor,DeptID,EnterDate,ModifiedUserID,ModifiedDate,CountTotal)" +
                "  values('" + CompanyCD + "','" + InWareNo + "','1','" + SourceBillID + "','" + TranSportID + "','" + PPersonID + "','" + Remark + "','" + Creator + "','" + CreatorDate + "','1'," + PPersonID + "," + DeptID + ",'" + ReceiveTime + "','" + userid + "','"+CreatorDate+"','"+RealSendNum+"')   set  @id=@@IDENTITY";

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
                        string hidInBusMxId = inseritems[3].ToString();  //采购入库单明细ID
                        string RealCarNum = inseritems[4].ToString() == "" ? "0" : inseritems[4].ToString();
                        //if (Convert.ToDouble(Quantity) <= 0) continue;  //如果煤种数量为0，则跳过
                        string strsqlDetail =@"  declare   @inno varchar(20) set @inno=(select max(ID) AS ID from jt_cgrk where inno='" + InWareNo + "')"+
                                       " insert into jt_cgrk_mx(CompanyCD,InNo,ProductID,ProductCount,StorageID,ModifiedDate,ModifiedUserID,cgdhmx_autoid,CarNum) "+
                                       " values('" + CompanyCD + "',@inno,'" + CoalID + "','" + Quantity + "','" + WareID + "','" + CreatorDate + "','" + userid + "','" + hidInBusMxId + "','" + RealCarNum + "') ";
                        SqlCommand command3 = new SqlCommand(strsqlDetail);
                        lstCmd.Add(command3);


                    }
                }



                string strlog = "  declare   @inno varchar(20) set @inno=(select max(ID) AS ID from jt_cgrk where inno='" + InWareNo + "')" +
                "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77721',@inno,'jt_cgrk，jt_cgrk_mx','新建','操作成功')";
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
                    jc = new JsonClass("保存成功", InWareNo, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", InWareNo, ID);
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


    //确认采购入库单
    private void ConfirmInWare(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //采购到货单id

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


                string Quantity = inseritems[0].ToString() == "" ? "0" : inseritems[0].ToString();  //入库的数量
                string hidInBusMxId = inseritems[1].ToString();  //采购到货单明细id

                //先判断入库的数量是否多于到货单的数量
                string strIsMore = @" select  (isnull(ProductCount,0)-isnull(InCount,0)) as count from dbo.jt_cgdh_mx where  id='" + hidInBusMxId + "' ";
                Double ProductCount = Convert.ToDouble(SqlHelper.ExecuteScalar(strIsMore));
                if (ProductCount < Convert.ToDouble(Quantity))
                {
                    jc = new JsonClass("第" + i + "行的煤种数量不能多于" + ProductCount + ",请重新保存！", "", 0);
                    context.Response.Write(jc.ToString());
                    context.Response.End();
                }
                else
                {

                    //更新采购到货单的已入库数量
                    string strUpdatefh = " update dbo.jt_cgdh_mx set InCount=isnull(InCount,0)+" + Quantity +
                                    "  where id='" + hidInBusMxId + "' ";

                    SqlCommand commandUpp = new SqlCommand(strUpdatefh);
                    lstCmd.Add(commandUpp);
                }
            }
        }

        string strconfirm = "update dbo.jt_cgrk  set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id='" + tempID + "' ";


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
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77721','" + tempID + "','jt_cgrk','确认','操作成功')";
            SqlHelper.ExecuteSql(strEnsurelog);

        }
        else
        {
            jc = new JsonClass("确认失败", "", 0);
        }

        context.Response.Write(jc.ToString());
        context.Response.End();
    }

    
    //反确认采购入库单
    private void CancelConfirmInWare(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        ArrayList lstCmd = new ArrayList();
        bool isSucc = false;//是否取消确认成功

        string strconfirm = "update dbo.jt_cgrk set billstatus=1,confirmor=null,confirmdate=null  where id='" + tempID + "' ";

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


                string Quantity = inseritems[0].ToString() == "" ? "0" : inseritems[0].ToString();  //入库的数量
                string hidInBusMxId = inseritems[1].ToString();  //采购到货单明细id






                //更新采购到货单已入库数量
                string strUpdatefh = " update dbo.jt_cgdh_mx set InCount=isnull(InCount,0)-" + Quantity +
                                "  where  id='" + hidInBusMxId + "' ";

                SqlCommand commandUpp = new SqlCommand(strUpdatefh);
                lstCmd.Add(commandUpp);
            }
        }

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
            //写入日志

            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77721','" + tempID + "','jt_cgrk','取消确认','操作成功')";
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
    /// 删除采购入库单
    /// </summary>
    /// <param name="context"></param>
    private void DeleteInWare(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allInWareID"].ToString().Trim();

        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的销售单 
            string IsHasEnsure = " select count(*) from jt_cgrk where id in (" + allid + ") and BillStatus='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除入库单主表
                string delsql = " delete from jt_cgrk  where id in (" + allid + ")";
                SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除入库单明细
                delsql = "delete from jt_cgrk_mx where InNo in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);

                //记录删除日志

                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','77721','" + allid + "','jt_cgrk,jt_cgrk_mx','删除','操作成功')";
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