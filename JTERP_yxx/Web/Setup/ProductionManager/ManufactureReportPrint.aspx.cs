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

public partial class Pages_PrinttingModel_ProductionManager_ManufactureReportPrint : BasePage
{
    #region Report ID
    public int intReportID
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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_REPORT;

        ManufactureReportModel modelReport = new ManufactureReportModel();
        modelReport.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelReport.ID = this.intReportID;

        #region 初始化 取基本信息及明细信息的字段以及对应的标题
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
                                { "单据编号", "ReportNo"}, 
                                { "主题", "Subject"}, 
                                { "生产任务单", "TaskNo" },
                                { "生产部门", "DeptName" },
                                { "生产日期", "DailyDate"},
                                { "填报人", "ReporterReal"},
                                { "填报日期", "ReportDate"},
                                { "单据状态", "strBillStatusText"},
                                { "制单人", "CreatorReal"},
                                { "制单日期", "CreateDate"},
                                { "确认人", "ConfirmorReal"},
                                { "确认日期", "ConfirmDate"},
                                { "最后更新人", "ModifiedUserID"},
                                { "最后更新日期", "ModifiedDate"},
                                { "备注", "Remark"},
                                { "完成数合计", "ProductionTotal"},
                                { "工时合计", "WorkTimeTotal"},
                                { "应到人数", "PlanHRs"},
                                { "实到人数", "RealHRs"},
                                { "应有工时", "PlanWorkTime"},
                                { "加班工时", "AddWorkTime"},
                                { "停工工时", "StopWorkTime"},
                                { "有效工时", "RealWorkTime"},
                                { "设备数量", "MachineCount"},
                                { "实际开动数", "OpenCount"},
                                { "总开动时间", "OpenTime"},
                                { "开动率", "OpenPercent"},
                                { "负荷率", "LoadPercent"},
                                { "设备使用率", "UsePercent"},
                                { "停机数目", "StopCount"},
                                { "停机时长", "StopTime"},
                                { "停机原因", "StopReason"},
                                { "领入合计", "TakeNum"},
                                { "耗用合计", "UsedNum"},
                                { "结存合计", "NowNum"},
                          };

        string[,] aProductDetail = { 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName"}, 
                                { "工时", "WorkTime" },
                                { "完成数", "UnFinishNum" },
                                { "合格数", "PassNum" },
                                { "合格率", "PassPercent"},
                           };

        string[,] aStaffDetail = { 
                                { "人员", "StaffReal"}, 
                                { "工时", "WorkTime"}, 
                                { "完成数", "FinishNum" },
                                { "合格数", "PassNum" },
                                { "合格率", "PassPercent" },
                           };

        string[,] aMachineDetail = { 
                                { "设备编号", "MachineNo"}, 
                                { "设备名称", "MachineName"}, 
                                { "开机时长", "UseHour" },
                                { "完成数", "FinishNum" },
                                { "合格数", "PassNum" },
                                { "合格率", "PassPercent" },
                           };
        string[,] aMaterialDetail = { 
                                { "物料编号", "ProdNo"}, 
                                { "物料名称", "ProductName"}, 
                                { "本日领入", "TakeNum" },
                                { "昨日结存", "BeforeNum" },
                                { "本日耗用", "UsedNum" },
                                { "本日损坏", "BadNum" },
                                { "本日结存", "NowNum" },
                           };
        #endregion


        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba." + ConstUtil.CODING_RULE_TABLE_REPORT);
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

        #region 2.所设的打印模板设置

        DataTable dbPrint = XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtBase = ManufactureReportBus.GetManufactureReport(modelReport);
        DataTable dtProductDetail = ManufactureReportBus.GetManufactureReportProduct(modelReport);
        DataTable dtStaffDetail = ManufactureReportBus.GetManufactureReportStaff(modelReport);
        DataTable dtMachineDetail = ManufactureReportBus.GetManufactureReportMachine(modelReport);
        DataTable dtMaterialDetail = ManufactureReportBus.GetManufactureReportMeterial(modelReport);

        string strBaseFields = "";
        string strDetailFields = "";
        string strDetailSecondFields = "";
        string strDetailThreeFields = "";
        string strDetailFourFields = "";

        if (dbPrint.Rows.Count > 0)
        {
            #region 设置过打印模板设置时 直接取出表里设置的值
            isSeted.Value = "1";
            strBaseFields = dbPrint.Rows[0]["BaseFields"].ToString();
            strDetailFields = dbPrint.Rows[0]["DetailFields"].ToString();
            strDetailSecondFields = dbPrint.Rows[0]["DetailSecondFields"].ToString();
            strDetailThreeFields = dbPrint.Rows[0]["DetailThreeFields"].ToString();
            strDetailFourFields = dbPrint.Rows[0]["DetailFourFields"].ToString();
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
            for (int n = 0; n < aProductDetail.Length / 2; n++)
            {
                strDetailFields = strDetailFields + aProductDetail[n, 1] + "|";
            }

            for (int m = 0; m < aStaffDetail.Length / 2; m++)
            {
                strDetailSecondFields = strDetailSecondFields + aStaffDetail[m, 1] + "|";
            }
            for (int x = 0; x < aMachineDetail.Length / 2; x++)
            {
                strDetailThreeFields = strDetailThreeFields + aMachineDetail[x, 1] + "|";
            }
            for (int x = 0; x < aMaterialDetail.Length / 2; x++)
            {
                strDetailFourFields = strDetailFourFields + aMaterialDetail[x, 1] + "|";
            }
            #endregion

            /*两种都可以*/
        }
        #endregion

        #region 3.输出主表信息
        if (!string.IsNullOrEmpty(strBaseFields))
        {
            tableBase.InnerHtml = WritePrintPageTable("生产任务汇报单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aProductDetail, dtBase, dtProductDetail, true);
        }
        #endregion

        #region 4.输出生产明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("生产任务汇报单", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aProductDetail, dtBase, dtProductDetail, false);
        }
        #endregion

        #region 5.输出人员明细信息
        if (!string.IsNullOrEmpty(strDetailSecondFields))
        {
            tableSecondDetail.InnerHtml = WritePrintPageTable("生产任务汇报单", strBaseFields.TrimEnd('|'), strDetailSecondFields.TrimEnd('|'), aBase, aStaffDetail, dtBase, dtStaffDetail, false);
        }
        #endregion

        #region 6.输出设备明细信息
        if (!string.IsNullOrEmpty(strDetailThreeFields))
        {
            tableThreeDetail.InnerHtml = WritePrintPageTable("生产任务汇报单", strBaseFields.TrimEnd('|'), strDetailThreeFields.TrimEnd('|'), aBase, aMachineDetail, dtBase, dtMachineDetail, false);
        }
        #endregion

        #region 7.输出物料明细信息
        if (!string.IsNullOrEmpty(strDetailFourFields))
        {
            tableFourDetail.InnerHtml = WritePrintPageTable("生产任务汇报单", strBaseFields.TrimEnd('|'), strDetailFourFields.TrimEnd('|'), aBase, aMaterialDetail, dtBase, dtMaterialDetail, false);
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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("生产任务汇报单") + ".xls");
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
