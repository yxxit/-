<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileAdd.aspx.cs" Inherits="Pages_Personal_FileManage_FileAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="../../../css/default.css" type="text/css" rel="stylesheet" />
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/Personal/Common.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>
<script src="../../../js/personal/Culture/UserListCtrl2.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
        $(document).ready(function(){        
            //LoadUserList('LoadUserList',BuildTree);      
            LoadUserList('LoadDepartmentList',BuildTree);                        
        });       
        
        function LoadUserList(action,callback)
        {
            $.ajax({ 
                    type: "POST",
                    url: "../../../Handler/Personal/MessageBox/SendInfo.ashx?action=" + action,
                    dataType:'string',
                    data: '',
                    cache:false,
                    success:function(data) 
                    {                          
                        var result = null;
                        eval("result = "+data);
                        
                        if(result.result)                    
                        {
                            callback(result.data);
                                             
                        }else{                  
                               showInfo(result.data);               
                        }                   
                    },
                    error:function(data)
                    {
                         showInfo(data.responseText);
                    }
                });
        }        
        
        function clearSel()
        {
 
               document.getElementById(lastid1).value = "";
               document.getElementById(lastid2).value = "";
               treeview_unsel();
               
        }
        
        function hideSelPanel()
        {
             var box = document.getElementById("selUserBox");
        
            box.style.display = "none";
        }

        var lastid1 = "", lastid2 = "";
        var selmode = 0;
        function showSelPanel(id1, id2, mode) {
            treeview_unsel();
            selmode = mode;
            lastid1 = id1;
            lastid2 = id2;
            var box = document.getElementById("selUserBox");
            box.style.left = "250px";
            box.style.top = "100px";

            box.style.display = "";

        } 
        
        function SaveToServer() { 
            if (document.getElementById("inputCompuny").value == "") {
                alert("请选择文档类型");
                return false;
            } 
            if (document.getElementById("memoUserID").value + "" == "") {
                alert("请选择发布部门");
                return false;
            }
            if (document.getElementById("txtMemoDate").value + "" == "") {
                alert("请选择发布日期");
                return false;
            }
            if(document.getElementById("txtName").value + "" == "")
            {
                alert("请输入标题");
                return false;
            }
            
            if(document.getElementById("txtName").value.length > 50)
            {
                 alert("标题长度不能超过50个字符");
                return false;
            }
            return true;
        }
</script>
    <title>附件新增</title>
</head>
<body>
<form id="form1" runat="server"><div>
<table width="95%" align="center" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex">
  <tr>
    <td valign="top">
        <img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td align="center" valign="top"></td>
  </tr>
  <tr>
    <td height="30" colspan="2" valign="top" class="Title">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="30" align="center" class="Title">新增附件</td>
        </tr>
        </table>
         <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
      <tr>
        <td height="28" bgcolor="#FFFFFF">
             <asp:ImageButton alt="保存" ID="ImageButton1" runat="server" ImageUrl="../../../images/Button/Bottom_btn_save.jpg"  OnClientClick="return SaveToServer();"  OnClick="ImageButton1_Click"/>
              <asp:HyperLink ID="hlGoBack" NavigateUrl="FileList.aspx" alt="返回" ImageUrl="../../../images/Button/Bottom_btn_back.jpg"
                                runat="server">返回</asp:HyperLink>
        </td>
      </tr>
      </table>
     </td></tr>
     <tr>
    <td colspan="2" valign="top" width="100%" >
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="6"></td>
          </tr>
        </table>
        <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
            <tr>
               <td height="20" class="td_list_title"><table width="100%" border="0" cellspacing="0" cellpadding="3">
                  <tr>
                    <td>附件信息 </td>
                    <td align="right">&nbsp;</td>
                  </tr>
              </table></td>
            </tr>
          </table>
                    <Table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" id="tblBaseInfo" style="display: block">
                       <tr>
                        <td width="8%" height="15" align="right" class="td_list_fields">附件名称<span style="color: Red">*</span></td> 
                        <td height="15" class="tdColInput">
                            <asp:TextBox ID="txtName" runat="server" CssClass="tdinput" Width="95%" MaxLength="100"></asp:TextBox>
                        </td>
                        <td align="right" class="td_list_fields" width="6%">
                            所属分类<span style="color: Red">*</span>
                        </td>
                        <td align="left" bgcolor="#FFFFFF" width="17%">
                            <asp:TextBox ID="Compuny" runat="server" CssClass="tdinput" onclick="alertdiv('Compuny,inputCompuny');"
                                ReadOnly="true" Width="70%"></asp:TextBox>
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('Compuny,inputCompuny');" />
                            <input name="inputCompuny" id="inputCompuny" type="hidden" runat="server" />
                        </td>
                        <td width="8%" height="15" align="right" class="td_list_fields">附件类型</td>
                        <td width="25%" height="15" class="tdinput">
                            <asp:DropDownList ID="txtType" runat="server">
                            <asp:ListItem>Word</asp:ListItem>
                            <asp:ListItem>Excel</asp:ListItem>
                            <asp:ListItem>ppt</asp:ListItem>
                            <asp:ListItem>wps</asp:ListItem>
                            <asp:ListItem>jpg</asp:ListItem>
                            <asp:ListItem>bmp</asp:ListItem>
                            <asp:ListItem>rar</asp:ListItem>
                            <asp:ListItem>zip</asp:ListItem>
                            <asp:ListItem>txt</asp:ListItem>
                            <asp:ListItem>其它</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td height="15" align="right" class="td_list_fields">内容摘要</td>
                        <td colspan="5" class="tdColInput">
                            <asp:TextBox ID="txtContent" Height="55" TextMode="MultiLine" runat="server" CssClass="tdinput" Width="98%"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td height="15" align="right" class="td_list_fields">可查看人员</td>
                        <td width="36%" class="tdColInput">
                            <asp:TextBox ID="UserCanViewUserName" ReadOnly="true" Width="250px" CssClass="tdinput"  runat="server" onclick="alertdiv('UserCanViewUserName,CanViewUser,2');"></asp:TextBox>
                            <input type="hidden" id="CanViewUser" runat="server" />
                            <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('UserCanViewUserName,CanViewUser,2');"
                                id="Img1" />
                        </td>
                        <td width="6%" height="15" align="right" class="td_list_fields">发布部门</td>
                        <td width="17%" class="tdColInput">
                             <asp:TextBox ID="txtMemoUserID" CssClass="tdinput" onclick="showSelPanel('memoUserID','txtMemoUserID',1)"
                                            ReadOnly="true" runat="server" ></asp:TextBox>
                                        <input type="hidden" id="memoUserID" runat="server" />
                                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="showSelPanel('memoUserID','txtMemoUserID',1)"
                                            id="Img2" />
                        </td>
                        <td height="15" align="right" class="td_list_fields">发布日期</td>
                        <td class="tdColInput">
                            <asp:TextBox ID="txtMemoDate" CssClass="tdinput" runat="server" ReadOnly="True" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('NeedDate')})"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td height="15" align="right" class="td_list_fields">选择附件</td>
                        <td colspan="5" class="tdColInput">
                            <asp:FileUpload ID="txtFile" runat="server"  CssClass="tdinput" Width="500px"/>
                        </td>
                      </tr>
                    </table>
     </td>
    </tr>
 </table></div>
 <div id="selUserBox" style="position:absolute; left:0px; top:0px; z-index:10; display: none;">
        <ul id="typeListTab">
            <li class="selTab">部门选择</li>
            <li class="tab">
                <img title="关闭" alt="关闭" onclick="hideSelPanel()" style="margin-left: 60px; cursor: pointer;"
                    align="absbottom" src="../../../Images/Pic/Close.gif" /></li>
        </ul>
        <div id="userList">
        </div>
        <div style="border: solid 1px #3366cc; padding: 5px; text-align: center; width: 200px;">
            <a href="#" onclick="clearSel()">清空</a>&nbsp;&nbsp;<a href="#" onclick="hideSelPanel()">确定</a>
        </div>
 </div>
</form>
</body>
</html>
