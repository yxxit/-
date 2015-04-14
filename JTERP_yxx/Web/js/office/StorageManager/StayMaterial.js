$(document).ready(function() {
//    IsDiplayOther('GetBillExAttrControl1_SelExtValue'); //物品扩展属性
    fnGetExtAttr(); //物品控件拓展属性
});

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var alertPrint = "";
var currentPageIndex = 1;
var action = "Get"; //操作
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

   var OverTime=$("#txtOverTime").val();        //起始日期
   var OverTimeEnd=$("#txtOverTimeEnd").val();  //终止日期
   var TurnOver=$("#txtTurnOver").val();        //起始周转率
   var TurnOverEnd=$("#txtTurnOverEnd").val();  //终止周转率

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
   if(OverTime=="")
   {
        isFlag=false;
        fieldText=fieldText +"起始日期|";
        msgText=msgText +"请选择起始日期!|";
   }
   if(OverTimeEnd=="")
   {
        isFlag=false;
        fieldText=fieldText +"终止日期|";
        msgText=msgText +"请选择终止日期!|";
   }
   if(CompareDate(OverTime, OverTimeEnd)==1)
   {
        isFlag=false;
        fieldText=fieldText + "日期|";
        msgText = msgText +  "起始日期不能大于终止日期|";
   }
   if(TurnOver != "" && TurnOverEnd != "")
   {
       if(TurnOver > TurnOverEnd)
       {
            isFlag=false;
            fieldText=fieldText + "周转率|";
            msgText = msgText +  "起始周转率不能大于终止周转率|";
        }
   }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }
    currentPageIndex = pageIndex;
      var BusiType=$("#hidBusiType").val();        //业务类型
      var ProductNo=$("#txtProductNo").val();        //物品编号
      var ProductName=$("#txtProductName").val();        //物品名称
      var MoreThanType=$("#sltMoreThanType").val();        //超储判断
      var UsedStatus=$("#sltUsedStatus").val();        //状态
      var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderBy + "&BusiType=" + escape(BusiType) + "&ProductNo=" + escape(ProductNo) + "&ProductName=" + escape(ProductName) + "&MoreThanType=" + escape(MoreThanType) + "&OverTime=" + escape(OverTime) + "&OverTimeEnd=" + escape(OverTimeEnd) + "&TurnOver=" + escape(TurnOver) + "&TurnOverEnd=" + escape(TurnOverEnd)+ "&UsedStatus=" + escape(UsedStatus);
//    var sidex = "ExtField" + EFIndex;
    
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StayMaterialInfo.ashx?action=Get' + UrlParam, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) 
        {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            if (msg.data.length != 0)
            {  
                $.each(msg.data, function(i, item) 
                {
                    if (item.ProductNo != null && item.ProductNo != "") 
                    {
                        alertPrint = 0;
                        $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Manufacturer + "\">" + fnjiequ(item.Manufacturer, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.UnitName + "\">" + fnjiequ(item.UnitName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + fnjiequ(item.ProductCount, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.MaxStockNum + "\">" + fnjiequ(item.MaxStockNum, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.OverCount + "\">" + fnjiequ(item.OverCount, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StayStandard + "\">" + fnjiequ(item.StayStandard, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Turnover + "\">" + jqControl(item.Turnover) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Slip + "\">" + fnjiequ(item.Slip) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.UsedStatusName+ "\">" + fnjiequ(item.UsedStatusName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                    }

                });
            }
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.all["Text2"].value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
            $("#txtBarCode").val(""); //清空条码
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
         complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.all["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
    });
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

function Fun_Search_StayMaterial() {
    search = "1";
    document.getElementById("hidSearchCondition").value = search; //这里只是放了一个标志位，说明是点过了检索按钮
    TurnToPage(1);
}

function Ifshow(count) {
    if (count == "0") {
        document.all["divpage"].style.display = "none";
        document.all["pagecount"].style.display = "none";
    }
    else {
        document.all["divpage"].style.display = "block";
        document.all["pagecount"].style.display = "block";
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
        TurnToPage(parseInt(newPageIndex, 10));
    }
}
//排序
function OrderBy(orderColum, orderTip) {
    if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
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
   TurnToPage(1);
}

//物品控件
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('txtProductNo').value = ProNo;
    document.getElementById('txtProductName').value = ProdName;
}



//document.onkeydown = ScanBarCodeSearch;
/*列表条码扫描检索*/
//function ScanBarCodeSearch() {
//    var evt = event ? event : (window.event ? window.event : null);
//    var el; var theEvent
//    var browser = IsBrowser();
//    if (browser == "IE") {
//        el = window.event.srcElement;
//        theEvent = window.event;
//    }
//    else {
//        el = evt.target;
//        theEvent = evt;
//    }
//    if (el.id != "txtBarCode") {
//        return;
//    }
//    else {
//        var code = theEvent.keyCode || theEvent.which;
//        if (code == 13) {
//            TurnToPage(1);
//            evt.returnValue = false;
//            evt.cancel = true;
//        }
//    }
//}

//function IsBrowser() {
//    var isBrowser;
//    if (window.ActiveXObject) {
//        isBrowser = "IE";
//    } else if (window.XMLHttpRequest) {
//        isBrowser = "FireFox";
//    }
//    return isBrowser;
//}



//添加批次选择项
//function fnGetPackage() {
//    var obj = document.getElementById("ddlBatchNo");
//    obj.options.length = 1;

//    var Storage = document.getElementById("ddlStorage").value;

//    var ProductNo = document.getElementById("txtProductNo").value;
//    //定义反确认动作变量
//    var action = "GetBatchNo";
//    var postParam = "action=" + action + "&Storage=" + Storage + "&ProductNo=" + ProductNo;
//    $.ajax(
//        {
//            type: "POST",
//            url: "../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?" + postParam,
//            dataType: 'html', //返回json格式数据
//            cache: false,
//            beforeSend: function() {
//            },
//            error: function() {
//            },
//            success: function(msg) {
//                var msginfo = msg.toString().split(',');
//                for (var i = msginfo.length - 1; i >= 0; i--) {
//                    if (msginfo[i].toString() != "") {
//                        var varItem = new Option(msginfo[i].toString(), msginfo[i].toString());
//                        obj.options.add(varItem);
//                    }
//                }
//            }
//        });


//}
function jqControl(value) {
    var ret = "";
    var point = document.getElementById("hidPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
} 
//打印单据
//打印单据
 function BillPrint()
 {
//    if(alertPrint == "")
//    {
//        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先检索！");
//        return;
//    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

   var OverTime=$("#txtOverTime").val();        //起始日期
   var OverTimeEnd=$("#txtOverTimeEnd").val();  //终止日期
   var TurnOver=$("#txtTurnOver").val();        //起始周转率
   var TurnOverEnd=$("#txtTurnOverEnd").val();  //终止周转率

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
   if(OverTime=="")
   {
        isFlag=false;
        fieldText=fieldText +"起始日期|";
        msgText=msgText +"请选择起始日期!|";
   }
   if(OverTimeEnd=="")
   {
        isFlag=false;
        fieldText=fieldText +"终止日期|";
        msgText=msgText +"请选择终止日期!|";
   }
   if(CompareDate(OverTime, OverTimeEnd)==1)
   {
        isFlag=false;
        fieldText=fieldText + "日期|";
        msgText = msgText +  "起始日期不能大于终止日期|";
   }
   if(TurnOver != "" && TurnOverEnd != "")
   {
       if(TurnOver > TurnOverEnd)
       {
            isFlag=false;
            fieldText=fieldText + "周转率|";
            msgText = msgText +  "起始周转率不能大于终止周转率|";
        }
   }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }
      pageIndex = 1;
      var BusiType=$("#hidBusiType").val();        //业务类型
      var ProductNo=$("#txtProductNo").val();        //物品编号
      var ProductName=$("#txtProductName").val();        //物品名称
      var MoreThanType=$("#sltMoreThanType").val();        //超储判断
      var UsedStatus=$("#sltUsedStatus").val();        //状态
      var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderBy + "&BusiType=" + escape(BusiType) + "&ProductNo=" + escape(ProductNo) + "&ProductName=" + escape(ProductName) + "&MoreThanType=" + escape(MoreThanType) + "&OverTime=" + escape(OverTime) + "&OverTimeEnd=" + escape(OverTimeEnd) + "&TurnOver=" + escape(TurnOver) + "&TurnOverEnd=" + escape(TurnOverEnd)+ "&UsedStatus=" + escape(UsedStatus);
   
    window.open("../../../Pages/PrinttingModel/StorageManager/StayMaterialPrint.aspx?"+UrlParam);
 }