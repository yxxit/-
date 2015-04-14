


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


function setTypes(obj)
{
    var BillType = document.getElementById("BillType");

    BillType.options.length = 0;
    
    BillType.options.add(new Option("请选择","-1"));
    for(var mem in billTypes)
    {
        if(billTypes[mem].p != obj.value)
        {
            continue;
        }
        
        BillType.options.add(new Option(billTypes[mem].t,billTypes[mem].v));
    }
}

var sessionSection="";
$(document).ready(function(){
     TurnToPage(currentPageIndex);
      
      var BillType = document.getElementById("BillType");
       var BillFlag = document.getElementById("BillFlag");
      
      BillFlag.options.add(new Option("请选择","-1"));
      for(var mem in billFlags)
      {
        BillFlag.options.add(new Option(billFlags[mem].t,billFlags[mem].v));
      }
      
      
       
      BillType.options.add(new Option("请选择","-1"));
//      for(var mem in billTypes)
//      {
//       if(billFlags[mem].t!="0" && billFlags[mem].t!="")
//       {
//            BillType.options.add(new Option(billFlags[mem].t,billFlags[mem].v));
//       }
//      }






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
          
           //var flowName=document.getElementById("flowName").value;
           var billNo=document.getElementById("billNo").value;//单据编号
           var slFlowStatus=document.getElementById("slFlowStatus").value;//审批状态
           var createDate1=document.getElementById("createDate1").value;//起始日期
           var createDate2=document.getElementById("createDate2").value;//开始日期
             var billFlag=document.getElementById("BillFlag").value;//单据类型
           var billType = document.getElementById("BillType").value;//单据类型
           var ApplyUser = document.getElementById("ApplyUser").value; //流程发起人
           
        
//           var result;eval("result="+result);

//           if(flowName + "" != "")
//           {
//            condition += " AND FlowName like '%"+getSafeSqlValue(flowName)+"%'";
//           }
            if(billNo + "" != "")
           {
              condition += " AND billNo like '%"+ getSafeSqlValue(billNo)+"%'";
           }
           
            if(slFlowStatus  != "-1")
           {
            condition += " AND FlowStatus="+slFlowStatus;
           }
              
              
              
              
               if(ApplyUser +"" !="")
          
            {
   
          condition += " AND ApplyUserEmpid='"+ApplyUser+"'";
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
            condition += " AND convert(varchar(12),ApplyDate,23)>='"+createDate1+"'";
           }
           if(createDate2 + "" != "")
           {
            dt2 = dateAdd(dt2,1*24*60*60*1000);  
            condition += " AND convert(varchar(12),ApplyDate,23)<='"+date2str(dt2,"yyyy-MM-dd")+"'";
           }
           
           
            if(billFlag  != "-1" && billFlag != "")
           {
            condition += " AND BillTypeFlag="+billFlag;
           }
            if(billType  != "-1"  && billType != "")
           {
            condition += " AND BillTypeCode="+billType;
           }
           //alert(condition);
          var action="LoadFlowWaitProcess";
           
           var fields="[ID],[BillNo],[ApplyUserEmpid],[ApplyDate],[FlowName],ApplyUserId,[FlowStatus],[BillTypeFlag] ,[BillTypeCode],BillID,[StepNo] ,[StepName] ,[ModifiedUserID],[ApplyUserName]";
           fields = UrlEncode(fields);
                       
          var prams = "Fields="+fields;
           prams += "&condition="+UrlEncode(condition);
           
           if(orderBy + "" != "")
           {
            prams += "&OrderExp="+UrlEncode(orderBy);
           }
           
           
           
           
         //显示数据
           $.ajax({
           type: "POST",//用POST方式传输
           //dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Personal/WorkFlow/WorkFlow.ashx',//目标地址
           cache:false,
           data: "action="+action+"&pageIndex="+pageIndex+"&pageSize="+pageCount+ "&ApplyUser="+ApplyUser +"&"+prams,//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前
           
           success: function(result) {
            //alert(result);
            var result; eval("result=" + result);
          
                     //var  msg = result.data.list;
           
           //return;//"[ID],[FlowName],[FlowStatus],[BillTypeFlag] ,[BillTypeCode],[StepNo] ,[StepName] ,[ModifiedUserID]
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    $.each(result.data.list,function(i,item){
                     if(item.ID != null && item.ID != ""){
                       
                       
                        var billtypeitem = getBillTypeItem(item.BillTypeFlag,item.BillTypeCode);
                         
                        
                        if(billtypeitem == null)
                        {
                            billtypeitem = {};
                        }
                       
                   
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  value="+item.ID+"  type='checkbox'/>"+"</td>"+
                         "<td height='22' align='center'><a href='#' onclick=GetLinkParam0('" +item.BillID +"')><span title=\"点击查看详细\">" + item.BillNo+ "</a></td>"+
                        "<td height='22' align='center'><input type='text'  name='itd'  style='background-color:transparent;border:none;text-align:center;' value='"+(billtypeitem.t)+"'  />"+""+"</td>"+
                               "<td height='22' align='center'>" + date2str(str2date(item.ApplyDate), "yyyy-MM-dd") + "</td>" +
                      
                      
                     //   "<td height='22' align='center'>"+ item.FlowName +"</td>"+
                 
                        
                        "<td height='22' align='center'>"+getStatusName(item.FlowStatus) +"</td>"+
                        "<td height='22' align='center'>" + item.ApplyUserName + "</td>" +
                       
                       //"<td height='22' align='center'>"+item.StepNo+"</td>"+
                       // "<td height='22' align='center'>"+item.StepName+"</td>"+
                           "<td height='22' align='center'>" + item.ModifiedUserID+ "</td>" +
                       
                        "<td height='22' align='center'><div class='menu'><ul><li><a href='#' >操作</a><ul><li><a href='#'  onclick=GetLinkParam0('" +item.BillID +"')>查看</a></li><li><a href='#'onclick='objFlow.Fun_Show(true,"+item.BillID+")'>审核撤销</a></li><li><a href='#'onclick='objFlow.Fun_Show(true,"+item.BillID+")'>审核通过</a></li></ul></li></ul></div></td>").appendTo($("#pageDataList1 tbody"));
                      
                          
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




//跳转到详细页面

var M='80011';


//判断是采购合同还是销售合同





function GetLinkParam0(id){



 var billId=document.getElementsByName("itd");


  for(var i = 0 ;i<billId.length;i++)
       {
         
           if(billId[i].value=="销售合同")
           {
             
                 GetLinkParam1(id); //销售合同审批
           }
           else
           {
              //采购合同审批
              GetLinkParam2(id);
           }
       }
    

    
}



////跳转到销售合同号

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


