<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>
<%@ Register Src="UserControl/TopMenuCell.ascx" TagName="TopMenuCell" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title> 
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    
    

    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
<%--    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>--%>
    <script src="js/JQuery/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="js/JQuery/jquery.messager.js" type="text/javascript"></script>
    <script src="js/common/Common.js" type="text/javascript"></script>
    <script src="js/common/Check.js" type="text/javascript"></script>
    <script src="js/ChangePsd.js" type="text/javascript"></script>
    <script src="js/SystemAlert.js" type="text/javascript"></script>
    <script src="js/Main.js" type="text/javascript"></script>
    
    
    <script type="text/javascript" src="js/swfobject.js"></script>
    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <!--jquery 多页签开始 -->
    <script type="text/javascript" src="js/jquery.jerichotab.js"></script>
<%--    <link rel="Stylesheet" href="css/common.css" />--%>
    <link rel="Stylesheet" href="css/jquery.jerichotab.css" />
    <!--jquery 多页签结束 -->
    <link rel="stylesheet" type="text/css" href="css/jt_default.css" />

    
    
    <!--20130118 DYG添加 设置页面显示-->
    <script type="text/javascript">
    
    
        var Zxl100Path = '';

        var initMenuStatus = 0;
        function switchMenu() 
        {
            if (initMenuStatus == 0) 
            {
                var html = '<img src="images/' + Zxl100Path + 'Main_left_close.gif"   ALIGN=ABSBOTTOM  border="0" />';
                document.getElementById("link_menuSwitch").innerHTML = html;
                var oldwidth = parent.$("#leftTD").width();
               $("#leftTD").width(5);
              $("#Left").width(0);
              $("#mef").css("margin-left","0");
                var newwidth = parent.$("#leftTD").width();
              $("#jericho_tab").width(parent.$("#jericho_tab").width() - newwidth + oldwidth);
                $("#tab_pages").width(parent.$("#tab_pages").width() - newwidth + oldwidth);
               $.fn.jerichoTab.resize();
                initMenuStatus = 1;
            }
            else 
            {
                var html = '<img src="images/' + Zxl100Path + 'Main_left_open.gif"   ALIGN=ABSBOTTOM  border="0" />';
                document.getElementById("link_menuSwitch").innerHTML = html;
                var oldwidth = parent.$("#leftTD").width();
              document.getElementById("leftTD").style.width = "184px";
              document.getElementById("Left").style.width = "184px";
                var newwidth = parent.$("#leftTD").width();
              $("#jericho_tab").width(parent.$("#jericho_tab").width() - newwidth + oldwidth);
              $("#tab_pages").width(parent.$("#tab_pages").width() - newwidth + oldwidth);
               $.fn.jerichoTab.resize();
                initMenuStatus = 0;
                 $("#mef").css("margin-left","168");
            }

        }

    
    
    
    
    
    
    
    
    
    function findDimensions() //函数：获取尺寸   
    {    //获取窗口宽度   
      var winWidth = 0;    var winHeight = 0;   
      if (window.innerWidth)    
        winWidth = window.innerWidth;   
      else if ((document.body) && (document.body.clientWidth))    
               winWidth = document.body.clientWidth;    //获取窗口高度    
      if (window.innerHeight)   
         winHeight = window.innerHeight;   
       else if ((document.body) && (document.body.clientHeight))    
                winHeight = document.body.clientHeight;    //通过深入Document内部对body进行检测，获取窗口大小    
       if (document.documentElement  && document.documentElement.clientHeight &&    document.documentElement.clientWidth)  
        {   
          winHeight = document.documentElement.clientHeight;  
          winWidth = document.documentElement.clientWidth;   
        }    //结果输出至两个文本框   
          document.getElementById("Left").style.height= winHeight*1-96+"px"; 
          document.getElementById("Left").style.width=185+"px";  
          window.frames["Left"].document.getElementById("menuPanel").style.height= winHeight*1-130+"px"; 
          $(window.frames["Left"].document).find("#menuPanel").css("overflow","hidden");
          document.getElementById("jerichotabiframe_main").style.height=winHeight*1-128+"px";
      //  alert( $("#Left").css("height")+" "+winHeight);
       } 

    function getFrame()
    {
     alert($(window.frames["Left"].document).find("#menuPanel").height())
    }
    </script>
    <!----------end---------->
    <script language="javascript" type="text/javascript">
        var CiframLoadCount = 0;
        function hideLoading() 
        {
            document.getElementById("loadingPanel").style.display = "none";
            var ids = "topTD,leftTD,Maintd,footBar";
            var idlist = ids.split(',');
            for (var i = 0; i < idlist.length; i++) 
            {
                document.getElementById(idlist[i]).style.display = "";
            }
        } 
        
        var iframeCnt = 2;        
        function checkIframeLoadedCnt()
        {
            iframeCnt--;
            if(iframeCnt==0)
            {
                hideLoading();
            }
        }        
        function MSGshow()
        {
            try
            {
                MSG.show();
            }   
            catch(eee)
            {
            }
        }
    </script>
    <script type="text/javascript">
    
    
    
    
    
        var framePageWidth;
        var iframe_height;
        var jericho = {
            showLoader: function() {
                $('#divMainLoader').css('display', '');
            },
            removeLoader: function() {
                $('#divMainLoader').css('display', 'none');
            },
            buildTabpanel: function() {
                $.fn.initJerichoTab({
                    renderTo: '#Maintd',
                    uniqueId: 'myJerichoTab',
                    contentCss: { 'height': $('#Maintd').height() - 50 },
                    tabs: [{
                        tid: "main",
                        title: '我的工作台',
                        closeable: false,
                        iconImg: 'images/jerichotab.png',
                        data: { dataType: 'iframe', dataLink: 'DeskTop.aspx' }
                        }],
                        activeTabIndex: 1,
                        loadOnce: true
                    });
                }
            }
            $().ready(function() {
                var left_height = $('#Left').height();
                var left_width = 189;//DYG 20130118 修改原语句 left_width=$('#Left').width();

                jericho.showLoader();
                var w = $(document).width();
                var h = $(document).height();
                framePageWidth = w - left_width;
                iframe_height = left_height - 35;
   
                jericho.buildTabpanel();

                $('#jericho_tab').css('width', ((w - left_width) + 'px'));
                $('#jerichotab_main').loadData();

                jericho.removeLoader();
            })
            $(window).resize(function() {
                var w = $(document).width();
             
                var leftTD_width = $(leftTD).width();
                //--DYG-20130118-用于当浏览器大小调整后，同样调整jericho_tab元素的样式--//
               $('#jericho_tab').css('width', ((w - 189) + 'px'));
               //--------//
                $('#Maintd').css({ width: w - leftTD_width });
            })
            function addTab(obj, tabid, tabTitle, link) {
                var tid, title, dataLink;
                if (tabid) {
                    tid = tabid;
                }
                else {
                    tid = $(obj).attr('tabid');
                }

                if (tabTitle) {
                    title = tabTitle;
                }
                else {
                    title = $(obj).attr('tabTitle');
                }

                if (link) {
                    dataLink = link;
                }
                else {
                    dataLink = $(obj).attr('dataLink');
                }

                $.fn.jerichoTab.addTab({
                    tid: tid,
                    title: title,
                    closeable: true,
                    iconImg: 'images/jerichotab.png',
                    data: {
                        dataType: "iframe",
                        dataLink: dataLink
                    }
                }).showLoader().loadData();
            }
    </script>

   
    <style type="text/css">
        .style1
        {
            color: #000;
            background-color: #fff;
            width: 99px;
    /*background-color:#dfebf8;*/
            text-align: right;/*文本右对齐 2010-10-15  添加*/
        }
    </style>

   
</head>
<body style="width:100%;height:auto;background-color:#FFFFFF">
    <input type="hidden" id="txtIsFresh" />
    <div id="loadingPanel" style="display: block; z-index: 10; margin: 0 auto; margin-top: 200px;
        text-align: center; width: 60%; background: #f1f1f1; color: Blue;">
        <table align="center">
            <tr>
                <td>
                    正在加载中...
                </td>
                <td>
                    <img src="Images/clock.gif" />
                </td>
            </tr>
        </table>
    </div>
    <form id="form1" runat="server">     
    <table style="width:100%;height:100%;" border="0" cellspacing="0" cellpadding="0"  >
<tr>
            <td colspan="2" valign="top" height="60" style="display: none;" id="topTD">
                <iframe src="Top.aspx" name="Top" width="100%" height="100%" frameborder="no" border="0"
                    marginwidth="0" marginheight="0" scrolling="no" id="Top"></iframe>
            </td>
        </tr>
        <tr>
            <td valign="top" id="leftTD" style="width: 184px;display: none">
                <iframe src="Left.aspx" name="Left" scrolling="no" id="Left">
               
         
            
                </iframe>
         
          <div id="mef" style="margin-left:168px; width:auto;   position:relative; margin-top:-280px;">
           <a style="height: 18pt; color: #ffffff;" title="点击收起或展开左侧" id="link_menuswitch" onclick="switchMenu();">&nbsp;&nbsp;<img src="images/main_left_open.gif"
                                                align="left" border="0" /></a>
         </div> 
    
            
            </td>
  
     
            
            <td valign="top" style="width: 100%; display: none;  background-image:url(images/main/body_bg.jpg);" id="Maintd"
                align="left">
                <div id="divMainLoader">
                   Loading...</div>
            </td>
        </tr>
        <tr id="footBar" style="display: none;">
            <td height="31" colspan="2" valign="bottom" style="width:100%; background-image:url(images/bottom.jpg);">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="TABLE-LAYOUT: fixed;" >
            <tr>                      
                        <td  width="45%" >
                         <table>
                          <tr>
                            <td> &nbsp; &nbsp;<img src="images/Button/Bottom_btn_backindex.gif" onmouseover="this.src='images/Button/Bottom_btn_backindex_2.png'" onmouseout="this.src='images/Button/Bottom_btn_backindex.gif'" style="cursor: pointer;" onclick="addTab(null,'main','我的工作台','DeskTop.aspx');" /></td>
                            <td><a href="LogOut.aspx"><img border="0" src="images/Button/Bottom_btn_Ckout.gif" style="cursor: pointer;" width="49" height="25" /></a></td>
                         
                             <td>
                                 &nbsp;<img src="images/Button/Bottom_btn_pwd.gif" onclick="ShowPsd();" style="vertical-align: middle;
                                padding-top: 2px; border: 0" border="0" />&nbsp;</td>
                         
                            <td>&nbsp;部门：</td>
                            <td><asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label></td>
                            <td>&nbsp; 用户：</td>
                            <td><asp:Label ID="lblUserInfo" runat="server" Text="Label"></asp:Label></td>
                          </tr>
                         </table>
                        </td>
                        <%--<td align="left" style="color: #fff;" width="25%"  >
                       
                         <asp:Label ID="lblVersion" runat="server" Text="Label"  Visible="false"></asp:Label> 
                          &nbsp;<asp:Label ID="lblVersionCom" runat="server" Text="Label" Visible="false"></asp:Label>
                          &nbsp;<asp:Label ID="lblVersionTel" runat="server" Text="Label" Visible="false"></asp:Label> 
                             &nbsp;
                         </td>--%>
                         <td align="right" >    
                            <img src="images/light_un.gif" style="cursor: pointer; vertical-align: middle; float: right;
                                border: 0; padding-right: 20px" id="checkTask" onclick="fnEmty()" title="暂无待办事项！" />&nbsp;
                            <img id="btn_czkhlxr" visible="false" runat="server" border="0" src="images/button/btn_czkhlxr.gif"
                                style="vertical-align: middle; padding-top: 2px" onclick="addTab(null,'2021202','联系人列表','Pages/Office/CustManager/LinkMan_Info.aspx?ModuleID=2021202');" />
                       
                                
                        </td>            
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divBackShadow" style="display: none">
        <iframe id="BackShadowIframe" frameborder="0" width="100%"></iframe>
    </div>
    <div id="ChangePsd" style="border: solid 2px #898989; background: #fff; padding: 10px;
        width: 400px; z-index: 100; position: absolute; top: 60%; left: 68%; margin: -200px 0 0 -400px;
       display:none ">
        <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FFFFFF" style="margin-left: 6px">
            <tr>
                <td height="20" class="td_list_title">
                    <table width="100%" border="0" cellspacing="0" cellpadding="3">
                        <tr>
                         <td>
                         
                         
                                修改密码<input id="hfcommanycd" type="hidden" runat="server" />
                            </td>
                            <td align="right">
                                <div id='searchClick'>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="99%" border="0" cellpadding="2" cellspacing="1" bgcolor="#FFFFFF" id="Tb_01"
            style="margin-left: 6px">
            <tr>
                <td height="20"  class="td_list_fields" align="right" colspan="2">
                    <table style="width: 100%" align="center" border="0">
                        <tr>
                            <td height="28" bgcolor="#FFFFFF" align="left">
                               <img alt="" src="Images/Button/Bottom_btn_save.jpg" onclick="EditPwd();" />
                                <img alt="关闭" src="Images/Button/Bottom_btn_back.jpg" onclick="ClearText();" /><input
                                    id="hf_psd" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td  align="right" class="style1" >
                    用户名<span class="redbold">*</span>
                </td>
                <td bgcolor="#FFFFFF">
                    <asp:TextBox ID="txt_User" runat="server"    CssClass="tdinput" Enabled="false" 
                       ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right"  height="20"  >
                    原密码<span class="redbold">*</span>
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtOldPassword" runat="server" BorderColor="#33CCFF"    TextMode="Password"
                        onblur="OnlyPsd();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" align="right"  height="20"  >
                    新密码<span class="redbold">*</span>
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtNewPassword" runat="server" BorderColor="#33CCFF"   TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr id="CloseDate">
                <td class="style1" align="right"  height="20"  >
                    确认新密码<span class="redbold">*</span>
                </td>
                <td height="20" bgcolor="#FFFFFF">
                    <asp:TextBox ID="txtRePassword" runat="server"   BorderColor="#33CCFF"  TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
      <input name="inpMessageTipTimerSpan" type="hidden" id="inpMessageTipTimerSpan" value="300" runat="server" />
      <input name="IsALertStorage" type="hidden" id="IsALertStorage" runat="server" value=""/>
      <input name="IsALertCust" type="hidden" id="IsALertCust" runat="server" value=""/>
      <input name="IsALertProvider" type="hidden" id="IsALertProvider" runat="server" value=""/>
      <input name="IsALertFlow" type="hidden" id="IsALertFlow" runat="server" value=""/>
      <input name="IsALertTask" type="hidden" id="IsALertTask" runat="server" value=""/>
      <input name="IsALertContract" type="hidden" id="IsALertContract" runat="server" value=""/>
      <input name="IsALertMemo" type="hidden" id="IsALertMemo" runat="server" value=""/>
      <input name="IsALertMsg" type="hidden" id="IsALertMsg" runat="server" value=""/>
      <input name="IsALertMeet" type="hidden" id="IsALertMeet" runat="server" value=""/>
      <input name="IsALertPower" type="hidden" id="IsAlertPower" runat="server" value="1"/>
<%--        <input name="CompanyCD" type="hidden" id="CompanyCD" runat="server" value="<%=SessionUtil.Session["UserInfo"] %>"/>--%>
    <table id="showCompanyCD">
    <tr>
    <td>当前帐套</td>
    <td><asp:TextBox ID="CompanyCD" runat="server"></asp:TextBox></td>
    </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
 var Cookie={
              set:function(name,value,min)   
              {         
                  var   exp     =   new   Date();       
                  exp.setTime(exp.getTime()   +   min*60*1000);   
                  document.cookie   =   name   +   "="+   escape(value)   +";expires="+   exp.toGMTString()+";path=/"; 
                        
              },   
              
              get:function(name)   
              {      

                  var exp = "(^|[\\s]+)"+name+"=([^;]*)(;|$)";      
                  var   arr   =   document.cookie.match(new RegExp(exp)); 
                  
                  if(arr   !=   null)
                     return   unescape(arr[2]); 
                  return   null;   
              },
                 
              del:function(name)   
              {         
                  var   exp   =   new   Date();   
                  exp.setTime(exp.getTime()   -   1);  
                
                  var   cval=this.get(name); 
                    
                  if(cval!=null)
                     document.cookie=name   +"="+cval+";expires="+exp.toGMTString();   
              }
            }           
            
            
     var holeHeight = document.documentElement.clientHeight;//设置整个网页的显示大小
     
     function adjustHeight(obj)
     {
        var win=obj;
        if (document.getElementById)
        {
            if (win && !window.opera)
            {
               // win.document.body.height = win.height-10;
            }
        }
       if( CiframLoadCount == 1 ){
            try{
              MSG.hide();
            }catch(ee){
            
            }
       }else if(CiframLoadCount == 2 ){
           
       }else if(CiframLoadCount == 0){
           CiframLoadCount++;
       }
     }
    function ShowPsd()
    {
        openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
        document.getElementById("ChangePsd").style.display = '';
    }
    
    //))/Images/Left_Frame/Fuction_01.jpg  

    </script>

    <script language="javascript" type="text/javascript">
    var s_UserSessionMinLife = UserSessionMinLife;

    function userLife()
    {
        $.ajax({ 
            type: "POST",
            url: "Handler/UserLifeHandler.ashx",
            dataType:'string',//返回json格式数据
            data: '',
            cache:false,
            success:function(data) 
            {
//                var rjson;
//                eval("rjson="+data);
//                
//                if(rjson.result)
//                {
//                    if(rjson.data.length > 0)
//                    {
//                        doNotice(rjson.data[0]);
//                    }
//                }
                    
                setTimeout(userLife,1000*s_UserSessionMinLife);
                
            } ,
            error:function(r){
                //alert(r.responseText);
                
            }
        });
    
    }    
    
   setTimeout(userLife,1000*5);
   var showedNoticeIDList;
   function doNotice(notice)
   {
      showedNoticeIDList = Cookie.get("showedNoticeIDList");  
      if(showedNoticeIDList == null)
      {
        showedNoticeIDList = ",";
      }
      
        if(showedNoticeIDList.indexOf(","+notice.ID+",") != -1)
            return;       
       //document.getElementById("panelContent").innerHTML = notice.Title+"<br>"+notice.Content+"<br>"+notice.PubDate;
       document.getElementById("ntitle").innerHTML = notice.Title;
       document.getElementById("ncontent").innerHTML = notice.Content;
       document.getElementById("ndate").innerHTML = notice.PubDate;
       
       document.getElementById("noticePanel").style.display = "";
       
            
       showedNoticeIDList += notice.ID+",";      
       Cookie.set("showedNoticeIDList",showedNoticeIDList,60*24*7);  
        
   }
   function hidePanel()
   {
        document.getElementById("noticePanel").style.display = "none";
   }
      
//ff && ie Event start here
function SearchEvent()
{    
    if(document.all)
        return event;

    func=SearchEvent.caller;
    while(func!=null)
    {
        var arg0=func.arguments[0];             
        if(arg0)
        {
            if(arg0.constructor==MouseEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==KeyboardEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==Event) // 如果就是event 对象
                return arg0;
        }
        func=func.caller;
    }
    return null;
}

function GetEventSource(evt)
{        
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
           
    return evt.target||evt.srcElement;
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


    document.oncontextmenu = function(){
        return false;
    };

   document.onkeydown = function(e){  
        var IsFresh=false;
        if(document.all)
        {
            if(event.keyCode == 116)
            {
              IsFresh=true;
            }
        }
        
        var evt = SearchEvent();
        if(evt.charCode == 116)
        {        
           IsFresh=true;
        }

     document.getElementById("txtIsFresh").value=IsFresh;
    // alert(document.getElementById("txtIsFresh").value);
   };
   window.onbeforeunload =function(){
            var IsFresh=document.getElementById("txtIsFresh").value;
       // var sel = confirm("确认退出吗？");
            var urlPara="Handler/UserLifeHandler.ashx?act=remove&IsFresh="+IsFresh.toString();
        //    alert(urlPara);
            //将F5事件重置
            document.getElementById("txtIsFresh").value="false";      
            $.ajax({
             url:urlPara ,
             async: false   
            }); 
            
      
        
   };
    </script>    
</body>

<script type="text/javascript">
//-------20130118 DYG 修改---------//
    window.onload = function() {
    //---先加载好窗口-DYG20130118---//
        $("#showCompanyCD").css("display","none");
        findDimensions();    //调用函数，获取数值  
        window.onresize=findDimensions;  //当改变窗口时，调用findDimensions函数
   //-------窗口样式加载完毕------//
   //--------然后再调用弹出窗程序----------//
        GetAllDestTopList();
        var time = document.getElementById("inpMessageTipTimerSpan").value;
        setInterval(GetAllDestTopList, 1000 * parseInt(time));      
   }  
</script>
</html>
