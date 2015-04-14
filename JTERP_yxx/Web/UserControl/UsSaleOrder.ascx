<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsSaleOrder.ascx.cs" Inherits="UserControl_UsSaleOrder" %>
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

    var popSellSaleOrderObj = new Object();
    popSellSaleOrderObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popSellSaleOrderObj.returnName = false;

    popSellSaleOrderObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popSellSaleOrderObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
       
        $("#SaleOrderId").val('');
         $("#Date1").val('');
          $("#Date2").val('');
        document.getElementById('HolidaySpan_SaleOrder').style.display = 'block';
        var isinit=1;
        TurnToPageUc_SaleOrder(currentPageIndexUc,isinit);
    }
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var OrderByUc_SaleOrder = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_SaleOrder(pageIndex,isinit)
    {
         
           currentPageIndexUc = pageIndex;
          
           var SaleOrderId =document.getElementById("SaleOrderId").value;
          var Date1=document.getElementById("Date1").value;
          var Date2=document.getElementById("Date2").value;
          var custname=document.getElementById("custname").value;
           action="SearchSaleOrder";
           
           var free1="物运信息";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/SaleOrderName_New.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+'&OrderByUc_SaleOrder='+OrderByUc_SaleOrder+
                    '&free1='+escape(free1)+'&SaleOrderId='+escape(SaleOrderId)+'&Date1='+escape(Date1)+'&Date2='+escape(Date2)+'&custname='+escape(custname)+'&isinit='+escape(isinit), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_SaleOrder_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_SaleOrder tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetSaleOrder('" + item.id + "','" + item.OrderNo + "','" + item.CustName + "','"+item.LinkManName+"','"+item.orderDate+"','"+item.ExpressOrderNum+"','"+item.cCusPerson+"','"+item.cCusPhone+"','"+item.cCusOAddress+"')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.OrderNo + "</td>"+
                        
                        "<td height='22' align='center'>" + item.CustName + "</td>"+
                        
                        "<td height='22' align='center'>" + item.LinkManName + "</td>"+
                        
                         "<td height='22' align='center'>" + item.orderDate + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.ExpressOrderNum+"</td>").appendTo($("#pageDataListUc_SaleOrder tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_SaleOrder_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_SaleOrder({pageindex},0);return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_SaleOrder_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_SaleOrder_Pager").show();IfshowUc_SaleOrder(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_SaleOrder","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出客户信息
function SearchSaleOrderData_SaleOrder()
{
    if(!CheckSaleOrderName_SaleOrder())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc_SaleOrder(1,0);  
   openRotoscopingDiv(false,"divSaleOrderNameS_SaleOrder","ifmSaleOrderNameS_SaleOrder");//弹遮罩层
    document.getElementById("HolidaySpan_SaleOrder").style.display= "block";
}
    
function IfshowUc_SaleOrder(count)
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
function ChangePageCountIndexUc_SaleOrder(newPageCount,newPageIndex)
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
        TurnToPageUc_SaleOrder(parseInt(newPageIndex),0);
    }
}
//排序
function OrderByUc_SaleOrder(orderColum,orderTip)
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
    OrderByUc_SaleOrder = orderColum+"_"+ordering;
    TurnToPageUc_SaleOrder(1,0);
}







function GetSaleOrder(id,OrderNo,CustName,LinkManName,orderDate,ExpressOrderNum,cCusPerson,cCusPhone,cCusOAddress)
{

    var cp_version ="medicine";
    if (cp_version == "medicine")
    {
         try
        {
             // LinkManClear(); //
            if (document.getElementById("txtSaleOrderNo")) {
                document.getElementById("txtSaleOrderNo").value ="";
            }
//             if (document.getElementById("txtExpressNo")) {
//                document.getElementById("txtExpressNo").value ="";
//            }
              
            
           
            
        }
        catch(err)
        {
            
        }
       
      
      
        document.getElementById("txtSaleOrderNo").value = OrderNo;
        
        if(document.getElementById("txtExpressNo").value =="")
        {
            document.getElementById("txtExpressNo").value = ExpressOrderNum;
        }
        
        if(document.getElementById("txtPerson").value =="")
        {
            document.getElementById("txtPerson").value = LinkManName;
        }
        if(document.getElementById("txtPersonPhone").value =="")
        {
            document.getElementById("txtPersonPhone").value = cCusPhone;
        }
        if(document.getElementById("txtFHAddress").value =="")
        {
            document.getElementById("txtFHAddress").value = cCusOAddress;
        }
        if(document.getElementById("txtCustName").value =="")
        {
        document.getElementById("txtCustName").value =CustName;
        }
       
        
        document.getElementById('HolidaySpan_SaleOrder').style.display = "none";
        closeRotoscopingDiv(false,"divSaleOrderNameS_SaleOrder");//关闭遮罩层

    }

    else
    {

    }
}

//主表单验证
function CheckSaleOrderName_SaleOrder()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var SaleOrderId = document.getElementById('SaleOrderId').value;//药品编号
   
    

    if(SaleOrderId.length>0 && SaleOrderId.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "订单号输入不正确|";
    }    
    
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivSaleOrderNameClose_SaleOrder()
{

    document.getElementById("SaleOrderId").value = "";
    document.getElementById("Date1").value = "";
    document.getElementById("Date2").value = "";
   

    closeRotoscopingDiv(false,"divSaleOrderNameS_SaleOrder");//关闭遮罩层
    document.getElementById('HolidaySpan_SaleOrder').style.display='none'; 
}

function SaleOrderClear_SaleOrder()
{
    try
    {
      
        document.getElementById("txtSaleOrderNo").value = "";
       
       
         
    }
    catch(e)
    { }
    
    DivSaleOrderNameClose_SaleOrder();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchSaleOrderData_SaleOrder();" id="txtUcSaleOrderName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divSaleOrderNameS_SaleOrder" style="display:none">
<iframe id="ifmSaleOrderNameS_SaleOrder" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_SaleOrder" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivSaleOrderNameClose_SaleOrder();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="SaleOrderClear_SaleOrder();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 订单号</td>
            <td width="15%" bgcolor="#FFFFFF"><input name="SaleOrderId" id="SaleOrderId"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">订单时间</td>
            <td width="40%" bgcolor="#FFFFFF">
            <input name="Date1" id="Date1"  class="tdinput"  type="text" style="width:45%"  onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('Date1')})" />
             至
       <input name="Date2" id="Date2"  class="tdinput"  type="text" style="width:45%"  onClick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('Date2')})" />
            </td>            
            <td class="td_list_fields" align="right" width="10%">
           客户名称
                </td>
            <td bgcolor="#FFFFFF" style="width: 24%">
           
             <input name="custname" id="custname"  class="tdinput"  type="text" style="width:95%" />
           
                </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchSaleOrderData_SaleOrder()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_SaleOrder" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_SaleOrder('OrderNo','oOrderNo');return false;">订单号<span id="oOrderNo" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_SaleOrder('CustName','oCustName');return false;">客户名称<span id="oCustName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_SaleOrder('LinkManName','oLinkManName');return false;">联系人<span id="oLinkManName" class="orderTip"></span></div></th>  
          <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_SaleOrder('orderDate','orderDate');return false;">订单时间<span id="orderDate" class="orderTip"></span></div></th>                                                
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_SaleOrder('ExpressOrderNum','oExpressOrderNum');return false;">快递单号<span id="oExpressOrderNum" class="orderTip"></span></div></th>
        
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
            <td height="28"  align="right"><div id="pageDataListUc_SaleOrder_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_SaleOrder_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_SaleOrder($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfSaleOrderID" type="hidden"  />
<input id="hfSaleOrderID_Ser" type="hidden" runat="server" />
<input id="hfSaleOrderNo" type="hidden"  /></td>
        </tr>
    </table>
</div>






