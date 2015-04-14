eventObj.Table = 'dg_Log';
$(document).ready(function() {
    var InNoID = document.getElementById("IndentityID").value;
    if (InNoID > 0) {

        LoadDetailInfo(InNoID);
     
    }
    else {
        try {
           
            $("#btn_save").show();
            $("#btnPageFlowConfrim").show();
             $("#Img4").show();
            $("#btn_Unclic_Close").show();
            $("#btn_Unclick_CancelClose").show();
            
           
        }
        catch (e) { }
    }
});
function LoadDetailInfo(InNoID) {
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Office/StorageManager/Disassembling.ashx", //目标地址
        data:"action=&ID="+InNoID,
        cache: false,
        success: function(msg) {
           
            //数据获取完毕，填充页面据显示
            document.getElementById('lbInNo').innerHTML = msg.data[0].billno;
            document.getElementById('div_InNo_uc').style.display = "none";
            document.getElementById('div_InNo_Lable').style.display = "block";
            $("#BomName").val( msg.data[0].Tproductname); //套件名称
            $("#BomID").val(msg.data[0].bomid);//套件名称id

            $("#TotalPrice").val(msg.data[0].totalprice); //拆装费
            $("#remark").val(msg.data[0].remark); //备注
            document.getElementById('DeptName').value = msg.data[0].deptname; //部门
            
            $("#txtDeptID").val(msg.data[0].departmentid); //部门ID
            document.getElementById('UserExecutor').value = msg.data[0].handsman;//经手人
            $("#txtExecutorID").val(msg.data[0].handsmanid); //经手人ID
            document.getElementById('createrID').value = msg.data[0].creater;//制单人id

            document.getElementById('creater').value = msg.data[0].creatname;//制单人
            document.getElementById('createDate').value = msg.data[0].CreatDate;//制单时间
            document.getElementById('sltBillStatus').value = msg.data[0].status;//单据状态
            document.getElementById('Confirmor').value = msg.data[0].Confirmer;//确认人
            document.getElementById('ConfirmDate').value = msg.data[0].ConfirmDate;//确认时间
            document.getElementById('Closer').value = msg.data[0].Closername;//结单人
            document.getElementById('CloseDate').value = msg.data[0].CloseDate;//结单时间
            document.getElementById('ModifiedUserID').value = msg.data[0].Updater;//最后更新人

            document.getElementById('ModifiedDate').value = msg.data[0].UpdateDate;//最后更新时间
            document.getElementById('IndentityID').value = msg.data[0].id;//id 

            $.each(msg.data, function(i, item) {

                if (item.productid != null && item.productid != '')
                 {
                  FillSignRow(i,item.typesname,item.Types,item.productid,item.prodno,item.productname,item.specification,item.unitid,item.Storageid,item.quota,item.price,item.codename,item.batch,item.amount,item.usedcount)
                }
            });
            if (document.getElementById('sltBillStatus').value == 1)//根据单据状态显示按钮
            {
                try {
                    $("#Confirm").show();
                    $("#btn_save").show();
                     $("#Img4").show();
                      $("#Img2").hide();
                    $("#btn_Unclic_Close").show();
                    $("#btn_Unclick_CancelClose").show();
                   
                }
                catch (e) { }
            }
            else if (document.getElementById('sltBillStatus').value == 2) {
                try {
                    $("#btn_save").hide();
                    $("#Confirm").hide();
                    $("#btn_UnClick_bc").show();
                    $("#btnPageFlowConfrim").show();
                    $("btn_Unclick_CancelClose").show();
                    $("#btn_Close").show();
                    $("#Img4").hide();
                      $("#Img2").show();
                    $("#btn_Unclic_Close").hide();
                    $("#btn_CancelClose").hide();
                    $("#btn_Unclick_CancelClose").show();
                    document.getElementById('btn_AddRow').onclick = function() { return false; };
                    document.getElementById('btnDele').onclick = function() { return false; };
                }
                catch (e) { }
            }
            else {
                try {
                    $("#btn_save").hide();
                    $("#Confirm").hide();
                    $("#btn_UnClick_bc").show();
                    $("#btnPageFlowConfrim").show();
                    $("#btn_Close").hide();
                    $("#Img4").show();
                      $("#Img2").hide();
                    $("#btn_Unclic_Close").show();
                    $("#btn_CancelClose").show();
                    $("#btn_Unclick_CancelClose").hide();
                    document.getElementById('btn_AddRow').onclick = function() { return false; };
                    document.getElementById('btnDele').onclick = function() { return false; };
                }
                catch (e) { }
            }

        },
        error: function() { },
        complete: function() { }
    });
 
}
//保存数据
    function Fun_Save_Disassembling() {
    
    var bmgz = "";
    var pcgz = "";
    var BratchNo = "null";
    var Flag = true;
    
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var IndentityID = $("#IndentityID").val();
    if (IndentityID == "0")//新建
    {
        if ($("#txtInNo_ddlCodeRule").val() == "")//手工输入
        {
            txtInNo = $("#txtInNo_txtCode").val();
            bmgz = "sd";
            if (txtInNo == "") {
                isFlag = false;
                fieldText += "单据编号|";
                msgText += "请输入单据编号|";
            }
            else if (!CodeCheck($("#txtInNo_txtCode").val())) {
                isFlag = false;
                fieldText = fieldText + "单据编号|";
                msgText = msgText + "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
            }
        }
        else {

            txtInNo = $("#txtInNo_ddlCodeRule").val();
            bmgz = "zd";
        }
    }
    else {
        txtInNo = document.getElementById("lbInNo").innerHTML;
        
    }
    //var txtInNo=$("#txtInNo_ddlCodeRule").val(); 
    var BomID = $("#BomID").val();
    var txtDeptID = $("#txtDeptID").val(); //部门
    if(txtDeptID==undefined||txtDeptID=="")
    {
        txtDeptID="0";
    }
    var TotalPrice = $("#TotalPrice").val();//拆装费
    var txtExecutor = $("#txtExecutorID").val();//经手人
     if(txtExecutor==undefined||txtExecutor=="")
    {
        txtExecutor="0";
    }
    var remark = $("#remark").val(); //备注
    var createrID = $("#createrID").val(); //制单人
    var createDate = $("#createDate").val(); //制单时间
    var stauts = $("#sltBillStatus").val(); //单据状态
    var Confirmor = $("#Confirmor").val(); //确认人
    var ConfirmDate = $("#ConfirmDate").val(); //确认时间
    var Closer = $("#Closer").val(); //结单人
    var CloseDate = $("#CloseDate").val(); //结单时间
    var billtype="2";
    if(document.getElementById("thisModuleID").value=="2051503")
    {
        billtype="1";
    }
    if (strlen(txtInNo) > 50) {
        isFlag = false;
        fieldText = fieldText + "单据编号|";
        msgText = msgText + "仅限于50个字符以内|";
    }

    

   
    if (strlen(remark) > 200) {
        isFlag = false;
        fieldText = fieldText + "备注|";
        msgText = msgText + "仅限于200个字符以内|";
    }
    //明细验证
    var signFrame = findObj("dg_Log", document);
    if (signFrame.rows.length <= 1) {
        isFlag = false;
        fieldText = fieldText + "明细信息|";
        msgText = msgText + "明细信息不能为空|";
    }
//    var ifProductCount = "0";
//    var ifProductNo = "0";
//    var ifUnitPrice = "0";
//    var ifRemark = "0";
//    var ifbig = "0"; //是否大于应收数量
//    var rownum = null; //第几行大于源单数量
//    for (i = 1; i < signFrame.rows.length; i++) {
//        var ProductCount;
//        if(document.getElementById("hidMoreUnit").value == "true")
//        {
//            ProductCount = "OutCount_SignItem_TD_Text_" + i;
//        }
//        else
//        {
//            ProductCount = "InCount_SignItem_TD_Text_" + i;
//        }
//        
//        //var ProductC = "OutCount_SignItem_TD_Text_" + i;
//        var UnitPrice = "UnitPrice_SignItem_TD_Text_" + i;
//        var Remark = "Remark_SignItem_TD_Text_" + i;
//        if (document.getElementById(ProductCount).value == "" || parseFloat(document.getElementById(ProductCount).value) <= 0 || document.getElementById(ProductCount).value == "0.00") {
//            ifProductCount = "1";
//        }
////        if (document.getElementById(ProductC).value == "" || parseFloat(document.getElementById(ProductC).value) <= 0) {
////            ifProductC = "1";
////        }
//        if (IsDisplayPrice!="none"&&(document.getElementById(UnitPrice).value == "" || parseFloat(document.getElementById(UnitPrice).value) <=0 || document.getElementById(UnitPrice).value == "0.00") ) {
//            ifUnitPrice = "1";
//        }
//        if (strlen($("#" + Remark).val()) > 200) {
//            ifRemark = "1";
//        }
//    }

//    if (ddlFromType == "0") {
//        for (i = 1; i < signFrame.rows.length; i++) {
//            var ProductNo = "ProductNo_SignItem_TD_Text_" + i;
//            if ($("#" + ProductNo).val() == "" || $("#" + ProductNo).attr("title") == "undefined") {
//                ifProductNo = "1";
//            }
//        }
//    }
//    if (ddlFromType == "1") {
//        for (i = 1; i < signFrame.rows.length; i++) {
//             var InCount;
//            if(document.getElementById("hidMoreUnit").value == "true")
//            {
//                InCount = "OutCount_SignItem_TD_Text_" + i;
//            }
//            else
//            {
//                InCount = "InCount_SignItem_TD_Text_" + i;
//            }
//            
//            var NotInCount = "NotInCount_SignItem_TD_Text_" + i;
//            if (parseFloat(document.getElementById(InCount).value) > parseFloat(document.getElementById(NotInCount).value)) {
//                ifbig = "1"; rownum = i;
//                break;
//            }
//        }
//    }

//    if (ifProductNo == "1") {
//        isFlag = false;
//        fieldText = fieldText + "明细物品|";
//        msgText = msgText + "请选择明细物品|";
//    }

//    if (ifUnitPrice == "1" && IsDisplayPrice != "none") {
//        isFlag = false;
//        fieldText = fieldText + "明细单价|";
//        msgText = msgText + "请输入有效的数值（大于0）|";
//    }

//    if (ifProductCount == "1") {
//        isFlag = false;
//        fieldText = fieldText + "基本数量|";
//        msgText = msgText + "请输入有效的数值（大于0）|";
//    }

//    if (ifbig == "1") {
//        isFlag = false;
//        fieldText = fieldText + "入库数量|";
//        msgText = msgText + "第" + rownum + "行入库数量不能大于未入库数量|";
//    }
//    if (ifRemark == "1") {
//        isFlag = false;
//        fieldText = fieldText + "明细备注|";
//        msgText = msgText + "仅限于200个字符以内|";
//    }

//    var List_TB = findObj("dg_Log", document);
//    var rowlength = List_TB.rows.length;
//    for (var i = 1; i < rowlength - 1; i++) {
//        for (var j = i + 1; j < rowlength; j++) {
//            var ProductNoi = "ProductNo_SignItem_TD_Text_" + i;
//            var ProductNoj = "ProductNo_SignItem_TD_Text_" + j;
//            var StorageIDi = " StorageID_SignItem_TD_Text_" + i;
//            var StorageIDj = " StorageID_SignItem_TD_Text_" + j;
////            if ($("#" + ProductNoi).val() == $("#" + ProductNoj).val() && $("#" + StorageIDi).val() == $("#" + StorageIDj).val()) {
////                isFlag = false;
////                fieldText = fieldText + "明细信息|";
////                msgText = msgText + "明细中不允许存在重复记录|";
////                break;
////            }
//        }
//    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }

    else {

        //期初库存明细

        var DetailProductID = new Array();//物品编号
        var Detailtpyes=new Array();//类型
        var DetailUnitID = new Array();//单位
        var DetailStorageID = new Array();//仓库
     
        var DetailProductCount = new Array();//数量
        var DetailUnitPrice = new Array();//单价
        var DetailTotalPrice = new Array();//金额
         var DetailBatchNo = new Array();//批次
         var quota=new Array();//定量
         
        
        
        var signFrame = findObj("dg_Log", document);
        var count = signFrame.rows.length; //有多少行
        for (var i = 1; i < count; i++) {
            if (signFrame.rows[i].style.display != "none") {
                var objProductNo = 'ProductNo_SignItem_TD_Text_' + (i);
                var objtypes='typename_'+(i);
                var objUnitID = 'UnitID_SignItem_TD_Text_' + (i);
                var objStorageID = 'StorageID_SignItem_TD_Text_' + (i);
                var objProductCount = 'ProCount_SignItem_TD_Text_' + (i);
                var objUnitPrice = 'UnitPrice_SignItem_TD_Text_' + (i);
                var objTotalPrice = 'TotalPrice_SignItem_TD_Text_' + (i);
                var objBatch = 'Batch_SignItem_TD_Text_' + (i);
                var objquota = 'Quota_SignItem_TD_Text_' + (i);
                var objRadio = 'chk_Option_' + (i);
                //var objUnitPrice = 'UnitPrice_SignItem_TD_Text_' + (i);

                DetailProductID.push($("#" + objProductNo).attr("title"));
               
                DetailUnitID.push($("#" + objUnitID).attr("title"));
                DetailStorageID.push(document.getElementById(objStorageID.toString()).value);
                Detailtpyes.push($("#" + objtypes).attr("title"));
                DetailProductCount.push(document.getElementById(objProductCount.toString()).value);
                DetailUnitPrice.push(document.getElementById(objUnitPrice.toString()).value);
                DetailTotalPrice.push(document.getElementById(objTotalPrice.toString()).value);
                quota.push(document.getElementById(objquota.toString()).value);
                DetailBatchNo.push($("#" + objBatch).val());//用于存储批次是否启用的数组
               
            }
        }

         var UrlParam = "&txtInNo="+escape(txtInNo)+
                        "&BomID="+escape(BomID)+
                        "&TotalPrice="+escape(TotalPrice)+
                        "&txtExecutor="+escape(txtExecutor)+
                        "&remark="+escape(remark)+
                        "&createrID="+escape(createrID)+
                        "&createDate="+escape(createDate)+
                        
                        "&Confirmor="+escape(Confirmor)+
                        "&ConfirmDate="+escape(ConfirmDate)+
                        "&Closer="+escape(Closer)+
                        "&CloseDate="+escape(CloseDate)+
                        "&txtDeptID="+escape(txtDeptID)+
                        "&billtype="+escape(billtype)+
                        
                        "&DetailProductID="+escape(DetailProductID)+
                        "&DetailUnitID="+escape(DetailUnitID)+
                        "&DetailStorageID="+escape(DetailStorageID)+
                        "&Detailtpyes="+escape(Detailtpyes)+
                        "&DetailProductCount="+escape(DetailProductCount.toString())+
                        "&DetailUnitPrice="+escape(DetailUnitPrice.toString())+
                        "&DetailTotalPrice="+escape(DetailTotalPrice.toString())+
                        "&quota="+escape(quota.toString())+
                        "&DetailBatchNo="+escape(DetailBatchNo.toString())+
                        "&action=edit"+
                        "&bmgz="+bmgz+
                        "&pcgz="+pcgz+
                        "&ID="+IndentityID.toString();
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx",
            dataType: 'json', //返回json格式数据
            cache: false,
            data:UrlParam,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {

                        document.getElementById('div_InNo_uc').style.display = "none";
                        document.getElementById('div_InNo_Lable').style.display = "";
//                        if(document.getElementById("hidBatchNo").value == "true")//是否启用批次
//                        {
//                            document.getElementById("divBatchNo").style.display = "none";
//                            document.getElementById("divBatchNoShow").style.display = "block";
//                        }
                        
                        if (parseInt(IndentityID) <= 0) {
                            document.getElementById('lbInNo').innerHTML = reInfo[1];
                            document.getElementById('IndentityID').value = reInfo[0];
                            document.getElementById('ModifiedUserID').value = reInfo[2];
                            document.getElementById('ModifiedDate').value = reInfo[3];
                           
                            
                        }
                        else {
                            document.getElementById('ModifiedUserID').value = reInfo[0];
                            document.getElementById('ModifiedDate').value = reInfo[1];
                        }
                    }
                    try {
                        $("#btnPageFlowConfrim").hide();
                        $("#Confirm").show();
                    }
                    catch (e) { }
                    popMsgObj.ShowMsg(data.info);
                }
                else {
                    popMsgObj.ShowMsg(data.info);
                }

               
            }
        });
    }
}

/*BOM选择*/
function Fun_FillParent_BomContent(bomID, bomNo, routeID, routeName,proname) {
    if (popBomObj.InputObj != null) {
        document.getElementById(popBomObj.InputObj).value = bomID;
        document.getElementById('BomName').value = proname;
        closeRotoscopingDiv(false, 'divPopBomShadow');
        document.getElementById('divBom').style.display = 'none';
        var isall="";
        if(parseFloat(bomID)>0)
        {
           if(confirm("是否展开至最末级？"))
           {
                isall="Yes";
           }
           $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx?",
            dataType: 'json', //返回json格式数据
            cache: false,
            data:"BomID="+bomID+"&isall="+isall+"&action=GetBom",
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');
            },
            success: function(msg) {
                 $("#dg_Log tbody").find("tr.newrow").remove();
                 $.each(msg.data,function(i,item)
                 {
                    FillSignRow(i,item.typename,item.typeid,item.id,item.prodno,item.productname,item.specification,item.unitid,item.storageid,item.quota,item.standardsell,item.codename,'','','')
                 });
            }
        });
        
        }
    }
    else {
        popMsgObj.ShowMsg('系统错误');
    }
}
/*清空BOM*/
function ClearBomControl() {
    if (popBomObj.InputObj != null) {
        document.getElementById( popBomObj.InputObj).value = '';
        document.getElementById('BomName').value = '';
        document.getElementById('divBom').style.display = 'none';
        closeRotoscopingDiv(false, 'divPopBomShadow');
    }
}


function FillSignRow(i, typename, typeid, ProductID, ProductNo, ProductName, Specification,UnitID, StorageID, quota, price,codename,batch,amount,pcount)
{

    var x=0;
    var signFrame = findObj("dg_Log", document);
    var rowID = signFrame.rows.length;
    var newTR = signFrame.insertRow(rowID); //添加行
    newTR.id = "SignItem_Row_" + rowID;
    newTR.className="newrow";

    var newNameXH = newTR.insertCell(x); //添加列:选择
    newNameXH.className = "cell";
    newNameXH.id = 'SignItem_TD_Check_' + rowID;
    newNameXH.innerHTML = "<input name='chk1' id='chk_Option_" + rowID + "' value=\"\" type='checkbox' onclick=\"IfSelectAll('chk1','checkall');\"/>";
    x++;
    

    var newFitNametd = newTR.insertCell(x); //添加列:物品编号
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_ProductNo_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"ProductNo_SignItem_TD_Text_" + rowID + "\" value=\"" + ProductNo + "\" title=\"" + ProductID + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%''  />"; //添加列内容
    x++;
    
    var newFitDesctd = newTR.insertCell(x); //添加列:物品名称
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_ProductName_' + rowID;
    newFitDesctd.innerHTML = "<input id='ProductName_SignItem_TD_Text_" + rowID + "' value=\"" + ProductName + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%'' />"; ; //添加列内容
    x++;
    
    var newNameTD = newTR.insertCell(x); //添加列:类型
    newNameTD.className = "cell";
    newNameTD.id = 'SignItem_TD_Indexs_' + rowID;
    newNameTD.innerHTML ="<input type=\"text\" id=\"typename_" + rowID + "\" value=\"" + typename + "\" title=\"" + typeid + "\" class=\"tdinput\"  readonly=\"readonly\" style='width:90%''  />";
    x++;
    
    var newFitNametd = newTR.insertCell(x); //添加列:仓库ID
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_StorageID_' + rowID;
    newFitNametd.innerHTML = "<select class='tdinput' id=\"StorageID_SignItem_TD_Text_" + rowID + "\"   style=' width:90%;'>" + document.getElementById("ddlStorageInfo").innerHTML + "</select>";
    document.getElementById("StorageID_SignItem_TD_Text_" + rowID).value = StorageID; //赋值当前的的仓库
    x++;
    
    var newFitDesctd = newTR.insertCell(x); //添加列:物品规格
    newFitDesctd.className = "cell";
    newFitDesctd.id = 'SignItem_TD_Specification_' + rowID;
    newFitDesctd.innerHTML = "<input id='Specification_SignItem_TD_Text_" + rowID + "' value=\"" + Specification + "\" type='text' class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容
    x++;
    
    var newBatch = newTR.insertCell(x); //添加列:批次
    newBatch.className = "cell";
    newBatch.id = 'SignItem_TD_Batch_' + rowID;
    newBatch.innerHTML = "<input id='Batch_SignItem_TD_Text_" + rowID + "' value=\""+batch+"\" type='text' onclick=\"popBatchObj.ShowListCheckSpecial('Batch_SignItem_TD_Text_"+rowID+"','"+ProductNo+"')\" class=\"tdinput\" readonly=\"readonly\"   style='width:90%' />"; ; //添加列内容
    x++;
    
     var newFitNametd = newTR.insertCell(x); //添加列:定量
    newFitNametd.className = "tdinput";
    newFitNametd.id = 'SignItem_TD_ProductCount_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"Quota_SignItem_TD_Text_" + rowID + "\"  onblur=\"quotaorcount("+rowID+",'Quota_SignItem_TD_Text_"+ rowID + "','','','')\" value=\"" + NumberSetPoint(quota) + "\"  class=\"tdinput\"  style='width:90%' />"; //添加列内容
    x++;
    
    var newFitNametd = newTR.insertCell(x); //添加列:单位
    newFitNametd.className = "cell";
    newFitNametd.id = 'SignItem_TD_UnitID_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"UnitID_SignItem_TD_Text_" + rowID + "\" value=\"" + codename + "\"  title=\"" + UnitID + "\" class=\"tdinput\" readonly=\"readonly\"  style='width:90%''  />"; //添加列内容
    x++;

   
    

    var newShuLiangtd = newTR.insertCell(x); //添加列:数量?
    newShuLiangtd.className = "tdinput";
    newShuLiangtd.id = 'SignItem_TD_ProductCount_' + rowID;
    newShuLiangtd.innerHTML = "<input type=\"text\" id=\"ProCount_SignItem_TD_Text_" + rowID + "\" onblur=\"quotaorcount("+rowID+",'','ProCount_SignItem_TD_Text_"+ rowID + "','','')\" value=\"" + NumberSetPoint(quota) + "\"  class=\"tdinput\"  style='width:90%' onblur=\"check(this.id);Number_roundBegin(this,2);TotalPrice_z(this.id);CountNum();\"/>"; //添加列内容
    x++;
    if(pcount!='')
    {
        document.getElementById("ProCount_SignItem_TD_Text_"+ rowID).value=NumberSetPoint(pcount);
    }

//if(document.getElementById("hidMoreUnit").value == "true")
//{
//        newDanWeitd.style.display = "block";
//       newShuLiangtd.style.display = "block";
//       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID). readOnly = true;
//       document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).onblur = function(){ return false;}
//}
//else
//{
//       newDanWeitd.style.display = "none";
//       newShuLiangtd.style.display = "none";
//        document.getElementById("ProductCount_SignItem_TD_Text_"+rowID).readOnly = false;
//}

    var newFitDesctd = newTR.insertCell(x); //添加列:单价
    newFitDesctd.className = "cell";
    
    newFitDesctd.id = 'SignItem_TD_UnitPrice_' + rowID;
    newFitDesctd.innerHTML = "<input name='chk' id='UnitPrice_SignItem_TD_Text_" + rowID + "' onblur=\"quotaorcount("+rowID+",'','','UnitPrice_SignItem_TD_Text_"+ rowID + "','')\" value=\"" + NumberSetPoint(price) + "\" type='text' class=\"tdinput\"  style='width:90%'     />"; //添加列内容
    x++;
    
    var newFitNametd = newTR.insertCell(x); //添加列:物品总价（金额）
    newFitNametd.className = "cell";
    
    newFitNametd.id = 'SignItem_TD_TotalPrice_' + rowID;
    newFitNametd.innerHTML = "<input type=\"text\" id=\"TotalPrice_SignItem_TD_Text_" + rowID + "\" value=\""+amount+"\" onblur=\"quotaorcount("+rowID+",'','','','TotalPrice_SignItem_TD_Text_"+ rowID + "')\" class=\"tdinput\"  style='width:90%''  readonly=\"readonly\" />"; //添加列内容
    x++;
    if(document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value==""||parseFloat(document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value)==0)
    {
        document.getElementById("TotalPrice_SignItem_TD_Text_" + rowID).value = (quota * price).toFixed($("#hidSelPoint").val());
    }

   document.getElementById("txtTRLastIndex").value=(rowID + 1).toString() ;//将行号推进下一行
   
}
   
function NumberSetPoint(num)
{
    var SetPoint = parseFloat(num).toFixed($("#hidSelPoint").val());
    return SetPoint;
}
//删除行
function DeleteSignRow() {
    //获取表格
    table = document.getElementById("dg_Log");
    var count = table.rows.length;
    if(count==3)
    {
         popMsgObj.ShowMsg('必须保留一个散件一个套件');
         return;
    }
    var rowscount=0;
    var istaojian=false;
    for (var i = count - 1; i > 0; i--) {
        var select = document.getElementById("chk_Option_" + i);
        if (select.checked) {
            if($("#typename_"+i).attr("title")=="1")
            {
               
            }else
            {
                istaojian=true;
            }
        }else
        {
            rowscount++;
        }
    }
    if(rowscount<2)
    {
         popMsgObj.ShowMsg('必须保留一个散件一个套件');
         return;
    }
    if(istaojian)
    {
        popMsgObj.ShowMsg('套件不能删除');
        return;
    }
    for (var i = count - 1; i > 0; i--) {
        var select = document.getElementById("chk_Option_" + i);
        if (select.checked) {
           DeleteRow(table, i);
        }
    }
}

function DeleteRow(table, row) {
    //获取表格
    var count = table.rows.length - 1;
    table.deleteRow(row);
    if (row < count)//
    {
        for (var j = parseInt(row) + 1; j <= count; j++) {
            var no = j - 1;
//            document.getElementById("SignItem_TD_Index_" + j).innerHTML = parseInt(document.getElementById("SignItem_TD_Index_" + j).innerHTML, 10) - 1;
//            document.getElementById("SignItem_TD_Index_" + j).id = "SignItem_TD_Index_" + no;
            document.getElementById("chk_Option_" + j).id = "chk_Option_" + no;
            document.getElementById("ProductNo_SignItem_TD_Text_" + j).id = "ProductNo_SignItem_TD_Text_" + no;
            document.getElementById("ProductName_SignItem_TD_Text_" + j).id = "ProductName_SignItem_TD_Text_" + no;
            document.getElementById("Specification_SignItem_TD_Text_" + j).id = "Specification_SignItem_TD_Text_" + no;
            document.getElementById("UnitID_SignItem_TD_Text_" + j).id = "UnitID_SignItem_TD_Text_" + no;
            document.getElementById("StorageID_SignItem_TD_Text_" + j).id = "StorageID_SignItem_TD_Text_" + no;
            document.getElementById("typename_" + j).id = "typename_" + no;
            document.getElementById("Batch_SignItem_TD_Text_" + j).id = "Batch_SignItem_TD_Text_" + no;
            document.getElementById("Quota_SignItem_TD_Text_" + j).id = "Quota_SignItem_TD_Text_" + no;
            document.getElementById("UnitPrice_SignItem_TD_Text_" + j).id = "UnitPrice_SignItem_TD_Text_" + no;
            document.getElementById("TotalPrice_SignItem_TD_Text_" + j).id = "TotalPrice_SignItem_TD_Text_" + no;
           

        }
    }
}
//添加行
function ShowProdInfo() 
{
    //然后调用添加空白行
    //AddRow_kb();
    //popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"', 'Check');
    popTechObj.ShowListCheckSpecial('SubStorageIn"+rowID+"', 'Check');

}

//多选明细方法---

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
        url: '../../../Handler/Office/SupplyChain/ProductCheck.ashx?str=' + str, //目标地址
        cache: false,
        data: '', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $.each(msg.data, function(i, item) {//填充物品ID，物品编号，物品名称，规格，单位ID，单位名称，入库数量(默认为0)
                if (!IsExist(item.ProdNo)) {
                    //AddRow(id,ProNo,ProdName,UnitName,Specification,UnitPrice)
                   var rowid= document.getElementById("txtTRLastIndex").value;
                  
                   FillSignRow(parseInt(rowid)+1,'散件','1',item.ID, item.ProdNo, item.ProductName,item.Specification,item.UnitID,item.StorageID,1,item.StandardSell,item.CodeName,'','','');
                }
            });

        },

        complete: function() { } //接收数据完毕
    });
    closeProductdiv();
}

//是否有重复数据
function IsExist(prodNo) {
    var signFrame = document.getElementById("dg_Log");
    if ((typeof (signFrame) == "undefined") || signFrame == null) {
        return false;
    }
    for (var i = 1; i < signFrame.rows.length; ++i) {
        var prodNo1 = document.getElementById("ProductNo_SignItem_TD_Text_" + i).value;

        if ((signFrame.rows[i].style.display != "none") && (prodNo1 == prodNo)) {
            rerowID = i;
            return true;
        }
    }
    return false;
}

//库存快照
function ShowSnapshot() {

    var intProductID = 0;
    var detailRows = 0;
    var snapProductName = '';
    var snapProductNo = '';

    var signFrame = findObj("dg_Log", document);
    var txtTRLastIndex = signFrame.rows.length;


    if (txtTRLastIndex > 1) {
        for (var i = 1; i < txtTRLastIndex; i++) {
            if (document.getElementById('chk_Option_' + i).checked) {
                detailRows++;
                intProductID = document.getElementById('ProductNo_SignItem_TD_Text_' + (i)).title;
                snapProductName = document.getElementById('ProductName_SignItem_TD_Text_' + (i)).value;
                snapProductNo = document.getElementById('ProductNo_SignItem_TD_Text_' + (i)).value;
            }
        }

        if (detailRows == 1) {
            if (intProductID <= 0 || strlen(cTrim(intProductID, 0)) <= 0) {
                popMsgObj.ShowMsg('选中的明细行中没有添加物品');
                return false;
            }

            ShowStorageSnapshot(intProductID, snapProductName, snapProductNo);
        }
        else {
            popMsgObj.ShowMsg('请选择单个物品查看库存快照');
            return false;
        }
    }
}
//确认
function ConfirmBill()
{
      if (confirm('确定要确认吗？')) {

        var signFrame = findObj("dg_Log", document);
    
            for (i = 1; i < signFrame.rows.length; i++) 
            {
                var InCount = "ProCount_SignItem_TD_Text_"+i;
                
                    
                var NotInCount = "Quota_SignItem_TD_Text_" + i;
                if (parseFloat(document.getElementById(InCount).value) <=0|| parseFloat(document.getElementById(NotInCount).value)<=0) {
                    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "物品数量或定量必须大于0");
                    return false;
                }
            }
        
      
          var billtype="2";
        if(document.getElementById("thisModuleID").value=="2051503")
        {
            billtype="1";
        }
        var InNoID = $("#IndentityID").val();
        var UrlParam = "&action=Confirm&ID=" + InNoID + "&billtype="+billtype;
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx",
            dataType: 'json', //返回json格式数据
            cache: false,
            data:UrlParam,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('ModifiedUserID').value = reInfo[0];
                        document.getElementById('ModifiedDate').value = reInfo[1];
                        document.getElementById('Confirmor').value = reInfo[0];
                        document.getElementById('ConfirmDate').value = reInfo[1];
                    }
                    document.getElementById('sltBillStatus').value = 2;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").show();
                         $("#Img4").hide();
                        $("#Img2").show();
                        $("#btn_Unclic_Close").hide();
                        $("#btn_CancelClose").hide();
                        $("#btn_Unclick_CancelClose").show();
                        document.getElementById('btn_AddRow').onclick = function() { return false; };
                         document.getElementById('btnDele').onclick = function() { return false; };
                    }
                    catch (e) { }
                }
                
                
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
}
//取消确认
function UnConfirmBill()
{
      if (confirm('确定要取消确认吗？')) {
        var billtype="2";
        if(document.getElementById("thisModuleID").value=="2051503")
        {
            billtype="1";
        }
        var InNoID = $("#IndentityID").val();
        var UrlParam = "&action=UnConfirm&ID=" + InNoID + "&billtype="+billtype;
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx",
            dataType: 'json', //返回json格式数据
            cache: false,
            data:UrlParam,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('ModifiedUserID').value = reInfo[0];
                        document.getElementById('ModifiedDate').value = reInfo[1];
                        document.getElementById('Confirmor').value = "";
                        document.getElementById('ConfirmDate').value = "";
                    }
                    document.getElementById('sltBillStatus').value = 1;
                    try {
                        $("#btn_save").show();
                        $("#btn_UnClick_bc").hide();
                        $("#Confirm").show();
                        $("#Img4").show();
                        $("#Img2").hide();
                        $("#btnPageFlowConfrim").hide();
                        $("#btn_Close").hide();
                        $("#btn_Unclic_Close").hide();
                        $("#btn_CancelClose").hide();
                        $("#btn_Unclick_CancelClose").show();
                        document.getElementById('btn_AddRow').onclick = ShowProdInfo;
                        document.getElementById('btnDele').onclick = DeleteSignRow;
                    }
                    catch (e) { }
                }
                
                
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
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
}
//结单
function CloseBill()
{
         if (confirm('确认要进行结单操作吗？')) {
        var IndentityID = $("#IndentityID").val();
        var UrlParam = "&action=Close&ID=" + IndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx" ,
            data:UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('ModifiedUserID').value = reInfo[0];
                        document.getElementById('ModifiedDate').value = reInfo[1];
                        document.getElementById('Closer').value = reInfo[0];
                        document.getElementById('CloseDate').value = reInfo[1];
                    }
                    document.getElementById('sltBillStatus').value = 3;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").hide();
                         $("#Img4").hide();
                        $("#Img2").hide();
                        $("#btn_Unclic_Close").show();
                        $("#btn_CancelClose").show();
                        $("#btn_Unclick_CancelClose").hide();
                    }
                    catch (e) { }
                }
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
   
}
//取消结单
function CancelCloseBill()
{
     if (confirm('确认要进行取消结单操作吗？')) {
        var IndentityID = $("#IndentityID").val();
        var UrlParam = "&action=CancelClose&ID=" + IndentityID.toString() + "";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/StorageManager/Disassembling.ashx",
            data:UrlParam,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                AddPop();
            },
            complete: function() { hidePopup(); },
            error: function() {
                popMsgObj.ShowMsg('请求发生错误');

            },
            success: function(data) {
                if (data.sta == 1) {
                    var reInfo = data.data.split('|');
                    if (reInfo.length > 1) {
                        document.getElementById('ModifiedUserID').value = reInfo[0];
                        document.getElementById('ModifiedDate').value = reInfo[1];
                        document.getElementById('Closer').value = "";
                        document.getElementById('CloseDate').value = "";
                    }
                    document.getElementById('sltBillStatus').value = 2;
                    try {
                        $("#btn_save").hide();
                        $("#Confirm").hide();
                        $("#btn_UnClick_bc").show();
                        $("#btnPageFlowConfrim").show();
                        $("#btn_Close").show();
                         $("#Img4").hide();
                        $("#Img2").show();
                        $("#btn_Unclic_Close").hide();
                        $("#btn_CancelClose").hide();
                        $("#btn_Unclick_CancelClose").show();
                    }
                    catch (e) { }
                }
                popMsgObj.ShowMsg(data.info);
            }
        });
    }
    else {
        return false;
    }
}

//获取批次
function Fun_FillBatch(id,batch,storageid,storagename)
{
    document.getElementById('divStorageProduct1').style.display='none';
    if(id!=""||id!=undefined)
    {
        var str=id.toString().split('_');
        document.getElementById(id).value=batch;
        document.getElementById("StorageID_SignItem_TD_Text_" + str[str.length-1] ).value=storageid;
         document.getElementById("StorageID_SignItem_TD_Text_" + str[str.length-1] ).disabled="disabled";
    }
}

//定额数量换算
function quotaorcount(rowid,quotaid,countid,priceid,totalpriceid)
{
    table = document.getElementById("dg_Log");
    var count = table.rows.length;
    var point=parseInt(document.getElementById("hidSelPoint").value);
   if(quotaid!="")
   {
        if(rowid=="1")
        {   
           
            var tquota=document.getElementById(quotaid).value;
            document.getElementById(quotaid).value=parseFloat(tquota).toFixed(point);
            if(document.getElementById(quotaid).value=="NaN")
            {
                document.getElementById(quotaid).value=parseFloat("1").toFixed(point);
                
            }
            else
            {
                
                var tcout=document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value;
                if(parseFloat(tcout).toFixed(point)==NaN)
                {
                    document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value=parseFloat("1").toFixed(point);
                    
                }
                var bilv=parseFloat(tcout)/parseFloat(tquota);
                for (var i = count - 1; i > 0; i--) 
                {
                   
                        var squota=parseFloat(document.getElementById("Quota_SignItem_TD_Text_"+i).value);
                        document.getElementById("ProCount_SignItem_TD_Text_"+i).value=parseFloat(bilv*squota).toFixed(point);
                         var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+i).value);
                         if(jiage>0)
                         {
                             document.getElementById("TotalPrice_SignItem_TD_Text_"+i).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+i).value)).toFixed(point);
                         }
                    
                }
            }
        }
        else
        {
             var bilv=parseFloat(document.getElementById("ProCount_SignItem_TD_Text_1").value)/parseFloat(document.getElementById("Quota_SignItem_TD_Text_1").value);
              document.getElementById(quotaid).value=parseFloat(document.getElementById(quotaid).value).toFixed(point);
              if(document.getElementById(quotaid).value=="NaN")
            {
                document.getElementById(quotaid).value=parseFloat("1").toFixed(point);
                
            }
              document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value=parseFloat(bilv*parseFloat(document.getElementById(quotaid).value)).toFixed(point);
              var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value);
             if(jiage>0)
             {
                 document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
             }
        }
   }else if(countid!="")
   {
         if(rowid=="1")
        {   
            var tcount=document.getElementById(countid).value;
            document.getElementById(countid).value=parseFloat(tcount).toFixed(point);
            if(document.getElementById(countid).value=="NaN")
            {
                document.getElementById(countid).value=parseFloat("1").toFixed(point);
                var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value);
                 if(jiage>0)
                 {
                     document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
                 }
                return;
            }
            else
            {
                var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value);
                 if(jiage>0)
                 {
                     document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
                 }
                var tquota=document.getElementById("Quota_SignItem_TD_Text_"+rowid).value;
                if(parseFloat(tcout).toFixed(point)==NaN)
                {
                    document.getElementById("Quota_SignItem_TD_Text_"+rowid).value=parseFloat("1").toFixed(point);
                    
                }
                var bilv=parseFloat(tcount)/parseFloat(tquota);
                for (var i = count - 1; i > 0; i--) 
                {
                    
                        var squota=parseFloat(document.getElementById("Quota_SignItem_TD_Text_"+i).value);
                        document.getElementById("ProCount_SignItem_TD_Text_"+i).value=parseFloat(squota*bilv).toFixed(point);
                         var jiage1=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+i).value);
                         if(jiage1>0)
                         {
                             document.getElementById("TotalPrice_SignItem_TD_Text_"+i).value=parseFloat(jiage1*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+i).value)).toFixed(point);
                         }
                    
                }
            }
        }
        else
        {
             var bilv=parseFloat(document.getElementById("ProCount_SignItem_TD_Text_1").value)/parseFloat(document.getElementById("Quota_SignItem_TD_Text_1").value);
              document.getElementById(countid).value=parseFloat(document.getElementById(countid).value).toFixed(point);
              if(document.getElementById(countid).value=="NaN")
                {
                    document.getElementById(countid).value=parseFloat("1").toFixed(point);
                    var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value);
                     if(jiage>0)
                     {
                         document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
                     }
                    return;
                }
                
              document.getElementById("Quota_SignItem_TD_Text_"+rowid).value=parseFloat(parseFloat(document.getElementById(countid).value)/bilv).toFixed(point);
                var jiage=parseFloat(document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value);
                 if(jiage>0)
                 {
                     document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(jiage*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
                 }
        }
   }
   else if(priceid!="")
   {
        var price=parseFloat(document.getElementById(priceid).value);
        document.getElementById(priceid).value=parseFloat(price).toFixed(point);
        if(price==NaN||price=="NaN")
        {
            document.getElementById(priceid).value=parseFloat(0).toFixed(point);
            return;
        }
        document.getElementById("TotalPrice_SignItem_TD_Text_"+rowid).value=parseFloat(price*parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
   }else if(totalpriceid!="")
   {
         var total=parseFloat(document.getElementById(totalpriceid).value);
        if(total==NaN||total=="NaN")
        {
            document.getElementById(totalpriceid).value=parseFloat(0).toFixed(point);
            return;
        }
        document.getElementById("UnitPrice_SignItem_TD_Text_"+rowid).value=parseFloat(total/parseFloat(document.getElementById("ProCount_SignItem_TD_Text_"+rowid).value)).toFixed(point);
   }
}
//拆卸费
function totalprice(id)
{
    var point=parseInt(document.getElementById("hidSelPoint").value);
    if(id.value=="")
    {
        id.value=parseFloat(0).toFixed(point);
    }
}

//全选
function fnSelectAll() {
    $.each($("#dg_Log :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
    });
}

/*
* 返回按钮
*/
function DoBack() {
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    moduleID = document.getElementById("thisModuleID").value;
    if(moduleID=="2051503")
        moduleID="2051504"
    else
        moduleID="2051506"
    window.location.href = "DisassemblingList.aspx?ModuleID=" + moduleID + searchCondition;
}

//打印
function fnPrintAssembl()
{
    var assemNo =document.getElementById("div_InNo_Lable").innerText;
    var modid=$.trim($("#thisModuleID").val());
    if (assemNo == '保存时自动生成' || assemNo == '') {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请保存单据再打印！");
        return;
    }
    window.open('../../../Pages/PrinttingModel/StorageManager/PrintAssembling.aspx?no=' + assemNo+'&ModuleID='+modid);
}
