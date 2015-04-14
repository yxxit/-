/* 分页相关变量定义 */  
var pageCount = 10;//每页显示记录数
var totalRecord = 0;//总记录数
var pagerStyle = "flickr";//jPagerBar样式
var currentPageIndex = 1;//当前页
var orderBy = "";//排序字段


/*
* 设置数据明细表的行颜色
*/
function SetTableRowColor(elem,colora,colorb, colorc, colord){
    //获取DIV中 行数据
    var tableTr = document.getElementById(elem).getElementsByTagName("tr");
    for(var i = 0; i < tableTr.length; i++)
    {
        //设置行颜色
        tableTr[i].style.backgroundColor = (tableTr[i].sectionRowIndex%2 == 0) ? colora:colorb;
        //设置鼠标落在行上时的颜色
        tableTr[i].onmouseover = function()
        {
            if(this.x != "1") this.style.backgroundColor = colorc;
        }
        //设置鼠标离开行时的颜色
        tableTr[i].onmouseout = function()
        {
            if(this.x != "1") this.style.backgroundColor = (this.sectionRowIndex%2 == 0) ? colora:colorb;
        }
    }
}


/*
* 添加凭证连接
*/
function LinkVoucherParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("VoucherModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "VoucherForm.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
   
    window.location=linkParam;
}

//删除收款单信息
function DeleteIncomeBill()
{
     
     var chkList = document.getElementsByName("Checkbox1");
     var chkValue = "";   
     var UrlParms="";
     var ConfirmStatus=document.getElementsByName();
     var BillingID="";
     var index=0;
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        { 
           var value= chkList[i].value .split("|");
           chkValue += " " + value[0].toString() + " ,"; 
//           if(ConfirmStatus=="已确认")
//           {
//             showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已确认的收款单不可删除！");
//             return;
//           }
           ++index;
           if(index>1)
           {
              showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择单个进行删除！");
              return;
           }
        }
    }
    if(chkValue=="")
    {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
       return;
    }
    chkValue=chkValue.substring(0, chkValue.length - 1);
//    BillingID=BillingID.substring(0, BillingID.length - 1);
//    TotalPrice=TotalPrice.substring(0, TotalPrice.length - 1);
    UrlParms="&Action=Delete&ID="+chkValue;
      if(confirm("确认删除吗！"))
      {
   $.ajax({ 
              type: "POST",
              url: "../../../Handler/JTHY/Expenses/PayBill_list.ashx?"+UrlParms,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {
              popMsgObj.ShowMsg('请求发生错误');
            }, 
            success:function(data) 
            { 
                 
                if(data.sta==1) 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除成功！");
                    TurnToPage(1);
                }
                else if(data.sta==2)
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已确认的收款单不可删除！");
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","删除失败！");
                } 
            } 
           });  
         }
  
}

//添加凭证
function AddVoucher()
{
     var chkList = document.getElementsByName("chkSelect");
     var chkValue = "";   
     var UrlParms="";
     var TotalPrice=0;
     var Confirm="";
     var IsVoucher="";
     var CurrencyType="";
     var CurrencyRate="";
     var CurrencyName="";
     var j=0;
     
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if ( chkList[i].checked )
        { 
          var value= chkList[i].value .split("|");
          
          if(j==0)
           {
               chkValue += " " + value[0].toString() + " ,"; 
               
               var ChangeAmount=value[3].replace(/,/g,"");
               TotalPrice+=parseFloat(ChangeAmount);
    //           
    //           TotalPrice+= parseFloat( value[3].replace(/ /g,"")); 
               IsVoucher=value[4].replace(/ /g,"");
               Confirm=value[2].replace(/ /g,"");
               
                /*
               币种，汇率，币种名称
               */
               CurrencyType=value[6].toString();
               CurrencyRate=value[7].toString();
               CurrencyName=value[8].toString();
               
               if(Confirm!="已确认")
               {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择已确认的收款单！");
                  return;
               }
               if(IsVoucher!="未登记")
               {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择未登记凭证的收款单！");
                  return;
               }
            }
            else
            {
            
               chkValue += " " + value[0].toString() + " ,"; 
               
               var ChangeAmount=value[3].replace(/,/g,"");
               TotalPrice+=parseFloat(ChangeAmount);
    //           
    //           TotalPrice+= parseFloat( value[3].replace(/ /g,"")); 
               IsVoucher=value[4].replace(/ /g,"");
               Confirm=value[2].replace(/ /g,"");
               
               
                /*
               币种，汇率
               */
              var  CurrencyTypeStr=value[6].toString();
              var  CurrencyRateStr=value[7].toString();
              
              if(CurrencyTypeStr!=CurrencyType)
              {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择币种相同的收款单登记凭证！");
                    return;
              }
              if(CurrencyRateStr!=CurrencyRate)
              {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择汇率相同的收款单登记凭证！");
                    return;
              }
              
               if(Confirm!="已确认")
               {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择已确认的收款单！");
                  return;
               }
               if(IsVoucher!="未登记")
               {
                  showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择未登记凭证的收款单！");
                  return;
               }
            }
            j++;
         }
    }
    
    if(chkValue=="")
    {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
       return;
    }
   
    chkValue=chkValue.substring(0, chkValue.length - 1);
  
    var parms=TotalPrice+"|"+chkValue+"|"+"officedba.IncomeBill"+"|"+CurrencyType+"|"+CurrencyRate+"|"+CurrencyName+"|1";
    //获取新建凭证模块功能ID
    var ModuleID = document.getElementById("VoucherModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "VoucherForm.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag+"&parms="+parms+"&Type=2";
   
    window.location.href=linkParam.replace(/ /g,"");
}





//反确认收款单
function ReconfirmIncomeBill()
{

    var chkList = document.getElementsByName("chkSelect");
    var chkValue = ""; 
    var Confirm="";
    var IsConfirm = "";
    var j = 0;
    var billingid="";
    var TotalPrice = 0;
    var price="";
   
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if (chkList[i].checked )
        { 
          var value= chkList[i].value .split("|");
           chkValue += " " + value[0].toString() + " ,";
           billingid=value[5].toString();
           price =value[3].toString();
          
           Confirm=value[2].replace(/ /g,"");
           IsConfirm=value[4].replace(/ /g,"");
           if(Confirm!="已确认")
           {
             break;
           }
           
       
//           if(IsConfirm=="已登记")
//           {
//              break;
//           }

           j++;
        }
    }

    if(chkValue=="")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
       return;
   }

   if (j > 1) {
       showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "只能选择一项进行反确认操作！");
       return;
   }


    
    if(Confirm!="已确认")
    {
       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择已确认的收款单进行反确认操作！");
       return;
    }
//     if(IsConfirm=="已登记")
//    {
//       showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","已登证的凭证不可反确认操作！");
//       return;
//   }
    
    
 

    chkValue=chkValue.substring(0, chkValue.length - 1);
    UrlParms="&Action=ReverseAudit&ID="+chkValue+"&billingid="+escape(billingid)+"&price="+escape(price);
   $.ajax({ 
              type: "POST",
              url: "../../../Handler/JTHY/Expenses/IncomeBill_list.ashx?"+UrlParms,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {
              popMsgObj.ShowMsg('请求发生错误');
            }, 
            success:function(data) 
            { 
                if(data.sta==1) 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","反确认成功！");
                    TurnToPage(currentPageIndex);
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","反确认失败！");
                } 
            } 
           });  
}

//确认收款单
function ConfirmIncomeBill()
{
    var chkList = document.getElementsByName("chkSelect");
    var chkValue = ""; 
    var Confirm="";
    var IsConfirm = "";
    var billingid="";
    var TotalPrice = 0;
    var price="";
    var CurrencyType = "";
    var CurrencyRate = "";
    var CurrencyName = "";
    var CustID = "";
    var incomeno="";
    var j = 0;
    
    for( var i = 0; i < chkList.length; i++ )
    {
        //判断选择框是否是选中的
        if (chkList[i].checked )
        { 
          var value= chkList[i].value .split("|");
           chkValue += " " + value[0].toString() + " ,";
           billingid=value[5].toString();
           price =value[3].toString();
           incomeno=value[10].toString();
           Confirm = value[1].replace(/ /g, "");
           j++;
           var ChangeAmount = value[3].replace(/,/g, "");
           TotalPrice += parseFloat(ChangeAmount);
           /*
           币种，汇率，币种名称
           */
           CurrencyType = value[6].toString();
           CurrencyRate = value[7].toString();
           CurrencyName = value[8].toString();
           CustID = value[9].toString();
           
           if(Confirm!="")
           {
             
              break;
           }
        }
    }
     if(Confirm!="")
     {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请选择未确认的收款单进行确认操作！");
        return;
     }
    if(chkValue=="")
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请至少选择一项！");
       return;
   }

   if (j > 1) {
       showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "只能选择一项进行确认操作！");
       return;
    }

    chkValue = chkValue.substring(0, chkValue.length - 1);
     UrlParms = "&Action=Audit&ID=" + chkValue + "&Currency=" + escape(CurrencyType) + "," + escape(CurrencyRate) + "&CustID=" + escape(CustID) + "&PayAmount=" + escape(TotalPrice)+"&billingid="+escape(billingid)+"&price="+escape(price)+"&incomeno="+escape(incomeno);
   $.ajax({ 
              type: "POST",
              url: "../../../Handler/JTHY/Expenses/IncomeBill_list.ashx?"+UrlParms,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() {
              popMsgObj.ShowMsg('请求发生错误');
            }, 
            success:function(data) 
            { 
                if(data.sta==1) 
                { 
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认成功！");
                    TurnToPage(currentPageIndex);
                }
                else
                {
                    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","确认失败！");
                } 
            } 
           });  
   
}

/*
* 查询收款单列表
*/
function SearchIncomeBillInfo(currPage)
{
   //判断开始时间不能大于结束时间
   var StartDate=document.getElementById("txtStartDate").value;
   var EndDate=document.getElementById("txtEndDate").value;
      StartDate=StartDate.replace(/-/g,"/"); 
     EndDate=EndDate.replace(/-/g,"/"); 
   if((StartDate!="" && EndDate=="") ||  (StartDate=="" && EndDate!=""))
   {
       popMsgObj.ShowMsg('请全选开始时间和结束时间！');
        return ; 
   }
   if(StartDate!="" && EndDate!="")
   {
     StartDate=StartDate.replace(/-/g,"/"); 
     EndDate=EndDate.replace(/-/g,"/"); 
     if(Date.parse(StartDate)>Date.parse(EndDate))
     {
        popMsgObj.ShowMsg('开始时间不能大于结束时间！');
        return ;
     }
   }
    var search = "";
    // 付款单号
    search += "IncomeNo=" + document.getElementById("txtIncomeNo").value;
    //收款方式
    //search += "&AcceWay=" + document.getElementById("IncomeBillType").value;
    //收款金额
     //search += "&TotalPrice=" + document.getElementById("txtTotalPrice").value;
    //确认状态
    search += "&ConfirmStatus=" + document.getElementById("DrpConfirmStatus").value;
    //往来用户
    search += "&CustName=" + document.getElementById("txtCustName").value;
    //开始时间
     search += "&StartDate=" + document.getElementById("txtStartDate").value;
    //结束时间
     search += "&EndDate=" + document.getElementById("txtEndDate").value;
     //登记凭证
     //search += "&IsAccount=" + document.getElementById("DrpIsAccount").value;
     search +="&Executor="+document.getElementById("txtExecutor").value;//经办人
     
     //search += "&ProjectID=" + document.getElementById("hidProjectID").value;

     //search += "&ProjectName=" + document.getElementById("txtProject").value;
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



/*
* 改页显示
*/
function ChangePageCountIndex(newPageCount, newPageIndex)
{
    //判断是否是数字
    if (!IsZint(newPageCount))
    {
        popMsgObj.ShowMsg('请输入正确的显示条数！');
        return;
    }
    if (!IsZint(newPageIndex))
    {
        popMsgObj.ShowMsg('请输入正确的转到页数！');
        return;
    }
    //判断重置的页数是否超过最大页数
    if(newPageCount <=0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1)/newPageCount) + 1)
    {
        popMsgObj.ShowMsg('转至页数超出查询范围！');
    }
    else
    {
        //设置每页显示记录数
        this.pageCount = parseInt(newPageCount);
        //显示页面数据
        TurnToPage(parseInt(newPageIndex));
    }
}


/*
* 获取链接的参数
*/
function GetLinkParam()
{
    //获取模块功能ID
    var ModuleID = document.getElementById("hidModuleID").value;
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //是否点击了查询标识
    var flag = "0";//默认为未点击查询的时候
    if (searchCondition != "") flag = "1";//设置了查询条件时
    linkParam = "IncomeBill_Add.aspx?ModuleID=" + ModuleID 
                            + "&PageIndex=" + currentPageIndex + "&PageCount=" + pageCount + "&" + searchCondition + "&Flag=" + flag;
    //返回链接的字符串
    return linkParam;
}
function GetLinkParam1(id, billingid, price) {
    parent.addTab(null, '2091304', '收款单', 'Pages/Office/FinanceManager/IncomeBill_Add.aspx?ModuleID=2091304&ID=' + id + '&BillingID=' + billingid + '&price=' + price);
}


/*
* 排序处理
*/
function OrderBy(orderColum,orderTip)
{
 var searchStr=document.getElementById("hidSearchCondition").value;
    if(searchStr=="")
    {
       // popMsgObj.ShowMsg('请检索数据后再');
    }
    else
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
}

/*
* 新建业务单信息
*/
function NewBilling()
{
    //    window.location.href = GetLinkParam();
    window.parent.addTab(null,'8880505','新建付款单','Pages/JTHY/Expenses/PayBill_Add.aspx?ModuleID=8880505');
    //window.parent.addTab(null,'8880101','新建收款单','Pages/JTHY/Expenses/IncomeBill_Add.ashx?ModuleID=8880101');//跳转到添加页面
    //parent.addTab(null, '2091304', '新建收款单', 'Pages/Office/FinanceManager/IncomeBill_Add.aspx?ModuleID=2091304');
}

/**
    跳转到添加页面
*/
function SelectDept(id,PayNo){
	//window.parent.addTab(null,'8880101','销售合同','Pages/JTHY/Expenses/IncomeBill_Add.aspx?ModuleID=8880501&Id='+escape(id)+'&incomeNo'+incomeNo+'');
	window.parent.addTab(null,'8880505','新建付款单','Pages/JTHY/Expenses/PayBill_Add.aspx?ModuleID=8880505&intMasterID='+escape(id)+'&PayNo='+PayNo+'');
}





/*
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    //设置当前页
    currentPageIndex = pageIndex;
    //获取查询条件
    var msg="";
    var searchCondition = document.getElementById("hidSearchCondition").value;//通过隐藏控件转换值
    //设置动作种类
    var action="Get";
    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //进行查询获取数据
    var total1=0;
    $.ajax({ 
              type: "POST",
              url: "../../../Handler/JTHY/Expenses/PayBill_list.ashx?"+postParam,
              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
                  //AddPop();
              }, 
            error: function() {
              popMsgObj.ShowMsg('请求发生错误');
            },
        success: function(data)
        {
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(data.data, function(i, item) {
                if (item.ID != null && item.ID != "") {
                            
                            $("<tr class='newrow'></tr>").append(
                            "<td height='22' align='center'>"+
                            "<input id='checkall"+i+"' name='Checkbox1'  onclick=IfSelectAll('Checkbox1','checkall') value="
                            +item.ID+" type='checkbox'/>"+
                            "</td>"+
                            "<td height='22'  align='center'><a href='#' onclick=\"SelectDept('" + item.ID + "','" + item.PayNo + "')\">" + item.PayNo + "</a></td>" +
                            "<td height='22' align='center' ><span title=\"" + item.CustName + "\">"+ item.CustName +"</span></td>"+
                                                     
                            "<td height='22'  align='center'>" + item.BillingID + "</td>"+                                                       
                            
                            "<td height='22'  align='center'>" + item.PayDate + "</td>"+
                            "<td height='22'  align='center'>" + item.PayAmount + "</td>"+
                            "<td height='22'  align='center'>" + item.AcceWay + "</td>"+
                            "<td height='22'  align='center'>" + item.Executor + "</td>"+
                            "<td height='22'  align='center'>" + item.Confirmor + "</td>"+
                            "<td height='22'  align='center'>" + item.ConfirmDate + "</td>").appendTo($("#tblDetailInfo tbody")
                            );  
        
                    }
            });
             
            //页码
            ShowPageBar(
                "divPageClickInfo",//[containerId]提供装载页码栏的容器标签的客户端ID
                "<%= Request.Url.AbsolutePath %>",//[url]
                {
                    style:pagerStyle,mark:"DetailListMark",
                    totalCount:data.totalCount,
                    showPageNumber:3,
                    pageCount:pageCount,
                    currentPageIndex:pageIndex,
                    noRecordTip:"没有符合条件的记录",
                    preWord:"上一页",
                    nextWord:"下一页",
                    First:"首页",
                    End:"末页",
                    onclick:"TurnToPage({pageindex});return false;"
                }
            );
            totalRecord = data.totalCount;
            $("#txtShowPageCount").val(pageCount);
            ShowTotalPage(data.totalCount, pageCount, pageIndex,$("#pagecount"));
            $("#txtToPage").val(pageIndex);
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}
