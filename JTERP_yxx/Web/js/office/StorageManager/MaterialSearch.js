$(document).ready(function() {
//    IsDiplayOther('GetBillExAttrControl1_SelExtValue'); //物品扩展属性
    fnGetExtAttr(); //物品控件拓展属性
    //$("#txt_DaiZhiBiaoZhun").val(parseFloat("0").toFixed($("#hidPoint").val()));
    set_time();
});
function check_txt(thisval)
{
    var point=$("#hidPoint").val();
    if(parseFloat(thisval.value).toString()=="NaN")
    {
        thisval.value=parseFloat("0").toFixed(point);
    }else
    {
         thisval.value=parseFloat(thisval.value).toFixed(point);
    }
}
function set_time()
{
    var val=$("#Select1").val();
    var time2=new Date();
    var year=time2.getFullYear();
    var month=time2.getMonth()+1;
    month=month>=10?month:("0"+month);
    var day=time2.getDate();
    day=day>=10?day:("0"+day);
    var time=year+"-"+month+"-"+day;
    if(val=="0")
    {
        $("#txtOverTime1").val(getAfterDate(time,-30));
    }else if(val=="1")
    {
        $("#txtOverTime1").val(getAfterDate(time,-90));
    }
    else if(val=="2")
    {
        $("#txtOverTime1").val(getAfterDate(time,-180));
    }
     else if(val=="3")
    {
        $("#txtOverTime1").val(getAfterDate(time,-365));
    }
   $("#txtOverTimeEnd1").val(time);

}

 function getAfterDate(curDate,count){
    //获取s系统时间 
    var LSTR_ndate=new Date(Date.parse(curDate.replace(/-/g,   "/"))); 
    var LSTR_Year=LSTR_ndate.getYear(); 
    var LSTR_Month=LSTR_ndate.getMonth(); 
    var LSTR_Date=LSTR_ndate.getDate(); 
    //处理 
    var uom = new Date(LSTR_Year,LSTR_Month,LSTR_Date); 
    uom.setDate(uom.getDate()+count);//取得系统时间的前一天,重点在这里,负数是前几天,正数是后几天 
    var LINT_MM=uom.getMonth(); 
    LINT_MM++; 
    var LSTR_MM=LINT_MM >= 10?LINT_MM:("0"+LINT_MM) 
    var LINT_DD=uom.getDate(); 
    var LSTR_DD=LINT_DD >= 10?LINT_DD:("0"+LINT_DD) 
    //得到最终结果 
    uom = uom.getFullYear() + "-" + LSTR_MM + "-" + LSTR_DD; 
    return uom;
   } 
var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var alertPrint = "";
var currentPageIndex = 1;
var action = "new"; //操作
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

   var OverTime=$("#txtOverTime1").val();        //起始日期
   var OverTimeEnd=$("#txtOverTimeEnd1").val();  //终止日期
   var time1=OverTime.split('-');
   var time2=OverTimeEnd.split('-');
     var stime=new Date(time1[0],time1[1],time1[2]);
     var etime=new Date(time2[0],time2[1],time2[2]);
     var mm=etime.getTime()-stime.getTime();
     var daycount=mm/3600000/24+1;
     //var productcount=$("#txt_DaiZhiBiaoZhun").val(); 
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
//    if(productcount=="")
//   {
//        isFlag=false;
//        fieldText=fieldText + "请输入|";
//        msgText = msgText +  "呆滞标准|";
//   }
//    if(productcount!="")
//   {
//       if(parseFloat(productcount).toString()=="NaN")
//       {
//            isFlag=false;
//            fieldText=fieldText + "呆滞标准|";
//            msgText = msgText +  "请输入数字|";
//       }
//        if(parseFloat(productcount).toString()<0)
//       {
//            isFlag=false;
//            fieldText=fieldText + "呆滞标准|";
//            msgText = msgText +  "不能小于0|";
//       }
//   }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }
    currentPageIndex = pageIndex;
     
      var ProductNo=$("#txtProductNo").val();        //物品编号
      var ProductName=$("#txtProductName").val();        //物品名称
      var UsedStatus=$("#sltUsedStatus").val();        //状态
      var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderBy + "&ProductNo=" + escape(ProductNo) + "&ProductName=" + escape(ProductName) + "&OverTime=" + escape(OverTime) + "&OverTimeEnd=" + escape(OverTimeEnd) + "&UsedStatus=" + escape(UsedStatus)+"&daycount="+escape(daycount);
//    var sidex = "ExtField" + EFIndex;
    
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StayMaterialInfo.ashx?action=new' + UrlParam, //目标地址
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
                    if (item.ProdNo != null && item.ProdNo != "") 
                    {
                        alertPrint = 0;
                        $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' title=\"" + item.ProdNo + "\">" + item.ProdNo + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + item.ProductName + "</td>" +
                         "<td height='22' align='center' title=\"" + item.Specification + "\">" + item.Specification + "</td>" +
                       
                        "<td height='22' align='center' title=\"" + item.CodeName + "\">" + item.CodeName+ "</td>" +
                        "<td height='22' align='center' title=\"" + item.productcount + "\">" + item.productcount+ "</td>" +
                       
                        
                       
                         "<td height='22' align='center' >呆滞</td>").appendTo($("#pageDataList1 tbody"));
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
function jqControl(value) {
    var ret = "";
    var point = document.getElementById("hidPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
} 