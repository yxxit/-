<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellContract_List.aspx.cs" Inherits="Pages_JTHY_ContractManage_SellContract_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc2" %>
    <%@ Register Src="~/UserControl/DeleteBill.ascx" TagName="DeleteBill" TagPrefix="uc8" %>

<%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel_Con"
    TagPrefix="uc11" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>销售合同查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/jthy/ContractManage/ContractInfo.js" type="text/javascript"></script>    


    <script type="text/javascript">
        function showDeleteDiv() {
            $("#deleteBill").show(500);
            DeleteBill();
        }
    </script>

</head>
<body>
    <uc11:CustNameSel_Con ID="CustNameSel_Con" runat="server" /> 
    <form id="form1" runat="server">
    <input type="hidden" id="hidisCust" runat="server" />
    <input type="hidden" id="hiddExpOrder" value="" runat="server" />
    <input id="hidselpoint" type="hidden" runat="server" />
    <input type="hidden" id="hiddUrl" runat="server" />
    <input type="hidden" id="hfCustNo" runat="server" />
    <input type="hidden" id="hfCustID" runat="server" />
    
    <span id="Forms" class="Spantype"></span>
    <uc2:GetBillExAttrControl ID="GetBillExAttrControl2" runat="server" />
    <input type="hidden" id="hidEFIndexP" runat="server" />
    <input type="hidden" id="hidEFDescP" runat="server" />  
    <uc8:DeleteBill ID="DeleteBill1"  runat="server"/>    
    

    <table width="98%" border="0"  cellpadding="0" cellspacing="0"   id="mainindex">
        <tr>
            <td valign="middle" align="center">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
        
                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                             &nbsp;&nbsp;检索条件            
                        </td>
                        <td align="right" >
        	                <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>            
                        </td>
                    </tr>
                </table>
            </td> 
        </tr>        
        <tr>
            <td >
                <table width="99%" border="0" align="center"   cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td class="td_list_fields" >
                                        合同号
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtOrderNo" class="tdinput" type="text" runat="server" style="width: 90%" />
                                    </td>
                                    <td class="td_list_fields">
                                        煤种
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtProductName" class="tdinput" type="text" runat="server" style="width: 90%" />
                                    </td>
                                    <td class="td_list_fields">
                                        质量(热卡)
                                    </td>
                                    <td class="tdColInput">
                                        <input id="txtSpecification" class="tdinput" type="text" runat="server" style="width: 90%" />
                                    </td>
                                     
                                </tr>
                                 <tr class="table-item">
                                    <td class="td_list_fields" >
                                        确认时间
                                    </td>
                                    <td class="tdColInput">
                                        <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" style="width: 45%">
                                                    <input type="text" name="time1" id="txtBeginT" runat="server" style="width: 95%;" class="tdinput Wdate"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" />
                                                </td>
                                                <td style="width: 10%">
                                                    <input id="Text8" runat="server" class="tdinput" value="至" type="text" style="width: 78%;" />
                                                </td>
                                                <td style="width: 45%">
                                                    <input type="text" id="txtEndT" runat="server" style="width: 95%;" class="tdinput Wdate"
                                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="td_list_fields" >
                                        合同状态
                                    </td>
                                    <td class="tdColInput">
                                          <select name="BillStatus" style="width: 60%; margin-top: 2px; margin-left: 2px;"
                                            id="BillStatus" runat="server">
                                            <option value="0" selected="selected">全部</option>
                                            <option value="1">制单</option>
                                            <option value="2">确认</option>
                                            <%--<option value="3">审核通过</option>
                                            <option value="4">作废</option>--%>
                                            <option value="9">关闭</option>
                                            
                                           
                                        </select>
                                    </td>
                                    
                                     <td class="td_list_fields" >
                                         客户名称
                                    </td>
                                    <td class="td_list_edit" valign="middle" >
	                                    <input id="opr_addcontract" type="hidden" runat="server" value="1" />  
	                                    <input id="txtCustomerID" type="hidden" runat="server" />  
	                                    <asp:TextBox ID="txtCustomerName" runat="server"  style="width: 80%; border:0px; background-color:#FFFFFF;"  onclick="SearchCustData();"></asp:TextBox>
	                                    <img alt="搜索" src="../../../Images/default/Search1.gif"  id="search"     style=" cursor:pointer;" onclick="SearchCustData();" />
                                    </td>
                                    <%--<td class="tdColInput">
                                        <input id="txtCustName" class="tdinput" type="text" runat="server" style="width: 90%" />
                                    </td>--%>
                                </tr>                                                             
                                <tr >
                                    <td colspan="6" align="center"   bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <img runat="server" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            id="btn_Search" onclick='SearchSellContract(1)' />
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
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
        
       <tr>
            <td height="30" colspan="2"   valign="top" class="Title">
            <table width="99%" border="0" align="center" border="0" cellpadding="0" cellspacing="0" >
        <tr class="menutitle1" >
            <td align="left" valign="middle">  
             &nbsp;&nbsp;销售合同列表            
            </td>
            <td align="right"  >   
                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                AlternateText="导出Excel" runat="server" Visible="false" />&nbsp;
                            <img id="btn_create" src="../../../images/Button/Bottom_btn_new.png" 
                                runat="server" alt="新建" style='cursor: hand;' onclick="CreateSaleContract();" />&nbsp;
                            <img alt="删除合同" src="../../../Images/Button/Main_btn_delete.png" id="btn_delete"
                                runat="server" onclick="DelSelContractInfo();"   />&nbsp;
                          <%--  <asp:ImageButton ID="ImageButton1" runat="server"  
                                ImageUrl="../../../images/Button/cw_sc1.jpg" AlternateText="删除订单" 
                                onclick="ImageButton1_Click"/>--%>
                               <%-- <img alt="删除订单，仅针对作废和制单状态的订单" src="../../../Images/Button/cw_sc1.jpg" id="Img1"
                                runat="server" onclick="showBillStatus();" />&nbsp;--%>
                            <img alt="修改订单" src="../../../Images/Button/Bottom_btn_edit.jpg" id="btn_edit" runat="server"
                                onclick="EditOrder();" style="display: none;" />&nbsp;
                            <img alt="处理订单" src="../../../Images/Button/btn_dealorder.jpg" id="btn_deal" runat="server"
                                onclick="DealOrder();" style="display: none;" />&nbsp;
                              </td>                       
        </tr>
	     </table></td>                 
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center"   cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr class="table-item">
                            <th    class="td_main_detail" width="3%">
                                选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div class="orderClick" onclick="OrderBy('OrderNo','oOrderNo');return false;">
                                    合同号<span id="oOrderNo" class="orderTip"></span></div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div id="oCustName_dsp" class="orderClick" onclick="OrderBy('CustName','oCustName');return false;">
                                    客户名称<span id="oCustName" class="orderTip"></span></div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div id = "oproductname_dsp" class="orderClick" onclick="OrderBy('productname','oproductname');return false;">
                                    签订日期</div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div id = "ospecification_dsp" class="orderClick" onclick="OrderBy('specification','ospecification');return false;">
                                    生效日期</div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                终止日期</th>
                            <th   class="td_main_detail" width="5%">
                                <div id = "oproductcount_dsp" class="orderClick" onclick="OrderBy('productcount','oproductcount');return false;">
                                    结算方式</div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div id="ounitprice_dsp" class="orderClick" onclick="OrderBy('unitprice','ounitprice');return false;">
                                    运输类型</div>
                            </th>
                            
                            <th   class="td_main_detail" width="5%">
                                <div class="orderClick" onclick="OrderBy('OrderDate','oOrderDate');return false;">
                                    合同状态</div>
                            </th>
                            <th class="td_main_detail" width="5%">
                                    数量
                            </th>
                            <th class="td_main_detail" width="5%">                                
                                    金额
                            </th>                          
                            <th  class="td_main_detail" width="5%">
                                <div class="orderClick" onclick="OrderBy('CreateName','oCreateName');return false;">
                                    创建人<span id="oCreateName" class="orderTip"></span></div>
                            </th>
                            <th   class="td_main_detail" width="5%">
                                <div class="orderClick" onclick="OrderBy('CreateDate','oCreateDate');return false;">
                                    创建日期<span id="oCreateDate" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
         <table width="99%" border="0" align="center"   cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0"   cellpadding="0" cellspacing="0" class="PageList">
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

