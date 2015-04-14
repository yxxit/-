<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransPort_ADD.aspx.cs" Inherits="Pages_JTHY_TransPortManage_TranSport_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl" TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/jthy/TranStatus.ascx" TagName="TranStatus" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>新建调运（调运单据新建）</title>
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/jthy/TransPortManage/TransPort_add.js" type="text/javascript"></script>
    
    <script type="text/javascript"> 
        function GetValuee(statuu) {
            $("#statusd").val(statuu);
        }
   
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <uc4:TranStatus ID="TranStatus" runat="server" />
    <input type="hidden" id="headid" runat="server" />
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="hidSendStatus" runat="server" />
    <input type="hidden" id="txtTranSportState" runat="server" />
    <input type="hidden" id="txtTranSportNo" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />
    <input type="hidden" id="txtCreatorID" name="txtCreatorID" runat="server" />
    <input type="hidden" id="txtCreateDate" name="txtCreateDate" runat="server" />
    <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
    <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单" runat="server" />
    <input type="hidden" id="txtConfirmorDate" name="txtConfirmorDate" runat="server" />
    <input type="hidden" id="Hidden1" name="txtModifiedUserID" runat="server" />
    <input type="hidden" id="Hidden2" name="txtModifiedDate" runat="server" />
    <input type="hidden" id="getOrderNO" />
    <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="left" class="Title">
                            &nbsp;&nbsp;
                            <asp:Label ID="labTitle_Write1" runat="server" Text="">调运单据新建</asp:Label>
                        </td>
                        <td align="right">
                            <img id="ImgUp" src="../../../Images/Button/Bottom_xiu.png" alt="修改状态" style="cursor: pointer; float: right" runat="server" onclick="xiugai()" />
                            <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                runat="server" onclick="SaveSellOrder();" />
                            <img id="imgUnSave" runat="server" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                style="display: none;" />
                            <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="审核生效" style="cursor: pointer;
                                display: none;" runat="server" onclick="Fun_ConfirmOperate();" />
                            <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法生效"
                                runat="server" />
                            <img id="UnConfirm" alt="取消生效" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer;
                                display: none;" onclick="cancelConfirm();" />
                            <img id="ImgUnConfirm" alt="无法取消生效" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;" />
                            <input type="hidden" id="Hidden3" runat="server" />
                            <input id="Hidden4" type="hidden" runat="server" />
                            <span runat="server" id="Span1"></span>
                            <input id="Text1" type="text" style="display: none;" />
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
                            调运单编号
                        </td>
                        <td class="td_list_edit">
                            <div id="divCodeRule" runat="server">
                                <uc3:CodingRuleControl ID="ddlTranSportID" runat="server" />
                            </div>
                            <asp:TextBox ID="txtTranSportID" runat="server" Enabled="false" class="tdinput" Style="width: 95%;
                                display: none"></asp:TextBox>
                        </td>
                        
                         <td align="right" class="td_list_fields" style="width: 10%">
                            经办人
                        </td>
                        <td class="td_list_edit">
                            <input id="txtPPersonID" type="hidden" runat="server" />
                            <asp:TextBox ID="txtPPerson" runat="server" class="tdinput" Style="width: 80%;" onclick="alertdiv('txtPPerson')"
                                ReadOnly="true"></asp:TextBox>
                            <img alt="搜索" src="../../../Images/default/Search1.gif" style="cursor: pointer;"
                                onclick="alertdiv('txtPPerson')" />
                        </td>
                        
                        <td class="td_list_fields">
                            车次
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtCarNo" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                        
                       
                    </tr>
                    <tr class="table-item">
                    
                     <td class="td_list_fields">
                            发站<span class="redbold">*</span>
                        </td>
                        <td class="td_list_edit">                            
                            <select name="drpStartStation"  class="tddropdlist"   runat="server" id="drpStartStation"></select>
                        </td>
                        
                        <td class="td_list_fields">
                            原到站
                        </td>
                        <td class="td_list_edit">                            
                            <select name="drpJh_place"  class="tddropdlist"   runat="server" id="drpJh_place"></select>
                        </td>
                        
                        <td class="td_list_fields">
                            实际到站<span class="redbold">*</span>
                        </td>
                        <td class="td_list_edit">                            
                            <select name="drpArriveStation" class="tddropdlist" runat="server" id="drpArriveStation"></select>
                        </td>
                        </tr>
                        <tr class="table-item">                        
                        <td class="td_list_fields">
                            发运日期
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtStartDate" runat="server" class="tdinput" Style="width: 80%;"
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"></asp:TextBox>
                            <img alt="搜索" src="../../../Images/datePicker.gif" />
                        </td>
                        
                        <td class="td_list_fields">
                            原收货人
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtJh_ReceMan" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            实收货人
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtSs_ReceMan" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr class="table-item">
                      <td   align="right">
                            计划车数<span class="redbold">*</span>
                        </td>
                        <td >
                            <input type="text" id="txtCarNum" onkeyup="return ValidateNumber(this,value)" name="txtVehicle_quantity"
                                style="width: 95%; border: 0px;" />
                        </td>
                        
                        <td   align="right">
                            实收车数
                        </td>
                        <td >
                            <input type="text" id="txtSs_VeQuan" onkeyup="return ValidateNumber(this,value)" name="txtCarNum"
                                style="width: 95%; border: 0px;" />
                        </td>
                        <td   align="right">
                            原发吨数<span class="redbold">*</span>
                        </td>
                        <td >
                            <input type="text" id="txtSendNum" onkeyup="return ValidateNumber(this,value)" name="txtSendNum"
                                style="width: 95%; border: 0px;" />
                        </td>
                    </tr>
                    <tr class="table-item">
                       <td align="right">
                            实收吨数
                        </td>
                        <td >
                            <input type="text" id="txtSs_quan" onkeyup="return ValidateNumber(this,value)" name="txtSs_quan"
                                style="width: 95%; border: 0px;" />
                        </td>
                        <td align="right" >
                            <span style="color: #B0B0B0">调运状态</span>
                        </td>
                        <td >
                            <input id="hdDeptID" type="hidden" runat="server" />
                            <input id="DeptName" runat="server" style="width: 80%; display: none" type="text"
                                class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" readonly="true" />
                            <img alt="搜索" src="../../../Images/default/Search1.gif" style="display: none; cursor: pointer;"  onclick="alertdiv('DeptName,hdDeptID')" />
                            <input type="text" id="statusd" disabled="disabled" name="statusd" class="tdinput"  runat="server" />
                        </td>
                        <td align="right"></td>
                        <td  valign="middle"></td>
                    </tr>
                    <tr  class="table-item">
                    <td align="right" >备注</td>
                    <td colspan="5" class="tdColInput">
                     <asp:TextBox ID="txtRemark" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                   </td>                   
                    </tr>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="20px" class="td_list_title" colspan="2">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;调运车详细信息
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
                    id="TableBJ">
                    <tr>
                        <td>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                <tr class="table-item">
                                    <td style="padding-top: 5px; padding-left: 5px;">
                                        <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="imgAdd"
                                            style="cursor: hand;" onclick="AddSignRow();" />
                                        <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="imgDel"
                                            style="cursor: hand;" onclick="fnDelOneRow();" />
                                    </td>
                                </tr>
                            </table>
                            <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0"
                                    cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td class="td_main_detail" style="width: 3%;">
                                            选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            车号
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            毛重
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            皮重
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            净重
                                        </td>
                                        <td class="td_main_detail" style="width: 5%;">
                                            累计
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
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"  id="Tb_04">
                <tr class="table-item">
                <td class="td_list_fields">
                            首车号
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtStartCarCode" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            尾车号
                        </td>
                        <td class="td_list_edit">
                            <asp:TextBox ID="txtEndCarCode" runat="server" class="tdinput" Style="width: 95%;"></asp:TextBox>
                        </td>
                        <td></td><td></td>
                
                </tr>
                    <tr class="table-item">
                        <td class="td_list_fields">
                            建档日期
                        </td>
                        <td class="td_main_detail">
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            建档人
                        </td>
                        <td class="td_main_detail">
                            <asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="80%" Enabled="False"></asp:TextBox>
                            <input visible="false" id="txtCreator" name="txtCreator" runat="server" />
                        </td>
                        <td height="20" align="right" class="td_list_fields" width="10%">
                            确认人
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="txtConfirmorId" name="txtConfirmorId" style="widows: 95%; border: 0px;
                                display: none;" />
                            <input id="txtConfirmor" name="txtConfirmor" disabled="disabled" style="widows: 95%;
                                border: 0px;" />
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td height="20" align="right" class="td_list_fields" width="10%">
                            确认日期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input id="txtConfirmDate" name="txtConfirmDate" disabled="disabled" style="widows: 95%;
                                border: 0px;" />
                        </td>
                        <td class="td_list_fields">
                            最后更新日期
                        </td>
                        <td class="td_main_detail">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            最后更新用户ID
                        </td>
                        <td class="td_main_detail">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="93%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <!-- End 默认信息 -->
    </form>
</body>
</html>
