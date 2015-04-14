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
using XBase.Model.Common;
using XBase.Common;
using System.Text;
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

public partial class Pages_PrinttingModel_ProductionManager_ManufactureTaskPrint : BasePage
{

    #region Task ID
    public int intTaskID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["ID"], out tempID);
            return tempID;
        }
    }
    #endregion
    //版本
    private string _version = "general";
    public string Version
    {
        get
        {
            return _version;
        }
        set
        {
            _version = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        _version = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version;
        Vervalue.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version;
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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_TASK;

        ManufactureTaskModel modelTask = new ManufactureTaskModel();
        modelTask.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelTask.ID = this.intTaskID;

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
                                { "单据编号", "TaskNo"}, 
                                { "主题", "Subject"}, 
                                { "源单类型", "strFromType" },
                                { "生产部门", "DeptName" },
                                { "加工类型", "strManufactureType"},
                                { "负责人", "PricipalReal"},
                                { "所属项目", "ProjectName"},
                                { "安排生产数量总计", "CountTotal"},
                                { "单据状态", "strBillStatusText"},
                                { "制单人", "CreatorReal"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorReal"},
                                { "确认日期", "ConfirmDate"},
                                { "结单人", "CloserReal"},
                                { "结单日期", "CloseDate"},
                                { "最后更新人", "ModifiedUserReal"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                          };
        string[,] aDetail;

        if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit)
        {
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == "floor")
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "尺寸", "Size" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "ProductCount"},
                                { "单位","UsedUnitName"},
                                { "数量","UsedUnitCount"},
                                { "Bom", "BomNo"},
                                { "工艺路线", "RouteNo"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "已生产数量", "ProductedCount"},
                                { "已入库数量", "InCount"},
                                { "已报数量", "ApplyCheckCount"},
                                { "实检数量", "CheckedCount"},
                                { "合格数量", "PassCount"},
                                { "不合格数量", "NotPassCount"},
                                { "纸号", "Pnumber" },
                                { "耐磨纸", "AbrasionResist" },
                                { "平衡纸", "BalancePaper" },
                                { "基材", "BaseMaterial" },
                                { "表面工艺", "SurfaceTreatment" },
                                { "总平米", "TotalSquare" },
                                { "背底钢板", "BackBottomPlate" },
                                { "扣型", "BuckleType" },
                                
                           };
            }
            else
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "ProductCount"},
                                { "单位","UsedUnitName"},
                                { "数量","UsedUnitCount"},
                                { "Bom", "BomNo"},
                                { "工艺路线", "RouteNo"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "已生产数量", "ProductedCount"},
                                { "已入库数量", "InCount"},
                                { "已报数量", "ApplyCheckCount"},
                                { "实检数量", "CheckedCount"},
                                { "合格数量", "PassCount"},
                                { "不合格数量", "NotPassCount"},
                           };
            }
        }
        else
        {
            if (((UserInfoUtil)SessionUtil.Session["UserInfo"]).Version == "floor")
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "尺寸", "Size" },
                                { "规格", "Specification" },
                                
                                { "单位", "UnitName" },
                                { "生产数量", "ProductCount"},
                                { "Bom", "BomNo"},
                                { "工艺路线", "RouteNo"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "已生产数量", "ProductedCount"},
                                { "已入库数量", "InCount"},
                                { "已报数量", "ApplyCheckCount"},
                                { "实检数量", "CheckedCount"},
                                { "合格数量", "PassCount"},
                                { "不合格数量", "NotPassCount"},
                                 { "纸号", "Pnumber" },
                                { "耐磨纸", "AbrasionResist" },
                                { "平衡纸", "BalancePaper" },
                                { "基材", "BaseMaterial" },
                                { "表面工艺", "SurfaceTreatment" },
                                { "总平米", "TotalSquare" },
                                { "背底钢板", "BackBottomPlate" },
                                { "扣型", "BuckleType" },
                                 
                           };
            }
            else
            {
                aDetail = new string[,] { 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "规格", "Specification" },
                                { "单位", "UnitName" },
                                { "生产数量", "ProductCount"},
                                { "Bom", "BomNo"},
                                { "工艺路线", "RouteNo"},
                                { "计划开工日期", "StartDate"},
                                { "计划完工日期", "EndDate"},
                                { "已生产数量", "ProductedCount"},
                                { "已入库数量", "InCount"},
                                { "已报数量", "ApplyCheckCount"},
                                { "实检数量", "CheckedCount"},
                                { "合格数量", "PassCount"},
                                { "不合格数量", "NotPassCount"},
                           };
            }
        }
        


        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba." + ConstUtil.CODING_RULE_TABLE_MANUFACTURETASK);
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
        DataTable dtTask = ManufactureTaskBus.GetTaskInfo(modelTask);
        DataTable dtDetail = ManufactureTaskBus.GetTaskDetailInfo(modelTask);
        string strBaseFields = "";
        string strDetailFields = "";


        if (dbPrint.Rows.Count > 0)
        {
            #region 设置过打印模板设置时 直接取出表里设置的值
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
            #endregion
        }
        else
        {

            #region 未设置过打印模板设置 默认显示所有的
            isSeted.Value = "0";

            /*未设置过打印模板设置时，默认显示的字段  基本信息字段*/
            for (int m = 10; m < aBase.Length / 2; m++)
            {
                strBaseFields = strBaseFields + aBase[m, 1] + "|";
            }
            /*未设置过打印模板设置时，默认显示的字段 基本信息字段+扩展信息字段*/
            if (countExt > 0)
            {
                for (int i = 0; i < countExt; i++)
                {
                    strBaseFields = strBaseFields + "ExtField" + (i + 1) + "|";
                }
            }
            /*未设置过打印模板设置时，默认显示的字段 明细信息字段*/
            for (int n = 0; n < aDetail.Length / 2; n++)
            {
                strDetailFields = strDetailFields + aDetail[n, 1] + "|";
            }
            #endregion

        }
        #region 2.主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("生产任务单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtTask, dtDetail, true);
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("生产任务单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtTask, dtDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("生产任务单") + ".xls");
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
