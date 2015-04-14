
function ReadSNArray(reader,sp)
{   

    var s = "";
    
    try
    {
        s=reader.ReadUsbSn();
    }catch(e){
        return "";
    }
    
	if(s + "" == "")
	{		
		return "";
	}


	var usbList = s.split("\\\\?\\usb");
	var usbStringList = "";
	for(var i=1;i<usbList.length;i++)
	{		
		var s2 = usbList[i].substring(1,usbList[i].length-39);		
		var s1 = s2.replace("vid_","").replace("pid_","").replace(/\#/g,"").replace(/\&/g,"");
		if(usbStringList != "")
			usbStringList  += sp;
		usbStringList  += s1;
	}
	
	return usbStringList;
	
	
}

/*
* 提交验证
*/
function LoginSubmit() {
var USBSNReader  = document.getElementById("USBSNReaderOCX1");
var snlist = ReadSNArray(USBSNReader,",");
if(snlist == "")
{
     //alert("没有读取到USBKEY");
    // return;
}

//alert(snlist);
    
    var errorMsg = "";
//    var keyflag = document.getElementById("HidKey").value;
//    if(keyflag == "0")
//    {
//        //errorMsg += "非法登录：错误原因可能如下\t\n\n\n 1、服务器加密锁不存在\n\n 2、服务器加密锁非法\n\n 3、系统加密锁配置错误\n\n";
//        errorMsg += "未找到加密锁的秘钥配置\n\n";
//    }
//    
//    if(keyflag == "2")
//    {
//       errorMsg += "加密锁不存在\n\n"; 
//    }
//    
//    if(keyflag == "3")
//    {
//        errorMsg +="加密锁非法，或加密锁秘钥配置不正确\n\n";
//    }
//    if(keyflag == "4")
//    {
//        errorMsg +="加密锁秘钥未配置\n\n";
//    }
//    
    UserID = document.getElementById("txtUserID").value;
    //用户名必须输入
    if (UserID == "")
    {
        errorMsg += "请输入用户名\n";
    }
    
    if(!UserID.match(/^[\u4e00-\u9fa5A-Za-z0-9]+$/))
    {
        errorMsg += "不合法的账号名称，只能包含汉字，英文，数字";
    }
    
    
    Password = document.getElementById("txtPassword").value;
    //密码必须输入
    if (Password == "")
    {
        errorMsg += "请输入密码\n";
    }
    CheckCode = document.getElementById("txtCheckCode").value;
    //验证码必须输入
    if (CheckCode == "")
    {
        errorMsg += "请输入验证码";
    }
    else if (CheckCode.length != 4)
    {
        errorMsg += "请输入四位的验证码";
    }
    //如有项目未输入，输出错误信息
          
    if (errorMsg != "")
    {
        alert(errorMsg);
        return;
    }
    
  var action="Logindata";
  var position="1";     
    //拼写请求URL参数
    var postParams = "UserID=" + UserID + "&Password=" + Password + "&CheckCode=" + CheckCode+"&snlist="+snlist+"&action="+action+"&position="+position;
//var login_start_date = new Date();
//var p=window.open("about:blank","_self");
    $.ajax({
        type: "POST",
        url: "Handler/Login.ashx?" + postParams,
        dataType: 'string', //返回json格式数据
        data: '',
        cache: false,
        success: function(data) {

            // var login_end_date = new Date();

            //  alert(login_end_date-login_start_date);//12 625

            // alert(data);       
            var checkError = data.split("|");
            if (checkError.length == 2) {
                document.getElementById(checkError[0]).focus();
                alert(checkError[1]);
            } else if (data.length > 0) {
                alert("登陆失败，请联系管理员");
            }
            else {
                //                window.open('Main.aspx','','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes');
                //                
                //                window.opener=null;
                //                window.open("","_self");
                //                window.close();


                //登陆成功,记住用户名和密码
                //记录用户名
                if (document.getElementById("chkUsername").checked) {
                    Cookie.set("chkJthyUsername", UserID, 60 * 24 * 14);
                } else {
                    Cookie.del("chkJthyUsername");
                }

                //记录用户密码
                if (document.getElementById("chkPassword").checked) {
                    Cookie.set("chkJthyPassword", Password, 60 * 24 * 14);
                } else {
                    Cookie.del("chkJthyPassword");
                }

                //登录进入，打上登录标记
                Cookie.set("isLogin","1",60 * 24 * 14);
                
//window.open('Main.aspx','_self','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes'); 
               //    SetWindowOpen(1);
         location.replace("Main.aspx");
         // window.location.href="Main.aspx"; 
       //          window.open("Main.aspx", "big","fullscreen=yes");
  // openwin("demo.aspx");


            }
        },
        error: function(r) {
            alert(r.responseText);
            alert("登陆失败，请联系管理员");
        }
    }); 
    // window.open("demo.aspx","big","fullscreen=yes");
     //window.open("demo.aspx?"+postParams,"big","fullscreen=yes");
}

/*
* 客户登录提交验证
*/
function LoginSubmit_Cust() {
var USBSNReader  = document.getElementById("USBSNReaderOCX1");
var snlist = ReadSNArray(USBSNReader,",");
if(snlist == "")
{
     //alert("没有读取到USBKEY");
    // return;
}

//alert(snlist);
    
    var errorMsg = "";
//    var keyflag = document.getElementById("HidKey").value;
//    if(keyflag == "0")
//    {
//        //errorMsg += "非法登录：错误原因可能如下\t\n\n\n 1、服务器加密锁不存在\n\n 2、服务器加密锁非法\n\n 3、系统加密锁配置错误\n\n";
//        errorMsg += "未找到加密锁的秘钥配置\n\n";
//    }
//    
//    if(keyflag == "2")
//    {
//       errorMsg += "加密锁不存在\n\n"; 
//    }
//    
//    if(keyflag == "3")
//    {
//        errorMsg +="加密锁非法，或加密锁秘钥配置不正确\n\n";
//    }
//    if(keyflag == "4")
//    {
//        errorMsg +="加密锁秘钥未配置\n\n";
//    }
//    
    UserID = document.getElementById("txtUserID").value;
    //用户名必须输入
    if (UserID == "")
    {
        errorMsg += "请输入用户名\n";
    }
    
    if(!UserID.match(/^[\u4e00-\u9fa5A-Za-z0-9]+$/))
    {
        errorMsg += "不合法的账号名称，只能包含汉字，英文，数字";
    }
    
    
    Password = document.getElementById("txtPassword").value;
    //密码必须输入
    if (Password == "")
    {
        errorMsg += "请输入密码\n";
    }
    CheckCode = document.getElementById("txtCheckCode").value;
    //验证码必须输入
    if (CheckCode == "")
    {
        errorMsg += "请输入验证码";
    }
    else if (CheckCode.length != 4)
    {
        errorMsg += "请输入四位的验证码";
    }
    //如有项目未输入，输出错误信息
          
    if (errorMsg != "")
    {
        alert(errorMsg);
        return;
    }
    
  var action="Logindata";
  var position="2";       
    //拼写请求URL参数
    var postParams = "UserID=" + UserID + "&Password=" + Password + "&CheckCode=" + CheckCode+"&snlist="+snlist+"&action="+action+"&position="+position;
//var login_start_date = new Date();
//var p=window.open("about:blank","_self");
    $.ajax({
        type: "POST",
        url: "Handler/Login.ashx?" + postParams,
        dataType: 'string', //返回json格式数据
        data: '',
        cache: false,
        success: function(data) {

            // var login_end_date = new Date();

            //  alert(login_end_date-login_start_date);//12 625

            // alert(data);       
            var checkError = data.split("|");
            if (checkError.length == 2) {
                document.getElementById(checkError[0]).focus();
                alert(checkError[1]);
            } else if (data.length > 0) {
                alert("登陆失败，请联系管理员");
            }
            else {
                //                window.open('Main.aspx','','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes');
                //                
                //                window.opener=null;
                //                window.open("","_self");
                //                window.close();


                //登陆成功,记住用户名和密码
                //记录用户名
                if (document.getElementById("chkUsername").checked) {
                    Cookie.set("chkJthyUsername", UserID, 60 * 24 * 14);
                } else {
                    Cookie.del("chkJthyUsername");
                }

                //记录用户密码
                if (document.getElementById("chkPassword").checked) {
                    Cookie.set("chkJthyPassword", Password, 60 * 24 * 14);
                } else {
                    Cookie.del("chkJthyPassword");
                }

                //登录进入，打上登录标记
                Cookie.set("isLogin","1",60 * 24 * 14);
                
//window.open('Main.aspx','_self','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes'); 
               //    SetWindowOpen(1);
         location.replace("pages/office/custweborder/Main.aspx");
         // window.location.href="Main.aspx"; 
       //          window.open("Main.aspx", "big","fullscreen=yes");
  // openwin("demo.aspx");


            }
        },
        error: function(r) {
            alert(r.responseText);
            alert("登陆失败，请联系管理员");
        }
    }); 
    // window.open("demo.aspx","big","fullscreen=yes");
     //window.open("demo.aspx?"+postParams,"big","fullscreen=yes");
}

function openNewWindow()
{
 var action="Logindata";
 var USBSNReader  = document.getElementById("USBSNReaderOCX1");
  var snlist = ReadSNArray(USBSNReader,",");
 
  var UserID = document.getElementById("txtUserID").value;
  var Password = document.getElementById("txtPassword").value;
  var CheckCode = document.getElementById("txtCheckCode").value;
  var remeberName=0;
  var remeberPwd=0;
  var checkRs=1;
   if (document.getElementById("chkUsername").checked) {
    remeberName=1;
   }
     if (document.getElementById("chkPassword").checked) {
     remeberPwd=1;
     }
    //拼写请求URL参数
    var postParam = "para=" + UserID + "|" + Password + "|" + CheckCode+"|"+snlist+"|"+action+"|"+remeberName+"|"+remeberPwd;
  // window.open("demo.aspx","big","fullscreen=yes");
 // window.open("demo.aspx?"+postParam,"big","fullscreen=yes");
 // window.close("login.aspx");
 if(CheckCode=='')
 {
 alert("请输入验证码");
 checkRs=0;
 }
  if (CheckCode.length != 4)
    {
       alert("请输入四位的验证码");
         checkRs=0;

    }
 if( checkRs==1)
   window.open('demo1.aspx?'+postParam,'big','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes'); 
//   window.opener=null;
//   window.open("","_self");                    
//   window.close();        
}

function openwin(url) {
var a = document.createElement("a");
a.setAttribute("href", url);
a.setAttribute("target", "_blank");
a.setAttribute("id", "openwin");
document.body.appendChild(a);
a.click();
}

 function call(htmlurl)
 { var   newwin=window.open(htmlurl,"airWin","top=0,left=0,toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width="+screen.width+",height="+(screen.height-70));//大小   
  newwin.focus();
   return false;
 }



function SetWindowOpen(type) 
{
    var objChild;                          
    var reWork = new RegExp('object','gi'); // Regular expression
    try 
    {
        objChild = window.open('Main.aspx','airWin','height='+(screen.height-50)+',width='+screen.width+',screenX=0,screenY=0,left=0,top=0,resizable=yes,scrollbars=yes'); 
        window.opener=null;
        window.open("","_self");                    
        window.close();        
    }
    catch(e){}

    if(!reWork.test(String(objChild)))
    {
        alert('您的IE设置了弹出窗口屏蔽功能，请通过IE设置允许本系统弹出窗口。\n\n设置步骤:IE菜单栏的“工具”---“弹出窗口阻止程序”---点击“关闭弹出窗口阻止程序”');
        window.location.href='login.aspx?flag=1';
    }      
}

/*
* 重新获取验证码
*/
function ReloadCheckCode() {
    var checkCode = document.getElementById("imgCheckCode");
    var rand = Math.random();
    checkCode.src = "CheckCode.aspx?randnum=" + rand;
}

document.onkeydown = KeyDown;

/*
*
*/
function KeyDown()
{
    var event = arguments[0]||window.event;  
    
    //获取按下键的值
    var keyCode = event.charCode||event.keyCode;
    
    //按回车时提交
    if (keyCode == 13)
    {
     LoginSubmit();
      //  openNewWindow();
    }
}

$(document).ready(function() {

    if (Cookie.get("chkJthyUsername") != null) {
        document.getElementById("txtUserID").value = Cookie.get("chkJthyUsername");
        document.getElementById("chkUsername").checked = true;
    }
    else
    {
    document.getElementById("chkUsername").checked = false;
    }

    if (Cookie.get("chkJthyPassword") != null) {
        document.getElementById("txtPassword").value = Cookie.get("chkJthyPassword");
        document.getElementById("chkPassword").checked = true;

    }
     else
    {
    document.getElementById("chkPassword").checked = false;
    }
    chkCompany();
}); 


function chkCompany()
{
    var CheckCode = document.getElementById("txtCheckCode").value;
    var UserID = document.getElementById("txtUserID").value;
    var Password = document.getElementById("txtPassword").value;
    var action="chkCompanyCD";
    if (UserID!="" && Password!="")
    {
         //拼写请求URL参数
        var postParams = "UserID=" + UserID + "&Password=" + Password +"&action="+action;
        $.ajax({
            type: "POST",
            url: "Handler/Login.ashx?" + postParams,
            dataType: "json", //数据格式:JSON
            data: '',
            cache: false,
            success:function(msg)
            {   
                if (msg.data[0].NameCn != null && msg.data[0].NameCn != "")
                { 
                
                    if(msg.data[0].UsedStatus=="1")
                    {
                        document.getElementById("txtCompanyCD").value=msg.data[0].NameCn;
                        document.getElementById("btnLogin").disabled=false;
                    }
                    else
                    {
                        document.getElementById("txtCompanyCD").value=msg.data[0].NameCn+"已停用！";
                        document.getElementById("btnLogin").disabled=true;
                    }  
                }
                else
                {
                    document.getElementById("txtCompanyCD").value="无帐套信息！"
                    document.getElementById("btnLogin").disabled=true;
                }    
          
            },
            error:function(r)
            {
                alert(r.responseText);
                alert("登陆失败，请联系管理员");
            }
        });        
    
    }
}

function AlertProductMsg() {
    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    //屏幕可用工作区高度： window.screen.availHeight;
    //屏幕可用工作区宽度： window.screen.availWidth;
    //网页正文全文宽：     document.body.scrollWidth;
    //网页正文全文高：     document.body.scrollHeight;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    //创建遮罩背景
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'ProductBigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#fff";
    maskObj.style.filter = "Alpha(opacity=70);";
    maskObj.style.opacity = "0.3";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "100";
    document.body.appendChild(maskObj);
}

// 弹出注册页面
function ShowRegPage()
{
    document.getElementById("divregpage").style.display = "block";
    document.getElementById('divzhezhao1').style.display = 'inline';
    AlertProductMsg();
}
function closeRegPage()
{
    document.getElementById("divregpage").style.display = "none";
    var ProductBigDiv = document.getElementById("ProductBigDiv");
    document.body.removeChild(ProductBigDiv);
    document.getElementById('divzhezhao1').style.display = 'none';
}

//检验用户名是否存在
function CheckUserNum()
{  
    var userid=document.getElementById('txtUserName').value;
    if(userid.length!=0)
    {
      var tablename="UserInfo";
      var columname="UserID";
    $.ajax({ 
              type: "POST",
              url: "Handler/UserCheck.ashx?strcode="+userid+"&colname="+columname+"&tablename="+tablename,
              //data:'strcode='+$("#txtEquipCode").val()+'&tablename='+tablename,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                 // AddPop();
              }, 
            error: function() 
            {
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
          success:function(data) 
            { 
                if(data.sta!=1) 
                { 
//                    popMsgObj.ShowMsg(data.data);
                     document.getElementById("lbl_username").innerHTML=data.data;
                     document.getElementById("lbl_username").style.color='red'; 
                }   
                else
                {
                    document.getElementById("lbl_username").innerHTML="该用户名可以使用！";
                    document.getElementById("lbl_username").style.color='blue'; 
                }      
            } 
           });
    }
  
}
//检验密码
function CheckPWD()
{
    var txtPassword = document.getElementById("txtPassword1").value;//密码
      if(txtPassword=="")
    {
       document.getElementById("lbl_password1").innerHTML="请输入密码！";
       document.getElementById("lbl_password1").style.color='red'; 
    }
    else 
    { 
        if(strlen(txtPassword)>10||strlen(txtPassword)<6)
        {
            document.getElementById("lbl_password1").innerHTML="密码位数应处于6-10位之间！";
            document.getElementById("lbl_password1").style.color='red'; 
        }
        else
        {
            document.getElementById("lbl_password1").innerHTML="";
        }
        if(!txtPassword.match(/^[0-9a-zA-Z]+$/))
        { 
           document.getElementById("lbl_password1").innerHTML="密码必须是字母、数字的组合！";
           document.getElementById("lbl_password1").style.color='red'; 
        }
         else
        {
            document.getElementById("lbl_password1").innerHTML="";
        }
     
    }
}
//检验确认密码
function CheckRePWD()
{
    var txtPassword = document.getElementById("txtPassword1").value;//密码
    var txtRePassword = document.getElementById("txtPasswordConfirm").value;//重复密码
    if(txtPassword=="")
    {
       document.getElementById("lbl_password1").innerHTML="请输入密码！";
       document.getElementById("lbl_confirmpassword").innerHTML="请先输入密码，再输入确认密码！";
           document.getElementById("lbl_password1").style.color='red'; 
           document.getElementById("lbl_confirmpassword").style.color='red'; 
    }
    else
    {
         if(txtRePassword=="")
         {
            document.getElementById("lbl_confirmpassword").innerHTML="请输入确认密码！";
                document.getElementById("lbl_confirmpassword").style.color='red'; 
         }
          else
         {
             if(txtPassword!=txtRePassword)
            {
              document.getElementById("lbl_confirmpassword").innerHTML="请确认两次输入的密码是否一致！";
                  document.getElementById("lbl_confirmpassword").style.color='red'; 
            }
            else
            {
              document.getElementById("lbl_confirmpassword").innerHTML="密码一致！";
                  document.getElementById("lbl_confirmpassword").style.color='blue'; 
            }
         }
    }
}

//提交注册
function RegSubmit()
{
   if(!CheckInput())
   {
     return;
   } 
   var CustName=document.getElementById("txtCompanyName").value;
   var CustNo=document.getElementById("hidCustNo").value;
   var CustAddr=document.getElementById("txtCompanyAddr").value;
   var UserName=document.getElementById("txtUserName").value;
   var PassWord=document.getElementById("txtPassword1").value;
   var ConfirmPassword=document.getElementById("txtPasswordConfirm").value;
   var RealName=document.getElementById("txtRealName").value;
   var LinkAddr=document.getElementById("txtLinkAddr").value;
   var WorkTel=document.getElementById("txtWorkTel").value;
   var Mobile=document.getElementById("txtMobile").value;
   var Email=document.getElementById("txtEmail").value;
   var Sex=document.getElementById("SeleSex").value;
   var action="insertreginfo";
    $.ajax({ 
              type: "POST",
              url: "Handler/CustReg.ashx?CustName="+escape(CustName)+"&CustAddr="+escape(CustAddr)+"&UserName="+escape(UserName)+"&PassWord="+PassWord+"&LinkName="+escape(RealName)+
                     "&LinkAddr="+escape(LinkAddr)+"&WorkTel="+escape(WorkTel)+"&Mobile="+escape(Mobile)+"&Email="+escape(Email)+"&Sex="+escape(Sex)+"&Action="+escape(action)+"&CustNo="+escape(CustNo),
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
            error: function() 
            {
            }, 
            success:function(data) 
            { 
                if(data.sta==1) 
                { 
                   document.getElementById("lbl_result").innerHTML="注册成功,待审核！";
                   document.getElementById("lbl_result").style.color='blue'; 
                }  
                else
                {
                  document.getElementById("lbl_result").innerHTML="注册失败,请检查！";
                   document.getElementById("lbl_result").style.color='red'; 
                }
            } 
     });
}

function CheckInput()
{
    var isFlag = true;
     var txtCompanyName = trim(document.getElementById("txtCompanyName").value);//CompanyName
    var txtUserName = trim(document.getElementById("txtUserName").value);//用户编号
    var txtRealName = trim(document.getElementById("txtRealName").value);//用户姓名
    var txtPassword = trim(document.getElementById("txtPassword1").value);//密码
    var txtRePassword = trim(document.getElementById("txtPasswordConfirm").value);//重复密码

    if(txtCompanyName=="")
    {
        isFlag = false;
        document.getElementById("lbl_companyname").innerHTML="请输入公司名称！";
        document.getElementById("lbl_companyname").style.color='red'; 
    }
    else
    {
      isFlag=CheckCustCompany(txtCompanyName);
    }
    if(txtUserName=="")
    {
       isFlag = false;
      document.getElementById("lbl_username").innerHTML="请输入用户名！";
      document.getElementById("lbl_username").style.color='red'; 
    }
     else
    {
       document.getElementById("lbl_username").innerHTML="";
    }
    if(txtPassword=="")
    {
        isFlag = false;
       document.getElementById("lbl_password1").innerHTML="请输入密码！";
       document.getElementById("lbl_password1").style.color='red'; 
    }
     else
    {
       document.getElementById("lbl_password1").innerHTML="";
    }
   
    if(txtRePassword=="")
    {
        isFlag = false;
        document.getElementById("lbl_confirmpassword").innerHTML="请输入确认密码！";
        document.getElementById("lbl_confirmpassword").style.color='red'; 
    }
     else
    {
       document.getElementById("lbl_confirmpassword").innerHTML="";
    }
     if(txtRealName=="")
    {
       isFlag = false;
      document.getElementById("lbl_realname").innerHTML="请输入真实姓名！";
      document.getElementById("lbl_realname").style.color='red'; 
    }
     else
    {
       document.getElementById("lbl_realname").innerHTML="";
    }
    return isFlag;
}
//reset reg info
function ClearTxt()
{
   document.getElementById("txtCompanyName").value="";
   document.getElementById("txtCompanyAddr").value="";
   document.getElementById("txtUserName").value="";
   document.getElementById("txtPassword1").value="";
   document.getElementById("txtPasswordConfirm").value="";
   document.getElementById("txtRealName").value="";
   document.getElementById("txtLinkAddr").value="";
   document.getElementById("txtWorkTel").value="";
   document.getElementById("txtMobile").value="";
   document.getElementById("txtEmail").value="";
   document.getElementById("SeleSex").selectedIndex=0;
}

//fill custinfo
function fnSelectCust(no,name,addr)
{
   document.getElementById("txtCompanyName").value=name;
   document.getElementById("hidCustNo").value=no;
   if(addr!="")
   {
   document.getElementById("txtCompanyAddr").value=addr;
   }
}

//判断是否存在注册时填写的客户名称
function CheckCustCompany(custname)
{
    var isexist=true;
//    var custname=document.getElementById("txtCompanyName").value;
    if(custname.length!=0)
    {
      var tablename="Customer";
      var columname="cCusName";
    $.ajax({ 
              type: "POST",
              async:false,
              url: "Handler/CustCompanyCheck.ashx?strname="+escape(custname)+"&colname="+columname+"&tablename="+tablename,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
            error: function() 
            {
            }, 
            success:function(data) 
            { 
                if(data.sta!=1) 
                { 
                    document.getElementById("lbl_companyname").innerHTML="公司名称不存在，请重新输入！";
                    document.getElementById("lbl_companyname").style.color='red'; 
                       isexist=false;
                }  
                else
                {
                  document.getElementById("lbl_companyname").innerHTML="";
                  isexist=true;
                } 
            } 
           });
    }
    return isexist;
}

