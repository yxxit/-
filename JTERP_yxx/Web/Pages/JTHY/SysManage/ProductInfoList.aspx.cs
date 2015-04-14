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
using System.Xml.Linq;
using XBase.Business.Office.SystemManager;
using XBase.Business.Office.SupplyChain;
using XBase.Common;
using XBase.Business.Office.StorageManager;
using XBase.Model.Office.StorageManager;
using XBase.Model.Office.SupplyChain;
public partial class Pages_Office_SupplyChain_ProductInfoList : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            this.hidModuleID.Value = ConstUtil.Menu_AddProduct;         
           
            string requestParam = Request.QueryString.ToString();
            //从列表过来时
            int firstIndex = requestParam.IndexOf("&");
            //返回回来时
            if (firstIndex > 0)
            {
                //获取是否查询的标识
                string flag = Request.QueryString["Flag"];
                //点击查询时，设置查询的条件，并执行查询
                if ("1".Equals(flag))
                {
                    //this.txt_TypeID.Value = Request.QueryString["txt_TypeID"];
                    ////this.txt_TypeID.Value = "";
                    //this.txt_ID.Value = Request.QueryString["TypeID"];
                    //txt_ProdNo.Value = Request.QueryString["ProdNo"];
                    //txt_PYShort.Value = Request.QueryString["PYShort"];
                    //txt_ProductName.Text = Request.QueryString["ProductName"];
                    //txt_BarCode.Value = Request.QueryString["BarCode"];
                    //this.selCorlor.SelectedValue = Request.QueryString["Color"];
                    //this.txt_Specification.Value = Request.QueryString["Specification"];
                    //this.UsedStatus.Value = Request.QueryString["UsedStatus"];
                    //this.CheckStatus.Value = Request.QueryString["CheckStatus"];
                    //获取当前页
                    string pageIndex = Request.QueryString["PageIndex"];
                    //获取每页显示记录数 
                    string pageCount = Request.QueryString["PageCount"];
                    //执行查询
                    ClientScript.RegisterStartupScript(this.GetType(), "DoSearch"
                            , "<script language=javascript>this.pageCount = parseInt(" + pageCount + ");Fun_Search_ProductInfo('" + pageIndex + "');</script>");
                }
            }
        }
    }
   
    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            string TypeFlag = row["TypeFlag"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodess.Text, nodess.Value, TypeFlag);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
       
    }
}
