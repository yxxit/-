<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QTest_ADD.aspx.cs" Inherits="Pages_JTHY_StockManage_QTest_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/jthy/BusInfo.ascx" TagName="BusInfo"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质检单</title>
     <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/jthy/stockmanage/QTest_ADD.js" type="text/javascript"></script>
     <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="getOrderNO" /><div id="orderNo1"></div>
   <uc1:Message ID="Message1" runat="server" />
   <uc2:ProviderInfo ID="ProviderInfo" runat="server" />
   <uc3:BusInfo ID="BusInfo" runat="server" /> 
     <input id="headid" type="hidden" runat="server" />
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="hidSendStatus" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />
    <input type="hidden" id="txtCreatorID" name="txtCreatorID" runat="server" />
    <input type="hidden" id="txtCreateDate" name="txtCreateDate" runat="server" />
    <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
    <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单"  runat="server" />
    <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" runat="server" />
    <input type="hidden" id="txtConfirmorDate" name="txtConfirmorDate" runat="server" />
    <input type="hidden" id="Hidden1" name="txtModifiedUserID" runat="server" />
    <input type="hidden" id="Hidden2" name="txtModifiedDate" runat="server" /> 

    <div>                                            
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">            
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="left" class="Title">&nbsp;&nbsp;
                                <asp:Label ID="labTitle_Write1" runat="server" Text="">质检单</asp:Label>                            
                            </td>
                            <td align="right">  
                            
                                <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                runat="server" onclick="SaveSellOrder();" />
                                <img id="imgUnSave" runat="server" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                style="display: none;" />
                                
                                <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="审核生效" style="cursor: pointer; display:none;"
                                    runat="server" onclick="Fun_ConfirmOperate();" />
                                <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法生效" 
                                    runat="server" />
                                   
                                <img id="UnConfirm" alt="取消生效" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer; display: none;" 
                                        onclick="cancelConfirm();"  />
                                <img id="ImgUnConfirm" alt="无法取消生效" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;" />
                                
                                
                                <input type="hidden" id="hidUpDateTime" runat="server" />
                              
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
                            <td   class="td_list_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;基本信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick'>
                                                <img alt="" src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01">              
                        
                        <tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">质检单编号</td>
                        <td style="width:23%" >

                            <div id="divCodeRule" runat="server">
                                <uc4:CodingRuleControl ID="ddlQTestBillNo" runat="server" />
                            </div>
                            <div id="divQTestBillNo"  style=" display:none;" runat="server">
                            </div>
                        </td>
                        <td   align="right" class="td_list_fields"  style="width:10%">检验方式</td>
                        <td style="width:23%">
                                <asp:DropDownList ID="drpCheckType" runat="server" style="width:80%">
                                   <asp:ListItem Value="1" Text="全检"></asp:ListItem>
                                  <asp:ListItem Value="2" Text="抽检" Selected="True"></asp:ListItem>
                               </asp:DropDownList>
                          </td>
                          <td  align="right" class="td_list_fields"  style="width:10%">检验日期</td>
                        <td    style="width:23%">
                              <asp:TextBox ID="txtCheckDate" runat="server" class="tdinput" style="width: 80%;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCheckDate')})" ReadOnly="true"></asp:TextBox>
                              <img alt="搜索" src="../../../Images/datePicker.gif"   />
                        </td>
                        
                        
                        
                        
                         <%--<td  align="right" class="td_list_fields"  style="width:10%">供应商</td>
                        <td   style="width:23%" >                        
                            <input id="txtProviderID" type="hidden" runat="server" />   
                            <asp:TextBox ID="txtProviderName" runat="server" Enabled="false"  class="tdinput" style="width: 95%;"  ></asp:TextBox>                            
                            </td>   --%>                      
                        
                        </tr>
                        <tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">检验人员<span class="redbold">*</span></td>
                        <td>
                            <input id="hdDeptID" type="hidden" runat="server" /> 
                            <input id="DeptName" type="hidden" runat="server" /> 
                            <input id="txtPPersonID" type="hidden" runat="server" /> 
                            <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="true"  class="tdinput" style="width:80%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                         </td> 
                         
                         <td   align="right" class="td_list_fields"  style="width:10%">来源到货号<span class="redbold">*</span></td>
                        <td    colspan="3"><input type="hidden" id="txtSourceBillID" value="" runat="server"  />                       
                            <asp:TextBox ID="txtSourceBillNo" runat="server" class="tdinput"  style="width: 80%;" onclick="fnSelect()"  ></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelect()" />
                        </td>
                        <%-- <td  align="right" class="td_list_fields"  style="width:10%">调运单号</td>
                        <td    >
                            <input type="hidden" id="txtTranSportID" value="" runat="server"  />
                            <asp:TextBox ID="txtTranSportNo" Enabled="false"   runat="server" class="tdinput" style="width: 85%;"></asp:TextBox>   
                              
                        </td>
                                 --%>           
                        </tr>
                        <%--<tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">煤种</td>
                        <td>
                             <input type="hidden" id="txtCoalID" value="" runat="server"  />
                             <asp:TextBox ID="txtCoalName" ReadOnly runat="server" class="tdinput" style="width: 85%;"></asp:TextBox>   
                        </td>
                        <td   align="right" class="td_list_fields"  style="width:10%">数量</td>
                        <td>
                            <asp:TextBox ID="txtQuantity"  runat="server" class="tdinput" style="width: 85%;" onkeyup='return ValidateNumber(this,value)'></asp:TextBox> 
                       </td>
                         
                        </tr>--%>
                        <tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">结果描述<span class="redbold">*</span></td>
                        <td>
                             <asp:TextBox ID="txtDescription" runat="server"    class="tdinput" style="width: 95%;"  ></asp:TextBox>
                          </td>
                        <td  align="right" class="td_list_fields"  style="width:10%">备注</td>
                        <td colspan="3">        
                        <asp:TextBox ID="txtRemark" runat="server"    class="tdinput" style="width: 95%;"  ></asp:TextBox>                
                        </td>           
                        </tr>                         
                    </table> 
                     <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">                        
                        <tr>
                            <td height="20px" class="td_list_title" colspan="2">
                           
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr  class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;质检报告
                                        </td>
                                        <td align="right">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>                                 
                            </td>
                        </tr>
                    </table>
                     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="TableBJ">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr  class="table-item">
                                        <td    style="padding-top: 5px; padding-left: 5px;">
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="img2"
                                                style="cursor:hand;" onclick="fnDelOneRow();" />
                                            <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="img1"
                                                style="cursor:hand;" onclick="AddSignRow();" />
                                            
                                        </td>
                                    </tr>
                                </table>
                                <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0"
                                        cellspacing="1" bgcolor="#999999">
                                        <tr  class="table-item">
                                            <td  class="td_main_detail" style="width: 10%;">
                                                选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                            </td>
                                            <td class="td_main_detail" style="width: 20%;">
                                                检验项目<span class="redbold">*</span>
                                            </td>
                                            <td class="td_main_detail" style="width: 20%;">
                                                检测数量
                                            </td>
                                            
                                            <td class="td_main_detail" style="width: 30%;">
                                                描述信息
                                            </td>                                          
                                            <td class="td_main_detail" style="width: 20%;">
                                                检验值 
                                            </td>                                           
                                                                                       
                                        </tr>                                        
                                    </table>                          
                                </div>
                            </td>
                        </tr>
                    </table> 
                     <br />                                           
                 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td   class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="1">
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
                            建档日期
                        </td>
                        <td  class="tdColInput" >
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td  class="td_list_fields" >
                            建档人
                        </td>
                        <td   class="tdColInput">
                            &nbsp;<asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="90%" Enabled="False"></asp:TextBox>
                        </td>
                        <td class="td_list_fields" >
                            确认人
                        </td>
                        <td   class="tdColInput" width="23%">  
                            <input id="Text1" name="txtConfirmorId" style="widows:95%; border:0px; display:none;" />
                            <input id="txtConfirmor" name="txtConfirmor" disabled="disabled" style="widows:95%; border:0px;" />                          
                        </td>
                    </tr>                   
                     <tr class="table-item">
                       <td class="td_list_fields" >
                            确认日期
                        </td>
                        <td   class="tdColInput" width="23%"> 
                            <input id="txtConfirmDate" name="txtConfirmDate" disabled="disabled" style="widows:95%; border:0px;" />                           
                        </td>
                        <td class="td_list_fields">
                            最后更新日期
                        </td>
                        <td   class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            最后更新用户ID
                        </td>
                        <td   class="tdColInput" >
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="93%" disabled Text=""></asp:TextBox>
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
