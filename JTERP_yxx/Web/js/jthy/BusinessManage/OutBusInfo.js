    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "id_a";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    
    var BusiType="";//1.普通销售 2.采购直销
    var typeflag="";
    $(document).ready(function()
    {
      requestobj = GetRequest();  
      var Isliebiao =requestobj['Isliebiao'];
      var PageIndex = requestobj['PageIndex'];
      var PageCount = requestobj['PageCount'];
      var typeflag=requestobj['typeflag'];
    if(typeflag=="0")  //普通销售
    {
        BusiType="1";
       
        $("#trProvider").hide();
        $("#purContractList").hide();
    }
    if(typeflag=="1")  //采购直销
    {
        BusiType="2";
        $("#trProvider").css("display","");
    }
    var url = location.search;
    var theRequest = new Object();
  
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
  
  function GetSearchCondition()
  {
        if(!fnCheck())
        {
            return;
        }        
        document.getElementById("checkall").checked = false;
    
        ifdel="0";
        var SendNo=$("#txtSendNo").val();  //发货单号
        var ProviderName=$("#txtProviderName").val();  //供货方 
        var BeginT=$("#txtBeginT").val();  //发货开始时间
        var EndT=$("#txtEndT").val();       //发货结束时间
        var SellerName=$("#txtSellerName").val();  //业务员
        var TranSNo=$("#txtTranSNo").val();  //调运单号
        var SendState=$("#txtSendState").val();  //运送状态        
     
        var PurContractNo=$("#txtPurContractNo").val(); //采购合同号
        var DeptID=$("#hdDeptID").val();  // 部门
        var ContractNo=$("#txtContractNo").val(); //销售合同号
        var CustName = $("#txtCustomerName").val(); //客户名称
         
        var SearchInfo= "&SendNo="+reescape(SendNo)+"&ProviderName="+reescape(ProviderName)+"&BeginT="+reescape(BeginT)+
                 "&EndT="+reescape(EndT)+"&SellerName="+reescape(SellerName)+"&TranSNo="+reescape(TranSNo)+"&SendState="+reescape(SendState)+
                 "&PurContractNo="+reescape(PurContractNo)+"&DeptID="+reescape(DeptID)+"&ContractNo="+reescape(ContractNo)+"&CustName="+reescape(CustName);  //数据
                 
        $("#hidSearchCondition").val(SearchInfo);
  }
  
  //搜索
   function SearchOutBus(pageIndex)
   {   
        currentPageIndex = pageIndex;
        action="SearchOutBusList";
        GetSearchCondition();
        TurnToPage(currentPageIndex)
   }
       
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           var totalProductcount = 0;
           var totalMoney = 0;

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/BusinessManage/OutBusInfo.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageCount=" + currentpageCount + "&Action=" + action + "&orderby=" + orderBy + "&BusiType=" + escape(BusiType) +
                    document.getElementById("hidSearchCondition").value,
               beforeSend: function () { AddPop(); $("#pageDataList1_PagerList").hide(); }, //发送数据之前

               success: function (msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataList1 tbody").find("tr.newrow").remove();
                   var j = 1;
                   $.each(msg.data, function (i, item) {
                       if (item.id != null && item.id != "") {
                           if (item.productcount == "") item.productcount = 0;
                           if (item.totalfee == "") item.totalfee = 0;
                           if (requestobj['typeflag'] == "1") // 采购直销
                           {
                               $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                              "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +
                              "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +
                              "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('90011','" + item.id + "')><span title=\"" + item.sendno + "\">" + item.sendno + "</a></td>" +
                              "<td height='22' align='center'>" + item.custname + "</td>" +
                              "<td height='22' align='center'>" + item.productname + "</td>" +
                              "<td height='22' align='center'>" + QfwFormat(parseFloat(item.productcount), 2) + "</td>" +
                              "<td height='22' align='center'>" + QfwFormat(parseFloat(item.TaxPrice), 2) + "</td>" +
                              "<td height='22' align='center'>" + QfwFormat(parseFloat(item.totalfee), 2) + "</td>" +
                              "<td height='22' align='center'>" + item.startstation + "</td>" +
                              "<td height='22' align='center'>" + item.endstation + "</td>" +
                              "<td height='22' align='center'>" + item.transstate + "</td>" +
                              "<td height='22' align='center'>" + item.BillStatus + "</td>" +
                              "<td height='22' align='center'>" + item.DeptName + "</td>" +
                              "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('80011','" + item.SaleContractId + "')>" + item.SaleContractNo + "</a></td>" +
                              "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('80021','" + item.CtrId + "')>" + item.ContractId + "</a></td>" +
                              "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('9992','" + item.DiaoyunId + "')>" + item.DiaoyunNo + "</a></td>").appendTo($("#pageDataList1 tbody"));
                               if ((parseFloat(item.productcount) != 0) && (item.productcount != "")) {
                                   totalProductcount += parseFloat(item.productcount);
                               }
                               if ((parseFloat(item.totalfee) != 0) && (item.totalfee != "")) {
                                   totalMoney += parseFloat(item.totalfee);
                               }
                               //alert(j);
                           }
                           if (requestobj['typeflag'] == "0")//出库销售
                           {
                               //alert(j);
                               $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                               "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +
                               "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +
                               "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('90031','" + item.id + "')><span title=\"" + item.sendno + "\">" + item.sendno + "</a></td>" +
                               "<td height='22' align='center'>" + item.custname + "</td>" +
                               "<td height='22' align='center'>" + item.productname + "</td>" +
                               "<td height='22' align='center'>" + QfwFormat(parseFloat(item.productcount), 2) + "</td>" +
                               "<td height='22' align='center'>" + QfwFormat(parseFloat(item.TaxPrice), 2) + "</td>" +
                               "<td height='22' align='center'>" + QfwFormat(parseFloat(item.totalfee), 2) + "</td>" +
                               "<td height='22' align='center'>" + item.startstation + "</td>" +
                               "<td height='22' align='center'>" + item.endstation + "</td>" +
                               "<td height='22' align='center'>" + item.transstate + "</td>" +
                               "<td height='22' align='center'>" + item.BillStatus + "</td>" +
                               "<td height='22' align='center'>" + item.DeptName + "</td>" +
                               "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('80011','" + item.SaleContractId + "')>" + item.SaleContractNo + "</a></td>" +
                               "<td height='22' align='center'><a href='#' onclick=GetLinkParam1('9992','" + item.DiaoyunId + "')>" + item.DiaoyunNo + "</a></td>").appendTo($("#pageDataList1 tbody"));
                               if ((parseFloat(item.productcount) != 0) && (item.productcount != "")) {
                                   totalProductcount += parseFloat(item.productcount);
                               }
                               if ((parseFloat(item.totalfee) != 0) && (item.totalfee != "")) {
                                   totalMoney += parseFloat(item.totalfee);
                               }
                           }
                       }
                   });

                   $("<tr class='newrow'></tr>").append("<td height='22' align='center'>本页合计</td>" +
                    "<td colspan='3'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(totalProductcount, 2) + "</td>" +
                    "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(totalMoney, 2) + "</td>" +
                    "<td colspan='8'></td>").appendTo($("#pageDataList1 tbody"));

                   $("<tr class='newrow'></tr>").append("<td height='22' align='center'>总计</td>" +
                     "<td colspan='3'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(msg.ttlCount, 2) + "</td>" +
                    "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(msg.ttlFee, 2) + "</td>" +
                    "<td colspan='8'></td>").appendTo($("#pageDataList1 tbody"));

                   //页码
                   ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: currentpageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"
                }//[attr]
                    ); document.getElementById("checkall").checked = false;
                   totalRecord = msg.totalCount;
                   // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                   document.getElementById("Text2").value = msg.totalCount;
                   $("#ShowPageCount").val(currentpageCount);
                   ShowTotalPage(msg.totalCount, currentpageCount, pageIndex, $("#pagecount"));
                   $("#ToPage").val(pageIndex);
               },
               error: function () { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
               complete: function () { hidePopup(); $("#pageDataList1_PagerList").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=0;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		//t[i].onclick=function(){//鼠标点击
			//if(this.x!="1"){
				//this.x="1";//
				//this.style.backgroundColor=d;
			//}else{
			//	this.x="0";
				//this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
			//}
		//}
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}


function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }

/*
* 获取链接的参数
*/
function GetLinkParam()
{
//    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    
    linkParam = "ProviderInfo_Add.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + currentpageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
}

//跳转到销售单查看页面
function GetLinkParam1(typeid,id) {
    if(typeid=="90021")
       parent.addTab(null,'90021','采购到货单','Pages/JTHY/BusinessManage/InBus_ADD.aspx?ModuleID=90021&intMasterID='+escape(id));  
    if(typeid=="80021")
       window.parent.addTab(null,'80021','采购合同','Pages/JTHY/ContractManage/PurContract_Add.aspx?ModuleID=80021&intMasterID='+escape(id));
    if(typeid=="80011")
       window.parent.addTab(null,'80011','销售合同','Pages/JTHY/ContractManage/SellContract_Add.aspx?ModuleID=80011&intMasterID='+escape(id));
    if(typeid=="9992")
       window.parent.addTab(null, '9992', '调运单管理', 'Pages/JTHY/TransPortManage/TranSport_ADD.aspx?ModuleID=9992&intMasterID=' + escape(id) );
    if(typeid=="90031")
      window.parent.addTab(null,'90031','出库销售单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=0&ModuleID=90031&intMasterID='+escape(id)+'');
    if(typeid=="90011")
      window.parent.addTab(null,'90011','采购直销单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=1&ModuleID=90011&intMasterID='+escape(id)+'');
       
//    if(BusiType=="1")  //销售出库
//    {
//        window.parent.addTab(null,'90031','出库销售单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=0&ModuleID=90031&intMasterID='+escape(id)+'');
//    }
//    if(BusiType=="2")  //采购直销
//    {
//        window.parent.addTab(null,'90011','采购直销单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=1&ModuleID=90011&intMasterID='+escape(id)+'');
//    }
}

//跳转到销售单新建页面
function  CreateSaleOrder()
{ 
    if(BusiType=="1")  //销售出库
    {
        window.parent.addTab(null,'90031','出库销售单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=0&ModuleID=90031');
    }
    if(BusiType=="2")  //采购直销
    {
        window.parent.addTab(null,'90011','采购直销单','Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=1&ModuleID=90011');
    }
    
}



function fnCheck()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
    popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}

 
function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtContractNo").value = "";
    document.getElementById("txtTitle").value = "";
    document.getElementById("DrpTypeID").value = "";
    document.getElementById("txtHidOurDept").value = "";
    document.getElementById("txtDeptID").value = "";
    document.getElementById("txtSeller").value = "";
    document.getElementById("txtSeller").title = "";
    document.getElementById("ddlFromType").value = "0";
    document.getElementById("txtHidProviderID").value = "";
    document.getElementById("txtProviderID").value = "";
    document.getElementById("ddlBillStatus").value = "0";
    document.getElementById("ddlUsedStatus").value = "0";
    
    document.getElementById("pagecount").style.display = "none";
//    document.getElementById("divpage").style.display = "none";
    document.getElementById("pageDataList1_PagerList").style.display = "none";
}

    
function Ifshow(count)
    {
        if(count=="0")
        {
            document.getElementById("divpage").style.display = "none";
            document.getElementById("pagecount").style.display = "none";
        }
        else
        {
            document.getElementById("divpage").style.display = "block";
            document.getElementById("pagecount").style.display = "block";
        }
    }
    
    function SelectDept(retval)
    {
        alert(retval);
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数必须输入正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数必须输入正整数！');
          return;
       }
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
//            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
//            return false;
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return;
        }
        else
        {
            currentpageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
    if(issearch=="")
            return;
        var ordering = "a";
        //var orderTipDOM = $("#"+orderTip);
        var allOrderTipDOM  = $(".orderTip");
        if( $("#"+orderTip).html()=="↓")
        {
             allOrderTipDOM.empty();
             $("#"+orderTip).html("↑");
        }
        else
        {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#"+orderTip).html("↓");
        }
        orderBy = orderColum+"_"+ordering;
        TurnToPage(1);
    }
 
 
 function SelectAll() {
        $.each($("#pageDataList1 :checkbox"), function(i, obj) {
            obj.checked = $("#checkall").attr("checked");
        });
    }
 
 

//删除操作
function  DelSelOrder()
{ 
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
        var ck = document.getElementsByName("Checkbox1");
        var table=document.getElementById("pageDataList1"); 
        var URLParams = "";  
        var Action = "DeleteOutBus";
        URLParams+="Action="+Action;
        var index = 0;
        var OutBusID="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked ) {
                debugger;
                OutBusID += table.rows[ck[i].value].cells[2].innerText+',';
                index++;
               
            }       
        }
        
        OutBusID = OutBusID.substring(0,OutBusID.length-1);
         
        if(index == 0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
            return;
        }
        URLParams+="&Length="+index+"";
        
        $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  "../../../Handler/JTHY/BusinessManage/DealOutBus_ADD.ashx",//目标地址
               data:URLParams+"&allOutBusID="+escape(OutBusID)+'&billtype='+escape(typeflag)+' ', //数据
               cache:false,
               beforeSend:function(){},//发送数据之前
                error: function() 
                {
                   popMsgObj.ShowMsg('请求发生错误！');
                   return;
                }, 
                success: function(msg){
           
                    if(msg.sta > 0)
                    {
                        popMsgObj.ShowMsg(msg.info);
                        SearchOutBus(1);  //刷新页面
                    }
                    else
                    {
                        popMsgObj.ShowMsg(msg.info);
                    }
                  },

   
               complete:function(){
                    hidePopup();
               }
        });
    }
}



