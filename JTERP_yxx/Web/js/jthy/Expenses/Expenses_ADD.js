var isnew="1";//1添加;2保存
var istmpquantity="0";//0没有数量为0的订单  1有数量为0的订单
$(document).ready(function() {

   requestobj = GetRequest();
  document.getElementById("hiddenId").value=requestobj['id'];
    if(document.getElementById("hiddenId").value!="" && document.getElementById("hiddenId").value!="undefined")
  {
    GetInfoById(document.getElementById("hiddenId").value);
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

 function GetInfoById(headid)
    {
          var action="SearchOne";
          var orderBy="id";

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/Expenses/Expenses_List.ashx', //目标地址
               cache: false,
               data: "id="+escape(headid)+'&action='+escape(action)+'',          
               beforeSend: function() {AddPop(); }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
               
                   var j = 1;
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "") {
                         isnew="2";
                          document.getElementById("divExpCode").innerHTML =item.ExpCode;
                          $("#divExpCode").css("display","block");
                          $("#divCodeRule").css("display","none");
                    
                         document.getElementById("txtPPerson").value=item.ApplyorName;  
                         document.getElementById("DeptName").value=item.DeptName; 
                         document.getElementById("txtTotalAmount").value=item.TotalAmount; 
                         
                         document.getElementById("txtEffectiveDate").value=item.AriseDate; 
                           document.getElementById("ddlExpType").value=item.ExpType; 
                             document.getElementById("txtCustomerName").value=item.CustName; 
                               document.getElementById("ddlPayType").value=item.PayType; 
                                 document.getElementById("UserLinker").value=item.TransactorName; 
                                   document.getElementById("txtReason").value=item.Reason; 
                                     document.getElementById("txtCreateDate").value=item.CreateDate; 
                                       document.getElementById("UserPrincipal").value=item.CreatorName; 
                                         document.getElementById("txtModifiedDate").value=item.ModifiedDate; 
                                           document.getElementById("txtModifiedUserID").value=item.ModifiedUserID; 
                                
                             document.getElementById("txtPPersonID").value=item.Applyor;  //申请人ID
                              document.getElementById("hdDeptID").value=item.DeptID; //申请部门ID
                               document.getElementById("txtCustomerID").value=item.CustID; //来往单位ID
                               document.getElementById("txtJoinUser").value=item.TransactorID; //经办人ID
                               

                            if(item.Status=="2")//已经确认
                            {
                                 document.getElementById("txtConfirmor").value=item.ConfirmorName; //确认人姓名
                                 document.getElementById("txtConfirmDate").value=item.ConfirmDate; //确认时间
                                 $("#hidBillStatus").val('2');
                        

                                $("#imgUnSave").css("display", "inline");
                                $("#btn_save").css("display", "none");
                           
                                $("#exp_unsure").css("display","inline");
                                $("#exp_sure").css("display","none");
                                
                                $("#txtConfirmor").css("display","block");
                                
                                $("#txtConfirmDate").css("display","block");
                           }
                           if(item.Status=="1")//没有确认
                           {
                                $("#btn_save").css("display", "inline");
                                $("#imgUnSave").css("display", "none");
                           
                                $("#exp_sure").css("display","inline");
                                $("#exp_unsure").css("display","none");
                                
                                $("#txtConfirmDate").css("display","block");
                                
                                $("#txtConfirmor").css("display","block");
                           }
                              
                                  

                         
                          
                       }
                   });
                   
                
                  
                   
               },
               error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
               complete: function() {hidePopup(); } //接收数据完毕
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

function getCoalNature(coalvalue)
{
     $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractList.ashx?str='+escape(coalvalue)+'&action=getprodcutinfo', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                                                                  
                 $("#txtSpecialName1").val(item.specification); //质量热卡
                 $("#txtUnitID1").val(item.unitid); //计量单位
                 $("#txtUnitName1").val(item.unitname); //计量单位 
                    
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

    var colProductNo = newTR.insertCell(m); //添加列:物品编号
    colProductNo.className = "cell";
    colProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
                                "<tr><td><input id=\"ProductNo" + rowID + "\" class=\"tdinput\" title=\"\" readonly style=' width:95%;COLOR: #848284 '><input type=\"hidden\"  id='hiddProductNo" + rowID + "'/></td>" +
                             "</tr></table>";
    m++;

    var newFitNametd = newTR.insertCell(m); //添加列:物品名称
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><input id='ProductName" + rowID + "'  disabled='disabled'  style=' width:95%; ' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;
    


    var newFitDesctd = newTR.insertCell(m); //添加列:规格
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><input  id='Specification" + rowID + "'  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;

    //---------最新版本需要添加此语句----20121213--------//
    //var newProductor= newTR.insertCell(m); //添加列:生产厂家
    //newProductor.className = "cell";
    //newProductor.innerHTML = "<div  id='newProductor" + rowID + "'  style=' width:95%;'></div>"; //添加列内容
    //m++;
   //--------------END----------------------------------//

    var newFitDesctd = newTR.insertCell(m); //添加列: 限销区域
    newFitDesctd.className = "cell";
    var id="#LimitSellArea" + rowID;
    newFitDesctd.innerHTML = "<input id='LimitSellArea" + rowID + "'   type='text' class='tdinput' style=' width:90%;' onclick=\"showAreaSelect('"+id+"')\" />"; //添加列内容
    m++;

     //---------最新版本需要添加此语句----20121213--------//
    var newStock= newTR.insertCell(m); //添加列:库存数量
    newStock.className = "cell";
    newStock.innerHTML = "<div  id='newStock" + rowID + "'  style=' width:95%;'></div>"; //添加列内容
    m++;
   //--------------END----------------------------------//

 
    var newFitDesctd = newTR.insertCell(m); //添加列:供货价
    newFitDesctd.className = "cell";
    //newFitDesctd.innerHTML = "<input id='GuidPrice" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;'/> <input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
    newFitDesctd.innerHTML = "<input id='GuidPrice" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;COLOR: #848284' readonly='true'/><input type=\"hidden\" value='0.00' id='hiddGuidPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseGuidPrice" + rowID + "'/>";
    //newFitDesctd.innerHTML ="<div id='orderPrice" + rowID + "'></div>";
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:开单价
    newFitDesctd.className = "cell";
    //newFitDesctd.innerHTML = "<input id='SupplyPrice" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;' onblur=\"fnTotalInfo();\"/> <input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/><input id='GuidPrice" + rowID + "' maxlength='10' type='hidden' class='tdinput' style=' width:90%;COLOR: #848284' readonly='true'/> <input type=\"hidden\" value='0.00' id='hiddGuidPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseGuidPrice" + rowID + "'/>"; //添加列内容
    newFitDesctd.innerHTML = "<input id='SupplyPrice" + rowID + "' maxlength='10' type='text' class='tdinput' style=' width:90%;' onblur=\"fnTotalInfo();\"/> <input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='ProductCount" + rowID + "' onblur=\"fnTotalInfo();\"   maxlength='10' type='text'  class='tdinput'  style=' width:90%;' />"; //添加列内容
    m++;   
    
    var newFitDesctd = newTR.insertCell(m); //添加列:折扣额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='DiscountPrice" + rowID + "'onblur=\"fnTotalInfo();\"  maxlength='10' type='text' class='tdinput' style=' width:90%;'/> <input type=\"hidden\" value='0.00' id='hiddDiscountPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseDiscountPrice" + rowID + "'/>"; //添加列内容
    m++;

    
    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='TotalPrice" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列: 备注
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='Remark" + rowID + "' type='text' class='tdinput'  maxlength='100' style=' width:94%;'/>"; //添加列内容
    m++;
    
        //---------最新版本需要添加此语句----20121213--------//
    var newSell= newTR.insertCell(m); //添加列:促销政策
    newSell.className = "cell";
    newSell.innerHTML = "<div  id='newSell" + rowID + "'  style=' width:95%;'></div>"; //添加列内容
    m++;
    //---------------------------end--------------------//

    txtTRLastIndex.value = rowID; //将行号推进下一行  
    
    
    }


//计算各种合计信息
function fnTotalInfo() {
  var TotalPrice = 0; //金额合计
    var CountTotal = 0; //数量合计
    var DisPrice=0;//折扣额合计
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
function SaveSellOrder()
{
    var isconfirm="";
    
    
    isconfirm="0";
    
    //$("#hidBillStatus").val('1');
     //--------------20121118-------------//
    //$("#Imgbtn_confirm").css("display","inline");
    //$("#btn_confirm").css("display","none");
    //----------------------------------------//
   
 
//    if (!CheckInput()) {
//        return;
//    }
    
    
    if($("#ddlExpCode_txtCode").val()==""  &&  $("#ddlExpCode_ddlCodeRule").val()=="")
    {
       popMsgObj.ShowMsg("单据号不能为空，请填写！");
       return;
    }
    if(document.getElementById("txtPPersonID").value=="")
    {
       popMsgObj.ShowMsg("申请人不能为空，请填写！");
       return;
    }
//     if(document.getElementById("drpSettleType").value=="")
//    {
//       popMsgObj.ShowMsg("结算方式不能为空，请填写！");
//       return;
//    }
//     if(document.getElementById("drpTransPortType").value=="")
//    {
//       popMsgObj.ShowMsg("运输类型不能为空，请填写！");
//       return;
//    }
     if(document.getElementById("txtTotalAmount").value=="")
    {
       popMsgObj.ShowMsg("合同金额不能为空，请填写！");
       return;
    }
    if(!Isfloat($("#txtTotalAmount").val()))
    {
        $("#txtTotalAmount").val("")
        popMsgObj.ShowMsg("合同金额的格式错误，请重写！");
       return;
    }
    
    if(document.getElementById("txtEffectiveDate").value=="")
    {
       popMsgObj.ShowMsg("申请日期不能为空，请填写！");
       return;
    }
//    var PPersonID= $("#txtPPersonID").val();
//    var JoinUser=$("#txtJoinUser").val();
//    if( PPersonID!="" && JoinUser!=""&& PPersonID==JoinUser)
//    {
//        popMsgObj.ShowMsg("经办人不能与申请人相同！");
//        return;
//    }
    
     
//    if(document.getElementById("drpCoalType1").value=="" || document.getElementById("drpCoalType1").value=="0")
//    {
//         popMsgObj.ShowMsg("请选择煤种！");
//         return;
//    }
//    if(document.getElementById("txtQuantity1").value=="")
//    {
//         popMsgObj.ShowMsg("数量不能为空，请填写！");
//         return;
//    }
//    if(document.getElementById("txtUnitCost1").value=="")
//    {
//         popMsgObj.ShowMsg("单价不能为空，请填写！");
//         return;
//    }
//    if(document.getElementById("txtMoney1").value=="")
//    {
//         popMsgObj.ShowMsg("金额不能为空，请填写！");
//         return;
//    }
 //  var strfitinfo = getDropValue().join("|");
 
             var strInfo = fnGetInfo();   
//             if(istmpquantity=="1")
//             {
//                 istmpquantity="0";
//                 popMsgObj.ShowMsg("订单数量为0不可保存！");
//                 return;
//             }
//            var headid=document.getElementById("headid").value;
//            if(headid=="undefined" || headid=="null" || headid=="")
//            {
//                headid="0";
//            }
//    
            var ID='0';
            var action='insert';
            if(isnew=="2")
            {
                ID=$("#hiddenId").val();
                action='update';
            }
            
            $.ajax({
                type: "POST",
                url: "../../../Handler/JTHY/Expenses/Expenses_ADD.ashx",
                data: strInfo + '&action='+escape(action)+'&Status=1&ID='+escape(ID),
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    AddPop();
                },
                error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
                success: function(data) {
                
                 
                    //isSave="1";
                    
                    if (data.sta > 0) {
                         isnew="2";
                          $("#hiddenId").val(data.sta);
                          document.getElementById("divExpCode").innerHTML =data.data;
                          $("#divExpCode").css("display","block");
                          $("#divCodeRule").css("display","none");

                          $("#hidBillStatus").val('1');
                          $("#exp_sure").css("display","inline");
                          $("#exp_unsure").css("display","none");
                          var d, s=""; 
                            d = new Date(); //Create Date object. 
                            s += d.getYear()+"-"; 
                            s += (d.getMonth() + 1) + "-"; //Get month 
                            s += d.getDate() + " "; //Get day 

                            $("#txtModifiedDate").val(s);
 
                        popMsgObj.ShowMsg("保存成功！");
                      }
                      else
                      {
                        popMsgObj.ShowMsg("保存失败！");
                      }
                      hidePopup();
                    
                }
            });
   
}




//获取主表信息
function fnGetInfo() {
    var strInfo = '';
    
    
    var ExpCodeType=$("#ddlExpCode_ddlCodeRule").val();  //编号类型
    var ExpCode="";   //单据编号
    if(ExpCodeType==""){
        ExpCode=$("#ddlExpCode_txtCode").val();  //手动输入时，获取单据编号    
    }
    
    if(isnew=="2"){        //如果是更新
        ExpCode=$("#divExpCode").text();
    }
    var Applyor=$("#txtPPersonID").val();  //申请人
    var DeptID=$("#hdDeptID").val();   //部门
    var TotalAmount=$("#txtTotalAmount").val();  //申请金额
    var AriseDate =$("#txtEffectiveDate").val();  //申请日期
    var ExpType=$("#ddlExpType").val();  //费用类别
    var CustID=$("#txtCustomerID").val(); //往来单位
    var PayType=$("#ddlPayType").val();  //支付方式
    var TransactorID=$("#txtJoinUser").val();  //经办人
    var Reason=$("#txtReason").val();  //备注
    var CreateDate= $("#txtCreateDate").val();  //建档日期
    var Creator= $("#txtCreator").val();  //建档人ID
    var ModifiedDate=$("#txtModifiedDate").val();  //最后更新日期
    var ModifiedUserID=$("#txtModifiedUserID").val();  //最后更新用户ID
    


                                                                                                                                             
    strInfo = 'ExpCodeType='+escape(ExpCodeType)+'&ExpCode='+escape(ExpCode)+'&Applyor='+escape(Applyor)
    +'&DeptID='+escape(DeptID)+'&TotalAmount='+escape(TotalAmount)+'&AriseDate='+escape(AriseDate)+
    '&ExpType='+escape(ExpType)+'&CustID='+escape(CustID)+'&PayType='+escape(PayType)+'&TransactorID='+escape(TransactorID)+
    '&Reason='+escape(Reason)+'&CreateDate='+escape(CreateDate)+'&Creator='+escape(Creator)+
    '&ModifiedDate='+escape(ModifiedDate)+'&ModifiedUserID='+escape(ModifiedUserID);
            
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
            var Remark = $("#Remark" + rowid).val();           //备注                            
            var ProductNo = $("#hiddProductNo" + rowid).val();   //物品编码（对应物品表编码）
            var ProductCount = $("#ProductCount" + rowid).val(); //订购数量  
            var TotalPrice = $("#TotalPrice" + rowid).val(); //金额
            var UnitPrice = $("#UnitPrice" + rowid).val();    //单价
            var GuidPrice=$("#GuidPrice"+rowid).val();
            var SupplyPrice=$("#SupplyPrice"+rowid).val();
            var DiscountPrice=$("#DiscountPrice"+rowid).val();
            var LimitSellArea=$("#LimitSellArea"+rowid).val();
            SendOrderFit_Item[j] = [[ProductNo], [ProductCount],[UnitPrice], [TotalPrice],[Remark],[GuidPrice],[SupplyPrice],[DiscountPrice],[LimitSellArea]];

        }
        if(ProductCount=="" || ProductCount=="0" || ProductCount=="0.00")
        {
        istmpquantity="1";
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
//确认订单
function  ChangeStatus()
{
//     if($("#hidBillStatus").val()!="1")
//      {
//         showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "非制单状态的单据不能确认！");
//         return;
//      }
      
//      var ufuserid=document.getElementById('ufuserid').value;
//      if(ufuserid=="")
//      {
//         showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "职员档案未做T6用户对照表，无法确认！");
//         return;
//      } 

      if (confirm("是否确认申请？")) {
      //----------------------20121119------------------------//
//           $("#Imgbtn_confirm").css("display","inline");
//           $("#btn_confirm").css("display","none");
//           $("#imgUnSave").css("display","inLine");
//           $("#btn_save").css("display","none");
      //------------------------------------------------------//
      
        var ID=$("#hiddenId").val();
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/Expenses/Expenses_ADD.ashx",
            data: fnGetInfo() + '&action=confirm&Status=2&ID='+escape(ID)+'',
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
            success: function(data) {
                if (data.sta>0) {
                 var str=data.data.split(',');
                    $("#hiddenId").val(data.sta );

                    //$("#billstatus").val("确认");
                    $("#hidBillStatus").val('2');
                    

                    $("#imgUnSave").css("display", "inline");
                    $("#btn_save").css("display", "none");
               
                    $("#exp_unsure").css("display","inline");
                    $("#exp_sure").css("display","none");
                    
                    document.getElementById("divExpCode").innerHTML =str[0];
                    $("#divExpCode").css("display","block");
                    $("#divCodeRule").css("display","none");
                       
                       $("#txtConfirmor").css("display","block");
                       $("#txtConfirmor").val(str[1]);
                    
                    $("#txtConfirmDate").css("display","block");
                    var d, s="" ; 
                            d = new Date(); //Create Date object. 
                            s += d.getYear()+"-"; 
                            s += (d.getMonth() + 1) + "-"; //Get month 
                            s += d.getDate() + " "; //Get day 
                       $("#txtConfirmDate").val(s);
                       $("#txtModifiedDate").val(s);
                }
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif",data.info);

            }
        });
    }
}

//反确认
function UnConfirm()
{

    if (confirm("是否反确认？")) {
    //--------------------20121119---------------------------//
         $("#Imgbtn_confirm").css("display","none");
        $("#btn_confirm").css("display","inLine");
           $("#imgUnSave").css("display","none");
        //-----------------------------------------//
//        var ID=$("#hiddOrderID").val();
//        $.ajax({
//            type: "POST",
//            url: "../../../Handler/Office/MedicineManager/DealCustOrder.ashx",
//            data: '&action=UnConfirm&BillStatus=1&ID='+escape(ID),
//            dataType: 'json', //返回json格式数据
//            cache: false,
//            beforeSend: function() {
//                AddPop();
//            },
//            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
//            success: function(data) {
//                if (data.sta == 1) {
//                    $("#hiddOrderID").val(data.id);

//                    $("#billstatus").val("确认");
//                    $("#hidBillStatus").val('1');
//                    $("#hidStatus").val('0');
//                    $("#hidSendStatus").val('0');
//                    $("#imgUnSave").css("display", "inline");
//                    $("#btn_save").css("display", "none");
//                      fnGetOrderInfo(data.id);
//                }
//                else {
//                    hidePopup();
//                }
//                hidePopup();
//                if (data.data.length != 0) {
//                     showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif",data.data+data.Msg);
//                }
//                else {
//                      showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.Msg);
//                }
//            }
//        });

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
            }
        }
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
    function SaveCustLinkMan(isde)
{
    var IsDefault=isde;
    var CustNo     = $("#addLinkMan_LtoCusNo").val();//对应客户编号
    var LinkManName= $("#txtLinkManName").val();//联系人姓名
    var Sex        = $("#seleSex").val();//性别
    var Important  = $("#seleImportant").val();//重要程度
    var Company    ="";
    var Appellation="";
    var Department = "";
    var Position   ="";
    var Operation  ="";
    var WorkTel    = $("#txtWorkTel").val();//工作电话
    var Fax        = $("#txtFax").val();//传真号
    var Handset    = $("#txtHandset").val();//手机号
    var MailAddress= "";
    var HomeTel    ="";
    var MSN        = "";
    var QQ         = "";
    var Post       = "";
    var HomeAddress= $("#txtHomeAddress").val();//住址
    var Remark     = "";
    var Age        = "";
    var Likes      = "";
    var LinkType   = $("#addLinkMan_ddlLinkType").val();
    var Birthday   = "";
    var PaperType  = "";
    var PaperNum   ="";
    var ProfessionalDes = "";
     
    var Photo      = "";//$("#txtPhoto").val();
    var LinkID = "";//第一次保存后返回的联系人ID
    var CanUserName = "";//可查看该客户档案的人员
    var CanUserID = "";//可查看该客户档案的人员姓名
    
    var HomeTown="";
    var NationalID="";
    var Birthcity="";
    var CultureLevel="";
    var Professional="";
    var GraduateSchool="";
    var IncomeYear="";
    var FuoodDrink="";
    var LoveMusic="";
    var LoveColor="";
    var LoveSmoke="";
    var LoveDrink="";
    var LoveTea="";
    var LoveBook="";
    var LoveSport="";
    var LoveClothes="";
    var Cosmetic="";
    var Nature="";
    var Appearance="";
    var AdoutBody="";
    var AboutFamily="";
    var Car="";
    var LiveHouse="";
     var Skype="";
     $.ajax({
        type:"POST",
        url: "../../../Handler/Office/CustManager/LinkManAdd.ashx",
        data:'CustNo='+reescape(CustNo)+    
            '&HomeTown='+escape(HomeTown)+
            '&NationalID='+escape(NationalID)+
            '&Birthcity='+escape(Birthcity)+
            '&CultureLevel='+escape(CultureLevel)+
            '&Professional='+escape(Professional)+
            '&GraduateSchool='+escape(GraduateSchool)+
            '&IncomeYear='+escape(IncomeYear)+
            '&FuoodDrink='+escape(FuoodDrink)+
            '&LoveMusic='+escape(LoveMusic)+
            '&LoveColor='+escape(LoveColor)+
            '&LoveSmoke='+escape(LoveSmoke)+
            '&LoveDrink='+escape(LoveDrink)+
            '&LoveTea='+escape(LoveTea)+
            '&LoveBook='+escape(LoveBook)+
            '&LoveSport='+escape(LoveSport)+
            '&LoveClothes='+escape(LoveClothes)+
            '&Cosmetic='+escape(Cosmetic)+
            '&Nature='+escape(Nature)+
            '&Appearance='+escape(Appearance)+
            '&AdoutBody='+escape(AdoutBody)+
            '&AboutFamily='+escape(AboutFamily)+
            '&Car='+escape(Car)+
            '&LiveHouse='+escape(LiveHouse)+
             '&LinkManName='+reescape(LinkManName)+
             '&Sex='+reescape(Sex)+
             '&Important='+reescape(Important)  +
             '&Company='+reescape(Company)    +
             '&Appellation='+reescape(Appellation)+
             '&Department='+reescape(Department) +
             '&Position='+reescape(Position)   +
             '&Operation='+reescape(Operation)  +
             '&WorkTel='+reescape(WorkTel)    +
             '&Fax='+reescape(Fax)        +
             '&Handset='+reescape(Handset)    +
             '&MailAddress='+reescape(MailAddress)+
             '&HomeTel='+reescape(HomeTel)    +
             '&MSN='+reescape(MSN)        +
             '&QQ='+reescape(QQ)         +
             '&Post='+reescape(Post)       +
             '&HomeAddress='+reescape(HomeAddress)+
             '&Remark='+reescape(Remark)     +
             '&Age='+reescape(Age)        +
             '&Likes='+reescape(Likes)      +
             '&LinkType='+reescape(LinkType)   +
             '&Birthday='+reescape(Birthday)   +
             '&PaperType='+reescape(PaperType)  +
             '&PaperNum='+reescape(PaperNum)   +
             '&Photo='+reescape(Photo)+
             '&LinkID='+reescape(LinkID)+
             '&CanUserName='+reescape(CanUserName)+
             '&CanUserID='+reescape(CanUserID)+
             '&ProfessionalDes='+reescape(ProfessionalDes)+
             '&IsDefault='+reescape(IsDefault)+
             '&Skype='+escape(Skype)+
             '&action='+reescape(isnew),
        dataType:'json',
        cache:false,
        beforeSend:function()
          { 
              AddPop();
          },
          error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");},
          success:function(data) 
            { 
                if(data.sta==1) 
                { 
                     isnew="2";  
                     document.getElementById("hfLinkmanID").value = data.data;
                     document.getElementById("txtUcLinkMan").value=document.getElementById("txtLinkManName").value;
                     document.getElementById("txtTelphone").value=Handset;
                     document.getElementById("ReceiveAddress").value=HomeAddress;
                     //document.getElementById("CustID").onclick = function(){ return false;}
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                } 
                else 
                { 
                  hidePopup();
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
                } 
            }
      });
}


//----------------------------------------------end-------------------------------------------------------------------//

function changePageStyle()
{
 //parent.addTab(null, '21305', '新建订单', 'Pages/Office/MedicineManager/NewOrderDemo.aspx?ModuleID=21305');
 window.open("NewOrderDemo.aspx?ModuleID=21305");

}