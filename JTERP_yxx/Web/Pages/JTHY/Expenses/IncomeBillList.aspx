<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IncomeBillList.aspx.cs" Inherits="Pages_JTHY_Expenses_IncomeBillList" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc13" %>
<%@ Register Src="~/UserControl/CustOrProvider.ascx" TagName="CustOrProvider" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收款单列表</title>
     
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
      
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    

    <script src="../../../js/jthy/Expenses/IncomeBillList.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
  
function temp() 
{    
  if (window.event.keyCode == 13) 
  {
    event.returnValue=false; 
    event.cancel = true;
  }
} 


  </script>
    
</head>
<body onkeydown="temp()">
<input type="hidden" runat="server" id="point" />
<uc2:CustOrProvider ID="CustOrProvider1" runat="server" />
   <form id="frmMain" runat="server">
<a name="DetailListMark"></a>

    <table  width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
        <%--<tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>--%>
        <tr class="menutitle1" >
                        <td align="left" valign="middle">  
                &nbsp;&nbsp;&nbsp;<img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>
            <td align="right" valign="top">
                <div id='divSearch'>
                    <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                </div>&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2"  >
                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                <tr class="table-item">
                                    <td height="20"  class="td_main_detail" width="10%" align="right">单据编号</td>
                                    <td class="tdColInput" width="23%">
                                        <asp:TextBox ID="txtIncomeNo" runat="server" class="tdinput" onchange="if(!CheckSpecialWord(this.value)){popMsgObj.ShowMsg('收款单号不能包含特殊字符，请重新输入');this.value='';}" ></asp:TextBox>
                                        &nbsp;</td>
                                        
                                        <td class="td_main_detail" align="right">
                                        往来客户
                                    </td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="tdinput" Width="85%"></asp:TextBox>
                                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="popSellCustObj.ShowList('protion','CustID','txtCustName','FromTBName','FileName','','0');" />
                                        <input runat="server" id="CustID" type="hidden" />
                                        <input runat="server" id="FromTBName" type="hidden" />
                                        <input runat="server" id="FileName" type="hidden" />
                                    </td>
                                    <td class="td_main_detail" align="right">
                                        执行人
                                    </td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtExecutor" runat="server" class="tdinput"></asp:TextBox>
                                        <!--<asp:TextBox ID="txtProject" runat="server" onclick="ShowProjectInfo('txtProject','hidProjectID');"
                                            ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                                        <input type="hidden" id="hidProjectID" runat="server" />
                                        <uc13:ProjectSelectControl ID="ProjectSelectControl2" runat="server" />-->
                                    </td>
                                    <!--<td  class="td_main_detail" width="10%" align="right">收款方式</td>
                                    <td class="tdColInput" width="23%">
                                        <asp:DropDownList ID="IncomeBillType" runat="server" Height="22px" Width="105px">
                                        <asp:ListItem Value="" Text="--请选择--"></asp:ListItem>
                                         <asp:ListItem Value="0" Text="现金"></asp:ListItem>
                                          <asp:ListItem Value="1" Text="银行转账"></asp:ListItem>
                                        </asp:DropDownList>
                                    &nbsp;</td>
                                    <td height="20"  class="td_main_detail" width="10%" align="right">收款金额</td>
                                    <td class="tdColInput" width="24%">
                                        <asp:TextBox ID="txtTotalPrice" runat="server" class="tdinput"></asp:TextBox>
                                    </td>-->
                                </tr>
                                <tr class="table-item">
                                    <td  class="td_main_detail" align="right">单据状态</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="DrpConfirmStatus" runat="server" Height="22px" Width="112px">
                                            <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                               <asp:ListItem Value="0">未确认</asp:ListItem>
                                            <asp:ListItem Value="1">已确认</asp:ListItem>
                                         
                                        </asp:DropDownList>
                                    </td>
                                    <!--<td  class="td_main_detail" align="right">登记凭证</td>
                                    <td class="tdColInput">
                                        <asp:DropDownList ID="DrpIsAccount" runat="server" Height="21px" 
                                            Width="104px">
                                            <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">已登记</asp:ListItem>
                                            <asp:ListItem Value="0">未登记</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>-->
                                    <td  class="td_main_detail" align="right">开始时间</td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtStartDate" runat="server" class="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" ReadOnly="true" ></asp:TextBox>
                                    </td>
                                    <td  class="td_main_detail" align="right">结束时间</td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txtEndDate" runat="server" class="tdinput" 
                                            onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" ReadOnly="true"  ></asp:TextBox>
                                    </td>                                        
                               
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch"   runat="server" visible="true"  style='cursor:pointer;' onclick='SearchIncomeBillInfo()' />

                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="5">
                <input type="hidden" id="hidModuleID" runat="server" />
                <input type="hidden" id="hidModuleIDAsse" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
                <%--<table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblDetailList" >--%>
                    <%--<tr>
                        <td valign="top">
                            <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                        </td>
                        <td align="center" valign="top"></td>
                    </tr>
                    <tr><td colspan="2" height="2"></td></tr>
                    <tr>
                        <td height="30" colspan="2" align="center" valign="top" class="Title">收款单列表</td>
                    </tr>--%>
                    <tr>
                        <td height="35" colspan="2" valign="top">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                
                                <tr class="menutitle1" >
                                    <td  align="left" valign="middle">&nbsp;&nbsp;收款单列表</td>
                                    <td align="right" >  

                                         <img src="../../../images/Button/Bottom_btn_new.jpg" alt="新建"  id="benNew"    runat="server"  visible=true  style='cursor:pointer;'  onclick="NewBilling();" />
                                         
                                        &nbsp;
                                         <img src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认"  id="btnConfirm"    runat="server"  visible=false  style='cursor:pointer;'   onclick="ConfirmIncomeBill();"/>
                                         
                                         &nbsp;
                      
                                          <img src="../../../images/Button/Main_btn_fqr.jpg" alt="反确认"  id="btnReConfirm"    runat="server"  visible=false style='cursor:pointer;'   onclick=" ReconfirmIncomeBill();"/>
                                        &nbsp;
                                        <!--<img src="../../../images/Button/Button_Djpz.jpg" alt="登帐"  id="btnRegi"    runat="server"  visible=false  style='cursor:pointer;'   onclick="AddVoucher();" />
                                        
                                            &nbsp;-->
                                        <img src="../../../images/Button/Main_btn_delete.jpg" alt="删除"  id="btndel"    runat="server" visible=true  style='cursor:pointer;'   onclick="DeleteIncomeBill();"/>
                                        
                                         <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg" AlternateText="导出Excel" runat="server" onclick="btnImport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <!-- <div style="height:252px;overflow-y:scroll;"> -->
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                                <tbody>
                                    <tr class="table-item">
                                      
                                        
                                        <th height="20" align="center" class="td_main_detail" >选择
                                        <input type="checkbox" id="checkall" name="checkall" onclick="AllSelect('checkall','Checkbox1') ;" />
                                        </th>
                                           <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('InComeNo','oC011');return false;">
                                                单据编号<span id="oC011" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('CustName','oC1');return false;">
                                                往来客户<span id="oC1" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('BillingNum','oC2');return false;">
                                                业务单号<span id="oC2" class="orderTip"></span>
                                            </div>
                                        </th>
                                          <!--<th align="center" class="td_main_detail" >
                                            <div class="orderClick" >
                                                所属项目<span id="Span2" class="orderTip"></span>
                                            </div>
                                        </th>-->
                                        <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('AcceDate','oC0');return false;">
                                                收款日期<span id="oC0" class="orderTip"></span>
                                            </div>
                                        </th>
                                        
                                        
                                        <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('TotalPrice','oC3');return false;">
                                                收款金额<span id="oC3" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('AcceWay','oC4');return false;">
                                                支付方式<span id="oC4" class="orderTip"></span>
                                            </div>
                                        </th>
                                        <!--<th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('ConfirmStatus','oC5');return false;">
                                                确认状态<span id="oC5" class="orderTip"></span>
                                            </div>
                                        </th>-->
                                         <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('Accountor','oC14');return false;">
                                               经办人<span id="oC14" class="orderTip"></span>
                                            </div>
                                        </th>
                                         <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('Confirmor','oC6');return false;">
                                                确认人<span id="oC6" class="orderTip"></span>
                                            </div>
                                        </th>
                                           <th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('ConfirmDate','oC12');return false;">
                                                确认日期<span id="oC12" class="orderTip"></span>
                                            </div>
                                        </th>
                                        
                                          <!--<th align="center" class="td_main_detail" >
                                            <div class="orderClick" onclick="OrderBy('IsAccount','oC7');return false;">
                                                登记凭证<span id="oC7" class="orderTip"></span>
                                            </div>
                                        </th>-->
                                        
                                           <!--<th align="center" class="td_main_detail" >
                                            <div class="orderClick">
                                                凭证号<span id="Span1" class="orderTip"></span>
                                            </div>
                                        </th>-->
                                        
                                    </tr>
                                </tbody>
                                
                            </table>
                            <!-- </div> -->
                            <br/>
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
                                        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                            <tr>
                                                <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                                    <div id="pagecount"></div>
                                                </td>
                                                <td height="28"  align="right">
                                                    <div id="divPageClickInfo" class="jPagerBar"></div>
                                                </td>
                                                <td height="28" align="right">
                                                    <div id="divPage">
                                                        每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount" size="3" />条&nbsp;&nbsp;
                                                        转到第<input name="txtToPage" type="text" id="txtToPage" size="3"/>页&nbsp;&nbsp;
                                                        <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                                    </div>
                                                 </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br/>
                        </td>
                    </tr>
                </table>            
            </td>
        </tr>
    </table>
<p>
<span id="Forms" class="Spantype" name="Forms"></span>
   <input type="hidden" id="VoucherModuleID" runat="server" />
    <input id="txtCustCode"  type="hidden"/></p>
 <uc1:Message ID="Message1" runat="server" />
</form>
</body>
</html>
