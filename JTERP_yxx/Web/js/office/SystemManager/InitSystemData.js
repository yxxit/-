/**********************************************
 * JS作用：   系统初始化数据
 * 建立人：   宋凯歌
 * 建立时间： 2010/11/12
 ***********************************************/
/* 页面初期显示 */

/*
* 保存组织机构时获取拼音缩写
*/
function GetPYShort() 
{
    if (!CheckInput()) 
    {
        return;
    }
    //获取机构名称
    var strdeptName = escape(document.getElementById("txtDeptName").value.Trim());
    //未输入时，返回不获取拼音缩写
    if (strdeptName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + strdeptName;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?" + postParams,
            dataType:'json',//返回json格式数据
            cache:false,
            error: function()
            {
            }, 
            success:function(data) 
            {
                //获取成功时，设置拼音缩写
                if(data.sta == 1)
                {
                    //设置拼音缩写
                    document.getElementById("hidDeptName").value = data.info;
                    fnAdd();
                }
            } 
        });
    }
}

///添加基本数据
function fnAdd() 
{

    //GetPYShort();//保存组织结构时机构名称的拼音缩写
//    var tabDeptInfo = "0",tabEmployeeInfo="0";
    var isFlag = true;
    var hidDeptName = document.getElementById("hidDeptName").value;//组织结构拼音缩写 
    var DeptName = document.getElementById("txtDeptName").value;//组织结构名称
    var EmployeeName = $("#txtEmployeeName").val();
    var HidEmployeeName = $("#hidEmployeeName").val();
    var StorageName = $("#txtStorageName").val();
    var CustName = $("#txtCustName").val();
    var HidCustName = $("#hidCustName").val();
    var LinkManName = $("#txtLinkManName").val();
    var WorkTel = $("#txtWorkTel").val();
    var TypeName = $("#txtTypeName").val();
    var ProName = $("#txtProName").val();
    var User = trim(document.getElementById("txtUser").value);//用户姓名
    var Password = trim(document.getElementById("txtPassword").value);//密码
//    if (tabDeptInfo == "0" && tabEmployeeInfo=="0") 
//    {
//        popMsgObj.ShowMsg('请选择！');
//    }
            if (confirm("确定后将添加默认数据！您确定要添加？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SystemManager/InitSystemData.ashx?HidDeptName="+hidDeptName+"&DeptName="+DeptName+"&EmployeeName="+EmployeeName+"&HidEmployeeName="+HidEmployeeName+"&StorageName="+StorageName+"&CustName="+CustName+"&HidCustName="+HidCustName+"&LinkManName="+LinkManName+"&WorkTel="+WorkTel+"&TypeName="+TypeName+"&ProName="+ProName+"&User="+User+"&Password="+Password+"",                   
                    dataType: 'json', //返回json格式数据
                    cache: false,
                    beforeSend: function() {
                        AddPop();
                    },

                    error: function() {

                        showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
                    },
                    success: function(data) {
                        if (data.sta == 1) {                            
                            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "添加成功！");
                        }
                        else if (data.sta == 3) {                            
                            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "已存在此用户，请重新输入！");
                        }
                        else {
                           showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "添加失败！");
                        }
                    }
                });
            }
}

//表单验证
function CheckInput() 
{
    var fieldText = "";
    var isFlag = true;
    var msgText = "";
    var DeptName = $("#txtDeptName").val();
    var EmployeeName = $("#txtEmployeeName").val();
    var StorageName = $("#txtStorageName").val();
    var CustName = $("#txtCustName").val();
    var LinkManName = $("#txtLinkManName").val();
    var WorkTel = $("#txtWorkTel").val();
    var TypeName = $("#txtTypeName").val();
     var ProName = $("#txtProName").val();
    var txtUser = trim(document.getElementById("txtUser").value);//用户姓名
    var txtPassword = trim(document.getElementById("txtPassword").value);//密码
    var RetVal = CheckSpecialWords();
    if (RetVal != "") 
    {
        isFlag = false;
        fieldText = fieldText + RetVal + "|";
        msgText = msgText + RetVal + "不能含有特殊字符|";
    }
    if(txtUser=="")
    {
        isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText +  "请输入用户名|";
   		
    }
    if(txtUser!="")
    {
        if(txtUser.length>10)
        {
            isFlag = false;
            fieldText = fieldText + "用户名|";
   		    msgText = msgText + "用户名长度必须在8-10位之间|";
        }
    }
    if(txtUser.length<8)
    {
        isFlag = false;
        fieldText = fieldText + "用户名|";
   		msgText = msgText + "用户名长度必须在8-10位之间|";
    }
    if(!txtUser.match(/^[a-zA-Z]+[\d]+([a-zA-Z0-9])*$|^[\d]+[a-zA-Z]+([a-zA-Z0-9])*$/))
    {
        isFlag = false;
        fieldText = fieldText + "用户名|";
	    msgText = msgText + "用户名必须是字母、数字的组合|";
    }
    
    if(txtPassword=="")
    {
        isFlag = false;
        fieldText = fieldText + "密码|";
   		msgText = msgText + "请输入密码|";
    }
    else 
    { 
        if(strlen(txtPassword)>16||strlen(txtPassword)<8)
        {
            isFlag = false;
            fieldText = fieldText +  "密码|";
   		    msgText = msgText + "密码位数处于8-16位之间|";
       		
        }
        if(!txtPassword.match(/^[0-9a-zA-Z]+$/))
        { 
           isFlag = false;
            fieldText = fieldText + "密码|";
   		    msgText = msgText + "密码必须是字母、数字的组合|";
        }
     
    }
    if(DeptName == "")
    {
        isFlag = false;
        fieldText = fieldText + "组织机构名称|";
        msgText = msgText + "请输入组织机构名称|";
    }
    if(EmployeeName == "")
    {
        isFlag = false;
        fieldText = fieldText + "人员档案名称|";
        msgText = msgText + "请输入人员档案名称|";
    }
    if(StorageName == "")
    {
        isFlag = false;
        fieldText = fieldText + "仓库名称|";
        msgText = msgText + "请输入仓库名称|";
    }
    if(CustName == "")
    {
        isFlag = false;
        fieldText = fieldText + "客户名称|";
        msgText = msgText + "请输入客户名称|";
    }
        if(LinkManName == "")
    {
        isFlag = false;
        fieldText = fieldText + "联系人|";
        msgText = msgText + "请输入联系人|";
    }
        if(WorkTel == "")
    {
        isFlag = false;
        fieldText = fieldText + "联系人电话|";
        msgText = msgText + "请输入联系人电话|";
    }
    if(TypeName == "")
    {
        isFlag = false;
        fieldText = fieldText + "供应商类别|";
        msgText = msgText + "请输入供应商类别|";
    }
    if(ProName == "")
    {
        isFlag = false;
        fieldText = fieldText + "供应商名称|";
        msgText = msgText + "请输入供应商名称|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText, msgText);
    }
     return isFlag;
}

/*
* 保存组织机构时获取拼音缩写
*/
function GetPYShortForEmployee() 
{
    if (!CheckInput()) 
    {
        return;
    }
    //获取机构名称
    var strEmployeeName = escape(document.getElementById("txtEmployeeName").value.Trim());
    //未输入时，返回不获取拼音缩写
    if (strEmployeeName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + strEmployeeName;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?" + postParams,
            dataType:'json',//返回json格式数据
            cache:false,
            error: function()
            {
            }, 
            success:function(data) 
            {
                //获取成功时，设置拼音缩写
                if(data.sta == 1)
                {
                    //设置拼音缩写
                    document.getElementById("hidEmployeeName").value = data.info;
                }
            } 
        });
    }
}

/*
* 保存组织机构时获取拼音缩写
*/
function GetPYShortForCust() 
{
    if (!CheckInput()) 
    {
        return;
    }
    //获取机构名称
    var strCustName = escape(document.getElementById("txtCustName").value.Trim());
    //未输入时，返回不获取拼音缩写
    if (strCustName == "")
    {
        return;
    }
    //输入时，获取拼音缩写
    else
    {
        postParams = "Text=" + strCustName;
        $.ajax({ 
            type: "POST",
            url: "../../../Handler/Common/PYShort.ashx?" + postParams,
            dataType:'json',//返回json格式数据
            cache:false,
            error: function()
            {
            }, 
            success:function(data) 
            {
                //获取成功时，设置拼音缩写
                if(data.sta == 1)
                {
                    //设置拼音缩写
                    document.getElementById("hidCustName").value = data.info;
                }
            } 
        });
    }
}


  
