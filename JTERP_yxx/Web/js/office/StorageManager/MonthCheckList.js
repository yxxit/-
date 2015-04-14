function checkthis(str)
{
    
    if(str!=null)
    {
        if(parseFloat(str.value).toString()=="NaN")
        {
            str.value="";
        }else
        {
            str.value=parseFloat(str.value).toFixed(document.getElementById("point").value);
        }
    }
}

 var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "years_d";//排序字段
    //jQuery-ajax获取JSON数据
    function TurnToPage_JieSuan(pageIndex)
    {
           var total1 = 0.0;
           currentPageIndex = pageIndex;
           var time1= document.getElementById("DDLTime1").value;
           var time2=document.getElementById("DDLTime12").value;
           var CheckCount1=document.getElementById("CheckCount1").value;
           var CheckCount2=document.getElementById("CheckCount2").value;
           var storageid = document.getElementById("ddlStorageID").value;
            var prono = document.getElementById("ProductNo").value;
             var proname = document.getElementById("ProductName").value;
             var OutCount1 = document.getElementById("OutCount1").value;
              var OutCount2 = document.getElementById("OutCount2").value;
               var InCount1 = document.getElementById("InCount1").value;
                var InCount2 = document.getElementById("InCount2").value;
                action="monthlist";
                var pcount=0;
                var innum=0;
                var outnum=0;
                var sinnum=0;
                var soutnum=0;
                var roadcout=0;
                var ordercount=0;
                 $("#pageindex").val(pageIndex);
                $("#pagesize").val(pageCount);
           //Start验证输入
           if(!CheckInput())
           {
               $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/StorageManager/MonthCheck.ashx',//目标地址
               cache:false,
               data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&time1="+escape(time1)+"&time2="+escape(time2)+"&CheckCount1="+escape(CheckCount1)+"&CheckCount2="+escape(CheckCount2)+"&storageid="+escape(storageid)+"&prono="+escape(prono)+"&proname="+escape(proname)+"&OutCount1="+escape(OutCount1)+"&OutCount2="+escape(OutCount2)+"&InCount1="+escape(InCount1)+"&InCount2="+escape(InCount2)+"",//数据
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                        $.each(msg.data,function(i,item){
                            if(item.years != null && item.years != "" && item.years !="undefined")
                            {
                                if(parseFloat(item.pcount) != 0)
                                {
                                    pcount += parseFloat(item.pcount);
                                }
                                if(parseFloat(item.outcount) != 0)
                                {
                                    soutnum += parseFloat(item.outcount);
                                }
                                if(parseFloat(item.incount) != 0)
                                {
                                    sinnum += parseFloat(item.incount);
                                }
                                  if(parseFloat(item.storincount) != 0)
                                {
                                    innum += parseFloat(item.storincount);
                                }
                                  if(parseFloat(item.storoutcount) != 0)
                                {
                                    outnum += parseFloat(item.storoutcount);
                                }
                                   if(parseFloat(item.roadcount) != 0)
                                {
                                    roadcout += parseFloat(item.roadcount);
                                }
                                    if(parseFloat(item.ordercount) != 0)
                                {
                                    ordercount += parseFloat(item.ordercount);
                                }
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.years+"</td>"+
                                
                                "<td height='22' align='center'>"+item.months+"</td>"+
                                "<td height='22' align='center'>"+item.productno+"</td>"+
                                "<td height='22' align='center'>"+item.productname+"</td>"+
                                "<td height='22' align='center'>"+item.unitname+"</td>"+
                                "<td height='22' align='center'>"+item.batchno+"</td>"+
                                "<td height='22' align='center'>"+item.pcount+"</td>"+
                                "<td height='22' align='center'>"+item.storoutcount+"</td>"+
                                "<td height='22' align='center'>"+item.storincount+"</td>"+
                                "<td height='22' align='center'>"+item.roadcount+"</td>"+
                                "<td height='22' align='center'>"+item.ordercount+"</td>"+
                                "<td height='22' align='center'>"+item.incount+"</td>"+
                                "<td height='22' align='center'>"+item.outcount+"</td>").appendTo($("#pageDataList1 tbody"));
                            }
                       });
                       
                                $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>"+
                                
                                "<td height='22' align='center'></td>"+
                                "<td height='22' align='center'></td>"+
                                "<td height='22' align='center'></td>"+
                                "<td height='22' align='center'></td>"+
                                "<td height='22' align='center'></td>"+
                                "<td height='22' align='center'>"+parseFloat(pcount).toFixed( $("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(outnum).toFixed($("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(innum).toFixed($("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(roadcout).toFixed($("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(ordercount).toFixed($("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(sinnum).toFixed($("#point").val())+"</td>"+
                                "<td height='22' align='center'>"+parseFloat(soutnum).toFixed($("#point").val())+"</td>").appendTo($("#pageDataList1 tbody"));
                       
                    //页码
                  ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage_JieSuan({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.getElementById("Text2").value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
               });
           }

    }

//排序
function OrderBy(orderColum, orderTip) {
    
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↓") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    else {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    orderBy = orderColum + "_" + ordering;
    $("#txtorderBy").val(orderBy); //把排序字段放到隐藏域中，

    TurnToPage_JieSuan(1);
}


 function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "";
        document.getElementById("pagecount").style.display = "";
    }
 }
    //改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {

    //判断是否是数字
    if (!PositiveInteger(newPageCount)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数应为正整数！");
        return;
    }

    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount, 10);
        TurnToPage_JieSuan(parseInt(newPageIndex, 10));
    }
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}
    
    
function CheckInput()
{
 var time1= document.getElementById("DDLTime1").value;
  var time2=document.getElementById("DDLTime12").value;
   var CheckCount1=document.getElementById("CheckCount1").value;
   var CheckCount2=document.getElementById("CheckCount2").value;
     var OutCount1 = document.getElementById("OutCount1").value;
      var OutCount2 = document.getElementById("OutCount2").value;
       var InCount1 = document.getElementById("InCount1").value;
        var InCount2 = document.getElementById("InCount2").value;
    
   //Start验证输入
   
      var   fieldText = "";
      var   msgText = "";
   var isErrorFlag = false;
   if(time1==""||time2=="")
   {
         isErrorFlag = true;
        fieldText = fieldText + "时间错误|";
        msgText = msgText + "系统无结算数据|";
   }
   if(CheckCount1!=""&&CheckCount2!="")
   {
        if (parseFloat(CheckCount1)>parseFloat(CheckCount2))
        {
            isErrorFlag = true;
           
             fieldText = fieldText + "结存量|";
            msgText = msgText + "查询范围错误|";
   
        }
   }
    if(OutCount1!=""&&OutCount2!="")
   {
        if (parseFloat(OutCount1)>parseFloat(OutCount2))
        {
            isErrorFlag = true;
           
              fieldText = fieldText + "出库数量|";
            msgText = msgText + "查询范围错误|";
        }
   }
    if(InCount1!=""&&InCount2!="")
   {
        if (parseFloat(InCount1)>parseFloat(InCount2))
        {
            isErrorFlag = true;
            fieldText = fieldText + "入库数量|";
            msgText = msgText + "查询范围错误|";
        }
   }
    if(isErrorFlag)
    {
         popMsgObj.Show(fieldText, msgText);
        return true;
    }
    else
    {
        return isErrorFlag;
    }
   //End验证输入 
}
