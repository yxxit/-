<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InBus_ADD.aspx.cs" Inherits="Pages_JTHY_BusinessManage_InBus_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"   TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/jthy/TranSportInfo.ascx" TagName="TranSportInfo"  TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/jthy/PurContractInfo.ascx" TagName="PurContractInfo"  TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"  TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/jthy/TranStatus.ascx" TagName="TranStatus"  TagPrefix="uc8" %>
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
   
    
    <script src="../../../js/jthy/common/jt_Business.js" type="text/javascript"></script>
    <script src="../../../js/jthy/BusinessManage/InBus_ADD.js" type="text/jscript"></script>
    <script type="text/javascript">
        //转换
        function TransPortTypewitch()
         {

            var tranport=$("#drpTransPortType").val();
          
           if(tranport=="10")
           {
            $("#Tables").show()
                 $("#TableBJ1").show();
                 
                   $("#company").css("display","none");
           }
          
          else  if(tranport=="20"){
            
                 $("#Tables").hide();
                 $("#TableBJ1").hide();
                     $("#company").css("display","");
                
            }
             else if(tranport=="30"){
             
              $("#Tables").hide();
                 $("#TableBJ1").hide();
                 
                   $("#company").css("display","none");
             }
         }

    </script>


    

</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <uc2:ProviderInfo ID="ProviderInfo" runat="server" />
    <uc3:TranSportInfo ID="TranSportInfo" runat="server" />
    <uc4:PurContractInfo ID="PurContractInfo" runat="server" />
    <uc8:TranStatus ID="TranStatus1" runat="server" />
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="hidSendStatus" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />
    <input type="hidden" id="txtCreatorID" name="txtCreatorID" runat="server" />
    <input type="hidden" id="txtCreateDate" name="txtCreateDate" runat="server" />
    <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
    <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单" runat="server" />
    <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" runat="server" />
    <input type="hidden" id="txtConfirmorDate" name="txtConfirmorDate" runat="server" />
    <input type="hidden" id="Hidden1" name="txtModifiedUserID" runat="server" />
    <input type="hidden" id="Hidden2" name="txtModifiedDate" runat="server" />
    <input type="hidden" id="getOrderNO" />
    <span id="Forms" class="Spantype"></span>
    <div>
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="mainindex">
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="left" class="Title">
                                &nbsp;&nbsp;
                                <asp:Label ID="labTitle_Write1" runat="server" Text="">采购到货单</asp:Label>
                            </td>
                            <td align="right">
                                <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                    runat="server" onclick="SaveSellOrder();" />
                                <img id="imgUnSave" runat="server" alt="无法保存" src="../../../Images/Button/UnClick_bc.jpg"
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
                                到货单编号<span class="redbold">*</span>
                            </td>
                            <td class="tdColInput">
                                <div id="divCodeRule" runat="server">
                                    <uc5:CodingRuleControl ID="ddlArriveCode" runat="server" />
                                </div>
                                <div id="divArriveCode" style="display: none;" runat="server">
                                </div>
                            </td>
                            <td class="td_list_fields">
                                来源采购合同<span class="redbold">*</span>
                            </td>
                            <td class="tdColInput">
                                <input type="hidden" id="txtSourceBillID" value="" runat="server" />
                                <input type="text" id="txtSourceBillNo" name="txtSourceBillNo" class="tdinput" onclick="fnSelectContract()"
                                    style="width: 80%; border: 0px" />
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelectContract()" />
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
                                供应商
                            </td>
                            <td class="tdColInput">
                                <input id="txtProviderID" type="hidden" runat="server" />
                                <asp:TextBox ID="txtProviderName" runat="server" ReadOnly="true" class="tdinput"  Style="width: 80%;" ></asp:TextBox>
                                <%--<img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName');" />--%>
                            </td>
                            <td class="td_list_fields">
                                联系人
                            </td>
                            <td class="tdColInput">
                                <asp:TextBox ID="txtLinkMan" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"></asp:TextBox>
                            </td>
                            <td class="td_list_fields">
                                调运类型
                            </td>
                            <td class="tdColInput">                                
                                <select class="tddropdlist" id="drpTransPortType"  name="drpTransPortType" onchange="TransPortTypewitch();">
                                <option value="10" selected="selected">火运</option>
                                <option value="20">汽运</option>
                                <option value="30">客户自提</option>
                            </select>
                            </td>
                        </tr>
                        <tr class="table-item">
                            <td class="td_list_fields">
                                采购员
                            </td>
                            <td class="tdColInput">
                                <input id="txtPPersonID" type="hidden" runat="server" />
                                <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="true" class="tdinput" Style="width: 80%;"
                                    onclick="alertdiv('txtPPerson')"></asp:TextBox>
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                            </td>
                            <td class="td_list_fields">
                                总金额
                            </td>
                            <td class="tdColInput">
                                <asp:TextBox ID="txtSumMoney" runat="server" class="tdinput" Text="0.00" Enabled="false"
                                    Style="width: 95%;"></asp:TextBox>
                            </td>
                            <td class="td_list_fields">
                                运费
                            </td>
                            <td class="tdColInput">
                                <%--<asp:TextBox ID="txtFreight" runat="server" class="tdinput" Text="0.00" style="width: 95%;"   ></asp:TextBox>--%>
                                <input id="txtFreight" name="txtFreight" value="0.00" onkeyup='return ValidateNumber(this,value)'
                                    style="width: 95%; border: 0px;" />
                            </td>
                        </tr>
                        <tr >
                            <td class="td_list_fields">
                            部门
                            </td>
                            <td class="tdColInput">
                            <input id="DeptName" runat="server" readonly="readonly" type="text" class="tdinput"
                                    style="width: 80%;" onclick="alertdiv('DeptName,hdDeptID')" />
                                <input id="hdDeptID" type="hidden" runat="server" />
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('DeptName,hdDeptID')" />
                            </td>
                            <td class="td_list_fields">
                            备注                                
                            </td>
                            <td class="tdColInput" colspan="3">
                            <asp:TextBox ID="txtRemark" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>                                
                            </td>                            
                        </tr>
                        <tr class="table-item"  id="company">       
                         <td  class="td_list_fields" >运输公司</td>
                        <td>   
                           <input id="txtServiceId" type="hidden" runat="server" />
                         <asp:TextBox ID="txtServicesName" runat="server" class="tdinput" Style="width: 80%;"
                                   onclick="popProviderObj.ShowProviderList('txtServiceId','txtServicesName',null,null,null,'5');"></asp:TextBox>
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand; text-align:center; " onclick="popProviderObj.ShowProviderList('txtServiceId','txtServicesName',null,null,null,'5');" />
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
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                        <tr>
                            <td height="20px" class="td_list_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;煤种信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ2','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="TableBJ2">
                        <tr>
                            <td>  
                              
                             <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td  bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                            <%--<img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="imgAdd"
                                                style="cursor:hand;" onclick="AddShows();" />--%>
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="imgDel"
                                                style="cursor:hand;" onclick="fnDelOneRow();" />
                                        </td>
                                    </tr>
                                </table>                            
                                <div id="div2" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0"
                                        cellspacing="1" bgcolor="#999999">
                                        <tr class="table-item">
                                            <td class="td_main_detail" style="width: 5%;">
                                                选择<input type="checkbox" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                            </td>
                                            <td class="td_main_detail" style="width: 8%;">
                                                入库仓库
                                            </td>
                                            <td class="td_main_detail" style="width: 10%;">
                                                煤种<span class="redbold">*</span>
                                            </td>
                                            <td class="td_main_detail" style="width: 8%;">
                                                质量(热卡)
                                            </td>
                                            <td class="td_main_detail" style="width: 8%;">
                                                计量单位
                                            </td>
                                            <td class="td_main_detail" style="width: 10%;">
                                                数量<span class="redbold">*</span>
                                            </td>
                                            <td class="td_main_detail" style="width: 10%;">
                                                单价<span class="redbold">*</span>
                                            </td>
                                            <td class="td_main_detail" style="width: 10%;">
                                                金额
                                            </td>
                                            <td class="td_main_detail" style="width: 8%;">
                                                税率(%)
                                            </td>
                                            <td class="td_main_detail" style="width: 7%; display:none;">
                                                是否质检
                                            </td>
                                            <td class="td_main_detail" style="width: 6%; display:none;">
                                                已报检数量
                                            </td>
                                            <td class="td_main_detail" style="width: 6%;">
                                                已入库数量
                                            </td>
                                            <%--<td class="td_main_detail" style="width: 6%;">
                                                已结算数量
                                            </td>--%>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="99%" border="0" align="center" id="Tables" cellpadding="0" cellspacing="0" bgcolor="#999999">
                        <tr>
                            <td height="20px" class="td_list_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;调运详细
                                        </td>
                                        <td align="right">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ1','searchClick3')" />
                                            </div>
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
                                        <td style="padding-top: 5px; padding-left: 5px;">
                                            <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="img1"
                                                style="cursor:hand;" onclick="fnSelTranSport();" />
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="img2"
                                                style="cursor:hand;" onclick="JTClear_TranSport();" />
                                        </td>
                                    </tr>
                                </table> --%>
                                <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="dg_diaoyun" style="height: auto;" align="center"
                                        cellpadding="0" cellspacing="1" bgcolor="#999999">
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
                                                <asp:TextBox ID="txtTranSportNo" Enabled="false" onclick="fnSelTranSport();" runat="server"
                                                    class="tdinput" Style="width: 70%;"></asp:TextBox>
                                                <img src="../../../Images/default/add1.gif" alt="搜索" style="cursor: hand" onclick="fnSelTranSport();" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSendTime" runat="server" class="tdinput" Style="width: 80%;"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <input id="txtSendNum" name="txtSendNum" disabled="disabled" class="tdinput" onkeyup='return ValidateNumber(this,value)'>
                                            </td>
                                            <td><!--实收吨数-->
                                                <asp:TextBox ID="txtGetNum" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td><!--剩余吨数-->
                                                <asp:TextBox ID="txtResidueNum" runat="server" class="tdinput" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTranSportState" Enabled="false" runat="server" class="tdinput"
                                                    Style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCarNo" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td class="td_main_detail">
                                                <asp:TextBox ID="txtStartStation" runat="server" Enabled="false" class="tdinput"
                                                    Style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td class="td_main_detail">
                                                <asp:TextBox ID="txtEndStation" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td class="td_main_detail">
                                                <asp:TextBox ID="txtCarNum" runat="server" Enabled="false" class="tdinput" Style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td class="td_main_detail">
                                                <a href="#" onclick="TranStateMod()">修改状态</a>&nbsp;&nbsp;
                                                <%-- <asp:DropDownList ID="drpUPTranSportState" runat="server">
                                                  <asp:ListItem Value="10" Text="未生效"></asp:ListItem>
                                                  <asp:ListItem Value="20" Text="装车"></asp:ListItem>
                                                  <asp:ListItem Value="30" Text="发货"></asp:ListItem>
                                                  <asp:ListItem Value="40" Text="在途"></asp:ListItem>
                                                  <asp:ListItem Value="50" Text="到货" Selected></asp:ListItem>
                                                </asp:DropDownList>--%>
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
                            <td class="td_list_title">
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
                                <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                            </td>
                            <td class="td_list_fields">
                                建档人
                            </td>
                            <td class="tdColInput">
                                &nbsp;<asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                    Width="90%" Enabled="False"></asp:TextBox>
                            </td>
                            <td class="td_list_fields">
                                确认人
                            </td>
                            <td class="tdColInput" width="23%">
                                <input id="txtConfirmorId" name="txtConfirmorId" style="width: 95%; border: 0px;
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
                                <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                    Width="95%" disabled Text=""></asp:TextBox>
                            </td>
                            <td class="td_list_fields">
                                最后更新用户ID
                            </td>
                            <td class="tdColInput">
                                <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                    Width="93%" disabled Text=""></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <!-- End 默认信息 -->
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
