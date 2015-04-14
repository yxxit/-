<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<%@ Register Src="UserControl/TopMenuCell.ascx" TagName="TopMenuCell" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link rel="stylesheet" type="text/css" href="css/jt_default.css" />

    <script type="text/javascript" src="js/swfobject.js"></script>
    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>

  
 <script  type="text/javascript">
   
    
   

//        var Zxl100Path = '';

//        var initMenuStatus = 0;
//        function switchMenu() 
//        {
//            if (initMenuStatus == 0) 
//            {
//                var html = '展开<img src="images/' + Zxl100Path + 'Main_left_close.gif"   ALIGN=ABSBOTTOM  border="0" />';
//                document.getElementById("link_menuSwitch").innerHTML = html;
//                var oldwidth = parent.$("#leftTD").width();
//                parent.$("#leftTD").width(5);
//                parent.$("#Left").width(0);
//                var newwidth = parent.$("#leftTD").width();
//                parent.$("#jericho_tab").width(parent.$("#jingh001").width() - newwidth + oldwidth);
//                parent.$("#tab_pages").width(parent.$("#tab_pages").width() - newwidth + oldwidth);
//                parent.$.fn.jerichoTab.resize();
//                initMenuStatus = 1;
//            }
//            else 
//            {
//                var html = '收起<img src="images/' + Zxl100Path + 'Main_left_open.gif"   ALIGN=ABSBOTTOM  border="0" />';
//                document.getElementById("link_menuSwitch").innerHTML = html;
//                var oldwidth = parent.$("#leftTD").width();
//                parent.document.getElementById("leftTD").style.width = "184px";
//                parent.document.getElementById("Left").style.width = "184px";
//                var newwidth = parent.$("#leftTD").width();
//                parent.$("#jericho_tab").width(parent.$("#jericho_tab").width() - newwidth + oldwidth);
//                parent.$("#tab_pages").width(parent.$("#tab_pages").width() - newwidth + oldwidth);
//                parent.$.fn.jerichoTab.resize();
//                initMenuStatus = 0;
//            }

//        }

        //ff && ie Event start here
//        function SearchEvent() {
//            if (document.all)
//                return event;

//            func = SearchEvent.caller;
//            while (func != null) {
//                var arg0 = func.arguments[0];
//                if (arg0) {
//                    if (arg0.constructor == MouseEvent) // 如果就是event 对象
//                        return arg0;
//                    if (arg0.constructor == KeyboardEvent) // 如果就是event 对象
//                        return arg0;
//                    if (arg0.constructor == Event) // 如果就是event 对象
//                        return arg0;
//                }
//                func = func.caller;
//            }
//            return null;
//        }

//        document.onkeydown = function(e) {
//            var IsFresh = false;
//            if (document.all) {
//                if (event.keyCode == 116) {
//                    IsFresh = true;
//                }
//            }

//            var evt = SearchEvent();
//            if (evt.charCode == 116) {
//                IsFresh = true;
//            }
//            window.parent.document.getElementById("txtIsFresh").value = IsFresh;
//        };
    </script>
    <style type="text/css">
        #img_logo
        {
            height: 66px;
            width: 303px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
   
    <table  style="width:100%;" border="0" cellspacing="0" cellpadding="0" >
        <tr>
            <td width="200" align="left" valign="middle" style=" background-image:url(images/head.jpg)">
                <img src="Images/logo.png" runat="server" id="img_logo" style=" width:300px; height:70px;"/>
            </td>
            <td valign="top" style=" background-image:url(images/head.jpg)">
                <table style="width: 100%; height:56px;"  border="0" align="left" cellpadding="0" cellspacing="0">
                  <tr>
                        <td height="38" align="left">
                      <%--  <a style="height: 18pt; color: #ffffff;" title="点击收起或展开左侧" id="link_menuswitch" href="javascript:switchmenu();">&nbsp;&nbsp;收起<img src="images/main_left_open.gif"
                                                align="left" border="0" /></a>--%>
                            </td></tr>
                       <tr>
                        <td style=" float:left">
                      
                 <asp:Label ID="Labelqm"   runat="server"></asp:Label>
                        
                    
                        </td>
                    </tr>
                </table>
            </td>           
        </tr>
    </table>
    
    
    
    <input id="hidCompanCD" type="hidden" runat="server" />
    <input id="hidEmployeeID" type="hidden" runat="server" />
     <uc2:Message ID="Message1" runat="server" />
    </form>

    <script language="javascript" type="text/javascript">
        parent.checkIframeLoadedCnt();
    </script>
</body>
</html>
