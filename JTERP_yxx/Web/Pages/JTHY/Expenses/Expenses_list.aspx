<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expenses_list.aspx.cs" Inherits="Pages_JTHY_Expenses_Expenses_list" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调运单列表</title>
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>  
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript" ></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/jthy/Expenses/Expenses_List.js" type="text/javascript"></script>
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
    <span id="Forms" class="Spantype"></span> 
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
	    <tr align="center">
	        <td>
	            <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1" >
                        <td align="left" valign="middle">  
                         &nbsp;&nbsp;检索条件            
                        </td>
                        <td align="right" >
            	            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />            
                            </div>
                        </td>
                    </tr>
	            </table>
	        </td>
	    </tr>        
        <tr>
            <td >
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"  bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"  class="table">
                                <tr class="table-item">                                    
                                    <td class="td_list_fields" >
                                        单据编号</td>
                                    <td class="tdColInput">
                                        <input name="txtExpCode" id="txtExpCode" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td width="10%"   class="td_list_fields" align="right">
                                        申请人</td>
                                    <td class="tdColInput">
                                        <input name="txtApplyor" id="txtApplyor" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td width="10%"   class="td_list_fields" align="right">
                                        经办人
                                     </td>
                                    <td class="tdColInput">
                                        <input name="txtTransactorName" id="txtTransactorName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td> 
                                    
                                     <%--<td width="10%"   class="td_list_fields" align="right">
                                        &nbsp;申请金额</td>
                                    <td class="tdColInput"> 
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="width: 45%">
                                                    <input name="txtMinTotalAmount" id="txtMinTotalAmount" class="tdinput" type="text" 
                                                        style="width: 95%;" runat="server" />
                                                <td style="width: 10%">
                                                    <input id="Text3" runat="server" class="tdinput" value="至" type="text" style="width: 88%;" />
                                                </td>
                                                <td style="width: 45%">
                                                    <input name="txtMaxTotalAmount" id="txtMaxTotalAmount" class="tdinput" type="text" 
                                                        style="width: 95%;" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td> --%>
                                </tr> 
                                <tr class="table-item">
                                    
                                    <td width="10%"   class="td_list_fields" align="right">
                                        申请日期</td>
                                    <td class="tdColInput">
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="width: 45%">
                                                    <input type="text" name="txtBeginT" id="txtBeginT" runat="server" style="width: 95%;" class="tdinput"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" readonly />
                                                </td>
                                                <td style="width: 10%">
                                                    <input id="Text8" runat="server" class="tdinput" value="至" type="text" style="width: 88%;" />
                                                </td>
                                                <td style="width: 45%">
                                                    <input type="text" id="txtEndT" runat="server" style="width: 95%;" class="tdinput"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" readonly />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="10%"   class="td_list_fields" align="right">
                                        往来单位</td>
                                    <td class="tdColInput">
                                        <input name="txtCustName" id="txtCustName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td width="10%"   class="td_list_fields" align="right">
                                    </td>
                                    <td class="tdColInput">
                                    </td>
                                </tr> 
                                
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF" >                                     	
                                        <img alt="检索" src="/images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='SearchExpensesData(1)' id="btnQuery"  runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>               
                </table>
            </td>
        </tr>        
    </table>
    
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"  id="mainindex">      
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1" >
                        <td align="left" valign="middle">  
                         &nbsp;&nbsp;费用申请单列表            
                        </td>
                        <td align="right"  >   
                            <img id="btn_create" src="../../../images/Button/Bottom_btn_new.png" 
                                runat="server" alt="新建" style='cursor: hand;' onclick="CreateExpenses()" />&nbsp;
                            <img id="btn_del" src="../../../Images/Button/Main_btn_delete.png" alt="删除" style='cursor: hand;'
                                 runat="server" onclick="DelExpensesInfo()" />&nbsp;&nbsp;
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
                            <th   width="6%" class="td_main_detail" >
                                选择<input type="checkbox" id="checkall" name="checkall" onclick="AllSelect('checkall','Checkbox1') ;" />
                            </th>
                            <th width="13%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    单据编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th width="11%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    申请人<span id="oC2" class="orderTip"></span></div>
                            </th>
                           <th width="11%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProductName','Span3');return false;">
                                    申请日期<span id="Span3" class="orderTip"></span></div>
                            </th>                           
                            <th width="11%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    金额<span id="oC5" class="orderTip"></span></div>
                            </th>
                            
                            <th width="13%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    往来单位<span id="Span7" class="orderTip"></span></div>
                            </th>                            
                            <th width="9%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    经办人<span id="Span8" class="orderTip"></span></div>
                            </th>   
                            <th width="9%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span1');return false;">
                                    确认人<span id="Span1" class="orderTip"></span></div>
                            </th>   
                            <th width="11%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span2');return false;">
                                    确认日期<span id="Span2" class="orderTip"></span></div>
                            </th>                             
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr >
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
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
