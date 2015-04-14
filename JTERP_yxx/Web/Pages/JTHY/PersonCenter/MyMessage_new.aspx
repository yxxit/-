<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyMessage_new.aspx.cs" Inherits="Pages_jthy_PersonCenter_MyMessage_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发送短信</title>
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/personal/common.js" type="text/javascript"></script>
    <script src="../../../js/personal/MessageBox/UserListCtrl.js" type="text/javascript"></script>
    <script src="../../../js/personal/MessageBox/SendInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="hidSearchCondition" runat="server" />
    <input type="hidden" id="hiddenEquipCode" value="" />
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">

        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="Title">
                            &nbsp;&nbsp;发送短信
                        </td >
                        <td align="right" valign="middle" >
                            
                            <img id="btnBack" alt="返回" src="../../../images/Button/Bottom_btn_back.jpg" onclick="BackToPage();"
                                runat="server" style="cursor: pointer; visibility: hidden" />
                            <input type="checkbox" id="smFlag" name="smFlag" style="visibility:hidden"/><%--发到手机--%>
                            <input type="checkbox" id="smFlagSite" checked="checked" name="smFlagSite" style="visibility:hidden" /><%--发到站内短信--%>
                            <img id="btn_send" visible="false" runat="server" src="../../../Images/Button/Main_btn_send.jpg"
                                style="cursor: pointer;" onclick="SendInfo(this)" />
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
                                        &nbsp;&nbsp;
                                    </td>
                                    <td align="right">
                                        <div id="infoTip" style="margin-left: 110px; color: Red;">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                           
                        </td>
                    </tr>
                </table>                
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr class="table-item">
                        <td class="td_main_detail "  >
                            收件人<font color="red">*</font>
                        </td>
                        <td >
                            <input type="text" id="txtToList" readonly style="color: #999999;margin-left:1%; width:95%;" value="请在右侧的导航中选择收件人" />
                            <input type="hidden" value="" id="seluseridlist" />
                        </td>
                        <td bgcolor="#FFFFFF" width="40%" align="center" >
                            <div style="color: Red; font-weight: bold;">
                                请在下面的导航中选择收件人</div>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_main_detail " >
                            主题<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF"  >
                            <input type="text" id="txtTitle" style=" margin-left:1%; width:95%;" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <ul id="typeListTab" >
                                <li id="tab_0" class="tab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)">
                                    全部</li>
                                <li id="tab_1" class="selTab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">
                                    部门</li>
                                <li id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)">
                                    分组</li>
                            </ul>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td class="td_main_detail " valign="top" >
                            内容<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF"  valign="top">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" style=" margin-top:7px; margin-left:1%;" runat="server" Rows="16" 
                                Height="234px" width="95%"></asp:TextBox>
                        </td>
                        <td valign="top" bgcolor="#FFFFFF">
                            <table>
                                <tr>
                                    <td>
                                        <div id="userList">
                                            </div>
                                        <div style="border: solid 1px #999999; margin:5px 10px; padding: 5px; text-align: center; width: 100px;">
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
    </table>
    </form>
</body>
</html>
