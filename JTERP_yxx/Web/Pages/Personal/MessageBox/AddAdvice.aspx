<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAdvice.aspx.cs" Inherits="Pages_Personal_MessageBox_AddAdvice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建个人建议</title>
    <link rel="stylesheet" type="text/css" href="../../../css/default.css" />
    <link href="../../../css/validatorTidyMode.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #txtTitle
        {
            width: 342px;
        }
        #txtToList
        {
            width: 467px;
        }
        .style1
        {
            width: 470px;
        }
        #selUserBox
        {
            background: #ffffff;
        }
        #userList
        {
            border: solid 1px #3366cc;
            width: 200px;
            height: 300px;
            overflow: auto;
            padding-left: 10px;
        }
        #typeListTab
        {
            background: #2255bb;
            padding: 5px;
            margin: 0px;
            width: 202px;
            background: #3366cc;
        }
        /* #typeListTab LI{cursor:pointer;display:inline;color:White;margin-left:5px;border:solid 1px #0000ff;padding:2px;}
       */.tab
        {
            cursor: pointer;
            display: inline;
            color: White;
            background-color: inherit;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        .selTab
        {
            cursor: pointer;
            display: inline;
            color: Black;
            background-color: White;
            margin-left: 5px;
            border: solid 1px #0000ff;
            padding: 2px;
        }
        .style3
        {
            width: 59px;
        }
        .style4
        {
            color: #044d77;
            background-color: #dfebf8;
            width: 108px;
        }
    </style>

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/personal/common.js" type="text/javascript"></script>

    <script src="../../../js/personal/MessageBox/UserListCtrl2.js" type="text/javascript"></script>

    <script src="../../../js/personal/MessageBox/SendInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
  

        function showInfo(msg)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
        }    
    
        $(document).ready(function(){        
            LoadUserList('LoadUserList',BuildTree);                           
         });
        
        var curFlag = 0;
        function swithEditPanel(flag)
        {
            var lastEle = document.getElementById("tab_"+curFlag);
            lastEle.className = "tab";
        
            curFlag = flag;                        
            document.getElementById("tab_"+flag).className = "selTab";           
            
        }
        
        
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
               document.getElementById("txtSender").value = "";
               document.getElementById("txtSenderHidden").value = "";
               treeview_unsel();               
        }
        
   
        
        function hideSelPanel()
        {
             var box = document.getElementById("selUserBox");
        
            box.style.display = "none";
        }
         function showSelPanel()
        {
            var ele = document.getElementById("txtSender") ;
            var pos = elePos(ele);
            
            var box = document.getElementById("selUserBox");
            box.style.left = pos.x+"px";
            box.style.top =pos.y+"px";
            box.style.display = "";
        }
        
        function HtmlEncode(input)
        {
           var converter=document.createElement("DIV");
           converter.innerText=input;
           var output=converter.innerHTML;
           converter=null;
           return output;
        }
        
        function SendInfo(obj)
{
     // document.getElementById("txtToList").value = userlist;
     // document.getElementById("seluseridlist").value = useridlist
   
    var title =HtmlEncode( document.getElementById("txtTitle").value);
    var content =HtmlEncode(document.getElementById("txtContent").value);
    var ids = document.getElementById("txtSenderHidden").value ;
    
    if(document.getElementById("txtTitle").value + "" == "")
    {
        showInfo("请填写信息标题");
        return;
    }
     if(document.getElementById("txtTitle").value.length>50)
    {
        showInfo("标题 长度不能超过50字符");
        return;
    }
    
    
     if(document.getElementById("txtContent").value + "" == "")
    {
        showInfo("请填写信息内容");
        return;
    }
      if(document.getElementById("txtContent").value.length>500)
    {
        showInfo("建议内容 长度不能超过500字符");
        return;
    }
    
    if(ids + "" == "")
    {
        showInfo("请选择收件人");
        return;
    }
    
    var slAdviceType = document.getElementById("slAdviceType").value;
    var slDisplayName = document.getElementById("slDisplayName").value;
    
    
    
    var action ="AddItem";
    var prams = "title="+UrlEncode(title);
    prams += "&content="+UrlEncode(content);
     prams += "&IDList="+ids;
      prams += "&AdviceType="+slAdviceType;
     prams += "&DisplayName="+slDisplayName;
     
    obj.disabled = true;
    
    $.ajax({ 
            type: "POST",
            url: "../../../Handler/Personal/MessageBox/AdviceSend.ashx?action=" + action,
            dataType:'string',
            data: prams,
            cache:false,
            success:function(data) 
            {                          
                obj.disabled = false;
                
                var result = null;
                eval("result = "+data);
                
                if(result.result)                    
                {      
                      showInfo(result.data);
                      
                       document.getElementById("txtTitle").value = "";
                       document.getElementById("txtContent").value = "";
                       document.getElementById("txtSenderHidden").value = "";
                       document.getElementById("txtSender").value = "";
                       
                       treeview_unsel(); 
                      
                }else{                  
                       showInfo(result.data);               
                }                   
            },
            error:function(data)
            {
                obj.disabled = false;
                
                 showInfo(data.responseText);
            }
        });
    
    
}
    </script>

</head>
<body>
    <br />
    <div id="divPBackShadow" style="display: none">
        <iframe src="../../../Pages/Common/MaskPage.aspx" id="PBackShadowIframe" frameborder="0"
            width="100%"></iframe>
    </div>
    <form id="EquipAddForm" runat="server">
    <div id="popupContent">
    </div>
    <span id="Forms" class="Spantype"></span>
    <table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
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
                            新建个人建议
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" width="100%">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="6">
                        </td>
                    </tr>
                </table>
                <table style="display: none;" width="99%" border="0" align="center" cellpadding="0"
                    cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="28" bgcolor="#FFFFFF">
                            <div id="infoTip" style="margin-left: 110px; color: Red;">
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td height="20" class="td_list_title">
                            <table width="100%" border="0" cellspacing="0" cellpadding="3">
                                <tr>
                                    <td class="style3">
                                        <img id="btn_save" visible="false" src="../../../images/Button/Bottom_btn_save.jpg"
                                            onclick="SendInfo(this)" style="cursor: pointer;" />
                                    </td>
                                    <td>
                                        <a id="alink_back" href="AdviceSended.aspx?ModuleID=10617">
                                            <img border="0" src="../../../images/Button/Bottom_btn_back.jpg" />
                                        </a>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="99%" border="0" align="left" cellpadding="2" cellspacing="1" bgcolor="#999999"
                    id="Tb_01">
                    <tr>
                        <td align="right" class="td_list_fields" style="width: 10%">
                            处理人<font color="red">*</font>
                        </td>
                        <td bgcolor="#FFFFFF" style="width: 23%">
                            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" style="width: 90%">
                                        <input type="text" id="txtSender" style="width: 100%" class="tdinput" readonly onclick="openRotoscopingDiv(false,'divPBackShadow','PBackShadowIframe');showSelPanel();" />
                                        <input type="hidden" id="txtSenderHidden" runat="server" />
                                    </td>
                                    <td>
                                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="showSelPanel('txtSenderHidden','txtSender',1)"
                                            id="Img2" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td height="20" align="right" class="td_list_fields">
                            建议类型<font color="red">*</font>
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <select id="slAdviceType">
                                <option value="1" selected>----建议----</option>
                                <option value="2">----意见----</option>
                            </select>
                        </td>
                        <td height="20" align="right" class="td_list_fields">
                            是否匿名<font color="red">*</font>
                        </td>
                        <td height="20" align="left" bgcolor="#FFFFFF">
                            <select id="slDisplayName">
                                <option value="0" selected>-----否-----</option>
                                <option value="1">-----是-----</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td height="20" align="right" class="td_list_fields" width="10%">
                            建议主题<font color="red">*</font>
                        </td>
                        <td height="20" bgcolor="#FFFFFF" colspan="5">
                            <input type="text" class="tdinput" id="txtTitle" width="99%" />
                        </td>
                    </tr>
                    <tr>
                        <td height="50" align="right" class="td_list_fields" valign="top" width="10%">
                            建议内容<font color="red">*</font>
                        </td>
                        <td height="50" bgcolor="#FFFFFF" colspan="5">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" class="tdinput" runat="server"
                                Rows="16" Columns="60" Height="300px" Width="99.8%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="28" bgcolor="#FFFFFF">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
    <div id="selUserBox" style="position: absolute; display: none; z-index: 100;">
        <ul id="typeListTab">
            <li id="tab_0" class="selTab" onclick="swithEditPanel(0);LoadUserList('LoadUserList',BuildTree)">
                全部</li>
            <li id="tab_1" class="tab" onclick="swithEditPanel(1);LoadUserList('LoadUserListWithDepartment',BuildTree)">
                部门</li>
            <li id="tab_2" class="tab" onclick="swithEditPanel(2);LoadUserList('LoadUserListWithGroup',BuildTree)">
                分组</li>
            <li style="display: inline;" onclick="hideSelPanel();closeRotoscopingDiv(false,'divPBackShadow');">
                <img style="margin-left: 60px; cursor: pointer;" align="absbottom" src="../../../Images/Pic/Close.gif" /></li>
        </ul>
        <div id="userList">
        </div>
        <div style="border: solid 1px #3366cc; padding: 5px; text-align: center; width: 200px;">
            <a href="#" onclick="clearSel();closeRotoscopingDiv(false,'divPBackShadow');">清空</a>
            &nbsp;&nbsp;<a href="#" onclick="hideSelPanel();closeRotoscopingDiv(false,'divPBackShadow');">确定</a>
        </div>
    </div>

    <script language="javascript" type="text/javascript">
    if(document.referrer.toLowerCase().indexOf("left.aspx") != -1)
    {
        document.getElementById("alink_back").style.display = "none";
    }
    </script>

</body>
</html>
