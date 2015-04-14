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
  
  function GetSearchCondition()
  {
        if(!fnCheck())
        {
            return;
        }
        
        document.getElementById("checkall").checked = false;
    
        ifdel="0";
        var SettleCode=$("#txtSettleCode").val();  //单据编号
        var BusTtype=$("#sel_cBusTtype").val();  //来源类型 
        var SourceBillNo=$("#txtSourceBillNo").val();  //来源单号
        var CustName=$("#txtCustName").val();       //客户名称
        var ProviderName=$("#txtProviderName").val();  //供应商名称
        var cPersonName=$("#txtcPersonName").val();  //调运单号
        var SearchInfo= "&SettleCode="+reescape(SettleCode)+"&BusTtype="+reescape(BusTtype)+"&SourceBillNo="+reescape(SourceBillNo)+
                 "&CustName="+reescape(CustName)+"&ProviderName="+reescape(ProviderName)+"&cPersonName="+reescape(cPersonName);//数据
                 
        $("#hidSearchCondition").val(SearchInfo);
  }
  
  
  
   function SearchSettleVouch(pageIndex)
   {
        
        currentPageIndex = pageIndex;
        action="SearchSettleVouchList";
        GetSearchCondition();
        TurnToPage(currentPageIndex)
   }
       
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           
           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url:'../../../Handler/JTHY/Expenses/SettleVouchInfo.ashx', //目标地址
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
                       if (item.id != null && item.id != "") {
                          
                              $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input id='Checkbox1'  Title='" + item.id + "' name='Checkbox1'  value=" + j + "  type='checkbox' onclick=IfSelectAll('Checkbox1','checkall') />" + "</td>" +
                            "<td height='22' align='center' style='display:none'>" + (j++) + "</td>" +  
                            "<td height='22' align='center' style='display:none'>" + item.id + "</td>" +  
                            "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('" + item.id +"','"+item.cBusTtype+"')><span title=\"" + item.SettleCode + "\">" + item.SettleCode + "</a></td>" +          
                            "<td height='22' align='center' >" + item.SourceBillNo + "</a></td>" +
                             "<td height='22' align='center' >" + item.BusTtype + "</a></td>" +                         
                            "<td height='22' align='center'class='P_ss'>" + Number(item.P_SettleTotalPrice).toFixed(2) + "</a></td>" +
                            "<td height='22' align='center'class='P_mm'>" + Number(item.P_Money).toFixed(2) + "</a></td>" +
                            "<td height='22' align='center'class='S_ss'>" + Number(item.S_SettelTotalPrice).toFixed(2) + "</a></td>" +
                            "<td height='22' align='center'class='S_mm'>" + Number(item.S_Money).toFixed(2) + "</a></td>" +
                             "<td height='22' align='center'>" + item.EmployeeName + "</a></td>" +
                                                             
                            "<td height='22' align='center'>" + item.BillStatus+ "</a></td>").appendTo($("#pageDataList1 tbody"));
                         
                         
                        
                       }
                   });
                   changeStyle();
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

//改变订单类型时
function ChangeBill()
{
    var selectedValue=$("#sel_cBusTtype").val();

    
    if(selectedValue=="1")  //直销
    {
        
        $("#txtProviderName").attr("disabled","disabled");
        $("#txtCustName").attr("disabled","");
        $("#txtProviderName").val("");
        
    }
    else if(selectedValue=="2") //采购到货
    {
        
        $("#txtProviderName").attr("disabled","");
        $("#txtCustName").attr("disabled","disabled");
        $("#txtCustName").val("");
         
    }
    else if(selectedValue=="3")  //采购直销
    {
        $("#txtProviderName").attr("disabled","");
        $("#txtCustName").attr("disabled","");
    }
}

function changeStyle()
{
    var selectedValue=$("#sel_cBusTtype").val();

    
    if(selectedValue=="1")  //直销
    {
        $("#P_Settle").css("display","none");
        $("#P_Money").css("display","none");
        $("#S_Settle").css("display","");
        $("#S_Money").css("display","");

        $("#check").css("width","10%");
        $("#BillNo").css("width","13%");
        $("#sourceNo").css("width","13%");
        $("#SourceType").css("width","13%");
        $("#S_Settle").css("width","13%");
        $("#S_Money").css("width","13%");
        $("#PersonMan").css("width","13%");
        $("#IsSure").css("width","12%");
        
        $(".P_ss").css("display","none");
        $(".P_mm").css("display","none");
        $(".S_ss").css("display","");
        $(".S_mm").css("display","");
    }
    else if(selectedValue=="2") //采购到货
    {
        $("#P_Settle").css("display","");
        $("#P_Money").css("display","");
        $("#S_Settle").css("display","none");
        $("#S_Money").css("display","none");

         $("#check").css("width","10%");
        $("#BillNo").css("width","13%");
        $("#sourceNo").css("width","13%");
        $("#SourceType").css("width","13%");
        $("#P_Settle").css("width","13%");
        $("#P_Money").css("width","13%");
        
        $("#PersonMan").css("width","13%");
        $("#IsSure").css("width","12%");
         $(".P_ss").css("display","");
        $(".P_mm").css("display","");
        $(".S_ss").css("display","none");
        $(".S_mm").css("display","none");
    }
    else if(selectedValue=="3")  //采购直销
    {
       $("#P_Settle").css("display","");
        $("#P_Money").css("display","");
        $("#S_Settle").css("display","");
        $("#S_Money").css("display","");
        
        $("#check").css("width","6%");
        $("#BillNo").css("width","12%");
        $("#sourceNo").css("width","12%");
        $("#SourceType").css("width","8%");
        $("#P_Settle").css("width","12%");
        $("#P_Money").css("width","11%");
        $("#S_Settle").css("width","12%");
        $("#S_Money").css("width","11%");

        $("#PersonMan").css("width","8%");
        $("#IsSure").css("width","8%");
        
        $(".P_ss").css("display","");
        $(".P_mm").css("display","");
        $(".S_ss").css("display","");
        $(".S_mm").css("display","");
        
    }
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


//跳转到结算单
function GetLinkParam0(id,cBusTtype) {

    parent.addTab(null,'8883','结算单','Pages/JTHY/Expenses/SettleVouch_ADD.aspx?ModuleID=8883&intMasterID='+escape(id)+'&cBusTtype='+escape(cBusTtype)+'' );
    
}

//新建结算单
function CreateNew()
{
    parent.addTab(null,'8883','结算单','Pages/JTHY/Expenses/SettleVouch_ADD.aspx?ModuleID=8883');
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
 


//删除
function  Fun_DeleteSettleVouch()
{ 
    var c=window.confirm("确认执行删除操作吗？")
    if(c==true)
    {
        var ck = document.getElementsByName("Checkbox1");
        var table=document.getElementById("pageDataList1"); 
        var URLParams = "";  
        var Action = "DeleteSettleVouch";
        URLParams+="action="+Action;
        var index = 0;
        var SettleVouchId="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {

                SettleVouchId += table.rows[ck[i].value].cells[2].innerText+',';
                index++;
               
            }       
        }
        
        SettleVouchId = SettleVouchId.substring(0,SettleVouchId.length-1);
        if(index == 0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请先选择数据后再删除！");
            return;
        }
        URLParams+="&Length="+index+"";
        
        $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url: "../../../Handler/JTHY/Expenses/DealSettleVouch.ashx",//目标地址
           data:URLParams+"&allSettleVouchId="+escape(SettleVouchId)+'',
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
                        SearchSettleVouch(1);  //刷新页面
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