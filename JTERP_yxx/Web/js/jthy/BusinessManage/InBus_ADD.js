var isnew = "1"; //1添加;2保存
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
var isconfirm = "0"; //0为保存，1为确认
var billstatus = "1"; //1制单，2确认，3变更
$(document).ready(function() {
    requestobj = GetRequest();    
    TransPortTypewitchs();
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        document.getElementById("hiddOrderID").value = document.getElementById("headid").value;
        GetInfoById(document.getElementById("headid").value);
        fnGetDetail(document.getElementById("headid").value);
        $("#labTitle_Write1").html("采购到货单查看");        
    }
    else {
        $("#labTitle_Write1").html("采购到货单新建");
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
    var action = "SearchInBusOne";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/BusinessManage/InBusInfo.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {
                    isnew = "2";
                    document.getElementById("divArriveCode").innerHTML = item.ArriveNo;
                    $("#divArriveCode").css("display", "block");
                    $("#divCodeRule").css("display", "none");
                    $("#ddlArriveCode_ddlCodeRule").val(item.ArriveNo);

                    //$("#txtArriveBillNo").val(item.ArriveNo);
                    $("#txtSourceBillID").val(item.SourceBillID);
                    $("#txtSourceBillNo").val(item.SourceBillNo);
                    $("#drpSettleType").val(item.PayType);
                    $("#txtProviderID").val(item.providerid);
                    $("#txtProviderName").val(item.providerName);
                    $("#txtLinkMan").val(item.ContactName);
                    var Freight = Number(item.Freight).toFixed(2);
                    $("#txtFreight").val(Freight);  //运费
                    $("#txtSendTime").val(item.SendTime);
                    $("#txtSendNum").val(item.SendNum); //发运数量
                    $("#txtGetNum").val(item.GetNum);     //实收吨数
                    $("#txtResidueNum").val(item.SendNum-item.GetNum);     //剩余吨数
                    
                    var TotalFee = Number(item.TotalFee).toFixed(2);
                    if (TotalFee)
                        $("#txtSumMoney").val(TotalFee);  //总金额
                    else
                        $("#txtSumMoney").val("0.00");
                    $("#txtPPersonID").val(item.PPersonID);
                    $("#txtPPerson").val(item.PPerson);
                    $("#DeptName").val(item.DeptName);
                    $("#hdDeptID").val(item.DeptID);
                    $("#drpTransPortType").val(item.transporttype);
                    $("#txtTranSportID").val(item.TransPortNo);
                    $("#txtTranSportNo").val(item.DiaoyunNO);
                    $("#txtTranSportState").val(item.transstate);
                    $("#txtCarNo").val(item.motorCade);
                    $("#txtStartStation").val(item.ship_place);
                    $("#txtEndStation").val(item.to_place);
                    $("#txtCarNum").val(item.CarNum); //发车数
                    $("#drpUPTranSportState").val(item.at_state);
                    $("#txt_CreateDate").val(item.CreateDate);
                    $("#UserPrincipal").val(item.CreatorName);
                    $("#txtModifiedDate").val(item.ModifiedDate);
                    $("#txtModifiedUserID").val(item.ModifiedUserID);
                    $("#txtServiceId").val(item.ServicesId);
                    $("#txtServicesName").val(item.ServicesName);
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
                    TransPortTypewitchs();
                }
            });
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}

//获取明细信息
function fnGetDetail(headid) {
    var action = "SearchInBusDetail";
    var ary = new Array();
    var rowsCount = 0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/BusinessManage/InBusInfo.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&headid=" + escape(headid) + '',
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    rowsCount++;
                    FillSignRow(i, item);
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
            $.each(msg.data, function(i, item) {
                $("#txtSpecialName" + rowid).val(item.HeatPower); //质量热卡
                $("#txtUnitID" + rowid).val(item.unitid); //计量单位
                $("#txtUnitName" + rowid).val(item.unitname); //计量单位 
                $("#drpWare" + rowid).val(item.storageId);   //仓库
            });

        },
        complete: function() { } //接收数据完毕
    });

}

//
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

// 从列表中打开单据时，填充明细信息
function FillSignRow(i, item) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    //  var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    // var rowID = parseInt(txtTRLastIndex.value) + 1;
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='" + item.id + "' >";
    m++;

    //加载仓库
    var newFitNametd = newTR.insertCell(m); //添加列:仓库
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    "<select name='drpWare" + rowID + "' class='tddropdlist'  runat='server' id='drpWare" + rowID + "' ></select></td></tr></table>"; //添加列内容
    m++;

    getWareData('drpWare' + rowID, item.StorageID); //加载仓库数据

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<input type='hidden' id='drpCoalType" + rowID + "'  value='" + item.ProductID + "'><input id='txtCoalName" + rowID + "' value='" + item.ProductName + "'  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
   
    //newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    //"<select name='drpCoalType" + rowID + "' disabled='disabled' class='tddropdlist'  runat='server' id='drpCoalType" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    m++;

    //getCoalData('drpCoalType' + rowID, item.ProductID); //加载煤种数据

    var newFitDesctd = newTR.insertCell(m); //添加列:热量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSpecialName" + rowID + "' value=\"" + item.HeatPower + "\"  maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input type='hidden' id='txtUnitID" + rowID + "' value=\"" + item.UnitID + "\" ><input  id='txtUnitName" + rowID + "' value=\"" + item.UnitName + "\"   style=' width:95%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "' value=\"" + item.ProductCount + "\" onpropertychange=\"getMoney(" + rowID + ");\"  onkeyup='return ValidateNumber(this,value)'   type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnitCost" + rowID + "' value=\"" + Number(item.TaxPrice).toFixed(2) + "\" onpropertychange=\"getMoney(" + rowID + ");\"  onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:金额
    newFitDesctd.className = "cell";
    var TotalFee = Number(item.TotalFee).toFixed(2);
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value=\"" + TotalFee + "\" onpropertychange=\"getTotalMoney();\" onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:税率
    newFitDesctd.className = "cell";
    var TaxRate = Number(item.TaxRate).toFixed(0);
    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' value=\"" + TaxRate + "\"  maxlength='10' type='text' value='17' onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:是否质检
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<select style='width:95%' id='txtISQTest" + rowID + "'><option value='1'>是</option><option value='0'>否</option></select>"; //添加列内容
    m++;

    $("#txtISQTest" + rowID).val(item.ISQTest);


    var newFitDesctd = newTR.insertCell(m); //添加列:已报检
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComQTest" + rowID + "'  value=\"" + item.CheckedCount + "\"     type='text' class='tdinput'  disabled='disabled'    style='width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:已入库
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComInWare" + rowID + "'  value=\"" + item.InCount + "\"    type='text' class='tdinput'  disabled='disabled'   style='width:90%;'/>"; //添加列内容
    m++;
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

//获取金额
function getMoney(rowid) {

    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var SaleCost = $("#txtUnitCost" + rowid).val();  //销售单价
    var imoney = (Number(quantity) * Number(SaleCost)).toFixed(2);  //金额
    document.getElementById("txtMoney" + rowid).value = Number(imoney).toFixed(2); 
}
//获取总金额
function getTotalMoney() {
    var summoney = 0;
    for (var i = 0; i < 100; i++) {
        if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
            summoney = summoney + Number(document.getElementById("txtMoney" + i).value);
        }
    }
    document.getElementById("txtSumMoney").value = Number(summoney).toFixed(2); ;
}


function SaveSellOrder() {
    var bmgz = "";
    var Flag = true;

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var i=0;
    var iCount = 0; //明细中数据数目    

    if ($("#ddlArriveCode_txtCode").val() == "" && $("#ddlArriveCode_ddlCodeRule").val() == "") {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "请输入单据编号|";
    }

    if (document.getElementById("txtSourceBillNo").value == "") {
       isFlag = false;
        fieldText = fieldText + "来源单据|";
        msgText = msgText + "来源到货单号不能为空|";
    }
    
//    if ($("#drpTransPortType").val()==10 && $("#txtTranSportNo").val() == "") {
//        isFlag = false;
//        fieldText = fieldText + "调运类型|";
//        msgText = msgText + "调运信息不能为空|";
//    }
    
    if ($("#drpTransPortType").val()==20 && $("#txtServiceId").val() == "") {
        isFlag = false;
        fieldText = fieldText + "来源信息|";
        msgText = msgText + "服务商信息不能为空|";
    }
    
    //明细验证
    var signFrame = findObj("dg_Log", document);
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            iCount = iCount + 1;
        }  
    }
    if (iCount == 0){
        isFlag = false;
        fieldText = fieldText + "明细信息|";
        msgText = msgText + "无明细数据|";
    }
    
    for (i = 1; i < signFrame.rows.length; i++) {
      if (signFrame.rows[i].style.display != "none") {
       var Quantity = document.getElementById("txtQuantity" + i).value;
       if (parseInt(Quantity) == 0 || Quantity=="") {
         isFlag = false;
         fieldText = fieldText + "明细信息|";
         msgText = msgText + "第"+i+"行数量不能为空|";
         }
         
         var UnitCost = document.getElementById("txtUnitCost" + i).value;

         if (parseInt(UnitCost) == 0 || UnitCost=="") {
          isFlag = false;
          fieldText = fieldText + "明细信息|";
          msgText = msgText + "第"+i+"行单价不能为空|";
         }
        
        if (document.getElementById("drpCoalType" + i).value == "") {
          isFlag = false;
          fieldText = fieldText + "明细信息|";
          msgText = msgText + "第"+i+"行煤炭类型不能为空|";
         } 
       }
    }
    
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else
    { 
      var strfitinfo = getDropValue().join("|");  //获取明细信息
      var strInfo = fnGetInfo();// 获取主表信息
      var headid = document.getElementById("headid").value;
      if (headid == "undefined" || headid == "null" || headid == "") {
          headid = "0";
      }

      $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/BusinessManage/DealInBus_ADD.ashx",
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
                $("#headid").val(data.sta);
                isnew = "2"; //更新状态
                document.getElementById("divArriveCode").innerHTML = data.data;
                $("#divArriveCode").css("display", "block");
                $("#divCodeRule").css("display", "none");

                $("#hidBillStatus").val('1');
                $("#txtBillStatusID").val('2');
                fnStatus($("#txtBillStatusID").val());  //控制按钮属性
                var d, s = "";
                d = new Date(); //Create Date object. 
                s += d.getYear() + "-";
                s += (d.getMonth() + 1) + "-"; //Get month 
                s += d.getDate() + " "; //Get day 

                $("#txtModifiedDate").val(s);

                popMsgObj.ShowMsg("保存成功！");
              }
              else {
                popMsgObj.ShowMsg(data.info);
              }
            hidePopup();            
          }
        });      
      }
}

//获取主表信息
function fnGetInfo() {
    var strInfo = '';

    var ArriveType = $("#ddlArriveCode_ddlCodeRule").val();  //编号类型
    var ArriveBillNo = "";   //单据编号
    if (ArriveType == "") {
        ArriveBillNo = $("#ddlArriveCode_txtCode").val();  //手动输入时，获取单据编号    
    }

    if (isnew == "2") {        //如果是更新
        ArriveBillNo = $("#divArriveCode").text();
    }

    //var ArriveBillNo=$("#txtArriveBillNo").val();// 到货单编号
    var SourceBillID = $("#txtSourceBillID").val(); // 来源订单ID
    var SourceBillNo = $("#txtSourceBillNo").val(); // 来源订单编号
    var SettleType = $("#drpSettleType").val(); // 结算方式
    var ProviderID = $("#txtProviderID").val(); // 供应商ID
    var ProviderName = $("#txtProviderName").val(); // 供应商名称
    var LinkMan = $("#txtLinkMan").val(); // 联系人
    var SendTime = $("#txtSendTime").val(); // 发运时间
    var SendNum = $("#txtSendNum").val(); // 发运数量
    var SumMoney = $("#txtSumMoney").val(); // 总金额
    var PPersonID = $("#txtPPersonID").val(); // 采购员ID
    var PPerson = $("#txtPPerson").val(); // 采购员名称
    var DeptID = $("#hdDeptID").val(); // 部门编码
    var DeptName = $("#DeptName").val(); // 部门名称
    var TransPortType = $("#drpTransPortType").val(); // 调运类型
    var ServicesId = document.getElementById("txtServiceId").value;// $("txtServicesId").val();

    var TranSportID = $("#txtTranSportID").val(); // 调运单ID
    var TranSportNo = $("#txtTranSportNo").val(); // 调运单号
    var TranSportState = $("#txtTranSportState").val(); // 当前调运状态
    var CarNo = $("#txtCarNo").val(); // 车次
    var StartStation = $("#txtStartStation").val(); // 发站
    var EndStation = $("#txtEndStation").val(); // 到站
    var CarNum = $("#txtCarNum").val(); // 发车数
    var UPTranSportState = $("#drpUPTranSportState").val(); // 更新调运单状态
    var Freight = $("#txtFreight").val();

    strInfo = 'ArriveBillNo=' + escape(ArriveBillNo) + '&ArriveType=' + escape(ArriveType) + '&SourceBillID=' + escape(SourceBillID) + '&SourceBillNo=' + escape(SourceBillNo)
    + '&SettleType=' + escape(SettleType) + '&ProviderID=' + escape(ProviderID) + '&ProviderName=' + escape(ProviderName) + '&LinkMan=' + escape(LinkMan) +
    '&SendTime=' + escape(SendTime) + '&SendNum=' + escape(SendNum) + '&SumMoney=' + escape(SumMoney) + '&PPersonID=' + escape(PPersonID) + '&PPerson=' + escape(PPerson) +
    '&DeptID=' + escape(DeptID) + '&DeptName=' + escape(DeptName) + '&TransPortType=' + escape(TransPortType) + '&TranSportID=' + escape(TranSportID)
    + '&TranSportNo=' + escape(TranSportNo) + '&TranSportState=' + escape(TranSportState) + '&CarNo=' + escape(CarNo) + '&StartStation=' + escape(StartStation)
    + '&EndStation=' + escape(EndStation) + '&CarNum=' + escape(CarNum) + '&Freight=' + escape(Freight) + '&UPTranSportState=' + escape(UPTranSportState) + '&ServicesId='+escape(ServicesId) + '';

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

            var Ware = $("#drpWare" + rowid).val(); //仓库
            var CoalType = $("#drpCoalType" + rowid).val(); //煤种
            var Quantity = $("#txtQuantity" + rowid).val(); //数量
            var UnitCost = $("#txtUnitCost" + rowid).val(); //单价
            var Money = $("#txtMoney" + rowid).val(); //金额
            var TaxRate = $("#txtTaxRate" + rowid).val(); //税率
            var ISQTest = $("#txtISQTest" + rowid).val(); //是否质检  1-是  0-否


            SendOrderFit_Item[j] = [[Ware], [CoalType], [Quantity], [UnitCost], [Money], [TaxRate], [ISQTest]];

        }

    }

    return SendOrderFit_Item;
}

//确认
function Fun_ConfirmOperate() {
    var c = window.confirm("确定执行确认操作吗？");
    if (c == true) {

        var headid = $("#headid").val();

        action = "ConfirmInBus";
        var strParams = "action=" + action + "&headid=" + headid + '&billtype=2' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/BusinessManage/DealInBus_ADD.ashx",
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
                hidePopup();
            }
        });

    }

}

//执行取消确认
function cancelConfirm() {

    var c = window.confirm("确定执行取消确认操作吗？");
    if (c == true) {


        var headid = $("#headid").val();

        action = "CancelConfirmInBus";


        var strParams = "action=" + action + "&headid=" + headid + '&billtype=2' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/BusinessManage/DealInBus_ADD.ashx",
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
                hidePopup();
            }
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

// 修改调运单状态
function TranStateMod() {
    var TranSportNo = document.getElementById("txtTranSportNo").value;  //调运单编号
    var TranSportState = $("#txtTranSportState").val();   //调运单状态
    var tranId = $("#txtTranSportID").val(); //调运单的id
    if (TranSportNo == "") {
        alert("请先添加调运");
        return;
    }
    ChangeStatus(TranSportNo, TranSportState, tranId);
}

function GetValuee(statuu) {
    $("#txtTranSportState").val(statuu);
}

//根据不同的运输类型，确认需要显示的内容 
// 10为火运， 20 汽运 30 客户自提
function TransPortTypewitchs()
 {
   var tranport=$("#drpTransPortType").val();
  
   if(tranport=="10")
   {
    $("#Tables").show()
        $("#TableBJ1").show();         
        $("#company").css("display","none");
   }
  
  else  if(tranport=="20"){
    
         $("#Tables").hide();
         $("#TableBJ1").hide();
         $("#company").css("display","");
        
    }
     else if(tranport=="30"){
     
      $("#Tables").hide();
      $("#TableBJ1").hide();
      $("#company").css("display","none");       
     
     } 
 }