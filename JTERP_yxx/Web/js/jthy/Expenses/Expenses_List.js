    var pageCount = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var action = "";//操作
    var orderBy = "";//排序字段
    var ifdel="0";//是否删除
    var type="";
    var data="";
    var day="";
    var modifyDay="";
    
   
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)   //20140331刘锦旗
    {
        document.getElementById("checkall").checked = false;
    
        ifdel="0";
           currentPageIndex = pageIndex;
            var ExpCode=$("#txtExpCode").val();
            var Applyor=$("#txtApplyor").val();
            var TransactorName=$("#txtTransactorName").val();
            var BeginT=$("#txtBeginT").val();  //申请开始时间
            var EndT=$("#txtEndT").val();       //申请结束时间
            var CustName=$("#txtCustName").val();
            action="SearchAll";
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/Expenses/Expenses_List.ashx',//目标地址
           cache:false,           
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&ExpCode="+reescape(ExpCode)+
                 "&Applyor="+reescape(Applyor)+"&TransactorName="+reescape(TransactorName)+"&BeginT="+reescape(BeginT)+
                 "&EndT="+reescape(EndT)+"&CustName="+reescape(CustName),//数据
           beforeSend:function(){AddPop();$("#pageDataList1_Pager").hide();},//发送数据之前           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataList1 tbody").find("tr.newrow").remove();
                    // var RelaGrade,UsedSta,Corp,Cate,Pagesta;
                    var RelaGrade,UsedSta;
                    $.each(msg.data,function(i,item){
                      if(item.id != null && item.id != "")
                      {
                          if(item.ConfirmDate=="1900-01-01"){
                            item.ConfirmDate="";
                          }

                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+
                            "<input id='Checkbox"+i+"' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.id+","+item.ExpCode
                            +"  type='checkbox'/>"+"</td>"+
                            "<td height='22'  align='center'><a href='#' onclick=\"SelectDept('" + item.id + "','" + item.ExpCode + "')\">" + item.ExpCode + "</a></td>" +
                            "<td height='22' align='center' ><span title=\"" + item.ApplyorName + "\">" + item.ApplyorName.substring(0,12) + "</span></td>"+
                            "<td height='22' align='center' ><span title=\"" + item.AriseDate + "\">"+ item.AriseDate.substring(0,15) +"</span></td>"+
                         
                         
                            "<td height='22'  align='center'>" + item.TotalAmount + "</td>"+
                           
                            "<td height='22'  align='center'>" + item.CustName + "</td>"+
                            "<td height='22'  align='center'>" + item.TransactorName + "</td>"+
                            "<td height='22'  align='center'>" + item.ConfirmorName + "</td>"+
                            "<td height='22'  align='center'>" + item.ConfirmDate + "</td>").appendTo($("#pageDataList1 tbody"));                            
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
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text2").value=msg.totalCount;
                  $("#ShowPageCount").val(pageCount);
                  ShowTotalPage(msg.totalCount,pageCount,pageIndex,$("#pagecount"));
                 
                  $("#ToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdel=="0"){hidePopup();}$("#pageDataList1_Pager").show();Ifshow(document.getElementById("Text2").value);pageDataList1("pageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
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

function SearchExpensesData(aa)
{

    if(!CheckInput())
    {
        return;
    }
    search="1";
    TurnToPage(aa);
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

function SelectDept(id,ExpCode)
{
    parent.addTab(null, '8881', '费用申请单', 'Pages/JTHY/Expenses/Expenses_ADD.aspx?ModuleID=8881&id=' + id+'&ExpCode='+ExpCode);
}
function CreateExpenses()
{

    parent.addTab(null, '2021101', '新建费用申请单',  'Pages/JTHY/Expenses/Expenses_ADD.aspx?ModuleID=8881');
}
    
//改变每页记录数及跳至页数
function ChangePageCountIndex(newPageCount,newPageIndex)
{

    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdel = "0";
        this.pageCount=parseInt(newPageCount);
        TurnToPage(parseInt(newPageIndex));
    }
}
    //排序
    function OrderBy(orderColum,orderTip)
    {
        if (totalRecord == 0) 
         {
            return;
         }

        ifdel = "0";
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
        $("#hiddExpOrder").val(orderBy);
        TurnToPage(1);
    }
   
//删除
 function DelExpensesInfo()
{

    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        var  ExpensesNos="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value.split(',')[0]+',';
                ExpensesNos += ck[i].value.split(',')[1]+',';              
            }
        }
        
        var Expensesids = ck2.substring(0,ck2.length-1);
         ExpensesNos =  ExpensesNos.substring(0, ExpensesNos.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条客户信息后再删除！");
            return;
        }
        else
        {
            action="Delete";
            //删除
             $.ajax({ 
                  type:"POST",
                  url:'../../../Handler/JTHY/Expenses/Expenses_List.ashx',//目标地址
                  data:"AllId="+reescape(Expensesids)+"&AllNO="+reescape(ExpensesNos)+"&action="+reescape(action),
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      AddPop();
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
                        //成功
                        TurnToPage(1);
                        ifdel = "1";
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                        return;
                    } 
                    else if(data.sta==0) 
                    {
                        //popMsgObj.Show('删除失败！');
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                    } 
                    else if(data.sta==2)
                    {
                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","申请已经确认，无法删除！");
                    }
                } 
               });
        }
    }
}

  //是否可以导出
function IfExp() {
    if (totalRecord == 0) {
        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请先检索出数据，才可以导出！");
        return false;
    }
    
    document.getElementById("hiddCustClass").value = $("#CustClassDrpControl1_CustClassHidden").val(); //客户分类
    return true;
}

//主表单验证
function CheckInput()
{
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var ExpCode = $('#txtExpCode').val();//单据编号

    var BeginT=$("#txtBeginT").val();  //申请开始时间
    var EndT=$("#txtEndT").val();       //申请结束时间
    
    var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    
    if(ExpCode.length>0 && ExpCode.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
        fieldText = fieldText + "单据编号|";
	    msgText = msgText + "单据编号输入不正确|";
    }
    
    
    if(BeginT!="" && EndT!="")
    {
        if(BeginT>EndT)
        {
            isFlag = false;
            fieldText = fieldText +"申请时间|";
	        msgText = msgText +RetVal+  "开始时间要小于结束时间|";
	    }
    }
    
    

    
   
      
    if(!isFlag)
    {   
      
        popMsgObj.Show(fieldText,msgText);
        
    }
    return isFlag;
}
    