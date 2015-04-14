var isnew = "1"; //1添加;2保存
var istmpquantity = "0"; //0没有数量为0的订单  1有数量为0的订单
$(document).ready(function() {

    requestobj = GetRequest();
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        document.getElementById("hiddOrderID").value = document.getElementById("headid").value;
        GetInfoById(document.getElementById("headid").value);
        fnGetDetail(document.getElementById("headid").value);
        $("#labTitle_Write1").html("销售合同单据查看");
    }
    else {
        $("#divCodeRule").css("display", "block");
        $("#txtContractID").css("display", "none");
        $("#labTitle_Write1").html("销售合同单据新建");
    }

});




function GetInfoById(headid) {  

function GetInfoById(headid) {  

    var action = "SearchSellContract";
    var orderBy = "id";
    $("#divCodeRule").css("display", "none");
    $("#txtContractID").css("display", "block");

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {
                    document.getElementById("txtContractID").value = item.Contractid;
                    document.getElementById("txtCustomerID").value = item.cCusCode;
                    document.getElementById("txtCustomerName").value = item.custname;
                    document.getElementById("txtDeliveryAddress").value = item.DeliveryAddress;
                    document.getElementById("txtSignDate").value = item.signdate;
                    document.getElementById("txtEffectiveDate").value = item.effectivedate;
                    document.getElementById("txtEndDate").value = item.enddate;
                    document.getElementById("drpSettleType").value = item.SettleType;
                    document.getElementById("drpTransPortType").value = item.TransPortType;
                    document.getElementById("txtContractMoney").value = item.ContractMoney;
                    document.getElementById("txtRemark").value = item.remark;
                    document.getElementById("txtPPersonID").value = item.PPersonID;
                    document.getElementById("txtPPerson").value = item.PPerson;
                    document.getElementById("hdDeptID").value = item.DeptID;
                    document.getElementById("DeptName").value = item.DeptName;
                    //附加信息
                    document.getElementById("txt_CreateDate").value=item.createdate;
                    document.getElementById("UserPrincipal").value=item.createname;
                    document.getElementById("txtConfirmord").value=item.Confirmor;
                    document.getElementById("txtConfirmDate").value=item.ConfirmDate;
                    document.getElementById("txtModifiedDates").value=item.ModifiedDate;
                    document.getElementById("txtModifiedUserIDs").value=item.ModifiedUserID;


                    $("#txtOprtID").val('saveOk');

                    //确认状态
                    document.getElementById("txtBillStatusID").value = item.billstatusid;
                    document.getElementById("hidBillStatus").value = item.billstatusid;
                    if (document.getElementById("hidBillStatus").value == "1") {
                        document.getElementById("txtBillStatusName").value = "制单";

                        fnStatus('2');
                    }
                    if (document.getElementById("hidBillStatus").value == "2") {
                        document.getElementById("txtBillStatusName").value = "确认";
                        fnStatus('3');
                    }

                    if (document.getElementById("hidBillStatus").value == "9") {
                        document.getElementById("txtBillStatusName").value = "关闭";
                        fnStatus('9');
                    }
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "1";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';

                    //            
                  //  GetFlowButton_DisplayControl();


                }
            });

            if (typeof (msg.dataAttach) != 'undefined') {
                document.getElementById("spanAttachmentName").innerHTML = "";
                $.each(msg.dataAttach, function(i, item) {
                    addi = i;
                    document.getElementById("hfPageAttachment").value = item.AnnAddr;
                    //                       document.getElementById("divDealAttachment").style.display = "block";
                    //                       document.getElementById("spanAttachmentName").innerHTML += "<div><table width='100%' border='0' align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'><tr><td height='22'  class='td_list_fields' align='right'>附件名称</td><td height='22' bgcolor='#FFFFFF'><A name='aFileName' onclick=\"DealAttachment('download'," + i + ");\" style=\"color:#000000;cursor: pointer;width: 150px;\"> " + item.AnnFileName + "</A></td><td height='22'  class='td_list_fields' align='right'>说明</td><td height='22' bgcolor='#FFFFFF'><input type='text' id='txtAnnRemark' name='txtAnnRemark' class='tdinput' value='" + item.AnnRemark + "' style='width:150px' /></td><td height='22'  class='td_list_fields' align='right'>上传时间</td><td height='22' bgcolor='#FFFFFF'><input type='text' id='txtUpDateTime' class='tdinput' name='txtUpDateTime' value = '" + item.UpDateTime + "' style='width:150px' /></td><td height='22' bgcolor='#FFFFFF'><A onclick='javascript:DelFileInput(this)' style=\"cursor: pointer;\">删除附件</A><input type='hidden' id='txtaddr' name='txtaddr" + i + "' value='" + item.AnnAddr + "'  /></td></tr></table> <div>";

                    $("#Tb_05").append("<tr class='table-item'><td height='22' align='right'class='td_list_fields' style='width:10%;' >附件名称</td><td height='22' bgcolor='#FFFFFF' style='width:20%;'><a href='#' name='aFileName' onclick=\"DealAttachment('download'," + addi + ");\" style=\"cursor: pointer;width:98%;\"> " + item.AnnFileName + "</a></td><td height='22'  class='td_list_fields' align='right' style='width:10%;'>附件说明</td><td height='22' bgcolor='#FFFFFF' style='width:20%;'><input type='text' id='txtAnnRemark" + addi + "' class='tdinput' name='txtAnnRemark' value='" + item.AnnRemark + "' style='width:98%' /></td><td height='22'  class='td_list_fields' align='right'style='width:10%;'>上传时间</td><td height='22' bgcolor='#FFFFFF'style='width:20%;'><input type='text' id='txtUpDateTime" + addi + "' name='txtUpDateTime' class='tdinput' value = '" + item.UpDateTime + "' style='width:98%' /></td><td height='22' bgcolor='#FFFFFF'><a href='#' onclick='javascript:DelFileInput(this)' style=\"cursor: pointer;\">删除附件</a><input type='hidden' id='txtaddr" + addi + "' name='txtaddr' value='" + item.AnnAddr + "'  /></td></tr>");
                    document.getElementById("hfAttachment").value += item.AnnFileName + ",";
                    //                       document.getElementById("divUploadAttachment").style.display = "none";
                });
            }
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}


//关闭
function CloseConfim() {
    var c = window.confirm("确定执行关闭操作吗？")
    if (c == true) {

        //  var ActionArrive = document.getElementById("txtAction").value.Trim()

        //  if(ActionArrive == "1")
        //   {
        //       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
        //       return;
        //   }

        glb_BillID = document.getElementById('headid').value;
        //document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        //document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        //document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action = "ClosePur";

        var confirmor = "";
        var contractNo = document.getElementById("txtContractID").value;
        // var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&contractNo=" + contractNo + "&headid=" + glb_BillID + '&billtype=2' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/ContractManage/DealContract.ashx?" + strParams,

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
                    // document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                    fnStatus('9');

                    // fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                    //fnFlowStatus($("#FlowStatus").val());
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "2";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';

                    //            
                    //                    GetFlowButton_DisplayControl();
                }
                else {

                    popMsgObj.ShowMsg(data.data);
                }
            }
        });
    }
}
//取消关闭
function UncloseConfim() {


    var c = window.confirm("确定执行取消关闭操作吗？")
    if (c == true) {

        //  var ActionArrive = document.getElementById("txtAction").value.Trim()

        //  if(ActionArrive == "1")
        //   {
        //       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
        //       return;
        //   }

        glb_BillID = document.getElementById('headid').value;
        //document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        //document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        //document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action = "UnClosePur";

        var confirmor = "";
        var contractNo = document.getElementById("txtContractID").value;
        // var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&contractNo=" + contractNo + "&headid=" + glb_BillID + '&billtype=2' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/ContractManage/DealContract.ashx?" + strParams,

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
                    document.getElementById("txtBillStatusName").value = " 取消关闭";
                    popMsgObj.ShowMsg('取消关闭成功');
                    // document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                    fnStatus('3');

                    // fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                    //fnFlowStatus($("#FlowStatus").val());
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "2";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';

                    //            
                    //                    GetFlowButton_DisplayControl();
                }
                else {

                    popMsgObj.ShowMsg(data.data);
                }
            }
        });

    }
}



//获取明细信息
function fnGetDetail(headid) {
    var action = "SearchSellContract";
    var orderBy = "id";
    var ary = new Array();
    var rowsCount = 0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() {

        },
        error: function() {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                $.each(data.data, function(i, item) {
                    rowsCount++;
                    FillSignRow(item.cinvccode, item.productname, item.unitid, item.unitname, item.iquantity, item.iunitcost, item.imoney, item.detailsid);


                    //                    var txtTRLastIndex = findObj("txtTRLastIndex", document);
                    //                    var rowID = parseInt(txtTRLastIndex.value);                   
                    //                    txtTRLastIndex.value = rowID; //将行号推进下一行


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
        url: '../../../Handler/JTHY/BusinessManage/ProductInfos.ashx', //目标地址
        cache: false,
        data: 'str=' + str + '&action=getprodcutinfoByIds', //数据
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表

            $.each(msg.data, function(i, item) {
                if (!IsExistCheck(item.id)) {
                    AddFillDetailRows(item.id, item.ProductName, item.UnitID, item.UnitName, item.StandardBuy);
                }

            });
        },
        complete: function() { } //接收数据完毕
    });
    document.getElementById('HolidaySpans').style.display = "none";
    closeRotoscopingDiv(false, "divProductNameS"); //关闭遮罩层
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

//-----------------上传控件------------------------------//

function DealAttachment(flag, i) {
    //flag未设置时，返回不处理
    if (flag == "undefined" || flag == "") {
        return;
    }
    //上传附件
    else if ("upload" == flag) {
        ShowUploadFile();
    }
    //清除附件
    else if ("clear" == flag) {
        //设置附件路径
        document.getElementById("hfPageAttachment").value = "";
        //下载删除不显示
        document.getElementById("divDealAttachment").style.display = "none";
        //上传显示 
        document.getElementById("divUploadAttachment").style.display = "block";
    }
    //下载附件
    else if ("download" == flag) {
        //获取附件路径
        attachUrl = document.getElementById("txtaddr" + i + "").value;
        //下载文件
        window.open("../../Common/DownloadFile.aspx?RelativeFilePath=" + attachUrl, "_blank");
    }
}
/*
* 上传文件后回调处理
*/
function AfterUploadFile(url, docName) {
    if (url != "") {

        var addi;
        addi++;
        //设置附件路径
        document.getElementById("hfPageAttachment").value = url;
        //下载删除显示       
        //        document.getElementById("divDealAttachment").style.display = "block";
        var upDateTime = document.getElementById("hidUpDateTime").value;
        //        document.getElementById("spanAttachmentName").innerHTML += "<div><table width='100%' border='0' align='center' cellpadding='2' cellspacing='1' bgcolor='#999999'><tr><td height='22'  class='td_list_fields' align='right'>附件名称</td><td height='22' bgcolor='#FFFFFF'><A name='aFileName' onclick=\"DealAttachment('download'," + addi + ");\" style=\"color:#000000;cursor: pointer;width: 150px;\"> " + docName + "</A></td><td height='22'  class='td_list_fields' align='right'>说明</td><td height='22' bgcolor='#FFFFFF'><input type='text' id='txtAnnRemark' class='tdinput' name='txtAnnRemark' style='width:150px' /></td><td height='22'  class='td_list_fields' align='right'>上传时间</td><td height='22' bgcolor='#FFFFFF'><input type='text' id='txtUpDateTime' name='txtUpDateTime' class='tdinput' value = '" + upDateTime + "' style='width:150px' /></td><td height='22' bgcolor='#FFFFFF'><A onclick='javascript:DelFileInput(this)' style=\"cursor: pointer;\">删除附件</A><input type='hidden' id='txtaddr' name='txtaddr" + addi + "' value='" + url + "'  /></td></tr></table><div>";
        $("#Tb_05").append("<tr><td height='22'  class='td_list_fields' align='right' style='width:10%;'>附件名称</td><td height='22' bgcolor='#FFFFFF' style='width:20%;'><a href='#' name='aFileName' onclick=\"DealAttachment('download'," + addi + ");\" style=\"cursor: pointer;width: 98%;\"> " + docName + "</a></td><td height='22'  class='td_list_fields' align='right' style='width:10%;'>附件说明</td><td height='22' bgcolor='#FFFFFF'style='width:20%;'><input type='text' id='txtAnnRemark" + addi + "' class='tdinput' name='txtAnnRemark' style='width:98%' /></td><td height='22'  class='td_list_fields' align='right' style='width:10%;'>上传时间</td><td height='22' bgcolor='#FFFFFF' style='width:20%;'><input type='text' id='txtUpDateTime" + addi + "' name='txtUpDateTime' class='tdinput' value = '" + upDateTime + "' style='width:98%' /></td><td height='22' bgcolor='#FFFFFF'><a href='#' onclick='javascript:DelFileInput(this)' style=\"cursor: pointer;\">删除附件</a><input type='hidden' id='txtaddr" + addi + "' name='txtaddr' value='" + url + "'  /></td></tr>");

        document.getElementById("hfAttachment").value += docName + ",";
        //上传不显示
        //        document.getElementById("divUploadAttachment").style.display = "none";

    }

}
//删除附件
function DelFileInput(oInputButton) {
    var divToDel = oInputButton.parentNode.parentNode;
    var divfilename = oInputButton.parentNode;
    var filename = divfilename.childNodes[1].value;
    var realname = divToDel.childNodes[1].innerText;
    $.ajax({
        type: 'POST',
        url: '../../../Handler/Office/CustManager/UploadfileDel.ashx',
        dataType: 'json',
        cache: false,
        data: 'filename=' + escape(filename) + '&realname=' + escape(realname),
        beforeSend: function() { },
        success: function(data) {
            if (data.sta == 1) {
                //成功
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除附件成功！");
                return;
            }
            else {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除附件失败！");
            }
        },
        complete: function() { },
        error: function(e) { }
    });
    divToDel.parentNode.removeChild(divToDel);
}

//-----------------------------------------------//







function getMoney(rowid) {
    var imoney = document.getElementById("txtQuantity" + rowid).value * document.getElementById("txtUnitCost" + rowid).value;
    document.getElementById("txtMoney" + rowid).value = imoney;
    var summoney = 0;
    for (var i = 0; i < 100; i++) {
        if (document.getElementById("txtMoney" + i) != "undefined" && document.getElementById("txtMoney" + i) != null) {
            summoney = summoney + Number(document.getElementById("txtMoney" + i).value);
        }
    }
    document.getElementById("txtContractMoney").value = summoney;

}




//保存
function SaveSellOrder() {
    
    var isconfirm = "";
    var strAction = "";

    isconfirm = "0";

    $("#hidBillStatus").val('1');
    //--------------20121118-------------//
    $("#Imgbtn_confirm").css("display", "inline");
    $("#btn_confirm").css("display", "none");
    //----------------------------------------//

    /////////////////
    if ($("#txtContractID").val == "" || ($("#ddlContractID_ddlCodeRule").val() == "" && $("#ddlContractID_txtCode").val() == "")) {
        popMsgObj.ShowMsg("合同号不能为空，请填写！");
        return;
    }
    if (document.getElementById("txtCustomerID").value == "") {
        popMsgObj.ShowMsg("客户不能为空，请填写！");
        return;
    }  

    //验证明细信息
    var signFrame = findObj("dg_Log", document);
    var iCount = 0; //明细中数据数目
    for (i = 1; i < signFrame.rows.length; i++) {
        if (signFrame.rows[i].style.display != "none") {
            iCount = iCount + 1;
            var rowid = signFrame.rows[i].id;
            var drpCoalType = $("#drpCoalType" + rowid).val(); //车号
            var txtQuantity = $("#txtQuantity" + rowid).val();
            var txtUnitCost = $("#txtUnitCost" + rowid).val();

            if (txtQuantity == "" ) {
                popMsgObj.ShowMsg("数量不为空！");
                return;
            }
           
            
            
        }
    }
    
    
    
    if (iCount == 0) {
        popMsgObj.ShowMsg("订单明细不能为空！|");
        return;
    }
    var strfitinfo = getDropValue().join("|");
    var strInfo = fnGetInfo();
    var headid = document.getElementById("headid").value;
    if (headid == "undefined" || headid == "null" || headid == "") {
        headid = "0";
    }

    $.ajax({
        type: "POST",
        url: "../../../Handler/JTHY/ContractManage/DealContract.ashx",
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

            $("#hiddOrderID").val(data.sta);
            $("#headid").val(data.sta);
            $("#txtContractID").val(data.data);
            $("#ddlContractID_txtCode").val(data.data);
            $("#divCodeRule").css("display", "none");
            $("#txtContractID").css("display", "block");
            fnStatus('2');

            if (isconfirm == "0") {
                $("#hidBillStatus").val('1');
            }
            else if (isconfirm == "1") {
                $("#billstatus").val("确认");
                $("#hidBillStatus").val('2');

                $("#imgUnSave").css("display", "inline");
                $("#btn_save").css("display", "none");
                $("#ImgUnConfirm").css("display", "none");
                $("#UnConfirm").css("display", "inline");



            }
                $("#hidStatus").val('0');
                $("#hidSendStatus").val('0');

                var glb_BillTypeFlag = "30";
                var glb_BillTypeCode = "1";
                var glb_BillID = 0;                                //单据ID
                var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                var FlowJS_BillStatus = 'hidBillStatus';

                //            
               // GetFlowButton_DisplayControl();
                //  fnGetOrderInfo(data.sta);
            }
            hidePopup();
            if (isconfirm == "0") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.info);
            }
            else if (isconfirm == "1") {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", data.info);
            }
        }
    });

}





function Fun_ConfirmOperate() {
    var c = window.confirm("确定执行确认操作吗？")
    if (c == true) {

        //  var ActionArrive = document.getElementById("txtAction").value.Trim()

        //  if(ActionArrive == "1")
        //   {
        //       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
        //       return;
        //   }

        glb_BillID = document.getElementById('headid').value;
        //document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        //document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        //document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action = "ConfirmSell";

        var confirmor = "";
        var contractNo = document.getElementById("txtContractID").value;
        // var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&contractNo=" + contractNo + "&headid=" + glb_BillID + '&billtype=1' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/ContractManage/DealContract.ashx?" + strParams,

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
                    // document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                   // fnStatus($("#txtBillStatusID").val());
                    fnStatus('3');
                    // fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                    //fnFlowStatus($("#FlowStatus").val());
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "1";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';

                    //            
                  //  GetFlowButton_DisplayControl();
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

        //  var ActionArrive = document.getElementById("txtAction").value.Trim()

        //  if(ActionArrive == "1")
        //   {
        //       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先保存再确认！");
        //       return;
        //   }

        glb_BillID = document.getElementById('headid').value;
        //document.getElementById("txtConfirmorReal").value = document.getElementById("UserName").value.Trim();
        //document.getElementById("txtConfirmor").value = document.getElementById("UserID").value.Trim();
        //document.getElementById("txtConfirmDate").value = document.getElementById("SystemTime").value.Trim();
        action = "CancelConfirmSell";

        var confirmor = "";
        var contractNo = document.getElementById("txtContractID").value;
        // var fromType = document.getElementById("ddlFromType").value.Trim();

        var strParams = "action=" + action + "&confirmor=" + confirmor + "&contractNo=" + contractNo + "&headid=" + glb_BillID + '&billtype=1' + '';
        $.ajax({
            type: "POST",
            url: "../../../Handler/JTHY/ContractManage/DealContract.ashx?" + strParams,

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
                    // document.getElementById("txtModifiedUserID").value = document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedUserIDReal").value =  document.getElementById("txtModifiedUserID2").value.Trim();
                    //  document.getElementById("txtModifiedDate").value = document.getElementById("txtModifiedDate2").value.Trim();
                    fnStatus($("#txtBillStatusID").val());

                    // fnStatus($("#ddlBillStatus").val(),$("#Isyinyong").val());
                    //fnFlowStatus($("#FlowStatus").val());
                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "2";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';

                    //            
                   // GetFlowButton_DisplayControl();
                }
                else {

                    popMsgObj.ShowMsg(data.info);
                }
            }
        });

    }
}


//获取主表信息
function fnGetInfo() {
    var strInfo = '';

    var AnnRemark = "", UpDateTime = "", AnnFileName = "", AnnAddr = "";
    var txtAnnRemark = document.getElementsByName("txtAnnRemark");
    var txtUpDateTime = document.getElementsByName("txtUpDateTime");
    var aFileName = document.getElementsByName("aFileName");
    var txtaddr = document.getElementsByName("txtaddr");
    if (txtAnnRemark != "" || txtUpDateTime != "") {
        for (var i = 0; i < txtUpDateTime.length; i++) {
            AnnRemark += txtAnnRemark[i].value + ",";
            UpDateTime += txtUpDateTime[i].value + ",";
            AnnFileName += aFileName[i].innerHTML + ",";
            AnnAddr += txtaddr[i].value + ",";
        }
    }

    ///////////////////
    var ContractIDType = document.getElementById("ddlContractID_ddlCodeRule").value;
    var ContractID = "";
    //获取编码规则下拉列表选中项  如果选中的是 手工输入时，校验编号是否输入
    if (ContractIDType == "") {
        ContractID = $("#ddlContractID_txtCode").val(); //客户编号
    }

    if ($("#txtContractID").val() != "") {
        ContractID = $("#txtContractID").val();
    }
    //var ContractID=$("#txtContractID").val();//合同号
    var CustomerID = $("#txtCustomerID").val(); //客户
    var DeliveryAddress = $("#txtDeliveryAddress").val(); //发货地址 
    var SignDate = $("#txtSignDate").val(); //签订日期
    var EffectiveDate = $("#txtEffectiveDate").val(); //生效日期
    var EndDate = $("#txtEndDate").val(); //终止日期
    var SettleType = $("#drpSettleType").val(); //结算方式
    var TransPortType = $("#drpTransPortType").val(); //运输类型
    var ContractMoney = $("#txtContractMoney").val(); //合同金额
    var Remark = $("#txtRemark").val(); //备注

    var PPersonID = $("#txtPPersonID").val(); //业务员编码
    var DeptID = $("#hdDeptID").val(); //部门编码

    strInfo = 'ContractID=' + escape(ContractID) + '&ContractIDType=' + ContractIDType + '&CustomerID=' + escape(CustomerID) + '&DeliveryAddress=' + escape(DeliveryAddress)
    + '&SignDate=' + escape(SignDate) + '&EffectiveDate=' + escape(EffectiveDate) + '&EndDate=' + escape(EndDate) + '&SettleType=' + escape(SettleType) +
    '&TransPortType=' + escape(TransPortType) + '&ContractMoney=' + escape(ContractMoney) + '&Remark=' + escape(Remark) + '&PPersonID=' + escape(PPersonID) + '&DeptID=' + escape(DeptID) +
    '&AnnFileName=' + escape(AnnFileName) + '&AnnRemark=' + escape(AnnRemark) + '&UpDateTime=' + UpDateTime + '&AnnAddr=' + escape(AnnAddr.replace(/\\/g, "\\\\")) + '';

    return strInfo;
}






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




function FillSignRow(cinvccode, productname, unitid, unitname, iquantity, iunitcost, imoney, detailsid) { //读取最后一行的行号，存放在txtTRLastIndex文本框中

    var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='" + detailsid + "' >";
    m++;

    //      var colProductNo = newTR.insertCell(m); //添加列:物品编号
    //       colProductNo.className = "cell";
    //     colProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
    //                                   "<tr><td><input id=\"ProductNo" + rowID + "\" class=\"tdinput\" title=\"\" readonly style=' width:95%;COLOR: #848284 '><input type=\"hidden\"  id='hiddProductNo" + rowID + "'/></td>" +
    //                              "</tr></table>";
    //      m++;

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    //    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    //    "<select name='drpCoalType" + rowID + "' class='tddropdlist'  runat='server' id='drpCoalType" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input  id='drpCoalType" + rowID + "' value=\"" + productname + "\"  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /><input type='hidden' id='productid" + rowID + "' value='" + cinvccode + "' ></td></tr></table>"; //添加列内容
    m++;


    //getCoalData('drpCoalType' + rowID, cinvccode); //加载煤种数据

    //    var newFitDesctd = newTR.insertCell(m); //添加列:规格(质量热卡)
    //    newFitDesctd.className = "cell";
    //    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input  id='txtSpecialName" + rowID + "' value=\"" + specification + "\"  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    //    m++;
    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input type='hidden' id='txtUnitID" + rowID + "'  value=\"" + unitid + "\" ><input  id='txtUnitName" + rowID + "' value=\"" + unitname + "\"  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;


    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtQuantity" + rowID + "' value=\"" + iquantity + "\"  maxlength='10' type='text' class='tdinput' style=' width:90%;'  onkeyup='return ValidateNumber(this,value)'   onpropertychange=\"getMoney(" + rowID + ");\"/></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtUnitCost" + rowID + "' value=\"" + Number(iunitcost).toFixed(2) + "\"  onkeyup='return ValidateNumber(this,value)'     maxlength='10' type='text'  class='tdinput'   onpropertychange=\"getMoney(" + rowID + ");\"  style=' width:90%;' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtMoney" + rowID + "' value=\"" + imoney + "\"   disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/></td></tr></table>"; //添加列内容
    m++;

    txtTRLastIndex.value = rowID; //将行号推进下一行  


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

            //$("#txtSpecialName" + rowid).val(''); //质量热卡
            $("#txtUnitID" + rowid).val(''); //计量单位
            $("#txtUnitName" + rowid).val(''); //计量单位
            $.each(msg.data, function(i, item) {
                // $("#txtSpecialName" + rowid).val(item.HeatPower); //质量热卡
                $("#txtUnitID" + rowid).val(item.unitid); //计量单位
                $("#txtUnitName" + rowid).val(item.unitname); //计量单位 

            });

        },
        complete: function() { } //接收数据完毕
    });

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
            var CoalType = $("#productid" + rowid).val();   //物品编码 
            var SpecialName = $("#txtSpecialName" + rowid).val(); //质量  
            var UnitID = $("#txtUnitID" + rowid).val();   //计量单位编码
            var Quantity = $("#txtQuantity" + rowid).val();  //数量   
            var UnitCost = $("#txtUnitCost" + rowid).val();        //单价
            var Money = $("#txtMoney" + rowid).val();      //金额
            var detailsid = $("#detailsid" + rowid).val();      //表体autoid
            SendOrderFit_Item[j] = [[CoalType], [SpecialName], [UnitID], [Quantity], [UnitCost], [Money], [detailsid]];
        }
    }
    return SendOrderFit_Item;
}





function AddFillDetailRows(id, ProductName, UnitID, UnitName, StandardBuy) {

    var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    var rowID = parseInt(txtTRLastIndex.value) + 1;

    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;

    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='" + id + "' >";
    m++;

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    //    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    //    "<select name='drpCoalType" + rowID + "' class='tddropdlist'   runat='server' id='drpCoalType" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input  id='drpCoalType" + rowID + "' value=\"" + ProductName + "\"  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /><input type='hidden' id='productid" + rowID + "' value='" + id + "' ></td></tr></table>"; //添加列内容
    m++;


    //getCoalData('drpCoalType' + rowID, id); //加载煤种数据F

    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input type='hidden' id='txtUnitID" + rowID + "'  value=\"" + UnitID + "\" ><input  id='txtUnitName" + rowID + "' value=\"" + UnitName + "\"  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtQuantity" + rowID + "'   maxlength='10' type='text' class='tdinput' style=' width:90%;'  onkeyup=\"ValidateNumber(this, this.value)\" onpropertychange=\"getMoney(" + rowID + ");\"/></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtUnitCost" + rowID + "' onkeyup=\"ValidateNumber(this, this.value)\"  value=\"" + Number(StandardBuy).toFixed(2) + "\"  maxlength='10' type='text'  class='tdinput'   onpropertychange=\"getMoney(" + rowID + ");\"  style=' width:90%;' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><input id='txtMoney" + rowID + "'    disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/></td></tr></table>"; //添加列内容
    m++;


    $("#txtTRLastIndex").val(rowID);

}





//添加新行
function AddSignRow() { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var txtTRLastIndex = document.getElementById("txtTRLastIndex");

    var rowID = parseInt(txtTRLastIndex.value) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m = 0;
    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid" + rowID + "' value='' >";
    m++;

    //      var colProductNo = newTR.insertCell(m); //添加列:物品编号
    //    colProductNo.className = "cell";
    //        colProductNo.innerHTML = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" +
    //                                 "<tr><td><input id=\"ProductNo" + rowID + "\" class=\"tdinput\" title=\"\" readonly style=' width:95%;COLOR: #848284 '><input type=\"hidden\"  id='hiddProductNo" + rowID + "'/></td>" +
    //                                "</tr></table>";
    //       m++;

    //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>" +
    "<select name='drpCoalType" + rowID + "' class='tddropdlist'  runat='server' id='drpCoalType" + rowID + "' onChange='getCoalNature(this.value," + rowID + ")' ></select></td></tr></table>"; //添加列内容
    m++;

    getCoalData('drpCoalType' + rowID, 0); //加载煤种数据

    //    var newFitDesctd = newTR.insertCell(m); //添加列:规格(质量热卡)
    //    newFitDesctd.className = "cell";
    //    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input  id='txtSpecialName" + rowID + "'  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    //    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input type='hidden' id='txtUnitID" + rowID + "' ><input  id='txtUnitName" + rowID + "'  style='width:95%;' disabled='disabled' type='text'  class='tdinput' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input id='txtQuantity" + rowID + "' maxlength='10' type='text' class='tdinput' style='width:90%;' oninput=\"getMoney(" + rowID + ");\"  onkeyup='return ValidateNumber(this,value)'  onpropertychange=\"getMoney(" + rowID + ");\"/> <input type=\"hidden\" value='0.00' id='hiddUnitPrice" + rowID + "'/><input type=\"hidden\" value='0.00' id='hiddBaseUnitPrice" + rowID + "'/></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input id='txtUnitCost" + rowID + "' value=0.00 onblur=\"fnTotalInfo();\"  onkeyup='return ValidateNumber(this,value)'   maxlength='10' type='text'  class='tdinput'  oninput=\"getMoney(" + rowID + ");\" onpropertychange=\"getMoney(" + rowID + ");\"  style=' width:90%;' /></td></tr></table>"; //添加列内容
    m++;

    var newFitDesctd = newTR.insertCell(m); //添加列: 金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='98%'><tr><td><input id='txtMoney" + rowID + "'  disabled='disabled'  type='text' class='tdinput' style=' width:90%;'/></td></tr></table>"; //添加列内容
    m++;

    txtTRLastIndex.value = rowID; //将行号推进下一行  


}

