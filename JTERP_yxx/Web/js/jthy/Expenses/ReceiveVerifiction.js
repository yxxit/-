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
       }
       else
       {
           document.getElementById("txtCustName").value="";
           document.getElementById("CustID").value="";
       }
   }
   //table行颜色
function dg_Log(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 0; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
        
    }
}
//检索收款单
function Fun_IncomeBill()
{
   if(document.getElementById("txtCustName").value=="")
   {
         popMsgObj.Show("请选择|", "客户信息|");
         return;
   }
   var Custid="";
   var rowID=1;
   var total1=0;
   var total2=0;
   Custid=$("#CustID").val();
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/FinanceManager/ReceiveVerifiction.ashx', //目标地址
            cache: false,
            data: "Custid="+Custid+"&check=0", //数据
            beforeSend: function() {  $("#dg_Log_Pager").hide();  }, //发送数据之前
            success: function(msg) {

                //数据获取完毕，填充页面据显示
                //数据列表
                $("#dg_Log tbody").find("tr.newrow").remove();
 
                $.each(msg.data, function(i, item) {
                  
                    if (item.id != null && item.id != "") {
                        if(parseFloat(item.totalprice)!=0)
                        {
                            total1+=parseFloat(item.totalprice);
                        }
                        if(parseFloat(item.naccounts)!=0)
                        {
                            total2+=parseFloat(item.naccounts);
                        }
                        $("<tr id=\"Item_Row_"+rowID+"\" class='newrow'   ></tr>").append("" +
                                "<td height='22'  >"+item.AcceDate+"</td>" +
                                "<td height='22'  >"+item.InComeNo+"</td>" +
                                "<td height='22'  >"+item.custname+"</td>" +
                                "<td height='22'  >"+item.currencyname+"</td>" +
                                "<td height='22'  >"+item.paymenttypes+"</td>" +
                                "<td height='22'  >"+item.acceway+"</td>" +
                                "<td height='22'  >"+item.totalprice+"</td>" +
                                "<td height='22' id=\"td"+rowID+"\" >"+item.naccounts+"</td>" +
                                "<td height='22' style=\"background:#FFFFFF\"  ><input type='hidden'  id=\"payBillID"+rowID+"\" value=\""+item.id+"\" /><input type='hidden'  id=\"payNo"+rowID+"\" value=\""+item.InComeNo+"\" /><input type='hidden'  id=\"tdPayAmount"+rowID+"\" value=\""+item.totalprice+"\" />"+
                                "<input type='hidden'  id=\"tdNAccounts"+rowID+"\" value=\""+item.naccounts+"\" /><input type=\"text\" id='TD_Text_price_" + rowID + "' value=\"0.00\"  class=\"tdinput\" size=\"10\" onfocus=\"GetPrice('TD_Text_price_" + rowID + "');\" onblur=\"GetAllPrice('TD_Text_price_" + rowID + "');\"  /></td>").appendTo($("#dg_Log tbody"));
                                
                                rowID++;
                    }
                    
                });
                  $("<tr id=\"Item_Row_"+rowID+"\" class='newrow'   ></tr>").append("" +
                                "<td height='22'  style=\"background:#FFFFFF\" >合计</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\" ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  >"+total1.toFixed(2)+"</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  >"+total2.toFixed(2)+"</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ><input type=\"text\" id='TD_Text_price' value=\"0.00\" readonly='readonly' class=\"tdinput\" size=\"10\"  /></td>").appendTo($("#dg_Log tbody"));
            },
            error: function() { popMsgObj.ShowMsg('请求发生错误'); },
           complete: function() {  $("#dg_Log_Pager").show();dg_Log("dg_Log","#E7E7E7", "#FFFFFF", "#cfc", "cfc");} //接收数据完毕
        });
   Fun_InvoiceBill();
    
}
//获取光标前的值
var price=0;
var billprice=0;
function GetPrice(text)
{
    var rows=text.split('_');
    if(rows[2]=="price")
    {
        price=parseFloat(document.getElementById(text).value);
    }
    else
    {
        billprice=parseFloat(document.getElementById(text).value);
     }
}
//本次结算总金额
function GetAllPrice(text)
{
    var rows=text.split('_');
    if(rows[2]=="price")
    {
        var total=parseFloat(document.getElementById("TD_Text_price").value);
        var thisprice=0;
        var allprice=parseFloat(document.getElementById("td"+rows[3]).innerText);
        thisprice=parseFloat(document.getElementById(text).value);
        if(thisprice.toString()=="NaN")
        {
            thisprice=0;
            document.getElementById(text).value=price.toFixed(2);
            return;
        }
        if(thisprice>allprice)
        {
            popMsgObj.ShowMsg('结算金额必须小于收款余额');
            document.getElementById(text).value=price.toFixed(2);
            document.getElementById(text).focus();
            return;
        }
        total+=thisprice-price;
        document.getElementById(text).value=parseFloat(thisprice).toFixed(2);
        document.getElementById("TD_Text_price").value=total.toFixed(2);
    }
    else
    {
        var total=parseFloat(document.getElementById("TD_price").value);
        var thisprice=0;
        var allprice=parseFloat(document.getElementById("bill_td"+rows[3]).innerText);
        thisprice=parseFloat(document.getElementById(text).value);
        if(thisprice.toString()=="NaN")
        {
            thisprice=0;
            document.getElementById(text).value=billprice.toFixed(2);
            return;
        }
        if(thisprice>allprice)
        {
            popMsgObj.ShowMsg('本次结算金额必须小于原单余额');
            document.getElementById(text).value=billprice.toFixed(2);
            document.getElementById(text).focus();
            return;
        }
        total+=thisprice-billprice;
        document.getElementById(text).value=parseFloat(thisprice).toFixed(2);
        document.getElementById("TD_price").value=total.toFixed(2); 
    }
}
//检索票据
function Fun_InvoiceBill()
{
   if(document.getElementById("txtCustName").value=="")
   {
         popMsgObj.Show("请选择|", "客户信息|");
         return;
   }
   var Custid="";
   var rowID=0;
   var total1=0;
   var total2=0;
   Custid=$("#CustID").val();
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/FinanceManager/ReceiveVerifiction.ashx', //目标地址
            cache: false,
            data: "Custid="+Custid+"&check=1", //数据
            beforeSend: function() {  $("#TB_Invoice_Pager").hide();  }, //发送数据之前
            success: function(msg) {

                //数据获取完毕，填充页面据显示
                //数据列表
                $("#TB_Invoice tbody").find("tr.newrow").remove();
 
                $.each(msg.data, function(i, item) {
                  
                    if (item.id != null && item.id != "") {
                        if(parseFloat(item.TotalPrice)!=0)
                        {
                            total1+=parseFloat(item.TotalPrice);
                        }
                        if(parseFloat(item.NAccounts)!=0)
                        {
                            total2+=parseFloat(item.NAccounts);
                        }
                        $("<tr id=\"Item_Row1_"+rowID+"\" class='newrow'   ></tr>").append("" +
                                "<td height='22'  >"+item.createdate+"</td>" +
                                "<td height='22'  >"+item.InvoiceType+"</td>" +
                                "<td height='22'  >"+item.BillingNum+"</td>" +
                                "<td height='22'  >"+item.custname+"</td>" +
                                "<td height='22'  >"+item.billcd+"</td>" +
                                "<td height='22'  >"+item.currencyname+"</td>" +
                                "<td height='22'  >"+item.TotalPrice+"</td>" +
                                "<td height='22' id=\"bill_td"+rowID+"\" >"+item.NAccounts+"</td>" +
                                "<td height='22' style=\"background:#FFFFFF\"  ><input type='hidden'  id=\"billingID"+rowID+"\" value=\""+item.id+"\" /><input type='hidden'  id=\"tdTotalPrice"+rowID+"\" value=\""+item.TotalPrice+"\" /><input type='hidden'  id=\"tdNAccountsBilling"+rowID+"\" value=\""+item.NAccounts+"\" />"+
                                "<input type=\"text\" id='TD_Text_price1_" + rowID + "' value=\"0.00\"  class=\"tdinput\" size=\"10\" onfocus=\"GetPrice('TD_Text_price1_" + rowID + "');\" onblur=\"GetAllPrice('TD_Text_price1_" + rowID + "');\"  /></td>").appendTo($("#TB_Invoice tbody"));
                                
                                rowID++;
                    }
                    
                });
                  $("<tr id=\"Item_Row1_"+rowID+"\" class='newrow'   ></tr>").append("" +
                                "<td height='22'  style=\"background:#FFFFFF\" >合计</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\" ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ></td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  >"+total1.toFixed(2)+"</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  >"+total2.toFixed(2)+"</td>" +
                                "<td height='22'  style=\"background:#FFFFFF\"  ><input type=\"text\" id='TD_price' value=\"0.00\" readonly='readonly' class=\"tdinput\" size=\"10\"  /></td>").appendTo($("#TB_Invoice tbody"));
            },
            error: function() { popMsgObj.ShowMsg('请求发生错误'); },
           complete: function() {  $("#TB_Invoice_Pager").show();dg_Log("TB_Invoice","#E7E7E7", "#FFFFFF", "#cfc", "cfc");} //接收数据完毕
        });

}
//保存单据
function SaveBills()
{
    if(!checkprice())
    {
        
        return;
    }
    var UrlParms="check=2";    
    var PayBillID=new Array();   
    var PayNo=new Array();                     
    var PayAmount=new Array();            
    var NAccounts=new Array();
    var SettleAmount=new Array(); 
   
    var BillingID=new Array();                        
    var PayAmountBilling=new Array();            
    var NAccountsBilling=new Array();
    var SettleAmountBilling=new Array(); 
                                                  
    var signFrame = findObj("dg_Log", document);
    var signFrameBlend = findObj("TB_Invoice", document);
    if(signFrame!=null && signFrame!=undefined && signFrameBlend!=null && signFrameBlend!=undefined)
    {
        //付款单 
        for (i = 0; i < signFrame.rows.length-1; i++) 
        {
            if (signFrame.rows[i].style.display != "none") 
            {        
                if(parseFloat($("#TD_Text_price_"+i).val()) > 0)
                {
                    PayBillID.push($("#payBillID"+i).val());  
                    PayNo.push($("#payNo"+i).val());  
                    PayAmount.push($("#tdPayAmount"+i).val());  
                    NAccounts.push($("#tdNAccounts"+i).val()); 
                    SettleAmount.push($("#TD_Text_price_"+i).val()); 
                }                                                               
            }        
        } 
        UrlParms+="&PayBillID="+escape(PayBillID)+"&PayNo="+escape(PayNo)+"&PayAmount="+escape(PayAmount)+"&NAccounts="+escape(NAccounts)+"&SettleAmount="+escape(SettleAmount);
        
        //核销单
        for (i = 0; i < signFrameBlend.rows.length-2; i++) 
        {
            if (signFrameBlend.rows[i].style.display != "none") 
            {      
                if(parseFloat($("#TD_Text_price1_"+i).val()) > 0)
                {
                    BillingID.push($("#billingID"+i).val());   
                    PayAmountBilling.push($("#tdTotalPrice"+i).val());  
                    NAccountsBilling.push($("#tdNAccountsBilling"+i).val()); 
                    SettleAmountBilling.push($("#TD_Text_price1_"+i).val()); 
                }                                                                 
            }        
        } 
        UrlParms+="&BillingID="+escape(BillingID)+"&PayAmountBilling="+escape(PayAmountBilling)+"&NAccountsBilling="+escape(NAccountsBilling)+"&SettleAmountBilling="+escape(SettleAmountBilling);
        
        $.ajax({ 
                      type: "POST",
                      url: "../../../Handler/Office/FinanceManager/ReceiveVerifiction.ashx",
                      dataType:'json',//返回json格式数据
                      cache:false,
                      data: UrlParms, //数据
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
                                Fun_IncomeBill();
                            }
                            else
                            {
                                popMsgObj.ShowMsg(data.info);
                            }
                    } 
        }); 
        }
}
//判断结算总金额是否相等
function checkprice()
{
    
    var Inprice=$("#TD_Text_price").val();//document.getElementById("TD_Text_price").value;
    var Reprice=$("#TD_price").val();//document.getElementById("TD_price").value;
    if(Inprice==""||Inprice==undefined)
    {
         popMsgObj.ShowMsg("请先进行检索!");
         return;
    }
     if(parseFloat(Inprice)==0){
            popMsgObj.ShowMsg("请输入结算金额!");
            return;
        }
    if(parseFloat(Inprice)==parseFloat(Reprice))
        return true;
    else
    {
        popMsgObj.ShowMsg('结算总金额必须等于核销总金额');
        return false;
    }
}
