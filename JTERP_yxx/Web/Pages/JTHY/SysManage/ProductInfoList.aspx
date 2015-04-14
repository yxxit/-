<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfoList.aspx.cs"
    Inherits="Pages_Office_SupplyChain_ProductInfoList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/GetBillExAttrControl.ascx" TagName="GetBillExAttrControl"
    TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>物品档案列表</title>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <link href="../../../css/jt_default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/jthy/sysmanage/ProductInfoList.js" type="text/javascript"></script>  

    <script type="text/javascript">
    
    //定义回车事件skg2010-11-04
    function document.onkeydown() 
    { 
        if(event.keyCode == 13)
        {
            Fun_Search_ProductInfo();
            event.returnValue = false;
        } 
    }
      function hidetxtUserList()
    {
        hideUserList();
        document.getElementById("txt_TypeID").value="";
          document.getElementById("txt_ID").value="";
    }
    
       function getChildNodes(nodeTable)
      {
            if(nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;  
           
            if(nodes.tagName == "DIV")
            {
                return nodes.childNodes;//return childnodes's nodeTables;
            }
            return [];
      }
        function showUserList()
        {
            var list = document.getElementById("userList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("txt_TypeID"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            document.getElementById("userList").style.display = "block";
        }
        
        
        function hideUserList()
        {
            document.getElementById("userList").style.display = "none";
        }
        function GetQuery()
        {
          //把扩展属性的值传给隐藏域
//            document.getElementById("HdselEFIndex").value = document.getElementById("GetBillExAttrControl1_SelExtValue").value;
//            document.getElementById("HdtxtEFDesc").value = document.getElementById("GetBillExAttrControl1_TxtExtValue").value;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <input id="HdselEFIndex" type="hidden" runat="server" />
    <input id="HdtxtEFDesc" type="hidden" runat="server" />
    <input id="HiddenBarCode" type="hidden" runat="server" />
    <input id="hidSearchCondition" type="hidden" runat="server" />
    <input id="hf_ID" type="hidden" />
    <asp:HiddenField ID="hidModuleID" runat="server" />
    <table width="98%"  border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="middle" align="center">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
        
                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;检索条件
                            <uc1:Message ID="Message2" runat="server" />
                        </td>
                        <td  align="right" valign="middle">
                            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td >
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    
                                    <td  class="td_list_fields" >
                                        物品编号
                                    </td>
                                    <td class="tdColInput">
                                        <input type="text" id="txt_ProdNo" specialworkcheck="物品编号" name="txtConfirmorReal0"
                                            class="tdinput" runat="server" /><input id="txt_ID" runat="server" type="hidden" />
                                    </td>
                                    <td class="td_list_fields" >
                                        物品名称
                                    </td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="txt_ProductName" specialworkcheck="物品名称" MaxLength="50" runat="server"
                                            CssClass="tdinput" Width="51%"></asp:TextBox>
                                    </td>
                                     <td class="td_list_fields" >
                                        &nbsp;启用状态
                                    </td>
                                    <td class="tdColInput">
                                        <select id="UsedStatus" runat="server" name="SetPro2" width="139px">
                                            <%--<option value="">--请选择--</option>--%>
                                            <option value="1" selected>启用</option>
                                            <option value="0">停用</option>
                                        </select>
                                    </td> 
                                </tr> 
                                <tr >
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'  onclick='Fun_Search_ProductInfo()' id="btnQuery" visible="false" runat="server" 
/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>               
                </table>
            </td>
        </tr>
        
    </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">


        <tr>
           <td height="30" colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1">
                        <td  align="left" valign="middle" >
                            &nbsp;&nbsp;物品档案列表
                        </td>
                        <td align="right" valign="middle">
                            <img alt="新建" src="../../../Images/Button/Bottom_btn_new.png" onclick="Show();" runat="server"
                                visible="false" id="btnNew" />
                            <img alt="" id="btnDel" runat="server" visible="false" src="../../../Images/Button/Main_btn_delete.png"
                                onclick="Fun_Delete_ProductInfo();" /><asp:ImageButton ID="btnImport" ImageUrl="../../../images/Button/Main_btn_out.jpg"
                                    AlternateText="导出Excel" runat="server" style="display:none;" OnClick="btnImport_Click" OnClientClick="GetQuery();" />
                            <%--<td align="right" style="width:94px"><img id="ImageButton1" runat="server" 
                                    src="../../../Images/Button/Bottom_btn_PYShort.jpg" alt="生成拼音缩写" style="cursor: hand;"
                                    onclick="CreatePYShort()" /></td>--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#CCCCCC">
                    <tbody>
                        <tr class="table-item">
                            <th width="3%" class="td_main_detail" >
                                选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAll();" />
                            </th>
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProdNo','oGroup');return false;">
                                    物品编号<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('ProductName','oC2');return false;">
                                    物品名称<span id="oC2" class="orderTip"></span></div>
                            </th>
                           
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('UnitName','oC5');return false;">
                                    基本单位<span id="oC5" class="orderTip"></span></div>
                            </th>
                            
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('Creator','Span7');return false;">
                                    建档人<span id="Span7" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail" >
                                <div class="orderClick" onclick="OrderBy('CreateDate','Span8');return false;">
                                    建档时间<span id="Span8" class="orderTip"></span></div>
                            </th>  
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
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
                                            <input name="text" type="hidden" id="Text2" />
                                            <span id="pageDataList1_Total"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
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
