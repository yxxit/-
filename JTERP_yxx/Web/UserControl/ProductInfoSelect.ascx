<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductInfoSelect.ascx.cs" Inherits="UserControl_ProductInfoSelect" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />
<style>

	.OfficeThingsListCss
    {
	    position:absolute;top:250px;left:250px;
	    border-width:1pt;border-color:#EEEEEE;border-style:solid;
	    width:800px;
	    display:none;
	    height:220px;
	    z-index:21;

	}
</style>
<script type="text/javascript">
    var pageCountUc = 10; //每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr"; //jPagerBar样式

    var currentPageIndexUc = 1;
    var action = ""; //操作
    var orderByUc = ""; //排序字段    
    var ifdelUc = "0"; 

    function TurnToPage(pageIndex) {
        currentPageIndexUc = pageIndex;
        var ProductNo = document.getElementById("txtProductNo").value;
        var ProductName = document.getElementById("txtProductName").value;
        var ProductShort = document.getElementById("txtUcProductShort").value;

        action = "SearchProduct";
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/JTHY/BusinessManage/ProductInfos.ashx', //目标地址
            cache: false,
            data: "pageIndex=" + pageIndex + "&pageCount=" + pageCountUc + "&action=" + action + "&orderbyuc=" + orderByUc + "&UproductNo=" + escape(ProductNo) + "&UproductName=" + escape(ProductName) +
                    '&UcProductShort=' + escape(ProductShort), //数据
            beforeSend: function() { AddPop(); $("#pageDataListUc_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                if (item.id != null && item.id != "")
                    $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  id='CheckboxProdID" + i + "'  name='CheckboxProdID'  value=" + item.id + "  type='checkbox'/>" + "</td>" +
                    // onclick=\"DeferMeeting('"+item.ID+"'"+",'"+item.MeetingNo+"'"+",'"+item.Title+"'"+",'"+item.StartDate+"'"+",'"+item.MeetingStatus+"');
                        "<td height='22' align='center'>" + item.prodNo + "</td>" +
                        "<td height='22' align='center'>" + item.ProductName + "</td>" +
                     
                        "<td height='22' align='center'>" + item.codeName + "</td>").appendTo($("#pageDataList tbody"));
                });
                //页码
                ShowPageBar("pageDataListUc_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1MarkUc",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountUc, currentPageIndex: currentPageIndexUc, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPage({pageindex});return false;"}//[attr]
                    );
                totalRecord = msg.totalCount;
                // $("#pageDataListUc_Total").html(msg.totalCount);//记录总条数
                document.getElementById("TextUc2").value = msg.totalCount;
                $("#ShowPageCountUc").val(pageCountUc);
                ShowTotalPage(msg.totalCount, pageCountUc, pageIndex, $("#pagecountUc"));
                $("#ToPageUc").val(pageIndex);
            },
            error: function() {
                showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
            },
            complete: function() { if (ifdelUc == "0") { hidePopup(); } $("#pageDataListUc_Pager").show(); IfshowUc(document.getElementById("TextUc2").value); pageDataList1("pageDataList", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
        });
    }
    //弹出客户信息
 
 
 
    function AddShows() {
        if (!CheckProductName()) {
            return;
        }
        ifdelUc = "0";
        search = "1";
        TurnToPage(1);
        openRotoscopingDiv(false, "divProductNameS", "ifmProductNameS"); //弹遮罩层
        document.getElementById("HolidaySpans").style.display = "block";
    }





   //是否全选
    function OptionCheckAll1() {
        if (document.getElementById("btnProductAll").checked) {
            var ck = document.getElementsByName("CheckboxProdID");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = true;
            }
        }
        else if (!document.getElementById("btnProductAll").checked) {
            var ck = document.getElementsByName("CheckboxProdID");
            for (var i = 0; i < ck.length; i++) {
                ck[i].checked = false;
            }
        }

    }

    function IfshowUc(count) {
        if (count == "0") {
            document.getElementById("divUcpage").style.display = "none";
            document.getElementById("pagecountUc").style.display = "none";
        }
        else {
            document.getElementById("divUcpage").style.display = "block";
            document.getElementById("pagecountUc").style.display = "block";
        }
    }
    //改变每页记录数及跳至页数
    function ChangePageCountIndexUc(newPageCount, newPageIndex) {
        if (!PositiveInteger(newPageCount)) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "每页显示应为正整数！");
            return;
        }
        if (!PositiveInteger(newPageIndex)) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数应为正整数！");
            return;
        }

        if (newPageCount <= 0 || newPageIndex <= 0 || newPageIndex > ((totalRecord - 1) / newPageCount) + 1) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "转到页数超出查询范围！");
            return false;
        }
        else {
            ifdelUc = "0";
            this.pageCountUc = parseInt(newPageCount);
            TurnToPage(parseInt(newPageIndex));
        }
    }
    //排序
    function OrderByUc(orderColum, orderTip) {
        if (totalRecord == 0) {
            return;
        }
        ifdelUc = "0";
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
        orderByUc = orderColum + "_" + ordering;
        TurnToPage(1);
    }
    
    
    
    
    
    function GetProduct(prodNo, productName, codeName, Size, Specification) {
        try {
            LinkManClear(); //每次选择客户时，使客户联系人为空
        }
        catch (err) {

        }

        if (document.getElementById('selllorderL') != undefined)  
        {
//            if (document.getElementById('selllorderL').value == '1') {
//                document.getElementById('txtUcCustName').value = CustName;
//                document.getElementById('txtUcCustID').value = CustID;
//            }
//            if (document.getElementById('selllorderL').value == '2') {
//                document.getElementById('txtUcCustName').value = CustNo;
//                document.getElementById('CustName').value = CustName;
//                document.getElementById('hiddenCustIDL').value = CustID;
//            }
//            if (document.getElementById('selllorderL').value == '3') {
//                document.getElementById('hiddenCustIDL').value = CustID;
//                document.getElementById("hfCustID").value = CustID;
//                document.getElementById("hfCustNo").value = CustNo;
//                document.getElementById("txtUcCustName").value = CustName;
//            }
//            else {
//                document.getElementById('txtUcCustName').value = CustNo;

//            }
        }
//        else if (document.getElementById('opr_addcontract') != undefined) //新建合同号
//        {
//            document.getElementById("txtCustomerID").value = CustID;
//            document.getElementById("txtCustomerName").value = CustName;
//            $("#txtDeliveryAddress").val(ReceiveAddress);  //交货地址
//        }
//        else if (document.getElementById('opr_addoutbus') != undefined)//采购直销、普通销售调用客户档案
//        {
//            document.getElementById("txtCustomerID").value = CustID;
//            document.getElementById("txtCustomerName").value = CustName;
//            document.getElementById("txtInvoiceUnit").value = BillUnit;

//        }
        else {
//            document.getElementById("hfCustID").value = CustID;
//            document.getElementById("CustNameSel1_hfCustID_Ser").value = CustID;
//            document.getElementById("hfCustNo").value = CustNo;
//            document.getElementById("txtUcCustName").value = CustName;
            try {
//                LoadBomInfo(CustID, CustNo, CustName);
            }
            catch (e)
        { }
        }

        document.getElementById('HolidaySpans').style.display = "none";
        closeRotoscopingDiv(false, "divProductNameS"); //关闭遮罩层
        //    try
        //    {
        //        GetSomeLinkInfoByID(CustID);
        //    }
        //    catch(err)
        //    {
        //        
        //    }
    }
    //主表单验证
    function CheckProductName() {
        var fieldText = "";
        var msgText = "";
        var isFlag = true;

        var txtUcCustNo = document.getElementById('txtProductNo').value; //商品
        var txtUcCustName = document.getElementById('txtProductName').value; //客户名称


        if (txtUcCustNo.length > 0 && txtUcCustNo.match(/^[A-Za-z0-9_]+$/) == null) {
            isFlag = false;
            msgText = msgText + "商品编号输入不正确|";
        }
        if (txtUcCustName.length > 0 && ValidText(txtUcCustName) == false) {
            isFlag = false;
            msgText = msgText + "商品简称输入不正确|";
        }


        if (!isFlag) {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", msgText);
        }
        return isFlag;
    }


    function DivProductNameClose() {

        document.getElementById("txtProductNo").value = "";
        document.getElementById("txtProductName").value = "";
        document.getElementById("txtUcProductShort").value = "";
        closeRotoscopingDiv(false, "divProductNameS"); //关闭遮罩层
        document.getElementById('HolidaySpans').style.display = 'none';
    }

//    function ProductClear() {
//        document.getElementById("hfCustID").value = "";
//        //  document.getElementById("CustNameSel1_hfCustID_Ser").value = "";  
//        document.getElementById("hfCustNo").value = "";
//        // document.getElementById("txtUcCustName").value = "";
//        if (document.getElementById('opr_addcontract') != undefined)  //add by bsd
//        {
//            document.getElementById("txtCustomerID").value = ""; //客户ID
//            document.getElementById("txtCustomerName").value = ""; //客户No
//        }
//        try {
//            document.getElementById("hidCustID_Tree").value = ""; //客户ID
//            document.getElementById("hidCustNo_Tree").value = ""; //客户No
//        }
//        catch (e)
//    { }
//        DivProductNameClose();
//    }


//    function CustClear1() {
//        document.getElementById("hfCustID").value = "";
//        document.getElementById("CustNameSel1_hfCustID_Ser").value = "";
//        document.getElementById("hfCustNo").value = "";
//        document.getElementById("txtUcCustName").value = "";
//        if (document.getElementById('selllorderL') != undefined)  //add by  linfei  销售报表-客户应收款查询
//        {
//            if (document.getElementById('selllorderL').value == '1') {
//                document.getElementById('txtUcCustName').value = '';
//                document.getElementById('txtUcCustID').value = '0';
//            }
//            if (document.getElementById('selllorderL').value == '2') {
//                document.getElementById('txtUcCustName').value = '';
//                document.getElementById('CustName').value = '';
//                document.getElementById('hiddenCustIDL').value = '0';
//            }
//        }
//        try {
//            document.getElementById("hidCustID_Tree").value = ""; //客户ID
//            document.getElementById("hidCustNo_Tree").value = ""; //客户No
//        }
//        catch (e)
//    { }
//        DivProductNameClose();
//    }

    function aa() {
        //document.getElementById("txtUcLinkMan").value = "";
        LinkManClear();
    }

 </script>

<%--<input onclick="SearchCustData();" id="txtUcCustName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divProductNameS" style="display:none">
<iframe id="ifmProductNameS" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpans" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivProductNameClose();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="ProductClear();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
      
      
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
         
            <td width="10%" height="20" align="right"> 商品编号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="txtProductNo" id="txtProductNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">商品名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="txtProductName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                拼音缩写</td>
            <td bgcolor="#FFFFFF" style="width: 24%">
                <input name="txtUcProductShort" id="txtUcProductShort"  class="tdinput"  type="text"  style="width:95%"  /></td>
          </tr>
          <tr>
            <td colspan="7" align="center" bgcolor="#FFFFFF">
        
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='AddShows()' /> 
                <img  id="confim"  alt="确认"  src="../../../Images/Button/Bottom_btn_confirm.jpg"  onclick='GetValue()'/>  
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList" bgcolor="#999999">
    <tbody>
      <tr>

        <th height="20" align="center" class="td_list_fields">选择<input type="checkbox" id="btnProductAll" name="btnProductAll" onclick="OptionCheckAll1()" /></th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc('ProductId','oCustNo');return false;">商品编号<span id="oCustNo" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc('ProductName','oCustName');return false;">商品名称<span id="oCustName" class="orderTip"></span></div></th>        
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc('ProductShort','oCustShort');return false;">计量单位<span id="oCustShort" class="orderTip"></span></div></th>        
       <%-- <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc('Prouctame','oTypeName');return false;">尺寸<span id="oTypeName" class="orderTip"></span></div></th>        
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc('Tel','oTel');return false;">规格型号<span id="oTel" class="orderTip"></span></div></th>--%>
        
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pagecountUc"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
   <%--     <input id="hfCustID" type="hidden"  />
<input id="hfCustID_Ser" type="hidden" runat="server" />
<input id="hfCustNo" type="hidden"  /></td>--%>
        </tr>
    </table>
</div>
