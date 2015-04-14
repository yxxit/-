function showInfo(msg)
{
    //document.getElementById("infoTip").innerHTML = msg;
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
}
function ShowInfo(msg)
{
    //document.getElementById("infoTip").innerHTML = msg;
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
}

function DelMessage()
{
       
    
        var ck = document.getElementsByName("Checkbox1");
        var ids = "";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
                if( ids != "")
                    ids += ",";
                ids += ck[i].value;
            }
        }        
     
        if(ids == "")
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
            return;
        }
        
         if(!confirm("确认删除吗"))
        {
            return;
        }         
                
        var action="DelItem";
        //删除
         $.ajax({ 
              type: "POST",
              url: "../../../Handler/Personal/MessageBox/Notice.ashx",
              data:"action="+action+"&idList="+ids,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() 
            {
               showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            }, 
            success:function(result) 
            { 
                if(result.result) 
                { 
                     TurnToPage(1);
                     ifshow="1";
                     showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                } 
            } 
           });
      
   
}


var sessionSection="";
$(document).ready(function(){
     TurnToPage(currentPageIndex);
                
//      var url = document.location.href.toLowerCase(); 
//      if(url.indexOf("/(s(") != -1)
//      {
//        var sidx = url.indexOf("/(s(")+1;
//        var eidx = url.indexOf("))")+2;
//        //alert(sidx+":"+eidx);
//        url = document.location.href; 
//        
//        sessionSection = url.substring(sidx,eidx);        
//        sessionSection += "/";
//      }
      
    });    
    
    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var orderBy = "";//排序字段
    var ifshow="0";
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)
    {
           currentPageIndex = pageIndex;
           //flowInstanceID,flowName,billTypeNo,slFlowStatus,BillType,createDate
           var condition = "1=1";
          
           var txtTitle=document.getElementById("txtTitle").value;
          
           var createDate1=document.getElementById("createDate1").value;
           var createDate2=document.getElementById("createDate2").value;
         
           
        
            if(txtTitle + "" != "")
           {
            txtTitle = txtTitle.replace(/\'/g, "''");
                txtTitle = txtTitle.replace(/\%/g, "[%]");
            condition += " AND a.newsTitle like '%"+txtTitle+"%'";
           }
         
            if(createDate1 + "" != "")
           {
            condition += " AND a.CreateDate>='"+createDate1+"'";
           }
           if(createDate2 + "" != "")
           {
            condition += " AND a.CreateDate<='"+createDate2+"'";
           }
                    
           //alert(condition);
           var action="LoadData";
        
           var fields="a.[ID],a.Creator,a.CreateDate,a.Comfirmor,b.EmployeeName,a.ComfirmDate,a.NewsTitle,a.NewsContent,a.Status,a.IsShow";
           fields = UrlEncode(fields);
                       
          var prams = "Fields="+fields;
           prams += "&condition="+UrlEncode(condition);
           
           if(orderBy + "" != "")
           {
            prams += "&OrderExp="+UrlEncode(orderBy);
           }
           
           $.ajax({
           type: "POST",//用POST方式传输
           //dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Personal/MessageBox/Notice.ashx',//目标地址
           cache:false,
           data: "action="+action+"&pageIndex="+pageIndex+"&pageSize="+pageCount+"&"+prams,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(result)
           {
          //alert(result);
                      var result;eval("result="+result);
         
                     //var  msg = result.data.list;
         
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(result.data.list,function(i,item){
                        if(item.ID != null && item.ID != "")
                        {
                        
                        if(item.Comfirmor == "0")
                        {
                            item.Comfirmor = "";
                            item.ComfirmDate = "";
                        }
                                                
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                        "<td height='22' align='center'>"+ item.CreateDate +"</td>"+
                         "<td height='22' align='center'><a href=\"#\" onclick=\"showEditPanel("+item.ID+")\" title=\"点击查看详细\">"+item.NewsTitle+"</a></td>"+ 
                        "<td height='22' align='center'>"+item.NewsContent+"</td>"+
                       
                        "<td height='22' align='center'>"+(item.Status == "0"?"否":"是")+"</td>"+
                        "<td height='22' align='center'>"+(item.IsShow == "0"?"否":"是")+"</td>"+
                        "<td height='22' align='center'>"+ item.ComfirmDate +"</td>"+
                        "<td height='22' align='center'>"+item.EmployeeName2 +"</td>").appendTo($("#pageDataList1 tbody"));
                        }
                   });
                    //页码
                   ShowPageBar("pageDataList1_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:result.data.count,showPageNumber:3,pageCount:pageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"}//[attr]
                    );
                totalRecord = result.data.count;
                  $("#pageDataList1_Total").html(totalRecord);//记录总条数
                  document.form1.elements["Text2"].value=totalRecord;
                  $("#ShowPageCount").val(pageCount);                 
                  $("#ToPage").val(pageIndex);
                  
                   ShowTotalPage(totalRecord,pageCount,pageIndex);
                  },
           error: function() {showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
},
           complete:function(){if(ifshow=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.form1.elements["Text2"].value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

    function SearchEquipData(aa)
    {
      search="1";
      ifshow="0";
      TurnToPage(1);
    }
    function ClearInput()
    {
        $(":text").each(function(){ 
        this.value=""; 
        }); 
    }
function Ifshow(count)
    {
        if(count=="0")
        {
           document.getElementById("divpage").style.display = "none";
            document.getElementById("PageCount").style.display = "none";
        }
        else
        {
            document.getElementById("divpage").style.display = "block";
            document.getElementById("PageCount").style.display = "block";
        }
    }
    
    function SelectDept(retval,action)
    {
        window.location.href='Equipment_Edit.aspx?retval='+escape(retval)+'&action='+action;
    }
    
    //改变每页记录数及跳至页数
    function ChangePageCountIndex(newPageCount,newPageIndex)
    {
        if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数超出查询范围！");
        }
        else
        {
             ifshow="0";
            this.pageCount=parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    var ordering = "asc";
    var lastInfoTip = null;
    function OrderBy(obj,orderColum,orderTip)
    {
        ifshow="0";
        if(lastInfoTip != null)
        {
            lastInfoTip.innerHTML = "";
        }
        
        lastInfoTip = obj.getElementsByTagName("SPAN")[0];
        //var orderTipDOM = $("#"+orderTip);
       if( ordering == "asc")
        {
            lastInfoTip.innerHTML = "&nbsp;<font color=blue  size=3>↓</font>";
            ordering = "desc";            
        }else{
             lastInfoTip.innerHTML = "&nbsp;<font color=blue size=3>↑</font>";
            ordering = "asc";   
        }
        orderBy = orderColum+" "+ordering;
       
        TurnToPage(1);
    }
  //设备复制
   function CopyEquip()
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
        x = ck2.split(',');
        if(x.length-1<=0)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一项再进行设备复制！");
            return;
        }
        if(x.length-1>1)
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","复制设备只能选一项！");
        else
           SelectDept(ck2.replace(",",""),"getbyequipno");
   }
    //设备申请领用
   function SendApply()
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
        x = ck2.split(',');
        if(x.length-1<=0)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一项在领用设备！");
            return;
        }
        if(x.length-1>1)
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","申请领用只能选一项！");
        else
          {//首先判断此设备是否空闲
            var action="sendapply";
            $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/AdminManager/EquipmentInfo.ashx",
                  data:"action="+action+"&equipno="+escape(ck2),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() 
                {
                   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==0) 
                    { 
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",data.data);//无空闲设备
                    }
                    else//有空闲设备
                    {
                        TurnToReceive(ck2);
                    } 
                } 
               });
          }
   }
       //设备申请维修
   function SendRepair()
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
        x = ck2.split(',');
        if(x.length-1<=0)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一项在维修设备！");
            return;
        }
        if(x.length-1>1)
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","申请维修只能选一项！");
        else
           TurnToRepair(ck2);
   }
  //设备报废申请
   function SendUseless()
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
        x = ck2.split(',');
        if(x.length-1<=0)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择一项再进行报废申请操作！");
            return;
        }
        if(x.length-1>1)
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","报废申请只能选一项！");
        else
           TurnToUseless(ck2);
   }
//跳转到领用页面
function TurnToReceive(retval)
    {
        var action="getbyequipno";
            window.location.href='Equipment_Receive.aspx?retval='+escape(retval)+'&action='+action;
    }  
//跳转到维修页面
function TurnToRepair(retval)
    {
        var action="getbyequipno";
            window.location.href='Equipment_Repair.aspx?retval='+escape(retval)+'&action='+action;
    }
//跳转到报废页面
function TurnToUseless(retval)
    {
        var action="getbyequipno";
            window.location.href='Equipment_Useless.aspx?retval='+escape(retval)+'&action='+action;
    }  
  

///删除设备信息
function DelEquipmentInfo()
{
    if(confirm("同时会删除配件信息，删除后不可恢复，确认删除吗！"))
    {
        var action="del";
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value+',';
            }
        }
        var equipmentids = ck2.substring(0,ck2.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
           showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/AdminManager/EquipmentInfo.ashx",
                  data:"action="+action+"&equipmentids="+escape(equipmentids),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() 
                {
                   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
                }, 
                success:function(data) 
                { 
                    if(data.sta==1) 
                    { 
                         TurnToPage(1);
                         ifshow="1";
                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                    }
                    else
                    {
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    } 
                } 
               });
    } 
    }
    else return false;

}