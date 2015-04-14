<%@ WebHandler Language="C#" Class="ProductInfoAdd" %>

using System;
using System.Web;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.IO;
using XBase.Common;
using XBase.Business.Common;
using XBase.Data.Office.SupplyChain;
public class ProductInfoAdd : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        //string companyCD = "AAAAAA";//[待修改]

        string Action = context.Request.Params["Action"].ToString().Trim();
        JsonClass jc;
        if (Action == ActionUtil.Del.ToString())
        {
            string strID = context.Request.QueryString["str"].ToString().Trim();
            strID = strID.Substring(0, strID.Length);
            string Fno = "";

            string[] ProductsNo = strID.Split(',');
            for (int i = 0; i < ProductsNo.Length; i++)
            {
                string ID = ProductsNo[i].ToString();
                string[] No = ID.Split('|');
                string Fsta = No[1].ToString();
                string flowflag = No[0].ToString();
                Fno += flowflag + ",";
                
                //if (Fsta != "草稿")
                //{
                //    jc = new JsonClass("只有审核状态为草稿时才可以删除，请检查数据", "", 0);
                //    context.Response.Write(jc);
                //    return;
                //}
                //else
                //{
                //    string flowflag = No[0].ToString();
                //    Fno += flowflag + ",";
                //}   //改为审核状态下也可以删除
            }
            Fno = Fno.Substring(0, Fno.Length - 1);
            string  Reason = ProductInfoBus.Existss(Fno);
            // string Reason = "false";
            if (Reason!="false")
            {
                jc = new JsonClass(Reason, "", 0);
            }
            else
            {
                if (ProductInfoBus.DeleteProductInfo(Fno))
                {
                    jc = new JsonClass("删除成功", "", 1);
                }
                else
                {
                    jc = new JsonClass("删除失败", "", 0);
                }
            }
        
            context.Response.Write(jc);
        }



        //david add 2012 08 16
        //覆盖拼音码
        //传入参数：action和str2：选中复选框的信息
        //返回值：jasonclass
        else if (Action == "Update")
        {
            string strID = context.Request.QueryString["str2"].ToString().Trim();
            strID = strID.Substring(0, strID.Length);
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码

            string[] ProductsNo = strID.Split(',');
            int Fno = 0;
            string Fna = "";

            Boolean k = false;
            for (int i = 0; i < ProductsNo.Length; i++)
            {
                string ID = ProductsNo[i].ToString();
                string[] No = ID.Split('|');
                string flowflag = No[0].ToString();
                Fno = Int32.Parse(flowflag);
                string name = No[2].ToString();
                Fna = name;
                int no = Int32.Parse(Fno.ToString());
                string na = Fna.Substring(0, Fna.Length - 1);
                string PYShort = "";                
                PYShort = XBase.Common.PYShortUtil.GetPYString(Fna);
                k = ProductInfoDBHelper.UpdateProductPYShort(PYShort, no, CompanyCD);
            }
            if (k)
            {
                jc = new JsonClass("生成成功", "", 1);
            }
            else
            {
                jc = new JsonClass("生成失败", "", 0);
            }
            context.Response.Write(jc);
        }

        ////david add 2012 08 16
        ////没有拼音码的插入，有的则没有操作
        ////传入参数：action和str2：选中复选框的信息
        ////返回值：jasonclass
        //else if (Action == "fUpdate")
        //{
        //    string strID = context.Request.QueryString["str2"].ToString().Trim();
        //    strID = strID.Substring(0, strID.Length);
        //    string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 

        //    string Fno = "";
        //    string Fna = "";

        //    string[] ProductsNo = strID.Split(',');
        //    for (int i = 0; i < ProductsNo.Length; i++)
        //    {
        //        string ID = ProductsNo[i].ToString();
        //        string[] No = ID.Split('|');
        //        string flowflag = No[0].ToString();
        //        Fno += flowflag + ",";
        //        string name = No[2].ToString();
        //        Fna += name + ",";
        //    }
        //    Fno = Fno.Substring(0, Fno.Length - 1);
        //    Fna = Fna.Substring(0, Fna.Length - 1);
        //    Boolean k = ProductInfoBus.JudgePY(Fno, Fna,CompanyCD);
        //    if (k)
        //    {
        //        jc = new JsonClass("生成成功", "", 1);
        //    }
        //    else
        //    {
        //        jc = new JsonClass("已有拼音码，无需生成", "", 0);
        //    }
        //    context.Response.Write(jc);
        //}


        else if (Action == "extValue")
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            string strKey = context.Request.Form["keyList"].ToString().Trim();
            string strProNo = context.Request.Form["ProNo"].ToString().Trim();
            strKey = strKey.Substring(1, strKey.Length - 1);
            strKey = strKey.Replace('|', ',');
            DataTable dt = ProductInfoBus.GetExtAttrValue(strKey, strProNo, CompanyCD);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("totalCount:");
            sb.Append(dt.Rows.Count.ToString());
            sb.Append(",data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");

            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
            return;

        }
        else if (Action == "ChangeStatus")
        {
            int ID = int.Parse(context.Request.QueryString["ProductID"].ToString());
            string CheckDate = context.Request.QueryString["CheckDate"].ToString().Trim();
            string CheckUser = context.Request.QueryString["CheckUser"].ToString().Trim();
            string StorageID = context.Request.QueryString["StorageID"].ToString().Trim();
            if (ProductInfoBus.UpdateStatus(ID, "1", CheckUser, CheckDate, StorageID))
            {
                jc = new JsonClass("审核成功", "", 1);
            }
            else
            {
                jc = new JsonClass("审核失败", "", 0);
            }
            context.Response.Write(jc);
        }
        else if (Action == "Add" || Action == "Edit")   //20140401 刘锦旗
        {
            //Hashtable ht = GetExtAttr(context);
            Hashtable ht = null;
            #region 添加其他往来单位
            
            ProductInfoModel Model = new ProductInfoModel();
            Model.OldNo = context.Request.Params["OldNo"].ToString().Trim();
            
            if (Action == "Add")
            {
                string ProNo = "";
                string CodeType = context.Request.Params["CodeType"].ToString().Trim();
                //if (action == "Add")
                //{
                if (!string.IsNullOrEmpty(CodeType))
                {
                    ProNo = ItemCodingRuleBus.GetCodeValue(CodeType, "ProductInfo", "ProdNo");
                }
                else
                {
                    ProNo = context.Request.Params["ProdNo"].ToString().Trim();//合同编号  
                    //bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductInfo"
                    //         , "ProdNo", ProNo);
                    ////存在的场合
                    //if (!isAlready)
                    //{
                    //    jc = new JsonClass("物品档案编码已经存在", "",0);
                    //    context.Response.Write(jc);
                    //    return;
                    //}
                    //Model.ProdNo = ProNo;
                }
                bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductInfo"
                              , "ProdNo", ProNo);

                //存在的场合
                if (!isAlready)
                {
                    jc = new JsonClass("物品档案编码已经存在", "", 0);
                    context.Response.Write(jc);
                    return;
                }

                //验证条码的唯一性
                string BarCode = context.Request.Params["BarCode"].ToString().Trim();
                if (!string.IsNullOrEmpty(BarCode))
                {
                    //为true，已经存在
                    if (ProductInfoBus.CheckBarCode(BarCode, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD))
                    {
                        jc = new JsonClass("物品条码已经存在", "", 0);
                        context.Response.Write(jc);
                        return;
                    }
                }
                Model.ProdNo = ProNo;
                //}
                //else
                //{
                //    model.RejectNo = context.Request.Params["arriveNo"].ToString().Trim();//合同编号 
                //}

                //判断是否存在

                //else
                //{
            }
            else if (Action == "Edit")
            {
                Model.ProdNo = context.Request.Params["ProdNo"].ToString().Trim();

                if (Model.ProdNo != Model.OldNo)
                {
                    bool isAlready = PrimekeyVerifyBus.CheckCodeUniq("ProductInfo"
                                 , "ProdNo", Model.ProdNo);
                    //存在的场合
                    if (!isAlready)
                    {
                        jc = new JsonClass("物品档案编码已经存在", "", 0);
                        context.Response.Write(jc);
                        return;
                    }

                }
            }
            string companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            Model.CompanyCD = companyCD;
            Model.PYShort = context.Request.Params["PYShort"].ToString().Trim();
            Model.ProductName = context.Request.Params["ProductName"].ToString().Trim().Replace('"', '“');
            Model.ShortNam = context.Request.Params["ShortNam"].ToString().Trim();
            //Model.CASRegi = context.Request.Params["CASRegi"].ToString().Trim();
            //Model.EngName = context.Request.Params["EngName"].ToString().Trim();
            Model.BarCode = context.Request.Params["BarCode"].ToString().Trim();
            Model.TypeID = context.Request.Params["TypeID"].ToString().Trim();
            Model.BigType = context.Request.Params["BigType"].ToString().Trim();
            //Model.GradeID = context.Request.Params["GradeID"].ToString().Trim();
            Model.UnitID = context.Request.Params["UnitID"].ToString().Trim();
            //Model.Brand = context.Request.Params["Brand"].ToString().Trim();
            //Model.ColorID = context.Request.Params["ColorID"].ToString().Trim();
            ///*规格型号特殊符号(+)处理*/
            //string Specification = context.Request.Params["Specification"].ToString().Trim();
            //string tmpSpec = "";
            //for (int i = 0; i < Specification.Length; i++)
            //{
            //    if (Specification[i].ToString() == "＋")
            //    {
            //        tmpSpec = tmpSpec + '+';
            //    }
            //    else
            //    {
            //        tmpSpec = tmpSpec + Specification[i].ToString();
            //    }
            //}

            //if (context.Request.Params["Validity"].Trim() != null && context.Request.Params["Validity"].Trim() != string.Empty)
            //{
            //    Model.Validity = Convert.ToDateTime(context.Request.Params["Validity"].ToString().Trim());//批件有效期
            //}
            //if (context.Request.Params["MedFileDate"].Trim() != null && context.Request.Params["MedFileDate"].Trim() != string.Empty)
            //{
            //    Model.MedFileDate = Convert.ToDateTime(context.Request.Params["MedFileDate"].ToString().Trim());//药品批准文号有效期
            //}
            //if (context.Request.Params["MedCheckDate"].Trim() != null && context.Request.Params["MedCheckDate"].Trim() != string.Empty)
            //{
            //    Model.MedCheckDate = Convert.ToDateTime(context.Request.Params["MedCheckDate"].ToString().Trim());//药品检验报告日期
            //}
            //Model.Specification = tmpSpec.Replace("&#174", "×");
            //Model.Qualitystandard = context.Request.Params["Qualitystandard"].ToString().Trim();
            //Model.Capability = context.Request.Params["Capability"].ToString().Trim();
            //Model.Size = context.Request.Params["Size"].ToString().Trim();
            //Model.Density = context.Request.Params["Density"].ToString().Trim();
            //Model.PieceCount = context.Request.Params["PieceCount"].ToString().Trim();
            //Model.Source = context.Request.Params["Source"].ToString().Trim();
            //Model.FromAddr = context.Request.Params["FromAddr"].ToString().Trim();
            //Model.DrawingNum = context.Request.Params["DrawingNum"].ToString().Trim();
            //Model.ImgUrl = context.Request.Params["ImgUrl"].ToString().Trim();
            //Model.FileNo = context.Request.Params["FileNo"].ToString().Trim();
            //Model.PricePolicy = context.Request.Params["PricePolicy"].ToString().Trim();
            //Model.Params = context.Request.Params["Params"].ToString().Trim();
            //Model.Questions = context.Request.Params["Questions"].ToString().Trim();
            //Model.ReplaceName = context.Request.Params["ReplaceName"].ToString().Trim();
            //Model.Description = context.Request.Params["Description"].ToString().Trim();
            //Model.StockIs = context.Request.Params["StockIs"].ToString().Trim();
            //Model.MinusIs = context.Request.Params["MinusIs"].ToString().Trim();
            Model.StorageID = context.Request.Params["StorageID"].ToString().Trim();
            Model.SafeStockNum = context.Request.Params["SafeStockNum"].ToString().Trim();
            //Model.StayStandard = context.Request.Params["StayStandard"].ToString().Trim();
            Model.MinStockNum = context.Request.Params["MinStockNum"].ToString().Trim();
            Model.MaxStockNum = context.Request.Params["MaxStockNum"].ToString().Trim();
            //Model.ABCType = context.Request.Params["ABCType"].ToString().Trim();
            //Model.CalcPriceWays = context.Request.Params["CalcPriceWays"].ToString().Trim();
            //Model.StandardCost = context.Request.Params["StandardCost"].ToString().Trim();
            //Model.PlanCost = context.Request.Params["PlanCost"].ToString().Trim();
            Model.StandardSell = context.Request.Params["StandardSell"].ToString().Trim();
            //Model.SellMin = context.Request.Params["SellMin"].ToString().Trim();
            //Model.SellMax = context.Request.Params["SellMax"].ToString().Trim();
            Model.TaxRate = context.Request.Params["TaxRate"].ToString().Trim();
            Model.InTaxRate = context.Request.Params["InTaxRate"].ToString().Trim();
            Model.SellTax = context.Request.Params["SellTax"].ToString().Trim();
            //Model.SellPrice = context.Request.Params["SellPrice"].ToString().Trim();
            //Model.TransferPrice = context.Request.Params["TransfrePrice"].ToString().Trim();
            //Model.Discount = context.Request.Params["Discount"].ToString().Trim();
            Model.StandardBuy = context.Request.Params["StandardBuy"].ToString().Trim();
            Model.TaxBuy = context.Request.Params["TaxBuy"].ToString().Trim();
            //Model.BuyMax = context.Request.Params["BuyMax"].ToString().Trim();
            Model.Remark = context.Request.Params["Remark"].ToString().Trim();
            Model.Creator = context.Request.Params["Creator"].ToString().Trim();
            Model.CreateDate = context.Request.Params["CreateDate"].ToString().Trim();
            //Model.CheckStatus = context.Request.Params["CheckStatus"].ToString().Trim();
            //Model.CheckUser = context.Request.Params["CheckUser"].ToString().Trim();
            //Model.CheckDate = context.Request.Params["CheckDate"].ToString().Trim();
            Model.UsedStatus = context.Request.Params["UsedStatus"].ToString().Trim();
            //Model.Manufacturer = context.Request.Params["Manufacturer"].ToString().Trim();
            //Model.Material = context.Request.Params["Material"].ToString().Trim();
            //Model.IsBatchNo = context.Request.Params["IsBatchNo"].ToString().Trim();
            //Model.StorageUnit = context.Request.Params["StorageUnit"].ToString().Trim();
            //Model.SellUnit = context.Request.Params["SellUnit"].ToString().Trim();
            //Model.PurchseUnit = context.Request.Params["PurchseUnit"].ToString().Trim();
            //Model.ProductUnit = context.Request.Params["ProductUnit"].ToString().Trim();
            //Model.GroupNo = context.Request.Params["GroupNo"].ToString().Trim();
            Model.ModifiedDate = System.DateTime.Now.ToString("yyyy-MM-dd");
            //Model.MaxImportPrice = context.Request.Params["MaxImportPrice"].ToString().Trim();
            //Model.MinSalePrice = context.Request.Params["MinSalePrice"].ToString().Trim();
            //Model.bsale = context.Request.Params["bsale"].ToString().Trim();
            //Model.bpurchase = context.Request.Params["bpurchase"].ToString().Trim();
            //Model.bconsume = context.Request.Params["bconsume"].ToString().Trim();
            //Model.baccessary = context.Request.Params["baccessary"].ToString().Trim();
            //Model.bself = context.Request.Params["bself"].ToString().Trim();
            //Model.bproducing = context.Request.Params["bproducing"].ToString().Trim();
            //Model.bservice = context.Request.Params["bservice"].ToString().Trim();
            //Model.Pnumber = context.Request.Params["Pnumber"].ToString().Trim();
            //Model.AbrasionResist = context.Request.Params["AbrasionResist"].ToString().Trim();
            //Model.BalancePaper = context.Request.Params["BalancePaper"].ToString().Trim();
            //Model.BaseMaterial = context.Request.Params["BaseMaterial"].ToString().Trim();
            //Model.SurfaceTreatment = context.Request.Params["SurfaceTreatment"].ToString().Trim();
            //Model.BackBottomPlate = context.Request.Params["BackBottomPlate"].ToString().Trim();
            //Model.BuckleType = context.Request.Params["BuckleType"].ToString().Trim();
            //Model.Pnumberid = context.Request.Params["Pnumberid"].ToString().Trim();
            //Model.AbrasionResistid = context.Request.Params["AbrasionResistid"].ToString().Trim();
            //Model.BalancePaperid = context.Request.Params["BalancePaperid"].ToString().Trim();
            //Model.BaseMaterialid = context.Request.Params["BaseMaterialid"].ToString().Trim();
            //Model.MedCheckNo = context.Request.Params["MedCheckNo"].ToString().Trim();//药品检验报告编号
            //Model.ModifiedUserID = "Admin";
            Model.ModifiedUserID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            Model.HeatPower = context.Request.Params["HeatPower"].ToString().Trim();
            Model.VolaPercent = context.Request.Params["VolaPercent"].ToString().Trim();
            Model.AshPercent = context.Request.Params["AshPercent"].ToString().Trim();
            Model.SulfurPercent = context.Request.Params["SulfurPercent"].ToString().Trim();
            Model.WaterPercent = context.Request.Params["WaterPercent"].ToString().Trim();
            Model.CarbonPercent = context.Request.Params["CarbonPercent"].ToString().Trim();
            
            

            if (Action == "Add")
            {
                string tempID = "0";
                if (ProductInfoBus.InsertProductInfo(Model, out tempID, ht))
                {
                    jc = new JsonClass("保存成功", Model.ProdNo, int.Parse(tempID));
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }
                context.Response.Write(jc);
            }
            else if (Action == "Edit")
            {
                //if (Model.IsBatchNo.Equals("0"))
                //{
                //    decimal totalProductCount = ProductInfoBus.GetProductCountByAllBatchNo(Model.ProdNo, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
                //    if (totalProductCount != 0)
                //    {
                //        jc = new JsonClass("该物品的批次的现有存量没有清零，暂时无法停用批次", "", 0);
                //        context.Response.Write(jc);
                //        return;
                //    }
                //}
                if (ProductInfoBus.UpdateProductInfo(Model, ht))
                //if(ProductInfoDBHelper.UpdateProductInfo(Model,ht))
                {
                    jc = new JsonClass("保存成功", Model.ProdNo, 1);
                }
                else
                {
                    jc = new JsonClass("保存失败", "", 0);
                }

                context.Response.Write(jc);
            }
            //}
            #endregion
        }
        if (Action == "GetGoodsInfoByBarcode")//根据扫描条码获取物品信息
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string Barcode = context.Request.Params["Barcode"].ToString().Trim();//条码


            //int DeptID = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).DeptID;
            //bool flag = ProductInfoBus.GetDeptID(DeptID);
            string StorageID = context.Request.Params["StorageID"].ToString().Trim();//仓库ID
            DataTable dt = ProductInfoDBHelper.GetDtGoodsInfoByBarcode(CompanyCD, Barcode,StorageID);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            sb.Append("data:");
            if (dt.Rows.Count == 0)
                sb.Append("[{\"ID\":\"\"}]");
            else
                sb.Append(JsonClass.DataTable2Json(dt));
            sb.Append("}");
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
        if (Action == "LoadProductInfo")   //20140402 刘锦旗（根据物品id查询物品信息）
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
            string ID = context.Request.Params["id"].ToString().Trim();//物品id
            DataTable dt = ProductInfoDBHelper.GetProductInfoById(CompanyCD, ID);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (dt != null)
            {

                sb.Append("{");
                sb.Append("data:");
                if (dt.Rows.Count == 0)
                    sb.Append("[{\"ID\":\"\"}]");
                else
                    sb.Append(JsonClass.DataTable2Json(dt));
                sb.Append("}");
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(sb.ToString());
            context.Response.End();
        }
    }
    /// <summary>
    /// 获取扩展属性值
    /// </summary>
    /// <returns></returns>
    private Hashtable GetExtAttr(HttpContext context)
    {
        try
        {
            Hashtable ht = new Hashtable();
            string strKeyList = context.Request.Params["keyList"].ToString().Trim();
            string[] arrKey = strKeyList.Split('|');
            //取得扩展属性值
            for (int y = 0; y < arrKey.Length; y++)
            {
                //不为空的字段名才取值
                if (arrKey[y].Trim().Length != 0)
                {
                    ht.Add(arrKey[y].Trim(), context.Request.Params[arrKey[y].Trim()].ToString().Trim());//添加keyvalue键值对
                }
            }
            return ht;
        }
        catch (Exception e)
        { return null; }
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}