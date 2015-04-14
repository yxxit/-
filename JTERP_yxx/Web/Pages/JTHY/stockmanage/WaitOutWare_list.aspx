<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WaitOutWare_list.aspx.cs" Inherits="Pages_JTHY_StockManage_WaitOutWare_list" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc11" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调运单列表</title>
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>  
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
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

</head>
<body>
<uc11:ProviderInfo ID="ProviderInfo" runat="server" /> 
    <form id="form1" runat="server">    
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
	    <tr align="center">
	        <td >
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
                                    <td width="10%"   class="td_list_fields" align="right">
                                        发货单号</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_ProdNo" specialworkcheck="物品编号" name="txtConfirmorReal0"
                                            class="tdinput" runat="server" /><input id="txt_ID" runat="server" type="hidden" />
                                    </td>
                                    <td width="10%"   class="td_list_fields" align="right">
                                        供货方</td>
                                    <td class="td_list_edit">
                                        <input id="opr_addcontract" type="hidden" runat="server" value="1" />  
                                        <input id="txtProviderID" type="hidden" runat="server" />   
                                        <asp:TextBox ID="txt_ProductName"  runat="server" ReadOnly="true"  class="tdinput" style="width: 80%; "   onclick="popProviderObj.ShowProviderList('txtProviderID','txt_ProductName',null,null,null,'0');"  ></asp:TextBox>
                                        <img alt="搜索" src="../../../Images/default/Search1.gif"   id="search"   style=" cursor:pointer;" onclick="popProviderObj.ShowProviderList('txtProviderID','txt_ProductName',null,null,null,'0');" />
                                    </td>
                                    <%--<td width="20%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="txt_ProductName" specialworkcheck="物品名称" MaxLength="50" runat="server"
                                            CssClass="tdinput" Width="51%"></asp:TextBox>
                                    </td>--%>
                                     <td width="10%"   class="td_list_fields" align="right">
                                        &nbsp;发货日期</td>
                                    <td width="20%" bgcolor="#FFFFFF"> 设定范围值
                                        &nbsp;</td> 
                                </tr> 
                                <tr class="table-item">
                                    
                                    <td width="10%"   class="td_list_fields" align="right">
                                        业务员</td>
                                        <td class="tdColInput">
	                                        <input id="txtPPersonID" type="hidden" runat="server" />
	                                        <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="false" class="tdinput" Style="width: 80%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
	                                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                                        </td>
                                   <%-- <td width="20%" bgcolor="#FFFFFF">
                                        没有js,想办法添加js
                                        <input type="text" id="Text1" specialworkcheck="物品编号" name="txtConfirmorReal0"
                                            class="tdinput" runat="server" /><input id="Hidden1" runat="server" type="hidden" />
                                    </td>--%>
                                    <td width="10%"   class="td_list_fields" align="right">
                                        发站</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <asp:TextBox ID="TextBox1" specialworkcheck="物品名称" MaxLength="50" runat="server"
                                            CssClass="tdinput" Width="51%"></asp:TextBox>
                                    </td>
                                     <td width="10%"   class="td_list_fields" align="right">
                                        &nbsp;运送状态</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        &nbsp;</td> 
                                </tr> 
                                
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF" >                                     	
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='Fun_Search_ProductInfo()' id="btnQuery"  runat="server" />
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
                         &nbsp;&nbsp;普通销售列表            
                        </td>
                        <td align="right" valign="middle" > 
                        
                            <img id="btn_create" src="../../../images/Button/Bottom_btn_new.png" 
                                runat="server" alt="新建" style='cursor: hand;' onclick="CreateCust()" />&nbsp;
                            <img id="btn_del" src="../../../Images/Button/Main_btn_delete.png" alt="删除" style='cursor: hand;'
                                runat="server" onclick="Fun_Delete_ProductInfo()" />&nbsp;&nbsp;
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
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th width="12%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    单据编号</div>
                            </th>
                            <th width="12%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    发货方</div>
                            </th>
                           
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    煤种</div>
                            </th>
                            
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    数量</div>
                            </th>
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    总金额</div>
                            </th>
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    发站<span id="Span8" class="orderTip"></span></div>
                            </th>  
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    到站<span id="Span1" class="orderTip"></span></div>
                            </th>  
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    调运状态<span id="Span2" class="orderTip"></span></div>
                            </th>  
                            <th width="10%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    <a title="包括 更改状态，">操作</a><span id="Span3" class="orderTip"></span></div>
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
