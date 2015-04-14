var isnew = "1"; //1新建;2更新
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
var isconfirm = "";




$(document).ready(function() {

    requestobj = GetRequest();
    // 
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {

        GetInfoById(document.getElementById("headid").value);
        GetDetailById(document.getElementById("headid").value);
        $("#labTitle_Write1").html("销售出库单查看");
    }
    else {
        $("#labTitle_Write1").html("销售出库单新建");
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

//根据出库单id获取主表信息
function GetInfoById(headid) {
    var action = "SearchOutWareOne";
    var orderBy = "id";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/stockmanage/OutWareInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=10&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {



                    isnew = "2";
                    $("#ddlOutWareID_ddlCodeRule").css("display", "none");
                    $("#ddlOutWareID_txtCode").val(item.outno);
                    $("#ddlOutWareID_txtCode").attr("readonly", "true");
                    $("#ddlOutWareID_txtCode").css("width", "95%");


                    $("#txtSourceBillID").val(item.frombillid);
                    $("#txtSourceBillNo").val(item.sendno);
                    $("#drpSettleType").val(item.settletype);
                    $("#txtCustomerID").val(item.custid);
                    $("#txtCustomerName").val(item.custname);
                    $("#txtInvoiceUnit").val(item.billunit);
                    $("#drpTransPortType").val(item.transporttype);
                    $("#txtOprID").val(item.ppersonid);
                    $("#txtOprName").val(item.pperson);
                    $("#txtOutWareTime").val(item.outdate);
                    $("#txtOutNum").val(item.outcount);
                    $("#txtPPersonID").val(item.ppersonid);
                    $("#txtPPerson").val(item.pperson);
                    $("#hdDeptID").val(item.deptid);
                    $("#DeptName").val(item.deptname);
                    $("#txtSendNum").val(item.transendcount);
                    $("#txtTransMoney").val(Number(item.transmoney).toFixed(2));  //运费
                    $("#txtRemark").val(item.remark);
                    //$("#txtCoalID").val(item.productid);
                    //$("#txtCoalName").val(item.productname);
                    //$("#txtQuantity").val(item.productcount);
                    //$("#txtSaleCost").val(item.unitprice);
                    //$("#txtTax").val(item.taxmoney);
                    //$("#txtTaxMoney").val(item.totalprice);

                    $("#txtTranSportID").val(item.diaoyunid);
                    $("#txtTranSportNo").val(item.diaoyunno);
                    $("#txtTranSportState").val(item.transstate);
                    $("#txtCarNo").val(item.carno);
                    $("#txtStartStation").val(item.startystation);
                    $("#txtEndStation").val(item.endstation);
                    $("#txtCarNum").val(item.carnum);
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
        complete: function() { } //接收数据完毕
    });
}

//根据出库单id获取明细表信息
function GetDetailById(headid) {
    var action = "SearchOutWareOneDetail";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/stockmanage/OutWareInfo.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {

                    FillSignRow(i, item);



                }
            });
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}


//删除明细中所有数据
function fnDelRow() {
    var dg_Log = findObj("TableCoalInfo", document);
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
    var dg_Log = findObj("TableCoalInfo", document);
    var rowscount = dg_Log.rows.length;
    for (i = rowscount - 1; i > 0; i--) {//循环删除行,从最后一行往前删除

        if ($("#chk" + i).attr("checked")) {
            dg_Log.rows[i].style.display = "none";
        }
    }
    $("#checkall").removeAttr("checked");
    //fnTotalInfo();
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
            $.each(msg.data, function(i, item) {
                $("#txtUnit" + rowid).val(item.unitname); //产品编号
                var storageid = item.storageId;
                if (storageid == "") {
                    storageid = '0';
                }
                $("#drpWare" + rowid).val(storageid);

            });

        },
        complete: function() { } //接收数据完毕
    });

}

///**///2012-12-25 因页面改版，原版本注释掉。
//function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
//   var txtTRLastIndex = document.getElementById("txtTRLastIndex");
//    
//    var rowID = parseInt(txtTRLastIndex.value) + 1;
//    var signFrame = document.getElementById("TableCoalInfo");
//    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
//    newTR.id = rowID;
//    var m=0;
//    
////    var newNameXH = newTR.insertCell(m); //添加列:选择框
////    newNameXH.className = "cell";
////    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid"+rowID+"' value='' >";
////    m++;

// 
//     //加载仓库
//    var newFitNametd = newTR.insertCell(m); //添加列:仓库
//    newFitNametd.className = "cell";
//    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
//    "<select name='drpWare"+rowID+"' class='tddropdlist' style='width: 95%;'  runat='server' id='drpWare"+rowID+"' ></select></td></tr></table>"; //添加列内容
//    m++;
//    
//    getWareData('drpWare'+rowID,0);//加载仓库数据
//    
//     //加载煤种数据 
//    var newFitNametd = newTR.insertCell(m); //添加列:煤种
//    newFitNametd.className = "cell";
//    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
//    "<select name='drpCoalName"+rowID+"' disabled='disabled' class='tddropdlist' style='width: 95%;'  runat='server' id='drpCoalName"+rowID+"' onChange='getCoalNature(this.value,"+rowID+")' ></select></td></tr></table>"; //添加列内容
//    m++;
//    
//    getCoalData('drpCoalName'+rowID,0);//加载煤种数据
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:煤种编号
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtProNo" + rowID + "' maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:数量
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "' onpropertychange=\"getMoneyAndCount("+rowID+");\"   onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/><input type='hidden' id='hidQuantity" + rowID + "'  />"; //添加列内容
//    m++;
//    


//    var newFitDesctd = newTR.insertCell(m); //添加列:销售单价
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtSaleCost" + rowID + "'  onpropertychange=\"getMoney("+rowID+");\"  onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:税率
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' onpropertychange=\"getMoney("+rowID+");\"   maxlength='10' type='text' value='17' onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:税额
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtTax" + rowID + "' maxlength='10' type='text'   onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:金额
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "'    onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//      
//    
//     var newFitDesctd = newTR.insertCell(m); //添加列:已出库数量
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtComOutNum" + rowID + "'    type='text' class='tdinput'   disabled='disabled'   style='width:90%;'/>"; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:已出库数量
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtComSettleNum" + rowID + "'    type='text' class='tdinput'    disabled='disabled'     style='width:90%;'/>"; //添加列内容
//    m++;
//    txtTRLastIndex.value = rowID; //将行号推进下一行   
//    
//}

function FillSignRow(i, item) {

    debugger
    var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("TableCoalInfo");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    //    var newNameXH = newTR.insertCell(m); //添加列:选择框
    //    newNameXH.className = "cell";
    //    newNameXH.innerHTML = "<input id='chk" + rowID + "' style='display:none;' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid"+rowID+"' value='' >";
    //    m++;


    //加载仓库
    var newFitNametd = newTR.insertCell(m); //添加列:仓库
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%' ><tr><td>" +
    "<select name='drpWare" + rowID + "' class='tddropdlist' style='width: 95%;'  runat='server' id='drpWare" + rowID + "' ></select></td></tr></table>"; //添加列内容
    m++;

    getWareData('drpWare' + rowID, item.StorageID); //加载仓库数据

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    "<select name='drpCoalName" + rowID + "' disabled='disabled'  class='tddropdlist' style='width: 95%;'  runat='server' id='drpCoalName" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    m++;

    getCoalData('drpCoalName' + rowID, item.ProductID); //加载煤种数据

    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnit" + rowID + "' value=\"" + item.unitName + "\"  maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /><input type='hidden' id='hidOutBusMxId" + rowID + "'  value=\"" + item.id + "\"   /> "; //添加列内容,隐藏输入框保存销售发货单明细id
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "' value=\"" + item.ProCount + "\"   onpropertychange=\"getMoneyAndCount(" + rowID + ");\"   onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/><input type='hidden' id='hidPreQuantity" + rowID + "'  value=\"" + item.ProCount + "\"   />"; //隐藏域hidPreQuantity用来获取保存订单时的数量
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:销售单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSaleCost" + rowID + "' value=\"" + Number(item.TaxPrice).toFixed(2) + "\"   onpropertychange=\"getMoney(" + rowID + ");\"  onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:税率
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' value=\"" + Number(item.TaxRate).toFixed(2) + "\" onpropertychange=\"getMoney(" + rowID + ");\"   maxlength='10' type='text'  onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:税额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTax" + rowID + "' value=\"" + Number(item.TotalTax).toFixed(2) + "\"   maxlength='10' type='text' onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value=\"" + Number(item.TotalFee).toFixed(2) + "\"    onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:发货总数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTotalNum" + rowID + "' value=\"" + item.TotalNum + "\"    type='text' class='tdinput'  disabled='disabled'  style='width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:未出库数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnOutNum" + rowID + "' value=\"" + item.ProductCount + "\"    type='text' class='tdinput'  disabled='disabled'  style='width:90%;'/>"; //添加列内容
    m++;
    //getTotalCount();  //获取总的出库数量
    txtTRLastIndex.value = rowID; //将行号推进下一行  
}

//加载煤种下拉列表
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

//绑定仓库下拉列表
function getWareData(selectid, b) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Common/WareInfo.ashx?action=getwaredata', //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                var seltext = item.StorageName;
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

//获取金额
function getMoney(rowid) {
    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var SaleCost = $("#txtSaleCost" + rowid).val();  //销售单价
    var imoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //金额

    document.getElementById("txtMoney" + rowid).value = Number(imoney).toFixed(2);

    var taxRate = Number($("#txtTaxRate" + rowid).val()) / 100; //税率

    var disTaxMoney = (Number(imoney) / (1 + taxRate)).toFixed(2); //除税金额
    var taxMoney = Number(imoney - disTaxMoney).toFixed(2);  //税额


    $("#txtTax" + rowid).val(taxMoney);


}

//当数量改变时既要获取总金额，又要获取总的出库数量
function getMoneyAndCount(rowid) {
    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var SaleCost = $("#txtSaleCost" + rowid).val();  //销售单价
    var imoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //金额

    document.getElementById("txtMoney" + rowid).value = Number(imoney).toFixed(2);

    var taxRate = Number($("#txtTaxRate" + rowid).val()) / 100; //税率

    var disTaxMoney = (Number(imoney) / (1 + taxRate)).toFixed(2); //除税金额
    var taxMoney = Number(imoney - disTaxMoney).toFixed(2);  //税额


    $("#txtTax" + rowid).val(taxMoney);
    getTotalCount(); //获取总的出库数量
}

//获取总的出库数量
function getTotalCount() {
    var summoney = 0;
    for (var i = 0; i < 100; i++) {
        if (document.getElementById("txtQuantity" + i) != "undefined" && document.getElementById("txtQuantity" + i) != null) {
            summoney = summoney + Number(document.getElementById("txtQuantity" + i).value);
        }
    }
    document.getElementById("txtOutNum").value = Number(summoney).toFixed(2);
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
            pCountDetail = $("#ProductCount" + rowid).val(); //数量
            if ($.trim(pCountDetail) == '') {
                pCountDetail = 0;
            }
            var UnitPriceDetail = $("#UnitPrice" + rowid).val(); //单价
            if ($.trim(UnitPriceDetail) == '') {
                UnitPriceDetail = 0;
            }
            else {
                Number_round(document.getElementById("UnitPrice" + rowid), precisionLength);
                if (!IsNumeric($("#UnitPrice" + rowid).val(), 14, precisionLength)) {
                    $("#UnitPrice" + rowid).val('');
                    UnitPriceDetail = 0;
                }
            }

            var SupplyPrice = $("#SupplyPrice" + rowid).val(); //供货价
            if ($.trim(SupplyPrice) == '') {
                SupplyPrice = 0;
            }
            else {
                Number_round(document.getElementById("SupplyPrice" + rowid), precisionLength);
                if (!IsNumeric($("#SupplyPrice" + rowid).val(), 14, precisionLength)) {
                    $("#SupplyPrice" + rowid).val('');
                    SupplyPrice = 0;
                }
            }


            var DiscountPrice = $("#DiscountPrice" + rowid).val(); //折扣额
            if ($.trim(DiscountPrice) == '') {
                DiscountPrice = 0;
            }
            else {
                Number_round(document.getElementById("DiscountPrice" + rowid), precisionLength);
                if (!IsNumeric($("#DiscountPrice" + rowid).val(), 14, precisionLength)) {
                    $("#DiscountPrice" + rowid).val('');
                    DiscountPrice = 0;
                }
            }
            var TotalPriceDetail = FormatAfterDotNumber((SupplyPrice * pCountDetail - DiscountPrice), precisionLength); //金额

            $("#TotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, precisionLength)); //金额
            TotalPrice += parseFloat(TotalPriceDetail);
            CountTotal += parseFloat(pCountDetail);
            DisPrice += parseFloat(DiscountPrice);

        }
    }
    //alert(TotalPrice);
    $("#CountTotalPrice").val(FormatAfterDotNumber(TotalPrice, precisionLength));
    //   $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength));
    $("#CountTotal").val(CountTotal);
    $("#DiscountPrice").val(FormatAfterDotNumber(DisPrice, precisionLength));
    $("#TotalPrice").val($("#CountTotalPrice").val());
    $("#DiscountAmount").val(($("#TotalPrice").val() * 1.0) * ($("#discount").val() * 1.0));

    changeDiscountAmount();
}
function changeDiscountAmount() {
    $("#DiscountAmount").val(($("#TotalPrice").val() * 1.0) - ($("#discount").val() * 1.0));
    $("#discount").val($("#DiscountPrice").val());
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
//表单验证
function CheckInput() {
    var fieldText = "";
    var isFlag = true;
    var msgText = "";

    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            iCount = iCount + 1;
            var ProductCount = $("#ProductCount" + rowid).val();

            if (ProductCount == "") {
                isFlag = false;
                fieldText = fieldText + "订单明细（行号：" + i + "）|";
                msgText = msgText + "请输入订单数量|";
            }
            else {
                if (!IsNumeric(ProductCount, 14, precisionLength)) {
                    isFlag = false;
                    fieldText = fieldText + "订单明细（行号：" + i + "）|";
                    msgText = msgText + "订单数量只能为数字|";
                }
            }
        }
    }

    if (iCount == 0) {
        isFlag = false;
        fieldText = fieldText + "订单明细|";
        msgText = msgText + "订单明细不能为空！|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }

    return isFlag;
}
//多选添加行
function AddSignRows() {
    //popTechObj.ShowListCheckSpecial('', 'check');
    //表单table增行，并初始化数据
    $("#dg_Log").Rows.Add();
}
//选择药品后，为明细栏 赋值 
function GetValue() {
    var ck = document.getElementsByName("CheckboxProdID");
    var strarr = '';
    var str = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            strarr += ck[i].value + ',';
        }
    }
    str = strarr.substring(0, strarr.length - 1);
    if (str == "") {
        popMsgObj.ShowMsg('请先选择数据！');
        return;
    }
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/MedicineManager/MedicineList.ashx?str=' + str + '&action=getprodcutinfo', //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                //填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                if (!IsExistCheck(item.ProdNo))//如果是重复的物品编号不允许添加了
                {
                    AddSignRow();
                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value);
                    $("#ProductNo" + rowID).val(item.cInvAddCode); //商品编号
                    if (document.getElementById("hiddProductNo" + rowID))
                        $("#hiddProductNo" + rowID).val(item.ProductNo); //商品编号
                    if (document.getElementById("ProductName" + rowID))
                        $("#ProductName" + rowID).val(item.cInvName); //商品名称]
                    if (document.getElementById("PYShort" + rowID))
                        $("#PYShort" + rowID).val(item.PYShort);
                    if (document.getElementById("Specification" + rowID))
                        $("#Specification" + rowID).val(item.Specification); //规格
                    if (document.getElementById("UnitID" + rowID))
                        $("#UnitID" + rowID).val(item.UnitName); //单位
                    if (document.getElementById("ProductCount" + rowID))
                        $("#ProductCount" + rowID).val('0'); //数量
                    if (document.getElementById("UnitPrice" + rowID))
                        $("#UnitPrice" + rowID).val(item.UnitPrice); //单价
                    if (document.getElementById("GuidPrice" + rowID))
                        $("#GuidPrice" + rowID).val(item.GuidPrice); //指导价
                    if (document.getElementById("SupplyPrice" + rowID))
                        $("#SupplyPrice" + rowID).val(item.GuidPrice); //供货价
                    if (document.getElementById("DiscountPrice" + rowID))
                        $("#DiscountPrice" + rowID).val("0");
                    //$("#TotalPrice" + rowID).val(item.TotalPrice); //金额
                    // $("#Remark" + rowID).val(item.Remark);      //备注                  
                }
                else {
                    alert("明细中不允许添加重复的物品！");
                }
            });
            fnTotalInfo();
        },
        complete: function() { } //接收数据完毕
    });
    closeProductdiv();
}

//判断物品在明细中添加是否重复
function IsExistCheck(prodNo) {
    var sign = false;
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; //明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            var prodNo1 = document.getElementById("ProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
                sign = true;
                break;
            }
        }
    }

    return sign;
}

//选择业务员
function fnSelectSeller() {

    alertdiv('UserSeller,Seller');

}
//选择收款员，默认情况下，收款员和业务员是同一人
function fnSelectCashier() {
    alertdiv('cashier,cashierId');
}

//选择客户
function fnSelectCustInfo() {
    popSellCustObj.ShowList('protion');
}
//选择T6部门
function fnSelectDept() {
    popSellDeptObj.ShowList('protion');
}

////选择收货联系人
//function fnSelectSeller1() {
//        // 
//        alertdiv('UserSeller,Seller');
//   
//}
////选择采购联系人
//function fnSelectSeller2() {
//   // 
//         alertdiv('cashier,cashierId');
//   
//}


//选择客户后，为页面填充数据
function GetCust(CustID, CustNo, CustName) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/CustManager/CustName.ashx",
        data: 'id=' + custID + 'CustNo=' + CustNo + 'CustName=' + CustName,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
            $("#CustID").val(data.CustName); //客户名称
            $("#CustID").attr("title", custID); //客户编号     
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}

//用于选择单据编号  
function DDL_Sell_Order_Change()//添加下拉框onchange事件 用于选择 0--手动输入 1--自动
{
    var ddlCodeR = document.getElementById('ddlSellOrderRule');
    if (ddlCodeR.selectedIndex == "0")                                  /*手动输入*/
    {
        //document.getElementById("txtOrderNo").style.visibility = "visible";
        document.getElementById("txtOrderNo").value = "";
        document.getElementById("txtOrderNo").disabled = false;
    }
    else {
        document.getElementById("txtOrderNo").disabled = true;
        document.getElementById("txtOrderNo").value = "保存时自动生成";
    }
}

//从发货单（出库）信息自定义控件传值
function GetOutBus(id, sendno, settletype, transporttype, custid, custname, billunit, sendnum, ppersonid, ppersonname, deptid, deptname, transmoney, diaoyunid, diaoyunno, transstate, carno, startstation, endstation, carnum, at_state, providerid, providername, CustJsFee, ProJsFee, SellMoney, ProMoney) {


    document.getElementById("txtSourceBillID").value = id;
    document.getElementById("txtSourceBillNo").value = sendno;
    document.getElementById("drpSettleType").value = settletype;
    document.getElementById("drpTransPortType").value = transporttype;
    document.getElementById("txtCustomerID").value = custid;
    document.getElementById("txtCustomerName").value = custname;
    document.getElementById("txtInvoiceUnit").value = billunit;
    document.getElementById("txtSendNum").value = sendnum;
    document.getElementById("txtPPersonID").value = ppersonid;
    document.getElementById("txtPPerson").value = ppersonname;
    document.getElementById("hdDeptID").value = deptid;
    document.getElementById("DeptName").value = deptname;
    document.getElementById("txtTransMoney").value = Number(transmoney).toFixed(2);

    //document.getElementById("drpWare").value =storageid;
    //document.getElementById("txtCoalID").value =productid;
    //document.getElementById("txtCoalName").value =productname;
    //document.getElementById("txtQuantity").value =productcount;
    //document.getElementById("txtSaleCost").value =unitcost;
    //document.getElementById("txtTaxRate").value =taxrate;
    //document.getElementById("txtTax").value =taxmoney;
    //document.getElementById("txtTaxMoney").value =imoney;

    document.getElementById("txtTranSportID").value = diaoyunid;
    document.getElementById("txtTranSportNo").value = diaoyunno;
    document.getElementById("txtTranSportState").value = transstate;
    document.getElementById("txtCarNo").value = carno;
    document.getElementById("txtStartStation").value = startstation;
    document.getElementById("txtEndStation").value = endstation;
    document.getElementById("txtCarNum").value = carnum;
    //$("#drpUPTranSportState").val(at_state);  //操作下拉列表
    $("#txtOutNum").val("0.00");

    fnGetDetail_Bus(id);   //获取煤种明细
    document.getElementById('HolidaySpan_OutBus').style.display = "none";
    closeRotoscopingDiv(false, "divJTNameS_OutBus"); //关闭遮罩层


}

function SaveSellOrder() {
    // 

    if ($("#ddlOutWareID_ddlCodeRule").val() == "" && $("#ddlOutWareID_txtCode").val() == "") {
        popMsgObj.ShowMsg("出库单编号不能为空，请填写！");
        return;
    }
    if ($("#txtSourceBillNo").val() == "") {
        popMsgObj.ShowMsg("来源出库单号不能为空，请选择！");
        return;
    }
    if ($("#txtOprName").val() == "") {
        popMsgObj.ShowMsg("出库人不能为空，请选择！");
        return;
    }
    //        if($("#txtOutNum").val()=="")
    //        {
    //            popMsgObj.ShowMsg("出库数量不能为空，请选择！");
    //            return;
    //        }
    if (Number($("#txtOutNum").val()).toFixed(2) == 0.00) {
        popMsgObj.ShowMsg("出库总数量必须大于零，请填写！");
        return;
    }

    var lineIndex = 0;   //遍历的行数
    var lineRealIndex = 0;  //实际的行数
    for (var i = 1; i < 100; i++) {
        lineIndex++;
        if (document.getElementById("txtQuantity" + i) != "undefined" && document.getElementById("txtQuantity" + i) != null) {
            if (Number($("#txtQuantity" + i).val()) > Number($("#txtUnOutNum" + i).val())) {
                popMsgObj.ShowMsg("第" + i + "行煤种数量必须小于未出库数量，请填写！");
                return;
            }
            lineRealIndex++;
        }
        if ((lineIndex - lineRealIndex) > 2) {
            break;
        }
    }
    // 
    var strfitinfo = getDropValues().join("|");

    // 
    var strInfo = fnGetInfo();


    var headid = document.getElementById("headid").value;
    if (headid == "undefined" || headid == "null" || headid == "") {
        headid = "0";
    }

    $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/stockmanage/OutWare_ADD.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=insert&headid=' + escape(headid) + '&isconfirm=' + escape(isconfirm),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            isSave = "1";
            if (data.sta > 0) {
                isnew = "2";
                $("#ddlOutWareID_ddlCodeRule").css("display", "none");
                $("#ddlOutWareID_txtCode").val(data.data);
                $("#ddlOutWareID_txtCode").attr("readonly", "true");
                $("#ddlOutWareID_txtCode").css("width", "95%");
                $("#headid").val(data.sta);
                $("#hidBillStatus").val('1');
                var d, s = "";
                d = new Date(); //Create Date object. 
                s += d.getYear() + "-";
                s += (d.getMonth() + 1) + "-"; //Get month 
                s += d.getDate() + " "; //Get day 

                $("#txtModifiedDate").val(s);

                fnStatus('2');  //控制按钮状态

                fnChangeNumber(); //保存的数量
                popMsgObj.ShowMsg("保存成功！");
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
            hidePopup();

        }
    });

}

//保存的数量
function fnChangeNumber() {
    var signFrame = findObj("TableCoalInfo", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            var preCount = $("#txtQuantity" + rowid).val();  //原来确认的煤种的数量
            if (preCount == "") {
                preCount = "0.00";
            }
            $("#hidPreQuantity" + rowid).val(preCount);

        }
    }
}


//获取主表信息

function fnGetInfo() {
    var strInfo = '';
    var OutCodeType = document.getElementById("ddlOutWareID_ddlCodeRule").value;
    var OutWareNo = $("#ddlOutWareID_txtCode").val(); //出库单号
    var SourceBillID = $("#txtSourceBillID").val(); //  来源订单ID
    var SourceBillNo = $("#txtSourceBillNo").val(); //  来源订单编码 
    var SettleType = $("#drpSettleType").val(); //结算方式
    var CustomerID = $("#txtCustomerID").val(); //客户编码
    var TransPortType = $("#drpTransPortType").val(); //调运类型
    var OprID = $("#txtOprID").val(); //出库人    
    var OutWareTime = $("#txtOutWareTime").val(); //出库时间  
    var OutNum = $("#txtOutNum").val(); //出库数量 
    var PPersonID = $("#txtPPersonID").val(); //业务员ID   
    var DeptID = $("#hdDeptID").val(); //部门ID  
    var SendNum = $("#txtSendNum").val(); //发运数量
    var TransMoney = $("#txtTransMoney").val(); //运费
    var Remark = $("#txtRemark").val(); // 备注  
    //var WareID=$("#drpWare").val();//仓库ID
    //var CoalID= $("#txtCoalID").val(); //煤种
    //var Quantity=$("#txtQuantity").val(); //数量
    //var SaleCost=$("#txtSaleCost").val();//售价
    //var TaxRate= $("#txtTaxRate").val();//税率
    //var Tax= $("#txtTax").val();//税额
    //var TaxMoney=$("#txtTaxMoney").val();//含税金额
    var TranSportID = $("#txtTranSportID").val(); //调运单ID
    var TranSportNo = $("#txtTranSportID").val(); //调运单编码
    //var TranSportState=$("#txtTranSportID").val();//调运单状态
    var CarNo = $("#txtCarNo").val(); //车次
    var StartStation = $("#txtStartStation").val(); //发站
    var EndStation = $("#txtStartStation").val(); //到站
    var CarNum = $("#txtCarNum").val(); //发车数
    //var UPTranSportState=$("#drpUPTranSportState").val();//更新调运单状态                                                                                                
    strInfo = 'OutWareNo=' + escape(OutWareNo) + '&SourceBillID=' + escape(SourceBillID) + '&SourceBillNo=' + escape(SourceBillNo)
    + '&SettleType=' + escape(SettleType) + '&CustomerID=' + escape(CustomerID) + '&TransPortType=' + escape(TransPortType) + '&OprID=' + escape(OprID) +
    '&OutWareTime=' + escape(OutWareTime) + '&OutNum=' + escape(OutNum) + '&PPersonID=' + escape(PPersonID) + '&DeptID=' + escape(DeptID)
    + '&SendNum=' + escape(SendNum) + '&TransMoney=' + escape(TransMoney) + '&Remark=' + escape(Remark)
    + '&TranSportID=' + escape(TranSportID)
    + '&TranSportNo=' + escape(TranSportNo) + '&CarNo=' + escape(CarNo) + '&StartStation=' + escape(StartStation)
    + '&EndStation=' + escape(EndStation) + '&CarNum=' + escape(CarNum) + '&OutCodeType=' + escape(OutCodeType) + '';

    return strInfo;
}
//获取明细数据

function getDropValues() {


    var SendOrderFit_Item = new Array();
    var signFrame = findObj("TableCoalInfo", document);
    var j = 0;
    IsMore = false;



    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var WareID = $("#drpWare" + rowid).val();           //仓库id   
            var CoalID = $("#drpCoalName" + rowid).val();           //煤种id     
            var Quantity = $("#txtQuantity" + rowid).val();           //数量
            var hidOutBusMxId = $("#hidOutBusMxId" + rowid).val();   //销售发货单明细ID
            var SaleCost = $("#txtSaleCost" + rowid).val();           //销售单价
            var TaxRate = $("#txtTaxRate" + rowid).val();           //税率 
            var Tax = $("#txtTax" + rowid).val();           //税额 
            var Money = $("#txtMoney" + rowid).val();           //金额   




            SendOrderFit_Item[j] = [[WareID], [CoalID], [Quantity], [SaleCost], [TaxRate], [Tax], [Money], [hidOutBusMxId]];

        }

    }


    return SendOrderFit_Item;
}




//获取单据详细信息
function fnGetOrderInfo(orderID) {
    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/MedicineManager/MedicineList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data.length == 1) {
                fnInitPage(data.data[0]); //给页面赋值
                fnGetDetail(data.data[0].OrderNo); //获取报价单明细信息
            }
        }
    });
}

//获取单据详细信息
function BN(orderID) {
    fnDelRow();
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/MedicineManager/MedicineList.ashx",
        data: "action=orderInfo&orderID=" + escape(orderID),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data.length == 1) {
                fnInitPage(data.data[0]); //给页面赋值
                fnGetDetail(data.data[0].OrderNo); //获取报价单明细信息
            }
        }
    });
}

//给页面赋值
function fnInitPage(data) {
    document.getElementById('ddlSellOrderRule').style.visibility = "hidden"; //在浏览内容时 下拉框隐藏
    document.getElementById('txtOrderNo').style.display = "none"; //textBox隐藏
    document.getElementById('txtOrder').innerHTML = data.OrderNo; //显示销售订单号
    $("#txtOrderNo").val(data.OrderNo);        //订单编号
    $("#CustID").val(data.CustName);         //客户ID（关联客户信息表）
    $("#CustID").attr("title", data.CustID);         //客户ID（关联客户信息表）
    $("#hfCustNo").val(data.CustNo);
    $("#txtUcLinkMan").val(data.LinkManName);
    $("#hfLinkmanID").attr("title", data.LinkManID);
    $("#txtTelphone").val(data.HandSet);
    $("#ReceiveAddress").val(data.ReceiveAddress);
    // $("#hfLinkmanID").val(data.CustLinkManID); 
    $("#DiscountAmount").val(data.DiscountAmount); //折后金额
    $("#tell").val(data.tell);
    $("#UserSeller").val(data.SellerName);         //业务员
    $("#Seller").val(data.Seller);         //业务员(对应员工表ID)
    $("#cashier").val(data.CashierName);      //收款员
    $("#TotalPrice").val(data.TotalPrice);     //金额合计
    $("#CountTotal").val(data.CountTotal);     //产品数量合计
    $("#txtorderdate").val(data.OrderDate);      //订单日期
    $("#Remark").val(data.Remark);         //备注
    $("#ReviewerComments").val(data.ReviewerComments);
    $("#billstatus").val(data.BillStatusText);      //单据状态（1制单，2确认，3审核通过，4作废）
    $("#hidBillStatus").val(data.BillStatus);
    if (document.getElementById("hidBillStatus").value != "1") {
        $("#btn_save").css('display', 'none');
        $("#imgUnSave").css('display', '');
    }
    $("#txtstatus").val(data.StatusText); //订单状态（0未处理，1已处理）
    $("#hidStatus").val(data.Status);
    $("#txtsendstatus").val(data.SendStatusText); //发货状态（0未发货，1已发货）  
    $("#hidSendStatus").val(data.SendStatus);

    //------20121227添加gmsDate-----------//
    if (document.getElementById("gmsDate"))
        $("#gmsDate").val(data.gmsDate);
    //---------end-----------------------//

    if (document.getElementById('txtCreatorReal'))
        document.getElementById('txtCreatorReal').innerHTML = data.CreateName;
    if (document.getElementById('txtCreateDate'))
        document.getElementById('txtCreateDate').innerHTML = data.CreateDate;
    if (document.getElementById('txtConfirmor'))
        document.getElementById('txtConfirmor').innerHTML = data.ConfirmName;
    if (document.getElementById('txtConfirmDate'))
        document.getElementById('txtConfirmDate').innerHTML = data.ConfirmDate;
    if (document.getElementById('txtInvalidor'))
        document.getElementById('txtInvalidor').innerHTML = data.InvalidName;
    if (document.getElementById('txtInvalidDate'))
        document.getElementById('txtInvalidDate').innerHTML = data.InvalidDate;
    if (document.getElementById('txtChecker'))
        document.getElementById('txtChecker').innerHTML = data.CheckName;
    if (document.getElementById('txtCheckDate'))
        document.getElementById('txtCheckDate').innerHTML = data.CheckDate;

}
//获取明细信息
function fnGetDetail(ChanceNo) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/MedicineManager/MedicineList.ashx",
        data: "action=detail&orderNo=" + escape(ChanceNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    AddSignRow();
                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value);
                    $("#ProductNo" + rowID).val(item.ProdNo); //商品编号
                    $("#hiddProductNo" + rowID).val(item.ProductNo); //商品编号
                    $("#ProductName" + rowID).val(item.ProductName); //商品名称
                    $("#Specification" + rowID).val(item.Specification); //规格
                    $("#UnitID" + rowID).val(item.UnitName); //单位
                    $("#ProductCount" + rowID).val(item.ProductCount); //数量
                    $("#UnitPrice" + rowID).val(item.UnitPrice); //单价
                    $("#GuidPrice" + rowID).val(item.GuidPrice); //指导价
                    $("#SupplyPrice" + rowID).val(item.SupplyPrice); //供货价
                    $("#DiscountPrice" + rowID).val(item.DiscountPrice); //折扣额
                    $("#LimitSellArea" + rowID).val(item.limitarea); //限销区域
                    $("#TotalPrice" + rowID).val(item.TotalPrice); //金额
                    $("#Remark" + rowID).val(item.Remark);      //备注
                    txtTRLastIndex.value = rowID; //将行号推进下一行




                });
            }
        },
        complete: function() { fnTotalInfo(); } //接收数据完毕
    });
}

//获取保存
function getCountValue() {


    var SendOrderFit_Item = new Array();
    var signFrame = findObj("TableCoalInfo", document);
    var j = 0;


    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;
            var Quantity = $("#hidPreQuantity" + rowid).val();           //获取保存时的数量
            var hidOutBusMxId = $("#hidOutBusMxId" + rowid).val();   //销售发货单明细ID


            SendOrderFit_Item[j] = [[Quantity], [hidOutBusMxId]];

        }

    }


    return SendOrderFit_Item;
}


//确认订单
function Fun_ConfirmOperate() {
    UpdateCount(); //先获取销售发货单明细中未出库的数量

    var signFrame = findObj("TableCoalInfo", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            var unOutNum = $("#txtUnOutNum" + rowid).val();   //目前销售发货单明细中未发货的数量
            var SaveNum = $("#hidPreQuantity" + rowid).val();  //保存时煤种的数量
            if (Number(SaveNum) > Number(unOutNum)) {
                popMsgObj.ShowMsg("第" + rowid + "行煤种数量不能多于" + unOutNum + "，请重新保存！");
                return;
            }

        }
    }


    var c = window.confirm("确定执行确认操作吗？");
    if (c == true) {


        var strfitinfo = getCountValue().join("|");
        var headid = $("#headid").val();

        action = "ConfirmOutWare";



        var strParams = "action=" + action + "&headid=" + headid + "&strfitinfo=" + strfitinfo + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/stockmanage/OutWare_ADD.ashx",
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
                    UpdateCount();  //更新页面的未出库数量


                }
                else {
                    popMsgObj.ShowMsg(data.info);
                }
                hidePopup();
            }
        });

    }
}


//保存的数量
function UpdateCount() {
    var ID = $("#txtSourceBillID").val(); //获取发货单id

    //////////////////////////////////////////////////
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Common/WareInfo.ashx', //目标地址
        cache: false,
        data: 'action=getOutCount&ID=' + escape(ID), //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            $.each(msg.data, function(i, item) {
                var signFrame = findObj("TableCoalInfo", document);
                var iCount = 0; //明细中数据数目
                for (i = 1; i < signFrame.rows.length; i++) {
                    if (signFrame.rows[i].style.display != "none") {
                        var rowid = signFrame.rows[i].id;
                        var mxId = $("#hidOutBusMxId" + rowid).val();
                        if (mxId == item.id) {
                            $("#txtUnOutNum" + rowid).val(item.ProductCount);
                            break;
                        }


                    }
                }

            });

        },
        complete: function() { } //接收数据完毕
    });

    //////////////////////////////////////
}

//反确认
function cancelConfirm() {

    var c = window.confirm("确定执行取消确认操作吗？");
    if (c == true) {
        var strfitinfo = getCountValue().join("|");
        var headid = $("#headid").val();

        action = "CancelConfirmOutWare";


        var strParams = "action=" + action + "&headid=" + headid + "&strfitinfo=" + strfitinfo + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/stockmanage/OutWare_ADD.ashx",
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
                    UpdateCount();  //更新页面的未出库数量

                }
                else {
                    popMsgObj.ShowMsg('取消确认失败！');
                }
                hidePopup();
            }
        });

    }
}

//查看库存量
function showCurrentStock(prodno) {
    document.getElementById("divCurrentStock").style.display = "block";
    document.getElementById('divzhezhao2').style.display = 'inline';
    AlertProductMsg();
    fnGetCurrentStockByNo(prodno); //获取物品库存量
}
//打印功能---2012-08-10 添加 8-14 修改 edit by dyg
function DoPrint() {
    orderNo = $("#txtOrderNo").val()
    //  alert(orderNo);
    if (document.getElementById("txtOprtID").value == "") {
        popMsgObj.ShowMsg("请保存后打印");
        return;
    }
    else {
        window.open("../../PrinttingModel/MedicineManager/orderForPrint3.aspx?id=" + document.getElementById("hiddOrderID").value);
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