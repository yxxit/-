$(document).ready(function() {

     TurnToPage();
    document.getElementById("inputCompanyCD").disabled=false; //企业代码
    document.getElementById("inputNameCn").disabled=false; //企业中文名称
    document.getElementById("inputNameEn").disabled=false; //英文名称
    document.getElementById("inputNameShort").disabled=false; //企业简称 
    document.getElementById("inputPYShort").disabled=false; //企业拼音代码
    document.getElementById("inputDocSavePath").disabled=false; //文档存放根目录 
});
 
//jQuery-ajax获取JSON数据
function  TurnToPage(){
  
    ActionFlag = "Search"
    $.ajax({
       
        type: "POST",
        url: "../../../Handler/Office/SystemManager/Company_Add.ashx?action=" + ActionFlag,
        dataType: 'json', //返回json格式数据,       
        beforeSend: function() { AddPop(); $("#pageDataList1_Pager").hide(); }, //发送数据之前

        success: function(msg) {
            //数据获取完毕，填充页面据显示
            //数据列表
            
            $("#pageDataList1 tbody").find("tr.newrow").remove();
            
            $.each(msg.data, function(i, item) {
                if (item.CompanyCD != null && item.CompanyCD != "")
                {
                    var UsedStatus = "";
                    if (item.UsedStatus != "0")
                    {
                        UsedStatus="启用";
                    }
                    else
                    {
                        UsedStatus="停用";
                    }
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center' title=\"" + item.CompanyCD + "\"><a href='#' onclick=\"Show('" + item.CompanyCD + "','" + item.NameCn + "','" + item.NameEn + "','" + item.NameShort + "','" + item.PYShort + "','" + item.DocSavePath + "','" + item.UsedStatus+ "')\">" + item.CompanyCD+ "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.NameCn + "\">" + fnjiequ(item.NameCn, 10) + "</td>" +
                         "<td height='22' align='center'>" + item.UserID + "</td>" +
                          
                           "<td height='22' align='center'>" + item.UserID + "</td>" +
                           
                        "<td height='22' nowrap  align='center' style='overflow:hidden;text-overflow:ellipsis' onmouseover='this.title=this.innerText'>" + UsedStatus + "</td>").appendTo($("#pageDataList1 tbody"));
                }
            });          
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {  popMsgObj.ShowMsg( textStatus); },
        complete: function() { hidePopup();pageDataList1("pageDataList1", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
    });
}
function Show(CompanyCD,NameCn,NameEn,NameShort,PYShort,DocSavePath,UsedStatus)
{
    document.getElementById('div_Add').style.display = 'block';    
    document.getElementById("div_Add").style.zIndex = "2";
    document.getElementById("divBackShadow").style.zIndex = "1";
if (typeof (CompanyCD) != "undefined") {
    document.getElementById("inputCompanyCD").value=CompanyCD; //企业代码
    document.getElementById("inputNameCn").value=NameCn; //企业中文名称
    document.getElementById("inputNameEn").value=NameEn; //英文名称
    document.getElementById("inputNameShort").value=NameShort; //企业简称 
    document.getElementById("inputPYShort").value=PYShort; //企业拼音代码
    document.getElementById('inputDocSavePath').value = 'E:\\CompanyDocs\\'+document.getElementById('inputCompanyCD').value;; //文档存放根目录 
    document.getElementById("drp_use").value=UsedStatus;//启用状态
    document.getElementById("HidCompanyCD").value=CompanyCD;

    document.getElementById("inputCompanyCD").disabled=true; //企业代码
    document.getElementById("inputNameCn").disabled=true; //企业中文名称
    document.getElementById("inputNameEn").disabled=true; //英文名称
    document.getElementById("inputNameShort").disabled=true; //企业简称 
    document.getElementById("inputPYShort").disabled=true; //企业拼音代码
    document.getElementById("inputDocSavePath").disabled=true; //文档存放根目录 
 }
 else
 {
    New();
 }
    

}
//table行颜色
function pageDataList1(o, a, b, c, d) {
    var t = document.getElementById(o).getElementsByTagName("tr");
    for (var i = 1; i < t.length; i++) {
        t[i].style.backgroundColor = (t[i].sectionRowIndex % 2 == 0) ? a : b;
        t[i].onmouseover = function() {
            if (this.x != "1") this.style.backgroundColor = c;
        }
        t[i].onmouseout = function() {
            if (this.x != "1") this.style.backgroundColor = (this.sectionRowIndex % 2 == 0) ? a : b;
        }
    }
}
function AlertMsg() {

    /**第一步：创建DIV遮罩层。*/
    var sWidth, sHeight;
    sWidth = window.screen.availWidth;
    //屏幕可用工作区高度： window.screen.availHeight;
    //屏幕可用工作区宽度： window.screen.availWidth;
    //网页正文全文宽：     document.body.scrollWidth;
    //网页正文全文高：     document.body.scrollHeight;
    if (window.screen.availHeight > document.body.scrollHeight) {  //当高度少于一屏
        sHeight = window.screen.availHeight;
    } else {//当高度大于一屏
        sHeight = document.body.scrollHeight;
    }
    //创建遮罩背景
    var maskObj = document.createElement("div");
    maskObj.setAttribute('id', 'BigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#777";
    maskObj.style.filter = "Alpha(opacity=30);";
    maskObj.style.opacity = "0.3";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "900";
    document.body.appendChild(maskObj);

}

function CloseDiv() {
    closeRotoscopingDiv(false, 'divBackShadow');
}

function Hide() {
    CloseDiv();
    document.getElementById('div_Add').style.display = 'none';
    New();
    TurnToPage();
}
function New() {
    document.getElementById("inputCompanyCD").value=''; //企业代码
    document.getElementById("inputNameCn").value=''; //企业中文名称
    document.getElementById("inputNameEn").value=''; //英文名称
    document.getElementById("inputNameShort").value=''; //企业简称 
    document.getElementById("inputPYShort").value=''; //企业拼音代码
    document.getElementById("inputDocSavePath").value=''; //文档存放根目录 
    document.getElementById("drp_use").value='1';//启用状态
    document.getElementById("HidCompanyCD").value='';
    document.getElementById("inputCompanyCD").disabled=false; //企业代码
    document.getElementById("inputNameCn").disabled=false; //企业中文名称
    document.getElementById("inputNameEn").disabled=false; //英文名称
    document.getElementById("inputNameShort").disabled=false; //企业简称 
    document.getElementById("inputPYShort").disabled=false; //企业拼音代码
    document.getElementById("inputDocSavePath").disabled=false; //文档存放根目录 
}
function GetRoot(){
          document.getElementById('inputDocSavePath').value = 'E:\\CompanyDocs\\'+document.getElementById('inputCompanyCD').value;
          CheckCompanyCD();
       }
       
       

/*
* 获取拼音缩写
*/
function GetPYShort()
{
    NameCn = document.getElementById("inputNameCn").value;
    if (NameCn  == "")
    {
        return;
    }
    else if (!CheckSpecification(NameCn)) {
        var fieldText = "";
        var msgText = "";
        fieldText = fieldText + "企业中文名称|";
        msgText = msgText + "企业中文名称格式不正确|";
        popMsgObj.Show(fieldText, msgText);

    }
    
    else 
    {
    postParams = "Text=" + NameCn ;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Common/PYShort.ashx?" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            
            {
                document.getElementById("inputPYShort").value = data.info;
            }
        } 
    });
    }
}
function CheckInput() {
    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    //var hiddenUserID = document.getElementById("hiddenUserID").value; //隐藏用户ID是否存在
    var inputCompanyCD = trim(document.getElementById("inputCompanyCD").value); //企业代码
    var inputNameCn= trim(document.getElementById("inputNameCn").value); //企业中文名称
    var inputNameEn = trim(document.getElementById("inputNameEn").value); //英文名称
    var inputNameShort = trim(document.getElementById("inputNameShort").value); //企业简称 
    var inputPYShort = document.getElementById("inputPYShort").value; //企业拼音代码
    var inputDocSavePath = document.getElementById("inputDocSavePath").value; //文档存放根目录 
    if (inputCompanyCD == "") {
        isFlag = false;
        fieldText = fieldText + "企业代码|";
        msgText = msgText + "请输入企业代码|";

    }
    if (!CheckSpecification(inputCompanyCD)) {
        isFlag = false;
        fieldText = fieldText + "企业代码|";
        msgText = msgText + "企业代码格式不正确|";

    }
    
    if (inputCompanyCD != "") {
        if (inputCompanyCD.length > 5) {
            isFlag = false;
            fieldText = fieldText + "企业代码|";
            msgText = msgText + "企业代码长度必须5位|";
        }
       
        if (!inputCompanyCD.match(/^[a-zA-Z]+[\d]+([a-zA-Z0-9])*$|^[\d]+[a-zA-Z]+([a-zA-Z0-9])*$/)) {
            isFlag = false;
            fieldText = fieldText + "企业代码|";
            msgText = msgText + "企业代码必须是字母或数字的的组合|";
        }
    }

    if (inputNameCn == "") {
        isFlag = false;
        fieldText = fieldText + "企业中文名称|";
        msgText = msgText + "请输入企业中文名称|";
    }
     if (!CheckSpecification(inputNameCn)) {
        isFlag = false;
        fieldText = fieldText + "企业中文名称|";
        msgText = msgText + "企业中文名称格式不正确|";

    }
     if (!CheckSpecification(inputNameEn)) {
        isFlag = false;
        fieldText = fieldText + "英文名称|";
        msgText = msgText + "英文名称格式不正确|";

    }
    if (!CheckSpecification(inputNameShort)) {
        isFlag = false;
        fieldText = fieldText + "企业简称|";
        msgText = msgText + "企业简称格式不正确|";

    }
    if (!CheckSpecification(inputPYShort)) {
        isFlag = false;
        fieldText = fieldText + "企业拼音代码|";
        msgText = msgText + "企业拼音代码格式不正确|";

    }
    if (inputDocSavePath == "") {
        isFlag = false;
        fieldText = fieldText + "文档存放根目录 |";
        msgText = msgText + "请输入文档存放根目录 |";
    }
  

    if (!isFlag) {
        //注：两个方法显示弹出提示信息层,方法一是对字段用红色处理，方法二是所有的提示信息传入未处理颜色

        //方法一
        popMsgObj.Show(fieldText, msgText);

        //方法二
        //popMsgObj.ShowMsg('所有的错误信息字符串');
    }

    return isFlag;
}
function CheckCompanyCD() {
    var inputCompanyCD = trim(document.getElementById("inputCompanyCD").value); //企业代码
    if (inputCompanyCD.length != 0) {
        var Action="CheckCompanyCD";
        $.ajax({
            type: "POST",
            url: "../../../Handler/Office/SystemManager/Company_Add.ashx?Action=" +Action+"&CompanyCD=" + inputCompanyCD,
            //data:'strcode='+$("#txtEquipCode").val()+'&tablename='+tablename,
            dataType: 'json', //返回json格式数据
            cache: false,
            beforeSend: function() {
                // AddPop();
            },
            //complete :function(){hidePopup();},
            error: function() {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
            },
            success: function(data) {
                if (data.sta != 1) {
                    popMsgObj.ShowMsg(data.data);
                   
                }
                
            }
        });
    }

}
function InsertCompanyData() {
    if (!CheckInput()) {
        return;
    }
    var inputCompanyCD = trim(document.getElementById("inputCompanyCD").value); //企业代码
    var inputNameCn= trim(document.getElementById("inputNameCn").value); //企业中文名称
    var inputNameEn = trim(document.getElementById("inputNameEn").value); //英文名称
    var inputNameShort = trim(document.getElementById("inputNameShort").value); //企业简称 
    var inputPYShort = document.getElementById("inputPYShort").value; //企业拼音代码
    var inputDocSavePath = document.getElementById("inputDocSavePath").value; //文档存放根目录 
    var  drp_use=document.getElementById("drp_use").value;//启用状态
   var HidCompanyCD= document.getElementById("HidCompanyCD").value;//ID
   
   var Action="Add";
    if (HidCompanyCD!="")
    {   
        Action="Update";
    }
    var UrlParam ="Action=" +Action+
                        "&CompanyCD=" + inputCompanyCD  +
                        "&NameCn=" + inputNameCn +
                        "&NameEn=" + inputNameEn +
                        "&NameShort=" + inputNameShort +
                        "&PyShort=" + inputPYShort+
                        "&UsedStatus=" + drp_use+
                        "&HidCompanyCD=" + HidCompanyCD+
                        "&DocSavePath=" + inputDocSavePath;
                        
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/SystemManager/Company_Add.ashx?" + UrlParam,
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            //AddPop();
        },
        //complete :function(){ //hidePopup();},
        error: function() {
            popMsgObj.ShowMsg('请求发生错误');

        },
        success: function(data) {
            if (data.sta == 1) {
                popMsgObj.ShowMsg(data.info);
               
                
                
            }
            else {
                popMsgObj.ShowMsg(data.info);
            }
        }
    });


}

    /*验证规格只允许+和*特殊字符可以输入*/
    function CheckSpecification(str) {
        var SpWord = new Array("'", "\\", "<", ">", "%", "?", "/"); //可以继续添加特殊字符 此 /  字符也不可输入 输出时会破坏JSON格式
        for (var i = 0; i < SpWord.length; i++) {
            for (var j = 0; j < str.length; j++) {
                if (SpWord[i] == str.charAt(j)) {
                    return false;
                    break;
                }
            }
        }
        return true;
    }