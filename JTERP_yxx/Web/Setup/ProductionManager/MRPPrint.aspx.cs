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

public partial class Pages_PrinttingModel_ProductionManager_MRPPrint : BasePage
{
    #region 签字变量
    public string signOne = "";//第一排 第一个签字的
    public string signTwo = "";//第一排 第二个签字的
    public string signThree = "";//第一排 第三个签字的
    public string signFour = "";//第一排 第四个签字的
    public string signFive = "";//第二排 第一个签字的
    public string signSix = "";//第二排 第二个签字的
    public string signSeven = "";//第二排 第三个签字的
    public string signEight = "";//第二排 第四个签字的
    #endregion
    #region MRP ID
    public int intMrpID
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
        hidPrintTypeFlag.Value = ConstUtil.PRINTBILL_TYPEFLAG_MRP.ToString();

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
        model.PrintTypeFlag = ConstUtil.PRINTBILL_TYPEFLAG_MRP;

        MRPModel modelMRP = new MRPModel();
        modelMRP.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelMRP.ID = this.intMrpID;

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
                                { "单据编号", "MRPNo"}, 
                                { "主题", "Subject"}, 
                                { "负责人", "PricipalReal" },
                                { "部门", "DeptName" },
                                { "主生产计划", "PlanNo"},
                                { "数量总计", "CountTotal"},
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
            aDetail = new string[,]{ 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "供应商", "Manufacturer" },
                                { "基本单位", "UnitName" },
                                { "基本数量", "PlanCount"},
                                { "单位","UsedUnitName"},
                                { "毛需求量", "GrossCount"},
                                { "应计划数量","UsedUnitCount"},
                                { "计划供料日期", "PlanDate"},
                                { "物料来源", "strMaterialSource"},
                                { "备注", "Remark"},
                                { "已生成采购需求数量", "ProcessedCount"},
                           };
        }
        else
        {
            aDetail = new string[,]{ 
                                { "序号", "SortNo"}, 
                                { "物品编号", "ProdNo"}, 
                                { "物品名称", "ProductName" },
                                { "供应商", "Manufacturer" },
                                { "单位", "UnitName" },
                                { "应计划数量", "PlanCount"},
                                { "毛需求量", "GrossCount"},
                                { "计划供料日期", "PlanDate"},
                                { "物料来源", "strMaterialSource"},
                                { "备注", "Remark"},
                                { "已生成采购需求数量", "ProcessedCount"},
                           };
        }




        #region 1.扩展属性
        int countExt = 0;
        DataTable dtExtTable = TableExtFieldsBus.GetAllList(((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD, "", "officedba."+ConstUtil.CODING_RULE_TABLE_MRP);
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

        DataTable dbPrint= XBase.Business.Common.PrintParameterSettingBus.GetPrintParameterSettingInfo(model);
        DataTable dtMRP = MRPBus.GetMRPInfo(modelMRP);
        DataTable dtDetail = MRPBus.GetMRPDetailInfo(modelMRP);
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
            string printtitle = System.Configuration.ConfigurationManager.AppSettings["PrintCompany"].ToString() + " <br/><span style=\"text-align: center; width: 640px; font-family:宋体; font-size:22px; font-weight:bold;\">物料需求计划</span>";
            tableBase.InnerHtml = WritePrintPageTable(printtitle, strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMRP, dtDetail, true);
        }
        else
        {
            string printtitle = System.Configuration.ConfigurationManager.AppSettings["PrintCompany"].ToString() + " <br/><span style=\"text-align: center; width: 640px; font-family:宋体; font-size:22px; font-weight:bold;\">物料需求计划</span>";
            tableBase.InnerHtml = "<tr><td colspan=\"10\" align=\"center\"><font size=\"5\" style=\"line-height:40px\"><b>" + printtitle + "</b></font></td></tr><tr height=\"20\"><td colspan=\"10\" style=\"color: #cccccc\"></td></tr>";
        }
        #endregion

        #region 3.明细信息
        if (!string.IsNullOrEmpty(strDetailFields))
        {
            tableDetail.InnerHtml = WritePrintPageTable("物料需求计划", strBaseFields.TrimEnd('|'), strDetailFields.TrimEnd('|'), aBase, aDetail, dtMRP, dtDetail, false);
        }
        #endregion

        #region 获取签字人员名称
        System.Xml.XmlDocument doc1 = new System.Xml.XmlDocument();
        doc1.Load(Server.MapPath(@"\\Pages\\PrinttingModel\\PrinttingXML\\" + ConfigurationSettings.AppSettings["PrintReportPath"].ToUpper() + "\\Print.xml"));
        System.Xml.XmlNodeList node = doc1.GetElementsByTagName("MRP");
        if (node.Count > 0)
        {
            signOne = node[0]["One"].InnerText;
            signTwo = node[0]["Two"].InnerText;
            signThree = node[0]["Three"].InnerText;
            signFour = node[0]["Four"].InnerText;
            signFive = node[0]["Five"].InnerText;
            signSix = node[0]["Six"].InnerText;
            signSeven = node[0]["Seven"].InnerText;
            signEight = node[0]["Eight"].InnerText;
        }
        #endregion
        lblPrintFoot.Text = System.Configuration.ConfigurationManager.AppSettings["Printfoot"].ToString();

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
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("物料需求计划") + ".xls");
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
