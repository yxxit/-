var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var action = ""; //操作
var orderBy = ""; //排序字段



/*------------------------------------新建-----------------------------------*/
/*
* 新建
*/
function DoNew() {
    window.location.href = GetLinkParam();
}

/*
* 获取链接的参数
*/
function GetLinkParam() {
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    if(ModuleID=="2051506")
    ModuleID="2051505";
    else if(ModuleID=="2051504")
     ModuleID="2051503";
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    linkParam = "Disassembling.aspx?ModuleID=" + ModuleID
                            + "&pageIndex=" + currentPageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
    //返回链接的字符串
    return linkParam;
}
/*------------------------------------新建-----------------------------------*/

/*--------------------------------Start 全选---------------------------------------------*/
//全选
function SelectAllCk() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}
/*--------------------------------End 全选---------------------------------------------*/

function NumberSetPoint(num)
{
    var SetPoint = parseFloat(num).toFixed($("#hidSelPoint").val());
    return SetPoint;
}

/*
* 查询
*/
function DoSearch1(currPage) {

    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var InNo = document.getElementById("txtInNo").value;
    var BomName = $("#BomName").val();
    var BomID = $("#BomID").val();
    var DeptName = $("#DeptName").val();
    var txtDeptID = document.getElementById("txtDeptID").value;
    var createDate1 = $("#createDate1").val();
    var createDate2 = document.getElementById("createDate2").value;
    var UserExecutor = document.getElementById("UserExecutor").value;
    var txtExecutorID = document.getElementById("txtExecutorID").value;
    var BillStatus = $("#BillStatus").val();
    var billtype="1"
      var ModuleID = document.getElementById("hidModuleID").value;
    if(ModuleID=="2051506")
    billtype="2";
    
    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (CompareDate(createDate1, createDate2) == 1) {
        isFlag = false;
        fieldText = fieldText + "查询时间段|";
        msgText = msgText + "起始时间不能大于终止时间|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    var UrlParam = "action=getlist" +
   "&InNo=" + escape(InNo) + "&BomName=" + escape(BomName) + "&BomID="+escape(BomID) + 
   "&DeptName=" + escape(DeptName) + "&txtDeptID="+escape(txtDeptID) + "&createDate1="+escape(createDate1) + 
   "&UserExecutor=" + escape(UserExecutor) + "&txtExecutorID=" + escape(txtExecutorID) +
   "&BillStatus=" + escape(BillStatus) +"&createDate2="+escape(createDate2)+
   "&Flag=1&billtype="+escape(billtype);
    //设置检索条件
    document.getElementById("hidSearchCondition").value = UrlParam;
    if (currPage == null || typeof (currPage) == "undefined") {
        TurnToPage(1);
    }
    else {
        TurnToPage(parseInt(currPage, 10));
    }
}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    $("#checkall").attr("checked", false);
    currentPageIndex = pageIndex;
    //获取查询条件
    var searchCondition = document.getElementById("hidSearchCondition").value;

    var UrlParam = "pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据 
    var totalprice=0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/DisassemblingList.ashx?' + UrlParam, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "")
                    if(parseFloat(item.totalprice)!=0)
                    {
                        totalprice+=parseFloat(item.totalprice);
                    }
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='OptionCheck_" + item.id + "' name='Checkbox1'  value=" + item.id + " onclick=IfSelectAll('Checkbox1','checkall')  type='checkbox'/>" + "</td>" +
                        "<td height='22' align='center' title=\"" + item.billno  + "\"><a href='" + GetLinkParam() + "&InNoID=" + item.id + "'>" + fnjiequ(item.billno, 16) + "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.CreatDate + "\">" + fnjiequ(item.CreatDate, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.handsman + "\">" + fnjiequ(item.handsman, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.productname + "\">" + fnjiequ(item.productname, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.totalprice + "\">" + fnjiequ(item.totalprice, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.deptname + "\">" + fnjiequ(item.deptname, 10) + "</td>" +
                        "<td height='22' align='center' >" +item.status + "</td>").appendTo($("#pageDataList1 tbody"));

            });
            
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center'></td>" +
                        "<td height='22' align='center' >" + totalprice.toFixed($("#hidSelPoint").val()) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>").appendTo($("#pageDataList1 tbody"));
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
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 1; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
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

function SelectDept(retval) {
    alert(retval);
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

function AddPop() {
    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/extanim64.gif", "数据处理中，请稍候……");
}
function showPopup(img, img1, retstr) {
    document.all.Forms.style.display = "block";
    document.all.Forms.innerHTML = Create_Div(img, img1, true);
    document.all.FormContent.innerText = retstr;
}
function hidePopup() {
    document.all.Forms.style.display = "none";
}
function Create_Div(img, img1, bool) {
    FormStr = "<table width=100% height='104' border='0' cellpadding=0 cellspacing=0 bgcolor=#FFFFFF>"
    FormStr += "<tr>"
    FormStr += "<td width=90%  bgcolor=#3F6C96>&nbsp;</td>"
    FormStr += "<td width=10% height=20 bgcolor=#3F6C96>"
    if (bool) {
        FormStr += "<img src='" + img + "' style='cursor:hand;display:block;' id='CloseImg' onClick=document.all['Forms'].style.display='none';>"
    }
    FormStr += "</td></tr><tr><td height='1' colspan='2' background='../../../Images/Pic/bg_03.gif'></td></tr><tr><td valign=top height=104 id='FormsContent' colspan=2 align=center>"
    FormStr += "<table width=100% border=0 cellspacing=0 cellpadding=0>"
    FormStr += "<tr>"
    FormStr += "<td width=25% align='center' valign=top style='padding-top:20px;'>"
    FormStr += "<img name=exit src='" + img1 + "' border=0></td>";
    FormStr += "<td width=75% height=50 id='FormContent' style='padding-left:5pt;padding-top:20px;line-height:17pt;' valign=top></td>"
    FormStr += "</tr></table>"
    FormStr += "</td></tr></table>"
    return FormStr;
}

/*清空BOM*/
function ClearBomControl() {
    if (popBomObj.InputObj != null) {
        document.getElementById( popBomObj.InputObj).value = '';
        document.getElementById('BomName').value = '';
        document.getElementById('divBom').style.display = 'none';
        closeRotoscopingDiv(false, 'divPopBomShadow');
    }
}

/*BOM选择*/
function Fun_FillParent_BomContent(bomID, bomNo, routeID, routeName,proname) {
    if (popBomObj.InputObj != null) {
        document.getElementById(popBomObj.InputObj).value = bomID;
        document.getElementById('BomName').value = proname;
        closeRotoscopingDiv(false, 'divPopBomShadow');
        document.getElementById('divBom').style.display = 'none';
       
    }
    else {
        popMsgObj.ShowMsg('系统错误');
    }
}
//删除单据
function fnDel() {

    if (confirm('删除后不可恢复，你确定要删除吗？')) {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        for (var i = 0; i < ck.length; i++) {
            if (ck[i].checked) {
                ck2 += ck[i].value + ',';
            }
        }
        var IDArray = ck2.substring(0, ck2.length - 1);
        x = ck2.split(',');
        if (x.length - 1 <= 0) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请至少选择一项删除！");
            return;
        }
        else {
            var action = 'Del';
            var UrlParam = "action=" + action + "&strID=" + IDArray;
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/StorageManager/DisassemblingList.ashx?" + UrlParam,
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    //AddPop();
                },
                //complete :function(){ //hidePopup();},
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');

                },
                success: function(data) {
                    popMsgObj.ShowMsg(data.info);

                    if (data.sta > 0) {
                       DoSearch1();
                    }
                }
            });
        }
    }
    else {
        return false;
    }
}