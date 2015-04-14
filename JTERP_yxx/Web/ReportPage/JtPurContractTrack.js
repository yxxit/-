jQuery(document).ready(function ($) {
    $('#Report').flexigrid({
        url: 'JtPurContractTrack.ashx?action=SearchAll',
        dataType: 'json',
        method:'POST',
        colModel: [
            { display: '序号', name: 'ID', width: 40, sortable: true, align: 'center' },
            { display: '合同号', name: 'Contractid', width: 80, sortable: true, align: 'center' },
            { display: '供应商', name: 'CustName', width: 200, sortable: true, align: 'center' },
            { display: '煤种', name: 'ProductName', width: 60, sortable: true, align: 'center' },
			{ display: '合同数量', name: 'iQuantity', width: 80, sortable: true, align: 'center' },
			{ display: '合同金额', name: 'iMoney', width: 80, sortable: true, align: 'center' },
            { display: '已发运数量', name: 'ProductCount', width: 80, sortable: true, align: 'center' },
            { display: '已结算数量', name: 'SttlCount', width: 80, sortable: true, align: 'center' },
            { display: '未结算数量', name: 'wCount', width: 80, sortable: true, align: 'center' },
            { display: '已发运金额', name: 'totalfee', width: 80, sortable: true, align: 'center' },
            { display: '已结算金额', name: 'SttlTotalPrice', width: 80, sortable: true, align: 'center' },
            { display: '未结算金额', name: 'wFee', width: 80, sortable: true, align: 'center' },

            { display: '签订日期', name: 'SignDate', width: 80, sortable: true, align: 'center' },
            { display: '生效日期', name: 'EffectiveDate', width: 80, sortable: true, align: 'center' },
            { display: '终止日期', name: 'EndDate', width: 80, sortable: true, align: 'center' },
            { display: '所属部门', name: 'DeptName', width: 80, sortable: true, align: 'center' },
            { display: '经办人', name: 'EmployeeName', width: 80, sortable: true, align: 'center' }
			],
        sortname: "Contractid", //根据需要修改，可以不写（默认为id），但是如果写就必须要正确
        sortorder: "asc",
        usepager: true,//是否分页
        title: '采购合同执行表',
        striped:true,//如果是FALSE就不显示
        useRp: true, //是否可以动态设置每页显示的结果数
        onSubmit: false, //调用自定义的计算函数，基本没用
        width: 'auto',
        height: getheight(), //高度不能自适应，否则，列无法隐藏。'auto',// 
        resizable: false, //resizable table是否可伸缩，暂时不知道其作用
        errormsg: '发生错误', //错误提示消息
        nowrap: true, //是否不换行,在一行显示
        page: 1, //current page,默认当前页
        //total: 1, //total pages,总页面数
        useRp: true, //是否可以动态设置每页显示的结果数
        rp: 20, // results per page,每页默认的结果数
        rpOptions: [10, 15, 20, 25, 40, 100], //可选择设定的每页结果数
        pagestat: '显示记录从{from}到{to}，总数 {total} 条', //显示当前页和总页面的样式
        procmsg: '正在处理数据，请稍候 ...', //正在处理的提示信息
        query: '', //搜索查询的条件
        qtype: '', //搜索查询的类别,暂时不知道怎么使用，或许，类似于Action
        qop: "Eq", //搜索的操作符
        nomsg: '没有符合条件的记录存在', //无结果的提示信息
        blockOpacity: 0.1, //透明度设置，暂时还不清楚
        onSuccess: false, //成功后执行
        hideOnSubmit: true, //是否在回调时显示遮盖
        showToggleBtn: true, //是否允许显示隐藏列，该属性有bug设置成false点击头脚本报错。
        showTableToggleBtn: true, //是否显示【显示隐藏Grid】的按钮
        //gridClass: "bbit-grid",//样式，暂时不清楚，没有这个样式，grid 也是一个问题
        buttons:
            [
              { name: '导出EXCEL', bclass: 'excel', onpress: toolbar },//toolbar自定义函数
		      { name: '打印', bclass: 'print', onpress: toolbar },//打印
              { name: '标准显示', bclass: 'stdfont', onpress: toolbar },
		      { name: '放大显示', bclass: 'addfont', onpress: toolbar },
		      { name: '缩小显示', bclass: 'refont', onpress: toolbar },
		      { name: '设置列宽', bclass: 'setcol', onpress: toolbar }
            ],
        searchitems: [
			{ display: '供应商', name: 'CustName', isdefault: true }
			]
    });
    $('#sform').submit(function () {
        $('#Report').flexOptions({ newp: 1 }).flexReload();
        return false;
    });
    $("#btnLoad").click(function () {//点击事件
        $('#Report').flexOptions({ newp: 1,
            query: $("#txtCompanyCD").val() + '#' + $("#txtCustName").val() + '#' + $("#txtContractid").val() + '#' + $("#txtDept").val() + '#' + $("#txtStartTime").val() + '#' + $("#txtEndTime").val() 
        }).flexReload();
        return false;        
    });
});

function sortAlpha(com) {
    jQuery('#Report').flexOptions({ newp: 1, params: [{ name: 'letter_pressed', value: com }, { name: 'qtype', value: $('select[name=qtype]').val()}] });
    jQuery("#Report").flexReload();
}

function addFormData() {
    var dt = $('#sform').serializeArray();
    $("#Report").flexOptions({ params: dt });
    return true;
}
//1，获取高度,可以考虑设置。
function getheight() {
    //var a = window.screen.height - 260;
    var a = window.screen.height-300;
    return a
}
//2，获取宽度,可以考虑设置，这种方式是可以的
function getweight() {
    var a = window.screen.weight;
    if(a<800){
        a = 1000;
    }
    return a
}

//打印功能实现
function PrintMytable() {
    var body = $("#Report")[0].innerHTML;
    var top = $(".hDivBox table")[0].innerHTML;
    var table = "<table border='1' cellSpacing=0 cellPadding=0>" + top + body + "</table>";
    var ReportStyle = "<style></style>";
    var html = ReportStyle + table;
    LODOP.PRINT_INIT("打印表格");
    LODOP.ADD_PRINT_TABLE(100, 20, 1000, 800, html);
    LODOP.PREVIEW();
}

var status0 = '';
var curfontsize = 12;
var curlineheight = 18;
function toolbar(com, grid) {
    if (com == "导出EXCEL") {
        window.clipboardData.setData("text", "" + document.all('Report').outerHTML);
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
        document.getElementById('Report').style.fontSize = '12px';
    }
    if (com == "放大显示") {
        document.getElementById('Report').style.fontSize = (++curfontsize) + 'px';
    }
    if (com == "缩小显示") {
        document.getElementById('Report').style.fontSize = (--curfontsize) + 'px';
    }
}

