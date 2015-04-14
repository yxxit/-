var isnew = "1"; //1添加;2保存
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
$(document).ready(function() {
    requestobj = GetRequest();
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        GetInfoById(document.getElementById("headid").value); //根据id获取详细信息
        GetDetailById(document.getElementById("headid").value);
        $("#labTitle_Write1").html("采购结算单查看");
        $("#txtProviderName").css("display", "none");
        $("#search").css("display", "none");        
    }
    else {

        $("#labTitle_Write1").html("采购结算单新建");

    }
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

function SearchIncomeSettle() {
    var ProviderName = $("#txtProviderName").val();
    if (ProviderName == "") {
        alert("请先选择供应商!");
        return;
    }
    var iscount;
    if (document.getElementById("iscountCheck").checked) {
        iscount = 1;
    } else {
        iscount = 0;
    }
    var action = "SearchIncomeSettle";
    var BeginT = $("#txtBeginT").val();  //发货开始时间
    var EndT = $("#txtEndT").val();       //发货结束时间
    var ary = new Array();
    var rowsCount = 0;
      fnDelRow();
      fnDelOneRow();
      $.ajax({
          type: "POST", //用POST方式传输
          dataType: "json", //数据格式:JSON
          url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
          cache: false,
          data: "action=" + action + "&provoderName=" + escape(ProviderName) + "&beginDate=" + escape(BeginT) + "&endDate=" + escape(EndT) + "&iscount=" + escape(iscount),
          beforeSend: function() {

          },
          error: function() {
              showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
          },
          success: function(data) {
              if (data.data != null) {
                  $.each(data.data, function(i, item) {
                      rowsCount++;
                      FillSignRows(i, item);
                  });
                  if(rowsCount==0){
                    alert("没有未结算的单据，请核实!");
                    return;
                  }
              }
              $("#txtTRLastIndex").val(rowsCount + 1);
          },
          complete: function() {

              //fnTotalInfo();
          } //接收数据完毕
      });
}

function FillSignRow(i, item) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    //  var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    // var rowID = parseInt(txtTRLastIndex.value) + 1;
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;
    //结算数量
    //已结算数量 item.sttlCounts
    //单据数量   item.ProductCount
    var sttlCounts;
    var sttlTotalPrices;
    var TaxPrice;
    var ProductCount;    
    
    if(item.jt_cg=="1"){
        sttlCounts=item.sttlCounts2;
        sttlTotalPrices=item.sttlTotalPrices2;
        TaxPrice=item.TaxPrice2;
        ProductCount=item.ProductCount2;
    }else{
        sttlCounts=item.sttlCounts1;
        sttlTotalPrices=item.sttlTotalPrices1;
        TaxPrice=item.TaxPrice1;
        ProductCount=item.ProductCount1;
    }
    

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='jt_cg" + rowID + "' value='" + item.jt_cg + "' ><input type='hidden' id='detailsid" + rowID + "' value='" + item.id + "' >";
    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列://单据编号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtArriveNo" + rowID + "' value=\"" + item.ArriveNo + "\"  disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:客户单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCustName" + rowID + "' value=\"" + item.CustName + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单据数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtNum" + rowID + "'    value=\"" + ProductCount + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单据单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxPrice" + rowID + "'       value=\"" + Number(TaxPrice).toFixed(2) + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:结算数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "'     value=\"" + Number(item.sttlCount).toFixed(2) + "\"    onblur=\"getTotalMoney();getMoney(" + rowID + ",1);\"  onkeyup='return ValidateNumber(this,value)'   type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:结算单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnitCost" + rowID + "'  value=\"" + Number(item.sttlPrice).toFixed(2) + "\"  onblur=\"getTotalMoney();getMoney(" + rowID + ",5);\"  onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:总金额
    newFitDesctd.className = "cell";
//    var TotalFee = Number(item.SttlTotalPrice).toFixed(2);
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value=\"" + Number(item.sttlTotalPrice).toFixed(2) + "\"    onblur=\"getTotalMoney();getMoney(" + rowID + ",7);\"  value='0.00'   onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单据日期
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCreateDate" + rowID + "' value=\"" + item.CreateDate + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;

    var newFitDesctd = newTR.insertCell(m);  //备注
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSttlRemark" + rowID + "'    type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:已结算数量
    newFitDesctd.className = "cell";

    newFitDesctd.innerHTML = "<input id='txtlCounts" + rowID + "'   disabled='disabled'   value=\"" + sttlCounts + "\"     type='text' class='tdinput'    style='width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:已结算金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtsttlTotalPrices" + rowID + "'  disabled='disabled'  value=\"" + Number(sttlTotalPrices).toFixed(2) + "\"      type='text' class='tdinput'    style='width:90%;'/>"; //添加列内容
    m++;   
   
    var newFitDesctd = newTR.insertCell(m); 
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSttlRemark" + rowID + "'    type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m);
    newFitDesctd.className = "cell";
   newFitDesctd.innerHTML = "<input id='txtProductId" + rowID + "' value=\"" + item.ProductID + "\"    type='hiden'  style='display:none;' class='tdinput' />";

   m++;
}


//通过id和源单类型获取结算单信息
function GetInfoById(headid) {
    var action = "SearchSettleOne";

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
        cache: false,
        data: "action=" + action + "&headid=" + escape(headid),
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            var j = 1;
            isnew = "2";
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {

                    $("#txtxsfpNo").val(item.CgfpNo); //结算单号
                    $("#txtCreateDate").val(item.CreateDate); //制单日期
                    $("#txtCreatePerson").val(item.CreatorName); //制单人  
                    $("#txtProviderID").val(item.ProviderId); //制单人

                    if (item.billStatus == "1") {
                        $("#txtStatus").val("制单");
                        fnStatus('2');
                    }
                    if (item.billStatus == '2') //如果是确认状态
                    {
                        $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                        $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                        $("#txtConfirmDate").val(item.ConfirmDate); //确认日期
                        $("#txtStatus").val("确认");
                        //确认日期
                        fnStatus('3');

                        //按钮状态
                    } if (item.billStatus == "9") {
                    $("#txtStatus").val("关闭");
                    $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                    $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                    $("#txtConfirmDate").val(item.ConfirmDate); //确认日期
                        fnStatus('9');
                    }

                }
            });

            $("#mainindex_1").css("display","none");//隐藏检索条件
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}

//详细页面
function GetDetailById(headid) {
    var action = "SearchIncomeSettleDetail";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
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

//获取金额
function getMoney(rowid, typeflag) {
    
    // rowid 行号， typeflag 传入类型 1 数量 5 销售单价 7 税率 9 采购单价 13 销售总价  16 采购总价

    var quantity = $("#txtQuantity" + rowid).val(); //数量
    var SaleCost = $("#txtUnitCost" + rowid).val();  //结算单价
    var imoney = (Number(quantity) * Number(SaleCost)).toFixed(2);  //金额
    var money = $("#txtMoney" + rowid).val(); //金额
   // document.getElementById("txtMoney" + rowid).value = Number(imoney).toFixed(2);

    var SaleImoney = ""; 
    switch (typeflag) {
        case 1:
            SaleImoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //销售金额
            document.getElementById("txtQuantity" + rowid).value = Number(quantity).toFixed(2);
            document.getElementById("txtMoney" + rowid).value = Number(SaleImoney).toFixed(2);
            
            if (document.getElementById("txtQuantity" + rowid).value == "0.00") {
                document.getElementById("txtUnitCost" + rowid).value = '0.00';
                return;
            }           
            
            break;
        case 5:
            SaleImoney = (Number(quantity) * Number(SaleCost)).toFixed(4);  //销售金额

            document.getElementById("txtUnitCost" + rowid).value = Number(SaleCost);
            document.getElementById("txtMoney" + rowid).value = Number(SaleImoney).toFixed(2);

            break;
        case 7:
            // document.getElementById("txtUnitCost" + rowid).value = Number(SaleCost).toFixed(2);

            if (quantity == "0.00") {
                document.getElementById("txtUnitCost" + rowid).value = '0.00';

                return;
            } else {
                document.getElementById("txtMoney" + rowid).value = Number(money).toFixed(2)
                document.getElementById("txtUnitCost" + rowid).value = (document.getElementById("txtMoney" + rowid).value / quantity).toFixed(8)
            }
            break;
        case 9:
            break;
        case 13:
            break;
        case 17:
            break;
    }    
}


//获取总金额
function getTotalMoney() {
    var summoney = 0;
    for (var i = 0; i < 100; i++) {
        if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
            summoney = summoney + Number(document.getElementById("txtMoney" + i).value);
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
//保存信息

function SaveSellOrder() {
    var bmgz = "";
    var Flag = true;
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var i=0;
    var iCount = 0; //明细中数据数目
    
    var headid = document.getElementById("headid").value;//获取页面中的id
    if (headid == "undefined" || headid == "null" || headid == "") {
        headid = "0";
    }
    
    if ($("#txtProviderID").val() == "" ) {
        isFlag = false;
        fieldText = fieldText + "供应商信息|";
        msgText = msgText + "供应商不能为空|";
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
       var UnitCost = document.getElementById("txtUnitCost" + i).value;
       var TtlNum = document.getElementById("txtNum" + i).value;  // 单据总数量
       var JslCounts = document.getElementById("txtlCounts" + i).value; //已结算数量
       
       if (parseInt(Quantity) == 0 || document.getElementById("txtQuantity" + i).value=="0") {
         isFlag = false;
         fieldText = fieldText + "明细信息|";
         msgText = msgText + "第"+i+"行数量不能为空|";
         }
         
       if (parseInt(UnitCost) == 0 || document.getElementById("txtUnitCost" + i).value=="0") {
         isFlag = false;
         fieldText = fieldText + "明细信息|";
         msgText = msgText + "第"+i+"行金额不能为空|";
         } 
       }
    }
    
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else
    { 
    //得到明细表中的数据
    var strfitinfo = getDropValue().join("|");
    var providerId = $("#txtProviderID").val();
    var isconfirm = "";
    $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/Expenses/IncomeSettle.ashx",
        data: 'strfitinfo=' + escape(strfitinfo) + '&action=insertIncomeSettle' + "&headid=" + headid + '&providerId=' + providerId,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function () {
            AddPop();
        },
        error: function () { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function (data) {
            isSave = "1";
            if (data.sta > 0) {
                $("#btn_Search").css("display", "none");
                fnStatus('2'); //更新状态
                $("#headid").val(data.sta);
                GetInfoById(data.sta);
                if (isconfirm == "1") {
                    $("#billstatus").val("确认");
                    $("#hidBillStatus").val('2');
                    $("#imgUnSave").css("display", "inline");
                    $("#btn_save").css("display", "none");
                    $("#ImgUnConfirm").css("display", "none");
                    $("#UnConfirm").css("display", "inline");
                }

                SetDropValue();
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

//修改列表的更新信息
function SetDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;

            var rowid = signFrame.rows[i].id;
            var ArriveNo = $("#txtArriveNo" + rowid).val(); //单据编号
            var Id = $("#detailsid" + rowid).val(); //单据编号的Id
            var CustName = $("#txtCustName" + rowid).val(); //客户编号
            var Num = $("#txtNum" + rowid).val(); //单据数量
            var TaxPrice = $("#txtTaxPrice" + rowid).val(); //单据单价
            var Quantity = $("#txtQuantity" + rowid).val(); //结算数量
            var UnitCost = $("#txtUnitCost" + rowid).val(); //结算单价
            var Money = $("#txtMoney" + rowid).val(); //结算总价
            var CreateDate = $("#txtCreateDate" + rowid).val(); //单据日期
            var SttlRemark = $("#txtSttlRemark" + rowid).val(); //备注
            var ProductId = $("#txtProductId" + rowid).val();  //商品的编号
            var jt_cg = $("#jt_cg" + rowid).val(); //获取保存类型
            var txtlCounts = $("#txtlCounts" + rowid).val(); //已结算数量
            var txtsttlTotalPrice = $("#txtsttlTotalPrice" + rowid).val(); //已结算金额

            //设置改变值
            $("#txtlCounts" + rowid).val((Number(txtlCounts) + Number(Quantity)).toFixed(2));
            $("#txtsttlTotalPrice" + rowid).val((Number(txtsttlTotalPrice) + Number(Money)).toFixed(2));
        }

    }
}
//获明所有的明细信息
function getDropValue() {
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;

            var rowid = signFrame.rows[i].id;
            var ArriveNo = $("#txtArriveNo" + rowid).val(); //单据编号
            var Id = $("#detailsid" + rowid).val(); //单据编号的Id
            var CustName = $("#txtCustName" + rowid).val(); //客户编号
            var Num = $("#txtNum" + rowid).val(); //单据数量
            var TaxPrice = $("#txtTaxPrice" + rowid).val(); //单据单价
            var Quantity = $("#txtQuantity" + rowid).val(); //结算数量
            var UnitCost = $("#txtUnitCost" + rowid).val(); //结算单价
            var Money = $("#txtMoney" + rowid).val(); //结算总价
            var CreateDate = $("#txtCreateDate" + rowid).val(); //单据日期
            var SttlRemark = $("#txtSttlRemark" + rowid).val(); //备注
            var ProductId = $("#txtProductId" + rowid).val();  //商品的编号
            var jt_cg = $("#jt_cg" + rowid).val();//获取保存类型
            SendOrderFit_Item[j] = [[ArriveNo], [Id], [CustName], [Num], [TaxPrice], [Quantity], [UnitCost], [Money], [CreateDate], [SttlRemark], [ProductId],[jt_cg]];

        }

    }

    return SendOrderFit_Item;
}


//执行确认操作
function Fun_ConfirmOperate() {
    var c = window.confirm("确定执行确认操作吗？")
    if (c == true) {
        glb_BillID = document.getElementById('headid').value;
        var confirmor = "";
        var strfitinfo = getDropValue().join("|");

        $.ajax({
            type: "POST",
            url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址

            data: 'strfitinfo=' + escape(strfitinfo) + '&action=ConfirmSell' + "&headid=" + escape(glb_BillID),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {

                if (data.sta > 0) {
                    document.getElementById("txtBillStatusID").value = "2";
                    document.getElementById("hidBillStatus").value = "2";
                    document.getElementById("txtBillStatusName").value = "确认";
                    popMsgObj.ShowMsg('确认成功');
                    fnStatus('3');
                    var d, s = "";
                    d = new Date(); //Create Date object. 
                    s += d.getYear() + "-";
                    s += (d.getMonth() + 1) + "-"; //Get month 
                    s += d.getDate() + " "; //Get day

                    $("#txtConfirmDate").val(s);  //确认日期
                    $("#txtConfirmor").val(data.data);     //确认人姓名
                    $("#txtStatus").val("确认");

                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "1";
                    var glb_BillID = document.getElementById('headid').value;                               
                    var glb_IsComplete = false;  
                }
                else {

                    popMsgObj.ShowMsg(data.data);
                }
            }
        });

    }
}


//取消确认
function Fun_UnConfirmOperate() {
    var c = window.confirm("确定执行取消确认操作吗？")
    if (c == true) {



        glb_BillID = document.getElementById('headid').value;

     //   action = "CancelConfirmSell";

        var strfitinfo = getDropValue().join("|");

        $.ajax({
            type: "POST",
            url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
            data: 'strfitinfo=' + escape(strfitinfo) + '&action=CancelConfirmSell' + "&headid=" + escape(glb_BillID),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {

                if (data.sta > 0) {
                    document.getElementById("txtBillStatusID").value = "1";
                    document.getElementById("hidBillStatus").value = "1";
                    document.getElementById("txtBillStatusName").value = "制单";
                    popMsgObj.ShowMsg('取消确认成功');

                    fnStatus('2');

                    $("#txtConfirmor").val("");  //取消确认人姓名
                    $("#txtConfirmorId").val("");  //确认人id
                    $("#txtConfirmDate").val(""); //确认日期
                    $("#txtStatus").val("制单");
                    var glb_BillID = document.getElementById('headid').value;                                
                    var glb_IsComplete = false;                                   
                  

                }
                else {

                    popMsgObj.ShowMsg(data.info);
                }
            }
        });

    }
}


//取消关闭
function UncloseConfim() {
    var c = window.confirm("确定执行取消关闭操作吗？")
    if (c == true) {

    
        glb_BillID = document.getElementById('headid').value;
      
        action = "UnClosePur";

        var confirmor = "";
    

      
        $.ajax({
            type: "POST",
            url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
            data: "action=" + action + "&headid=" + escape(glb_BillID),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
            
            },
            success: function(data) {

                if (data.sta > 0) {
                    document.getElementById("txtBillStatusID").value = "2";
                    document.getElementById("hidBillStatus").value = "2";
                    document.getElementById("txtBillStatusName").value = " 取消关闭";
                    popMsgObj.ShowMsg('取消关闭成功');
                   
                    fnStatus('3');
                    $("#txtStatus").val("确认");
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "2";
                    var glb_BillID = document.getElementById('headid').value;              
                    var glb_IsComplete = false;   
                }
                else {

                    popMsgObj.ShowMsg(data.data);
                }
            }
        });

    }
}
//关闭
function CloseConfim() {
    var c = window.confirm("确定执行关闭操作吗？")
    if (c == true) {
        glb_BillID = document.getElementById('headid').value;
        action = "CloseConfim";

        var confirmor = "";
        $.ajax({
            type: "POST",
            url: '../../../Handler/JTHY/Expenses/IncomeSettle.ashx', //目标地址
            data: "action=" + action + "&headid=" + escape(glb_BillID),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
                //popMsgObj.ShowMsg('sdf');
            },
            success: function(data) {

                if (data.sta > 0) {
                    document.getElementById("txtBillStatusID").value = "9";
                    document.getElementById("hidBillStatus").value = "9";
                    document.getElementById("txtBillStatusName").value = "关闭";
                    popMsgObj.ShowMsg('关闭成功');

                    fnStatus('9');
                    $("#txtConfirmor").val("");  //取消确认人姓名
                    $("#txtConfirmorId").val("");  //确认人id
                    $("#txtConfirmDate").val(""); //确认日期
                    $("#txtStatus").val("关闭");
                  
                    var glb_BillID = document.getElementById('headid').value;
                    var glb_IsComplete = false;

                }
                else {

                    popMsgObj.ShowMsg(data.data);
                }
            }
        });

    }
}
function FillSignRows(i, item) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    //  var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    // var rowID = parseInt(txtTRLastIndex.value) + 1;
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    //结算数量
    //已结算数量 item.sttlCounts
    //单据数量   item.ProductCount
    var jssl=0;
    if(Math.abs(item.ProductCount)<=Math.abs(item.sttlCounts)){
        jssl=0;
    }else{
        jssl=Math.abs(item.ProductCount) - Math.abs(item.sttlCounts);
    }
    
    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='jt_cg" + rowID + "' value='" + item.jt_cg + "' ><input type='hidden' id='detailsid" + rowID + "' value='" + item.id + "' >";
    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列://单据编号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtArriveNo" + rowID + "' value=\"" + item.ArriveNo + "\"  disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容

    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列:客户单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCustName" + rowID + "' value=\"" + item.CustName + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列:单据数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtNum" + rowID + "'    value=\"" + item.ProductCount + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单据单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxPrice" + rowID + "'       value=\"" + Number(item.TaxPrice).toFixed(2) + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:结算数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "'   value=\"" + jssl + "\"  onblur=\"getTotalMoney();getMoney(" + rowID + ",1);\"  onkeyup='return ValidateNumber(this,value)'   type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:结算单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnitCost" + rowID + "'  value='0.00'  onblur=\"getTotalMoney();getMoney(" + rowID + ",5);\"  onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:总金额
    newFitDesctd.className = "cell";
    //    var TotalFee = Number(item.SttlTotalPrice).toFixed(2);
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value='0.00'   onblur=\"getTotalMoney();getMoney(" + rowID + ",7);\"  value='0.00'   onkeyup='return ValidateNumber(this,value)'  maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单据日期
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCreateDate" + rowID + "' value=\"" + item.CreateDate + "\" disabled='disabled' type='text' class='tdinput' style=' width:90%' >";

    m++;
    var newFitDesctd = newTR.insertCell(m);  //备注
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSttlRemark" + rowID + "'    type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:已结算数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtlCounts" + rowID + "'  disabled='disabled'     value=\"" + item.sttlCounts + "\"     type='text' class='tdinput'    style='width:90%;'/>"; //添加列内容
    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列:已结算金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtsttlTotalPrice" + rowID + "'  disabled='disabled'  value=\"" + Number(item.sttlTotalPrice).toFixed(2) + "\"      type='text' class='tdinput'    style='width:90%;'/>"; //添加列内容
    m++;
    var newFitDesctd = newTR.insertCell(m);
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtProductId" + rowID + "' value=\"" + item.ProductId + "\"    type='hiden'  style='display:none;' class='tdinput' />";

    m++;
}

