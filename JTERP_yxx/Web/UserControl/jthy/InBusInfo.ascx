<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InBusInfo.ascx.cs" Inherits="UserControl_InBusInfo" %>
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

  
    var popInBusObj = new Object();
    popInBusObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popInBusObj.returnName = false;

    popInBusObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popInBusObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#ProviderName").val('');
        $("#InBusNo").val('');
        $("#ColName").val('');
        document.getElementById('HolidaySpan_InBus').style.display = 'block';
        TurnToPageUc_InBus(currentPageIndexUc);
    }
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var OrderByUc_InBus = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_InBus(pageIndex)
    {

           currentPageIndexUc = pageIndex;
           var ProviderName =document.getElementById("ProviderName").value;           
           var InBusNo =document.getElementById("InBusNo").value;
           var ColName=$("#ColName").val();
          
           action="SearchInBusListToTest";
           
           var free1="到货单列表到质检单";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/BusinessManage/InBusInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+'&OrderByUc_InBus='+OrderByUc_InBus+
                    '&free1='+escape(free1)+'&InBusNo='+escape(InBusNo)+'&ProviderName='+escape(ProviderName)+'&ColName='+escape(ColName), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_InBus_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_InBus tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.ID != null && item.ID != "")

                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetInBus('" + item.ID + "','" + item.ArriveNo + "','" + item.ProviderID + "','"+item.providername+"','"+item.diaoyunid+"','"+item.diaoyunno+"','"+item.ProductID+"','"+item.productname+"','"+item.ProductCount+"','"+item.TotalFee+"','"+item.ProJsFee+"')\" id='Checkbox1' value=" + item.ID + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.ArriveNo + "</td>"+
                        
                        "<td height='22' align='center'>" + item.providername + "</td>"+
                        
                        "<td height='22' align='center'>" + item.diaoyunno + "</td>"+
                        
                         "<td height='22' align='center'>" + item.productname + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.ProductCount+"</td>").appendTo($("#pageDataListUc_InBus tbody"));
                   });
                   //debugger;
                    //页码
                   ShowPageBar("pageDataListUc_InBus_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_InBus({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_InBus_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_InBus_Pager").show();IfshowUc_InBus(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_InBus","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出调运信息
function fnSelectInBus()
{
    if(!CheckJTName_InBus())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc_InBus(1);  
    openRotoscopingDiv(false,"divJTNameS_InBus","ifmJTNameS_InBus");//弹遮罩层
    document.getElementById("HolidaySpan_InBus").style.display= "block";
}
    
function IfshowUc_InBus(count)
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
function ChangePageCountIndexUc_InBus(newPageCount,newPageIndex)
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
        TurnToPageUc_InBus(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_InBus(orderColum,orderTip)
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
    OrderByUc_InBus = orderColum+"_"+ordering;
    TurnToPageUc_InBus(1);
}







//主表单验证
function CheckJTName_InBus()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var InBusNo = document.getElementById('InBusNo').value;//到货单编号
    var ProviderName = document.getElementById('ProviderName').value;//供应商名称
    var ColName=$("#ColName").val();    //煤种
    

    if(InBusNo.length>0 && InBusNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "到货单编号输入不正确|";
    }    
    if(ProviderName.length>0 && ValidText(ProviderName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "供应商名称输入不正确|";
    }
    if(ColName.length>0 && ValidText(ColName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "煤种输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivJTNameClose_InBus()
{

    document.getElementById("InBusNo").value = "";
    document.getElementById("ProviderName").value = "";
    $("#ColName").val('');

    closeRotoscopingDiv(false,"divJTNameS_InBus");//关闭遮罩层
    document.getElementById('HolidaySpan_InBus').style.display='none'; 
}

function JTClear_InBus()
{
    try
    {
      
        document.getElementById("txtSourceBillID").value ="";  
        document.getElementById("txtSourceBillNo").value ="";
        document.getElementById("txtProviderID").value ="";
        document.getElementById("txtProviderName").value ="";
        document.getElementById("txtTranSportID").value ="";
        document.getElementById("txtTranSportNo").value ="";
        document.getElementById("txtCoalID").value ="";
        document.getElementById("txtCoalName").value ="";
        document.getElementById("txtQuantity").value ="";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_InBus();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_InBus();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_InBus" style="display:none">
<iframe id="ifmJTNameS_InBus" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_InBus" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_InBus();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_InBus();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 到货单号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="InBusNo" id="InBusNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">供应商名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="ProviderName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                煤种</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
               <input id="ColName"  class="tdinput"  type="text"  style="width:95%" /> </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='fnSelectInBus()' /> 
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_InBus" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >到货单号<span id="oInBusNo" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >供应商名称<span id="oInBusName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >调运单号<span id="oInBusStd" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >煤种<span id="Span1" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >数量<span id="Span2" class="orderTip"></span></div></th> 
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
            <td height="28"  align="right"><div id="pageDataListUc_InBus_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_InBus_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_InBus($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


