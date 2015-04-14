<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckNote.aspx.cs" Inherits="Pages_Personal_MessageBox_CheckNote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link rel="stylesheet" type="text/css" href="../../../css/default.css" />
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/Personal/Common.js" type="text/javascript"></script>

<script src="../../../js/Personal/MessageBox/Notice.js" type="text/javascript"></script>

<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<title>公告列表</title>
        <style type="text/css">
            .style1
            {
                width: 266px;
            }
            .style2
            {
                width: 77px;
            }
            .style3
            {
                width: 212px;
            }
            .style4
            {
                width: 218px;
            }
           #editPanel
        {
            width:400px;
            background-color:#fefefe;
            position:absolute;
            border:solid 1px #000000;
            padding:5px;
        }
            #spNewsContent
            {
                height: 114px;
                width: 278px;
            }
        </style>
        <script language="javascript" type="text/javascript">
        
        
        function ConfirmBat()
        {
             var ck = document.getElementsByName("Checkbox1");
                var ids = "";    
                for( var i = 0; i < ck.length; i++ )
                {
                    if ( ck[i].checked )
                    {
                        if( ids != "")
                            ids += ",";
                        ids += ck[i].value;
                    }
                }        
             
                if(ids == "")
                {
                   ShowInfo("请至少选择一项");
                    return;
                }
        
            //拼写请求URL参数
            var postParams = "Action=ConfirmBat&&id="+ids;
            $.ajax({ 
                type: "POST",
                url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
                dataType:'string',
                data: '',
                cache:false,
                success:function(data) 
                {                      
                    var result = null;
                    eval("result = "+data);
                    
                    if(result.result)                    
                    {                   
                       alert("审核成功");
                        TurnToPage(currentPageIndex);       
                    }
                    else
                    {
                        ShowInfo(result.data);
                    }    
                },
                error:function(data)
                {
                     ShowInfo(data.responseText);
                }
            });
        }
        
        
       function EditItem()
        {    
            var chks = document.getElementsByName("Checkbox1");
            var ic = 0;
            for(var j=0;j<chks.length;j++)
            {  
                if(chks[j].checked)
                    ic++
            }
            
            if(ic>1)
            {
                showInfo("不能同时修改2个或者2个以上的记录");
                return;
            }
            
            for(var i=0;i<chks.length;i++)
            {                
                if(chks[i].checked)
                {                    
                   showEditPanel(chks[i].value);
                   //chks[i].nextSibling.value;
                   return;
                }
            }
            
            showInfo("请选择要修改的信息");
        
    }

    function showEditPanel(id,uid)
    {
             if( document.getElementById("UserID").value == uid )
         {
            document.getElementById("btnNoteEdit").style.display = "";
         }else{
            document.getElementById("btnNoteEdit").style.display = "none";
         }
         
        //拼写请求URL参数
        var postParams = "Action=GetItem&id="+id;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
            dataType:'string',
            data: '',
            cache:false,
            success:function(data) 
            {                      
                var result = null;
                eval("result = "+data);
                
                if(result.result)                    
                {
                    showEditPanelData(result.data);
                }
                else
                {
                    ShowInfo(result.data);
                }    
            },
            error:function(data)
            {
                 ShowInfo(data.responseText);
            }
        });
    }

    function CancelEdit()
    {
        document.getElementById("editPanel").style.display = "none";
    }
    
    function HtmlEncode(input)
    {
       var converter=document.createElement("DIV");
       converter.innerText=input;
       var output=converter.innerHTML;
       converter=null;
       return output;
    }
    function HtmlDecode(input)
    {
       var converter=document.createElement("DIV");
       converter.innerHTML=input;
       var output=converter.innerText;
       converter=null;
       return output;
    }
    function showEditPanelData(data)
    {        
    

        document.getElementById("itemID").value = data.ID;
    
        document.getElementById("spNewsTitle").value = HtmlDecode(data.NewsTitle);
        document.getElementById("spNewsContent").value = HtmlDecode(data.NewsContent.replace(/<br><br>/g,"\r"));
        document.getElementById("slStatus").value = data.Status;
        document.getElementById("slIsShow").value = data.IsShow;
        document.getElementById("spCreateDate").innerHTML = data.CreateDate;
        
        //alert(data.Status);
        if(data.Status != "0")
        {
            document.getElementById("spNewsTitle").disabled = true;
            document.getElementById("spNewsContent").disabled = true;
            document.getElementById("slStatus").disabled = true;
        }
     
        var ele = document.getElementById("editPanel");
        ele.style.display = "block";
        ele.style.left = "200";
        ele.style.top = "160";
        
    }
    
    
    
    
    function saveEdit()
    {
        
    
        var title = HtmlEncode(document.getElementById("spNewsTitle").value);
        var content = HtmlEncode(document.getElementById("spNewsContent").value);
        var id =  document.getElementById("itemID").value;
        var Status = document.getElementById("slStatus").value;
        var IsShow = document.getElementById("slIsShow").value;
          
         if(title + "" == "")
            {
                showInfo("请填写信息标题");
                return;
            }
             if(title.length>50)
            {
                showInfo("标题 长度不能超过50");
                return;
            }
            
             if(content + "" == "")
            {
                showInfo("请填写信息内容");
                return;
            }
              if(content.length>500)
            {
                showInfo("内容 长度不能超过500");
                return;
            }
            title =UrlEncode(title);
            content =UrlEncode(content)
       
        //拼写请求URL参数
        var postParams = "Action=EditItem&Status="+Status+"&IsShow="+IsShow+"&id="+id+"&title="+title+"&content="+content;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
            dataType:'string',
            data: '',
            cache:false,
            success:function(data) 
            {                      
                var result = null;
                eval("result = "+data);
                
                if(result.result)                    
                {                  
                    alert("保存成功");
                    TurnToPage(currentPageIndex);
                    
                    document.getElementById("editPanel").style.display = "none";
                }
                else
                {
                    ShowInfo(result.data);
                }    
            },
            error:function(data)
            {
                 ShowInfo(data.responseText);
            }
        });
       
    }
    
    function isCanDelete( uid){

         if( document.getElementById("UserID").value == uid )
         {
           return "  isDel='1' ";
         }else{
           return "   isDel='0'    ";
         }
    }
        
        </script>
</head>
<body>
<input type="hidden" id="isSearched" value="0" />
<input type="hidden" id="hidStatus" value="0" />
<input type="hidden" id="UserID" runat="server" />
<form id="form1" runat="server">
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
<table width="95%" height="57" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td rowspan="2" align="right" valign="top"><div id='searchClick'><img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/></div>
        &nbsp;&nbsp;</td>
  </tr>
  <tr>
    <td  valign="top" class="Blue"><img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件</td>
  </tr>
  <tr>
    <td colspan="2"  >
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF">
       
        <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20"  align="right" class="td_list_fields" >公告时间   </td>
            <td bgcolor="#FFFFFF" class="style1"> <input onkeypress="return false;" name="createDate" id="createDate1" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
                ~<input onkeypress="return false;" name="createDate" id="createDate2" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
           </td>
            <td   align="right" class="td_list_fields" >公告主题</td>
            <td bgcolor="#FFFFFF" class="style3"><input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:180px;" /></td>
            <td bgcolor="#FFFFFF" align=left class="style4"><img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchEquipData()'  /></td>
            <td bgcolor="#FFFFFF">&nbsp;</td>
            
          </tr>   
          
                 
          
          <tr class="table-item">
            <td width="10%" height="20"  align="right" class="td_list_fields" >&nbsp;</td>
            <td bgcolor="#FFFFFF" class="style1">&nbsp;</td>
          <td colspan="6" align="left" bgcolor="#FFFFFF">
          </td>
           </tr>  
        </table></td>
      </tr>
    </table>
    </td>
  </tr>
</table>

<table width="95%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td align="center" valign="top">&nbsp;
    
    </td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">公告审核</td>
  </tr>
  
  <tr>
    <td height="35" colspan="2" valign="top">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF">
          &nbsp;&nbsp;&nbsp;
          
          <a id="btn_new" runat="server" visible="false" href="AddNotice.aspx?ModuleID=10619"><img src="../../../images/Button/Bottom_btn_new.jpg" border="0" /></a>
          
          <asp:HyperLink NavigateUrl="javascript:DelMessage();"
             ID="btn_del" runat="server" visible="false" ImageUrl="../../../images/Button/Main_btn_delete.jpg"></asp:HyperLink>
             
            <%--<asp:HyperLink NavigateUrl="javascript:EditItem();"
             ID="btn_edit" runat="server" ImageUrl="../../../images/Button/Bottom_btn_edit.jpg"></asp:HyperLink>--%>
                         
           <asp:HyperLink  NavigateUrl="javascript:ConfirmBat()"
             ID="btn_confirm" runat="server"   ImageUrl="../../../images/Button/Main_btn_sh.jpg"></asp:HyperLink>
             
          </td>
        </tr>
      </table>
    </td>
  </tr>
  
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
      
        <th height="20" align="center" class="td_list_fields">
            选择</th>
            
             <th   width="20%" align="center" class="td_list_fields" >
              <div class="orderClick"  onclick="OrderBy(this,'NewsTitle','oC2');return false;">
            公告主题<span></span></div></th>    
        <th align="center" class="td_list_fields">
        <div class="orderClick" onclick="OrderBy(this,'Creator','oGroup');return false;">
            创建人<span></span></div></th>
        <th align="center" class="td_list_fields" >
              <div class="orderClick" onclick="OrderBy(this,'CreateDate','oC1');return false;">
            创建时间<span></span></div></th>       
       
        <th width="30%" align="center" class="td_list_fields" >
             <div class="orderClick" onclick="OrderBy(this,'NewsContent','oC5');return false;">
            公告内容<span></span></div></th>
        
        <th align="center" class="td_list_fields">
        <div onclick="OrderBy(this,'Status','oC6');return false;">
            审核状态<span></span></div></th>   
         
        <th align="center" class="td_list_fields">
        <div onclick="OrderBy(this,'IsShow','oC6');return false;">
            是否在公告板显示<span></span></div></th>
        
        <th align="center" class="td_list_fields"  style=" display:none">
              <div class="orderClick" onclick="OrderBy(this,'ComfirmDate','oC1');return false;">
            审核时间<span></span></div></th>       
                
        <th align="center" class="td_list_fields"     style=" display:none" >
        <div class="orderClick" onclick="OrderBy(this,'Comfirmor]','oC2');return false;">
            审核人<span></span></div></th>             
        
      </tr>
    </tbody>
    </table>

      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="PageCount"></div></td>
            <td height="28"  align="right"><div id="pageDataList1_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divpage">
              <input name="text" type="text" id="Text2" style="display:none" />
              <span id="pageDataList1_Total" style="display:none"></span>每页显示
              <input name="text" type="text" id="ShowPageCount"/>
                条 转到第
              <input name="text" type="text" id="ToPage"/>
                页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:pointer;' alt="go" width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" /> </div></td>
          </tr>
        </table></td>
        </tr>
    </table>
    <br/></td>
  </tr>
</table>
</form>



<div id="editPanel" style="display:none;">            
    <table id="itemContainer" border="1" width="100%" cellpadding="3" style="border-collapse:collapse;">
        
        <tr>
            <td style="width:80px;">公告主题</td><td>               
                <input type="text" id="spNewsTitle"   style=" width:94%;"  />
                <input type="hidden" id="itemID" />
            </td>
        </tr>
         <tr>
            <td valign="top"  style="width:80px;">公告内容</td><td>
                <textarea id="spNewsContent" > </textarea>               
            </td>
        </tr>
       
         <tr>
            <td>审核状态</td><td>
                    <select id="slStatus">
                       
                        <option value="0">未审核</option>
                        <option value="1">已审核</option>
                    </select>
            </td>
        </tr>
        
        <tr>
            <td>是否在公告板显示</td><td>
                    <select id="slIsShow">
                       
                        <option value="1">是</option>
                        <option value="0">否</option>
                    </select>
            </td>
        </tr>
        
         <tr>
            <td>发布时间</td><td>
                <span id="spCreateDate"></span>
            </td>
        </tr>
              
        <tr><td></td><td style="padding:3px;">
             <span id="btnNoteEdit"  runat="server"><a href="javascript:saveEdit()">确定</a>&nbsp;&nbsp;
            |&nbsp;&nbsp;</span><a href="javascript:CancelEdit()">取消</a>
        </td></tr>
    </table>
</div>

</body>
</html>
