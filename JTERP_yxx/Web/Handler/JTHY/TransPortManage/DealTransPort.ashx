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
    static string billtypes = "";
    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Params["action"].ToString());//操作
            string billtype = (context.Request.Params["billtype"].ToString()); //调运单类型类型：1.火运 2.汽运
            billtypes = billtype;
            if (action == "insert")//编辑调运单
            {
                InsertSellOrder(context);
            }
            else if (action == "ConfirmTranS")   //确认调运单
            {
                ConfirmTranS(context);
            }
            else if (action == "CancelConfirmTranS")
            {
                CancelConfirmTranS(context);    //取消确认
            }
            else if (action == "Delete")//删除调运单
            {
                DelTransPort(context);
            }
            else if (action == "Uc_insertDetail") //保存过磅信息
            {
                Uc_insertDetail(context);
            }


        }
    }



    //插入调运单
    private void InsertSellOrder(HttpContext context)
    {
        bool isSucc = false;//是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();
        string jcQual = "true";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();

        string TranSportID = context.Request.Params["TranSportID"].ToString().Trim();
        string StartStation = context.Request.Params["StartStation"].ToString().Trim();
        string ArriveStation = context.Request.Params["ArriveStation"].ToString().Trim();
        string StartDate = context.Request.Params["StartDate"].ToString().Trim();
        string StartCarCode = context.Request.Params["StartCarCode"].ToString().Trim();
        string EndCarCode = context.Request.Params["EndCarCode"].ToString().Trim();
        string CarNo = context.Request.Params["CarNo"].ToString().Trim(); // 车次
        float CarNum = float.Parse(context.Request.Params["CarNum"].ToString().Trim()); // 原发车数
        float SendNum = float.Parse(context.Request.Params["SendNum"].ToString().Trim()); // 原发吨数
        string PPersonID = context.Request.Params["PPersonID"].ToString().Trim();
        string DeptID = context.Request.Params["DeptID"].ToString().Trim();
        string TransPortType = context.Request.Params["TransPortType"].ToString().Trim();
        
        string Jh_place = context.Request.Params["Jh_place"].ToString().Trim(); // 计划到站
        string Jh_ReceMan = context.Request.Params["Jh_ReceMan"].ToString().Trim(); // 原收货人
        string Ss_ReceMan = context.Request.Params["Ss_ReceMan"].ToString().Trim(); // 实际收货人
        string strSs_VeQuan = context.Request.Params["Ss_VeQuan"].ToString().Trim();
        if (strSs_VeQuan == "") strSs_VeQuan = "0";
        float Ss_VeQuan = float.Parse(strSs_VeQuan); //  实收车数量

        string strSs_quan = context.Request.Params["Ss_quan"].ToString().Trim();
        if (strSs_quan == "") strSs_quan = "0";
        float Ss_quan = float.Parse(strSs_quan); // 实收吨数
        
        string Remark = context.Request.Params["Remark"].ToString().Trim();    //备注

        //表体

        JsonClass jc;
        if (jcQual == "true")
        {
            int ID = int.Parse(tempID);
            if (ID > 0)
            {
                //更新
                string strup = "update jt_HuocheDiaoyun set at_department='" + DeptID + "',Ship_type='" + billtypes + "',ship_time='" + StartDate +
                      "',ship_place='" + StartStation + "'," +
                      "to_place='" + ArriveStation + "',motorcade='" + CarNo + "',trans_type='" + TransPortType + "',ship_quantity=" + SendNum + "," +
                      "vehicle_quantity=" + CarNum + ",num_first_vehicle='" + StartCarCode + "',num_last_vehicle='" + EndCarCode +
                      "',ModifiedDate='" + CreatorDate + "',ModifiedUserID='" + Creator + "',PPersonID='" + PPersonID + 
                      "',Jh_place='" + Jh_place + "',Jh_ReceMan='" + Jh_ReceMan + "',Ss_ReceMan='" + Ss_ReceMan +
                      "',Ss_VeQuan=" + Ss_VeQuan + ",Ss_quan=" + Ss_quan + ",Remark='" + Remark +                      
                      "' where id_at='" + ID + "'  ";
               
                SqlHelper.ExecuteSql(strup);
                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');


                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = "delete from jt_HuocheDiaoyun_mx where id_at_state='" + ID.ToString() + "' ";
                SqlHelper.ExecuteSql(strdeldetails);
                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string CarCode = inseritems[0].ToString();
                        string GrossWeight = inseritems[1].ToString();
                        string TareWeight = inseritems[2].ToString();
                        string NetWeight = inseritems[3].ToString();
                        string SumWeight = inseritems[4].ToString();

                        string strsql = " insert into jt_HuocheDiaoyun_mx(id_at_state,num_vehicle,gw,pw,nw,sumnum) " +
                       "values('" + ID.ToString() + "','" + CarCode + "','" + GrossWeight + "','" + TareWeight + "','" + NetWeight + "','" + SumWeight + "')";
                        SqlHelper.ExecuteSql(strsql);

                    }
                }

                isSucc = true;

                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + ID.ToString() + "','jt_HuocheDiaoyun，jt_HuocheDiaoyun_mx','修改','操作成功')";
                SqlHelper.ExecuteSql(strlog);
            }
            else
            {

                string TranSportIDType = context.Request.Params["TranSportIDType"].ToString().Trim();

                string tableName = "jt_HuocheDiaoyun";//采购合同表
                string columnName = "DiaoyunNO";//调运编号


                if (TranSportIDType != "")
                    TranSportID = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue_jt(TranSportIDType, tableName, columnName);

                else
                {
                    bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq_jt(tableName, columnName, TranSportID);
                    if (!ishave)
                    {
                        jc = new JsonClass("faile", "该编号已被使用，请输入未使用的编号！", 2);
                        context.Response.Write(jc);
                        context.Response.End();
                    }
                }

                //插入表头
                //string strsql = "insert into jt_HuocheDiaoyun(at_department, Ship_type,ship_time,ship_place,to_place,motorcade,trans_type,ship_quantity,vehicle_quantity," +
                string strsql = "insert into jt_HuocheDiaoyun(at_department, Ship_type,ship_time,ship_place,to_place,motorcade,ship_quantity,vehicle_quantity," +
                 
                "num_first_vehicle,num_last_vehicle,Creator,CreateDate,CompanyCD,DiaoyunNO,PPersonID,BillStatus,Jh_place,Jh_ReceMan,Ss_ReceMan,Ss_VeQuan,Ss_quan,Remark)" +
                 "values('" + DeptID + "','" + billtypes + "','" + StartDate + "','" + StartStation + "','" + ArriveStation + "','" + CarNo + "'," + 
                 //TransPortType + "','" + SendNum + "','" + CarNum + "','" + StartCarCode + "','" + EndCarCode + "','" + Creator + "','" + CreatorDate + "','" +
                  SendNum + "," + CarNum + ",'" + StartCarCode + "','" + EndCarCode + "','" + Creator + "','" + CreatorDate + "','" +
                 
                 CompanyCD + "','" + TranSportID + "','" + PPersonID + "','1','" + Jh_place + "','" + Jh_ReceMan + "','" + Ss_ReceMan + "'," + Ss_VeQuan + "," +
                 Ss_quan + ",'" + Remark + "')";            
                
                DataTable dttest = SqlHelper.ExecuteSql(strsql);
                //插入表体
                string strheadid = "select id_at as id  from jt_HuocheDiaoyun where   DiaoyunNO='" + TranSportID + "' ";
                DataTable dtid = SqlHelper.ExecuteSql(strheadid);
                if (dtid.Rows.Count > 0)
                {
                    ID = int.Parse(dtid.Rows[0]["id"].ToString());
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
                            string CarCode = inseritems[0].ToString();
                            string GrossWeight = inseritems[1].ToString();
                            string TareWeight = inseritems[2].ToString();
                            string NetWeight = inseritems[3].ToString();
                            string SumWeight = inseritems[4].ToString();

                            strsql = " insert into jt_HuocheDiaoyun_mx(id_at_state,num_vehicle,gw,pw,nw,sumnum) " +
                           "values('" + dtid.Rows[0]["id"].ToString() + "','" + CarCode + "','" + GrossWeight + "','" + TareWeight + "','" + NetWeight + "','" + SumWeight + "')";
                            SqlHelper.ExecuteSql(strsql);

                        }
                    }


                }

                isSucc = true;
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                               "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + ID.ToString() + "','jt_HuocheDiaoyun，jt_HuocheDiaoyun_mx','新建','操作成功')";
                SqlHelper.ExecuteSql(strlog);

            }


            if (isSucc)
            {
                if (ID > 0)
                {
                    jc = new JsonClass("保存成功", TranSportID, ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", TranSportID, int.Parse(tempID));
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


    //确认调运单
    private void ConfirmTranS(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();  //调运单id

        bool isSucc = false;//是否确认成功
        try
        {
            string strconfirm = "update dbo.jt_HuocheDiaoyun set billstatus=2,confirmor='" + employeeid + "',confirmdate=getdate()  where id_at='" + tempID + "' ";
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
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + tempID + "','jt_HuocheDiaoyun','确认','操作成功')";
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

    //取消确认
    private void CancelConfirmTranS(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        bool isSucc = false;//是否取消确认成功

        //判断该调运单已经被到货单或发货单引用
        string IsUsed = @"select count(id) from 
    (select id from jt_xsfh where Transporter='" + tempID + "' UNION  select id from jt_cgdh  where TransPortNo='" + tempID + "'  ) as a";
        int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsUsed, null));
        if (count == 0)  //如果没被引用
        {
            ArrayList lstCmd = new ArrayList();
            string strconfirm = "update dbo.jt_HuocheDiaoyun set billstatus=1,confirmor=null,confirmdate=null  where id_at='" + tempID + "' ";
            SqlCommand command1 = new SqlCommand(strconfirm);
            lstCmd.Add(command1);
            //写入日志
            string strCanclelog = " insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                              "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + tempID + "','jt_HuocheDiaoyun','取消确认','操作成功')";
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
            jc = new JsonClass("该调运单已经被到货单或发货单引用，无法取消确认！", "", 0);
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
    /// 删除调运单
    /// </summary>
    /// <param name="context"></param>
    private void DelTransPort(HttpContext context)
    {


        JsonClass jc;
        bool isSucc = false;//是否删除成功
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string allid = context.Request.Params["allTransID"].ToString().Trim();

        if (allid.Length > 0)
        {
            //先判断所选的选项中是否有已经确认的销售单 
            string IsHasEnsure = " select count(*) from jt_HuocheDiaoyun where id_at in (" + allid + ") and BillStatus='2' ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
            if (count == 0)  //如果都没确认
            {
                ArrayList lstCmd = new ArrayList();
                //删除调运单主表
                string delsql = " delete from jt_HuocheDiaoyun where id_at in (" + allid + ")";
                System.Data.SqlClient.SqlCommand commandDelMain = new SqlCommand(delsql);
                lstCmd.Add(commandDelMain);

                //删除调运单明细
                delsql = "delete from jt_HuocheDiaoyun_mx where id_at_state in (" + allid + ")";
                SqlCommand commandDelMx = new SqlCommand(delsql);
                lstCmd.Add(commandDelMx);

                //记录删除日志

                string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                   "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + allid + "','jt_HuocheDiaoyun,jt_HuocheDiaoyun_mx','删除','操作成功')";
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
    
    //保存过磅信息
    private void Uc_insertDetail(HttpContext context)
    {
        JsonClass jc;
        bool isSucc = false;//是否删除成功
        ArrayList lstCmd = new ArrayList();
        context.Response.ContentType = "text/plain";
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string headid = context.Request.Params["headid"].ToString().Trim();
        //插入表体

        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
        strarray = strfitinfo.Split('|');


        //更新表体，先删除原有表体数据，再执行增加功能
        string strdeldetails = "delete from jt_HuocheDiaoyun_mx where id_at_state='" + headid + "' ";
        SqlCommand commanddeldetails = new SqlCommand(strdeldetails);
        lstCmd.Add(commanddeldetails);
        for (int i = 1; i < strarray.Length; i++)
        {
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                string CarCode = inseritems[0].ToString();
                string GrossWeight = inseritems[1].ToString();
                string TareWeight = inseritems[2].ToString();
                string NetWeight = inseritems[3].ToString();
                string SumWeight = inseritems[4].ToString();

                string strsql = " insert into jt_HuocheDiaoyun_mx(id_at_state,num_vehicle,gw,pw,nw,sumnum) " +
               "values('" + headid + "','" + CarCode + "','" + GrossWeight + "','" + TareWeight + "','" + NetWeight + "','" + SumWeight + "')";
                SqlCommand commanddetails = new SqlCommand(headid);
                lstCmd.Add(commanddetails);

            }
        }
        string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
        "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','9992','" + headid + "','jt_HuocheDiaoyun_mx','修改','操作成功')";
        SqlHelper.ExecuteSql(strlog);
        SqlCommand commandlog = new SqlCommand(strlog);
        lstCmd.Add(commandlog);
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

            jc = new JsonClass("保存成功", "", int.Parse(headid));

        }
        else
        {
            jc = new JsonClass("保存失败", "", 0);
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