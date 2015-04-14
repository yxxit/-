<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManufactureReportPrint.aspx.cs"
    Inherits="Pages_PrinttingModel_ProductionManager_ManufactureReportPrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <style type="text/css">
        @media print
        {
            .onlyShow
            {
                display: none;
            }
            .onlyPrint
            {
                border-bottom: 1px solid #000000;
                page-break-before: always;
            }
        }
    </style>
    <style type="text/css" media="print">
        .noprint
        {
            border: 0px;
        }
        .noprint2
        {
            display: none;
        }
    </style>
    <style type="text/css" id="cssID">
        .busBtn
        {
            background: url(../../../Images/default/btnbg.gif) 0px -5px;
            border: 1px solid #cccccc;
            padding-top: 2px;
            cursor: pointer;
        }
        .trTitle
        {
            text-align: left;
            vertical-align: middle;
            padding-left: 10px;
            height: 36px;
            font-size: 16px;
            border: 1px solid #000000;
        }
        .tdFirstTitleMyLove
        {
            width: 10%;
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            font-weight: bold;
        }
        .tdFirstTitle
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-top: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle
        {
            border: 1px solid #000000;
            text-align: center;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
        }
        .tdTitle2
        {
            width: 12%;
            border: 1px solid #000000;
            text-align: right;
            border-left: none;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
        }
        .tdContent
        {
            width: 10%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
            font-weight: bold;
        }
        .tdContent2
        {
            width: 48%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            padding: 8px 0px 8px 5px;
            overflow: visible;
            word-break: break-all;
            font-size: 12px;
        }
        .tdLastContent
        {
            width: 24%;
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdColContent
        {
            border: 1px solid #000000;
            border-left: none;
            text-align: left;
            border-top: none;
            overflow: visible;
            word-break: break-all;
            padding: 8px 0px 8px 5px;
            font-size: 12px;
        }
        .tdDetail
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            border-bottom: none;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .tdPageLast td
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            overflow: visible;
            word-break: break-all;
            padding: 5px 0px 5px 5px;
            font-size: 12px;
        }
        .trDetailFirst
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-top: none;
        }
        .trDetail
        {
            border: 1px solid #000000;
            text-align: center;
            padding: 8px 8px 5px 0px;
            font-size: 12px;
            border-left: none;
            border-top: none;
            word-break: break-all;
        }
        .setDiv
        {
            width: 796px;
            overflow-x: auto;
            overflow-y: auto;
            height: 400px;
            scrollbar-face-color: #E7E7E7;
            scrollbar-highlight-color: #ffffff;
            scrollbar-shadow-color: COLOR:#000000;
            scrollbar-3dlight-color: #ffffff;
            scrollbar-darkshadow-color: #ffffff;
        }
    </style>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <title>生产任务汇报单</title>

    <script type="text/javascript">


        //此段js兼容ff的outerHTML，去掉后outerHTML在ff下不可用
        if (typeof (HTMLElement) != "undefined" && !window.opera) {
            HTMLElement.prototype.__defineGetter__("outerHTML", function() {
                var a = this.attributes, str = "<" + this.tagName, i = 0; for (; i < a.length; i++)
                    if (a[i].specified)
                    str += " " + a[i].name + '="' + a[i].value + '"';
                if (!this.canHaveChildren)
                    return str + " />";
                return str + ">" + this.innerHTML + "</" + this.tagName + ">";
            });
            HTMLElement.prototype.__defineSetter__("outerHTML", function(s) {
                var r = this.ownerDocument.createRange();
                r.setStartBefore(this);
                var df = r.createContextualFragment(s);
                this.parentNode.replaceChild(df, this);
                return s;
            });
            HTMLElement.prototype.__defineGetter__("canHaveChildren", function() {
                return !/^(area|base|basefont|col|frame|hr|img|br|input|isindex|link|meta|param)$/.test(this.tagName.toLowerCase());
            });
        }


        //打印的方法
        function pageSetup() {
            try {
                window.print();
            }
            catch (e) {
                alert("您的浏览器不支持此功能,请选择：文件→打印(P)…")
            }
        }

        //获取导出至excel的html的方法
        function fnGetTable() {
            var o_hid = document.getElementById("hiddExcel");
            o_hid.value = "";
            o_hid.value = o_hid.value + document.getElementById("cssID").outerHTML + document.getElementById("divMain").innerHTML;

            return true;
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WB" width="0">
        </object>
        <span class="noprint2" style="text-align: center; margin-top: 4px; width: 640px;">
            <input type="button" id="print" value=" 打 印 " onclick="pageSetup();" class="busBtn" />
            <asp:Button ID="btnImport" runat="server" Text=" 导 出 " CssClass="busBtn" OnClientClick="return fnGetTable();"
                OnClick="btnImport_Click" />
            <input type="button" id="btnSet" value=" 打印模板设置 " onclick="ShowSet('div_InInfo');"
                class="busBtn" />
        </span>
        <div id="divMain" align="center">
            <table width="640px" border="0" style="font-size: 12px;">
                <tbody id="tableBase" runat="server">
                </tbody>
            </table>
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableDetail" runat="server">
                </tbody>
            </table>
            <br />
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableSecondDetail" runat="server">
                </tbody>
            </table>
            <br />
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableThreeDetail" runat="server">
                </tbody>
            </table>
            <br />
            <table width="640px" border="0" cellpadding="0" cellspacing="1">
                <tbody id="tableFourDetail" runat="server">
                </tbody>
            </table>
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
    </div>
    <!-- Start 参数设置 -->
    <div align="center" id="div_InInfo" style="width: 70%; z-index: 100; position: absolute;
        display: none">
        <table border="0" cellspacing="1" bgcolor="#999999" style="width: 70%">
            <tr>
                <td bgcolor="#EEEEEE" align="center">
                    <table width="100%">
                        <tr>
                            <td align="left" onmousedown="MoveDiv('div_InInfo',event)" title="点击此处可以拖动窗口" onmousemove="this.style.cursor='move';"
                                style="font-size: 12px; font-weight: bold;">
                                &nbsp;&nbsp;打印模板设置
                            </td>
                            <td width="50" align="right">
                                <img src="../../../images/default/0420close.gif" onclick="CloseSet('div_InInfo');"
                                    style="cursor: hand;" />
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" cellspacing="1" bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF" align="center" valign="top" style="width: 90%">
                                <div id="divSet" style="display: none;" class="setDiv">
                                    <!-- Start 打印模板设置 -->
                                    <table border="0" cellspacing="1" bgcolor="#CCCCCC" style="font-size: 12px;">
                                        <tr>
                                            <td bgcolor="#FFFFFF" align="left">
                                                <table width="100%" border="0" align="left" cellspacing="0">
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            基本信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1BackNo" id="ck1ReportNo" value="ReportNo" /><input
                                                                type="text" id="txtBackNo" value="单据编号：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1Subject" id="ck1Subject" value="Subject" /><input
                                                                type="text" id="txtSubject" value="主题：" size="20" readonly />
                                                        </td>
                                                        <td align="left">
                                                            <input type="checkbox" name="ck1TaskNo" id="ck1TaskNo" value="TaskNo" /><input type="text"
                                                                id="txtTaskNo" value="生产任务单：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1DeptName" id="ck1DeptName" value="DeptName" /><input
                                                                type="text" id="txtDeptName" value="生产部门：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1DailyDate" id="ck1DailyDate" value="DailyDate" /><input
                                                                type="text" id="txtDailyDate" value="生产日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ReporterReal" id="ck1ReporterReal" value="ReporterReal" /><input
                                                                type="text" id="txtReporterReal" value="填报人：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ReportDate" id="ck1ReportDate" value="ReportDate" /><input
                                                                type="text" id="txtReportDate" value="填报日期：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ExtField1" id="ck1ExtField1" value="ExtField1" style="display: none" /><input
                                                                type="text" id="txtExtField1" value="" size="20" style="display: none" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ExtField2" id="ck1ExtField2" value="ExtField2" style="display: none" /><input
                                                                type="text" id="txtExtField2" value="" size="20" style="display: none" readonly />
                                                        </td>
                                                    </tr>
                                                    <tbody id="extTable">
                                                        <tr class='ext'>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ck1ExtField3" id="ck1ExtField3" value="ExtField3" style="display: none" /><input
                                                                    type="text" id="txtExtField3" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ck1ExtField4" id="ck1ExtField4" value="ExtField4" style="display: none" /><input
                                                                    type="text" id="txtExtField4" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField5" id="ck1ExtField5" value="ExtField5" style="display: none" /><input
                                                                    type="text" id="txtExtField5" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ck1ExtField6" id="ck1ExtField6" value="ExtField6" style="display: none" /><input
                                                                    type="text" id="txtExtField6" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ckExtField7" id="ck1ExtField7" value="ExtField7" style="display: none" /><input
                                                                    type="text" id="txtExtField7" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" name="ck1ExtField8" id="ck1ExtField8" value="ExtField8" style="display: none" /><input
                                                                    type="text" id="txtExtField8" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                        </tr>
                                                        <tr class='ext'>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ck1ExtField9" id="ck1ExtField9" value="ExtField9" style="display: none" /><input
                                                                    type="text" id="txtExtField9" value="" size="20" style="display: none" readonly />
                                                            </td>
                                                            <td width="28%" align="left">
                                                                <input type="checkbox" name="ck1ExtField10" id="ck1ExtField10" value="ExtField10"
                                                                    style="display: none" /><input type="text" id="txtExtField10" value="" size="20"
                                                                        style="display: none" readonly />
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            生产状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ProductionTotal" id="ck1ProductionTotal" value="ProductionTotal" /><input
                                                                type="text" id="txtProductionTotal" value="完成数合计：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1WorkTimeTotal" id="ck1WorkTimeTotal" value="WorkTimeTotal" /><input
                                                                type="text" id="txtWorkTimeTotal" value="工时合计：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            人员状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1PlanHRs" id="ck1PlanHRs" value="PlanHRs" /><input
                                                                type="text" id="txtPlanHRs" value="应到人数：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1RealHRs" id="ck1RealHRs" value="RealHRs" /><input
                                                                type="text" id="txtRealHRs" value="实到人数：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1PlanWorkTime" id="ck1PlanWorkTime" value="PlanWorkTime" /><input
                                                                type="text" id="txtPlanWorkTime" value="应有工时：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1AddWorkTime" id="ck1AddWorkTime" value="AddWorkTime" /><input
                                                                type="text" id="txtAddWorkTime" value="加班工时：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1StopWorkTime" id="ck1StopWorkTime" value="StopWorkTime" /><input
                                                                type="text" id="txtStopWorkTime" value="停工工时：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1RealWorkTime" id="ck1RealWorkTime" value="RealWorkTime" /><input
                                                                type="text" id="txt有效工时" value="有效工时：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            设备状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1MachineCount" id="ck1MachineCount" value="MachineCount" /><input
                                                                type="text" id="txtMachineCount" value="设备数量：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1OpenCount" id="ck1OpenCount" value="OpenCount" /><input
                                                                type="text" id="txtOpenCount" value="实际开动数：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1OpenTime" id="ck1OpenTime" value="OpenTime" /><input
                                                                type="text" id="txtOpenTime" value="总开动时间：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1OpenPercent" id="ck1OpenPercent" value="OpenPercent" /><input
                                                                type="text" id="txtOpenPercent" value="开动率：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1LoadPercent" id="ck1LoadPercent" value="LoadPercent" /><input
                                                                type="text" id="txtLoadPercent" value="负荷率：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1UsePercent" id="ck1UsePercent" value="UsePercent" /><input
                                                                type="text" id="txtUsePercent" value="设备使用率：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1StopCount" id="ck1StopCount" value="StopCount" /><input
                                                                type="text" id="txtStopCount" value="停机数目：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1StopTime" id="ck1StopTime" value="StopTime" /><input
                                                                type="text" id="txtStopTime" value="停机时长：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1StopReason" id="ck1StopReason" value="StopReason" /><input
                                                                type="text" id="txtStopReason" value="停机原因：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            物料使用状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1TakeNum" id="ck1TakeNum" value="TakeNum" /><input
                                                                type="text" id="txtTakeNum" value="领入合计：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1UsedNum" id="ck1UsedNum" value="UsedNum" /><input
                                                                type="text" id="txtUsedNum" value="耗用合计：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1NowNum" id="ck1NowNum" value="NowNum" /><input type="text"
                                                                id="txtNowNum" value="结存合计：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            备注信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1strBillStatusText" id="ck1strBillStatusText" value="strBillStatusText" /><input
                                                                type="text" id="txtstrBillStatusText" value="单据状态：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1CreatorReal" id="ck1CreatorReal" value="CreatorReal" /><input
                                                                type="text" id="txtCreatorReal" value="制单人：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1CreateDate" id="ck1CreateDate" value="CreateDate" /><input
                                                                type="text" id="txtCreateDate" value="制单日期：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ConfirmorReal" id="ck1ConfirmorReal" value="ConfirmorReal" /><input
                                                                type="text" id="txtConfirmorReal" value="确认人：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ConfirmDate" id="ck1ConfirmDate" value="ConfirmDate" /><input
                                                                type="text" id="txtConfirmDate" value="确认日期：" size="20" readonly />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" name="ck1ModifiedUserID" id="ck1ModifiedUserID" value="ModifiedUserID" /><input
                                                                type="text" id="txtModifiedUserID" value="最后更新人：" size="20" readonly />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1ModifiedDate" id="ck1ModifiedDate" value="ModifiedDate" /><input
                                                                type="text" id="txtModifiedDate" value="最后更新日期：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="28%" align="left">
                                                            <input type="checkbox" name="ck1Remark" id="ck1Remark" value="Remark" /><input type="text"
                                                                id="txtRemark" value="备注：" size="20" readonly />
                                                        </td>
                                                        <td width="28%" align="left">
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            生产明细
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="listSetDetail">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProdNo" id="ck2ProdNo" value="ProdNo" /><input type="text"
                                                                            id="txtDProdNo" value="物品编号" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2ProductName" id="ck2ProductName" value="ProductName" /><input
                                                                            type="text" id="txtDProductName" value="物品名称" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2WorkTime" id="ck2WorkTime" value="WorkTime" /><input
                                                                            type="text" id="txtDWorkTime" value="工时" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2UnFinishNum" id="ck2UnFinishNum" value="UnFinishNum" /><input
                                                                            type="text" id="txtUnFinishNum" value="完成数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2PassNum" id="ck2PassNum" value="PassNum" /><input
                                                                            type="text" id="txtDPassNum" value="合格数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck2PassPercent" id="ck2PassPercent" value="PassPercent" /><input
                                                                            type="text" id="txtDPassPercent" value="合格率" size="8" readonly />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            人员明细
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="Table1">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck3StaffReal" id="ck3StaffReal" value="StaffReal" /><input
                                                                            type="text" id="txtStaffReal" value="人员" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck3WorkTime" id="ck3WorkTime" value="WorkTime" /><input
                                                                            type="text" id="txtWorkTime" value="工时" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck3FinishNum" id="ck3FinishNum" value="FinishNum" /><input
                                                                            type="text" id="txtFinishNum" value="完成数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck3PassNum" id="ck3PassNum" value="PassNum" /><input
                                                                            type="text" id="txtPassNum" value="合格数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck3PassPercent" id="ck3PassPercent" value="PassPercent" /><input
                                                                            type="text" id="txtPassPercent" value="合格率" size="8" readonly />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            设备状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="Table2">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4MachineNo" id="ck4MachineNo" value="MachineNo" /><input
                                                                            type="text" id="txt4MachineNo" value="设备编号" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4MachineName" id="ck4MachineName" value="MachineName" /><input
                                                                            type="text" id="txt4MachineName" value="设备名称" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4UseHour" id="ck4UseHour" value="UseHour" /><input
                                                                            type="text" id="txt4UseHour" value="开机时长" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4FinishNum" id="ck4FinishNum" value="FinishNum" /><input
                                                                            type="text" id="txt4FinishNum" value="完成数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4PassNum" id="ck4PassNum" value="PassNum" /><input
                                                                            type="text" id="txt4PassNum" value="合格数" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck4PassPercent" id="ck4PassPercent" value="PassPercent" /><input
                                                                            type="text" id="txt4PassPercent" value="合格率" size="8" readonly />
                                                                    </td>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td height="20" colspan="3" bgcolor="#E1F0FF" style="font-weight: bold; color: #5D5D5D;">
                                                            物料使用状况
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table width="100%" border="0" cellspacing="1" bgcolor="#000000" id="Table3">
                                                                <tr>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5ProdNo" id="ck5ProdNo" value="ProdNo" /><input type="text"
                                                                            id="txt5ProdNo" value="物料编号" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5ProductName" id="ck5ProductName" value="ProductName" /><input
                                                                            type="text" id="txt5ProductName" value="物料名称" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5TakeNum" id="ck5TakeNum" value="TakeNum" /><input
                                                                            type="text" id="txt5TakeNum" value="本日领入" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5BeforeNum" id="ck5BeforeNum" value="BeforeNum" /><input
                                                                            type="text" id="txt5BeforeNum" value="昨日结存" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5UsedNum" id="ck5UsedNum" value="UsedNum" /><input
                                                                            type="text" id="txt5UsedNum" value="本日耗用" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5BadNum" id="ck5BadNum" value="BadNum" /><input type="text"
                                                                            id="txt5BadNum" value="本日损坏" size="8" readonly />
                                                                    </td>
                                                                    <td bgcolor="#FFFFFF">
                                                                        <input type="checkbox" name="ck5NowNum" id="ck5NowNum" value="NowNum" /><input type="text"
                                                                            id="txt5NowNum" value="本日结存" size="8" readonly />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <input type="hidden" id="isSeted" value="0" runat="server" />
                                    <!-- End 打印模板设置 -->
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td>
                                <input type="button" id="btnPrintSave" name="btnPrintSave" value=" 保 存 " class="busBtn"
                                    onclick="SaveSet();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="divPageMask" style="display: none">
        <iframe id="PageMaskIframe" frameborder="0" width="100%"></iframe>
    </div>
    <!-- End 参数设置-->
    </form>
    <p>
</body>
</html>

<script src="../../../js/common/PrintParameterSetting.js" type="text/javascript"></script>

<script language="javascript">
    var intReportID = <%=intReportID %>;
    printSetObj.BillTypeFlag = '<%=XBase.Common.ConstUtil.BILL_TYPEFLAG_PRODUCTION %>';
    printSetObj.PrintTypeFlag = '<%=XBase.Common.ConstUtil.PRINTBILL_TYPEFLAG_REPORT %>';
    /*跳转页面*/
    printSetObj.ToLocation ='ManufactureReportPrint.aspx?ID=' + intReportID;
    /*表名称*/
    printSetObj.TableName = 'officedba.'+'<%=XBase.Common.ConstUtil.CODING_RULE_TABLE_REPORT %>';
    /*取基本信息及明细信息的字段*/
    printSetObj.ArrayDB = new Array(    
                                        [   
                                            'ExtField1','ExtField2','ExtField3','ExtField4','ExtField5','ExtField6','ExtField7','ExtField8', 'ExtField9','ExtField10',
                                            'ReportNo','Subject','TaskNo','DeptName', 'DailyDate','ReporterReal','ReportDate','strBillStatusText','CreateDate','CreatorReal','ConfirmorReal','ConfirmDate',
                                            'ModifiedUserID','ModifiedDate','ProductionTotal','WorkTimeTotal','PlanHRs','RealHRs','PlanWorkTime','AddWorkTime','StopWorkTime','RealWorkTime',
                                            'MachineCount','OpenCount','OpenTime','OpenPercent','LoadPercent','UsePercent','StopCount','StopTime','StopReason','TakeNum','UsedNum','NowNum','Remark'
                                        ],
                                        [   
                                            'ProdNo','ProductName','WorkTime','UnFinishNum','PassNum','PassPercent'
                                        ]
                                        ,
                                        [
                                            'StaffReal','WorkTime','FinishNum','PassNum','PassPercent'
                                        ]
                                        ,
                                        [
                                            'MachineNo','MachineName','UseHour','FinishNum','PassNum','PassPercent'
                                        ]
                                        ,
                                        [
                                            'ProdNo','ProductName','TakeNum','BeforeNum','UsedNum','BadNum','NowNum'
                                        ]
                                    );
    


</script>

