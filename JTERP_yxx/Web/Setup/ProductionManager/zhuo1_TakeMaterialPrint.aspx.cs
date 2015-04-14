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
using XBase.Common;
using System.Text;
using XBase.Business.Office.ProductionManager;
using XBase.Business.Office.SupplyChain;
using XBase.Model.Common;
using System.Text.RegularExpressions;
using XBase.Data.DBHelper;
using XBase.Model.Office.ProductionManager;

public partial class Pages_PrinttingModel_ProductionManager_zhuo1_TakeMaterialPrint : BasePage
{
    public Hashtable ht = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        TakeMaterialModel modelTake = new TakeMaterialModel();
        modelTake.CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        modelTake.ID = int.Parse(Request.QueryString["ID"].ToString());

        DataTable dt = TakeMaterialBus.GetTakeInfo(modelTake);//获取主表数据
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            ht.Add(dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
        }
        ht.Add("CompanyName", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName);
        ht.Add("SelPoint", ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint);
        //DataTable dts = StorageInProcessBus.GetStorageInProcessDetailInfo(id, ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD);
        int rowCount = 0;
        //string storageName = "";
        //if (dts.Rows.Count != 0)
        //{
        //    for (int i = 0; i < dts.Rows.Count; i++)
        //    {
        //        storageName += dts.Rows[i]["StorageName"].ToString() + ",";
        //    }
        //    storageName = storageName.Substring(0, storageName.Length - 1);
        //    lbl_Storage.Text = storageName;
        //}
        //else
        //{
        //    lbl_Storage.Text = "";
        //}

        //string printtitle = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyName;
        //<span style=\"text-align: center; width: 680px; font-family:宋体; font-size:15px; font-weight:bold;\">" + printtitle + "</span><br/>
        Tab_Head.InnerHtml = "<span style=\"text-align: center; width: 680px; font-family:宋体; font-size:22px; font-weight:bold;\">领料单</span>";

        DataTable dtd = TakeMaterialBus.GetTakeDetailInfo(modelTake);//获取明细数据
        rowCount = dtd.Rows.Count;

        if (rowCount > 10)
        {
            rpt_details.DataSource = dtd;
        }
        else
        {
            // for (int i=0; i<dtd.Rows.Count<10; i++)
            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = dtd.NewRow();
                //dtd.Rows.Add(dr);
            }
            rpt_details.DataSource = dtd;

        }
        rpt_details.DataBind();
    }
    //导出
    protected void btnImport_Click(object sender, EventArgs e)
    {
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        Response.Clear();
        Response.Charset = "gb2312";
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("领料单") + ".xls");
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
}
