<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlowWaitProcess.aspx.cs"
    Inherits="Pages_Personal_WorkFlow_FlowWaitProcess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/Personal/Common.js" type="text/javascript"></script>

    <script src="../../../js/Personal/WorkFlow/FlowWaitProcess.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <title>待我审批的流程</title>
</head>
<body>
    <input type="hidden" id="isSearched" value="0" />
    <form id="form1" runat="server">
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype" name="Forms"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
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
                                    <td width="10%" height="20" class="td_list_fields" align="right">
                                        创建时间
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input onkeypress="return false;" name="createDate" id="createDate1" class="tdinput"
                                            type="text" size="10" onclick="WdatePicker()" />
                                        ~<input onkeypress="return false;" name="createDate" id="createDate2" class="tdinput"
                                            type="text" size="10" onclick="WdatePicker()" />
                                    </td>
                                    <td class="td_list_fields" align="right">
                                        流程名称
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input name="flowName" id="flowName" type="text" class="tdinput" size="19" />
                                    </td>
                                    <td class="td_list_fields" align="right">
                                        单据编号
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input name=" billNo," id="billNo" type="text" class="tdinput" size="19" />
                                    </td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20" class="td_list_fields" align="right">
                                        流程状态
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <select id="slFlowStatus" style="width: 120px;">
                                            <option value="-1" selected>——请选择——</option>
                                            <option value="1">待审批</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤销审批</option>
                                        </select>
                                    </td>
                                    <td class="td_list_fields" align="right">
                                        单据类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="330">
                                        <select id="BillFlag" onchange="setTypes(this)" style="width: 120px;">
                                        </select>
                                        <select id="BillType" style="width: 120px;">
                                        </select>
                                    </td>
                                    <td class="td_list_fields" align="right">流程发起人</td>
                                        <td bgcolor="#FFFFFF">
                                        <asp:TextBox ID="UserApplyUserID" Style="width: 98%" ReadOnly="true" CssClass="tdinput"
                                            runat="server" onclick="alertdiv('UserApplyUserID,ApplyUser,2');">
                                        </asp:TextBox>
                                        <input type="hidden" id="ApplyUser" runat="server" />
                                        </td>
                                </tr>
                                <tr class="table-item">
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                        <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            onclick='SearchEquipData()' width="52" height="23" />
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
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                待我审批的流程
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
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'billNo','oGroup');return false;">
                                    单据编号<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="return false;">
                                    对应单据类型<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'FlowName','oC1');return false;">
                                    流程名称<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'FlowStatus','oC2');return false;">
                                    流程状态<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'ApplyUserId','oC4');return false;">
                                    流程发起人<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'StepNo','oC5');return false;">
                                    当前处理步骤序号<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'StepName','oC6');return false;">
                                    当前处理步骤描述<span></span></div>
                            </th>
                            <th align="center" class="td_list_fields">
                                <div class="orderClick" onclick="OrderBy(this,'ModifiedUserID','oC8');return false;">
                                    当前处理人<span></span></div>
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
</body>
</html>
