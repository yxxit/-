<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeInfo_Add.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeInfo_Add" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/SysParamDrpControl.ascx" tagname="SysParam" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加人员</title>
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/jthy/sysmanage/EmployeeInfo_Add.js" type="text/javascript"></script>    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript">

</script>
    </head>
<body> 
<form id="frmMain" runat="server">
<input type="hidden" id="hidEditFlag" runat="server" />
<input type="hidden" id="hidFromPage" runat="server" />
<input type="hidden" id="hidWorkModuleID" runat="server" />
<input type="hidden" id="hidLeaveModuleID" runat="server" />
<input type="hidden" id="hidReserveModuleID" runat="server" />
<input type="hidden" id="hidInterviewModuleID" runat="server" />
<input type="hidden" id="hidWaitModuleID" runat="server" />
<input type="hidden" id="hidInitSysModuleID" runat="server" />
<input type="hidden" id="hidInitHumanModuleID" runat="server" />
<input type="hidden" id="hidSearchCondition" runat="server" />
<input type="hidden" id="hidSysteDate" runat="server" />
<input type="hidden" id="hidEmployeeID" runat="server" />
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
    <tr >
        <td height="30" colspan="2" valign="top" class="Title" >
            <table width="100%" border="0" cellspacing="0" cellpadding="0" border="0">
                <tr >       
                    <td align="left"  class="Title"><div id="divTitle" runat="server" style="margin-left:7px;">&nbsp;&nbsp;新建人员档案</div></td>   
                    <td align="right"  >
                    <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand" onclick="SaveEmployeeInfo();"/>
                    &nbsp;                                    
                    <img src="../../../Images/Button/btn_jxlr.jpg" runat="server" ID="btnCont" visible="false" alt="继续录入" style="cursor:hand" onclick="Continue();"/>
                    <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server"  visible="false"  alt="返回" id="btnBack" onclick="DoBack();" style="cursor:hand" /> 
                    <img src="../../../Images/Button/Main_btn_print.jpg" runat="server"  alt="打印" id="btnPrint" onclick="DoPrint();" style="cursor:hand;display:none;"  />
                    &nbsp;
                    </td>
                </tr>
            </table>
        </td>  
    </tr>
    </table>
<!-- <div style="height:500px;overflow-y:scroll;"> -->
<table width="98%" border="0" cellpadding="0" cellspacing="0" id="tblmain">
    <tr>
        <td colspan="2" valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="3">
                    </td>
                </tr>
            </table>
        </td>
     </tr>
      <tr>  
        <td  colspan="2">
            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                <tr>
                     <td   class="td_list_title">
                        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="3">
                            <tr class="menutitle1">
                                <td align="left">
                                        &nbsp;&nbsp; 基本信息
                                </td>
                                <td align="right">
                                    <div id='divBaseInfo'>
                                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('tblBaseInfo','divBaseInfo')"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>          
            <table width="99%"  border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display:block">
                <tr  class="table-item">
                  <td  align="right"  class="td_list_fields">编号<span class="redbold">*</span></td>
                  <td class="tdColInput">
                    <div id="divCodeRule" runat="server" style="margin-top:2px;margin-left:2px;">
                        <uc1:CodingRule ID="codruleEmployNo" runat="server"  />
                    </div>
                    <div id="divEmployeeNo" runat="server" class="tdinput" style="margin-top:2px;margin-left:2px;"></div>
                  </td>
                  <td   align="right"  class="td_list_fields">姓名<span class="redbold">*</span></td>
                  <td class="tdColInput">
                    <input type="text" maxlength="25" class="tdinput" id="txtEmployeeName"  style="width:95%" onblur="GetPYShort();"  specialworkcheck="姓名"  runat="server" />
                  </td>
                  <td align="right"  class="td_list_fields"><div id="divNum">工号</div></td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtEmployeeNum" runat="server" Width="95%" MaxLength="20" CssClass="tdinput"></asp:TextBox>
                  </td> 
                                   
                </tr>
                <tr  class="table-item">
                  
                  <td align="right"  class="td_list_fields" >拼音缩写</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtPYShort" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                  <td align="right"  class="td_list_fields" >人员分类<span class="redbold">*</span></td>
                  <td class="tdColInput" >
                    <select id="ddlFlag" runat="server" style="margin-top:2px;margin-left:2px;">
                        <option value="1" selected="selected">在职人员</option>
                        <option value="2" >人才储备</option>
                        <option value="3">离职人员</option>
                    </select>
                  </td> 
                                    
                  
                  <td align="right"  class="td_list_fields" >出生日期</td>
                  <td class="tdColInput" >
                    <asp:TextBox ID="txtBirth" runat="server" ReadOnly="true" MaxLength="10" CssClass="tdinput" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBirth')})"></asp:TextBox>
                  </td>
                </tr>  
                <tr  class="table-item" id="Tr1" runat="server">
                  <td height="25" align="right"  class="td_list_fields">
                      所在部门</td>
                  <td class="tdColInput" valign="middle">
                      <input id="DeptName" runat="server" type="text" class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" />
                      <input id="hdDeptID" type="hidden" runat="server" /> </td>
                  <td align="right"  class="td_list_fields" >入职时间</td>
                  <td class="tdColInput" >
                    <input ID="txtEnterDate" readonly="readonly" class="tdinput" runat="server" 
                          onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEnterDate')})" /></td>
                
                  <td align="right"  class="td_list_fields" width="10%">联系电话</td>
                  <td class="tdColInput" width="23%">
                    <asp:TextBox ID="txtTelephone" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                </tr>
                
                <tr  class="table-item">
                  <td align="right"  class="td_list_fields" width="10%">手机号码</td>
                  <td class="tdColInput" width="23%">
                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="50" CssClass="tdinput" Width="95%"></asp:TextBox>
                  </td>
                  <td align="right"  class="td_list_fields" width="10%">电子邮件</td>
                  <td class="tdColInput" width="24%">
                    <asp:TextBox ID="txtEMail" runat="server" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>
                  </td>
                
                  <td height="25" align="right"  class="td_list_fields">性别<span class="redbold">*</span></td>
                  <td class="tdColInput" >
                    <asp:DropDownList ID="ddlSex" runat="server" style="margin-top:2px;margin-left:2px;">
                        <asp:ListItem Text="男" Value="1"></asp:ListItem>
                        <asp:ListItem Text="女" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                  </td>
                 </tr>  
                <tr style=" display:none">
                  <td align="right"  class="td_list_fields" style="display:none">岗位</td>
                  <td class="tdColInput" style="display:none">  
                    <asp:TextBox runat="server"  ID="ddlQuarter_ddlCodeType" MaxLength="50" Width="95%" CssClass="tdinput"></asp:TextBox>                  
                  </td>
                  <td align="right"  class="td_list_fields" width="10%"></td>
                  <td class="tdColInput" width="23%">                    
                  </td>  
                  <td align="right"  class="td_list_fields" width="10%"></td>
                  <td class="tdColInput" width="23%">                    
                  </td> 
                </tr>                  
   </table>
<!-- </div> -->
</td>
</tr>
</table>
<div id="popupContent"></div>
<span id="Forms" class="Spantype"></span>
<uc1:Message ID="msgError" runat="server" />
</form>
</body>
</html>
