<%@ WebHandler Language="C#" Class="PaySettle" %>
using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using XBase.Data.JTHY.Expenses;
using XBase.Data.DBHelper;
using System.Text;
using XBase.Common;
using System.Data.SqlClient;
using System.Collections;
public class PaySettle : IHttpHandler,IRequiresSessionState
{
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        HttpRequest request = context.Request;
         
        if (context.Request.RequestType == "POST")
        {          
            string Action = context.Request.Params["Action"];
            int User = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID;
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            if (Action == "SearchPaySettle")
            {
                DataTable dt = new DataTable();

                string custName = context.Request.Form["custName"].ToString();
                string beginDate = context.Request.Form["beginDate"].ToString();
                string endDate = context.Request.Form["endDate"].ToString();
                
                  int  iscount =   Convert.ToInt32( context.Request.Form["iscount"].ToString());
                dt = PaySettleDBHelper.SearchPaySettle(custName, beginDate, endDate,iscount);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("{");
                sb.Append("data:");
                sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");

                context.Response.Write(sb.ToString()); 
                context.Response.End();


            }
            else if (Action == "ConfirmSell")//确认
            {

                ConfirmSell(context);
            }

            else if (Action == "CancelConfirmSell")//取消确认
            {
                CancelConfirmSell(context);

            }
            else if (Action == "CloseConfim")//关闭
            {

                CloseConfim(context);
            }
            else if (Action == "UnClosePur")//取消关闭
            {
                UnClosePur(context);

            }
            else if (Action == "SearchPaySettleDetail") // 获取指定 ID 结算单详细信息
            {

                SearchPaySettleDetail(context);

            }

            else if (Action == "DeletePaySettle")
            {

                JsonClass jc;
                bool isSucc = false;//是否删除成功
                context.Response.ContentType = "text/plain";
                string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
                string allid = context.Request.Params["allPaySettle"].ToString().Trim();
                if (allid.Length > 0)
                {
                    //先判断所选的选项中是否有已经确认的销售单 
                    string IsHasEnsure = " select count(id) from jt_xsfp where id in (" + allid + ") and BillStatus='2' ";                   
                    int count = Convert.ToInt32(SqlHelper.ExecuteScalar(IsHasEnsure, null));
                    if (count == 0)  //如果都没确认
                    {
                        ArrayList lstCmd = new ArrayList();
                        //删除采购到货主表
                        string delsql = " delete from Jt_xsfp where id in (" + allid + ")";
                        SqlCommand commandDelMain = new SqlCommand(delsql);
                        lstCmd.Add(commandDelMain);

                        //删除采购到货明细
                        delsql = "delete from Jt_xsfp_mx where xsfpId in (" + allid + ")";
                        SqlCommand commandDelMx = new SqlCommand(delsql);
                        lstCmd.Add(commandDelMx);

                        //记录删除日志
                        string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880525','" + allid + "','Jt_xsfp ,Jt_xsfp_mx','删除','操作成功')";
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
                    else {
                        jc = new JsonClass("已经确认单据无法删除，请先取消确认！", "", 0);
                        context.Response.Write(jc.ToString());
                        context.Response.End();
                    }
                }
                if (isSucc)
                {

                    jc = new JsonClass("删除成功！", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败！", "", 0);
                }

                context.Response.Write(jc.ToString());
                context.Response.End();
            }
            else if (Action == "insertPaySettle")
            {
                InsertPaySettle(context);

            }
            else if (Action == "SearchPayOne")  // 
            {

                string headid = context.Request.Form["headid"].ToString();

                context.Response.ContentType = "text/plain";

                DataTable dt = new DataTable();
                string strQuery = "   select a.XsfpNo,a.CustID, a. id,a.billStatus,CONVERT(varchar(100),a.CreateDate, 23) as CreateDate," +
                                  " CONVERT(varchar(100),a.ConfirmDate, 23) as ConfirmDate,a.Confirmor," +
                                  " e.employeeName as CreatorName,h.EmployeeName as ConfirmorName from dbo.Jt_xsfp a" +
                                  " left join  officedba.EmployeeInfo  e on e.id=a.creator " +
                                  " left join officedba.EmployeeInfo h on h.id=a.Confirmor where a.CompanyCD='" + CompanyCD + "' and a.id = " + headid;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                try
                {
                    dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(strQuery);                 
                    sb.Append("{");
                    sb.Append("data:");
                    sb.Append(JsonClass.DataTable2Json(dt));
                    sb.Append("}");



                }
                catch (Exception ex)
                {
                 string str= ex.Message;
                }

                context.Response.Write(sb.ToString());
                context.Response.End();
            }
            else if (Action == "SearhcPaySettlePages") // 销售结算列表
            {

                int pageCount = int.Parse(context.Request.Form["pageCount"].ToString());//每页显示记录数
                int pageIndex = int.Parse(context.Request.Form["pageIndex"].ToString());//当前页
                //检索条件
                string BeginT = context.Request.Form["BeginT"].ToString().Trim();  //开始日期
                string EndT = context.Request.Form["EndT"].ToString().Trim();   //结束日期
                string SendNo = context.Request.Form["SendNo"].ToString().Trim(); //单据编号
                string CreateName = context.Request.Form["CreateName"].ToString().Trim();   //创建人
                string CustName = context.Request.Form["CustName"].ToString().Trim();//客户
                
                string orderBy = " id  desc  ";
                int TotalCount = 0;
                DataTable dt = new DataTable();
                string strQuery = "   select a.id,a.custId,convert(varchar(12),a.CreateDate,23) CreateDate,p.CustName as custName,p.custName as payName,a.xsfpNo, c.sttlPrice,c.sttlCount,c.sttlTotalPrice,c.SttlRemark," +
              "   f.sttlCount as sttlCounts,f.SttlTotalPrice as sttlTotalPrices,(Case a.billStatus when 1 then '制单' when 2 then '确认' when 9 then '关闭' else '其他' end) billstatus," +
              "   e.employeeName as createName,h.EmployeeName as ConfirmorName,f.ProductCount"+
              
              "   from jt_xsfp a left join  officedba.CustInfo p on p.Id=a.CustID" +
              "   left join Jt_xsfp_mx c on  a.id=c.xsfpId " +
              "   left join officedba.EmployeeInfo  e on e.id=a.creator " +
              "   left join officedba.EmployeeInfo h on h.id=a.Confirmor " +
              "   left join jt_xsfh_mx f on f.id = c.payId   where  c.CompanyCD = '" + CompanyCD + "'";
                
                if (CustName != "")
                    strQuery += " and p.CustName like '%" + CustName + "%'";
                
                if (SendNo != "")
                    strQuery += " and a.xsfpNo like '%" + SendNo + "%'";
                
                if (CreateName != "")
                    strQuery += " and e.EmployeeName like '%" + CreateName + "%'";
                
                if (BeginT != "")
                    strQuery += " and convert(varchar(12),a.CreateDate,23)>='" + BeginT + "'";
                
                if (EndT != "")
                    strQuery += " and convert(varchar(12),a.CreateDate,23)<='" + EndT + "'";

                //求数量及金额总和
                DataTable dtTtl = new DataTable();
                string strQueryTtl = "select sum(sttlCount) as ttlCount,sum(sttlTotalPrice) as ttlFee from (" + strQuery + ") as tempTable";


                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand();
                comm.CommandText = strQuery;
                dt = SqlHelper.PagerWithCommand(comm, pageIndex, pageCount, orderBy, ref TotalCount);

                StringBuilder sb = new StringBuilder();
                if (dt.Rows.Count == 0)
                {
                    sb.Append("{");
                    sb.Append("totalCount:");
                    sb.Append(TotalCount.ToString());
                    sb.Append(",data:");
                    sb.Append("0");
                    sb.Append("}");
                }
                else
                {
                    sb.Append(JsonClass.FormatDataTableToJson(dt, TotalCount));
                    dtTtl = SqlHelper.ExecuteSql(strQueryTtl);
                    if (dtTtl.Rows.Count > 0)
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(",");
                        sb.Append("ttlCount:");
                        sb.Append(dtTtl.Rows[0][0].ToString());
                        sb.Append(",");
                        sb.Append("ttlFee:");
                        sb.Append(dtTtl.Rows[0][1].ToString());
                        sb.Append("}");
                    }
                }
                context.Response.Write(sb.ToString());
                context.Response.End();
            }
        }
    }
      
    //查询详细信息
    private void SearchPaySettleDetail(HttpContext context)
    {
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string headid = context.Request.Form["headid"].ToString();
        context.Response.ContentType = "text/plain";
        DataTable dt = new DataTable();
        //yxx
        //string strQuery = " select c.payId as id,convert(varchar(12),a.CreateDate,23) CreateDate, c.SendNo, p.CustName as CustName, c.sttlPrice,c.sttlCount,c.sttlTotalPrice,c.SttlRemark," +
        //    " f.sttlCount as sttlCounts,f.SttlTotalPrice as sttlTotalPrices,(Case a.billStatus when 1 then '制单' when 2 then '确认' when 9 then '关闭' else '其他' end) billstatus," +
        //    " f.TaxPrice as TaxPrice,f.ProductCount as ProductCount,c.ProductID " +
        //    " from jt_xsfp a  " +
        //    "   left join  officedba.CustInfo p on p.Id=a.CustID  " +
        //   "   left join Jt_xsfp_mx c on  a.id=c.xsfpId " +
        //   "   left join jt_xsfh d on d.sendNO=c.sendNO " +
        //   "   left join jt_xsfh_mx f on f.sendNO = d.id and f.id = c.payId " +
        //   "   where c.CompanyCD='" + CompanyCD + "' and a.id=" + headid;
        string strQuery = " select c.payId as id,convert(varchar(12),a.CreateDate,23) CreateDate, c.SendNo, p.CustName as CustName, c.sttlPrice,c.sttlCount,c.sttlTotalPrice,c.SttlRemark," +
            " f.sttlCount as sttlCounts,f.SttlTotalPrice as sttlTotalPrices,(Case a.billStatus when 1 then '制单' when 2 then '确认' when 9 then '关闭' else '其他' end) billstatus," +
            " f.TaxPrice as TaxPrice,f.ProductCount as ProductCount,c.ProductID " +
            " from jt_xsfp a left join  officedba.CustInfo p on p.Id=a.CustID " +
            " left join Jt_xsfp_mx c on  a.id=c.xsfpId  left join jt_xsfh_mx f on f.id = c.payId " +
            " where c.CompanyCD='" + CompanyCD + "' and c.xsfpId = " + headid;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            dt = XBase.Data.DBHelper.SqlHelper.ExecuteSql(strQuery);
            sb.Append("{");
            sb.Append("data:");
            sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}"); 
        }
        catch (Exception ex)
        {

        }
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    
    
   //插入销售结算
    
    private void InsertPaySettle(HttpContext context)
    {
        
        //是否保存成功
        string strMsg = string.Empty;//操作返回的信息  
        string tempID = "0";

        tempID = context.Request.Params["headid"].ToString().Trim();
        string jcQual = "true";

        JsonClass jc;
        bool isSucc = false;
        int ID = int.Parse(tempID);
        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
         string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();
         SqlCommand commandd = new SqlCommand();

        ArrayList lstCmd = new ArrayList();
    
        if (jcQual == "true")
        {

            if (ID > 0)
            {
                string strup = "   update  dbo.Jt_xsfp  set ModifiedDate='" + CreatorDate + "',ModifiedUserID='" + userid + "' where ID='" + ID.ToString() + "' ";
                SqlCommand command1 = new SqlCommand(strup);

                lstCmd.Add(command1);

                string[] strarray = null;
                string recorditems = "";
                string[] inseritems = null;
                string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
                strarray = strfitinfo.Split('|');

                //更新表体，先删除原有表体数据，再执行增加功能
                string strdeldetails = " delete   from  Jt_xsfp_mx where xsfpId='" + ID.ToString() + "' ";
                SqlCommand command_deldetail = new SqlCommand(strdeldetails);
                lstCmd.Add(command_deldetail);
               
                for (int i = 0; i < strarray.Length; i++)
                {
                    recorditems = strarray[i];
                    inseritems = recorditems.Split(',');
                    if (recorditems.Length != 0)
                    {
                        string ArriveNo = inseritems[0].ToString();//单据编号
                        string Id = inseritems[1].ToString();
                        string CustName = inseritems[2].ToString();
                        string Num = inseritems[3].ToString();//单据数量
                        // string TaxPrice = inseritems[4].ToString();//单据单价
                        string Quantity = inseritems[5].ToString();//结算数量
                        decimal UnitCost = decimal.Parse(inseritems[6].ToString());//结算单价
                        string Money = inseritems[7].ToString();//结算总价
                        string CreateDate = inseritems[8].ToString();//单据创建日期
                        string SttlRemark = inseritems[9].ToString();//结算备注
                        string ProductId = inseritems[10].ToString(); //商品的编号
                     
                        string strsqls =
                       "  insert into Jt_xsfp_mx(CompanyCD,SendNo,SortNo,ProductID,SttlPrice,SttlTotalPrice,SttlCount,SttlRemark,xsfpId,payId)" +
                      "  values('" + CompanyCD + "','" + ArriveNo + "','" + (i + 1).ToString() + "','" + ProductId + "','"
                      + UnitCost + "','" + Money + "','" + Quantity + "','" + SttlRemark + "','" + ID.ToString() + "'," + Id + ")";

                        SqlCommand command_updetails = new SqlCommand(strsqls);
                        lstCmd.Add(command_updetails);
                    }
                }
                string strlog = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125','" + ID.ToString() + "','dbo.Jt_xsfp,dbo.Jt_cgfp_mx','修改','操作成功')";
                //SqlHelper.ExecuteSql(strlog);
                SqlCommand command2 = new SqlCommand(strlog);
                lstCmd.Add(command2);
            }
            else
             {
                //插入数据库
                 string custId = context.Request.Form["custId"].ToString();
                 string getNo="dbo.f_GetNo()";
                 string strsql = "insert into Jt_xsfp(CompanyCD,Creator,CreateDate,BillStatus,ModifiedUserID,XsfpNo,custId)" +
                      "  values('" + CompanyCD + "','" + Creator + "','" + CreatorDate + "','1','" + userid + "',"+getNo+","+custId+") set @ID=@@IDENTITY";

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
                        string ArriveNo = inseritems[0].ToString();//单据编号
                        string Id = inseritems[1].ToString();
                        string CustName = inseritems[2].ToString();
                        string Num = inseritems[3].ToString();//单据数量
                        // string TaxPrice = inseritems[4].ToString();//单据单价
                        string Quantity = inseritems[5].ToString();//结算数量
                        decimal UnitCost = decimal.Parse(inseritems[6].ToString());//结算单价
                        string Money = inseritems[7].ToString();//结算总价
                        string CreateDate = inseritems[8].ToString();//单据创建日期
                        string SttlRemark = inseritems[9].ToString();//结算备注
                        string ProductId = inseritems[10].ToString(); //商品的编号                        
                        string strsqls = " declare @Id varchar(20) set @Id=(select max(ID) AS ID from dbo.Jt_xsfp)" +
                        "  insert into dbo.Jt_xsfp_mx (CompanyCD,SendNo,SortNo,ProductID,SttlPrice,SttlTotalPrice,SttlCount,SttlRemark,payId,xsfpId)" +
                       "  values('" + CompanyCD + "','" + ArriveNo + "','" + (i + 1).ToString() + "','" + ProductId + "','"
                       + UnitCost + "','" + Money + "','" + Quantity + "','" + SttlRemark + "',"+Id+",@Id)";
                        SqlCommand command_updetails = new SqlCommand(strsqls);
                        lstCmd.Add(command_updetails);
                    }
                }
                string strlog = "  declare @Id varchar(20) set @Id=(select max(ID) AS ID from Jt_xsfp ) " +
                             "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                                            "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125',@Id,'Jt_xsfp，Jt_xsfp_mx','新建','操作成功')";
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
                    jc = new JsonClass("保存成功", ID.ToString(), ID);
                }
                else
                {
                    jc = new JsonClass("保存成功", ID.ToString(), int.Parse(tempID));
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

    //确认销售信息
    private void ConfirmSell(HttpContext context)
    {

        string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        string Creator = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID.ToString();
        string CreatorDate = DateTime.Now.ToShortDateString();
        string employeeid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString();
        string employeeName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName.ToString();
        
        JsonClass jc;
        string tempID = "0";
        tempID = context.Request.Params["headid"].ToString().Trim();

        bool isSucc = false;//是否保存成功

        ArrayList lstCmd = new ArrayList();
        string strconfirm;
         strconfirm = "update Jt_xsfp set BillStatus=2,confirmor='" + employeeid + "',confirmdate='" + CreatorDate + "' where id='" + tempID + "' ";
        
        
        SqlCommand commandDel = new SqlCommand(strconfirm);
        lstCmd.Add(commandDel);
        //记录删除日志
        string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125','" + tempID + "','Jt_xsfp','销售发票确认','操作成功')";
        SqlCommand command2 = new SqlCommand(deleLogSql);
        lstCmd.Add(command2);             
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
        strarray = strfitinfo.Split('|');
        
        string quantity;
        string TotalMoney;
      
        for (int i = 0; i < strarray.Length; i++)
        {
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                string ArriveNo = inseritems[0].ToString();//单据编号
                string Id = inseritems[1].ToString();//单据id
                decimal Quantity = decimal.Parse(inseritems[5].ToString());//结算数量
                decimal UnitCost = decimal.Parse(inseritems[6].ToString());//结算单价
                decimal Money = decimal.Parse( inseritems[7].ToString());//结算总价
                string CreateDate = inseritems[8].ToString();//单据创建日期
                //查询的销售发货，还是销售发票  string sql = @"select SttlCount,SttlTotalPrice from  jt_xsfp_mx where Id = '" + Id + "'";//
                //string sql = @"select SttlCount,SttlTotalPrice from Jt_xsfp_mx where payid= '" + Id + "'";//yxx
                string sql = @"select SttlCount,SttlTotalPrice from jt_xsfh_mx where id= '" + Id + "'";                 
                DataTable dt1 = SqlHelper.ExecuteSql(sql);
                // DataTable dt = SqlHelper.ExecuteSql(sql2);
                decimal qutiay =  Convert.ToDecimal(  (dt1.Rows[0]["SttlCount"]));
                decimal total =  Convert.ToDecimal(  (dt1.Rows[0]["SttlTotalPrice"]));
                quantity = (qutiay + Quantity).ToString("f2");                 
               TotalMoney=(total + Money).ToString("f2");
               string update = "update  jt_xsfh_mx set SttlCount='" + quantity + "',SttlTotalPrice='"+TotalMoney+"'  where id='" + Id + "' ";

               SqlCommand command = new SqlCommand(update);
               lstCmd.Add(command);
             }
        }        
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
            jc = new JsonClass("确认成功",employeeName, int.Parse(tempID));
        }
        else
        {
            jc = new JsonClass("确认失败", "销售发票", 0);
        }
        context.Response.Write(jc);
    }


    //取消确认信息
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
        string strconfirm;
        ArrayList lstCmd = new ArrayList();
     
        strconfirm = "update dbo.Jt_xsfp set BillStatus=1,confirmor=null,confirmdate=null   where id='" + tempID + "' ";
        SqlCommand commandDel = new SqlCommand(strconfirm);
        lstCmd.Add(commandDel);

        //记录删除日志

        string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125','" + tempID + "','Jt_xsfp','销售发票取消确认','操作成功')";
        SqlCommand command2 = new SqlCommand(deleLogSql);
        lstCmd.Add(command2);
        string[] strarray = null;
        string recorditems = "";
        string[] inseritems = null;
        string strfitinfo = context.Request.Params["strfitinfo"].ToString().Trim();
        strarray = strfitinfo.Split('|');
        string quantity;

        string TotalMoney;

        for (int i = 0; i < strarray.Length; i++)
        {
            recorditems = strarray[i];
            inseritems = recorditems.Split(',');
            if (recorditems.Length != 0)
            {
                string ArriveNo = inseritems[0].ToString();//单据编号
                string Id = inseritems[1].ToString();//单据id
                //     string CustName = inseritems[2].ToString();
                //   string Num = inseritems[3].ToString();//单据数量
                // string TaxPrice = inseritems[4].ToString();//单据单价
                decimal Quantity = decimal.Parse(inseritems[5].ToString());//结算数量
                decimal UnitCost = decimal.Parse(inseritems[6].ToString());//结算单价
                decimal Money = decimal.Parse(inseritems[7].ToString());//结算总价
                string CreateDate = inseritems[8].ToString();//单据创建日期
                //string sql = @"select SttlCount,SttlTotalPrice from Jt_xsfp_mx where payid='" + Id + "'";
                string sql = @"select SttlCount,SttlTotalPrice from jt_xsfh_mx where id= '" + Id + "'";
                DataTable dt1 = SqlHelper.ExecuteSql(sql);
                // DataTable dt = SqlHelper.ExecuteSql(sql2);
                decimal qutiay = Convert.ToDecimal((dt1.Rows[0]["SttlCount"]));
                decimal total = Convert.ToDecimal((dt1.Rows[0]["SttlTotalPrice"]));
                quantity = (qutiay - Quantity).ToString("f2");
                TotalMoney = (total -Money).ToString("f2");
                string update = "update  jt_xsfh_mx set SttlCount='" + quantity + "',SttlTotalPrice='" + TotalMoney + "'  where id='" + Id + "' ";

                SqlCommand command = new SqlCommand(update);
                lstCmd.Add(command);
            }
        }        
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
            jc = new JsonClass("取消确认成功", "销售发票", int.Parse(tempID));
        }
        else
        {
            jc = new JsonClass("取消确认失败", "销售发票", 0);
        }
        context.Response.Write(jc);
    }

    //销售发票关闭
    private void CloseConfim(HttpContext context)
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
        string strconfirm = "update Jt_xsfp set BillStatus=9  where id='" + tempID + "' ";
        SqlCommand commandDel = new SqlCommand(strconfirm);
        lstCmd.Add(commandDel);

        //记录删除日志

        string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125','" + tempID + "','Jt_xsfp','销售发票关闭','操作成功')";
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

            jc = new JsonClass("关闭成功", "销售发票", int.Parse(tempID));

        }
        else
        {
            jc = new JsonClass("关闭失败", "销售发票", 0);
        }
        context.Response.Write(jc);


    }






    //取消销售发票关闭
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
        string strconfirm = "update Jt_xsfp set BillStatus=2 where id='" + tempID + "' ";
        SqlCommand commandDel = new SqlCommand(strconfirm);
        lstCmd.Add(commandDel);

        //记录删除日志

        string deleLogSql = "  insert into officedba.ProcessLog(companycd,userid,operatedate,moduleid,objectid,objectname,element,remark)" +
                           "  values('" + CompanyCD + "','" + userid + "','" + DateTime.Now.ToString() + "','8880125','" + tempID + "','Jt_xsfp','销售发票取消关闭','操作成功')";
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

            jc = new JsonClass("取消关闭成功", "销售发票", int.Parse(tempID));

        }
        else
        {
            jc = new JsonClass("取消关闭失败", "销售发票", 0);
        }
        context.Response.Write(jc);


    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}