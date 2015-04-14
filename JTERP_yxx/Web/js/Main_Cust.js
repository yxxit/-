var jsondata1;
var jsondata2;
var jsondata3;
var jsondata4;
var ProductAlarmCount = 0;
var ContactDeferCount = 0;
var ProviderContactCount = 0;
var ContractAlarmCount = 0;
var BeiWangLuCount = 0;
var CanHuiTongZhiCount = 0;
var returnResult = 0;
var sessionSection = "";
var isalertstorage = "";
var isalertcust = "";
var isalertprovider = "";
var isalertflow = "";
var isalerttask = "";
var isalertcontract = "";
var isalertmemo = "";
var isalertmsg = "";
var isalertmeet = "";
 
//检查是否是登录进入主页面，如果不是，跳转到登录页面
if( getCookie("isLogin") != null)
{
    DeleteCookie("isLogin");
}
else
{
       
      location.replace("../../../Login_Cust.aspx");
}

////--------法人委托书自定义变量---------//
//var IsAlertPowerdata="1";                  //设置是否允许弹出该种类的警示
//var OverduePowerdata=0;                    //初始化过期的数据
//var PowerDate=0;                           //初始化即将过期的数据
////--------许可证自定义变量-----------//
//var IsAlertUsedata="1";                    //设置是否允许弹出该种类的警示
//var OverdueUsedata=0;                      //初始化过期的数据 
//var Usedata=0;                             //初始化即将过期的数据
////--------经营许可证自定义变量--------//
//var IsAlertCertidata="1";                  //设置是否允许弹出该种类的警示
//var OverCertidata=0;                       //初始化过期的数据      
//var Certidata=0;                           //初始化即将过期的数据
////----------委托书自定义变量---------//
//var IsAlertWardata="1";                    //设置是否允许弹出该种类的警示
//var OverWardata=0;                         //初始化过期的数据    
//var Wardata=0;                             //初始化即将过期的数据
////---------gms有效期自定义变量------//
//var IsAlertGmspdata="1";                   //设置是否允许弹出该种类的警示
//var OverGmspdata=0;                        //初始化过期的数据
//var Gmspdata=0;                            //初始化即将过期的数据
////-------------------------------------------------//
//var MedFileDate=0;                         //批准文号
//var OverdueMedFileDate=0;                  //过期的批准文号
//var Validity=0;                            //批件有效期
//var OverdueValidity=0;                     //过期的批件

//var TableStr = "";
//var data="";
//  data=""+getDate()+"|"+getModifyDate()+"";
//    $(document).ready(function() 
//    {     
////        $("#CompanyCD").css("display","none");
//          $("#showCompanyCD").css("display","none");
//        
//        var url = document.location.href.toLowerCase();
//        if (url.indexOf("/(s(") != -1) 
//        {
//            var sidx = url.indexOf("/(s(") + 1;
//            var eidx = url.indexOf("))") + 2;
//            //alert(sidx+":"+eidx);
//            url = document.location.href;

//            sessionSection = url.substring(sidx, eidx);
//            sessionSection += "/";
//        }
//        var lefttd = document.getElementById("leftTD");
//        var Left = document.getElementById("Left");
//        var Maintd = document.getElementById("Maintd");
//        //    var Main=document.getElementById("Main");
//        var Mwidth = document.documentElement.clientWidth;
//        Left.style.width = lefttd.style.width;
//        Maintd.style.width = parseInt(Mwidth) - parseInt(lefttd.style.width);
//        //     Main.style.width= Maintd.style.width;

//         isalertstorage = document.getElementById("IsAlertStorage").value;
//        isalertcust = document.getElementById("IsAlertCust").value;
//         isalertprovider = document.getElementById("IsAlertProvider").value;
//         isalertflow = document.getElementById("IsAlertFlow").value;
//        isalerttask = document.getElementById("IsAlertTask").value;
//        isalertcontract = document.getElementById("IsAlertContract").value;
//        isalertmemo = document.getElementById("IsAlertMemo").value;
//        isalertmsg = document.getElementById("IsAlertMsg").value;
//        isalertmeet = document.getElementById("IsAlertMeet").value;
 
//    });
    
    
    function getCookie(name) 
    {
        var bikky = document.cookie;
        name += "=";
        var i = 0; 
        while (i < bikky.length) 
        {
          var offset = i + name.length;
          if (bikky.substring(i, offset) == name) 
          { 
            var endstr = bikky.indexOf(";", offset); 
            if (endstr == -1) endstr = bikky.length;
              return unescape(bikky.substring(offset, endstr)); 
          }
            i = bikky.indexOf(" ", i) + 1; 
            if (i == 0) break; 
        }
        return null; 
    }
    
    function SetCookie(name, value, expires, path, domain, secure) { 
    document.cookie = name + "=" + escape(value) + ((expires) ? "; expires=" + expires.toGMTString() : "") + ((path) ? "; path=" + path : "") + ((domain) ? "; domain=" + domain : "") + ((secure) ? "; secure" : ""); 
    return value; 
    }
    function DeleteCookie(name) { 
    if (getCookie(name) != null) { 
    SetCookie(name, "", null, "/", null); 
    } 
    } 

    function GetAllDestTopList() 
    {
        TableStr = "<table   id=\"mainindex\" style=\"width:100%;background-color:White\"  >";
        SearchTaskList();
        SearchDeskFlow();
        SearchUnreadMessage();
        SearchStorageProductAlarm();
        SearchContactDefer();
        SearchProviderContactCount();
        SearchContractAlarm();
        SearchBeiWangLu();
        SearchCanHuiTongZhi();
        if($("#CompanyCD").val()=="HCYY1")//
        {
           // TableStr+="<tr><td>客户档案通知</td></tr>";
        SearchGmspdata('CustInfo','cust');
        SearchWardata('CustInfo','cust');
        SearchUsedata('CustInfo','cust');
        SearchCertidata('CustInfo','cust');    
        SearchPowerdata('CustInfo','cust');
        
            //TableStr+="<tr><td>供应商档案通知</td></tr>";
        SearchGmspdata('ProviderInfo','provider');
        SearchWardata('ProviderInfo','provider');
        SearchUsedata('ProviderInfo','provider');
        SearchCertidata('ProviderInfo','provider'); 
     //    //   SearchPowerdataAndEndTb('ProviderInfo','provider');
       SearchPowerdata('ProviderInfo','provider');
       // endTable();
      SearchMedFileDate('ProductInfo');
      SearchValidity('ProductInfo');
        }
//        endTable();
        
    }

    function SearchTaskList() 
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: 'Handler/Personal/Task/TaskList.ashx?TaskType=1', //目标地址
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前 
            success: function(msg) {
                jsondata1 = eval(msg.data);
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }


    function SearchDeskFlow() 
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/DeskTop.ashx', //目标地址
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
                 var jsondata = eval('(' + msg + ')');
                 jsondata2 = jsondata.data.list;
                returnResult++;
                isAllRetrun();

            },
            error: function(res) {

            }
        });
    }


    function getBillTypeItem(i, j) 
    {
        for (var mem in billTypes) 
        {
            if (billTypes[mem].v == j && billTypes[mem].p == i) 
            {
                return billTypes[mem];
                break;
            }
        }
        return null;
    }

    function SearchUnreadMessage() 
    {

        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Personal/MessageBox/InputBox.ashx', //目标地址
            data: "action=desktoploaddata",
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
                var jsondata = eval('('+msg+')');
                jsondata4 = jsondata.data.list;
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }

    function SearchStorageProductAlarm() 
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Office/StorageManager/StorageProductAlarm.ashx?orderby=&pageCount=10000&pageIndex=1&BarCode=&sltAlarmType=0', //目标地址
            data: "action=desktoploaddata",
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
            try
            {
            var jsondata = eval('(' + msg + ')');
            ProductAlarmCount = jsondata.totalCount;
                returnResult++;
            }
           catch(e)
           {}
               //isAllRetrun();
                 ShowStorageProductAlarm();
            },
            error: function(res) {

            }
        });
    }


    function SearchContactDefer() 
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Office/CustManager/ContactDeferInfo.ashx', //目标地址
            data: "action=desktoploaddata&orderby=&pageCount=10000&pageIndex=1",
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
            var jsondata = eval('(' + msg + ')');
            ContactDeferCount = jsondata.totalCount;
                returnResult++;
               // isAllRetrun();
               ShowContactDefer();
            },
            error: function(res) {

            }
        });
    }


    function SearchProviderContactCount() 
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Office/PurchaseManager/ProviderContactHistoryWarning.ashx', //目标地址
            data: "action=desktoploaddata&orderby=&pageCount=10000&pageIndex=1",
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
            var jsondata = eval('(' + msg + ')');
            ProviderContactCount = jsondata.totalCount;
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }




    function SearchBeiWangLu() 
    {
        var sign = 'sign=GetBeiWangLuCount';
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Main.ashx', //目标地址
            data: sign,
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
                var jsondata = eval('(' + msg + ')');
                BeiWangLuCount = jsondata.totalCount;
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }


    function SearchCanHuiTongZhi() 
    {
        var sign = 'sign=SearchCanHuiTongZhi';
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Main.ashx', //目标地址
            data: sign,
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
            var jsondata = eval('(' + msg + ')');
            CanHuiTongZhiCount = jsondata.totalCount;
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }
    function SearchContractAlarm() 
    {
        var sign = 'sign=GetContractCount';
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "html", //数据格式:JSON
            url: 'Handler/Main.ashx', //目标地址
            data: sign,
            cache: false, //指令
            beforeSend: function() {
            }, //发送数据之前
            success: function(msg) {
            var jsondata = eval('(' + msg + ')');
            ContractAlarmCount = jsondata.totalCount;
                returnResult++;
                isAllRetrun();
            },
            error: function(res) {

            }
        });
    }
/*
检索法人证书有效期的数据
*/ 

function SearchPowerdata(table,type)
{
 var sign = 'sign=GetPowerdata';//设置操作
 var day=getDate();  //获得当前日期
 var ModifyDay=getModifyDate(); //获得修改后的日期
 var keyWord="Powerdata";//设置要查询的数据表关键字段
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverduePowerdata = jsondata.OverdueCount;
        PowerDate=jsondata.PowerDate;
            returnResult++;
            ShowPowerdata(type);
         
        },
        error: function(res) {

        }
    });
}
/*
 2012-10-15 添加 
*/
function SearchPowerdataAndEndTb(table,type)
{
 var sign = 'sign=GetPowerdata';//设置操作
 var day=getDate();  //获得当前日期
 var ModifyDay=getModifyDate(); //获得修改后的日期
 var keyWord="Powerdata";//设置要查询的数据表关键字段
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverduePowerdata = jsondata.OverdueCount;
        PowerDate=jsondata.PowerDate;
            returnResult++;
            ShowPowerdata(type);
            endTable();
         
        },
        error: function(res) {

        }
    });
}
/*
检索许可证有效期的数据
*/ 
function SearchUsedata(table,type)
{
   var sign = 'sign=GetUsedata';
   var day=getDate(); 
   var ModifyDay=getModifyDate();   
   var keyWord="Usedata";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueUsedata = jsondata.OverdueCount;
        Usedata=jsondata.Usedata;
            returnResult++;
               ShowUsedata(type);
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}
/*
检索经营许可证书有效期的数据
*/ 
function SearchCertidata(table,type)
{
   var sign = 'sign=GetCertidata';
   var day=getDate();   
   var ModifyDay=getModifyDate(); 
   var keyWord="Certidata";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueCertidata = jsondata.OverdueCount;
        Certidata=jsondata.Certidata;
            returnResult++;
               ShowCertidata(type);
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}
/*
检索委托证书有效期的数据
*/ 
function SearchWardata(table,type)
{
   var sign = 'sign=GetWardata';
   var day=getDate();
   var ModifyDay=getModifyDate(); 
   var keyWord="Wardata";  
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueWardata= jsondata.OverdueCount;
        Wardata=jsondata.Wardata;
            returnResult++;
               ShowWardata(type);
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}

/*
检索GMS有效期的数据
*/ 
function SearchGmspdata(table,type)
{
   var sign = 'sign=GetGmspdata';
   var day=getDate();
   var ModifyDay=getModifyDate();    
   var keyWord="Gmspdata";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueGmspdata= jsondata.OverdueCount;
        Gmspdata=jsondata.Gmspdata;
            returnResult++;
               ShowGmspdata(type);
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}
/*
药品批准文号有效期
*/
function SearchMedFileDate(table)
{
   var sign = 'sign=GetMedFileDate';
   var day=getDate();
   var ModifyDay=getModifyDate();    
   var keyWord="MedFileDate";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueMedFileDate= jsondata.OverdueCount;
       MedFileDate=jsondata.MedFileDate;
            returnResult++;
               ShowMedFileDate('MedFileDate');
               //endTable();
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}

/*
药品批件有效期
*/
function SearchValidity(table)
{
   var sign = 'sign=GetValidity';
   var day=getDate();
   var ModifyDay=getModifyDate();    
   var keyWord="Validity";
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "html", //数据格式:JSON
        url: "Handler/Main.ashx?day="+day+"&ModifyDay="+ModifyDay+"&keyWord="+keyWord+"&table="+table, //目标地址
        data: sign,
        cache: false, //指令
        beforeSend: function() {
        }, //发送数据之前
        success: function(msg) {
        var jsondata = eval('(' + msg + ')');
        OverdueValidity= jsondata.OverdueCount;
        Validity=jsondata.Validity;
            returnResult++;
               ShowValidity('MedFileDate');
               endTable();
            //isAllRetrun();
        },
        error: function(res) {

        }
    });
}

    function getBillTypeItem(i, j)
     {
        for (var mem in billTypes) 
        {
            if (billTypes[mem].v == j && billTypes[mem].p == i) {
                return billTypes[mem];
                break;
            }
        }

        return null;
    }


    function getStatusName(i) 
    {
        if (i == "1")
            return "待我审批";
        if (i == "2")
            return "审批中";
        if (i == "3")
            return "审批通过";
        if (i == "4")
            return "审批不通过";
        if (i == "5")
            return "撤销审批";
        return "未知";
    }


    function isAllRetrun() 
    {
//        if (returnResult>0) {
//            ShowStorageProductAlarm();
//            ShowContactDefer();
//            ShowProviderContact();
//            ShowSearchTaskList();
//            ShowSearchDeskFlow();
//            ShowSearchContractAlarm();
//            ShowSearchUnreadMessage();
//            ShowBeiWangLuCount();
//            ShowCanHuiTongZhi();
//             ShowUsedata();
//           ShowPowerdata();

//        } 
////        if(returnResult>0)
////          {
////          ShowUsedata();
////           ShowPowerdata();
////           }
//           else
//           return;
          
          

    }






    function ShowStorageProductAlarm() 
    {
        if (isalertstorage == "1") 
        {
            try {
                if (ProductAlarmCount + "" != "undefined" && ProductAlarmCount > 0) {
                    TableStr += "<tr><td style='FONT-SIZE: 12px;' >库存限量报警：<a target='Main' href='Pages/Office/StorageManager/StorageProductAlarm.aspx?ModuleID=2051902'><font  color='red'>" + ProductAlarmCount + "</font>条 </a></td></tr>";
                }
            } catch (ee) { }
        }
    }


    function ShowContactDefer() 
    {
        if (isalertcust == "1") 
        {
            try {
                if (ContactDeferCount > 0) {
                    TableStr += "<tr><td style='FONT-SIZE: 12px;' >客户联络延期告警：<a target='Main' href='Pages/Office/CustManager/ContactDefer_Info.aspx?ModuleID=2021303'>  <font  color='red'>" + ContactDeferCount + "</font>条 </a></td></tr>";
                }
            } catch (ee) { }
        }
    }


    function ShowProviderContact() 
    {
        if (isalertprovider == "1") 
        {
            try {
                if (ProviderContactCount > 0) {
                    TableStr += "<tr><td style='FONT-SIZE: 12px;' >供应商联络延期告警：<a target='Main' href='Pages/Office/PurchaseManager/ProviderContactHistoryWarning.aspx?ModuleID=2041203'><font  color='red'>" + ProviderContactCount + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }
    }

    function ShowSearchDeskFlow()
     {
        if (isalertflow == "1") 
        {
            try {
                if (jsondata2.length > 0) {
                    TableStr += "<tr><td style='FONT-SIZE: 12px;' >待我审批的流程：<a target='Main' href='Pages/Personal/WorkFlow/FlowWaitProcess.aspx?ModuleID=10713'><font  color='red'>" + jsondata2.length + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }

    }

    function ShowSearchTaskList() 
    {
        if (isalerttask == "1") 
        {
            try {
                if (jsondata1.length > 0) {
                    TableStr += "<tr><td  style='FONT-SIZE: 12px;'>我的待办任务：<a target='Main' href='Pages/Personal/Task/TaskList.aspx?ModuleID=10112'><font  color='red'>" + jsondata1.length + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }

    }

    function ShowSearchContractAlarm()
     {
        if (isalertcontract == "1") 
        {
            try {
                if (ContractAlarmCount > 0) {
                    TableStr += "<tr><td  style='FONT-SIZE: 12px;'>即将到期的劳动合同：<a target='Main' href='Pages/Office/HumanManager/EmployeeContract_Info.aspx?ModuleID=2011208'><font  color='red'>" + ContractAlarmCount + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }
    }

    function ShowBeiWangLuCount() 
    {
        if (isalertmemo == "1") 
        {
            try {
                if (BeiWangLuCount > 0) {
                    TableStr += "<tr><td  style='FONT-SIZE: 12px;'>我的备忘录：<a target='Main' href='pages/personal/memo/memoList.aspx?ModuleID=10312'><font  color='red'>" + BeiWangLuCount + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }
    }
    function ShowSearchUnreadMessage() 
    {
        if (isalertmsg == "1") 
        {
            try {
                if (typeof (jsondata4) != undefined && jsondata4.length > 0) {
                    TableStr += "<tr><td style='FONT-SIZE: 12px;' >我的未读短信：<a target='Main' href='pages/personal/messagebox/UnReadedInfo.aspx?ModuleID=10613'><font  color='red'>" + jsondata4.length + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
        }
    }
    function ShowCanHuiTongZhi()
     {
        if (isalertmeet == "1")
         {
            try 
            {
                if (CanHuiTongZhiCount > 0) 
                {
                    TableStr += "<tr><td  style='FONT-SIZE: 12px;'>我的参会通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'><font  color='red'>" + CanHuiTongZhiCount + "</font>条</a></td></tr>";
                }
            } catch (ee) { }
         }
     }

    function ShowGmspdata(type)
    {
      if(type=="cust")
      {
        TableStr+="<tr><td><strong>客户档案通知</strong></td></tr>";
        TableStr+="<tr><td><hr/></td></tr>";
        }
      if(type=="provider")
      {
        TableStr+="<tr><td><strong>供应商档案通知</strong></td></tr>";
         TableStr+="<tr><td><hr/></td></tr>";
        }
        if (IsAlertWardata== "1") 
          {
            try 
            {
                if (OverdueGmspdata>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>GMP有效期：</td><td><a href='#'onclick=SelectDept('Gmspdata','过期的GMP',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueGmspdata+ "</font>条</a></td>";
                    TableStr+="<td><a href='#' onclick=SelectDept('Gmspdata','即将过期的GMP',0,'"+type+"')>即将过期:<font  color='red'>"+Gmspdata+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr>";
                 }
            } catch (ee) { }
         }
         }
         
    function ShowWardata(type)
    {
        if (IsAlertWardata== "1") 
          {
            try 
            {
                if (OverdueWardata>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>质保书通知：</td><td><a href='#'onclick=SelectDept('Wardata','过期的质保书',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueWardata+ "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('Wardata','即将过期的质保书',0,'"+type +"')>即将过期:<font  color='red'>"+Wardata+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr>";
                 }
            } catch (ee) { }
         }
         }

    function ShowCertidata(type)
    {
        if (IsAlertCertidata== "1") 
          {
            try 
            {
                if (OverdueCertidata>0)//target='Main' href='Pages/Office/CustManager/Cust_Info.aspx?type=Certidata&data="+getDate()+"'
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>经营许可证通知：</td><td><a href='#'onclick=SelectDept('Certidata','过期的经营许可证',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueCertidata + "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('Certidata','即将过期的经营许可证',0,'"+type+"')>即将过期:<font  color='red'>"+Certidata+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr>";
                 }
            } catch (ee) { }
         }
         }
    function ShowUsedata(type)
    {
        if (IsAlertUsedata== "1") 
          {
            try 
            {
                if (OverdueUsedata>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>营业执照通知：</td><td><a href='#'onclick=SelectDept('Usedata','过期的营业执照',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueUsedata + "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('Usedata','即将过期的营业执照',0,'"+type+"')>即将过期:<font  color='red'>"+Usedata+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr>";
                  // TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;许可证通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>已经过期:<font  color='red'>" + OverdueUsedata + "</font>条</a>";
                // TableStr+= "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>即将过期:<font  color='red'>"+Usedata+"</font>条</td></a></tr>";
                }
            } catch (ee) { }
         }
         }
     
    function ShowPowerdata(type)
    {
        if (IsAlertPowerdata == "1") 
          {
            try 
            {
                if (OverduePowerdata>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>法人委托书通知：</td><td><a  href='#'onclick=SelectDept('PowerDate','过期的法人委托书',1,'"+type+"')>已经过期:<font  color='red'>" + OverduePowerdata + "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('PowerDate','即将过期的法人委托书通知',0,'"+type+"')>即将过期:<font  color='red'>"+PowerDate+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr><tr style='height: 12px;'><td></td></tr>";
                   // TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法人委托书通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>已经过期:<font  color='red'>" + OverduePowerdata + "</font>条</a>";
                // TableStr+= "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>即将过期:<font  color='red'>"+PowerDate+"</font>条</td></a></tr>";
                }
            } catch (ee) { }
          }
     }
     
     function ShowMedFileDate(type)
     {
        TableStr+="<tr><td><strong>物品档案通知</strong></td></tr>";
        TableStr+="<tr><td><hr/></td></tr>";
       try 
            {
                if (OverdueMedFileDate>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>批准文号有效期：</td><td><a  href='#' onclick=SelectDept('MedFileDate','过期的批准文号',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueMedFileDate + "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('MedFileDate','即将过期的批准文号',0,'"+type+"')>即将过期:<font  color='red'>"+MedFileDate+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr>";
                   // TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法人委托书通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>已经过期:<font  color='red'>" + OverduePowerdata + "</font>条</a>";
                // TableStr+= "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>即将过期:<font  color='red'>"+PowerDate+"</font>条</td></a></tr>";
                }
            } catch (ee) { }
     }
     
     function ShowValidity(type)
     {
       try 
            {
                if (OverdueMedFileDate>0)
                 {
                    TableStr+="<tr><td  style='FONT-SIZE: 12px;'><table><tr><td>批件有效期：</td><td><a  href='#' onclick=SelectDept('MedFileDate','过期的批件',1,'"+type+"')>已经过期:<font  color='red'>" + OverdueMedFileDate + "</font>条</a></td>";
                    TableStr+="<td><a href='#'onclick=SelectDept('MedFileDate','即将过期的批件',0,'"+type+"')>即将过期:<font  color='red'>"+MedFileDate+"</font>条</a></td></tr>";
                    TableStr+="</table></td></tr><tr style='height: 12px;'><td></td></tr>";
                   // TableStr += "<tr><td  style='FONT-SIZE: 12px;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;法人委托书通知：<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>已经过期:<font  color='red'>" + OverduePowerdata + "</font>条</a>";
                // TableStr+= "<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a target='Main' href='Pages/Office/AdminManager/MeetingInfo_Info.aspx?ModuleID=2021102'>即将过期:<font  color='red'>"+PowerDate+"</font>条</td></a></tr>";
                }
            } catch (ee) { }
     }
    function  endTable()
      {

      TableStr += "</table>";

      var alllistcount = 0;
      if (isalerttask == "1") 
      {
            try 
            {
                if (jsondata1.length != null)
                    alllistcount += jsondata1.length;
            } catch (ee) { }
      }
      if (isalertflow == "1") 
      {
            try 
            {
                if (jsondata2.length != null)
                    alllistcount += jsondata2.length;
            } catch (ee) { }
      }
      if (isalertmsg == "1") 
      {
            try 
            {
                if (jsondata4.length != null)
                    alllistcount += jsondata4.length;
            } catch (ee) { }
        }
      if (isalertstorage == "1") 
      {
            try
             {
                if (ProductAlarmCount != null)
                    alllistcount += ProductAlarmCount;
            } catch (ee) { }
        }
       if (isalertcust == "1")
       {
            try 
            {
                if (ContactDeferCount != null)
                    alllistcount += ContactDeferCount;
            } catch (ee) { }
        }
       if (isalertprovider == "1")
       {
            try 
            {
                if (ProviderContactCount != null)
                    alllistcount += ProviderContactCount;
            } catch (ee) { }
        }
       if (isalertcontract == "1")
       {
            try
             {
                if (ContractAlarmCount != null)
                    alllistcount += ContractAlarmCount;
            } catch (ee) { }
        }
       if (isalertmemo == "1") 
       {
            try
             {
                if (BeiWangLuCount != null)
                    alllistcount += BeiWangLuCount;
            } catch (ee) { }
        }
       if (isalertmeet == "1") 
       {
            try 
            {
                if (CanHuiTongZhiCount != null)
                    alllistcount += CanHuiTongZhiCount;
            } catch (ee) { }
        }
       if(IsAlertPowerdata=="1")
       {
            try {
                if (OverduePowerdata != null)
                    alllistcount += OverduePowerdata;
            } catch (ee) { }
        }

        if (alllistcount > 0) 
        {
          
            document.getElementById("checkTask").src = "images/light.gif";//当提示框中有信息记录，更换原有的img url
            document.getElementById("checkTask").title = "您有新的待办任务，请尽快解决";
            //  document.getElementById("checkTask").attachEvent("onclick", showMe);
            $("#checkTask").bind("click", showMe);
           // $.messager.lays(300, 230);//指定提示框的大小样式
           $.messager.lays(300, 260);//指定提示框的大小样式 2012-10-15
            $.messager.show(' 您有新的待办事项,请尽快处理！ ', TableStr);
        }
        else 
        {
            document.getElementById("checkTask").title = "暂无待办事项！";
            document.getElementById("checkTask").src = "images/light_un.gif";
            $("#checkTask").bind("click", fnEmty);
         }
        returnResult = 0;
     }


    var MSG;
    function showMe() {
        $.messager.lays(310, 260);
        $.messager.show(' 您有新的待办事项,请尽快处理！ ', TableStr, 0);
    }

    function fnEmty()
    { }
         
    function getDate()//获取日期
    {
      var day=new Date();
      var year=day.getFullYear();
      
      var d2=day.toLocaleDateString();
      var month=d2.split("年")[1].split("月")[0];
      if(month<10)//将小于10的月份转换为以0开头的两位数月份，否则会出现日期比较错误
      {
        month="0"+month;
      }
      else
      {
        month=month;
      }
      var day=d2.split("月")[1].split("日")[0];
              if(day<10)
        day="0"+day;
      return (year+"-"+month+"-"+day);
    }
    function getModifyDate()//获取5天后的日期
    {
        var myDate=new Date();
        myDate.setDate(myDate.getDate()+5);
        var time2=myDate.toDateString();
        var month=time2.split(" ")[1];
        switch(month)//根据获得的字符串确定月份
        {
         case 'Jan':month=1;break;
         case 'Feb':month=2;break;
         case 'Mar':month=3;break;
         case 'Apr':month=4;break;
         case 'May':month=5;break;
         case 'June':month=6;break;
         case 'July':month=7;break;
         case 'Aug':month=8;break;
         case 'Sept':month=9;break;
         case 'Oct':month=10;break;
         case 'Nov':month=11;break;
         case 'Dec':month=12;break;
        }
        var day=time2.split(" ")[2]; 
        if(day<10)
        day="0"+day;
        var year=time2.split(" ")[3];
        return(year+"-"+month+"-"+day);

    }

    function SelectDept(infoType,pageType,dataType,showType)
    {
       if(dataType=="1")
       {
    //   parent.addTab(this);
    //   dataLink='Pages/Office/CustManager/Cust_Info.aspx?type='+infoType+'&data='+getDate();
    //   tabTitle=pageType;
         if(showType=='cust')
          parent.addTab(null, '2021101', ''+pageType+'', 'Pages/Office/CustManager/Cust_Info.aspx?type='+infoType+'&data='+getDate());
         if(showType=='provider')
          parent.addTab(null, '2041101','供应商档案','Pages/Office/PurchaseManager/ProviderInfoInfo.aspx?type='+infoType+'&data='+getDate());   
          if(showType=='MedFileDate')
          parent.addTab(null, '2081601','物品档案','Pages/Office/SupplyChain/ProductInfoList.aspx?type='+infoType+'&data='+getDate()); 
       }
       if(dataType=="0")
       {
         if(showType=='cust')
          parent.addTab(null, '2021101', ''+pageType+'', 'Pages/Office/CustManager/Cust_Info.aspx?type='+infoType+'&data='+getDate()+'|'+getModifyDate());
         if(showType=='provider')
          parent.addTab(null, '2041101','供应商档案','Pages/Office/PurchaseManager/ProviderInfoInfo.aspx?type='+infoType+'&data='+getDate()+'|'+getModifyDate());
         if(showType=='MedFileDate')
          parent.addTab(null, '2081601','物品档案','Pages/Office/SupplyChain/ProductInfoList.aspx?type='+infoType+'&data='+getDate()+'|'+getModifyDate());
       }
    }

