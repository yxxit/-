<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TeachInfoUC.ascx.cs" Inherits="UserControl_TeachInfoUC" %>
<style type="text/css">
    .tdinput
    {
        border-width: 0pt;
        background-color: #ffffff;
        height: 21px;
        margin-left: 2px;
    }
    #Teach_List
    {
        border: solid 1px #111111;
        width: 165px;
        z-index: 101;
         bottom:0px;
        display: none;
        position: absolute;
        background-color: White;
    }
</style>

<a name="pageprodDataListMark"></a><span id="Forms" class="Spantype"></span>
  
<!--提示信息弹出详情start-->
<div id="divzhezhao1" style="filter: Alpha(opacity=0); width: 93%; padding: 1px;height: 500px; z-index: 199; bottom:50%; left:0px;position: absolute; display: none;">
    <iframe style="border: 0; width: 100%; height: 100%; position: absolute;"></iframe>
</div>
<div id="divTeach" style="border: solid 8px #93BCDD; background: #fff; padding: 10px; width: 93%;  z-index: 200; bottom:50%;left:0px; position: absolute; display: none;">
    <div id="divquery">
        <table width="100%">
            <tr>
                <td>                    
                    <img alt="关闭" id="btn_Close1" src="../../../images/Button/Bottom_btn_close.jpg" style='cursor: hand;'
                        onclick='closeTeachdiv();' />
                    <img alt="清除" id="ClearInputTeach" src="../../../images/Button/Bottom_btn_del.jpg"
                        style='cursor: hand; display: none' onclick='ClearPkroductInfo();' />
                    <input id="hf_typeid" type="hidden" />
                </td>
            </tr>
        </table>
        <table width="100%" height="57" border="0" cellpadding="0" cellspacing="0" id="mainindex">
            <tr>
                <td valign="top">
                    <img src="../../../images/Main/Line.jpg" width="122" height="7" />
                </td>
                <td rowspan="2" align="right" valign="top">
                    <div id='searchClick12'>
                        <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable12','searchClick12')" /></div>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" class="Blue">
                    <img src="../../../images/Main/Arrow.jpg" width="21" height="18" align="absmiddle" />检索条件
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" border="0" align="left" cellpadding="0" id="searchtable12" cellspacing="0"
                        bgcolor="#CCCCCC">
                        <tr>
                            <td bgcolor="#FFFFFF">
                                <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                    class="table">
                                    <tr class="table-item">
                                    <td width="10%" height="20"  class="td_list_fields"  align="right">
                                        工艺编号
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input type="text" id="txt_TeachNo" specialworkcheck="品名编号" maxlength="30" class="busTdInput"
                                            style="width: 92%" runat="server" />
                                    </td>
                                    <td width="10%" height="20"  class="td_list_fields"  align="right">
                                        工艺名称</td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        <input id="txt_ID" runat="server" type="hidden" />
                                        <asp:TextBox ID="txt_TeachName" specialworkcheck="品名" MaxLength="50" runat="server"
                                            CssClass="busTdInput" Width="92%"></asp:TextBox>
                                    </td>
                                    <td width="10%"  class="td_list_fields"  align="right">
                                        规格型号
                                    </td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        &nbsp;<input type="text" id="txt_Specification" specialworkcheck="规格型号" maxlength="30"
                                            class="busTdInput" style="width: 92%" runat="server" /></td>
                                </tr>
                                <tr class="table-item">
                                    <td width="10%" height="20"  class="td_list_fields"  align="right">
                                        </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                  </td>
                                    <td width="10%" height="20"  class="td_list_fields"  align="right">
                                        &nbsp;&nbsp;
                                    </td>
                                    <td width="20%" bgcolor="#FFFFFF">
                                        </td>
                                    <td width="10%"  class="td_list_fields"  align="right">
                                        &nbsp;</td>
                                    <td width="25%" bgcolor="#FFFFFF">
                                        &nbsp;</td>
                                </tr>
                                  
                                    <tr>
                                        <td colspan="8" align="center" bgcolor="#FFFFFF">
                                      <img alt="确定" src="../../../images/Button/btn_sure.jpg" style='cursor: hand;'
                                                onclick='GetTeachValue()' id="imgsure" />
                                            <img alt="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: hand;'
                                                onclick='Fun_Search_Teach()' id="btn_search" />
                                         
                                            <table width="100%" border="0" align="left" cellpadding="0" cellspacing="1" id="pageDataListTeach"
                                                bgcolor="#999999">
                                                <tbody>
                                                    <tr>
                                                        <th height="27" width="50" align="center" class="busListTitle">
                                                            选择<input type="checkbox" id="btnAll" name="btnAll" onclick="OptionCheckAllTeach();" />
                                                        </th>                                                        
                                                       <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderByTeach('TeachNo','oGroup');return false;">
                                                                工艺编号<span id="oGroup" class="orderTip"></span></div>
                                                        </th>
                                                        <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderByTeach('TeachName','oC2');return false;">
                                                                工艺名称<span id="oC2" class="orderTip"></span></div>
                                                        </th>
                                                        <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderByTeach('TypeName','oC3');return false;">
                                                                工艺分类<span id="oC3" class="orderTip"></span></div>
                                                        </th>
                                                        <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderByTeach('UnitName','oC5');return false;">
                                                                单位<span id="oC5" class="orderTip"></span></div>
                                                        </th>
                                                        <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderByTeach('Specification','oSpecification');return false;">
                                                                规格<span id="oSpecification" class="orderTip"></span></div>
                                                        </th>
                                                         
                                                        <th align="center" class="busListTitle">
                                                            <div class="orderClick" onclick="OrderBy('QualityName','Quality');return false;">
                                                                材质<span id="Quality" class="orderTip"></span></div>
                                                        </th>                                              
                                                        
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
            style="margin-left: 10px; position: relative">
            <tr>
                <td height="28" background="../../../images/Main/PageList_bg.jpg">
                    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                        <tr>
                            <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                <div id="pagecountTeach">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="pageDataList1_PagerTeach" class="jPagerBar">
                                </div>
                            </td>
                            <td height="28" align="right">
                                <div id="divpageTeach">
                                    <input name="text" type="text" id="TextTeach" style="display: none" />
                                    <span id="pageDataList1_TotalTeach"></span>每页显示
                                    <input name="text" type="text" id="ShowPageCountTeach" size="6" />
                                    条 转到第
                                    <input name="text" type="text" id="ToPageTeach" size="6" />
                                    页
                                    <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                        width="36" height="28" align="absmiddle" onclick="ChangePageCountIndexTeach($('#ShowPageCountTeach').val(),$('#ToPageTeach').val());" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="Teach_List" style="display: none;">
            <iframe id="aaaa" style="position: absolute; z-index: -1; bottom:0px; width: 165px; height: 100px;"
                frameborder="0"></iframe>
            <div style="background-color: Silver; padding: 3px; height: 20px; padding-left: 50px;
                padding-top: 1px">
                <a href="javascript:hidetxtUserList()" style="float: right;">清空</a>
            </div>
            <div style="padding-top: 5px; height: 300px; width: 165px; overflow: auto; margin-top: 1px">
                <asp:TreeView ID="TreeView1" runat="server" ShowLines="True">
                </asp:TreeView>
            </div>
        </div>
    </div>
</div>
<!--提示信息弹出详情end-->

<script language="javascript">

    var popTechObjT = new Object();
    popTechObjT.InputObj = null;
    popTechObjT.OperateObj = null;
    popTechObjT.Special = null;
    popTechObjT.CheckSpecial = null;
    var QueryID = "0";
    popTechObjT.CleraInput = null;
    popTechObjT.CleraHidden = null;
    var TeachTypeID = "0";
    /*单选*/
    popTechObjT.ShowList = function(TeachType,objInput, CleraInput, CleraHidden) {
        popTechObjT.InputObj = objInput;
        popTechObjT.CheckSpecial = null;
        popTechObjT.CleraInput = CleraInput;
        popTechObjT.CleraHidden = CleraHidden;
        TeachTypeID = TeachType;
        if (typeof (CleraInput) != "undefined" || (typeof (CleraHidden) != "undefined")) {
            document.getElementById('ClearInputTeach').style.display = 'inline';
        }
        else {
            document.getElementById('ClearInputTeach').style.display = 'none';
        }
        HideTeachbtn();
    }

    popTechObjT.ShowListCheckSpecial = function(TeachType, CheckSpecial)//多选事件
    {

        popTechObjT.CheckSpecial = CheckSpecial;
        TeachTypeID = TeachType;
        AlertTeachMsg();
        Fun_Search_Teach();
        document.getElementById('divTeach').style.display = 'block';
        document.getElementById('divzhezhao1').style.display = 'block';
        document.getElementById("btnAll").style.display = '';
        document.getElementById("imgsure").style.display = '';
        PCenterToDocument("divTeach", false);
    }

    function HideTeachbtn() {
        document.getElementById('divTeach').style.display = 'block';
        document.getElementById('divzhezhao1').style.display = 'block';
        document.getElementById("btnAll").style.display = 'none';
        document.getElementById("imgsure").style.display = 'none';
        AlertTeachMsg();
        Fun_Search_Teach();
        PCenterToDocument("divTeach", false);
    }    
    var ProdNo = "";
    var ProdName = "";
    var PYShort = "";
    var typeid = "";
    var Specification = "";
    var Manufacturer = "";
    var FromAddr = "";
    var Material = "";
    var StartStorage = "";
    var EndStorage = "";
    var pageCountTeach = 10; //每页计数
    var totalRecord = 0;
    var pagerTeachStyle = "flickr"; //jPagerBar样式
    var flag = "";
    var str = "";
    var currentPageIndexTeach = 1;
    var action = ""; //操作
    var orderByP = ""; //排序字段
   
    //jQuery-ajax获取JSON数据
    function TurnToPageTeach(pageIndex) {
        
        document.getElementById("btnAll").checked = false;
        var pageCount=pageCountTeach;
        currentPageIndexTeach = pageIndex;
        var TeachName = "";
        var TypeID = "";
        var UnitID = "";
        var UsedStatus = "";
        var Remark = "";
        var EFIndex = "";
        var EFDesc = "";
        var orderBy = "";
        var TeachType = TeachTypeID;
        var TeachNo = document.getElementById("TeachInfoUC1_txt_TeachNo").value;
        var TeachName = document.getElementById("TeachInfoUC1_txt_TeachName").value;
        var Specification = document.getElementById("TeachInfoUC1_txt_Specification").value;
        var UsedStatus ="1";
        var txtOutNo = "";
        var action = "Load";
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/SupplyChain/TeachInfo.ashx?action=' + action, //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex +
                  "&pageCount=" + pageCount +
                  "&action=" + action +
                  "&orderby=" + orderBy +
                  "&TeachType=" + escape(TeachType) +
                  "&TeachNo=" + escape(TeachNo) +
                  "&TeachName=" + escape(TeachName) +
                  "&Specification=" + escape(Specification) +
                  "&UsedStatus=" + escape(UsedStatus), //数据 //数据
            beforeSend: function() { $("#pageDataList_PagerTeach").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataListTeach tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {

                    if (item.ID != null && item.ID != "") {
                        var tempOnclick = "" + item.ID + ",'" + item.TeachNo + "','" + item.TeachName + "','" + item.TeachType + "','" + item.TypeName + "','" + item.UnitID + "','" + item.UnitName + "','" +
                        item.Specification + "','" + item.Quality + "','" + item.QualityName + "'";

                        if (popTechObjT.CheckSpecial != null) {
                            CheckSpe = " <td height='22' align='center'>" + " <input id='CheckboxTeachID' name='CheckboxTeachID'  value=" + item.ID + "  type='checkbox'/></td>";
                        }
                        else if (typeof (popTechObjT.CheckSpecial) == "undefined" || (popTechObjT.CheckSpecial == null)) {
                            CheckSpe = " <td height='22' align='center'>" + " <input type=\"radio\" name=\"radioTech\" id=\"radioTech_" + item.ID + "\"" + "value=\"" + item.ID + "\" onclick=\"Fun_FillTeach(" + tempOnclick + ");closeTeachdiv();\" />" + "</td>";
                        }
                        $("<tr class='newrow'></tr>").append(CheckSpe +
                        "<td height='22' align='center'><a href=\"#\">" + item.TeachNo + "</a></td>" +
                        "<td height='22' align='center' title=\"" + item.TeachName + "\">" + item.TeachName + "</td>" +
                        "<td height='22' align='center' title=\"" + item.TypeName + "\">" + item.TypeName + "</td>" +
                        "<td height='22' align='center' title=\"" + item.UnitName + "\">" + item.UnitName + "</td>" +
                        "<td height='22' align='center'>" + item.Specification + "</td>" +
                        "<td height='22' align='center'>" + item.QualityName + "</td>").appendTo($("#pageDataListTeach tbody"));
                    }
                });
                //页码
                ShowPageBar("pageDataList1_PagerTeach", //[containerId]提供装载页码栏的容器标签的客户端ID
                    "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerTeachStyle, mark: "pageprodDataListMark",
                    totalCount: msg.totalCount,
                    showPageNumber: 3,
                    pageCount: pageCountTeach,
                    currentPageIndex: pageIndex,
                    noRecordTip: "没有符合条件的记录",
                    preWord: "上页",
                    nextWord: "下页",
                    First: "首页",
                    End: "末页",
                    onclick: "TurnToPageTeach({pageindex});return false;"}//[attr]
                    );
                totalRecord = msg.totalCount;

                document.getElementById('TextTeach').value = msg.totalCount;

                $("#ShowPageCountTeach").val(pageCountTeach);
                ShowTotalPage(msg.totalCount, pageCountTeach, pageIndex);
                $("#ToPageTeach").val(pageIndex);
                ShowTotalPage(msg.totalCount, pageCountTeach, currentPageIndexTeach, $("#pagecountTeach"));
                //document.getElementById('tdResult').style.display='block';
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {alert(textStatus); },
            complete: function() { hidePopup(); $("#pageDataList1_PagerTeach").show(); IfshowTeach(document.getElementById('TextTeach').value); pageDataListTeach("pageDataListTeach", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
   
    
    //table行颜色
    function pageDataListTeach(o, a, b, c, d) {
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

    function Fun_Search_Teach(aa) {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;        
        var TeachNo = document.getElementById("TeachInfoUC1_txt_TeachNo").value;
        var TeachName = document.getElementById("TeachInfoUC1_txt_TeachName").value;
        var Specification = document.getElementById("TeachInfoUC1_txt_Specification").value;
        var RetVal = CheckSpecialWords();
        if (RetVal != "") {
            isFlag = false;
            fieldText = fieldText + RetVal + "|";
            msgText = msgText + RetVal + "不能含有特殊字符|";
        }
       

       
        TurnToPageTeach(1)
    }
    function IfshowTeach(count) {
        if (count == "0") {
            document.getElementById('divpageTeach').style.display = "none";
            document.getElementById("pagecountTeach").style.display = "none";
        }
        else {
            document.getElementById("divpageTeach").style.display = "block";
            document.getElementById("pagecountTeach").style.display = "block";
        }
    }
    function SelectDept(retval) {
        alert(retval);
    }

    //改变每页记录数及跳至页数
    function ChangePageCountIndexTeach(newPageCount, newPageIndex) {
    
        if (!IsZint(newPageCount)) {
            popMsgObj.ShowMsg('显示条数格式不对，必须是正整数！');
            return;
        }
        if (!IsZint(newPageIndex)) {
            popMsgObj.ShowMsg('跳转页数格式不对，必须是正整数！');
            return;
        }
        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            popMsgObj.ShowMsg('转到页数超出查询范围！');
            return false;
        }
        else {
            this.pageCountTeach = parseInt(newPageCount);
           
            TurnToPageTeach(parseInt(newPageIndex));
            document.getElementById("btnAll").checked = false;
        }
    }

    //排序
    function OrderByTeach(orderColum, orderTip) {
        var ordering = "a";
        //var orderTipDOM = $("#"+orderTip);
        var allOrderTipDOM = $(".orderTip");
        if ($("#" + orderTip).html() == "↓") {
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↑");
        }
        else {
            ordering = "d";
            allOrderTipDOM.empty();
            $("#" + orderTip).html("↓");
        }
        orderByP = orderColum + "_" + ordering;
        TurnToPageTeach(1);
    }

    function ClearTeachInfo() {
        if (typeof (popTechObjT.CleraHidden) != "undefined") {
            document.getElementById(popTechObjT.CleraHidden).value = "";
        }
        if (typeof (popTechObjT.CleraInput) != "undefined") {
            document.getElementById(popTechObjT.CleraInput).value = "";
            document.getElementById(popTechObjT.CleraInput).title = "";
        }
        closeTeachdiv();
    }
    function closeTeachdiv() {
        document.getElementById("divTeach").style.display = "none";
        document.getElementById("TeachInfoUC1_txt_TeachNo").value = "";
        document.getElementById("TeachInfoUC1_txt_TeachName").value= "";
        document.getElementById("TeachInfoUC1_txt_Specification").value= "";
        var TeachBigDiv = document.getElementById("TeachBigDiv");
        var divTeach = document.getElementById("divTeach");
        document.body.removeChild(TeachBigDiv);
        divTeach.style.display = "none";
        document.getElementById('divzhezhao1').style.display = 'none';
    }
    function AlertTeachMsg() {

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
        maskObj.setAttribute('id', 'TeachBigDiv');
        maskObj.style.position = "absolute";
        maskObj.style.top = "0";
        maskObj.style.left = "0";
        maskObj.style.background = "#777";
        maskObj.style.filter = "Alpha(opacity=10);";
        maskObj.style.opacity = "0.3";
        maskObj.style.width = sWidth + "px";
        maskObj.style.height = sHeight + "px";
        maskObj.style.zIndex = "100";
        document.body.appendChild(maskObj);
    }
    function OptionCheckAllTeach() {
        if (document.getElementById("btnAll").checked) {
            var ck = document.getElementsByName("CheckboxTeachID");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = true;
            }
        }
        else if (!document.getElementById("btnAll").checked) {
            var ck = document.getElementsByName("CheckboxTeachID");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = false;
            }
        }
    }
    function SelectedNodeChanged1(code_text, type_code) {
        
        hideUserList();
    }
    function hidetxtUserList() {
        hideUserList();
        document.getElementById("txt_TypeID").value = "";
        document.getElementById("hf_typeid").value = '';
    }

    function getChildNodes(nodeTable) {
        if (nodeTable.nextSibling == null)
            return [];
        var nodes = nodeTable.nextSibling;

        if (nodes.tagName == "DIV") {
            return nodes.childNodes; //return childnodes's nodeTables;
        }
        return [];
    }

    function showUserList() {
        var list = document.getElementById("Teach_List");

        if (list.style.display != "none") {
            list.style.display = "none";
            return;
        }

        var pos = elePos(document.getElementById("txt_TypeID"));

        list.style.left = pos.x;
        list.style.top = pos.y + 20;
        document.getElementById("Teach_List").style.display = "block";
        document.getElementById("Teach_List").style.top = pos.y - 60;
        document.getElementById("Teach_List").style.left = pos.x + 60;
    }
    function elePos(et) {

        var left = -140;
        var top = -145;
        while (et.offsetParent) {
            left += et.offsetLeft;
            top += et.offsetTop;
            et = et.offsetParent;
        }
        left += et.offsetLeft;
        top += et.offsetTop;
        return { x: left, y: top };
    }

    function hideUserList() {
        document.getElementById("Teach_List").style.display = "none";
    }


    /*控件居中
    *objID 需要居中的控件 ID名称 
    *div中的样式 top: 30%; left: 40%; margin: 5px 0 0 -400px;" 需删除
    *isPercent 表示 该控件 定义的高度是百分比（true） 还是数值（fasle）
    *必须在控件显示之后调用该方法
    *by pdd
    */
    function PCenterToDocument(objID, isPercent) {
        var obj = document.getElementById(objID);

        /*定义位置*/
        var _top = 0;
        var _left = 0;
        var objWidth = 0, objHeight = 0;

        //获取滚动条滚动的长度
        var scrollLength = PgetScrollTop();
        /*获取控件尺寸*/
        if (isPercent) {
            objWidth = obj.style.width.replace("px", "") == "" ? 0 : PPercentToFloat(obj.style.width);
            //  objHeight=obj.style.height.replace("px","")==""?0:PercentToFloat(obj.style.height); 
        }
        else {
            objWidth = obj.style.width.replace("px", "") == "" ? 0 : obj.style.width.replace("px", "");
        }
        objHeight = obj.offsetHeight == "" ? 0 : obj.offsetHeight;

        /*定义文档尺寸*/
        var docWidth = 0, docHight = 0;

        /*获取当前文档尺寸*/
        if (document.documentElement.clientHeight > document.body.scrollHeight) {
            //有垂直滚动条
            docWidth = parseInt(document.documentElement.scrollWidth);
            //docHight=parseInt(document.documentElement.clientHeight);
        }
        else {
            //无垂直滚动条
            docWidth = parseInt(document.documentElement.scrollWidth);
            //docHight=parseInt( document.documentElement.scrollHeight);
        }
        docHight = parseInt(document.documentElement.clientHeight);
        /*设置位置*/
        if (isPercent) {
            /*定义的控件尺寸为百分比*/
            //_top=docHight*(1-(objHeight==0?0.3:objHeight))/2;
            _left = docWidth * (1 - (objWidth == 0 ? 0.3 : objWidth)) / 2;
        }
        else {
            /*定义的控件尺寸为数值*/

            _left = (docWidth - (objWidth == 0 ? 100 : objWidth)) / 2;
        }

        _top = (docHight - (objHeight == 0 ? 200 : objHeight)) / 3;
        //alert(_top+","+_left+","+scrollLength+","+objHeight);
        obj.style.bottom = "0px";

        obj.style.left = "12px";


    }
    /*
    *设置浮点数百分比格式
    *使用前需要的num 做验证
    *num 需要格式的数字 默认 2位
    * PDD
    */
    function PFormatPercent(num) {
        var tmp = parseFloat(num * 100);
        return tmp.toFixed(2) + "%";
    }

    /*将百分数转化成浮点 4位有效数字 需自己对参数进行验证 PDD*/
    function PPercentToFloat(str) {
        if (str == "")
            return "";
        var tmp = str.replace("%", "");
        return parseFloat(parseFloat(tmp) / 100).toFixed(4);
    }
    /********************
    * 取窗口滚动条高度 
    ******************/
    function PgetScrollTop() {
        var scrollTop = 0;
        if (document.documentElement && document.documentElement.scrollTop) {
            scrollTop = document.documentElement.scrollTop;
        }
        else if (document.body) {
            scrollTop = document.body.scrollTop;
        }
        return scrollTop;
    }


    /********************
    * 取窗口可视范围的高度 
    *******************/
    function PgetClientHeight() {
        var clientHeight = 0;
        if (document.body.clientHeight && document.documentElement.clientHeight) {
            var clientHeight = (document.body.clientHeight < document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
        }
        else {
            var clientHeight = (document.body.clientHeight > document.documentElement.clientHeight) ? document.body.clientHeight : document.documentElement.clientHeight;
        }
        return clientHeight;
    }

    /********************
    * 取文档内容实际高度 
    *******************/
    function PgetScrollHeight() {
        return Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
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

</script>

