<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserSearch.ascx.cs" Inherits="UserControl_UserSearch" %>

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
   
   
    var popPurContractObj = new Object();
    popPurContractObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popPurContractObj.returnName = false;

    popPurContractObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popPurContractObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
       
        $("#UserInfoId").val('');
        document.getElementById('HolidaySpan_UserInfo').style.display = 'block';
        TurnToPageUc_Pur_UserInfo(currentPageIndexUc_Pur);
 
 
 
    }
    var pageCountUc_Pur = 10;//每页计数
    var totalRecord_Pur = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc_Pur = 1;
    var action = "";//操作
    var OrderByUc_PurContract = "";//排序字段    



 function TurnToPageUc_Pur_UserInfo(pageIndex)
    {
      
           currentPageIndexUc_Pur = pageIndex;
          
           
            var userInfo=document.getElementById("UserInfoId").value;
            action="SearchUser";
           
          
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/SearchUser.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc_Pur+"&action="+action+'&OrderByUc_PurContract='+OrderByUc_PurContract+
                    '&UserInfo='+escape(userInfo),
           beforeSend:function(){AddPop();$("#pageDataListUc_UserInfo_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_PurContract tbody").find("tr.newrow").remove();                   
                
                 
                    $.each(msg.data,function(i,item){
                   
                 
                    if (item.id != null && item.id != "")
                      
                   
                  
                       
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetUser('"+item.UserId+"','"+item.EmployeeName+"')\" id='Checkbox1' value=" + item.UserId + "  type='radio'/>" + "</td>" +

                        "<td height='22' align='center'>" + item.UserId+ "</td>"+
                        
                                            
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>").appendTo($("#pageDataListUc_PurContract tbody"));
                         
                  
                
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_UserInfo_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc_Pur,currentPageIndex:currentPageIndexUc_Pur,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_Pur_UserInfo({pageindex});return false;"}//[attr]
                    );
                  totalRecord_Pur = msg.totalCount;
                 // $("#pageDataListUc_PurContract_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowpageCountUc_Pur").val(pageCountUc_Pur);
                  ShowTotalPage(msg.totalCount,pageCountUc_Pur,pageIndex,$("#pageCountUc_Pur"));
                  $("#ToPageUc_Pur").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_UserInfo_Pager").show();IfshowUc_PurContract(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_PurContract","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    
    



//弹出用户层
debugger;
function UserInfo()
{
   
    isPurSell="1";
    ifdelUc = "0";
    search="1";        
    TurnToPageUc_Pur_UserInfo(1);  
  
    openRotoscopingDiv(false,"divJTNameS_UserInfo","ifmJTNameS_UserInfo");//弹遮罩层
    document.getElementById("HolidaySpan_UserInfo").style.display= "block";
}




    
function IfshowUc_PurContract(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pageCountUc_Pur").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pageCountUc_Pur").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_PurContract(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_Pur-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc_Pur=parseInt(newPageCount);
        TurnToPageUc_Pur_UserInfo(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_PurContract(orderColum,orderTip)
{
    if (totalRecord_Pur == 0) 
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
    OrderByUc_PurContract = orderColum+"_"+ordering;
    TurnToPageUc_Pur_UserInfo(1);
}



// 获取采购单号后，对相应栏目赋值，包括采购到货及 采购直销
function GetUser(userId,EmployeeName)
{         
debugger;
   $("#Drp_UserInfo").val(userId); 
 

   if(userId=="0")
   {
      popMsgObj.ShowMsg("请选择用户！");
      return;
   }

   var httpRequstResult = httpRequst("../../../Handler/Office/SystemManager/GetUserName.ashx?userid="+userId);
   document.getElementById("txtUserName").value=httpRequstResult.split('|')[0];
   document.getElementById("LblRoleID").innerHTML =httpRequstResult.split('|')[1];
 


        document.getElementById('HolidaySpan_UserInfo').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_UserInfo");//关闭遮罩层    
}



function DivJTNameClose_PurContract()
{

    document.getElementById("UserInfoId").value = "";

    closeRotoscopingDiv(false,"divJTNameS_UserInfo");//关闭遮罩层
    document.getElementById('HolidaySpan_UserInfo').style.display='none'; 
}

//清除
function JTClear_PurContract()
{
    try
    {
       document.getElementById("Drp_UserInfo").value="";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_PurContract();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_PurContract();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>





<div id="divJTNameS_UserInfo" style="display:none">
<iframe id="ifmJTNameS_UserInfo" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_UserInfo" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_PurContract();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_PurContract();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 用户编号</td>
            <td width="23%" bgcolor="#FFFFFF" colspan="4"><input name="UserInfoId" id="UserInfoId"  class="tdinput"  type="text" style="width:95%" /></td>
            
           <td bgcolor="#FFFFFF" style="width: 50%">
               <input id="Text1"  class="tdinput"  type="text"  style="width:95%" /> </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='UserInfo()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_PurContract" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >用户编号<span id="oUserId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >用户姓名<span id="oUserName" class="orderTip"></span></div></th> 
       
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pageCountUc_Pur"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_UserInfo_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_PurContract_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowpageCountUc_Pur"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc_Pur" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_PurContract($('#ShowpageCountUc_Pur').val(),$('#ToPageUc_Pur').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


