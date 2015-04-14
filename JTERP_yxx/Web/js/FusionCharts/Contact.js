//判断物品在明细中添加是否重复
function IsExistCheck(prodNo) {
    var sign = false;
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; //明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            var prodNo1 = document.getElementById("detailsid" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
                sign = true;
                break;
            }
        }
    }

    return sign;
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






//去除全选按钮
function fnUnSelect(obj) {
    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }
    else {
        //验证明细信息
        var signFrame = findObj("dg_Log", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 1; i < signFrame.rows.length; i++) {
            if (signFrame.rows[i].style.display != "none") {
                iCount = iCount + 1;
                var rowid = signFrame.rows[i].id;
                if ($("#chk" + rowid).attr("checked")) {
                    checkCount = checkCount + 1;
                }
            }
        }
        if (checkCount == iCount) {
            $("#checkall").attr("checked", "checked");
        }

    }
}








//只能输入数字
function ValidateNumber(e, pnumber) {
    if (!/^\d+[.]?\d*$/.test(pnumber)) {
        var newValue = /^\d+[.]?\d*/.exec(e.value);
        if (newValue != null) {
            e.value = newValue;
        }
        else {
            e.value = "";
        }
    }
    return false;
}



//计算各种合计信息
function fnTotalInfo() {
    var TotalPrice = 0; //金额合计
    var CountTotal = 0; //数量合计
    var DisPrice = 0; //折扣额合计
    var signFrame = findObj("dg_Log", document);
    var j = 0; var pCountDetail = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
        }
    }

}




function fnStatus(BillStatus) {


    try {
        switch (BillStatus) { //单据状态（1制单，2确认，3.变更,9关闭）
            case '1': //制单（没保存）
                $("#imgSave").css("display", "inline");  //保存
                $("#imgUnSave").css("display", "none");
                $("#btn_confirm").css("display", "none");
                $("#Imgbtn_confirm").css("display", "inline");  //无法确认
                $("#UnConfirm").css("display", "none");
                $("#ImgUnConfirm").css("display", "inline");  //无法反确认

                break;
            case '2': //确认（已保存没确认）  保存状态

                $("#imgSave").css("display", "inline");  //可以保存更新 
                $("#imgUnSave").css("display", "none");
                $("#btn_confirm").css("display", "inline"); //确认
                $("#Imgbtn_confirm").css("display", "none");
                $("#UnConfirm").css("display", "none");
                $("#ImgUnConfirm").css("display", "inline");
                $("#ImageClose").css("display", "none");
                $("#ImageClose_btn").css("display", "inline");
                $("#UnClose").css("display", "none");
                $("#UnClose_btn").css("display", "inline");


                //无法反确认

                break;
            case '3':  //变更（已确认，可以反确认）
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");  //无法保存
                $("#btn_confirm").css("display", "none");
                $("#Imgbtn_confirm").css("display", "inline");  //无法确认

                $("#UnConfirm").css("display", "inline");   //可以反确认
                $("#ImgUnConfirm").css("display", "none");
                $("#ImageClose").css("display", "inline");
                $("#ImageClose_btn").css("display", "none");
                $("#UnClose").css("display", "none");
                $("#UnClose_btn").css("display", "inline");
                break;
            case '9': //关闭
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");  //无法保存
                $("#btn_confirm").css("display", "none");
                $("#Imgbtn_confirm").css("display", "inline");  //无法确认
                $("#UnConfirm").css("display", "none");   //可以反确认
                $("#ImgUnConfirm").css("display", "inline");
                $("#ImageClose").css("display", "none");
                $("#ImageClose_btn").css("display", "inline");

                $("#UnClose").css("display", "inline");
                $("#UnClose_btn").css("display", "none");

        }


    }
    catch (e)
   { }
}


