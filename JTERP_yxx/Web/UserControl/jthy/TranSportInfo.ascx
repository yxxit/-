<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TranSportInfo.ascx.cs" Inherits="UserControl_TranSportInfo" %>
<%--<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>--%>
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
    var popTranSportObj = new Object();
    popTranSportObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popTranSportObj.returnName = false;

    popTranSportObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popTranSportObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#TranSportNo").val('');
        $("#txt_motorcade").val('');
        $("#TranSportInfo_txtBeginTxx").val('');
        $("#TranSportInfo_txtEndTxx").val('');
        
        document.getElementById('HolidaySpan_TranSport').style.display = 'block';
        TurnToPageUc_tra_TranSport(currentPageIndexUc_tra);
    }
    var pageCountUc_tra = 10;//每页计数
    var totalRecord_tra = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc_tra = 1;
    var action = "";//操作
    var OrderByUc_TranSport = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_tra_TranSport(pageIndex)
    {
           currentPageIndexUc_tra = pageIndex;
           var TranSportNo =document.getElementById("TranSportNo").value;   //调运单编号        
           var motorcade=$("#txt_motorcade").val();  //车次
            var BeginTxx=$("#TranSportInfo_txtBeginTxx").val();  //发运开始时间
            var EndTxx = $("#TranSportInfo_txtEndTxx").val();       //发运结束时间  
            var ArriveStation = $("#ArriveStation").val();       //到站  
            var StartStation = $("#StartStation").val();       //发站  
                
            if (document.getElementById("iscountCheck").checked) {//是否显示全部  
                iscount = 1;
            } else {
                iscount = 0;
            }
           action="uc_SearchTransPortList";           
           var free1="火车调运单";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/TransPortManage/TransPortInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc_tra+"&action="+action+'&OrderByUc_TranSport='+OrderByUc_TranSport+
                    '&free1=' + escape(free1) + '&TranSportNo=' + escape(TranSportNo) +
                    '&motorcade=' + escape(motorcade) + '&BeginT=' + escape(BeginTxx) +
                    '&EndT=' + escape(EndTxx) + '&iscount=' + escape(iscount)
                    + '&StartStation=' + escape(StartStation) + '&ArriveStation=' + escape(ArriveStation)
                    , //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_TranSport_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_TranSport tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    if (item.id != null && item.id != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" 
                        + "<input  onclick=\"GetTranSport('"+item.GetNum+"','" + item.id + "','" + item.transportid + "','"+item.transstate+"','" + item.transtatevalue + "','"+item.StartDate+"','"+item.CarNo+"','"+item.carNum+"','"+item.StartStation+"','"+item.ArriveStation+"','"+item.SendNum+"','"+item.CreateDate+"')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.transportid + "</td>"+
                        
                        "<td height='22' align='center'>" + item.transstate + "</td>"+
                        
                        "<td height='22' align='center'>" + item.StartDate + "</td>"+
                        
                         "<td height='22' align='center'>" + item.CarNo + "</td>"+
                         
                         "<td height='22' align='center'>" + item.StartStation + "</td>"+
                          
                         "<td height='22' align='center'>" + item.ArriveStation + "</td>"+
                           
                         "<td height='22' align='center'>" + item.SendNum + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.CreateDate+"</td>").appendTo($("#pageDataListUc_TranSport tbody"));
                   });
                    //页码
                   ShowPageBar("pageDataListUc_TranSport_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc_tra,currentPageIndex:currentPageIndexUc_tra,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_tra_TranSport({pageindex});return false;"}//[attr]
                    );
                  totalRecord_tra = msg.totalCount;
                  // $("#pageDataListUc_TranSport_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowpageCountUc_tra").val(pageCountUc_tra);
                  ShowTotalPage(msg.totalCount,pageCountUc_tra,pageIndex,$("#pageCountUc_tra"));
                  $("#ToPageUc_tra").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_TranSport_Pager").show();IfshowUc_TranSport(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_TranSport","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
   }

//弹出调运信息
function fnSelTranSport()
{
    if(!CheckJTName_TranSport())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
    $("#HolidaySpan_TranSport").css("top",document.body.clientHeight-320+'px');  
    TurnToPageUc_tra_TranSport(1);  
    openRotoscopingDiv(false,"divJTNameS_TranSport","ifmJTNameS_TranSport");//弹遮罩层
    document.getElementById("HolidaySpan_TranSport").style.display= "block";
}
    
function IfshowUc_TranSport(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pageCountUc_tra").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pageCountUc_tra").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_TranSport(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_tra-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc_tra=parseInt(newPageCount);
        TurnToPageUc_tra_TranSport(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_TranSport(orderColum,orderTip)
{
    if (totalRecord_tra == 0) 
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
    OrderByUc_TranSport = orderColum+"_"+ordering;
    TurnToPageUc_tra_TranSport(1);
}
//选中，之后给他们赋值
function GetTranSport(GetNum,id,transportid,transstate,transtatevalue,StartDate,CarNo,carNum,StartStation,ArriveStation,SendNum,CreateDate)
{     
        document.getElementById("txtTranSportID").value ="";  
        document.getElementById("txtTranSportNo").value ="";
        document.getElementById("txtCarNo").value ="";
        document.getElementById("txtStartStation").value ="";
        document.getElementById("txtEndStation").value ="";
        $("#txtTranSportState").val(transstate);  //当前状态
        $("#txtSendNum").val(SendNum);   //原发吨数
        $("#txtCarNum").val(carNum);        //发车数
        $("#txtSendTime").val(StartDate);   //发运时间
        $("#txtGetNum").val(GetNum);     //实收吨数
        $("#txtResidueNum").val(SendNum-GetNum);     //剩余吨数
        
                     
        document.getElementById("txtTranSportID").value = id;
        document.getElementById("txtTranSportNo").value = transportid;
        //document.getElementById("drpUPTranSportState").value = transtatevalue;
        document.getElementById("txtCarNo").value = CarNo; 
        document.getElementById("txtStartStation").value = StartStation;
        document.getElementById("txtEndStation").value = ArriveStation; 
        document.getElementById('HolidaySpan_TranSport').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_TranSport");//关闭遮罩层
}

//主表单验证
function CheckJTName_TranSport()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}

function DivJTNameClose_TranSport()
{

//    document.getElementById("TranSportId").value = "";
//    document.getElementById("TranSportName").value = "";

    closeRotoscopingDiv(false,"divJTNameS_TranSport");//关闭遮罩层
    document.getElementById('HolidaySpan_TranSport').style.display='none'; 
}

function JTClear_TranSport()
{
    try
    {
      
        document.getElementById("txtTranSportID").value ="";  
        document.getElementById("txtTranSportNo").value ="";
        document.getElementById("txtTranSportState").value ="";
        document.getElementById("txtCarNo").value ="";
        document.getElementById("txtStartStation").value ="";
        document.getElementById("txtEndStation").value ="";
        document.getElementById("txtCarNum").value ="";
        document.getElementById("drpUPTranSportState").value ="10";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_TranSport();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_TranSport();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_TranSport" style="display:none">
<iframe id="ifmJTNameS_TranSport" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_TranSport" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none; 
        left: 55%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_TranSport();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_TranSport();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 调运单号</td>
            <td width="16%" bgcolor="#FFFFFF"><input name="TranSportNo" id="TranSportNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">发运时间</td>
            <td class="tdColInput">
                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" style="width: 45%">
                            <input type="text" name="txtBeginTxx" id="txtBeginTxx" runat="server" style="width: 95%;" class="tdinput"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" readonly />
                        </td>
                        <td style="width: 10%">
                            <input id="Text8" runat="server" class="tdinput" value="至" type="text" style="width: 88%;" />
                        </td>
                        <td style="width: 45%">
                            <input type="text" name="txtEndTxx" id="txtEndTxx" runat="server" style="width: 95%;" class="tdinput"
                                onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('OrderDate')})" readonly />
                        </td>
                    </tr>
                </table>
            </td>            
            <td class="td_list_fields" align="right" width="10%">
                车次</td>
            <td bgcolor="#FFFFFF" style="width: 16%">
               <input type="text" id="txt_motorcade"  name="txt_motorcade" class="tdinput" />
            </td>
            <td>
                                    
            <input type="checkbox" class="td_list_fields" style="width: 14%"   id="iscountCheck" name="iscount" />显示全部调运
            </td>
          </tr>
          <!-- 2014-9-25 yxx -->
          <tr class="table-item">
            <td height="20" class="td_list_fields" align="right">发站</td>
            <td bgcolor="#FFFFFF">
                <input name="StartStation" id="StartStation"  class="tdinput"  type="text" style="width:95%" />
            </td>
            <td height="20" class="td_list_fields" align="right">到站</td>
            <td bgcolor="#FFFFFF">
                <input name="ArriveStation" id="ArriveStation"  class="tdinput"  type="text" style="width:95%" />
            </td>
            <td colspan="5"></td>
          </tr>
          <tr>
            <td colspan="7" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='TurnToPageUc_tra_TranSport(1)' /> 
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_TranSport" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >调运单号<span id="oTranSportId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >调运单状态<span id="oTranSportName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >发运时间<span id="oTranSportStd" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >车次<span id="oTranSportClassName" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >发站<span id="Span1" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >到站<span id="Span2" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >原发吨数<span id="Span3" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >制单日期<span id="Span4" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pageCountUc_tra"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_TranSport_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_TranSport_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowpageCountUc_tra"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc_tra" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_TranSport($('#ShowpageCountUc_tra').val(),$('#ToPageUc_tra').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


