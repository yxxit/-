var isnew="1";//1添加;2保存
var isconfirm='';



$(document).ready(function() {


    requestobj = GetRequest();
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        //详细信息
     

        GetInfoById(document.getElementById("headid").value);
        GetDetailById(document.getElementById("headid").value);
        $("#labTitle_Write1").html("质检单据查看");
    }
    else {
        $("#labTitle_Write1").html("质检单据新建");
    }

});



//获取url中"?"符后的字串
function GetRequest()
 {
   var url = location.search; 
   var theRequest = new Object();
   if (url.indexOf("?") != -1) 
   {
      var str = url.substr(1);
      strs = str.split("&");
      for(var i = 0; i < strs.length; i ++) 
      {
         theRequest[strs[i].split("=")[0]]=unescape(strs[i].split("=")[1]);
      }
   }

   return theRequest;
  }





  function GetInfoById(headid) {
    
          var action="SearchQTestInfo";

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/stockmanage/QTestInfo.ashx', //目标地址
               cache: false,
               data: "action=" + action + "&headid="+escape(headid)+'',          
               beforeSend: function() {  }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
               
                   var j = 1;
                   isnew="2";
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "") {
                       
                      document.getElementById("divQTestBillNo").innerHTML =item.ReportNo;
                      $("#divQTestBillNo").css("display","block");
                      $("#divCodeRule").css("display","none");

                      $("#txtSourceBillNo").val(item.SourceBillID);
                      //   $("#txtSourceBillNo").val(item.SourceBillNo);
                         
                      //   $("#txtProviderID").val(item.ProviderID);
//                         $("#txtProviderName").val(item.ProviderName);
                         $("#txtCheckDate").val(item.CheckDate);
                         $("#drpCheckType").val(item.CheckMode);
//                         $("#txtTranSportID").val(item.TranSportID);
//                         $("#txtTranSportNo").val(item.DiaoyunNO);
//                         $("#txtCoalID").val(item.CoalID);
                         $("#txtCoalName").val(item.CoalName);
                         $("#txtQuantity").val(item.CheckNum);
                         $("#hdDeptID").val(item.ChecDeptId);
                         $("#DeptName").val(item.DeptName);
                         $("#txtPPersonID").val(item.PPersonID);
                         $("#txtPPerson").val(item.PPerson);
                         $("#txtDescription").val(item.CheckResult);
                         $("#txtRemark").val(item.Remark);
                         $("#txt_CreateDate").val(item.CreateDate);
                         $("#UserPrincipal").val(item.CreatorName);
                         $("#txtModifiedDate").val(item.ModifiedDate);
                         $("#txtModifiedUserID").val(item.ModifiedUserID);
                         
                         
                         
                         if(item.BillStatus=="1")
                         {
                            fnStatus('2');   //按钮状态
                         }
                         else if(item.BillStatus=="2")
                         {
                            $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                            $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                            $("#txtConfirmDate").val(item.ConfirmDate);  //确认日期
                            fnStatus('3');   //按钮状态
                         }
                         
                         


                       }
                   });
                   
                
                  
                   
               },
               error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
               complete: function() { } //接收数据完毕
           });
    }
    
    
    
    function GetDetailById(headid)
    {
          var action="SearchQTestDetail";
          var rowsCount=0;
          $.ajax({
              type: "POST", //用POST方式传输
              dataType: "json", //数据格式:JSON
              url: '../../../Handler/JTHY/stockmanage/QTestInfo.ashx', //目标地址
              cache: false,
              data: "action=" + action + "&headid=" + escape(headid) + '',
              beforeSend: function() { }, //发送数据之前

              success: function(msg) {
                  //数据获取完毕，填充页面据显示
                  //数据列表

                  var j = 1;
                  $.each(msg.data, function(i, item) {
                      if (item.id != null && item.id != "") {
                          rowsCount++;
                       
                          FillSignRow(i, item.CheckItem, item.CheckStandard, item.CheckValue, item.chexiangNo);

                      }
                  });


                  $("#txtTRLastIndex").val(rowsCount + 1);

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





function getCheckData(selectid,b)
{
     $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Common/CheckInfo.ashx?action=getcheckdata', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                  var seltext=item.TypeName;
                  var selvalue=item.ID;                                                                
                  var o=document.getElementById(selectid);
                  var varItem=new Option(seltext,selvalue);//要添加的选项
                  o.options.add(varItem);//添加选项
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


function getCheckNature(coalvalue,rowid)
{
     $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Common/CheckInfo.ashx?str='+escape(coalvalue)+'&action=getcheckdesc', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
                   $("#txtCheckTarget"+rowid).val(''); //描述信息
            $.each(msg.data, function(i, item) {
                                                                  
                   $("#txtCheckTarget"+rowid).val(item.Description); //描述信息
                   
                    
            });
            
        },
        complete: function() { } //接收数据完毕
    });
}

/**///2012-12-25 因页面改版，原版本注释掉。
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
   var txtTRLastIndex = document.getElementById("txtTRLastIndex");
    
    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;
    
       var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
    m++;
    
    //加载检测项目
    var newFitNametd = newTR.insertCell(m); //添加列:检测项目
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = ""+
    "<select name='drpCheckItem"+rowID+"' style='width:80%' class='tddropdlist'  runat='server' id='drpCheckItem"+rowID+"' onChange='getCheckNature(this.value,"+rowID+")' ></select>"; //添加列内容
    m++;
    
    getCheckData('drpCheckItem'+rowID,0);//加载检测项目

    var newFitDesctd = newTR.insertCell(m); //添加列:检测车号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCarNo" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;'    /> "; //添加列内容
    m++;        
   
    var newFitDesctd = newTR.insertCell(m); //添加列:描述信息
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='txtCheckTarget" + rowID + "'   style=' width:95%;'    type='text'  class='tdinput' />"; //添加列内容
    m++;
       
    var newFitDesctd = newTR.insertCell(m); //添加列:检验值
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCheckValue" + rowID + "'     type='text'  class='tdinput'    style=' width:90%;' />"; //添加列内容
    m++;  
    
    txtTRLastIndex.value = rowID; //将行号推进下一行 
    }


//填写 详细信息
   function FillSignRow(i,checkitem,checkdesc,checkvalue,chexiangno) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
   //  var txtTRLastIndex = document.getElementById("txtTRLastIndex");
    
   // var rowID = parseInt(txtTRLastIndex.value) + 1;
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;
    
    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  />";
    m++;


    //加载检测项目
    var newFitNametd = newTR.insertCell(m); //添加列:检测项目
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "" +
    "<select name='drpCheckItem" + rowID + "' style='width:80%' class='tddropdlist'  runat='server' id='drpCheckItem" + rowID + "' onChange='getCheckNature(this.value," + rowID + ")' ></select>"; //添加列内容
    m++;

    getCheckData('drpCheckItem' + rowID, checkitem); //加载检测项目
 
        var newFitDesctd = newTR.insertCell(m); //添加列:检测车号
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCarNo" + rowID + "' value='"+chexiangno+"'  maxlength='10' type='text' class='tdinput' style=' width:90%;'    /> "; //添加列内容
    m++;
    
  
     
   
    var newFitDesctd = newTR.insertCell(m); //添加列:描述信息
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input  id='txtCheckTarget" + rowID + "' value='"+checkdesc+"'   style=' width:95%;'    type='text'  class='tdinput' />"; //添加列内容
    m++;

       
    var newFitDesctd = newTR.insertCell(m); //添加列:检验值
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCheckValue" + rowID + "' value='"+checkvalue+"'     type='text'  class='tdinput'    style=' width:90%;' />"; //添加列内容
    m++;   

  //  txtTRLastIndex.value = rowID; //将行号推进下一行  
     $("#txtTRLastIndex").val((rowID + 1).toString()); //将行号推进下一行
    
    }
    

//计算各种合计信息
function fnTotalInfo() {
  var TotalPrice = 0; //金额合计
    var CountTotal = 0; //数量合计
    var DisPrice = 0; //折扣额合计
    var precisionLength;
    var signFrame = findObj("dg_Log", document);
    var j = 0;var pCountDetail=0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
             var rowid = signFrame.rows[i].id;
            pCountDetail=$("#ProductCount" + rowid).val(); //数量
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
           var TotalPriceDetail = FormatAfterDotNumber((SupplyPrice* pCountDetail- DiscountPrice), precisionLength); //金额
          
            $("#TotalPrice" + rowid).val(FormatAfterDotNumber(TotalPriceDetail, precisionLength)); //金额
            TotalPrice += parseFloat(TotalPriceDetail);
            CountTotal += parseFloat(pCountDetail);
            DisPrice+=parseFloat(DiscountPrice);
           
        }
    }
    //alert(TotalPrice);
    $("#CountTotalPrice").val(FormatAfterDotNumber(TotalPrice, precisionLength));
    //   $("#CountTotal").val(FormatAfterDotNumber(CountTotal, precisionLength));
  $("#CountTotal").val(CountTotal);
       $("#DiscountPrice").val(FormatAfterDotNumber(DisPrice, precisionLength));
       $("#TotalPrice").val($("#CountTotalPrice").val());
       $("#DiscountAmount").val((  $("#TotalPrice").val()*1.0)*($("#discount").val()*1.0));
    
       changeDiscountAmount();
}
function changeDiscountAmount()
{
 $("#DiscountAmount").val((  $("#TotalPrice").val()*1.0)-($("#discount").val()*1.0));
   $("#discount").val( $("#DiscountPrice").val());
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
        url: '../../../Handler/Office/MedicineManager/MedicineList.ashx?str='+str+'&action=getprodcutinfo', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                //填充物品ID，物品编号，物品名称,单位ID，单位名称，规格，
                if(!IsExistCheck(item.ProdNo))//如果是重复的物品编号不允许添加了
                 {  
                    AddSignRow();
                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    var rowID = parseInt(txtTRLastIndex.value);
                    $("#ProductNo" + rowID).val(item.cInvAddCode); //商品编号
                    if(document.getElementById("hiddProductNo"+rowID))
                    $("#hiddProductNo" + rowID).val(item.ProductNo); //商品编号
                    if(document.getElementById("ProductName"+rowID))
                    $("#ProductName" + rowID).val(item.cInvName); //商品名称]
                    if(document.getElementById("PYShort"+rowID))
                    $("#PYShort"+rowID).val(item.PYShort);
                    if(document.getElementById("Specification"+rowID))
                    $("#Specification" + rowID).val(item.Specification); //规格
                    if(document.getElementById("UnitID"+rowID))
                    $("#UnitID" + rowID).val(item.UnitName); //单位
                    if(document.getElementById("ProductCount"+rowID))
                    $("#ProductCount" + rowID).val('0'); //数量
                    if(document.getElementById("UnitPrice"+rowID))
                    $("#UnitPrice" + rowID).val(item.UnitPrice); //单价
                    if(document.getElementById("GuidPrice"+rowID))
                    $("#GuidPrice" + rowID).val(item.GuidPrice); //指导价
                    if(document.getElementById("SupplyPrice"+rowID))
                       $("#SupplyPrice" + rowID).val(item.GuidPrice); //供货价
                    if(document.getElementById("DiscountPrice"+rowID))
                    $("#DiscountPrice"+rowID).val("0");
                    //$("#TotalPrice" + rowID).val(item.TotalPrice); //金额
                   // $("#Remark" + rowID).val(item.Remark);      //备注                  
                }
                else
                {
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
function IsExistCheck(prodNo)
{
    var sign=false ;
    var signFrame = findObj("dg_Log",document);
    var DetailLength = 0;//明细长度
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  
        for (i = 1; i < signFrame.rows.length; i++) 
        {
          var prodNo1 = document.getElementById("ProductNo" + i).value.Trim();
            if ((signFrame.rows[i].style.display != "none" )&&(prodNo1 == prodNo)) 
            {
                 sign= true;
                 break ;
            }
        }
    }
 
    return sign ;
}

//选择业务员
function fnSelectSeller() {
   
        alertdiv('UserSeller,Seller');
   
}
//选择收款员，默认情况下，收款员和业务员是同一人
function fnSelectCashier()
{
        alertdiv('cashier,cashierId');
}

//选择客户
function  fnSelectCustInfo() {
        popSellCustObj.ShowList('protion');
}
//选择T6部门
function fnSelectDept(){
        popSellDeptObj.ShowList('protion');
}

////选择收货联系人
//function fnSelectSeller1() {
//         
//        alertdiv('UserSeller,Seller');
//   
//}
////选择采购联系人
//function fnSelectSeller2() {
//    
//         alertdiv('cashier,cashierId');
//   
//}


//选择客户后，为页面填充数据
function GetCust(CustID,CustNo,CustName) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/CustManager/CustName.ashx",
        data: 'id=' + custID+'CustNo='+CustNo+'CustName='+CustName,
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
    if(ddlCodeR.selectedIndex == "0")                                  /*手动输入*/  
    {
        //document.getElementById("txtOrderNo").style.visibility = "visible";
        document.getElementById("txtOrderNo").value = "";
        document.getElementById("txtOrderNo").disabled=false;
    }
    else
    {
        document.getElementById("txtOrderNo").disabled = true;
        document.getElementById("txtOrderNo").value = "保存时自动生成";
    }
}

function cancelConfirm()
{
  $("#hidBillStatus").val('1');
  var orderNo=$("#hiddOrderID").val()*1;
  $("#Imgbtn_confirm").css("display","none");
  $("#btn_confirm").css("display","inline");
  $("#btn_save").css("display","inLine");
   $("#imgUnSave").css("display","none");
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/MedicineManager/DealCustOrder.ashx",
        data: 'orderNo='+orderNo + '&action=cancelConfirm',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
            if (data.data=="ok") {
                
                    
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "取消确认成功！");
               $("#billstatus").val("制单");
                  $("#ImgUnConfirm").css("display","inline");
                        $("#UnConfirm").css("display","none");
            }
        }
    });
  
}
function getMoney()
{
 var imoney=document.getElementById("txtQuantity1").value*document.getElementById("txtUnitCost1").value;
 document.getElementById("txtMoney1").value=imoney;
 document.getElementById("txtContractMoney").value=imoney;
 
}

//只能输入数字
function ValidateNumber(e, pnumber)
{
    if (!/^\d+[.]?\d*$/.test(pnumber))
    {
        var newValue = /^\d+[.]?\d*/.exec(e.value);
        if (newValue != null)
        {
            e.value = newValue;
        }
        else
        {
            e.value = "";
        }
    }
    return false;
}



//点击确认填充input

function GetConfim() {
    debugger;
    var ck = document.getElementsByName("CheckboxProdID");
    var strCheck = "";
    var strId = "";
    var strNo = "";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            strCheck = ck[i].value;
            var Id = $("#arriveId" + strCheck).val();
            if (strId.indexOf(Id) != -1) {
                continue;
            }
            else {
                strId += Id + ',';
                strNo += $("#arriveNo" + strCheck).val() + ',';
            }
        }
    }
    if (strId == "") {
        popMsgObj.ShowMsg('请先选择数据！');
        return;
    }
    strId = strId.substring(0, strId.length - 1);
    strNo = strNo.substring(0, strNo.length - 1);
    $("#txtSourceBillID").val(strId);
    $("#txtSourceBillNo").val(strNo);
    DivJTNameClose_InBus();
}




//从到货信息自定义控件传值
function GetInBus(ID,ArriveNo,ProviderID,providername,diaoyunid,diaoyunno,ProductID,productname,ProductCount,TotalFee,ProJsFee)
{  
           
        
        document.getElementById("txtSourceBillID").value ="";  
        document.getElementById("txtSourceBillNo").value ="";
        // document.getElementById("txtProviderID").value ="";
        //document.getElementById("txtProviderName").value ="";
        //document.getElementById("txtTranSportID").value ="";
        //document.getElementById("txtTranSportNo").value ="";
        //document.getElementById("txtCoalID").value ="";
        //document.getElementById("txtCoalName").value ="";
        //document.getElementById("txtQuantity").value ="";
    
                     
        document.getElementById("txtSourceBillID").value =ID;  
        document.getElementById("txtSourceBillNo").value =ArriveNo;
//        document.getElementById("txtProviderID").value =ProviderID;
//        document.getElementById("txtProviderName").value =providername;
//        document.getElementById("txtTranSportID").value =diaoyunid;
//        document.getElementById("txtTranSportNo").value =diaoyunno;
//        document.getElementById("txtCoalID").value =ProductID;
//        document.getElementById("txtCoalName").value =productname;
//        document.getElementById("txtQuantity").value =ProductCount;
         
        document.getElementById('HolidaySpan_Bus').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_Bus");//关闭遮罩层

    
}

function SaveSellOrder()
{
    
    debugger;
    isconfirm="0";
        
    if($("#ddlQTestBillNo_ddlCodeRule").val()=="" && $("#ddlQTestBillNo_txtCode").val()=="")
    {
       popMsgObj.ShowMsg("质检单号不能为空，请填写！");
       return;
    }
//    if(document.getElementById("txtCoalName").value=="")
//    {
//       popMsgObj.ShowMsg("煤种不能为空，请填写！");
//       return;
//    }
//    if(document.getElementById("txtQuantity").value=="")
//    {
//       popMsgObj.ShowMsg("数量不能为空，请填写！");
//       return;
//    }
    if(document.getElementById("txtPPerson").value=="")
    {
       popMsgObj.ShowMsg("质检员不能为空，请填写！");
       return;
    }
    if($("#txtDescription").val()=="")
    {
       popMsgObj.ShowMsg("结果描述不能为空，请填写！");
       return;
    }   
    
    
    var lineIndex=0;   //遍历的行数
    var lineRealIndex=0;  //实际的行数
    for(var i=1;i<100;i++)
    {
        lineIndex++;
        if(document.getElementById("txtCarNo"+i)!="undefined" && document.getElementById("txtCarNo"+i)!=null)
        {
            if(document.getElementById("txtCarNo"+i).value=="" )
            {
                 popMsgObj.ShowMsg("第"+i+"行的车号不能为空，请填写！");
                 return;
            }
            if(document.getElementById("drpCheckItem"+i).value=="0")
            {
                 popMsgObj.ShowMsg("请选择第"+i+"行的检测项目！");
                 return;
            }

            lineRealIndex++;
        }
        if((lineIndex-lineRealIndex)>2)
        {
            break;
        }
    }
    if(lineRealIndex==0)
    {
        popMsgObj.ShowMsg("请添加质检报告！");
        return;
    }
    

         
    var strfitinfo = getDropValue().join("|");
      


    if(!isHasline)
    {
       popMsgObj.ShowMsg("质检报告不能为空，请添加！");
       return;
    }

    if(!isHasCarNo)
    {
       popMsgObj.ShowMsg("第"+whline+"行检测车号不能为空，请填写！");
       return;
    }

    if(!isHasOption)
    {
       popMsgObj.ShowMsg("请选择第"+whline+"行的检验项目！");
       return;
    }   
   

     var strInfo = fnGetInfo();   
    
    
    var headid=document.getElementById("headid").value;
    if(headid=="undefined" || headid=="null" || headid=="")
    {
        headid="0";
    }

    $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/stockmanage/DealQTest_ADD.ashx",
        data: strInfo +'&strfitinfo=' + escape(strfitinfo)+ '&action=insert&billtype=1&headid='+escape(headid)+'&isconfirm='+escape(isconfirm),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        success: function(data) {
        
       
            isSave="1";
            if (data.sta > 0) {
            
                  isnew="2"
                  $("#headid").val(data.sta);
                  document.getElementById("divQTestBillNo").innerHTML =data.data;
                  $("#divQTestBillNo").css("display","block");
                  $("#divCodeRule").css("display","none");
                  var d, s=""; 
                d = new Date(); //Create Date object. 
                s += d.getYear()+"-"; 
                s += (d.getMonth() + 1) + "-"; //Get month 
                s += d.getDate() + " "; //Get day 

                $("#txtModifiedDate").val(s);
                
                fnStatus('2');  //控制按钮状态

         
                 popMsgObj.ShowMsg("保存成功！");
              }
              else
              {
                popMsgObj.ShowMsg(data.info);
              }
              hidePopup();
             
            
        }
    });
   
}




//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    
    var QTestType=$("#ddlQTestBillNo_ddlCodeRule").val();  //编号类型
    var QTestBillNo="";   //质检单号
    if(QTestType==""){
        QTestBillNo=$("#ddlQTestBillNo_txtCode").val();  //手动输入时，获取单据编号    
    }
    
    if(isnew=="2"){        //如果是更新
        QTestBillNo=$("#divQTestBillNo").text();
    }
     
    var SourceBillID=$("#txtSourceBillID").val();// 到货单ID
    var SourceBillNo=$("#txtSourceBillNo").val();// 多货单编码
//    var ProviderID=$("#txtProviderID").val();// 供应商id
//    var TranSportID=$("#txtTranSportID").val();// 调运单ID
//    var TranSportNo=$("#txtTranSportNo").val();// 调运单编码
//    var CoalID=$("#txtCoalID").val();// 煤种id
//    var Quantity=$("#txtQuantity").val();// 数量
    
    var CheckDate=$("#txtCheckDate").val();// 检验日期
    var CheckType= $("#drpCheckType").val();//检验方式
    var PPersonID=$("#txtPPersonID").val();// 检验人
    var DeptID=$("#hdDeptID").val();// 检验部门
    var Description=$("#txtDescription").val();// 结果描述
    var Remark= $("#txtRemark").val();// 备注
 
                                                                                                                                             
    strInfo = 'SourceBillID='+escape(SourceBillID)+'&SourceBillNo='+escape(SourceBillNo)+
    
    '&QTestBillNo='+escape(QTestBillNo)+'&QTestType='+escape(QTestType)+'&CheckDate='+escape(CheckDate)+'&CheckType='+escape(CheckType)+'&PPersonID='+escape(PPersonID)
    +'&DeptID='+escape(DeptID)+'&Description='+escape(Description)+'&Remark='+escape(Remark)+'';
            
    return strInfo;
}
//获取明细数据
function getDropValue() {

    isHasCarNo=true;  //是否有检验车号
    isHasOption=true; //是否选择了质检项目
    isHasline=false;  //是否添加了质检报告
    whline='';  //哪一行
    
    var SendOrderFit_Item = new Array();
    var signFrame = findObj("dg_Log", document);
    var j = 0;
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;  
           // rowid="";//暂时先设为空，后续动态实现增加行，此为行号标记                  
            var CarNo = $("#txtCarNo" + rowid).val();           //车号                            
            var CheckItem = $("#drpCheckItem" + rowid).val();   //检验项目
            var CheckTarget = $("#txtCheckTarget" + rowid).val(); //描述
            var CheckValue = $("#txtCheckValue" + rowid).val(); //检验值
            
            isHasline=true;
            if(CarNo=="") 
            {
                isHasCarNo=false;
                whline=i;
            }
            
            if(CheckItem=="0") 
            {
                isHasOption=false;
                whline=i;
            }
            
            if(CheckValue=="") 
            {
                isHasValue=false;
                whline=i;
            }
            
            SendOrderFit_Item[j] = [[CarNo], [CheckItem],[CheckTarget], [CheckValue]];

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
    $("#DiscountAmount").val(data.DiscountAmount);//折后金额
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
    if(document.getElementById("hidBillStatus").value!="1")
    {
        $("#btn_save").css('display','none');
        $("#imgUnSave").css('display','');
    }
    $("#txtstatus").val(data.StatusText);//订单状态（0未处理，1已处理）
    $("#hidStatus").val(data.Status);
    $("#txtsendstatus").val(data.SendStatusText);//发货状态（0未发货，1已发货）  
    $("#hidSendStatus").val(data.SendStatus);
    
    //------20121227添加gmsDate-----------//
    if(document.getElementById("gmsDate"))
    $("#gmsDate").val(data.gmsDate);
    //---------end-----------------------//
    
   if(document.getElementById('txtCreatorReal'))
   document.getElementById('txtCreatorReal').innerHTML = data.CreateName;
   if(document.getElementById('txtCreateDate'))
   document.getElementById('txtCreateDate').innerHTML = data.CreateDate;
   if(document.getElementById('txtConfirmor'))
   document.getElementById('txtConfirmor').innerHTML = data.ConfirmName;
   if(document.getElementById('txtConfirmDate'))
   document.getElementById('txtConfirmDate').innerHTML = data.ConfirmDate;
   if(document.getElementById('txtInvalidor'))
   document.getElementById('txtInvalidor').innerHTML = data.InvalidName;
   if(document.getElementById('txtInvalidDate'))
   document.getElementById('txtInvalidDate').innerHTML = data.InvalidDate;
   if(document.getElementById('txtChecker'))
   document.getElementById('txtChecker').innerHTML = data.CheckName;
   if(document.getElementById('txtCheckDate'))
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
                    $("#GuidPrice"+rowID).val(item.GuidPrice);//指导价
                    $("#SupplyPrice"+rowID).val(item.SupplyPrice);//供货价
                    $("#DiscountPrice"+rowID).val(item.DiscountPrice);//折扣额
                    $("#LimitSellArea"+rowID).val(item.limitarea);//限销区域
                    $("#TotalPrice" + rowID).val(item.TotalPrice); //金额
                    $("#Remark" + rowID).val(item.Remark);      //备注
                    txtTRLastIndex.value = rowID; //将行号推进下一行
                    
                    
            
            
                });
            }
        },
       complete:function(){fnTotalInfo();}//接收数据完毕
    });
}


//确认
function Fun_ConfirmOperate()
{
    var c=window.confirm("确定执行确认操作吗？");
    if(c==true)
    {


        var headid=$("#headid").val();

        action ="ConfirmQTest";
             


        var strParams = "action=" + escape(action) + "&headid=" + escape(headid)  +"&billtype=1"+'';
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/JTHY/stockmanage/DealQTest_ADD.ashx",
            data:strParams,   
            dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                AddPop();
              }, 
              error: function() 
              {
               popMsgObj.ShowMsg('请求失败');
              }, 
              success:function(data) 
              { 
                if(data.sta>0)
                {  
                    popMsgObj.ShowMsg('确认成功！');
                    fnStatus('3');  //确认之后的按钮状态
                    isconfirm="1";
                    var d, s=""; 
                    d = new Date(); //Create Date object. 
                    s += d.getYear()+"-"; 
                    s += (d.getMonth() + 1) + "-"; //Get month 
                    s += d.getDate() + " "; //Get day 

                    $("#txtConfirmDate").val(s);  //确认日期
                    
                    $("#txtConfirmorId").val(data.sta);   //确认人id
                    $("#txtConfirmor").val(data.data);     //确认人姓名
                    
                     
                }  
                else
                {
                      popMsgObj.ShowMsg('确认失败！');
                }
                 
              },
              complete: function() { hidePopup();} //接收数据完毕 
           }); 
      }
}



//执行取消确认
function cancelConfirm()
{
    
    var c=window.confirm("确定执行取消确认操作吗？");
    if(c==true)
    {


        var headid=$("#headid").val();

        action ="CancelConfirmQTest";
             

        var strParams = "action=" + escape(action) + "&headid=" + escape(headid)  +"&billtype=1"+'';
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/JTHY/stockmanage/DealQTest_ADD.ashx",
            data:strParams,   
            dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                AddPop();
              }, 
              error: function() 
              {
               popMsgObj.ShowMsg('请求失败！');
              }, 
              success:function(data) 
              { 
                if(data.sta>0)
                {  
                                               
                    popMsgObj.ShowMsg('取消确认成功！');
                    fnStatus('2');  //按钮状态，
                    isconfirm="0";
                    $("#txtConfirmDate").val("");  //清空确认日期
                    
                    $("#txtConfirmorId").val('');   //清空确认人id
                    $("#txtConfirmor").val('');     //清空确认人姓名
                     
                }  
                else
                {
                    popMsgObj.ShowMsg(data.info);
                }
              },
              complete: function() { hidePopup();} //接收数据完毕 
           });   
               
      }
}



//查看库存量
function showCurrentStock(prodno)
{
    document.getElementById("divCurrentStock").style.display = "block";
    document.getElementById('divzhezhao2').style.display = 'inline';
    AlertProductMsg();
    fnGetCurrentStockByNo(prodno); //获取物品库存量
}
//打印功能---2012-08-10 添加 8-14 修改 edit by dyg
function DoPrint()
{ 
orderNo=$("#txtOrderNo").val()
//  alert(orderNo);
  if(document.getElementById("txtOprtID").value=="")
  {
     popMsgObj.ShowMsg("请保存后打印");
     return;
  }  
  else
  {
 window.open("../../PrinttingModel/MedicineManager/orderForPrint3.aspx?id="+document.getElementById("hiddOrderID").value);
  }

}
///----------------------新建订单页面增加客户联系人 20121127-------------------------------------------//
function AddLinkManData()
{
  var CustNo     = $("#hfCustNo").val();//客户编号
    
    var IsDefault="0";
 
     if (document.getElementById('addLinkMan_RdIsDefault').checked) {    
              $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:"../../../Handler/Office/CustManager/CustLinkMan.ashx",//目标地址
           data:'&CustNo='+escape(CustNo),
           cache:false,
           beforeSend:function(){},//发送数据之前           
           success: function(msg){
                    //数据获取完毕
                if(msg.data==0)
                {
                  IsDefault="1";
                  SaveCustLinkMan(IsDefault);
                }
                else
                {
                //  $.each(msg.data,function(i,item){
                //  if (item.LinkManName==LinkManName)
                //  {
               //      IsDefault="1";
               //      SaveCustLinkMan(IsDefault);
                 // }
               //   else
               //   {
                    alert("该客户已经存在一个默认联系人！");
               //   }
                 // });
                }
           },
           error: function() 
           {
               
           }, 
           complete:function(){ }//接收数据完毕
           });
       
    }else
    {
        SaveCustLinkMan(IsDefault);
    }
    
 }   
    


//控制按钮状态
function fnStatus(BillStatus)
{
    try{
        switch (BillStatus) { //单据状态（1制单，2确认，3.变更）
            case '1': //制单（没保存）
             $("#imgSave").css("display", "inline");  //保存
             $("#imgUnSave").css("display", "none");
             $("#btn_confirm").css("display","none");
             $("#Imgbtn_confirm").css("display","inline");  //无法确认
             $("#UnConfirm").css("display","none");
             $("#ImgUnConfirm").css("display","inline");  //无法反确认

                break;
            case '2': //确认（以保存没确认）
                 
             $("#imgSave").css("display", "inline");  //可以保存更新 
             $("#imgUnSave").css("display", "none");  
             $("#btn_confirm").css("display","inline"); //确认
             $("#Imgbtn_confirm").css("display","none");  
             $("#UnConfirm").css("display","none");
             $("#ImgUnConfirm").css("display","inline");  //无法反确认
                               
                break;
            case '3':  //变更（已确认，可以反确认）
                $("#imgSave").css("display", "none");  
             $("#imgUnSave").css("display", "inline");  //无法保存
             $("#btn_confirm").css("display","none");
             $("#Imgbtn_confirm").css("display","inline");  //无法确认
             $("#UnConfirm").css("display","inline");   //可以反确认
             $("#ImgUnConfirm").css("display","none");  
            

        }
   }
   catch(e)
   {}
}