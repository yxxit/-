<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>    
    <script src="js/JQuery/jquery.min.js" type="text/javascript"></script>
     <script src="js/common/Common.js" type="text/javascript"></script>
    <script src="js/common/Check.js" type="text/javascript"></script>
   <link href="css/Loginnew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
         var Cookie = {
             set: function(name, value, min) {
                 var exp = new Date();
                 exp.setTime(exp.getTime() + min * 60 * 1000);
                 document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";


             },
             get: function(name) {

                 var exp = "(^|[\\s]+)" + name + "=([^;]*)(;|$)";
                 var arr = document.cookie.match(new RegExp(exp));

                 if (arr != null)
                     return unescape(arr[2]);
                 return null;
             },

             del: function(name) {
                 var exp = new Date();
                 exp.setTime(exp.getTime() - 10000);
                 var cval = this.get(name);
                 if (cval != null)
                     document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
             }
         } 
function GetEventCode(evt)
{
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
    
    return evt.keyCode || evt.charCode;
}

function checkKey()
{
    var code ;
    if(document.all)
      {
        code = event.keyCode;
      }else{
        code = GetEventCode();
      }
        
      if(code >= 97 && code <= 123)
      {
        if(document.all)
        {
         event.keyCode = code-32;
        }else{
         evt.charCode =  code-32;
        }
      }                
} 
 </script>
 
 <style type="text/css">
 

 </style>
</head>
<body>
    <form id="form1" runat="server" class="login-form">
    <asp:HiddenField ID="HidKey" Value="0" runat="server" />
    <input type="hidden" id="hidCustNo" value="" runat="server" />
    <div id="divDefault" runat="server">
    </div>
    <div runat="server" id="divVerConert">
     
      
       <div class="w" >

    <div id="logo" style=" text-align:center">
        <img src="Images/logoTest.png" height="102" width="509" alt="金泰" />
      
        <b></b>
    </div>
    
    
    、

    </div>
<div id="entry" class=" w1" style=" margin-top:5px;">


<div id="bgDiv" class="mc ">
<img src="Images/left.jpg"  width="300"  height="235" style=" margin-top:20px"/>

<div class="form">
<div class="item fore1"">

<div class="item-ifo" >

<p>
   用户名：



<input name="txtUserID" id="txtUserID" class="text" type="text" onblur="chkCompany();"/>
    <span style="float:right; margin-top:-23px;" class="sname"></span>
</p>
      

</div>

</div>


<div class="item fore2"">



<div class="item-ifo">
<p>

 密&nbsp;&nbsp;&nbsp;&nbsp;码：

<input id="txtPassword" class="text"  name="txtPassword" type="password" maxlength="16" onblur="chkCompany();" />
    <span style="float:right" class="spass"></span>
    
    <div class="i-name ico"></div>

 </p> 
</div>

</div>



<div class="item fore3"">



<div class="item-ifo">
<p>

 帐&nbsp;&nbsp;&nbsp;&nbsp;套：

<input id="txtCompanyCD" class="text"  name="txtCompanyCD" type="text" runat="server" />

 
    <span style="float:right" class="spass"></span>
    
    <div class="i-name ico"></div>

 </p> 
</div>

</div>


<div class="item fore4">


<div class="item-ifo">
<p>
验证码：
<input id="txtCheckCode" class="text" maxlength="4"   name="txtCheckCode" type="text" style="width:80px" /> &nbsp;<img src="CheckCode.aspx" name="imgCheckCode" align="middle" id="imgCheckCode"  /> <a onclick="ReloadCheckCode();" href="#">换一张</a>
                      
 
</p>


</div>

</div>
<p style=" margin-left:60px">
<input name="chkUsername" id="chkUsername" type="checkbox" value="checkbox" checked="checked"/>记住用户名
<input name="chkPassword" id="chkPassword" type="checkbox" value="checkbox" checked="checked"/>记住密码

</p>

<div class="item login-btn2013" style="margin-top:10px; margin-left:50px">
      <img src="Images/denglu.jpg" name="btnLogin" align="absmiddle"
      id="btnLogin" style="cursor: pointer" onclick="LoginSubmit();" />

</div>
</div>
</div>
</div>
<div id="footer" style=" text-align:center"> &nbsp;&nbsp;版权所有：北京金泰恒业燃料有限公司 &nbsp;2013-2014</div>
</div> 
    </form>        
</body>
</html>
<script src="js/login.js" type="text/javascript"></script>