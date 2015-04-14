
//document.onkeydown = ScanBarCodeSearch;
///*列表条码扫描检索*/
//function ScanBarCodeSearch() {
//    var evt = event ? event : (window.event ? window.event : null);
//    var el; var theEvent
//    var browser = IsBrowser();
//    if (browser == "IE") {
//        el = window.event.srcElement;
//        theEvent = window.event;
//    }
//    else {
//        el = evt.target;
//        theEvent = evt;
//    }
//    if (el.id != "txtBarCode") {
//        return;
//    }
//    else {
//        var code = theEvent.keyCode || theEvent.which;
//        if (code == 13) {
//            TurnToPage(1);
//            evt.returnValue = false;
//            evt.cancel = true;
//        }
//    }
//}

//function IsBrowser() {
//    var isBrowser;
//    if (window.ActiveXObject) {
//        isBrowser = "IE";
//    } else if (window.XMLHttpRequest) {
//        isBrowser = "FireFox";
//    }
//    return isBrowser;
//}




//物品控件
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('txtProductNo').value = ProNo;
    //document.getElementById('txtProductName').value=ProdName;
    document.getElementById('hiddenProductID').value = id;

}

function dosearch() {
    var ddlStorage = document.getElementById("ddlStorage").value;

    var ProductID = document.getElementById("hiddenProductID").value;
    var StartDate = document.getElementById("txtStartDate").value;
    var EndDate = document.getElementById("txtEndDate").value;
    var BatchNo = document.getElementById("ddlBatchNo").value;
    var SourceType = document.getElementById("ddlSourceType").value;

    var SourceNo = document.getElementById("txtSourceNo").value;

    var CreatorID = document.getElementById("txtCreatorID").value;

    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value;
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value;

    var Specification = document.getElementById("txt_Specification").value;
    var ColorID = document.getElementById("sel_ColorID").value;
    var Material = document.getElementById("sel_Material").value;
    var Manufacturer = document.getElementById("txt_Manufacturer").value;
    var Size = document.getElementById("txt_Size").value;
    var FromAddr = document.getElementById("txt_FromAddr").value;
    var BarCode = document.getElementById("txt_BarCode").value;

    var ckbIsM = 0;
    if (document.getElementById("ckbIsM").checked) {
        ckbIsM = 1;
    }

    var hidurl = "StorageID=" + ddlStorage + "&ProductID=" + ProductID + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&BatchNo=" + BatchNo + "&SourceType=" + SourceType + "&SourceNo=" + SourceNo +
                 "&CreatorID=" + CreatorID + "&EFIndex=" + EFIndex + "&EFDesc=" + EFDesc + "&Specification=" + Specification + "&ColorID=" + ColorID + "&Material=" + Material +
                  "&Manufacturer=" + Manufacturer + "&Size=" + Size + "&FromAddr=" + FromAddr + "&BarCode=" + BarCode + "&ckbIsM=" + ckbIsM;
    window.open("../../PrinttingModel/StorageManager/CrystalStorageJournalPrt.aspx?" + hidurl, 'storagerpt');
}
