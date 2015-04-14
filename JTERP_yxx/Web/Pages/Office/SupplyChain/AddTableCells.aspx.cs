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
using XBase.Model.Office.SupplyChain;
using XBase.Business.Office.SupplyChain;

public partial class Pages_Office_SupplyChain_AddTableCells : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;//公司代码 
            DataTable dt = ProductExtListBus.GetAllList(CompanyCD);
            if (dt.Rows.Count > 0)
            {
                ListItem Item = new ListItem();
                Item.Value = "0";
                Item.Text = "--请选择--"; 

                this.PCustom1.DataTextField = "EFDesc";
                PCustom1.DataValueField = "EFIndex";
                PCustom1.DataSource = dt;
                PCustom1.DataBind();
                PCustom1.Items.Insert(0, Item);

                this.PCustom2.DataTextField = "EFDesc";
                PCustom2.DataValueField = "EFIndex";
                PCustom2.DataSource = dt;
                PCustom2.DataBind();
                PCustom2.Items.Insert(0, Item);

                this.PCustom3.DataTextField = "EFDesc";
                PCustom3.DataValueField = "EFIndex";
                PCustom3.DataSource = dt;
                PCustom3.DataBind();
                PCustom3.Items.Insert(0, Item);

                this.PCustom4.DataTextField = "EFDesc";
                PCustom4.DataValueField = "EFIndex";
                PCustom4.DataSource = dt;
                PCustom4.DataBind();
                PCustom4.Items.Insert(0, Item);

                this.PCustom5.DataTextField = "EFDesc";
                PCustom5.DataValueField = "EFIndex";
                PCustom5.DataSource = dt;
                PCustom5.DataBind();
                PCustom5.Items.Insert(0, Item);

                this.PCustom6.DataTextField = "EFDesc";
                PCustom6.DataValueField = "EFIndex";
                PCustom6.DataSource = dt;
                PCustom6.DataBind();
                PCustom6.Items.Insert(0, Item);

                this.PCustom7.DataTextField = "EFDesc";
                PCustom7.DataValueField = "EFIndex";
                PCustom7.DataSource = dt;
                PCustom7.DataBind();
                PCustom7.Items.Insert(0, Item);

                this.PCustom8.DataTextField = "EFDesc";
                PCustom8.DataValueField = "EFIndex";
                PCustom8.DataSource = dt;
                PCustom8.DataBind();
                PCustom8.Items.Insert(0, Item);

                this.PCustom9.DataTextField = "EFDesc";
                PCustom9.DataValueField = "EFIndex";
                PCustom9.DataSource = dt;
                PCustom9.DataBind();
                PCustom9.Items.Insert(0, Item);

                this.PCustom10.DataTextField = "EFDesc";
                PCustom10.DataValueField = "EFIndex";
                PCustom10.DataSource = dt;
                PCustom10.DataBind();
                PCustom10.Items.Insert(0, Item);
            }
        }
    }
}
