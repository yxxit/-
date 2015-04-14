<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InitSystemData.aspx.cs" Inherits="Pages_Office_SystemManager_InitSystemData" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加初始化数据</title>
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
    <input type="hidden" id="hidDeptName" runat="server" /><%--获取组织机构缩写--%>
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
                                添加初始化数据
                            </td>
                        </tr>
                    </table>
                    <%--                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF">--%>
                    <%--                                <img runat="server" src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" onclick="GetPYShort();"
                                    style='cursor: pointer;' id="btnAdd" />
                                <asp:HiddenField ID="hidModuleID" runat="server" />
                                <asp:HiddenField ID="hidSearchCondition" runat="server" />--%>
                    <%--                            </td>
                        </tr>
                    </table>--%>
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
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    用户名<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input id="txtUser" class="tdinput" style="width: 35%" type="text" runat="server" />
                                    &nbsp&nbsp&nbsp&nbsp&nbsp 用户名长度必须在8-10位之间(字母与数字结合)
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    密码<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtPassword" class="tdinput" style="width: 35%" value="88888888"
                                        runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp 密码位数处于8-16位之间(字母与数字结合)
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    组织机构名称<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input id="txtDeptName" class="tdinput" style="width: 35%" type="text" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp<span
                                        class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    人员档案名称<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" maxlength="130" class="tdinput" style="width: 35%" id="txtEmployeeName"
                                        value="管理员" onblur="GetPYShortForEmployee();" specialworkcheck="姓名" runat="server" />&nbsp&nbsp&nbsp&nbsp&nbsp<span
                                            class="redbold">可修改</span>
                                    <input type="hidden" id="hidEmployeeName" value="GLY" runat="server" /><%--获取人员档案名称缩写--%>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    仓库名称<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtStorageName" value="默认仓库" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="仓库名称">&nbsp&nbsp&nbsp&nbsp&nbsp<span class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    客户名称<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtCustName" value="常州市莱特网络技术有限公司" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="常州市莱特网络技术有限公司" onblur="GetPYShortForCust();">&nbsp&nbsp&nbsp&nbsp&nbsp<span
                                            class="redbold">可修改</span>
                                    <input type="hidden" id="hidCustName" value="CZLTWLJSYXGS" runat="server" /><%--获取人员档案名称缩写--%>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    联系人<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtLinkManName" value="莱特客服" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="莱特客服">&nbsp&nbsp&nbsp&nbsp&nbsp<span class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    联系人电话<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtWorkTel" value="86577660" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="86577660">&nbsp&nbsp&nbsp&nbsp&nbsp<span class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    供应商类别<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtTypeName" value="普通供应商" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="普通供应商">&nbsp&nbsp&nbsp&nbsp&nbsp<span class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" style="width: 15%">
                                    供应商名称<span class="redbold">*</span>
                                </td>
                                <td bgcolor="#FFFFFF">
                                    <input type="text" id="txtProName" value="常州市莱特网络技术有限公司" maxlength="130" class="tdinput"
                                        style="width: 35%" specialworkcheck="常州市莱特网络技术有限公司">&nbsp&nbsp&nbsp&nbsp&nbsp<span
                                            class="redbold">可修改</span>
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" colspan='2'>
                                    默认人员档案名称（管理员）、默认角色（管理员，功能全部） 、用户使用期限：90天 、默认帐号关联（管理员与管理员）、默认供应商联络期限7天
                                </td>
                            </tr>
                            <tr>
                                <td height="20" bgcolor="#FFFFFF" colspan='2'>
                                    公司仓库（默认仓库）、物品分类（成品：默认成品分类、原材料：默认原材料分类、半成品：默认半成品分类）、默认计量单位（个、台、件、只）
                                </td>
                            </tr>
                              <tr>
                                <td height="20" bgcolor="#FFFFFF" colspan='2'>
                                    默认货币（人民币、美元、日元、欧元、英镑）、设备类别（默认）、设备（默认设备）、竞争对手细分（默认）、日常调整原因（默认）、采购退货原因（默认）、采购类别（默认类别）
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
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" align="center">
                                <img runat="server" src="../../../images/Button/Bottom_btn_Init.jpg" alt="保存" onclick="GetPYShort();"
                                    style='cursor: pointer;' id="btnAdd" />
                                <asp:HiddenField ID="hidModuleID" runat="server" />
                                <asp:HiddenField ID="hidSearchCondition" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <uc7:Message ID="Message1" runat="server" />

    <script src="../../../js/office/SystemManager/InitSystemData.js" type="text/javascript"></script>

    </form>
</body>
</html>
