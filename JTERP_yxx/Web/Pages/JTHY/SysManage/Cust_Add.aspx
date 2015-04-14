<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cust_Add.aspx.cs" Inherits="Pages_Office_CustManager_Cust_Add" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeTypeDrpControl" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加客户信息</title>
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/jthy/CustManager/CustAdd.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/TreeView.js" language="javascript" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>  
</head>
<body>
    <form id="form1" runat="server"><!--AsyncPostBackTimeout="90"-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
　　Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); 
　　function EndRequestHandler(sender, args)
　　{
        
　　if (args.get_error() != undefined)
　　{
　　if ((args.get_response().get_statusCode() == '12007') || (args.get_response().get_statusCode() == '12029'))
　　{             
　　document.writeln("服务器连接失败。");
　　window.onerror = false; //JS中遇到脚本错误时不做任何操作
　　}
　　}
　　}
　　</script>   
     <input type="hidden" id="hiddenEquipCode" value="" />
     <input type="hidden" id="hidUpDateTime" runat="server" /><%--用于上传附件时间--%>
    <table width="98%"  border="0" cellpadding="0" cellspacing="0"  id="mainindex">        
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="left" class="Title">
                            &nbsp;&nbsp;新建客户档案
                        </td>
                        <td align="right">
                            &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" runat="server" visible="false"
                                id="btn_save" style="cursor: hand" onclick="SaveCustData();" />
                            <img src="../../../Images/Button/btn_save_add.jpg" style="cursor: hand" id="btn_save_add" onclick="SaveCustDataAndNewAdd();"
                                    runat="server" alt="保存并新增"/>
                             <img alt="客户联系人" src="../../../Images/Button/btn_custlinkman.jpg" style="cursor: hand;display:none;" id="btn_custlinkman1"
                                    runat="server" onclick="ViewCustLinkMan();"/>
                                    <input type="hidden" id="btn_custlinkman" />
                                <img alt="客户联系人" src="../../../Images/Button/btn_clmUnclick.jpg" style="display: none;" id="btn_clmUnclick1"
                                    runat="server"/>
                                    <input type="hidden" id="btn_clmUnclick" />
                                <img runat="server" alt="客户联络" src="../../../Images/Button/btn_custcontact.jpg"
                                    id="btn_custcontact1" style="cursor: hand;display:none;" onclick="ViewCustContact();" />
                                    <input type="hidden" id="btn_custcontact" />

                                 <img runat="server" alt="客户联络" src="../../../Images/Button/btn_ccUnclick.jpg"
                                    id="btn_ccUnclick1" style="display: none;" />   
                                    <input type="hidden" id="btn_ccUnclick" />
                                  <img runat="server" alt="客户服务" src="../../../Images/Button/btn_custservice.jpg"
                                    id="btn_custservice1" style="cursor: hand;display:none;" onclick="ViewCustService();" />
                                    <input type="hidden" id="btn_custservice" />
                                 <img runat="server" alt="客户服务" src="../../../Images/Button/btn_csUnclick.jpg"
                                    id="btn_csUnclick1"  style="display: none;" />  
                                    <input type="hidden" id="btn_csUnclick" />

                                   <img runat="server" alt="客户反馈" src="../../../Images/Button/btn_custcomplain.jpg"
                                    id="btn_custcomplain1" style="cursor: hand;display: none;" onclick="ViewCustComplain();" />
                                    <input type="hidden" id="btn_custcomplain" />
                                 <img runat="server" alt="客户反馈" src="../../../Images/Button/btn_custcpUnclick.jpg"
                                    id="btn_custcpUnclick1" style="display: none;"/>  
                                    <input type="hidden" id="btn_custcpUnclick" />
                                  <img runat="server" alt="综合查询" src="../../../Images/Button/btn_colligate.jpg"
                                    id="btn_colligate1" style="cursor: hand;display: none;" onclick="ViewCustColligate();" />
                                    <input type="hidden" id="btn_colligate" />
                                 <img runat="server"  alt="综合查询" src="../../../Images/Button/btn_collUnclick.jpg"
                                    id="btn_collUnclick1" style="display: none;" />  
                                    <input type="hidden" id="btn_collUnclick" />

                            
                                
                                <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                    display: none;" onclick="Back();" />
                           
                                <img id="btnPrint" visible="false" src="../../../images/Button/Main_btn_print.jpg"
                                    style="cursor: pointer; display:none;" onclick="PagePrint()" title="打印" alt="打印"/>
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
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr class="table-item">
                        <td  class="td_list_fields"   >
                            客户编号<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <div id="divCodeRule" runat="server">
                                <uc3:CodingRuleControl ID="ddlCustNo" runat="server" />
                            </div>
                            <div id="divCustNo" runat="server">
                            </div>
                        </td>
                        <td  class="td_list_fields"    >
                            客户名称<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                            <input name="txtCustName" class="tdinput" id="txtCustName" specialworkcheck="客户名称"
                                type="text" style="width: 95%" maxlength="50" onblur="LoadPYShort();" />
                        </td>                        
                        <td    class="td_list_fields" >
                            客户简称
                        </td>
                        <td   class="tdColInput">
                            <input name="txtCustNam" id="txtCustNam" specialworkcheck="客户简称" type="text" class="tdinput"
                                style="width: 95%" maxlength="25" /></td>
                    </tr>
                    <tr class="table-item">
                        
                        <td    class="td_list_fields" >
                            拼音缩写
                        </td>
                        <td   class="tdColInput">
                            <input name="txtCustShort" id="txtCustShort" specialworkcheck="拼音缩写" type="text"
                                class="tdinput" style="width: 95%" maxlength="25" /></td>
                       
                          <td    class="td_list_fields" >
                            分管业务员<span class="redbold">*</span>
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="UserLinker" id="UserLinker" type="text" runat="server" readonly class="tdinput"
                                style="width: 95%" maxlength="50" onclick="alertdiv('UserLinker,txtJoinUser');" /><input
                                    type="hidden" runat="server" id="txtJoinUser" />
                        </td>
                        <td    class="td_list_fields" >
                            结算单位
                        </td>
                        <td   class="tdColInput">
                            <input name="txtBillUnit" id="txtBillUnit" type="text" class="tdinput"
                                style="width: 95%" maxlength="50" />
                        </td>                       
                    </tr>
                    <tr class="table-item">
                    <td    class="td_list_fields" >
                            注册地址
                        </td>
                        <td   class="tdColInput">
                        <input name="txtSetupAddress" id="txtSetupAddress" type="text" class="tdinput" style="width: 95%"  maxlength="50" />                        
                         </td>
                       
                    <td    class="td_list_fields" >
                     收货地址
                    </td>
                    <td   align="left" class="tdColInput">
                            <input name="txtReceiveAddress" id="txtReceiveAddress" type="text" class="tdinput"
                                style="width: 95%" maxlength="50" />
                        </td>
                       <td    class="td_list_fields"  style="width: 10%">
                           <%-- 所属区域--%>
                        </td>
                        <td   align="left" class="tdColInput" style="width: 23%">
                            <asp:DropDownList ID="ddlArea" runat="server" style="display:none;">
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr class="table-item">
                        <td    class="td_list_fields" >
                            <div id="divContactName">联系人</div>
                            
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="txtContactName" id="txtContactName" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>
                        <td    class="td_list_fields" >
                            电话
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="txtTel" id="txtTel" type="text" class="tdinput" style="width: 95%" maxlength="25" />
                        </td>
                        <td    class="td_list_fields" >
                            手机
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="txtMobile" id="txtMobile" type="text" class="tdinput" style="width: 95%" maxlength="11" />
                        </td>
                    </tr>   
                     <tr class="table-item">
                        <td    class="td_list_fields" >
                            客户简介
                        </td>
                        <td   class="tdColInput" colspan="5">
                            <textarea name="txtCustNote" id="txtCustNote" class="tdinput" rows="3" cols="80"
                                style="width: 99%; height: 90px"></textarea>
                        </td>
                    </tr> </table>
                    <!-- 基本信息结束-->
                
               
                <br />
               <!-- 财务信息开始 -->              
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;财务信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick4'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','searchClick4')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"   id="Tb_04">
                    <!---->
                    <tr id="trMoneyType" class="table-item">
                        <td    class="td_list_fields"  >
                            账号
                        </td>
                        <td   align="left" class="tdColInput" >
                            <input id="txtAccountNum" class="tdinput" maxlength="25" name="txtAccountNum" style="width: 95%"
                                type="text" />
                        </td>
                        
                        <td    class="td_list_fields"  >
                            开户行
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="txtOpenBank" id="txtOpenBank" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                        </td>
                        <td    class="td_list_fields" >
                            户名
                        </td>
                        <td   align="left" class="tdColInput" >
                            <input name="txtAccountMan" id="txtAccountMan" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                        </td>
                    </tr>
                    
                    <tr id="trAccountNum" class="table-item">
                        <td  class="td_list_fields" >
                            税务登记号
                        </td>
                        <td align="left" class="tdColInput">
                            <input name="txtTaxCD" id="txtTaxCD" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>                         
                        <td    class="td_list_fields" >
                             
                        </td>
                        <td class="tdColInput" >
                            
                        </td>
                        <td    class="td_list_fields">
                            信用额度(万元)
                        </td>
                        <td   align="left" class="tdColInput">
                            <input id="txtMaxCredit" value="0.0000" type="text" class="tdinput" onchange="Number_round(this,2)"
                                maxlength="12" />
                        </td>
                       
                    </tr>
                    
                </table>
                <br />
                  <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;附件管理
                                    </td>
                                    <td align="right">
                                        <div id='searchClick5'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_05','searchClick5')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_05">
           
                     <tr class="table-item">
                        <td   align="left" bgcolor="#FFFFFF" colspan="8">
                            <div id="divUploadAttachment" runat="server" width="100%" style="margin:0px; padding:0px;">
                              <input id="hideCustID" type="hidden" name="hideCustID"/>
                              <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>                               
                              
                              <a href="#" onclick="DealAttachment('upload');">上传附件</a> 
                            </div>
                            <div id="divDealAttachment" runat="server" style="display: none;margin:0px; padding:0px" width="100%" >
                                <span id='spanAttachmentName' runat="server" style="margin:0px; padding:0px;" >
                                </span>
                                <br/>
                                <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                            </div>
                        </td>
                    </tr>
                </table>                 
                    <div>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                              <asp:Timer ID="Timer1" runat="server" Interval="1500">
                              </asp:Timer>                             
                              <asp:Literal ID="Literal1" runat="server"></asp:Literal>                            
                          </ContentTemplate>
                        </asp:UpdatePanel>                      
                    </div>    
                      
                <asp:HiddenField ID="hfAttachment" runat="server" />
                <asp:HiddenField ID="hfPageAttachment" runat="server" />
                <br />
                <!--辅助信息开始-->
                <table width="100%" id="tb_FZ" border="0" cellspacing="0" cellpadding="0">
                <tr><td>
                
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td   class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;辅助信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick3'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03"> 
                    <tr class="table-item">
                        <td  class="td_list_fields" >
                            备注
                        </td>
                        <td class="tdColInput" colspan="5" >
                            <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" class="tdinput" style="width: 99%;
                                height: 40px"></textarea>
                        </td>
                    </tr> 
                        <tr class="table-item">
                        <td class="td_list_fields" >
                            建档人
                        </td>
                        <td   class="tdColInput">
                            <input name="txtCreator" id="txtCreator" type="text" class="tdinput" style="width: 95%"
                                runat="server" disabled="disabled" /></td>
                        <td class="td_list_fields" >
                            建档日期<span class="redbold">*</span>
                        </td>
                        <td   class="tdColInput">
                           <input name="txtCreatedDate" id="txtCreatedDate" type="text" class="tdinput" runat="server"
                                disabled="disabled" /></td>
                        <td class="td_list_fields" >
                            启用状态<span class="redbold">*</span></td>
                        <td   align="left" bgcolor="#FFFFFF" >
                            <select name="seleUsedStatus" width="20px" id="seleUsedStatus0">
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>
                      </td>
                        
                    </tr>                    
                   
                    
                    <tr class="table-item">
                        <td  class="td_list_fields" >
                            最后更新用户
                        </td>
                        <td align="left" class="tdColInput">
                            <input name="txtModifiedUser" id="txtModifiedUser" type="text" class="tdinput" runat="server"
                                disabled="disabled" />
                        </td>
                        <td    class="td_list_fields" >
                            最后更新日期
                        </td>
                        <td   align="left" class="tdColInput">
                            <input name="txtModifiedDate" id="txtModifiedDate" type="text" class="tdinput" runat="server"
                                disabled="disabled" />
                        </td>                       
                        <td    class="td_list_fields"  style="width: 10%">
                            页面状态
                        </td>
                        <td   bgcolor="#FFFFFF" >
                            <select name="selepagestatus" width="20px" id="selepagestatus">
                                <option value="0">--请选择--</option>
                                <option value="1">确认</option>
                                <option value="2">未确认</option>
                            </select>
                        </td>
                    </tr>
                </table>
                
                </td></tr>
                 </table>
                <!--辅助信息结束-->
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;权限信息
                                    </td>
                                    <td align="right">
                                        <div id='search6'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_06','search6')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_06">
                    <!---->
                    <tr class="table-item">
                        <td    class="td_list_fields"  style="width: 10%">
                            可查看该客户档案的人员
                        </td>
                        <td   align="left" bgcolor="#FFFFFF">
                            <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" />
                        </td>
                    </tr>
                </table>
                
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
                <uc1:Message ID="Message1" runat="server" />
                <input type="hidden" runat="server" id="txtRecorder" />
                <input type="hidden" runat="server" id="txtChairman" />
                <input type="hidden" runat="server" id="txtSender" />
              <input type="hidden" runat="server" id="hfCustID" />
                <input type="hidden" id="hiddKey" />
                 <input id="hCondition" type="hidden" />
                <span id="Forms" class="Spantype"></span>
                <br />
            </td>
        </tr>
    </table>
    <!--  -->

    </form>
</body>
</html>
