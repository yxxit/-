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

using XBase.Model.Office.StorageManager;
using XBase.Business.Office.StorageManager;
using XBase.Common;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_StorageManager_StorageSearch : BasePage
{
    private string _ismoreunit = "True";
    public string MoreUnit
    {
        get
        {
            return _ismoreunit;
        }
        set
        {
            _ismoreunit = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCom();
            HiddenPoint.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).SelPoint;//小数位
            //扩展属性
            //GetBillExAttrControl1.TableName = "officedba.ProductInfo";
            string EFIndex = Request.QueryString["EFIndex"];
            string EFDesc = Request.QueryString["EFDesc"];
            IsMoreUnit.Value = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();
            MoreUnit = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).IsMoreUnit.ToString();
            //GetBillExAttrControl1.ExtIndex = EFIndex;
            //GetBillExAttrControl1.ExtValue = EFDesc;
            //GetBillExAttrControl1.SetExtControlValue();
            //获取仓库列表
            StorageModel model = new StorageModel();
            model.CompanyCD = UserInfo.CompanyCD;
            model.UsedStatus = "1";
            DataTable dt1 = StorageBus.GetStorageListBycondition2(model);
            if (dt1.Rows.Count > 0)
            {
                ddlStorage.DataSource = dt1;
                ddlStorage.DataTextField = "StorageName";
                ddlStorage.DataValueField = "ID";
                ddlStorage.DataBind();
                ddlStorage.Items.Insert(0, new ListItem("--请选择--", ""));
            }
            //获取材质列表
            string typeflag = "5";
            string typecode = "5";
            DataTable dt2 = XBase.Business.Office.SystemManager.CodePublicTypeBus.GetCodePublicByCode(typeflag, typecode);
            if (dt2.Rows.Count > 0)
            {
                //ddlMaterial.DataSource = dt2;
                //ddlMaterial.DataTextField = "TypeName";
                //ddlMaterial.DataValueField = "ID";
                //ddlMaterial.DataBind();
                //ddlMaterial.Items.Insert(0, new ListItem("--请选择--", ""));
            }
        }
    }
    protected void btnImport_Click(object sender, ImageClickEventArgs e)
    {
        StorageProductModel model = new StorageProductModel();
        model.CompanyCD = UserInfo.CompanyCD;
        string ProductNo = string.Empty;
        string ProductName = string.Empty;
        string BarCode = string.Empty;
        model.StorageID = ddlStorage.SelectedValue;
        XBase.Model.Office.SupplyChain.ProductInfoModel pdtModel = new XBase.Model.Office.SupplyChain.ProductInfoModel();
        pdtModel.ProdNo = txtProductNo.Value;
        pdtModel.ProductName = txtProductName.Value;
        //pdtModel.BarCode = HiddenBarCode.Value.Trim();
        //pdtModel.Specification = txtSpecification.Value;
        //pdtModel.Manufacturer = txtManufacturer.Value;
       // pdtModel.Material = ddlMaterial.SelectedValue;
        //pdtModel.FromAddr = txtFromAddr.Value;
        //pdtModel.ColorID = sel_ColorID.SelectedValue;
        string StorageCount = txtStorageCount.Value;
        string StorageCount1 = txtStorageCount1.Value;
        model.ProductCount = StorageCount;

        string BatchNo = "0";// this.ddlBatchNo.SelectedValue;
        string EFIndex = hiddenEFIndex.Value .Trim ();
        string EFDesc = hiddenEFDesc.Value .Trim ();
        string EFName = hiddenEFIndexName.Value.Trim();
        string     sidex= "ExtField"+EFIndex;
        //ProductNo = txtProductNo.Value;
        //ProductName = txtProductName.Value;
       // BarCode = HiddenBarCode.Value.Trim();

        string orderBy = txtorderBy.Value;
        if (!string.IsNullOrEmpty(orderBy))
        {
            if (orderBy.Split('_')[1] == "a")
            {
                orderBy = orderBy.Split('_')[0] + " asc";
            }
            else
            {
                orderBy = orderBy.Split('_')[0] + " desc";
            }
        }
        model.Storagestatus = DropDownList2.SelectedValue;
        //model.Prostatus = DropDownList1.SelectedValue;
        //DataTable dt = StorageSearchBus.GetProductStorageTableBycondition(model, ProductNo, ProductName, orderBy, BarCode);
        DataTable dt = StorageSearchBus.GetProductStorageTableBycondition(model, pdtModel, StorageCount1, EFIndex, EFDesc, orderBy,BatchNo);
        if (!string.IsNullOrEmpty(EFIndex) && !string.IsNullOrEmpty(EFDesc))
        {
            OutputToExecl.ExportToTableFormat(this, dt,
            new string[] { "仓库编号", "仓库名称","批次", "所属部门", "物品编号", "物品名称", "规格","颜色", "基本单位", "基本数量", "单位","数量",EFName  },
            new string[] { "StorageNo", "StorageName", "BatchNo", "DeptName", "ProductNo", "ProductName", "Specification", "ColorName", "UnitID", "ProductCount", "CodeName", "StoreCount", sidex },
            "现有库存查询列表");
        }
        else
        {
            OutputToExecl.ExportToTableFormat(this, dt,
                new string[] { "仓库编号", "仓库名称", "批次", "所属部门", "物品编号", "物品名称", "规格","颜色", "基本单位", "基本数量", "单位", "数量" },
                new string[] { "StorageNo", "StorageName", "BatchNo", "DeptName", "ProductNo", "ProductName", "Specification", "ColorName", "UnitID", "ProductCount", "CodeName", "StoreCount" },
                "现有库存查询列表");
        }
    }

    private void BindCom()
    {
        string TypeFlag = "5";
        string Code = "2";
        DataTable dt = null;
       
        Code = "3";
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定颜色
        if (dt.Rows.Count > 0)
        {
            //this.sel_ColorID.DataTextField = "TypeName";
            //sel_ColorID.DataValueField = "ID";
            //sel_ColorID.DataSource = dt;
            //sel_ColorID.DataBind();
        }
        //sel_ColorID.Items.Insert(0, new ListItem("--请选择--", ""));
       

    }
}
