<%@ WebHandler Language="C#" Class="ProviderOrCustomSelect" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using XBase.Common;
using XBase.Model.Office.CustManager;
using XBase.Business.Office.CustManager;
using XBase.Business.Common;

public class ProviderOrCustomSelect : SubBaseHandler
{

    public override void ActionHandler(string action)
    {
        switch (action.ToLower())
        {
            case "get":
                GetProviderOrCustom();
                break;
            case "add":
                AddProviderCustom();
                break;
        }


    }

    private void AddProviderCustom()
    {
        string CustNoType = GetRequest("CustNoType");
        string CustNo = GetRequest("CustNo");
        string tableName = "CustInfo";//入库表
        string columnName = "CustNo";//入库单编号
        string codeValue = CustNo;
        
        XBase.Model.Office.CustManager.CustInfoModel model = new XBase.Model.Office.CustManager.CustInfoModel();
        //XBase.Model.Office.CustManager.CustInfoModel model =
        //    GetModel<XBase.Model.Office.CustManager.CustInfoModel>(new string[] { 
        //    "CustName","ContactName","Tel","Mobile","Fax","WebSite","email","ReceiveAddress","Post","OpenBank","AccountMan","AccountNum"});
        model.CompanyCD = UserInfo.CompanyCD;
        //model.BranchID = UserInfo.BranchID;
        //model.CustNo = XBase.Business.Common.BillNo.Create("SONGJIE", "officedba.CustInfo", "CustNo", UserInfo.CompanyCD, UserInfo.BranchID);

        if (CustNoType != "")
            model.CustNo = XBase.Business.Common.ItemCodingRuleBus.GetCodeValue(CustNoType, tableName, columnName);

        else
        {
            bool ishave = XBase.Business.Common.PrimekeyVerifyBus.CheckCodeUniq(tableName, columnName, codeValue);
            if (!ishave)
            {
                OutputResult(false, "该编号已被使用，请输入未使用的编号！");
                return;
            }
            model.CustNo = codeValue;
        }
        model.CreatedDate = DateTime.Now;
        model.Creator = UserInfo.EmployeeID;
        model.CustName = GetRequestForm("CustName", false);
        model.ContactName = GetRequestForm("ContactName", false);
        model.Tel = GetRequestForm("Tel",false);
        model.Fax = GetRequestForm("Fax",false);
        model.WebSite = GetRequestForm("WebSite", false);
        model.email = GetRequestForm("email", false);
        model.ReceiveAddress = GetRequestForm("ReceiveAddress", false);
        model.Post = GetRequestForm("Post", false);
        model.OpenBank = GetRequestForm("Post",false);
        model.AccountMan = GetRequestForm("AccountMan", false);
        model.AccountNum = GetRequestForm("AccountNum", false);
        model.Mobile = GetRequestForm("Mobile", false);
        string[] arr = new string[] { "CustName", "CompanyCD" };
        string[] CustNames = new string[] { model.CustName, UserInfo.CompanyCD };

        bool NameHas = XBase.Business.Common.PrimekeyVerifyDBHelper.PrimekeyVerifytc("officedba.CustInfo", arr, CustNames);
        if (NameHas)
        {
            OutputResult(false, "该往来单位名称已被使用，请输入未使用的往来单位名称");
            return;
        }        
        
        string id = XBase.Business.Common.ProviderOrCustomBus.AddProviderCustom(model);
        bool flag=false;
        if (!string.IsNullOrEmpty(id))
            flag = true;

        OutputResult(flag, id);

    }


    private void GetProviderOrCustom()
    {
        string CustNo = GetRequestForm("CustNo", false);
        string CustName = GetRequestForm("CustName", false);
        string Contactor = GetRequestForm("Contactor", false);
        int PageIndex = Convert.ToInt32(GetRequestForm("pageIndex", true));
        int PageSize = Convert.ToInt32(GetRequestForm("PageSize", true));
        string OrderBy = GetRequestForm("OrderBy", false);
        if (OrderBy != "")
        {
            OrderBy = "ID DESC";
        }
        string SubStoreID = GetRequestForm("SubStoreID", false);

       

        Hashtable htParams = new Hashtable();
        if (!string.IsNullOrEmpty(CustNo))
            htParams.Add("CustNo", "%" + CustNo + "%");
        if (!string.IsNullOrEmpty(CustName))
            htParams.Add("CustName", "%" + CustName + "%");
        if (!string.IsNullOrEmpty(Contactor))
            htParams.Add("Contactor", Contactor);

        htParams.Add("CompanyCD", UserInfo.CompanyCD);
        if (!string.IsNullOrEmpty(SubStoreID) && SubStoreID!="0")
        {
            htParams.Add("BranchID", SubStoreID);
        }
        else
        {
            htParams.Add("BranchID", UserInfo.BranchID);
        }

        int TotalCount = 0;

        DataTable dt = XBase.Business.Common.ProviderOrCustomBus.GetProviderCustomList(htParams, PageSize, PageIndex, OrderBy, ref TotalCount);

        OutputDataTable(dt, TotalCount);

    }
   

}