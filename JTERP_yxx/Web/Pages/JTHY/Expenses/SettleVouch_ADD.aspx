<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SettleVouch_ADD.aspx.cs" Inherits="Pages_JTHY_Expenses_SettleVouch_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/jthy/InBusInfo.ascx" TagName="InBusInfo"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/jthy/OutBusinessInfo.ascx" TagName="OutBusinessInfo"
    TagPrefix="uc4" %>
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
    
    <script src="../../../js/jthy/Expenses/SettleVouch_ADD.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server" >
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="getOrderNO" />
    <input type="hidden" id="hiddenId" />
    <input type="hidden" id="hidBillStatus" />
    
    
    <uc1:Message ID="Message1" runat="server" /> 
    <uc3:InBusInfo ID="InBusInfo" runat="server" />
    <uc4:OutBusinessInfo ID="OutBusinessInfo" runat="server" /> 
    <%--<uc11:CustNameSel ID="CustNameSel" runat="server" />--%>
    <div >                                            
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">            
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="left" class="Title">&nbsp;&nbsp;
                                <asp:Label ID="labTitle_Write1" runat="server" Text="">结算单</asp:Label>                            
                            </td>
                            <td align="right">  
                                <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                    runat="server" onclick="SaveSellOrder();" />
                                <img id="imgUnSave" runat="server" alt="无法保存" src="../../../Images/Button/UnClick_bc.jpg"
                                    style="display: none;" />
                                    
                                <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="确认" style="cursor: pointer; display:none;"
                                    runat="server" onclick="Fun_ConfirmOperate();" />
                                <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法确认" 
                                    runat="server" />
                                   
                                <img id="UnConfirm" alt="反确认" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer;
                                    display: none;" onclick="cancelConfirm();" visible="false" />
                                <img id="ImgUnConfirm" alt="无法反确认" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;" />
                                   
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
                                           &nbsp;&nbsp;业务信息
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
                            <td class="td_list_fields1" style=" width:8%;"  >单据编号<span class="redbold">*</span></td>
                            <td class="tdColInput1" style=" width:16%;"  >
                                <input type="text" id="txtSettleCode" class="tdinput" style="width: 95%;" />
                            </td>
                            <td   class="td_list_fields1"  >来源类型<span class="redbold">*</span></td>
                            <td  class="tdColInput1"> 
                                <select id="sel_cBusTtype"  name="sel_cBusTtype" onchange="ChangeBill()" style="width:80%">
                                <option value="0">---请选择---</option>
                                <option value="1">直销</option>
                                <option value="2">采购到货</option>
                                <option value="3">采购直销</option>
                                </select>                          
                            </td>
                            <td   class="td_list_fields1" >来源单号<span class="redbold">*</span></td>
                            <td  class="tdColInput1" >
                                <input type="hidden" id="txthidSourceID" />
                                <input type="hidden" id="txtSourceBillID" value="" runat="server"  />                       
                                <asp:TextBox ID="txtSourceBillNo" runat="server" class="tdinput"  ReadOnly="true" style="width: 70%;" onclick="fnFromBillNo()"  ></asp:TextBox>
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnFromBillNo()" />                      
                            </td>                         
                            <td  class="td_list_fields1">经办人<span class="redbold">*</span></td>
                            <td  class="tdColInput1"   >
                                <input id="txtPPersonID" type="hidden" runat="server" /> 
                             <asp:TextBox ID="txtPPerson" runat="server" ReadOnly  class="tdinput" style="width: 70%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
                             <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                            </td>
                        </tr>
                         <tr class="table-item" id="id_Customer">
                            
                            <td  class="td_list_fields1"  >客户名称</td>
                            <td  class="tdColInput1"   >
                                <input id="opr_addoutbus" type="hidden" runat="server" value="" />  
                                 <input id="txtCustomerID" type="hidden" runat="server" /> 
                                 <asp:TextBox ID="txtCustomerName" runat="server" Enabled="false"  class="tdinput" style="width: 95%;" ></asp:TextBox>
                                 
                            </td>
                             <td  class="td_list_fields1"  >本次结算<span class="redbold">*</span></td>
                            <td    class="tdColInput1" >
                                <input type="hidden" id="txtS_SPrice" />
                                <asp:TextBox ID="txtS_SettelTotalPrice" runat="server" onkeyup='return ValidateNumber(this,value)' class="tdinput" style="width: 95%;" ></asp:TextBox>
                             </td>
                            <td  class="td_list_fields1"  >已结算金额</td>
                            <td class="tdColInput1">
                                <asp:TextBox ID="txtS_SettleMoney" runat="server" Enabled="false"  class="tdinput" style="width: 95%;" ></asp:TextBox>
                            </td>
                            <td   class="td_list_fields1"  >总金额</td>
                            <td    class="tdColInput1" >
                                <asp:TextBox ID="txtS_TotalMoney" runat="server" Enabled="false"  class="tdinput" style="width: 95%;" ></asp:TextBox>
                            </td>            
                        </tr>
                        <tr class="table-item" id="id_Provider">
                            
                            <td  class="td_list_fields1"  >供应商名称</td>
                            <td  class="tdColInput1"   >
                                <input id="txtProviderID" type="hidden" runat="server" />   
                                <asp:TextBox ID="txtProviderName" runat="server" Enabled="false"  class="tdinput" style="width: 95%;"></asp:TextBox>
                            </td>
                             <td  class="td_list_fields1"  >本次结算<span class="redbold">*</span></td>
                            <td    class="tdColInput1" >
                                <input type="hidden" id="txtP_SPrice" />
                                <asp:TextBox ID="txtP_SettleTotalPrice" runat="server" onkeyup='return ValidateNumber(this,value)' class="tdinput" style="width: 95%;" ></asp:TextBox>
                             </td>
                            <td  class="td_list_fields1"  >已结算金额</td>
                            <td class="tdColInput1">
                                <asp:TextBox ID="txtP_SettleMoney" runat="server"  Enabled="false" class="tdinput" style="width: 95%;" ></asp:TextBox>
                            </td>
                            <td   class="td_list_fields1"  >总金额</td>
                            <td    class="tdColInput1" >
                                <asp:TextBox ID="txtP_TotalMoney" runat="server" Enabled="false"  class="tdinput" style="width: 95%;" ></asp:TextBox>
                            </td>            
                        </tr>

                         <tr class="table-item">
                            <td class="td_list_fields1" >备注</td>
                            <td colspan="7" class="tdColInput1" align="left">
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
                                           &nbsp;&nbsp;辅助信息
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
                            建档日期
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtCreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="td_list_fields" >
                            建档人 
                        </td>
                        <td class="tdColInput" >
                           <asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" 
                                Width="90%" Enabled="False"></asp:TextBox>
                           <input type="hidden" id="txtCreator" runat="server" />

                        </td>
                        <td class="td_list_fields" >
                            确认人
                        </td>
                        <td class="tdColInput" > 
                            <input name="txtConfirmor" id="txtConfirmor" type="text" runat="server"  disabled="disabled"  class="tdinput"
                                style="width: 95%;" maxlength="50" /> 
                            <input type="hidden" id="txtConfirmorId" />                       
                        </td>
                    </tr>                   
                    <tr  class="table-item">
                       <td class="td_list_fields" >
                            确认日期
                        </td>
                        <td  class="tdColInput">  
                            <input name="txtConfirmDate" id="txtConfirmDate" type="text" runat="server" disabled="disabled" class="tdinput"
                                style="width: 95%; " maxlength="50"  />                        
                        </td>
                        <td   class="td_list_fields">
                            最后更新日期
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
