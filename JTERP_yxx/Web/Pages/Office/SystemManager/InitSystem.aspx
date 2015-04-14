<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitSystem.aspx.cs" Inherits="Pages_Office_SystemManager_InitSystem" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统初始化</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Ajax.js" type="text/javascript"></script>


    <style type="text/css">
        .divboxbody.mydivleft
        {
            float: left;
            padding-left: 10px;
        }
    </style>
</head>
<body>
    <form id="frmMain" runat="server">
    <div id="popupContent">
    </div>
    <div>
        <span id="Forms" class="Spantype"></span>
        <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="mainindex">
            <tr>
                <td valign="top">
                    <input type="hidden" id="hiddenUserID" value="" />
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
                               系统初始化
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">
                                <img runat="server"  src="../../../images/Button/Main_btn_delete.jpg"
                                alt="删除" onclick="fnDel()" style='cursor: pointer;' id="btnDel" />
                                <asp:HiddenField ID="hidModuleID" runat="server" />
                                <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            </td>
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
                 
                    <div>
                        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                            id="Tb_01" style="display: block">
                            <tr>
                                
                                <td bgcolor="#FFFFFF">
                                    <input id="JXC" type="checkbox" onclick="Check();" runat="server" />   清除采购、生产、库存、销售、质检、门店配送、门店
                                    订货、财务等管理模块数据
                                </td>
                            </tr>
                            <tr id="CloseDate">
                                
                                <td height="20" bgcolor="#FFFFFF">
                                    <div class="mydivleft">
                                        <input id="CCust" type="checkbox" runat="server" onclick="CheckC();"/>  清除技术、客户、人力资源、办公自动化等管理模块数据
                                    </div>
                                </td>
                            </tr>
                              <tr id="Tr1">
                                
                                <td height="20" bgcolor="#FFFFFF">
                                    <div class="mydivleft">
                                        <input id="Base" type="checkbox" runat="server"/>  清除基础设置数据
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="10">
                                </td>
                            </tr>
                        </table>
                        <input id="txtUserName" type="hidden" />
                </td>
            </tr>
        </table>
    </div>
  <uc7:Message ID="Message1" runat="server" />

    <script src="../../../js/office/SystemManager/InitSystem.js" type="text/javascript"></script>
    </form>
</body>
</html>

