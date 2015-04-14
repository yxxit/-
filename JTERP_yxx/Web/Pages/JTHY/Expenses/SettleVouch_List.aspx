<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettleVouch_List.aspx.cs" Inherits="Pages_JTHY_Expenses_SettleVouch_List" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采购到货单列表</title>
     <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />   
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script charset="utf-8"  src="../../../js/jthy/Expenses/SettleVouch_List.js" type="text/javascript"></script>
    <script type="text/javascript">
    
    //定义回车事件skg2010-11-04
    function document.onkeydown() 
    { 
        if(event.keyCode == 13)
        {
            Fun_Search_ProductInfo();
            event.returnValue = false;
        } 
    }     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type="hidden" id="hiddExpOrder" value="" runat="server" />
    <input id="hidselpoint" type="hidden" runat="server" />
    <input type="hidden" id="hiddUrl" runat="server" />
    <input type="hidden" id="hfCustNo" runat="server" />
    <input type="hidden" id="hfCustID" runat="server" />
    <input id="hf_ID" type="hidden" />
     <input id="hidSearchCondition" type="hidden" runat="server" /> 
    <span id="Forms" class="Spantype"></span>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td  >
                <table width="99%" border="0" align="center" border="0" cellpadding="0" cellspacing="0" >
        
                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;检索条件
                        </td>
                        <td align="right" valign="middle" >
                            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                                &nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td >
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    
                                    <td class="td_list_fields">
                                        单据编号</td>
                                    <td class="tdColInput">
                                        <input name="txtSettleCode" id="txtSettleCode" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td class="td_list_fields">
                                        来源类型</td>
                                    <td class="tdColInput">
                                        
                                        <select id="sel_cBusTtype"  name="sel_cBusTtype" onchange="ChangeBill()" style="width:80%">
                                        <option value="1" selected="selected" >直销</option>
                                        <option value="2">采购到货</option>
                                        <option value="3">采购直销</option>
                                        </select>                          
                                        
                                    </td>
                                    <td class="td_list_fields" >
                                        来源单号
                                    </td>
                                    <td class="tdColInput"> 
                                        <input name="txtSourceBillNo" id="txtSourceBillNo" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td> 
                                </tr> 
                                <tr class="table-item">
                                    
                                    <td class="td_list_fields" >
                                        客户名称</td>
                                    <td class="tdColInput">
                                        <input name="txtCustName" id="txtCustName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td width="10%" height="20" class="td_list_fields" align="right">
                                        供应商名称</td>
                                    <td class="tdColInput" >
                                        <input name="txtProviderName" id="txtProviderName" class="tdinput" type="text"  disabled="disabled"
                                            style="width: 95%;" runat="server" />
                                    </td>
                                     <td width="10%" height="20" class="td_list_fields" align="right">
                                       经办人</td>
                                    <td class="tdColInput">
                                        <input name="txtcPersonName" id="txtcPersonName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td> 
                                </tr> 
                                
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='SearchSettleVouch(1)' id="btnQuery"  runat="server" 
/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>               
                </table>
            </td>
        </tr>
        
    </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1">
                        <td  align="left" valign="middle" >
                            &nbsp;&nbsp;业务信息
                        </td>
                        <td align="right" valign="middle" >
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.png" onclick="CreateNew();"  
                                 id="btnNew" />&nbsp;
                            <img alt="删除" id="btnDel" runat="server" src="../../../Images/Button/Main_btn_delete.png"
                                onclick="Fun_DeleteSettleVouch();" /> 
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        <tr>
            
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr class="table-item">
                            <th class="td_main_detail" width="6%" id="check">
                                选择<input type="checkbox" id="checkall" name="checkall" onclick="AllSelect('checkall','Checkbox1') ;" />
                            </th>
                            <th class="td_main_detail" width="12%" id="BillNo" >
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    单据编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th class="td_main_detail" width="12%" id="sourceNo" >
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    来源单号<span id="oC2" class="orderTip"></span></div>
                            </th>
                           
                            <th class="td_main_detail" width="8%" id="SourceType">
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    来源类型<span id="oC5" class="orderTip"></span></div>
                            </th>
                            
                            <th class="td_main_detail" width="12%" id="P_Settle" style="display:none;">
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    采购结算金额<span id="Span7" class="orderTip"></span></div>
                            </th>
                            <th class="td_main_detail" width="10%" id="P_Money" style="display:none;">
                                <div class="orderClick" onclick="OrderBy('Creator','Span9');return false;">
                                    采购总金额<span id="Span9" class="orderTip"></span></div>
                            </th>
                            <th class="td_main_detail" width="12%" id="S_Settle">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    销售结算金额<span id="Span8" class="orderTip"></span></div>
                            </th>  
                            <th class="td_main_detail" width="10%" id="S_Money">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span1');return false;">
                                    销售总金额<span id="Span1" class="orderTip"></span></div>
                            </th>  
                            <th class="td_main_detail" width="8%" id="PersonMan">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span2');return false;">
                                    经办人<span id="Span2" class="orderTip"></span></div>
                            </th>  
                            <th class="td_main_detail" width="8%" id="IsSure">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span3');return false;">
                                    <a title="包括 更改状态，">操作</a><span id="Span3" class="orderTip"></span></div>
                            </th>  
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_PagerList" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="hidden" id="Text2" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
