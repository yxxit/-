<%@ Control Language="C#" AutoEventWireup="true" CodeFile="addCustLinkMan.ascx.cs"
    Inherits="UserControl_addCustLinkMan" %>
    <script src="../js/JQuery/jquery_last.js"></script>
    <script type="text/javascript">
      function closeDiv()
      {
       $("#addCLinkMan").fadeOut();

      }
      
    </script>
<div id="addCLinkMan" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
    width: 800px; z-index: 2005; position: absolute;display:none;top: 40%; left: 50%; margin: 5px 0 0 -400px;
    height: 220px;">
    <div>
        <img src="../../../Images/Button/Bottom_btn_close.jpg"  onclick="closeDiv()"/><img src="../../../Images/Button/Bottom_btn_save.jpg"  onclick="AddLinkManData()"/>&nbsp;
    </div>
     <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <!--style="display:block"-->
                    <tr>
                        <td height="20"   class="td_list_fields" align="right" style="width: 10%">
                            对应客户<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" style="width: 23%">                        
                           <input name="LtoCustNo" id="LtoCusNo" runat="server" class="tdinput" type="text" style="width: 78%;" />                      
                        </td>
                        <td  height="20" align="right" style="width: 10%" class="td_list_fields">
                            联系人姓名<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="txtLinkManName" id="txtLinkManName" type="text" class="tdinput" specialworkcheck="联系人姓名"
                                />
                        </td>
                        <td  height="20" align="right" style="width: 12%" class="td_list_fields">
                            性别<span class="redbold">*</span>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" style="width: 22%">
                            <select name="seleSex" width="20px" id="seleSex">
                                <option value="1">男</option>
                                <option value="2">女</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                       <td  height="20" align="right" class="td_list_fields">
                            重要程度
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <select name="seleImportant" width="20px" id="seleImportant">
                                <option value="0">--请选择--</option>
                                <option value="1">不重要</option>
                                <option value="2">普通</option>
                                <option value="3">重要</option>
                                <option value="4">关键</option>
                            </select>
                        </td>
                       <td  height="20" align="right" class="td_list_fields">
                            联系人类型
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            <asp:DropDownList ID="ddlLinkType" runat="server">
                            </asp:DropDownList>
                        </td>
                               <td height="20" align="right" class="td_list_fields">
                        是否默认联系人
                        </td>
                        <td height="20" bgcolor="#FFFFFF">
                            &nbsp; 是<input id="RdIsDefault" type="radio" value="1" validationgroup="5" runat="server"
                                name="ta" />
                            否<input id="RdNotDefault" type="radio" checked value="0" validationgroup="5" runat="server"
                                name="ta" />
                        </td>
                    </tr>
                   
                    <tr>
 <td  height="20" align="right" style="width: 10%" class="td_list_fields">
                            工作电话
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input name="txtWorkTel" id="txtWorkTel" type="text" class="tdinput" style="width: 95%" />
                        </td>
                        <td  height="20" align="right" style="width: 10%" class="td_list_fields">
                            手机号
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="txtHandset" type="text" class="tdinput" style="width: 99%"/>
                        </td>
                     <td  height="20" align="right" style="width: 10%" class="td_list_fields">
                            传真
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" style="width: 23%">
                            <input id="txtFax" type="text" class="tdinput"  style="width: 95%" />
                        </td>
                    </tr>
                     <tr>
                      <td  height="20" align="right" class="td_list_fields">
                            住址
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF" colspan="5">
                            <input name="txtHomeAddress" id="txtHomeAddress" type="text" class="tdinput" style="width: 99%" />
                        </td>
                    </tr>
                </table>
</div>
