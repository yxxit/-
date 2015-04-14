<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterSetting.aspx.cs"
    Inherits="Pages_Office_SystemManager_ParameterSetting" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参数设置</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/office/SystemManager/ParameterSetting.js" type="text/javascript"></script>

</head>
<body>
    <form id="EquipAddForm" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenID" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            参数设置
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
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基础业务规则设置
                                    </td>
                                    <td align="right">
                                        <div id='searchClick1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 基础业务 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_01">
   
                                <tr>
                                    <td align="right">
                                        条码：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <input type="radio" id="dioCB1" name="dioCB" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioCB2" name="dioCB" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgBarCodeContorl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(2,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：启用条码之后，可以使用条码扫描仪进行扫描物品</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        多计量单位：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioMU1" name="dioMU" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioMU2" name="dioMU" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMoreUnitControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(3,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td>
                                        <div class="sysinfo">
                                            提示：启用多计量单位，请把未做完的单据做完后再启用；启用后不可再停用</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        自动生成凭证：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radvoucher1" name="radvoucher" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" checked id="radvoucher2" name="radvoucher" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgVoucherControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(6,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：必须先设置辅助核算，才能设置自动生成凭证</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        自动审核登帐：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radapply1" name="radapply" runat="server" value="3" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="radapply2" name="radapply" checked="true" runat="server"
                                            value="4" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgApplyControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(7,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：只有启用了自动生成凭证的，才可以启用自动审核登账</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        超订单发/到货：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="radOver1" name="radover" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="radOver2" name="radover" checked="true" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgOverControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(8,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：启用超订单发/到货时,在销售发货(采购到货)时允许大于订单数量发货(到货)
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" width="15%">
                                        超任务单领料：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="RadTake1" name="radover" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="RadTake2" name="radover" checked  runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgistakeover"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(14,false);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：启用超任务单领料时,在建领料单时允许大于订单数量领料。
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                             <tr>
                                    <td align="right" width="15%">
                                        出入库是否显示价格：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioBN1" name="dioBN" runat="server" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="dioBN2" name="dioBN" checked="true"
                                            runat="server" />停用
                                    </td>
                                    <td align="right" width="2%">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgStorageControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(1,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF" width="19%">
                                        <div class="sysinfo">
                                            提示：限制仓管员对出库或入库的详细价格的了解</div>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        允许出入库价格为零：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioZero1" name="dioZero" runat="server" value="1" />
                                        启用&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioZero2" name="dioZero" checked="true" runat="server" value="2" />停用
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgInOutPriceControl"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(9,false);"
                                            runat="server" visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认不允许出入库价格设为零
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        小数精度设置：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left">
                                        <select id="SelPoint" runat="server">
                                            <option value="1">1</option>
                                            <option value="2" selected>2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="5">5</option>
                                            <option value="6">6</option>
                                        </select>&nbsp;位
                                    </td>
                                    <td align="left">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" style="cursor: pointer" alt="保存"
                                            id="btn_point" border="0" onclick="ParameterSetting(5,true);" runat="server"
                                            visible="false" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：系统缺省默认为2位小数，最大支持6位小数</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        行业类型选择：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left"> 
                                     <select id="IndustrySelect" runat="server">
                                            <option value="0" selected>通用生产版</option>
                                            <option value="1" >木地板行业</option> 
                                            <option value="2">化工行业</option>
                                            <option value="3">医药行业</option>                                             
                                        </select>                                      
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgIndustrySelect"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(10,true);"
                                            runat="server"  />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：请根据自身行业类型进行选择，如不清楚，请联系客服人员。
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                               <tr>
                                    <td align="right" width="15%">
                                        制单人与确认人是否可以为同一人：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioIsSame1" name="radIsSame" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" checked id="dioIsSame2" name="radIsSame" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgIsSameSel"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(1,1);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认制单人和确认人是两个不同的人员。</div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        是否只能修改本人制单的单据：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" checked id="dioUpdateSelf1" name="radUpdateSelf" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioUpdateSelf2" name="radUpdateSelf" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgUpdateSelf"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(1,2);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认只能修改本人制单的单据。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right" width="15%">
                                        是否启用定制打印模板：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="print1" name="print" runat="server" value="1" onclick="TRVisible('false')" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" checked id="print2" name="print" runat="server" value="2" onclick="TRVisible('true')" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img1"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(11,true);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：若未定制打印模板请选择否。</div>
                                    </td>
                                </tr>
                                 <tr id="TR_PrintNo" runat="server">
                                    <td align="right" width="15%">
                                        定制打印模板编码：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                    <table width="70%">
                                    <tr><td align="left">
                                        <input type="text" id="PrintNo" onkeyup="value=value.replace(/[^\w\.\/]/ig,'')" runat="server" style=" width:40px;" />
                                        </td>
                                        <td align="right">
                                        帐套编码:
                                        </td>
                                        <td align="left">
                                        <input type="text" id="CompanyCD" runat="server"  disabled="disabled" style=" width:40px;" />
                                        </td>
                                        </tr>
                                        </table>
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img2"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(12,true);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认和帐套编码相同，特殊情况请输入您定制的打印模板编码。</div>
                                    </td>
                                </tr>
                                 <tr id="TR1" runat="server">
                                    <td align="right" width="15%">
                                        打印模板宽度：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                    
                                     <input type="text" id="txt_printwidth" onkeyup="clearNoNum(this)" runat="server" style=" width:80px;" />
                                        
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img5"
                                            style="cursor: hand; float: left" border="0" onclick="ParameterSetting(13,true);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：若未设置宽度，系统默认为680。</div>
                                    </td>
                                </tr>

                            </table>
                            <!-- End 基础业务 -->
                            <br />
                        </td>
                    </tr>
                </table>
                   <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        销售业务规则设置
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick2')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 销售业务 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_02">
   
                                <tr>
                                    <td align="right" style="width:32%;">
                                        低于销售最低限价的处理：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" style="width:22%;">
                                        <select id="MinSaleSel" runat="server">
                                            <option value="0" selected>不控制</option>
                                            <option value="1">不允许交易</option>
                                            <option value="2">提示</option>
                                            <option value="3">授权码</option>
                                        </select>&nbsp;
                                    </td>
                                    <td align="left" style="width:5%;">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" style="cursor: pointer" alt="保存"
                                            id="ImgMinSale" border="0" onclick="BusinessLogicSet(2,1);" runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认情况下对低于销售最低限价不做控制。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        销售最低限价控制时机：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left"> 
                                     <select id="MinSaleTimeSel" runat="server">
                                            <option value="0" selected>单据保存时</option>
                                            <option value="1" >单据确认时</option>  
                                            <option value="2" >单据保存和确认时</option>                                           
                                        </select>                                      
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMinSaleTime"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(2,2);"
                                            runat="server"  />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认在单据保存时做销售最低限价控制。
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        客户超过信用限额时控制：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left"> 
                                     <select id="CustOverCreditSel" runat="server">
                                            <option value="0" selected>不控制</option>
                                            <option value="1" >不允许交易</option>  
                                            <option value="2" >提示</option>  
                                            <option value="3" >授权码</option>                                          
                                        </select>                                      
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgCustOverCredit"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(2,3);"
                                            runat="server"  />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认对客户超过信用限额不做控制。
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        客户信用控制处理时机：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left"> 
                                     <select id="CustCreditTimeSel" runat="server">
                                            <option value="0" selected>单据保存时</option>
                                            <option value="1" >单据确认时</option>  
                                            <option value="2" >单据保存和确认时</option>                                           
                                        </select>                                      
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgCustCreditTime"
                                           style="cursor: hand; float: left;" border="0" onclick="BusinessLogicSet(2,4);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认在单据保存时做客户信用控制处理。
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <!-- End 销售业务 -->
                            <br />
                        </td>
                    </tr>
                </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        采购业务规则设置
                                    </td>
                                    <td align="right">
                                        <div id='searchClick3'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 采购业务 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_03">
   
                                <tr>
                                    <td align="right" style="width:32%;">
                                       超过采购最高限价的处理：
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" style="width:22%;">
                                        <select id="MaxPurchaseSel" runat="server">
                                            <option value="0" selected>不控制</option>
                                            <option value="1">不允许交易</option>
                                            <option value="2">提示</option>
                                            <option value="3">授权码</option>
                                        </select>&nbsp;
                                    </td>
                                    <td align="left" style="width:5%;">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" style="cursor: pointer" alt="保存"
                                            id="ImgMaxPurchaseSel" border="0" onclick="BusinessLogicSet(3,1);" runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认情况下对超过采购最高限价不做控制。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        采购最高限价控制时机：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left"> 
                                     <select id="MaxPurchaseTimeSel" runat="server">
                                            <option value="0" selected>单据保存时</option>
                                            <option value="1" >单据确认时</option>  
                                            <option value="2" >单据保存和确认时</option>                                           
                                        </select>                                      
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgMaxPurchaseTime"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(3,2);"
                                            runat="server"  />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认在单据保存时做采购最高限价控制。
                                        </div>
                                    </td>
                                </tr>                  
                            </table>
                            <!-- End 采购业务 -->
                            <br />
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        库存业务规则设置
                                    </td>
                                    <td align="right">
                                        <div id='searchClick4'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','searchClick4')" alt="展开或收起"/></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 <table width="99%" border="0" cellspacing="1" bgcolor="#999999" cellpadding="2" align="center">
                    <tr>
                        <td bgcolor="#ffffff" align="center" valign="bottom">
                            <!-- Start 库存业务 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_04">
                                <tr>
                                    <td align="right" width="15%">
                                        允许超发货通知单出库：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="22%" align="left">
                                        <input type="radio" id="dioOverSend1" name="dioOverSend" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioOverSend2" name="dioOverSend" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right" style="width:5%">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgOverSend"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,1);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认不允许超发货通知单出库。
                                        </div>
                                    </td>
                                </tr>
                                  <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width:32%;">
                                       允许超调拨通知单出库：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioOverTransfer1" name="dioOverTransfer" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioOverTransfer2" name="dioOverTransfer" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgOverTransfer"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,2);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                            提示：默认不允许超调拨通知单出库。</div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        允许超到货通知单入库：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioOverArrive1" name="dioOverArrive" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioOverArrive2" name="dioOverArrive" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgOverArrive"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,3);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认不允许超到货通知单入库。
                                        </div>
                                    </td>
                                </tr>  
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        采购到货单审核时，是否自动生成入库单：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioCheckPurchaseIn1" name="dioCheckPurchaseIn" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioCheckPurchaseIn2" name="dioCheckPurchaseIn" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgCheckPurchaseIn"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,4);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认采购到货单审核时，不自动生成入库单。
                                        </div>
                                    </td>
                                </tr>  
                                 <tr>
                                    <td align="right">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                    <td align="left">
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="15%">
                                        销售发货单审核时，是否自动生成出库单：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioSellSend1" name="dioSellSend" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioSellSend2" name="dioSellSend" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="imgSellSend"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,5);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：默认销售发货单审核时，不自动生成出库单。
                                        </div>
                                    </td>
                                </tr>    
                                 <tr>
                                    <td align="right" width="15%">
                                        是否允许零库存出库：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="diostoragezero1" name="diostoragezero" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="diostoragezero2" name="diostoragezero" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img3"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,6);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：可用库存为零时仍可出库
                                        </div>
                                    </td>
                                </tr>   
                                 <tr>
                                    <td align="right" width="15%">
                                        是否允许超生产任务单入库：
                                    </td>
                                    <td bgcolor="#FFFFFF" width="10%" align="left">
                                        <input type="radio" id="dioover1" name="dioover" runat="server" value="1" />
                                        是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" id="dioover2" name="dioover" checked="true" runat="server" value="2" />否
                                    </td>
                                    <td align="right">
                                        <img src="../../../images/Button/Bottom_btn_save.jpg" alt="保存" id="img4"
                                            style="cursor: hand; float: left" border="0" onclick="BusinessLogicSet(4,7);"
                                            runat="server" />
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <div class="sysinfo">
                                        提示：完工入库单数量可大于生产下达数量
                                        </div>
                                    </td>
                                </tr>                      
                            </table>
                            <!-- End 库存业务 -->
                            <br />
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
