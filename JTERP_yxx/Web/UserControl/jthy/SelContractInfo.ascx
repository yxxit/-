<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelContractInfo.ascx.cs" Inherits="UserControl_SelContractInfo" %>
<%--<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>--%>
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
    var popSelContractObj = new Object();
    popSelContractObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popSelContractObj.returnName = false;

    popSelContractObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popSelContractObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#SelContractNo").val('');
        $("#SelCustName").val('');
        document.getElementById('HolidaySpan_SelContract').style.display = 'block';
        TurnToPageUc_sel_SelContract(currentPageIndexUc_sell);
    }
    var pageCountUc_sell = 10;//每页计数
    var totalRecord_sell = 0;
    var pagerStyle = "flickr";//jPagerBar样式    
    var currentPageIndexUc_sell = 1;
    var action = "";//操作
    var OrderByUc_SelContract = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_sel_SelContract(pageIndex) {    
           currentPageIndexUc_sell = pageIndex;
           var SelContractNo=$("#SelContractNo").val();  //合同编号
           var SelCustName=$("#SelCustName").val();     //客户名称          
           action="uc_SearchSellContractList";           
           var free1="销售合同";
           var id="";
           $.ajax({
               type: "POST", //用POST方式传输
               dataType: "json", //数据格式:JSON
               url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
               cache: false,
               data: "pageIndex=" + pageIndex + "&pageCount=" + pageCountUc_sell + "&action=" + action + '&OrderByUc_SelContract=' + OrderByUc_SelContract +
                    '&free1=' + escape(free1) + '&SelContractNo=' + escape(SelContractNo) + '&SelCustName=' + escape(SelCustName), //数据
               beforeSend: function() { AddPop(); $("#pageDataListUc_SelContract_Pager").hide(); }, //发送数据之前           
               success: function(msg) {
                   //数据获取完毕，填充页面据显示
                   //数据列表
                   $("#pageDataListUc_SelContract tbody").find("tr.newrow").remove();
                   $.each(msg.data, function(i, item) {
                       if (item.id != null && item.id != "")
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetSelContract('" + item.id + "','" + item.Contractid + "','" + item.SettleType + "','" + item.SettleTypeName + "','" + item.cCusCode + "','" + item.custname + "','" + item.TransPortType + "','" + item.TransPortTypeName + "','" + item.ContractMoney + "','" + item.linkman + "','" + item.billunit + "','" + item.ManagerId + "','" + item.ManagerName + "','" + item.deptId + "','" + item.deptName + "')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +

                        "<td height='22' align='center'>" + item.Contractid + "</td>" +
                        "<td height='22' align='center'>" + item.custname + "</td>" +
                        "<td height='22' align='center'>" + item.SettleTypeName + "</td>" +
                        "<td height='22' align='center'>" + item.TransPortTypeName + "</td>" +
                        "<td height='22' align='center'>" + item.ContactSum + "</td>" +
                        "<td height='22' align='center'>" + item.ContractMoney + "</td>" +
                        "<td height='22' align='center'>" + item.createdate + "</td>").appendTo($("#pageDataListUc_SelContract tbody"));
                   });
                   //页码
                   ShowPageBar("pageDataListUc_SelContract_Pager", //[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>", //[url]
                    {style: pagerStyle, mark: "pageDataList1MarkUc",
                    totalCount: msg.totalCount, showPageNumber: 3, pageCount: pageCountUc_sell, currentPageIndex: currentPageIndexUc_sell, noRecordTip: "没有符合条件的记录", preWord: "上一页", nextWord: "下一页", First: "首页", End: "末页",
                    onclick: "TurnToPageUc_sel_SelContract({pageindex});return false;"}//[attr]
                    );
                   totalRecord_sell = msg.totalCount;
                   // $("#pageDataListUc_SelContract_Total").html(msg.totalCount); //记录总条数
                   document.getElementById("TextUc2").value = msg.totalCount;
                   $("#ShowPageCountUc_sell").val(pageCountUc_sell);
                   ShowTotalPage(msg.totalCount, pageCountUc_sell, pageIndex, $("#pageCountUc_sell"));
                   $("#ToPageUc_sel").val(pageIndex);
               },
               error: function() {
                   showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
               },
               complete: function() { if (ifdelUc == "0") { hidePopup(); } $("#pageDataListUc_SelContract_Pager").show(); IfshowUc_SelContract(document.getElementById("TextUc2").value); pageDataList1("pageDataListUc_SelContract", "#E7E7E7", "#FFFFFF", "#cfc", "cfc"); } //接收数据完毕
           });
    }
//弹出调运信息
    function fnSelectSellContract() {
    if(!CheckJTName_SelContract())
    {
        return;
    }
    ifdelUc = "0";
    search="1";        
    TurnToPageUc_sel_SelContract(1);  
    openRotoscopingDiv(false,"divJTNameS_SelContract","ifmJTNameS_SelContract");//弹遮罩层
    document.getElementById("HolidaySpan_SelContract").style.display= "block";
}
    
function IfshowUc_SelContract(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pageCountUc_sell").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pageCountUc_sell").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_SelContract(newPageCount,newPageIndex)
{
    if(!PositiveInteger(newPageCount))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","每页显示应为正整数！");
        return;
    } 
    if(!PositiveInteger(newPageIndex))
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数应为正整数！");
        return;
    } 

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_sell-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc_sell=parseInt(newPageCount);
        TurnToPageUc_sel_SelContract(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_SelContract(orderColum,orderTip)
{
    if (totalRecord_sell == 0) 
     {
        return;
     }
    ifdelUc = "0";
    var ordering = "a";
    //var orderTipDOM = $("#"+orderTip);
    var allOrderTipDOM  = $(".orderTip");
    if( $("#"+orderTip).html()=="↓")
    {
         allOrderTipDOM.empty();
         $("#"+orderTip).html("↑");
    }
    else
    {
        ordering = "d";
        allOrderTipDOM.empty();
        $("#"+orderTip).html("↓");
    }
    OrderByUc_SelContract = orderColum+"_"+ordering;
    TurnToPageUc_sel_SelContract(1);
}
function GetSelContract(id,Contractid,SettleType,SettleTypeName,cCusCode,custname,TransPortType,TransPortTypeName,ContractMoney,linkman,billunit,ManagerId,ManagerName,deptId,deptName)
{   
        document.getElementById("txtSourceBillID").value ="";  
        document.getElementById("txtSourceBillNo").value ="";
        document.getElementById("drpSettleType").value ="";
        document.getElementById("txtCustomerID").value ="";
        document.getElementById("txtCustomerName").value ="";
        document.getElementById("txtInvoiceUnit").value ="";
        //document.getElementById("drpTransPortType").value ="";
        document.getElementById("txtSumMoney").value ="";       
                     
        document.getElementById("txtSourceBillID").value =id;  
        document.getElementById("txtSourceBillNo").value =Contractid;
        document.getElementById("drpSettleType").value =SettleType;
        document.getElementById("txtCustomerID").value =cCusCode;
        document.getElementById("txtCustomerName").value =custname;
        document.getElementById("txtInvoiceUnit").value =billunit;
        //document.getElementById("drpTransPortType").value =TransPortType;
        document.getElementById("txtSumMoney").value =ContractMoney;
        $("#txtPPersonID").val(ManagerId);   //业务员id
        $("#txtPPerson").val(ManagerName);     //业务员名称
        $("#DeptName").val(deptName);       //部门名称
        $("#hdDeptID").val(deptId);       //部门id
        $("#txtTransportFee").val("0.00"); //运费
         
        fnGetDetail_Sell(id);          
        document.getElementById('HolidaySpan_SelContract').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_SelContract");//关闭遮罩层
}

function fnGetDetail_Sell(headid) {
    var action="uc_SellContractDetail";
    var orderBy="id";
    var ary = new Array();
    var rowsCount=0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy + "&headid=" + escape(headid) + '',
        beforeSend: function () {

        },
        error: function () {
            showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function (data) {
            if (data.data != null) {
                ClearRows();  //清除已经存在的行
                $.each(data.data, function (i, item) {
                    rowsCount++;
                    //FillSignRow_Sell(i,item.cinvccode,item.specification,item.unitid,item.unitname,item.iquantity,item.iunitcost,item.imoney,item.detailsid);
                    //FillDetails(i,item.cinvccode,item.specification,item.unitid,item.productname,item.iquantity,item.iunitcost,item.imoney,item.detailsid);           
                    FillSignRow(i, item);
                });
            }
            $("#txtTRLastIndex").val(rowsCount + 1);
        },
        complete: function () {

            //fnTotalInfo();
        } //接收数据完毕
    });
} 
 function FillDetails(i,cinvccode,specification,unitid,productname,iquantity,iunitcost,imoney,detailsid)
 {
    $("#txtCoalID").val(cinvccode);
    $("#txtCoalName").val(productname);
    $("#txtQuantity").val(iquantity);
    $("#txtSaleCost").val(iunitcost);
    $("#txtTaxMoney").val(imoney);
 
 }
 
 //删除已经存在的行
 function ClearRows()
 {
     var signFrame = findObj("dg_Log", document);
    if((typeof(signFrame) != "undefined")&&(signFrame !=null))
    {  

        while(signFrame.rows.length>1)
        {
            signFrame.deleteRow(1);
        }
    } 

 }
 

 function FillSignRow_Sell(i,cinvccode,specification,unitid,unitname,iquantity,iunitcost,imoney,detailsid) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
    var rowID = parseInt(i) + 1;
    var signFrame = document.getElementById("dg_Log");
    var newTR = signFrame.insertRow(signFrame.rows.length); //添加行
    newTR.id = rowID;
    var m=0;
    
    var newNameXH = newTR.insertCell(m); //添加列:选择框
    newNameXH.className = "cell";
    newNameXH.innerHTML = "<input id='chk" + rowID + "' onclick = 'fnUnSelect(this)'  value=" + rowID + " type='checkbox'  /><input type='hidden' id='detailsid"+rowID+"' value='"+detailsid+"' >";
    m++;

   //加载仓库
    var newFitNametd = newTR.insertCell(m); //添加列:仓库
    newFitNametd.className = "cell";
    newFitNametd.innerHTML ="<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
    "<select  id='drpWare"+rowID+"' name='drpWare"+rowID+"'   class='tddropdlist'  ></select></td></tr></table>"; //添加列内容
    m++;
    
    getWareData('drpWare'+rowID,0);//加载仓库数据
    

   //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
    "<select id='drpCoalType"+rowID+"'   name='drpCoalType"+rowID+"' class='tddropdlist' onChange='getCoalNature(this.value,"+rowID+")' ></select></td></tr></table>"; //添加列内容
    m++;
    
    getCoalData('drpCoalType'+rowID,cinvccode);//加载煤种数据
   
    var newFitDesctd = newTR.insertCell(m); //添加列:质量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSpecialName" + rowID + "' maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input type='hidden' id='txtUnitID"+rowID+"' ><input  id='txtUnitName" + rowID + "'  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
    m++;   
       
    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "'  value='"+iquantity+"'    type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
        var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnitCost" + rowID + "' maxlength='10' type='text' value='"+iunitcost+"' class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "' maxlength='10' type='text' value='"+imoney+"'  class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
      
    
    var newFitDesctd = newTR.insertCell(m); //添加列:税率
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' maxlength='10' type='text' value='17' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:是否质检
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<select style='width:95%' id='txtISQTest"+rowID+"'><option value='1'>是</option><option value='0'>否</option></select>"; //添加列内容
    m++;   
       
    var newFitDesctd = newTR.insertCell(m); //添加列:已报检
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComQTest" + rowID + "'     type='text' class='tdinput'  readonly    style='width:90%;'/>"; //添加列内容
    m++;
    
     var newFitDesctd = newTR.insertCell(m); //添加列:已入库
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComInWare" + rowID + "'    type='text' class='tdinput'  readonly   style='width:90%;'/>"; //添加列内容
    m++;

  //  txtTRLastIndex.value = rowID; //将行号推进下一行  
     $("#txtTRLastIndex").val((rowID + 1).toString()); //将行号推进下一行
    
    }

//主表单验证
function CheckJTName_SelContract()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;


    var RetVal=CheckSpecialWords();
    if(RetVal!="")
    {
        isFlag = false;
        fieldText = fieldText + RetVal+"|";
        msgText = msgText +RetVal+  "不能含有特殊字符|";
    }
    if(!isFlag)
    {
        popMsgObj.Show(fieldText,msgText);
    } 
    return isFlag;   
}

function DivJTNameClose_SelContract()
{

    document.getElementById("SelContractNo").value = "";
    document.getElementById("SelCustName").value = "";

    closeRotoscopingDiv(false,"divJTNameS_SelContract");//关闭遮罩层
    document.getElementById('HolidaySpan_SelContract').style.display='none'; 
}

function JTClear_SelContract()
{
    try
    {
      
        document.getElementById("txtSourceBillID").value ="";  
        document.getElementById("txtSourceBillNo").value ="";
        document.getElementById("drpSettleType").value ="";
        document.getElementById("txtCustomerID").value ="";
        document.getElementById("txtCustomerName").value ="";
        document.getElementById("txtInvoiceUnit").value ="";
        document.getElementById("drpTransPortType").value ="";
        document.getElementById("txtSumMoney").value ="";
         
    }
    catch(e)
    { }
    
    DivJTNameClose_SelContract();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_SelContract();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_SelContract" style="display:none">
<iframe id="ifmJTNameS_SelContract" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_SelContract" style="border: solid 5px #999999; background: #fff; width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_SelContract();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_SelContract();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 合同号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="SelContractNo" id="SelContractNo"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">客户</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="SelCustName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                </td>
            <td bgcolor="#FFFFFF" style="width: 23%">
               &nbsp;
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='TurnToPageUc_sel_SelContract(1)' /> 
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_SelContract" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >合同号<span id="oSelContractId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >客户<span id="oSelContractName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >结算方式<span id="oSelContractStd" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >调运类型<span id="oSelContractClassName" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >吨数<span id="Span2" class="orderTip"></span></div></th>                  
        <th align="center" class="td_list_fields"><div class="orderClick" >总金额<span id="Span1" class="orderTip"></span></div></th>                  
        <th align="center" class="td_list_fields"><div class="orderClick" >制单日期<span id="Span4" class="orderTip"></span></div></th>
      </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999" class="PageList">
      <tr>
        <td height="28"  background="../../../images/Main/PageList_bg.jpg" >
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
          <tr>
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pageCountUc_sell"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_SelContract_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_SelContract_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc_sell"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc_sel" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_SelContract($('#ShowPageCountUc_sell').val(),$('#ToPageUc_sel').val());" /> </div></td>
          </tr>
        </table>
        <a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
        <input id="hfJTID_Ser" type="hidden" runat="server" />
        <input id="hfJTNo" type="hidden"  />
        </td></tr>
    </table>
</div>


