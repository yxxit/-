<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expenses_ADD.aspx.cs" Inherits="Pages_JTHY_Expenses_Expenses_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%--<%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel"
    TagPrefix="uc11" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/jthy/Expenses/Expenses_add.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server" >
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="getOrderNO" />
    <input type="hidden" id="hiddenId" />
    <input type="hidden" id="hidBillStatus" />
    
    
    <uc1:Message ID="Message1" runat="server" /> 
    <%--<uc11:CustNameSel ID="CustNameSel" runat="server" />--%>
    <div >                                            
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">            
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="left" class="Title">&nbsp;&nbsp;
                                <asp:Label ID="labTitle_Write1" runat="server" Text="">费用申请单</asp:Label>                            
                            </td>
                            <td align="right">  
                                <img id="btn_save" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                    runat="server" onclick="SaveSellOrder();" />
                                <img id="imgUnSave" runat="server" alt="无法保存" src="../../../Images/Button/UnClick_bc.jpg"
                                    style="display: none;" /> &nbsp;
                                <img alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg" id="exp_sure"
                                onclick="ChangeStatus();" style="display: none; " runat="server"  />
                                <img alt="无法确认" src="../../../Images/Button/UnClick_qr.jpg" id="exp_unsure"
                                 runat="server"  />
                                <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" style="display:none;"
                                id="product_btnback" onclick="DoBack();" runat="server" />
                                    
                                     <input type="hidden" id="hidUpDateTime" runat="server" />
                                     <input id="headid" type="hidden" runat="server" />
                                <span runat="server" id="GlbFlowButtonSpan"></span>                                
                               <input id="txtOprtID" type="text" style="display: none;" />
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
                        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01">              
                        
                        <tr class="table-item">
                            <td class="td_list_fields" >单据编号<span class="redbold">*</span></td>
                            <td class="tdColInput" >
                                <div id="divCodeRule" runat="server">
                                    <uc3:CodingRuleControl ID="ddlExpCode" runat="server" />
                                </div>
                                <div id="divExpCode"  style=" display:none;" runat="server">
                                </div>
                            </td>
                            <td   class="td_list_fields"  >申请人<span class="redbold">*</span></td>
                            <td  class="tdColInput">                            
                                <input id="txtPPersonID" type="hidden" runat="server" /> 
                                <asp:TextBox ID="txtPPerson" runat="server" ReadOnly  class="tdinput" style="width: 95%;" onclick="alertdiv('txtPPerson')">
                                </asp:TextBox>
                            </td>
                            <td   class="td_list_fields" >申请部门</td>
                            <td  class="tdColInput" >
                                <input id="DeptName" runat="server" readonly  type="text" class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" />
                                <input id="hdDeptID" type="hidden" runat="server" />                         
                            </td>                         
                        
                        </tr>
                         <tr class="table-item">
                            <td  class="td_list_fields">申请金额<span class="redbold">*</span></td>
                            <td  class="tdColInput"   >
                                <input type="text" id="txtTotalAmount" class="tdinput" style="width: 95%;" />
                            </td>
                            <td  class="td_list_fields"  >申请日期<span class="redbold">*</span></td>
                            <td  class="tdColInput"   >
                             <asp:TextBox ID="txtEffectiveDate" runat="server" class="tdinput" style="width: 95%;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEffectiveDate')})" ReadOnly="true"></asp:TextBox>
                            </td>
                             <td  class="td_list_fields"  >费用类别</td>
                            <td    class="tdColInput" >
                                <asp:DropDownList ID="ddlExpType" runat="server" class="tddropdlist"></asp:DropDownList>
                             </td>
                                            
                        </tr>
                        <tr class="table-item">
                            <td  class="td_list_fields"  >往来单位</td>
                            <td class="tdColInput">
                                <input id="opr_addcontract" type="hidden" runat="server" value="1" />  
                                <input id="txtCustomerID" type="hidden" runat="server" />   
                                    <asp:TextBox ID="txtCustomerName" runat="server" class="tdinput" ReadOnly="true" style="width: 95%; " onclick="SearchCustData();">
                                </asp:TextBox>
                            
                            </td>
                            <td   class="td_list_fields"  >支付方式</td>
                            <td    class="tdColInput" >
                                <asp:DropDownList ID="ddlPayType" runat="server" class="tddropdlist"></asp:DropDownList>
                            </td>
                             <td  class="td_list_fields"  >经办人</td>
                            <td class="tdColInput"   >
                                <input name="UserLinker" id="UserLinker" type="text" runat="server" readonly class="tdinput"
                                style="width: 95%" maxlength="50" onclick="alertdiv('UserLinker,txtJoinUser');" /><input
                                    type="hidden" runat="server" id="txtJoinUser" />
                            </td>
                        </tr> 
                         <tr class="table-item">
                            <td  class="td_list_fields" >备注</td>
                            <td colspan="5" class="tdColInput" align="left">
                                <textarea name="txtReason" id="txtReason"   rows="3" cols="70"
                                style="width: 99%; height:70px;"></textarea>                        
                            </td>           
                        </tr>                         
                    </table> 
                 <br />                                           
                 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr  class="menutitle1">
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
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04">                   
                    <tr  class="table-item">
                        <td class="td_list_fields" >
                            建档日期<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtCreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="td_list_fields" >
                            建档人<span class="redbold">*</span> 
                        </td>
                        <td class="tdColInput" >
                           <asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="90%" Enabled="False"></asp:TextBox>
                           <input type="hidden" id="txtCreator" runat="server" />

                        </td>
                        <td class="td_list_fields" >
                            确认人
                        </td>
                        <td class="tdColInput" > 
                            <input name="txtConfirmor" id="txtConfirmor" type="text" runat="server" readonly class="tdinput"
                                style="width: 95%;display:none;" maxlength="50" />                        
                        </td>
                    </tr>                   
                    <tr  class="table-item">
                       <td class="td_list_fields" >
                            确认日期
                        </td>
                        <td  class="tdColInput">  
                            <input name="txtConfirmDate" id="txtConfirmDate" type="text" runat="server" readonly class="tdinput"
                                style="width: 95%; display:none;" maxlength="50"  />                        
                        </td>
                        <td   class="td_list_fields">
                            最后更新日期<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            最后更新用户
                        </td>
                        <td  class="tdColInput">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
                    <!-- End 默认信息 -->
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
