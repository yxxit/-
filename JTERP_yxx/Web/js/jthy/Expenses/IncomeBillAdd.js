$(document).ready(function() {

    requestobj = GetRequest();
    var mytemp=requestobj['intMasterID'];
    var mytemp2=requestobj['incomeNo'];
    document.getElementById("headid").value = requestobj['intMasterID'];
    if (document.getElementById("headid").value != "" && document.getElementById("headid").value != "undefined") {
        document.getElementById("hiddOrderID").value = document.getElementById("headid").value;
        GetInfoById(document.getElementById("headid").value);
        //fnGetDetail(document.getElementById("headid").value);
        //$("#labTitle_Write1").html("销售合同单据查看");
    }
//    else {
//        $("#divCodeRule").css("display", "block");
//        $("#txtContractID").css("display", "none");
//        $("#labTitle_Write1").html("销售合同单据新建");
//    }

});

function GetInfoById(headid) {
  
    var action = "LoadIncomBill";
    var orderBy = "id";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/Expenses/IncomeBill_Add.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function() { }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            
            $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "") {

                    document.getElementById("divCodeRule").style.display="none";
                    document.getElementById("divIncomeNo").style.display="block";
                    document.getElementById("divIncomeNo").innerHTML = item.IncomeNo;//赋值
                    
                    document.getElementById("hideId").value = item.id;///------------------
                    document.getElementById("txtAcceDate").value = item.AcceDate;
                    document.getElementById("txtCustName").value = item.CustName;

                    document.getElementById("txtOrder").value = item.FromBillType;
                    document.getElementById("BillingType").value = item.BillingID;
                    document.getElementById("txtTotalPrice").value = item.TotalPrice;
                    document.getElementById("PaymentTypes").value = item.PaymentTypes;
                    document.getElementById("DrpAcceWay").value = item.AcceWay;
                    document.getElementById("UsertxtExcutor").value = item.Executor;//显示执行人内容
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
        var AcceDate=document.getElementById("txtAcceDate").value;//收款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var TotalPrice=document.getElementById("txtTotalPrice").value;//收款金额
         
         var PaymentTypes=document.getElementById("PaymentTypes").value;//款项类型
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
         //if(!CheckInput())return;//验证其他
         
        
        //设置参数
        var UrlParms="Action="+"update";//----------------------修改
        UrlParms+="&ID="+ID;
        UrlParms+="&IncomeNo="+IncomeNo;//获取收款单号
        UrlParms+="&AcceDate="+AcceDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&TotalPrice="+TotalPrice;//
        
        
        UrlParms+="&PaymentTypes="+PaymentTypes;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/IncomeBill_Add.ashx?"+UrlParms,
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
                            document.getElementById("btnConfirm").disabled=true;
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
        var AcceDate=document.getElementById("txtAcceDate").value;//收款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var TotalPrice=document.getElementById("txtTotalPrice").value;//收款金额
         
         var PaymentTypes=document.getElementById("PaymentTypes").value;//款项类型
         var AcceWay=document.getElementById("DrpAcceWay").value;//付款方式
         var Executor=document.getElementById("txtSaveUserID").value;//执行人
                 
         //获取当前时间
         var myDate = new Date();
         var getConfirmTime=myDate.toLocaleDateString();
         
         //附加信息，确认是基本上用不到
         var AccountDate=document.getElementById("txtAccountDate").value;//
         var Accountor=document.getElementById("txtAccountor").value;//
         var ConfirmDate=document.getElementById("txtConfirmDate").value;//
         var Confirmor=document.getElementById("txtConfirmor").value;//
         
         //添加验证
         if(!CheckInput())return;//验证其他
        
        //设置参数
        var UrlParms="Action="+"confirm";
        UrlParms+="&ID="+ID;
        UrlParms+="&IncomeNo="+IncomeNo;//获取收款单号
        UrlParms+="&AcceDate="+AcceDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&TotalPrice="+TotalPrice;//
        
        
        UrlParms+="&PaymentTypes="+PaymentTypes;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/IncomeBill_Add.ashx?"+UrlParms,
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
                            
                            document.getElementById("txtConfirmDate").value=getConfirmTime;
                            //些许bug
                            var temp3=document.getElementById("jiandangren2").value;//--------------2014-7-15  1
                            document.getElementById("txtConfirmor").value=temp3;//--------------2014-7-15  1
                        }
                        else
                        {
                            popMsgObj.ShowMsg("操作失败");
                        }
                } 
           });  
   
}




//打印功能的实现
function DoPrint()
{ 
//  if(document.getElementById("txtOprtID").value=="")//其作用是？
//  {
//     popMsgObj.ShowMsg("请保存后打印");
//     return;
//  }  
//  window.open("../../PrinttingModel/FinanceManager/InComeBillingAdd.aspx?Id=" + document.getElementById("txtOprtID").value);
    window.open("../../PrinttingModel/FinanceManager/InComeBillingAdd.aspx?Id=1");
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
   
   
   
   //收款接受方式判断
   function AcceWay()
   {
     var Way=document.getElementById("DrpAcceWay").value;

     
   }
   //添加收款信息（simple）
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
        var AcceDate=document.getElementById("txtAcceDate").value;//收款日期
        var CustName=document.getElementById("txtCustName").value;//往来顾客
        
        var FromBillType="1";//document.getElementById("BillingType").value;//业务单类型
        var BillingID="1";//document.getElementById("BillingID").value;//来源单编号,现在不要去去做
        var TotalPrice=document.getElementById("txtTotalPrice").value;//收款金额
         
         var PaymentTypes=document.getElementById("PaymentTypes").value;//款项类型
         var AcceWay=document.getElementById("DrpAcceWay").value;//付款方式
         var Executor=document.getElementById("txtSaveUserID").value;//执行人,返回的int类型的值返回它的id
         var txtSaveUserID=document.getElementById("txtSaveUserID").value;
                 
         //获取保存时间
         var myDate = new Date();
         var myTime=myDate.toLocaleDateString();
         
         //附加信息
         var AccountDate=myTime;//document.getElementById("txtAccountDate").value;//建档日期
         var Accountor="";//document.getElementById("jiandangrenId").value;//在隐藏项中，该值为int型
         var ConfirmDate=document.getElementById("txtConfirmDate").value;//
         var Confirmor=document.getElementById("txtConfirmor").value;//
         
         //添加验证
//         if(IncomeNo==null&&IncomeNo==""){
//            popMsgObj.ShowMsg('单据编号不能为空！');
//            return;
//         }
//         if(TotalPrice==null||TotalPrice==""){
//            popMsgObj.ShowMsg('收款金额不能为空！');
//            return;
//         }
//         if(CustName==null||CustName==""){
//            popMsgObj.ShowMsg('往来客户不能为空！');
//            return;
//         }
//         if(BillingID==null||BillingID==""){
//            popMsgObj.ShowMsg('来源表单不能为空！');
//            return;
//         }
//         if(AcceDate==null||AcceDate==""){
//            popMsgObj.ShowMsg('收款日期不能为空！');
//            return;
//         
//         }
         
       if(!CheckInput())return;//验证其他
         
        
        //设置参数
        var UrlParms="Action="+Action;
        UrlParms+="&ID="+ID;
        UrlParms+="&IncomeNo="+IncomeNo;//获取收款单号
        UrlParms+="&AcceDate="+AcceDate;//
        UrlParms+="&CustName="+CustName;//
        
        UrlParms+="&FromBillType="+FromBillType;//
        UrlParms+="&BillingID="+BillingID;//
        UrlParms+="&TotalPrice="+TotalPrice;//
        
        
        UrlParms+="&PaymentTypes="+PaymentTypes;//
        UrlParms+="&AcceWay="+AcceWay;//
        UrlParms+="&Executor="+Executor;//
        
        
        UrlParms+="&AccountDate="+AccountDate;//
        UrlParms+="&Accountor="+Accountor;//
        UrlParms+="&ConfirmDate="+ConfirmDate;//
        UrlParms+="&Confirmor="+Confirmor;//

         
        $.ajax({ 
                  type: "POST",
                  url: "../../../Handler/JTHY/Expenses/IncomeBill_Add.ashx?"+UrlParms,
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

                            document.getElementById("divIncomeNo").innerHTML=IncomeNo;//--------------

                            document.getElementById("divIncomeNo").style.display="block";//--------------

                            document.getElementById("divCodeRule").style.display="none";
                            document.getElementById("txtAction").value="2";//改变其值，不等于1的时候修改//--------------
                            //将数据的id保存到隐藏的空间里
                            document.getElementById("hideId").value=data.sta;
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
   
   //验证
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
            fieldText = fieldText + "收款单编号|";
   		    msgText = msgText +  "请输入收款单编号|";
           }
           else
           {
              if(!CodeCheck(IncomeNo))
              {
               isFlag = false;
               fieldText = fieldText + "收款单编号|";
   		       msgText ="收款单编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]{}）组成|";
              }
           }
       }
       if(strlen(AcceDate)<=0)
       {
            isFlag = false;
            fieldText = fieldText + "收款日期|";
   		    msgText = msgText +  "收款日期不能为空|";
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
            fieldText = fieldText + "收款金额|";
   		    msgText = msgText +  "收款金额不能为空|";
       }
       if(!IsNumeric(TotalPrice,12,2))
       {
            isFlag = false; 
            fieldText = fieldText + "收款金额|";
	        msgText = msgText +  "收款金额格式不正确且小数后只能输入两位|";
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
                fieldText = fieldText + "收款金额|";
	            msgText = msgText +  "收款金额不能大于业务单未结金额|";
             } 
             if(parseFloat(TotalPrice)>parseFloat(NAccounts))
             {
                 isFlag = false; 
                fieldText = fieldText + "收款金额|";
	            msgText = msgText +  "收款金额应小于或等于业务单未结金额|";
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
                       fieldText = fieldText + "收款金额|";
	                  msgText = msgText +  "收款金额不能大于业务单未结金额|";
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
              url: "../../../Handler/JTHY/Expenses/IncomeBill_Add.ashx?"+UrlParms,
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