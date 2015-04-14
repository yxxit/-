var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var currentpageCount = 10;
var action = ""; //操作
var orderBy = "ModifiedDate_d"; //排序字段
var Isliebiao;

var ifdel = "0"; //是否删除
var issearch = "";

var typeflag = "";
    

$(document).ready(function() {
    requestobj = GetRequest();
    var Isliebiao = requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];

    var url = location.search;
    var theRequest = new Object();
    
    getLoadDate();//获取当前日期，并和控件赋值
});

//获取当前日期，并和控件赋值
function getLoadDate(){
    var date = new Date();
    var strYear = date.getFullYear();
    
    var month=date.getMonth()+1;
    month=month<10?("0"+month):month;
    var dt=date.getDate();
    dt=dt<10?("0"+dt):dt;
    
    var nowDate=strYear+"-"+month+"-"+dt;
    document.getElementById("txtEndT").value = nowDate;//默认结束时间   
    
    var oldDate=getLastMonthYestdy(date);  //对日期进行处理  
    document.getElementById("txtBeginT").value = oldDate;//默认开始时间
}
//获得上个月在昨天这一天的日期
function getLastMonthYestdy(date){  
     var daysInMonth = new Array([0],[31],[28],[31],[30],[31],[30],[31],[31],[30],[31],[30],[31]);  
     var strYear = date.getFullYear();    //获取年份
     var strDay = date.getDate();         //获取日期
     var strMonth = date.getMonth()+1;    //获取月份，并处理
     if(strYear%4 == 0 && strYear%100 != 0){  //判断是否是闰年
        daysInMonth[2] = 29;  
     }  
     if(strMonth - 1 == 0)  
     {  
        strYear -= 1;  
        strMonth = 12;  
     }  
     else  
     {  
        strMonth -= 1;  
     }  
     strDay = daysInMonth[strMonth] >= strDay ? strDay : daysInMonth[strMonth];  
     if(strMonth<10)    
     {    
        strMonth="0"+strMonth;    
     }  
     if(strDay<10)    
     {    
        strDay="0"+strDay;    
     }  
     datastr = strYear+"-"+strMonth+"-"+strDay;  
     return datastr;  
  }  


//获取url中"?"符后的字串
function GetRequest() {
    var url = location.search;
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }

    return theRequest;
}

function SearchSettleList(pageIndex) {

    currentPageIndex = pageIndex;
    TurnToPage(currentPageIndex);
}


function SelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    var BeginT = $("#txtBeginT").val();  //制单开始时间
    var EndT = $("#txtEndT").val();       //制单结束时间
    var SendNo = $("#txtSendNo").val(); //单据编号
    var CreateName = $("#txtPPerson").val(); //创建人
    var CustName=$("#txtProviderName").val();//供应商名称
    currentPageIndex = pageIndex;
    action = "SearchSettleList";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
        cache: false,
        data: "pageIndex=" + pageIndex + "&pageCount=" + currentpageCount + "&Action=" + action + "&orderby=" + orderBy + "&CustName=" + escape(CustName) +
                    "&BeginT=" + escape(BeginT) + "&EndT=" + escape(EndT) + "&SendNo=" + escape(SendNo) + "&CreateName=" + escape(CreateName),
        beforeSend: function () { AddPop(); $("#pageDataList1_PagerList").hide(); }, //发送数据之前

        success: function (msg) {

            //数据获取完毕，填充页面据显示
            //数据列表
            var total1 = 0;
            var total2 = 0;
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            var j = 1;
            $.each(msg.data, function (i, item) {
                if (item.id != null && item.id != "") {
                    var sttlCounts; //总结算数量
                    var sttlTotalPrices; //总金额
                    var ProductCount; //总发货数量
                    if (item.jt_cg == 0) {
                        sttlCounts = item.sttlCounts1;
                        sttlTotalPrices = item.sttlTotalPrices1;
                        ProductCount = item.ProductCount1;
                    } else {
                        sttlCounts = item.sttlCounts2;
                        sttlTotalPrices = item.sttlTotalPrices2;
                        ProductCount =item.ProductCount2;
                    }

                    if (parseFloat(item.sttlCount) != 0 && parseFloat(item.sttlCount) != "") {
                        total1 += parseFloat(item.sttlCount);
                    }
                    if (parseFloat(item.sttlTotalPrice) != 0 && parseFloat(item.sttlTotalPrice) != "") {
                        total2 += parseFloat(item.sttlTotalPrice);
                    }

                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" +
                        "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                        "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +
                        "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +
                        "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('" + item.id + "')><span title=\"" + item.cgfpNo + "\">" + item.cgfpNo + "</a></td>" +


                        "<td height='22' align='center'>" + item.custName + "</a></td>" +
                        "<td height='22' align='center'>" + item.payName + "</a></td>" +
                        "<td height='22' align='center'>" + item.sttlCount + "</a></td>" +
                        "<td height='22' align='center'>" + item.sttlPrice + "</a></td>" +
                        "<td height='22' align='center'>" + item.sttlTotalPrice + "</a></td>" +
                        "<td height='22' align='center'>" + item.CreateDate + "</a></td>" +
                        "<td height='22' align='center'>" + item.createName + "</a></td>" +
                        "<td height='22' align='center'>" + item.ConfirmorName + "</a></td>" +
                        "<td height='22' align='center'>" + item.SttlRemark + "</a></td>" +
                        "<td height='22' align='center'>" + sttlCounts + "</a></td>" +
                        "<td height='22' align='center'>" + QfwFormat((ProductCount - sttlCounts),2) + "</a></td>" +
                        "<td height='22' align='center'>" + sttlTotalPrices + "</a></td>" +
                        "<td height='22' align='center'>" + item.billstatus + "</a></td>").appendTo($("#pageDataList1 tbody"));
                }
            });

            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>本页合计</td>" +
                        "<td height='22' colspan='3'></td>" +
                        "<td height='22' align='center' >" + QfwFormat(total1, 2) + "</td>" +
                        "<td height='22' align='center'></td>" +
                        "<td height='22' align='center' >" + QfwFormat(total2, 2) + "</td>" +
                        "<td height='22' colspan='8'></td>").appendTo($("#pageDataList1 tbody"));

            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>" +
                        "<td height='22' colspan='3'></td>" +
                        "<td height='22' align='center' >" + QfwFormat(msg.ttlCount, 2) + "</td>" +
                        "<td height='22' align='center'></td>" +
                        "<td height='22' align='center' >" + QfwFormat(msg.ttlFee, 2) + "</td>" +
                        "<td height='22' colspan='8'></td>").appendTo($("#pageDataList1 tbody"));


            //页码
            ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: currentpageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"
                }//[attr]
                    ); document.getElementById("checkall").checked = false;
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.getElementById("Text2").value = msg.totalCount;
            $("#ShowPageCount").val(currentpageCount);
            ShowTotalPage(msg.totalCount, currentpageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function () { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function () { hidePopup(); $("#pageDataList1_PagerList").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}



//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        //t[i].onclick=function(){//鼠标点击
        //if(this.x!="1"){
        //this.x="1";//
        //this.style.backgroundColor=d;
        //}else{
        //	this.x="0";
        //this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
        //}
        //}
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}


function SelectAll() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

function CreateNew() {
    //Pages/JTHY/Expenses/IncomeSettle.aspx?ModuleID=8880130
    //window.parent.addTab(null, '90031', '登记到货', 'Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=0&ModuleID=90031');
    window.parent.addTab(null, '8880130', '采购结算单', 'Pages/JTHY/Expenses/IncomeSettle.aspx?ModuleID=8880130&typeflag=0');
}


function fnCheck() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var InNo = $('#txt_InNo').val(); //单据编号

    var BeginT = $("#txtBeginT").val();  //申请开始时间
    var EndT = $("#txtEndT").val();       //申请结束时间

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }




    if (InNo.length > 0 && InNo.match(/^[A-Za-z0-9_]+$/) == null) {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "单据编号输入不正确|";
    }


    if (BeginT != "" && EndT != "") {
        if (BeginT > EndT) {
            isFlag = false;
            fieldText = fieldText + "申请时间|";
            msgText = msgText + RetVal + "开始时间要小于结束时间|";
        }
    }




    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    return isFlag;
}


function ClearInput() {
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

function SelectDept(retval) {
    alert(retval);
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    if (!IsZint(newPageCount)) {
        popMsgObj.ShowMsg('显示条数必须输入正整数！');
        return;
    }
    if (!IsZint(newPageIndex)) {
        popMsgObj.ShowMsg('跳转页数必须输入正整数！');
        return;
    }
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        //            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        //            return false;
        popMsgObj.ShowMsg('转到页数超出查询范围！');
        return;
    }
    else {
        currentpageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
//排序
function OrderBy(orderColum, orderTip) {
    if (issearch == "")
        return;
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
    TurnToPage(1);
}

//新建
function GetLinkParam0(id) {
    window.parent.addTab(null, '8880130', '采购结算单', 'Pages/JTHY/Expenses/IncomeSettle.aspx?ModuleID=8880130&intMasterID=' + escape(id));
}

//删除操作 debugger;

    function Fun_DeleteIncomeSettle()
    {
        var ck = document.getElementsByName("Checkbox1");
        var table = document.getElementById("pageDataList1");
        var URLParams = "";
        var Action = "DeleteIncomeSettle";
        URLParams += "Action=" + Action;
        var index = 0;
        var IsId = "";
        var status = "";
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {

          //在js中判断状态值
         status = table.rows[ck[i].value].cells[10].innerText;
         if (status == "确认") {
             alert("结算单已经被确认！请先取消确认！");
             return;
         }

         if (status == "关闭") {

             alert("结算单已经被关闭！请先取消关闭！")
                 return;
         }
       
            IsId += table.rows[ck[i].value].cells[2].innerText + ',';
             index++;


                
                
            }
        }

        IsId = IsId.substring(0, IsId.length - 1);

        if (index == 0) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先选择数据后再删除！");
            return;
        }
        
        
        
        URLParams += "&Length=" + index + "";


        var c = window.confirm("确认执行删除操作吗？")
        if (c == true) {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: "../../../Handler/JTHY/Expenses/IncomeSettle.ashx", //目标地址
            data: URLParams + "&allIncomeSettle=" + escape(IsId) + '&billtype=' + escape(typeflag) + ' ', //数据
            cache: false,
            beforeSend: function() { }, //发送数据之前
            error: function() {
                popMsgObj.ShowMsg('请求发生错误！');
                return;
            },
            success: function(msg) {

                if (msg.sta > 0) {
                    popMsgObj.ShowMsg(msg.info);
                    SearchSettleList(1);  //刷新页面
                }
                else {
                    popMsgObj.ShowMsg(msg.info);
                }
            },


            complete: function() {
                hidePopup();
            }
        });
    }
}



