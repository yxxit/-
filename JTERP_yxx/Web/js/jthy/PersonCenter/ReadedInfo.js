var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var orderBy = ""; //排序字段
var ifshow = "0";
var sessionSection = "";
//排序
var ordering = "asc";
var lastInfoTip = null;

$(document).ready(function() {
    requestobj = GetRequest();

    if (requestobj['isReturn'] == "1") {
        //从其他界面返回
        $("#txtTitle").val(requestobj['Title']);
        $("#txtSenderHidden").val(requestobj['SendUserID']);
        $("#txtSender").val(requestobj['SendUser']);
        $("#createDate1").val(requestobj['createDate1']);
        $("#createDate2").val(requestobj['createDate2']);
        pageCount = requestobj['pageSize'];
        TurnToPage(requestobj['pageIndex']);
    }
});

function DelMessage() {
    if (!confirm("确认删除吗")) {
        return;
    }

    var ck = document.getElementsByName("Checkbox1");
    var ids = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            if (ids != "")
                ids += ",";
            ids += ck[i].value;
        }
    }

    if (ids == "") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请至少选择一项删除！");
        return;
    }

    var action = "DelItem";
    //删除
    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/InputBox.ashx",
        data: "action=" + action + "&idList=" + ids,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(result) {
            if (result.result) {
                TurnToPage(1);
                ifshow = "1";
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除成功！");
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除失败！");
            }
        }
    });


}

//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    $("#hidSearchCondition").val('');
    currentPageIndex = pageIndex;
    var condition = "1=1";
    var txtTitle = document.getElementById("txtTitle").value;
    var txtSender = document.getElementById("txtSenderHidden").value;
    var createDate1 = document.getElementById("createDate1").value;
    var createDate2 = document.getElementById("createDate2").value;
    if (txtTitle + "" != "") {
        txtTitle = txtTitle.replace(/\'/g, "''");
        txtTitle = txtTitle.replace(/\%/g, "[%]");
        condition += " AND a.Title like '%" + txtTitle + "%'";
    }
    $("#hidSearchCondition").val("&Title=" + escape(txtTitle));
    if (txtSender + "" != "") {
        condition += " AND a.SendUserID in( " + txtSender + ")";
    }
    $("#hidSearchCondition").val($("#hidSearchCondition").val() + "&SendUserID=" + escape(txtSender));
    $("#hidSearchCondition").val($("#hidSearchCondition").val() + "&SendUser=" + escape($("#txtSender").val()));
    var dt1 = str2date(createDate1);
    var dt2 = str2date(createDate2);
    if (dt1 > dt2 && dt2 != null) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "开始日期不能大于结束日期！");
        return;
    }
    if (createDate1 + "" != "") {
        condition += " AND a.CreateDate>='" + createDate1 + "'";
    }
    $("#hidSearchCondition").val($("#hidSearchCondition").val() + "&createDate1=" + escape(createDate1));
    if (createDate2 + "" != "") {
        dt2 = dateAdd(dt2, 1 * 24 * 60 * 60 * 1000);
        condition += " AND a.CreateDate<='" + date2str(dt2, "yyyy-MM-dd") + "'";
    }
    $("#hidSearchCondition").val($("#hidSearchCondition").val() + "&createDate2=" + escape(createDate2));
    condition += " AND a.Status ='1'";
    var action = "LoadData";
    var fields = "a.[ID],a.SendUserID,a.CreateDate,a.ReceiveUserID,a.Title,a.Content,a.Status,b.EmployeeName";
    fields = UrlEncode(fields);
    var prams = "Fields=" + fields;
    prams += "&condition=" + UrlEncode(condition);
    if (orderBy + "" != "") {
        prams += "&OrderExp=" + UrlEncode(orderBy);
    }
    $("#hidSearchCondition").val($("#hidSearchCondition").val() + "&pageIndex=" + pageIndex + "&pageSize=" + pageCount);
    $.ajax({
        type: "POST", //用POST方式传输
        url: '../../../Handler/Personal/MessageBox/InputBox.ashx', //目标地址
        cache: false,
        data: "action=" + action
                + "&pageIndex=" + pageIndex
                + "&pageSize=" + pageCount
                + "&" + prams, //数据
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前
        success: function(result) {
            var result; eval("result=" + result);
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(result.data.list, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center'>" + "<input id='Checkbox1' name='Checkbox1'  value=" + item.ID + "  type='checkbox'/>" + "</td>" +
                        "<td height='22' align='center'><a href=\"#\" onclick=\"viewMsg(" + item.ID + ")\" title=\"点击查看详细\">" + item.Title + "</a></td>" +
                        "<td height='22' align='center'>" + item.Content + "</td>" +
                        "<td height='22' align='center'>" + item.EmployeeName + "</td>" +
                        "<td height='22' align='center'>" + date2str(str2date(item.CreateDate), "yyyy-MM-dd HH:mm:ss") + "</td>" +
                        "<td height='22' align='center'><a href='#' onclick=SendMessage('" + item.SendUserID + "','" + item.EmployeeName + "','" + item.Title + "');>回复</a></td>"
                        ).appendTo($("#pageDataList1 tbody"));
                }
            });
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: result.data.count, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = result.data.count;
            $("#pageDataList1_Total").html(totalRecord); //记录总条数
            document.form1.elements["Text2"].value = totalRecord;
            $("#ShowPageCount").val(pageCount);
            $("#ToPage").val(pageIndex);

            ShowTotalPage(totalRecord, pageCount, pageIndex);
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        complete: function() {
            if (ifshow == "0") {
                hidePopup();
            }
            $("#pageDataList1_Pager").show();
            Ifshow(document.form1.elements["Text2"].value);
            pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
        } //接收数据完毕
    });


}

// 回复短信
function SendMessage(id, name, Title) {
    window.location.href = 'SendInfo.aspx?ModuleID=10611&SendMode=0&UserID=' + id
                            + '&UserName=' + escape(name)
                            + '&Theme=' + escape('回复:' + Title)
                            + $("#hidSearchCondition").val();
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

function SearchEquipData(aa) {
    document.getElementById('isSearched').value = "1";
    search = "1";
    ifshow = "0";
    TurnToPage(1);
}
function ClearInput() {
    $(":text").each(function() {
        this.value = "";
    });
}
function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("PageCount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("PageCount").style.display = "block";
    }
}

function SelectDept(retval, action) {
    window.location.href = 'Equipment_Edit.aspx?retval=' + escape(retval) + '&action=' + action;
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    if (!IsNumber(newPageCount)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示条数必须为数字！");
        return;
    } else {
        if (newPageCount.substring(0, 1) == "0") {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示条数不能以0开头！");
            return;
        }
    }


    if (!IsNumber(newPageIndex)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转至页数必须为数字！");
        return;
    } else {
        if (newPageIndex.substring(0, 1) == "0") {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转至页数不能以0开头！");
            return;
        }
    }
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转至页数超出查询范围！");
    }
    else {
        ifshow = "0";
        this.pageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}

function OrderBy(obj, orderColum, orderTip) {
    if (document.getElementById('isSearched').value == "0") {
        return;
    }
    ifshow = "0";
    if (lastInfoTip != null) {
        lastInfoTip.innerHTML = "";
    }

    lastInfoTip = obj.getElementsByTagName("SPAN")[0];
    //var orderTipDOM = $("#"+orderTip);
    if (ordering == "asc") {
        lastInfoTip.innerHTML = "&nbsp;<font color=blue  size=3>↓</font>";
        ordering = "desc";
    } else {
        lastInfoTip.innerHTML = "&nbsp;<font color=blue size=3>↑</font>";
        ordering = "asc";
    }
    orderBy = orderColum + " " + ordering;

    TurnToPage(1);
}

function showInfo(msg) {
    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", msg);
}

$(document).ready(function() {

    LoadUserList('LoadUserList', BuildTree);


});

var curFlag = 0;
function swithEditPanel(flag) {
    var lastEle = document.getElementById("tab_" + curFlag);
    lastEle.className = "tab";

    curFlag = flag;


    document.getElementById("tab_" + flag).className = "selTab";


}

function LoadUserList(action, callback) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/SendInfo.ashx?action=" + action,
        dataType: 'string',
        data: '',
        cache: false,
        success: function(data) {
            var result = null;
            eval("result = " + data);

            if (result.result) {
                callback(result.data);

            } else {
                showInfo(result.data);
            }
        },
        error: function(data) {
            showInfo(data.responseText);
        }
    });
}

function clearSel() {
    document.getElementById("txtSender").value = "";
    document.getElementById("txtSenderHidden").value = "";

    treeview_unsel();
}

function hideSelPanel() {
    var box = document.getElementById("selUserBox");

    box.style.display = "none";
}

function showSelPanel() {
    var box = document.getElementById("selUserBox");
    box.style.left = "74%";
    box.style.top = "60px";
    box.style.display = "";
}

function viewMsg(id) {
    var action = "GetItem";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/InputBox.ashx?action=" + action,
        dataType: 'string',
        data: 'id=' + id,
        cache: false,
        success: function(data) {
            var result = null;
            eval("result = " + data);

            if (result.result) {
                //showInfo(result.data);  
                dispData(result.data);
            } else {
                showInfo(result.data);
            }
        },
        error: function(data) {
            showInfo(data.responseText);
        }
    });

}

function dispData(data) {
    var ttID = document.getElementById("ttID");
    var ttTitle = document.getElementById("ttTitle");
    var ttSendUser = document.getElementById("ttSendUser");
    var ttSendDate = document.getElementById("ttSendDate");
    var ttContent = document.getElementById("ttContent");
    ttTitle.innerHTML = data.ID;
    ttSendUser.innerHTML = data.SendUserID;
    ttTitle.innerHTML = data.Title;
    ttSendDate.innerHTML = data.CreateDate;
    ttContent.innerHTML = data.Content;

    document.getElementById("editPanel").style.left = "200px";
    document.getElementById("editPanel").style.top = "200px";
    document.getElementById("editPanel").style.display = "";
}

function hideMsg() {
    document.getElementById("editPanel").style.display = "none";
}