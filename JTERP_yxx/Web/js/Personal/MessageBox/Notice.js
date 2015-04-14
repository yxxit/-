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


    
        
        function ConfirmBat()
        {
       
var nodeList=document.getElementsByName("itd"); 
var f="";  
for( var i = 0;i<nodeList.length;i++){   

   f+=nodeList[i].value; 
}        var ck = document.getElementsByName("Checkbox1");
                var ids = "";    
                for( var i = 0; i < ck.length; i++ )
                {
                    if ( ck[i].checked )
                    {
                         if(nodeList[i].value=="已审核"){
                  alert("此公告已经审核");
                 
                  
                return;
              }
                    
                        if( ids != "")
                            ids += ",";
                        ids += ck[i].value;
                    }
                }        
             
                if(ids == "")
                {
                   ShowInfo("请至少选择一项");
                    return;
                }        
            //拼写请求URL参数
            
            
            
             
            var postParams = "Action=ConfirmBat&&id="+ids;
            $.ajax({ 
                type: "POST",
                url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
                dataType:'string',
                data: '',
                cache:false,
                success:function(data) 
                {                      
                    var result = null;
                    eval("result = "+data);
                    
                     
                    if(result.result)                    
                    {                   
                         alert("审核成功");
                        TurnToPage(currentPageIndex);       
                    }
                    else
                    {
                        ShowInfo(result.data);
                    }    
                },
                error:function(data)
                {
                     ShowInfo(data.responseText);
                }
            });
        }        
        
        
        
       function EditItem()
        {    
            var chks = document.getElementsByName("Checkbox1");
            var ic = 0;
            for(var j=0;j<chks.length;j++)
            {  
                if(chks[j].checked)
                    ic++
            }
            
            if(ic>1)
            {
                showInfo("不能同时修改2个或者2个以上的记录");
                return;
            }
            
            for(var i=0;i<chks.length;i++)
            {                
                if(chks[i].checked)
                {                    
                   showEditPanel(chks[i].value);
                   //chks[i].nextSibling.value;
                   return;
                }
            }
            
            showInfo("请选择要修改的信息");
        
    }



    
    function showEditPanel(id)
    {
      
        //拼写请求URL参数
        var postParams = "Action=GetItem&id="+id;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
            dataType:'string',
            data: '',
            cache:false,
            success:function(data) 
            {                      
                var result = null;
                eval("result = "+data);
                
                if(result.result)                    
                {
                    showEditPanelData(result.data);
                }
                else
                {
                    ShowInfo(result.data);
                }    
            },
            error:function(data)
            {
                 ShowInfo(data.responseText);
            }
        });
    }

    function CancelEdit()
    {
        document.getElementById("editPanel").style.display = "none";
    }
    function HtmlEncode(input)
    {
       var converter=document.createElement("DIV");
       converter.innerText=input;
       var output=converter.innerHTML;
       converter=null;
       return output;
    }
    function HtmlDecode(input)
    {
       var converter=document.createElement("DIV");
       converter.innerHTML=input;
       var output=converter.innerText;
       converter=null;
       return output;
    }
    
    
   
    function showEditPanelData(data)
    {        
      
        document.getElementById("itemID").value = data.ID;
    
        document.getElementById("spNewsTitle").value = HtmlDecode(data.NewsTitle);
        document.getElementById("spNewsContent").value =HtmlDecode(data.NewsContent.replace(/<br><br>/g,"\r"));
        document.getElementById("slStatus").value = data.Status;
        document.getElementById("slIsShow").value = data.IsShow;
        document.getElementById("spCreateDate").innerHTML = data.CreateDate;
        
        //alert(data.Status);
        if(data.Status != "0")
        {
            document.getElementById("spNewsTitle").readOnly = true;
            document.getElementById("spNewsContent").readOnly = true;
            document.getElementById("slStatus").readOnly = true;
        }
     
        var ele = document.getElementById("editPanel");
        ele.style.display = "block";
        ele.style.left = "200";
        ele.style.top = "160";
        
    }
    
    
    
    
    function saveEdit()
    {
        
    
        var title = HtmlEncode(document.getElementById("spNewsTitle").value);
        var content = HtmlEncode(document.getElementById("spNewsContent").value);
        var id =  document.getElementById("itemID").value;
        var Status = document.getElementById("slStatus").value;
        var IsShow = document.getElementById("slIsShow").value;
          
         if(title + "" == "")
            {
                showInfo("请填写信息标题");
                return;
            }
             if(title.length>50)
            {
                showInfo("标题 长度不能超过50");
                return;
            }
            
             if(content + "" == "")
            {
                showInfo("请填写信息内容");
                return;
            }
              if(content.length>500)
            {
                showInfo("内容 长度不能超过500");
                return;
            }
            title =UrlEncode(title);
            content =UrlEncode(content)
       
        //拼写请求URL参数
        var postParams = "Action=EditItem&Status="+Status+"&IsShow="+IsShow+"&id="+id+"&title="+title+"&content="+content;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Personal/MessageBox/Notice.ashx?" + postParams,
            dataType:'string',
            data: '',
            cache:false,
            success:function(data) 
            {                      
                var result = null;
                eval("result = "+data);
                
                if(result.result)                    
                {                  
                    alert("保存成功");
                    TurnToPage(currentPageIndex);
                    
                    document.getElementById("editPanel").style.display = "none";
                }
                else
                {
                    ShowInfo(result.data);
                }    
            },
            error:function(data)
            {
                 ShowInfo(data.responseText);
            }
        });
       
    }
    
    function isCanDelete( uid){

         if( document.getElementById("UserID").value == uid )
         {
           return "  isDel='1' ";
         }else{
           return "   isDel='0'    ";
         }
    }
      


function DelMessage()
{


//var d = document.getElementById("itd");   
//var nodeList = document.getElementById("itd");
var nodeList=document.getElementsByName("itd"); 

var f="";  
for( var i = 0;i<nodeList.length;i++){   
   // alert(nodeList[i].innerHTML);   
  // f+=nodeList[i].innerHTML; 
   
   f+=nodeList[i].value; 
} 


        var ck = document.getElementsByName("Checkbox1");
      
        
        
        var ids = "";    
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
            
            
              if(nodeList[i].value=="已审核"){
              alert("此公告已经审核无法删除");
              return;
              }
           
               if(ck[i].isDel =="0" ){
                   showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","只能选择自己的公告删除。");
                   return;
               }      
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
        
       
        
        
         if(!confirm("确认删除吗??"))
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
    // TurnToPage(currentPageIndex);
                
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
           var condition = "Status="+document.getElementById("Select1").value;//审核状态
          
           var txtTitle=document.getElementById("txtTitle").value;//公告主题
                
           var createDate1=document.getElementById("createDate1").value;//起始时间
           var createDate2=document.getElementById("createDate2").value;//结束时间
        
        
            if(txtTitle + "" != "")
           {
            txtTitle = txtTitle.replace(/\'/g, "''");
                txtTitle = txtTitle.replace(/\%/g, "[%]");
            condition += " AND a.newsTitle like '%"+txtTitle+"%'";
           } 
         
          var dt1 = str2date(createDate1);
            var dt2 = str2date(createDate2);
            if( dt1 > dt2 && dt2 != null)
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","开始日期不能大于结束日期！");               
                return;
            }           
         
            if(createDate1 + "" != "")
           {
            condition += " AND a.CreateDate>='"+createDate1+"'";
           }
           if(createDate2 + "" != "")
           {
            dt2 = dateAdd(dt2,1*24*60*60*1000);  
            condition += " AND a.CreateDate<='"+date2str(dt2,"yyyy-MM-dd")+"'";
           }
           
         
           //alert(condition);
           var action="LoadData";
        
           var fields="a.[ID],a.NewsTitle,a.Creator,a.CreateDate,a.Comfirmor,b.EmployeeName,a.ComfirmDate,a.NewsContent,a.Status,a.IsShow";
           fields = UrlEncode(fields);
                       
          var prams = "Fields="+fields;
           prams += "&condition="+UrlEncode(condition);
           
           if(orderBy + "" != "")
           {
            prams += "&OrderExp="+UrlEncode(orderBy);
           }else{
            prams += "&OrderExp="+UrlEncode("a.[ID] DESC");
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
                        }else{
                            item.ComfirmDate = date2str(str2date(item.ComfirmDate),"yyyy-MM-dd") 
                        }
                        
                        var linkt = "";
                        //if( item.Status == "0")
                        //{
                            linkt = "<a href=\"#\" onclick=\"showEditPanel("+item.ID+","+item.Creator+")\" title=\"点击查看详细\">"+item.NewsTitle+"</a>";
                        //}else{
                        //    linkt = item.NewsTitle;
                        //}
                        
                          if( document.getElementById("hidStatus").value =="1" ){
                          
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+"  type='checkbox'   "+isCanDelete(item.Creator)+"  />"+"</td>"+
                        
                        "<td height='22'  align='center'>"+linkt+"</a></td>"+ 
                        "<td height='22' align='center'>"+item.NewsContent.substring(8, 0)+"</td>"+                       
                     // "<td height='22' align='center'>"+"<span id='itd'>"+(item.Status == "0"?"未审核":"已审核")+"</span>"+"</td>"+
                       
                       "<td height='22' align='center' ><input type='text' id='itd' name='itd'  style='background-color:transparent;border:none;text-align:center;' value='"+(item.Status == "0"?"未审核":"已审核")+"'  />"+""+"</td>"+
                        
                        "<td height='22' align='center'>"+(item.IsShow == "0"?"否":"是")+"</td>"+
                        
                       "<td height='22' align='center'>"+ date2str(str2date(item.CreateDate),"yyyy-MM-dd") +"</td>"+  
                       "<td height='22' align='center'>"+item.EmployeeName+"</td>"+                     
                        "<td height='22' align='center'>"+ item.ComfirmDate +"</td>"+
                        
                        "<td height='22' align='center'>"+item.EmployeeName2 +"</td>").appendTo($("#pageDataList1 tbody"));
                        
                        
                        }else{
                                     $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+"  type='checkbox'   "+isCanDelete(item.Creator)+"  />"+"</td>"+
                        
                        "<td height='22' align='center'>"+linkt+"</a></td>"+ 
                        
                        
                       // "<td height='22' align='center'>"+ date2str(str2date(item.CreateDate),"yyyy-MM-dd HH:mm:ss") +"</td>"+
                         
                        "<td height='22' align='center'>"+item.NewsContent+"</td>"+
                       
                          "<td height='22' align='center'>"+"<span id='itd'>"+(item.Status == "0"?"未审核":"已审核")+"</span>"+"</td>"+
                        
                        "<td height='22' align='center'>"+(item.IsShow == "0"?"否":"是")+"</td>"+
                        
                          "<td height='22' align='center'>"+ date2str(str2date(item.CreateDate),"yyyy-MM-dd HH:mm:ss") +"</td>"+
                           "<td height='22' align='center'>"+item.EmployeeName+"</td>"+
                          
                        "<td height='22' align='center'>"+ item.ComfirmDate +"</td>"+
                        
                        "<td height='22' align='center'>"+item.EmployeeName2 +"</td>").appendTo($("#pageDataList1 tbody"));
                        
                        
                        
                        }
                        
                        
                        
                        
                        
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
  document.getElementById('isSearched').value = "1";
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
    if( !IsNumber(newPageCount))
         {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示条数必须为数字！");
            return;
         }else{
            if(newPageCount.substring(0,1) == "0")
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示条数不能以0开头！");
                 return;
            }
         }
         
       
        if(! IsNumber(newPageIndex))
         {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数必须为数字！");
             return;
         }else{
            if(newPageIndex.substring(0,1) == "0")
            {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转至页数不能以0开头！");
                 return;
            }
         }
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
        if(document.getElementById('isSearched').value == "0"){
          return;
        }
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
               if(ck[i].isDel =="0" ){
                    alert("只能选择删除自己的公告！");
               }else{
                 ck2 += ck[i].value+',';
              }
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