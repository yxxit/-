<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductInfoAdd.aspx.cs" Inherits="Pages_Office_SupplyChain_ProductInfoAdd" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/Common/GetExtAttributeControl.ascx" TagName="GetExtAttributeControl"
    TagPrefix="uc3" %>
<%@ Register Src="../../../UserControl/PaperPlateSelControl.ascx" TagName="PaperPlateSelControl" TagPrefix="uc5"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物品档案</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
    <script src="../../../js/common/Check.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>  
    <script src="../../../js/common/TreeView.js" type="text/javascript"></script>    
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script> 
    
    
    <style type="text/css">
        #pageDataList2 TD
        {
            color: #333333;
        }
        #pageDataList2 A
        {
            color: #333333;
        }
        #userList
        {
            border: solid 1px #111111;
            width: 200px;
            z-index: 11;
            display: none;
            position: absolute;
            background-color: #f1f1f1;
        }
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
        }
    </style>
    

    <script type="text/javascript">
    var intOtherCorpInfoID = <%=intOtherCorpInfoID %>;
       
        function hidetxtUserList() {
            hideUserList();           
        }   
        
        function showUserList() {
            var list = document.getElementById("userList");

            if (list.style.display != "none") {
                list.style.display = "none";
                return;
            }

            var pos = elePos(document.getElementById("txt_TypeID"));            
            list.style.left = pos.x;
            list.style.top = pos.y + 20;
            document.getElementById("userList").style.display = "block";
        }

        function hideUserList() {
            document.getElementById("userList").style.display = "none";
        }
        function SelectedNodeChanged(code_text, type_code, typeflag) {
            document.getElementById("txt_BigType").value = typeflag;
            document.getElementById("txt_TypeID").value = code_text;
            document.getElementById("txt_Code").value = type_code;

            switch (typeflag) {
                case "1 ":
                    document.getElementById("txt_BigTypeName").value = "成品";
                    break;
                case "2 ":
                    document.getElementById("txt_BigTypeName").value = "原材料";
                    break;
                case "3 ":
                    document.getElementById("txt_BigTypeName").value = "固定资产";
                    break;
                case "4 ":
                    document.getElementById("txt_BigTypeName").value = "低值易耗";
                    break;
                case "5 ":
                    document.getElementById("txt_BigTypeName").value = "包装物";
                    break;
                case "6 ":
                    document.getElementById("txt_BigTypeName").value = "服务产品";
                    break;          
            }
            hideUserList();
        }
        
    </script>

    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />   

</head>
<body text="100">
    <form id="frmMain" runat="server">
    <uc1:Message ID="Message1" runat="server" />
    <uc5:PaperPlateSelControl ID="PaperPlateSelControl" runat="server" />
    <input type="hidden" id="txtIndentityID" value="0" runat="server" />
    <input type="hidden" id="txtPrincipal" value="0" runat="server" />
    <!-- 小数位数 -->
    <input type="hidden" id="hidPoint" value="<%=((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).SelPoint.ToString() %>" />
    
    <table width="98%"  border="0" cellpadding="0" cellspacing="0" 
        id="mainindex">

        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
                    <tr >
                        <td  align="left" valign="middle" class="Title" >
                            &nbsp;&nbsp;<% if (this.intOtherCorpInfoID > 0){ %>物品档案<%}else{ %>新建物品档案<%} %>
                        </td>
                        <td align="right" >
                            &nbsp;<img id="product_btn_Add" src="../../../images/Button/Bottom_btn_save.jpg" onclick="Fun_Save_ProductInfo();"
                                style="cursor: pointer; " title="保存物品信息" runat="server" visible="false" /><span
                                    id="GlbFlowButtonSpan"></span>
                            <img alt="确认" src="../../../Images/Button/Bottom_btn_confirm.jpg" id="product_btn_AD1"
                                onclick="ChangeStatus();" style="display: none; " runat="server" visible="false" />
                            <img alt="无法确认" src="../../../Images/Button/UnClick_qr.jpg" id="product_btnunsure1"
                                 runat="server" visible="false" />
                            <img src="../../../images/Button/Bottom_btn_back.jpg" border="0" style="display:none;"
                                id="product_btnback" onclick="DoBack();" runat="server" />
                            <input type="hidden" id="product_btnunsure" />
                            <input type="hidden" id="product_btn_AD" />
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
                                    <td  align="left" valign="middle" >
                                        &nbsp;&nbsp;基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick1'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick1')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01" style="display: block">
                    <tr class="table-item">
                        <td height="20" align="right" valign="top" class="td_list_fields" width="10%">
                            物品编号<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%" style=" padding-top:5px;">                        
                            <div id="divInputNo" runat="server" style="float: left">
                                <uc2:CodingRuleControl ID="CodingRuleControl1" runat="server" />
                            </div>
                            <asp:TextBox ID="divNo" class="tdinput"   MaxLength="50" runat="server" Width="50%" style=" display:none; float: left"></asp:TextBox>
                        </td>
                        <td height="20" align="right" valign="top" class="td_list_fields" width="10%">
                            物品名称<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%" valign="top" >
                            <asp:TextBox ID="txt_ProductName" specialworkcheck="物品名称" onmouseout="LoadPYShort();"
                                MaxLength="50" runat="server" CssClass="tdinput" Width="90%"></asp:TextBox>
                        </td>  
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            简称
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_ShortNam" specialworkcheck="名称简称" name="txtConfirmorReal3"
                                class="tdinput" runat="server" size="20" style="width: 90%" />
                        </td>                      
                    </tr>                    
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields">
                            基本单位<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:DropDownList ID="sel_UnitID" runat="server" CssClass="tddropdlist" ><%--onchange="InitGroupUnit();">--%>
                            </asp:DropDownList>
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            拼音缩写
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_PYShort" specialworkcheck="拼音缩写" name="txtConfirmorReal0"
                                class="tdinput" runat="server" style="width: 90%" />
                        </td>                   
                        <td height="20" align="right"  class="td_list_fields">
                            条码
                        </td>
                        <td height="20" class="tdColInput">
                            <input id="txt_BarCode" specialworkcheck="条码" cols="20" maxlength="50" name="S1"
                                class="tdinput" runat="server" style="width: 90%" />
                        </td> 
                    </tr> 
                     <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields">
                            物品分类<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="20" class="tdColInput">
                           <input type="text" id="txt_TypeID" readonly onclick="showUserList()" class="tdinput" value="" runat="server" style="width: 90%" />
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            所属大类
                        </td>
                        <td height="20" class="tdColInput">
                           <input id="txt_Code" type="hidden" runat="server" />
                           <input type="text" runat="server" id="txt_BigTypeName" value=""  class="tdinput" style="width: 90%" />
                        </td>                   
                        <td height="20" align="right"  class="td_list_fields">                            
                        </td>
                        <td height="20" class="tdColInput">                            
                        </td> 
                    </tr> 
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields">
                            发热量（Qnet,ar）
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_HeatPower" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />
                            
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            挥发份(Vd)%
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_VolaPercent" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />
                            
                        </td>                   
                        <td height="20" align="right"  class="td_list_fields">
                            灰份(Ad)%
                        </td>
                        <td height="20" class="tdColInput">  
                            <input type="text" id="txt_AshPercent" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />                         
                        </td> 
                    </tr> 
                    
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields">
                            全硫份(st,d)%
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_SulfurPercent" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />
                           
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            水(Mt,ar)%
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_WaterPercent" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />
                        </td>                   
                        <td height="20" align="right"  class="td_list_fields">
                            固定碳%
                        </td>
                        <td height="20" class="tdColInput">
                            <input type="text" id="txt_CarbonPercent" specialworkcheck="拼音缩写" 
                                class="tdinput" runat="server" style="width: 90%" />
                        </td> 
                    </tr> 
                    
                    
                    <tr class="table-item">   
                    <td  align="right" class="td_list_fields">
                            <input type="hidden" id="hidSearchCondition" runat="server" />
                            <input type="hidden" id="hidFromPage" runat="server" />
                            备注
                        </td>
                        <td class="tdColInput" colspan="5">
                            <asp:TextBox ID="txt_Remark" runat="server" CssClass="tdinput" Width="90%" TextMode="MultiLine" ></asp:TextBox>
                        </td> 
                   </tr>               
                </table>
                <br />                
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td  align="left" valign="middle" >
                                        &nbsp;&nbsp;价格信息
                                        <input id="txtPlanNoHidden" type="hidden" />
                                        <input id="txt_TypeCode" type="hidden" />
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <input id="Hidden1" type="hidden" />
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02" style="display: block">                    
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            去税售价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_StandardSell" runat="server" class="tdinput" Width="90%" 
                                onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(true);' ></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            销项税率(%)
                        </td>
                        <td height="20" class="tdColInput" width="23%" >
                            <asp:TextBox ID="txt_TaxRate" MaxLength="50" runat="server" CssClass="tdinput" Width="80%"
                                onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(true);' ></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            含税售价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_SellTax" name="txtConfirmorReal4" class="tdinput" width="23%"
                                runat="server" size="20" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTaxNew(false);' />
                        </td>
                    </tr>                    
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            去税进价(元)
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            &nbsp;<asp:TextBox ID="txt_TaxBuy" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="74%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            &nbsp;进项税率(%)
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txt_InTaxRate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="80%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(true);'></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            含税进价(元)
                        </td>
                        <td height="20" class="tdColInput" width="24%">
                            <input type="text" id="txt_StandardBuy" name="txtConfirmorReal6" class="tdinput"
                                runat="server" size="20" style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());LoadSellTax(false);' />
                        </td>
                    </tr>
                   
                </table>
                <br /> 
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td  align="left" valign="middle" >
                                        &nbsp;&nbsp;库存信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonTotal'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','divButtonTotal')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03" style="display: block">                   
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            安全库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <input type="text" id="txt_SafeStockNum"  name="txtConfirmorReal0"
                                class="tdinput" runat="server" style="width: 80%" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            最低库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_MinStockNum"  MaxLength="50"
                                runat="server" CssClass="tdinput" Width="80%" 
                                onblur='Number_round(this,$("#hidPoint").val());' ></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            最高库存量
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_MaxStockNum" runat="server"
                                CssClass="tdinput" Width="80%" onblur='Number_round(this,$("#hidPoint").val());'></asp:TextBox>
                        </td>
                    </tr>
                                
                    <tr id="storage" runat="server" class="table-item">
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            主放仓库<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <asp:DropDownList ID="sel_StorageID" runat="server" CssClass="tddropdlist">
                            </asp:DropDownList>
                        </td>
                        
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            当前库存量
                        </td>
                        <td height="20" class="tdColInput"  colspan="3">
                            <input type="text" id="txt_Storage" name="txtConfirmorReal0"
                                class="tdinput" runat="server" readonly style="width: 90%" onblur='Number_round(this,$("#hidPoint").val());' />
                        </td>                      
                        
                    </tr>
                </table>
                <br /> 
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr class="menutitle1">
                                    <td  align="left" valign="middle" >
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
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            建档日期<span class="redbold">*</span>
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            <asp:TextBox ID="txt_CreateDate" runat="server" CssClass="tdinput" Width="80%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            建档人<span class="redbold">*</span> 
                        </td>
                        <td height="20" align="left" class="tdColInput" width="23%">
                            &nbsp;<asp:TextBox ID="UserPrincipal" runat="server" CssClass="tdinput" ReadOnly="true"
                                Width="90%" Enabled="False"></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            启用状态
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                            <select id="sel_UsedStatus" name="D2" runat="server" class="tddropdlist" style="width:90%" >
                                
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>
                        </td>
                    </tr>                   
                    <tr class="table-item">
                        <td height="20" align="right"  class="td_list_fields">
                            最后更新日期<span class="redbold">*</span>
                        </td>
                        <td height="20" class="tdColInput">
                            <asp:TextBox ID="txtModifiedDate" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="95%" disabled Text=""></asp:TextBox>
                        </td>
                        <td height="20" align="right"  class="td_list_fields">
                            最后更新用户ID
                        </td>
                        <td height="20" class="tdColInput" colspan="3">
                            <asp:TextBox ID="txtModifiedUserID" MaxLength="50" runat="server" CssClass="tdinput"
                                Width="93%" disabled Text=""></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    
    <div id="userList" style="display: none; ">
        <iframe id="aaaa" style="position: absolute; z-index: -1; width: 200px; height: 290px;"
            frameborder="0"></iframe>
        <div style="background-color: Silver; padding: 3px; height: 20px; padding-left: 50px;
            padding-top: 1px">
            <table width="100%">
                <tr>
                    <td align="right">
                        <a href="javascript:ClearProductClass()">清空</a>
                    </td>
                    <td width="20%" align="right">
                        <a href="javascript:hidetxtUserList()">关闭</a>
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding-top: 5px; height: 270px; width: 200px; overflow: auto; margin-top: 1px">
            <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
            </asp:TreeView>
        </div>
    </div>
    
   
    <p>    &nbsp;</p>
    <span id="Forms" class="Spantype"></span>
    <input type="hidden" id="hiddKey" />
    <asp:HiddenField ID="hidListModuleID" runat="server" />
    <asp:HiddenField ID="hidModuleID" runat="server" />
    <asp:HiddenField ID="txt_PlanCost" runat="server" />
    <asp:HiddenField ID="txt_SellMax" runat="server" />
    <asp:HiddenField ID="txt_SellMin" runat="server" />
    <asp:HiddenField ID="txt_BigType" runat="server" />
    <asp:HiddenField ID="txt_BuyMax" runat="server" />
    <asp:HiddenField ID="txt_IsConfirmProduct" runat="server" Value="" />
    <p>&nbsp;</p>
    </form>
    </body>
</html>
<script src="../../../js/jthy/sysmanage/ProductInfoAdd.js"  type="text/javascript"></script>
