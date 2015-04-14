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
public partial class Pages_Office_SupplyChain_ProductInfoAdd : BasePage
{
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

    #region System Init ModuleID
    public int SysModuleID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["SysModuleID"], out tempID);
            return tempID;
        }
    }
    #endregion

    #region 变量
    /// <summary>
    /// 用户信息
    /// </summary>
    protected UserInfoUtil UserInfo = null;

    /// <summary>
    /// 是否启用多计量单位(true：启用；false：不启用)
    /// </summary>
    private bool _isMoreUnit = false;

    #endregion 
    private string TypeFlag = "";  
    private string Flag = "";
    private string Code = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
            _version = userInfo.Version;

            // 多计量单位控制
            _isMoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit;
            //模板列表模块ID
            //this.txtModifiedDate.Text = Convert.ToString(DateTime.Now.ToShortDateString());
            this.txtModifiedDate.Text = DateTime.Now.ToString("yyyy-MM-dd");  //2010-10-21 刘朋 修改
            this.txtModifiedUserID.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            hidModuleID.Value = ConstUtil.Menu_SerchProduct;
            //获取请求参数
            string requestParam = Request.QueryString.ToString();
            //通过参数个数来判断是否从菜单过来
            int firstIndex = requestParam.IndexOf("&");
            //从列表过来时
            if (firstIndex > 0)
            {
                //返回按钮可见
                //product_btnback.Visible = true;
                //获取列表的查询条件
                string searchCondition = requestParam.Substring(firstIndex);
                //去除参数
                searchCondition = searchCondition.Replace("&ModuleID=" + ConstUtil.Menu_AddProduct, string.Empty);
                //设置检索条件
                hidSearchCondition.Value = searchCondition;
                //迁移页面
                hidFromPage.Value = Request.QueryString["FromPage"];
            }
            else
            {
                //返回按钮不可见
                product_btnback.Visible = false;
            }
            CodingRuleControl1.CodingType = ConstUtil.CODING_RULE_TYPE_ZERO;
            CodingRuleControl1.ItemTypeID = ConstUtil.CODINGA_BASE_ITEM_PRODUCT;

            this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;

            //CodingRuleControl1.TableName = "ProductInfo";
            //CodingRuleControl1.ColumnName = "ProdNo";
            //this.txt_CheckUser.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            this.txtPrincipal.Value = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeID);
            //this.UserPrincipal.Text = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName;
            //this.txt_CheckUserName.Text = Convert.ToString(((UserInfoUtil)SessionUtil.Session["UserInfo"]).EmployeeName);
            //this.txt_CheckDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.txt_CreateDate.Text = Convert.ToString(DateTime.Now.ToShortDateString()); 
            this.txt_CreateDate.Text = DateTime.Now.ToString("yyyy-MM-dd");  //2010-10-21 刘朋 修改            
            BindCom();//绑定下拉框
            BindTree();
        }
    }

   public int intOtherCorpInfoID
    {
        get
        {
            int tempID = 0;
            int.TryParse(Request["intOtherCorpInfoID"], out tempID);
            return tempID;
        }
    }

    /// <summary>
    /// 绑定所有下拉框
    /// </summary>
    private void BindCom()
    {
        TypeFlag = "5";
        Code = "2";
        DataTable dt = null;       
      
       
       
        dt = CodeReasonFeeBus.GetCodeUnitType();//绑定单位
        if (dt.Rows.Count > 0)
        {
            this.sel_UnitID.DataTextField = "CodeName";
            sel_UnitID.DataValueField = "ID";
            sel_UnitID.DataSource = dt;
            sel_UnitID.DataBind();
        }
        sel_UnitID.Items.Insert(0, new ListItem("--请选择--", "0"));
       
        StorageModel model = new StorageModel();
        UserInfoUtil userInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        model.CompanyCD = userInfo.CompanyCD;
        DataTable dt_Stor = StorageBus.GetStorageListBycondition(model);//绑定仓库
        if (dt_Stor.Rows.Count > 0)
        {
            sel_StorageID.DataSource = dt_Stor;
            sel_StorageID.DataTextField = "StorageName";
            sel_StorageID.DataValueField = "ID";
            sel_StorageID.DataBind();
            sel_StorageID.Items.Insert(0, new ListItem("--请选择--", "0"));
        }
        
        //DataTable dt_BindUnit =new DataTable();
        //dt_BindUnit = ProductInfoBus.GetUnitGroupList(this.txtUnitGroup.Value);
    }
    /// <summary>
    /// 绑定树
    /// </summary>
    private void BindTree()
    {
        DataTable dt_product = CategorySetBus.GetProductType();
        DataView dataView = dt_product.DefaultView;
        string BigtypeName = "";
        for (int i = 1; i < 13; i++)
        {
            dataView.RowFilter = "TypeFlag='" + i + "'";
            DataTable dtnew = new DataTable();
            dtnew = dataView.ToTable();
            TreeNode node = new TreeNode();
            switch (i)
            {
                case 1:
                    BigtypeName = "成品";
                    break;
                case 2:
                    BigtypeName = "原材料";
                    break;
                case 3:
                    BigtypeName = "固定资产";
                    break;
                case 4:
                    BigtypeName = "低值易耗";
                    break;
                case 5:
                    BigtypeName = "包装物";
                    break;
                case 6:
                    BigtypeName = "服务产品";
                    break;            
            }
            try
            {
                node.Value = dtnew.Rows[0]["TypeFlag"].ToString();
                node.Text = BigtypeName;
                node.NavigateUrl = string.Format("javascript:javascript:void(0)");
                BindTreeChildNodes(node, dtnew);
                this.TreeView1.Nodes.Add(node);
                //TreeView1.Attributes.Add("onclick", "OnTreeNodeClick()");
                node.Expanded = false;
            }
            catch (Exception ex)
            {

            }

        }

    }
    private void BindTreeChildNodes(TreeNode node, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=0"))
        {
            TreeNode nodes = new TreeNode();
            nodes.Text = row["CodeName"].ToString();
            nodes.Value = row["ID"].ToString();
            TypeFlag = row["TypeFlag"].ToString();
            if (dt.Select("SupperID=" + row["ID"].ToString()) != null && dt.Select("SupperID=" + row["ID"].ToString()).Length > 0)
            {
                nodes.NavigateUrl = string.Format("javascript:javascript:void(0)");
            }
            else
            {
                nodes.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodes.Text, nodes.Value, TypeFlag);
            }


            LoadSubData(row["ID"].ToString(), nodes, dt);
            node.ChildNodes.Add(nodes);
            node.Expanded = false;
        }
    }

    private void LoadSubData(string pid, TreeNode nodes, DataTable dt)
    {
        foreach (DataRow row in dt.Select("SupperID=" + pid))
        {
            TreeNode nodess = new TreeNode();
            nodess.Text = row["CodeName"].ToString();
            nodess.Value = row["ID"].ToString();
            TypeFlag = row["TypeFlag"].ToString();
            nodess.NavigateUrl = string.Format("javascript:SelectedNodeChanged('{0}','{1}','{2}');", nodess.Text, nodess.Value, TypeFlag);
            LoadSubData(row["ID"].ToString(), nodess, dt);
            nodes.ChildNodes.Add(nodess);
            nodes.Expanded = false;
        }
    }

}
