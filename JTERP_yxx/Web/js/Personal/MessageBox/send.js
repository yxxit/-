
//LoadUserList
//LoadUserListWithDepartment
//LoadUserListWithGroup

function showInfo(msg) {
    //document.getElementById("infoTip").innerHTML = msg;
    showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", msg);


}





function SendInfo(obj) {
    // document.getElementById("txtToList").value = userlist;
    // document.getElementById("seluseridlist").value = useridlist

    var title = document.getElementById("txtTitle").value;
    var content = document.getElementById("txtContent").value;
    var ids = document.getElementById("seluseridlist").value;

    if (title + "" == "") {
        showInfo("请填写信息标题");
        return;
    }
    if (title.length > 50) {
        showInfo("标题 长度不能超过50");
        return;
    }


    if (content + "" == "") {
        showInfo("请填写信息内容");
        return;
    }
    if (content.length > 500) {
        showInfo("内容 长度不能超过500");
        return;
    }

    if (ids + "" == "") {
        showInfo("请选择收件人");
        return;
    }

    var hassmFlag = document.all("smFlag").checked;
    var hassmFlagSite = document.all("smFlagSite").checked;

    if (!hassmFlag && !hassmFlagSite) {
        showInfo("请选择发送方式，即发送手机短信，还是发送站内短信");
        return;
    }


    var action = "SendInfo";
    var prams = "title=" + UrlEncode(title);
    prams += "&content=" + UrlEncode(content);
    prams += "&IDList=" + ids;


    if (hassmFlag && hassmFlagSite) {
        prams += "&smFlag=3";
    } else if (hassmFlagSite) {
        prams += "&smFlag=2";
    } else if (hassmFlag) {
        prams += "&smFlag=1";
    }



    obj.disabled = true;

    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/SendInfo.ashx?action=" + action,
        dataType: 'string',
        data: prams,
        cache: false,
        success: function(data) {
            obj.disabled = false;

            var result = null;
            try {
                eval("result = " + data);
            } catch (e) {
                alert(e.message + "\n\n" + data);
                return;
            }

            if (result.result) {
                showInfo(result.data);

                document.getElementById("txtTitle").value = "";
                document.getElementById("txtContent").value = "";
                document.getElementById("seluseridlist").value = "";
                document.getElementById("txtToList").value = "";

                treeview_unsel();

            } else {
                showInfo(result.data);
            }
        },
        error: function(data) {
            obj.disabled = false;

            showInfo(data.responseText);
        }
    });






















}















function LoadUserList(action, callback) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/SendInfo.ashx?action=" + action,
        dataType: 'string',
        data: '',
        cache: false,
        success: function(data) {
            var result = null;
            eval("result = " + data);

            if (result.result) {
                callback(result.data);

                if (firstload) {
                    SetGroupList();
                    firstload = false;
                }
            } else {
                showInfo(result.data);
            }
        },
        error: function(data) {
            showInfo(data.responseText);
        }
    });














}
//------------------------
function AddContact() {
    if (treeview_selNodes.length == 0) {
        showInfo("请选择要添加的联系人");
        return;
    }

    var gid = document.getElementById("slgroupid").value;
    if (gid == "-1") {
        showInfo("请选择一个分组");
        return;
    }

    var ids = document.getElementById("seluseridlist").value
    if (ids == "") {
        showInfo("请选择要添加的联系人");
        return;
    }



    //IDList
    remoteCall("addcontact", "IDList=" + ids + "&groupid=" + gid, function(data) {
        showInfo(data);
    });

}

function DelContact() {
    if (treeview_selNodes.length == 0) {
        showInfo("请选择要删除的联系人");
        return;
    }

    var ids = "";
    for (var i = 0; i < treeview_selNodes.length; i++) {
        var node = treenodes[treeview_selNodes[i]];
        if (node.isUser) {
            if (ids != "")
                ids += ",";
            ids += node.value2;

        }
    }

    if (ids == "") {
        showInfo("请选择要删除的联系人");
        return;
    }

    if (!confirm("确认删除吗")) {
        return;
    }

    //IDList
    remoteCall("delcontact", "IDList=" + ids, function(data) {
        showInfo(data);
    });
}



//------------------------
function AddGroup() {
    CancelGroup();
}

function EditGroup() {
    if (treeview_selnodeindex == -1) {
        showInfo("请选择要修改的分组");
        return;
    }

    if (treenodes[treeview_selnodeindex].isUser) {
        showInfo("请选择要修改的分组");
        return;
    }

    document.getElementById("groupname").value = treenodes[treeview_selnodeindex].text;
    document.getElementById("groupid").value = treenodes[treeview_selnodeindex].value;

}

function DelGroup() {
    if (treeview_selnodeindex == -1) {
        showInfo("请选择要删除的分组");
        return;
    }

    if (treenodes[treeview_selnodeindex].isUser) {
        showInfo("请选择要删除的分组");
        return;
    }


    if (!confirm("确认删除吗")) {
        return;
    }

    var prams = "ID=" + treenodes[treeview_selnodeindex].value;

    var action = "delitem";
    remoteCall(action, prams, function(data) {
        showInfo(data);

    });

}



function SaveGroup() {

    var action = "additem";

    var groupname = document.getElementById("groupname").value;
    if (groupname + "" == "") {
        showInfo("分组名称必须填写");
        return;
    }

    var prams = "groupname=" + UrlEncode(groupname);

    if (treeview_selnodeindex != -1) {
        prams += "&ID=" + document.getElementById("groupid").value;
        action = "edititem";
    }


    remoteCall(action, prams, function(data) {
        showInfo(data);


    });
}

function CancelGroup() {
    document.getElementById("groupname").value = "";
    document.getElementById("groupid").value = "";
}






function SetGroupList() {
    remoteCall("LoadData", "", function(data) {
        var sl = document.getElementById("slgroupid");
        sl.length = 1;

        for (var i = 0; i < data.length; i++) {
            sl.options.add(new Option(data[i].text, data[i].value));
        }
    });

}

function remoteCall(action, params, callBack) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Personal/MessageBox/Group.ashx?action=" + action + "&" + params,
        dataType: 'string',
        data: '',
        cache: false,
        success: function(data) {
            var result = null;
            eval("result = " + data);

            if (result.result) {
                callBack(result.data);

                if (action != "LoadData" && action != "addcontact") {
                    CancelGroup();
                    LoadUserList('LoadUserListWithGroup', BuildTree);
                    SetGroupList();
                }

            } else {
                showInfo(result.data);
            }
        },
        error: function(data) {
            showInfo(data.responseText);
        }
    });


















}
