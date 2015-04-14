    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "";//排序字段
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
  
  
function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    
    var ReportNo = $('#txtReportNo').val();//质检单号

    var BeginT=$("#txtBeginT").val();  //申请开始时间
    var EndT=$("#txtEndT").val();       //申请结束时间
    
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    
    
    
    if(ReportNo.length>0 && ReportNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
        fieldText = fieldText + "质检单号|";
	    msgText = msgText + "质检单号输入不正确|";
    }
    
    
    if(BeginT!="" && EndT!="")
    {
        if(BeginT>EndT)
        {
            isFlag = false;
            fieldText = fieldText +"申请时间|";
	        msgText = msgText +RetVal+  "开始时间要小于结束时间|";
	    }
    }
    
    
    
    
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}
  
  function GetSearchCondition()
  {
        if(!fnCheck())
        {
            return;
        }
        
        document.getElementById("checkall").checked = false;
    
        ifdel="0";
        var ReportNo=$("#txtReportNo").val();  //质检单号
        var OtherCorpName=$("#txtOtherCorpName").val();  //供货方 
        var BeginT=$("#txtBeginT").val();  //入库开始时间
        var EndT=$("#txtEndT").val();       //入库结束时间
        var ProductName=$("#txtProductName").val();  //煤种
        var StartPlace=$("#txtStartPlace").val();  //发站
        var InBusNo=$("#txtInBusNo").val();     //到货单编号
        var SearchInfo= "&ReportNo="+reescape(ReportNo)+"&OtherCorpName="+reescape(OtherCorpName)+"&BeginT="+reescape(BeginT)+
                 "&EndT="+reescape(EndT)+"&ProductName="+reescape(ProductName)+"&StartPlace="+reescape(StartPlace)+"&InBusNo="+reescape(InBusNo);//数据
                 
        $("#hidSearchCondition").val(SearchInfo);
  }
  
  
  
   function SearchQTestList(pageIndex)
   {
    
            currentPageIndex = pageIndex;
            action="SearchQTestList";
            GetSearchCondition();
            TurnToPage(currentPageIndex);
          
   }
   
     
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex) {


           currentPageIndex = pageIndex;
           
           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/stockmanage/QTestInfo.ashx', //目标地址
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
                   if (item.ID != null && item.ID != "") {
                          
                                        
               
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.ID+ "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                        "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +
                        "<td height='22' align='center' style='display:none'>" + item.ID + "</td>" +  
                        "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('" + item.ID +"')><span title=\"" + item.reportno + "\">" + item.reportno + "</a></td>" +
                        "<td height='22' align='center'>" + item.checkresult + "</a></td>" +
                         "<td height='22' align='center'>" + item.employeeName + "</a></td>" +

                        "<td height='22' align='center'>" + item.CheckStandard + "</a></td>" +
                        "<td height='22' align='center'>" + item.CheckValue + "</a></td>" +
                         "<td height='22' align='center'>" + item.createorName + "</a></td>" +
                        "<td height='22' align='center'>" + item.billstatus + "</a></td>").appendTo($("#pageDataList1 tbody"));
                       }
                   });
                   

                   
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

 
 
 
 
//跳转到质检单
function GetLinkParam1(id) {
    //"&intMasterProviderID=" + item.ID + "'

    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;    
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null,'77711','质检报告','Pages/JTHY/StockManage/QTest_ADD.aspx?ModuleID=77711&intMasterID='+escape(id)+'&'+searchCondition+'&Flag='+flag+'');
}





//点击新建按钮
function CreateNew() {

    window.parent.addTab(null,'77711','质检报告','Pages/JTHY/StockManage/QTest_ADD.aspx?ModuleID=77711');
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
    if(totalRecord==0)
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
 


function  DelQTestInfo()
{ 

    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
        var ck = document.getElementsByName("Checkbox1");
        var table=document.getElementById("pageDataList1"); 
        var URLParams = "";  
        var Action = "DeleteQTestInfo";
        URLParams+="Action="+Action;
        var index = 0;
        var QTestID="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {

                QTestID += table.rows[ck[i].value].cells[2].innerText+',';
                index++;
               
            }       
        }
        
        QTestID = QTestID.substring(0,QTestID.length-1);
        if(index == 0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
            return;
        }
        URLParams+="&Length="+index+"";
        
        $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  "../../../Handler/JTHY/stockmanage/DealQTest_ADD.ashx",//目标地址
               data:URLParams+"&allQTestID="+escape(QTestID)+' ', //数据
               cache:false,
               beforeSend:function(){AddPop();},//发送数据之前
                error: function() 
                {
                   popMsgObj.ShowMsg('请求发生错误！');
                   return;
                }, 
                success: function(msg){
           
                    if(msg.sta > 0)
                    {
                        popMsgObj.ShowMsg(msg.info);
                        SearchQTestList(1);  //刷新页面
                    }
                    else
                    {
                        popMsgObj.ShowMsg(msg.info);
                    }
                  },

   
               complete:function(){
                    hidePopup();
               }
        });
    }
}


 