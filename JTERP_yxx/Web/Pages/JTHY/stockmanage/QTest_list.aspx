<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QTest_list.aspx.cs" Inherits="Pages_JTHY_StockManage_QTest_list" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>质检单列表</title>
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>  
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript" ></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/jthy/stockmanage/QTest_List.js" type="text/javascript"></script>
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
     <input id="hidSearchCondition" type="hidden" runat="server" /> 
    <span id="Forms" class="Spantype"></span>
    
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
	<tr align="center"><td>
	<table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
        <tr class="menutitle1" >
            <td align="left" valign="middle">  
             &nbsp;&nbsp;检索条件            
            </td>
            <td align="right" >
            	<div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>            
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
                                    <td class="td_list_fields">
                                        质检单号</td>
                                    <td class="tdColInput">
                                        <input name="txtReportNo" id="txtReportNo" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    <td class="td_list_fields">
                                        供货方</td>
                                    <td class="tdColInput">
                                        <input name="txtOtherCorpName" id="txtOtherCorpName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                     <td class="td_list_fields">
                                        质检日期</td>
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
                                </tr> 
                                <tr class="table-item">
                                    
                                    <td class="td_list_fields">
                                        煤种</td>
                                    <td class="tdColInput"> 
                                        <input name="txtProductName" id="txtProductName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />                                       
                                    </td>
                                    <td class="td_list_fields">
                                        发站</td>
                                    <td class="tdColInput">
                                        <input name="txtStartPlace" id="txtStartPlace" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                     <td class="td_list_fields">
                                        到货单编号</td> 
                                     <td class="tdColInput">
                                        <input name="txtInBusNo" id="txtInBusNo" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td> 
                                </tr> 
                                
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF" >                                     	
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='SearchQTestList(1)' id="btnQuery"  runat="server" />
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
             &nbsp;&nbsp;质检单列表            
            </td>
            <td align="right"  > 
                        
                     
                <img alt="新建" src="../../../Images/Button/Bottom_btn_new.png" id="btn_new" runat="server"
                            onclick="CreateNew();" />&nbsp;
                <img alt="删除" src="../../../Images/Button/Main_btn_delete.png" id="btn_delete"
                            runat="server" onclick="DelQTestInfo();"   />&nbsp;
             </td>
                                                      
        </tr>
	     </table></td>                 
        </tr>     
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr class="table-item">
                            <th   width="8%" class="td_main_detail" >
                                选择<input type="checkbox" id="checkall" name="checkall" onclick="AllSelect('checkall','Checkbox1');" />
                            </th>
                            <th width="14%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('reportno','oGroup');return false;">
                                    单据编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th width="14%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('providername','oC2');return false;">
                                 质检结果  <span id="oC2" class="orderTip"></span></div>
                            </th>
                           
                            <th width="12%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('coalname','oC5');return false;">
                                    检验人<span id="oC5" class="orderTip"></span></div>
                            </th>
                            
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('checknum','Span3');return false;">
                                    检验项目<span id="Span3" class="orderTip"></span></div>
                            </th>
                            
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ship_place','Span8');return false;">
                                    检验值<span id="Span8" class="orderTip"></span></div>
                            </th>  
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('to_place','Span1');return false;">
                                    创建人<span id="Span1" class="orderTip"></span></div>
                            </th>  
                        
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('checkresult','Span4');return false;">
                                    操作<span id="Span4" class="orderTip"></span></div>
                            </th>
                            
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr >
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
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
