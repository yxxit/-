<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProviderInfo_Add.aspx.cs" Inherits="Pages_Office_PurchaseManager_ProviderInfo_Add" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRuleControl" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建供应商档案</title>  
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
    <script src="../../../js/jthy/sysmanage/ProviderInfoAdd.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
   
    <script type="text/javascript"> 
function SelectedNodeChanged(code_text,type_code)
{   
   document.getElementById("txtCustClassName").value=code_text;
   document.getElementById("txtCustClass").value=type_code;
   hideUserList();
}
function hidetxtUserList()
{
   hideUserList();
}
function showUserList()
{
  var list = document.getElementById("userList");
  if(list.style.display != "none")
   {
      list.style.display = "none";
      return;
   }
   document.getElementById("userList").style.display = "block";
}
function hideUserList()
{
 document.getElementById("userList").style.display = "none";
}
function clearInfo() {
    document.getElementById("txtCustClassName").value = "";
    document.getElementById("txtCustClass").value = "";
    hideUserList();
}
</script>
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <input type="hidden" id="hidModuleID" runat="server" />    
    <input id="HiddenPoint" type="hidden" runat="server" />
    <input type="hidden" id="txtConfirmor" name="txtConfirmor" class="tdinput" runat="server" readonly />
    <input name="UserName" id="UserName" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"  />
    <input name="UserID" id="UserID" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/>
    <input name="SystemTime" id="SystemTime" runat="server" style="display:none" class="tdinput" type="text" size="15" readonly="readonly"/>
    
    <uc1:Message   ID="Message1" runat="server" />
    <span id="Forms" class="Spantype"></span>
    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td height="30" align="left" class="Title">
                            <div id="divTitle" runat="server">
                                &nbsp;&nbsp;新建供应商档案
                            </div>
                        </td>
                                    
                        <td align="right" >
                            &nbsp;<img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server"  alt="保存" id="save_PurchaseReject" style="cursor:pointer" onclick="InsertProviderInfo();" visible="false" runat="server"/>
                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor:hand; display:none;" onclick="Back();" />
                            <img  src="../../../Images/Button/Main_btn_print.jpg" alt="打印" style="  cursor: pointer; display:none;"  id="imgPrint"  onclick="ProviderInfoPrint();" /> 
                            <input type="hidden" id="hiddenBillStatus" name="hiddenBillStatus" value="0" />
                            <input type="hidden" id="txtIndentityID" value="0" runat="server" />
                            <input type="hidden" id="txtIsliebiaoNo" value="0" runat="server" />
                            &nbsp;
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
                         <td  class="td_list_title">
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
                <table width="99%" border="0" align="center"  cellpadding="2" cellspacing="1"bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr class="table-item">
                        <td  class="td_list_fields" >
                            供应商编号<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput" >
                            <div id="divInputNo" runat="server">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <div id="divProviderInfoNo" runat="server" class="tdinput"  style="display: none">
                            </div>
                        </td>
                        <td   class="td_list_fields" >
                            供应商名称<span class="redbold">*</span>
                        </td>
                        <td class="tdColInput">
                                <asp:TextBox ID="txtCustName" runat="server" MaxLength="50"  CssClass="tdinput" Width="95%" SpecialWorkCheck="供应商名称"  ></asp:TextBox>
                        </td>
                        <td  class="td_list_fields" >
                            供应商简称
                        </td>
                        <td  class="tdColInput">
                            <asp:TextBox ID="txtCustNam" runat="server" MaxLength="25"  CssClass="tdinput"  onblur="LoadPYShort();" Width="95%"  SpecialWorkCheck="供应商简称" ></asp:TextBox>
                        </td>                          
                    </tr>
                    <tr class="table-item">
                        
                        
                        <td align="right" class="td_list_fields">
                            分管采购员
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="UsertxtManager" onclick="alertdiv('UsertxtManager,HidManager');" runat="server"
                             ReadOnly="true" CssClass="tdinput" Width="95%"></asp:TextBox>
                             <input type="hidden" id="HidManager" runat="server" />
                        </td>
                        
                        <td class="td_list_fields" >
                            供应商拼音代码
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtPYShort" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"></asp:TextBox>
                         </td>
                         <td align="right"  class="td_list_fields"  >
                            <%--所在区域--%>
                        </td>
                        <td class="tdColInput"  >
                           <select name="drpAreaID" style="width: 120px;margin-top:2px;margin-left:2px; display:none;" runat="server" id="drpAreaID"> </select>
                        </td>
                    </tr>                   
                    <tr class="table-item">                       
                        <td class="td_list_fields">
                            联系人
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtContactName" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="联系人" ></asp:TextBox>
                        </td>
                        <td class="td_list_fields" >
                            电话
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtTel" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="电话" ></asp:TextBox>
                        </td>
                        <td align="right"  class="td_list_fields"  >
                            手机
                        </td>
                        <td class="tdColInput" >
                            <asp:TextBox ID="txtMobile" runat="server" MaxLength="25"  CssClass="tdinput" Width="95%"  SpecialWorkCheck="手机" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="table-item">                        
                        <td class="td_list_fields">
                            发货地址
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtSendAddress" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>
                        <td class="td_list_fields">
                            注册地址
                        </td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtSetupAddress" MaxLength="100" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>
                        </td>                        
                        <td class="td_list_fields">
                        供应商类别                           
                        </td>
                        <td class="tdColInput">
                         <select name="txtCustType"  style="width: 120px;margin-top:2px;margin-left:2px;" id="txtCustType" >
                              <option value="0" selected="selected">普通供应商</option>
                              <option value="5">服务商</option></select>
                        
                        </td>
                    </tr>
                      <tr class="table-item">
                        <td align="right"  class="td_list_fields"  >
                            供应商简介
                        </td>
                        <td class="tdColInput"  colspan="5">                           
                            <textarea name="txtCustNote" id="txtCustNote" rows="3" cols="80" style="width:99%"></textarea>                            
                        </td>                   
                    </tr>       
                    
                </table>
               <br /> 
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td   class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;财务信息
                                    </td>
                                    <td align="right">
                                        <div id='divFinance'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divFinance')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center"  cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_04" style="display: block">
                            
                    <tr class="table-item">
                    <td class="td_list_fields" >
                            开户行</td>
                        <td   class="tdColInput"  >
                            <asp:TextBox ID="txtOpenBank" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="开户行" ></asp:TextBox>
                        </td>
                        <td class="td_list_fields" >
                            户名
                        </td>
                        <td  class="tdColInput">
                            <asp:TextBox ID="txtAccountMan" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"  SpecialWorkCheck="户名" ></asp:TextBox>
                        </td>
                        <td   align="right"  class="td_list_fields"  >
                            帐号
                        </td>
                        <td   class="tdColInput" >
                            <asp:TextBox ID="txtAccountNum" runat="server" MaxLength="25" CssClass="tdinput" Width="95%"  SpecialWorkCheck="帐号" ></asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
                <br /> 
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr >
                        <td  class="td_list_title">
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
                <table width="99%" border="0" align="center"  cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_05">
                    
                  
                 
                     <tr id="Attachment" class="table-item">
                        <td align="left" bgcolor="#FFFFFF" colspan="8">
                            <div id="divUploadAttachment" runat="server" width="100%" style="margin:0px; padding:0px;">
                              <input id="hideCustID" type="hidden" name="hideCustID"/>
                              <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>                               
                              <%--<a href="#"  onclick="NewMessage()" id="newDoc">上传文档</a>
                              <a href="#"  onclick="" id="newDoc1" style="color:Gray;" title="该功能当前不可用，将在保存客户信息后开放。">上传文档</a> --%>
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
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr >
                         <td   class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;辅助信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center"  cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" style="display: block">
                           
                        <tr class="table-item">
                        <td   align="right"  class="td_list_fields">
                            启用状态<span class="redbold">*</span>
                        </td>
                        <td   align="left" class="tdColInput">
                            <select name="drpUsedStatus"  style="width: 120px;margin-top:2px;margin-left:2px;" id="drpUsedStatus" >
                                        <option value="1" selected="selected">启用</option>
                                        <option value="0">停用</option></select>
                        </td>
                        <td   align="right"  class="td_list_fields"  >
                            建档人
                        </td>
                        <td   class="tdColInput" >
                            <input type="text" id="txtCreatorReal" name="txtCreatorReal" class="tdinput"  
                                runat="server" readonly="true" disabled="disabled" />
                            <input type="hidden" id="HidCreator" name="HidCreator" class="tdinput" runat="server"/>
                        </td>
                        <td   align="right"  class="td_list_fields"  >
                            建档日期
                        </td>
                        <td   class="tdColInput"  >
                            <input type="text" id="txtCreateDate" name="txtCreateDate" class="tdinput" 
                                runat="server" readonly disabled="disabled" />
                        </td>
                    </tr>
                     <tr class="table-item">
                        <td align="right"  class="td_list_fields"  >
                            最后更新日期
                        </td>
                        <td class="tdColInput"  >
                           <asp:TextBox ID="txtModifiedDate" runat="server"  CssClass="tdinput" 
                                readonly="true" Width="95%" Enabled="False"></asp:TextBox>
                            <input type="hidden" id="txtModifiedDate2" name="txtModifiedDate2" class="tdinput" runat="server" readonly />
                        </td>
                        <td align="right"  class="td_list_fields"  >
                            最后更新用户
                        </td>
                        <td class="tdColInput"  >
                           <input type="text" id="txtModifiedUserIDReal" name="txtModifiedUserIDReal" 
                                class="tdinput" disabled="disabled" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" class="tdinput" runat="server" readonly />
                            <input type="hidden" id="txtModifiedUserID2" name="txtModifiedUserID2" class="tdinput" runat="server" readonly />
                        </td>    
                        
                        <td align="right"  class="td_list_fields"  >
                           
                        </td>
                        <td class="tdColInput" >
                                <%--<input name='usernametemp' type='hidden' id='usernametemp' runat="server" />--%>
                                <%--<input name='datetemp' type='hidden' id='datetemp' runat="server" />--%>
                                <input id="txtAction" type="hidden" value="1" />
                                <input type="hidden" id="hidIsliebiao" name="hidIsliebiao" runat="server"/>
                                <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                <input type="hidden" id="Hidden1" runat="server" />
                        </td>                   
                     </tr>
                    
                   
                </table>
               
                 
               </td>
        </tr>
    </table>
    </form>
</body>
</html>
