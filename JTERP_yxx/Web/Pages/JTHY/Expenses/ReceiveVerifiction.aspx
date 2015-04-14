<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiveVerifiction.aspx.cs" Inherits="Pages_Office_FinanceManager_ReceiveVerifiction" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc13" %>
<%@ Register src="../../../UserControl/CustOrProvider.ascx" tagname="CustOrProvider" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收款核销单</title>
   <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
     <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
      <script src="../../../js/common/Common.js" type="text/javascript"></script>
      <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
      <script type="text/javascript" src="../../../js/common/Page.js"></script>
      <script type="text/javascript" src="../../../js/JTHY/Expenses/ReceiveVerifiction.js"></script>
</head>
<body>
<uc13:ProjectSelectControl ID="ProjectSelectControl" runat="server" />
 <uc3:CustOrProvider ID="CustOrProvider1" runat="server" />
<uc1:Message ID="messages" runat="server" />
    <form id="form1" runat="server">
    <div>
    <div <%--style="height: 500px; overflow: scroll;"--%>>
        <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="mainindex">
            <tr>
                <td valign="top">
                    <input type="hidden"  id="hiddenEquipCode" value="" />
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" alt=""/>
                </td>
                <td align="center" valign="top">
                </td>
            </tr>
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="center" class="Title">
                                收款核销单
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                            <table width="100%">
                                <tr>
                                    <td height="28"  bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                       
                                        &nbsp;
                                        
                                       <%-- <img src="../../../Images/Button/Bottom_btn_new1.jpg" alt="分摊" id="Btnfentan" style="cursor: hand;
                                    margin: 0px; "  />
&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                    
                                <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="BtnSave" style="cursor: hand;
                                    margin: 0px; " onclick="SaveBills();" /></td>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="客户名称">客户名称</asp:Label>
                                        <asp:TextBox ID="txtCustName" runat="server" onclick="popSellCustObj.ShowList('protion','CustID','txtCustName','','');" ReadOnly></asp:TextBox>
                                        <input id="CustID" type="hidden" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch"   runat="server" visible="false" onclick="Fun_IncomeBill();"  style='cursor:pointer;'  />
                                    </td>
                               </tr>     
                            </table>
                               <!-- 参数设置：是否启用条码 -->
                                <input type="hidden" id="hidBarCode" runat="server" value="" />
                             
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                   <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                     
                        <tr>
                            <td height="20"  class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td valign="top">
                                            <span class="Blue">收款单列表</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table2','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="Table2">
                        <tr>
                            <td>
                               
                              
                                    <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0" cellspacing="1" >
                                        <tbody>
                                        <tr>
                                            <td align="center"  class="detail_no" style="width: 4%">
                                                单据日期
                                            </td>
                                            <td align="center" class="detail_name" style="width: 4%">
                                                单据编号
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 5%">
                                                客户
                                            </td>
                                            <td align="center" id="BaseUnitD" runat="server" class="td_list_fields" style="width: 3%" >
                                                币种
                                            </td>
                                            <td align="center" id="BaseCountD" runat="server" class="td_list_fields" style="width: 3%" >
                                                款项类型
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 3%">
                                                收款方式
                                            </td>
                                            <td align="center"  style="width: 3%" class="detail_spec">
                                                收款金额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 3%">
                                                收款余额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 3%">
                                                结算金额<span style="color:Red">*</span>
                                             </td>
                                        </tr>
                                        </tbody>
                                        </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    
                       <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                     
                        <tr>
                            <td height="20"  class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td valign="top">
                                            <span class="Blue">待核销单列表</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Table2','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="table1">
                        <tr>
                            <td>
                               
                               
                                    <table width="100%" border="0" id="TB_Invoice" style="height: auto;" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                        <tbody>
                                        <tr>
                                            
                                            <td align="center"   class="detail_no" style="width: 4%">
                                                单据日期
                                            </td>
                                            <td align="center" class="detail_name" style="width: 4%">
                                                单据类型
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 3%">
                                                单据编号
                                            </td>
                                            <td align="center"  style="width: 4%" class="detail_spec">
                                                客户
                                            </td>
                                             <td align="center"  class="td_list_fields" style="width: 3%">
                                                订单号
                                             </td>
                                            <td align="center" id="Td1" runat="server" class="td_list_fields" style="width: 3%" >
                                                币种
                                            </td>
                                            <td align="center" id="Td2" runat="server" class="td_list_fields" style="width: 3%" >
                                                原单金额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 2%">
                                                原单余额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 3%">
                                                本次结算<span style="color:Red">*</span>
                                            </td>
                                        </tr>
                                        </tbody>
                                        </table>
                            </td>
                        </tr>
                    </table>              
                </td>
            </tr>
        </table>
    </div>
    </div>
    </form>
</body>
</html>