<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiveVerifictionList.aspx.cs" Inherits="Pages_Office_FinanceManager_ReceiveVerifictionList" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/CustOrProvider.ascx" tagname="CustOrProvider" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js"  language="javascript" type="text/javascript" ></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    <script src="../../../js/JTHY/Expenses/ReceiveVerifictionList.js" type="text/javascript"></script>
    
<script type="text/javascript"> 
//定义回车事件 钱锋锋 2010-11-04
function document.onkeydown() 
{ 
    
    if(event.keyCode == 13)
    {
        Fun_Search_ManufactureReport(1);
        event.returnValue = false;
    } 
}
</script>
    <title>核销明细表</title>
     <style type="text/css">
        .style1
        {
            color: #044d77;
            background-color: #dfebf8;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <!-- Start 消 息 提 示 -->
    <uc1:Message ID="Message1" runat="server" />
    <uc3:CustOrProvider ID="CustOrProvider1" runat="server" />
    <%-- <uc2:ManufactureTask ID="ManufactureTask1" runat="server" />--%>
    <!-- End 消 息 提 示 -->
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
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
                                    <td height="20" class="td_list_fields" align="right" width="10%">
                                        往来单位</td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <%--<asp:TextBox ID="txtReportNo" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtTaskID" MaxLength="50" runat="server" onclick="popSellCustObj.ShowList('protion','txtTaskNo','txtTaskID','','');" CssClass="tdinput" Width="85%" 
                                         
                                            ></asp:TextBox>
                                            <input type="hidden" id="txtTaskNo" runat="server" />
                                    </td>
                                   <td class="td_list_fields" align="right" width="10%">
                                        核销款类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <%--<asp:TextBox ID="txtSubject" MaxLength="50" runat="server" CssClass="tdinput" Width="95%"
                                            specialworkcheck="主题"></asp:TextBox>--%>
                                            
                                        <asp:DropDownList ID="RVFtype" runat="server">
                                            <asp:ListItem Value="2">收款核销单</asp:ListItem>
                                            <asp:ListItem Value="1">付款核销单</asp:ListItem>
                                        </asp:DropDownList>
                                            
                                    </td>
                                    <td class="td_list_fields" align="right" width="10%">
                                        票据编号
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:TextBox ID="UserReportReal" runat="server" 
                                             CssClass="tdinput" Width="95%"></asp:TextBox>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td height="20" class="td_list_fields" align="right" width="10%">
                                        开票日期
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <asp:TextBox ID="txtstartdate1"  ReadOnly="true" Text="" runat="server" CssClass="tdinput" Width="70"
                                         
                                       onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtReportDate')})"
                                            ></asp:TextBox>
                                        
                                        <asp:TextBox ID="TextBox3"  Text="至" runat="server" CssClass="tdinput" Width="17"
                                            ></asp:TextBox>
                                        <asp:TextBox ID="txtstartdate2" ReadOnly="true"  Text="" runat="server" CssClass="tdinput" Width="70" 
                                        onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtReportDate')})"
                                            ></asp:TextBox>
                                    </td>
                                   <td class="td_list_fields" align="right" width="10%">
                                        开票金额
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                       <%-- <!-- Start 扩展属性条件 -->
                                        <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                                        <!-- End 扩展属性条件 -->--%>
                                        <asp:TextBox ID="txtprice1" onblur="checkprice('txtprice1')"  Text=""  runat="server" CssClass="tdinput" Width="70"
                                            ></asp:TextBox>
                                        
                                        <asp:TextBox ID="TextBox1"  Text="至" runat="server" CssClass="tdinput" Width="17"
                                            ></asp:TextBox>
                                        <asp:TextBox ID="txtprice2" onblur="checkprice('txtprice2')"  Text="" runat="server" CssClass="tdinput" Width="70" 
                                            ></asp:TextBox>
                                    </td>
                                   <td class="td_list_fields" align="right" width="10%">
                                       开票类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <asp:DropDownList ID="BillType" runat="server">
                                            <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">增值税发票</asp:ListItem>
                                            <asp:ListItem Value="2">普通地税</asp:ListItem>
                                            <asp:ListItem Value="3">普通国税</asp:ListItem>
                                            <asp:ListItem Value="4">收据</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <input type="hidden" id="hidModuleID" runat="server" />
                                        <input type="hidden" id="hidEditModuleID" runat="server" />
                                        <img id="btnSearch" alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_ManufactureDispatchReportList(1)' visible="false" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" height="85" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                核销明细列表</td>
        </tr>
        <%--<tr >
            <td height="35" colspan="2" valign="top" visible="false">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img id="btnNew" src="../../../images/Button/Bottom_btn_new.jpg" alt="新建生产任务汇报" style='cursor: hand;'
                                border="0" onclick="NewItem();" visible="false" runat="server" />
                            <img id="btnDel" src="../../../images/Button/Main_btn_delete.jpg" alt="删除生产任务汇报"
                                style='cursor: hand;' border="0" onclick="Fun_Delete_ManufactureReport();" visible="false"
                                runat="server" />
                            <asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                AlternateText="导出Excel" runat="server" OnClick="btnImport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr>
            <td height="50" id="tdResult">
            <div id="divDetail" style=" height:100%; width:99%;overflow-x: auto; overflow-y: auto;" >
                <table width="150%" height="23" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
                    <tbody>
                        <tr>
                            <th align="center" class="style1" style="width:3%; height:22px;" >
                                <%--<div class="orderClick" onclick="OrderBy('ReportNo','oReportNo');return false;">--%>
                                    单据日期<%--<span id="oReportNo" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:5%; height:22px;">
                               <%-- <div class="orderClick" onclick="OrderBy('deptname','odeptname');return false;">--%>
                                    客户<%--<span id="odeptname" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1"style="width:4%; height:22px;" >
                               <%-- <div class="orderClick" onclick="OrderBy('reportdate','oreportdate');return false;">--%>
                                    开票类型<%--<span id="oreportdate" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1"style="width:4%; height:22px;" >
                                <%--<div class="orderClick" onclick="OrderBy('Sequname','oSequname');return false;">--%>
                                    票据编号<%--<span id="oSequname" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;" >
                                <%--<div class="orderClick" onclick="OrderBy('wcname','owcname');return false;">--%>
                                    币种<%--<span id="owcname" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;" >
                               <%-- <div class="orderClick" onclick="OrderBy('employeesname','oemployeesname');return false;">--%>
                                    汇率<%--<span id="oemployeesname" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;">
                                <div class="orderClick" onclick="OrderBy('operators','ooperators');return false;">
                                    开票总金额<%--<span id="ooperators" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;">
                               <%-- <div class="orderClick" onclick="OrderBy('productcount','oproductcount');return false;">--%>
                                    结算金额<%--<span id="oproductcount" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;">
                                <%--<div class="orderClick" onclick="OrderBy('FinishNum','oFinishNum');return false;">--%>
                                    余额<%--<span id="oFinishNum" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;">
                               <%-- <div class="orderClick" onclick="OrderBy('PassNum','oPassNum');return false;">--%>
                                    结算方式<%--<span id="oPassNum" class="orderTip"></span></div>--%>
                            </th>
                            <th align="center" class="style1" style="width:3%; height:22px;">
                                <%--<div class="orderClick" onclick="OrderBy('nopassnum','onopassnum');return false;">--%>
                                    核销日期<%--<span id="onopassnum" class="orderTip"></span></div>--%>
                            </th>
                             <th align="center" class="style1" style="width:3%; height:22px;">
                                <%--<div class="orderClick" onclick="OrderBy('timeunit','otimeunit');return false;">--%>
                                    收回单据类型<%--<span id="otimeunit" class="orderTip"></span></div>--%>
                            </th>
                             <th align="center" class="style1" style="width:4%; height:22px;">
                                <%--<div class="orderClick" onclick="OrderBy('WorkTime','oWorkTime');return false;">--%>
                                    账款单编号<%--<span id="oWorkTime" class="orderTip"></span></div>--%>
                            </th>
                            <%-- <th align="center" class="style1" >
                                <div class="orderClick" onclick="OrderBy('RealstartDate','oRealstartDate');return false;">
                                    实际开工时间<span id="oRealstartDate" class="orderTip"></span></div>
                            </th>
                             <th align="center" class="style1" >
                                <div class="orderClick" onclick="OrderBy('RealEndDate','oRealEndDate');return false;">
                                    实际完工时间<span id="oRealEndDate" class="orderTip"></span></div>
                            </th>                            
                             <th align="center" class="style1" >
                                <div class="orderClick" onclick="OrderBy('productname','oproductname');return false;">
                                    存货名称<span id="oproductname" class="orderTip"></span></div>
                            </th>--%>
                        </tr>
                    </tbody>
                </table>
                </div>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
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
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
    </form>
</body>
</html>
