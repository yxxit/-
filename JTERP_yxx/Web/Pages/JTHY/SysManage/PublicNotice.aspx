<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicNotice.aspx.cs" Inherits="Pages_Personal_MessageBox_PublicNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/Personal/Common.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>

<script src="../../../js/Personal/MessageBox/Notice.js" type="text/javascript"></script>
<title>公告列表</title> 


 <style type="text/css">
 #editPanel
 {
   width :400px;
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
       
    <style type="text/css">
        .style1
        {
            background-color: #FFFFFF;
            width: 216px;
        }
    </style>
    
  <script type="text/javascript">
  
function isSelect()
{
 
  var d=document.getElementById("Select1").value;
    if(d=="1")
    {
    
     document.getElementById("btn_confirm").style.display="none";
      document.getElementById("btn_del").style.display="none";

    }
    else
    {
      document.getElementById("btn_confirm").style.display="block";
        document.getElementById("btn_del").style.display="block";

    }


}
 </script>
    
    
    
    
</head>
<body>

<input type="hidden" id="isSearched" value="0" />
<input type="hidden" id="hidStatus" value="1" />
<input type="hidden" id="UserID" runat="server" />
<form id="form1" runat="server">
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype" name="Forms"></span>
<table width="98%"  border="0" cellpadding="0" cellspacing="0" id="mainindex">
  <tr>
    <td valign="middle" align="center">
        <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >

            <tr class="menutitle1">
                <td align="left" valign="middle" >
                    &nbsp;&nbsp;检索条件
                </td>
                <td align="right" valign="middle">
                    <div id='searchClick'>
                        <img src="../../../images/Main/Close.jpg" style="CURSOR: pointer"  onclick="oprItem('searchtable','searchClick')"/>
                    </div>
                </td>
            </tr>
        </table>
    </td>
    
  </tr>
  <tr>
    <td   >
        <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
             <tr>
                <td bgcolor="#FFFFFF">
                    <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
                          <tr class="table-item">
                              <td class="td_list_fields" >
                                    公告时间   
                              </td>
                              <td class="style1"> 
                                    <input onkeypress="return false;" name="createDate" id="createDate1" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
                                   
                                   ~<input onkeypress="return false;" name="createDate" id="createDate2" class="tdinput" type="text" size="14" onclick="WdatePicker()" />
                              </td>
                              <td   class="td_list_fields" >
                                    公告主题
                              </td>
                              <td class="tdColInput">
                                    <input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:80%;" />
                              </td>
                              
                              
                                  <td class="td_list_fields">审核状态</td><td>
                    
                       <select id="Select1"  onchange="isSelect(this);">
                       
                        <option value="0">未审核</option>
                        <option value="1" >已审核</option>
                    </select>
                    
                    
                    
                    
            </td>
                            
                          </tr>   
                          
                            
                          
                          
                          <tr>
                             <td colspan="6" align="center" bgcolor="#FFFFFF">
                                <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchEquipData()'  />
                             </td>
                          </tr>  
                    </table>
                </td>
             </tr>
        </table>
    </td>
  </tr>
</table>

<table width="98%"  height="57" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex" >

  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">
        <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >
            <tr class="menutitle1">
                <td  align="left" valign="middle" >
                    &nbsp;&nbsp;公告列表
                </td>
                    <td align="right" valign="middle" >
                      
                     <%-- 
                        <a id="btn_new" runat="server" href="AddNotice.aspx?ModuleID=10619">
                            <img src="../../../images/Button/Bottom_btn_new.png"  alt=""border="0" />
                        </a>
                      --%>
                      
                         
                        <asp:HyperLink NavigateUrl="AddNotice.aspx?ModuleID=10619"
                         ID="HyperLink1"  runat="server"    ImageUrl="../../../images/Button/Bottom_btn_new.png">
                    </asp:HyperLink> 
                    
                       
                        <asp:HyperLink NavigateUrl="javascript:ConfirmBat()"
                         ID="btn_confirm"  runat="server"    ImageUrl="../../../images/Button/Main_btn_sh.png">
                    </asp:HyperLink> 
                    
                       
                         
                       <asp:HyperLink NavigateUrl="javascript:DelMessage();"
                        ID="btn_del" runat="server" ImageUrl="../../../images/Button/Main_btn_delete.png">
                    </asp:HyperLink>
                        
  
                </td>
            </tr>
        </table>                 
    </td>
  </tr>
  
  <tr>
    <td colspan="2">
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1" bgcolor="#CCCCCC">
            <tbody>
                <tr class="table-item">
                    <th width="5%" class="td_main_detail" >
                        选择
                    </th>
                    <th width="18%" class="td_main_detail"  >
                          <div class="orderClick"  onclick="OrderBy(this,'NewsTitle','oC2');return false;">
                                公告主题<span id="oC2" class="orderTip"></span>
                            </div>
                    </th> 
                 
                    <th width="18%"   class="td_main_detail"  >
                        <div class="orderClick" onclick="OrderBy(this,'NewsContent','oC5');return false;">
                            公告内容<span id="oC5" class="orderTip"></span>
                        </div>
                    </th>
                    
                    <th width="8%" class="td_main_detail" >
                        <div onclick="OrderBy(this,'Status','oC6');return false;">
                            审核状态<span id="oC6" class="orderTip"></span>
                        </div>
                    </th>   
                         
                    <th width="8%" class="td_main_detail" >
                        <div onclick="OrderBy(this,'IsShow','Span3');return false;">
                            是否首页显示<span id="Span3" class="orderTip"></span>
                        </div>
                    </th>
                    
                     <th width="10%" class="td_main_detail" >
                        <div class="orderClick" onclick="OrderBy(this,'CreateDate','oC1');return false;">
                            创建时间<span id="Span1" class="orderTip"></span>
                        </div>
                    </th> 
                    <th width="10%" class="td_main_detail" >
                        <div class="orderClick" onclick="OrderBy(this,'Creator','oGroup');return false;">
                            创建人<span id="oGroup" class="orderTip"></span>
                        </div>
                    </th>
                    <th width="10%" class="td_main_detail" >
                        <div class="orderClick" onclick="OrderBy(this,'ComfirmDate','Span4');return false;">
                            审核时间<span id="Span4" class="orderTip"></span>
                        </div>
                    </th> 
                    <th width="10%" class="td_main_detail" >
                        <div class="orderClick" onclick="OrderBy(this,'Comfirmor','Span5');return false;">
                            审核人<span id="Span5" class="orderTip"></span>
                        </div>
                    </th>             
                
              </tr>
        </tbody>
    </table>

      <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
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







<div id="editPanel" style=" display:none; border:4px solid #B9D1EA">            
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
                    <select id="slStatus" disabled="disabled">
                       
                        <option value="0">未审核</option>
                        <option value="1" >已审核</option>
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
