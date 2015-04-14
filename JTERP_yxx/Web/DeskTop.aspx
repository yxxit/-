<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeskTop.aspx.cs" Inherits="DeskTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>   

    <script type="text/javascript">
        function ChangeTitleClass(index){
           if(index == 1)
                            SearchTaskList();
           else if(index == 2)
                            SearchDeskFlow();
           else if( index == 3)
                            SearchUnreadMessage();
           for( var i = 1;i<= 3;i++){
              if( i != index  ){
                 document.getElementById("divli"+i).style.backgroundColor = "#B6B6B6";
              }
              else{
                    document.getElementById("divli"+i).style.backgroundColor = '#ffffff';
               }
           }
        }
    </script>

    <link rel="stylesheet" type="text/css" href="css/jt_default.css" />
    <link href="css/pagecss.css" type="text/css" rel="Stylesheet" />
    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="js/JQuery/jquery_last.js" type="text/javascript"></script>
    <script src="js/SystemAlert.js" type="text/javascript"></script>
    <script src="js/DeskTop.js" type="text/javascript"></script>
    <script src="js/common/Common.js" type="text/javascript"></script>
      <script src="../../../js/jthy/ContractManage/ContractInfo.js" type="text/javascript"></script>    


    <style type="text/css">
     .noticetable
        {
            position: absolute;
            top: 180px;
            left: 190px;
             width: 420px;
            background-color: #dddddd;
            z-index: 100;
             border:4px solid #B9D1EA;
        }
        
      .black2
      {
        padding-right:220px;
      	 
      }
      body
      {
      	 
      	  background-image:url(images/main/body_bg.jpg);
      	 
      	
      }
     
    </style>
</head>
<body onload=" InitPage();" >
<div>
    <span id="Forms" class="Spantype"></span>
    <br />    
    <table align="center" width="95%" border="0" cellspacing="0" cellpadding="0" >
    <tr><td align="left" class="Title" width="100%"  style=" background-color:#E1E1E1;"> <span style=" font-size:15px;">个人中心&nbsp;&nbsp;》首页</span></td>
   <td align="right"  style=" display:none">             
             <div id="divTime" style="text-align: right;font-size:14px;border: 0px; padding: 3px; border-style: double; width: 260px;"></div>
    </td>
    </tr>
    </table>
    <br />
        
    <table style="display:none;" align="center" width="95%" border="0" cellspacing="0" cellpadding="1" background="images/main/body_bg.jpg">
      <tr>             
       <td style="width:50%" align="center" valign="top">
         <table width="95%" border="0" cellpadding="0" cellspacing="0" bgcolor="white" >                 
           <tr>
            <td style="text-indent:1em;" align="left" height="30" valign="middle" bgcolor="#3DCDC4" class="white">
                发给我的消息
            </td>                        
           </tr>
           <tr >
            <td height="30" align="center" valign="middle" bgcolor="#cccccc" >
              <table width="100%" border="0" cellpadding="1" cellspacing="1" id="jt_desk_message">
                <tr bgcolor="#ffffff">
                <td height="30" class="black2" align="left" >
                </td>
                <td ></td></tr>
                                                      
              </table>
             </td>                        
             </tr>
         </table>
       </td>
       <td style="width:50%" align="center" valign="top">
        <table width="95%" border="0" cellpadding="0" cellspacing="0" bgcolor="white" >                 
           <tr > <td style="text-indent:1em;"align="left" height="30" valign="middle" bgcolor="#FAAB4C" class="white">
                   发给我的流程
              </td>                        
                </tr>
            <tr >
                  <td height="30" align="center" valign="middle" bgcolor="#cccccc" >
                    <table width="100%" border="0" cellpadding="1" cellspacing="1" id="tbList2">                            
                    <tr bgcolor="#ffffff">
                <td height="30" class="black2" align="left" >
                </td>
                <td ></td></tr>
                        
                        
                         </table>
                        </td>                        
                    </tr>
                   <input id="hidSearchCondition" type="hidden" runat="server" />   <!--隐藏的-->
       
       
           </table>
            </td>
            </tr>
       </table>
       <br />
       
       <table align="center" width="95%" border="0" cellspacing="0" cellpadding="1" background="images/main/body_bg.jpg">    
        <tr >             
            <td style="width:50%; display:none;" align="center" valign="top">
            <table width="95%" border="0" cellpadding="0" cellspacing="0" bgcolor="white" >                 
                    <tr >
                        <td style="text-indent:1em;" align="left" height="30" valign="middle" bgcolor="#36B6F3" class="white">
                            通知公告
                        </td>                        
                    </tr>
                    <tr >
                        <td height="30"   bgcolor="#cccccc" >
                            <table width="100%" border="0" cellpadding="1" cellspacing="1" id="tbdestnoticeList">                          
                             <tr  bgcolor="#ffffff"><td height="30" class="black2" >
                        <td ></td></tr> 
                            </table>
                        </td>                        
                    </tr>
                    </table>
            </td>
            <td style="width:50%" align="left" valign="top">
            <table width="50%" border="0" cellpadding="0" cellspacing="0" bgcolor="white" >                 
                    <tr >
                        <td style="text-indent:1em;"align="left" height="30" valign="middle" bgcolor="#FF6364" class="white">
                            煤炭市场信息
                        </td>                        
                    </tr>
                    <tr>
                        <td height="30" align="center" valign="middle" bgcolor="#cccccc" >
                            <table width="100%" border="0" cellpadding="1" cellspacing="1" >                            
                            <tr bgcolor="#ffffff"><td height="30"  class="black2">
                            <a href="http://osc.cqcoal.com/CoalIndex/chs/new/" target="_Blank">动力煤价格指数查看</a>
                            </td>
                            </tr>                                                       
                            </table>
                            </tr
                        </td>                        
                    </tr>
                    </table>                    
            </td>
            </tr>
       </table>
    
    
    
    
    
    
    
    
    
    
  <div id="editPanel"    style="display: none; border:4px solid #B9D1EA"  class="noticetable">
        <table  bgcolor="#F5F5F5" id="itemContainer"   cellspacing="1" cellpadding="1" width="420px" >
            <tr>
                <td style="width:25%;" align="left">
                    主题
                </td>
                <td style="width:70%;">
                    <span id="ttTitle"></span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    接收时间
                </td>
                <td >
                    <span id="ttSendDate"></span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    发件人
                </td>
                <td >
                    <span id="ttSendUser"></span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    短信内容：
                </td>                
                <td >
                    <span id="ttContent"></span>
                </td>
            </tr>
            <tr>                
                <td colspan="2">
                    <a href="#" onclick="hideMsg()">确定</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    
    
    
    
    
    
     <div id="myforwer"  style="display: none;  class="noticetable">
        <table id="Table1" border="1" width="400px" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 80px; background-color: #eeeeee; float: right;">
                    公告主题
                </td>
                <td>
                    <input type="text" id="Text1" style="width:280px;" />
                    <input type="hidden" id="Hidden1" />
                </td>
                <td>
                    <a href="#" onclick="javascript:document.getElementById('desktopnotice').style.display='none';">
                        关闭</a>
                </td>
            </tr>
            <tr>
                <td valign="top" style="background-color: #eeeeee">
                    公告内容
                </td>
                <td colspan="2">
                    <textarea id="Textarea1" rows="10" cols="59" > </textarea>
                </td>
            </tr>
        </table>
    </div>  
    
    
    
    
    <div id="desktopnotice" style="display: none;" class="noticetable">
        <table id="noticeTable" border="1" width="400px" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 80px; background-color: #eeeeee; float: right;">
                    公告主题
                </td>
                <td>
                    <input type="text" id="spNewsTitle" style="width:200px;" />
                    <input type="hidden" id="itemID" />
                </td>
                <td>
                    <a href="#"onclick="javascript:document.getElementById('desktopnotice').style.display='none';">
                        <span style=" margin-left:-80px;">关闭</span></a>
                </td>
            </tr>
            <tr>
                <td valign="top" style="background-color: #eeeeee">
                    公告内容
                </td>
                <td colspan="2">
                    <textarea id="spNewsContent" rows="9" cols="43" > </textarea>
                </td>
            </tr>
        </table>
    </div>  
   
    </div>  
</body>
</html>
