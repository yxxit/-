using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XBase.Business.Office.SystemManager;

public partial class Pages_Office_SupplyChain_TaskAlertSetting : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetSetting();
        }
    }
    protected void GetSetting()
    {
        //是否提醒库存限量报警
        bool IsStorageAlert = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "1",UserInfo.UserID,true))
        {
            IsStorageAlert = true;
        }
        dioStorage1.Checked = IsStorageAlert;
        dioStorage2.Checked = !IsStorageAlert;

        //客户联络延期告警
        bool IsCustDelay = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "2",UserInfo.UserID, true))
        {
            IsCustDelay = true;
        }
        dioCC1.Checked = IsCustDelay;
        dioCC2.Checked = !IsCustDelay;

        //供应商联络延期告警
        bool IsProviderDelay = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "3",UserInfo.UserID,true))
        {
            IsProviderDelay = true;
        }
        radPC1.Checked = IsProviderDelay;
        radPC2.Checked = !IsProviderDelay;

        //是否提醒待我审批的流程
        bool Isflow = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "4", UserInfo.UserID,true))
        {
            Isflow = true;
        }
        radFlow1.Checked = Isflow;
        radFlow2.Checked = !Isflow;

        //是否提醒我的待办任务
        bool Istask = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "5", UserInfo.UserID, true))
        {
            Istask = true;
        }
        radTask1.Checked = Istask;
        radTask2.Checked = !Istask;

        //即将到期的劳动合同
        bool IsContract = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "6", UserInfo.UserID, true))
        {
            IsContract = true;
        }
        dioCT1.Checked = IsContract;
        dioCT2.Checked = !IsContract;

        //我的备忘录
        bool IsMemo = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "7", UserInfo.UserID, true))
        {
            IsMemo = true;
        }
        dioMemo1.Checked = IsMemo;
        dioMemo2.Checked = !IsMemo;

        //是否提醒我的未读短信
        bool IsUnRMsg = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "8", UserInfo.UserID, true))
        {
            IsUnRMsg = true;
        }
        dioUnRM1.Checked = IsUnRMsg;
        dioUnRM2.Checked = !IsUnRMsg;

        //是否提醒我的参会通知
        bool IsMeet = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "9", UserInfo.UserID, true))
        {
            IsMeet = true;
        }
        dioMN1.Checked = IsMeet;
        dioMN2.Checked = !IsMeet;

        //质保证书预警
        bool IsZB = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "10", UserInfo.UserID, true))
        {
            IsZB = true;
        }
        dioYYZZ1.Checked = IsZB;
        dioYYZZ2.Checked = !IsZB;

        txtYYZZDay.Value = BusiLogicSetBus.GetWaringDays(UserInfo.CompanyCD, "10", UserInfo.UserID, true);
        Lock1.Text = BusiLogicSetBus.GetLock(UserInfo.CompanyCD, "10", UserInfo.UserID, true);

        //药品经营（生产）许可证预警
        bool IsXuKe = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "11", UserInfo.UserID, true))
        {
            IsXuKe = true;
        }
        dioXKZ1.Checked = IsXuKe;
        dioXKZ2.Checked = !IsXuKe;
        txtXKZDay.Value=   BusiLogicSetBus.GetWaringDays(UserInfo.CompanyCD, "11", UserInfo.UserID, true);
        Lock2.Text = BusiLogicSetBus.GetLock(UserInfo.CompanyCD, "11", UserInfo.UserID, true);
        //GMP(GSP)证书过期预警
        bool IsGMP = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "12", UserInfo.UserID, true))
        {
            IsGMP = true;
        }
        dioGMP1.Checked = IsGMP;
        dioGMP2.Checked = !IsGMP;
        txtGMPDay.Value=  BusiLogicSetBus.GetWaringDays(UserInfo.CompanyCD, "12", UserInfo.UserID, true);
        Lock3.Text = BusiLogicSetBus.GetLock(UserInfo.CompanyCD, "12", UserInfo.UserID, true);
        //药品保质期预警
        bool IsYPBZQ = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "13", UserInfo.UserID, true))
        {
            IsYPBZQ = true;
        }
        dioYPBZQ1.Checked = IsYPBZQ;
        dioYPBZQ2.Checked = !IsYPBZQ;
        txtYPBZQDay.Value=  BusiLogicSetBus.GetWaringDays(UserInfo.CompanyCD, "13", UserInfo.UserID, true);
        Lock4.Text = BusiLogicSetBus.GetLock(UserInfo.CompanyCD, "13", UserInfo.UserID, true);
        //法人委托书预警
        bool IsFRWTS = false;
        if (BusiLogicSetBus.GetAlertContent(UserInfo.CompanyCD, "14", UserInfo.UserID, true))
        {
            IsFRWTS = true;
        }
        dioFRWTS1.Checked = IsFRWTS;
        dioFRWTS2.Checked = !IsFRWTS;

        txtFRWTSDay.Value= BusiLogicSetBus.GetWaringDays(UserInfo.CompanyCD, "14", UserInfo.UserID, true);
        Lock5.Text = BusiLogicSetBus.GetLock(UserInfo.CompanyCD, "14", UserInfo.UserID, true);
    }

}
