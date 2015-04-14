<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Company_Add.aspx.cs" Inherits="Pages_Office_SystemManager_Company_Add" %>

<%@ Register Src="../../../UserControl/Message.ascx" TagName="Message" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <link href="../../../css/default.css" rel="stylesheet" type="text/css" />
    <title>帐套管理</title>&nbsp;<link href="../../../css/validatorTidyMode.css" rel="stylesheet"
        type="text/css" /> 
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidator.js" type="text/javascript"></script>

    <script src="../../../js/JQuery/formValidatorRegex.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script src="../../../js/common/Check.js" type="text/javascript"></script>

    <script src="../../../js/common/Page.js" type="text/javascript"></script>


    <style type="text/css">
        #EmployeeID
        {
            width: 124px;
        }
        #Checkbox1
        {
            width: 20px;
        }
        #EmployeeID0
        {
            width: 124px;
        }
        #txtUserID
        {
            width: 224px;
        }
        #txtOpenDate
        {
            width: 223px;
        }
        #txtCloseDate
        {
            width: 218px;
        }
        
  *{padding:0; margin:0} /*此行样式一定要加，不然可能会引起BUG出现。*/

  #div_Add{
		position:absolute;
		width:200px;
		height:250px;
		font-size:12px;
		background:#666;
		border:1px solid #000;
		z-index:950;
		display:none;
		left:200px;
		
  }
 </style>
</head>
<body>
    <form id="frmMain" runat="server">
        <input  type="hidden" id="IsCompanyOpen" runat="server"/>
        <input  type="hidden" id="HidCompanyCD" runat="server"/>
         <input name="text" type="text" id="Text2" style="display: none" />  
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <uc1:Message ID="Message1" runat="server" />
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
        <tr>
            <td valign="top">
                <input type="hidden" id="hiddenUserID" value="" />
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
                            帐套管理
                            <asp:HiddenField ID="hidModuleID" runat="server" />
                           
                           
                        </td>
                    </tr>
                </table>
              
            </td>
        </tr>
         <tr>
            <td height="35" colspan="2" valign="top">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
            <img alt="" src="../../../Images/Button/Bottom_btn_new.jpg"
                onclick="Show();" runat="server"  id="btnNew" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
           <td>        
   
     <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                    bgcolor="#999999" >
        <tbody>
                        <tr>
                            
                             <th align="center" class="td_list_fields" >
                              
                                   公司代码
                            </th>
                            <th align="center" class="td_list_fields" >
                             
                                    公司名称
                            </th>
                              <th align="center" class="td_list_fields" >
                               
                                    超级管理员名
                            </th>
                                <th  runat="server" align="center" class="td_list_fields" >
                               
                                    超级管理员密码
                            </th>
                             <th align="center" class="td_list_fields" >
                               
                                   状态
                              
                            </th>
                            
                        </tr>
            </tbody>
        </table>
        
        </td>
        </tr>
    </table>
      <div id="divBackShadow" style="display: none">
    <iframe src="../../../Pages/Common/MaskPage.aspx" id="BackShadowIframe" frameborder="0"
        width="100%"></iframe></div>
     <div id="div_Add"   style="border: solid 10px #898989; z-index:21; background: #fff;  padding: 10px; width: 800px; top: 48%; left: 50%; margin: -200px 0 0 -400px; ">
    <table width="92%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#999999" style=" margin-left:34px">
      <tr>
        <td height="28" bgcolor="#FFFFFF" align="left">
          
            <img alt="保存"  src="../../../Images/Button/Bottom_btn_save.jpg" onclick="InsertCompanyData();" id="Img1" runat="server" />
            <img alt="返回"  src="../../../Images/Button/Bottom_btn_back.jpg" onclick="Hide();" /></td>
          </tr>
      </table>
        
         <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                        id="Tb_01" style="display: block">
                      <tr>
                <th align="right" class="td_list_fields">
                    企业代码<font  color="red" >*</font>
                </th>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="inputCompanyCD" runat="server"   onblur="GetRoot();"    />
                </td>
                <th align="right" class="td_list_fields">
                    企业中文名称<font  color="red" >*</font>
                </th>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="inputNameCn" runat="server"  onblur="GetPYShort();"  />
                </td>
                <th align="right" class="td_list_fields">
                    企业英文名称
                </th>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="inputNameEn" runat="server" />
                </td>
               
            </tr>
                     <tr >
                     <th align="right" class="td_list_fields">
                    企业简称
                </th>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="inputNameShort" runat="server" />
                </td>
                <th align="right" class="td_list_fields">
                    企业拼音代码
                </th>
                <td bgcolor="#FFFFFF">
                    <input type="text" id="inputPYShort" runat="server" />
                </td>
                  <th align="right" class="td_list_fields">
                    文档存放根目录
                </th>
                <td bgcolor="#FFFFFF"> 
                     <input type="text" id="inputDocSavePath" runat="server" style="width: 96%"   />
                </td>
                </tr>
                   <tr >
                     <th align="right" class="td_list_fields">
                    状态
                </th>
                <td bgcolor="#FFFFFF" colspan="5">
                   <asp:DropDownList ID="drp_use" runat="server">
                       <asp:ListItem Value="0">停用</asp:ListItem>
                        <asp:ListItem Value="1">启用</asp:ListItem>
                    </asp:DropDownList>
                </td>
                </tr>
                    </table>
               

</div>
    </form>
    <p>
        <input id="hidden_companycd" type="hidden" runat="server" />
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
 <script src="../../../js/office/SystemManager/Company_Add.js" type="text/javascript"></script>