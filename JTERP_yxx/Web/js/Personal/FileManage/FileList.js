var sessionSection = "";

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式

var currentPageIndex = 1;
var orderBy = ""; //排序字段
var ifshow = "0";
var isFirstSearch ="1"; // 是否第一次进入系统, 1，是，0 否
$(document).ready(function() {

    var requestObj = GetRequest();
    if (requestObj['Title'] != "" && requestObj['Title'] != null) {
        document.getElementById("txtTitle").value = requestObj['Title'];
    }
    if (requestObj['Content'] != "" && requestObj['Content'] != null) {
        document.getElementById("txtContent").value = requestObj['Content'];
    }
    if (requestObj['createdate1'] != "" && requestObj['createdate1'] != null) {
        document.getElementById("createDate1").value = requestObj['createdate1'];
    }
    if (requestObj['createdate2'] != "" && requestObj['createdate2'] != null) {
        document.getElementById("createDate2").value = requestObj['createdate2'];
    }
    if (requestObj['createname'] != "" && requestObj['createname'] != null) {
        document.getElementById("txtCreator").value = requestObj['createname'];
    }
    if (requestObj['pageSize'] != "" && requestObj['pageSize'] != null) {
        pageCount = requestObj['pageSize']; //每页计数
    }
    if (requestObj['orderby'] != "" && requestObj['orderby'] != null) {
        orderBy = requestObj['orderby']; //排序字段
    }

    if (requestObj['pageIndex'] != "" && requestObj['pageIndex'] != null) {
        TurnToPage(parseInt(requestObj['pageIndex']));
    }
    else {
        SearchEquipData(1);
        isFirstSearch ="0";
    }


});


function SearchEquipData(aa) {
    document.getElementById('isSearched').value = "1";    
    // isSearch = "1";
    ifshow = "0";
    TurnToPage(1);
}


function addNewPage(){
    parent.addTab(null, '300110221', '新建文件附件', "Pages/Personal/FileManage/FileAdd.aspx?ModuleID=30011022");
}
function editPage(value1,value2){
    parent.addTab(null, '300110222', '修改文件附件', "Pages/Personal/FileManage/FileEdit.aspx?ID="+value+"&typename="+update);
}


//去除全选按钮
function fnUnSelect(obj) {


    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 0; i < signFrame.rows.length - 1; i++) {

            iCount = iCount + 1;

            if ($("#chk" + i).attr("checked")) {
                checkCount = checkCount + 1;
            }

        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
}

function getFileExt(str) 
{ 
    var d=/\.[^\.]+$/.exec(str); 
    var newstr=d;
    if(newstr==".jpg" || newstr==".jpeg" || newstr==".gif" || newstr==".png" || newstr==".bmp")
    {
        return "图片";
    }else if(newstr==".doc" || newstr==".xls" || newstr==".txt" || newstr==".ppt" || newstr==".docx" || newstr==".xlsx" || newstr==".pptx")
    {
        return "办公文档";
    }
    else if(newstr==".zip" || newstr==".rar")
    {
        return "压缩包";
    }else{
        return "未知类型";
    }

}



function DealAttachment(flag,attachUrl) {
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    //下载附件
    else if ("download" == flag) {
    //获取附件路径
        //下载文件
         
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + escape(attachUrl), "_blank");
    }
}


//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    /*搜索内容*/
    var culturetype;
    currentPageIndex = pageIndex; 
    var txtTitle = document.getElementById("txtTitle").value;
    var txtContent = document.getElementById("txtContent").value;
    var createDate1 = document.getElementById("createDate1").value;
    var createDate2 = document.getElementById("createDate2").value;
    var createname = document.getElementById("txtCreator").value;
    
    if (isFirstSearch == 0){   
          culturetype = document.getElementById("inputCompuny").value;     
    }
    else{
        culturetype = (document.getElementById("inputCompuny").value == "") ? 0:document.getElementById("inputCompuny").value;
     } 
    
    //数据处理
     if (txtTitle + "" != "") {
        txtTitle = txtTitle.replace(/\'/g, "''");
        txtTitle = txtTitle.replace(/\%/g, "[%]");
    }


    if (txtContent + "" != "") {
        txtContent = txtContent.replace(/\'/g, "''");
        txtContent = txtContent.replace(/\%/g, "[%]");
    }

    var dt1 = str2date(createDate1);
    var dt2 = str2date(createDate2);
    if (dt1 > dt2 && dt2 != null) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "开始日期不能大于结束日期！");
        return;
    }
    
    //var testurl="e://laite//201310//2013103084125_社保凭证.xls";
    
    var action = "LoadallData";
    var prams = 'Title=' + escape(txtTitle) + '&Content=' + escape(txtContent) + '&createdate1=' + escape(createDate1) + '&createdate2=' + escape(createDate2) +
                 '&createname=' + escape(createname)+"&CultureType="+escape(culturetype) + "&pageIndex=" + pageIndex + "&pageSize=" + pageCount + "&orderby=" + escape(orderBy);
    document.getElementById("hidSearchCondition").value = prams;
    
     $.ajax({
        type: "POST", //用POST方式传输
        //dataType:"json",//数据格式:JSON
        url: '../../../Handler/Personal/FileManage/FileList.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&" + prams , //数据
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前
        
        success: function(result) {
            //alert(result);
            document.getElementById("checkall").checked = false;
            var result; eval("result=" + result);
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(result.data.list, function(i, item) {
 
                if (item.ID != "" && item.ID != null) {
                     
                    $("<tr class='newrow'></tr>").append("<td height='22' align='left'>" + "<input id='chk" + i + "'  onclick = 'fnUnSelect(this)'  Title='" + item.TypeName + "'    value='" + item.ID + "'   type='checkbox'/>" + "</td>" +
                                     "<td height='22' align='left'>" + item.TypeName + "</td>" + 
                                     "<td height='22' align='left'><span title=\"" + item.Title + "\">" + item.Title + "</span></td>" +
                                     "<td height='22' align='left'><div style='width:350px;height:30px;border:0px;overflow:hidden;text-overflow:ellipsis'><span title=\"" + item.Culturetent + "\">" + item.Culturetent.substring(0,25) + "</span></div></td>" +
                                     "<td height='22' align='left'>" + getFileExt(item.Attachment) + "</td>" +
                                     "<td height='22' align='left'>" + item.EmployeeName + "</td>" +
                                     "<td height='22' align='left' style='display:none' >" + item.DeptName + "</td>" +
                                     "<td height='22' align='left'>" + date2str(str2date(item.CreateDate), "yyyy-MM-dd") + "</td>" +
                                     "<td height='22' align='left'>" + date2str(str2date(item.ModifiedDate),"yyyy-MM-dd") + "</td>"+
                                   // "<td height='22' align='center'><a href=\"../../../upload/fileStorage/" +escape(item.Attachment) + "\" target='_blank'>查看</a>| <a href='FileEdit.aspx?ID=" + item.ID + "&typename="+ item.TypeName +"' >修改</a></td>").appendTo($("#pageDataList1 tbody")); 
 "<td height='22' align='center'><a href='#' onclick=\"DealAttachment('download','"+item.iFileAddr+"')\">查看</a>| <a href='FileEdit.aspx?ID=" + item.ID + "&typename="+ item.TypeName +"' >修改</a></td>").appendTo($("#pageDataList1 tbody")); 
 
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
            if (ifshow == "0") { hidePopup(); }
            $("#pageDataList1_Pager").show(); 
            Ifshow(document.form1.elements["Text2"].value); 
            pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
        } //接收数据完毕
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


//排序
function OrderBy(orderColum, orderTip) {
    if (document.getElementById('isSearched').value == "0") {
        return;
    }
    var ordering = "d";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↑") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    else {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    orderBy = orderColum + "_" + ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
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

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转至页数超出查询范围！");
    }
    else {
        ifshow = "0";
        this.pageCount = parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
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


function fnDel()
{
    var DetailID = '';
    var pageDataList1 = findObj("pageDataList1", document);
    for (i = 0; i < pageDataList1.rows.length; i++) {
        if ($("#chk" + i).attr("checked")) {
            //DetailID += $("#chk" + i).attr("title") + ',';
            DetailID+=$("#chk"+i).val()+',';
        }
    }
	DetailID = DetailID.substring(0,DetailID.length-1);
    if (DetailID == '') {
        alert('请至少选择一条数据！');
    }else {
         if (confirm("数据删除后将不可恢复！您确定要删除？")) {
            $.ajax({
                        type: "POST",
                        url: "../../../Handler/Personal/FileManage/FileList.ashx",
                        data: "action=delFile&FileIDs=" + DetailID,
                        dataType: 'json', //返回json格式数据
                        cache: false,
                        beforeSend: function() {

                        },

                        error: function() {
                            alert('请求发生错误！');
                        },
                        success: function(result) {
                            TurnToPage(1);
                        }
                    });
         }
    }
}