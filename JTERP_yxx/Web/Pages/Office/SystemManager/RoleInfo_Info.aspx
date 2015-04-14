<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleInfo_Info.aspx.cs" Inherits="Pages_Office_SystemManager_RoleInfo_Info" %>
<%@ Register src="../../../UserControl/Message.ascx" tagname="Message" tagprefix="uc1" %>
<%@ Register src="../../../UserControl/CodingRuleControl.ascx" tagname="CodingRule" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色表</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/BaseDataTree.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="../../../js/common/PageBar-1.1.1.js" type="text/javascript"></script>
    <script src="../../../js/common/Page.js" type="text/javascript"></script>
    <script src="../../../js/office/SystemManager/RoleInfo_Query.js" type="text/javascript"></script>
    <script src="../../../js/common/check.js" type="text/javascript"></script>
    <script src="../../../js/common/Common.js" type="text/javascript"></script>
    <script src="../../../js/common/UserOrRoleSelect.js" type="text/javascript"></script>
</head>
<body>
<form id="frmMain" runat="server">
 

    <br />
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#F0F0F0" class="checktable" id="tblDetailList" >
      <tr>
        <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /> </td>
      </tr>
      <tr>
        <td height="30" valign="top"><span class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />角色设置</span></td>
      </tr>
      <tr>
        <td height="2"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC" >
        <tr>
              <td bgcolor="#FFFFFF"><table width="100%"border="0" cellpadding="0" cellspacing="0"   id="mainindex">
                <tr>
                  <td width="350" align="left"  valign="top" bgcolor="#FFFFFF" class="Blue" ><input type="hidden" id="hidSelectValue" />
                      <div>
                        <table width="98%" >
                          <tr>
                            <td><%--    <a href="#" onclick="SetSelectValue('','','');">角色 </a>--%>
                                <div runat="server" id="divCompany"></div></td>
                          </tr>
                          <tr>
                            <td><table>
                                <tr>
                                  <td>&nbsp;</td>
                                  <td><div id="divRoleTree" style="overflow-x:auto;overflow-y:auto;height:500px;width:300px;">正在加载数据,请稍等......</div></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                      </div></td>
                  <td align="left" valign="top" bgcolor="#FFFFFF"><table width="99%" border="0" style="margin-right:10%;margin-top:5%;" align="center" cellpadding="0" cellspacing="0" >
                      <tr>
                        <td width="20%" class="Blue">选择的角色：</td>
                        <td align="left"><div id="divSelectName" style="text-align:left;"></div></td>
                      </tr>
                      <tr>
                        <td colspan="2" height="20"></td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/Bottom_btn_new.jpg" alt="新建" id="btnNew" runat="server" style="cursor:hand"  onclick="DoEditRole('3');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/cw_tjtj.jpg" alt="添加同级"  id="btnAddSame" runat="server" style="cursor:hand"  onclick="DoEditRole('0');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/cw_xj.jpg" alt="添加下级" id="btnAddSub" runat="server" style="cursor:hand"  onclick="DoEditRole('1');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../Images/Button/Show_edit.jpg" alt="修改" id="btnModify" runat="server" style="cursor:hand"  onclick="DoEditRole('2');"/> </td>
                      </tr>
                      <tr>
                        <td colspan="2"><img src="../../../images/Button/Show_del.jpg" alt="删除" id="btnDelete" runat="server" onclick="DoDelete()" style='cursor:pointer;'   /> </td>
                      </tr>
                  </table></td>
                </tr>
              </table></td>
          </tr>
        </table>
        <br /></td>
      </tr>
    </table>
    <br />    
    <br />
    <br />
      
  <div id="divEditRole" runat="server" style="background: #fff; padding: 10px; width: 850px; z-index:1; position: absolute;top: 20%; left: 15%;  display:none ;    ">    
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="tblRoleInfo">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="40" valign="top" colspan="2">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
                        <tr>
                            <td height="30" class="tdColInput">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <img src="../../../Images/Button/Bottom_btn_save.jpg" runat="server" visible="false" alt="保存" id="btnSave" style="cursor:hand"  onclick="DoSaveInfo();"/>
                                            <img src="../../../Images/Button/Bottom_btn_back.jpg" alt="返回"  visible="true" id="btnBack" runat="server" style="cursor:hand"   onclick="DoBack();"/>
                                         </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- <div style="height:500px;overflow-y:scroll;"> -->
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td  colspan="2">
                                <table>
                                    <tr>
                                        <td colspan="2" height="4">
                                            <input type="hidden" id="hidEditFlag" runat="server" />
                                            <input type="hidden" id="hidModuleID" runat="server" />
                                            <input type="hidden" id="hidSearchCondition" runat="server" />
                                            <input type="hidden" id="hidRoleID" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="tblBaseInfo" style="display:block">
                                    <tr>
                                        
                                        <td height="20" align="right" class="td_list_fields" width="8%">上级角色<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="22%">
                                            <asp:TextBox ID="RoleTxtSuperName" runat="server"  ReadOnly ="true"  CssClass="tdinput" Width="100%"  onclick="alertdiv('RoleTxtSuperName,txtSuperRoleID');"></asp:TextBox>
                                            <input type="hidden" id="txtSuperRoleID" runat="server" />
                                             <input type="hidden" id="txtSuperHistroyID" runat="server" />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td height="20" align="right" class="td_list_fields" width="8%">角色名称<span class="redbold">*</span></td>
                                        <td height="20" class="tdColInput" width="24%">
                                            <asp:TextBox ID="txtRoleName" runat="server" MaxLength="25" CssClass="tdinput"  Width="99%"></asp:TextBox>
                                        </td>
                                      
                                  
                                    <tr>
                                     <td height="20" align="right" class="td_list_fields" >描述信息</td>
                                        <td height="20" class="tdColInput" >
                                            <asp:TextBox ID="txtremark" runat="server" MaxLength="50" Width="100%" CssClass="tdinput"  TextMode="MultiLine"  Height="54px"></asp:TextBox>
                                            
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        <tr><td colspan="2" height="10"></td></tr>
                    </table>
                <!-- </div> -->
                </td>
            </tr>
        </table>    
    </div>
    
    <uc1:Message ID="msgError" runat="server" />
    <a name="DetailListMark"></a>
    <span id="Forms" class="Spantype" name="Forms"></span>
</form>
</body>
</html>