<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransPort_list.aspx.cs" Inherits="Pages_JTHY_TransPortManage_TranSport_list" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调运单列表</title>
    
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />
    
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>
     
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/jthy/transportmanage/Transport_List.js" type="text/javascript"></script>
    
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

    <style type="text/css">
        .style1
        {
            background-color: #FFFFFF;
            width: 11%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hidisCust" runat="server" />
    <input type="hidden" id="hiddExpOrder" value="" runat="server" />
    <input id="hidselpoint" type="hidden" runat="server" />
    <input type="hidden" id="hiddUrl" runat="server" />
    <input type="hidden" id="hfCustNo" runat="server" />
    <input type="hidden" id="hfCustID" runat="server" />
    <input id="hf_ID" type="hidden" />
    <span id="Forms" class="Spantype"></span>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="middle" >
                <table width="99%" border="0" align="center" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;检索条件
                        </td>
                        <td  align="right" valign="middle">
                            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
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
                                    
                                    <td class="td_list_fields" >
                                        车次
                                    </td>
                                    <td class="style1">
                                        <input type="text" id="txt_motorcade"  name="txt_motorcade" class="tdinput" />
                                    </td>
                                   
                                    <td class="td_list_fields" >
                                        发站
                                    </td>
                                    <td class="style1">
                                        <input type="text" id="txt_ship_place"  name="txt_ship_place" class="tdinput"  />
                                    </td> 
                                    <td class="td_list_fields" >
                                        到站
                                    </td>
                                    <td class="style1">
                                        <input type="text" id="txt_to_place"  name="txt_to_place" class="tdinput"  />
                                     </td> 
                                    <td class="td_list_fields" >
                                        发运日期
                                    </td>
                                    <td class="tdColInput">
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="width: 45%">
                                                    <input type="text" name="txtBeginT" id="txtBeginT" runat="server" style="width: 95%;" class="tdinput"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})"  />
                                                </td>
                                                <td style="width: 10%">
                                                    <input id="Text8" runat="server" class="tdinput" value="至" type="text" style="width: 88%;" />
                                                </td>
                                                <td style="width: 45%">
                                                    <input type="text" id="txtEndT" runat="server" style="width: 95%;" class="tdinput"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr> 
                                <tr >
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='SearchTransPort(1)' id="btnQuery"  runat="server" />
                                        <input id="hidSearchCondition" type="hidden" runat="server" /> 
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
                            &nbsp;&nbsp;调运单列表
                        </td>
                        <td align="right" valign="middle">
                             
                            
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.png" id="btn_new" runat="server"
                            onclick="CreateNew();" />&nbsp;
                            <img alt="删除" id="btnDel" runat="server"   src="../../../Images/Button/Bottom_btn_del.png"
                                onclick="DelTransPortInfo();" /> 
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
                            <th width="3%" class="td_main_detail" >
                                选择<input type="checkbox" id="checkall" name="checkall" onclick="fnSelectAll();" />
                            </th>
                             <th width="8%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    调运单号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('ProdNo','Span1');return false;">
                                    发运日期<span id="Span1" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    车次<span id="oC2" class="orderTip"></span></div>
                            </th>
                           
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    发站<span id="oC5" class="orderTip"></span></div>
                            </th>
                            
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('Creator','Span4');return false;">
                                    到站<span id="Span4" class="orderTip"></span></div>
                            </th>
                            <th width="6%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('Creator','Span5');return false;">
                                    发运数量<span id="Span5" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    建档时间<span id="Span8" class="orderTip"></span></div>
                            </th> 
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('checkresult','Span2');return false;">
                                    操作<span id="Span2" class="orderTip"></span></div>
                            </th> 
                                <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('checkresult','Span2');return false;">
                                    调运状态<span id="Span3" class="orderTip"></span></div>
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
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
