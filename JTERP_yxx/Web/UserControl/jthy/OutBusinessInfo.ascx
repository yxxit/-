<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OutBusinessInfo.ascx.cs" Inherits="UserControl_OutBusinessInfo" %>
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

  
    var popOutBusObj = new Object();
    popOutBusObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popOutBusObj.returnName = false;

    popOutBusObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popOutBusObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#txtOutBusNo").val('');
        $("#txtCustName").val('');
        $("#txtProName").val('');
        document.getElementById('HolidaySpan_OutBus').style.display = 'block';
        TurnToPageUc_xsck_OutBus(currentPageIndexUc_xsck);
    }
    var pageCountUc_xsck = 10;//每页计数
    var totalRecord_xsck = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc_xsck = 1;
    var action = "";//操作
    var OrderByUc_OutBus = "";//排序字段    
    var ifdelUc = "0";
    var BusiType="";   //1为出库销售，2为采购直销

 function TurnToPageUc_xsck_OutBus(pageIndex)
    {

         
           currentPageIndexUc_xsck = pageIndex;
           var OutBusNo =document.getElementById("txtOutBusNo").value;   //销售单号        
           var CustName =document.getElementById("txtCustName").value;  //客户名称
           var ProName =document.getElementById("txtProName").value;  //煤种
          
           action="SearchOutBusToOutWare";
           
           var free1="销售发货单到销售出库单";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/BusinessManage/OutBusInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc_xsck+"&action="+action+'&OrderByUc_OutBus='+OrderByUc_OutBus+
                    '&free1='+escape(free1)+'&OutBusNo='+escape(OutBusNo)+'&CustName='+escape(CustName)+'&ProName='+escape(ProName)+'&BusiType=1'+'', //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_OutBus_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_OutBus tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")

                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetOutBus('" + item.id + "','"+item.sendno+"','"+item.settletype+
                        "','"+item.transporttype+"','"+item.custid+"','"+item.custname+"','"+item.billunit+"','"+item.sendnum+"','"
                        +item.ppersonid+"','"+item.ppersonname+"','"+item.deptid+"','"+item.deptname+"','"+item.transmoney
                        +"','"+item.diaoyunid+"','"
                        +item.diaoyunno+"','"+item.transstate+"','"+item.carno+"','"+item.startstation+"','"+item.endstation+"','"+item.carnum+"','"+item.at_state+"','"+item.providerid
                        +"','"+item.providername+"','"+item.CustJsFee+"','"+item.ProJsFee+"','"+item.SellMoney+"','"+item.ProMoney+"')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.sendno + "</td>"+
                        
                        "<td height='22' align='center'>" + item.custname + "</td>"+
                        
                        "<td height='22' align='center'>" + item.diaoyunno + "</td>"+
                        
                         "<td height='22' align='center'>" + item.productname + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.productcount+"</td>").appendTo($("#pageDataListUc_OutBus tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_OutBus_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc_xsck,currentPageIndex:currentPageIndexUc_xsck,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_xsck_OutBus({pageindex});return false;"}//[attr]
                    );
                  totalRecord_xsck = msg.totalCount;
                 // $("#pageDataListUc_OutBus_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowpageCountUc_xsck").val(pageCountUc_xsck);
                  ShowTotalPage(msg.totalCount,pageCountUc_xsck,pageIndex,$("#pageCountUc_xsck"));
                  $("#ToPageUc_xsck").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_OutBus_Pager").show();IfshowUc_OutBus(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_OutBus","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出调运信息
function fnSelectOutBusiness(type)
{


    if(!CheckJTName_OutBus())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
    BusiType=type;
        
    TurnToPageUc_xsck_OutBus(1);  
    openRotoscopingDiv(false,"divJTNameS_OutBus","ifmJTNameS_OutBus");//弹遮罩层
    document.getElementById("HolidaySpan_OutBus").style.display= "block";
}
    
function IfshowUc_OutBus(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pageCountUc_xsck").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pageCountUc_xsck").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_OutBus(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_xsck-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc_xsck=parseInt(newPageCount);
        TurnToPageUc_xsck_OutBus(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_OutBus(orderColum,orderTip)
{
    if (totalRecord_xsck == 0) 
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
    OrderByUc_OutBus = orderColum+"_"+ordering;
    TurnToPageUc_xsck_OutBus(1);
}









//获取煤种明细
function fnGetDetail_Bus(headid)
{
    var action="uc_SearchOutBusDetail";
    var orderBy="id";
    var ary = new Array();
    var rowsCount=0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url:'../../../Handler/JTHY/BusinessManage/OutBusInfo.ashx',//目标地址
        cache: false,
        data: "action=" + action + "&orderby=" + orderBy+"&headid="+escape(headid)+'',          
        beforeSend: function() {

        },
        error: function() {
          showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                ClearRows();  //清除已经存在的行
                $.each(data.data, function(i, item) {
                    rowsCount++;         
                    FillSignRow(i,item);
                });
            }
            $("#txtTRLastIndex").val(rowsCount + 1);
        },
       complete:function(){
         
        //fnTotalInfo();
         }//接收数据完毕
    });
}




 //删除已经存在的行
 function ClearRows()
 {
     
    var signFrame = findObj("TableCoalInfo",document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        while(signFrame.rows.length>1)
        {
            signFrame.deleteRow(1);
        }
    } 

 }



//主表单验证
function CheckJTName_OutBus()
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

function DivJTNameClose_OutBus()
{

    $("#txtOutBusNo").val('');
    $("#txtCustName").val('');
    $("#txtProName").val('');

    closeRotoscopingDiv(false,"divJTNameS_OutBus");//关闭遮罩层
    document.getElementById('HolidaySpan_OutBus').style.display='none'; 
}

function JTClear_OutBus()
{
    try
    {
      
        document.getElementById("txtSourceBillID").value ="";  
        document.getElementById("txtSourceBillNo").value ="";
        document.getElementById("drpSettleType").value ="";
        document.getElementById("drpTransPortType").value ="";
        document.getElementById("txtCustomerID").value ="";
        document.getElementById("txtCustomerName").value ="";
        document.getElementById("txtInvoiceUnit").value ="";
        document.getElementById("txtSendNum").value ="";
        document.getElementById("txtPPersonID").value ="";
        document.getElementById("txtPPerson").value ="";
        document.getElementById("hdDeptID").value ="";
        document.getElementById("DeptName").value ="";
        document.getElementById("txtTransMoney").value ="";
        
        document.getElementById("drpWare").value ="";
        document.getElementById("txtCoalID").value ="";
        document.getElementById("txtCoalName").value ="";
        document.getElementById("txtQuantity").value ="";
        document.getElementById("txtSaleCost").value ="";
        document.getElementById("txtTaxRate").value ="";
        document.getElementById("txtTax").value ="";
        document.getElementById("txtTaxMoney").value ="";
        
        document.getElementById("txtTranSportID").value ="";
        document.getElementById("txtTranSportNo").value ="";
        document.getElementById("txtTranSportState").value ="";
        document.getElementById("txtCarNo").value ="";  
        document.getElementById("txtStartStation").value ="";
        document.getElementById("txtEndStation").value ="";
        document.getElementById("txtCarNum").value ="";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_OutBus();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_OutBus();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_OutBus" style="display:none">
<iframe id="ifmJTNameS_OutBus" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_OutBus" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_OutBus();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_OutBus();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 销售单号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="txtOutBusNo" id="txtOutBusNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">客户名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="txtCustName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                煤种</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
               <input id="txtProName"  class="tdinput"  type="text"  style="width:95%" /> </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='TurnToPageUc_xsck_OutBus(1);' /> 
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_OutBus" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >销售单号<span id="oOutBusId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >客户名称<span id="oOutBusName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >调运单号<span id="oOutBusStd" class="orderTip"></span></div></th>                         
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
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pageCountUc_xsck"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_OutBus_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_OutBus_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowpageCountUc_xsck"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc_xsck" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_OutBus($('#ShowpageCountUc_xsck').val(),$('#ToPageUc_xsck').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


