<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageCheckList.aspx.cs"
    Inherits="Pages_Office_StorageManager_StorageCheckList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>

<%@ Register src="../../../UserControl/GetBillExAttrControl.ascx" tagname="GetBillExAttrControl" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <style type="text/css">
        .fontBlod
        {
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .tboxsize
        {
            width: 90%;
            height: 99%;
        }
        .textAlign
        {
            text-align: center;
        }
    </style>
     <script type="text/javascript"> 
//定义回车事件 钱锋锋 2010-11-04
function document.onkeydown() 
{ 
    
    if(event.keyCode == 13)
    {
       TurnToPage(1);
        event.returnValue = false;
    } 
}
</script>
    <title>盘点单列表</title>

    <script src="../../../js/office/StorageManager/StorageCheckList.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="txtPara" value="" type="hidden" runat="server" />
          <input type="hidden"  id="txtPageIndex" />
          <input id="HiddenPoint" type="hidden" runat="server" />
    <input type="hidden" id="txtPageSize"  />
    <input type="hidden" id="txtOrderBy"  runat="server" />
    <input id="txtIsSearch" value="" type="hidden" />
    <input id="MoudleID" runat="server" type="hidden" />
    
    <uc1:Message ID="Message1" runat="server" />
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype"></span>
     <div id="divPageMask" style="display:none">
        <iframe  id="PageMaskIframe" frameborder="0" width="100%" ></iframe>
    </div>
    <table  width="98%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
        <%--<tr class="menutitle1">
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                </div>
                &nbsp;&nbsp;
            </td>
        </tr>--%>
        <tr class="menutitle1">
            <td align="left" valign="middle">
                &nbsp;&nbsp;<img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
            </td>  
            <td  align="right">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                </div>
                &nbsp;&nbsp;
            </td>    
              
         </tr>
        <tr>
            <td colspan="2">
                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="searchtable" >
                    <tr class="table-item">
                        <td height="25" align="right"  class="td_main_detail" width="10%">
                            盘点单编号
                        </td>
                        <td height="25" class="tdColInput" width="23%">
                            <input type="text" id="txtCheckNo" class="tdinput tboxsize" maxlength="50"  runat="server"/>
                        </td>
                        <td height="25" align="right"  class="td_main_detail" width="10%">
                            盘点单主题
                        </td>
                        <td height="25" class="tdColInput" width="23%">
                            <input id="txtTitle" type="text" runat="server" class="tdinput tboxsize" maxlength="50" runat="server" />
                        </td>
                        <td height="25" align="right"  class="td_main_detail" width="10%">
                            经办人
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="UserTransactor" onclick="alertdiv('UserTransactor,txtTransactor')"
                                readonly class="tdinput tboxsize" />
                            <input type="hidden" id="txtTransactor"  runat="server"/>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td height="25" align="right"  class="td_main_detail">
                            盘点部门
                        </td>
                        <td height="25" class="tdColInput">
                            <input type="text" id="DeptDept" class="tdinput tboxsize" readonly onclick="alertdiv('DeptDept,txtDeptID')" />
                            <input type="hidden" id="txtDeptID"  runat="server"/>
                        </td>
                        <td height="25" align="right"  class="td_main_detail">
                            盘点仓库
                        </td>
                        <td height="25" class="tdColInput">
                            <asp:DropDownList ID="ddlStorageID" CssClass="tddropdlist" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td height="25" align="right"  class="td_main_detail">
                            盘点类型
                        </td>
                        <td height="25" class="tdColInput">
                            <%--<select id="ddlCheckType">
                                <option value="-1">--请选择--</option>
                                <option value="1">调增</option>
                                <option value="0">调减</option>
                            </select>--%><asp:DropDownList ID="ddlCheckType" CssClass="tddropdlist" runat="server">
                            </asp:DropDownList>
                            
                            
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td height="25" align="right"  class="td_main_detail">
                            盘点时间
                        </td>
                        <td height="25" class="tdColInput">
                            <input type="text" id="txtStartDate" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartDate')})"
                                readonly class="tdinput tboxsize"  runat="server" />
                        </td>
                        <td height="25" align="right"  class="td_main_detail">
                            差异数量
                        </td>
                        <td height="25" class="tdColInput">
                            <input type="text" id="txtDiffCountStart" class="tdinput tboxsize" style="width: 80px"  runat="server" onchange="Number_round(this,2)"  />至<input
                                type="text" id="txtDiffCountEnd" class="tdinput tboxsize" style="width: 80px"  runat="server"  onchange="Number_round(this,2)" />
                        </td>
                        <td height="25" align="right"  class="td_main_detail">
                            单据状态
                        </td>
                        <td height="25" class="tdColInput">
                            <asp:DropDownList ID="ddlBillStatus" CssClass="tddropdlist" runat="server" >
                                <asp:ListItem Value="-1" Text="--请选择--"></asp:ListItem>
                                <asp:ListItem Text="制单" Value="1"></asp:ListItem>
                                <asp:ListItem Text="执行" Value="2"></asp:ListItem>
                                <asp:ListItem Text="手工结单" Value="4"></asp:ListItem>
                                <asp:ListItem Text="自动结单" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="table-item">
                        <td height="25" align="right"  class="td_main_detail">
                            审批状态
                        </td>
                        <td height="25" class="tdColInput">
                            <asp:DropDownList ID="ddlConfirmStatus" CssClass="tddropdlist" runat="server">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="0">待提交</asp:ListItem>
                                <asp:ListItem Value="1">待审批</asp:ListItem>
                                <asp:ListItem Value="2">审批中</asp:ListItem>
                                <asp:ListItem Value="3">审批通过</asp:ListItem>
                                <asp:ListItem Value="4">审批不通过</asp:ListItem>
                                <asp:ListItem Value="5">撤销审批</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td  class="td_main_detail" align="right">
                                    批次
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtBatchNo" class="tdinput" runat="server" type="text" size="15" specialworkcheck="批次" />
                                    </td>
                        <td height="25" align="right"  class="td_main_detail">
                       产品名称
                        </td>
                        <td height="25" class="tdColInput">
                           <input id="txtProductName" class="tdinput" runat="server" type="text" size="15" specialworkcheck="产品名称" />
                        </td>
                        
                    </tr>
                     <tr class="table-item">
                        <td height="25" align="right"  class="td_main_detail">
                            <span id="OtherConditon" style="display:none">其他条件</span>
                        </td>
                        <td height="25" class="tdColInput">
                            <uc2:GetBillExAttrControl ID="GetBillExAttrControl1" runat="server" />
                        </td>
                        <td  class="td_main_detail" align="right">
                                  
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                       
                                    </td>
                        <td height="25" align="right"  class="td_main_detail">
                       
                        </td>
                        <td height="25" class="tdColInput">
                           
                        </td>
                        
                    </tr>
                    <tr class="table-item">
                        <td colspan="6" align="center" bgcolor="#FFFFFF">
                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style="cursor: pointer;"
                                onclick="TurnToPage(1);" id="img_btn_search"   runat="server" visible="true" />
                            <%-- <img alt="重置" src="../../../images/Button/Bottom_btn_re.jpg" style='cursor: hand;'
                                            onclick="Fun_ClearInput()" width="52" height="23" id="imgReset"/>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <%--<td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>--%>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr class="menutitle1">
                        <td align="left" valign="middle">
                            期末盘点单列表
                        </td>
                    
                        <td align="right" valign="center">
                    
                            <img src="../../../images/Button/Bottom_btn_new.jpg" alt="新建" style='cursor:pointer;
                                ' onclick="createNew();"
                              id="img_btn_new" runat="server" visible="false" />
                            <img id="img_btn_del" src="../../../images/Button/Main_btn_delete.jpg" alt="删除" style='cursor: hand;
                                ' border="0" onclick="storageDelete();" runat="server" visible="false" />
                            <asp:ImageButton ID="imgOutput" runat="server" AlternateText="导出"  
                                ImageUrl="../../../images/Button/Main_btn_out.jpg" 
                                onclick="imgOutput_Click" style="height: 24px" 
                                 />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="98%" border="0" align="center" cellpadding="2" cellspacing="1" id="tblStoragelist"
                    bgcolor="#999999">
                    <tbody>
                        <tr class="table-item">
                            <th height="20" align="center" class="td_main_detail" >
                                选择<input type="checkbox" id="chk_StorageList" onclick="selectAll();" />
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CheckNo','CheckNo');return false;">
                                    盘点单编号<span id="CheckNo" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProductName','ProductName');return false;">
                                    产品名称<span id="ProductName" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Title','Title');return false;">
                                    盘点单主题<span id="Title" class="orderTip"></span></div>
                            </th>
                           <%-- <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('BatchNo','Title');return false;">
                                    批次<span id="Title" class="orderTip"></span></div>
                            </th>--%>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('DeptID','DeptID');return false;">
                                    部门<span id="DeptID" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('StorageID','StorageID');return false;">
                                    仓库<span id="StorageID" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Transactor','Transactor');return false;">
                                    经办人<span id="Transactor" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CheckStartDate','CheckStartDate');return false;">
                                    盘点开始日期<span id="CheckStartDate" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CheckType','CheckType');return false;">
                                    盘点类型<span id="CheckType" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('DiffCount','DiffCount');return false;">
                                    差异数量<span id="DiffCount" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('BillStatus','BillStatus');return false;">
                                    单据状态<span id="BillStatus" class="orderTip"></span></div>
                            </th>
                            <th align="center" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('FlowStatus','FlowStatus');return false;">
                                    审批状态<span id="FlowStatus" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
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
                                            <input name="text" type="text" id="ShowPageCount" onblur="checkPage('ShowPageCount',1)" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" onblur="checkPage('ToPage',2)" />
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
    </form>
</body>
</html>
