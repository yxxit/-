//选择往来客户
   function SelectCust()
   {
       var url="../../../Pages/Office/FinanceManager/CustSelect.aspx";
       var returnValue = window.showModalDialog(url, "", "dialogWidth=300px;dialogHeight=500px");
       if(returnValue!="" && returnValue!=null)
       {
         var value=returnValue;
         value=value.split("|");
          document.getElementById("txtCustName").value=value[0].toString();
          document.getElementById("CustID").value=value[1].toString();
          document.getElementById("FromTBName").value=value[2].toString();
          document.getElementById("FileName").value=value[3].toString();
          
       }
       else
       {
           document.getElementById("txtCustName").value="";
           document.getElementById("CustID").value="";
           document.getElementById("FromTBName").value="";
           document.getElementById("FileName").value="";
       }
   }
   
/*
* 查询付款单列表
*/
function SearchPayBillInfo(currPage)
{
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
        popMsgObj.ShowMsg('显示条数必须输入正整数！');
        return;
    }
    if (!IsZint(newPageIndex))
    {
        popMsgObj.ShowMsg('跳转页数必须输入正整数！');
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
* 翻页处理
*/
function TurnToPage(pageIndex)
{
    if(document.getElementById("txtCustName").value=="")
    {
         popMsgObj.Show("请选择|", "供应商信息|");
         return;
    }
    //设置当前页
    currentPageIndex = pageIndex;
//    //获取查询条件
//    var searchCondition = document.getElementById("hidSearchCondition").value;
    //设置动作种类
    var action="Get";
//    var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&" + searchCondition;
    //var postParam = "PageIndex=" + pageIndex + "&PageCount=" + pageCount + "&Action=" + action + "&OrderBy=" + orderBy + "&CustID=" + document.getElementById("CustID").value;
    var postParam = "Action=" + action + "&CustID=" + document.getElementById("CustID").value;
    var PayAmountTotal=0;//付款金额
    var BalanceAmountTotal=0;//付款余额  
    var PayAmountTotalBilling=0;//付款金额
    var BalanceAmountTotalBilling=0;//付款余额   
    
    //进行查询获取数据
    $.ajax({
        type: "POST",//用POST方式传输
        url:  '../../../Handler/Office/FinanceManager/PayBlendBill.ashx?' + postParam,//目标地址
        dataType:"json",//数据格式:JSON
        cache:false,
        beforeSend:function()
        {
            AddPop();
        },//发送数据之前
        success: function(msg)
        {
            //数据获取完毕，填充页面据显示
            //付款数据列表
            $("#tblDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.data
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    {
                        if(parseFloat(item.PayAmount)>0)
                       {
                            PayAmountTotal+=parseFloat(item.PayAmount);
                       }
                        if(parseFloat(item.NAccounts)>0)
                       {
                            BalanceAmountTotal+=parseFloat(item.NAccounts);
                       }                       
                       
                    $("<tr class='newrow'></tr>").append("<td height='22' style='display:none' align='center'>"
                        + "<input id='payBillID" + i + "' value='" + item.ID + " ' type='hidden'/>"
                        + "</td>" //选择框
                        + "<td height='22' align='center'>"+item.PayDate+"</td>" //付款日期
                        + "<td height='22' id='payNo" + i + "' align='center'>" + item.PayNo + "</td>" //付款单编码
                        + "<td height='22' align='center'>"+item.CustName+"</td>" //往来客户                       
                        + "<td height='22' align='center'>"+item.CurrencyName+"</td>" //币种
                        + "<td height='22' align='center'>"+item.PaymentType+"</td>" //款项类型                       
                        + "<td height='22' align='center'>"+item.AcceWay+"</td>" //支付方式
                        + "<td height='22' id='tdPayAmount" + i + "' align='left'>"+item.PayAmount+"</td>" //付款金额 
                        + "<td height='22' id='tdNAccounts" + i + "' align='left'>" + NumRound(item.NAccounts,2) + "</td>" //付款余额
                        + "<td height='22' align='left'><input id='settleAmount" + i + "' type='text' value='0.00' class=\"tdinput\" onblur=\"Number_round(this,2);CalculateTotal();\" style='width:95%;' /></td>").appendTo($("#tblDetailInfo tbody"));//结算金额
                    }
            });
            
                var PayAmountTotal1=parseFloat(PayAmountTotal).toFixed(2);
                var BalanceAmountTotal1=parseFloat(BalanceAmountTotal).toFixed(2);                
                 $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +                               
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='left'>" + PayAmountTotal1 + "</td>" +
                   "<td height='22' align='left'>" + BalanceAmountTotal1 + "</td>" +
                   "<td height='22' id='tdSettleAmountTotal' align='left'>0.00</td>").appendTo($("#tblDetailInfo tbody"));
                   
            //待核销列表
            $("#tblBlendDetailInfo tbody").find("tr.newrow").remove();
            $.each(msg.billingData
                ,function(i,item)
                {
                    if(item.ID != null && item.ID != "")
                    {
                        if(parseFloat(item.TotalPrice)>0)
                       {
                            PayAmountTotalBilling+=parseFloat(item.TotalPrice);
                       }
                        if(parseFloat(item.NAccounts)>0)
                       {
                            BalanceAmountTotalBilling+=parseFloat(item.NAccounts);
                       }                       
                       
                    $("<tr class='newrow'></tr>").append("<td height='22' style='display:none' align='center'>"
                        + "<input id='billingID" + i + "' value='" + item.ID + "' type='hidden'/>"
                        + "</td>" //选择框
                        + "<td height='22' align='center'>"+item.CreateDate+"</td>" //单据日期
                        + "<td height='22' align='center'>"+item.InvoiceType+"</td>" //单据类型
                        + "<td height='22' align='center'>" + item.BillingNum + "</td>" //单据编号
                        + "<td height='22' align='center'>"+item.ContactUnits+"</td>" //往来客户
                        + "<td height='22' align='center'>"+item.BillCD+"</td>" //订单号                       
                        + "<td height='22' align='center'>"+item.CurrencyName+"</td>" //币种 
                        + "<td height='22' id='tdTotalPrice" + i + "' align='left'>"+item.TotalPrice+"</td>" //原单金额 
                        + "<td height='22' id='tdNAccountsBilling" + i + "' align='left'>" + item.NAccounts + "</td>" //原单余额                   
                        + "<td height='22' align='left'><input id='settleAmountBilling" + i + "' type='text' value='0.00' class=\"tdinput\" onblur=\"Number_round(this,2);CalculateBillingTotal();\" style='width:95%;' /></td>").appendTo($("#tblBlendDetailInfo tbody"));//本次结算
                    }
            });
            
                var PayAmountTotalBilling1=parseFloat(PayAmountTotalBilling).toFixed(2);
                var BalanceAmountTotalBilling1=parseFloat(BalanceAmountTotalBilling).toFixed(2);                
                 $("<tr class='newrow'></tr>").append("<td height='22' align='center'>合计</td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +
                   "<td height='22' align='center'></td>" +    
                   "<td height='22' align='center'></td>" +                                              
                   "<td height='22' align='left'>" + PayAmountTotalBilling1 + "</td>" +
                   "<td height='22' align='left'>" + BalanceAmountTotalBilling1 + "</td>" +                   
                   "<td height='22' id='tdSettleAmountTotalBilling' align='left'>0.00</td>").appendTo($("#tblBlendDetailInfo tbody"));
        },
        error: function() 
        {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        complete:function(){
            hidePopup();
            $("#divPageClickInfo").show();
            SetTableRowColor("tblDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
            SetTableRowColor("tblBlendDetailInfo","#E7E7E7","#FFFFFF","#cfc","cfc");
        }
    });
}

function CalculateTotal(){
    var settleAmountTotal=0;//结算金额
    var signFrame = findObj("tblDetailInfo", document);
    for (i = 0; i < signFrame.rows.length-2; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
            var rowid = i;
            var pCountDetail =0;
            var NAccounts=0;
            
            if($("#settleAmount" + rowid))
            {
                NAccounts = $("#tdNAccounts" + rowid).text();
                pCountDetail=($("#settleAmount" + rowid).val()=="")?0:$("#settleAmount" + rowid).val(); //结算金额  
                if(parseFloat(pCountDetail)>parseFloat(NAccounts))
                {                    
                    popMsgObj.ShowMsg("结算金额不能大于付款余额!");
                    $("#settleAmount" + rowid).val('');
                    return;
                }   
                settleAmountTotal += parseFloat(pCountDetail);
            }                 
        }        
    }          
    $("#tdSettleAmountTotal").text((parseFloat(settleAmountTotal)).toFixed(2));//金额合计    
}

function CalculateBillingTotal(){
    var settleAmountTotal=0;//结算金额
    var signFrame = findObj("tblBlendDetailInfo", document);
    for (i = 0; i < signFrame.rows.length-2; i++) 
    {
        if (signFrame.rows[i].style.display != "none") 
        {
            var rowid = i;
            var pCountDetail =0;
            var NAccounts=0;
            
            if($("#settleAmountBilling" + rowid))
            {
                NAccounts = $("#tdNAccountsBilling" + rowid).text();
                pCountDetail=($("#settleAmountBilling" + rowid).val()=="")?0:$("#settleAmountBilling" + rowid).val(); //结算金额  
                if(parseFloat(pCountDetail)>parseFloat(NAccounts))
                {                    
                    popMsgObj.ShowMsg("结算金额不能大于付款余额!");
                    $("#settleAmountBilling" + rowid).val('');
                    return;
                }   
                settleAmountTotal += parseFloat(pCountDetail);
            }                 
        }        
    }          
    $("#tdSettleAmountTotalBilling").text((parseFloat(settleAmountTotal)).toFixed(2));//金额合计    
}

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

function SaveBlend(){
    var payBillTotal=$("#tdSettleAmountTotal").text();
    var billingTotal=$("#tdSettleAmountTotalBilling").text();
    
    if(billingTotal==payBillTotal){
        if(payBillTotal==""){
            popMsgObj.ShowMsg("请先进行检索!");
            return;
        }
        if(parseFloat(billingTotal)==0){
            popMsgObj.ShowMsg("请输入结算金额!");
            return;
        }
    }
    
    if(parseFloat(billingTotal)!=parseFloat(payBillTotal))
    {        
        popMsgObj.ShowMsg("本次结算合计金额不等于付款单结算合计金额!");
        return;
    }
    
    var Action=null;
    Action="Blending";                  
    var UrlParms="Action="+escape(Action);    
    var PayBillID=new Array();   
    var PayNo=new Array();                     
    var PayAmount=new Array();            
    var NAccounts=new Array();
    var SettleAmount=new Array(); 
   
    var BillingID=new Array();                        
    var PayAmountBilling=new Array();            
    var NAccountsBilling=new Array();
    var SettleAmountBilling=new Array(); 
                                                  
    var signFrame = findObj("tblDetailInfo", document);
    var signFrameBlend = findObj("tblBlendDetailInfo", document);
    if(signFrame!=null && signFrame!=undefined && signFrameBlend!=null && signFrameBlend!=undefined)
    {
        //付款单 
        for (i = 0; i < signFrame.rows.length-2; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {        
                if(parseFloat($("#settleAmount"+i).val()) > 0)
                {
                    PayBillID.push($("#payBillID"+i).val());  
                    PayNo.push($("#payNo"+i).text());  
                    PayAmount.push($("#tdPayAmount"+i).text());  
                    NAccounts.push($("#tdNAccounts"+i).text()); 
                    SettleAmount.push($("#settleAmount"+i).val()); 
                }                                                               
            }        
        } 
        UrlParms+="&PayBillID="+escape(PayBillID)+"&PayNo="+escape(PayNo)+"&PayAmount="+escape(PayAmount)+"&NAccounts="+escape(NAccounts)+"&SettleAmount="+escape(SettleAmount);
        
        //核销单
        for (i = 0; i < signFrameBlend.rows.length-2; i++) 
        {
            if (signFrameBlend.rows[i].style.display != "none") 
            {      
                if(parseFloat($("#settleAmountBilling"+i).val()) > 0)
                {
                    BillingID.push($("#billingID"+i).val());   
                    PayAmountBilling.push($("#tdTotalPrice"+i).text());  
                    NAccountsBilling.push($("#tdNAccountsBilling"+i).text()); 
                    SettleAmountBilling.push($("#settleAmountBilling"+i).val()); 
                }                                                                 
            }        
        } 
        UrlParms+="&BillingID="+escape(BillingID)+"&PayAmountBilling="+escape(PayAmountBilling)+"&NAccountsBilling="+escape(NAccountsBilling)+"&SettleAmountBilling="+escape(SettleAmountBilling);
        
        $.ajax({ 
                      type: "POST",
                      url: "../../../Handler/Office/FinanceManager/PayBlendBill.ashx?"+UrlParms,
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
                            if(data.sta>0) 
                            {
                                popMsgObj.ShowMsg(data.info);
                                SearchPayBillInfo();
                            }
                            else
                            {
                                popMsgObj.ShowMsg(data.info);
                            }
                    } 
        }); 
    }    
         
}