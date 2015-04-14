    var pageCount = 20;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 20;
    var action = "";//操作
    var orderBy = "ModifiedDate_d";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";
    
   
    var isShowWaring="";
    var waringname="";
    var toend="";
    
    $(document).ready(function()
    {
    requestobj = GetRequest(); 
    var CustNo = "";
    var CustName = ""; 
    var CustNam = "";
    var PYShort = "";
    var CustType = "";
    var CustClass = "";
    var CustClassName = "";
    var AreaID = "";
    var CreditGrade = "";
    var Manager = "";
    var ManagerName ="";
    var StartCreateDate = "";
    var EndCreateDate = "";
    SearchProductInfoData();
   
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
    isShowWaring= requestobj['isShowWaring'];//是否预警
    waringname=requestobj['waringname'];//证件名称
    toend=requestobj['toend']; //1-即将到期 2-已过期
    if(isShowWaring=="undefined")
    {
    isShowWaring="";
    }
    if(waringname=="undefined")
    {
    waringname="";
    }
    if(toend=="undefined")
    {
    toend="";
    }
//  if(typeof(ContractNo1)!="undefined")
    if(typeof(Isliebiao)!="undefined")
    { 
       currentPageIndex = PageIndex;
       currentpageCount = PageCount;
       if(isShowWaring=="1")
       {
         SearchProductInfoData();
       }

    }
    
    var url = location.search;
    var theRequest = new Object();
    
    if (url.indexOf("?") != -1) 
    {
             var str = url.substr(1);
        
             var strs=str.split("&");
             type=strs[0].split("=")[1];
             //data=strs[1].split("=")[1];
             data="";
             if(data.indexOf("|") != -1)
              {
                day=data.split("|")[0];
                modifyDay=data.split("|")[1];
              }
              else
              {
               modifyDay=data;
               day="";
              }
            //alert(type+" "+day+" "+modifyDay);
           
           
             if(isShowWaring=="1")
             {
              SearchProductInfoData();
             }
        
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
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
  
           currentPageIndex = pageIndex;

           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/Office/MedicineManager/MyOrderInfo.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageCount=" + currentpageCount + "&action=" + action + "&orderby=" + orderBy +
                    document.getElementById("hidSearchCondition").value,
               beforeSend: function() { AddPop(); $("#pageDataList1_PagerList").hide(); }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataList1 tbody").find("tr.newrow").remove();
                   var j = 1;
                   $.each(msg.data, function(i, item) {
                       if (item.autoid != null && item.autoid != "") {
                          var keyvalue="";
                          if(item.BillStatus=="待审核")
                          {
                             keyvalue="删除";
                          }
                                        
               
                           $("<tr class='newrow'></tr>").append(
                        "<td height='30' align='center' style='display:none'>" + (j++) + "</td>" +  
                         "<td height='30' align='center' style='display:none'>" + item.autoid + "</td>" +    
                         
                          "<td height='30' align='center' style='display:none'>" + item.cInvCode + "</td>" +  
                           "<td height='30' align='center' style='display:none'><input type='text' id='cinvcode"+j+"'  class='smallInput' value='"+item.cInvCode+"' /></td>" +                       
                        "<td height='30' align='center'>" + item.orderid + "</td>" +   
                        "<td height='30' align='center'>" + item.BillStatus + "</td>" +   
                          "<td height='30' align='center'>" + item.CreateDate + "</td>" +  
                           
                        "<td height='30' align='center'>" + item.cInvName + "</td>" +          
                        "<td height='30' align='center'>" + item.cAddress + "</td>" +
                        
                        "<td height='30' align='center'>" + item.cInvStd + "</td>" +
                        "<td height='30' align='center'>" + item.cComUnitName + "</td>" +
                        "<td height='30' align='center'>" + item.Packge + "</td>" +
                         "<td height='30' align='center'>" + item.MPackge + "</td>" +
                         
                           "<td height='30' align='center'>" + item.cBatch + "</td>" +
                             "<td height='30' align='center'>" + item.dVDate + "</td>" +
                               "<td height='30' align='center'>" + item.cFile + "</td>" +
                                "<td height='30' align='center'>" + item.PurchaseNum + "</td>" +
                              
                                  "<td height='30' align='center'>" + item.D_Money + "</td>" +
                               
                                 "<td height='30' align='center'><a href='#' onclick=delrow('"+item.autoid+"')>"+keyvalue+"</a></td>").appendTo($("#pageDataList1 tbody"));
                                 
                        
                       }
                   });
                   //页码
                   ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: currentpageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}
                    ); 
                   totalRecord = msg.totalCount;
                   // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                   document.getElementById("Text2").value = msg.totalCount;
                   $("#ShowPageCount").val(currentpageCount);
                   ShowTotalPage(msg.totalCount, currentpageCount, pageIndex, $("#pagecount"));
                   $("#ToPage").val(pageIndex);
               },
               error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
               complete: function() { hidePopup(); $("#pageDataList1_PagerList").show(); Ifshow(document.getElementById("Text2").value); pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
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

var M='20131008';
function GetLinkParam1(autoid) {   
    parent.addTab(null,'0','购物车','Pages/Office/CustWebOrder/Cart.aspx?autoid='+escape(autoid));
}

 function delrow(autoid)
    {
    
    
    //检索条件
    var issearch=1;

    var DrugName ="";//品名
    var Address = "";//厂家
    var DrugStd= "";//规格
    var DrugClass="";//品类
    var getlist="";
    
   
    action="delrow";
   
    var Isliebiao = 1;

    var URLParams = "action="+escape(action)+"&Isliebiao="+escape(Isliebiao)+"&DrugName="+escape(DrugName)+"&Address="+escape(Address)+"&DrugStd="+escape(DrugStd)
    +"&waringname="+escape(waringname)+"&toend="+escape(toend)+"&isShowWaring="+escape(isShowWaring)+"&jixinglist="+escape(getlist)
    +"&DrugClass="+escape(DrugClass)+"&autoid="+escape(autoid)+""; 
      //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
   isShowWaring="";

      search="1";
      TurnToPage(currentPageIndex);
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

function  GetCheckBoxListValue(objID)
{
     var  v =  new  Array();
     var  CheckBoxList = document.getElementById(objID);
     if (CheckBoxList.tagName == "TABLE")
     {
         for (i=0;i<CheckBoxList.rows.length;i++)    
             for (j=0;j<CheckBoxList.rows[i].cells.length;j++)
   if (CheckBoxList.rows[i].cells[j].childNodes[0])
                     if (CheckBoxList.rows[i].cells[j].childNodes[0].checked== true )
                        v.push("'"+CheckBoxList.rows[i].cells[j].childNodes[1].innerText+"'");
    } 
     if (CheckBoxList.tagName == "SPAN")
     {
         for (i=0;i<CheckBoxList.childNodes.length;i++)
             if (CheckBoxList.childNodes[i].tagName == "INPUT")
                 if (CheckBoxList.childNodes[i].checked== true )
                 {
                    i++;
                    v.push("'"+CheckBoxList.childNodes[i].innerText+"'");
                }             
    } 
     return  v;
} 

function SearchProductInfoData()
    {
    
    if(!fnCheck())
    return;
    
    //检索条件
    var issearch=1;

    var DrugName = document.getElementById("txtDrugName").value.Trim();//品名
    var Address = document.getElementById("txtAddress").value.Trim();//厂家
    var DrugStd= document.getElementById("txtDrugStd").value.Trim();//规格
    var DrugClass=document.getElementById("txtDrugClass").value.Trim();//品类
    var getlist=GetCheckBoxListValue('listJiXing');
    
    var date1=document.getElementById("txtOrderDate1").value;
    var date2=document.getElementById("txtOrderDate2").value;
   
    action="Search";
   
    var Isliebiao = 1;

    var URLParams = "&Isliebiao="+escape(Isliebiao)+"&DrugName="+escape(DrugName)+"&Address="+escape(Address)+"&DrugStd="+escape(DrugStd)
    +"&waringname="+escape(waringname)+"&toend="+escape(toend)+"&isShowWaring="+escape(isShowWaring)+"&jixinglist="+escape(getlist)
    +"&DrugClass="+escape(DrugClass)+"&date1="+escape(date1)+"&date2="+escape(date2); 
     
   isShowWaring="";
 //----------------------------------------------------------------------------------------------//
    //设置检索条件
    document.getElementById("hidSearchCondition").value = URLParams;
    
      search="1";
      TurnToPage(currentPageIndex);
    }
function ClearInput()
{
    $("#pageDataList1 tbody").find("tr.newrow").remove();
    document.getElementById("txtCustomerId").value="";
    document.getElementById("txtCustomerName").value="";
    document.getElementById("ddlCustNature").value="";
     
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
 
    //新建药品档案
   function CreateCustInfo()
   { 
    parent.addTab(null, '30011019', '新建新增客户资质审核表', 'Pages/Office/FirstBusiness/Customer.aspx');
   }

function FillProvider(providerid,providerno,providername,taketype,taketypename,carrytype,carrytypename,paytype,paytypename)
{  
   
    document.getElementById("txtProviderID").value = providername;
    document.getElementById("txtHidProviderID").value = providerid;
    
    closeProviderdiv();
}

function incart()
{
    var getnum="";
    for(var i=2;i<22;i++)
    {
        var qty=Number(document.getElementById("qty"+i.toString()).value);
        if(qty!="" && qty!=0)
        {
            
            var orderqty=Number(document.getElementById("orderqty"+i.toString()).innerText);  
            if(orderqty=="")
            {
            orderqty=0;
            }    
            
            document.getElementById("orderqty"+i.toString()).innerText=orderqty+qty;
            
            getnum="1";
        }
        
    }
    if(getnum=="")
    {
       popMsgObj.ShowMsg("没有找到要保存的数据！");
       return;
    }
     
      var strInfo="";
      var strfitinfo = getDropValue().join("|");
      var isconfirm='1';
         $.ajax({
                type: "POST",
                url: "../../../Handler/Office/MedicineManager/DealOrderCart.ashx",
                data: strInfo + '&strfitinfo=' + escape(strfitinfo) + '&action=insert&isconfirm='+escape(isconfirm),
                dataType: 'json', //返回json格式数据
                cache: false,
                beforeSend: function() {
                    AddPop();
                },
                error: function() {
                 
                 showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
                success: function(data) {
                
                
                    if (data.sta > 0) {
                    
                       
                      }
                     hidePopup();
                    
                }
            });
            
   
}

//获取明细数据
function getDropValue() {
    var SendOrderFit_Item = new Array();  
     var j = 0;
    for (rowid = 2;rowid < 22; rowid++) {    
            j=j+1;                                                      
            var ProductNo = $("#cinvcode" + rowid).val();   //物品编码（对应物品表编码）
            var ProductCount = $("#qty" + rowid).val(); //订购数量 
            if(ProductCount!="")
            {             
            SendOrderFit_Item[j] = [[ProductNo], [ProductCount]];  
            document.getElementById("qty"+rowid.toString()).value="";
            }
    }
   
   
    return SendOrderFit_Item;
}



