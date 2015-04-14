<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendInfo.aspx.cs" Inherits="Pages_Personal_MessageBox_SendInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发送短信</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #txtTitle
        {
            width: 466px;
        }
        #txtToList
        {
            width: 467px;
        }
        .style1
        {
            width: 470px;
        }
        .style2
        {
            width: 64px;
        }
        #typeListTab
        {
            background: #2255bb;
            padding: 5px;
            margin: 0px;
            width: 202px;
            background: #999999;
        }
        /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */.tab
        {
            cursor: pointer;
            display: inline;
            color: White;
            background-color: inherit;
            margin-left: 5px;
            border: solid 1px #666666;
            padding: 2px;
        }
        .selTab
        {
            cursor: pointer;
            display: inline;
            color: Black;
            background-color: White;
            margin-left: 5px;
            border: solid 1px #666666;
            padding: 2px;
        }
        .tabe
        {
            cursor: pointer;
            display: inline;
            color: Black;
            background-color: White;
            margin-left: 15px;
            padding: 2px;
        }
        #userList
        {
            border: solid 1px #999999;
            width: 200px;
            height: 300px;
            overflow: auto;
            padding-left: 10px;
        }
        .style3
        {
            width: 59px;
        }
    </style>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/personal/common.js" type="text/javascript"></script>

    <script src="../../../js/personal/MessageBox/UserListCtrl.js" type="text/javascript"></script>

    <script src="../../../js/personal/MessageBox/SendInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

</head>
<body>
    <br />
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="hidSearchCondition" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
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
                            发送短信
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" width="100%">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <div id="infoTip" style="margin-left: 110px; color: Red;">
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td colspan="2">
                                        <img id="btn_send" visible="false" runat="server" src="../../../Images/Button/Main_btn_send.jpg"
                                            style="cursor: pointer;" onclick="SendInfo(this)" />
                                        <img id="btnBack" alt="返回" src="../../../images/Button/Bottom_btn_back.jpg" onclick="BackToPage();"
                                            runat="server" style="cursor: pointer; visibility: hidden" />
                                        <input type="checkbox" id="smFlag" name="smFlag" />发到手机
                                        <input type="checkbox" id="smFlagSite" checked name="smFlagSite" />发到站内短信
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr>
                        <td align="right" class="td_list_fields" >
                            收件人<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <input type="text" id="txtToList" readonly style="color: #999999;" value="请在右侧的导航中选择收件人" />
                            <input type="hidden" value="" id="seluseridlist" />
                        </td>
                        <td bgcolor="#FFFFFF">
                            <div style="color: Red; font-weight: bold;">
                                请在下面的导航中选择收件人</div>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="td_list_fields" >
                            主题<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <input type="text" id="txtTitle" />
                        </td>
                        <td bgcolor="#FFFFFF">
                            <ul id="typeListTab">
                                <li id="tab_0" class="tab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)">
                                    全部</li>
                                <li id="tab_1" class="selTab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">
                                    部门</li>
                                <li id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)">
                                    分组</li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" class="td_list_fields" >
                            内容<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" Rows="16" Columns="60"
                                Height="300px" Width="464px"></asp:TextBox>
                        </td>
                        <td valign="top" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <div id="userList">
                                        </div>
                                        <div style="border: solid 1px #999999; padding: 5px; text-align: center; width: 200px;">
                                            <a href="#" onclick="treeview_selall()">全选/清空</a>
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <div id="userspanel" style="display: block;">
                                            <img src="../../../Images/Button/Btn_tjdfz.jpg" onclick="AddContact()" />
                                            <br />
                                            <br />
                                            分组:<select id="slgroupid"><option value="-1">——请选择——</option>
                                            </select>
                                        </div>
                                        <div id="grouppanel" style="display: none;">
                                            <img src="../../../Images/Button/Btn_tjfz.jpg" onclick="AddGroup();document.getElementById('groupname').focus();" /><br />
                                            <br />
                                            <img src="../../../Images/Button/Btn_xgfz.jpg" onclick="EditGroup();" /><br />
                                            <br />
                                            <img src="../../../Images/Button/Btn_scfz.jpg" onclick="DelGroup();" /><br />
                                            <br />
                                            <img src="../../../Images/Button/Btn_sclxr.jpg" onclick="DelContact()" />
                                            <br />
                                            <br />
                                            分组名称<font color="red">*</font>：<br />
                                            <input type="text" id="groupname" />
                                            <input type="hidden" id="groupid" /><br />
                                            <br />
                                            <img alt="保存" src="../../../images/Button/Bottom_btn_save.jpg" onclick="SaveGroup(this)"
                                                style="cursor: pointer;" />
                                            <img alt="取消" src="../../../images/Button/Bottom_btn_cancel.jpg" onclick="CancelGroup(this)"
                                                style="cursor: pointer;" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="28" bgcolor="#FFFFFF">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
