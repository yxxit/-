 var treeveiwPopUp;
 var  showOrderStr ="";
var SearchedFlag = false;
var totalRecord = 0;
var pageCount = 10;
var pagenum = 0;
var pageindex=0;
var jsondata;
 
 function DeptComfirm(){
       document.getElementById("lblGroupBy").value =   treeveiwPopUp.getValue().val;
       document.getElementById("tboxGroupBy").value =   treeveiwPopUp.getValue().txt;
       document.getElementById("divSelDept").style.display ='none';
 }
 
 function  DeptCancel(){
       document.getElementById("divSelDept").style.display ='none';
 }
 
function SelectDept(){
   $.ajax({
            type: "POST",//用POST方式传输 
            dataType:"json",//数据格式:JSON
            url:  '../../../Handler/Personal/AimManager/GetDeptList.ashx', //目标地址
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前
           success: function(msg)
           {
                treeveiwPopUp = new TreeView("divtreeview",msg.data,0,0,-1,0,0);
                document.getElementById("divSelDept").style.display ='';
           },
           error:function(res) {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }
          });  

}

function  SelectGroupBy(){
    if(  document.getElementById("radioDept").checked ==true)
          SelectDept();
    else{
          showSelPanel('lblGroupBy','tboxGroupBy',2);divposition(this);
    }
}

function ChangeGroupBy(){
  var listtable = document.getElementById("tbCountList");
                  var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       }
        if(  document.getElementById("radioDept").checked ==true)
             document.getElementById('lblGroupObj').innerHTML ='部门名称';
        else
             document.getElementById('lblGroupObj').innerHTML ='负责人名称';
       document.getElementById("lblGroupBy").value =   "";
       document.getElementById("tboxGroupBy").value =  "";
}

function SearchStat(){
    var parm =  GetSearchParm();
    $.ajax({
            type: "POST",//用POST方式传输 
            dataType:"json",//数据格式:JSON
            url:  '../../../Handler/Personal/WorkFlow/FlowReport.ashx?'+parm, //目标地址
            cache:false,//指令
           beforeSend:function()
           {
           },//发送数据之前
           success: function(msg)
           {
               jsondata = eval(msg.data);
               ShowList();     
           },
           error:function(res) {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }
          });  
}


function GetSearchParm(){
     var parm_str = "";
       if( document.getElementById("radioDept").checked ==true  ){
           parm_str +="GroupBy=Dept"+"&";
       }else{
           parm_str +="GroupBy=Staff"+"&";
       }
       parm_str +="GroupByValue=" + document.getElementById("lblGroupBy").value+"&";
       parm_str +="StartDate=" + document.getElementById("txtStartDate").value+"&";
       parm_str +="EndDate="+ document.getElementById("txtEndDate").value+"&";
       parm_str +="AimType="+ document.getElementById("cbaimtype").checked+"&";
       parm_str +="AimStatus="+ document.getElementById("cbaimstatus").checked+"&";
       return parm_str;
}

function AddGroupByTable(){
  var listtable = document.getElementById("tbCountList");
                  var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       }
   if( document.getElementById("cbaimtype").checked ==true   ){
       document.getElementById("thAimType").style.display ='';
   }else{
      document.getElementById("thAimType").style.display ='none';
   }
  if( document.getElementById("cbaimstatus").checked ==true   ){
       document.getElementById("thAimStatus").style.display ='';
   }else{
      document.getElementById("thAimStatus").style.display ='none';
   }
}

function ShowList(){
                  pageindex = parseInt( document.getElementById("txtToPage").value)-1;
                  pageCount = parseInt( document.getElementById("txtShowPageCount").value);
                  var listtable = document.getElementById("tbCountList");
                  var   length= listtable.rows.length ;      
                       for( var i=0; i<length; i++ )
                       {
                          listtable.deleteRow(0);     
                       } 
                        if(jsondata == undefined  )
                           return;
                for(var listindex = pageindex*pageCount; listindex<pageindex*pageCount+pageCount;listindex++){
                             
                              if( listindex >= jsondata.length )
                               break;
                              var tr =  listtable.insertRow(-1);
                              tr.height = "20";
                              tr.onmouseover= function (){ currtrColor = this.style.background; this.style.background = '#CAFFBA';}
                              tr. onmouseout= function (){  this.style.background = currtrColor;}
                              if(listindex % 2 == 0){
                                tr.style.background = "#ffffff";
                              }else{
                                 tr.style.background = "#E7E7E7";
                              }
                                           if( document.getElementById("radioDept").checked == false   )
                                               tr.insertCell(-1).innerHTML = jsondata[listindex].PrincipalName ;
                                           else
                                                tr.insertCell(-1).innerHTML = jsondata[listindex].DeptName ;
                                           if( document.getElementById("cbaimstatus").checked == true   )
                                                tr.insertCell(-1).innerHTML =ConvertAimStatusToCharacter(jsondata[listindex].FlowStatus ) ;
                                           tr.insertCell(-1).innerHTML =jsondata[listindex].countnum  ;
                    } 
}

                     
 function ConvertAimStatusToCharacter( staNum ){
       switch (staNum){
         case "1": return "待审批"  ;break;
         case "2": return "审批中"  ;break;
         case "3": return "审批通过"  ;break;
         case "4": return "审批不通过"  ;break;
         case "5": return "撤销审批"  ;break;
         default : return  "";
       }
    }