
/*编辑参数设置*/
/*
条码：默认启用
多计量单位：默认停用
自动生成凭证：默认停用
自动审核登帐：默认停用
超订单发/到货：默认停用
出入库是否显示价格：默认启用
允许出入库价格为零：默认停用
小数精度设置：默认2位
行业类别选择，默认为生产型通用版
超任务单领料：默认停用
*/
function ParameterSetting(type, isPoint) {

    $("#td_Msg").html("");
    var StorageUsedStatus = 0;
    var BarcodeUsedStatus = 0;
    var UnitUsedStatus = 0;
    var VoucherUsedStatus = 0; //凭证
    var ApplyUsedStatus = 0; //审核
    var OverOrderUsedStatus = 0; //超订单发货
    var InOutPriceUsedStatus = 0; //出入库价格允许为零
    var IsPrint=0;//是否启用定制打印
    var PrintNo= document.getElementById("PrintNo").value;//打印模板前缀
    var SelPoint = $("#SelPoint").val();
    var IndustrySelect = $("#IndustrySelect").val();
    var printwidth = $("#txt_printwidth").val(); ;
    var OverTakeUsedStatus = 0;//超任务单领料
    if(type=="12")
    {
        if(PrintNo=="")
        {
            alert("请输入打印模板编码！");
            return;
        }
    }
    if(type=="13")
    {
        if(PrintNo=="")
        {
            alert("请输入打印模板宽度！");
            return;
        }
    }
    if (document.getElementById('dioBN1').checked) {
        StorageUsedStatus = 1;
    }
    
    if (document.getElementById('print1').checked) {
        IsPrint = 1;
    }
    if (document.getElementById('dioCB1').checked) {
        BarcodeUsedStatus = 1;
    }
    if (document.getElementById('dioMU1').checked) {
        UnitUsedStatus = 1;
    }
    if (document.getElementById('radOver1').checked) {
        OverOrderUsedStatus = 1;
    }
    if (document.getElementById('radvoucher1').checked) {
        VoucherUsedStatus = 1;
    }
    if (document.getElementById('radapply1').checked) {
        ApplyUsedStatus = 1;
    }
    if (document.getElementById('dioZero1').checked) {
        InOutPriceUsedStatus = 1;
    }
    if (document.getElementById('RadTake1').checked) {
        OverTakeUsedStatus = 1;
    }


    var Action = 'set';
    var UrlParam = "";

    switch (parseInt(type)) {
        case 1:
            UrlParam = "action=" + Action + "&UsedStatus=" + StorageUsedStatus;
            break;
        case 2:
            UrlParam = "action=" + Action + "&UsedStatus=" + BarcodeUsedStatus;
            break;
        case 3:
            UrlParam = "action=" + Action + "&UsedStatus=" + UnitUsedStatus;
            break;
        case 5:
            UrlParam = "action=" + Action + "&UsedStatus=0&SelPoint=" + SelPoint;
            break;
        case 6:
            UrlParam = "action=" + Action + "&UsedStatus=" + VoucherUsedStatus;
            break;
        case 7:
            UrlParam = "action=" + Action + "&UsedStatus=" + ApplyUsedStatus;
            break;
        case 8:
            UrlParam = "action=" + Action + "&UsedStatus=" + OverOrderUsedStatus;
            break;
        case 9:
            UrlParam = "action=" + Action + "&UsedStatus=" + InOutPriceUsedStatus;
            break;
        case 10:
            UrlParam = "action=" + Action + "&UsedStatus=0&SelPoint=" + IndustrySelect;
            break;
        case 11:
            UrlParam = "action=" + Action + "&UsedStatus="+IsPrint;
            break;
        case 12:
            UrlParam = "action=" + Action + "&UsedStatus="+PrintNo;
            break;
        case 13:
            UrlParam = "action=" + Action + "&UsedStatus="+printwidth;
            break;
        case 14:
            UrlParam = "action=" + Action + "&UsedStatus=" + OverTakeUsedStatus;
            break;  
    }
    


    UrlParam += "&FunctionType=" + type;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/ParameterSetting.ashx?",
        data: UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            //  jAlert('请求发生异常');
        },
        success: function(msg) {
            if (msg.result) {
                popMsgObj.ShowMsg(msg.data);
            }
            else {
                popMsgObj.ShowMsg(msg.data);
            }
        }
    });
}

function BusinessLogicSet(logicType,logicID)
{
   var IsSameLogicSet=0;
   var UpdateSelfLogicSet=0;
   
   var MinSaleLogicSet=$("#MinSaleSel").val();
   var MinSaleTimeLogicSet=$("#MinSaleTimeSel").val();
   var CustCreditLogicSet=$("#CustOverCreditSel").val();
   var CustCreditTimeLogicSet=$("#CustCreditTimeSel").val();
   
   var MaxPurchaseLogicSet=$("#MaxPurchaseSel").val();
   var MaxPurchaseTimeLogicSet=$("#MaxPurchaseTimeSel").val();
   
   var OverSendLogicSet=0;
   var OverTransferLogicSet=0;
   var OverArriveLogicSet=0;
   var checkPILogicSet=0;
   var SellSendLogicSet=0;
   var StorageZero=0;//是否负库存
   var InStroageOver=0;//是否超生产订单入库
   
     if (document.getElementById('dioIsSame1').checked) {
        IsSameLogicSet = 1;
    }
    if (document.getElementById('dioUpdateSelf1').checked) {
        UpdateSelfLogicSet = 1;
    }
    
    if(document.getElementById('dioOverSend1').checked)
    {
      OverSendLogicSet=1;
    }
    if(document.getElementById('dioOverTransfer1').checked)
    {
       OverTransferLogicSet=1;
    }
    if(document.getElementById('dioOverArrive1').checked)
    {
       OverArriveLogicSet=1;
    }
    if(document.getElementById('dioCheckPurchaseIn1').checked)
    {
      checkPILogicSet=1;
    }
    if(document.getElementById('dioSellSend1').checked)
    {
      SellSendLogicSet=1;
    }
    if(document.getElementById('diostoragezero1').checked)
    {
      StorageZero=1;
    }
    if(document.getElementById('dioover1').checked)
    {
      InStroageOver=1;
    }
    
    var Action = 'setbus';
    var UrlParam = "";
    
     switch (parseInt(logicType)) {
        case 1:
             switch(parseInt(logicID))
             {
               case 1:
               UrlParam = "action=" + Action + "&LogicName="+escape("制单人与确认人是否可以为同一人") +
                         "&Description="+escape("设置为是表示单据的确认人和制单人可以是同一个人；设置为否时表示确认人和制单人必须是两个不同的人员。")+ 
                         "&LogicSet=" + IsSameLogicSet;
               break;
               case 2:
               UrlParam= "action=" + Action + "&LogicName="+escape("是否只能修改本人制单的单据")+
                         "&Description="+escape("设置为是表示只能修改自己录入的单据，不能修改其他人员录入的单据")+
                         "&LogicSet=" + UpdateSelfLogicSet;
               break;
              }
            break;
        case 2:
            switch(parseInt(logicID))
            {
               case 1:
               UrlParam = "action=" + Action + "&LogicName=" + escape("低于销售最低限价的处理") +
                          "&Description=" + escape("默认情况下对低于销售最低限价不做控制") +
                          "&LogicSet=" + MinSaleLogicSet;
               break;
               case 2:
               UrlParam= "action=" + Action + "&LogicName=" + escape("销售最低限价控制时机") +
                          "&Description=" + escape("默认在单据保存时做销售最低限价控制") +
                          "&LogicSet=" + MinSaleTimeLogicSet;
               break;
               case 3:
               UrlParam= "action=" + Action + "&LogicName=" + escape("客户超过信用限额时控制") +
                          "&Description=" + escape("默认对客户超过信用限额不做控制") +
                          "&LogicSet=" + CustCreditLogicSet;
               break;
               case 4:
               UrlParam= "action=" + Action + "&LogicName=" + escape("客户信用控制处理时机") +
                          "&Description=" + escape("默认在单据保存时做客户信用控制处理") +
                          "&LogicSet=" + CustCreditTimeLogicSet;
               break;
                          
            }
            break;
         case 3:
             switch(parseInt(logicID))
            {
               case 1:
                UrlParam = "action=" + Action + "&LogicName=" + escape("超过采购最高限价处理") +
                          "&Description=" + escape("默认情况下对超过采购最高限价不做控制") +
                          "&LogicSet=" + MaxPurchaseLogicSet;
                break;
               case 2:
                UrlParam = "action=" + Action + "&LogicName=" + escape("采购最高限价控制时机") +
                          "&Description=" + escape("默认在单据保存时做采购最高限价控制") +
                          "&LogicSet=" + MaxPurchaseTimeLogicSet;
                break; 
            }
            break;
        case 4:
           switch(parseInt(logicID))
           {
             case 1:
             UrlParam = "action=" + Action + "&LogicName=" + escape("允许超发货通知单出库") +
                          "&Description=" + escape("设置为是表示允许超发货通知单出库，设置为否表示超发货通知单不能出库。") +
                          "&LogicSet=" + OverSendLogicSet;
              break;
              case 2:
             UrlParam = "action=" + Action + "&LogicName=" + escape("允许超调拨通知单出库") +
                          "&Description=" + escape("设置为是表示允许超调拨通知单出库，设置为否表示超调拨通知单不能出库。") +
                          "&LogicSet=" + OverTransferLogicSet;
              break;
             case 3:
             UrlParam = "action=" + Action + "&LogicName=" + escape("允许超到货通知单入库") +
                          "&Description=" + escape("设置为是表示允许超到货通知单入库，设置为否表示超到货通知单不能入库。") +
                          "&LogicSet=" + OverArriveLogicSet;
              break;
               case 4:
             UrlParam = "action=" + Action + "&LogicName=" + escape("采购到货单审核时，是否自动生成入库单") +
                          "&Description=" + escape("设置为是表示对采购到货单进行审核时，自动生成采购入库单。") +
                          "&LogicSet=" + checkPILogicSet;
              break;
               case 5:
             UrlParam = "action=" + Action + "&LogicName=" + escape("销售发货单审核时，是否自动生成出库单") +
                          "&Description=" + escape("设置为是表示对销售发货单进行审核时，自动生成销售出库单。") +
                          "&LogicSet=" + SellSendLogicSet;
              break;
                  case 6:
             UrlParam = "action=" + Action + "&LogicName=" + escape("是否允许零库存出库") +
                          "&Description=" + escape("设置为是表示可用库存量为零时仍可出库。") +
                          "&LogicSet=" + StorageZero;
              break;
                  case 7:
             UrlParam = "action=" + Action + "&LogicName=" + escape("是否允许超生产任务单入库") +
                          "&Description=" + escape("设置为是表示完工入库单数量可大于生产下达数量。") +
                          "&LogicSet=" + InStroageOver;
              break;
           }
            
            break;
       }
   UrlParam += "&LogicType=" + logicType + "&LogicID=" +logicID;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/ParameterSetting.ashx?",
        data: UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
//            jAlert('请求发生异常');
        },
        success: function(msg) {
            if (msg.result) {
                popMsgObj.ShowMsg(msg.data);
            }
            else {
                popMsgObj.ShowMsg(msg.data);
            }
        }
    });
}
$(document).ready(function() {
    if(document.getElementById("print1").checked==true)
    {
        document.getElementById("img2").disabled=false;
         document.getElementById("img2").src="../../../images/Button/Bottom_btn_save.jpg"
        
    }else
    {
        document.getElementById("img2").disabled=true;
        
         document.getElementById("img2").src="../../../Images/Button/UnClick_bc.jpg"
    }
});
function TRVisible(type)
{
    if(type=="true")
    {
        document.getElementById("img2").disabled=true;
        document.getElementById("img2").src="../../../Images/Button/UnClick_bc.jpg"
        document.getElementById("PrintNo").value="";
    }else
    {
        document.getElementById("img2").disabled=false;
         document.getElementById("img2").src="../../../images/Button/Bottom_btn_save.jpg"
    }
}

function clearNoNum(obj)
{
//先把非数字的都替换掉，除了数字和.
obj.value = obj.value.replace(/[^\d.]/g,"");
//必须保证第一个为数字而不是.
obj.value = obj.value.replace(/^\./g,"");
//保证只有出现一个.而没有多个.
obj.value = obj.value.replace(/\.{2,}/g,".");
//保证.只出现一次，而不能出现两次以上
obj.value = obj.value.replace(".","$#$").replace(/\./g,"").replace("$#$",".");
}


