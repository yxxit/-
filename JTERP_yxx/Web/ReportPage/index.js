jQuery(document).ready(function ($) {
    $('#mytable').flexigrid({
        url: 'Flexigrid.ashx?action=SearchAll',
        dataType: 'json',
        colModel: [
            { display: '序号', name: 'ID', width: 40, sortable: true, align: 'center' },
			{ display: '客户编号', name: 'cCusCode', width: 80, sortable: true, align: 'center' },
			{ display: '客户名称', name: 'cCusName', width: 180, sortable: true, align: 'center' },
            { display: '客户地址', name: 'cCusAddress', width: 200, sortable: true, align: 'center' },
            { display: '客户日期', name: 'dCusDevDate', width: 80, sortable: true, align: 'center' },
            { display: ' 操 作 ', name: 'ope', width: 80, sortable: true, align: 'center' }
			],
        sortname: "ID",
        sortorder: "asc",
        usepager: true,
        title: '销售报表',
        useRp: true,
        onSubmit: false,
        width: 800,
        height: 500,
        errormsg: '发生错误', //错误提示消息
        //usepager: false,//是否分页
        nowrap: true, //是否不换行,这个可以使用
        page: 1, //current page,默认当前页
        //total: 1, //total pages,总页面数
        useRp: true, //use the results per page select box,是否可以动态设置每页显示的结果数
        rp: 25, // results per page,每页默认的结果数
        rpOptions: [10, 15, 20, 25, 40, 100], //可选择设定的每页结果数
        pagestat: '显示记录从{from}到{to}，总数 {total} 条', //显示当前页和总页面的样式
        procmsg: '正在处理数据，请稍候 ...', //正在处理的提示信息
        query: '', //搜索查询的条件
        qtype: '', //搜索查询的类别
        qop: "Eq", //搜索的操作符
        nomsg: '没有符合条件的记录存在', //无结果的提示信息
        blockOpacity: 0.1, //透明度设置
        //onToggleCol: false, //当在行之间转换时，可在此方法中重写默认实现，基本无用
        //onChangeSort: false, //当改变排序时，可在此方法中重写默认实现，自行实现客户端排序
        onSuccess: false, //成功后执行
        hideOnSubmit: true, //是否在回调时显示遮盖
        buttons:
            [
              { name: '导出EXCEL', bclass: 'excel', onpress: toolbar },//toolbar自定义函数
		      { name: '打印', bclass: 'print', onpress: toolbar },//打印
              { name: '标准显示', bclass: 'stdfont', onpress: toolbar },
		      { name: '放大显示', bclass: 'addfont', onpress: toolbar },
		      { name: '缩小显示', bclass: 'refont', onpress: toolbar },
		      { name: '设置列宽', bclass: 'setcol', onpress: toolbar },
              { name: '删除', bclass: 'delete', onpress: toolbar }
            ],
        searchitems: [
			{ display: '客户编号', name: 'cCusCode', isdefault: true }
			]
    });
    $('#sform').submit(function () {
        $('#mytable').flexOptions({ newp: 1 }).flexReload();
        return false;
    });
    $("#btnLoad").click(function () {//点击事件
        $('#mytable').flexOptions({ newp: 1, query: $("#cCusCode").val() + '#' + $("#cCusName").val() }).flexReload(); //newp是关键词,这里面的"."的作用，代表
        return false;        
    });
});

function sortAlpha(com) {
    jQuery('#mytable').flexOptions({ newp: 1, params: [{ name: 'letter_pressed', value: com }, { name: 'qtype', value: $('select[name=qtype]').val()}] });
    jQuery("#mytable").flexReload();
}

function addFormData() {
    var dt = $('#sform').serializeArray();
    $("#mytable").flexOptions({ params: dt });
    return true;
}
//获取高度
function getheight() {
    //var a = window.screen.height - 260;
    var a = window.screen.height;
    return a
}
//打印功能实现
function PrintMytable() {
    LODOP.PRINT_INIT("打印插件功能演示_Lodop功能_打印表格");
    LODOP.ADD_PRINT_TABLE(100, 20, 1000, 800, $("#mytable")[0].outerHTML);
    LODOP.PREVIEW();
};		

var status0 = '';
var curfontsize = 12;
var curlineheight = 18;
function toolbar(com, grid) {
    if (com == "导出EXCEL") {
        window.clipboardData.setData("text", "" + document.all('mytable').outerHTML);
        try {
            var ExApp = new ActiveXObject("Excel.application");
            var ExWBk = ExApp.workbooks.add();
            var ExWSh = ExWBk.worksheets(1);
            ExWSh.Cells.NumberFormatLocal = "";
            ExApp.DisplayAlerts = false;
            ExApp.visible = true;
        } catch(e) {
            alert("您的电脑没有安装Microsoft Excel软件！");
            return false;
        }
        ExWBk.worksheets(1).Paste;
    }
    if (com == "打印") {
        PrintMytable();
    }

    if (com == "标准显示") {
        curfontsize = 12;
        document.getElementById('mytable').style.fontSize = '12px';
    }
    if (com == "放大显示") {
        document.getElementById('mytable').style.fontSize = (++curfontsize) + 'px';
    }
    if (com == "缩小显示") {
        document.getElementById('mytable').style.fontSize = (--curfontsize) + 'px';
    }
    if (com == "删除") {
        if ($('.trSelected', grid).length == 0) {
            alert("请选择要删除的数据");
        } else { 
            if(confirm('是否删除这 ' + $('.trSelected',grid).length + ' 条记录吗?')){  
                 var  ids ="";  
                 for(var i=0;i<$('.trSelected',grid).length;i++){
                     //ids += "," + $('.trSelected', grid).find("td:first").eq(i).text(); //获取id  
                     ids += "," + $('.trSelected', grid).find("td:nth-child(2)").eq(i).text(); //获取cCusCode 编号，td:nth-child(3)
                 }
                 ids = ids.substring(1);  
                 $.ajax({  
                       type: "POST",  
                       url: "Flexigrid.ashx?action=Delete",
                       data: "cCusCode=" + ids,  
                       dataType:"text",  
                       success: function(msg){  
                           if(msg=="success"){
                               $("#mytable").flexReload();  
                           }else{  
                               alert("有错误发生,msg="+msg);  
                           }  
                       },
                       error: function(msg){  
                           alert(msg);  
                       }  
                });  
            }  
        }        
    }

    /*
    //其他
    if (com=='删除'){  
            $("action").value="delete";  
            if($('.trSelected',grid).length==0){  
                alert("请选择要删除的数据");  
            }else{  
                if(confirm('是否删除这 ' + $('.trSelected',grid).length + ' 条记录吗?')){  
                     var  ids ="";  
                     for(var i=0;i<$('.trSelected',grid).length;i++){  
                        ids+=","+$('.trSelected',grid).find("td:first").eq(i).text();//获取id  
                      }  
                      idsids=ids.substring(1);  
                      $.ajax({  
                            type: "POST",  
                            url: "flexGridServlet.do?actionaction="+${"action"}.value,  
                            data: "id="+ids,  
                            dataType:"text",  
                            success: function(msg){  
                                if(msg=="success"){  
                                    $("#flex1").flexReload();  
                                }else{  
                                    alert("有错误发生,msg="+msg);  
                                }  
                            },
                            error: function(msg){  
                                alert(msg);  
                            }  
                        });  
                }  
           }  
        }
        if (com=='添加'){  
            $("action").value="add";  
            alert("flexGridServlet.do?actionaction="+$("action").value);  
            window.location.href="flexGridServlet.do?action="+$("action").value;  
        }else if (com=='修改'){  
            $("action").value="modify";  
            if($(".trSelected").length==1){  
                alert("flexGridServlet.do?action="+$("action").value+"&id="+$('.trSelected',grid).find("td").eq(0).text());  
                window.location.href="flexGridServlet.do?action="+$("action").value+"&id="+$('.trSelected',grid).find("td").eq(0).text();  
                  
            }else if($(".trSelected").length>1){  
                alert("请选择一个修改,不能同时修改多个");  
            }else if($(".trSelected").length==0){  
                alert("请选择一个您要修改的记录")  
            }  
               
            //$("#flex1").flexReload({});  
        }*/

}

