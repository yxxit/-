<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo_Modify.aspx.cs" Inherits="Pages_Office_SystemManager_UserInfo_Modify" %>

<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>用户管理追加</title>&nbsp;<link href="../../../css/validatorTidyMode.css" rel="stylesheet"
        type="text/css" />
    <script src="../../../js/office/SystemManager/UserInfoModify.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
   <script src="../../../js/common/Common.js" type="text/javascript"></script>
   <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>  
   <%-- <script src="../../../js/common/USBSNReader.js" type="text/javascript"></script>--%>
   <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
  <style type="text/css">                                                             
    #EmployeeID
    {
        width: 124px;
    }
    #Checkbox1
    {
        width: 20px;
    }
    #EmployeeID0
    {
        width: 124px;
    }
      .style1
      {
          width: 362px;
      }
      .style2
      {
          border: 1px solid #cccccc;
          padding-left: 5px;
          font-size: 12px;
          height: 20px;
          background-color: #ffffff;
          width: 362px;
          margin-right: 0px;
      }
      .style3
      {
          width: 140px;
      }
      .style4
      {
          color: #044d77;
          background-color: #dfebf8;
          width: 83px
      }
    </style>
      <script type="text/javascript" language="javascript">
            //选择员工姓名
        function fnSelectEmployee() {
             alertdiv('EmployeeName,EmployeeID');
        }
    </script>
 </head>
 <body>
    <form id="frmMain" runat="server" >
     <input  type="hidden" id="IsCompanyOpen" runat="server"/>
    <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
     <input type="hidden" id="EmployeeID" runat="server" />
    <div id="popupContent">
        <uc1:Message ID="Message1" runat="server" />
    </div>
    <span id="Forms" class="Spantype"></span>
      <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" runat="server" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            修改用户信息
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="btnSave" style="cursor: hand; float:left"
                               border="0" onclick="InsertUserData();"  runat="server"  visible="false" />
                                             <a onclick="DoBack();">
                                            <img src="../../../images/Button/Bottom_btn_back.jpg" border="0"  style=" float:left;" id="btnback" runat="server"/><asp:HiddenField ID="hidSearchCondition" runat="server" />
                         
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                            <asp:HiddenField ID="txtUserName" runat="server" />
                            </a></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
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
                                        基础信息
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
       <div id="toachun">
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr>
                        <td  align="right" class="td_list_fields" >
                            用户名<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF" class="style3">
                            <input id="txtUserID" class="tdinput" name="txtUserID" size="15" type="text" 
                                runat="server" disabled="disabled"  />
                        </td>
                        <td  align="right" class="style4" >
                            员工姓名<span class="redbold">*</span></td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <%--<font color="red">
                                <select id="EmployeeID" runat="server" name="SetPro1" width="139px">
                                    <option></option>
                                </select></font>--%>
                                 <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" style="width: 90%">
                                            <input id="EmployeeName" class="tdinput" onclick="fnSelectEmployee()" readonly="readonly"
                                                style="width: 85%;" type="text" runat="server" />
                                        </td>
                                        <td>
                                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelectEmployee()" />
                                        </td>
                                    </tr>
                                </table>
                        </td>
                        <td  align="right" class="td_list_fields" >
                            锁定
                        </td>
                        <td bgcolor="#FFFFFF">
                            &nbsp;<font color="red"><input id="UsedStatus" type="hidden" value="1" runat="server"/></font><input id="chkLockFlag" type="checkbox" runat="server" /></td>
                    </tr>
                    <tr id="CloseDate">
                        <td class="td_list_fields" align="right"  height="20"  >
                            生效日期<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="style3" >
                            <input id="txtOpenDate" class="tdinput" name="txtbuydate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOpenDate')})"
                                size="15" type="text" /></td>
                        <td class="style4" align="right"  height="20"  >
                            失效日期<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" class="style2" >
                            <input id="txtCloseDate" class="tdinput" name="txtbuydate0" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtCloseDate')})"
                                size="15" type="text" /> </td>
                                  <td class="td_list_fields" align="right"  height="20"  >
                             <span  id="spanUsbTitle" style=" display:none"> 是否启用加密狗</span> 
                            </td>
                            <td height="20" bgcolor="#FFFFFF">
                           <input type="checkbox"  id="chkIsHardValidate" checked="false" runat="server" onclick="ReadUsbSN();" style=" display:none"/><input id="usbkey"  name="usbkey"  type="text"
                                   disabled="disabled"  class="tdinput" style=" display:none; width:90%" />
                            </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        备注信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Open.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: none">
                    <tr>
                        <td class="td_list_fields"  height="20"  align="center"  >
                            备注
                        </td>
                        <td height="20" colspan="5" bgcolor="#FFFFFF">
                            <textarea name="txtEquipRemark" id="txtRemark" class="tdinput" cols="50" rows="5" runat="server"></textarea>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="2" bgcolor="#999999">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
    <p>
        <input id="Hidden1" type="hidden" />
    </p>
    </body>
</html>
