<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayBlendBill.aspx.cs" Inherits="Pages_Office_FinanceManager_PayBlendBill" %>
<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register src="../../../UserControl/CustOrProvider.ascx" tagname="CustOrProvider" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>付款核销单</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    
    <script src="../../../js/JTHY/Expenses/PayBlendBill.js" type="text/javascript"></script>
</head>
<body>
    <uc3:CustOrProvider ID="CustOrProvider1" runat="server" />
    <form id="form1" runat="server">
    <div>
    <div <%--style="height: 500px; overflow: scroll;"--%>>
        <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
            id="mainindex">
            <tr>
                <td valign="top">
                    <input type="hidden" id="hiddenEquipCode" value="" />
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" alt=""/>
                </td>
                <td align="center" valign="top">
                </td>
            </tr>
            <tr>
                <td height="30" colspan="2" valign="top" class="Title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="30" align="center" class="Title">
                                付款核销单
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">
                            <table width="100%">
                                <tr>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;">                                       
                                        
                                        <%--<img src="../../../Images/Button/Bottom_btn_new1.jpg" id="Btnfentan" alt="分摊" style="cursor: hand; 
                                    margin: 0px;" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; --%>     
                                <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="BtnSave" style="cursor: hand;
                                    margin: 0px;" onclick="SaveBlend();" /></td>
                                    <td height="28" bgcolor="#FFFFFF" style="padding-top: 5px; padding-left: 5px;" align="center">
                                        <asp:Label ID="Label1" runat="server" Text="往来单位"></asp:Label>                                        
                                        <input id="txtCustName" type="text" onclick="popSellCustObj.ShowList('protion','CustID','txtCustName','FromTBName','FileName','','1');" readonly style="width: 150px; height: 20px; border: solid 1px #ccc; margin-left: 0px;" 
                                            class="tdinput" runat="server" />                                  
                                        <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" id="btnSearch" runat="server"
                                            style="cursor: pointer;" onclick="SearchPayBillInfo('1');" />
                                    </td>
                               </tr>     
                            </table>
                               <!-- 参数设置：是否启用条码 -->
                                <input type="hidden" id="hidBarCode" runat="server" value="" />
                             
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                   <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                     
                        <tr>
                            <td height="20"  class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td valign="top">
                                            <span class="Blue">付款单列表</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='searchClick3'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblDetailInfo','searchClick3')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblDetailInfo" bgcolor="#999999">
                        <tbody>
                            <tr>                                           
                                            <td align="center"  class="detail_no" style="width: 6%">
                                                单据日期
                                            </td>
                                            <td align="center" class="detail_name" style="width: 8%">
                                                单据编号
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 10%">
                                                往来单位
                                            </td>                                                                                     
                                            <td align="center" id="BaseUnitD" runat="server" class="td_list_fields" style="width: 4%" >
                                                币种
                                            </td>
                                            <td align="center" id="BaseCountD" runat="server" class="td_list_fields" style="width: 6%" >
                                                款项类型
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 6%">
                                                付款方式
                                            </td>
                                            <td align="center"  style="width: 5%" class="detail_spec">
                                                付款金额
                                            </td>  
                                            <td align="center"  class="td_list_fields" style="width: 5%">
                                                付款余额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 5%">
                                                结算金额<span class="redbold">*</span>
                                             </td>
                              </tr>
                        </tbody>
                        
                    </table>
                    <br />
                    
                       <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                     
                        <tr>
                            <td height="20"  class="td_list_title" colspan="2">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td valign="top">
                                            <span class="Blue">待核销单列表</span>
                                        </td>
                                        <td align="right" valign="top">
                                            <div id='Div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tblBlendDetailInfo','Div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblBlendDetailInfo" bgcolor="#999999">
                        <tbody>
                            <tr>
                                            
                                            <td align="center"  class="detail_no" style="width: 6%">
                                                单据日期
                                            </td>
                                            <td align="center" class="detail_name" style="width: 6%">
                                                单据类型
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 8%">
                                                单据编号
                                            </td>
                                            <td align="center"  style="width: 10%" class="detail_spec">
                                                往来单位
                                            </td>   
                                            <td align="center"  class="td_list_fields" style="width: 8%">
                                                订单号
                                             </td>                                        
                                            <td align="center" id="Td1" runat="server" class="td_list_fields" style="width: 4%" >
                                                币种
                                            </td>
                                            <td align="center" id="Td2" runat="server" class="td_list_fields" style="width: 5%" >
                                                原单金额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 5%">
                                                原单余额
                                            </td>
                                            <td align="center"  class="td_list_fields" style="width: 5%">
                                                本次结算<span class="redbold">*</span>
                                            </td>                                                         
                                        </tr>
                        </tbody>                        
                    </table>              
                </td>
            </tr>
        </table>
    </div>
    </div>
    <uc1:Message ID="Message1" runat="server" />
        <!--往来客户 隐藏域 开始 -->
        <input id="CustID" type="hidden" runat="server" />
        <input id="FromTBName" type="hidden" runat="server" />
        <input id="FileName" type="hidden" runat="server" />
        <!--往来客户 隐藏域 结束 -->
        <span id="Forms" class="Spantype" name="Forms"></span>
    </form>
</body>
</html>
