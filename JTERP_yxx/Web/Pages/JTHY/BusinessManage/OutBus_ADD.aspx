<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutBus_ADD.aspx.cs" Inherits="Pages_JTHY_BusinessManage_OutBus_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/jthy/TranSportInfo.ascx" TagName="TranSportInfo"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/jthy/SelContractInfo.ascx" TagName="SelContractInfo"    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/jthy/PurContractInfo.ascx" TagName="PurContractInfo_1"   TagPrefix="uc6" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/jthy/TranStatus.ascx" TagName="TranStatus"
    TagPrefix="uc8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/jthy/BusinessManage/OutBus_add.js" type="text/javascript"></script>

    <script src="../../../js/jthy/common/jt_Business.js" type="text/javascript">
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <uc2:CustNameSel ID="CustNameSel" runat="server" />
    <uc4:TranSportInfo ID="TranSportInfo" runat="server" />
    <uc5:SelContractInfo ID="SelContractInfo" runat="server" />
    <uc6:PurContractInfo_1 ID="PurContractInfo_1" runat="server" />
    <uc7:ProviderInfo ID="ProviderInfo" runat="server" />
    <uc8:TranStatus ID="TranStatus" runat="server" />
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />
    <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
    <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单" runat="server" />
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="getOrderNO" /><div id="orderNo1">
    </div>
    <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="left" class="Title">
                            &nbsp;&nbsp;
                            <asp:Label ID="labTitle_Write1" runat="server" Text="">出库销售单新建</asp:Label>
                        </td>
                        <td align="right">
                            <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                runat="server" onclick="SaveSellOrder();" />
                            <img id="imgUnSave" runat="server" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                style="display: none;" />
                            <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="审核生效" style="cursor: pointer;
                                display: none;" runat="server" onclick="Fun_ConfirmOperate();" />
                            <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法生效"
                                runat="server" />
                            <img id="UnConfirm" alt="取消生效" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer;
                                display: none;" onclick="cancelConfirm();" visible="false" />
                            <img id="ImgUnConfirm" alt="无法取消生效" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;" />
                            <input type="hidden" id="hidUpDateTime" runat="server" />
                            <input id="headid" type="hidden" runat="server" />
                            <span runat="server" id="GlbFlowButtonSpan"></span>
                            <input id="txtOprtID" type="text" style="display: none;" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="3">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr class="table-item">
                        <td class="td_list_fields">
                            发货单编号<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <div id="divCodeRule" runat="server">
                                <uc3:CodingRuleControl ID="ddlSendNo" runat="server" />
                            </div>
                        </td>
                        <td class="td_list_fields">
                            来源销售合同<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <input id="txtSourceBillID" type="hidden" runat="server" />
                            <input type="text" id="txtSourceBillNo" name="txtSourceBillNo" class="tdinput" onclick="fnSelectSellContract()"
                                style="width: 80%; border: 0px" />
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelectSellContract()" />
                        </td>
                        <td class="td_list_fields">
                            结算方式
                        </td>
                        <td class="tdColInput">
                            <select name="drpSettleType" class="tddropdlist" runat="server" id="drpSettleType">
                            </select>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_list_fields">
                            收货单位
                        </td>
                        <td class="tdColInput">
                            <input id="opr_addoutbus" type="hidden" runat="server" value="" />
                            <input id="txtCustomerID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtCustomerName" runat="server" class="tdinput" Style="width: 80%;"></asp:TextBox>
                            <%--onclick="SearchCustData();"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="SearchCustData();" />--%>
                        </td>
                        <td class="td_list_fields">
                            开票单位
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtInvoiceUnit" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            调运类型
                        </td>
                        <td class="tdColInput">
                            <select class="tddropdlist" id="drpTransPortType" name="drpTransPortType" onchange="TransPortTypewitchs()">
                                <option value="10" selected="selected">火运</option>
                                <option value="20">汽运</option>
                                <option value="30">客户自提</option>
                            </select>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_list_fields">
                            经办人
                        </td>
                        <td class="tdColInput">
                            <input id="txtPPersonID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="false" class="tdinput" Style="width: 80%;"
                                onclick="alertdiv('txtPPerson')"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                        </td>
                        <td class="td_list_fields">
                            运费
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtTransportFee" runat="server" class="tdinput" onkeyup='return ValidateNumber(this,value)'
                                Style="width: 95%;"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            销售金额
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtSumMoney" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"
                                Text="0.00"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="table-item" style="display: none">
                        <td class="td_list_fields">
                            业务类型
                        </td>
                        <td class="tdColInput">
                            <select name="drpBusiType" class="tddropdlist" runat="server" id="drpBusiType" disabled>
                                <option value="1" selected="selected">普通销售</option>
                                <option value="2">采购直销</option>
                            </select>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="table-item" id="PurProperty1">
                        <td class="td_list_fields">
                            来源采购合同<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <input id="txtPurContractID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtPurContractNo" runat="server" class="tdinput" onclick="fnSelectPurContract()"
                                Style="width: 80%;"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelectPurContract()" />
                        </td>
                        <td class="td_list_fields">
                            供货商
                        </td>
                        <td class="tdColInput">
                            <input id="txtProviderID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtProviderName" runat="server" class="tdinput" Style="width: 80%;"></asp:TextBox>
                            <%--onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');"></asp:TextBox>
               <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');" />--%>
                        </td>
                        <td class="td_list_fields">
                            采购金额
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtSupplyAmount" Enabled="false" runat="server" class="tdinput"
                                Style="width: 95%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="table-item" id="company">
                        <td class="td_list_fields">
                            运输公司
                        </td>
                        <td>
                            <input id="txtServiceID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtcompany" runat="server" class="tdinput" Style="width: 80%;" onclick="popProviderObj.ShowProviderList('txtServiceID','txtcompany',null,null,null,'5');"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand; text-align: center;"
                                onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'5');" />
                        </td>
                        <td class="td_list_fields">
                        </td>
                        <td>
                        </td>
                        <td class="td_list_fields">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_list_fields">
                            部门
                        </td>
                        <td class="tdColInput">
                            <input id="DeptName" runat="server" readonly="readonly" type="text" class="tdinput"
                                style="width: 80%; border: 0px;" onclick="alertdiv('DeptName,hdDeptID')" />
                            <input id="hdDeptID" type="hidden" runat="server" />
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('DeptName,hdDeptID')" />
                        </td>
                        <td class="td_list_fields">
                            备注
                        </td>
                        <td colspan="3" class="tdColInput">
                            <asp:TextBox ID="txtRemark" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="20px" class="td_list_title" colspan="2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;煤种信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick3'>
                                            <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ','searchClick3')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                    id="TableB1">
                    <tr>
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr class="table-item">
                                    <td bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                        <%--<img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="imgAdd"
                                                style="cursor:hand;" onclick="AddShows();" />--%>
                                        <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="imgDel"
                                            style="cursor: hand;" onclick="fnDelOneRow();" />
                                    </td>
                                </tr>
                            </table>
                            <div id="div2" style="width: 100%; background-color: #FFFFFF;">
                                <%--<table width="100%" border="0" id="TableCoalInfo" style="height: auto;" align="center"  id="dg_Log" --%>
                                <table width="100%" border="0" style="height: auto;" align="center" id="dg_Log" cellpadding="0"
                                    cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td class="td_main_detail" style="width: 3%;">
                                            选择<input type="checkbox" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                        </td>
                                        <td class="td_main_detail" id="wareName">
                                            仓库
                                        </td>
                                        <td class="td_main_detail" id="colName">
                                            煤种名称<span class="redbold">*</span>
                                        </td>
                                        <td class="td_main_detail" id="colNo">
                                            计量单位
                                        </td>
                                        <td class="td_main_detail" width="10%">
                                            数量<span class="redbold">*</span>
                                        </td>
                                        <td class="td_main_detail" id="PurProperty2" width="10%">
                                            进货单价<span class="redbold">*</span>
                                        </td>
                                        <td class="td_main_detail" width="10%">
                                            销售单价<span class="redbold">*</span>
                                        </td>
                                        <td class="td_main_detail" width="7%">
                                            税率(%)
                                        </td>
                                        <td class="td_main_detail" width="10%">
                                            税额
                                        </td>
                                        <td class="td_main_detail" width="10%" id="PurProperty4">
                                            购货金额
                                        </td>
                                        <td class="td_main_detail" width="10%">
                                            销货金额
                                        </td>
                                        <%--//<td class="td_main_detail" width="5%">--%>
                                        <%--<td class="td_main_detail" style="display:none;">
                                            已出库数量
                                        </td>--%>
                                        <%--<td class="td_main_detail" width="5%">--%>
                                        <td class="td_main_detail">
                                            已结算数量
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" id="Tables" border="0" align="center" cellpadding="0" cellspacing="1"
                    bgcolor="#999999">
                    <tr>
                        <td height="20px" class="td_list_title" colspan="2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;调运详细
                                    </td>
                                    <td align="right">
                                        <div id='searchClick4'>
                                            <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ1','searchClick3')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                    id="TableBJ1">
                    <tr>
                        <td>
                            <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td >
                                            <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="img1"
                                                style="cursor:hand;" onclick="fnSelTranSport();" />
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="img2"
                                                style="cursor:hand;" onclick="JTClear_TranSport();" />
                                        </td>
                                    </tr>
                                </table> --%>
                            <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                <table width="100%" border="0" style="height: auto;" align="center" cellpadding="0"
                                    cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td class="td_main_detail" style="width: 12%;">
                                            调运单号<%--<span class="redbold">*</span>--%>
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            发运时间
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            原发吨数
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            实收吨数
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            剩余吨数
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            当前状态
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            车次
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            发站
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            到站
                                        </td>
                                        <td class="td_main_detail" style="width: 8%;">
                                            发车数
                                        </td>
                                        <td class="td_main_detail" style="width: 10%;">
                                            <a title="包括 更改状态，">操作</a>
                                        </td>
                                    </tr>
                                    <tr class="table-item">
                                        <td class="td_main_detail">
                                            <input type="hidden" id="txtTranSportID" value="" runat="server" />
                                            <asp:TextBox ID="txtTranSportNo" Enabled="false" runat="server" class="tdinput" Style="width: 80%;"
                                                onclick="fnSelTranSport();"></asp:TextBox>
                                            <img src="../../../Images/default/add1.gif" alt="搜索" style="cursor: hand" onclick="fnSelTranSport();" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSendTime" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSendNum" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td><!--实收吨数-->
                                            <asp:TextBox ID="txtGetNum" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td><!--剩余吨数-->
                                            <asp:TextBox ID="txtResidueNum" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="td_main_detail">
                                            <asp:TextBox ID="txtTranSportState" Enabled="false" runat="server" class="tdinput"
                                                Style="width: 95%;"></asp:TextBox>
                                        </td>
                                        <td class="td_main_detail">
                                            <asp:TextBox ID="txtCarNo" Enabled="false" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtStartStation" Enabled="false" runat="server" class="tdinput"
                                                Style="width: 95%;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEndStation" Enabled="false" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCarNum" Enabled="false" runat="server" class="tdinput" onkeyup='return ValidateNumber(this,value)'
                                                Style="width: 95%;"></asp:TextBox>
                                        </td>
                                        <td>
                                            <a href="#" onclick="TranStateMod()">修改状态</a>&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;附加信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonNote'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divButtonNote')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04">
                    <tr class="table-item">
                        <td class="td_list_fields">
                            建档日期
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txt_CreateDate" Enabled="false" runat="server" CssClass="tdinput"
                                Width="80%"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            建档人
                        </td>
                        <td class="tdColInput">
                            &nbsp;<asp:TextBox ID="UserPrincipal" Enabled="false" runat="server" CssClass="tdinput"
                                ReadOnly="true" Width="90%"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            确认人
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="txtConfirmorId" name="txtConfirmorId" style="widows: 95%; border: 0px;
                                display: none;" />
                            <input id="txtConfirmor" name="txtConfirmor" disabled="disabled" style="width: 95%;
                                border: 0px;" />
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_list_fields">
                            确认日期
                        </td>
                        <td class="tdColInput" width="23%">
                            <input id="txtConfirmDate" name="txtConfirmDate" disabled="disabled" style="width: 95%;
                                border: 0px;" />
                        </td>
                        <td class="td_list_fields">
                            最后更新日期
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" Enabled="false" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" Text=""></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            最后更新用户ID
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedUserID" Enabled="false" MaxLength="50" runat="server"
                                CssClass="tdinput" Width="93%" Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <!-- End 默认信息 -->
    </form>
</body>
</html>
