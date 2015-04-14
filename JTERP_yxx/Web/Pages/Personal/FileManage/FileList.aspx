<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileList.aspx.cs" Inherits="Pages_Personal_FileManage_FileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<link href="../../../css/default.css" type="text/css" rel="stylesheet" />
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

<script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<script src="../../../js/Personal/Common.js" type="text/javascript"></script>

<script src="../../../js/Personal/FileManage/FileList.js" type="text/javascript"></script>
<script src="../../../js/common/check.js" type="text/javascript"></script>
<script src="../../../js/common/page.js" type="text/javascript"></script>
<script src="../../../js/common/Common.js" type="text/javascript"></script>
<script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script><!--企业文化类型等等-->
<script>
function selectall()
{
   $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
   });
}

</script>
    <title>附件列表及浏览</title>
</head>
<body>
    <form id="form1" runat="server">
 <input type="hidden" id="isSearched" value="0" runat="server" />
 <input type="hidden" id="hidSearchCondition" value="" runat="server"/>
 <input type="hidden" id="hidtypeid" runat="server" /> <!--传入的分类类别-->
 <input type="hidden" id="hidtypename" runat="server" /><!--culturetypename-->
<a name="pageDataList1Mark"></a>
<span id="Forms" class="Spantype"></span>

    <table width="98%" border="0" cellpadding="0" cellspacing="0" class="checktable" id="mainindex">
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
                    <td width="10%" height="20" class="td_list_fields" align="right">发布时间   </td>
                    <td bgcolor="#FFFFFF"  width="23%" > <input   class="tdinput" onkeypress="return false;" name="createDate" id="createDate1"  type="text" size="14"   readonly="readonly"  onclick="WdatePicker()" />
                        ~<input class="tdinput" onkeypress="return false;" name="createDate" id="createDate2" type="text" size="14"  readonly="readonly" onclick="WdatePicker()" />
                   </td>
                    <td   align="right" class="td_list_fields" width="10%" >主题</td>
                    <td bgcolor="#FFFFFF"  width="23%" ><input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width:180px;" /></td>
                    
                    <td  class="td_list_fields" align="right" width="10%" >内容摘要</td>
                    <td bgcolor="#FFFFFF"  width="23%" ><input name="txtContent" id="txtContent" type="text" class="tdinput" style="width:180px;" /></td>
                    
                  </tr>
                                   
                  
                  <tr class="table-item">
                    <td width="10%" height="20"  align="right" class="td_list_fields" >发布人</td>
                    <td bgcolor="#FFFFFF" class="style1"><input name="txtContent" id="txtCreator" type="text" class="tdinput" style="width:180px;" /></td>
                    <td align="right" class="td_list_fields" >企业文化类型</td>
                    <td bgcolor="#FFFFFF" class="style3"> 
                        <%--<select id="ddlculturetype" class="tddropdlist" runat="server" style="width:70%">  </select>   --%>    
                        <asp:TextBox ID="Compuny" runat="server" CssClass="tdinput" onclick="alertdiv('Compuny,inputCompuny');"  ReadOnly="true"  Width="70%">
                        </asp:TextBox>
                        <img src="../../../Images/default/search1.gif" alt="搜索" style="cursor: hand" onclick="alertdiv('Compuny,inputCompuny');" />
                        <input name="inputCompuny" id="inputCompuny" type="hidden" runat="server" />                    
                     </td>
                    <td  align="right" class="td_list_fields" >&nbsp;</td>
                    <td bgcolor="#FFFFFF" class="style3">&nbsp;</td>
                  </tr>     
                  <tr>
                  <td colspan="6" bgcolor="#FFFFFF" align="center" ><img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor:pointer;' onclick='SearchEquipData()'  /></td>
                  </tr>
                  
               </table>
                </td>
             </tr>
            </table>
            </td>
          </tr>
    </table>
    
    
<table width="98%" border="0" cellpadding="0" cellspacing="0" class="maintable" id="mainindex"  >
  <tr>
    <td valign="top"><img src="../../../images/Main/Line.jpg" width="122" height="7" /></td>
    <td align="center" valign="top">&nbsp;
    
    </td>
  </tr>
  <tr>
    <td height="30" colspan="2" align="center" valign="top" class="Title">
        附件列表及浏览
    </td>
  </tr>
    <tr>
    <td height="15" colspan="2" align="left" valign="top" >
    <table>
       <tr>
       <td>您当前的查看权限：</td>
       <td>
           <asp:Literal ID="UserLookPower" runat="server"></asp:Literal>&nbsp;&nbsp;除此之外还可以查看指定您查看权限的文档</td>
       </tr>
    </table>
      
    </td>
  </tr>
  
  <tr>
    <td height="35" colspan="2" valign="top">
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999">
        <tr>
          <td height="28" bgcolor="#FFFFFF">  &nbsp;&nbsp;&nbsp;
<img alt="新建" src="../../../Images/Button/Bottom_btn_new.jpg" id="Img1" onclick="addNewPage()" style='cursor: pointer;' />
            <img alt="删除" src="../../../Images/Button/Main_btn_delete.jpg" id="btn_del" onclick="fnDel()" style='cursor: pointer;' />
                  
          </td>
        </tr>
      </table>
    </td>
  </tr>  
  <tr>
    <td colspan="2">
    <table width="99%" border="0" align="center" cellpadding="2" cellspacing="1" id="pageDataList1" bgcolor="#999999">
    <tbody>
      <tr>     
        <th width="10%"  height="20" align="center" class="td_list_fields">
            选择<input type="checkbox" id="checkall" onclick="selectall()" value="checkbox" /></th>
        <th width="13%"  align="center" class="td_list_fields">
        <div class="orderClick" onclick="OrderBy('TypeName','oTypeName');return false;">
            分类<span id="oTypeName" class="orderTip"></span></div></th>
        <th width="17%" align="center" class="td_list_fields" >
              <div class="orderClick" onclick="OrderBy('Title','oTitle');return false;">
                  主题<span id="oTitle" class="orderTip"></span></div></th>       
          <th width="10%" align="center"  class="td_list_fields">
              <div class="orderClick">
                  内容摘要</div></th>    
        <th  width="10%"  align="center" class="td_list_fields">
             <div class="orderClick" >
                 文件类型</div></th>
        <th  width="10%" align="center" class="td_list_fields">
             <div class="orderClick" onclick="OrderBy('EmployeeName','oEmployeeName');return false;"   >
                 发布人<span id="oEmployeeName" class="orderTip"></span></div></th>
                
        <th  width="10%"  align="center" style="display:none"  class="td_list_fields">
             <div class="orderClick" onclick="OrderBy('DeptName','oDeptName');return false;" style="display:none" >
                 发布部门<span id="oDeptName" class="orderTip"></span></div></th>
               
        <th width="10%"  align="center" class="td_list_fields">
            <div class="orderClick" onclick="OrderBy('CreateDate','oCreateDate');return false;">
                发布日期<span id="oCreateDate" class="orderTip"></span></div>
        </th> 
         <th width="10%"  align="center" class="td_list_fields">
             <div  class="orderClick"  onclick="OrderBy('ModifiedDate','oModifiedDate');return false;">
                 修改日期<span id="oModifiedDate" class="orderTip"></span></div> </th> 
          <th width="10%"  align="center" class="td_list_fields">
             <div  class="orderClick" ></div> </th> 
      </tr>
    </tbody>
    </table>
    
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
</body>
</html>
