<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ware_Add.aspx.cs" Inherits="Pages_JTHY_SysManage_Ware_Add" %>

<%@ Register Src="../../../UserControl/CustClassDrpControl.ascx" TagName="CustClassDrpControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
    <%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
    
<%--<%@ Register Src="../../../UserControl/CustNameSel.ascx" TagName="CustNameSel2" TagPrefix="uc4" %>

<%@ Register src="../../../UserControl/CodeTypeDrpControl.ascx" tagname="CodeTypeDrpControl" tagprefix="uc5" %>
<%@ Register src="../../../UserControl/ViewCustLinkMan.ascx" tagname="ViewCustLinkMan" tagprefix="uc6" %>
<%@ Register Src="../../../UserControl/ViewCustContact.ascx" TagName="ViewCustContact" TagPrefix="uc7" %>
<%@ Register Src="../../../UserControl/ViewCustService.ascx" TagName="ViewCustService" TagPrefix="uc8" %>
<%@ Register Src="../../../UserControl/ViewCustComplain.ascx" TagName="ViewCustComplain" TagPrefix="uc9"%>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加仓库信息</title>
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>

    <script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>

    <script src="../../../js/common/TreeView.js" language="javascript" type="text/javascript"></script>
    
    <script src="../../../js/common/UploadFile.js" type="text/javascript"></script>
    <script>
      function a()
      {
       alert("click");
      }
    </script>

    <style type="text/css">
        .cell1
        {
            background-color: #FFFFFF;
            font-family: "tahoma";
            color: #333333;
            font-size: 12px;
            display: none;
        }
        .jPagerBar
        {
            font-size: 12px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }   
        #TextBox1
        {
         display:none;	
        }    
        #Timer1
        {
          height:1px;
        }
        span #Timer1
        {
         height:1px;	
        }
        #SeleCustTypeSell
        {
            height: 16px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server"><!--AsyncPostBackTimeout="90"-->
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript">
　　Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler); 
　　function EndRequestHandler(sender, args)
　　{
        
　　if (args.get_error() != undefined)
　　{
　　if ((args.get_response().get_statusCode() == '12007') || (args.get_response().get_statusCode() == '12029'))
　　{
                   
                   
　　document.writeln("服务器连接失败。");
　　window.onerror = false; //JS中遇到脚本错误时不做任何操作

　　}
　　}
　　}
　　</script>
    <%--<uc6:ViewCustLinkMan ID="ViewCustLinkMan1" runat="server" />
    <uc7:ViewCustContact ID="ViewCustContact1" runat="server" />
     <uc8:ViewCustService ID="ViewCustService1" runat="server" />
     <uc9:ViewCustComplain ID="ViewCustComplain1" runat="server" />--%>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenEquipCode" value="" />
                <img src="../../../images/Main/Line.jpg" width="122" height="7" />
            </td>
            <td align="center" valign="top">
            </td>
        </tr>
        <tr>
            <td height="30" colspan="2" valign="top" class="Title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="30" align="center" class="Title">
                            新建仓库档案
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" align="left" bgcolor="#FFFFFF">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <img src="../../../Images/Button/Bottom_btn_save.jpg" alt="保存" runat="server" visible="false"
                                            id="btn_save" style="cursor: hand" onclick="SaveCustData();" />
                                        <img src="../../../Images/Button/btn_save_add.jpg" style="cursor: hand" id="btn_save_add" onclick="SaveCustDataAndNewAdd();"
                                                runat="server" alt="保存并新增"/>
                                        
                                        <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回" id="btn_back" style="cursor: hand;
                                            " onclick="Back();" />
                                    </td>
                                    <td align="right">
                                        <img id="btnPrint" visible="false" src="../../../images/Button/Main_btn_print.jpg"
                                            style="cursor: pointer" onclick="PagePrint()" title="打印" alt="打印"/>
                                    </td>
                                </tr>
                            </table>
                            <input type="hidden" id="hidUpDateTime" runat="server" /><%--用于上传附件时间--%>
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
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        基本信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_01','searchClick')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr>
                        <td  class="td_list_fields" align="right" style="width:10%">
                            仓库编号<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF"  style="width:23%">
                            <div id="divCodeRule" runat="server">
                                <uc3:CodingRuleControl ID="ddlCustNo" runat="server" />
                            </div>
                            <div id="divCustNo" runat="server">
                            </div>
                        </td>
                        <td  class="td_list_fields" align="right"  style="width:10%">
                            仓库名称<span class="redbold">*</span>
                        </td>
                        <td bgcolor="#FFFFFF"  style="width:23%">
                            <input name="txtCustName" class="tdinput" id="txtCustName" specialworkcheck="客户名称"
                                type="text" style="width: 95%" maxlength="50" onblur="LoadPYShort();" />
                        </td>
                        <td  class="td_list_fields" align="center" style="width:10%">
                            所属部门</td>
                        <td bgcolor="#FFFFFF"  style="width:24%">
                           <select width="20px" id="selCustBig" name="D2" onchange="CustSwitch();">
                                <option value="1">企业</option>
                                <option value="2">会员</option>
                            </select></td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="center">
                            负责人
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtCustNam" id="txtCustNam" specialworkcheck="负责人" type="text" class="tdinput"
                                style="width: 95%" maxlength="25" /></td>
                          <td height="22"  class="td_list_fields" align="center">
                            电话
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtTelephone" id="txtTelephone" type="text" runat="server" class="tdinput"
                                style="width: 95%" maxlength="50"  />
                        </td>
                        <td height="22"  class="td_list_fields" align="center">
                            资金定额
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtFund" id="txtFund" type="text" runat="server" class="tdinput"
                                style="width: 95%" maxlength="50"  />
                        </td>
                    </tr>
                     <tr>
                        <td height="22"  class="td_list_fields" align="center">
                            对应条形码
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtBarCode" id="txtBarCode" type="text" runat="server" class="tdinput"
                                style="width: 95%" maxlength="50"  />
                        </td>
                        <td height="22"  class="td_list_fields" align="center">
                            计价方式
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select width="20px" id="Select1" name="D1">
                                <option value="0">--请选择--</option>
                                <option value="1">移动平均法</option>
                                <option value="2">先进先出法</option>
                                <option value="3">全月平均法</option>
                                <option value="4">个别计价法</option>
                                <option value="5">售价法</option>
                                <option value="6">后进先出法</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="center">
                        仓库属性
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select width="20px" id="Select2" name="D1">
                                <option value="0">--请选择--</option>
                                <option value="1">普通仓</option>
                                <option value="2">现场仓</option>
                                <option value="3">委外仓</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                    <td height="22"  class="td_list_fields" align="center">
                    仓库地址
                    </td>
                    <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                            <input name="txtReceiveAddress" id="txtReceiveAddress" type="text" class="tdinput"
                                style="width: 95%" maxlength="50" />
                        </td>

                    </tr>
                    <tr>
                        <td  class="td_list_fields" align="center">
                            备注
                        </td>
                        <td align="left" bgcolor="#FFFFFF" colspan="5">
                            <textarea name="txtRemark" id="Textarea1" rows="3" cols="80" class="tdinput" style="width: 99%;
                                height: 40px"></textarea>
                        </td>
                    </tr>
                    <%--<tr>
                        <td height="22"  class="td_list_fields" align="right">
                            客户管理分类
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select width="20px" id="SeleCustTypeManage" name="D1">
                                <option value="0">--请选择--</option>
                                <option value="1">VIP客户</option>
                                <option value="2">主要客户</option>
                                <option value="3">普通客户</option>
                                <option value="4">临时客户</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            客户营销分类
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select width="20px" id="SeleCustTypeSell" name="D2">
                                <option value="0">--请选择--</option>
                                <option value="1">经济型客户</option>
                                <option value="2">个性化客户</option>
                                <option value="3">方便型客户</option>
                                <option value="4">道德型客户</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            客户优质级别
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="ddlCreditGrade" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            客户时间分类
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select width="20px" id="SeleCustTypeTime" name="D3">
                                <option value="0">--请选择--</option>
                                <option value="1">老客户</option>
                                <option value="2">新客户</option>
                                <option value="3">潜在客户</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            客户细分
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <uc2:CustClassDrpControl ID="CustClassDrpControl1" runat="server" />
                        </td>
                       <td height="22"  class="td_list_fields" align="right">
                            <div id="divProfessional" style="display:none;">所学专业</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <div id="ddlProfessional" style="display:none;"><uc5:CodeTypeDrpControl ID="CodeTypeDrpControl3" runat="server" /></div>
                            
                        </td>
                       
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            建档人
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtCreator" id="txtCreator" type="text" class="tdinput" style="width: 95%"
                                runat="server" disabled="disabled" /></td>
                        <td height="22"  class="td_list_fields" align="right">
                            建档日期<span class="redbold">*</span>
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                           <input name="txtCreatedDate" id="txtCreatedDate" type="text" class="tdinput" runat="server"
                                disabled="disabled" /></td>
                        <td height="22"  class="td_list_fields" align="right">
                            <div id="divSex" style="display:none;">性别</div></td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                        <div id="divseleSex" style="display:none;">
                            <select name="seleSex" width="20px" id="seleSex" >
                                <option value="1">男</option>
                                <option value="2">女</option>
                            </select></div></td>
                    </tr>
                    <tr id="trUsedStatus" >
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            启用状态<span class="redbold">*</span></td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="seleUsedStatus" width="20px" id="seleUsedStatus0">
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>
                      </td>
                        <td height="22" class="td_list_fields" align="right" style="width: 10%">
                            <div id="divFirstBuyDate">首次交易日期</div></td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="txtFirstBuyDate" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtFirstBuyDate')})" /></td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            服务到期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            &nbsp;<input id="txtStopDate" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStopDate')})" /></td>
                    </tr>--%>
                   <%--新添加201207020--%>
                   <%--
                    <tr >
                       
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            最近修改日期
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="extModifiedDate0" id="extModifiedDate0" type="text" 
                                class="tdinput" runat="server"
                                disabled="disabled" />
                         </td>
                         <td height="22"  class="td_list_fields" align="right" style="width: 10%"> </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%"></td>
                         <td height="22"  class="td_list_fields" align="right" style="width: 10%"> </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%"></td>
                   </tr> 
                   --%>
                </table>
                <%--<div id="ExtDiv" style="display: none">
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                        <tr>
                             <td height="20" class="td_list_title">
                                <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                    <tr>
                                        <td>
                                            扩展信息
                                        </td>
                                        <td align="right">
                                            <div id='div1'>
                                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('ExtTab','div1')" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="ExtTab">
                        <tbody>
                        </tbody>
                    </table>
                </div>--%>
                  <%--<%if (((XBase.Common.UserInfoUtil)XBase.Common.SessionUtil.Session["UserInfo"]).Version == "medicine")
                    { %>
                 <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                       医药行业专用信息
                                    </td>
                                    <td align="right">
                                        <div id='Div2'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_yy','Div2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                 <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_yy" style="display: block">
                            <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            营业执照有效期</td>
                        <td height="20" class="tdColInput" width="23%">
                                                     
                            <input id="extusedata" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker

({dateFmt:'yyyy-MM-dd',el:$dp.$('extusedata')})" /></td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            经营许可证有效期</td>
                        <td height="20" class="tdColInput" width="23%">
                           
                            <input id="extcertidata" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker

({dateFmt:'yyyy-MM-dd',el:$dp.$('extcertidata')})" /></td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            质保书有效期</td>
                        <td height="20" class="tdColInput" width="24%">
                         
                            <input id="extwardata" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker

({dateFmt:'yyyy-MM-dd',el:$dp.$('extwardata')})" /></td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            法人委托书有效期
                        </td>
                        <td height="20" class="tdColInput" width="23%">
                          
                            <input id="extpowerdata" type="text" class="tdinput" readonly="readonly" onclick="WdatePicker

({dateFmt:'yyyy-MM-dd',el:$dp.$('extpowerdata')})" /></td>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            GMP、GSP有效期
                        </td>
                        <td height="20" class="tdColInput" width="23%" >
                          
                            <input id="extgmspdata" type="text" class="tdinput" readonly="readonly" onclick="WdatePickerr

({dateFmt:'yyyy-MM-dd',el:$dp.$('extgmspdata')})" /></td>
                         <td height="20" align="right"  class="td_list_fields" width="10%">
                             &nbsp;</td>
                        <td height="20" class="tdColInput" width="24%">
                            
                                    </td>
                    </tr>
                    
                </table>
                <%} %>--%>
                <%--<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        业务信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick2'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_02','searchClick2')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_02">
                    <!--style="display:block"-->
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            国家地区
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <asp:DropDownList ID="ddlCountry" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            区域
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <asp:DropDownList ID="ddlArea" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            <div id="divProvince">省</div>
                             <div id="divLinkType" style="display:none;">联系人类型</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            <input name="txtProvince" id="txtProvince" type="text" class="tdinput" style="width: 95%" maxlength="25" />
                            <div id="divddlLinkType" style="display:none;">
                           <asp:DropDownList ID="ddlLinkType" runat="server" >
                            </asp:DropDownList> </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            <div id="divCity">市(县)</div>
                            <div id="divPaperNum" style="display:none;">身份证号</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtCity" id="txtCity" type="text" class="tdinput" style="width: 95%" maxlength="25" />
                            <input name="txtPaperNum" maxlength="25" id="txtPaperNum"  type="text" class="tdinput" style="width: 95%; display:none;" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            业务类型
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <select name="seleBusiType" width="20px" id="seleBusiType">
                                <option value="0">--请选择--</option>
                                <option value="1">普通销售</option>
                                <option value="2">委托代销</option>
                                <option value="3">直运</option>
                                <option value="4">零售</option>
                                <option value="5">销售调拨</option>
                            </select>
                        </td>
                       <td height="22"  class="td_list_fields" align="right">
                            <div id="divCustNum" style="display:none;">卡号</div></td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtCustNum" id="txtCustNum" type="text" class="tdinput" style="width: 95%;display:none;" maxlength="25" />
                         </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            <div id="divContactName">联系人</div>
                            <div id="divBirthday" style="display:none;">生日</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtContactName" id="txtContactName" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                                 <input id="txtBirthday" type="text" style="display:none;" class="tdinput" readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtBirthday')})" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            电话
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtTel" id="txtTel" type="text" class="tdinput" style="width: 95%" maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            手机
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtMobile" id="txtMobile" type="text" class="tdinput" style="width: 95%"
                                maxlength="11" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            传真
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtFax" id="txtFax" type="text" class="tdinput" style="width: 95%" maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            <div id="divOnLine">在线咨询</div>
                            <div id="divPosition" style="display:none;">职务</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtOnLine" id="txtOnLine" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                                <input name="txtPosition" id="txtPosition"  type="text" class="tdinput" size="15" style="display:none;"
                                maxlength="10" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            <div id="divWebSite">公司网址</div>
                            <div id="divAge" style="display:none;">年龄(岁)</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtWebSite" id="txtWebSite" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                                <input name="txtAge" id="txtAge"  type="text" class="tdinput" style="display:none;" size="15" maxlength="3" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            邮编
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtPost" id="txtPost" type="text" class="tdinput" style="width: 95%"
                                maxlength="10" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            电子邮件
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtemail" id="txtemail" type="text" class="tdinput" style="width: 95%"
                                maxlength="30" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                        
                        <div id="divHomeTown" style="display:none;">籍贯</div>
                            
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">                           
                            <input type="text" class="tdinput"  style="display:none;" id="txtHomeTown" maxlength="50" style="width:99%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                        <div id="divCarryType">运送方式</div>
                        <div id="divNational" style="display:none;" >民族</div>
                            
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                        <div id="divddlCarryType">
                            <asp:DropDownList ID="ddlCarryType" runat="server">
                            </asp:DropDownList></div>
                            <div id="ddlNational" style="display:none;" >
                            <uc5:CodeTypeDrpControl ID="CodeTypeDrpControl1" runat="server" />
                            </div>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                        <div id="divTakeType">交货方式</div>
                        <div id="divCulture" style="display:none;">所受教育</div>
                            
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                        <div id="divddlTakeType">
                            <asp:DropDownList ID="ddlTakeType" runat="server">
                            </asp:DropDownList>
                            </div>
                            <div id="ddlCulture" style="display:none;">
                            <uc5:CodeTypeDrpControl ID="CodeTypeDrpControl2" runat="server" />
                            </div>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            联络期限(天)
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="ddlLinkCycle" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>                  
                    <tr id="trSellArea">
                        <td height="22"  class="td_list_fields" align="right">
                            经营范围
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                            <textarea name="txtSellArea" class="tdinput" id="txtSellArea" rows="3" cols="80"
                                style="width: 99%; height: 40px"></textarea>
                        </td>
                    </tr>
                    <tr id="trRelaGrade">
                        <td height="22" class="td_list_fields" align="right" style="width: 10%">
                            关系等级
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="seleRelaGrade0" width="20px" id="seleRelaGrade0">
                                <option value="0">--请选择--</option>
                                <option value="1">密切</option>
                                <option value="2">较好</option>
                                <option value="3">一般</option>
                                <option value="4">较差</option>
                            </select>
                        </td>
                        <td height="22" class="td_list_fields" align="right" style="width: 10%">
                            最后更新用户
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="txtModifiedUser0" id="txtModifiedUser0" type="text" 
                                class="tdinput" runat="server"
                                disabled="disabled" /></td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            最后更新日期
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            &nbsp;
                            <input name="txtModifiedDate0" id="txtModifiedDate0" type="text" 
                                class="tdinput" runat="server"
                                disabled="disabled" /></td>
                    </tr>                    
                </table>--%>
                <%--<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        财务信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick4'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_04','searchClick4')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"   id="Tb_04">
                    <!---->
                    <tr id="trMoneyType">
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            账号
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" >
                            <input id="txtAccountNum" class="tdinput" maxlength="25" name="txtAccountNum" style="width: 95%"
                                type="text" />
                        </td>
                        
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            开户行
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="txtOpenBank" id="txtOpenBank" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            户名
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            <input name="txtAccountMan" id="txtAccountMan" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                        </td>
                    </tr>
                    
                    <tr id="trAccountNum">
                        <td  class="td_list_fields" align="right">
                            税务登记号
                        </td>
                        <td align="left" bgcolor="#FFFFFF">
                            <input name="txtTaxCD" id="txtTaxCD" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>                         
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                             注册地址
                        </td>
                        <td bgcolor="#FFFFFF" >
                            <input name="txtSetupAddress" id="txtSetupAddress" type="text" class="tdinput" style="width: 95%"
                                maxlength="50" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            支付方式
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <asp:DropDownList ID="ddlMoneyType" runat="server" Style="margin-bottom: 0px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            允许延期付款<span class="redbold">*</span>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="seleCreditManage" width="20px" id="seleCreditManage" onchange="DivMaxcShow();">
                                <option value="1">否</option>
                                <option value="2">是</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            信用额度(元)
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="txtMaxCredit" value="0.00" type="text" class="tdinput" onchange="Number_round(this,2)"
                                maxlength="12" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            帐期天数(天)<span id="divMaxC" style="display: none;" class="redbold">*</span>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            <input name="MaxCreditDate" id="txtMaxCreditDate" type="text" class="tdinput" style="width: 95%"
                                maxlength="12" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            结算方式
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <asp:DropDownList ID="ddlPayType" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                        <div id="divCurrencyType">结算币种</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                        <div id="divddlCurrencyType">
                            <asp:DropDownList ID="ddlCurrencyType" runat="server">
                            </asp:DropDownList>
                            </div>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            <div id="divBillType">发票类型</div>
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">   
                        <div id="divseleBillType">                    
                            <select name="seleBillType" width="20px" id="seleBillType">
                                <option value="0">--请选择--</option>
                                <option value="1">增值税发票</option>
                                <option value="2">普通地税</option>
                                <option value="3">普通国税</option>
                                <option value="4">收据</option>
                            </select>
                            </div> 
                        </td>
                    </tr>
                    
                </table>--%>
                  <%--<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        附件管理
                                    </td>
                                    <td align="right">
                                        <div id='searchClick5'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_05','searchClick5')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>--%>
                <%--<table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_05">--%>
                  <%-- <tr>
                    <td height="22" align="left" bgcolor="#FFFFFF" colspan="8">
                       <!--<div id="divDoc" runat="server" width="100%" style="margin:0px; padding:0px;">
                           <table>
                            <tr>
                              <td>
                               <a href="#" onclick="">相关文档</a>
                              </td>
                              <td colspan="3">  <img src="../../../Images/Button/Bottom_btn_new.png" />
                            <img src="../../../Images/Button/Bottom_btn_new-1.jpg"  >
                          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Pages/Office/CustManager/demo.aspx" Target="_blank">demo</asp:HyperLink>
                               
                              </td>
                           </tr>
                          </table>
                      </div>-->
                     
                   </td>
                  </tr>--%>
                    <%-- <tr>
                        <td height="22" align="left" bgcolor="#FFFFFF" colspan="8">
                            <div id="divUploadAttachment" runat="server" width="100%" style="margin:0px; padding:0px;">
                              <input id="hideCustID" type="hidden" name="hideCustID"/>
                              <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>                               
                              <a href="#"  onclick="NewMessage()" id="newDoc">上传文档</a>
                              <a href="#"  onclick="" id="newDoc1" style="color:Gray;" title="该功能当前不可用，将在保存仓库信息后开放。">上传文档</a> 
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
                </table> --%>                
                    <%--<div>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                              <asp:Timer ID="Timer1" runat="server" Interval="1500">
                              </asp:Timer>                             
                              <asp:Literal ID="Literal1" runat="server"></asp:Literal>                            
                          </ContentTemplate>
                        </asp:UpdatePanel>                      
                    </div>    
                      
                <asp:HiddenField ID="hfAttachment" runat="server" />
                <asp:HiddenField ID="hfPageAttachment" runat="server" />--%>
                <!--辅助信息开始-->
                <%--<table width="100%" id="tb_FZ" border="0" cellspacing="0" cellpadding="0">
                <tr><td>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        辅助信息
                                    </td>
                                    <td align="right">
                                        <div id='searchClick3'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_03','searchClick3')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_03">
                    <!---->
                    <tr>
                        <td height="22" bgcolor="#E6E6E6" colspan="6">
                            输入提示：以下五个红色字体标记的客户属性请尽量填入真实数据，将用于决策模式下的客户分类建模。
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%; color: #FF0000;">
                            单位性质
                        </td>
                        <td height="22" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="txtCompanyType" width="20px" id="txtCompanyType">
                            <option value="0">--请选择--</option>
                                <option value="1">事业</option>
                                <option value="2">企业</option>
                                <option value="3">社团</option>
                                <option value="4">自然人</option>
                                <option value="5">其他</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%; color: #FF0000;">
                            资产规模(万元)
                        </td>
                        <td height="22" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="txtCapitalScale" id="txtCapitalScale" type="text" class="tdinput" onchange="Number_round(this,2)"
                                maxlength="12" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%; color: #FF0000;">
                            成立时间
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            <input name="txtSetupDate" id="txtSetupDate" type="text" class="tdinput" style="width: 95%"
                                readonly="readonly" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtSetupDate')})"
                                maxlength="10" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="color: #FF0000;">
                            注册资本(万元)
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtSetupMoney" id="txtSetupMoney" type="text" class="tdinput" onchange="Number_round(this,2)"
                                maxlength="12" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="color: #FF0000;">
                            员工总数(个)
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input id="txtStaffCount" type="text" class="tdinput" maxlength="9" />
                        </td>
                        <%--<td height="22" bgcolor="#E6E6E6" colspan="2">
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                           所属分类
                        </td>
                        <td height="22" bgcolor="#FFFFFF"style="width: 24%">
                            <select width="20px" id="seleCategory" name="seleCategory">
                                <option value="0">--请选择--</option>
                                <option value="1">个体</option>
                                <option value="2">集体</option>
                            </select>
                          </td> 
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            法人代表
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtArtiPerson" id="txtArtiPerson" type="text" class="tdinput" style="width: 95%"
                                maxlength="10" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            行业
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtTrade" id="txtTrade" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            营业执照号
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtBusiNumber" id="txtBusiNumber" type="text" class="tdinput" maxlength="25" />
                        </td>
                    </tr>
                    <tr>
                    <td height="22"  class="td_list_fields" align="right">
                            企业类型
                        </td>
                        <td height="22" bgcolor="#FFFFFF"style="width: 23%">
                            <select width="20px" id="selecorptype" name="selecorptype">
                                <option value="0">--请选择--</option>
                                <option value="1">生产企业</option>
                                <option value="2">经营</option>
                            </select>
                          </td> 
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            营业执照有效期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            &nbsp;</td>
                     <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                           经营许可证有效期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            &nbsp;</td>
                    </tr> 
                    <tr >
                     <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            质保书有效期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            &nbsp;</td>
                     <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            法人委托书有效期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            &nbsp;</td>
                      <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                           GMP,GSP有效期</td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            &nbsp;</td>
                      </tr>
                    
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            是否为一般纳税人
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select name="seleIsTax" width="20px" id="seleIsTax">
                                <option value="0">--请选择--</option>
                                <option value="1">是</option>
                                <option value="2">否</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            客户来源
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtSource" id="txtSource" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            年销售额(万元)
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtSaleroomY" id="txtSaleroomY" type="text" class="tdinput" onchange="Number_round(this,2)"
                                maxlength="12" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            年利润额(万元)
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtProfitY" id="txtProfitY" value="0.00" type="text" class="tdinput"
                                onchange="Number_round(this,2)" maxlength="12" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            销售模式
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtSellMode" id="txtSellMode" type="text" class="tdinput" style="width: 95%;"
                                maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            上级客户
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <uc4:CustNameSel2 ID="CustNameSel1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            价值评估
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <select name="seleMeritGrade" width="20px" id="seleMeritGrade">
                                <option value="0">--请选择--</option>
                                <option value="1">高</option>
                                <option value="2">中</option>
                                <option value="3">低</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            阶段
                        </td>
                        <td height="22" bgcolor="#FFFFFF">
                            <input name="txtPhase" id="txtPhase" type="text" class="tdinput" style="width: 95%"
                                maxlength="25" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            热点客户
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <select name="seleHotIs" width="20px" id="seleHotIs">
                                <option value="0">--请选择--</option>
                                <option value="1">是</option>
                                <option value="2">否</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            热度
                        </td>
                        <td height="22" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="seleHotHow" width="20px" id="seleHotHow">
                                <option value="0">--请选择--</option>
                                <option value="1">低热</option>
                                <option value="2">中热</option>
                                <option value="3">高热</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            关系等级
                        </td>
                        <td height="22" bgcolor="#FFFFFF" style="width: 23%">
                            <select name="seleRelaGrade" width="20px" id="seleRelaGrade">
                                <option value="0">--请选择--</option>
                                <option value="1">密切</option>
                                <option value="2">较好</option>
                                <option value="3">一般</option>
                                <option value="4">较差</option>
                            </select>
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            启用状态<span class="redbold">*</span>&nbsp;
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" style="width: 24%">
                            <select name="seleUsedStatus" width="20px" id="seleUsedStatus">
                                <option value="1">启用</option>
                                <option value="0">停用</option>
                            </select>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            关系描述
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF" colspan="5">
                            <textarea name="txtRelation" id="txtRelation" rows="3" class="tdinput" cols="80"
                                style="width: 99%; height: 40px"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_list_fields" align="right">
                            备注
                        </td>
                        <td align="left" bgcolor="#FFFFFF" colspan="5">
                            <textarea name="txtRemark" id="txtRemark" rows="3" cols="80" class="tdinput" style="width: 99%;
                                height: 40px"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="22"  class="td_list_fields" align="right">
                            最后更新用户
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtModifiedUser" id="txtModifiedUser" type="text" class="tdinput" runat="server"
                                disabled="disabled" />
                        </td>
                        <td height="22"  class="td_list_fields" align="right">
                            最后更新日期
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <input name="txtModifiedDate" id="txtModifiedDate" type="text" class="tdinput" runat="server"
                                disabled="disabled" />
                        </td>
                        <%--<td height="22"  class="td_list_fields" align="right">
                            &nbsp;
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            &nbsp;
                        </td>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            页面状态
                        </td>
                        <td height="22" bgcolor="#FFFFFF" style="width: 24%">
                            <select name="selepagestatus" width="20px" id="selepagestatus">
                                <option value="0">--请选择--</option>
                                <option value="1">确认</option>
                                <option value="2">未确认</option>
                            </select>
                        </td>
                    </tr>
                </table>
                
                </td></tr>
                 </table>
                <!--辅助信息结束-->
                
                 <!--联系人关怀开始--> 
                 <table width="100%" id="tb_GH" style="display: none;" border="0" cellspacing="0" cellpadding="0">
                <tr><td>                
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
              <td height="11"  class="td_list_title"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                <tr>
                  <td>客户关怀 </td>
                  <td align="right"><div id='searchClickG'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer" onclick="oprItem('Tb_G','searchClickG')"/></div></td>
                </tr>
              </table></td>
            </tr>
          </table>
          <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999" id="Tb_G" ><!---->
               <tr>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">年收入情况</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                     <input type="text" class="tdinput" id="txtIncomeYear" maxlength="25" style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">饮食偏好</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input type="text" class="tdinput" id="txtFuoodDrink"  maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的音乐</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                     <input type="text" class="tdinput" id="txtLoveMusic"  maxlength="25" style="width:99%"/></td>
              </tr>
               <tr>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的颜色</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input type="text" class="tdinput" id="txtLoveColor"  maxlength="25" style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的香烟</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input type="text" class="tdinput" id="txtLoveSmoke" maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">爱喝的酒</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                    <input type="text" class="tdinput" id="txtLoveDrink" maxlength="25"  style="width:99%"/></td>
              </tr>
               <tr>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">爱喝的茶</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                     <input type="text" class="tdinput" id="txtLoveTea" maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的书籍</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input type="text" class="tdinput" id="txtLoveBook" maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的运动</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                     <input type="text" class="tdinput" id="txtLoveSport" maxlength="25"  style="width:99%"/></td>
              </tr>
               <tr>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的品牌服饰</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                    <input type="text" class="tdinput" id="txtLoveClothes" maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">喜欢的化妆品</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:23%">
                     <input type="text" class="tdinput" id="txtCosmetic" maxlength="25"  style="width:99%"/></td>
                <td height="22"  class="td_list_fields" align="right" style="width:10%">开什么车</td>
                <td height="22" align="left" bgcolor="#FFFFFF" style="width:24%">
                     <input type="text" class="tdinput" id="txtCar" maxlength="25"  style="width:99%"/></td>
              </tr>               
               <tr height="22">
                <td  class="td_list_fields" align="right" >性格描述</td>
                <td align="left" bgcolor="#FFFFFF" colspan="3">
                             <input type="text" class="tdinput" id="txtNature" maxlength="50"  style="width:99%"/></td>
                <td  class="td_list_fields" align="right">家人情况</td>
                <td align="left" bgcolor="#FFFFFF">
                            <input type="text" class="tdinput" id="txtAboutFamily"  maxlength="50" style="width:99%"/></td>
              </tr>
               <tr height="22">
                <td  class="td_list_fields" align="right" >外表描述</td>
                <td align="left" bgcolor="#FFFFFF" colspan="3" >
                    <input type="text" class="tdinput" id="txtAppearance" maxlength="50"  style="width:99%"/></td>
                <td  class="td_list_fields" align="right" >健康状况</td>
                <td align="left" bgcolor="#FFFFFF" >
                   <input type="text" class="tdinput" id="txtAdoutBody" maxlength="50"  style="width:99%"/></td>
              </tr>
              </table>
              </td></tr>
                 </table>
                <!--联系人关怀结束-->
                <!--经营信息开始-->                
                <table width="100%" id="tb_JY" border="0" cellspacing="0" cellpadding="0">
                <tr><td>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        经营信息
                                    </td>
                                    <td align="right">
                                        <div id='divButtonSell'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('tbSell','divButtonSell')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="tbSell">
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            经营理念
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="90%">
                            <textarea id="CompanyValues" cols="20" class="tdinput" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            企业口号
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="23%">
                            <textarea id="CatchWord" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            企业文化概述
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                            <textarea id="ManageValues" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            发展潜力
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                            <textarea id="Potential" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            存在问题
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                            <textarea id="Problem" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            市场优劣势
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                            <textarea id="Advantages" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            行业地位
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="23%">
                            <textarea id="TradePosition" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            竞争对手
                        </td>
                        <td colspan="5" height="20" class="tdColInput" width="23%">
                            <textarea id="Competition" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            合作伙伴
                        </td>
                        <td height="20" colspan="5" class="tdColInput" width="24%">
                            <textarea id="Collaborator" class="tdinput" cols="20" style="width: 99%; height: 40px"
                                rows="2"></textarea>
                        </td>
                    </tr>
                </table>
               </td></tr>
                 </table>
                <!--经营信息结束-->
                <!--合作策略开始-->
                <table width="100%" id="tb_HZ" border="0" cellspacing="0" cellpadding="0">
                <tr><td>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                         <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        合作策略
                                    </td>
                                    <td align="right">
                                        <div id='mydivToConn'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('mytbCon111','mydivToConn')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="mytbCon111">
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            发展计划
                        </td>
                        <td colspan="5" class="tdColInput">
                            <textarea id="ManagePlan" maxlength="500" cols="20" style="width: 99%; height: 30px"
                                class="tdinput" rows="2"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right"  class="td_list_fields" width="10%">
                            合作方法
                        </td>
                        <td colspan="5" class="tdColInput">
                            <textarea id="Collaborate" maxlength="500" cols="20" style="width: 99%; height: 30px"
                                class="tdinput" rows="2"></textarea>
                        </td>
                    </tr>
                </table>
                </td></tr>
                 </table>
                <!--合作策略结束-->
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="11"  class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td>
                                        权限信息
                                    </td>
                                    <td align="right">
                                        <div id='search6'>
                                            <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('Tb_06','search6')" /></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_06">
                    <!---->
                    <tr>
                        <td height="22"  class="td_list_fields" align="right" style="width: 10%">
                            可查看该客户档案的人员
                        </td>
                        <td height="22" align="left" bgcolor="#FFFFFF">
                            <textarea id="txtCanUserName" rows="3" readonly cols="80" style="width: 99%; height: 40px"
                                class="tdinput" onclick="alertdiv('txtCanUserName,txtCanUserID,2');"></textarea>
                            <input type="hidden" id="txtCanUserID" />
                        </td>
                    </tr>
                </table>--%>
                
                <input name='txtTRLastIndex' type='hidden' id='txtTRLastIndex' value="1" />
                <uc1:Message ID="Message1" runat="server" />
                <input type="hidden" runat="server" id="txtRecorder" />
                <input type="hidden" runat="server" id="txtChairman" />
                <input type="hidden" runat="server" id="txtSender" />
              <input type="hidden" runat="server" id="hfCustID" />
                <input type="hidden" id="hiddKey" />
                 <input id="hCondition" type="hidden" />
                <span id="Forms" class="Spantype"></span>
                <br />
            </td>
        </tr>
    </table>
    <!--  -->

    </form>
</body>
</html>
