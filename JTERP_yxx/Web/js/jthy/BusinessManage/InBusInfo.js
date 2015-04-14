    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var currentpageCount = 10;
    var action = "";//操作
    var orderBy = "id";//排序字段
    var Isliebiao ;
    
    var ifdel="0";//是否删除
    var issearch="";    
    $(document).ready(function()
    {
    requestobj = GetRequest();  
    var Isliebiao =requestobj['Isliebiao'];
    var PageIndex = requestobj['PageIndex'];
    var PageCount = requestobj['PageCount'];
 
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
  
  // 获取检索条件
  function GetSearchCondition()
  {
        if(!fnCheck())
        {
            return;
        }        
        document.getElementById("checkall").checked = false;    
        ifdel="0";
        var ArriveNo=$("#txtArriveNo").val();  //单据编号
        var ProviderName=$("#txtProviderName").val();  //供货方 
        var BeginT=$("#txtBeginT").val();  //发货开始时间
        var EndT=$("#txtEndT").val();       //发货结束时间
        var PurchaserName=$("#txtPurchaserName").val();  //采购员
        var TranSNo=$("#txtTranSNo").val();  //调运单号
        var State=$("#txtState").val();  //运送状态
        var PurContractNo=$("#txtPurContractNo").val(); //
        var TranSNo=$("#txtTranSNo").val();
        var DeptID=$("#hdDeptID").val();       
        
        var SearchInfo= "&ArriveNo="+reescape(ArriveNo)+"&ProviderName="+reescape(ProviderName)+"&BeginT="+reescape(BeginT)+
                 "&EndT="+reescape(EndT)+"&PurchaserName="+reescape(PurchaserName)+"&TranSNo="+reescape(TranSNo)+"&State="+reescape(State)+
                 "&PurContractNo="+reescape(PurContractNo)+"&DeptID="+reescape(DeptID);//数据                 
        $("#hidSearchCondition").val(SearchInfo);
       // return;
  }
  
   function SearchInBus(pageIndex)
   {   
        currentPageIndex = pageIndex;
        action="SearchInBusList";
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
               url: '../../../Handler/JTHY/BusinessManage/InBusInfo.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageCount=" + currentpageCount + "&Action=" + action + "&orderby=" + orderBy +
                    document.getElementById("hidSearchCondition").value,
               beforeSend: function() { AddPop(); $("#pageDataList1_PagerList").hide(); }, //发送数据之前

               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataList1 tbody").find("tr.newrow").remove();
                   var j = 1;
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "") {  
                            if(item.productcount=="")item.productcount=0;
                            if(item.totalmoney=="")item.totalmoney=0;                                                    
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                            "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +  
                            "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +  
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('90021','" + item.id +"')><span title=\"" + item.arriveno + "\">" + item.arriveno + "</a></td>" +          
                            "<td height='22' align='center'>" + item.provider + "</td>" +
                            "<td height='22' align='center'>" + item.productname + "</td>" +                         
                            "<td height='22' align='center'>" + QfwFormat(parseFloat(item.productcount),2) + "</td>" +
                            "<td height='22' align='center'>" + QfwFormat(parseFloat(item.TaxPrice), 2) + "</td>" +
                            "<td height='22' align='center'>" + QfwFormat(parseFloat(item.totalmoney),2) + "</td>" +
                            "<td height='22' align='center'>" + item.startstation + "</td>" +
                            "<td height='22' align='center'>" + item.endstation + "</td>" +
                            "<td height='22' align='center'>" + item.transstate + "</td>" +    
                            "<td height='22' align='center'>" + item.billstatus + "</td>" + 
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('80021','" + item.CtrId +"')>" + item.Contractid + "</a></td>" + 
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('9992','" + item.DiaoyunId +"')>" + item.DiaoyunNO + "</a></td>" +                                                          
                            "<td height='22' align='center'>" + item.DeptName+ "</a></td>").appendTo($("#pageDataList1 tbody")); 
                            
                             if((parseFloat(item.productcount)!=0)&&(item.productcount!=""))
                             {
                               totalProductcount+=parseFloat(item.productcount);
                             }
                             if((parseFloat(item.totalmoney)!=0)&&(item.totalmoney!=""))
                             {
                               totalMoney+=parseFloat(item.totalmoney);
                             }
                       }
                   });                   
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>本页合计</td>" +
                    "<td colspan='3'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(totalProductcount, 2) + "</td>" +
                     "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'>" +QfwFormat(totalMoney,2)+"</td>"+                 
                    "<td colspan='7'></td>").appendTo($("#pageDataList1 tbody"));
                    
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>总计</td>" +
                    "<td colspan='3'></td>" +
                    "<td height='22' align='center'>" + QfwFormat(msg.ttlCount, 2) + "</td>" +
                     "<td height='22' align='center'></td>" +
                    "<td height='22' align='center'>" +QfwFormat(msg.ttlFee,2) +"</td>"+
                    "<td height='22' colspan='7'></td>").appendTo($("#pageDataList1 tbody"));  
                    
                   //页码
                   ShowPageBar("pageDataList1_PagerList", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1Mark",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: currentpageCount, currentPageIndex: pageIndex, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    ); document.getElementById("checkall").checked = false;
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

//跳转到采购到货单号
function GetLinkParam0(typeid,id) {
    if(typeid=="90021")
       parent.addTab(null,'90021','采购到货单','Pages/JTHY/BusinessManage/InBus_ADD.aspx?ModuleID=90021&intMasterID='+escape(id));  
    if(typeid=="80021")
       window.parent.addTab(null,'80021','采购合同','Pages/JTHY/ContractManage/PurContract_Add.aspx?ModuleID=80021&intMasterID='+escape(id));
    if(typeid=="9992")
       window.parent.addTab(null, '9992', '调运单管理', 'Pages/JTHY/TransPortManage/TranSport_ADD.aspx?ModuleID=9992&intMasterID=' + escape(id) );
       
}
function CreateSaleOrder() {
    //Pages/JTHY/BusinessManage/InBus_ADD.aspx?ModuleID=90021
    //window.parent.addTab(null, '90031', '登记到货', 'Pages/JTHY/BusinessManage/OutBus_ADD.aspx?typeflag=0&ModuleID=90031');
    window.parent.addTab(null, '90021', '登记到货', 'Pages/JTHY/BusinessManage/InBus_ADD.aspx?typeflag=0&ModuleID=90021');
    
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

function  Fun_DeleteInBusInfo()
{ 
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
        var ck = document.getElementsByName("Checkbox1");
        var table=document.getElementById("pageDataList1"); 
        var URLParams = "";  
        var Action = "DeleteInBusInfo";
        URLParams+="action="+Action;
        var index = 0;
        var InBusId="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
                InBusId += table.rows[ck[i].value].cells[2].innerText+',';
                index++;               
            }       
        }
        
        InBusId = InBusId.substring(0,InBusId.length-1);
         
        if(index == 0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
            return;
        }
        URLParams+="&Length="+index+"";
        
        $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  "../../../Handler/JTHY/BusinessManage/DealInBus_ADD.ashx",//目标地址
           data:URLParams+"&allInBusID="+escape(InBusId)+'',
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
                        SearchInBus(1);  //刷新页面
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
