<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zhuo1_BackMaterialPrint.aspx.cs" Inherits="Pages_PrinttingModel_ProductionManager_zhuo1_BackMaterialPrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        .tdFirstTitleMyLove
        {
            width: 8%;
            border: 1px solid #000000;
            text-align: center;
            font-size: 12px;
            font-weight: bold;
        }
        .tdContent
        {
            border: 1px solid #000000;
            border-left: none;
            text-align: center;
            overflow: visible;
            font-size: 12px;
            font-weight: bold;
        }
        .tdDetail
        {
            border: 1px solid #000000;
            text-align: left;
            width: 100%;
            border-bottom: none;
            overflow: visible;
            font-size: 12px;
        }
        .trDetailFirst
        {
            border: 1px solid #000000;
            text-align: center;
            font-size: 12px;
            border-top: none;
        }
        .trDetail
        {
            border: 1px solid #000000;
            text-align: center;
            font-size: 12px;
            border-left: none;
            border-top: none;
        }
    </style>
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
            var obj=new Object();
        
           
            var o_hid = document.getElementById("hiddExcel");
            o_hid.value = "";
            o_hid.value = o_hid.value + document.getElementById("cssID").outerHTML + document.getElementById("divMain").innerHTML;

           return true;
        }

    </script>
    <title>退料单</title>
</head>
<body  onload="fnGetTable()">
    <form id="form1" runat="server">
    <div align="center">
       <object classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" height="0" id="WB" width="0">
        </object>
        <span class="noprint2" style="text-align: center; margin-top: 4px; width: 680px;">
            <input type="button" id="print" value=" 打 印 " onclick="pageSetup();" class="busBtn" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnImport" runat="server" Text=" 导 出 " CssClass="busBtn" onclick="btnImport_Click1"  OnClientClick="return fnGetTable();"
                 />
        </span>
         <div id="divMain" align="center">
        <table width="680px" border="0" style="font-size: 12px;">
                <tr style="height:6px;"><td colspan="9"></td></tr>
                <tr style="height:20px;">
                    <td colspan="9" align="center" id="Tab_Head" runat="server">
                    </td>
                </tr>
                <tr style="height:10px;">
                    <td colspan="9" style="color: #cccccc">
                    </td>
                </tr>             
                        <tr style="height:13px;">
                            <td align="right" width="10%">
                                单据编号：
                            </td>
                            <td colspan="3" align="left" width="60%">
                                
                                <%=ht["BackNo"]%>
                            </td>
                            <td align="right" width="10%">
                                单据日期：
                            </td>
                            <td align="left" width="20%">
                               
                                <%=ht["CreateDate"]%>
                            </td>
                        </tr>
                        <tr style="height:13px;">
                           
                            <td align="right">
                                领料单号：
                            </td>
                            <td colspan="3" align="left">
                               
                                <%=ht["TakeNo"]%>
                            </td>
                             <td align="right">
                                生产部门：
                            </td>
                            <td colspan="3" align="left">
                                
                                <%=ht["ProcessDeptName"]%>
                            </td>
                      
                        
            </table>
            <table width="680px" border="0" cellpadding="0" cellspacing="1" >
            <tr  valign="top"> <td>             
            <table width="100%" border="0" cellpadding="0" cellspacing="1">
                <tr style="height:18px;">
                   
                    <td align="center" class="tdFirstTitleMyLove">
                         序号
                    </td>
                    <td align="center" class="tdContent">
                        物品名称
                    </td>
                    <td align="center" class="tdContent">
                        规格
                    </td>
                    <td align="center" class="tdContent">
                        单位
                    </td>
                     <td align="center" class="tdContent">
                        数量
                    </td>
                    <td align="center" class="tdContent">
                        退料仓库
                    </td>
                    
                </tr>
                <asp:Repeater ID="rpt_details" runat="server" >
                <ItemTemplate>
                <tr style="height:16px">
                
                    <td class="trDetailFirst"><%#Eval("SortNo")%>&nbsp;</td>
                    <td class="trDetail"><%#Eval("ProductName")%>&nbsp;</td>
                    <td class="trDetail"><%#Eval("Specification")%>&nbsp;</td>
                    <td class="trDetail"><%#Eval("UnitName")%>&nbsp;</td>
                    
                    <td class="trDetail"><%#Eval("BackCount")%>&nbsp;</td>
                    <td class="trDetail"><%#Eval("StorageName")%>&nbsp;</td>
                 
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                </table>
                </td></tr>
              <%--  <tr style="height:14px">
                
                <td colspan="9" style="font-size:12px;border: 1px solid #000000; text-align: left; border-top:none; border-right:none; border-left:none;">
                           操作人员： <asp:Label ID="lbl_UserMans" runat="server" Text=""></asp:Label>
                   </td>
                </tr>--%>
            </table>  
            <table width="680px" border="0" style="font-size: 12px;">
           <%-- <tr>
            <td align="right" style="height:13px;width:15%">                
            每页小计：</td>
            <td colspan="7" style="width:65%"></td>
            <td align="left"><asp:Label ID="Lbl_totalprice" runat="server" Text=""></asp:Label></td>
            </tr>--%>           <%-- <tr>
              <td align="right" style="height:13px;width:15%">合计：</td>
              <td align="left" colspan="7"><asp:Label ID="lbl_totalUpper" runat="server" Text=""></asp:Label></td>
            </tr>--%>
            <%-- <tr style="height:13px">
                    <td align="right" width="10%">
                        备注：
                    </td>
                    <td colspan="8" align="left"  width="20%">
                       
                        <%=ht["Remark"]%>
                    </td>
                  
                </tr>--%>
                <tr style="height:13px">
                    <td align="right" width="10%">
                        制单人：
                    </td>
                    <td colspan="2" align="left" width="20%">
                        
                         <%=ht["CreatorReal"]%>
                    </td>
                    <td align="right" width="10%">
                       
                    </td>
                    <td colspan="2" align="left" width="20%">
                       
                    </td>
                    <td align="right" width="10%">
                          确认人：
                    </td>
                    <td width="30%" colspan="2" align="left">
                       <%=ht["ConfirmorReal"]%>
                    </td>
                </tr>
                  <tr style="height:13px">
                    <td align="right" width="10%">
                        退料人：
                    </td>
                    <td colspan="2" align="left" width="20%">
                        
                         <%=ht["TakerReal"]%>
                    </td>
                    <td align="right" width="10%">
                       
                    </td>
                    <td colspan="2" align="left" width="20%">
                       
                    </td>
                    <td align="right" width="10%">
                          收料人：
                    </td>
                    <td width="30%" colspan="2" align="left">
                       <%=ht["ReceiverReal"]%>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" id="hiddExcel" runat="server" />
      
    </div>
    </form>
</body>
</html>