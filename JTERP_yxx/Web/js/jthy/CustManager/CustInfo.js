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
    
    
$(document).ready(function(){

//alerCustType();


    requestobj = GetRequest(); 
    var CustNo = requestobj['CustNo'];
    var CustNam = requestobj['CustNam'];
    var CustClass = requestobj['CustClass'];
    var CustClassName = requestobj['CustClassName'];    
    
    if(typeof(CustNam)!="undefined")
    { 
       $("#txtCustNo").attr("value",CustNo);//客户简称
       $("#txtCustNam").attr("value",CustNam);
       $("#CustClassDrpControl1_CustClassHidden").attr("value",CustClass);       
       $("#CustClassDrpControl1_CustClass").attr("value",CustClassName);
       currentPageIndex = requestobj['currentPageIndex'];
       pageCount = requestobj['pageCount'];
       
       SearchCustData(currentPageIndex);
    }
    var url = location.search;
    var theRequest = new Object();
    
    if (url.indexOf("?") != -1) 
    {
        var str = url.substr(1);
        if(str!="ModuleID=555232")
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
            if(type=="PowerDate")
            {
               document.getElementById("txtpowerbegin").value=day;
               document.getElementById("txtpowerend").value=modifyDay;
            }
            if(type=="Usedata")
            {
               document.getElementById("txtusebegin").value=day;
               document.getElementById("txtuseend").value=modifyDay;
            }
            if(type=="Certidata")
            {
              document.getElementById("txtcertibegin").value=day;
              document.getElementById("txtcertiend").value=modifyDay;
            }
            if(type=="Wardata")
            {
               document.getElementById("txtwarbegin").value=day;
               document.getElementById("txtwarend").value=modifyDay;
            }
            if(type=="Gmspdata")
            {
             document.getElementById("txtgmspbegin").value=day;
             document.getElementById("txtgmspend").value=modifyDay;
            }      
       
         TurnToPage(1);
        }
      }
});
    
    
    //jQuery-ajax获取JSON数据
    function TurnToPage(pageIndex)   //20140331刘锦旗
    {
        document.getElementById("checkall").checked = false;
    
        ifdel="0";
           currentPageIndex = pageIndex;
           var CustNo =document.getElementById("txtCustNo").value;  //客户编号
           //var CustNam =document.getElementById("txtCustNam").value; //客户简称          
           //var CustClass = $("#CustClassDrpControl1_CustClassHidden").val(); //客户分类
           //var CustClass2 = document.getElementById("CustClassDrpControl1_CustClassHidden").value;          
           var CustName =document.getElementById("txtCustName").value;  //客户名称
           //var CustShort = document.getElementById("txtCustShort").value;
           //var Area = document.getElementById("ddlArea").value;
           //var CreditGrade = document.getElementById("ddlCreditGrade").value;  //客户优质级别
//            var CreditGrade ="0";
//           var CustType = document.getElementById("ddlCustType").value;
//           var RelaGrade = document.getElementById("seleRelaGrade").value;
           var Manager = document.getElementById("txtManager").value;
//           var CreatedBegin = document.getElementById("txtCreatedBegin").value;
//           var CreatedEnd = document.getElementById("txtCreatedEnd").value;
//           var UsedStatus = document.getElementById("seleUsedStatus").value;  //启用状态
//           var Tel = document.getElementById("txtTel").value;
//           var CustBig = document.getElementById("selCustBig").value;
//           var CustAdd = document.getElementById("txtCustAdd").value;
//           var Creator=document.getElementById("txtCreator").value;
           //20120724新添加 医药行业专用
//           var Corptype = document.getElementById("selecorptype").value;//企业类型
//           var Category = document.getElementById("selecategory").value;
//           var Pagestatus = document.getElementById("selepagestatus").value;
          
//           var Usebegin = document.getElementById("txtusebegin").value;
//           var Useend = document.getElementById("txtuseend").value;
//           
//            if (Usebegin != "" && Useend != "") {
//                if (Usebegin > Useend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }           
//           
//           var Certibegin = document.getElementById("txtcertibegin").value;
//           var Certiend = document.getElementById("txtcertiend").value;
//           var Warbegin = document.getElementById("txtwarbegin").value;
//           var Warend = document.getElementById("txtwarend").value;
//           var Powerbegin = document.getElementById("txtpowerbegin").value;
//           var Powerend = document.getElementById("txtpowerend").value;
//           var Gmspbegin = document.getElementById("txtgmspbegin").value;
//           var Gmspend = document.getElementById("txtgmspend").value;
//            if((document.getElementById("txtusebegin")!=null)&&(document.getElementById("txtuseend")!=null))
//            {
//            var Usebegin = document.getElementById("txtusebegin").value;
//            var Useend = document.getElementById("txtuseend").value;
//            if (Usebegin != "" && Useend != "") {
//                if (Usebegin > Useend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//            }
//            else
//            {
//             var Usebegin="";
//             var Useend="";
            //}
            
//            if((document.getElementById("txtcertibegin")!=null)&&(document.getElementById("txtcertiend")!=null))
//            {
//            var Certibegin = document.getElementById("txtcertibegin").value;
//            var Certiend = document.getElementById("txtcertiend").value;
//            if (Certibegin != "" && Certiend != "") {
//                if (Certibegin > Certiend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//            }
//            else
//            {
//             var Certibegin="";
//             var Certiend="";
//            }
            
//            if((document.getElementById("txtwarbegin")!=null)&&(document.getElementById("txtwarend")!=null))
//            {
//            var Warbegin = document.getElementById("txtwarbegin").value;
//            var Warend = document.getElementById("txtwarend").value;
//            if (Warbegin != "" && Warend != "") {
//                if (Warbegin > Warend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//            }
//            else
//            {
//             var Warbegin="";
//             var Warend="";
//            }
            
//            if((document.getElementById("txtpowerbegin")!=null)&&(document.getElementById("txtpowerend")!=null))
//            {
//            var Powerbegin = document.getElementById("txtpowerbegin").value;
//            var Powerend = document.getElementById("txtpowerend").value;
//            if (Powerbegin != "" && Powerend != "") {
//                if (Powerbegin > Powerend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//            }
//            else
//            {
//             var Powerbegin="";
//             var Powerend="";
//            }
            
//            if((document.getElementById("txtgmspbegin")!=null)&&(document.getElementById("txtgmspend")!=null))
//            {
//            var Gmspbegin = document.getElementById("txtgmspbegin").value;
//            var Gmspend = document.getElementById("txtgmspend").value;
//            if (Gmspbegin != "" && Gmspend != "") {
//                if (Gmspbegin > Gmspend) {
//                    alert("起始时间不能大于终止时间!");
//                    return;
//                }
//            }
//            }
//            else
//            {
//             var Gmspbegin="";
//             var Gmspend="";
//            }
           
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/CustInfo.ashx',//目标地址
           cache:false,           
           data: "pageIndex="+pageIndex+"&pageCount="+pageCount+"&action="+action+"&orderby="+orderBy+"&CustNo="+reescape(CustNo)+
                 "&CustName="+reescape(CustName)+"&Manager="+reescape(Manager),//数据
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
                           switch(item.RelaGrade)
                          {
                                case "1":
                                RelaGrade = "密切";
                                break;
                                case "2":
                                RelaGrade = "较好";
                                break;
                                case "3":
                                RelaGrade = "一般";
                                break;
                                case "4":
                                RelaGrade = "较差";
                                break;
                                default:
                                RelaGrade = "";
                          }
                          switch(item.UsedStatus)
                          {
                                case "1":
                                UsedSta = "启用";
                                break;
                                case "0":
                                UsedSta = "停用";
                                break;                                
                                default:
                                UsedSta = "启用";
                          }

                            $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input id='Checkbox1' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall')  value="+item.id+","+item.CustNo+"  type='checkbox'/>"+"</td>"+
                            "<td height='22'  align='center'><a href='#' onclick=\"SelectDept('" + item.id + "','" + item.CustBig + "','" + item.CustNo + "')\">" + item.CustNo + "</a></td>" +
                            "<td height='22' align='center' ><span title=\"" + item.CustName + "\">" + item.CustName.substring(0,12) + "</span></td>"+
                            "<td height='22' align='center' style='display:none'>"+ item.CustBigName +"</td>"+
                            "<td height='22' align='center' ><span title=\"" + item.ReceiveAddress + "\">"+ item.ReceiveAddress.substring(0,15) +"</span></td>"+
                            "<td height='22'  style='display:none'>" + item.CustShort + "</td>"+
                            "<td height='22' align='center'  style='display:none'>" + item.CodeName + "</td>"+
                            "<td height='22'  align='center'style='display:none'>" + item.TypeName + "</td>"+
                            "<td height='22' align='center' style='display:none'>" + item.Area + "</td>"+
                            "<td height='22'  align='center'>" + item.Manager + "</td>"+
                            "<td height='22' align='center'  style='display:none'>" + item.CreditGrade + "</td>"+
                            "<td height='22' align='center'  style='display:none'>" + RelaGrade + "</td>"+
                            "<td height='22'  align='center'>" + item.Creator + "</td>"+
                            "<td height='22'  align='center'>" + item.CreatedDate + "</td>"+
                            "<td height='22'  align='center'>"+ UsedSta +"</td>").appendTo($("#pageDataList1 tbody"));                            
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

function SearchCustData(aa)
{

    if(!CheckInput())
    {
        return;
    }
    ifdel = "0";
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

function SelectDept(custid,custbig,custno)
{
    parent.addTab(null, '2021101', '新建客户档案', 'Pages/JTHY/SysManage/Cust_Add.aspx?ModuleID=555231&custid=' + custid+'&custbig='+custbig+'&custno='+custno);
}
function CreateCust()
{
    //window.location.href='Cust_Add.aspx';
    var CustNo =document.getElementById("txtCustNo").value;
    //var CustNam =document.getElementById("txtCustNam").value;    
    //var CustClass = $("#CustClassDrpControl1_CustClassHidden").val();   
    //var CustClassName = $("#CustClassDrpControl1_CustClass").val();
    
//    window.location.href='Cust_Add.aspx?custid=-1&Pages=Cust_Info.aspx&CustNam='+CustNam+'&CustNo='+CustNo+'&ModuleID=2021101'+
//                            '&CustClass='+CustClass+'&CustClassName='+CustClassName+'&currentPageIndex='+currentPageIndex+'&pageCount='+pageCount;
    parent.addTab(null, '2021101', '新建客户档案', 'Pages/JTHY/SysManage/Cust_Add.aspx?ModuleID=555231');
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
 function DelCustInfo()
{

    if(confirm("删除后不可恢复，确认删除吗！"))
    {
        var ck = document.getElementsByName("Checkbox1");
        var ck2 = "";
        var CustNos="";
        for( var i = 0; i < ck.length; i++ )
        {
            if ( ck[i].checked )
            {
               ck2 += ck[i].value.split(',')[0]+',';
               CustNos += ck[i].value.split(',')[1]+',';              
            }
        }
        
        var custids = ck2.substring(0,ck2.length-1);
        CustNos = CustNos.substring(0,CustNos.length-1);
        x = ck2.split(',');
        if(x.length-1<=0)
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一条客户信息后再删除！");
            return;
        }
        else
        {
            //删除
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/Office/CustManager/CustInfoDel.ashx",
                  data:"allcustid="+reescape(custids)+"&AllCustNO="+reescape(CustNos),
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
                     if(data.sta==2) 
                    { 
                         showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","有关联数据，删除失败！");                     
                    } 
                    else
                    {
                        //popMsgObj.Show('删除失败！');
                        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
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
    
    var txtCustNo = document.getElementById('txtCustNo').value;//客户编号
    
    var RetVal=CheckSpecialWords();

    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    
    
    if(txtCustNo.length>0 && txtCustNo.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
        fieldText = fieldText + "客户编号|";
	    msgText = msgText + "客户编号输入不正确|";
    }  
    if(!isFlag)
    {   
      
        popMsgObj.Show(fieldText,msgText);
        
    }
    return isFlag;
}
    
