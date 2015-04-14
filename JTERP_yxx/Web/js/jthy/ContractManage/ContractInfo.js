    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "id_a";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
     
   
    
    $(document).ready(function()
    {
    requestobj = GetRequest();  
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
 
    var url = location.search;
    var theRequest = new Object();
  
});

    
    

//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }

   return theRequest;
  }
  
  //获取销售合同的检索条件
 function getSellContractCondition()
 {
    if(!fnCheck())
    {
        return;
    }
    
    document.getElementById("checkall").checked = false;


    var OrderNo=$("#txtOrderNo").val();  //合同号
    var ProductName=$("#txtProductName").val(); //煤种
    var Specification=$("#txtSpecification").val(); //质量(热卡)
    var BeginT=$("#txtBeginT").val();  //创建开始时间
    var EndT=$("#txtEndT").val();       //创建结束时间
    var BillStatus=$("#BillStatus").val();  //合同状态
    var CustName = $("#txtCustomerName").val();  //客户名称
    var SearchInfo= "&OrderNo="+reescape(OrderNo)+"&CustName="+reescape(CustName)+"&BeginT="+reescape(BeginT)+
             "&EndT="+reescape(EndT)+"&ProductName="+reescape(ProductName)+"&Specification="+reescape(Specification)+"&BillStatus="+reescape(BillStatus);//数据
             
    $("#hidSearchCondition").val(SearchInfo);
    
 }
 
 
 //获取采购合同的检索条件
 function getPurContractCondition()
 {
    if(!fnCheck())
    {
        return;
    }
    
    document.getElementById("checkall").checked = false;


    var OrderNo=$("#txtOrderNo").val();  //合同号
    var ProductName=$("#txtProductName").val(); //煤种
    var Specification=$("#txtSpecification").val(); //质量(热卡)
    var BeginT=$("#txtBeginT").val();  //创建开始时间
    var EndT=$("#txtEndT").val();       //创建结束时间
    var BillStatus=$("#BillStatus").val();  //合同状态
    var ProviderName=$("#txtProName").val();  //供应商名称
    var SearchInfo= "&OrderNo="+reescape(OrderNo)+"&ProviderName="+reescape(ProviderName)+"&BeginT="+reescape(BeginT)+
             "&EndT="+reescape(EndT)+"&ProductName="+reescape(ProductName)+"&Specification="+reescape(Specification)+"&BillStatus="+reescape(BillStatus);//数据
             
    $("#hidSearchCondition").val(SearchInfo);
    
 }
  
   function SearchSellContract(pageIndex)
   {
   
        currentPageIndex = pageIndex;
        action="SearchSellContractList";
        getSellContractCondition();
        TurnToPage(currentPageIndex)
   }
   
    function SearchPurContract(pageIndex)
   {
        currentPageIndex = pageIndex;
        action="SearchPurContractList";
        getPurContractCondition();
        TurnToPage(currentPageIndex)   
   }
    
    //jQuery-ajax获取JSON数据
   function TurnToPage(pageIndex) {

         //debugger;
           var totalProductcount = 0;
           var totalMoney = 0;
           currentPageIndex = pageIndex;           
           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageCount=" + currentpageCount + "&Action=" + action + "&orderby=" + orderBy +
                    document.getElementById("hidSearchCondition").value,
               beforeSend: function() { AddPop(); $("#pageDataList1_PagerList").hide(); }, //发送数据之前

               success: function(msg) {
                
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataList1 tbody").find("tr.newrow").remove();
                   var j = 1;
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "") {                                        
                           if(action=="SearchSellContractList")// 销售合同列表 
                           {
                              $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                            "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +  
                            "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +  
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('" + item.id +"')><span title=\"" + item.Contractid + "\">" + item.Contractid + "</a></td>" +          
                            "<td height='22' align='center'>" + item.custname + "</a></td>" +
                            "<td height='22' align='center'>" + item.signdate + "</a></td>" +                          
                            "<td height='22' align='center'>" + item.effectivedate + "</a></td>" +
                            "<td height='22' align='center'>" + item.enddate + "</a></td>" +
                            "<td height='22' align='center'>" + item.SettleTypeName + "</a></td>" +
                            "<td height='22' align='center'>" + item.TransPortTypeName + "</a></td>" +
                            "<td height='22' align='center'>" + item.billstatus + "</a></td>" +
                            "<td height='22' align='center'>" + item.ProductCount + "</a></td>" +
                            "<td height='22' align='center'>" + item.totalmoney + "</a></td>" +
                            "<td height='22' align='center'>" + item.createname + "</a></td>" +
                            "<td height='22' align='center'>" + item.createdate + "</a></td>").appendTo($("#pageDataList1 tbody"));
                         }
                         else //采购合同列表
                         {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                            "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +  
                            "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +  
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('" + item.id +"')><span title=\"" + item.Contractid + "\">" + item.Contractid + "</a></td>" +          
                            "<td height='22' align='center'>" + item.cvenname + "</a></td>" +
                            "<td height='22' align='center'>" + item.signdate + "</a></td>" +                          
                            "<td height='22' align='center'>" + item.effectivedate + "</a></td>" +
                            "<td height='22' align='center'>" + item.enddate + "</a></td>" +
                            "<td height='22' align='center'>" + item.SettleTypeName + "</a></td>" +
                            "<td height='22' align='center'>" + item.TransPortTypeName + "</a></td>" +
                            "<td height='22' align='center'>" + item.billstatus + "</a></td>" +
                            "<td height='22' align='center'>" + item.ProductCount + "</a></td>" +
                            "<td height='22' align='center'>" + item.totalmoney + "</a></td>" + 
                            "<td height='22' align='center'>" + item.createname + "</a></td>" +
                            "<td height='22' align='center'>" + item.createdate + "</a></td>").appendTo($("#pageDataList1 tbody"));
                         }
                        if((parseFloat(item.ProductCount)!=0)&&(item.ProductCount!=""))
                        {
                            totalProductcount+=parseFloat(item.ProductCount);
                        }
                        if((parseFloat(item.totalmoney)!=0)&&(item.totalmoney!=""))
                        {
                           totalMoney+=parseFloat(item.totalmoney);
                        }                        
                       }
                   });
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>本页合计</td>" +
                    "<td colspan='8'></td>" +                                 
                    "<td height='22' align='center'>" + QfwFormat(totalProductcount,2) + "</td>" +
                    "<td height='22' align='center'>" +QfwFormat(totalMoney,2)+"</td>"+                 
                    "<td colspan='2'></td>").appendTo($("#pageDataList1 tbody"));
                    
                     $("<tr class='newrow'></tr>").append("<td height='22' align='center'>总计</td>" +
                    "<td colspan='8'></td>" +                    
                    "<td height='22' align='center'>" + QfwFormat(msg.ttlCount,2) + "</td>" +
                    "<td height='22' align='center'>" +QfwFormat(msg.ttlFee,2) +"</td>"+
                    "<td height='22' colspan='2'></td>").appendTo($("#pageDataList1 tbody")); 
                    
                   //页码
                   ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: currentpageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    ); document.getElementById("checkall").checked = false;
                   totalRecord = msg.totalCount;
                   // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                   document.getElementById("Text2").value = msg.totalCount;
                   $("#ShowPageCount").val(currentpageCount);
                   ShowTotalPage(msg.totalCount, currentpageCount, pageIndex, $("#pagecount"));
                   $("#ToPage").val(pageIndex);
               },
               error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
               complete: function() { hidePopup(); $("#pageDataList1_PagerList").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		//t[i].onclick=function(){//鼠标点击
			//if(this.x!="1"){
				//this.x="1";//
				//this.style.backgroundColor=d;
			//}else{
			//	this.x="0";
				//this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
			//}
		//}
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}


function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }

/*
* 获取链接的参数
*/
function GetLinkParam()
{
//    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    
    linkParam = "ProviderInfo_Add.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + currentpageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;

}
var M='80011';

function GetLinkParam0(id){
    if(action=="SearchSellContractList"){
        GetLinkParam1(id);//若为销售合同
    }
    if(action=="SearchPurContractList"){
        GetLinkParam2(id); //若为采购合同
    }
}

//新建销售合同
function CreateSaleContract()
{
     window.parent.addTab(null,'80011','销售合同','Pages/JTHY/ContractManage/SellContract_Add.aspx?ModuleID=80011');
}
//新建采售合同
function CreatePurContract()
{
    window.parent.addTab(null,'80021','采购合同','Pages/JTHY/ContractManage/PurContract_Add.aspx?ModuleID=80021');
}



//跳转到销售合同号
function GetLinkParam1(id) { 

    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;    
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null,'80011','销售合同','Pages/JTHY/ContractManage/SellContract_Add.aspx?ModuleID=80011&intMasterID='+escape(id)+'&'+searchCondition+'&Flag='+flag+'');    
}

//跳转到采购合同号
function GetLinkParam2(id) {
    //"&intMasterProviderID=" + item.ID + "'
    
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;    
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null,'80021','采购合同','Pages/JTHY/ContractManage/PurContract_Add.aspx?ModuleID=80021&intMasterID='+escape(id)+'&'+searchCondition+'&Flag='+flag+'');
}


function fnCheck()
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

 
function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtContractNo").value = "";
    document.getElementById("txtTitle").value = "";
    document.getElementById("DrpTypeID").value = "";
    document.getElementById("txtHidOurDept").value = "";
    document.getElementById("txtDeptID").value = "";
    document.getElementById("txtSeller").value = "";
    document.getElementById("txtSeller").title = "";
    document.getElementById("ddlFromType").value = "0";
    document.getElementById("txtHidProviderID").value = "";
    document.getElementById("txtProviderID").value = "";
    document.getElementById("ddlBillStatus").value = "0";
    document.getElementById("ddlUsedStatus").value = "0";
    
    document.getElementById("pagecount").style.display = "none";
//    document.getElementById("divpage").style.display = "none";
    document.getElementById("pageDataList1_PagerList").style.display = "none";
}

    
function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
            document.getElementById("pagecount").style.display = "none";
        }
        else
        {
            document.getElementById("divpage").style.display = "block";
            document.getElementById("pagecount").style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数必须输入正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//            return false;
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
            currentpageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
    if(issearch=="")
            return;
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
        TurnToPage(1);
    }
 


function  DelSelContractInfo()
{ 
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
    var ck = document.getElementsByName("Checkbox1");
    var table=document.getElementById("pageDataList1"); 
    var URLParams = "";  
    var Action = "DeleteSelContract";
    URLParams+="Action="+Action;
    var index = 0;
    var SelContractID="";
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {

            SelContractID += table.rows[ck[i].value].cells[2].innerText+',';
            index++;
           
        }       
    }
    
    SelContractID = SelContractID.substring(0,SelContractID.length-1);
     
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
        return;
    }
    URLParams+="&Length="+index+"";
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/ContractManage/DealContract.ashx?'+URLParams+"&allSelContractID="+escape(SelContractID)+"&billtype=1"+'',//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
            error: function() 
                {
                    
                   popMsgObj.ShowMsg('请求发生错误！');
                   return;
                }, 
           success: function(msg){
           
                    SearchSellContract(1);
                    if(msg.sta == 0)
                    {
                        popMsgObj.ShowMsg(msg.info);
                    }
                    else
                    {
                        popMsgObj.ShowMsg('删除成功！');
                    }
                  },
           error: function() {
            
           }, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){
            
           }
           });
    }
}


function  DelPurContractInfo()
{ 
//debugger;
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
    var ck = document.getElementsByName("Checkbox1");
    var table=document.getElementById("pageDataList1"); 
    var URLParams = "";  
    var Action = "DeletePurContract";
    URLParams+="Action="+Action;
    var index = 0;
    var PurContractID="";
    for( var i = 0; i < ck.length; i++ )
    {
        if ( ck[i].checked )
        {

            PurContractID += table.rows[ck[i].value].cells[2].innerText+',';
            index++;
           
        }       
    }
    
    PurContractID = PurContractID.substring(0,PurContractID.length-1);
     
    if(index == 0)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
        return;
    }
    URLParams+="&Length="+index+"";
    
    $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/ContractManage/DealContract.ashx?'+URLParams+"&allPurContractID="+escape(PurContractID)+"&billtype=2"+'',//目标地址
           cache:false,
           beforeSend:function(){},//发送数据之前
            error: function() 
                {
                    
                   popMsgObj.ShowMsg('请求发生错误！');
                   return;
                }, 
           success: function(msg){
           
                    SearchPurContract(1);
                    if(msg.sta == 0)
                    {
                        popMsgObj.ShowMsg(msg.info);
                    }
                    else
                    {
                        popMsgObj.ShowMsg('删除成功！');
                    }
                  },
           error: function() {
            
           }, 
//           complete:function(){hidePopup();$("#PurPlanPage1").show();Ifshow(document.all["Text2"].value);pageDataList1("PurPlanBill","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           complete:function(){
            
           }
           });
    }
}

function fnSelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

 