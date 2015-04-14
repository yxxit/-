<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SaleDeptSel.ascx.cs" Inherits="UserControl_SaleDeptSel" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<style>
	.OfficeThingsListCss
    {
	    position:absolute;top:250px;left:250px;
	    border-width:1pt;border-color:#EEEEEE;border-style:solid;
	    width:800px;
	    display:none;
	    height:220px;
	    z-index:21;
	}
</style>
<script type="text/javascript">
    var popSellDeptObj = new Object();
    popSellDeptObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popSellDeptObj.returnName = false;

    popSellDeptObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popSellDeptObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#DeptName").val('');
        $("#DeptId").val('');
        document.getElementById('HolidaySpan_Dept').style.display = 'block';
        TurnToPageUc_Dept(currentPageIndexUc);
    }
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var OrderByUc_Dept = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_Dept(pageIndex)
    {
         
           currentPageIndexUc = pageIndex;
           var DeptName =document.getElementById("DeptName").value;           
           var DeptId =document.getElementById("DeptId").value;
          
           action="SearchDept";
           
           var free1="销售部门";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/DeptName.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+'&OrderByUc_Dept='+OrderByUc_Dept+
                    '&free1='+escape(free1)+'&DeptId='+escape(DeptId)+'&DeptName='+escape(DeptName), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_Dept_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_Dept tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetDept('" + item.id + "','" + item.DeptId + "','" + item.DeptName + "')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.DeptId + "</td>"+
                        
                        "<td height='22' align='center'>" + item.DeptName + "</td>"+
                        
                        "<td height='22' align='center'>" + item.cDepPerson + "</td>"+
                        
                         "<td height='22' align='center'>" + item.iDepGrade + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.bDepEnd+"</td>").appendTo($("#pageDataListUc_Dept tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_Dept_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_Dept({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_Dept_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_Dept_Pager").show();IfshowUc_Dept(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_Dept","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出客户信息
function SearchCustData_Dept()
{
    if(!CheckCustName_Dept())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc_Dept(1);  
   openRotoscopingDiv(false,"divCustNameS_Dept","ifmCustNameS_Dept");//弹遮罩层
    document.getElementById("HolidaySpan_Dept").style.display= "block";
}
    
function IfshowUc_Dept(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pagecountUc").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pagecountUc").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_Dept(newPageCount,newPageIndex)
{
if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc=parseInt(newPageCount);
        TurnToPageUc_Dept(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_Dept(orderColum,orderTip)
{
    if (totalRecord == 0) 
     {
        return;
     }
    ifdelUc = "0";
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    OrderByUc_Dept = orderColum+"_"+ordering;
    TurnToPageUc_Dept(1);
}







function GetDept(id,DeptId,DeptName)
{

    var cp_version ="medicine";
    if (cp_version == "medicine")
    {
         try
        {
             // LinkManClear(); //
            if (document.getElementById("SaleDeptId")) {
                document.getElementById("SaleDeptId").value ="";
            }
             if (document.getElementById("SaleDept")) {
                document.getElementById("SaleDept").value ="";
            }
            
           
            
        }
        catch(err)
        {
            
        }
       
      
      
        document.getElementById("SaleDeptId").value = DeptId;
        document.getElementById("SaleDept").value = DeptName;
        
       
        
        document.getElementById('HolidaySpan_Dept').style.display = "none";
        closeRotoscopingDiv(false,"divCustNameS_Dept");//关闭遮罩层

    }

    else
    {

    }
}

//主表单验证
function CheckCustName_Dept()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var DeptId = document.getElementById('DeptId').value;//部门编号
    var DeptName = document.getElementById('DeptName').value;//部门名称
    

    if(DeptId.length>0 && DeptId.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "部门编号输入不正确|";
    }    
    if(DeptName.length>0 && ValidText(DeptName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "部门名称输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivCustNameClose_Dept()
{

    document.getElementById("DeptId").value = "";
    document.getElementById("DeptName").value = "";

    closeRotoscopingDiv(false,"divCustNameS_Dept");//关闭遮罩层
    document.getElementById('HolidaySpan_Dept').style.display='none'; 
}

function CustClear_Dept()
{
    try
    {
      
        document.getElementById("SaleDeptId").value = "";
        document.getElementById("SaleDept").value = "";
        
         
    }
    catch(e)
    { }
    
    DivCustNameClose_Dept();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchCustData_Dept();" id="txtUcCustName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divCustNameS_Dept" style="display:none">
<iframe id="ifmCustNameS_Dept" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_Dept" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivCustNameClose_Dept();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="CustClear_Dept();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 部门编号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="DeptId" id="DeptId"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">部门名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="DeptName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                </td>
            <td bgcolor="#FFFFFF" style="width: 24%">
                </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchCustData_Dept()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_Dept" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="return false;">部门编号<span id="oDeptId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="return false;">部门名称<span id="oDeptName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="return false;">部门联系人<span id="ocDepPerson" class="orderTip"></span></div></th>   
         <th align="center" class="td_list_fields"><div class="orderClick" onclick="return false;">部门等级<span id="oiDepGrade" class="orderTip"></span></div></th>                                               
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="return false;">是否末级<span id="obDepEnd" class="orderTip"></span></div></th>
        
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountUc"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_Dept_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_Dept_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_Dept($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfCustID" type="hidden"  />
<input id="hfCustID_Ser" type="hidden" runat="server" />
<input id="hfCustNo" type="hidden"  /></td>
        </tr>
    </table>
</div>



