<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrencyTypeSettingList.aspx.cs"
    Inherits="Pages_Office_FinanceManager_CurrencyTypeSettingList" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>币种类别设置</title>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    function New()
    {
      document.getElementById('txtCurrencyName').focus();
      document.getElementById('txtCurrencySymbol').value="";
      document.getElementById('RdbisMaster').value="0";
      document.getElementById('DdlConvertWay').value="0";
      document.getElementById("txtChangeTime").value="";
      document.getElementById('txtExchangeRate').value="";
      document.getElementById("RdbUsedStatus").value="1";
      document.getElementById("txtAction").value="Add";
    }
    </script>

    <style type="text/css">
        BODY
        {
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
            line-height: 120%;
            text-decoration: none;
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            background-color: #666666;
        }
        #mainindex
        {
            margin-top: 10px;
            margin-left: 10px;
            background-color: #F0f0f0;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
        }
        .maintable
        {
            filter: progid:dximagetransform.microsoft.dropshadow(color=#000000,offx=2,offy=3,positive=true);
        }
        .Title
        {
            font-family: "tahoma";
            color: #000000;
            font-weight: bolder;
            font-size: 16px;
            line-height: 120%;
            text-decoration: none;
        }
        .orderClick
        {
            cursor: pointer;
        }
        .orderTip
        {
            margin-left: 3px;
        }
        .PageList
        {
            font-family: "tahoma";
            color: #1E5CBA;
            font-size: 12px;
            line-height: 120%;
            text-decoration: none;
        }
        .jPagerBar
        {
            font-size: 12px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
    </style>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" align="center" valign="top" class="Title">
                币种类别设置
            </td>
        </tr>
        <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <img src="../../../Images/Button/Bottom_btn_new.png" id="btn_New" runat="server"
                                onclick="New();" visible="false" />
                            <asp:ImageButton ID="Save_CurreyType" runat="server" ImageUrl="../../../Images/Button/Bottom_btn_save.jpg"
                                OnClick="Save_FinanceWarning_Click" Visible="false" />
                            <asp:ImageButton ID="Delete_CurreyType" runat="server" ImageUrl="../../../Images/Button/Main_btn_delete.png"
                                OnClick="Delete_FinanceWarning_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="overflow-x: hidden; overflow-y: auto; height: 207px">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#C0C0C0">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <asp:DataList ID="WarningList" runat="server" UseAccessibleHeader="True" Width="843px"
                                    DataKeyField="ID" OnItemCommand="WarningList_ItemCommand">
                                    <HeaderTemplate>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="1">
                                            <tr>
                                                <td style="width: 50px" align="center" background="../../../images/Main/Table_bg.jpg">
                                                    序号
                                                </td>
                                                <td align="center" style="width: 50px" background="../../../images/Main/Table_bg.jpg">
                                                    选择
                                                </td>
                                                <td align="center" style="width: 150px" background="../../../images/Main/Table_bg.jpg">
                                                    币种名称
                                                </td>
                                                <td align="center" style="width: 80px" background="../../../images/Main/Table_bg.jpg">
                                                    币种符号
                                                </td>
                                                <td align="center" style="width: 80px" background="../../../images/Main/Table_bg.jpg">
                                                    是否本币
                                                </td>
                                                <td align="center" style="width: 80px" background="../../../images/Main/Table_bg.jpg">
                                                    汇率
                                                </td>
                                                <td align="center" style="width: 100px" background="../../../images/Main/Table_bg.jpg">
                                                    汇率调整时间
                                                </td>
                                                <td align="center" style="width: 80px" background="../../../images/Main/Table_bg.jpg">
                                                    启用状态
                                                </td>
                                                <td align="center" style="width: 80px" background="../../../images/Main/Table_bg.jpg">
                                                    操作
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="0" cellpadding="1" cellspacing="1" bgcolor="#C0C0C0"
                                            id="dt_content">
                                            <tr>
                                                <td style="width: 50px" align="center" bgcolor="#FFFFFF">
                                                    <%#Container.ItemIndex+1 %>
                                                </td>
                                                <td style="width: 50px" align="center" bgcolor="#FFFFFF">
                                                    <asp:CheckBox ID="CheckStatus" runat="server" />
                                                </td>
                                                <td style="width: 150px" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabCurrencyName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CurrencyName")%>'></asp:Label>
                                                </td>
                                                <td style="width: 80px" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabCurrencySymbol" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CurrencySymbol")%>'></asp:Label>
                                                </td>
                                                <td style="width: 80px" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabisMaster" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "isMaster")%>'></asp:Label>
                                                </td>
                                                <td style="width: 80px;" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabExchangeRate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ExchangeRate")%>'></asp:Label>
                                                </td>
                                                <td style="width: 100px" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabChangeTime" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ChangeTime")%>'></asp:Label>
                                                </td>
                                                <td style="width: 80px" align="center" bgcolor="#FFFFFF">
                                                    <asp:Label ID="LabUsedStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UsedStatus")%>'></asp:Label>
                                                </td>
                                                <td style="width: 80px" align="center" bgcolor="#FFFFFF">
                                                    <asp:ImageButton ID="BtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/Button/Show_Change.jpg" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="23" bgcolor="#FFFFFF">
                            <table id="dt_detail" style="display: block">
                                <tr>
                                    <td align="right">
                                        币种名称：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrencyName" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        币种符号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCurrencySymbol" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        是否本币：
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RdbisMaster" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                   <td align="right">
                                        使用状态
                                    </td>
                                    <td >
                                        <asp:RadioButtonList ID="RdbUsedStatus" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                                            <asp:ListItem Value="0">停用</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        汇率调整时间：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtChangeTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtChangeTime')})"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        汇率：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExchangeRate" runat="server" onkeydown="Numeric_OnKeyDown()"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                     <td align="right">
                                        折算方式：
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="DdlConvertWay" runat="server">
                                            <asp:ListItem Value="1" Selected="True">人民币</asp:ListItem>
                                            <asp:ListItem Value="2">美元</asp:ListItem>
                                            <asp:ListItem Value="3">英镑</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <uc1:Message ID="Message1" runat="server" />
            </td>
        </tr>
    </table>
    <p>
        <input id="txtAction" type="hidden" value="Add" runat="server" /></p>
    <asp:HiddenField ID="txtHiddenFieldID" runat="server" />
    <input id="txtCpntrolID" type="hidden" value="User|txtWarningPerson" /></form>
</body>
</html>
