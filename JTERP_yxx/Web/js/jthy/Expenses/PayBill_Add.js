$(document).ready(function() {

    requestobj = GetRequest();
    var mytemp=requestobj['intMasterID'];
    var mytemp2=requestobj['PayNo'];
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        document.getElementById("hiddOrderID").value = document.getElementById("headid").value;
        GetInfoById(document.getElementById("headid").value);
    }
});

function GetInfoById(headid) {

    //var ID=document.getElementById("hideId").value;
    var action = "LoadIncomBill";
    var orderBy = "id";
    //$("#divCodeRule").css("display", "none");
    //$("#txtContractID").css("display", "block");

    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/PayBill_Add.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            var j = 1;
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {

                    document.getElementById("divCodeRule").style.display="none";
                    document.getElementById("divIncomeNo").style.display="block";
                    document.getElementById("divIncomeNo").innerHTML = item.PayNo;//赋值
                    
                    document.getElementById("txtAcceDate").value = item.PayDate;
                    document.getElementById("txtCustName").value = item.CustName;

                    document.getElementById("hideId").value = item.id;//id的获取赋值
                    document.getElementById("txtOrder").value = item.FromBillType;
                    document.getElementById("BillingType").value = item.BillingID;
                    document.getElementById("txtTotalPrice").value = item.PayAmount;
                    document.getElementById("PaymentTypes").value = item.PaymentType;
                    document.getElementById("DrpAcceWay").value = item.AcceWay;
                    document.getElementById("UsertxtExcutor").value = item.Executor;
                    document.getElementById("txtAccountDate").value = item.AccountDate;
                    document.getElementById("txtAccountor").value = item.Accountor;

                    document.getElementById("txtConfirmDate").value = item.ConfirmDate;
                    document.getElementById("txtConfirmor").value = item.Confirmor;
                    
                     var temp4=item.ConfirmStatus;
                    if(item.ConfirmStatus==1)
                    {
                        document.getElementById("btnIncomeBill_Save").src="../../../images/Button/UnClick_bc.jpg";
                            document.getElementById("btnConfirm").src="../../../images/Button/UnClick_qr.jpg";
                            document.getElementById("btnReConfirm").src="../../../images/Button/Main_btn_fqr.jpg";
                            document.getElementById("btnIncomeBill_Save").disabled=true;
                            document.getElementById("btnConfirm").disabled=true;
                            document.getElementById("btnReConfirm").disabled=false;
                    
                    }
                    else
                    {
                        document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
                        document.getElementById("btnConfirm").src="../../../images/Button/Bottom_btn_confirm.jpg";
                        document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
                        document.getElementById("btnIncomeBill_Save").disabled=false;
                        document.getElementById("btnConfirm").disabled=false;
                        document.getElementById("btnReConfirm").disabled=true;

                    }

                    $("#txtOprtID").val('saveOk');
                    

                    var glb_BillTypeFlag = "30";
                    var glb_BillTypeCode = "1";
                    var glb_BillID = document.getElementById('headid').value;                                //单据ID
                    var glb_IsComplete = false;                                          //是否需要结单和取消结单(小写字母)
                    var FlowJS_HiddenIdentityID = 'hiddOrderID';                      //自增长后的隐藏域ID
                    var FlowJs_BillNo = 'txtContractID';          //当前单据编码名称
                    var FlowJS_BillStatus = 'hidBillStatus';


                }
            });
                document.getElementById("txtAction").value="2";//改变其值，不等于1的时候修改            

        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
        complete: function() { } //接收数据完毕
    });
}





///反确认
function ReconfirmIncomeBill()
{
        var ID=document.getElementById("hideId").value;
        
        var PayNo=null;
        var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
        PayNo=document.getElementById("divIncomeNo").innerHTML;
        if(PayNo=="")
        {
           if (CodeType == "")
           {
              PayNo=$("#CodingRuleControl1_txtCode").val();
           }
        }
        var PayDate=document.getElementById("txtAcceDate").value;//付款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var PayAmount=document.getElementById("txtTotalPrice").value;//付款金额
         
         var PaymentType=document.getElementById("PaymentTypes").value;//款项类型
         var AcceWay=document.getElementById("DrpAcceWay").value;//付款方式
         var Executor=document.getElementById("txtSaveUserID").value;//执行人
                 
         //获取当前时间
         var myDate = new Date();
         var getConfirmTime=myDate.toLocaleDateString();
         
         //附加信息
         var AccountDate=document.getElementById("txtAccountDate").value;//
         var Accountor=document.getElementById("txtAccountor").value;//
         var ConfirmDate=document.getElementById("txtConfirmDate").value;//
         var Confirmor=document.getElementById("txtConfirmor").value;//
         
         //添加验证
         if(!CheckInput())return;//验证其他
         
        
        //设置参数
        var UrlParms="Action="+"update";
        UrlParms+="&ID="+ID;
        UrlParms+="&PayNo="+PayNo;//获取付款单号
        UrlParms+="&PayDate="+PayDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&PayAmount="+PayAmount;//
        
        
        UrlParms+="&PaymentType="+PaymentType;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                error: function() {
                  popMsgObj.ShowMsg('请求发生错误');
                }, 
                success:function(data) 
                { 
                        if(data.sta>0) 
                        {
                            popMsgObj.ShowMsg(data.data);
                            //对图片进行控制
                            document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
                            document.getElementById("btnConfirm").src="../../../images/Button/UnClick_qr.jpg";
                            document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
                            document.getElementById("btnIncomeBill_Save").disabled=false;
                            document.getElementById("btnConfirm").disabled=false;
                            document.getElementById("btnReConfirm").disabled=true;
                            
                            //document.getElementById("txtAccountDate").value=AcceDate;//.val("2014-7-9");//"AccountDate";
                            //document.getElementById("txtAccountor").value="管理员";//.innerHTML="管理员";//Accountor;
                            document.getElementById("txtConfirmDate").value="";//.innerHTML="2014-7-9";//ConfirmDate;
                            document.getElementById("txtConfirmor").value="";//.innerHTML="管理员";;//Confirmor;
                        }
                        else
                        {
                            popMsgObj.ShowMsg("操作失败");
                        }
                } 
           });  
}

//确认
function ConfirmIncomeBill()
{
        var ID=document.getElementById("hideId").value;
        
        var PayNo=null;
        var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
        PayNo=document.getElementById("divIncomeNo").innerHTML;
        if(PayNo=="")
        {
           if (CodeType == "")
           {
              PayNo=$("#CodingRuleControl1_txtCode").val();
           }
        }
        var PayDate=document.getElementById("txtAcceDate").value;//付款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="1";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var PayAmount=document.getElementById("txtTotalPrice").value;//付款金额
         
         var PaymentType=document.getElementById("PaymentTypes").value;//款项类型
         var AcceWay=document.getElementById("DrpAcceWay").value;//付款方式
         var Executor=document.getElementById("txtSaveUserID").value;//执行人
                 
         //获取当前时间
         var myDate = new Date();
         var getConfirmTime=myDate.toLocaleDateString();
         
         //附加信息
         var AccountDate=document.getElementById("txtAccountDate").value;//
         var Accountor=document.getElementById("txtAccountor").value;//
         var ConfirmDate=document.getElementById("txtConfirmDate").value;//
         var Confirmor=document.getElementById("txtConfirmor").value;//
         
         //添加验证
       if(!CheckInput())return;//验证其他
         
        
        //设置参数
        var UrlParms="Action="+"confirm";
        UrlParms+="&ID="+ID;
        UrlParms+="&PayNo="+PayNo;//获取付款单号
        UrlParms+="&PayDate="+PayDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&PayAmount="+PayAmount;//
        
        
        UrlParms+="&PaymentType="+PaymentType;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                error: function() {
                  popMsgObj.ShowMsg('请求发生错误');
                }, 
                success:function(data) 
                { 
                        if(data.sta>0) 
                        {
                            popMsgObj.ShowMsg(data.data);
                            //对图片进行控制
                            document.getElementById("btnIncomeBill_Save").src="../../../images/Button/UnClick_bc.jpg";
                            document.getElementById("btnConfirm").src="../../../images/Button/UnClick_qr.jpg";
                            document.getElementById("btnReConfirm").src="../../../images/Button/Main_btn_fqr.jpg";
                            document.getElementById("btnIncomeBill_Save").disabled=true;
                            document.getElementById("btnConfirm").disabled=true;
                            document.getElementById("btnReConfirm").disabled=false;
                            
                            var temp3=document.getElementById("jiandangren2").value;//--------------2014-7-15  1
                            document.getElementById("txtConfirmor").value=temp3;//--------------2014-7-15  1
                            document.getElementById("txtConfirmDate").value=getConfirmTime;//.innerHTML="2014-7-9";//ConfirmDate;
                        }
                        else
                        {
                            popMsgObj.ShowMsg("操作失败");
                        }
                } 
           });  
   
}




function DoPrint()
{ 
  if(document.getElementById("txtOprtID").value=="")
  {
     popMsgObj.ShowMsg("请保存后打印");
     return;
  }  
  window.open("../../PrinttingModel/FinanceManager/InComeBillingAdd.aspx?Id=" + document.getElementById("txtOprtID").value);
}

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
   
   $(document).ready(function(){
        btntype();
   });
   
      /*
* 返回业务单列表
*/
function DoBack()
{

    var CashOrBankType=document.getElementById("hidCashOrBankType").value;
   //获取查询条件
   var searchCondition = document.getElementById("hidSearchCondition").value;
   if(CashOrBankType=="")
   {
        //获取模块功能ID
        var ModuleID = document.getElementById("hidModuleID").value;
        window.location.href = "IncomeBillList.aspx?ModuleID=" + ModuleID + searchCondition;
   }
   else if(CashOrBankType=="1")
   {
         //获取模块功能ID
        var ModuleID = document.getElementById("hidCashModuleID").value;
        window.location.href = "CashAccount.aspx?ModuleID=" + ModuleID + searchCondition+"&Flag=1";
   }
   else if(CashOrBankType=="2")
   {
         //获取模块功能ID
        var ModuleID = document.getElementById("hidBankModuleID").value;
        window.location.href = "BankAccount.aspx?ModuleID=" + ModuleID + searchCondition+"&Flag=1";
   }
   
}

    function changes()
    {
        //var billtype=parseFloat(document.getElementById("BillingType").value);
        //document.getElementById("txtCustName").value="";
        //document.getElementById("txtOrder").value="";
        document.getElementById("txtTotalPrice").value="";
    }
   //选择业务单
   function SelectBilling()
   {
     var Confirmstatus=document.getElementById("txtConfirmStatus").value;
     if(Confirmstatus=="已确认") return;
     var billtype=document.getElementById("BillingType").value;
      var url="../../../Pages/Office/FinanceManager/SelectBillingList.aspx?type=2&BillingType="+billtype;
      var returnValue = window.showModalDialog(url, "", "dialogWidth=900px;dialogHeight=450px");
      if(typeof(returnValue) != "undefined" )
      { 
        document.getElementById("txtTotalPrice").value="";
        document.getElementById("txtNAccounts").value="";
         returnValue=returnValue.split("|");
        document.getElementById("txtSaveID").value=returnValue[0].toString();
        document.getElementById("txtOrder").value=returnValue[1].toString();
        document.getElementById("txtCustName").value=returnValue[2].toString();
        var AcceWay=returnValue[3].toString()
        document.getElementById("txtTotalPrice").value=returnValue[4].toString().replace(/,/g,"");
        document.getElementById("txtNAccounts").value=returnValue[4].toString().replace(/,/g,"");
        
        
        //document.getElementById("txtCurryType").value=returnValue[5].toString();
        //document.getElementById("hiddenCurryTypeID").value=returnValue[6].toString();
        document.getElementById("txtExchangeRate").value=parseFloat(returnValue[7].toString()).toFixed(2);
        document.getElementById("txtExchangeRate").disabled=true;
        
        /*往来单位隐藏域*/
         document.getElementById("CustID").value=returnValue[8].toString();
         document.getElementById("FromTBName").value=returnValue[9].toString();
         document.getElementById("FileName").value=returnValue[10].toString();
            
            
        if(AcceWay=="现金")
        {
          document.getElementById("DrpAcceWay").value="0";
          
          //document.getElementById("ddlBankName").disabled=true;
          //document.getElementById("txtBanlNo").disabled = true;
        }
        else
        {
          document.getElementById("DrpAcceWay").value="1";
          //document.getElementById("ddlBankName").disabled=false;
          //document.getElementById("txtBanlNo").disabled = false;
        }
      }

   }
   
     
   //选择科目
   function SelectAccounts()
   {
       var Way=document.getElementById("DrpAcceWay").value;
       if(Way!="0")
       {
       var url="../../../Pages/Office/FinanceManager/SubjectsList.aspx?IsShow=Show";
       var returnValue = window.showModalDialog(url, "", "dialogWidth=700px;dialogHeight=500px");
       if(returnValue!="" && returnValue!=null)
       {
          //document.getElementById("ddlBankName").value="";
          var info=returnValue.split("|");
          //document.getElementById("ddlBankName").value=info[1].toString();
       }
       }
   }
   
   
   
   //付款接受方式判断
   function AcceWay()
   {
     var Way=document.getElementById("DrpAcceWay").value;
 
//     if(Way!="0")
//     {
//        document.getElementById("ddlBankName").disabled=false;
//        document.getElementById("txtBanlNo").disabled=false; 
//     }
//     else
//     {
//        document.getElementById("ddlBankName").disabled=true;
//        document.getElementById("txtBanlNo").disabled=true;
//     }
     

     
   }
   //添加付款信息（simple）
   function InsertIBI(){
         var ID =document.getElementById("hideId").value;
         var Action=null;
         if(document.getElementById("txtAction").value=="1")//--------------
         {
           Action="insert";
         }
         else
         {

           Action="Edit";
         }
        //理想化，页面值获取值
        //获取付款单号,查看其数据
        var IncomeNo=null;
        var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
        PayNo=document.getElementById("divIncomeNo").innerHTML;
        if(PayNo=="")
        {
           if (CodeType == "")
           {
              PayNo=$("#CodingRuleControl1_txtCode").val();
           }
        }
        var PayDate=document.getElementById("txtAcceDate").value;//付款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="1";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var PayAmount=document.getElementById("txtTotalPrice").value;//付款金额
         
         var PaymentType=document.getElementById("PaymentTypes").value;//款项类型
         var AcceWay=document.getElementById("DrpAcceWay").value;//付款方式
         var Executor=document.getElementById("txtSaveUserID").value;//执行人
                 
         //获取保存时间
         var myDate = new Date();
         var myTime=myDate.toLocaleDateString();
         
         //附加信息
         var AccountDate=myTime;//
         var Accountor=document.getElementById("txtAccountor").value;//
         var ConfirmDate=document.getElementById("txtConfirmDate").value;//
         var Confirmor=document.getElementById("txtConfirmor").value;//
         
         //添加验证
         if(!CheckInput())return;//验证其他
        
        //设置参数
        var UrlParms="Action="+Action;
        UrlParms+="&ID="+ID;
        UrlParms+="&PayNo="+PayNo;//获取付款单号
        UrlParms+="&PayDate="+PayDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&PayAmount="+PayAmount;//
        
        
        UrlParms+="&PaymentType="+PaymentType;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
                  dataType:'json',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                error: function() {
                  popMsgObj.ShowMsg('请求发生错误');
                }, 
                success:function(data) 
                { 
                        
                        if(data.sta!=0) 
                        {
                            if(data.sta==-1){
                               popMsgObj.ShowMsg(data.data);
                            }
                            else
                            if(data.sta==-2){//当为-2的时候，为编辑显示//--------------
                                 popMsgObj.ShowMsg(data.data);
                                //对图片进行控制
                                document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
                                document.getElementById("btnConfirm").src="../../../images/Button/Bottom_btn_confirm.jpg";
                                document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
                                document.getElementById("btnIncomeBill_Save").disabled=false;
                                document.getElementById("btnConfirm").disabled=false;
                                document.getElementById("btnReConfirm").disabled=true;
                                document.getElementById("txtAction").value="2";//改变其值，不等于1的时候修改    
                                
                                //该数据如何从程序中获取
                                var temp3=document.getElementById("jiandangren").value;//--------------
                                document.getElementById("jiandangren2").value=temp3;//--------------2014-7-15  1
                                document.getElementById("txtAccountor").value=temp3;//从页面的隐藏控件进行传值
                            
                            }
                            
                            else{
                            popMsgObj.ShowMsg(data.data);
                            //对图片进行控制
                            document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
                            document.getElementById("btnConfirm").src="../../../images/Button/Bottom_btn_confirm.jpg";
                            document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
                            document.getElementById("btnIncomeBill_Save").disabled=false;
                            document.getElementById("btnConfirm").disabled=false;
                            document.getElementById("btnReConfirm").disabled=true;
                            //将数据的id保存到隐藏的空间里
                            document.getElementById("hideId").value=data.sta;
                            
                            document.getElementById("divIncomeNo").innerHTML=PayNo;//--------------

                            document.getElementById("divIncomeNo").style.display="block";//--------------

                            document.getElementById("divCodeRule").style.display="none";
                            document.getElementById("txtAction").value="2";//改变其值，不等于1的时候修改//--------------
                            
                            //显示附加信息
                            //$("txtAccountDate").val("2014-7-9");
                            document.getElementById("txtAccountDate").value=myTime;//.val("2014-7-9");//"AccountDate";
                            //该数据如何从程序中获取
                            var temp3=document.getElementById("jiandangren").value;//--------------
                            document.getElementById("jiandangren2").value=temp3;//--------------2014-7-15  1
                            document.getElementById("txtAccountor").value=temp3;//从页面的隐藏控件进行传值
                            }
                            
                        }
                        else
                        {
                            popMsgObj.ShowMsg("操作失败");
                        }
                } 
           });  
        
   
   
   }
   
   //添加付款信息
   function InsertIncomeBillInfo()
   {
    var rValue=document.getElementById("HiddenIsSubjectsBgein").value;
    
    //if(rValue=="")//修改一下，先执行下一步操
    if(false)
    {
       popMsgObj.ShowMsg('科目期初值未设置！请设置科目期初值后添加付款单');
       return ;
    }
    else
    {
        //验证时间
        var rDate=document.getElementById("txtAcceDate").value;
       
        var rValueStr=rValue.split("-");
        var DeprDateYear =rValueStr[0].toString();
        var DeprDateMoth =rValueStr[1].toString();
        if (parseInt(DeprDateMoth) <= 9)
        {
            DeprDateMoth = "0" + DeprDateMoth;
        }
        var DeprDate=DeprDateYear+"-"+DeprDateMoth+"-01";
        
        if(!compareDate(DeprDate,rDate))
        {                    
           popMsgObj.ShowMsg('付款单付款日期应大于科目期初值设置期数！');
           return ;
       }
    }
    if(!CheckInput())return;//验证其他
    
//    var IncomeNo=null;
//    var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
//    IncomeNo=document.getElementById("divIncomeNo").innerHTML;
//    if(IncomeNo=="")
//    {
//       if (CodeType == "")
//       {
//          IncomeNo=$("#CodingRuleControl1_txtCode").val();
//       }
//    }
     var Action=null;
     if(document.getElementById("txtAction").value=="1")
     {
       Action="Add";
     }
     else
     {

       Action="Edit";
     }
     //=============================================
     var CustName=document.getElementById("txtCustName").value;
     var TotalPricee=document.getElementById("txtTotalPrice").value;
     document.getElementById("price").value=TotalPricee;
     var AcceWay=document.getElementById("DrpAcceWay").value;
     //var BankName=document.getElementById("ddlBankName").value;
     //var BankNo=document.getElementById("txtBanlNo").value;
     var Excutor=document.getElementById("txtSaveUserID").value;
     var Summary=document.getElementById("txtRemark").value;
     var BillingID=document.getElementById("txtSaveID").value;
     var AcceDate=document.getElementById("txtAcceDate").value;
     var OprtID=document.getElementById("txtOprtID").value;
     //var OldPrice=document.getElementById("txtOldPrice").value;
     var ExchangeRate=document.getElementById("txtExchangeRate").value;
     //var CurryType=document.getElementById("hiddenCurryTypeID").value;
     var CustID=document.getElementById("CustID").value;
     var FromTBName=document.getElementById("FromTBName").value;
     var FileName = document.getElementById("FileName").value;
     var ProjectID = document.getElementById("hidProjectID").value;
     var PaymentTypes=document.getElementById("PaymentTypes").value;
     if(OldPrice=="undefined")
     {
       OldPrice="";
     }
     var rblBlengding="";
     if(BillingID==""||BillingID=="0")
     {
      rblBlengding="9";
     }
     
        var UrlParms="Action="+escape(Action);
        UrlParms+="&CustName="+escape(CustName);
        UrlParms+="&TotalPricee="+escape(TotalPricee);
        UrlParms+="&AcceWay="+escape(AcceWay);
        UrlParms+="&BankName="+escape(BankName);
        UrlParms+="&BankNo="+escape(BankNo);
        UrlParms+="&Excutor="+escape(Excutor);
        UrlParms+="&Summary="+escape(Summary);
        UrlParms+="&BillingID="+escape(BillingID);
        UrlParms+="&AcceDate="+escape(AcceDate);
        UrlParms+="&IncomeNo="+escape(IncomeNo);
        UrlParms+="&CodeType="+escape(CodeType);
        UrlParms+="&OprtID="+escape(OprtID);
        UrlParms+="&OldPrice="+escape(OldPrice);
        UrlParms+="&rblBlengding="+escape(rblBlengding);
        UrlParms+="&PayOrInComeType=2";
        UrlParms+="&BlendingType="+escape(rblBlengding);
        UrlParms+="&ExchangeRate="+escape(ExchangeRate);
        //UrlParms+="&CurryType="+escape(CurryType);
        UrlParms+="&CustID="+escape(CustID);
        UrlParms+="&FromTBName="+escape(FromTBName);
        UrlParms += "&FileName=" + escape(FileName);
        UrlParms += "&ProjectID=" + escape(ProjectID);
        UrlParms += "&PaymentTypes=" + escape(PaymentTypes);
        
        
      
        
        
         var tab=document.getElementById("BlendingTB"); 
         tab=null;
         if(tab!=null&&tab!=undefined)
         {
            var blendingValue="";
            var BillingID=document.getElementById("txtSaveID").value;
         
              var SourceDt=new Array();
              var SourceID=new Array();
              var BillCD=new Array();
              var BillingType=new Array();
              var InvoiceType=new Array();
              var TotalPrice=new Array();
              var YAccounts=new Array();
              var NAccounts=new Array();
              var ContactUnits=new Array();
              var Status=new Array();
              var CreateDate=new Array();
              var BlendingID=new Array();
              var NowAmount=new Array();
               var CurrencyType=new Array();
              var CurrencyRate=new Array();
            
              var tbValue=0; 
              for(var i=0;i<tab.rows.length-1;i++)   
              {   
                  var yamount=document.getElementById("ThisAccounts"+i+"").value;
                  if(yamount=="")
                  {
                    yamount=0;
                  }
                  tbValue+=parseFloat(yamount); 
                  SourceDt.push(document.getElementById("SourceDt"+i+"").value);   
                  SourceID.push(document.getElementById("SourceID"+i+"").value);  
                  BillCD.push(document.getElementById("BillCD"+i+"").value);  
                  BillingType.push(document.getElementById("hiddenBillingType"+i+"").value);  
                  InvoiceType.push(document.getElementById("hiddenInvoiceType"+i+"").value); 
                  TotalPrice.push(document.getElementById("TotalPrice"+i+"").value); 
                  CurrencyType.push(document.getElementById("CurrencyType"+i+"").value);
                  CurrencyRate.push(document.getElementById("Rate"+i+"").value);
                  var ThisMoney=document.getElementById("ThisAccounts"+i+"").value;
                  BlendingID.push(document.getElementById("BlendingID"+i+"").value);
                  if(ThisMoney=="")
                  {
                     ThisMoney=0;
                  }
                  NowAmount.push(ThisMoney); 
                  var YMoney=document.getElementById("YAccounts"+i+"").value;
                  if(YMoney=="")
                  {
                    YMoney=0;
                  }
                  var NMoney=document.getElementById("NAccounts"+i+"").value;
                  if(NMoney=="")
                  {
                    NMoney=0;
                  }
                  YAccounts.push(parseFloat(YMoney)+parseFloat(ThisMoney));
                  NAccounts.push(parseFloat(NMoney)-parseFloat(ThisMoney)); 
                  ContactUnits.push(document.getElementById("ContactUnits"+i+"").value);
                  CreateDate.push(document.getElementById("CreateDate"+i+"").value);
                  if(parseFloat(NMoney)-parseFloat(ThisMoney)==0)
                  {
                    Status.push("1");
                  } 
                  else
                  {
                    Status.push("0");
                  }  
              }
              
              if(TotalPricee!=tbValue)
              {
                 popMsgObj.ShowMsg('勾兑金额合计必须和付款总金额相等');
                 return ;
              }
              else
              {
                 
                 UrlParms+="&SourceDt="+escape(SourceDt);
                 UrlParms+="&SourceID="+escape(SourceID);
                 UrlParms+="&BillCD="+escape(BillCD);
                 UrlParms+="&BillingType="+escape(BillingType);
                 UrlParms+="&InvoiceType="+escape(InvoiceType);
                 UrlParms+="&TotalPrice="+escape(TotalPrice);
                 UrlParms+="&YAccounts="+escape(YAccounts);
                 UrlParms+="&NAccounts="+escape(NAccounts);
                 UrlParms+="&ContactUnits="+escape(ContactUnits);
                 UrlParms+="&Status="+escape(Status);
                 UrlParms+="&CreateDate="+escape(CreateDate);
                 UrlParms+="&BlendingID="+escape(BlendingID);
                 UrlParms+="&NowAmount="+escape(NowAmount);
                 UrlParms+="&CurrencyRate="+escape(CurrencyRate);
                 UrlParms+="&CurrencyType="+escape(CurrencyType);
                 
              $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
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
                            var Info=data.data;
                            Info=Info.split("|");
                            if(document.getElementById("txtAction").value=="1")
                            {
                             document.getElementById("txtOprtID").value=Info[0].toString();
                             if(CodeType!="")
                             {
                               document.getElementById("divIncomeNo").innerHTML=Info[1].toString();
                             }
                             else
                             {
                               document.getElementById("divIncomeNo").innerHTML=IncomeNo;
                             }
                             }
                            document.getElementById("txtIncomePrice").value=TotalPrice;
                            document.getElementById("divIncomeNo").style.display="block";
                            document.getElementById("divCodeRule").style.display="none";
                            document.getElementById("txtAction").value="2";
                            //BlendingBind();
                            
                        }
                        else
                        {
                            popMsgObj.ShowMsg(data.info);
                        }
                } 
           });  
                 
              }  
      
        }
        else if(tab==null||tab==undefined||rblBlengding=="0")
        {
             $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
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
                            var Info=data.data;
                            Info=Info.split("|");
                            if(document.getElementById("txtAction").value=="1")
                            {
                             document.getElementById("txtOprtID").value=Info[0].toString();
                             if(CodeType!="")
                             {
                               document.getElementById("divIncomeNo").innerHTML=Info[1].toString();
                             }
                             else
                             {
                               document.getElementById("divIncomeNo").innerHTML=IncomeNo;
                             }
                             }
                            document.getElementById("txtIncomePrice").value=TotalPrice;
                            document.getElementById("divIncomeNo").style.display="block";
                            document.getElementById("divCodeRule").style.display="none";
                            document.getElementById("txtAction").value="2";
                            //BlendingBind();
                            btntype();
                            document.getElementById("txtConfirmStatus").value="未确认";
                            
                        }
                        else
                        {
                            popMsgObj.ShowMsg(data.info);
                        }
                } 
           });  
        } 
   }
   
    function CheckInput()
   {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;    
        var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
        var AcceDate=document.getElementById("txtAcceDate").value;
        var CustName=document.getElementById("txtCustName").value;
        var TotalPrice=document.getElementById("txtTotalPrice").value;
        //未付金额
        var Way=document.getElementById("DrpAcceWay").value;
        var Order=document.getElementById("txtOrder").value;
        if (CodeType == "")
        {
            var IncomeNo=null;
            var CodeType=$("#CodingRuleControl1_ddlCodeRule").val();
            IncomeNo=document.getElementById("divIncomeNo").innerHTML;
            if(IncomeNo=="")
            {
               if (CodeType == "")
               {
                  IncomeNo=$("#CodingRuleControl1_txtCode").val();
               }
            }
        
           if(strlen(IncomeNo)<=0)
           {
            isFlag = false;
            fieldText = fieldText + "付款单编号|";
   		    msgText = msgText +  "请输入付款单编号|";
           }
           else
           {
              if(!CodeCheck(IncomeNo))
              {
               isFlag = false;
               fieldText = fieldText + "付款单编号|";
   		       msgText ="付款单编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
              }
           }
       }
       if(strlen(AcceDate)<=0)
       {
            isFlag = false;
            fieldText = fieldText + "付款日期|";
   		    msgText = msgText +  "付款日期不能为空|";
       }
       if(strlen(CustName)<=0)
       {
            isFlag = false;
            fieldText = fieldText + "往来客户|";
   		    msgText = msgText +  "往来客户不能为空|";
       }
       if(strlen(TotalPrice)<=0)
       {
            isFlag = false;
            fieldText = fieldText + "付款金额|";
   		    msgText = msgText +  "付款金额不能为空|";
       }
       if(!IsNumeric(TotalPrice,12,2))
       {
            isFlag = false; 
            fieldText = fieldText + "付款金额|";
	        msgText = msgText +  "付款金额格式不正确且小数后只能输入两位|";
       }
       else
       {
        var IncomePrice=document.getElementById("txtIncomePrice").value;          
        var NAccounts=document.getElementById("txtNAccounts").value;
        if(document.getElementById("txtAction").value!="1")
        {
          
          if(Order.length>0)
          {
             if(parseFloat(TotalPrice)>parseFloat(NAccounts))
             {
                 isFlag = false; 
                fieldText = fieldText + "付款金额|";
	            msgText = msgText +  "付款金额不能大于业务单未结金额|";
             } 
             if(parseFloat(TotalPrice)>parseFloat(NAccounts))
             {
                 isFlag = false; 
                fieldText = fieldText + "付款金额|";
	            msgText = msgText +  "付款金额应小于或等于业务单未结金额|";
             }
          }
        }
        else
        {
             if(Order.length>0)
             {
                 var NAccounts=document.getElementById("txtNAccounts").value;
                 if(parseFloat(TotalPrice)>parseFloat(NAccounts))
                 {
                      isFlag = false; 
                       fieldText = fieldText + "付款金额|";
	                  msgText = msgText +  "付款金额不能大于业务单未结金额|";
                 }
             }
        }
       }
      
        if(!isFlag)
        {
          popMsgObj.Show(fieldText,msgText);
        }
        return isFlag;
   }
   
   
function BlendingBind()
{
    
    var blendingValue=document.getElementById("rblBlengding").value;
    var BillingID=document.getElementById("txtSaveID").value;
    var ID=document.getElementById("txtOprtID").value;
    var ACT=document.getElementById("txtAction").value;
    if(ACT=="1")
    {
        if(blendingValue=="1"&&BillingID!="")
        {
        var Action="Get";
         
       
         var UrlParms="Action="+escape(Action);
             UrlParms+="&BillingID="+escape(BillingID);
             UrlParms+="&PayOrIncomeType=2";
         $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
                  dataType:'html',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() {
                  popMsgObj.ShowMsg('请求发生错误');
                }, 
                success:function(msg) 
                { 
                    document.getElementById("BlendingDetails").innerHTML=msg;
                    document.getElementById("BlendingDetails").style.display="block";
                } 
               });
         }
         else 
         {
              document.getElementById("BlendingDetails").innerHTML="";
              document.getElementById("BlendingDetails").style.display="none";
         }
     }
     else if(ACT=="2")
     {
         if(blendingValue=="1"&&BillingID!="")
        {
          var Action="EditGet";
         
       
          var UrlParms="Action="+escape(Action);
             UrlParms+="&BillingID="+escape(BillingID);
             UrlParms+="&PayOrIncomeType=2";
             UrlParms+="&PayID="+escape(ID);
         $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/FinanceManager/PayBill_Add.ashx?"+UrlParms,
                  dataType:'html',//返回json格式数据
                  cache:false,
                  beforeSend:function()
                  { 
                      //AddPop();
                  }, 
                //complete :function(){hidePopup();},
                error: function() {
                  popMsgObj.ShowMsg('请求发生错误');
                }, 
                success:function(msg) 
                { 
                    document.getElementById("BlendingDetails").innerHTML=msg;
                    document.getElementById("BlendingDetails").style.display="block";
                } 
               });
         }
         else 
         {
              document.getElementById("BlendingDetails").innerHTML="";
              document.getElementById("BlendingDetails").style.display="none";
         }
     }
}
function btntype()
{
    if(document.getElementById("divIncomeNo").innerHTML=="")
    {
        try
        {
        document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
        document.getElementById("btnConfirm").src="../../../images/Button/UnClick_qr.jpg";
        document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
        document.getElementById("btnIncomeBill_Save").disabled=false;
        document.getElementById("btnConfirm").disabled=true;
        document.getElementById("btnReConfirm").disabled=true;
        }catch(ex){}
    }else if(document.getElementById("divIncomeNo").innerHTML!=""&&parseInt(document.getElementById("Confirm").value)<=0)
    {
        try{
        if(document.getElementById("btnIncomeBill_Save")!=null)
        {
            document.getElementById("btnIncomeBill_Save").src="../../../images/Button/Bottom_btn_save.jpg";
            document.getElementById("btnIncomeBill_Save").disabled=false;
        }
        
        document.getElementById("btnConfirm").src="../../../images/Button/Bottom_btn_confirm.jpg";
        document.getElementById("btnReConfirm").src="../../../images/Button/btn_fqru.jpg";
        document.getElementById("btnConfirm").disabled=false;
        document.getElementById("btnReConfirm").disabled=true;
        document.getElementById("txtTotalPrice").disabled=false;
        }catch(ex){}
    }
    else
    {
    try{
        if(document.getElementById("btnIncomeBill_Save")!=null)
        {
            document.getElementById("btnIncomeBill_Save").disabled=true;
            document.getElementById("btnIncomeBill_Save").src="../../../images/Button/UnClick_bc.jpg";
        }
        document.getElementById("btnConfirm").src="../../../images/Button/UnClick_qr.jpg";
        document.getElementById("btnReConfirm").src="../../../images/Button/Main_btn_fqr.jpg";
        document.getElementById("btnConfirm").disabled=true;
        document.getElementById("btnReConfirm").disabled=false;
        document.getElementById("txtTotalPrice").disabled=true;
        }catch(ex){}
    }
}

//确认
function ConfirmIncomeBill1()
{
    var chkValue = ""; 
    var Confirm="";
    var IsConfirm = "";

    var TotalPrice = 0;
    var CurrencyType = "";
    var CurrencyRate = "";
    var CurrencyName = "";
    var price="";
    var CustID = "";
    var billingid="";
    var incomeno="";
    chkValue=document.getElementById("txtOprtID").value;
    //CurrencyType=document.getElementById("txtCurryType").value;
    CurrencyRate=document.getElementById("txtExchangeRate").value;
    CustID=document.getElementById("CustID").value;
    TotalPrice=document.getElementById("txtTotalPrice").value;
    price=document.getElementById("price").value;
    billingid=document.getElementById("txtSaveID").value;
    incomeno=document.getElementById("divIncomeNo").innerHTML;
    
     UrlParms = "&Action=Audit&ID=" + chkValue + "&Currency=" + escape(CurrencyType) + "," + escape(CurrencyRate) + "&CustID=" + escape(CustID) + "&PayAmount=" + escape(TotalPrice)+"&price="+escape(price)+"&billingid="+escape(billingid)+"&incomeno="+escape(incomeno);
   $.ajax({ 
              type: "POST",
              url: "../../../Handler/JTHY/Expenses/PayBill_Add.ashx?"+UrlParms,
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
                     popMsgObj.ShowMsg("确认成功");
                    document.getElementById("Confirm").value="1";
                    document.getElementById("txtConfirmor").value=document.getElementById("Confirmname").value
                    document.getElementById("txtConfirmStatus").value="已确认";
                    document.getElementById("txtConfirmDate").value=getSysDate();
                    btntype();
                }
                else
                {
                    popMsgObj.ShowMsg("确认失败");
                } 
            } 
           });  
}
//反确认
function ReconfirmIncomeBill1()
{
       var chkValue = ""; 
       var Confirm="";
       var IsConfirm = "";
       chkValue=document.getElementById("txtOprtID").value;
       price=document.getElementById("price").value;
       billingid=document.getElementById("txtSaveID").value;
       UrlParms="&Action=ReverseAudit&ID="+chkValue+"&price="+escape(price)+"&billingid="+escape(billingid);
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
                       popMsgObj.ShowMsg("取消确认成功");
                        document.getElementById("Confirm").value="0";
                        document.getElementById("txtConfirmor").value="";
                        document.getElementById("txtConfirmStatus").value="未确认";
                        document.getElementById("txtConfirmDate").value="";
                        btntype();
                    }
                    else
                    {
                        popMsgObj.ShowMsg("取消确认失败");
                    } 
                } 
               });  
}