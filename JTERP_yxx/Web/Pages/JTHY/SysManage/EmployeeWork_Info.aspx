<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeWork_Info.aspx.cs" Inherits="Pages_Office_HumanManager_EmployeeWork_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeType" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在职人员列表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/jthy/sysmanage/EmployeeWork_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script type="text/javascript"> 
//定义回车事件 钱锋锋 2010-11-04
function document.onkeydown() 
{ 
    
    if(event.keyCode == 13)
    {
        SearchEmployeeWork();
        event.returnValue = false;
    } 
}
</script>
</head>
<body>
<form id="frmMain" runat="server">
    <input type="hidden" id="hidModuleID" runat="server" />
    <input type="hidden" id="hidSysteDate" runat="server" />
    <table width="98%"border="0" cellpadding="0" cellspacing="0"  id="mainindex">
        <tr>
        
            <td valign="middle" align="center">
                 <table width="99%" border="0" cellpadding="0" cellspacing="0"  >
                    <tr class="menutitle1" >
                        <td align="left" valign="middle">  
                            &nbsp;&nbsp;检索条件            
                        </td>
                        <td align="right" valign="middle"  >
                            <div id='divSearch'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer"  onclick="oprItem('tblSearch','divSearch')"/>
                            </div>
                        </td>
                    </tr>
                </table>
           </td>
                        
        </tr>
      
            
    
        <tr>
            <td >

                <table width="99%" border="0" align="center" cellpadding="0" id="tblSearch"  cellspacing="0" bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                                <tr class="table-item">
                                    <td  class="td_list_fields" >姓名</td>
                                    <td  class="tdColInput">
                                        <input name="txtEmployeeName" runat="server" id="txtEmployeeName" type="text" class="tdinput" size = "13" SpecialWorkCheck="姓名" />
                                    </td>
                                    <td   class="td_list_fields"  >手机号码</td>
                                    <td  class="tdColInput">
                                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="50" SpecialWorkCheck="手机号码" CssClass="tdinput" Width="95%"></asp:TextBox>
                                    </td>
                                    <td  class="td_list_fields">拼音缩写</td>
                                    <td  class="tdColInput">
                                        <input name="txtCardID" id="txtPYShort" runat="server" type="text" class="tdinput" size = "19" SpecialWorkCheck="拼音缩写" />
                                    </td>
                                </tr>                                                  
                                <tr >
                                    <td colspan="6" align="center" bgcolor="#FFFFFF">
                                        <uc1:Message ID="Message1" runat="server" />
                                        <input type="hidden" id="hidSearchCondition" name="hidSearchCondition" />
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" visible="false" runat="server" style='cursor:pointer;' onclick='SearchEmployeeWork()'/>                                        
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 </table>
             </td>
         </tr>
    </table>
    <table width="98%"  border="0" cellpadding="0" cellspacing="0"
    id="mainindex">

        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >                   
                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;在职人员列表</td>
                        <td  align="right" valign="middle" >                            
                            <img src="../../../Images/Button/Bottom_btn_new.png" alt="新建" visible="false" id="btnNew" runat="server" style="cursor:hand" onclick="DoNew();" />&nbsp;
                            <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/Images/Button/Main_btn_out.jpg" style="display:none;" onclick="btnImport_Click" />
                            &nbsp;
                        </td>
                    </tr>
                </table>            
            </td>
        </tr>
                    
        <tr>
            <td colspan="2"  >
                <%--<div style="height:252px;overflow-y:scroll;">--%>
                <table width="99%" border="0"  align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#CCCCCC">
                    <tbody>
                        <tr class="table-item"  >                                       
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('EmployeeNo','oC0');return false;">
                                    编号<span id="oC0" class="orderTip"></span>
                                </div>
                            </th>
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('EmployeeNum','oC1');return false;">
                                    工号<span id="oC1" class="orderTip"></span>
                                </div>
                            </th>
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('PYShort','oC2');return false;">
                                    拼音缩写<span id="oC2" class="orderTip"></span>
                                </div>
                            </th>
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('EmployeeName','oC3');return false;">
                                    姓名<span id="oC3" class="orderTip"></span>
                                </div>
                            </th>                                        
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('DeptName','oC5');return false;">
                                    部门<span id="oC5" class="orderTip"></span>
                                </div>
                            </th>
                           
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('QuarterName','oC7');return false;">
                                    岗位<span id="oC7" class="orderTip"></span>
                                </div>
                            </th>                                         
                             <th class="td_main_detail">
                                <div class="orderClick" style="width:95%" onclick="OrderBy('EntryDate','oC9');return false;">
                                    入职时间<span id="oC9" class="orderTip"></span>
                                </div>
                            </th>                                        
                        </tr>
                    </tbody>
                </table>                           
                <br/>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
                    <tr>
                        <td height="28"   background="../../../images/Main/PageList_bg.jpg" >
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  >
                                        <div id="pagecount"></div>
                                    </td>
                                    <td height="28"  align="right">
                                        <div id="divPageClickInfo" class="jPagerBar"></div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divPage">
                                            每页显示<input name="txtShowPageCount" type="text" id="txtShowPageCount"size="3" maxlength="4" />条&nbsp;&nbsp;
                                            转到第<input name="txtToPage" type="text" id="txtToPage" maxlength="4" size="3"/>页&nbsp;&nbsp;
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#txtShowPageCount').val(),$('#txtToPage').val());" />
                                        </div>
                                     </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br/>
            </td>
        </tr>
                
    </table>
    <a name="DetailListMark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
    <input id="hiddExpOrder" type="hidden" runat="server" />
</form>
</body>
</html>
