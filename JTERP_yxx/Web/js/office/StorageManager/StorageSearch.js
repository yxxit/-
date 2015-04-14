$(document).ready(function() {
    IsDiplayOther('GetBillExAttrControl1_SelExtValue'); //物品扩展属性
    fnGetExtAttr(); //物品控件拓展属性
});

var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var alertPrint ="";
var currentPageIndex = 1;
var action = "Get"; //操作
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;

    var Num = 0; //数量
    var basicNum = 0; //基本数量

    var RetVal = CheckSpecialWords();
    if (RetVal != "") {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }

    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
        return false;
    }

    currentPageIndex = pageIndex;
    var ddlStorage = document.getElementById("ddlStorage").value;
    var txtProductNo = document.getElementById("txtProductNo").value;
    var txtProductName = document.getElementById("txtProductName").value;
    if (document.getElementById("txtBarCode").value != "") {
        document.getElementById("HiddenBarCode").value = document.getElementById("txtBarCode").value;
    }
    else {
        $("#HiddenBarCode").val("");
    }
    var ColorID = document.getElementById("sel_ColorID").value;
    var BarCode = document.getElementById("HiddenBarCode").value; //商品条码
    var BatchNo = document.getElementById("ddlBatchNo").value; //批次
    var txtSpecification = document.getElementById("txtSpecification").value; //规格型号
    var txtManufacturer = document.getElementById("txtManufacturer").value; //厂家
    var ddlMaterial = document.getElementById("ddlMaterial").value; //材质
    var txtFromAddr = document.getElementById("txtFromAddr").value; //产地
    var txtStorageCount = document.getElementById("txtStorageCount").value; //库存数量
    var txtStorageCount1 = document.getElementById("txtStorageCount1").value; //库存数量
    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值\
    var isbatch=document.getElementById("isbatch").value;
    var prostatus=document.getElementById("DropDownList1").value;
    var storagestatus=document.getElementById("DropDownList2").value;
    var currSelectText = document.getElementById("GetBillExAttrControl1_SelExtValue").options[document.getElementById("GetBillExAttrControl1_SelExtValue").selectedIndex].text;
    var UrlParam = "&pageIndex=" + pageIndex + "&pageCount=" + pageCount + "&orderby=" + orderBy + "&ddlStorage=" + escape(ddlStorage) + "&txtProductNo=" + escape(txtProductNo) + "&txtProductName=" + escape(txtProductName) + "&BarCode=" + escape(BarCode) + "&txtSpecification=" + escape(txtSpecification) + "&txtManufacturer=" + escape(txtManufacturer) + "&ddlMaterial=" + escape(ddlMaterial) + "&txtFromAddr=" + escape(txtFromAddr) + "&txtStorageCount=" + escape(txtStorageCount) + "&txtStorageCount1=" + escape(txtStorageCount1) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + "&BatchNo=" + escape(BatchNo) + "&ColorID=" + escape(ColorID)+"&isbatch="+escape(isbatch)+"&prostatus="+escape(prostatus)+"&storagestatus="+escape(storagestatus);
    var sidex = "ExtField" + EFIndex;
    var selpoint = document.getElementById("HiddenPoint").value;
    var total1=0;
    var total2=0;
    if(isbatch=="1")
    {
        document.getElementById("batch").style.display="none";
    }else
    {
        document.getElementById("batch").style.display="block";
    }
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?action=Get' + UrlParam, //目标地址
        cache: false,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            document.getElementById("thHole").style.display = "none";
            document.getElementById("newItem").innerHTML = "";
            document.getElementById("hiddenEFIndex").value = EFIndex;
            document.getElementById("hiddenEFDesc").value = EFDesc;
            alertPrint =1;
            if (EFIndex != -1 && EFDesc != "" && EFIndex != "") {
                

                document.getElementById("thHole").style.display = "block";
                document.getElementById("newItem").innerHTML = currSelectText;
                $('#divClick').click(function() { OrderBy(sidex, 'Span8'); return false; });
                document.getElementById("hiddenEFIndexName").value = currSelectText;

                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        Num += parseFloat(item.StoreCount);
                        basicNum += parseFloat(item.ProductCount);
                        alertPrint = "1";
                        if(document.getElementById("IsMoreUnit").value=="True")
                        {
                          if(parseFloat(item.ProductCount)>0||parseFloat(item.ProductCount)<0)
                          {
                            total1+=parseFloat(item.ProductCount);
                          }
                          if(parseFloat(item.StoreCount)>0||parseFloat(item.StoreCount)<0)
                          {
                            total2+=parseFloat(item.StoreCount);
                          }
                          if(isbatch=="1")
                          {
                            $("<tr class='newrow'></tr>").append(
                            
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                            
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.UnitID + "\">" + fnjiequ(item.UnitID, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.ProductCount + "\">" + jqControl(item.ProductCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item[sidex] + "\">" + fnjiequ(item[sidex], 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                             $("<tr class='newrow'></tr>").append(
                            
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.BatchNo + "\"><a href='javascript:getfloorbatch(\""+item.ProductID+"\",\""+item.BatchNo+"\")'>" + fnjiequ(item.BatchNo, 10) + "</a></td>" +
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.UnitID + "\">" + fnjiequ(item.UnitID, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.ProductCount + "\">" + jqControl(item.ProductCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item[sidex] + "\">" + fnjiequ(item[sidex], 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                         }
                         }else
                         {

                          if(parseFloat(item.StoreCount)>0||parseFloat(item.StoreCount)<0)
                          {
                            total2+=parseFloat(item.StoreCount);
                          }
                          if(isbatch=="1")
                          {
                                 $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +

                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item[sidex] + "\">" + fnjiequ(item[sidex], 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                                    $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.BatchNo + "\"><a href='javascript:getfloorbatch(\""+item.ProductID+"\",\""+item.BatchNo+"\")'>" + fnjiequ(item.BatchNo, 10) + "</a></td>" +
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +

                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item[sidex] + "\">" + fnjiequ(item[sidex], 10) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                         }
                         }
                    }

                });
            }
            else {

                $.each(msg.data, function(i, item) {
                    if (item.ID != null && item.ID != "") {
                        Num += parseFloat(item.StoreCount);
                        basicNum += parseFloat(item.ProductCount);
                       alertPrint = "1";
                         if(document.getElementById("IsMoreUnit").value=="True")
                        {
                         if(parseFloat(item.ProductCount)>0||parseFloat(item.ProductCount)<0)
                          {
                            total1+=parseFloat(item.ProductCount);
                          }
                          if(parseFloat(item.StoreCount)>0||parseFloat(item.StoreCount)<0)
                          {
                            total2+=parseFloat(item.StoreCount);
                          }
                         if(isbatch=="1")
                         {
                            $("<tr class='newrow' style='width:100%'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.UnitID + "\">" + fnjiequ(item.UnitID, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + jqControl(item.ProductCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                        }
                        else
                        {
                                 $("<tr class='newrow' style='width:100%'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                              "<td height='22' align='center' title=\"" + item.BatchNo + "\"><a href='javascript:getfloorbatch(\""+item.ProductID+"\",\""+item.BatchNo+"\")'>" + fnjiequ(item.BatchNo, 10) + "</a></td>" +
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
                            "<td height='22' align='center'  title=\"" + item.UnitID + "\">" + fnjiequ(item.UnitID, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductCount + "\">" + jqControl(item.ProductCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                        }
                        }else
                        {
                          if(parseFloat(item.StoreCount)>0||parseFloat(item.StoreCount)<0)
                          {
                            total2+=parseFloat(item.StoreCount);
                          }
                          if(isbatch=="1")
                          {
                                $("<tr class='newrow' style='width:100%'></tr>").append(
                            "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                             
    //                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
     
                            "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                             "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                            "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                        }else
                        {
                                  $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' title=\"" + item.StorageNo + "\">" + fnjiequ(item.StorageNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.StorageName + "\">" + fnjiequ(item.StorageName, 10) + "</td>" +
                          "<td height='22' align='center' title=\"" + item.BatchNo + "\"><a href='javascript:getfloorbatch(\""+item.ProductID+"\",\""+item.BatchNo+"\")'>" + fnjiequ(item.BatchNo, 10) + "</a></td>" +
//                        "<td height='22' align='center' title=\"" + item.DeptName + "\">" + fnjiequ(item.DeptName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductNo + "\">" + fnjiequ(item.ProductNo, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ProductName + "\">" + fnjiequ(item.ProductName, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.Specification + "\">" + fnjiequ(item.Specification, 10) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.ColorName + "\">" + fnjiequ(item.ColorName, 10) + "</td>" +
 
                        "<td height='22' align='center' title=\"" + item.CodeName + "\">" + fnjiequ(item.CodeName) + "</td>" +
                         "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + jqControl(item.StoreCount) + "</td>" +
                        "<td height='22' align='center' title=\"" + item.MaterialName + "\">" + fnjiequ(item.MaterialName,10) + "</td>").appendTo($("#pageDataList1 tbody"));
                        }
                        }
                    }

                });
            }
             if (EFIndex != -1 && EFDesc != "" && EFIndex != "") 
             {
                if(document.getElementById("IsMoreUnit").value=="True")
                {
                    if(isbatch=="1")
                    {
                      $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                         
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center'  ></td>" +
                        "<td height='22' align='center'  >" + parseFloat(total1).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' >" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                              $("<tr class='newrow'></tr>").append(
                        "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center'  ></td>" +
                        "<td height='22' align='center'  >" + parseFloat(total1).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' >" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>").appendTo($("#pageDataList1 tbody"));
                         }
                }else
                {
                    if(isbatch=="1")
                    {
                           $("<tr class='newrow'></tr>").append(
                         "<td height='22' align='center' >本页合计</td>" +
                        
                        "<td height='22' align='center' ></td>" +
                       "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +

                       "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                       "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                     }else
                     {
                             $("<tr class='newrow'></tr>").append(
                         "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                       "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +

                       "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' title=\"" + item.StoreCount + "\">" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                       "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                     }
                }
             }
             else
             {
                 if(document.getElementById("IsMoreUnit").value=="True")
                {
                    if(isbatch=="1")
                    {
                     $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                        

                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' >" + parseFloat(total1).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' >" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                       "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                       }else
                       {
                        $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>" +

                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' >" + parseFloat(total1).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' >" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                       "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                       }
                }else
                {
                        if(isbatch=="1")
                        {
                             $("<tr class='newrow' style='width:100%'></tr>").append(
                       "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                        

                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center'>" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                        }else
                        {
                                  $("<tr class='newrow' style='width:100%'></tr>").append(
                       "<td height='22' align='center' >本页合计</td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center' ></td>" +

                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                        "<td height='22' align='center' ></td>" +
                         "<td height='22' align='center'>" + parseFloat(total2).toFixed(selpoint) + "</td>" +
                        "<td height='22' align='center' ></td>" ).appendTo($("#pageDataList1 tbody"));
                        }
                }
             }
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
            document.all["Text2"].value = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pagecount"));
            $("#ToPage").val(pageIndex);
            $("#txtBarCode").val(""); //清空条码
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() {
            hidePopup();
            $("#pageDataList1_Pager").show();
            Ifshow(document.all["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");
            $.ajax({
                type: "POST", //用POST方式传输
                url: '../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?action=SumGet' + UrlParam, //目标地址
                dataType: "json", //数据格式:JSON
                cache: false,
                beforeSend: function() {
                }, //发送数据之前
                success: function(data) {

                    if (data.sta == 1) {
                        var messageValue = data.info.split("|");


                        alertPrint =1;
                        if (EFIndex != -1 && EFDesc != "" && EFIndex != "") {
                          if(document.getElementById("IsMoreUnit").value=="True")
                            {
                            if(isbatch=="1")
                            {
                                $("<tr class='newrow' style='width:100%'></tr>").append(
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            
                             
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                              "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                            "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }else
                        {
                         $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                         
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                          "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }
                        }
                        else
                        {
                            if(isbatch=="")
                            {
                               $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        
                          
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                          "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }else
                        {
                                                           $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                          "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                        }
                        }
                        }
                        else {
                         if(document.getElementById("IsMoreUnit").value=="True")
                            {
                            if(isbatch=="1")
                            {
                            $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        
                          
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                                    $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[1]) + "</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                         }
                         }else
                         {
                         if(isbatch=="1")
                         {
                                $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          
                        
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +

                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                         }else
                         {
                              $("<tr class='newrow' style='width:100%'></tr>").append(
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\" >合计</td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                          
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +

                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>" +
                        "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  >" + jqControl(messageValue[0]) + "</td>" +
                         "<td height='22' align='center' background=\"../../../images/Main/Table_bg.jpg\" bgcolor=\"#FFFFFF\"  ></td>").appendTo($("#pageDataList1 tbody"));
                         }
                         }
                        }


                    }

                },
                error: function() {
                    popMsgObj.ShowMsg('请求发生错误！');
                },
                complete: function() {
                }
            });




        } //接收数据完毕
    });
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}

function Fun_Search_StorageInfo() {
    search = "1";
    document.getElementById("hidSearchCondition").value = search; //这里只是放了一个标志位，说明是点过了检索按钮
    TurnToPage(1);
}

function Ifshow(count) {
    if (count == "0") {
        document.all["divpage"].style.display = "none";
        document.all["pagecount"].style.display = "none";
    }
    else {
        document.all["divpage"].style.display = "block";
        document.all["pagecount"].style.display = "block";
    }
}

//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {

    //判断是否是数字
    if (!PositiveInteger(newPageCount)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示应为正整数！");
        return;
    }
    if (!PositiveInteger(newPageIndex)) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数应为正整数！");
        return;
    }
    if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
        return false;
    }
    else {
        this.pageCount = parseInt(newPageCount, 10);
        TurnToPage(parseInt(newPageIndex, 10));
    }
}
//排序
function OrderBy(orderColum, orderTip) {
    if (document.getElementById("hidSearchCondition").value == "" || document.getElementById("hidSearchCondition").value == null) return;
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↓") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    else {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    orderBy = orderColum + "_" + ordering;
    $("#txtorderBy").val(orderBy); //把排序字段放到隐藏域中，
    Fun_Search_StorageInfo();
}

//物品控件
function Fun_FillParent_Content(id, ProNo, ProdName) {
    document.getElementById('txtProductNo').value = ProNo;
    document.getElementById('txtProductName').value = ProdName;
}



document.onkeydown = ScanBarCodeSearch;
/*列表条码扫描检索*/
function ScanBarCodeSearch() {
    var evt = event ? event : (window.event ? window.event : null);
    var el; var theEvent
    var browser = IsBrowser();
    if (browser == "IE") {
        el = window.event.srcElement;
        theEvent = window.event;
    }
    else {
        el = evt.target;
        theEvent = evt;
    }
    if (el.id != "txtBarCode") {
        return;
    }
    else {
        var code = theEvent.keyCode || theEvent.which;
        if (code == 13) {
            TurnToPage(1);
            evt.returnValue = false;
            evt.cancel = true;
        }
    }
}

function IsBrowser() {
    var isBrowser;
    if (window.ActiveXObject) {
        isBrowser = "IE";
    } else if (window.XMLHttpRequest) {
        isBrowser = "FireFox";
    }
    return isBrowser;
}



//添加批次选择项
function fnGetPackage() {
    var obj = document.getElementById("ddlBatchNo");
    obj.options.length = 1;

    var Storage = document.getElementById("ddlStorage").value;

    var ProductNo = document.getElementById("txtProductNo").value;
    //定义反确认动作变量
    var action = "GetBatchNo";
    var postParam = "action=" + action + "&Storage=" + Storage + "&ProductNo=" + ProductNo;
    $.ajax(
        {
            type: "POST",
            url: "../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?" + postParam,
            dataType: 'html', //返回json格式数据
            cache: false,
            beforeSend: function() {
            },
            error: function() {
            },
            success: function(msg) {
                var msginfo = msg.toString().split(',');
                for (var i = msginfo.length - 1; i >= 0; i--) {
                    if (msginfo[i].toString() != "") {
                        var varItem = new Option(msginfo[i].toString(), msginfo[i].toString());
                        obj.options.add(varItem);
                    }
                }
            }
        });


}
function jqControl(value) {
    var ret = "";
    var point = document.getElementById("HiddenPoint").value;
    if (value != null && value != "" && value != undefined) {
        ret = parseFloat(value).toFixed(point);
    }
    return ret;
} 
//打印单据
 function BillPrint()
 {
    if(alertPrint == "")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先检索！");
        return;
    }
    var ddlStorage = document.getElementById("ddlStorage").value;
    var txtProductNo = document.getElementById("txtProductNo").value;
    var txtProductName = document.getElementById("txtProductName").value;
    if (document.getElementById("txtBarCode").value != "") {
        document.getElementById("HiddenBarCode").value = document.getElementById("txtBarCode").value;
    }
    else {
        $("#HiddenBarCode").val("");
    }
    var ColorID = document.getElementById("sel_ColorID").value;
    var BarCode = document.getElementById("HiddenBarCode").value; //商品条码
    var BatchNo = document.getElementById("ddlBatchNo").value; //批次
    var txtSpecification = document.getElementById("txtSpecification").value; //规格型号
    var txtManufacturer = document.getElementById("txtManufacturer").value; //厂家
    var ddlMaterial = document.getElementById("ddlMaterial").value; //材质
    var txtFromAddr = document.getElementById("txtFromAddr").value; //产地
    var txtStorageCount = document.getElementById("txtStorageCount").value; //库存数量
    var txtStorageCount1 = document.getElementById("txtStorageCount1").value; //库存数量
    var EFIndex = document.getElementById("GetBillExAttrControl1_SelExtValue").value; //扩展属性select值
    var EFDesc = document.getElementById("GetBillExAttrControl1_TxtExtValue").value; //扩展属性文本框值
    var currSelectText = document.getElementById("GetBillExAttrControl1_SelExtValue").options[document.getElementById("GetBillExAttrControl1_SelExtValue").selectedIndex].text;
    var UrlParam = "orderby=" + orderBy + "&ddlStorage=" + escape(ddlStorage) + "&txtProductNo=" + escape(txtProductNo) + "&txtProductName=" + escape(txtProductName) + "&BarCode=" + escape(BarCode) + "&txtSpecification=" + escape(txtSpecification) + "&txtManufacturer=" + escape(txtManufacturer) + "&ddlMaterial=" + escape(ddlMaterial) + "&txtFromAddr=" + escape(txtFromAddr) + "&txtStorageCount=" + escape(txtStorageCount) + "&txtStorageCount1=" + escape(txtStorageCount1) + "&EFIndex=" + escape(EFIndex) + "&EFDesc=" + escape(EFDesc) + "&BatchNo=" + escape(BatchNo) + "&ColorID=" + escape(ColorID);
   
   
    window.open("../../../Pages/PrinttingModel/StorageManager/StorageSearchPrint.aspx?"+UrlParam);
 }
 //显示层
 function getdiv(id)
{
    var h=parseFloat(document.documentElement.clientHeight);
    var w=parseFloat(document.documentElement.clientWidth);
    
    var oObj = document.getElementById(id);
    var divw=parseFloat(oObj.style.width);
    var divh=parseFloat(oObj.style.height);
    w=(w-divw)/2;
    h=(h-divh)/2;
   oObj.style.left = parseInt(w)+"px";
    oObj.style.top = parseInt(h)+"px";
    
    document.getElementById(id).style.display="block";
}
//关闭层
function closeBilldiv()
{
   
    document.getElementById("divBillChoose").style.display="none";
}
//获取产品配置
function getfloorbatch(proid,batchno)
{
          
         $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: '../../../Handler/Office/StorageManager/StorageSearchInfo.ashx?action=getfloorbatch'+'&proid='+escape(proid)+'&batchno='+escape(batchno),//目标地址
           cache:false,
           beforeSend:function(){ getdiv("divBillChoose")},
           success: function(msg)
           {    
                    $("#Table1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                        if(item!=null && item!=""&&item.Pnumber!=undefined)
                        {
                        
                        $("<tr class='newrow'></tr>").append(
                            
                            
                            "<td height='22' align='center' bgcolor='#FFFFFF'>"+ item.Pnumber +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.AbrasionResist +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.BalancePaper +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.BaseMaterial +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.SurfaceTreatment +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.BackBottomPlate  +"</td>"+
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.BuckleType  +"</td>"+
                             
                             "<td align=\"center\" bgcolor='#FFFFFF'>"+item.Pakeages  +"</td>" ).appendTo($("#Table1 tbody")); 
                                 
                   }});
                  
           },
           error: function(){closeBilldiv()},
             complete:function(){}//接收数据完毕
                   });
     
}