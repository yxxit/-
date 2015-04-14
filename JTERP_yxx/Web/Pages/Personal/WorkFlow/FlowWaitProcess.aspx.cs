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

using System.Text;

public partial class Pages_Personal_WorkFlow_FlowWaitProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            LoadBillTypeList();
        }
    }


    private void LoadBillTypeList()
    {
        System.Text.RegularExpressions.Regex noblacks = new System.Text.RegularExpressions.Regex("[\\s]+");

        DataTable billTypeList = XBase.Business.SystemManager.BillType.GetBillTypeList();
        ArrayList TypeFlags = new ArrayList();

        DataTable tTable = billTypeList.Clone();
        foreach (DataRow row in billTypeList.Rows)
        {
            if (row["TypeLabel"].ToString() != "0")
                continue;
            if (row["AuditFlag"].ToString() != "1")
                continue;
            if (!TypeFlags.Contains(row["TypeFlag"].ToString()))
            {
                tTable.ImportRow(row);
                TypeFlags.Add(row["TypeFlag"].ToString());
            }
        }
        DataTable tTable2 = billTypeList.Clone();
        foreach (DataRow row in tTable.Rows)
        {
         
            //BillType.Items.Add(new ListItem(row["ModuleName"].ToString(), "-1"));

            DataRow[] rows = billTypeList.Select("TypeFlag='" + row["TypeFlag"].ToString() + "'");
            foreach (DataRow row2 in rows)
            {
                if (row2["TypeLabel"].ToString() != "0")
                    continue;
                if (row2["AuditFlag"].ToString() != "1")
                    continue;

                tTable2.ImportRow(row2);
                // BillType.Items.Add(new ListItem("|--"+row2["TypeName"].ToString(),  row2["TypeFlag"].ToString()+row2["TypeCode"].ToString()));
            }
        }

        StringBuilder billTypesScript = new StringBuilder();

        billTypesScript.Append("var billFlags=[");
        bool first = true;
        foreach (DataRow row in tTable.Rows)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                billTypesScript.Append(",");
            }
            billTypesScript.Append("{t:\"" + noblacks.Replace(row["ModuleName"].ToString(), "") + "\",v:\"" + row["TypeFlag"].ToString() + "\"}");
        }
        billTypesScript.AppendLine("];");

        billTypesScript.Append("var billTypes=[");
        first = true;
        foreach (DataRow row in tTable2.Rows)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                billTypesScript.Append(",");
            }
            string url = row["PageUrl"].ToString();
            if (url + "" == "")
            {
                url = "#";
            }
            billTypesScript.Append("{u:\"" + url + "\",p:\"" + row["TypeFlag"].ToString() + "\",t:\"" + noblacks.Replace(row["TypeName"].ToString(), "") + "\",v:\"" + row["TypeCode"].ToString() + "\"}");
        }
        billTypesScript.AppendLine("];");

        ClientScript.RegisterClientScriptBlock(this.GetType(), "billTypes", billTypesScript.ToString(), true);




    }
}
