<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyWorkFlow.aspx.cs"   Inherits="Pages_jthy_PersonCenter_MyWorkFlow" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%--<%@ Register Src="../../../UserControl/CustLinkManSel.ascx" TagName="CustLinkManSel"
    TagPrefix="uc4" %>--%>
<%--<%@ Register Src="~/UserControl/CodeTypeDrpControl.ascx" TagName="CodeTypeDrpControl"
    TagPrefix="uc6" %>--%>
<%@ Register Src="~/UserControl/AddLimitArea.ascx" TagName="Limitarea" TagPrefix="uc7" %>
<%--<%@ Register src="~/UserControl/addCustLinkMan.ascx" tagname="addCustLinkMan" tagprefix="uc8" %>--%>
<%@ Register Src="../../../UserControl/FlowApply.ascx" TagName="FlowApply" TagPrefix="uc10" %>
<%--<%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel"
    TagPrefix="uc11" %>--%>
 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />

    <script src="../../../js/JQuery/jquery-1.4.4.min.js" type="text/javascript"></script>
    
    
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/Personal/Common.js" type="text/javascript"></script>

    <script src="../../../js/jthy/PersonCenter/MyWorkFlow.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
    
   <script src="../../../js/jthy/contractmanage/flowbuttoncontrol.js" type="text/javascript"></script>
      
 

 <script type="text/javascript">
  
     
    var glb_BillTypeFlag ="30";
    var glb_BillTypeCode ="1";
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID'; //自增长后的隐藏域ID
    var FlowJs_BillNo ='billNo';//当前单据编码名称
    var FlowJS_BillStatus ='hidBillStatus';    //单据状态ID
   
  
  </script>

  
  
    <title>待我审批的流程</title>
    <style type="text/css">
           .style1
          {
            background-color: #FFFFFF;
            width: 324px;
          }
        #BillType
        {
            width: 81px;
        }
        #BillFlag
        {
            width: 74px;
         }
        
    .menu{ margin-top:-10px; }
    .menu ul li a, .menu ul li a:visited {display:block;  text-decoration:none; color:#000;width:104px; height:19px; text-align:center; color:#fff; border:1px solid #fff; background:#008EFF; line-height:20px; font-size:11px; overflow:hidden;}
 .menu ul {padding:0; margin:0;list-style-type: none;}
 .menu ul li {float:left; margin-right:1px;}
 .menu ul li ul {display: none;}

 .menu ul li:hover a {color:#fff; background:#36f;}
 .menu ul li:hover ul {display:block; top:20px; left:0; width:105px;}
 .menu ul li:hover ul li a.hide {background:#6a3; color:#fff;}
 .menu ul li:hover ul li:hover a.hide {background:#6fc; color:#000;}
 .menu ul li:hover ul li ul {display: none;}
 .menu ul li:hover ul li a {display:block; background:#ddd; color:#000; }
 .menu ul li:hover ul li a:hover {background:#6fc; color:#000;}
 .menu ul li:hover ul li:hover ul {display:block; position:absolute; left:105px;}
 .menu ul li:hover ul li:hover ul.left {left:-105px; }
       
    </style>
</head>
<body>


    <form id="form1" runat="server">
<span id="Span1" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />    
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="hidSendStatus" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />
    <%--<uc11:CustNameSel ID="CustNameSel" runat="server" />--%>
     <input type="hidden" id="txtCreatorID" name="txtCreatorID" runat="server" />
     <input type="hidden" id="txtCreateDate" name="txtCreateDate" runat="server" />
     <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
     <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单"  runat="server" />
     <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" runat="server" />
     <input type="hidden" id="txtConfirmor" name="txtConfirmor" runat="server" />
     <input type="hidden" id="txtConfirmorReal" name="txtConfirmorReal" runat="server" />
     <input type="hidden" id="txtConfirmorDate" name="txtConfirmorDate" runat="server" />
     <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" runat="server" />
     <input type="hidden" id="txtModifiedDate" name="txtModifiedDate" runat="server" /> 
     <script src="../../../js/common/Flow.js" type="text/javascript"></script>
     <input type="hidden" id="getOrderNO" />
     <uc10:FlowApply ID="FlowApply1" runat="server" />

    <input type="hidden" id="isSearched" value="0" />

    
    <a name="pageDataList1Mark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="middle" align="center">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >

                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;检索条件
                        </td>
                        <td align="right" valign="middle" >
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
                            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                                
                                     
                      
                                <tr class="table-item">
                                    
                                    
                                    
                                    <td class="td_list_fields" >
                                        流程发起人
                                    </td>
                                    <td class="tdColInput">
                                        <asp:TextBox ID="UserApplyUserID" Style="width: 98%" ReadOnly="true" CssClass="tdinput"
                                            runat="server" onclick="alertdiv('UserApplyUserID,ApplyUser,2');" 
                                            Height="21px"></asp:TextBox>
                                        <input type="hidden" id="ApplyUser" runat="server" />
                                    </td>
                                     <td class="td_list_fields">
                                        单据编号
                                    </td>
                                    <td class="tdColInput"  colspan="3">
                                        <input name="billNo" id="billNo" type="text" class="tdinput" size="19" />
                                    </td>
                                    <td class="td_list_fields" align="right"  style=" display:none" >
                                        单据类型
                                    </td>
                                   <td bgcolor="#FFFFFF" width="300px" style=" display:none">
                                    <%--下拉框,列表选择现有 单据类型--%>
                                        <select id="BillFlag" onchange="setTypes(this)"> 
                                          
                                        </select>
                                        <select id="BillType">
                                        </select>
                                        
                                    </td>
                                   
                                </tr>
                                <tr class="table-item">
                                    <td  class="td_list_fields">
                                        审批状态
                                    </td>
                                    <td class="tdColInput">
                                        <select id="slFlowStatus" style="width: 120px;">
                                            <option value="-1" selected>——请选择——</option>
                                            <option value="1">未审核</option>
                                            <option value="2">审批中</option>
                                            <option value="3">审批通过</option>
                                            <option value="4">审批不通过</option>
                                            <option value="5">撤销审批</option>
                                        </select>
                                    </td>
                                    
                                    
                                    <td class="td_list_fields" >
                                        创建时间
                                    </td>
                                    <td class="tdColInput" colspan="3">
                                        <input onkeypress="return false;" name="createDate" id="createDate1" class="tdinput"
                                            type="text" size="10" onclick="WdatePicker()" />
                                        &nbsp;~&nbsp;<input onkeypress="return false;" name="createDate" id="createDate2" class="tdinput"
                                            type="text" size="10" onclick="WdatePicker()" />
                                    </td>
                                </tr>
                                <tr >
                                    <td colspan="6" class="td_main_detail">
                                        <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            onclick='SearchEquipData()'/>
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
            <td  colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1">
                        <td  align="left" valign="middle" >
                            &nbsp;&nbsp;待我审批的流程
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
                            
                              <th width="5%" class="td_main_detail" >
                        选择
                    </th>
                            
                            
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'billNo','oGroup');return false;">
                                    单据编号<span></span></div>
                            </th>
                            <th  width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="return false;">
                                    对应单据类型<span></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'FlowName','oC1');return false;">
                                    单据创建时间<span></span></div>
                            </th>
                            
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'ApplyUserId','oC4');return false;">
                                     状态<span></span></div>
                            </th>
                           <th width="5%"  class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'FlowStatus','oC2');return false;">
                                    流程发起人<span></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'ModifiedUserID','oC8');return false;">
                                   流程接收人<span></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'ModifiedUserID','oC8');return false;">
                                    操作<span></span>
                                    </div>
                            </th>
                        </tr>
                        <tr>
                        <td>
                     <%--   <div id='Header'><ul id='nav'><li class='nav'>操作<ul><li>查看</li><li>审批通过</li><li>审批退回</li></ul></li></ul></div>--%>
                        <span runat="server" id="GlbFlowButtonSpan" style="display:none"></span>
                        </td>
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
