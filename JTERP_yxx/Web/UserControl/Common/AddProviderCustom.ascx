<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddProviderCustom.ascx.cs"
    Inherits="UserControl_Common_AddProviderCustom" %>
      
    <%@ Register Src="~/UserControl/CodingRuleControl.ascx" TagName="CodingRuleControl"
    TagPrefix="uc3" %>
    
<div id="divRotoscopingpAddProCus" style="display: none">
    <iframe id="divIframeAddProCus" frameborder="0" width="100%"></iframe>
</div>
<div id="divAddProviderCustom" style="width:70%; z-index: 100; position: absolute;
    display: none;">
    <table border="0" cellspacing="1" bgcolor="#999999" style="width: 100%">
        <tr>
            <td bgcolor="#EEEEEE" align="center">
                <table width="100%">
                    <tr class="td_list_title">
                        <td align="left" onmousedown="MoveDiv('divAddProviderCustom',event)" title="点击此处可以拖动窗口"
                            onmousemove="this.style.cursor='move';" >
                            &nbsp;&nbsp;添加往来单位
                        </td>
                        <td width="20" align="right">
                            <img src="../../../images/default/0420close.gif" onclick='closeAddProCustom();' style="cursor: hand;" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" bgcolor="#999999">
                    <tr>
                        <td bgcolor="#ffffff" width="99%" align="center" valign="bottom">
                            <!-- Start 基本信息 -->
                            <table width="99%" border="0" align="center" cellpadding="2" id="Tb_01">
                                <tr>
                                   <td align="right" width="13%"  class="td_list_fields"  >
                                        往来单位编号<span class="redbold">*</span>
                                    </td>
                                    <td bgcolor="#FFFFFF" width="20%">
                                        <div id="divCodeRule" runat="server">
                                            <uc3:CodingRuleControl ID="ddlCustNo" runat="server" />
                                        </div>
                                         <div id="divCustNo" runat="server">
                                        </div>
                                    </td>
                                   <td align="right" width="12%" class="td_list_fields"  >
                                        往来单位类型
                                    </td>
                                    <td bgcolor="#FFFFFF" width="23%">
                                        <select id="selProviderCustomType" style="width: 95%" disabled>
                                            <option value="1">供应商</option>
                                            <option value="2">客户</option>
                                            <option value="3">客户与供应商</option>
                                            <option value="4">银行</option>
                                            <option value="5">物流</option>
                                            <option value="6">其他</option>
                                        </select>
                                    </td>
                                   <td align="right"  width="12%" class="td_list_fields"  >
                                        往来单位名称<span class="redbold">*</span>
                                    </td>
                                    <td bgcolor="#FFFFFF" width="24%">
                                        <input id="txtPCNameUC" maxlength="30" specialworkcheck="往来单位名称" type="text" class="busTdInput"
                                            style="width: 93%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        开户行
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtBankName" type="text" class="busTdInput" size="19" maxlength="30" style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        户名
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtAccountName" style="width: 93%" type="text" class="busTdInput" maxlength="30" />
                                    </td>
                                    <td align="right">
                                        账号
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtAccountNo" maxlength="25" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        联系人
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtContactorUC" type="text" class="busTdInput" size="19" maxlength="25"
                                            style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        联系电话
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtContactTelUC" style="width: 93%" type="text" class="busTdInput" maxlength="25" />
                                    </td>
                                    <td align="right">
                                        手机
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtMobilePhoneUC" maxlength="25" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        传真
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtFaxUc" maxlength="25" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        公司网址
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtCompanyWebSiteUC" maxlength="30" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        E-mail
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtEmailUC" maxlength="25" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        QQ&nbsp;
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtQQNoUC" maxlength="25" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        地址 &nbsp;
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtAddressUC" maxlength="50" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                    <td align="right">
                                        邮编
                                    </td>
                                    <td bgcolor="#FFFFFF">
                                        <input id="txtPostCodeUC" maxlength="10" type="text" class="busTdInput" style="width: 93%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" align="center">
                                        <input type="button" class="busBtn" value=" 确定 " onclick="addProviderCustom();" />
                                    </td>
                                </tr>
                            </table>
                            <!-- End 基本信息 -->
                            <!-- Start -->
                            <!-- End  -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript">
/*
页面必须引用 肖合明 的弹出JS
调用方法：
showAddForPage(stype,textID,valueID)

stype: 表示往来单位类型  1.供应商/2.客户/3.客户与供应商/4.银行/5.物流/6.其他
textID:文本控件ID
valueID:值控件ID

自动添加填充到指定的控件
*/





//用于保存返回填充控件的ID
var valProCusID="";
var txtProCusID="";
//用户保存添加往来单位的类型 默认为客户
//1.供应商/2.客户/3.客户与供应商/4.银行/5.物流/6.其他
var type="2";


// 处理单位类型
function SetProvider(stype)
{
    if(stype.Trim()=="")
    {
        $("#selProviderCustomType").val(type);
        return;
    }
    var a=stype.Trim().split(',');
    var i=0;
    type=a[i];
    $("#selProviderCustomType").val(type);
    for(i=1;i<7;i++)
    {
        if(stype.indexOf(i)<0)
        {
            $("#selProviderCustomType option[value='"+i+"']").remove();
        }
    }    
    if(a.length>1)
    {
        $("#selProviderCustomType").removeAttr("disabled");
    }
}


function showAddForPage(stype,textID,valueID)
{
    $("#txtPCNameUC").val("");
    $("#txtContactorUC").val("");
    $("#txtContactTelUC").val("");
    $("#txtMobilePhoneUC").val("");
    $("#txtFaxUc").val("");
    $("#txtCompanyWebSiteUC").val("");
    $("#txtEmailUC").val("");
    $("#txtAddressUC").val("");
    $("#txtPostCodeUC").val("");
    $("#txtBankName").val("");
    $("#txtAccountName").val("");
    $("#txtAccountNo").val(""); 
    
    SetProvider(stype);
    
    //保存控件ID
    valProCusID=valueID;
    txtProCusID=textID;
    
    //显示层
    showAddProCustom();
}

//表单验证
function checkform()
{ 
    var fieldText = "";
    var msgText = "";
    var isFlag = true;   
    var ddlCustNoType =$("#AddProviderCustom1_ddlCustNo_ddlCodeRule").val();//客户编号类型
    var txtCustNo = $("#AddProviderCustom1_ddlCustNo_txtCode").val();// 客户编号

    var txtPCNameUC=$("#txtPCNameUC").val();//客户名称
    var txtBankName=$("#txtBankName").val();//开户行
    var txtAccountName=$("#txtAccountName").val();//户名
    var txtAccountNo=$("#txtAccountNo").val();//帐号
    var txtContactorUC=$("#txtContactorUC").val();//联系人
    var txtContactTelUC=$("#txtContactTelUC").val();//联系电话
    var txtMobilePhoneUC=$("#txtMobilePhoneUC").val();//手机
    var txtFaxUc=$("#txtFaxUc").val();//传真
    var txtCompanyWebSiteUC=$("#txtCompanyWebSiteUC").val();//网址
    var txtEmailUC=$("#txtEmailUC").val();//电子邮件
    var txtQQNoUC=$("#txtQQNoUC").val();//QQ
    var txtAddressUC=$("#txtAddressUC").val();//地址
    var txtPostCodeUC=$("#txtPostCodeUC").val();//邮政编码
    
    if (ddlCustNoType == "" && txtCustNo == "" )//如果选中的是手工输入时，编号必须输入
    {
        isFlag = false;
        fieldText = fieldText +  "客户编号|";
   		msgText = msgText +  "请输入客户编号|";
    }
    
     if(txtPCNameUC=="")
     {
         isFlag = false;
           fieldText = fieldText + "客户名称|";
        msgText="对不起，往来单位名称不能为空！\n";
     } 
     
      if(!isFlag)
    {       
        popMsgObj.Show(fieldText,msgText);
    }

    return isFlag;
}

//提交数据
function addProviderCustom()
{
     if(!checkform())
     {
        return;
     }
     
    //取值
     var CustNoType =$("#AddProviderCustom1_ddlCustNo_ddlCodeRule").val();
     var CustNo="";
     if(CustNoType=="")
     {
        CustNo = $("#AddProviderCustom1_ddlCustNo_txtCode").val(); //客户编号
     }
    var pcName=$("#txtPCNameUC").val(),
          pcContactor=$("#txtContactorUC").val(),
          pcTel=$("#txtContactTelUC").val(),
          pcMobile=$("#txtMobilePhoneUC").val(),
          pcFax=$("#txtFaxUc").val(),
          pcWebSite=$("#txtCompanyWebSiteUC").val(),
          pcEmail=$("#txtEmailUC").val(),
          pcAddress=$("#txtAddressUC").val(),
          pcPostCode=$("#txtPostCodeUC").val(),
          pcBackName=$("#txtBankName").val(),
          pcAccountName=$("#txtAccountName").val(),
          pcAccountNo=$("#txtAccountNo").val();
    
//    //验证
//    var msgText="";
//   
//     if(pcName=="")
//     {
//       msgText="对不起，往来单位名称不能为空！\n";
//     } 
////     if(pcBackName=="")
////     {
////        msgText+="对不起，开户行不能为空！\n"
////     }
////     if(pcAccountName=="")
////     {
////        msgText+="对不起，户名不能为空！\n"
////     }
////      if(pcAccountNo=="")
////     {
////        msgText+="对不起，账号能为空！\n"
////     }
//     
//     if(msgText!="")
//     {
//        popMsgObj.ShowMsg(msgText);
//        return;
//     }
//     
    //构造参数
    var paras="CustNoType="+escape(CustNoType)+
                   "&CustNo="+escape(CustNo)+
                   "&CustName="+escape(pcName)+
                    "&NatureType="+$("#selProviderCustomType").val()+
                    "&ContactName="+escape(pcContactor)+
                    "&Tel="+escape(pcTel)+
                    "&Mobile="+escape(pcMobile)+
                    "&Fax="+escape(pcFax)+
                    "&WebSite="+escape(pcWebSite)+
                    "&email="+escape(pcEmail)+
                    "&ReceiveAddress="+escape(pcAddress)+
                    "&Post="+escape(pcPostCode)+
                    "&OpenBank="+escape(pcBackName)+
                    "&AccountMan="+escape(pcAccountName)+
                    "&AccountNum="+escape(pcAccountNo)+
                    "&action=add";
     //异步页面
     var url="../../../Handler/Common/ProviderOrCustomSelect.ashx";
     
     
       $.ajax({
            type: "POST",
            dataType:"json",
            url: url,
            cache:false,
            data: paras,
            beforeSend:function(){ },
            success: function(msg){
                    //添加成功
                    if(msg.result)
                    {
                        closeAddProCustom();
                        //添加成功 填充控件
                        $("#"+valProCusID).val(msg.data);
                        $("#"+txtProCusID).val(pcName);
                        $("#"+txtProCusID).attr("title",msg.data);
                        try{
    
                             $("#txtCustLinkName").attr("value",pcContactor);//联系人名  
                             $("#txtCustLinkTel").attr("value",pcTel);//客户联系电话    
                             $("#txtCustLinkAddress").attr("value",pcAddress);//客户联系电话 
                             $("#Contractor").attr("value",pcContactor);//联系人名  
                             $("#ContractPhone").attr("value",pcTel);//客户联系电话    
                             $("#AriveAddress").attr("value",pcAddress);//客户联系电话 
   
                             }catch(Error){}
                    }
                    //添加失败
                    else
                    {
                        popMsgObj.ShowMsg(msg.data);
                    }
                  },
           error: function(msg) {}, 
           complete:function(){}//接收数据完毕
           });
    
    
}





//关闭
function closeAddProCustom()
{
    $("#divAddProviderCustom").hide();
    closeRotoscopingDiv(false,"divRotoscopingpAddProCus","divIframeAddProCus");
}
//显示层
function showAddProCustom()
{
    $("#divAddProviderCustom").show();
    openRotoscopingDiv(false,"divRotoscopingpAddProCus","divIframeAddProCus");
    CenterToDocument("divAddProviderCustom",true);
}




</script>

