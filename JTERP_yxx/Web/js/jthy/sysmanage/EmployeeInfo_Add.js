
/*
* 基本信息校验
*/
function CheckBaseInfo()
{
    //出错字段
    var fieldText = "";
    //出错提示信息
    var msgText = "";
    //是否有错标识
    var isErrorFlag = false;
    
    //获取编辑模式
    editFlag = document.getElementById("hidEditFlag").value;
    //新建时，编号选择手工输入时
    if ("INSERT" == editFlag)
    {
        //获取编码规则下拉列表选中项
        codeRule = document.getElementById("codruleEmployNo_ddlCodeRule").value;
        //如果选中的是 手工输入时，校验编号是否输入
        if (codeRule == "")
        {
            //获取输入的编号
            employeeNo = document.getElementById("codruleEmployNo_txtCode").value;
            //编号必须输入
            if (employeeNo == "")
            {
                isErrorFlag = true;
                fieldText += "编号|";
   		        msgText += "请输入编号|";
            } 
            else
            {
                if (!CodeCheck(employeeNo))
                {
                    isErrorFlag = true;
                    fieldText += "编号|";
                    msgText += "编号只能由英文字母 (a-z大小写均可)、数字 (0-9)、字符（_-/.()[]|";
                }
            }
        }
    }
    //姓名
    if (document.getElementById("txtEmployeeName").value == "")
    {
        isErrorFlag = true;
        fieldText += "姓名|";
        msgText += "请输入姓名|";
    }
    var RetVal=CheckSpecialWords();    
    if(RetVal!="")
    {
       isErrorFlag = true;
        fieldText = fieldText + RetVal+"|";
	    msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
        
    //获取分类
    flag = document.getElementById("ddlFlag").value;  
    
    //出身日期不为空时，判断出身日期是否正确
    var inputBirth = document.getElementById("txtBirth").value;
    if (inputBirth != "")
    {
        //获取当前系统日期
        systeDate  = document.getElementById("hidSysteDate").value;
        if (CompareDate(inputBirth, systeDate) == 1)
        {
            isErrorFlag = true;
            fieldText += "出生日期|";
            msgText += "您输入的出生日期晚于当前系统日期|";  
        }
    }
    
   
    //手机号码不为空时，判断手机号码是否正确
    if (document.getElementById("txtMobile").value != "")
    {
        if(!IsNumber(document.getElementById("txtMobile").value))
        {
            isErrorFlag = true;
            fieldText += "手机号码|";
            msgText += "请输入正确的手机号码|";  
        }
    }
    
    //电子邮件不为空时，判断电子邮件是否正确
    if (document.getElementById("txtEMail").value != "")
    {
        if(!IsEmail(document.getElementById("txtEMail").value))
        {
            isErrorFlag = true;
            fieldText += "电子邮件|";
            msgText += "请输入正确的电子邮件|";  
        }
    }
    
  
    
    //如果有错误，显示错误信息
    if(isErrorFlag)
    {
        //显示错误信息
        popMsgObj.Show(fieldText,msgText);
    }

    return isErrorFlag;
}


/*
* 保存人员信息
*/
function SaveEmployeeInfo()
{
    /* 页面信息进行校验 */
    //基本信息校验 有错误时，返回
    if (CheckBaseInfo())
    {
        return;
    //工作经历校验 有错误时，返回
    } 
    //获取人员基本信息参数
    postParams = GetBaseInfoParams();
    
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Office/HumanManager/EmployeeInfo_Add.ashx",
        data : postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                //设置编辑模式
                document.getElementById("hidEditFlag").value = data.info;
                /* 设置编号的显示 */ 
                //显示人员的编号 人员编号DIV可见              
                document.getElementById("divEmployeeNo").style.display = "block";
                //设置人员编号
                document.getElementById("divEmployeeNo").innerHTML = data.data;
                //编码规则DIV不可见
                document.getElementById("divCodeRule").style.display = "none";
                //设置人员编号
                //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存成功！");
                hidePopup();
                popMsgObj.ShowMsg("保存成功！");
                //相片地址
                //document.getElementById("hfPhotoUrl").value = document.getElementById("hfPagePhotoUrl").value
                //简历
                //document.getElementById("hfResume").value = document.getElementById("hfPageResume").value;
            }
            else if(data.sta == 2)
            {
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","您输入的编号已经存在！");
            }
            else 
            { 
                hidePopup();
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","保存失败,请确认！");
            } 
        } 
    });    
}

/*
* 获取人员信息基本资料
*/
function GetBaseInfoParams()
{
    editFlag = document.getElementById("hidEditFlag").value;
    var strParams = "&EditFlag=" + editFlag;//编辑标识
    var no = "";
    //更新的时候
    if ("UPDATE" == editFlag)
    {
        //人员编号
        no = document.getElementById("divEmployeeNo").innerHTML;
    }
    //插入的时候
    else
    {
        //获取编码规则ID
        codeRule = document.getElementById("codruleEmployNo_ddlCodeRule").value;
        //手工输入的时候
        if ("" == codeRule)
        {
            //人员编号
            no = document.getElementById("codruleEmployNo_txtCode").value;
        }
        else
        {
            //编码规则ID
            strParams += "&CodeRuleID=" + document.getElementById("codruleEmployNo_ddlCodeRule").value;
        }
    }
    //人员编号
    strParams += "&EmployeeNo=" + no;   
   
    
    strParams += "&EmployeeName=" + reescape(document.getElementById("txtEmployeeName").value);//姓名
    strParams += "&EmployeeNum=" + reescape(document.getElementById("txtEmployeeNum").value);//工号
    strParams += "&PYShort=" + reescape(document.getElementById("txtPYShort").value);//拼音缩写
 
    //人员分类
    flag = document.getElementById("ddlFlag").value;
    strParams += "&Flag=" + flag;

    strParams += "&Quarter=" + document.getElementById("ddlQuarter_ddlCodeType").value;//所在岗位
    strParams += "&DeptID=" + document.getElementById("hdDeptID").value;//所在部门    
    strParams += "&Sex=" + document.getElementById("ddlSex").value;//性别
    strParams += "&Birth=" + document.getElementById("txtBirth").value;//出身日期
    //strParams += "&Origin=" + reescape(document.getElementById("txtOrigin").value);//籍贯
    strParams += "&Telephone=" + reescape(document.getElementById("txtTelephone").value);//联系电话
    strParams += "&Mobile=" + reescape(document.getElementById("txtMobile").value);//手机号码
    strParams += "&EMail=" + reescape(document.getElementById("txtEMail").value);//电子邮件   
    strParams+="&EnterDate=" + reescape(document.getElementById("txtEnterDate").value);//入职时间
        
        
    
    return strParams;   
}


/*
* 人员分类改变时，改变对应的输入项目
*/
function ChangeFlag()
{
    //获取分类
    flag = document.getElementById("ddlFlag").value;
    //人才储备时
    if ("2" == flag)
    {
        //列名修改为应聘职务
        document.getElementById("divJobTitle").innerHTML = "应聘职务<span class='redbold'>*</span>";
        document.getElementById("divNum").innerHTML = "工号";
        //隐藏所在岗位
        document.getElementById("divQuarter").innerHTML = "";
        //所在岗位不显示
        document.getElementById("divQuarterValue").style.display = "none";
        //职称不显示
        document.getElementById("divPosition").style.display = "none";
        //部门不显示
        document.getElementById("divDeptName").style.display = "none";
        //隐岗位职等
        document.getElementById("divAdminLevelID").style.display = "none";
        //应聘职务显示
        document.getElementById("divPositionTitle").style.display = "block";
        //隐入职时间
        document.getElementById("divEnterDate").style.display = "none";  
        
        
    }
    //在职人员和离职人员时
    else
    {
        //列名修改为职称
        document.getElementById("divJobTitle").innerHTML = "职称<span class='redbold'>*</span>";
        //显示所在岗位
        document.getElementById("divQuarter").innerHTML = "岗位<span class='redbold'>*</span>";
        document.getElementById("divQuarter").style.display = "block";
        document.getElementById("divNum").innerHTML = "工号<span class='redbold'>*</span>";
        //职称显示
        document.getElementById("divPosition").style.display = "block";
        //所在岗位显示
        document.getElementById("divQuarterValue").style.display = "block";
        //应聘职务不显示
        document.getElementById("divPositionTitle").style.display = "none";
        
        //部门显示
        document.getElementById("divDeptName").style.display = "block";
        //显示岗位职等
        document.getElementById("divAdminLevelID").style.display = "block";
        //显示入职时间        
        document.getElementById("divEnterDate").style.display = "block";  
    }
}


/*
* 获取拼音缩写
*/
function GetPYShort()
{
    employeeName = document.getElementById("txtEmployeeName").value;
    if (employeeName == "")
    {
        return;
    }
    else
    {
    postParams = "Text=" + employeeName;
    $.ajax({ 
        type: "POST",
        url: "../../../Handler/Common/PYShort.ashx?" + postParams,
        dataType:'json',//返回json格式数据
        cache:false,
        error: function()
        {
            //showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            
            {
                document.getElementById("txtPYShort").value = data.info;
            }
        } 
    });
    }
}

/*
* 返回按钮
*/
function DoBack()
{
    //获取查询条件
    searchCondition = document.getElementById("hidSearchCondition").value;
    //迁移页面
    fromPage = document.getElementById("hidFromPage").value;
    //在职人员列表
    if (fromPage == "1")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidWorkModuleID").value;
        window.location.href = "EmployeeWork_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //离职人员列表
    else if (fromPage == "2")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidLeaveModuleID").value;
        window.location.href = "EmployeeLeave_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //人才储备列表
    else if (fromPage == "3")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidReserveModuleID").value;
        window.location.href = "EmployeeReserve_Info.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //新建面试
    else if (fromPage == "4")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidInterviewModuleID").value;
        window.location.href = "RectInterview_Edit.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //待入职
    else if (fromPage == "5")
    {
        //获取模块功能ID
        moduleID = document.getElementById("hidWaitModuleID").value;
        window.location.href = "WaitEnter.aspx?ModuleID=" + moduleID + searchCondition;
    }
    //人力资源初始化向导
    else if (fromPage == "6") {
        //获取模块功能ID
        moduleID = document.getElementById("hidInitHumanModuleID").value;
        window.location.href = "InitGuid.aspx?ModuleID=" + moduleID;
    }
    //系统管理初始化
    else if (fromPage == "7") {
        //获取模块功能ID
        moduleID = document.getElementById("hidInitSysModuleID").value;
        window.location.href = "../SystemManager/InitGuid.aspx?ModuleID=" + moduleID;
    }
}

function Continue()
{
//    var SearCondition = document.getElementById("hidSearchCondition").value;
//    var fromPage = document.getElementById("hidFromPage").value;
//    window.location.href = "EmployeeInfo_Add.aspx?ModuleID=2011201&type=Continue" + SearCondition +"&FromPage="+fromPage;
    document.getElementById("txtEmployeeName").value = "";
    document.getElementById("txtPYShort").value = "";

    document.getElementById("hidEditFlag").value = "INSERT";

    document.getElementById("divEmployeeNo").style.display = "none";
    document.getElementById("divEmployeeNo").innerHTML = "";
    document.getElementById("divCodeRule").style.display = "block";

    var NoType = document.getElementById("codruleEmployNo_ddlCodeRule").value;

    if(NoType == "")
    {
        document.getElementById("codruleEmployNo_txtCode").value = "";
    }
    else
    {
        document.getElementById("codruleEmployNo_txtCode").value = "保存时自动生成";
    }

    //document.getElementById("codruleEmployNo_txtCode").value = "";

}
 