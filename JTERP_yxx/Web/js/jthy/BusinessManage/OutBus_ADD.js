var isnew = "1"; //1添加;2保存
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
var orderBy = "id_a";
var typeflag = "";
var isconfirm = "";

$(document).ready(function () {
    //debugger;
    requestobj = GetRequest();
    TransPortTypewitchs();
    document.getElementById("headid").value = requestobj['intMasterID'];
    //根据传来的参数确定是   0.库存销售    1.采购直销
    typeflag = requestobj['typeflag']; //判断使用什么样式
    initpage(typeflag); //页面初始化
    //初始化页面
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        GetInfoById(document.getElementById("headid").value);
        GetDetailById(document.getElementById("headid").value);
        if (typeflag == "0") {
            $("#labTitle_Write1").html("出库销售单查看");
        }
        else {
            $("#labTitle_Write1").html("采购直销单查看");
        }
    }
    else {
        if (typeflag == "0") {
            $("#labTitle_Write1").html("出库销售单新建");

        }
        else {
            $("#labTitle_Write1").html("采购直销单新建");
        }
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

// 根据传入参数 0 库存销售， 1 采购直销
function initpage(typeflag) {
    if (typeflag == "0")//普通销售
    {
        document.getElementById("PurProperty1").style.display = "none"; // 主表明细信息
        document.getElementById("PurProperty2").style.display = "none"; // 进货单价
        document.getElementById("PurProperty4").style.display = "none"; // 购货金额
        $(".PurProperty3").css("display", "none");
        $(".PurProperty5").css("display", "none");
        document.getElementById("drpBusiType").value = "1";
        $("#wareName").css("width", "8%");
        $("#colName").css("width", "8%");
        $("#colNo").css("width", "7%");
    }        
    if (typeflag == "1")//采购直销
    {
        document.getElementById("wareName").style.display = "none"; 
        document.getElementById("drpBusiType").value = "2";
        $(".wareName").css("display", "none");
        $("#wareName").css("width", "8%");
        $("#colName").css("width", "8%");
        $("#colNo").css("width", "8%");  
    }
}

function GetInfoById(headid) {  
    var action = "SearchOutBusOne";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/BusinessManage/OutBusInfo.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&typeflag=" + typeflag + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {
                    isnew = "2";
                    $("#ddlSendNo_ddlCodeRule").css("display", "none");
                    $("#ddlSendNo_txtCode").val(item.SendNo);
                    $("#ddlSendNo_txtCode").attr("readonly", "true");
                    $("#ddlSendNo_txtCode").css("width", "95%");
                    $("#txtSourceBillID").val(item.FromBillID);
                    $("#txtSourceBillNo").val(item.Contractid);
                    $("#drpSettleType").val(item.PayType);
                    $("#txtCustomerID").val(item.CustID);
                    $("#txtCustomerName").val(item.CustName);
                    $("#txtInvoiceUnit").val(item.BillUnit);
                    $("#drpTransPortType").val(item.CarryType);
                    $("#txtSendTime").val(item.ship_time);
                    $("#txtSendNum").val(item.SendNum);
                    
                    $("#txtGetNum").val(item.GetNum);     //实收吨数
                    $("#txtResidueNum").val(item.SendNum-item.GetNum);     //剩余吨数
                    
                    $("#txtSumMoney").val(Number(item.TotalFee).toFixed(2));  //总金额
                    $("#txtPPersonID").val(item.Seller);
                    $("#txtPPerson").val(item.EmployeeName);
                    $("#DeptName").val(item.DeptName);
                    $("#hdDeptID").val(item.SellDeptid);
                    $("#txtTransportFee").val(Number(item.TransportFee).toFixed(2));  //运费
                    $("#txtRemark").val(item.Remark);
                    $("#txtTranSportID").val(item.Transporter);
                    $("#txtTranSportNo").val(item.DiaoyunNO);
                    $("#txtTranSportState").val(item.transstate);
                    $("#txtCarNo").val(item.CarNo);
                    $("#txtStartStation").val(item.StartStation);
                    $("#txtEndStation").val(item.EndStation);
                    $("#txtCarNum").val(item.CarNum);
                    $("#drpUPTranSportState").val(item.at_state);
                    $("#txt_CreateDate").val(item.CreateDate);
                    $("#UserPrincipal").val(item.CreatorName);
                    $("#txtModifiedDate").val(item.ModifiedDate);
                    $("#txtModifiedUserID").val(item.ModifiedUserID);                    
                    $("#txtServiceID").val(item.ServicesId);
                    $("#txtcompany").val(item.ServicesName);                    
                    TransPortTypewitchs();
                    if (typeflag == "1")//采购直销
                    {
                        $("#txtPurContractID").val(item.PurContractID);
                        $("#txtPurContractNo").val(item.PurContractNo);
                        $("#txtProviderID").val(item.ProviderID);
                        $("#txtProviderName").val(item.ProviderName);
                        $("#txtSupplyAmount").val(Number(item.SupplyAmount).toFixed(2)); //供货金额
                    }
                }
                if (item.billStatus == '2') //如果是确认状态
                {
                    $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                    $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                    $("#txtConfirmDate").val(item.ConfirmDate);  //确认日期
                    isconfirm = "1";
                    fnStatus('3');   //按钮状态
                }
                else {
                    isconfirm = "0";
                    fnStatus('2');   //按钮状态
                }
            });

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}
function GetDetailById(headid) {
    var action = "SearchOutBusDetail";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/BusinessManage/OutBusInfo.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&typeflag=" + typeflag + "&headid=" + escape(headid) + '',
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

// 所有明细数据显示，均通过本函数处理
function FillSignRow(i, item) {
    var txtTRLastIndex = document.getElementById("txtTRLastIndex");
    var ProductCount;
    if (document.getElementById("headid").value == "" || document.getElementById("headid").value == "undefined") {
        ProductCount = 0;
    } else {
        ProductCount = item.ProductCount;
    }
    var rowID = parseInt(i) + 1;
    //var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='' >";
    m++;

    //加载仓库
    var newFitNametd = newTR.insertCell(m); //添加列:仓库
    //newFitNametd.className = "cell";
    newFitNametd.className = "wareName"; 
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%' style='background-color:White;'><tr><td>" +
    "<select name='drpWare" + rowID + "' class='tddropdlist' style='width: 95%;'  runat='server' id='drpWare" + rowID + "' ></select></td></tr></table>"; //添加列内容
    //newFitNametd.innerHTML = "<select name='drpWare" + rowID + "'class='tdinput' style='width: 95%;'  runat='server' id='drpWare" + rowID + "' ></select>";
    m++;
    getWareData('drpWare' + rowID, item.StorageID); //加载仓库数据

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    "<input id='txtCoalName"+rowID+"' value='" + item.ProductName +"' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> <input id='IdCoalName"+rowID+"' value='"+item.ProductID+ "' type='hidden'>";
    //"<select name='drpCoalName" + rowID + "' disabled='disabled' class='tddropdlist' style='width: 95%;'  runat='server' id='drpCoalName" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    m++;
    //getCoalData('drpCoalName' + rowID, item.ProductID); //加载煤种数据

    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnit" + rowID + "' value=\"" + item.unitName + "\"  maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "' value=\"" + ProductCount + "\"   onblur=\"getMoney(" + rowID + ",1);\"   onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:进货单价
    newFitDesctd.className = "PurProperty3";
    newFitDesctd.innerHTML = "<input id='txtInCost" + rowID + "' value=\"" + item.InCost + "\"   onpropertychange=\"getMoney(" + rowID + ",9);\"   onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:99%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:销售单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSaleCost" + rowID + "' value=\"" + Number(item.TaxPrice).toFixed(4) + "\"   onblur=\"getMoney(" + rowID + ",5);\"  onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:税率
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' value=\"" + Number(item.TaxRate).toFixed(4) + "\"  onblur=\"getMoney(" + rowID + ",7);\"   maxlength='10' type='text'  onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    if (item.TotalTax == "" || item.TotalTax == "undefined") {
        var quantity = $("#txtQuantity" + rowID).val(); //数量
        var SaleCost = $("#txtSaleCost" + rowID).val();  //销售单价
        var imoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //金额
        var taxRate = Number($("#txtTaxRate" + rowID).val()) / 100; //税率
        var disTaxMoney = (Number(imoney) / (1 + taxRate)).toFixed(2); //除税金额
        item.TotalTax = Number(imoney - disTaxMoney).toFixed(2);  //税额
    }

    var newFitDesctd = newTR.insertCell(m); //添加列:税额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTax" + rowID + "' value=\"" + Number(item.TotalTax).toFixed(2) + "\"  maxlength='10' type='text' onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:购货金额
    newFitDesctd.className = "PurProperty5";
    newFitDesctd.innerHTML = "<input id='txtPurMoney" + rowID + "' value=\"" + Number(item.TotalInCost).toFixed(2) + "\" onblur=\"getMoney(" + rowID + ",17);\"   onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:99%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:销货金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value=\"" + Number(item.TotalFee).toFixed(2) + "\" onblur=\"getMoney(" + rowID + ",13);\"   onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

//    var newFitDesctd = newTR.insertCell(m); //添加列:已出库数量
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtComOutNum" + rowID + "' value=\"" + item.OutCount + "\"    type='text' class='tdinput'    disabled='disabled'    style='width:90%; '/>"; //添加列内容
//    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:已出库数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComSettleNum" + rowID + "' value=\"" + item.SttlCount + "\"    type='text' class='tdinput'    disabled='disabled'    style='width:90%;'/>"; //添加列内容
    m++;
    initpage(typeflag);
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

//根据输入数据或单位或总金额，进行相互转换
// rowid 行号， typeflag 传入类型 1 数量 5 销售单价 7 税率 9 采购单价 13 销售总价  17 采购总价
function getMoney(rowid,typeflag) {    
    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var SaleCost = $("#txtSaleCost" + rowid).val();  //销售单价
    var PurCost = $("#txtInCost" + rowid).val();  //采购单价
    var SaleImoney="";var PurImoney="";var taxRate="";var taxMoney="";var disTaxMoney="";var InCost="";
    var signFrame = findObj("dg_Log", document);  // 计算总行数
    var i=1;
    var summoney = 0;
    var sumPurMoney = 0; 
    
    switch (typeflag){
    case 1:
      SaleImoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //销售金额
      PurImoney = (Number(quantity) * Number(PurCost)).toFixed(4); //采购总金额
      document.getElementById("txtMoney" + rowid).value = Number(SaleImoney).toFixed(2);
      taxRate = Number($("#txtTaxRate" + rowid).val()) / 100; //税率    
      disTaxMoney = (Number(SaleImoney) / (1 + taxRate)).toFixed(2); //除税金额
      taxMoney = Number(SaleImoney - disTaxMoney).toFixed(2);  //税额
      $("#txtTax" + rowid).val(taxMoney); 
      document.getElementById("txtPurMoney" + rowid).value = Number(PurImoney).toFixed(2);
        
    break;
    case 5:
      SaleImoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //销售金额
      PurImoney = (Number(quantity) * Number(PurCost)).toFixed(4);
      document.getElementById("txtMoney" + rowid).value = Number(SaleImoney).toFixed(2);
      taxRate = Number($("#txtTaxRate" + rowid).val()) / 100; //税率    
      disTaxMoney = (Number(SaleImoney) / (1 + taxRate)).toFixed(2); //除税金额
      taxMoney = Number(SaleImoney - disTaxMoney).toFixed(2);  //税额
      $("#txtTax" + rowid).val(taxMoney);
      
    break;
    case 7:
    break;
    case 9:
      PurImoney = (Number(quantity) * Number(PurCost)).toFixed(4);  //采购总金额
      document.getElementById("txtPurMoney" + rowid).value = Number(PurImoney).toFixed(2);
      
    break;
    case 13:// 销货总金额  
      SaleImoney = $("#txtMoney" + rowid).val();
      taxRate = Number($("#txtTaxRate" + rowid).val()) / 100; //税率
        SaleCost= Number((SaleImoney/quantity)*10000).toFixed(4)/10000; 
        taxMoney = SaleImoney-SaleImoney/(1+taxRate);
       $("#txtSaleCost" + rowid).val(SaleCost);        
       $("#txtTax" + rowid).val(taxMoney);        
    break;
    case 17:        
      PurImoney = $("#txtPurMoney" + rowid).val();
      PurCost = Number((PurImoney/quantity)*10000).toFixed(4)/10000; 
      $("#txtInCost" + rowid).val(PurCost);     
    break;
    }
    
    for ( i = 1; i <= signFrame.rows.length; i++) {
        if (document.getElementById("txtPurMoney" + i) != "undefined" && document.getElementById("txtPurMoney" + i) != null) {            
            sumPurMoney = sumPurMoney + Number(document.getElementById("txtPurMoney" + i).value);
        }
    }
    document.getElementById("txtSupplyAmount").value = Number(sumPurMoney).toFixed(2); 
    
    for ( i = 1; i <= signFrame.rows.length; i++) {
      if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
          summoney = summoney + Number(document.getElementById("txtMoney" + i).value);           
         }
       }
    document.getElementById("txtSumMoney").value = Number(summoney).toFixed(2); 
    
    
}


//计算供货金额
// rowid 行号， typeflag 传入类型 1数量 5 单价 9 总价
function getMoney_Pur(rowid) {
    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var InCost = $("#txtInCost" + rowid).val();  //销售单价
    var imoney = (Number(quantity) * Number(InCost)).toFixed(4);  //金额
    document.getElementById("txtPurMoney" + rowid).value = Number(imoney).toFixed(2);   
}

//获取总金额
function getTotalMoney(rowid,typeflag) {


// var quantity = $("#txtQuantity" + rowid).val(); //数量
//    var SaleCost = $("#txtSaleCost" + rowid).val();  //销售单价
//    var PurCost = $("#txtInCost" + rowid).val();  //采购单价
//    var SaleImoney="";var PurImoney="";var taxRate="";var taxMoney="";var disTaxMoney="";var InCost="";
//    var signFrame = findObj("dg_Log", document);  // 计算总行数
//    var i=1;
//    var summoney = 0;
//    var sumPurMoney = 0;    
//   
//    for (var i = 1;  i <= signFrame.rows.length; i++) {
//        if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
//            summoney = summoney + Number(document.getElementById("txtMoney" + i).value);           
//        }
//    }    
//    document.getElementById("txtSumMoney").value = Number(summoney).toFixed(2);
//    
//     SaleImoney = $("#txtMoney" + rowid).val();
//       SaleCost = Number((SaleImoney/quantity)*10000).toFixed(4)/10000;       
//       document.getElementById("txtSaleCost" + rowid).value = SaleCost;  
//       

//    for (var i = 1; i <= signFrame.rows.length; i++) {
//        if (document.getElementById("txtPurMoney" + i) != "undefined" && document.getElementById("txtPurMoney" + i) != null) {            
//            sumPurMoney = sumPurMoney + Number(document.getElementById("txtPurMoney" + i).value);
//        }
//    }
//    document.getElementById("txtSupplyAmount").value = Number(sumPurMoney).toFixed(2);    
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
//判断物品在明细中添加是否重复
function IsExistCheck(prodNo) {
    var sign = false;
    var signFrame = findObj("dg_Log", document);
    var DetailLength = 0; //明细长度
    if ((typeof (signFrame) != "undefined") && (signFrame != null)) {
        for (i = 1; i < signFrame.rows.length; i++) {
            var prodNo1 = document.getElementById("txtProNo" + i).value.Trim();
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

//选择客户
function fnSelectCustInfo() {
    popSellCustObj.ShowList('protion');
}
//选择T6部门
function fnSelectDept() {
    popSellDeptObj.ShowList('protion');
}


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


function SaveSellOrder() {
     //debugger;
    var isconfirm = "";    
    var strAction = "";
    var bmgz = "";
    var Flag = true;

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var i=0;
    var iCount = 0; //明细中数据数目
    isconfirm = "0";

    $("#hidBillStatus").val('1');
    //--------------20121118-------------//
    $("#Imgbtn_confirm").css("display", "inline");
    $("#btn_confirm").css("display", "none");
    //----------------------------------------//      

    if ($("#ddlSendNo_ddlCodeRule").val() == "" && $("#ddlSendNo_txtCode").val() == "") {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "请输入单据编号|";
    }
    
    if ($("#txtSourceBillNo").val() == "" ) {
        isFlag = false;
        fieldText = fieldText + "源单来源|";
        msgText = msgText + "请选择来源销售合同|";
    }
    
    if ($("#txtPurContractNo").val() == "" && typeflag == "1") {
        isFlag = false;
        fieldText = fieldText + "源单来源|";
        msgText = msgText + "请选择来源采购合同|";
    }
//    if ($("#drpTransPortType").val() == 10 && $("#txtTranSportNo").val() == "") {
//        isFlag = false;
//        fieldText = fieldText + "调运类型|";
//        msgText = msgText + "调运信息不能为空|";
//    }
    
    if ($("#txtTranSportNo").val() == "" && $("#txtTranSportNo").val()=="10" ) {
        isFlag = false;
        fieldText = fieldText + "源单来源|";
        msgText = msgText + "请选择调运单据|";
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
        if (parseInt(Quantity) == 0 || Quantity == "") {
         isFlag = false;
         fieldText = fieldText + "明细信息|";
         msgText = msgText + "第"+i+"行数量不能为空|";
         }
        
        if(document.getElementById("PurProperty2").style.display !="none"){
            var InCost = document.getElementById("txtInCost" + i).value;
            if (parseInt(InCost) == 0 || InCost == "") {
                isFlag = false;
                fieldText = fieldText + "明细信息|";
                msgText = msgText + "第" + i + "行销售单价不能为空|";
            }
        }
        

         var UnitCost = document.getElementById("txtSaleCost" + i).value;
         if (parseInt(UnitCost) == 0 || UnitCost == "") {
          isFlag = false;
          fieldText = fieldText + "明细信息|";
          msgText = msgText + "第"+i+"行单价不能为空|";
         }
        
        if (document.getElementById("txtCoalName" + i).value == "") {
          isFlag = false;
          fieldText = fieldText + "明细信息|";
          msgText = msgText + "第"+i+"行煤炭类型不能为空|";
         } 
       }
    }
        
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else { 
      var strfitinfo = getDropValue().join("|");
      var strInfo = fnGetInfo();//其作用

      var headid = document.getElementById("headid").value;
      if (headid == "undefined" || headid == "null" || headid == "") {
          headid = "0";
      }

      $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/BusinessManage/DealOutBus_ADD.ashx",
        data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=insert&billtype=' + escape(typeflag) + '&headid=' + escape(headid) + '&isconfirm=' + escape(isconfirm),
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
                $("#ddlSendNo_ddlCodeRule").css("display", "none");
                $("#ddlSendNo_txtCode").val(data.data);
                $("#ddlSendNo_txtCode").attr("readonly", "true");
                $("#ddlSendNo_txtCode").css("width", "95%");
                $("#headid").val(data.sta);
                $("#hidBillStatus").val('1');
                var d, s = "";
                d = new Date(); //Create Date object. 
                s += d.getYear() + "-";
                s += (d.getMonth() + 1) + "-"; //Get month 
                s += d.getDate() + " "; //Get day 

                $("#txtModifiedDate").val(s);

                fnStatus('2');  //控制按钮状态

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

//获取明细数据
function getDropValue() {
    //---------------获取煤种信息----------------//
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;

            var Ware = $("#drpWare" + rowid).val(); //仓库
            var CoalID = $("#IdCoalName" + rowid).val(); //煤种id
            //var CoalName = $("#txtCoalName" + rowid).find("option:selected").text();
            var CoalName = $("#txtCoalName" + rowid).val();
            var Quantity = $("#txtQuantity" + rowid).val(); //数量
            var InCost = $("#txtInCost" + rowid).val(); //进货单价
            var SaleCost = $("#txtSaleCost" + rowid).val(); //销售单价

            var TaxRate = $("#txtTaxRate" + rowid).val(); //税率
            var Tax = $("#txtTax" + rowid).val(); //税额
            var Money = $("#txtMoney" + rowid).val(); //含税金额
            var TotalInCost = $("#txtPurMoney" + rowid).val(); // 进货总金额
            SendOrderFit_Item[j] = [[Ware], [CoalID], [CoalName], [Quantity], [InCost], [SaleCost], [TaxRate], [Tax], [Money],[TotalInCost]];
        }
    }
    return SendOrderFit_Item;
}

//获取主表信息
function fnGetInfo() {
    var strInfo = ''; //表头
    var strdetailinfo = ''; //表体


    var CodeType = $("#ddlSendNo_ddlCodeRule").val();
    var SendNo = "";
    if (CodeType == "")  //手动编号或更新时
        SendNo = $("#ddlSendNo_txtCode").val();
    if (isnew == "2") {
        SendNo = $("#ddlSendNo_txtCode").val();
    }
    var SourceBillID = $("#txtSourceBillID").val(); //销售合同ID 
    var SourceBillNo = $("#txtSourceBillNo").val(); //销售合同编码 
    var SettleType = $("#drpSettleType").val(); //结算方式
    var CustomerID = $("#txtCustomerID").val(); // 客户编码
    var CustomerName = $("#txtCustomerName").val(); // 客户名称
    var InvoiceUnit = $("#txtInvoiceUnit").val(); // 开票单位
    var TransPortType = $("#drpTransPortType").val(); // 调运类型
    var SendTime = $("#txtSendTime").val(); // 发运时间
    var SendNum = $("#txtSendNum").val(); //  发运吨数
    var GetNum = $("#txtGetNum").val(); //  实收吨数-----2014--8-2
    
    var SumMoney = $("#txtSumMoney").val(); // 总金额
    var PPersonID = $("#txtPPersonID").val(); // 业务员ID
    var PPerson = $("#txtPPerson").val(); // 业务员名称
    var DeptName = $("#DeptName").val(); // 部门名称
    var DeptID = $("#hdDeptID").val(); // 部门编码
    var PurContractID = $("#txtPurContractID").val(); // 采购合同号ID
    var PurContractNo = $("#txtPurContractNo").val(); // 采购合同号编码
    var ProviderID = $("#txtProviderID").val(); // 供应商ID
    var ProviderName = $("#txtProviderName").val(); // 供应商名称
    var SupplyAmount = $("#txtSupplyAmount").val(); // 供货金额
    var Remark = $("#txtRemark").val(); // 备注
    var BusiType = $("#drpBusiType").val(); // 业务类型 1.普通销售 2.采购直销
    var TransportFee = $("#txtTransportFee").val();  //运费
     var serviceId=$("#txtServiceID").val(); //服务商id
     var serviceName=$("#txtcompany").val();//服务商名称
  
   
    //表体
    //    var Ware=$("#drpWare").val();//仓库ID
    //    var CoalID=$("#txtCoalID").val();//煤种编码
    //    var CoalName=$("#txtCoalName").val();//煤种名称
    //    var Quantity=$("#txtQuantity").val();//数量
    //    var InCost=$("#txtInCost").val();//进货单价
    //    var SaleCost=$("#txtSaleCost").val();//销售单价
    //    var TaxRate=$("#txtTaxRate").val();//税率
    //    var Tax=$("#txtTax").val();//税额
    //    var TaxMoney=$("#txtTaxMoney").val();//含税金额
    var TranSportID = $("#txtTranSportID").val(); //调运单ID
    var TranSportNo = $("#txtTranSportID").val(); //调运单编码
    var UPTranSportState = $("#drpUPTranSportState").val(); //更新调运状态

   
    strInfo = 'SendNo=' + escape(SendNo) + '&CodeType=' + escape(CodeType) + '&SourceBillID=' + escape(SourceBillID) + '&SourceBillNo=' + escape(SourceBillNo)
    + '&SettleType=' + escape(SettleType) + '&CustomerID=' + escape(CustomerID) + '&CustomerName=' + escape(CustomerName) + '&InvoiceUnit=' + escape(InvoiceUnit) +
    '&TransPortType=' + escape(TransPortType) + '&SendTime=' + escape(SendTime) + '&SendNum=' + escape(SendNum) +'&GetNum=' + escape(GetNum) + '&SumMoney=' + escape(SumMoney)
    + '&PPersonID=' + escape(PPersonID) + '&PPerson=' + escape(PPerson) + '&DeptName=' + escape(DeptName) + '&DeptID=' + escape(DeptID)
    + '&PurContractID=' + escape(PurContractID) +'&serviceId='+escape(serviceId)+'&servcieName='+escape(serviceName)+'&PurContractNo=' + escape(PurContractNo) + '&ProviderID=' + escape(ProviderID) + '&ProviderName=' + escape(ProviderName)
    + '&SupplyAmount=' + escape(SupplyAmount) + '&Remark=' + escape(Remark) + '&BusiType=' + escape(BusiType) + '&TransportFee=' + escape(TransportFee) + '';

    strdetailinfo = '&TranSportID=' + escape(TranSportID) + '&TranSportNo=' + escape(TranSportNo) + '&UPTranSportState=' + escape(UPTranSportState);
    return strInfo + strdetailinfo;
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

//确认
function Fun_ConfirmOperate() {
    var c = window.confirm("确定执行确认操作吗？");
    if (c == true) {
        var headid = $("#headid").val();
        action = "ConfirmOutBus";
        var strParams = "action=" + action + "&headid=" + headid + '&billtype=' + escape(typeflag) + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/BusinessManage/DealOutBus_ADD.ashx",
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
        action = "CancelConfirmOutBus";
        var strParams = "action=" + action + "&headid=" + headid + '&billtype=' + escape(typeflag) + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/BusinessManage/DealOutBus_ADD.ashx",
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

//----------------------------------------------end-------------------------------------------------------------------//
function changePageStyle() {
    //parent.addTab(null, '21305', '新建订单', 'Pages/Office/MedicineManager/NewOrderDemo.aspx?ModuleID=21305');
    window.open("NewOrderDemo.aspx?ModuleID=21305");

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
            case '2': //确认（已保存没确认）

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