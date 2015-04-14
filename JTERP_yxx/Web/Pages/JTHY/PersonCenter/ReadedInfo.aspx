<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadedInfo.aspx.cs" Inherits="Pages_Personal_MessageBox_ReadedInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
 

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/Personal/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/personal/common.js" type="text/javascript"></script>

    <script src="../../../js/personal/MessageBox/UserListCtrl2.js" type="text/javascript"></script>

    <script src="../../../js/Personal/MessageBox/ReadedInfo.js" type="text/javascript"></script>

    <title>未读短信</title>
    <style type="text/css">
        #selUserBox
        {
            background: #ffffff;
        }
        #userList
        {
            border: solid 1px #3366cc;
            width: 200px;
            height: 300px;
            overflow: auto;
            padding-left: 10px;
        }
        #typeListTab
        {
            background: #2255bb;
            padding: 5px;
            margin: 0px;
            width: 202px;
            background: #3366cc;
        }
        /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */.tab
        {
            cursor: pointer;
            display: inline;
            color: White;
            background-color: inherit;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        .selTab
        {
            cursor: pointer;
            display: inline;
            color: Black;
            background-color: White;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
        }
        .style1
        {
            width: 266px;
        }
        .style2
        {
            width: 77px;
            height: 20px;
        }
        .style3
        {
            width: 212px;
            height: 20px;
        }
        .style4
        {
            width: 218px;
            height: 20px;
        }
        .style5
        {
            height: 20px;
        }
        .style6
        {
            width: 266px;
            height: 20px;
        }
    </style>
</head>
<body>
    <input type="hidden" id="isSearched" value="0" />
    <form id="form1" runat="server">
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype" name="Forms"></span>
    <input type="hidden" id="hidSearchCondition" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
     <tr>
            <td valign="middle" align="center">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0">
                    <tr class="menutitle1">
                        <td align="left" valign="middle">
                            &nbsp;&nbsp;检索条件
                        </td>
                        <td align="right" valign="middle">
                            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    <td class="td_list_fields" align="right" width="10%">
                                        接收时间
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <input onkeydown="return false;" name="createDate" id="createDate1" class="tdinput"
                                            type="text" size="14" onclick="WdatePicker()" />
                                        ~<input onkeydown="return false;" name="createDate" id="createDate2" class="tdinput"
                                            type="text" size="14" onclick="WdatePicker()" />
                                    </td>
                                   <td class="td_list_fields" align="right" width="10%">
                                        主题
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width: 99%;" />
                                    </td>
                                   <td class="td_list_fields" align="right" width="10%">
                                        发送人
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <input name="txtSender" id="txtSender" onclick="showSelPanel()" readonly="readonly"
                                            type="text" class="tdinput" style="width: 80%;" />
                                        <input type="hidden" value="" id="txtSenderHidden" />
                                        <a href="#" onclick="clearSel(this)">清空</a>
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            onclick='SearchEquipData()' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
      
         <tr>
            <td colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" cellpadding="0" cellspacing="0">
                    <tr class="menutitle1">
                        <td align="left" valign="middle">
                            &nbsp;&nbsp;已发短信
                        </td>
                        <td align="right" valign="middle">
                            <img src="../../../images/Button/Main_btn_delete.jpg" title="删除" id="btn_del" runat="server"
                                visible="false" onclick="DelMessage()" style='cursor: pointer;' />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th height="20" align="center" class="td_list_fields">
                                选择
                            </th>
                            <th width="20%" align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'Title','oC2');return false;">
                                    主题<span></span></div>
                            </th>
                            <th width="30%" align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'Content','oC5');return false;">
                                    内容<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'SendUserID','oGroup');return false;">
                                    发送人<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'CreateDate','oC1');return false;">
                                    接收时间<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick">
                                    操作<span></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="PageCount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total" style="display: none"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: pointer;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
    <div id="selUserBox" style="position: absolute; display: none;">
        <ul id="typeListTab">
            <li id="tab_0" class="selTab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)">
                全部</li>
            <li id="tab_1" class="tab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">
                部门</li>
            <li id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)">
                分组</li>
            <li style="display: inline;" onclick="hideSelPanel()">
                <img style="margin-left: 60px; cursor: pointer;" align="absbottom" src="../../../Images/Pic/Close.gif" /></li>
        </ul>
        <div id="userList">
        </div>
        <div style="border: solid 1px #3366cc; padding: 5px; text-align: center; width: 200px;">
            <a href="#" onclick="clearSel()">清空</a>
        </div>
    </div>
    <div id="editPanel" style="display: none;">
        <table id="itemContainer" border="1" width="100%" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 40px;">
                    主题
                </td>
                <td>
                    <span id="ttTitle"></span>
                </td>
            </tr>
            <tr>
                <td>
                    接收时间
                </td>
                <td>
                    <span id="ttSendDate"></span>
                </td>
            </tr>
            <tr>
                <td>
                    发件人
                </td>
                <td>
                    <span id="ttSendUser"></span>
                </td>
            </tr>
            <tr>
                <td>
                    短信内容：
                </td>
                <td align="left" valign="top">
                    <span id="ttContent"></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding: 3px;">
                    <a href="#" onclick="hideMsg()">确定</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
