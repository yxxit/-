<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskAlertSetting.aspx.cs" Inherits="Pages_Office_SupplyChain_TaskAlertSetting" %>


<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>待办任务提醒设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>
   <script src="../../../js/office/SupplyChain/AlertSetting.js" type="text/javascript"></script>
     <style type="text/css">
         #Text1
         {
             width: 55px;
         }
         #Text2
         {
             width: 55px;
         }
         #Text3
         {
             width: 55px;
         }
         #Text4
         {
             width: 55px;
         }
         .style2
         {
             width: 201px;
         }
         .style3
         {
             width: 38%;
         }
         .style4
         {
             width: 86px;
         }
         </style>
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            待办任务提醒设置
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
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        待办任务提醒设置
                                    </td>
                                    <td align="right">
                                        <div id='searchClick1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start  -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_01">
   
                                <tr>
                                    <td align="right" style="width:20%">
                                        库存限量报警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" style="width:25%">
                                        <input type="radio" id="dioStorage1" name="dioStorage" runat="server" checked/>
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioStorage2" name="dioStorage"
                                            runat="server" />否
                                    </td>
                                    <td align="right" >
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgStorageContorl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(1,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示库存限量报警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        客户联络延期告警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioCC1" name="dioCustContact" runat="server" checked/>
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioCC2" name="dioCustContact"
                                            runat="server" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgCustContactControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(2,false);"
                                            runat="server"  />
                                    </td>
                                    <td >
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示客户联络延期告警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        供应商联络延期告警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="radPC1" name="dioProContact" runat="server" value="1" checked />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio"  id="radPC2" name="dioProContact" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgProviderControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(3,false);"
                                            runat="server"  />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示供应商联络延期告警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        待我审批的流程：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="radFlow1" name="dioFlow" runat="server" value="3" checked/>
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="radFlow2" name="dioFlow" runat="server"
                                            value="4" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgFlowControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(4,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示待我审批的流程。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        我的待办任务：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="radTask1" name="dioTask" runat="server" value="1" checked />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="radTask2" name="dioTask" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgTaskControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(5,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：选择“是”，则提示我的待办任务。
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                             <tr>
                                    <td align="right">
                                        即将到期的劳动合同：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioCT1" name="dioCT" runat="server" checked/>
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioCT2" name="dioCT"
                                            runat="server" />否
                                    </td>
                                    <td align="right" >
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgContractControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(6,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF" >
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示即将到期的劳动合同。</div>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        我的备忘录：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioMemo1" name="dioMemo" runat="server" value="1" checked />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioMemo2" name="dioMemo" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMemoControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(7,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：选择“是”，则提示我的备忘录。
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                               <tr>
                                    <td align="right">
                                        我的未读短信：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioUnRM1" name="dioUnReadM" runat="server" value="1" checked/>
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioUnRM2" name="dioUnReadM" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgUnReadMessage"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(8,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示我的未读短信。</div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        我的参会通知：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" checked id="dioMN1" name="dioMNotice" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioMN2" name="dioMNotice" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMNotice"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(9,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示我的参会通知。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                
                                
                            </table>
                            <!-- End -->
                            <br />
                        </td>
                    </tr>
                </table>
                
               
               
               
               
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        医药行业专用待办任务提醒设置
                                    </td>
                                    <td align="right">
                                        <div id='Div1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick1')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start  -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_02">
   
                                
                               
                              <tr>
                                    <td align="right"  bgcolor="#FFFFFF" style="width:20%">
                                        质量保证书过期预警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" class="style3">
                                        <input type="radio" checked id="dioYYZZ1" name="dioYYZZ" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioYYZZ2" name="dioYYZZ" runat="server" value="2" />否
                                        &nbsp;&nbsp;提前天数：<input id="txtYYZZDay" type="text" style="width:56px" runat="server" value="0" 
                                            onKeyUp="this.value=this.value.replace(/[^\.\d]/g,'');if(this.value.split('.').length>2){this.value=this.value.split('.')[0]+'.'+this.value.split('.')[1]}" />
                                       &nbsp;&nbsp;       
                                            是否超期锁定：
                                        <asp:DropDownList ID="Lock1" runat="server">
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    
                                    <td align="right" class="style4"  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img1"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(10,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示质量保证书过期预警。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style3">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style2"  bgcolor="#FFFFFF">
                                       药品经营(生产)许可证过期预警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" class="style3">
                                        <input type="radio" checked id="dioXKZ1" name="dioXKZ" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioXKZ2" name="dioXKZ" runat="server" value="2" />否
                                         &nbsp;&nbsp;提前天数：<input id="txtXKZDay" type="text" runat="server" style="width:56px" value="0"  onKeyUp="this.value=this.value.replace(/[^\.\d]/g,'');if(this.value.split('.').length>2){this.value=this.value.split('.')[0]+'.'+this.value.split('.')[1]}" />
                                         
                                          &nbsp;&nbsp;       
                                            是否超期锁定：
                                        <asp:DropDownList ID="Lock2" runat="server">
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style4"  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img2"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(11,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示药品经营(生产)许可证过期预警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style3">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" class="style2"  bgcolor="#FFFFFF">
                                                                                GMP(GSP)证书过期预警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" class="style3">
                                        <input type="radio" checked id="dioGMP1" name="dioGMP" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioGMP2" name="dioGMP" runat="server" value="2" />否
                                         &nbsp;&nbsp;提前天数：<input id="txtGMPDay" type="text" runat="server" style="width:56px"  value="0" onKeyUp="this.value=this.value.replace(/[^\.\d]/g,'');if(this.value.split('.').length>2){this.value=this.value.split('.')[0]+'.'+this.value.split('.')[1]}" />
                                         
                                          &nbsp;&nbsp;       
                                            是否超期锁定：
                                        <asp:DropDownList ID="Lock3" runat="server">
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style4"  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img3"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(12,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示GMP（GSP）证书过期预警。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style3">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="style2"  bgcolor="#FFFFFF">
                                     药品保质期预警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" class="style3">
                                        <input type="radio" checked id="dioYPBZQ1" name="dioYPBZQ" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioYPBZQ2" name="dioYPBZQ" runat="server" value="2" />否
                                         &nbsp;&nbsp;提前天数：<input id="txtYPBZQDay" runat="server" type="text" style="width:56px" value="0"  onKeyUp="this.value=this.value.replace(/[^\.\d]/g,'');if(this.value.split('.').length>2){this.value=this.value.split('.')[0]+'.'+this.value.split('.')[1]}" />
                                         
                                          &nbsp;&nbsp;       
                                            是否超期锁定：
                                        <asp:DropDownList ID="Lock4" runat="server">
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style4"  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img4"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(13,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示药品保质期预警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style3">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="right" class="style2"  bgcolor="#FFFFFF">
                                        法人委托书过期预警：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" class="style3">
                                        <input type="radio" checked id="dioFRWTS1" name="dioFRWTS" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioFRWTS2" name="dioFRWTS" runat="server" value="2" />否
                                         &nbsp;&nbsp;提前天数：<input id="txtFRWTSDay" runat="server" type="text" style="width:56px" value="0"  onKeyUp="this.value=this.value.replace(/[^\.\d]/g,'');if(this.value.split('.').length>2){this.value=this.value.split('.')[0]+'.'+this.value.split('.')[1]}" />
                                         
                                          &nbsp;&nbsp;       
                                            是否超期锁定：
                                        <asp:DropDownList ID="Lock5" runat="server">
                                            <asp:ListItem>是</asp:ListItem>
                                            <asp:ListItem>否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" class="style4"  bgcolor="#FFFFFF">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img5"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(14,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：选择“是”，则提示法人委托书预警。</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF" class="style3">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                
                            </table>
                            <!-- End -->
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
