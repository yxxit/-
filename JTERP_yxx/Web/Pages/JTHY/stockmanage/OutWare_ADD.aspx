<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutWare_ADD.aspx.cs" Inherits="Pages_JTHY_StockManage_OutWare_ADD" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%--<%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel"
    TagPrefix="uc2" %>--%>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/jthy/OutBusinessInfo.ascx" TagName="OutBusinessInfo"
    TagPrefix="uc4" %>
<%@ Register Src="../../../UserControl/jthy/TranSportInfo.ascx" TagName="TranSportInfo"
    TagPrefix="uc5" %>
<%@ Register Src="../../../UserControl/jthy/WeighInfo.ascx" TagName="WeighInfo"
    TagPrefix="uc6" %>
    <%@ Register Src="../../../UserControl/CustNameSel_Con.ascx" TagName="CustNameSel_Con"
    TagPrefix="uc11" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" language="javascript" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>
    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/page.js" type="text/javascript"></script>
    <script src="../../../js/jthy/stockmanage/OutWare_ADD.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>    
    </head>
<body>
    <form id="form1" runat="server">
    <uc1:Message ID="Message1" runat="server" />  
    <uc11:CustNameSel_Con ID="CustNameSel_Con" runat="server" /> 

    <%--<uc2:CustNameSel ID="CustNameSel" runat="server" />   --%>  
    <uc4:OutBusinessInfo ID="OutBusinessInfo" runat="server" /> 
    <uc5:TranSportInfo ID="TranSportInfo" runat="server" />   
    <uc6:WeighInfo ID="WeighInfo" runat="server" /> 
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="getOrderNO" />
    <input type='hidden' id='txtTRLastIndex' value="0" />
    <input type="hidden" id="hidStatus" runat="server" />
    <input id="headid" type="hidden" runat="server" />
    <input type="hidden" id="txtBillStatusID" name="txtBillStatusID" value="1" runat="server" />
    <input type="hidden" id="txtBillStatusName" name="txtBillStatusName" value="制单"  runat="server" />
    <div id="orderNo1"></div>
    

    <div>                                            
        <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">            
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="left" class="Title">&nbsp;&nbsp;
                                <asp:Label ID="labTitle_Write1" runat="server" Text="">销售出库单</asp:Label>                            
                            </td>
                            <td align="right">  
                                <img id="imgSave" src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" style="cursor: pointer;"
                                    runat="server" onclick="SaveSellOrder();" />
                                <img id="imgUnSave" runat="server" alt="保存" src="../../../Images/Button/UnClick_bc.jpg"
                                    style="display: none;" />
                                    
                                <img id="btn_confirm" src="../../../Images/Button/Bottom_btn_ok.jpg" alt="审核生效" style="cursor: pointer; display:none;"
                                    runat="server" onclick="Fun_ConfirmOperate();" />
                                <img id="Imgbtn_confirm" src="../../../Images/Button/Bottom_btn_confirm2.jpg" alt="无法生效" 
                                    runat="server" />
                                   
                                <img id="UnConfirm" alt="取消生效" src="../../../Images/Button/btn_fqr.jpg" style="cursor: pointer; display: none;" 
                                        onclick="cancelConfirm();"  />
                                <img id="ImgUnConfirm" alt="无法取消生效" src="../../../Images/Button/btn_fqru.jpg" style="cursor: pointer;" />
                                    
                                <input type="hidden" id="hidUpDateTime" runat="server" />
                                
                                <span runat="server" id="GlbFlowButtonSpan"></span>                                
                                <input id="txtOprtID" type="text" style="display: none;" />
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
                                        <td align="left" valign="middle">  
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
                            <td class="td_list_fields"  >出库单编号</td>
                            <td  class="td_list_edit"  >
                              <div id="divCodeRule" runat="server">
                                <uc3:CodingRuleControl ID="ddlOutWareID" runat="server" />
                              </div>  
                                </td>
                            <td   class="td_list_fields"  >来源销货单<span class="redbold">*</span></td>
                            <td   class="td_list_edit" >                          
                                <input type="hidden" id="txtSourceBillID" value="" runat="server"  />                       
                                <asp:TextBox ID="txtSourceBillNo" runat="server" class="tdinput"  style="width:80%;"  onclick="fnSelectOutBusiness()" ></asp:TextBox>
                                <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="fnSelectOutBusiness()" />
                            </td>
                             <td  class="td_list_fields" >结算方式</td>
                            <td  class="td_list_edit" >                        
                               <select name="drpSettleType" class="tddropdlist" runat="server" id="drpSettleType">       
                                 </select>  
                            </td>                         
                        
                        </tr>
                         <tr class="table-item">
                            <td  class="td_list_fields"  >客户名称</td>
                            <td    class="td_list_edit" >
                                  <input id="opr_addoutbus" type="hidden" runat="server" value="" />  
                                 <input id="txtCustomerID" type="hidden" runat="server" />   
                                 <asp:TextBox ID="txtCustomerName" runat="server" class="tdinput" style="width: 90%;" onclick="SearchCustData();"></asp:TextBox>
                                 <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="SearchCustData();" />
                            </td>
                            <td   align="right" class="td_list_fields"  style="width:10%">开票单位</td>
                            <td    class="td_list_edit" >
                              <asp:TextBox ID="txtInvoiceUnit" runat="server" Enabled="false" class="tdinput" style="width: 95%;"  ></asp:TextBox>
                            </td>
                             <td  align="right" class="td_list_fields"  style="width:10%">调运类型</td>
                            
                            <td  class="td_list_edit">
                                <select name="drpTransPortType" class="tddropdlist"  runat="server" id="drpTransPortType"></select>
                            </td>
                                                
                        </tr>
                        <tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">出库人<span class="redbold">*</span></td>
                        <td     class="td_list_edit">
                            <input id="txtOprID" type="hidden" runat="server" /> 
                            <asp:TextBox ID="txtOprName" runat="server" class="tdinput" style="width: 80%;"   onclick="alertdiv('txtOprName')"></asp:TextBox>  
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtOprName')" />
                        </td>
                        <td   align="right" class="td_list_fields"  style="width:10%">出库时间</td>
                        <td    class="td_list_edit" >
                        <asp:TextBox ID="txtOutWareTime" runat="server" class="tdinput" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtOutWareTime')})" ReadOnly  style="width: 80%;"   ></asp:TextBox>
                        <img alt="搜索" src="../../../Images/datePicker.gif"   />
                        </td>
                         <td  align="right" class="td_list_fields"  style="width:10%">出库总数量</td>
                        <td    class="td_list_edit" >
                        <asp:TextBox ID="txtOutNum" runat="server"  Enabled="false" onkeyup='return ValidateNumber(this,value)'  class="tdinput" style="width: 95%;"  ></asp:TextBox>    
                        </td>
                        </tr>                     
                         <tr class="table-item">
                        <td  align="right" class="td_list_fields"  style="width:10%">业务员</td>
                        <td    class="td_list_edit" >
                               <input id="txtPPersonID" type="hidden" runat="server" /> 
                               <asp:TextBox ID="txtPPerson" runat="server" ReadOnly="true"  class="tdinput" style="width: 80%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
                               <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('txtPPerson')" />
                        </td>
                        <td   align="right" class="td_list_fields"  style="width:10%">部门</td>
                        <td    class="td_list_edit" >
                            <input id="DeptName" runat="server" readonly="readonly"  type="text"  style="width: 80%;" class="tdinput" onclick="alertdiv('DeptName,hdDeptID')" />
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('DeptName,hdDeptID')" />
                             <input id="hdDeptID" type="hidden" runat="server" />
                        </td>
                         <td  align="right" class="td_list_fields"  style="width:10%">发运数量</td>
                        <td  class="td_list_edit">
                             <asp:TextBox ID="txtSendNum" runat="server" Enabled="false" class="tdinput" style="width: 95%;"  ></asp:TextBox>
                        </td>                                            
                        </tr>       
                        
                         <tr class="table-item">
                         <td  align="right" class="td_list_fields"  style="width:10%">运费</td>
                         <td >
                            <asp:TextBox ID="txtTransMoney" runat="server" class="tdinput" style="width: 95%;"  ></asp:TextBox>
                         </td>
                        <td  align="right" class="td_list_fields"  style="width:10%">备注</td>
                        <td colspan="3">    
                            <asp:TextBox ID="txtRemark" runat="server" class="tdinput" style="width: 95%;"  ></asp:TextBox>                    
                        </td>           
                        </tr>                         
                    </table>         
                    
                     <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">                        
                        <tr>
                            <td height="20px" class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr  class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;煤种信息
                                        </td>
                                        <td align="right">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ','searchClick3')" /></div>
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
                                <%--<table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr>
                                        <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                                           <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="imgAdd"
                                                style="cursor:hand;" onclick="AddSignRow();" />
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="imgDel"
                                                style="cursor:hand;" onclick="fnDelOneRow();" />
                                        </td>
                                    </tr>
                                </table>--%>                                 
                                <div id="div2" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="TableCoalInfo" style="height: auto;" align="center" cellpadding="0"
                                        cellspacing="1" bgcolor="#999999">
                                        <tr  class="table-item">
                                            <td class="td_main_detail" style="width: 6%; display:none;">
                                                选择<input type="checkbox"  id="Checkbox2" onclick="fnSelectAll()" value="checkbox" />
                                            </td>
                                            <td class="td_main_detail" style="width: 10%;" >
                                                仓库
                                            </td>
                                            
                                            <td class="td_main_detail" style="width: 10%;" >
                                                煤种名称</td>
                                            <td class="td_main_detail" id="colNo"  >
                                                计量单位</td>
                                            <td class="td_main_detail" width="8%" >
                                                数量<span class="redbold">*</span>
                                            </td>                                            
                                            <td class="td_main_detail" width="9%"  >
                                                销售单价</td> 
                                             <td class="td_main_detail" width="7%"  >
                                                税率(%)</td>       
                                            <td class="td_main_detail"   width="9%" >
                                                税额</td> 
                                            <td class="td_main_detail"  width="10%"  >
                                                金额</td> 
                                           <td class="td_main_detail"  width="9%"  >
                                                发货总数量</td>
                                           <td class="td_main_detail"  width="9%">
                                                未出库数量</td>
                                        </tr>
                                         <%--<tr  class="table-item">
                                            <td>
                                                <select name="drpWare" class="tddropdlist"  runat="server" id="drpWare" style="width:95%"></select>  
                                            </td>
                                            <td>
                                                  <asp:TextBox ID="txtCoalID" ReadOnly runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>
                                             <td>
                                                   <asp:TextBox ID="txtCoalName" ReadOnly runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>
                                            
                                            <td>
                                                   <asp:TextBox ID="txtQuantity"  runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                  <asp:TextBox ID="txtSaleCost"  runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td> 
                                            <td>
                                                  <asp:TextBox ID="txtTaxRate"  runat="server" class="tdinput" style="width: 95%;" Text="17" ></asp:TextBox>
                                            </td>                                         
                                            <td>
                                                
                                                 <asp:TextBox ID="txtTax"  runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>
                                           
                                            <td>
                                                 <asp:TextBox ID="txtTaxMoney"  runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtComOutNum" ReadOnly runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>                                            
                                             <td>
                                                <asp:TextBox ID="txtComSettleNum" ReadOnly runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>
                                            </td>    
                                        </tr>--%>
                                    </table>
                          
                                </div>
                            </td>
                        </tr>
                    </table> 
                    
                    <br />
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">                        
                        <tr>
                            <td height="20px" class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr  class="menutitle1">
                                        <td align="left">
                                            &nbsp;&nbsp;调运详细
                                        </td>
                                        <td align="right">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/close.jpg" style="cursor: pointer" onclick="oprItem('TableBJ1','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999"
                        id="TableBJ1">
                        <tr>
                            <td>   
                                <table width="100%" border="0" style="display:none;" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                                    <tr class="table-item">
                                        <td style="padding-top: 5px; padding-left: 5px;">
                                            <img runat="server" src="../../../images/Button/Show_add.jpg" alt="添加" id="img1"
                                                style="cursor:hand; display:none;" onclick="fnSelTranSport();" />
                                            <img runat="server" src="../../../images/Button/Show_del.jpg" alt="删除" id="img2"
                                                style="cursor:hand;display:none;" onclick="JTClear_TranSport();" />
                                             <a href="#" style="display:none;" onclick="fnAddWeigh()">添加过磅信息</a>
                                        </td>
                                    </tr>
                                </table>                              
                                <div id="divDetail" style="width: 100%; background-color: #FFFFFF;">
                                    <table width="100%" border="0" id="dg_Log" style="height: auto;" align="center" cellpadding="0"
                                        cellspacing="1" bgcolor="#999999">
                                        <tr  class="table-item">                                        
                                            <td class="td_main_detail" style="width: 20%;">
                                                调运单号
                                            </td>
                                            <td class="td_main_detail"  style="width: 13%;" >
                                                当前状态</td>
                                            <td class="td_main_detail"   style="width: 13%;">
                                                车次</td>
                                            <td class="td_main_detail"   style="width: 13%;">
                                                发站
                                            </td>                                          
                                            <td class="td_main_detail"   style="width: 13%;">
                                                到站</td>                                          
                                            
                                            <td class="td_main_detail"   style="width: 13%;">
                                                发车数</td> 
                                           <%--<td class="td_main_detail"   style="width: 13%;">
                                                <a title="包括 更改状态，">操作</a></td>--%>
                                        </tr>
                                         <tr  class="table-item">
                                            <td class="td_main_detail" style="width: 15%;">
                                                  <input type="hidden" id="txtTranSportID" value="" runat="server"  />
                                                  <asp:TextBox ID="txtTranSportNo"  Enabled="false" runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>   
                                            </td>
                                            <td class="td_main_detail" style="width: 15%;">
                                                   <asp:TextBox ID="txtTranSportState"  Enabled="false" runat="server" class="tdinput" style="width: 95%;"></asp:TextBox>  
                                            </td>
                                            <td class="td_main_detail" style="width: 15%;">
                                              <asp:TextBox ID="txtCarNo"  runat="server"  Enabled="false" class="tdinput" style="width: 95%;"></asp:TextBox>  
                                            </td>                                          
                                            <td class="td_main_detail" style="width: 15%;">
                                                
                                                <asp:TextBox ID="txtStartStation"  runat="server"  Enabled="false" class="tdinput" style="width: 95%;"></asp:TextBox> 
                                            </td>
                                           
                                            <td class="td_main_detail" style="width: 15%;">
                                                 <asp:TextBox ID="txtEndStation"  runat="server"  Enabled="false" class="tdinput" style="width: 95%;"></asp:TextBox>  
                                            </td>
                                            <td class="td_main_detail" style="width: 15%;">
                                            <asp:TextBox ID="txtCarNum" runat="server"  Enabled="false" class="tdinput"  style="width: 95%;"   ></asp:TextBox>
                                            </td>                                            
                                            <%--<td class="td_main_detail" style="width: 15%;">
                                                <asp:DropDownList ID="drpUPTranSportState" runat="server">
                                                  <asp:ListItem Value="10" Text="未生效"></asp:ListItem>
                                                  <asp:ListItem Value="20" Text="装车"  ></asp:ListItem>
                                                  <asp:ListItem Value="30" Text="发货" Selected="True"></asp:ListItem>
                                                  <asp:ListItem Value="40" Text="在途"></asp:ListItem>
                                                  <asp:ListItem Value="50" Text="到货"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>--%>  
                                        </tr>
                                    </table>
                          
                                </div>
                            </td>
                        </tr>
                    </table> 
                  <br />                         
                 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr  class="menutitle1">
                                    <td align="left" valign="middle">  
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
                    <tr  class="table-item">
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            建档日期
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            建档人 
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            &nbsp;<asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="90%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            确认人
                        </td>
                        <td height="20" class="tdColInput" width="23%"> 
                            <input id="txtConfirmorId" name="txtConfirmorId" style="widows:95%; border:0px; display:none;" />
                            <input id="txtConfirmor" name="txtConfirmor" disabled="disabled" style="widows:95%; border:0px;" />                          
                        </td>
                    </tr>                   
                    <tr  class="table-item">
                       <td height="20" align="right"  class="td_list_fields" width="10%">
                            确认日期
                        </td>
                        <td height="20" class="tdColInput" width="23%"> 
                            <input id="txtConfirmDate" name="txtConfirmDate" disabled="disabled" style="widows:95%; border:0px;" />                          
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            最后更新日期
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            最后更新用户ID
                        </td>
                        <td height="20" class="tdColInput" >
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="93%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>
                
                    <!-- End 默认信息 -->
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
