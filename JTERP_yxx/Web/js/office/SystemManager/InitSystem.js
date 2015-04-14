/**********************************************
 * JS作用：   系统初始化
 * 建立人：   钱锋锋
 * 建立时间： 2010/10/22
 ***********************************************/
/* 页面初期显示 */
var JXC=document.getElementById("JXC");
var CCust=document.getElementById("CCust");
CCust.disabled=true;
var Base=document.getElementById("Base");
Base.disabled=true;
function Check()
{
    
    if(JXC.checked)
    {
        CCust.disabled=false;
    }
    else
    {
        CCust.disabled=true;
        CCust.checked=false;
        Base.disabled=true;
        Base.checked=false;
    }
}
function CheckC()
{
    if(CCust.checked)
    {
        Base.disabled=false;
    }
    else
    {
        Base.disabled=true;
        Base.checked=false;
    }
}
///删除竞争对手
function fnDel() {
    var flag=0;
    var isFlag = true;
    if (JXC.checked)
    {
        flag=1;
    }
    if(CCust.checked)
    {
        flag=2;
    }
    if(Base.checked)
    {
        flag=3;
    }       
    if (flag== 0) {
        popMsgObj.ShowMsg('请选择！');
    }
    else {
        if (isFlag) {
            if (confirm("数据删除后将不可恢复！您确定要删除？")) {
                //删除
                $.ajax({
                    type: "POST",
                    url: "../../../Handler/Office/SystemManager/InitSystem.ashx?flag=" + flag,                   
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
                            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除成功！");
                        }
                        else {
                           showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "删除失败！");
                        }
                    }
                });
            }
        }
        else {
            popMsgObj.Show(fieldText, msgText);
        }
    }
}