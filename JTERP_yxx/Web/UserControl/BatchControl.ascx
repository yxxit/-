<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BatchControl.ascx.cs" Inherits="UserControl_BatchControl" %>


   <style type="text/css">

.tdinput
{
	border-width:0pt;
	background-color:#ffffff;
	height:21px;
	margin-left:2px;
}
  #userList 
        {
            border:solid 1px #111111;
            width:165px;
            z-index:11;
            display:none;
            position:absolute;
            background-color:White;
            
        }
    </style>
    <a name="pageprodDataListMark"></a>
    <span id="Forms" class="Spantype"></span>
    <!--提示信息弹出详情start-->
 <%--   <div id="divzhezhao1" style="width: 950px;padding: 10px; height: 500px;  z-index: 199; position: absolute; display: none; top: 20%; left: 10%;">
        <iframe style="border: 0; width: 950px; height: 100%; position: absolute;"></iframe>
    </div>--%>
    <div id="divStorageProduct1" style="padding : 10px; width: 1015px; z-index: 1000; position: absolute; top: 20%; left:2%; display:none" onmousedown="javascript:moveStart(event,this.id);">

        <div id="divquery1" style="width: 100%; left: 0px;">
                   <%-- <a onclick="closeProductdiv1('divStorageProduct1')" style="text-align: right; cursor: pointer">关闭</a>--%>
                   
                   <input id="hf_typeid" type="hidden" />
                    <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0"  id="mainindex">
                            <tr>
            <td valign="top">
                <%--<a onclick="closeProductdiv1()" style="text-align: right; cursor: pointer" class="Blue">关闭</a>--%>
                <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeProductdiv1();' />
            </td>
           
        </tr>
        <tr>
            <td valign="top">
                <img src="../../../images/Main/Line.jpg" width="122" height="7" /> 
            </td>
            <td rowspan="2" align="right" valign="top">
                <div id='searchClick'>
                    <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" /></div>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" class="Blue">
                <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="100%" border="0" align="left" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                     <td width="10%" height="20" class="td_list_fields" align="right">
                                            物品编号
                                        </td>
                                        <td width="23%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_ProdNO" specialworkcheck="物品编号" class="tdinput" />
                                        </td>
                                        <td width="10%" height="20" class="td_list_fields" align="right">
                                            物品名称
                                        </td>
                                        <td width="23%" bgcolor="#FFFFFF">
                                            <input type="text" id="txt_ProdName" specialworkcheck="物品名称" class="tdinput" />
                                        </td>
                                        <td class="td_list_fields" align="right" width="10%">
                                            批次
                                        </td>
                                        <td bgcolor="#FFFFFF" width="24%">
                                            <input type="text" id="txt_PYShort" specialworkcheck="批次" name="txtConfirmorReal2"
                                                class="tdinput" />
                                        </td>
                                </tr>
                                <tr>
                                    <td colspan="8" align="center" bgcolor="#FFFFFF">
                                       <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                            onclick='Fun_Search_Product1()' id="btn_search" />
                                             <img alt="确定" src="../../../images/Button/Bottom_btn_ok.jpg" style='cursor: hand;'
                                            onclick="GetValue();" id="imgsure" />
        <table width="100%" border="0" align="left" cellpadding="0" cellspacing="1" id="pageDataListProduct1"
            bgcolor="#999999">
            <tbody>
                <tr>
                    <th height="20" align="center" class="td_list_fields">
                        选择
                    </th>
                    <%-- <th align="center" class="td_list_fields">
                        <div class="orderClick">
                            查看<span id="oID" class="orderTip"></span></div>
                    </th>--%>
                    <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            物品编号</span></div>
                    </th>
                    <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            物品名称</div>
                    </th>
                    <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            单位</div>
                    </th>
                     <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            批次</div>
                    </th>
                     <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            仓库名称</div>
                    </th>
                      <th align="center" class="td_list_fields">
                        <div class="orderClick" >
                            可用存量</div>
                    </th>
                </tr>
            </tbody>
        </table>
                                        </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" style=" margin-left:10px; position:relative">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="pagecountProduct">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pagerproduct" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpageProduct">
                                            <input name="text" type="text" id="TextProduct" style="display: none" />
                                            <span id="pageDataList1_TotalProduct"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCountProduct" size="6" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPageproduct"  size="6"/>
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexProduct($('#ShowPageCountProduct').val(),$('#ToPageproduct').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="userList" style="display:none;">
  <iframe id="aaaa" style="position: absolute; z-index: -1; width:165px; height:100px;" frameborder="0">  </iframe>  
<div style="background-color:Silver;padding:3px; height:20px; padding-left:50px; padding-top:1px">
<a href="javascript:hidetxtUserList()" style="float:right;">关闭</a>
</div>
<div style=" padding-top:5px; height:300px; width:165px; overflow:auto; margin-top:1px">
    <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
    </asp:TreeView></div>
</div>
        </div>
    </div>
    <!--提示信息弹出详情end-->
<script language="javascript"  type="text/javascript">
var popBatchObj=new Object();
popBatchObj.InputObj = null;
popBatchObj.OperateObj = null;
popBatchObj.Special = null; 
popBatchObj.CheckSpecial = null;
var QueryID="0";
popBatchObj.ShowList=function(objInput)
{
    popBatchObj.InputObj = objInput;
    popBatchObj.CheckSpecial=null;
Hidebtn();
}
popBatchObj.ShowListOperate=function(objInput,objOperate)
{
    popBatchObj.InputObj = objInput;
    popBatchObj.OperateObj = objOperate;
      popBatchObj.CheckSpecial=null;
   Hidebtn();
}
popBatchObj.ShowListSpecial=function(objInput,Special)
{
if(document.getElementById('ddlInStorage')!=null)
{
   if(Special!="DETAIL"&&Special!="-1")
   {
       Special=document.getElementById('ddlInStorage').value;

   }
}
 
    popBatchObj.InputObj = objInput;
    popBatchObj.Special = Special;
    QueryID=Special;//生成DETAIL
  popBatchObj.CheckSpecial=null;
   Hidebtn1();
}

function Hidebtn1()
{
 document.getElementById('divStorageProduct1').style.display='inline';
    //document.getElementById('divzhezhao1').style.display='inline';
// document.getElementById("btnAll").style.display='none';
 document.getElementById("imgsure").style.display='none';
  //AlertProductMsg();
  Fun_Search_Product1();
}
popBatchObj.ShowListCheckSpecial=function(objInput,CheckSpecial)
{
   
    popBatchObj.InputObj = objInput;
    popBatchObj.CheckSpecial = CheckSpecial;
    
    document.getElementById('divStorageProduct1').style.display='inline';
   // document.getElementById('divzhezhao1').style.display='inline';
   // AlertProductMsg();
    Fun_Search_Product1();
//     document.getElementById("btnAll").style.display='inline';
 document.getElementById("imgsure").style.display='inline';
//    document.getElementById("btnAll").style.display='block';
}
    var ProdNo="";
    var ProdName="";
    var batch="";
  
    var pageCountproduct = 10;//每页计数
    var totalRecord = 0;
    var pagerproductStyle = "flickr";//jPagerBar样式
    var flag="";
     var str="";
    var currentPageIndexproduct = 1;
    var action = "";//操作
    var orderByP = "ID";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPageProduct1(pageIndex)
    {//document.getElementById("btnAll").checked=false;
          currentPageIndexproduct = pageIndex;
           var ProductName="";
           var TypeID="";
           var UnitID = "";
           var UsedStatus = "";
           var Remark = "";
           var EFIndex="";
           var EFDesc="";
           var urlParameter="pageIndex="+pageIndex+"&pageCount="+pageCountproduct+"&action=getBatch&orderByP="+orderByP+"&ProductID="+escape(ProdNo)+"&ProdName="+escape(ProdName)+"&PYShort="+escape(PYShort)+"&ProdNo="+popBatchObj.CheckSpecial;
          
//           if(document.getElementById("spanOther").style.display!="none")
//          {
//            EFIndex=document.getElementById("selEFIndex").value;
//            EFDesc=document.getElementById("txtEFDesc").value;
//          }
          
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/BatchProductInfo.ashx',//目标地址
           cache:false,
           data:urlParameter,//数据
           beforeSend:function(){$("#pageDataList_PagerProduct").hide();},//发送数据之前
           
         success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListProduct1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                       
                        var tempOnclick="";
                        var CheckSpe="";
//                        if(popBatchObj.Special!=null)
//                        {
//                            tempOnclick = ""+  item.ID+",'"+escape(item.ProdNo)+"','"+escape(item.ProductName)+"','"+item.StandardSell+"','"+item.UnitID+"','"+item.CodeName+"','"+item.TaxRate+"','"+item.SellTax+"','"+item.Discount+"','"+item.Specification+"','"+item.CodeTypeName+"','"+item.TypeID+"','"+item.StandardCost+"','"+item.Source+"'";
//                        }
//                        else
//                        {
//                         tempOnclick= ""+  item.ID+",'"+escape(item.ProdNo)+"','"+escape(item.ProductName)+"','"+item.StandardSell+"','"+item.UnitID+"','"+item.CodeName+"','"+item.TaxRate+"','"+item.SellTax+"','"+item.Discount+"','"+item.Specification+"','"+item.CodeTypeName+"','"+item.TypeID+"'";
//                        }
//                        if(popBatchObj.CheckSpecial!=null)
//                        {
//                        CheckSpe=" <td height='22' align='center'>"+" <input id='CheckboxProdID' name='CheckboxProdID'  value="+item.ID+"  type='checkbox'/>"+"</td>";
//                        }
//                        else if(typeof(popBatchObj.CheckSpecial)=="undefined"||(popBatchObj.CheckSpecial==null))
//                        {
                         //CheckSpe=" <td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_Content("+popBatchObj.InputObj+","+item.Batch+");\" />"+"</td>";
////                             CheckSpe=" <td height='22' align='center'>"+" <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillParent_Content("+tempOnclick+");closeProductdiv1();\" />"+"</td>";

//                        }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'> <input type=\"radio\" name=\"radioTech\" id=\"radioTech_"+item.ID+"\" value=\""+item.ID+"\" onclick=\"Fun_FillBatch('"+popBatchObj.InputObj+"','"+item.Batch+"','"+item.storageid+"','"+item.storagename+"');\" /></td>"+
                        "<td height='22' align='center' >" + item.ProdNo + "</td>"+
                        "<td height='22' align='center'>"+item.ProductName+"</td>"+
                        "<td height='22' align='center'>"+item.CodeName+"</td>"+
                        "<td height='22' align='center'>"+item.Batch+"</td>"+
                        "<td height='22' align='center'>"+item.storagename+"</td>"+
                        "<td align='center'>"+item.productcount+"</td>").appendTo($("#pageDataListProduct1 tbody"));
                        }
                   });
                  
                     //页码
                      ShowPageBar("pageDataList1_Pagerproduct",//[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerproductStyle,mark:"pageprodDataListMark",
                    totalCount:msg.totalCount,
                    showPageNumber:3,
                    pageCount:pageCountproduct,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上页",
                    nextWord:"下页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPageProduct1({pageindex});return false;"}//[attr]
                    );
                      totalRecord = msg.totalCount;
                     // $("#pageDataList1_TotalProduct").html(msg.totalCount);//记录总条数
                      document.all["TextProduct"].value=msg.totalCount;
                      $("#ShowPageCountProduct").val(pageCountproduct);
                      ShowTotalPage(msg.totalCount,pageCountproduct,pageIndex);
                      $("#ToPageproduct").val(pageIndex);
                          ShowTotalPage(msg.totalCount,pageCountproduct,currentPageIndexproduct,$("#pagecountProduct"));
                      //document.getElementById('tdResult').style.display='block';
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pagerproduct").show();pageDataListProduct1("pageDataListProduct1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
       //table行颜色
function pageDataListProduct1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}

function Fun_Search_Product1(aa)
{
      var fieldText = "";
      var msgText = "";
      var isFlag = true;
     ProdNo=document.getElementById("txt_ProdNO").value;
     ProdName=document.getElementById("txt_ProdName").value;
     PYShort=document.getElementById("txt_PYShort").value;
     
     var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
//    if(StartStorage.length>0)
//    {
//     if(!IsNumericFH(StartStorage,15,15))
//    {
//      isFlag = false;
//           fieldText += "开始数量|";
//                msgText += "开始数量必须是数字格式|";
//    }
//    }
////      if(EndStorage.length>0)
////    {
////     if(!IsNumericFH(EndStorage,15,15))
////    {
////      isFlag = false;
////      fieldText += "结束数量|";
////      msgText += "结束数量必须是数字格式|";
////    }
////    }
//   if(StartStorage.length>0&&EndStorage.length>0)
//   {
//    if(parseFloat(StartStorage)>parseFloat(EndStorage))
//    {
//      isFlag = false;
//      fieldText += "库存查询范围|";
//      msgText += "开始数量不能大于结束数量|";
//    }
//   }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return ;
    }
      search="1";
     TurnToPageProduct1(1)
}
//    function Ifshowproduct(count)
//    {
//        if(count=="0")
//        {
//            document.all["divpageProduct"].style.display = "none";
//            document.all["pagecountProduct"].style.display = "none";
//        }
//        else
//        {
//            document.all["divpageProduct"].style.display = "block";
//            document.all["pagecountProduct"].style.display = "block";
//        }
//    }
    function SelectDept(retval)
    {
        alert(retval);
    }

 //改变每页记录数及跳至页数
    function ChangePageCountIndexProduct(newPageCount,newPageIndex)
    {
     if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCountproduct=parseInt(newPageCount);
            TurnToPageProduct1(parseInt(newPageIndex));
            //document.getElementById("btnAll").checked=false;
        }
    }

    //排序
    function OrderByProduct(orderColum,orderTip)
    {
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
        orderBy = orderColum+"_"+ordering;
      TurnToPageProduct1(1);
    }
 

function closeProductdiv1()
{
    document.getElementById("divStorageProduct1").style.display="none";
    document.getElementById("txt_ProdNO").value="";
    document.getElementById("txt_ProdName").value="";
    document.getElementById("txt_PYShort").value="";
    document.getElementById("hf_typeid").value="";
    var ProductBigDiv = document.getElementById("ProductBigDiv");
		var divStorageProduct1 = document.getElementById("divStorageProduct1");
		if (ProductBigDiv)
		{
		document.body.removeChild(ProductBigDiv); 
		}
		divStorageProduct1.style.display="none";
	//	document.getElementById('divzhezhao1').style.display='none';
}
	function AlertProductMsg(){

	   /**第一步：创建DIV遮罩层。*/
		var sWidth,sHeight;
		sWidth = window.screen.availWidth;
		//屏幕可用工作区高度： window.screen.availHeight;
		//屏幕可用工作区宽度： window.screen.availWidth;
		//网页正文全文宽：     document.body.scrollWidth;
		//网页正文全文高：     document.body.scrollHeight;
		if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
			sHeight = window.screen.availHeight;  
		}else{//当高度大于一屏
			sHeight = document.body.scrollHeight;   
		}
		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','ProductBigDiv');
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=10);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "100";
		document.body.appendChild(maskObj);
		
	}
//function OptionCheckAll()
//{
//  if(document.getElementById("btnAll").checked)
//  {
//     var ck = document.getElementsByName("CheckboxProdID");
//        for( var i = 0; i < ck.length; i++ )
//        {
//        ck[i].checked=true ;
//        }
//  }
//  else if(!document.getElementById("btnAll").checked)
//  {
//    var ck = document.getElementsByName("CheckboxProdID");
//        for( var i = 0; i < ck.length; i++ )
//        {
//        ck[i].checked=false ;
//        }
//  }
//}
function SelectedNodeChanged(code_text,type_code)
{   

document.getElementById("hf_typeid").value=type_code;
   document.getElementById("txt_TypeID").value=code_text;
   hideUserList();
}
    function hidetxtUserList()
    {
        hideUserList();
        document.getElementById("txt_TypeID").value="";
    }
    
       function getChildNodes(nodeTable)
      {
            if(nodeTable.nextSibling == null)
                return [];
            var nodes = nodeTable.nextSibling;  
           
            if(nodes.tagName == "DIV")
            {
                return nodes.childNodes;//return childnodes's nodeTables;
            }
            return [];
      }
      
        function showUserList()
        {
            var list = document.getElementById("userList");
           
            if(list.style.display != "none")
            {
                list.style.display = "none";
                return;
            }
            
            var pos = elePos(document.getElementById("txt_TypeID"));
            
            list.style.left = pos.x;
            list.style.top = pos.y+20;
            document.getElementById("userList").style.display = "block";
        }
        function elePos(et) {
   
    var left=-140;
	var top=-145;
	while(et.offsetParent){
	left+=et.offsetLeft;
	top+=et.offsetTop;
	et=et.offsetParent;
	}
	left+=et.offsetLeft;
	top+=et.offsetTop;
	return {x:left,y:top}; 
}
        
        function hideUserList()
        {
            document.getElementById("userList").style.display = "none";
        }
</script>
