var isCust="";

// document.getElementById("oCustName").style.display="1112";
// document.getElementById("oCustName").value = "kkk";
//$("#oCustName").attr("value","kaa");


$(document).ready(function() {
    //物品扩展属性
    //IsDiplayOtherP('GetBillExAttrControl2_SelExtValue', 'GetBillExAttrControl2_TxtExtValue'); //扩展属性
    isCust=document.getElementById("hidisCust").value;
    var DispType = document.getElementById("hidisCust").value;
    // document.getElementById("oCustName_dsp").innerHTML="kkka111111111111111111111";
    document.getElementById("oCustName_dsp").innerText ="客户名称";    
   
    if(isCust=="0")
    {
       document.getElementById("td_custname").style.display="";
       document.getElementById("td_ypclass").style.display="";
       document.getElementById("txtCustName").style.display="";
       document.getElementById("ClassNM").style.display="";
       document.getElementById("img_search").style.display="";       
       document.getElementById("time1").value=AddDays(-30);
       document.getElementById("time2").value=AddDays(0);
    }
    else
    {
       document.getElementById("td_custname").style.display="none";
       document.getElementById("td_ypclass").style.display="none";
       document.getElementById("txtCustName").style.display="none";
       document.getElementById("ClassNM").style.display="none";
       document.getElementById("img_search").style.display="none";
       document.getElementById("time1").value=AddDays(-30);
       document.getElementById("time2").value=AddDays(0);
    }
});


var pageCount = 10; //每页计数
var totalRecord = 0;
var pagerStyle = "flickr"; //jPagerBar样式
var currentPageIndex = 1;
var orderBy = ""; //排序字段
//jQuery-ajax获取JSON数据
function TurnToPage(pageIndex) {
    currentPageIndex = pageIndex;
    var action = "getorderlist";
    var orderno=document.getElementById("txtOrderNo").value;
    var productname=document.getElementById("txtProductName").value;
    var specification=document.getElementById("txtSpecification").value;
    var time1= $("#time1").val();
    var time2= $("#time2").val();
    var BillStatus=$("#BillStatus").val();
    var isSelfOrder=$("#isSelfOrder").val();
    var DispType = document.getElementById("DispType").value;
    if (DispType == "0")  //显示单据信息
    {
       document.getElementById("oproductname_dsp").innerText ="联系人"; 
       document.getElementById("ospecification_dsp").innerText ="地址";  
       document.getElementById("ounitname_dsp").innerText ="电话";  
       document.getElementById("oproductcount_dsp").innerText ="发货方式";  
       document.getElementById("ounitprice_dsp").innerText ="开票方式";    
    
    }
    else                 //显示单据明细信息 
    {
       document.getElementById("oproductname_dsp").innerText ="药品名称"; 
       document.getElementById("ospecification_dsp").innerText ="单位";  
       document.getElementById("ounitname_dsp").innerText ="数量";  
       document.getElementById("oproductcount_dsp").innerText ="价格";  
       document.getElementById("ounitprice_dsp").innerText ="金额";   
    }
    //var EFIndexP = document.getElementById("GetBillExAttrControl2_SelExtValue").value; //物品扩展属性select值
    //var EFDescP = document.getElementById("GetBillExAttrControl2_TxtExtValue").value; //物品扩展属性文本框值
    var EFIndexP = DispType;
    var EFDescP = "";
    //$("#hidEFIndexP").val(EFIndexP);
    //$("#hidEFDescP").val(EFDescP);
      var strUrl = 'pageIndex=' + pageIndex + '&pageCount=' + pageCount + '&orderby=' + escape(orderBy) +
               '&action=' + escape(action)+'&ProductName='+escape(productname)+'&Specification='+escape(specification)+'&EFIndexP='+escape(EFIndexP)+'&EFDescP='+escape(EFDescP)
               +'&OrderNo='+escape(orderno)+'&time1='+escape(time1)+'&time2='+escape(time2)+'&BillStatus='+escape(BillStatus)+'&isSelfOrder='+escape(isSelfOrder);
    if(isCust=="0")
    {
       var CustName=document.getElementById("txtCustName").value;
       var ClassCode=document.getElementById("ClassCode").value;
       strUrl+='&CustName='+escape(CustName)+'&ClassCode='+escape(ClassCode);
    }
        $("#hiddUrl").val(strUrl);
     var total1=0;
     var total2=0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/Office/MedicineManager/MedicineList.ashx', //目标地址
        cache: false,
        data: strUrl,
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            $.each(msg.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                    var OrderNo=item.OrderNo;
                    var CustName=item.CustName;
                    var productname=item.productname;
                    var specification=item.specification;                   
                    var CreatDate_dsp = item.CreateDate.substring(0,10);
                    var ConfirmDate_dsp = item.ConfirmDate.substring(0,10);
                    if (OrderNo != null) {
                        if (OrderNo.length > 15) {
                            OrderNo = OrderNo.substring(0, 15) + '...';
                        }
                      }
                       if (CustName != null) {
                          if (CustName.length > 15) {
                            CustName = CustName.substring(0, 15) + '...';
                        }
                      }
                        if (productname != null) {
                           if (productname.length > 15) {
                            productname = productname.substring(0, 15) + '...';
                        }
                      }
                        if (specification != null) {
                           if (specification.length > 15) {
                            specification = specification.substring(0, 15) + '...';
                        }
                      }
                    if(parseFloat(item.productcount)!=0&&parseFloat(item.productcount)!="NaN")
                    {
                        total1+=parseFloat(item.productcount);
                    }
                     if(parseFloat(item.TotalPrice)!=0&&parseFloat(item.TotalPrice)!="NaN")
                    {
                        total2+=parseFloat(item.TotalPrice);
                    }
                      
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='chk" + i + "' onclick = 'fnUnSelect(this)'  value='" + item.ID + "|"+item.ProductNo+"' name='Checkbox1'  type='checkbox' runat='server'/>" + "</td>" +
                      "<td height='22' align='center'><a href='#' id='OrderNo"+i+"' title=\"" + item.OrderNo + "\"  onclick=fnOrderInfo('" + item.ID + "')>" + OrderNo + "</a></td>" +
                      "<td height='22' align='center'><span title=\"" + item.CustName + "\">" + CustName + "</span></td>" +
                      "<td height='22' align='center'><span title=\"" + item.productname + "\">" + productname + "</span></td>" +
                       "<td height='22' align='center'><span  title=\"" + item.unitname + "\">" + item.unitname + "</span></td>" +
                      "<td height='22' align='center'><span  title=\"" + item.productcount + "\">" + item.productcount + "</span></td>" +
                    //"<td height='22' align='center'><span  title=\"" + item.unitprice + "\">" +parseFloat(item.unitprice).toFixed($("#hidselpoint").val())+ "</span></td>" +
                    //"<td height='22' align='center'><span  title=\"" + item.TotalPrice + "\">" + parseFloat(item.TotalPrice).toFixed($("#hidselpoint").val())+ "</span></td>"+
                    "<td height='22' align='center'><span  title=\"" + item.unitprice + "\">" +item.unitprice+ "</span></td>" +
                    "<td height='22' align='center'><span  title=\"" + item.TotalPrice + "\">" + item.TotalPrice+ "</span></td>"+
                    
                    "<td height='22' align='center'><span  title=\"" + item.sellerName + "\">" + item.sellerName + "</span></td>"+
                    "<td height='22' align='center' id='billStatus"+i+"'>" + item.BillStatusText + "</td>"+
                    "<td height='22' align='center'>" + item.ConfirmName + "</td>"+
                     "<td height='22' align='center'><span title=\"" + item.ConfirmDate + "\">" + ConfirmDate_dsp + "</td>"+
                    
                    //"<td height='22' align='center'>"+item.SendStatusText+"</td>"+
                     "<td height='22' align='center'>" + item.CreateName + "</td>"+
                     "<td height='22' align='center'><span title=\"" + item.CreateDate + "\">" +  CreatDate_dsp+ "</td>" ).appendTo($("#pageDataList1 tbody"));
                }
            });

           $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>" +
                    "<td height='22' align='center' colspan='5'></td>" +
                    "<td height='22' align='center'>" + parseFloat(total1).toFixed($("#hidselpoint").val()) + "</td>" +
                    "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'>" + parseFloat(total2).toFixed($("#hidselpoint").val()) + "</td>" +
                    "<td height='22' align='center'></td>" +   
                    "<td height='22' align='center'></td>" +
                     "<td height='22' align='center'></td>" +
                      "<td height='22' align='center'></td>" +
                     "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'></td>").appendTo($("#pageDataList1 tbody"));
            //页码
            ShowPageBar("pageDataList1_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
            totalRecord = msg.totalCount;
            $("#ShowPageCount").val(pageCount);
            ShowTotalPage(msg.totalCount, pageCount, pageIndex, $("#pageSellOffcount"));
            $("#ToPage").val(pageIndex);
        },
        error: function() { popMsgObj.ShowMsg('请求发生错误！'); },
        complete: function() { hidePopup(); $("#pageDataList1_Pager").show(); Ifshow(document.form1.elements["Text2"].value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });

}

//选择客户
function  fnSelectCustInfo() {
        popSellCustListObj.ShowList('protion');
}

function Ifshow(count) {
    if (count == "0") {
        document.getElementById("divpage").style.display = "none";
        document.getElementById("pageSellOffcount").style.display = "none";
    }
    else {
        document.getElementById("divpage").style.display = "block";
        document.getElementById("pageSellOffcount").style.display = "block";
    }
}
//去除全选按钮
function fnUnSelect(obj) {


    if (!obj.checked) {
        $("#checkall").removeAttr("checked");
        return;
    }

    else {
        //验证明细信息
        var signFrame = findObj("pageDataList1", document);
        var iCount = 0; //明细中总数据数目
        var checkCount = 0; //明细中选择的数据数目
        for (i = 0; i < signFrame.rows.length - 1; i++) {

            iCount = iCount + 1;

            if ($("#chk" + i).attr("checked")) {
                checkCount = checkCount + 1;
            }
        }
        if (checkCount == iCount) {

            $("#checkall").attr("checked", "checked");
        }

    }
}

//全选
function selectall() {
    $.each($("#pageDataList1 :checkbox"), function(i, obj) {
        obj.checked = $("#checkall").attr("checked");
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
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount, newPageIndex) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    $("#checkall").removeAttr("checked")
    if (!IsNumber(newPageIndex) || newPageIndex == 0) {
        isFlag = false;
        fieldText = fieldText + "跳转页面|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!IsNumber(newPageCount) || newPageCount == 0) {
        isFlag = false;
        fieldText = fieldText + "每页显示|";
        msgText = msgText + "必须为正整数格式|";
    }
    if (!isFlag) {
        popMsgObj.Show(fieldText, msgText);
    }
    else {
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            this.pageCount = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
}

//排序
function OrderBy(orderColum, orderTip) {
    var strUrl = $.trim($("#hiddUrl").val());
    if (strUrl.length == 0) {
        return;
    }
    var ordering = "d";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM = $(".orderTip");
    if ($("#" + orderTip).html() == "↑") {
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↓");
    }
    else {
        ordering = "a";
        allOrderTipDOM.empty();
        $("#" + orderTip).html("↑");
    }
    orderBy = orderColum + "_" + ordering;
    $("#hiddExpOrder").val(orderBy);
    TurnToPage(1);
}

//查看明细
function fnOrderInfo(retval) {

    if(isCust=="1")
     {
       //新窗口打开
       //window.open("DealCustOrder.aspx?action=edit&id="+escape(retval)+"&isCust="+escape(isCust));
       window.parent.addTab(null, '2130301', '销售订单', 'Pages/Office/MedicineManager/DealCustOrderDemo.aspx?action=edit&id='+escape(retval)+'&isCust='+escape(isCust));
     }
    else
    {
      //新窗口打开
      // window.open("DealCustOrder.aspx?action=deal&id="+escape(retval)+"&isCust="+escape(isCust));
        window.parent.addTab(null, '2130301', '销售订单', 'Pages/Office/MedicineManager/DealCustOrderDemo.aspx?action=deal&id='+escape(retval)+'&isCust='+escape(isCust));
    }
}

////删除订单 edit by dyg 2012-09-15
//function deleteOrder()
//{
//     var ck = document.getElementsByName("Checkbox1");//此时的ck不是一个字符串，而是一个字符串数组。
//     var ids = "";//声明一个变量，辅助作用
//     var newIds;
//     var orderid="";
//     var add=0;//声明一个数组下标0，用于确定数组的首元素
//     var add2=0;//声明一个数组下标0，用于确定数组的首元素
//     var value=new Array();//声明一个数组，用于存放数值
//     var newValue=new Array();//声明一个数组，用于存放修改过的数值
//     var newOrderID=new Array();//声明一个数组，用于存放最终转换好格式的数值
//     var isexist;//标记是否存在
//     var action="deleteBills";
//  
//    alert("仅能删除制单和作废状态下的订单！");
//       for (var i = 0; i < ck.length; i++) 
//       {//该循环用于排除处于确认状态下的订单，并将其设置为非选择状态
//          if (ck[i].checked) 
//          {
//            if(document.getElementById("billStatus"+i).innerHTML=="确认")
//              {           
//              ck[i].checked=false; 
//              }
//              else
//              {
//              
//              }
//          }
//        }
//    for (var i = 0; i < ck.length; i++) 
//    {
//        if (ck[i].checked)
//         {       
////             alert(i+ck[i].value);
//               //记录数组的下标 
//                if (ids != "")
//                    ids += ",";//为第一个字符串的结尾处添加逗号分隔
//                 ids +=ck[i].value;//获得这个控件的value值
//                 value[add]=ids;add++;//将值保存到数组中 
//        
//          }
//    }
//    if(value.length==1)
//    {
//       newOrderID[0]=value[0].split(",")[0].split("|")[1];
//    }
//    else
//    {
//        for(var k=0;k<value.length;k++)
//        {
//         
//            newIds=value[k].split(",")[k].split("|")[1];
//            //调用二次split函数，对数组元素中的数值进行处理，第一次split函数的处理结果是取出形如XX|XX的字符串
//            //第二次split函数的处理是针对形如XX|XX的字符串取值，通过这次的函数处理，将得到“|”符号前面的数值。
//            newValue[k]=newIds;//将split函数处理好的数值存到数组中
//        }
//       newOrderID[0]=0;//确定首元素,便于下面的数值筛选的逻辑判断
//        // alert(value.length);
//         
//        for(var j=0;j<newValue.length;j++)//for循环，用于遍历数组元素，外层循环
//        {              
//            if(newValue[j]==newValue[j+1])
//            { 
//             isexist=0;//设置初始状态
//             for(var m=0;m<newOrderID.length;m++)//内循环，该层循环的长度为newOrderID数组的长度
//               {
//                if(newValue[j]==newOrderID[m])
//                isexist=1;
//                else
//                isexist=0;
//               }  
//               if(isexist==1)//如果newValue的两个相邻的元素的值相等，并且该值在newOrderID数组中也存在，则不进行任何操作。
//                {}
//               else
//               {
//                 newOrderID[add2]=newValue[j];add2++;//如果在newOrderID数组中不存在，则去折两个元素的第一个元素值，并将它赋值给newOrderID数组中去。
//                 }
//            }
//          if(newValue[j]!=newValue[j+1])      
//             {
//              isexist=0;
//               for(var m=0;m<newOrderID.length;m++)//内循环，该层循环的长度为newOrderID数组的长度
//               {
//                if(newValue[j+1]==newOrderID[m])
//                isexist=1;
//                else
//                isexist=0;
//               }  
//                  if(isexist==1)
//                    {}
//                   else
//                   {
//                     newOrderID[add2]=newValue[j+1];add2++;//如果在newOrderID数组中不存在，则去折两个元素的第二个元素值，并将它赋值给newOrderID数组中去。
//                     }
//             }
//                          
//         }
//     }
//      //   alert("您选择了"+newValue[0]+newValue[1]+newValue[2]);
//    //alert(value.length+""+value);
// alert(newOrderID);
//      if (ids == "") {
//        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有选择数据，请选择一项处理！");
//        return;
//    }
////===============================jquery ajax 数据处理==============================================//
//  $.ajax({
//             type: "POST",
//        url: "../../../Handler/Office/MedicineManager/DealCustOrder.ashx?action="+action+"&orderNo="+newOrderID,
//      dataType:"json",
//       cache: false,
//        beforeSend: function() {
//          
//        },
//        error: function() { alert("数据获取失败!"); },
//        success: function(msg) {
//          alert("订单表中共删除了"+msg.rs+"条记录!订单明细表中共删除了"+msg.rsDetial+"条记录！");
//        
//        }
//      })
//}
//============================================= 删除订单修订版 ==================================================================//
//modified by DYG 2012-09-17
//函数说明：用于删除处于制单或作废状态下的单据
//返回值：无
function DeleteOrder()
{
  var ck = document.getElementsByName("Checkbox1");//此时的ck不是一个字符串，而是一个字符串数组。
     var ids = "";//声明一个变量，辅助作用
     var newIds;
     var orderid="";
     var add=0;//声明一个数组下标0，用于确定数组的首元素
     var add2=0;//声明一个数组下标0，用于确定数组的首元素
     var value=new Array();//声明一个数组，用于存放数值
     var newValue=new Array();//声明一个数组，用于存放修改过的数值
     var newOrderID=new Array();//声明一个数组，用于存放最终转换好格式的数值
     var isexist;//标记是否存在
     var action="deleteBills";
  
    alert("仅能删除制单和作废状态下的订单！");
       for (var i = 0; i < ck.length; i++) 
       {//该循环用于排除处于确认状态下的订单，并将其设置为非选择状态
          if (ck[i].checked) 
          {
            if(document.getElementById("billStatus"+i).innerHTML=="确认"||document.getElementById("billStatus"+i).innerHTML=="审核通过")
              {           
              ck[i].checked=false; 
              }
              else
              {
           
              }
          }
        }
          for (var i = 0; i < ck.length; i++) 
            {
                if (ck[i].checked)
                 {       
        //             alert(i+ck[i].value);
                       //记录数组的下标 
                     
                         value[add]=document.getElementById("OrderNo"+i).title;add++;//将值保存到数组中 
                
                  }
                  else
                  {}
            }
          //  alert(value);
            
    if(value.length==1)
    {
       newOrderID[0]=value[0];
    }
    else
    {
       
       newOrderID[0]=value[0];//确定首元素,便于下面的数值筛选的逻辑判断
        // alert(value.length);
         
        for(var j=0;j<value.length;j++)//for循环，用于遍历数组元素，外层循环
        {              
            if(value[j]==value[j+1])
            { 
             isexist=0;//设置初始状态
             for(var m=0;m<newOrderID.length;m++)//内循环，该层循环的长度为newOrderID数组的长度
               {
                    if(value[j]==newOrderID[m])
                    {
                    isexist=1;//如果value的两个相邻的元素的值相等，并且在newOrderID数组中存在该数值，则设isexist的值为1
                    }
                    else
                    {
                    isexist=0;
                    }
               }
               //如果该数值在数组中存在，则不进行任何操作  
               if(isexist==1)
                {
              
                }
               else
               {
                 newOrderID[add2+1]=value[j];add2++;//如果在newOrderID数组中不存在，则去取两个元素的第一个元素值，并将它赋值给newOrderID数组的中去。
                 }
            }
          if(value[j]!=value[j+1])      
             {
              isexist=0;
               for(var m=0;m<newOrderID.length;m++)//内循环，该层循环的长度为newOrderID数组的长度,该循环用于检查数组中是否存在与之相等的元素
               {
                if(value[j+1]==newOrderID[m])
                isexist=1;
                else
                isexist=0;
               }  
                  if(isexist==1)
                    {}
                   else
                   {
                     newOrderID[add2+1]=value[j+1];add2++;//如果在newOrderID数组中不存在，则去折两个元素的第二个元素值，并将它赋值给newOrderID数组中去。
                     }
             }
                          
         }
     }
      //   alert("您选择了"+newValue[0]+newValue[1]+newValue[2]);
    //alert(value.length+""+value);
 //alert(newOrderID); 
 
    if (value == "") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有选择数据，请选择一项处理！");
        return;
    }
//===============================jquery ajax 数据处理==============================================//
 $.ajax({
             type: "POST",
        url: "../../../Handler/Office/MedicineManager/DealCustOrder.ashx?action="+action+"&orderNo="+newOrderID,
      dataType:"json",
       cache: false,
        beforeSend: function() {
          
        },
        error: function() { alert("数据获取失败!"); },
        success: function(msg) {
          alert("订单表中共删除了"+msg.rs+"条记录!订单明细表中共删除了"+msg.rsDetial+"条记录！");
        
        }
      })
}


//显示订单状态
function showBillStatus()
{
     var ck = document.getElementsByName("Checkbox1");//此时的ck不是一个字符串，而是一个字符串数组。
     var billStatus="";
       for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
       //记录数组的下标 
        if(document.getElementById("billStatus"+i).innerHTML=="确认")
        {
       billStatus+=document.getElementById("billStatus"+i).innerHTML;
        ck[i].checked=false;
       }
        }
       
    } alert(billStatus);
}

//处理订单--
function DealOrder() 
{
    var ck = document.getElementsByName("Checkbox1");
    var ids = "";
    var orderid="";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            if (ids != "")
                ids += ",";
            ids += ck[i].value;
        }
    }

    if (ids == "") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有选择数据，请选择一项处理！");
        return;
    }

    if (ids.split(",").length > 1) {
          var id_temp=ids.split(",")[0];
           for(var k=1;k<ids.split(",").length;k++)
           {
              if(ids.split(",")[k]!=id_temp)
              {
                  showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无法同时处理多条记录！请选择同一订单！");
                  return;
              }
           }
           orderid=id_temp;
    }
    else
    {
       orderid=ids;
    }
   window.open("DealCustOrder.aspx?action=deal&id="+escape(orderid)+"&isCust="+escape(isCust));
}

//修改订单
function EditOrder()
{
    var ck = document.getElementsByName("Checkbox1");
    var ids = "";
    var orderid="";
    for (var i = 0; i < ck.length; i++) {
        if (ck[i].checked) {
            if (ids != "")
                ids += ",";
            ids += ck[i].value;
        }
    }

    if (ids == "") {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "没有选择数据，请选择一项修改！");
        return;
    }

    if (ids.split(",").length > 1) {
          var id_temp=ids.split(",")[0];
           for(var k=1;k<ids.split(",").length;k++)
           {
              if(ids.split(",")[k]!=id_temp)
              {
                  showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "无法同时修改多条记录！请选择同一订单！");
                  return;
              }
           }
           orderid=id_temp;
    }
    else
    {
       orderid=ids;
    }
  window.open("DealCustOrder.aspx?action=edit&id="+escape(orderid)+"&isCust="+escape(isCust));
}

function AddDays(AddDayCount)
{
    var dd = new Date(); 
    dd.setDate(dd.getDate()+AddDayCount);//获取AddDayCount天后的日期 
    var y = dd.getYear(); 
    var m = dd.getMonth()+1;//获取当前月份的日期 
    var d = dd.getDate(); 
    return y+"-"+m+"-"+d; 
}