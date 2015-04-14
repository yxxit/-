<%@ WebHandler Language="C#" Class="Disassembling" %>

using System;
using System.Web;
using System.Data;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Common;
using System.Collections.Generic;
using XBase.Business.Common;


public class Disassembling : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) {
        if (context.Request.RequestType == "POST")
        {
            string action = (context.Request.Form["action"].ToString());//操作
            if (action == "GetBom")//弹出层信息
            {
                GetBom(context);//根据Bom查询子健
            }
            else if (action == "edit")//获取基本信息和根据传过来的DetailID数组获取明细
            {
                editbill(context);//保存或修改
            }
            else if (action == "Confirm")
            {
                confirm(context);//确认
            }
            else if (action == "UnConfirm")
            {
                Unconfirm(context);//反确认
            }
            else if (action == "Close")//结单
            {
                close(context);
            }
            else if (action == "CancelClose")//取消结单
            {
                CancelClose(context);
            }
            
            else
            {
                GetBillByID(context);//查询单据
            }

        }
    }
    public DataTable dt = new DataTable();
    public int count = 0;
    public string ismore = "";
    private void GetBillByID(HttpContext context)
    {
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        DataTable getbill = new DataTable();
        getbill = DisassemblingBus.GetBillByID(ID.ToString());
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");
        sb.Append("data:");
        if (getbill.Rows.Count == 0)
            sb.Append("[{\"id\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(getbill));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
        
    }
    private void del(HttpContext context)
    {
        JsonClass jc = new JsonClass();
        string strID = context.Request.Params["strID"].ToString();
        string[] IDArray = null;
        IDArray = strID.Split(',');
        bool ifdele = true;
        int delcout = 0;
        for (int i = 0; i < IDArray.Length; i++)
        {
            if (!DisassemblingBus.isdel(IDArray[i]))
            {
                ifdele = false;
                break;
            }
        }

        if (ifdele == true)
        {
            for (int i = 0; i < IDArray.Length; i++)
            {

                if (DisassemblingBus.DeleteIt(IDArray[i]))
                {
                    delcout += 1;
                }
            }
            if (delcout > 0)
            {
                jc = new JsonClass("删除成功", "", 1);
            }
            else
            {
                jc = new JsonClass("删除失败", "", 0);
            }
        }
        else
        {
            jc = new JsonClass("已确认后的单据不允许删除！", "", 0);

        }
        context.Response.Write(jc);
    }
    //结单
    private void close(HttpContext context)
    {
        JsonClass jc = new JsonClass();
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        if (DisassemblingBus.CloseBill(ID.ToString()))
        {
            jc = new JsonClass("结单成功", LoginUserName + " | " + Date, 1);
        }
        else
        {
            jc = new JsonClass("结单失败", "", 0);
        }
        context.Response.Write(jc);
    }
    //取消结单
    private void CancelClose(HttpContext context)
    {
        JsonClass jc = new JsonClass();
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        if (ID > 0)
        {
            if (DisassemblingBus.CancelCloseBill(ID.ToString()))
            {
                jc = new JsonClass("取消结单成功", LoginUserName + " | " + Date, 1);
            }
            else
            {
                jc = new JsonClass("取消结单失败", "", 0);
            }
        }
        context.Response.Write(jc);
    }
    //确认
    private void confirm(HttpContext context)
    {
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        JsonClass jc = new JsonClass();
        int ID = int.Parse(context.Request.Params["ID"].ToString());
        
        string Msg = string.Empty;
        if (ID > 0)
        {
            //string TotalPrice = context.Request.Params["TotalPrice"].ToString();

            DisassemblingModel model = new DisassemblingModel();
            model.ID = ID;
            model.companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            model.BillType = int.Parse(context.Request.Params["billtype"].ToString());
            string proname="";
            if (!DisassemblingBus.ISConfirmBill(model,true,out proname))
            {
                jc = new JsonClass(proname+"库存不足", "", 0);
                context.Response.Write(jc);
                return;
            }
            else
            {
                if (DisassemblingBus.ConfirmBill(model, out Msg))
                {
                    jc = new JsonClass(Msg, LoginUserName + "|" + Date, 1);
                }
                else
                {
                    jc = new JsonClass(Msg, "", 0);
                }
            }
        }
        else
        {
            jc = new JsonClass("请求单据不存在", "", 0);
        }
        context.Response.Write(jc);
    }
    //反确认
    private void Unconfirm(HttpContext context)
    {
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        JsonClass jc = new JsonClass();
        int ID = int.Parse(context.Request.Params["ID"].ToString());

        string Msg = string.Empty;
        if (ID > 0)
        {
            //string TotalPrice = context.Request.Params["TotalPrice"].ToString();

            DisassemblingModel model = new DisassemblingModel();
            model.ID = ID;
            model.companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            model.BillType = int.Parse(context.Request.Params["billtype"].ToString());
            string proname = "";
            //if (!DisassemblingBus.ISConfirmBill(model, false, out proname))
            //{
            //    jc = new JsonClass(proname + "库存不足", "", 0);
            //    context.Response.Write(jc);
            //    return;
            //}
            //else
            //{
                if (DisassemblingBus.UnConfirmBill(model, out Msg))
                {
                    jc = new JsonClass(Msg, LoginUserName + "|" + Date, 1);
                }
                else
                {
                    jc = new JsonClass(Msg, "", 0);
                }
            //}
            
        }
        else
        {
            jc = new JsonClass("请求单据不存在", "", 0);
        }
        context.Response.Write(jc);
    }
    private void editbill(HttpContext context)
    {
        int loginUser_id = int.Parse(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID.ToString());
        string LoginUserName = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;//登陆用户名
        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
        JsonClass jc = new JsonClass();
      
        int ID = int.Parse(context.Request.Params["ID"].Trim());
        string BomID = context.Request.Params["BomID"].Trim();
        string TotalPrice = context.Request.Params["TotalPrice"].Trim();
        string txtExecutor = context.Request.Params["txtExecutor"].Trim();
        string remark = context.Request.Params["remark"].Trim();
        string createrID = context.Request.Params["createrID"].Trim();

        string createDate = context.Request.Params["createDate"].ToString().Trim();//物品ID
        string txtDeptID = context.Request.Params["txtDeptID"].ToString().Trim();//序号
        string billtype = context.Request.Params["billtype"].ToString().Trim();//单位

        //string DetailFromBillID = context.Request.Params["DetailFromBillID"].ToString().Trim();//
        //string DetailFromType = context.Request.Params["DetailFromType"].ToString().Trim();//
        string DetailProductID = context.Request.Params["DetailProductID"].ToString().Trim();//单价
        string DetailUnitID = context.Request.Params["DetailUnitID"].ToString().Trim();//数量
        string DetailStorageID = context.Request.Params["DetailStorageID"].ToString().Trim();//总价
        string Detailtpyes = context.Request.Params["Detailtpyes"].ToString().Trim();//
        string DetailProductCount = context.Request.Params["DetailProductCount"].ToString().Trim();//备注

        string DetailUnitPrice = context.Request.Params["DetailUnitPrice"].ToString().Trim();//单位
        string DetailTotalPrice = context.Request.Params["DetailTotalPrice"].ToString().Trim();//数量
        string Detailquota = context.Request.Params["quota"].ToString().Trim();//实际单价
        string DetailBatchNo = context.Request.Params["DetailBatchNo"].ToString().Trim();//换算率


        DisassemblingModel model = new DisassemblingModel();
        model.companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//[待修改] 
        model.ID = int.Parse(ID.ToString());
        model.BillNo = context.Request.Params["txtInNo"].ToString().Trim();
        model.BillType = int.Parse(billtype);
        model.BomID = int.Parse(BomID);

        model.CreatDate = DateTime.Parse(createDate);
        model.creater = int.Parse(createrID);
        model.departmentID = int.Parse(txtDeptID);
        model.HandsManID = int.Parse(txtExecutor);
        model.TotalPrice = decimal.Parse(TotalPrice);
        model.Remark = remark;

        List<DisassemblingListModel> modellist = new List<DisassemblingListModel>();
        string[] ProductID = DetailProductID.Split(',');
        string[] UnitID = DetailUnitID.Split(',');
        string[] StorageID = DetailStorageID.Split(',');
        string[] tpyes = Detailtpyes.Split(',');
        string[] ProductCount = DetailProductCount.Split(',');
        string[] unitPrice = DetailUnitPrice.Split(',');
        string[] totalPrice = DetailTotalPrice.Split(',');
        string[] quota = Detailquota.Split(',');
        string[] BatchNo = DetailBatchNo.Split(',');

        if (ProductID.Length >= 1)
        {

            for (int i = 0; i < ProductID.Length; i++)
            {
                DisassemblingListModel model1 = new DisassemblingListModel();
                model1.BillsNo = model.BillNo;
                model1.Amount = decimal.Parse(totalPrice[i]);
                model1.Batch = BatchNo[i];
                model1.Productid = int.Parse(ProductID[i]);
                model1.UsedCount = decimal.Parse(ProductCount[i]);
                model1.UnitID = int.Parse(UnitID[i]);
                model1.Price = decimal.Parse(unitPrice[i]); ;
                model1.Quota = decimal.Parse(quota[i]); ;
                model1.Storageid = int.Parse(StorageID[i]);
                model1.Types = int.Parse(tpyes[i]);
                model1.companyCD = model.companyCD;

                modellist.Add(model1);
            }
        }

        if (ID > 0)
        {
            #region 修改其他入库
            //获取页面初始时明细中的入库数量，源单行号，源单编号


            if (DisassemblingBus.Update(model, modellist))
            {
                jc = new JsonClass("保存成功", LoginUserName + "|" + Date, 1);
            }
            else
            {
                jc = new JsonClass("保存失败", "", 0);
            }
            #endregion
        }
        else
        {
            //StorageInOtherModel model = new StorageInOtherModel();
            //model.CompanyCD = companyCD;
            if (context.Request.Params["bmgz"].ToString().Trim() == "zd")
            {
                model.BillNo = ItemCodingRuleBus.GetCodeValue(context.Request.Params["txtInNo"].ToString().Trim(), "Disassembling", "billno");
                if (model.BillNo == "")
                {
                    jc = new JsonClass("该单据编号规则自动生成的序号已经达到上限，请检查编号规则设置！", "", 2);
                    context.Response.Write(jc);
                    context.Response.End();
                }
            }
            else
            {
                model.BillNo = context.Request.Params["txtInNo"].ToString().Trim();
            }
            //if (context.Request.Params["pcgz"].ToString().Trim() == "zd")
            //{
            //    BratchNo = XBase.Business.Office.SystemManager.BatchNoRuleSetBus.GetCodeValue(BratchNo);
            //}
            bool isExsist = DisassemblingBus.exsist(model.BillNo);
            if (!isExsist)
            {
                jc = new JsonClass("该编号已被使用，请输入未使用的编号！", "", 2);
            }
            else
            {
               
                int IndexIDentity = 0;
                string NewBratchNo = "";
                if (DisassemblingBus.Add(model, modellist, out IndexIDentity))
                {
                    jc = new JsonClass("保存成功", IndexIDentity.ToString() + "|" + model.BillNo.ToString() + "|" + LoginUserName + "|" + Date + "|" + NewBratchNo, 1);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                //}

            }//model.InNo = ItemCodingRuleBus.GetCodeValue(context.Request.Params["txtInNo"].ToString().Trim());
        }
        context.Response.Write(jc);
    }
    private void GetBom(HttpContext context)
    {

        string id = context.Request.Form["BomID"].ToString();
        string isall = context.Request.Form["isall"].ToString();
        if (isall != "")
        {
            ismore = "Yes";
        }
        GetBom(id);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("{");

        sb.Append("data:");
        if (dt.Rows.Count == 0)
            sb.Append("[{\"typename\":\"\"}]");
        else
            sb.Append(JsonClass.DataTable2Json(dt));
        sb.Append("}");
        context.Response.ContentType = "text/plain";
        context.Response.Write(sb.ToString());
        context.Response.End();
    }
    private void GetBom(string id)
    {
        DataTable getdt = DisassemblingBus.GetBOM(id);
        for (int i = 0; i < getdt.Rows.Count; i++)
        {
            if (count == 0)
            {
                dt = getdt.Clone();
                count++; 
            }
            DataRow dr = dt.NewRow();
            for (int j = 0; j < getdt.Columns.Count; j++)
            {
                
                dr[j] = getdt.Rows[i][j].ToString();
                
            }
            dt.Rows.Add(dr);
        }
        GetBomdetail(id);
        if (ismore == "Yes")
        {
            GetBomid(id);
        }
    }
    private void GetBomid(string id)
    {
        DataTable dtid = DisassemblingBus.GetBOMID(id);
        if (dtid.Rows.Count > 0)
        {
            for (int i = 0; i < dtid.Rows.Count; i++)
            {
                GetBomdetail(dtid.Rows[i][0].ToString());
                 if (ismore == "Yes")
                 {
                     GetBomid(dtid.Rows[i][0].ToString());
                 }
            }
        }
       
    }
    private void GetBomdetail(string id)
    {
        DataTable getdt = DisassemblingBus.GetBOMinfo(id);
        for (int i = 0; i < getdt.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j < getdt.Columns.Count; j++)
            {
               
                dr[j] = getdt.Rows[i][j].ToString();
               
            }
            dt.Rows.Add(dr);
        }
        
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}