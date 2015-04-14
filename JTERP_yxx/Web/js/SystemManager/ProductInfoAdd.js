var OldNo="";
var glb_SelPoint=2;//小数位数

$(document).ready(function() {   
       requestobj = GetRequest();
       var recordnoparam=requestobj['intOtherCorpInfoID'];
       if(recordnoparam>0)
       {
        $('#txtIndentityID').val(recordnoparam);
        $('#product_btnunsure').css("display","none");
        $('#divInputNo').css("display","block");
        
        $('#divNo').css("display","none");
        $('#CodingRuleControl1_ddlCodeRule').css("display","none");
        $('#CodingRuleControl1_txtCode').css("display","block");
        $('#CodingRuleControl1_txtCode').addClass("tdinput");
        $('#CodingRuleControl1_txtCode').css("width","90%");
        GetProductInfo(recordnoparam);
       }
       
       
       
});


function GetProductInfo(recordnoparam){  //20140402 刘锦旗
    
    var retval = recordnoparam;
    $.ajax({
       type: "POST", //用POST方式传输
       dataType: "json", //数据格式:JSON
       url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx", //目标地址
       data: 'id=' + reescape(retval)+'&Action=LoadProductInfo' ,
       cache: false,
       beforeSend: function() {AddPop(); }, //发送数据之前
       success: function(msg) {
           //数据获取完毕，填充页面据显示
           $.each(msg.data, function(i, item) {
               if (item.ID != null && item.ID != ""){
                   $("#CodingRuleControl1_txtCode").val(item.ProdNo);
                   $("#divNo").val(item.ProdNo);
                   $("#txt_PYShort").val(item.PYShort);
                   $("#txt_ProductName").val(item.ProductName);
                   $("#txt_ShortNam").val(item.ShortNam);
                   $("#txt_BarCode").val(item.BarCode);
                   $("#sel_UnitID").val(item.UnitID);
                   $("#txt_Remark").val(item.Remark);
                   
                   $("#UserPrincipal").val(item.EmployeeName);
                   $("#txt_CreateDate").val(item.CreateDate);
                   $("#sel_UsedStatus").val(item.UsedStatus);
                   $("#txtModifiedDate").val(item.ModifiedDate);
                   $("#txtModifiedUserID").val(item.ModifiedUserID);
                   $("#sel_StorageID").val(item.StorageID);
                   $("#txt_SafeStockNum").val(item.SafeStockNum);
                   $("#txt_MinStockNum").val(item.MinStockNum);
                   $("#txt_MaxStockNum").val(item.MaxStockNum);
                   $("#txt_StandardSell").val(item.StandardSell);
                    $("#txt_StandardBuy").val(item.StandardBuy);
                   $("#txt_SellTax").val(item.SellTax);
                   $("#txt_TaxBuy").val(item.TaxBuy);
                   $("#txt_InTaxRate").val(item.InTaxRate);
                   $("#txt_TaxRate").val(item.TaxRate);
                   $("#txt_HeatPower").val(item.HeatPower);
                   $("#txt_VolaPercent").val(item.VolaPercent);
                   $("#txt_AshPercent").val(item.AshPercent);
                   $("#txt_SulfurPercent").val(item.SulfurPercent);
                   $("#txt_WaterPercent").val(item.WaterPercent);
                   $("#txt_CarbonPercent").val(item.CarbonPercent);

                }
            });
        },
        error: function() 
          {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
          },
        complete :function(){hidePopup();}
          
                           
    });
}



function Fun_GroupList_Content() {
    var UnitNo = $("#HdGroupNo").val();
    if (UnitNo == "" || UnitNo == null) {
        
    }
    else {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SupplyChain/ProductInfo.ashx?action=LoadUnit&GroupUnitNo=' + UnitNo, //目标地址
            cache: false,
            data: '', //数据
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
                $.each(msg.data, function(i, item) {
                    document.getElementById("selStorageUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selSellUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selPurchseUnit").options.add(new Option(item.CodeName, item.UnitID));
                    document.getElementById("selProductUnit").options.add(new Option(item.CodeName, item.UnitID));
                });
            },
            error: function() {
                howPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
            },
            complete: function() {

            } //接收数据完毕
        });
    }

}


//若是中文则自动填充拼音缩写
function LoadPYShort() {
    var txt_ProductName = $("#txt_ProductName").val();
    if (txt_ProductName.length > 0 && isChinese(txt_ProductName)) {
        $.ajax({
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?Text=" + escape(txt_ProductName),
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                //AddPop();
            },
            //complete :function(){ //hidePopup();},
            error: function() { },
            success: function(data) {
                document.getElementById('txt_PYShort').value = data.info;
            }
        });
    }
}

function Fun_Save_ProductInfo() {
    var ProdNo="";
    var fieldText="";
    var msgText="";
    var isFlag=true;
    var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
    //基本信息
    requestobj=GetRequest();
    recordnoparam=requestobj['intOtherCorpInfoID'];
    if (typeof(recordnoparam)=="undefined") {
        Action='Add';
    }
    else {
        Action='Edit';
    }
    if (document.getElementById('txtIndentityID').value != "0") {
        Action='Edit';
        ProdNo=$.trim($("#divNo").val()); 
        OldNo=ProdNo;
    }
    if (Action=="Edit") {
        ProdNo=$.trim($("#divNo").val()); 
    }
    var PYShort=$("#txt_PYShort").val();
    var ProductName=trim($("#txt_ProductName").val());
    var ShortNam = trim($("#txt_ShortNam").val());
    
//    var CASRegi = $("#txt_casregi").val(); //化工行业CAS登记号
//    var EngName = $("#txt_engname").val(); //物品英文名字
    
    var BarCode=$("#txt_BarCode").val();  //条形码
    //var TypeID=$("#txt_Code").val();
    //var BigType=$("#txt_BigType").val();
    //var GradeID=$("#sel_GradeID").val();
    var UnitID=$("#sel_UnitID").val();  //基本单位
    //var Brand=$("#sel_Brand").val();
    //var ColorID=$("#sel_ColorID").val();
    //var Specification=$("#txt_Specification").val();
//    var Size=$("#txt_Size").val();//尺寸
//    var Density=$("#ddlDensity").val();//密度
//    var PieceCount=$("#txt_PieceCount").val();//每件片数
    //var Source=$("#sel_Source").val();
    //var FromAddr=$("#txt_FromAddr").val();
    //var DrawingNum=$("#txt_DrawingNum").val();
    //var ImgUrl=document.getElementById("hfPagePhotoUrl").value;
  
    //var PricePolicy=$("#txt_PricePolicy").val();
    //var Params=$("#txt_Params").val();
    //var Questions=$("#txt_Questions").val();
    //var ReplaceName=$("#txt_ReplaceName").val();
    //var Description=$("#txt_Description").val();
    var StorageID=$("#sel_StorageID").val();   //主放仓库
    var SafeStockNum=$("#txt_SafeStockNum").val();  //安全库存量
    //var StayStandard=$("#txt_StayStandard").val();  
    var MinStockNum=$("#txt_MinStockNum").val(); //最低库存量
    var MaxStockNum=$("#txt_MaxStockNum").val(); //最高库存量   
    //var ABCType=$("#sel_ABCType").val();
    //var CalcPriceWays=$("#sel_CalcPriceWays").val();
    //var StandardCost=$("#txt_StandardCost").val();
    //var PlanCost=$("#txt_PlanCost").val();
    var StandardSell=$("#txt_StandardSell").val();  //去税售价
    //var SellMin=$("#txt_SellMin").val();
    //var SellMax=$("#txt_SellMax").val();
    var TaxRate=$("#txt_TaxRate").val();
    var InTaxRate=$("#txt_InTaxRate").val();
    var SellTax=$("#txt_SellTax").val();
    //var SellPrice=$("#txt_SellPrice").val();
    //var TransfrePrice=$("#txt_TransfrePrice").val();
    //var Discount=$("#txt_Discount").val();
    var StandardBuy=$("#txt_StandardBuy").val();  //含税进价
    var TaxBuy=$("#txt_TaxBuy").val();
    //var BuyMax=$("#txt_BuyMax").val();
    var Remark=$("#txt_Remark").val();
    var Creator=$("#txtPrincipal").val();   //建档人
    var CreateDate=$("#txt_CreateDate").val();
    //var CheckStatus=$("#sel_CheckStatus").val();
//    var CheckUser="";
//    var CheckDate="";
    var UsedStatus=$("#sel_UsedStatus").val();  //启用状态
//    var StockIs="";
//    var MinusIs="";
//    var IsBatchNo="";
 
//    var Capability=$("#Capability").val();  
//      if(document.getElementById("txt_FileNo")!=null)
//      {
//    var FileNo=$("#txt_FileNo").val();  
//    var Qualitystandard=$("#Qualitystandard").val();
//    var Validity=$("#Validity").val();
//    var MedFileDate=$("#MedFileDate").val();
//    var MedCheckDate=$("#MedCheckDate").val();
//    var MedCheckNo=$("#MedCheckNo").val();             //药品检验报告编号
//    }
//    else
//    {
//    var FileNo=""; 
//    var Qualitystandard="";
//    var Validity="";
//    var MedFileDate="";
//    var MedCheckDate="";
//    var MedCheckNo="";
//    }
    
    
 
  
//   var MaxImportPrice=$("#MaxImportPrice").val();
//   var MinSalePrice=$("#MinSalePrice").val();
//   
//   var Pnumber=$("#txt_Pnumber").val();
//   var AbrasionResist=$("#txt_AbrasionResist").val();
//   var BalancePaper=$("#txt_BalancePaper").val();
//   var BaseMaterial=$("#txt_BaseMaterial").val();
//   var SurfaceTreatment=$("#txt_SurfaceTreatment").val();
//   var BackBottomPlate=$("#txt_BackBottomPlate").val();
//   var BuckleType=$("#txt_BuckleType").val();
//   var Pnumberid=$("#hf_txt_Pnumber").val();
//   var AbrasionResistid=$("#hf_txt_AbrasionResist").val();
//   var BalancePaperid=$("#hf_txt_BalancePaper").val();
//   var BaseMaterialid=$("#hf_txt_BaseMaterial").val();

    
    //煤炭物品的属性20140401
    var HeatPower=$("#txt_HeatPower").val();  //发热量
    var VolaPercent=$("#txt_VolaPercent").val();    //挥发份
    var AshPercent=$("#txt_AshPercent").val();  //灰份
    var SulfurPercent=$("#txt_SulfurPercent").val();    //全硫份
    var WaterPercent=$("#txt_WaterPercent").val();  //水分
    var CarbonPercent=$("#txt_CarbonPercent").val();    //固定碳
    
   
    /*****************************************************************************物品控件新建验证Start**************************************************************************/
    if (CodeType == "") {
        ProdNo=trim($("#CodingRuleControl1_txtCode").val());
        if (ProdNo=="") {
            isFlag = false;
            fieldText += "物品编号|";
            msgText += "请输入物品编号|";
        }
        if (strlen(ProdNo) > 50) {
            isFlag = false;
            fieldText += "物品编号|";
            msgText += "物品编号仅限于50个字符以内|";
        }
        if (strlen(ProdNo)> 0) {
        var lastlen=strlen(ProdNo)-2;
        
            if (ProdNo.indexOf("%")==0|| ProdNo.lastIndexOf("%")== lastlen)
             {
                isFlag = false;
                fieldText = fieldText + "物品编号|";
                msgText = msgText + "物品编号不能以%开头与结尾";
             }
             
        }
       
        
        
    }
    if (ProductName == "") {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "请输入物品名称|";
    }
    if (strlen(ProductName) > 100) {
        isFlag = false;
        fieldText += "物品名称|";
        msgText += "物品名称仅限于100个字符以内|";
    }
    if (StorageID == null) {
        isFlag = false;
        fieldText += "主放仓库|";
        msgText += "请先创建主放仓库|";
    }
    if (StorageID == "0") {
        isFlag = false;
        fieldText += "主放仓库|";
        msgText += "请选择主放仓库|";
    }
    if (strlen(ShortNam) > 100) {
        isFlag = false;
        fieldText += "名称简称|";
        msgText += "名称简称仅限于50个字符以内|";
    }
//    if (strlen(Specification) > 100) {
//        isFlag = false;
//        fieldText += "规格型号|";
//        msgText += "规格型号仅限于100个字符以内|";
//    }

//    var tmpSpecification = '';
// 
//    /*处理规格*/
//    for (var i = 0; i < Specification.length; i++) {
//        if (Specification.charAt(i) == '+') {
//            tmpSpecification = tmpSpecification + '＋';
//        }
//        else {
//            tmpSpecification = tmpSpecification + Specification.charAt(i);
//        }
//    }

    //Specification = tmpSpecification.replace('×', '&#174');
    
    if (UnitID == "0" || UnitID == null) {
        isFlag = false;
        fieldText += "基本单位|";
        msgText += "请选择基本单位|";
    }
    
   
    if (MinStockNum != "") {
        if (!IsNumeric(MinStockNum)) {
            isFlag = false;
            fieldText += "最低库存量|";
            msgText += "最低库存量格式不对|";
        }
        else {
            if (MinStockNum.indexOf('.') > -1) {
                if (!IsNumeric(MinStockNum, 8, glb_SelPoint)) {
                    isFlag = false;
                    fieldText += "最低库存量|";
                    msgText += "最低库存量格式不对|";
                }
            }
            else if (MinStockNum.indexOf('.') == -1) {
                if (strlen(MinStockNum) > 8) {
                    isFlag = false;
                    fieldText += "最低库存量|";
                    msgText += "最低库存量整数部分不能超过8位|";
                }
            }
        }
    }
    if(HeatPower!=""){
        if (!IsNumeric(HeatPower)) {
            isFlag = false;
            fieldText += "发热量|";
            msgText += "发热量格式不对|";
        }
    
    }
    
    if(VolaPercent!=""){
        if (!IsNumeric(VolaPercent)) {
            isFlag = false;
            fieldText += "挥发份|";
            msgText += "挥发份格式不对|";
        }
    
    }
    
    if(AshPercent!=""){
        if (!IsNumeric(AshPercent)) {
            isFlag = false;
            fieldText += "灰份|";
            msgText += "灰份格式不对|";
        }
    
    }
    
    if(SulfurPercent!=""){
        if (!IsNumeric(SulfurPercent)) {
            isFlag = false;
            fieldText += "全硫份|";
            msgText += "全硫份格式不对|";
        }
    
    }
    
    if(WaterPercent!=""){
        if (!IsNumeric(WaterPercent)) {
            isFlag = false;
            fieldText += "水分|";
            msgText += "水分格式不对|";
        }
    }
    
    if(CarbonPercent!=""){
        if (!IsNumeric(CarbonPercent)) {
            isFlag = false;
            fieldText += "固定碳|";
            msgText += "固定碳格式不对|";
        }
    }
    


   
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    /*****************************************************************************物品控件新建验证End**************************************************************************/
//    if ( (StandardSell == "") && (TaxBuy == "")) {
//        if (!window.confirm('去税售价、去税进价都为空值，是否继续保存？')) {
//            return false;
//        }
//    }


    
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx",
        data:'OldNo='+escape(OldNo)+
         '&ProdNo='+escape(ProdNo)+
         '&PYShort='+escape(PYShort)+
         '&ProductName='+escape(ProductName)+
         '&ShortNam=' + escape(ShortNam) +
//          '&EngName=' + escape(EngName) +
//         '&CASRegi=' + escape(CASRegi) +
         '&BarCode='+escape(BarCode)+
//         '&TypeID='+escape(TypeID)+
//         '&BigType='+escape(BigType)+
//         '&GradeID='+escape(GradeID)+
         '&UnitID='+escape(UnitID)+
//         '&Brand='+escape(Brand)+
//         '&ColorID='+escape(ColorID)+
//         '&Specification='+escape(Specification)+'&Size='+escape(Size)+
//         '&Density='+escape(Density)+'&PieceCount='+escape(PieceCount)+
//         '&Source='+escape(Source)+
//         '&FromAddr='+escape(FromAddr)+
//         '&DrawingNum='+escape(DrawingNum)+
//         '&ImgUrl='+escape(ImgUrl)+
//         '&FileNo='+escape(FileNo)+
//         '&PricePolicy='+escape(PricePolicy)+
//         '&Params='+escape(Params)+
//         '&Questions='+escape(Questions)+
//         '&ReplaceName='+escape(ReplaceName)+
//         '&Description='+escape(Description)+
//         '&StockIs='+escape(StockIs)+
//         '&MinusIs='+escape(MinusIs)+
         '&StorageID='+escape(StorageID)+
         '&SafeStockNum='+escape(SafeStockNum)+
         //'&StayStandard='+escape(StayStandard)+
         '&MinStockNum='+escape(MinStockNum)+
         '&MaxStockNum='+escape(MaxStockNum)+
//         '&ABCType='+escape(ABCType)+
//         '&CalcPriceWays='+escape(CalcPriceWays)+
//         '&StandardCost='+escape(StandardCost)+
//         '&PlanCost='+escape(PlanCost)+
         '&StandardSell='+escape(StandardSell)+
//         '&SellMin='+escape(SellMin)+
//         '&SellMax='+escape(SellMax)+
         '&TaxRate='+escape(TaxRate)+
         '&InTaxRate='+escape(InTaxRate)+
         '&SellTax='+escape(SellTax)+
//         '&SellPrice='+escape(SellPrice)+
//         '&TransfrePrice='+escape(TransfrePrice)+
//         '&Discount='+escape(Discount)+
         '&StandardBuy='+escape(StandardBuy)+
         '&TaxBuy='+escape(TaxBuy)+
//         '&BuyMax='+escape(BuyMax)+
         '&Remark='+escape(Remark)+
         '&Creator='+escape(Creator)+
         '&CreateDate='+escape(CreateDate)+
//         '&CheckStatus='+escape(CheckStatus)+
//         '&CheckUser='+escape(CheckUser)+
//         '&CheckDate='+escape(CheckDate)+
         '&UsedStatus='+escape(UsedStatus)+
         '&CodeType='+escape(CodeType)+
//         '&Manufacturer='+escape(Manufacturer)+
//         '&Material='+escape(Material)+
//         '&GroupNo='+escape(GroupNo)+
//         '&StorageUnit='+escape(StorageUnit)+
//         '&SellUnit='+escape(SellUnit)+
//         '&PurchseUnit='+escape(PurchseUnit)+
//         '&MaxImportPrice='+escape(MaxImportPrice)+
//         '&MinSalePrice='+escape(MinSalePrice)+
//         '&Qualitystandard='+escape(Qualitystandard)+
//         '&Capability='+escape(Capability)+
//         '&Validity='+escape(Validity)+
//         '&MedFileDate='+escape(MedFileDate)+
//         '&MedCheckDate='+escape(MedCheckDate)+
//         '&MedCheckNo='+escape(MedCheckNo)+
        // '&bsale='+escape(bsale)+'&bpurchase='+escape(bpurchase)+'&bconsume='+escape(bconsume)+'&baccessary='+escape(baccessary)+
         //'&bself='+escape(bself)+'&bproducing='+escape(bproducing)+'&bservice='+escape(bservice)+
         //'&Pnumber='+escape(Pnumber)+'&AbrasionResist='+escape(AbrasionResist)+'&BalancePaper='+escape(BalancePaper)+'&BaseMaterial='+escape(BaseMaterial)+
         //'&SurfaceTreatment='+escape(SurfaceTreatment)+'&BackBottomPlate='+escape(BackBottomPlate)+'&BuckleType='+escape(BuckleType)+
         // '&Pnumberid='+escape(Pnumberid)+'&AbrasionResistid='+escape(AbrasionResistid)+'&BalancePaperid='+escape(BalancePaperid)+'&BaseMaterialid='+escape(BaseMaterialid)+
         //'&ProductUnit='+escape(ProductUnit)+'&IsBatchNo='+escape(IsBatchNo)+
         '&Action='+Action+'&HeatPower='+escape(HeatPower)+'&VolaPercent='+escape(VolaPercent)+'&AshPercent='+escape(AshPercent)+
         '&SulfurPercent='+escape(SulfurPercent)+'&WaterPercent='+escape(WaterPercent)+'&CarbonPercent='+escape(CarbonPercent)+'',
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {AddPop();
        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        },
        success: function(data) {
            if (data.sta > 0) {
                popMsgObj.ShowMsg(data.info);
                if (Action == 'Add') {
                    document.getElementById('txtIndentityID').value = data.sta;
                }
                document.getElementById("product_btn_AD").style.display = "inline";
                document.getElementById("product_btnunsure").style.display = "none";
                document.getElementById("divNo").value = data.data;
                document.getElementById("divInputNo").style.display = "block";
                document.getElementById("divNo").style.display = "none";
                document.getElementById('CodingRuleControl1_ddlCodeRule').style.display = 'none';
                document.getElementById('CodingRuleControl1_txtCode').style.display = 'block';
                document.getElementById('CodingRuleControl1_txtCode').value = data.data;
                document.getElementById('CodingRuleControl1_txtCode').className = 'tdinput';
                document.getElementById('CodingRuleControl1_txtCode').style.width = '90%';
                //                        document.getElementById("txt_TypeID").disabled=true;
                if (Action == "Edit") {
                    //document.getElementById('sel_CheckStatus').value = "0";
                    
                    var myDate = new Date();
                    $("#txt_CheckDate").val(myDate.getFullYear()+"-"+(myDate.getMonth()+1)+"-"+myDate.getDate());
                }
//                if (document.getElementById('sel_CheckStatus').value == "0") {
//                    document.getElementById("product_btn_AD").style.display = "block";
//                }
//                else if (document.getElementById('sel_CheckStatus').value == "1") {
//                    document.getElementById("product_btnunsure").style.display = "none";
//                }
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        },
        complete :function(){hidePopup();}
    });

}

function ChangeStatus() {
    var CheckDate = $("#txt_CheckDate").val();
    var StorageID = $("#sel_StorageID").val();
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    if(CheckDate){
    
        if (strlen(CheckDate) > 0) {
            if (!isDate(CheckDate)) {
                isFlag = false;
                fieldText = fieldText + "审核日期|";
                msgText = msgText + "审核日期式不正确|";
            }
        }
        else {
            isFlag = false;
            fieldText = fieldText + "审核日期|";
            msgText = msgText + "请选择审核日期|";
        }
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return;
    }
    requestobj = GetRequest();
    recordnoparam = requestobj['intOtherCorpInfoID'];
    if (typeof (recordnoparam) != "undefined") {
        intOtherCorpInfoID = recordnoparam;
    }
    else {
        intOtherCorpInfoID = document.getElementById('txtIndentityID').value
    }
    var Action = "ChangeStatus";
    var CheckUser = $("#txt_CheckUser").val();
    var SellTaxNew = document.getElementById("txt_SellTax").value; //含税售价
    var StandardSell = document.getElementById("txt_StandardSell").value; //去税售价
    var TaxRate = document.getElementById("txt_TaxRate").value; //销项税率
    var sum = parseFloat(StandardSell) * (parseFloat(TaxRate) / 100 + 1);
    var SellTax = document.getElementById("txt_TaxBuy").value; //含税售价
    var Standard = document.getElementById("txt_StandardBuy").value; //去税售价
    var Tax = document.getElementById("txt_InTaxRate").value; //销项税率
    var sub = parseFloat(SellTax) * (parseFloat(Tax) / 100 + 1);


    if (parseFloat(document.getElementById("txt_SellTax").value).toFixed(2) != sum.toFixed(2) || (isNaN(sum) || isNaN(parseFloat(document.getElementById("txt_SellTax").value).toFixed(2))) || (parseFloat(document.getElementById("txt_StandardBuy").value).toFixed(2) != sub.toFixed(2) || (isNaN(sub) || isNaN(parseFloat(document.getElementById("txt_StandardBuy").value).toFixed(2))))) {
        if (SellTaxNew == "" && (TaxRate == "") && (StandardSell == "") && (SellTax == "" && (Tax == "") && (Standard == ""))) {
            var msg = window.confirm("确认后，物品不能被删除!是否确认该记录？")
            if (msg == true) {
                var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                    },
                    error: function() {
                        popMsgObj.ShowMsg('请求发生错误');
                    },
                    success: function(data) {
                        if (data.sta > 0) {
                            popMsgObj.ShowMsg(data.info);
                            window.location.reload();
//                            document.getElementById('sel_CheckStatus').value = data.sta;
//                            document.getElementById('sel_UsedStatus').value = "1";
//                            document.getElementById("divConfirmor").style.display = "block";
//                            removesel();
//                            document.getElementById('sel_UsedStatus').disabled = false;
//                            document.getElementById("product_btn_AD").style.display = "none";
//                            document.getElementById("product_btnunsure").style.display = "block";
//                            if (isMoreUnit) {
//                                document.getElementById('sel_UnitID').disabled = true;
//                            }                              
                        }
                        else {
                            popMsgObj.ShowMsg(data.info);
                        }
                    }
                });
            }
        }
        else {
            var msg = window.confirm("调整后的含税售价不等于去税售价*（1+销项税率）或调整后的含税进价不等于去税进价*（1+进项税率），是否继续操作？")
            if (msg == true) {
                var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                    },
                    error: function() {
                        popMsgObj.ShowMsg('请求发生错误');
                    },
                    success: function(data) {
                        if (data.sta > 0) {
                            popMsgObj.ShowMsg(data.info);
                            window.location.reload();
//                            document.getElementById('sel_CheckStatus').value = data.sta;
//                            document.getElementById('sel_UsedStatus').value = "1";
//                            document.getElementById("divConfirmor").style.display = "block";
//                            removesel();
//                            document.getElementById('sel_UsedStatus').disabled = false;
//                            document.getElementById("product_btn_AD").style.display = "none";
//                            document.getElementById("product_btnunsure").style.display = "block";                            
                        }
                        else {
                            popMsgObj.ShowMsg(data.info);
                        }
                    }
                });
            }
        }


    }
    else {
        var UrlParam = "Action=" + Action + "&ProductID=" + intOtherCorpInfoID + "&CheckUser=" + CheckUser + "&CheckDate=" + CheckDate + "&StorageID=" + StorageID
        var msg = window.confirm("确认后，物品不能被删除!是否确认该记录？")
        if (msg == true) {
            $.ajax({
                type: "POST",
                url: "../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?" + UrlParam,
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                },
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误');
                },
                success: function(data) {
                    if (data.sta > 0) {
                        popMsgObj.ShowMsg(data.info);
                        window.location.reload();
//                        document.getElementById('sel_CheckStatus').value = data.sta;
//                        document.getElementById('sel_UsedStatus').value = "1";
//                        document.getElementById("divConfirmor").style.display = "block";
//                        removesel();
//                        document.getElementById('sel_UsedStatus').disabled = false;
//                        document.getElementById("product_btn_AD").style.display = "none";
//                        document.getElementById("product_btnunsure").style.display = "block";
                    }
                    else {
                        popMsgObj.ShowMsg(data.info);
                    }
                }
            });
        }

    }

}

function removesel() {
    for (var i = 0; i < document.getElementById("sel_UsedStatus").options.length; i++) {
        if (document.getElementById("sel_UsedStatus").options[i].value == "") {
            document.getElementById("sel_UsedStatus").options.remove(i);
            break;
        }

    }
}
///含税售价、去税售价、税率的相互转换
function LoadSellTaxNew(isLeftToRight) {

    var StandardSellNew = document.getElementById("txt_StandardSell").value; //去税售价
    var TaxRateNew = document.getElementById("txt_TaxRate").value; //销项税率
    var SellTaxNew = document.getElementById("txt_SellTax").value; //含税售价

    var sub = parseFloat(StandardSellNew) * (parseFloat(TaxRateNew) / 100 + 1);

    if (isLeftToRight) {
        /*左：有 && 中：有*/
        if (StandardSellNew != "" && TaxRateNew != "") 
        {
            document.getElementById("txt_SellTax").value = sub.toFixed(glb_SelPoint);/*计算右边*/
        }
        /*左：空 && 中：有 && 右：有*/
        if (StandardSellNew == "" && TaxRateNew != "" && SellTaxNew != "") {
            StandardSellNew = parseFloat(SellTaxNew / (TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_StandardSell").value = StandardSellNew;/*计算左边*/
        }
        /*左：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew=="" && SellTaxNew != "") {
            document.getElementById('txt_TaxRate').value = 100 * parseFloat((SellTaxNew / StandardSellNew) - 1).toFixed(glb_SelPoint);/*计算中间*/
        }
        
    }
    else {
        /*中：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew != "") {
            StandardSellNew = parseFloat(parseFloat(SellTaxNew) / parseFloat(TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_StandardSell").value = StandardSellNew;/*计算左边*/
        }
        /*左：有 && 中：有 && 右：无*/
        if (StandardSellNew != "" && TaxRateNew != "" && SellTaxNew == "") {
            document.getElementById("txt_SellTax").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew == "" && SellTaxNew != "") {
            document.getElementById('txt_TaxRate').value = 100 * parseFloat((SellTaxNew / StandardSellNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
    }
}


function LoadSellTax(isLeftToRight) {
    var SellTaxNew = document.getElementById("txt_TaxBuy").value; //去税进价
    var TaxRateNew = document.getElementById("txt_InTaxRate").value; //进项税率
    var StandardSellNew = document.getElementById("txt_StandardBuy").value; //含税进价

    
    var sub = parseFloat(SellTaxNew) * (parseFloat(TaxRateNew) / 100 + 1);

    if (isLeftToRight) {
        /*左：有 && 中：有*/
        if (SellTaxNew != '' && TaxRateNew != '') {
            document.getElementById("txt_StandardBuy").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：空 && 中：有 && 右：有*/
        if (SellTaxNew == "" && TaxRateNew != "" && StandardSellNew != "") {
            SellTaxNew = parseFloat(StandardSellNew / (TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_TaxBuy").value = SellTaxNew; /*计算左边*/
            
        }
        /*左：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew=="" && StandardSellNew != "") {
            document.getElementById('txt_InTaxRate').value = 100 * parseFloat((StandardSellNew / SellTaxNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
        
    }
    else {
        /*中：有 && 右：有*/
        if (StandardSellNew != "" && TaxRateNew != "") {
            SellTaxNew = parseFloat(parseFloat(StandardSellNew) / parseFloat(TaxRateNew / 100 + 1)).toFixed(glb_SelPoint);
            document.getElementById("txt_TaxBuy").value = SellTaxNew; /*计算左边*/
        }
        /*左：有 && 中：有 && 右：无*/
        if (SellTaxNew != "" && TaxRateNew != "" && StandardSellNew == "") {
            document.getElementById("txt_StandardBuy").value = sub.toFixed(glb_SelPoint); /*计算右边*/
        }
        /*左：有 && 右：有*/
        if (SellTaxNew != "" && TaxRateNew == "" && StandardSellNew != "") {
            document.getElementById('txt_InTaxRate').value = 100 * parseFloat((StandardSellNew / SellTaxNew) - 1).toFixed(glb_SelPoint); /*计算中间*/
        }
    }

}

/*验证规格只允许+特殊字符可以输入*/
function CheckSpecification(str) {
    var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/","*"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
    for (var i = 0; i < SpWord.length; i++) {
        for (var j = 0; j < str.length; j++) {
            if (SpWord[i] == str.charAt(j)) {
                return false;
                break;
            }
        }
    }
    return true;
}



/*返回*/
function DoBack() {
    var searchCondition = document.getElementById("hidSearchCondition").value;
    if (SysModuleID>0) {
        //获取模块功能ID
        window.location.href = "../SystemManager/InitGuid.aspx?ModuleID=" + SysModuleID;
    }
    else {
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "ProductInfoList.aspx?ModuleID=" + ModuleID + searchCondition;
    }
}


