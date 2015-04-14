<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurContract_Add.aspx.cs" Inherits="Pages_JTHY_ContractManage_PurContract_Add" %>
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
 
<%@ Register Src="../../../UserControl/ProviderInfo.ascx" TagName="ProviderInfo"
    TagPrefix="uc11" %>
    
   <%@ Register  Src="~/UserControl/ProductInfoSelect.ascx" TagName="ProductInfoSelect"
  TagPrefix="uc12" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
     <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
   <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"> </script>  
 <%--   <script src="../../../js/jthy/ContractManage/CheckDate.js" type="text/javascript"></script>
    <script src="../../../js/jthy/ContractManage/FlowButtonControl.js" type="text/javascript"></script>--%>
    <script src="../../../js/common/TreeView.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
     <script src="../../../js/jthy/ContractManage/AddContract_Pur.js" type="text/javascript"></script>
 <%--    <script src="../../../js/jthy/ContractManage/FlowButtonControl.js" type="text/javascript"></script>--%>
  <%--  <script src="../../../js/jthy/ContractManage/CheckDate.js" type="text/javascript"></script>--%>
   <script src="../../../js/FusionCharts/Contact.js" type="text/javascript"> </script>
     <script language="javascript" type='text/javascript'>
        function WindowShow(url,iWidth,iHeight)
        {
	        if (iWidth != null && iHeight != null)
            {
    	        window.open(url, "", "width=" + iWidth + ",height=" + iHeight + ",toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            else if (iWidth != null && iHeight == null)
            {
    	        window.open(url, "", "width=770" + ",height=" + iWidth + ",toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            else if (iWidth == null && iHeight == null)
            {
    	        window.open(url, "", "toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            return false;
        }
        
        function SaveJpgToServer()
        {
            // 
            var obj= document.getElementById("dealsaveJpg")
            if(obj!= null)
            {
                obj.click();
            }
        }
    </script>
    
    <script type="text/javascript">
   var glb_BillTypeFlag ="30";
    var glb_BillTypeCode ="2";
    var glb_BillID = 0;                                //单据ID
    var glb_IsComplete = false;                   //是否需要结单和取消结单(小写字母)
    var FlowJS_HiddenIdentityID ='hiddOrderID';                      //自增长后的隐藏域ID
    var FlowJs_BillNo ='txtContractID';          //当前单据编码名称
    var FlowJS_BillStatus ='hidBillStatus';                             //单据状态ID                            //单据状态ID
    
    </script>
      <script type="text/javascript" language="javascript">
      function show_child() 
        { 
        var iTop=(window.screen.height-600)/2; 
        var iLeft=(window.screen.width-400)/2; 
        var child=window.open("SelectProduceWare1.aspx","child","height=600,width=400,status=yes,toolbar=no,menubar=no,location=no,left="+iLeft); 
    
        }     
    </script>
    <script type="text/javascript">
  function showAreaSelect(id)
    {
//    var dom="#'"+id+"'";
     $("#areaSelect").fadeIn();
  // $(id).val("");
     $("#aeraId").val(id);
     //changeValue(id);
    }
function changeValue(id)
{
var v=$("#Select1");
//if(v.attr("value",'').selected==false)
v.change(function (){
  $(id).val(v.val());id="";

}) 
}
function add()
{
var LtoCustNo=$("#hfCustNo").val();
if(LtoCustNo=="")
{
  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择客户！");
}
else
{
//$("#LtoCustNo").val("0");

$("#addCLinkMan").show();
if(document.getElementById("txtUcLinkMan").value)
{
 $("#addLinkMan_RdIsDefault").attr("disabled","disabled");
 $("#addLinkMan_RdNotDefault").attr("checked","checked");
}
else
{
}
document.getElementById("addLinkMan_LtoCusNo").value=LtoCustNo;
}
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />    
    <input type="hidden" id="hiddtitlename" runat="server" value="0" />
    <input type="hidden" id="hiddOrderID" runat="server" value="0" />
    <input type="hidden" id="hidisCust" runat="server" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidBillStatus" runat="server" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input type="hidden" id="hidSendStatus" runat="server" />
    <input type="hidden" id="ThisID" runat="server" />     
     <input type="hidden" id="txtCreatorID" name="txtCreatorID" runat="server" />
     <input type="hidden" id="txtCreateDate" name="txtCreateDate" runat="server" />
     <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
     <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单"  runat="server" />
     <input type="hidden" id="txtConfirmorID" name="txtConfirmorID" runat="server" />
     <input type="hidden" id="txtConfirmorDate" name="txtConfirmorDate" runat="server" />
     <input type="hidden" id="txtModifiedUserID" name="txtModifiedUserID" runat="server" />
     <input type="hidden" id="txtModifiedDate" name="txtModifiedDate" runat="server" />
     <input type="hidden" id="getOrderNO" />

  <uc12:ProductInfoSelect ID="ProductInfoSelect" runat="server" />
  
  <uc10:FlowApply ID="FlowApply1" runat="server" />
  <uc11:ProviderInfo ID="ProviderInfo" runat="server" /> 
    <div>
                                            
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td  height="30" align="left" class="Title">
                                &nbsp;&nbsp;<asp:Label ID="labTitle_Write1" runat="server" Text="">新建采购合同</asp:Label>
                            
                            </td>
                            <td align="right" >
                                <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;" runat="server" onclick="SaveSellOrder();"  visible="false"  />
                                <img id="imgUnSave" runat="server" alt="保存" src="../../../Images/Button/UnClick_bc.jpg" style="display: none;"  visible="false"  runat="server"  />
                                     <input type="hidden" id="hidUpDateTime" runat="server" />
                                     <input id="headid" type="hidden" runat="server" />
                                     <span runat="server" id="GlbFlowButtonSpan"></span>
                                <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="审核生效" style="cursor: pointer;display: none;"
                                    runat="server" onclick="Fun_ConfirmOperate();"  visible="false" />
                               
                                <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法审核生效" runat="server"  visible="false" />

                                <img id="UnConfirm" alt="取消生效" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer;display: none;" 
                                    onclick="Fun_UnConfirmOperate();"  runat="server"  visible="false" />
                                <img id="ImgUnConfirm" alt="无法取消生效" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;"  runat="server"  visible="false" />
                                <img id="ImageClose" alt="终止"    src="../../../Images/Button/Bottom_ClickClose.png"  style="cursor: pointer; display:none;"  runat="server" 
                                    onclick="CloseConfim();" visible="false" />
                                <img id="ImageClose_btn" alt="无法终止" src="../../../Images/Button/btn_UnClose.png" style="cursor: pointer;"  runat="server"  visible="false" />
                                <img id="UnClose" alt="取消终止" src="../../../Images/Button/Btn_QuClose.png" style="cursor: pointer;
                                    display: none;" onclick="UncloseConfim();"  runat="server"  visible="false" />
                                <img id="UnClose_btn" alt="无法取消终止" src="../../../Images/Button/btn_UnQuClose.png" style="cursor: pointer;"  runat="server"   visible="false" />
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
                            <td height="20" class="td_list_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;基本信息
                                        </td>
                                        <td align="right">
                                            <div id='searchClick'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_01">
               
                        
                        <tr class="table-item">
                            <td  class="td_list_fields"  >合同号<span class="redbold">*</span></td>
                            <td class="td_list_edit" >
                                <div id="divCodeRule" runat="server">
                                    <uc3:CodingRuleControl ID="ddlContractID" runat="server" />
                                </div>
                                <asp:TextBox ID="txtContractID" runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                            </td>
                            <td   class="td_list_fields"  >
                                供应商名称<span class="redbold">*</span>
                            </td>
                            <td class="td_list_edit">
                                
                                <input id="opr_addcontract" type="hidden" runat="server" value="1" />  
                                <input id="txtProviderID" type="hidden" runat="server" />   
                                <asp:TextBox ID="txtProviderName"  runat="server" ReadOnly="true"  class="tdinput" style="width: 80%; "   onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');"  ></asp:TextBox>
                                <img alt="搜索" src="../../../Images/default/Search1.gif" style=" cursor:pointer;" onclick="popProviderObj.ShowProviderList('txtProviderID','txtProviderName',null,null,null,'0');" />
                                        
                            </td>
                            <td  class="td_list_fields" >
                                交货地点
                            </td>
                            <td class="td_list_edit" >
                                <asp:TextBox ID="txtDeliveryAddress" runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                            </td>
                         
                        
                        </tr>
                        <tr class="table-item">
                            <td  class="td_list_fields"  >签订日期</td>
                            <td class="td_list_edit"  >
                                            <asp:TextBox ID="txtSignDate" runat="server" class="tdinput" style="width: 80%;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSignDate')})" ReadOnly="true"></asp:TextBox>
                                            <img alt="搜索" src="../../../Images/datePicker.gif"   />
                            </td>
                            <td class="td_list_fields"  >生效日期</td>
                            <td class="td_list_edit"  >
                                <asp:TextBox ID="txtEffectiveDate" runat="server" class="tdinput" style="width: 80%;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEffectiveDate')})" ReadOnly="true">2014-01-01</asp:TextBox>
                                <img alt="搜索" src="../../../Images/datePicker.gif"   />
                            </td>
                                <td  class="td_list_fields"  >终止日期</td>
                            <td class="td_list_edit"  >
                                <asp:TextBox ID="txtEndDate" runat="server" class="tdinput" style="width: 80%;" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndDate')})" ReadOnly="true">2014-12-31</asp:TextBox>
                                <img alt="搜索" src="../../../Images/datePicker.gif" />
                            </td>
                                            
                        </tr>
                        
                        
                        <tr class="table-item">
                            <td   class="td_list_fields"  >结算方式</td>
                            <td class="td_list_edit"  >
                                <select name="drpSettleType" class="tddropdlist"   runat="server" id="drpSettleType"></select>
                            </td>
                            <td  class="td_list_fields"  >运输类型</td>
                            <td class="td_list_edit"  >
                                <select name="drpTransPortType" class="tddropdlist"  runat="server" id="drpTransPortType"></select>
                            </td>
                            <td class="td_list_fields" >合同金额</td>
                            <td class="td_list_edit"  >
                                <asp:TextBox ID="txtContractMoney" runat="server" Enabled="false"  Text="0.00" class="tdinput" style="width: 95%;"></asp:TextBox>
                            </td>                                            
                        </tr>
                        <tr class="table-item">
                            <td  class="td_list_fields"  >经办人</td>
                            <td class="td_list_edit"  >
                                 <input id="txtPPersonID" type="hidden" runat="server" /> 
                                 <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="true"  class="tdinput" style="width: 80%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
                                 <img alt="搜索" src="../../../Images/default/Search1.gif"  style=" cursor:pointer;" onclick="alertdiv('txtPPerson')" />
                            </td>
                            <td class="td_list_fields"  >部门</td>
                            <td class="td_list_edit"  >
                                <input id="hdDeptID" type="hidden" runat="server" />
                                 <input id="DeptName" runat="server" readonly="readonly"  type="text"  style="width:80%; display:inline;" class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" />
                                 <img alt="搜索" src="../../../Images/default/Search1.gif" style=" cursor:pointer;" onclick="alertdiv('DeptName,hdDeptID')" />
                            </td>
                                <td  class="td_list_fields"  >合同类型</td>
                            <td class="td_list_edit"  >
                                <asp:TextBox ID="txtContractType" runat="server"  Enabled="false" class="tdinput" style="width: 95%;" value="采购合同" ></asp:TextBox>
                            </td>
                                            
                        </tr>
                        <tr class="table-item">
                        <td  align="right" class="td_list_fields">备注信息</td>
                    
                        <td bgcolor="#FFFFFF" colspan="5"  >
                            <textarea id="txtRemark" cols="20" rows="2" style="width:99%"></textarea>
                        </td>
                        </tr>
                      
                        
                     
                        
                    </table>
                   
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                        <tr>
                            <td class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr class="menutitle1">
                                    
                                        <td align="left">
                                            &nbsp;&nbsp;合同明细
                                        </td>
                                        <td align="right">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ','searchClick3')" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="TableBJ">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td  bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                            <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="imgAdd"
                                                style="cursor:hand;" onclick="AddShows();" />
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="imgDel"
                                                style="cursor:hand;" onclick="fnDelOneRow();" />
                                        </td>
                                    </tr>
                                </table>
                                <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0"
                                        cellspacing="1" bgcolor="#999999">
                                        <tr class="table-item">
                                            <td  class="td_main_detail" style="width: 6%;">
                                                选择<input type="checkbox" visible="false" id="checkall" onclick="fnSelectAll()" value="checkbox" />
                                            </td>
                                            <td class="td_main_detail" style="width: 15%;">
                                               煤种<span class="redbold">*</span>
                                            </td>
                                           <%-- <td class="td_main_detail" style="width: 15%;">
                                                质量(热卡)
                                            </td>   --%>                                       
                                            <td class="td_main_detail" style="width: 15%;">
                                                计量单位 
                                            </td>                                           
                                            <td  class="td_main_detail" style="width: 15%;">
                                                数量<span class="redbold">*</span>
                                            </td>
                                            <td  class="td_main_detail" style="width: 15%;">
                                               单价
                                            </td>
                                            <td class="td_main_detail" style="width: 15%;">
                                              金额
                                            </td>                                            
                                        </tr>                                          
                                    </table>                          
                                </div>
                            </td>
                        </tr>
                    </table>
                    
                    
                    
                    
                    
                     <br />
                     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                        <td  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left">
                                        &nbsp;&nbsp;附件信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick5'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_05','searchClick5')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_05">
                     <tr>
                        <td align="left" bgcolor="#FFFFFF" colspan="8" class="style1">
                            <div id="divUploadAttachment" runat="server" width="100%" style="margin:0px; padding:0px;">
                              <input id="hideCustID" type="hidden" name="hideCustID"/>
                              <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>                               
                     
                              <a href="#" onclick="DealAttachment('upload');">上传附件</a> 
                          
                            </div>
                            <div id="divDealAttachment" runat="server" style="display: none;margin:0px; padding:0px" width="100%" >
                                <span id='spanAttachmentName' runat="server" style="margin:0px; padding:0px;" >
                                </span>
                                <br/>
                                <a href="#" onclick="DealAttachment('upload');">上传附件</a>
                            </div>
                        </td>                         
                    </tr>
                </table>         
                <asp:HiddenField ID="hfAttachment" runat="server" />
                <asp:HiddenField ID="hfPageAttachment" runat="server" />                    
                    <!-- End 默认信息 -->
                </td>
            </tr>
        </table>
    </div>
    
    
        <br />
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
        <tr>
            <td height="20" class="td_list_title">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr class="menutitle1">
                        <td align="left">
                            &nbsp;&nbsp;附加信息
                        </td>
                        <td align="right">
                            <div id='divButtonNote'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','divButtonNote')" /></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="Tb_04">
        <tr class="table-item">
            <td class="td_list_fields">
                建档日期
            </td>
            <td class="tdColInput">
                <asp:TextBox ID="txt_CreateDate" Enabled="false" runat="server" CssClass="tdinput"
                    Width="80%"></asp:TextBox>
            </td>
            <td class="td_list_fields">
                建档人
            </td>
            <td class="tdColInput">
                &nbsp;<asp:TextBox ID="UserPrincipal" Enabled="false" runat="server" CssClass="tdinput"
                    ReadOnly="true" Width="90%"></asp:TextBox>
            </td>
            <td class="td_list_fields">
                确认人
            </td>
            <td class="tdColInput" width="23%">
                <input id="Text1" name="txtConfirmorId" style="widows: 95%; border: 0px;
                    display: none;" />
                <input id="txtConfirmor" name="txtConfirmor" disabled="disabled" style="widows: 95%;
                    border: 0px;" />
            </td>
        </tr>
        <tr class="table-item">
            <td class="td_list_fields">
                确认日期
            </td>
            <td class="tdColInput" width="23%">
                <input id="txtConfirmDate" name="txtConfirmDate" disabled="disabled" style="widows: 95%;
                    border: 0px;" />
            </td>
            <td class="td_list_fields">
                最后更新日期
            </td>
            <td class="tdColInput">
                <asp:TextBox ID="txtModifiedDates" Enabled="false" MaxLength="50" runat="server" CssClass="tdinput"
                    Width="95%" Text=""></asp:TextBox>
            </td>
            <td class="td_list_fields">
                最后更新用户ID
            </td>
            <td class="tdColInput">
                <asp:TextBox ID="txtModifiedUserIDs" Enabled="false" MaxLength="50" runat="server"
                    CssClass="tdinput" Width="93%" Text=""></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
