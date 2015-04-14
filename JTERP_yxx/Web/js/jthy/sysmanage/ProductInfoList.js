$(document).ready(function()
{
    var url = location.search;
    var theRequest = new Object();
    var type="";
    var data="";
    var day="";
    var modifyDay="";
    //alert(url);
    if (url.indexOf("?") != -1) 
    {
        var str = url.substr(1);
        if(str!="ModuleID=555253")
        {
             var strs=str.split("&");
             type=strs[0].split("=")[1];
             data=strs[1].split("=")[1];             
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
            if(type=="MedFileDate")
            {
               document.getElementById("MedFileDateBegin").value=day;
               document.getElementById("MedFileDateEnd").value=modifyDay;
            }
         Fun_Search_ProductInfo(1);
        }
    }

}); 

    //去左空格;
function ltrim(s){
return s.replace( /^\s*/, "");
}
//去右空格;
function rtrim(s){
return s.replace( /\s*$/, "");
}
   //左右空格;
function trim(s){
return rtrim(ltrim(s));
} 
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    var flag="";
     var str="";
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    //jQuery-ajax获取JSON数据
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
             //扩展属性
//           var EFIndex=document.getElementById("GetBillExAttrControl1_SelExtValue").value;//扩展属性select值
//           var EFDesc=document.getElementById("GetBillExAttrControl1_TxtExtValue").value;//扩展属性文本框值
           var serch=document.getElementById("hidSearchCondition").value;
           currentPageIndex = pageIndex;
           action="Load";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/ProductInfo.ashx?action='+action,//目标地址
           cache:false,
               data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&" + serch,//数据
               beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
         
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                        $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(msg.data,function(i,item){                    
                             var temp =item.ID+"|"+item.CheckStatus+"|"+item.ProductName;
                           
                             var otherwhere='';
                              if(msg.id){
                              otherwhere= "<td height='22' align='center'>"+item['ExtField'+EFIndex]+"</td>";}
                        if(item.ID != null && item.ID != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1'  name='Checkbox1'  value="+temp+" onclick=IfSelectAll('Checkbox1','btnAll')  type='checkbox'/>"+"</td>"+
                        "<td height='22' align='center' title=\"" + item.ProdNo + "\">  <a href='#' onclick=fnproductinfo('"+ item.ID + "')>" + fnjiequ(item.ProdNo, 10) + "</a></td>" +
                        "<td height='22' align='center' title=\""+item.ProductName+"\">"+fnjiequ(item.ProductName,10)+"</td>"+
                        //"<td height='22' align='center' title=\""+item.TypeName+"\">"+fnjiequ(item.TypeName,10)+"</td>"+
                        "<td height='22' align='center' title=\""+item.UnitName+"\">"+fnjiequ(item.UnitName,10)+"</td>"+
                        //"<td height='22' align='center'>"+item.Specification+"</td>"+
                        //"<td height='22' align='center'>"+item.ColorName+"</td>"+
                        "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+item.CreateDate.substring(0,10)+"</td>"+
//                        "<td height='22' align='center'>"+item.CheckStatus+"</td>"+
//                         "<td height='22' align='center'>"+item.UsedStatus+"</td>"+
                      otherwhere).appendTo($("#pageDataList1 tbody"));
                   });
                      ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                       "<%= Request.Url.AbsolutePath %>",//[url]
                        {style:pagerStyle,mark:"pageDataList1Mark",
                        totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                        onclick:"TurnToPage({pageindex});return false;"}//[attr]
                        );
                      totalRecord = msg.totalCount;
                      document.getElementById('Text2').value=msg.totalCount;
                      //document.getElementById("Text2").value=msg.totalCount;
                      $("#Text2").val(msg.totalCount);
                      $("#ShowPageCount").val(pageCount);
                      ShowTotalPage(msg.totalCount,pageCount,pageIndex);
                      $("#ToPage").val(pageIndex);
                       ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                      $("#txt_BarCode").val("");//清空条码
                      },
               error: function() {}, 
               complete:function(){hidePopup();$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "ProductInfoAdd.aspx?orderby=" + orderBy + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag+"&ModuleID="+ModuleID;
    //返回链接的字符串
    return linkParam;
}
function fnproductinfo(id) {
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null, ModuleID, '物品档案', 'Pages/JTHY/SysManage/ProductInfoAdd.aspx?ModuleID=' + ModuleID + '&intOtherCorpInfoID=' + escape(id) + "&" + searchCondition + "&Flag=" + flag);
}

function selall()
{
    var j=0;
    var m=parseInt(j);
   var ck2 = document.getElementsByName("Checkbox1");
    for( var i = 0; i < ck2.length; i++ )
    {
    if(ck2[i].checked)
    {
     j++
    }
    }
    if(j==ck2.length)
    {
    document.getElementById("btnAll").checked=true;
    }
    else
    {
     document.getElementById("btnAll").checked=false;
    }
}

function Fun_Search_ProductInfo(currPage)
{        
           var fieldText = "";
           var msgText = "";
           var isFlag = true;
           var search = "";
//           var txt_TypeID=document.getElementById("txt_TypeID").value;
//           var TypeID=document.getElementById("txt_ID").value;
           var ProdNo=document.getElementById("txt_ProdNo").value;
//           var PYShort=document.getElementById("txt_PYShort").value;
           var ProductName=document.getElementById("txt_ProductName").value;
//           var Color=document.getElementById("selCorlor").value;
           
//           var BarCode=document.getElementById("txt_BarCode").value;
           
//            if(document.getElementById("txt_BarCode").value!="")
//            {
//              document.getElementById("HiddenBarCode").value=document.getElementById("txt_BarCode").value;
//            }
//            else
//            {
//                $("#HiddenBarCode").val("");
//            }
//           var BarCode = document.getElementById("HiddenBarCode").value;//商品条码
//           var Specification=document.getElementById("txt_Specification").value;
           var UsedStatus = document.getElementById("UsedStatus").value;

        
//         if(document.getElementById("txt_FileNo")!=null)
//           var FileNo=document.getElementById("txt_FileNo").value;//批准文号
//          else
//           var FileNo="";
//            
//          if(document.getElementById("Qualitystandard")!=null)   
//           var Qualitystandard=document.getElementById("Qualitystandard").value;//质量标准
//        else
//           var Qualitystandard="";
//           
//          if(document.getElementById("MedCheckNo")!=null) 
//           var MedCheckNo=document.getElementById("MedCheckNo").value;//药品检验报告编号
//        else
//          var MedCheckNo="";
//             
//           if(document.getElementById("ValidityBegin")!=null)
//           var ValidityBegin=document.getElementById("ValidityBegin").value;
//         else
//         var ValidityBegin="";
           
//         if(document.getElementById("ValidityEnd")!=null)
//         {
//           var ValidityEnd=document.getElementById("ValidityEnd").value;
//           
//            if (ValidityBegin != "" && ValidityEnd != "") {
//                if (ValidityBegin > ValidityEnd) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//           }
//            else
//           var ValidityEnd="";
//           
//         if((document.getElementById("MedFileDateBegin")!=null)&&(document.getElementById("MedFileDateEnd")!=null))
//         {
//           var MedFileDateBegin=document.getElementById("MedFileDateBegin").value;
//           var MedFileDateEnd=document.getElementById("MedFileDateEnd").value;
//           
//            if (MedFileDateBegin != "" && MedFileDateEnd != "") {
//                if (MedFileDateBegin > MedFileDateEnd) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//           }
//           else
//          {
//          var MedFileDateBegin="";
//          var MedFileDateEnd="";
//         }
           
//        if((document.getElementById("MedFileDateBegin")!=null)&&(document.getElementById("MedFileDateEnd")!=null))
//         {
//           var MedCheckDateBegin=document.getElementById("MedCheckDateBegin").value;
//           var MedCheckDateEnd=document.getElementById("MedCheckDateEnd").value;

//            if (MedCheckDateBegin != "" && MedCheckDateEnd != "") {
//                if (MedCheckDateBegin > MedCheckDateEnd) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//           }
//          else
//            {
//          var MedCheckDateBegin="";
//           var MedCheckDateEnd="";
//            }
//            var FileNo=document.getElementById("txt_FileNo").value;
//            var Qualitystandard=document.getElementById("Qualitystandard").value;
//            var MedCheckNo=document.getElementById("MedCheckNo").value;
           //---------------------------------------------------------------//
//           if (!CheckSpecification(Specification)) {
//               isFlag = false;
//               fieldText = "规格型号|";
//               msgText = msgText + "不能含有特殊字符|";
//           }

//           var tmpSpecification = '';

//           /*处理规格型号*/
//           for (var i = 0; i < Specification.length; i++) {
//               if (Specification.charAt(i) == '+') {
//                   tmpSpecification = tmpSpecification + '＋';
//               }
//               else {
//                   tmpSpecification = tmpSpecification + Specification.charAt(i);
//               }
//           }

//           Specification = tmpSpecification.replace('×', '&#174');
          //search+="TypeID="+escape(TypeID);
          search+="ProdNo="+escape(ProdNo); 
          //search+="&PYShort="+escape(PYShort); 
          search+="&ProductName="+escape(ProductName); 
          //search+="&BarCode="+escape(BarCode); 
          //search+="&Specification="+escape(Specification); 
          search+="&UsedStatus="+escape(UsedStatus); 
//          search+="&CheckStatus="+escape(CheckStatus); 
//          search +="&EFIndex="+escape(EFIndex);
//          search +="&Color="+escape(Color);
//          search+="&EFDesc="+escape(EFDesc);
//          search+="&txt_TypeID="+escape(txt_TypeID)
          
          
          var RetVal=CheckSpecialWords();
//    if(EFIndex!=""&&EFIndex!=null&&(EFDesc!=''))
//    {
//        $("#ThShow").show();
//        $("#DivOtherC").html(document.getElementById("GetBillExAttrControl1_SelExtValue").options[document.getElementById("GetBillExAttrControl1_SelExtValue").selectedIndex].text);
//    }
//    else
//    {
//        $("#ThShow").hide();
//    }
    if(RetVal!="")
    {
            isFlag = false;
            fieldText = fieldText + RetVal+"|";
   		    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
      if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
        return ;
    }
    //设置检索条件
    document.getElementById("hidSearchCondition").value = search;
    if (currPage == null || typeof(currPage) == "undefined")
    {
        TurnToPage(1);
    }
    else
    {
        TurnToPage(parseInt(currPage));
    }
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
            document.getElementById('divpage').style.display = "block";
            document.getElementById('pagecount').style.display = "block";
        }
    }
    
    //改变每页记录数及跳至页数
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
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
            return false;
        }
        else
        {
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderBy(orderColum,orderTip)
    {
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


function Fun_Delete_ProductInfo()
{  
        var c=window.confirm("确认执行删除操作吗？")
        if(c==true)
        {
        var ck = document.getElementsByName("Checkbox1");
        var x=Array(); 
        var ck2 = "";
        var str="";
        Action="Del";
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
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除物品信息至少要选择一项！");
       else
       {
         var UrlParam ='';
       var UrlParam = str+"\
                     &Action="+Action
        $.ajax({ 
                type: "POST",
                url:  '../../../Handler/Office/SupplyChain/ProductInfoAdd.ashx?str='+UrlParam,//目标地址
              dataType:'json',//返回json格式数据
              cache:false,
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
                popMsgObj.ShowMsg(data.info);
                //popMsgObj.ShowMsg("kkk");
                
                document.getElementById("btnAll").checked=false;
                var ck2 = document.getElementsByName("Checkbox1");
                for( var i = 0; i < ck2.length; i++ )
                {
                ck2[i].checked=false ;
                }
                if(data.sta>0)
                {
                    Fun_Search_ProductInfo();
                }
              } 
           });
           }
    }
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
 function Show()
{
    //    window.location.href = GetLinkParam();
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null, ModuleID, '物品档案', 'Pages/JTHY/SysManage/ProductInfoAdd.aspx?ModuleID=555252');//Pages/Office/SupplyChain/ProductInfoAdd.aspx?ModuleID=' + ModuleID + "&" + searchCondition + "&Flag=" + flag);
}

 //document.onkeydown = ScanBarCodeSearch;


    function IsBrowser()
    {
        var isBrowser ;
        if(window.ActiveXObject){
        isBrowser = "IE";
        }else if(window.XMLHttpRequest){
        isBrowser = "FireFox";
        }
        return isBrowser;
    }


    /*验证规格只允许+和*特殊字符可以输入*/
    function CheckSpecification(str) {
        var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
        for (var i = 0; i < SpWord.length; i++) {
            for (var j = 0; j < str.length; j++) {
                if (SpWord[i] == str.charAt(j)) {
                    return false;
                    break;
                }
            }
        }
        return true;
    }  