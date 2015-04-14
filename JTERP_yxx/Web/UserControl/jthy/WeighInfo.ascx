<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WeighInfo.ascx.cs" Inherits="UserControl_WeighInfo" %>
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


    var popWeighObj = new Object();
    popWeighObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popWeighObj.returnName = false;

    popWeighObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popWeighObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#WeighName").val('');
        $("#WeighId").val('');
        document.getElementById('HolidaySpan_Weigh').style.display = 'block';
        TurnToPageUc_Weigh(currentPageIndexUc);
    }
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var OrderByUc_Weigh = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_Weigh(pageIndex)
    {
         
           currentPageIndexUc = pageIndex;
               
           var WeighId =document.getElementById("txtTranSportID").value;
          
           action="SearchTransPort";
           
           var free1="按调货单ID检索调运单明细信息";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/TransPortManage/TransPortInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+'&OrderByUc_Weigh='+OrderByUc_Weigh+
                    '&free1='+escape(free1)+'&headid='+escape(WeighId), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_Weigh_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    var rowcount=0;
                    $("#pageDataListUc_Weigh tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")
                        rowcount++;
                        $("<tr class='newrow' id='"+rowcount+"'></tr>").append("<td height='22' align='center' width='20%'>" + "<input style='width:80%'  id='txtCarCode'"+rowcount+"  value='"+item.CarCode+"'   type='text'/>" + "</td>" +
       
                        "<td height='22' align='center' width='20%'>" +"<input  style='width:80%'  id='txtGrossWeight'"+rowcount+" value='"+item.GrossWeight+"'   type='text'/>" + "</td>"+
                        
                        "<td height='22' align='center' width='20%'>" +"<input  style='width:80%'   id='txtTareWeight'"+rowcount+"  value='"+item.TareWeight+"'   type='text'/>"  + "</td>"+
                        
                        "<td height='22' align='center' width='20%'>" + "<input  style='width:80%'    id='txtNetWeight'"+rowcount+"  value='"+item.NetWeight+"'   type='text'/>"  + "</td>"+
                                   
                                            
                        "<td height='22' align='center' width='20%'>"+"<input  style='width:80%'   id='txtSumWeight'"+rowcount+"  value='"+item.SumWeight+"'   type='text'/>" +"</td>").appendTo($("#pageDataListUc_Weigh tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_Weigh_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_Weigh({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_Weigh_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_Weigh_Pager").show();IfshowUc_Weigh(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_Weigh","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出过磅信息
function fnAddWeigh()
{
    if(!CheckJTName_Weigh())
    {
        return;
    }
    if($("#txtWeighID").val()=="")
    {
         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择调运单！");
         return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc_Weigh(1);  
    openRotoscopingDiv(false,"divJTNameS_Weigh","ifmJTNameS_Weigh");//弹遮罩层
    document.getElementById("HolidaySpan_Weigh").style.display= "block";
}
    
function IfshowUc_Weigh(count)
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
function ChangePageCountIndexUc_Weigh(newPageCount,newPageIndex)
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
        TurnToPageUc_Weigh(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_Weigh(orderColum,orderTip)
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
    OrderByUc_Weigh = orderColum+"_"+ordering;
    TurnToPageUc_Weigh(1);
}







function GetWeigh(id,Weighid,transtatevalue,StartDate,CarNo,StartStation,ArriveStation,SendNum,CreateDate)
{  
           
        
        document.getElementById("txtWeighID").value ="";  
        document.getElementById("txtWeighNo").value ="";
        document.getElementById("drpUPWeighState").value ="10";
        document.getElementById("txtCarNo").value ="";
        document.getElementById("txtStartStation").value ="";
        document.getElementById("txtEndStation").value ="";
        document.getElementById("txtCarNum").value ="";
        
                     
        document.getElementById("txtWeighID").value = id;
        document.getElementById("txtWeighNo").value = Weighid;
        document.getElementById("drpUPWeighState").value = transtatevalue;
        document.getElementById("txtCarNo").value = CarNo;
        document.getElementById("txtStartStation").value = StartStation;
        document.getElementById("txtEndStation").value = ArriveStation;
        document.getElementById("txtCarNum").value = SendNum;
         
        document.getElementById('HolidaySpan_Weigh').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_Weigh");//关闭遮罩层

    
}

//保存过磅信息
function Save_Weigh()
{
    var strfitinfo = getDropValue().join("|");
    
    
    var headid=$("#txtTranSportID").val();
    if(headid=="undefined" || headid=="null" || headid=="")
    {
        headid="0";
    }

    $.ajax({
        type: "POST",
        url:   '../../../Handler/JTHY/TransPortManage/DealTransPort.ashx',
        data:'strfitinfo=' + escape(strfitinfo)+ '&action=Uc_insertDetail&billtype=1&headid='+escape(headid),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
        

            if (data.sta > 0) {
               

                popMsgObj.ShowMsg("保存成功！");
              }
              else
              {
                popMsgObj.ShowMsg(data.info);
              }
              hidePopup();
        }
    });
    
}

//获取明细数据
function getDropValue() {
   
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("pageDataListUc_Weigh", document);
    
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
                               
                                   
            var CarCode = $("#txtCarCode" + rowid).val(); //车号
            var GrossWeight= $("#txtGrossWeight" + rowid).val(); //毛重
            var TareWeight= $("#txtTareWeight" + rowid).val(); //皮重
            var NetWeight=$("#txtNetWeight" +rowid).val();//净重
            var SumWeight=$("#txtSumWeight" + rowid).val(); //累计
            SendOrderFit_Item[i] = [[CarCode], [GrossWeight],[TareWeight],[NetWeight], [SumWeight]];

        }
        
    }
    return SendOrderFit_Item;
}

//主表单验证
function CheckJTName_Weigh()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var WeighId = document.getElementById('WeighId').value;//药品编号
    var WeighName = document.getElementById('WeighName').value;//药品名称
    

    if(WeighId.length>0 && WeighId.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "编号输入不正确|";
    }    
    if(WeighName.length>0 && ValidText(WeighName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "名称输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivJTNameClose_Weigh()
{

    document.getElementById("WeighId").value = "";
    document.getElementById("WeighName").value = "";

    closeRotoscopingDiv(false,"divJTNameS_Weigh");//关闭遮罩层
    document.getElementById('HolidaySpan_Weigh').style.display='none'; 
}

function JTClear_Weigh()
{
    try
    {
      
        document.getElementById("txtWeighID").value ="";  
        document.getElementById("txtWeighNo").value ="";
        document.getElementById("txtWeighState").value ="";
        document.getElementById("txtCarNo").value ="";
        document.getElementById("txtStartStation").value ="";
        document.getElementById("txtEndStation").value ="";
        document.getElementById("txtCarNum").value ="";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_Weigh();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_Weigh();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_Weigh" style="display:none">
<iframe id="ifmJTNameS_Weigh" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_Weigh" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_Weigh();" />
        <%--<img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_Weigh();" /> --%>
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item" style="display:none">
            <td width="10%" height="20" class="td_list_fields" align="right"> 调运单号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="WeighId" id="WeighId"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">发运时间</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="WeighName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                车次</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
               <input id="Text1"  class="tdinput"  type="text"  style="width:95%" /> </td>
          </tr>
          <tr style="display:none">
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchJTData_Weigh()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_save" alt="保存" src="../../../Images/Button/Bottom_btn_save.jpg" style='cursor:hand;' onclick='Save_Weigh()' />                      
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_Weigh" bgcolor="#999999">
    <tbody>
      <tr>       
        <th align="center" class="td_list_fields"><div class="orderClick" >车号<span id="oWeighId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >毛重<span id="oWeighName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >皮重<span id="oWeighStd" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >净重<span id="oWeighClassName" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >累计<span id="Span1" class="orderTip"></span></div></th>
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
            <td height="28"  align="right"><div id="pageDataListUc_Weigh_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_Weigh_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_Weigh($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


