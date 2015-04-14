var isnew="1";//1添加;2更新
var istmpquantity="0";//0没有数量为0的订单  1有数量为0的订单


$(document).ready(function() {

  requestobj = GetRequest();
  document.getElementById("headid").value=requestobj['intMasterID'];
  if(document.getElementById("headid").value!="" && document.getElementById("headid").value!="undefined")
  {
    var BusTtype=requestobj['cBusTtype'];
    $("#sel_cBusTtype").val(BusTtype);
    ChangeBill();
    GetInfoById(document.getElementById("headid").value,BusTtype);  //获取结算单信息
    $("#labTitle_Write1").html("结算单查看");
  }
  else
  {
    $("#labTitle_Write1").html("结算单新建");
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

 //通过id和源单类型获取结算单信息
 function GetInfoById(headid,BusTtype)
    {
          var action="SearchSettleVouchOne";
          var orderBy="id";

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/Expenses/SettleVouchInfo.ashx', //目标地址
               cache: false,
               data: "action=" + action + "&orderby=" + orderBy+"&headid="+escape(headid)+"&BusTtype="+escape(BusTtype)+'',          
               beforeSend: function() {  }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
               
                   var j = 1;
                   isnew="2";
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "") {
                       $("#txtSettleCode").val(item.SettleCode);
                       $("#txtSettleCode").attr("disabled","disabled");
                       
                       $("#txthidSourceID").val(item.SourceBillID);
                       $("#txtSourceBillID").val(item.SourceBillID);
                       $("#txtSourceBillNo").val(item.SourceBillNo);
                       $("#txtPPersonID").val(item.PersonId);
                       $("#txtPPerson").val(item.PersonName);
                       $("#txtCustomerID").val(item.CustID);
                       $("#txtCustomerName").val(item.CustName);
                       $("#txtS_SPrice").val(item.S_SettelTotalPrice);
                       $("#txtS_SettelTotalPrice").val(Number(item.S_SettelTotalPrice).toFixed(2));
                       $("#txtS_SettleMoney").val(Number(item.CustJsFee).toFixed(2));
                       $("#txtS_TotalMoney").val(Number(item.S_Money).toFixed(2));
                       $("#txtProviderID").val(item.ProviderID);
                       $("#txtProviderName").val(item.ProviderName);
                       $("#txtP_SPrice").val(item.P_SettleTotalPrice);
                       $("#txtP_SettleTotalPrice").val(Number(item.P_SettleTotalPrice).toFixed(2));
                       $("#txtP_SettleMoney").val(Number(item.ProJsFee).toFixed(2));
                       $("#txtP_TotalMoney").val(Number(item.P_Money).toFixed(2));
                       $("#txtReason").val(item.cMemo);
                       $("#txtCreateDate").val(item.CreateDate);
                       $("#UserPrincipal").val(item.CreatorName);
                       $("#txtCreator").val(item.Creator);
                       $("#txtModifiedDate").val(item.ModifiedUserID);
                       $("#txtModifiedUserID").val(item.ModifiedDate);
                         

                         
                         
                         if(item.billStatus=='2') //如果是确认状态
                        {
                            $("#txtConfirmor").val(item.ConfirmorName);  //确认人姓名
                            $("#txtConfirmorId").val(item.Confirmor);  //确认人id
                            $("#txtConfirmDate").val(item.ConfirmDate);  //确认日期
                            fnStatus('3');   //按钮状态
                        }
                        else
                        {
                            fnStatus('2');   //按钮状态
                        }
                         


 
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



function FillSignRow(i,item)
{
    var txtTRLastIndex = document.getElementById("txtTRLastIndex");
    
    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("TableCoalInfo");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;
    


 
     //加载仓库
    var newFitNametd = newTR.insertCell(m); //添加列:仓库
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%' ><tr><td>"+
    "<select name='drpWare"+rowID+"' class='tddropdlist' style='width: 95%;'  runat='server' id='drpWare"+rowID+"' ></select></td></tr></table>"; //添加列内容
    m++;
    
    getWareData('drpWare'+rowID,item.StorageID);//加载仓库数据
    
     //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
    "<select name='drpCoalName"+rowID+"' disabled='disabled'  class='tddropdlist' style='width: 95%;'  runat='server' id='drpCoalName"+rowID+"' onChange='getCoalNature(this.value,"+rowID+")' ></select></td></tr></table>"; //添加列内容
    m++;
    
    getCoalData('drpCoalName'+rowID,item.ProductID);//加载煤种数据
    
    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnit" + rowID + "' value=\"" + item.unitName + "\"  maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /><input type='hidden' id='hidInBusMxId" + rowID + "'  value=\"" + item.id + "\"   /> "; //添加列内容,隐藏输入框保存销售发货单明细id
    m++;
    

    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "' value=\"" + item.ProCount + "\"   onpropertychange=\"getTotalCount();\"   onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/><input type='hidden' id='hidPreQuantity" + rowID + "'  value=\"" + item.ProCount + "\"   />"; //隐藏域hidPreQuantity用来获取保存订单时的数量
    m++;
 
    
    var newFitDesctd = newTR.insertCell(m); //添加列:实到车数
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtCarNum" + rowID + "' value=\"" + item.CarNum + "\"     onkeyup='return ValidateNumber(this,value)'     type='text' class='tdinput' style=' width:90%;'/>";
    m++;
 

//    var newFitDesctd = newTR.insertCell(m); //添加列:销售单价
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtSaleCost" + rowID + "' value=\"" + Number(item.TaxPrice).toFixed(2) + "\"   onpropertychange=\"getMoney("+rowID+");\"  onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  />"; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:税率
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' value=\"" + Number(item.TaxRate).toFixed(2) + "\" onpropertychange=\"getMoney("+rowID+");\"   maxlength='10' type='text'  onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:税额
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtTax" + rowID + "' value=\"" + Number(item.TotalTax).toFixed(2) + "\"   maxlength='10' type='text' onkeyup='return ValidateNumber(this,value)' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
//    
//    var newFitDesctd = newTR.insertCell(m); //添加列:金额
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' value=\"" + Number(item.TotalFee).toFixed(2) + "\"    onkeyup='return ValidateNumber(this,value)' maxlength='10' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
//    m++;
      
    
     var newFitDesctd = newTR.insertCell(m); //添加列:发货总数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTotalNum" + rowID + "' value=\"" + item.TotalNum + "\"    type='text' class='tdinput'  disabled='disabled'  style='width:90%;'/>"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:未入库数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnInNum" + rowID + "' value=\"" + item.ProductCount + "\"    type='text' class='tdinput'  disabled='disabled'  style='width:90%;'/>"; //添加列内容
    m++;
    //getTotalCount();  //获取总的出库数量
    txtTRLastIndex.value = rowID; //将行号推进下一行  
}


//加载煤种下拉列表
function getCoalData(selectid,b)
{
     $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractList.ashx?action=getcoaldata', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                  var seltext=item.ProductName;
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



//绑定仓库下拉列表
function getWareData(selectid,b)
{
     $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Common/WareInfo.ashx?action=getwaredata', //目标地址
        cache: false,
        data: '' , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {
                  var seltext=item.StorageName;
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



//获取总的出库数量
function  getTotalCount()
{
    var summoney=0;
     for(var i=0;i<100;i++)
     {
       if(document.getElementById("txtQuantity"+i)!="undefined" && document.getElementById("txtQuantity"+i)!=null)
       {
            summoney=summoney+Number(document.getElementById("txtQuantity"+i).value);
       }
     }
     document.getElementById("txtInNum").value=Number(summoney).toFixed(2);
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
//        debugger;
//        alertdiv('UserSeller,Seller');
//   
//}
////选择采购联系人
//function fnSelectSeller2() {
//   debugger;
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

//选择来源单号
function fnFromBillNo()
{
    var selectedValue=$("#sel_cBusTtype").val();
    if(selectedValue=="1")  //直销
    {
        fnSelectOutBusiness('1');  //获取发货（出库）信息自定义控件
    }
    else if(selectedValue=="2") //采购到货
    {
        fnSelectInBus();  //获取到货信息自定义控件
    }
    else if(selectedValue=="3")  //采购直销
    {
        fnSelectOutBusiness('2');  //获取发货单（采购直销）信息自定义控件
    }
    else
    {
        alert("请选择来源类型！");
    }

}

//从到货信息自定义控件传值
function GetInBus(ID,ArriveNo,ProviderID,providername,diaoyunid,diaoyunno,ProductID,productname,ProductCount,TotalFee,ProJsFee)
{  

    $("#txtSourceBillID").val(ID);
    $("#txtSourceBillNo").val(ArriveNo);
    $("#txtProviderID").val(ProviderID);
    $("#txtProviderName").val(providername);
    $("#txtP_SettleMoney").val(Number(ProJsFee).toFixed(2));    //采购已结算金额
    $("#txtP_TotalMoney").val(Number(TotalFee).toFixed(2));   //采购总金额
    
    document.getElementById('HolidaySpan_InBus').style.display = "none";
    closeRotoscopingDiv(false,"divJTNameS_InBus");//关闭遮罩层
    
    

    
}

//从发货单信息自定义控件传值
function GetOutBus(id,sendno,settletype,transporttype,custid,custname,billunit,sendnum,ppersonid,ppersonname,deptid,deptname,transmoney,diaoyunid,diaoyunno,transstate,carno,startstation,endstation,carnum,at_state,providerid,providername,CustJsFee,ProJsFee,SellMoney,ProMoney)
{  
    
    $("#txtSourceBillID").val(id);
    $("#txtSourceBillNo").val(sendno);
    $("#txtCustomerID").val(custid);
    $("#txtCustomerName").val(custname);
    $("#txtS_SettleMoney").val(Number(CustJsFee).toFixed(2));  //销售已结算金额
    $("#txtS_TotalMoney").val(Number(SellMoney).toFixed(2));  //销售总金额
    
    var selectedValue=$("#sel_cBusTtype").val();
    if(selectedValue=="3")  //采购直销 
    {
        $("#txtProviderID").val(providerid);
        $("#txtProviderName").val(providername); 
        $("#txtP_SettleMoney").val(Number(ProJsFee).toFixed(2));       //采购已结算金额
        $("#txtP_TotalMoney").val(Number(ProMoney).toFixed(2));       //采购总金额
    }
    document.getElementById('HolidaySpan_OutBus').style.display = "none";
    closeRotoscopingDiv(false,"divJTNameS_OutBus");//关闭遮罩层

    
}

//改变订单类型时
function ChangeBill()
{
    var selectedValue=$("#sel_cBusTtype").val();
    $("#txtSourceBillID").val("");
    $("#txtSourceBillNo").val("");
    $("#txtCustomerID").val("");
    $("#txtCustomerName").val("");
    $("#txtS_SettelTotalPrice").val("");
    $("#txtS_SettleMoney").val("");
    $("#txtS_TotalMoney").val("");
    $("#txtProviderID").val("");
    $("#txtProviderName").val("");
    
    $("#txtP_SettleTotalPrice").val("");
    $("#txtP_SettleMoney").val("");
    $("#txtP_TotalMoney").val("");
    
    if(selectedValue=="1")  //直销
    {
        $("#id_Provider").css("display","none");
        $("#id_Customer").css("display","");
    }
    else if(selectedValue=="2") //采购到货
    {
        $("#id_Provider").css("display","");
        $("#id_Customer").css("display","none");
    }
    else if(selectedValue=="3")  //采购直销
    {
        $("#id_Provider").css("display","");
        $("#id_Customer").css("display","");
    }
}


//保存
function SaveSellOrder()
{
    var isconfirm="";
    var strAction="";
    
    isconfirm="0";
    
    if($("#txtSettleCode").val()=="" )
    {
       popMsgObj.ShowMsg("单据编号不能为空，请填写！");
       return;
    }
    if($("#sel_cBusTtype").val()=="0")
    {
        popMsgObj.ShowMsg("请选择来源单据类型！");
        return;
    }
    if(document.getElementById("txtSourceBillNo").value=="")
    {
       popMsgObj.ShowMsg("来源单号不能为空，请选择！");
       return;
    }
    if($("#txtPPerson")=="")
    {
       popMsgObj.ShowMsg("经办人不能为空，请选择！");
       return;
    }
    
    var S_SetPrice=$("#txtS_SettelTotalPrice").val();  //销售结算金额
    var S_SettleMoney=$("#txtS_SettleMoney").val();
    var S_TotalMoney=$("#txtS_TotalMoney").val();
    if($("#id_Customer").css("display")!="none" && S_SetPrice=="")
    {
       popMsgObj.ShowMsg("本次结算不能为空，请填写！");
       return;
    }
    if(Number(S_SetPrice)>(Number(S_TotalMoney)-Number(S_SettleMoney)))
    {
       popMsgObj.ShowMsg("销售本次结算金额不能大于"+(Number(S_TotalMoney)-Number(S_SettleMoney))+"，请重新填写！");
       return;
    }

    var P_SetPrice=$("#txtP_SettleTotalPrice").val();   //采购结算金额
    var P_SettleMoney=$("#txtP_SettleMoney").val();
    var P_TotalMoney=$("#txtP_TotalMoney").val();
    if($("#id_Provider").css("display")!="none" && P_SetPrice=="")
    {
       popMsgObj.ShowMsg("本次结算不能为空，请填写！");
       return;
    }
    
    if(Number(P_SetPrice)>(Number(P_TotalMoney)-Number(P_SettleMoney)))
    {
       popMsgObj.ShowMsg("采购本次结算金额不能大于"+(Number(P_TotalMoney)-Number(P_SettleMoney))+"，请重新填写！");
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
            url: "../../../Handler/JTHY/Expenses/DealSettleVouch.ashx",
            data: strInfo +'&action=insert&headid='+escape(headid)+'&isconfirm='+escape(isconfirm),
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
                      $("#txtSettleCode").val(data.data);
                      $("#txtSettleCode").attr("disabled","disabled");
                      

                      $("#hidBillStatus").val('1'); 
                      var d, s=""; 
                        d = new Date(); //Create Date object. 
                        s += d.getYear()+"-"; 
                        s += (d.getMonth() + 1) + "-"; //Get month 
                        s += d.getDate() + " "; //Get day 

                        $("#txtModifiedDate").val(s);
                        
                        fnStatus('2');  //控制按钮状态
                        var S_SPrice=$("#txtS_SettelTotalPrice").val();  //记录保存的销售本次结算金额
                        $("#txtS_SPrice").val(S_SPrice);
                        
                        var P_SPrice=$("#txtP_SettleTotalPrice").val(); //记录保存的采购本次结算金额
                        $("#txtP_SPrice").val(P_SPrice);
                        
                        var hidSourceID=$("#txtSourceBillID").val(); //记录保存时的源单id
                        $("#txthidSourceID").val(hidSourceID);
                        
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

//保存的数量
function fnChangeNumber()
{
    var signFrame = findObj("TableCoalInfo", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].id;
            var preCount=$("#txtQuantity" + rowid).val();  //原来保存的煤种的数量
            if(preCount=="")
            {
                preCount="0.00";
            }
            $("#hidPreQuantity"+rowid).val(preCount);
            
        }
    }
}



//获取主表信息
function fnGetInfo() {
    var strInfo = '';
      
    
    var SettleCode=$("#txtSettleCode").val();   //单据编号
    var sel_cBusTtype=$("#sel_cBusTtype").val();  //来源类型
   
    
    var SourceBillID=$("#txtSourceBillID").val();//  来源订单ID
    var SourceBillNo=$("#txtSourceBillNo").val();//  来源订单编码
    var PPersonID=$("#txtPPersonID").val();  //经办人id
    var CustomerID=$("#txtCustomerID").val();  //客户id
 
    
    
    var S_SettelTotalPrice=$("#txtS_SettelTotalPrice").val();   //销售本次结算

    var ProviderID=$("#txtProviderID").val();//  供应商ID

    
    var P_SettleTotalPrice=$("#txtP_SettleTotalPrice").val();   //采购本次结算
    
    var Reason=$("#txtReason").val();  //备注



                                                                                                                                             
    strInfo = 'SettleCode='+escape(SettleCode)+'&sel_cBusTtype='+escape(sel_cBusTtype)+'&SourceBillID='+escape(SourceBillID)+'&SourceBillNo='+escape(SourceBillNo)
    +'&PPersonID='+escape(PPersonID)+'&CustomerID='+escape(CustomerID)+
    '&S_SettelTotalPrice='+escape(S_SettelTotalPrice)+'&ProviderID='+escape(ProviderID)
    +'&P_SettleTotalPrice='+escape(P_SettleTotalPrice)+'&Reason='+escape(Reason)+'';
            
    return strInfo;
}
//获取明细数据
function getDropValue() {


    var SendOrderFit_Item = new Array();
    var signFrame = findObj("TableCoalInfo", document);
    var j = 0;
    IsMore=false;
    

    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            j = j + 1;
            var rowid = signFrame.rows[i].id;                  
            var WareID = $("#drpWare" + rowid).val();           //仓库id   
            var CoalID = $("#drpCoalName" + rowid).val();           //煤种id     
            var Quantity = $("#txtQuantity" + rowid).val();           //数量
            var hidInBusMxId=$("#hidInBusMxId" + rowid).val();   //销售发货单明细ID 
            
            var CarNum=$("#txtCarNum"+rowid).val();    //实到车数
            
            
            SendOrderFit_Item[j] = [[WareID], [CoalID],[Quantity],[hidInBusMxId],[CarNum]];

        }
        
    }
   
   
    return SendOrderFit_Item;
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
  


//----------------------------------------------end-------------------------------------------------------------------//



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
            var hidInBusMxId=$("#hidInBusMxId" + rowid).val();   //采购到货单明细ID
            
            
            SendOrderFit_Item[j] = [[Quantity], [hidInBusMxId]];

        }
        
    }
   
   
    return SendOrderFit_Item;
}


//确认订单
function  Fun_ConfirmOperate()
{
    UpdateCount();//先获取采购到货单明细中未入库的数量
    
    var P_SMoney= $("#txtP_SettleMoney").val();  //采购已结算金额
    var P_SPrice= $("#txtP_SPrice").val();      //采购本次结算金额
    var P_TMoney= $("#txtP_TotalMoney").val();  //采购总金额
    var S_SMoney= $("#txtS_SettleMoney").val();  //销售已结算金额
    var S_SPrice= $("#txtS_SPrice").val();      //销售本次结算金额
    var S_TMoney= $("#txtS_TotalMoney").val();  //销售总金额
    
    
    if(Number(P_SPrice)>(Number(P_TMoney)-Number(P_SMoney)))
    {
        popMsgObj.ShowMsg("采购结算金额不能多于"+(Number(P_TMoney)-Number(P_SMoney)).toFixed(2)+"，请重新保存！");
        return;
    }
     if(Number(S_SPrice)>(Number(S_TMoney)-Number(S_SMoney)))
    {
        popMsgObj.ShowMsg("销售结算金额不能多于"+(Number(S_TMoney)-Number(S_SMoney)).toFixed(2)+"，请重新保存！");
        return;
    }

    
    
    var c=window.confirm("确定执行确认操作吗？");
    if(c==true)
    {
        var headid=$("#headid").val();
        var sourceType=$("#sel_cBusTtype").val();//源单类型
        var hidSourceID=$("#txthidSourceID").val(); //源单id
        action ="ConfirmSettleVouch";
             


        var strParams = "action=" + action + "&headid=" + headid  +"&P_SPrice=" + P_SPrice  +"&S_SPrice=" + S_SPrice  +"&sourceType=" + sourceType +"&hidSourceID=" + hidSourceID +'';
        $.ajax({ 
            type: "POST",
            url:"../../../Handler/JTHY/Expenses/DealSettleVouch.ashx",
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
                    UpdateCount();  //更新页面的未出库数量
                    
                     
                }  
                else
                {
                      popMsgObj.ShowMsg(data.info);
                }
                 hidePopup();
              } 
           });   
               
      }
}


//获取
function UpdateCount()
{          
    var ID= $("#txtSourceBillID").val();//获取采购到货单id
    var sourceType=$("#sel_cBusTtype").val();  //来源类型
    //////////////////////////////////////////////////
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/SettleVouchInfo.ashx', //目标地址
        cache: false,
        data: 'action=getSettleMoney&ID='+escape(ID)+'&sourceType='+escape(sourceType) , //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            $.each(msg.data, function(i, item) {
                $("#txtP_SettleMoney").val(Number(item.ProJsFee).toFixed(2));  //采购已结算金额
                $("#txtP_TotalMoney").val(Number(item.ProMoney).toFixed(2));
                $("#txtS_SettleMoney").val(Number(item.CustJsFee).toFixed(2));  //销售已结算金额
                $("#txtS_TotalMoney").val(Number(item.SellMoney).toFixed(2));
                
            });
            
        },
        complete: function() { } //接收数据完毕
    });
    
//////////////////////////////////////
}

//反确认
function cancelConfirm()
{

    var c=window.confirm("确定执行取消确认操作吗？");
    if(c==true)
    {
        var P_SPrice= $("#txtP_SPrice").val();      //采购本次结算金额
        var S_SPrice= $("#txtS_SPrice").val();      //销售本次结算金额
        var sourceType=$("#sel_cBusTtype").val();//源单类型
        var hidSourceID=$("#txthidSourceID").val(); //源单id
        var headid=$("#headid").val();

        action ="CancelConfirmSettleVouch";
             

        var strParams = "action=" + action + "&headid=" + headid  +"&P_SPrice=" + P_SPrice  +"&S_SPrice=" + S_SPrice  +"&sourceType=" + sourceType +"&hidSourceID=" + hidSourceID +'';
        $.ajax({ 
            type: "POST",
            url:"../../../Handler/JTHY/Expenses/DealSettleVouch.ashx",
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
                    UpdateCount();  //更新页面的未出库数量
                     
                }  
                else
                {
                    popMsgObj.ShowMsg('取消确认失败！');
                }
                 hidePopup();
              } 
           });   
               
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