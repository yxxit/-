<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayBill_Add.aspx.cs" Inherits="Pages_JTHY_Expenses_PayBill_Add" %>

<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/Common/ProjectSelectControl.ascx" tagname="ProjectSelectControl" tagprefix="uc13" %>
<%@ Register src="../../../UserControl/CustOrProvider.ascx" tagname="CustOrProvider" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建付款单</title>
    <%--<link href="../../../css/default.css" rel="stylesheet" type="text/css" />--%>

<link href="../../../css/jt_default.css" type="text/css" rel="stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    
    
    
    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/JTHY/Expenses/CurryType.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/JTHY/Expenses/PayBill_Add.js" type="text/javascript"></script>
    <style type="text/css">
        #txtJTRemark
        {
            height: 46px;
            width: 746px;
        }
        #txtRemark
        {
            height: 19px;
            width: 124px;
        }
        #Text9
        {
            width: 620px;
            height: 22px;
        }
        #txtSellArea1
        {
            height: 46px;
            width: 746px;
        }
        #txtBankName
        {
            width: 205px;
        }
        .style1
        {
            width: 23%;
        }
    </style>

    <script type="text/javascript" language="javascript">
     //对输入的数字自动保留两位小数，四舍五入
        function Numb_round(numberStr,fractionDigits)
        {   
            var num=numberStr.toString().split('.');
            if(num[1]!=""&&num[1]!=null&&parseInt(num[1])==0)
            {
                 return numberStr;
            }
            else
            {
                if(numberStr!=parseInt(numberStr))
                {
                      with(Math)
                      {   
                        return round(numberStr*pow(10,fractionDigits))/pow(10,fractionDigits);
                      }   
                }
                else
                {
                       return numberStr+".00";
                }
            }
        }   
    </script>

</head>
<body onload="AcceWay();">
    <form id="Form1" runat="server">
    <input type="hidden" id="txtPrincipal" runat="server" />
    <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
    <input type="hidden" id="islistcome" runat="server" />
    <input type="hidden" id="headid" runat="server" />
    <input type="hidden" id="hiddOrderID" runat="server" />
    <input type="hidden" id="Confirm" runat="server" value="0" />
    <input type="hidden" id="Confirmname" runat="server" />
    <input type="hidden" id="jiandangren" runat="server" />
    <input type="hidden" id="jiandangren2" runat="server" />
    <uc3:CustOrProvider ID="CustOrProvider1" runat="server" />
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <%--<tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>--%>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            <div id="divTitle" runat="server">                                新建付款单</div>
                        </td>
                    </tr>
                </table>--%>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <!-- Start 单据状态值 -->
                            <table width="100%">
                                <tr>
                                    <td height="30" align="left" class="Title">
                                        <div id="divTitle" runat="server">                                新建付款单</div>
                                    </td>
                                    <td align="right">
                                    <input type="hidden" id="price" runat="server" />
                                    <input type="hidden" id="billingid" runat="server" />
                                        <img id="btnIncomeBill_Save" src="../../../images/Button/Bottom_btn_save.jpg" onclick="InsertIBI();"
                                            runat="server" visible="true" style="cursor: pointer" title="保存付款单" />
                                            &nbsp;<span id="GlbFlowButtonSpan"></span>
                                         <img src="../../../images/Button/Bottom_btn_confirm.jpg" alt="确认"  id="btnConfirm"    runat="server"  visible=true  style='cursor:pointer;'   onclick="ConfirmIncomeBill();"/>
                                         <input type="hidden" id="hideId" runat="server" />
                                         &nbsp;
                      
                                          <img src="../../../images/Button/Main_btn_fqr.jpg" alt="反确认"  id="btnReConfirm"    runat="server"  visible=true style='cursor:pointer;'   onclick=" ReconfirmIncomeBill();"/>
                                        &nbsp;
                                         
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" runat="server" visible="false"
                                            alt="返回" id="btnBack" onclick="DoBack();" style="cursor: hand;display:none;" />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" src="../../../images/Button/Main_btn_print.jpg" style="cursor: pointer;display:none;"
                                            onclick="if(confirm('请确认保存后打印！')){DoPrint();}" title="打印" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table  width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td align="left" valign="middle">
                                        业务信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">
                    <tr class="table-item">
                        <td align="right" width="10%" class="td_list_fields">
                            单据编号<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF" class="style1">
                            <div id="divCodeRule" runat="server">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" CodingType="9" ItemTypeID="2" />
                            </div>
                            <div id="divIncomeNo" runat="server" class="tdinput" style="display: none">
                            </div>
                        </td>
                        <td  class="td_list_fields" align="right" width="10%">
                            付款日期&nbsp;<span class="redbold">*</span>&nbsp;
                        </td>
                        <td bgcolor="#FFFFFF" width="23%">
                            <input id="txtAcceDate" type="text" runat="server" readonly="readonly" class="tdinput"
                                maxlength="50" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtbuydate')})" />
                        </td>
                        <td  class="td_list_fields" align="right" width="10%">
                           往来客户<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF" width="24%">
                            <input id="txtCustName" type="text" style="width:75%" class="tdinput" onclick="popSellCustObj.ShowList('protion','CustID','txtCustName','FromTBName','FileName','','2');" runat="server" />
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor:hand" onclick="popSellCustObj.ShowList('protion','CustID','txtCustName','FromTBName','FileName','','2');" />
                        </td>
                    </tr>
                    <tr class="table-item">
                 <td height="22"  class="td_list_fields" align="right">业务单类型</td>
                <td bgcolor="#FFFFFF"  width="24%">
                
                    <asp:DropDownList ID="BillingType" runat="server" Width="40%" onchange="changes()">
                  </asp:DropDownList>    
                </td>
                 <td height="22"  class="td_list_fields" align="right">
                            来源单据编号<%--<span id="fapiao1" style="color:Red; ">*</span>--%>
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input class="tdinput" id="txtOrder" style="width:75%" onclick="SelectBilling();" disabled="disabled" runat="server" type="text" size="20" maxlength="50"
                                readonly="readonly" />
                                 <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor:hand" onclick="SelectBilling()" />
                        </td>
                <td height="22"  class="td_list_fields" align="right">
                    付款金额<span class="redbold">*</span>
                </td>
            <td height="22" bgcolor="#FFFFFF" class="style1">
                <input id="txtTotalPrice" type="text" class="tdinput" runat="server" />
            </td>
           
           
        </tr>
        <tr class="table-item">
            <td height="22"  class="td_list_fields" align="right">                                            
                款项类型                                                                                      
            </td>                                                                                             
            <td height="22" align="left" bgcolor="#FFFFFF" >                                                  
                 <asp:DropDownList ID="PaymentTypes" runat="server" Width="40%" onpropertychange="AcceWay();">
                    <asp:ListItem Selected="True" Value="0">应收</asp:ListItem>                               
                    <asp:ListItem Value="1">预收</asp:ListItem>                                               
                    <asp:ListItem Value="2">其他费用</asp:ListItem>                                           
                </asp:DropDownList>                                                                           
            </td>                                                                                             
            <td height="22"  class="td_list_fields" align="right">
                付款方式
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF">
                <asp:DropDownList ID="DrpAcceWay" runat="server" Width="40%" onpropertychange="AcceWay();">
                    <asp:ListItem Selected="True" Value="0">现金</asp:ListItem>
                    <asp:ListItem Value="1">银行转账</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="22"  class="td_list_fields" align="right">
                执行人
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF">
                <%--<input id="UsertxtExcutor" type="text" onfocus="alertdiv('UsertxtExcutor,txtSaveUserID');"
                    runat="server" class="tdinput" />--%>
                <input id="UsertxtExcutor" type="text" style="width: 80%;" onfocus="alertdiv('UsertxtExcutor,txtSaveUserID');"
                    runat="server" class="tdinput" />
                    <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('UsertxtExcutor,txtSaveUserID')" />    
            </td>
           
        </tr>
        
      
    </table>
  
    <br />
    <div id="BlendingDetails" style="display: none; width:99%;" align="center">
    </div>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
        <tr>
             <td height="20" class="td_list_title">
                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                    <tr class="menutitle1">
                        <td align="left" valign="middle">
                            附加信息
                            <td align="right">
                                <div id='divButtonTotal'>
                                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','divButtonTotal')" /></div>
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
        id="Tb_02" style="display: block">
        <tr class="table-item">
            <td height="22"  class="td_list_fields" align="right" width="9%">
                建档日期
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="txtAccountDate" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
            <td height="22"  class="td_list_fields" align="right" width="10%">
                建档人
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="txtAccountor" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
            <td height="22"  class="td_list_fields" align="right" width="10%">
                确认日期
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="txtConfirmDate" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
        </tr>
        <tr class="table-item">
            <td height="22"  class="td_list_fields" align="right" width="10%">
                确认人
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="txtConfirmor" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
            <td height="22"  class="td_list_fields" align="right" width="10%">
                
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="Text1" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
            <td height="22"  class="td_list_fields" align="right" width="10%">
                
            </td>
            <td height="22" align="left" bgcolor="#FFFFFF" width="23%">
                <input id="Text2" type="text" class="tdinput" runat="server" readonly="true" />
            </td>
        </tr>
    </table>
    
    <br />
    </td> </tr> </table>
    <p>
        <input id="txtNAccounts" type="hidden" runat="server" />
        <input id="txtIncomePrice" type="hidden" runat="server" />
        <input id="txtOldPrice" type="hidden" runat="server" />
        <input type="hidden" id="hidModuleID" runat="server" />
        <input type="hidden" id="hidSearchCondition" runat="server" />
        <input id="txtAction" type="hidden" value="1" runat="server" />
        <input id="txtOprtID" type="hidden" runat="server" />
        <input id="txtSaveID" type="hidden" runat="server" />
        <input id="txtSaveUserID" type="hidden" runat="server" />
        <input id="hidCashModuleID" type="hidden" runat="server" />
        <input id="hidBankModuleID" type="hidden" runat="server" />
        <input id="hidCashOrBankType" type="hidden" runat="server" />
        <input type="hidden" id="HiddenIsSubjectsBgein" runat="server" />
        <!--往来客户 隐藏域 开始 -->
        <input id="CustID" type="hidden" runat="server" />
        <input id="FromTBName" type="hidden" runat="server" />
        <input id="FileName" type="hidden" runat="server" />
        <!--往来客户 隐藏域 结束 -->
        <div id="Container" style="border: solid 10px #898989; background: #fff; padding: 10px;
            width: 400px; z-index: 1001; position: absolute; top: 50%; left: 70%; margin: -200px 0 0 -400px;
            display: none;">
        </div>
    </p>
    <uc1:Message ID="Message1" runat="server" />
    </form>
</body>
</html>
