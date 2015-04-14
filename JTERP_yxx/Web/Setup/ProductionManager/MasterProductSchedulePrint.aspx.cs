using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using XBase.Business.Office.ProductionManager;
using XBase.Model.Office.ProductionManager;
using XBase.Common;
using XBase.Model.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_ProductionManager_MasterProductSchedulePrint : BasePage
{
    #region Master ID
    public int intMasterID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        hidBillTypeFlag.Value = ConstUtil.BILL_TYPEFLAG_PRODUCTION;
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_MASTERPRODUCTSCHEDULE.ToString();

        if (!IsPostBack)
        {
            LoadPrintInfo();
        }
    }

    #region 加载打印信息
    protected void LoadPrintInfo()
    {
        PrintParameterSettingModel model = new PrintParameterSettingModel();
        model.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        model.BillTypeFlag = int.Parse(ConstUtil.BILL_TYPEFLAG_PRODUCTION);
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_MASTERPRODUCTSCHEDULE;

        MasterProductScheduleModel modelMaster = new MasterProductScheduleModel();
        modelMaster.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMaster.ID = this.intMasterID;
        //bool isMoreUnit = ((XBase.Common.UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;
        


        /*此处需注意在模板设置表里的字段和取基本信息的字段是否一致*/
        string[,] aBase = { 
                                { "{ExtField1}", "ExtField1"},
                                { "{ExtField2}", "ExtField2"},
                                { "{ExtField3}", "ExtField3"},
                                { "{ExtField4}", "ExtField4"},
                                { "{ExtField5}", "ExtField5"},
                                { "{ExtField6}", "ExtField6"},
                                { "{ExtField7}", "ExtField7"},
                                { "{ExtField8}", "ExtField8"},
                                { "{ExtField9}", "ExtField9"},
                                { "{ExtField10}", "ExtField10"},
                                { "单据编号", "PlanNo"}, 
                                { "主题", "Subject"}, 
                                { "负责人", "PrincipalReal" },
                                { "部门", "DeptName" },
                                { "计划生产数量合计", "CountTotal"},
                                { "单据状态", "strBillStatusText"},
                                { "制单人", "CreatorReal"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorReal"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserReal"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };
        string[,] aDetail;

        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "基本单位", "CodeName" },
                                { "基本数量", "ProduceCount" },
                                { "单位", "UsedUnitName" },
                                { "计划生产数量", "UsedUnitCount"},
                                { "需求数量", "ProductCount"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "销售订单编号", "FromBillNo"},
                                { "销售订单行号", "FromLineNo"},
                                { "备注", "DetailRemark"},
                                { "已下达数量", "PlanCount"},
                           };
        }
        else
        {
            aDetail = new string[,]{ 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "单位", "CodeName" },
                                { "计划生产数量", "ProduceCount"},
                                { "需求数量", "ProductCount"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "销售订单编号", "FromBillNo"},
                                { "销售订单行号", "FromLineNo"},
                                { "备注", "DetailRemark"},
                                { "已下达数量", "PlanCount"},
                           };
        }



        #region 扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba."+ConstUtil.CODING_RULE_TABLE_SCHEDULE);
        if (dtExtTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtExtTable.Rows.Count; i++)
            {
                for (int x = 0; x < (aBase.Length / 2) - 15; x++)
                {
                    if (x == i)
                    {
                        aBase[x, 0] = dtExtTable.Rows[i]["EFDesc"].ToString();
                        countExt++;
                    }
                }
            }
        }
        #endregion

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtBase = MasterProductScheduleBus.GetMasterProductScheduleInfo(modelMaster);
        DataTable dtDetail = MasterProductScheduleBus.GetMasterProductScheduleDetailInfoList(modelMaster);
 
        string strBaseFields = "";
        string strDetailFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
        }
        else
        {
            isSeted.Value = "0";
            strBaseFields = "PlanNo|Subject|PrincipalReal|DeptName|CountTotal|strBillStatusText|CreatorReal|CreateDate|ConfirmorReal|ConfirmDate|CloserReal|CloseDate|ModifiedUserID|ModifiedDate|Remark";
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
            {
                strDetailFields = "SortNo|ProdNo|ProductName|Specification|CodeName|ProduceCount|UsedUnitName|UsedUnitCount|ProductCount|StartDate|EndDate|FromBillNo|FromLineNo|DetailRemark|PlanCount";
            }
            else
            {
                strDetailFields = "SortNo|ProdNo|ProductName|Specification|CodeName|ProduceCount|ProductCount|StartDate|EndDate|FromBillNo|FromLineNo|DetailRemark|PlanCount";
            }
            /*基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "|" + "ExtField" + (i + 1);
                }
            }
            
        }

        #region 主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("主生产计划", strBaseFields, strDetailFields, aBase, aDetail, dtBase, dtDetail, true);
        }
        #endregion

        #region 明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("主生产计划", strBaseFields, strDetailFields, aBase, aDetail, dtBase, dtDetail, false);
        }
        #endregion

    }
    #endregion

    #region 导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("主生产计划") + ".xls");
        Response.Write("<html><head><META http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\"></head><body>");
        Response.Write(hiddExcel.Value);
        Response.Write(tw.ToString());
        Response.Write("</body></html>");
        Response.End();
        hw.Close();
        hw.Flush();
        tw.Close();
        tw.Flush();
    }
    #endregion
}
