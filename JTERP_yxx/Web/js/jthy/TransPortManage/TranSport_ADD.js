var isnew = "1"; //1新建;2更新
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
$(document).ready(function() {

    requestobj = GetRequest();
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        document.getElementById("hiddOrderID").value = document.getElementById("headid").value;
        GetInfoById(document.getElementById("headid").value);
        fnGetDetail(document.getElementById("headid").value);
        $("#labTitle_Write1").html("调运单据查看");

        $("#ImgUp").css("display", "block");
        
    }
    else {
        $("#labTitle_Write1").html("调运单据新建");
        $("#ImgUp").css("display", "none");
    }

});

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


function GetInfoById(headid) {
    var action = "SearchTransPort";
    var orderBy = "id";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/TransPortManage/TransPortInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            isnew = "2";
            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {
                    document.getElementById("ImgUp").style.display = "block";
                    $("#txtTranSportID").val(item.transportid);
                    $("#txtTranSportID").css("display", "inline");
                    $("#divCodeRule").css("display", "none");
                    document.getElementById("drpStartStation").value = item.StartStation;
                    document.getElementById("drpArriveStation").value = item.ArriveStation;
                    document.getElementById("txtStartDate").value = item.StartDate;
                    document.getElementById("txtStartCarCode").value = item.StartCarCode;
                    document.getElementById("txtEndCarCode").value = item.EndCarCode;
                    document.getElementById("txtCarNo").value = item.CarNo;
                    document.getElementById("txtCarNum").value = item.CarNum;
                    document.getElementById("txtSendNum").value = item.SendNum;
                    document.getElementById("txtPPersonID").value = item.PPersonID;
                    document.getElementById("txtPPerson").value = item.PPersonName;
                    document.getElementById("hdDeptID").value = item.DeptID;
                    document.getElementById("DeptName").value = item.DeptName;
                    // document.getElementById("drpTransPortType").value = item.TransPortType; 调运类型，在本功能中，主要以火运为主
                    document.getElementById("UserPrincipal").value = item.CreatorName;
                    document.getElementById("txt_CreateDate").value = item.CreateDate;
                    document.getElementById("txtModifiedUserID").value = item.ModifiedName;
                    document.getElementById("statusd").value = item.states;
                    document.getElementById("txtTranSportState").value = item.states;
                    document.getElementById("txtTranSportNo").value = item.id;
                    if (item.ModifiedDate != "1900-01-01") {
                        document.getElementById("txtModifiedDate").value = item.ModifiedDate;
                    }

                    document.getElementById("drpJh_place").value = item.Jh_place;
                    document.getElementById("txtJh_ReceMan").value = item.Jh_ReceMan;
                    document.getElementById("txtSs_ReceMan").value = item.Ss_ReceMan;
                    document.getElementById("txtSs_quan").value = item.Ss_quan;
                    document.getElementById("txtSs_VeQuan").value = item.Ss_VeQuan;
                    document.getElementById("txtRemark").value = item.Remark;                 

                    if (item.billStatus == '2') //如果是确认状态
                    {
                        $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                        $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                        $("#txtConfirmDate").val(item.ConfirmDate);  //确认日期
                        fnStatus('3');   //按钮状态
                    }
                    else {
                        fnStatus('2');   //按钮状态
                    }
                    $("#txtOprtID").val('saveOk');
                }
            });
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() {

        } //接收数据完毕
    });
}

//获取明细信息
function fnGetDetail(headid) {
    var action = "SearchTransPort";
    var orderBy = "id";
    var ary = new Array();
    var rowsCount = 0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/TransPortManage/TransPortInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    rowsCount++;
                    FillSignRow(i, item.CarCode, item.GrossWeight, item.TareWeight, item.NetWeight, item.SumWeight, item.detailsid);
                });
            }
            $("#txtTRLastIndex").val(rowsCount + 1);
        },
        complete: function() {

            //fnTotalInfo();
        } //接收数据完毕
    });
}

//删除明细中所有数据
function fnDelRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        dg_Log.deleteRow(i);
    }
    var txtTRLastIndex = findObj("txtTRLastIndex", document); //重置最后行号为
    txtTRLastIndex.value = "0";
    fnTotalInfo();
}

//删除明细一行
function fnDelOneRow() {
    var dg_Log = findObj("dg_Log", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除
        if ($("#chk" + i).attr("checked")) {
            dg_Log.rows[i].style.display = "none";
        }
    }
    $("#checkall").removeAttr("checked");
    fnTotalInfo();
}




function getCoalNature(coalvalue, rowid) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractList.ashx?str=' + escape(coalvalue) + '&action=getprodcutinfo', //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            $("#txtSpecialName" + rowid).val(''); //质量热卡
            $("#txtUnitID" + rowid).val(''); //计量单位
            $("#txtUnitName" + rowid).val(''); //计量单位
            $.each(msg.data, function(i, item) {
                $("#txtSpecialName" + rowid).val(item.specification); //质量热卡
                $("#txtUnitID" + rowid).val(item.unitid); //计量单位
                $("#txtUnitName" + rowid).val(item.unitname); //计量单位 

            });

        },
        complete: function() { } //接收数据完毕
    });

}

function getCoalData(selectid, b) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractList.ashx?action=getcoaldata', //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                var seltext = item.ProductName;
                var selvalue = item.ID;
                var o = document.getElementById(selectid);
                var varItem = new Option(seltext, selvalue); //要添加的选项
                o.options.add(varItem); //添加选项
                if (b != '0') {
                    if (item.ID == b) {
                        o.value = b;
                    }
                }

            });

        },
        complete: function() { } //接收数据完毕
    });

}

/**///2014-03-31  
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='' >";
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:车号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCarCode" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:毛重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtGrossWeight" + rowID + "' onkeyup='return ValidateNumber(this,value)'  onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;' oninput=\"getNetWeight(" + rowID + ");\" onpropertychange=\"getNetWeight(" + rowID + ");\"  /> "; //添加列内容
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:皮重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTareWeight" + rowID + "' onkeyup='return ValidateNumber(this,value)'  onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;' oninput=\"getNetWeight(" + rowID + ");\" onpropertychange=\"getNetWeight(" + rowID + ");\"  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:净重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtNetWeight" + rowID + "' onkeyup='return ValidateNumber(this,value)'   onkeyup='return ValidateNumber(this,value)'   maxlength='10' type='text'  class='tdinput'    style=' width:90%;' />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:累计
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSumWeight" + rowID + "'  onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    txtTRLastIndex.value = rowID; //将行号推进下一行  


}

function getNetWeight(rowID) {
    if (document.getElementById("txtGrossWeight" + rowID) != "undefined" && document.getElementById("txtTareWeight" + rowID) != "undefined") {
        var GrossWeight = document.getElementById("txtGrossWeight" + rowID).value;
        var TareWeight = document.getElementById("txtTareWeight" + rowID).value;
        if (GrossWeight == "") {
            GrossWeight = "0";
        }
        if (TareWeight == "") {
            TareWeight = "0";
        }
        document.getElementById("txtNetWeight" + rowID).value = (Number(GrossWeight) - Number(TareWeight)).toFixed(2);

    }
}
function FillSignRow(i, CarCode, GrossWeight, TareWeight, NetWeight, SumWeight, detailsid) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;
    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='" + detailsid + "' >";
    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列:车号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><input  id='txtCarCode" + rowID + "' value=\"" + CarCode + "\"  style=' width:95%;'   type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:毛重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><input  id='txtGrossWeight" + rowID + "'   onkeyup='return ValidateNumber(this,value)'   value=\"" + GrossWeight + "\"  style=' width:95%;'   type='text'  class='tdinput' oninput=\"getNetWeight(" + rowID + ");\" onpropertychange=\"getNetWeight(" + rowID + ");\" /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:皮重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><input  id='txtTareWeight" + rowID + "'   onkeyup='return ValidateNumber(this,value)'   value=\"" + TareWeight + "\"  style=' width:95%;'   type='text'  class='tdinput'  oninput=\"getNetWeight(" + rowID + ");\" onpropertychange=\"getNetWeight(" + rowID + ");\" /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:净重
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtNetWeight" + rowID + "'   onkeyup='return ValidateNumber(this,value)'   value=\"" + NetWeight + "\"  maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:累计
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSumWeight" + rowID + "'  onkeyup='return ValidateNumber(this,value)'   value=\"" + SumWeight + "\"    type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    //  txtTRLastIndex.value = rowID; //将行号推进下一行  
    $("#txtTRLastIndex").val((rowID + 1).toString()); //将行号推进下一行

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

function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

//多选添加行
function AddSignRows() {
    //popTechObj.ShowListCheckSpecial('', 'check');
    //表单table增行，并初始化数据
    $("#dg_Log").Rows.Add();
}

function getMoney(rowid) {
    var imoney = document.getElementById("txtQuantity" + rowid).value * document.getElementById("txtUnitCost" + rowid).value;
    document.getElementById("txtMoney" + rowid).value = imoney;
    var summoney = 0;
    for (var i = 0; i < 100; i++) {
        if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
            summoney = summoney + Number(document.getElementById("txtMoney" + i).value);
        }
    }
    document.getElementById("txtContractMoney").value = summoney;

}

function SaveSellOrder() {
    var isconfirm = "";
    var strAction = "";

    isconfirm = "0";

    $("#hidBillStatus").val('1');
    //--------------20121118-------------//
    $("#Imgbtn_confirm").css("display", "inline");
    $("#btn_confirm").css("display", "none");
    //----------------------------------------//  

    if ($("#ddlTranSportID_ddlCodeRule").val() == "" && $("#ddlTranSportID_txtCode").val() == "") {
        popMsgObj.ShowMsg("调运单号不能为空，请填写！");
        return;
    }
    if (document.getElementById("txtSendNum").value == "" || document.getElementById("txtSendNum").value == "0") {
        popMsgObj.ShowMsg("原发吨数不能为空，请填写！");
        return;
    }
    
     if (document.getElementById("txtCarNum").value == "" || document.getElementById("txtCarNum").value == "0") {
        popMsgObj.ShowMsg("请输入原计划车数！");
        return;
    }
    
    //if (document.getElementById("txtStartStation").value == "") {
    if (document.getElementById("drpStartStation").value == "--请选择--") {
        popMsgObj.ShowMsg("请选择始发站点！");
        return;
    }
    if (document.getElementById("drpArriveStation").value == "--请选择--") {
        popMsgObj.ShowMsg("请选择终到站站点！");
        return;
    }
    //判断毛重
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var CarCode = $("#txtCarCode" + rowid).val();   //车号 
            var GrossWeight = $("#txtGrossWeight" + rowid).val(); //毛重  
            var TareWeight = $("#txtTareWeight" + rowid).val();   //皮重
            var NetWeight = $("#txtNetWeight" + rowid).val();  //净重   
            var SumWeight = $("#txtSumWeight" + rowid).val();        //累计
            var detailsid = $("#detailsid" + rowid).val();      //表体autoid
            if (TareWeight == "" || TareWeight=="0") {
                popMsgObj.ShowMsg("皮重不能为空");
                return;
            }
            if (GrossWeight == "" || GrossWeight == "0") {
                popMsgObj.ShowMsg("毛重不能为空");
                return;
            }

            if (Number(TareWeight) > Number(GrossWeight)) {
                popMsgObj.ShowMsg("皮重要小于毛重！");
                return;
            }

        }
    }





    
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();

    var headid = document.getElementById("headid").value;
    if (headid == "undefined" || headid == "null" || headid == "") {
        headid = "0";
    }

    $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/TransPortManage/DealTransPort.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=insert&billtype=1&headid=' + escape(headid) + '&isconfirm=' + escape(isconfirm),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
        
            isSave = "1";
            if (data.sta > 0) {
                $("#hiddOrderID").val(data.sta);
                $("#headid").val(data.sta);
                $("#txtTranSportID").val(data.data);
                $("#txtTranSportID").css("display", "inline");
                $("#divCodeRule").css("display", "none");
                isnew = "2";
                //                           
                //                          if(isconfirm=="0")
                //                          {
                //                            $("#hidBillStatus").val('1');
                //                          }
                //                          else if(isconfirm=="1")
                //                          {
                //                                $("#billstatus").val("确认");
                //                                $("#hidBillStatus").val('2');
                //                                $("#imgUnSave").css("display", "inline");
                //                                $("#btn_save").css("display", "none");
                //                                $("#ImgUnConfirm").css("display","none");
                //                                $("#UnConfirm").css("display","inline");
                //                          }
                //                        $("#hidStatus").val('0');
                //                        $("#hidSendStatus").val('0');

                var d, s = "";
                d = new Date(); //Create Date object. 
                s += d.getYear() + "-";
                s += (d.getMonth() + 1) + "-"; //Get month 
                s += d.getDate() + " "; //Get day 
                $("#txtModifiedDate").val(s);
                fnStatus('2');  //控制按钮状态
                var glb_BillTypeFlag = "30";
                var glb_BillTypeCode = "3";
                var glb_BillID = 0;                                //单据ID
                var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                var FlowJs_BillNo = 'txtTranSportID';          //当前单据编码名称
                var FlowJS_BillStatus = 'hidBillStatus';
                //            
                //  GetFlowButton_DisplayControl();

            }
            hidePopup();
            if (isconfirm == "0") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.info);
            }
            else if (isconfirm == "1") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.info);
            }
        }
    });

}


//获取主表信息
function fnGetInfo() {
    var strInfo = '';

    //var TranSportID=$("#txtTranSportID").val();// 调运单编号
    var TranSportIDType = document.getElementById("ddlTranSportID_ddlCodeRule").value;
    var TranSportID = ""; //调运单编号
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (TranSportIDType == "") {
        TranSportID = $("#ddlTranSportID_txtCode").val(); //调运单编号
    }
    if ($("#txtTranSportID").val() != "") {
        TranSportID = $("#txtTranSportID").val();
    }

    var StartStation = $("#drpStartStation").val(); // 发站
    var ArriveStation = $("#drpArriveStation").val(); // 到站
    var StartDate = $("#txtStartDate").val(); // 发运日期
    var StartCarCode = $("#txtStartCarCode").val(); // 首车号
    var EndCarCode = $("#txtEndCarCode").val(); // 尾车号
    var CarNo = $("#txtCarNo").val(); // 车次
    var CarNum = $("#txtCarNum").val(); // 原发车数
    var SendNum = $("#txtSendNum").val(); // 原发吨数
    var PPersonID = $("#txtPPersonID").val(); // 分管员
    var DeptID = $("#hdDeptID").val(); // 分管部门
    var TransPortType = $("#drpTransPortType").val(); // 调运类型

    var Jh_place = $("#drpJh_place").val(); //计划地址
    var Jh_ReceMan = $("#txtJh_ReceMan").val();  // 原收货人
    var Ss_ReceMan = $("#txtSs_ReceMan").val();  // 实收货人
    var Ss_VeQuan = $("#txtSs_VeQuan").val(); //实收车数
    var Ss_quan = $("#txtSs_quan").val();  // 实收吨数
    var Remark = $("#txtRemark").val();    // 备注

    // txtJh_ReceMan，txtSs_ReceMantxtCarNumtxtVehicle_quantity 原发车数txtSendNumtxtSs_quan 实收吨数txtRemark Ss_VeQuan
    
    strInfo = 'TranSportIDType=' + escape(TranSportIDType) + '&TranSportID=' + escape(TranSportID) + '&StartStation=' + escape(StartStation) + '&ArriveStation=' + escape(ArriveStation)+ 
    '&StartDate=' + escape(StartDate) + '&StartCarCode=' + escape(StartCarCode) + '&EndCarCode=' + escape(EndCarCode) + '&CarNo=' + escape(CarNo) +
    '&CarNum=' + escape(CarNum) + '&SendNum=' + escape(SendNum) + '&PPersonID=' + escape(PPersonID) + '&DeptID=' + escape(DeptID) + '&TransPortType=' + escape(TransPortType) +
    '&Jh_place=' + escape(Jh_place) + '&Jh_ReceMan=' + escape(Jh_ReceMan) + '&Ss_ReceMan=' + escape(Ss_ReceMan) + '&Ss_VeQuan=' + escape(Ss_VeQuan) +
    '&Ss_quan=' + escape(Ss_quan) + '&Remark=' + escape(Remark) + '';

    return strInfo;
}
//获取明细数据
function getDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var CarCode = $("#txtCarCode" + rowid).val();   //车号 
            var GrossWeight = $("#txtGrossWeight" + rowid).val(); //毛重  
            var TareWeight = $("#txtTareWeight" + rowid).val();   //皮重
            var NetWeight = $("#txtNetWeight" + rowid).val();  //净重   
            var SumWeight = $("#txtSumWeight" + rowid).val();        //累计
            var detailsid = $("#detailsid" + rowid).val();      //表体autoid

            SendOrderFit_Item[j] = [[CarCode], [GrossWeight], [TareWeight], [NetWeight], [SumWeight], [detailsid]];

        }

    }

    return SendOrderFit_Item;
}

//确认火车调运单
function Fun_ConfirmOperate() {
    var c = window.confirm("确定执行确认操作吗？");
    if (c == true) {


        var headid = $("#headid").val();

        action = "ConfirmTranS";



        var strParams = "action=" + escape(action) + "&headid=" + escape(headid) + "&billtype=1" + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/TransPortManage/DealTransPort.ashx",
            data: strParams,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() {
                popMsgObj.ShowMsg('请求失败');
            },
            success: function(data) {
                if (data.sta > 0) {
                    popMsgObj.ShowMsg('确认成功！');
                    fnStatus('3');  //确认之后的按钮状态
                    isconfirm = "1";
                    var d, s = "";
                    d = new Date(); //Create Date object. 
                    s += d.getYear() + "-";
                    s += (d.getMonth() + 1) + "-"; //Get month 
                    s += d.getDate() + " "; //Get day 

                    $("#txtConfirmDate").val(s);  //确认日期
                    $("#txtConfirmorId").val(data.sta);   //确认人id
                    $("#txtConfirmor").val(data.data);     //确认人姓名
                }
                else {
                    popMsgObj.ShowMsg('确认失败！');
                }

            },
            complete: function() { hidePopup(); } //接收数据完毕 
        });

    }
}

//执行取消确认
function cancelConfirm() {
    var c = window.confirm("确定执行取消确认操作吗？");
    if (c == true) {
        var headid = $("#headid").val();
        action = "CancelConfirmTranS";
        var strParams = "action=" + escape(action) + "&headid=" + escape(headid) + "&billtype=1" + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/TransPortManage/DealTransPort.ashx",
            data: strParams,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() {
                popMsgObj.ShowMsg('请求失败！');
            },
            success: function(data) {
                if (data.sta > 0) {
                    popMsgObj.ShowMsg('取消确认成功！');
                    fnStatus('2');  //按钮状态，
                    isconfirm = "0";
                    $("#txtConfirmDate").val("");  //清空确认日期
                    $("#txtConfirmorId").val('');   //清空确认人id
                    $("#txtConfirmor").val('');     //清空确认人姓名

                }
                else {
                    popMsgObj.ShowMsg(data.info);
                }
            },
            complete: function() { hidePopup(); } //接收数据完毕 
        });

    }
}

//控制按钮状态
function fnStatus(BillStatus) {
    try {
        switch (BillStatus) { //单据状态（1制单，2确认，3.变更）
            case '1': //制单（没保存）
                $("#imgSave").css("display", "inline");  //保存
                $("#imgUnSave").css("display", "none");
                $("#btn_confirm").css("display", "none");
                $("#Imgbtn_confirm").css("display", "inline");  //无法确认
                $("#UnConfirm").css("display", "none");
                $("#ImgUnConfirm").css("display", "inline");  //无法反确认

                break;
            case '2': //确认（以保存没确认）

                $("#imgSave").css("display", "inline");  //可以保存更新 
                $("#imgUnSave").css("display", "none");
                $("#btn_confirm").css("display", "inline"); //确认
                $("#Imgbtn_confirm").css("display", "none");
                $("#UnConfirm").css("display", "none");
                $("#ImgUnConfirm").css("display", "inline");  //无法反确认

                break;
            case '3':  //变更（已确认，可以反确认）
                $("#imgSave").css("display", "none");
                $("#imgUnSave").css("display", "inline");  //无法保存
                $("#btn_confirm").css("display", "none");
                $("#Imgbtn_confirm").css("display", "inline");  //无法确认
                $("#UnConfirm").css("display", "inline");   //可以反确认
                $("#ImgUnConfirm").css("display", "none");
        }
    }
    catch (e)
   { }

}

// 调运单状态修改 
function xiugai() {
    var TranSportNo = document.getElementById("txtTranSportID").value;  //调运单编号
    var TranSportState = $("#statusd").val();   //调运单状态
    var tranId = $("#txtTranSportNo").val();
    if (TranSportNo == "") {
        alert("请先添加调运");
        return;
    }
    ChangeStatus(TranSportNo, TranSportState, tranId);
}

