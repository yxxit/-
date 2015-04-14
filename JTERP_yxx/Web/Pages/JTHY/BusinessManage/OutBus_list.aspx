<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutBus_list.aspx.cs" Inherits="Pages_JTHY_BusinessManage_OutBus_list" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc11" %>
    <%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel_Con"
    TagPrefix="uc111" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>直销列表+出库销售列表</title>
    
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
    
    <script charset="utf-8"  src="../../../js/jthy/BusinessManage/OutBusInfo.js" type="text/javascript"></script>
    
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
    <uc11:ProviderInfo ID="ProviderInfo" runat="server" /> 
    <uc111:CustNameSel_Con ID="CustNameSel_Con" runat="server" /> 
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
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"  bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"  class="table">
                                <tr class="table-item">                                    
                                    <td class="td_list_fields"><span  id="sendNoNo" >发货单号</span>
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtSendNo" id="txtSendNo" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    
                                    <td class="td_list_fields"><span  id="Span10" >销售合同号</span>
                                    </td>
                                    <td class="tdColInput">
                                        <input name="txtContractNo" id="txtContractNo" value="" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>  
                                    
                                    <td class="td_list_fields">
                                        调运单号</td>
                                    <td class="tdColInput">
                                        <input name="txtTranSNo" id="txtTranSNo" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>                                     
                                </tr> 
                                <tr class="table-item"> 
                                    <td class="td_list_fields">客户</td>
                                    <td class="td_list_edit" valign="middle" >
	                                    <input id="Hidden1" type="hidden" runat="server" value="1" />  
	                                    <input id="txtCustomerID" type="hidden" runat="server" />  
	                                    <asp:TextBox ID="txtCustomerName" runat="server"  style="width: 80%; border:0px; background-color:#FFFFFF;"  onclick="SearchCustData();"></asp:TextBox>
	                                    <img alt="搜索" src="../../../Images/default/Search1.gif"  id="search"     style=" cursor:pointer;" onclick="SearchCustData();" />
                                    </td>
                                    <%--<td class="tdColInput">
                                        <input name="txtCustName" id="txtCustName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>--%>                                     
                                    <td class="td_list_fields">部门</td>
                                    <td class="tdColInput">
                                       <input id="DeptName" runat="server" readonly="readonly" type="text" class="tdinput"
                                         style="width: 80%;" onclick="alertdiv('DeptName,hdDeptID')" />
                                        <input id="hdDeptID" type="hidden" value="" runat="server" />
                                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('DeptName,hdDeptID')" />                                    
                                    </td>
                                    
                                    <%--<td class="td_list_fields">单据状态</td>
                                    <td class="tdColInput">
                                        <input name="txtSendState" id="txtSendState" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>   --%>                                                                     
                                    <td class="td_list_fields">
                                        创建日期</td>
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
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                                 
                                <tr class="table-item" id="trProvider"> 
                                    <td class="td_list_fields">
                                        供货方</td>
                                    <td class="td_list_edit">
	                                    <input id="opr_addcontract" type="hidden" runat="server" value="1" />  
	                                    <input id="txtProviderID" type="hidden" runat="server" />   
	                                    <asp:TextBox ID="txtProviderName"  runat="server" ReadOnly="true"  class="tdinput" style="width: 80%; "   onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');"  ></asp:TextBox>
                                        <img alt="搜索" src="../../../Images/default/Search1.gif"   id="Img1"   style=" cursor:pointer;" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');" />
                                    </td>

                                    <%--<td class="tdColInput">
                                        <input name="txtProviderName" id="txtProviderName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td> --%>                                   
                                     <td class="td_list_fields">
                                        供货单号</td>
                                    <td class="tdColInput">
                                        <input name="txtPurContractNo" id="txtPurContractNo" value="" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    
                                     <td class="td_list_fields">业务员</td>
                                    <td class="tdColInput">
                                        <input name="txtSellerName" id="txtSellerName" class="tdinput" type="text" 
                                            style="width: 95%;" runat="server" />
                                    </td>
                                    
                                </tr>                                
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF" >                                     	
                                         <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='SearchOutBus(1)' id="btnQuery"  runat="server" />
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
                        <td align="right"  >   
                            <img id="btn_create" src="../../../images/Button/Bottom_btn_new.png" 
                                runat="server" alt="新建" style='cursor: hand;' onclick="CreateSaleOrder()" />&nbsp;
                            <img id="btn_del" src="../../../Images/Button/Main_btn_delete.png" alt="删除" style='cursor: hand;'
                                 runat="server" onclick="DelSelOrder()" />&nbsp;&nbsp;
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
                            <th     width="5%" class="td_main_detail"  >
                                选择<input type="checkbox" id="checkall" name="checkall" onclick="SelectAll();" />
                            </th>
                            <th   width="8%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('ProdNo','Span9');return false;">
                                    单据编号<span id="Span9" class="orderTip"></span></div>
                            </th>
                            <th   width="8%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('ProductName','Span7');return false;">
                                    收货单位<span id="Span7" class="orderTip"></span></div>
                            </th>
                           
                            <th   width="5%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('UnitName','Span6');return false;">
                                    煤种<span id="Span6" class="orderTip"></span></div>
                            </th>
                            
                            <th   width="5%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('Creator','Span5');return false;">
                                    数量<span id="Span5" class="orderTip"></span></div>
                            </th>
                            <th   width="5%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('Creator','Span11');return false;">
                                    单价<span id="Span11" class="orderTip"></span></div>
                            </th>
                            <th   width="8%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('Creator','Span4');return false;">
                                    总金额<span id="Span4" class="orderTip"></span></div>
                            </th>
                            <th   width="7%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    发站<span id="Span8" class="orderTip"></span></div>
                            </th>  
                            <th   width="7%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span1');return false;">
                                    到站<span id="Span1" class="orderTip"></span></div>
                            </th>  
                            <th   width="7%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span2');return false;">
                                    调运状态<span id="Span2" class="orderTip"></span></div>
                            </th> 
                            <th   width="6%" class="td_main_detail"  >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span3');return false;">
                                    <a title="包括 更改状态，">操作</a><span id="Span3" class="orderTip"></span></div>
                            </th>                             
                            <th   width="5%" class="td_main_detail"  >部门                               
                            </th>                            
                            <th   width="7%" class="td_main_detail"  >销售合同                                
                            </th>  
                            <th   width="7%" class="td_main_detail" id="purContractList" >采购合同                                
                            </th>
                            <th   class="td_main_detail">调运单号                   
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
