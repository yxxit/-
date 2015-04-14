var jsondata1;
var jsondata2;
var jsondata3;
var jsondata4;
var jsondata5;
var jsondata6;
var jsondata7;
var jsondata8;

var returnResult =0;
var Today = new Date();
var tY = Today.getFullYear();
var tM = Today.getMonth();
var tD = Today.getDate();
var dateTimeClock;
var sessionSection="";
$(document).ready(function(){
      var url = document.location.href.toLowerCase(); 
      if(url.indexOf("/(s(") != -1)
      {
        var sidx = url.indexOf("/(s(")+1;
        var eidx = url.indexOf("))")+2;
        //alert(sidx+":"+eidx);
        url = document.location.href; 
        
        sessionSection = url.substring(sidx,eidx);        
        sessionSection += "/";
      }
      
    }); 

 //****************************************************************************************************************
//审批流
 

 function SearchDeskFlow(){

 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
        
           
            url: 'Handler/Personal/WorkFlow/WorkFlow.ashx',//目标地址
            data:"action=desktoploaddata",
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                     
                      
                      jsondata2 = msg.data.list;
                   
                      returnResult++;
                      isDeskAllRetrun();
                      
           },
           error:function(res) {
           
           }
          });
      }
 
 
 
 
 
 
 

 
 function getBillTypeItem(i,j)
{
    for(var mem in  billTypes)
    {
        if(billTypes[mem].v == j && billTypes[mem].p == i)
        {            
            return billTypes[mem];
            break;
        }
    }
    
    return null;
}





//查询短消息
 function SearchDeskUnreadMessage(){
 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
              url:  'Handler/Personal/MessageBox/InputBox.ashx',//目标地址
           data:"action=desktoploaddata",
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           {    
            
                       jsondata4 = msg.data.list;
                       returnResult++;
                       isDeskAllRetrun();
           },
           error:function(res) {
           
           }
          });        
 }
 
 
 function getBillTypeItem(i,j)
{
    for(var mem in  billTypes)
    {
        if(billTypes[mem].v == j && billTypes[mem].p == i)
        {            
            return billTypes[mem];
            break;
        }
    }
    
    return null;
}





function viewMsg(id)
   {
              var action  ="GetItem";              
              $.ajax({ 
                    type: "POST",
                    url: "Handler/Personal/MessageBox/InputBox.ashx?action=" + action,
                    dataType:'string',
                    data: 'id='+id,
                    cache:false,
                    success:function(data) 
                    {                          
                        var result = null;
                        eval("result = "+data);
                        
                        if(result.result)                    
                        {
                              //showInfo(result.data);  
                               dispData(result.data);              
                        }else{                  
                              showInfo(result.data);               
                        }                   
                    },
                    error:function(data)
                    {
                         showInfo(data.responseText);
                    }
                });
                
        } 
        
        
               
  function dispData(data)
        {
            var ttID = document.getElementById("ttID");
            var ttTitle = document.getElementById("ttTitle");
            var ttSendUser = document.getElementById("ttSendUser");
            var ttSendDate = document.getElementById("ttSendDate");
            var ttContent = document.getElementById("ttContent");
            ttTitle.innerHTML = data.ID;
            ttSendUser.innerHTML = data.SendUserID;
            ttTitle.innerHTML = data.Title;
            ttSendDate.innerHTML = data.CreateDate;
            ttContent.innerHTML = data.Content;
            document.getElementById("editPanel").style.left = "100px";
            document.getElementById("editPanel").style.top ="100px";
            document.getElementById("editPanel").style.display = "";            
           SearchDeskUnreadMessage();
        }
        function hideMsg()
        {
            document.getElementById("editPanel").style.display = "none";
        }
        

//获取当前日期和时间
function Getdate()
{
   var today=new Date();
    var year=today.getYear();
    var month=today.getMonth()+1;
    var day=today.getDate();
    var hours = today.getHours();
    var minutes = today.getMinutes();
    var seconds = today.getSeconds();
    var timeValue= hours;//((hours >12) ? hours -12 :hours);
    timeValue += ((minutes < 10) ? ":0" : ":") + minutes+"";
    //timeValue += (hours >= 12) ? "PM" : "AM";
    timeValue+=((seconds < 10) ? ":0" : ":") + seconds+"";
    var timetext=year+"-"+month+"-"+day+" "+timeValue
    //document.write("<span onclick=\"document.getElementById('time').value='"+timetext+"'\">"+timetext+"</span>");
    document.getElementById("Today").value=year+"-"+month+"-"+day;//获取当前日期
//    document.getElementById("liveclock").innerText = timetext; //div的html是now这个字符串
    document.getElementById("liveclock").value = timeValue; //div的html是now这个字符串
    setTimeout("Getdate()",1000); //设置每秒，调用showtime方法
    
 }
 var c="";






//查询所有的公告信息
function SearchDeskTopNotice(){
 
  $.ajax({
            type: "POST",//用POST方式传输
            dataType:"json",//数据格式:JSON
            url:  'Handler/Personal/MessageBox/Notice.ashx',//目标地址
            data:"action=desktopdataload",
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前 
           success: function(msg)
           { 
            
                      jsondata3 = msg.data.list;      
                       returnResult++;
                      isDeskAllRetrun();
           },
           error:function(res) {
              
           }
          });        
 }
 
 
 
 
 function HtmlDecode(input)
    {
       var converter=document.createElement("DIV");
       converter.innerHTML=input;
       var output=converter.innerText;
       converter=null;
       return output;
    }
    
    
 function showNoticePanel(id){
       var title=document.getElementById ("spNewsTitle");
       var content=document.getElementById("spNewsContent");
       for(var listindex = 0 ;listindex<jsondata3.length;listindex++)
       {
           if(jsondata3[listindex].ID==id)
           {
               title.value=HtmlDecode(jsondata3[listindex].NewsTitle);
               content.value=HtmlDecode(jsondata3[listindex].NewsContent);
           }
       }
    document.getElementById('desktopnotice').style.display='';
 } 
 


 

 function getStatusName(i)
{ 
    if( i == "1")
        return "待我审批";
    if( i == "2")
        return "审批中";
   if( i == "3")
        return "审批通过";
    if( i == "4")
        return "审批不通过";
            if( i == "5")
        return "撤销审批";
  return "未知";
}

function  InitPage(){  
 
      SearchDeskFlow(); //查找流程

      SearchDeskTopNotice(); //查找公告
      SearchDeskUnreadMessage(); //查找未读消息 
  
      GetServerTime();
 }

function isDeskAllRetrun(){
 
    if(  returnResult > 0 )
    {
       ShowDeskSearchDeskFlow(); // 待办流程
       ShowDeskSearchUnreadMessage(); // 未读短信  
       ShowDeskSearchDeskTopNotice(); //公告  
      
    }else
       return;
}
   

   
//****显示流程***************************



   function ShowDeskSearchDeskFlow(){
   
      var   listtable =  document.getElementById("tbList2");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%'; 
                       listtable.cellSpacing=1; 
                 try{
             var  th = listtable.insertRow(-1);
                           th.align = "left";
                            th.style.background="#ffffff";  
                            th.height = 30; 
                            th.insertCell(-1).innerHTML = "<strong>单据编号</strong>";
                           th.insertCell(-1).innerHTML = "<strong>单据类型</strong>";  
                           th.insertCell(-1).innerHTML = "<strong>流程提交人</strong>";                       
                                                  
                                                  
                    listcount =jsondata2.length;
                    for(var listindex = 0 ;listindex<jsondata2.length;listindex++){
                           if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "30";
                              tr.align = "left";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#F0f0f0";
                              }else{
                                 tr.style.background = "#F0f0f0";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=8;
                                 td.innerHTML = "<a href='Pages/Personal/WorkFlow/FlowWaitProcess.aspx' >更多...</a> " 
                                return;
                              }
//                      var billtypeitem = getBillTypeItem(jsondata2[listindex].BillTypeFlag,jsondata2[listindex].BillTypeCode);
//                        
//                       if(billtypeitem == null)
//                       {
//                            billtypeitem = {};
//                        }
//                        
                     
                             var tr =  listtable.insertRow(-1);
                              tr.height = "30";
                              tr.align = "left";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#fff";
                              }else{
                                 tr.style.background = "#fff";
                              }
                                      //tr.insertCell(-1).innerHTML = "<a href='/"+sessionSection+jsondata2[listindex].BillID+"&intFromType=2'  title='点击查看"+ jsondata2[listindex].BillNo+"的详细信息'  >"+ jsondata2[listindex].BillNo +"</a>";
                                       //  tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"GetLinkParam0("+ jsondata2[listindex].BillNo+")\" title=\"点击查看详细\">"+ jsondata2[listindex].BillNo+"</a>" 
                                   
                            
                                    tr.insertCell(-1).innerHTML="<a href='#' onclick=GetLinkParam0('" + jsondata2[listindex].BillID +"')><span title=\"点击查看详细\">" + jsondata2[listindex].BillNo+ "</a>"
                                        // tr.insertCell(-1).innerHTML =  billtypeitem.t;
                                        //   tr.insertCell(-1).innerHTML = jsondata2[listindex].BillNo ; 
                                          tr.insertCell(-1).innerHTML = jsondata2[listindex].FlowName ;                           
                                          // tr.insertCell(-1).innerHTML = getStatusName(jsondata2[listindex].FlowStatus); 
                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].ApplyUserName; 
//                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].StepNo; 
//                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].StepName;   
//                                           tr.insertCell(-1).innerHTML = jsondata2[listindex].ModifiedUserID;  
//                    
                    }                  
                   
                  }catch(e){  } 
   }
   
   
   
  
  




 //跳转到详细页面 



var M='80011';



//判断是采购合同还是销售合同

 
function GetLinkParam0(id){


  for(var listindex = 0 ;listindex<jsondata2.length;listindex++)
       {
           if(jsondata2[listindex].FlowName=="销售合同审批")
           {
                GetLinkParam1(id); //销售合同审批
           }
           else
           {
             GetLinkParam2(id);//采购合同审批
           }
       }
    

    
}


//跳转到销售合同号

function GetLinkParam1(id) {


    //获取查询条件
    var searchCondition = "";    
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null,'80011','销售合同详细页面','Pages/JTHY/ContractManage/SellContract_Add.aspx?ModuleID=80011&intMasterID='+escape(id)+'&'+searchCondition+'&Flag='+flag+'');

    
}

//跳转到采购合同号
function GetLinkParam2(id) {

    
    //获取查询条件
  var  searchCondition ="" ;
    var flag = "0"; //默认为未点击查询的时候
    if (searchCondition != "") flag = "1"; //设置了查询条件时
    window.parent.addTab(null,'80021','采购合同详细页面','Pages/JTHY/ContractManage/PurContract_Add.aspx?ModuleID=80021&intMasterID='+escape(id)+'&'+searchCondition+'&Flag='+flag+'');
}



   
   /*显示公告*/
    function ShowDeskSearchDeskTopNotice(){

     var   listtable =  document.getElementById("tbdestnoticeList");
                       
                       var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%';   
                       listtable.height =20;
                 try{
                     var th = listtable.insertRow(-1);
                        th.align = "left";
                        th.height = 30;
                        th.style.background="#ffffff";  
                        th.insertCell(-1).innerHTML = "<strong>公告主题</strong>";
                        th.insertCell(-1).innerHTML = "<strong>发布时间</strong>";  
                        
                    listcount =jsondata3.length;
                    for(var listindex = 0 ;listindex<jsondata3.length;listindex++){
                          if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                tr.height = "30";
                          
                               var td=tr.insertCell(-1);
                              
                              tr.align = "left";
                             
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                                
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=2;
                                 td.innerHTML = "<a href='Pages/Personal/MessageBox/DeskTopPublicNotice.aspx' >更多...</a> " 
                             
                                return;
                              }
                  
                             var tr =  listtable.insertRow(-1);
                              tr.height = "30";
                               tr.align="left";
                             
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#fff";
                              }else{
                                 tr.style.background = "#fff";
                             }
                             var a = jsondata3[listindex].NewsTitle;
                             if (a.length > 8) {
                                 tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"showNoticePanel(" + jsondata3[listindex].ID + ")\" title=\"点击查看详细\">" + a.substring(0, 8) + "..." + "</a>"
                                 tr.insertCell(-1).innerHTML = jsondata3[listindex].ShortCreateDate;
                             } else {
                             tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"showNoticePanel(" + jsondata3[listindex].ID + ")\" title=\"点击查看详细\">" + jsondata3[listindex].NewsTitle + "</a>"
                                 tr.insertCell(-1).innerHTML = jsondata3[listindex].ShortCreateDate;
                             }  
                    }
                    
                
                  }catch(e){  } 
   }
   
    /*公告显示结束*/
    
    
    
    
    /*显示未读信息*/
   function ShowDeskSearchUnreadMessage(){
     var   listtable =  document.getElementById("jt_desk_message");                       
                        var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                       listtable.style.width = '100%';   
                       listtable.height =20;
                 try{
                     var  th = listtable.insertRow(-1);
                          th.align = "left";
                          th.height = 30;                           
                          th.style.background="#fff"; 
                          th.insertCell(-1).innerHTML = "<strong>信息主题</strong>";
                          th.insertCell(-1).innerHTML = "<strong>发布时间</strong>";
                          th.insertCell(-1).innerHTML = "<strong>发布人</strong>";                        
                   
                                                  
                    listcount =jsondata4.length;
                    for(var listindex = 0 ;listindex<jsondata4.length;listindex++){
                             if (listindex>2 ){
                                 var tr =  listtable.insertRow(-1);
                                  tr.height = "30";
                                  tr.align = "left";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                               if(listindex % 2 == 0){
                                tr.style.background = "#fff";
                              }else{
                                 tr.style.background = "#fff";
                              }
                                 var td = tr.insertCell(-1);
                                 td.colSpan=3;
                                 td.innerHTML = "<a href='Pages/Personal/MessageBox/UnReadedInfo.aspx' >更多...</a> " 
                                return;
                              }
                       //var billtypeitem = getBillTypeItem(jsondata4[listindex].BillTypeFlag,jsondata4[listindex].BillTypeCode);
                        
                        //if(billtypeitem == null)
                        //{
                            //billtypeitem = {};
                        //}
                             var tr =  listtable.insertRow(-1);
                              tr.height = "30";
                              tr.align = "left";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#fff";
                              }else{
                                 tr.style.background = "#fff";
                              }                                         
                                 tr.insertCell(-1).innerHTML = "<a href=\"#\" onclick=\"viewMsg("+ jsondata4[listindex].ID+")\" title=\"点击查看详细\">"+ jsondata4[listindex].Title+"</a>"                         
                                 tr.insertCell(-1).innerHTML = jsondata4[listindex].CreateDate; 
                                 tr.insertCell(-1).innerHTML = jsondata4[listindex].SendUserName;          
                    }
                     
                
        }catch(e){  }
   }
 /*显示未读信息结束 */ 
             
  function DateInit(){
    //document.getElementById("lblshowyear").innerHTML ="公元"+ new Date().getYear()+"年" ;
    //document.getElementById("lblmonth").innerHTML = new Date().getMonth()+1 +"月";
    //document.getElementById("spanday").innerHTML = new Date().getDate();
    //document.getElementById("lblweek").innerHTML =returnWeek(new Date().getDay());
  }
  
  function returnWeek(day){
       switch (day){
         case 1:return "星期一";
         case 2:return "星期二";
         case 3:return "星期三";
         case 4:return "星期四";
         case 5:return "星期五";
         case 6:return "星期六";
         case 0:return "星期日";
       }
  }
  
 
function GetServerTime(){
   $.ajax({ 
              type: "POST",
              url: "Handler/DeskTop.ashx?GetServerTime=true",
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              }, 
           // complete :function(){hidePopup();},
            error: function() { showPopup("Images/Pic/Close.gif","Images/Pic/note.gif","请求发生错误！");}, 
            success:function(msg) 
            { 
                 
                 var datearray = msg.data.split(' ')[0].split('-');
                 var timearray = msg.data.split(' ')[1].split(':');
                  dateTimeClock =  new Date(datearray[0],datearray[1],datearray[2],timearray[0],timearray[1],timearray[2]) ;
                  startTimer();
            },
            complete:function(){}//接收数据完毕
           });
}

var timer;
function startTimer(){
  timer= setInterval("RefreshTime();",1000);  //1秒执行一次
}

function stopTimer(){
  clearInterval(timer);
}

function RefreshTime(){
  dateTimeClock =  dateAdd(dateTimeClock,1000);
  document.getElementById("divTime").innerHTML = date2str(dateTimeClock,"当前时间 yyyy-MM-dd HH:mm:ss");
}

function dateAdd(date,val)
{    
    var b=Date.parse(date)+val ;
    b   = new   Date(b)   ;
    return b;
}

function date2str(obj,fmt)
{
    var y = obj.getYear();
    var M = obj.getMonth()+1;
    var d = obj.getDate();
    
    var h = obj.getHours();
    var m = obj.getMinutes();
    var s = obj.getSeconds();
    
    //var f = obj.getMilliseconds();
    
    if(M<10)
        M="0"+M;
    if(d<10)
        d="0"+d;
    if(h<10)
        h="0"+h;
    if(m<10)
        m="0"+m;
    if(s<10)
        s="0"+s;
         
    //fmt:yyyyMMddHHmmss fff
    
    fmt = fmt.replace("yyyy",y);
    fmt = fmt.replace("MM",M);
    fmt = fmt.replace("dd",d);
    fmt = fmt.replace("HH",h);
    fmt = fmt.replace("mm",m);
    fmt = fmt.replace("ss",s);
    
    return fmt;
   
}







