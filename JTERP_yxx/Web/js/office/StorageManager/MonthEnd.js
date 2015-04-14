function DoSave() {
    var yearMonth = $("#selYearMonthC").val();
    var startDate = $("#txtStartDateC").val();
    var endDate = $("#txtEndDateC").val();

    var title = "", msg = "";
    if (startDate == "") {
        title += "开始日期|";
        msg += "开始日期不能为空|";
    }
    if (endDate == "") {
        title += "结束日期|";
        msg += "结束日期不能为空|";
    }

    if (title != "" && msg != "") {
        popMsgObj.Show(title, msg);
    }

    var params = "action=calculation" +
                         "&yearMonth=" + yearMonth +
                         "&StartDate=" + startDate +
                         "&EndDate=" + endDate;
    $.ajax({
        type: "POST",
        dataType: "json",
        url: '../../../Handler/Office/StorageManager/MonthEnd.ashx',
        cache: false,
        data: params,
        beforeSend: function() {
        },
        success: function(msg) {
            popMsgObj.Show("月结|", msg.data + "|");
        },
        error: function() {
            popMsgObj.Show("月结|", "月结发生错误|");
        },
        complete: function() {
        }
    });


}