
/*编辑消息提示设置*/
/*
库存限量报警：默认启用
客户联络延期告警：默认启用
供应商联络延期预警：默认启用
待我审批的流程：默认启用
我的待办任务：默认启用
即将到期的劳动合同：默认启用
我的备忘录：默认启用
我的未读短信：默认启用
我的参会通知：默认启用
*/
function ParameterSetting(type, isPoint) {

    var StorageUsedStatus = 0;
    var CustUsedStatus = 0;
    var ProviderUsedStatus = 0;
    var FlowUsedStatus = 0; 
    var TaskUsedStatus = 0; 
    var ContractUsedStatus = 0; 
    var MemoUsedStatus = 0;
    var UnReadMsgUsedStatus = 0;
    var MeetUsedStatus = 0;
    
    //添加证照预警参数设置  默认启用
    var YYZZStatus = 0;
    var XKZStatus=0;
    var GMPStatus=0;
    var YPBZQStatus=0;
    var FRWTSStatus=0;
    var YYZZDay=0;
    var XKZDay=0;
    var GMPDay=0;
    var YPBZQDay=0;
    var FRWTSDay=0;
    //添加证照预警参数设置 END
    
    //添加超期锁定参数设置 默认启用
    var Lock1=document.getElementById('Lock1').value;
     var Lock2=document.getElementById('Lock2').value
      var Lock3=document.getElementById('Lock3').value
       var Lock4=document.getElementById('Lock4').value
        var Lock5=document.getElementById('Lock5').value
        

    if (document.getElementById('dioStorage1').checked) {
        StorageUsedStatus = 1;
    }

    if (document.getElementById('dioCC1').checked) {
        CustUsedStatus = 1;
    }
    if (document.getElementById('radPC1').checked) {
        ProviderUsedStatus = 1;
    }
    if (document.getElementById('radFlow1').checked) {
        FlowUsedStatus = 1;
    }
    if (document.getElementById('radTask1').checked) {
        TaskUsedStatus = 1;
    }
    if (document.getElementById('dioCT1').checked) {
        ContractUsedStatus = 1;
    }
    if (document.getElementById('dioMemo1').checked) {
        MemoUsedStatus = 1;
    }
    if (document.getElementById('dioUnRM1').checked) {
        UnReadMsgUsedStatus = 1;
    }
    if (document.getElementById('dioMN1').checked) {
        MeetUsedStatus = 1;
    }
    
    //证照预警赋值
    if(document.getElementById('dioYYZZ1').checked){
    YYZZStatus=1;
    YYZZDay=document.getElementById('txtYYZZDay').value;
    }
    if(document.getElementById('dioXKZ1').checked){
    XKZStatus=1;
    XKZDay=document.getElementById('txtXKZDay').value;
    }
    if(document.getElementById('dioGMP1').checked){
    GMPStatus=1;
    GMPDay=document.getElementById('txtGMPDay').value;
    }
    if(document.getElementById('dioYPBZQ1').checked){
    YPBZQStatus=1;
    YPBZQDay=document.getElementById('txtYPBZQDay').value;
    }
    
    if(document.getElementById('dioFRWTS1').checked){
    FRWTSStatus=1;
    FRWTSDay=document.getElementById('txtFRWTSDay').value;
    }
    
    var iDays="&iDays=0";
    var Action = 'setMsg';
    var UrlParam = "";
    
   
    var myLock="否";
    var Lock= "&Lock="+escape(myLock);
    
    switch (parseInt(type)) {
        case 1:
            UrlParam = "action=" + Action + "&UsedStatus=" + StorageUsedStatus+iDays+Lock;
            break;
        case 2:
            UrlParam = "action=" + Action + "&UsedStatus=" + CustUsedStatus+iDays+Lock;
            break;
        case 3:
            UrlParam = "action=" + Action + "&UsedStatus=" + ProviderUsedStatus+iDays+Lock;
            break;
        case 4:
            UrlParam = "action=" + Action + "&UsedStatus=" + FlowUsedStatus+iDays+Lock;
            break;
        case 5:
            UrlParam = "action=" + Action + "&UsedStatus=" + TaskUsedStatus+iDays+Lock;
            break;
        case 6:
            UrlParam = "action=" + Action + "&UsedStatus=" + ContractUsedStatus+iDays+Lock;
            break;
        case 7:
            UrlParam = "action=" + Action + "&UsedStatus=" + MemoUsedStatus+iDays+Lock;
            break;
        case 8:
            UrlParam = "action=" + Action + "&UsedStatus=" + UnReadMsgUsedStatus+iDays+Lock;
            break;
        case 9:
            UrlParam = "action=" + Action + "&UsedStatus=" + MeetUsedStatus+iDays+Lock;
            break;
        case 10:
            iDays="&iDays="+YYZZDay;
            Lock="&Lock="+escape(Lock1);
            UrlParam = "action=" + Action + "&UsedStatus=" + YYZZStatus+iDays+Lock;
            break;
        case 11:
            iDays="&iDays="+XKZDay;
             Lock="&Lock="+escape(Lock2);
            UrlParam = "action=" + Action + "&UsedStatus=" + XKZStatus+iDays+Lock;
            break;
        case 12:
            iDays="&iDays="+GMPDay;
             Lock="&Lock="+escape(Lock3);
            UrlParam = "action=" + Action + "&UsedStatus=" + GMPStatus+iDays+Lock;
            break;
        case 13:
            iDays="&iDays="+YPBZQDay;
             Lock="&Lock="+escape(Lock4);
            UrlParam = "action=" + Action + "&UsedStatus=" + YPBZQStatus+iDays+Lock;
            break;
        case 14:
            iDays="&iDays="+FRWTSDay;
             Lock="&Lock="+escape(Lock5);
            UrlParam = "action=" + Action + "&UsedStatus=" + FRWTSStatus+iDays+Lock;
            break;
            
    }


    UrlParam += "&FunctionType=" + type;
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/ParameterSetting.ashx?",
        data: UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
        },
        error: function() {
            //  jAlert('请求发生异常');
        },
        success: function(msg) {
            if (msg.result) {
                popMsgObj.ShowMsg(msg.data);
            }
            else {
                popMsgObj.ShowMsg(msg.data);
            }
        }
    });
}