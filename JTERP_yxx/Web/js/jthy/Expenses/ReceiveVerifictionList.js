
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

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var action = ""; //操作
var orderBy = "id_d"; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    currentPageIndex = pageIndex;
//    var TaskNo = $("#txtTaskID").val();//任务单号
//    var productname = $("#UserReportReal").val();//存货名称
//    var hbno = $("#txtSubject").val();//汇报单号
//     var Deptment = $("#Deptment").val();//部门名称
//       var startdate1 = $("#txtstartdate1").val();
//         var startdate2 = $("#txtstartdate2").val();
//         var enddate1 = $("#txtenddate1").val();
//         var enddate2 = $("#txtenddate2").val();  
         var TaskNo = $("#txtTaskID").val();//往来单位id
         var productname = $("#UserReportReal").val();//票据编码
         var hbno = $("#RVFtype").val();//账款类型
         var Deptment = $("#BillType").val();//开票类型
         var startdate1 = $("#txtstartdate1").val();
         var startdate2 = $("#txtstartdate2").val();
         var price1 = $("#txtprice1").val();
         var price2 = $("#txtprice2").val();
         var productcount2=0;//计划数量
         var FinishNum2=0;//完成数量
         var passnum2=0;//合格数量
         var nopassnum2=0;//不合格数量
         var WorkTime2=0;//工时
    //var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    //var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值

    //Start验证输入
         var heightval=0;

        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/FinanceManager/ReceiveVerifictionList.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&action=" + action + "&orderby=" + orderBy + "&TaskNo=" + escape(TaskNo) + "&productname=" + escape(productname) + "&hbno=" + escape(hbno)  + "&Deptment=" + escape(Deptment)  +"&startdate1=" + escape(startdate1)  +"&startdate2=" + escape(startdate2)  +"&enddate1=" + escape(price1)  +"&enddate2=" + escape(price2)  + "", //数据
            beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {

                    if (item.id != null && item.id != "") {
                      
                     
                        heightval+=22;
                        if(item.bCreateDate!="")
                        {
                           if(parseFloat(item.totalprice)!=0&&parseFloat(item.totalprice)!=NaN)
                           {
                                productcount2+=parseFloat(item.totalprice);
                           }
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor=\"#E7E7E7\" >"+item.bCreateDate+"</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.contactUnits + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.InvoiceType + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.BillingNum + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.currencyname + "</td>" +                                                               
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.exchangerate + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.totalprice + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.yaccounts + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.naccounts + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.AcceWay + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.acreatedate + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.PayOrInComeType + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + item.ShouFuKuanNO + "</td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                           if(parseFloat(item.yaccounts)!=0&&parseFloat(item.yaccounts)!=NaN)
                           {
                                FinishNum2+=parseFloat(item.yaccounts);
                           }
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor=\"#FFFFFF\" >"+item.bCreateDate+"</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.contactUnits + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.InvoiceType + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.BillingNum + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.currencyname + "</td>" +                                                               
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.exchangerate + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.totalprice + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.yaccounts + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.naccounts + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.AcceWay + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.acreatedate + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.PayOrInComeType + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#FFFFFF\" >" + item.ShouFuKuanNO + "</td>").appendTo($("#pageDataList1 tbody"));
                         }
                    }
                });
                
                var productcount1=parseFloat(productcount2).toFixed(2);
                var  FinishNum1=parseFloat(FinishNum2).toFixed(2);
                var passnum1=(parseFloat(productcount2)-parseFloat(FinishNum2)).toFixed(2);
//                var nopassnum1=parseFloat(nopassnum2).toFixed(2);
//                var  WorkTime1=parseFloat(WorkTime2).toFixed(2);
                 $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor=\"#E7E7E7\" >合计</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +                                                               
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + productcount1+ "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + FinishNum1 + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" >" + passnum1 + "</td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>" +
                                "<td height='22' align='center' bgcolor=\"#E7E7E7\" ></td>").appendTo($("#pageDataList1 tbody"));
             
          
            
                //页码
                ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>", //[url]
                        {style: pagerStyle, mark: "pageDataList1Mark",
                        totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                        onclick: "TurnToPage({pageindex});return false;"}//[attr]
                        );
                totalRecord = msg.totalCount;
                // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                document.getElementById("Text2").value = msg.totalCount;
                $("#ShowPageCount").val(pageCount);
                ShowTotalPage(msg.totalCount, pageCount, pageIndex);
                $("#ToPage").val(pageIndex);
                ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
                
               
            },
                
            error: function() { },
            complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            document.getElementById("divDetail").style.height=heightval+80+"px"; } //接收数据完毕
        });
    
    }
//    function CheckInput() {
//   
//}
function Fun_Search_ManufactureDispatchReportList(currPage) {
//alert("1");
var isFlag = true;
    search = "";
    //任务单编号
var TaskNo = $("#txtTaskID").val();//往来单位id
    var productname = $("#UserReportReal").val();//票据编码
    var hbno = $("#RVFtype").val();//账款类型
     var Deptment = $("#BillType").val();//开票类型
       var startdate1 = $("#txtstartdate1").val();
         var startdate2 = $("#txtstartdate2").val();
         var enddate1 = $("#txtprice1").val();
         var enddate2 = $("#txtprice2").val();
    var fieldText = "";
    var msgText = "";
    var search = "";

    //返回相关
    //单据编号
    search += "TaskNo=" + TaskNo;
    //物品名称
    search += "&productname=" + productname;
    //是否派工
    search += "&hbno=" + hbno;
    search += "&Deptment=" + Deptment;
    search += "&startdate1=" + startdate1;
    search += "&startdate2=" + startdate2;
    search += "&enddate1=" + enddate1;
    search += "&enddate2=" + enddate2;
   

//    //设置检索条件
  document.getElementById("hidSearchCondition").value = search;

   
    if (productname!="") {
        if (!CheckSpecialWord(productname)) {
            isFlag = false;
            fieldText = fieldText + "票据编码|";
            msgText = msgText + "不能含有特殊字符|";
        }
    }
    if(enddate1!=""&&enddate2!="")
    {
        if(parseFloat(enddate1)>parseFloat(enddate2))
        {
            isFlag = false;
            fieldText = fieldText + "开票金额|";
            msgText = msgText + "范围选择错误|";
        }
    }
      if(startdate1!=""&&startdate2!="")
    {
        if(startdate1>startdate2)
        {
            isFlag = false;
            fieldText = fieldText + "开票时间|";
            msgText = msgText + "范围选择错误|";
        }
    }
    
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (currPage == null || typeof (currPage) == "undefined"||currPage=="") {
            TurnToPage(1);
        }
        else {
            TurnToPage(parseInt(currPage));
        }
    }
}

function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pagecount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pagecount").style.display = "block";
    }
}


//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    if (!IsNumber(newPageIndex) || newPageIndex == 0) {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }

}
//排序
function OrderBy(orderColum, orderTip) {

    var hidSearchCondition = document.getElementById("hidSearchCondition").value;
    if (hidSearchCondition.length == 0) {
        return;
    }
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
    //alert( $("#" + orderTip).html());
    orderBy = orderColum + "_" + ordering;
    TurnToPage(1);
}
 //选择往来客户
   function SelectCust()
   {
       var url="../../../Pages/Office/FinanceManager/CustSelect.aspx";
       var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=500px");
       if(returnValue!="" && returnValue!=null)
       {
         var value=returnValue;
         value=value.split("|");
          document.getElementById("txtTaskID").value=value[0].toString();
          document.getElementById("txtTaskNo").value=value[1].toString();
         
       }
       else
       {
           document.getElementById("txtTaskID").value="";
           document.getElementById("txtTaskNo").value="";
          
       }
   }
  //验证是否为数据类型
  function checkprice(name)
  {
        var price=document.getElementById(name).value;
        if(price=="")
            return;
        if(parseFloat(price)==NaN)
        {
            document.getElementById(name).value="";
        }else
        {
            document.getElementById(name).value=parseFloat(price).toFixed(2);
        }
  }