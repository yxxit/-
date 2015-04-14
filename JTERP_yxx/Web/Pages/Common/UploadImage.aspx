<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadImage.aspx.cs" Inherits="Pages_Common_UploadImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>扫描上传</title>
    <link rel="stylesheet" type="text/css" href="../../css/default.css" />
    <script language="javascript" type='text/javascript'>
        function WindowShow(url,iWidth,iHeight)
        {
	        if (iWidth != null && iHeight != null)
            {
    	        window.open(url, "", "width=" + iWidth + ",height=" + iHeight + ",toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            else if (iWidth != null && iHeight == null)
            {
    	        window.open(url, "", "width=770" + ",height=" + iWidth + ",toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            else if (iWidth == null && iHeight == null)
            {
    	        window.open(url, "", "toolbar=no,resizable=yes,scrollbars=yes,location=no, status=no");
            }
            return false;
        }
        
        function SaveJpgToServer()
        {
            // 
            var obj= document.getElementById("dealsaveJpg")
            if(obj!= null)
            {
                obj.click();
            }
        }
    </script>
    <script type="text/javascript">
        submitFlag = false;
        /* 确定 */
        function DoConfirm()
        {
            fileUrl = document.getElementById("uploadFileUrl").value;
            docName = document.getElementById("uploadDocName").value;
            window.parent.HideUploadFile(fileUrl,docName, "1");
        }
        /* 取消 */
        function DoCancel()
        {
            window.parent.HideUploadFile("", "0");
        }
        /* 校验输入内容 */
        function CheckInput()
        {
         //   submitFlag = false;
         //   //获取本地路径
         //   filePath = document.getElementById("flLocalFile").value;
            
         //   //未选择文件时
        //    if( filePath == "")
        //    {
        //        //显示错误信息
        //        popMsgObj.ShowMsg("请输入本地文件路径");
        //    }
        //    else
        //    {
                submitFlag = true;
        //    }
        }
        
    var popMsgObj=new Object();
    popMsgObj.content = "";
    
    //调用方法(全部提示信息)
    popMsgObj.ShowMsg = function(msgInfo)
    {
        popMsgObj.content = "";
        if(msgInfo != null)
        {
            popMsgObj.content = msgInfo;
        }
        document.getElementById('mydiv').innerHTML = "<table width=\"290\" border=\"0\" cellspacing=\"10\" cellpadding=\"0\" ><tr><td width=\"290\"><strong><font color=\"red\">" 
        + popMsgObj.content+"</font><strong></td> </tr></table><table width=\"200\"><tr><td align=\"right\"><img src=\"../../../Images/Button/closelabel.gif\" onclick=\"document.getElementById('mydiv').style.display='none';\" /></td></tr></table>" ;
        document.getElementById('mydiv').style.display = 'block';
    }
        function Update()
        {
            submitFlag=true;
        }
 
 
 
 function getUploadEvent()
 {
    document.getElementById("btnUpload").click();
 }

    </script>

</head>
<body >
    <form id="form1" runat="server" onsubmit="return submitFlag;">
    <!--提示信息弹出详情start-->
        <div id="mydiv" style="border:solid 5px #898989; background:#fff; z-index:1001; position:absolute; display:none; top:25%; left:25%; ">
        </div>
        <!--提示信息弹出详情end-->
        <table width="98%" border="0" cellpadding="0" cellspacing="0" style="margin-left:5px;margin-top:5px;background-color:#F0f0f0;">
            <tr>
                <td valign="top" colspan="2">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
            </tr>
            <tr>
                <td height="30" align="center" colspan="2" class="Title">扫描文件</td>
            </tr>
            <tr>
                <td height="20"  class="td_list_title" colspan="2">
                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                        <tr>
                            <td>扫描预览-请点击【预览】按钮，开启预览画面<asp:FileUpload ID="flLocalFile" runat="server" style="display:none" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" class="tdColInput" height="10"></td></tr>
            <tr>
                 <td colspan="2" class="tdColInput" height="400" style="text-align:center">
                 <object  id="captrue"  width="500" height="400" classid="clsid:454C18E2-8B7D-43C6-8C17-B1825B49D7DE">
                                                        <param name="_Version" value="65536" />
                                                        <param name="_ExtentX" value="2646" />
                                                        <param name="_ExtentY" value="1323" />
                                                        <param name="_StockProps" value="0" />
                                                    </object>
                 </td>
            </tr>
            <tr><td colspan="2" class="tdColInput" height="10"></td></tr>
            <tr>
                <td class="tdColInput" align="center" colspan="2">
               
                     <input type="button" id="btnStart" style=" width:48px;height:23px;border:none;background-image:url(../../Images/review.jpg)" name="btnStartName"  onclick="captrue.bStopPlay();captrue.bStartPlay();" />
                     <input type="button" id="Button1" style=" width:48px;height:23px;border:none;background-image:url(../../Images/scan.jpg)"  name="btnStartName"   onclick="captrue.bStopPlay();captrue.bStartPlay();captrue.bSaveJPG('<%= scanImgPath %>','<%= imagename %>');captrue.bStopPlay();getUploadEvent()"  />
                    
                      
                          
                          <asp:ImageButton    ImageUrl="~/Images/Button/Main_btn_upload.jpg" ID="btnUpload"  style="display:none"
                         runat="server" OnClientClick="CheckInput();" onclick="btnUpload_Click"  />
                         
                                                
                       <img src="../../Images/Button/Bottom_btn_cancel.jpg" style="cursor:hand" onclick="captrue.bStopPlay();DoCancel();" id="btnCancel" />
                </td>
            </tr>
        </table>
        <input type="hidden" id="uploadFileUrl" runat="server" />
        <input type="hidden" id="uploadDocName" runat="server" />
    </form>
</body>

</html>
