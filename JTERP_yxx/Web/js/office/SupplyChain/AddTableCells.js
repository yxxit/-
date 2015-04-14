var ModuleID="";
$(document).ready(function() 
{
//    var TableName="officedba.PurchaseInStorage";
//    GetExtAttr(TableName,null);
      requestobj = GetRequest();
      ModelNo=requestobj['ModelNo'];
      ModuleID=requestobj['ModuleID'];
      IsDisplayFull();
      if(typeof(ModelNo)!="undefined")
         LoadData(ModelNo);  
      else   LoadBillType();

  });
  
//是否显示所有自定义项
function IsDisplayFull()
{
    var FunctionModule=$("#FunctionModule").val();
    if(ModuleID=="2081310")
    {
//        $("#td_11").hide();
//        $("#td_11c").hide();
//        $("#td_12").hide();
//        $("#td_12c").hide();
//        $("#tr_13").hide();
//        $("#tr_16").hide();
        //$("#tr_19").hide();
        $("#tr_11").hide();
//        $("#tr_13").hide();
        $("#tr_15").hide();
//        $("#tr_16").hide();
        $("#tr_19").hide();
    }
    else
    {
//        $("#td_11").show();
//        $("#td_11c").show();
//        $("#td_12").show();
//        $("#td_12c").show();
//        $("#tr_13").show();
//        $("#tr_16").show();
        //        $("#tr_19").show();
        $("#tr_11").show();
//        $("#tr_13").show();
        $("#tr_15").show();
//        $("#tr_16").show();
        $("#tr_19").show();
    }
    if(FunctionModule!="8")
    {
//        $("#td_11").hide();
//        $("#td_11c").hide();
//        $("#td_12").hide();
//        $("#td_12c").hide();
//        $("#tr_13").hide();
//        $("#tr_16").hide();
        //        $("#tr_19").hide();
        $("#tr_11").hide();
//        $("#tr_13").hide();
        $("#tr_15").hide();
//        $("#tr_16").hide();
        $("#tr_19").hide();
    }
    else
    {
//        $("#td_11").show();
//        $("#td_11c").show();
//        $("#td_12").show();
//        $("#td_12c").show();
//        $("#tr_13").show();
//        $("#tr_16").show();
        //        $("#tr_19").show();
        $("#tr_11").show();
//        $("#tr_13").show();
        $("#tr_15").show();
//        $("#tr_16").show();
        $("#tr_19").show();
    }
}
//列表页面---查看数据
function LoadData(ModelNo)
{
    var Action="GetDataByNo";
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/TableCellsInfo.ashx",
        data: 'Action=' + escape(Action) +
        '&ModelNo=' + escape(ModelNo),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(msg) {
            $("#ModelName").attr("disabled", true);
            $("#FormType").attr("disabled", true);
            $("#FunctionModule").attr("disabled", true);
            InsertOrUpdate = "Update";
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    $("#ModelName").val(item.ModelNo);
                    $("#FunctionModule").val(item.FunctionModule);
                    LoadBillType(); //加载单据下拉列表
                    $("#FormType").val(item.TabName);
                    var Custom = "Custom" + (i + 1);
                    $("#" + Custom).val(item.EFDesc);
                    var pCustom = "PCustom" + (i + 1);
                    $("#" + pCustom).val(item.PEFIndex);
                }
            });
            hidePopup();
        }
    });
}
var InsertOrUpdate="Insert";//新增OR更新
//保存操作
function Save()
{
    if(!CheckInput())
        return;
    var FormType=$.trim($("#FormType").val());//源单类型
    var ModelNo=$.trim($("#ModelName").val());//模板名称
    var FunctionModule=$("#FunctionModule").val();//功能模块
    GetCustomValue();//获取自定义项的值
    var CustomValues = CustomValueArray.join("$"); //自定义项
    var pCustomValues = pCustomValueArray.join("$");//对应物品特性项
    var Action="AddExtAttr";   
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/TableCellsInfo.ashx",
        data: 'Action=' + escape(Action) + 
        '&InsertOrUpdate=' + escape(InsertOrUpdate) + 
        '&FormType=' + escape(FormType) + 
        '&ModelNo=' + escape(ModelNo) +
        '&FunctionModule=' + escape(FunctionModule) + 
        '&CustomValues=' + escape(CustomValues)+'&pCustomValues='+escape(pCustomValues),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
           AddPop();
        },
        //complete :function(){hidePopup();},
        error: function() { //jHide();jAlert("请求发生错误！");
        },
        success: function(data) 
        {      
           if(data.sta!="0")
           {
            $("#ModelName").attr("disabled",true);
            $("#FormType").attr("disabled",true);
            $("#FunctionModule").attr("disabled",true);
            InsertOrUpdate="Update";
            }
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.Msg);
        }
    });
}
//获取自定义项的值
var CustomValueArray = new Array();
var pCustomValueArray = new Array();
function GetCustomValue()
{
    var Custom1=$.trim($("#Custom1").val());
    var Custom2=$.trim($("#Custom2").val());
    var Custom3=$.trim($("#Custom3").val());
    var Custom4=$.trim($("#Custom4").val());
    var Custom5=$.trim($("#Custom5").val());
    var Custom6=$.trim($("#Custom6").val());
    var Custom7=$.trim($("#Custom7").val());
    var Custom8=$.trim($("#Custom8").val());
    var Custom9=$.trim($("#Custom9").val());
    var Custom10=$.trim($("#Custom10").val());
    var Custom11=$.trim($("#Custom11").val());
    var Custom12=$.trim($("#Custom12").val());
    var Custom13=$.trim($("#Custom13").val());
    var Custom14=$.trim($("#Custom14").val());
    var Custom15=$.trim($("#Custom15").val());
    var Custom16=$.trim($("#Custom16").val());
    var Custom17=$.trim($("#Custom17").val());
    var Custom18=$.trim($("#Custom18").val());
    var Custom19=$.trim($("#Custom19").val());
    var Custom20 = $.trim($("#Custom20").val());

    var PCustom1 = $.trim($("#PCustom1").val());
    var PCustom2 = $.trim($("#PCustom2").val());
    var PCustom3 = $.trim($("#PCustom3").val());
    var PCustom4 = $.trim($("#PCustom4").val());
    var PCustom5 = $.trim($("#PCustom5").val());
    var PCustom6 = $.trim($("#PCustom6").val());
    var PCustom7 = $.trim($("#PCustom7").val());
    var PCustom8 = $.trim($("#PCustom8").val());
    var PCustom9 = $.trim($("#PCustom9").val());
    var PCustom10 = $.trim($("#PCustom10").val());
    
    var i=0;
    if (Custom1 != "") { CustomValueArray[i] = [Custom1];pCustomValueArray[i] = [PCustom1]; i++; }
    if (Custom2 != "") { CustomValueArray[i] = [Custom2]; pCustomValueArray[i] = [PCustom2]; i++; }
    if (Custom3 != "") { CustomValueArray[i] = [Custom3]; pCustomValueArray[i] = [PCustom3]; i++; }
    if (Custom4 != "") { CustomValueArray[i] = [Custom4]; pCustomValueArray[i] = [PCustom4]; i++; }
    if (Custom5 != "") { CustomValueArray[i] = [Custom5]; pCustomValueArray[i] = [PCustom5]; i++; }
    if (Custom6 != "") { CustomValueArray[i] = [Custom6]; pCustomValueArray[i] = [PCustom6]; i++; }
    if (Custom7 != "") { CustomValueArray[i] = [Custom7]; pCustomValueArray[i] = [PCustom7]; i++; }
    if (Custom8 != "") { CustomValueArray[i] = [Custom8]; pCustomValueArray[i] = [PCustom8]; i++; }
    if (Custom9 != "") { CustomValueArray[i] = [Custom9]; pCustomValueArray[i] = [PCustom9]; i++; }
    if (Custom10 != "") { CustomValueArray[i] = [Custom10]; pCustomValueArray[i] = [PCustom10]; i++; }
    var FunctionModule=$("#FunctionModule").val();
    if(FunctionModule=="8")
    {
        if(Custom11!=""){CustomValueArray[i]=[Custom11];i++;}
        if(Custom12!=""){CustomValueArray[i]=[Custom12];i++;}
        if(Custom13!=""){CustomValueArray[i]=[Custom13];i++;}
        if(Custom14!=""){CustomValueArray[i]=[Custom14];i++;}
        if(Custom15!=""){CustomValueArray[i]=[Custom15];i++;}
        if(Custom16!=""){CustomValueArray[i]=[Custom16];i++;}
        if(Custom17!=""){CustomValueArray[i]=[Custom17];i++;}
        if(Custom18!=""){CustomValueArray[i]=[Custom18];i++;}
        if(Custom19!=""){CustomValueArray[i]=[Custom19];i++;}
        if(Custom20!=""){CustomValueArray[i]=[Custom20];i++;}
    }
}
//保存时获取各扩展属性值
//var RetVal=GetExtAttrValue();


//表单验证
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    var ModelName=$.trim($("#ModelName").val());
    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(ModelName=="")
    {
        isFlag = false;
        fieldText = fieldText + "模板编号|";
   		msgText = msgText + "模板编号不能为空|";
    }
     var CustomLength=10;
     if(ModuleID=="2081310")
     {
        var FunctionModule=$("#FunctionModule").val();
        if(FunctionModule!="8")
            CustomLength=10;
        else
            CustomLength=20;
     }
     else 
        CustomLength=20;
     var count=0;
     var lenthflag=true;
   	 for(var i=1;i<=CustomLength;i++)//自定义项空值和长度判断
   	 {
          if($.trim($("#Custom"+i).val())=="")
            count++;
          else
          {
            if(strlen($.trim($("#Custom"+i).val()))>20)
                lenthflag = false;
          }
   	 }
   	 if(count==CustomLength)
   	 {
   	    isFlag = false;
        fieldText = fieldText + "自定义项|";
   		msgText = msgText + "至少有一项自定义项不能为空|";
   	 }
   	 if(!lenthflag)
     {
        isFlag = false;
        fieldText = fieldText + "自定义项|";
   		msgText = msgText + "各自定义项不能超过20个字符|";
   	 }
   	
    if(strlen(ModelName.Trim())>30)
    {
        isFlag = false;
        fieldText = fieldText + "模板编号|";
   		msgText = msgText + "不能超过30个字符|";
    }
     var xhflag=true;
   	 for(var i=CustomLength;i>1;i--)
   	 {
   	      var j=i-1;
          if($.trim($("#Custom"+i).val())!="" && $.trim($("#Custom"+j).val())=="")
          {
   	            xhflag=false;
   	            break;
   	      }
   	 }
    if(!xhflag)
    {
        isFlag = false;
        fieldText = fieldText + "自定义项设置|";
   		msgText = msgText + "请顺序添加各自定义项|";
   	}
   	var repeatflag=true;//重复项判断
   	for(var i=1;i<=CustomLength;i++)
   	{
        for(var j=i+1;j<=CustomLength;j++)
        {
          if($.trim($("#Custom"+i).val())!="" && $.trim($("#Custom"+j).val())!="")
          {
            if($.trim($("#Custom"+i).val())==$.trim($("#Custom"+j).val()))
            {
                repeatflag=false;
                break;
            }
          }
        }
   	}
   	if(!repeatflag)
    {
        isFlag = false;
        fieldText = fieldText + "自定义项设置|";
   		msgText = msgText + "不能存在重复的自定义项|";
   	}
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    }
    return isFlag;
}
//返回
function DoBack()
{
    window.history.back(-1);
}
//选择功能模块填充单据类型
var ManagerArray=new Array();
function LoadBillType()
{
    ClearDDL();
    IsDisplayFull();
    if(ModuleID=="2081310")
    {}
    else
    {
        $("#FunctionModule").val("8")
        $("#FunctionModule").attr("disabled",true);
    }
    var FunctionModule=$("#FunctionModule").val();
    if(FunctionModule=="1")
    {
        ManagerArray=[
        [["销售计划"],["officedba.SellPlanDetail"]],
        [["销售机会"],["officedba.SellChanceDetail"]],
        [["销售报价单"],["officedba.SellOfferDetail"]],
        [["销售合同"],["officedba.SellContractDetail"]],
        [["销售订单"],["officedba.SellOrderDetail"]],
        [["销售发货通知单"],["officedba.SellSendDetail"]],
        [["回款计划"],["officedba.SellGatheringDetail"]],
        [["销售退货单"],["officedba.SellBackDetail"]],
        [["委托代销结算单"],["officedba.SellChannelSttlDetail"]]
        ]
    }
    else if(FunctionModule=="2")
    {
        ManagerArray=[
        [["采购申请单"],["officedba.PurchaseApplyDetail"]],
        [["采购计划单"],["officedba.PurchasePlanDetail"]],
        [["采购询价单"],["officedba.PurchaseAskPriceDetail"]],
        [["采购合同"],["officedba.PurchaseContractDetail"]],
        [["采购订单"],["officedba.PurchaseOrderDetail"]],
        [["采购到货单"],["officedba.PurchaseArriveDetail"]],
        [["采购退货单"],["officedba.PurchaseRejectDetail"]]
        ]
    }
    else if(FunctionModule=="3")
    {
        ManagerArray=[
        [["采购入库单"],["officedba.StorageInPurchaseDetail"]],
        [["生产完工入库单"],["officedba.StorageInProcessDetail"]],
        [["其他入库单"],["officedba.StorageInOtherDetail"]],
        [["红冲入库单"],["officedba.StorageInRedDetail"]],
        [["销售出库单"],["officedba.StorageOutSellDetail"]],
        [["其他出库单"],["officedba.StorageOutOtherDetail"]],
        [["红冲出库单"],["officedba.StorageOutRedDetail"]],
        [["借货单"],["officedba.StorageBorrowDetail"]],
        [["借货返还单"],["officedba.StorageReturnDetail"]],
        [["库存调拨单"],["officedba.StorageTransferDetail"]],
        [["库存调整单"],["officedba.StorageAdjustDetail"]],
        [["盘点单"],["officedba.StorageCheckDetail"]],
        [["库存报损单"],["officedba.StorageLossDetail"]]
        ]
    }
    else if(FunctionModule=="4")
    {
        ManagerArray=[
        [["主生产计划"],["officedba.MasterProductScheduleDetail"]],
        [["物料需求计划"],["officedba.MRPDetail"]],
        [["生产任务单"],["officedba.ManufactureTaskDetail"]],
        [["生产任务单汇报"],["officedba.ManufactureReportDetail"]],
        [["领料单"],["officedba.TakeMaterialDetail"]],
        [["退料单"],["officedba.BackMaterialDetail"]]
        ]
    }
    else if(FunctionModule=="5")
    {
        ManagerArray=[
        [["质检申请单"],["officedba.QualityCheckApplayDetail"]],
        [["质检报告单"],["officedba.QualityCheckReportDetail"]],
        [["不合格品处置单"],["officedba.CheckNotPassDetail"]]
        ]
    }
    else if(FunctionModule=="6")
    {
        ManagerArray=[
        [["配送单"],["officedba.SubDeliverySendDetail"]],
        [["配送退货单"],["officedba.SubDeliveryBackDetail"]],
        [["分店调拨单"],["officedba.SubDeliveryTransDetail"]]
        ]
    }
    else if(FunctionModule=="7")
    {
        ManagerArray=[
        [["分店入库单"],["officedba.SubStorageInDetail"]],
        [["分店销售订单"],["officedba.SubSellOrderDetail"]],
        [["分店销售退货单"],["officedba.SubSellBackDetail"]]
        ]
    }
    else if(FunctionModule=="8")
    {
        ManagerArray=[
        [["项目档案"],["officedba.ProjectInfoDetail"]]
        ]
    }
//    else if(FunctionModule=="8")
//    {
//        ManagerArray=[
//        [["物品特性"],["officedba.ProductInfo"]]
//        ]
//    }
    for(var i=0;i<ManagerArray.length;i++)//填充单据类型
    {    
        document.getElementById('FormType').options[i]=new Option(ManagerArray[i][0],ManagerArray[i][1]);
    }
}
//清空选项
 function ClearDDL()
 {
    var length=document.getElementById('FormType').options.length;
    if(length>1)
    {
        for(i=length-1;i>=0;i--)   
        {   
          document.getElementById('FormType').remove(i);   
          length--;
        }   
    }
 }
