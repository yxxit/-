$(document).ready(function(){
Fun_Search_UserInfo();
    });  
  
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var flag="";
     var ActionFlag=""
     var str="";
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
   
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {    
          document.getElementById("btnAll").checked=false;
           var serch=document.getElementById("hidSearchCondition").value;
           ActionFlag="Search"
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/CustProduct.ashx?str='+str,//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&ActionFlag="+escape(ActionFlag)+"&" + serch,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){
                     var tempisstop='';
                        if(item.ID != null && item.ID != "")
                        { 
                        if(item.IsStop=="1")
                        {
                          tempisstop="启用";
                        }
                        else
                        {
                        tempisstop="停用";
                        }
                       
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+" onclick=IfSelectAll('Checkbox1','btnAll') type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\""+item.CustName+"\"><a href='#' onclick=\"Show('"+item.CustID+"','"+item.CustName+"','"+item.ProdNo+"','"+item.ProductName+"','"+item.ID+"','"+item.ProdAlias+"','"+item.ProdPrice+"','"+item.IsStop+"');\">"+ item.CustName+"</a></td>"+
                        "<td height='22' align='center'>" + item.ProductName + "</td>"+
                         "<td height='22' align='center'>" + item.ProdAlias + "</td>"+
                         "<td height='22' align='center'>" + item.ProdPrice + "</td>"+
                        "<td height='22' align='center'>"+tempisstop+"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                       
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                   ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                   
                    document.getElementById('Text2').value=msg.totalCount;
//                   document.getElementById('Text2').value=msg.totalCount;
//                  document.all["Text2"].value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                  $("#ToPage").val(pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");}, 
           complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById('Text2').value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
    //table行颜色
function pageDataList1(o,a,b,c,d){
	var t=document.getElementById(o).getElementsByTagName("tr");
	for(var i=1;i<t.length;i++){
		t[i].style.backgroundColor=(t[i].sectionRowIndex%2==0)?a:b;
		t[i].onmouseover=function(){
			if(this.x!="1")this.style.backgroundColor=c;
		}
		t[i].onmouseout=function(){
			if(this.x!="1")this.style.backgroundColor=(this.sectionRowIndex%2==0)?a:b;
		}
	}
}
function Fun_Search_UserInfo(aa)
{
    var search="";
   var CustName=document.getElementById("SearchCustName").value;     //客户名称
   var ProdName=document.getElementById("SearchProName").value; //物品名称
   var ProdAlias=document.getElementById("SearchProAlias").value; //物品别名
   var ProdPrice=document.getElementById("SearchProPrice").value; //物品价格  
   var IsStop=document .getElementById("SearchIsStop").value;   
   search+="CustName="+escape(CustName);
   search+="&ProdName="+escape(ProdName);
   search+="&ProdAlias="+escape(ProdAlias);
   search+="&ProdPrice="+escape(ProdPrice);
   search+="&IsStop="+escape(IsStop);  
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    TurnToPage(1);
}
//删除
 function DelCodePubInfo()
   {
      
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
        var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        x=ck2;
        var str = ck2.substring(0,ck2.length-1);
        if(x.length-1<1)
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除列表信息至少要选择一项！");
        else
          {
         ActionFlag="Del";
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/SupplyChain/CustProduct.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  data: "ActionFlag="+escape(ActionFlag),//数据
                  cache:false,
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  //complete :function(){ //hidePopup();},
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        Fun_Search_UserInfo();
                        document.getElementById("btnAll").checked=false;
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
            
          }
        }  
        else   return   false;   
   
   }
function Ifshow(count)
    {
        if(count=="0")
        {
        document.getElementById('divpage').style.display = "none";
        document.getElementById('pagecount').style.display = "none";
        }
        else
        {
         document.getElementById('divpage').style.display = "block";
        document.getElementById('pagecount').style.display = "block";
        }
    }
    

    
//    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
      if(!IsZint(newPageCount))
       {
          popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
          return;
       }
       if(!IsZint(newPageIndex))
       {
          popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
          return;
       }
       if(newPageCount <=0 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","显示页数超出显示范围！");
            return false;
        }
        if(newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
            document.getElementById("btnAll").checked=false;
        }
    }
//    //排序
    function OrderBy(orderColum,orderTip)
    {
        var ordering = "a";
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

		function CloseDiv(){
	 closeRotoscopingDiv(false,'divBackShadow');
	}


function Show(CustID,CustName,ProdNo,ProdName,ID,ProdAlias,ProdPrice,IsStop)
{  
   flag="1";
   openRotoscopingDiv(false,'divBackShadow','BackShadowIframe');
   document.getElementById("div_Add").style.zIndex="2";
   document.getElementById("divBackShadow").style.zIndex="1";
   document.getElementById('div_Add').style.display='block';  
    if(typeof(CustID)!="undefined" )
    {
       document.getElementById("txtProdAlias").value=ProdAlias; //物品别名
    document.getElementById("txtProdPrice").value=ProdPrice; //物品价格  
    document.getElementById("HidProductID").value = ProdNo;
    document.getElementById("txtProductName").value = ProdName;
    $("#CustID").val(CustName); //客户名称
     $("#CustID").attr("title", CustID); //客户编号
    document.getElementById("rd_use").checked=true;
      document.getElementById("hf_ID").value=ID;
     
     
      
      if(IsStop=="1")
      {
      document.getElementById("rd_use").checked=true;
      }
      else if(IsStop=="0")
      {
         document.getElementById("rd_notuse").checked=true;
      }
      flag="2";
    }
}

//添加
function InsertCustProduct()
{
   var fieldText = "";
   var msgText = "";
   var isFlag = true;   
   if(flag=="1")
   {
      ActionFlag="Add"
   }
   else if(flag=="2")
   {
      ActionFlag="Edit";
   }
  var CustID = $("#CustID").attr("title");     //客户ID（对应客户表ID）
  var ProdNo=document.getElementById("HidProductID").value; //物品ID
  var ProdAlias=document.getElementById("txtProdAlias").value; //物品别名
  var ProdPrice=document.getElementById("txtProdPrice").value; //物品价格  
  if(CustID=="")
  {
     isFlag = false;
     fieldText = fieldText + "客户名称|";
     msgText = msgText +  "请输入客户名称|";
   }
   if(ProdNo=="")
  {
     isFlag = false;
     fieldText = fieldText + "物品名称|";
     msgText = msgText +  "请输入物品名称|";
   } 
   if(ProdAlias=="")
  {
     isFlag = false;
     fieldText = fieldText + "物品别名|";
     msgText = msgText +  "请输入物品别名|";
   } 
   if(ProdPrice==="")
  {
     isFlag = false;
     fieldText = fieldText + "销售价格|";
     msgText = msgText +  "请输入销售价格|";
   }   
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
        return;
    } 
  var ID= document.getElementById("hf_ID").value;  
  if(document.getElementById("rd_use").checked)
  {
   UseStatus="1";
  }
  else if(document.getElementById("rd_notuse").checked)
  {
     UseStatus="0";
  }
    $.ajax({ 
                  type: "POST",
                 url: "../../../Handler/Office/SupplyChain/CustProduct.ashx?str="+str,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  data: "ActionFlag="+escape(ActionFlag)+"&CustID="+escape(CustID)+"&ProdNo="+escape(ProdNo)+"&ProdAlias="+escape(ProdAlias)+"&ProdPrice="+escape(ProdPrice)+"&UseStatus="+escape(UseStatus)+"&ID="+ID,//数据
                  beforeSend:function()
                  { 
                     //AddPop();
                  }, 
                  error: function() 
                  {
                    popMsgObj.ShowMsg('请求发生错误');
                    
                  }, 
                  success:function(data) 
                  { 
                    if(data.sta==1) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        Fun_Search_UserInfo();
                        Hide();
                    }
                    else
                    {
                        popMsgObj.ShowMsg(data.info);
                    }
                  } 
               });
}
 function Hide()
    {
     CloseDiv();
     document.getElementById('div_Add').style.display='none';
     New();
    }
 function New()
    {
     document.getElementById("txtProdAlias").value=""; //物品别名
    document.getElementById("txtProdPrice").value=""; //物品价格  
    document.getElementById("HidProductID").value = "";
    document.getElementById("txtProductName").value = "";
    $("#CustID").val(""); //客户名称
    $("#CustID").attr("title", ""); //客户编号
    document.getElementById("rd_use").checked=true;   ;
    
   
}
function ClearQueryInfo()
{
    $(":text").each(function(){ 
    this.value=""; 
    }); 
     document.getElementById("UsedStatus").value='1';    
     
     $("#pageDataList1 tbody").find("tr.newrow").remove();
}
function OptionCheckAll()
{

  if(document.getElementById("btnAll").checked)
  {
     var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=true ;
        }
  }
  else if(!document.getElementById("btnAll").checked)
  {
    var ck = document.getElementsByName("Checkbox1");
        for( var i = 0; i < ck.length; i++ )
        {
        ck[i].checked=false ;
        }
  }
}


function clearinput(controlName,controlidCD)
{
    document.getElementById(controlName).value = "";
    document.getElementById(controlidCD).value = "";
}


//选择物品
function Fun_FillParent_Content(id,no,productname,price,unitid,unit,taxrate,taxprice,discount,standard)
{
    document.getElementById("HidProductID").value = no;
    document.getElementById("txtProductName").value = productname;
    document.getElementById("txtProdAlias").value=productname;; //物品别名
    document.getElementById("txtProdPrice").value=price; //物品价格  
  
}
//清除客户信息
function ClearSellModuCustdiv() {
    $("#CustID").val(''); //客户名称
    $("#CustID").attr("title", ''); //客户编号
    
    closeSellModuCustdiv(); //关闭客户选择控件
}

//选择客户后，为页面填充数据
function fnSelectCust(custID) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SellManager/SellModuleSelectCustUC.ashx",
        data: 'actionSellCust=info&id=' + custID,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "获取客户数据失败,请确认"); },
        success: function(data) {
            $("#CustID").val(data.CustName); //客户名称
            $("#CustID").attr("title", custID); //客户编号
            
        }
    });
    closeSellModuCustdiv(); //关闭客户选择控件
}
//打印功能
function fnPrint() {
    var serch=document.getElementById("hidSearchCondition").value;
    window.open('../../../Pages/PrinttingModel/SupplyChain/CustProduct.aspx?' +serch);
}