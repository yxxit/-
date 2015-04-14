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

using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SystemManager_ParameterSetting :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetSetting();
            GetBusiSetting();
        }

    }

    protected void GetSetting()
    {
        //出库是否显示价格
        bool IsPriceOn=false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "1",true))
        {
            IsPriceOn = true;
        }
        dioBN1.Checked = IsPriceOn;
        dioBN2.Checked = !IsPriceOn;

        //条码是否启用
        bool IsBarCode = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "2",true))
        {
            IsBarCode = true;
        }
        dioCB1.Checked = IsBarCode;
        dioCB2.Checked = !IsBarCode;

        //多计量单位是否启用
        bool IsMoreUnit = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "3",false))
        {
            IsMoreUnit = true;
        }
        dioMU1.Checked = IsMoreUnit;
        dioMU2.Checked = !IsMoreUnit;


        //自动生成凭证
        bool Isvoucher = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "6", false))
        {
            Isvoucher = true;
        }
        radvoucher1.Checked = Isvoucher;
        radvoucher2.Checked = !Isvoucher;

        //自动审核登帐
        bool Isapply = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "7", false))
        {
            Isapply = true;
        }
        radapply1.Checked = Isapply;
        radapply2.Checked = !Isapply;

        //超订单发货
        bool IsOverOrder = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "8", false))
        {
            IsOverOrder = true;
        }
        radOver1.Checked = IsOverOrder;
        radOver2.Checked = !IsOverOrder;

        //超订单领料
        bool IsOverTake = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "14", false))
        {
            IsOverTake = true;
        }
        RadTake1.Checked = IsOverTake;
        RadTake2.Checked = !IsOverTake;

        //允许出入库价格为零
        bool IsZero = false;
        if (ParameterSettingBus.Get(UserInfo.CompanyCD, "9", false))
        {
            IsZero = true;
        }
        dioZero1.Checked = IsZero;
        dioZero2.Checked = !IsZero;

        // 获取小数位精度
        DataTable dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD,"5");
        if (dt != null) 
        {
            if (dt.Rows.Count > 0) 
            {
                SelPoint.Value = dt.Rows[0]["SelPoint"].ToString();
            }
        }

       // 获取行业类别
       dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD, "10");
       if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                IndustrySelect.Value = dt.Rows[0]["SelPoint"].ToString();
            }
        }
        //是否启用定制打印

       bool IsPrint = false;
       if (ParameterSettingBus.Get(UserInfo.CompanyCD, "11", false))
       {
           IsPrint = true;
       }
       print1.Checked = IsPrint;
       print2.Checked = !IsPrint;

       if (IsPrint)
       {
           //获取打印编码
           dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD, "12");
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   PrintNo.Value = dt.Rows[0]["PrintNo"].ToString();
               }
           }
       }

       dt = ParameterSettingBus.GetPoint(UserInfo.CompanyCD, "13");
       if (dt != null)
       {
           if (dt.Rows.Count > 0)
           {
               txt_printwidth.Value = dt.Rows[0]["PrintNo"].ToString();
           }
       }
       CompanyCD.Value = UserInfo.CompanyCD;
    }

    protected void GetBusiSetting()
    {
        //制单人与确认人是否可以为同一个人
        bool IsSame = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "1", "1", false))
        {
            IsSame = true;
        }
        dioIsSame1.Checked = IsSame;
        dioIsSame2.Checked = !IsSame;

        //是否只能修改本人制单的单据
        bool updateSelf = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "1", "2", true))
        {
            updateSelf = true;
        }
        dioUpdateSelf1.Checked = updateSelf;
        dioUpdateSelf2.Checked = !updateSelf;

        //获取低于销售最低限价的处理方式
        DataTable dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD,"2","1");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                MinSaleSel.Value = dt.Rows[0]["LogicSet"].ToString();
            }
        }

        //获取销售最低限价控制时机
        dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD, "2", "2");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                MinSaleTimeSel.Value = dt.Rows[0]["LogicSet"].ToString();
            }
        }

        //获取客户超过信用限额时控制方式
        dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD, "2", "3");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                CustOverCreditSel.Value = dt.Rows[0]["LogicSet"].ToString();
            }
        }

        //获取客户信用控制处理时机
        dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD, "2", "4");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                CustCreditTimeSel.Value = dt.Rows[0]["LogicSet"].ToString();
            }
        }

        //获取超过采购最高限价处理方式
        dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD, "3", "1");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                MaxPurchaseSel.Value=dt.Rows[0]["LogicSet"].ToString(); 
            }
        }

        //获取采购最高限价控制时机
        dt = BusiLogicSetBus.GetValue(UserInfo.CompanyCD, "3", "2");
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                MaxPurchaseTimeSel.Value = dt.Rows[0]["LogicSet"].ToString();
            }
        }

        //是否允许超发货通知单出库
        bool overSend = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "1", false))
        {
            overSend = true;
        }
        dioOverSend1.Checked = overSend;
        dioOverSend2.Checked = !overSend;

        //是否允许超调拨通知单出库
        bool overTransfer = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "2", false))
        {
            overTransfer = true;
        }
        dioOverTransfer1.Checked = overTransfer;
        dioOverTransfer2.Checked = !overTransfer;

        //是否允许超到货通知单入库
        bool overArrive = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "3", false))
        {
            overArrive = true;
        }
        dioOverArrive1.Checked = overArrive;
        dioOverArrive2.Checked = !overArrive;

        //采购入库单审核时，是否修改现存量
        bool checkPurchaseIn = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "4", false))
        {
            checkPurchaseIn = true;
        }
        dioCheckPurchaseIn1.Checked = checkPurchaseIn;
        dioCheckPurchaseIn2.Checked = !checkPurchaseIn;

        //销售发货单审核时，是否修改现存量
        bool sellSend = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "5", false))
        {
            sellSend = true;
        }
        dioSellSend1.Checked = sellSend;
        dioSellSend2.Checked = !sellSend;

        //是否0库存出库
        bool storagezero = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "6", false))
        {
            storagezero = true;
        }

        diostoragezero1.Checked = storagezero;
        diostoragezero2.Checked = !storagezero;

        //是否超生产订单入库
        bool instorageover = false;
        if (BusiLogicSetBus.GetBus(UserInfo.CompanyCD, "4", "7", false))
        {
            instorageover = true;
        }

        dioover1.Checked = instorageover;
        dioover2.Checked = !instorageover;
    }

}
