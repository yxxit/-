<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurContractInfo.ascx.cs" Inherits="UserControl_PurContractInfo" %>
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
    var popPurContractObj = new Object();
    popPurContractObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popPurContractObj.returnName = false;

    popPurContractObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popPurContractObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#PurContractName").val('');
        $("#PurContractId").val('');
        document.getElementById('HolidaySpan_PurContract').style.display = 'block';
        TurnToPageUc_Pur_PurContract(currentPageIndexUc_Pur);
    }
    var pageCountUc_Pur = 10;//每页计数
    var totalRecord_Pur = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc_Pur = 1;
    var action = "";//操作
    var OrderByUc_PurContract = "";//排序字段    
    var ifdelUc = "0";
    var isPurSell="0";//是否采购直销 默认否
 function TurnToPageUc_Pur_PurContract(pageIndex)
    {
           currentPageIndexUc_Pur = pageIndex;
           var PurContractName =document.getElementById("PurContractName").value;           
           var PurContractId =document.getElementById("PurContractId").value;
          
           action="SearchPurContractFromUserControl";
           
           var free1="采购合同";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/JTHY/ContractManage/ContractInfo.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc_Pur+"&action="+action+'&OrderByUc_PurContract='+OrderByUc_PurContract+
                    '&free1='+escape(free1)+'&PurContractId='+escape(PurContractId)+'&PurContractName='+escape(PurContractName), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_PurContract_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_PurContract tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetPurContract('"+item.managerid+"','"+item.deptid+"','" + item.id + "','" + item.Contractid + "','"+item.providerName+"','"+item.managerName+"','"+item.deptName+"','" + item.SettleType + "','"+item.SettleTypeName+"','"+item.providerid+"','"+item.TransPortType+"','"+item.TransPortTypeName+"','"+item.ContractMoney+"','"+item.linkman+"')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +

                        "<td height='22' align='center'>" + item.Contractid + "</td>"+
                        "<td height='22' align='center'>" + item.providerName + "</td>"+
                        "<td height='22' align='center'>" + item.SettleTypeName + "</td>"+
                        "<td height='22' align='center'>" + item.TransPortTypeName + "</td>"+
                        "<td height='22' align='center'>" + item.ContactSum + "</td>" +
                        "<td height='22' align='center'>" + item.ContractMoney + "</td>"+
                        "<td height='22' align='center'>"+item.createdate+"</td>").appendTo($("#pageDataListUc_PurContract tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_PurContract_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc_Pur,currentPageIndex:currentPageIndexUc_Pur,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_Pur_PurContract({pageindex});return false;"}//[attr]
                    );
                  totalRecord_Pur = msg.totalCount;
                 // $("#pageDataListUc_PurContract_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowpageCountUc_Pur").val(pageCountUc_Pur);
                  ShowTotalPage(msg.totalCount,pageCountUc_Pur,pageIndex,$("#pageCountUc_Pur"));
                  $("#ToPageUc_Pur").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_PurContract_Pager").show();IfshowUc_PurContract(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_PurContract","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出-采购到货使用
function fnSelectContract()
{
    if(!CheckJTName_PurContract())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
    TurnToPageUc_Pur_PurContract(1);  
    openRotoscopingDiv(false,"divJTNameS_PurContract","ifmJTNameS_PurContract");//弹遮罩层
    document.getElementById("HolidaySpan_PurContract").style.display= "block";
}

//弹出-供采购直销使用
function fnSelectPurContract()
{
    if(!CheckJTName_PurContract())
    {
        return;
    }
    isPurSell="1";
    ifdelUc = "0";
    search="1";        
    TurnToPageUc_Pur_PurContract(1);  
    openRotoscopingDiv(false,"divJTNameS_PurContract","ifmJTNameS_PurContract");//弹遮罩层
    document.getElementById("HolidaySpan_PurContract").style.display= "block";
}
    
function IfshowUc_PurContract(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pageCountUc_Pur").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pageCountUc_Pur").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_PurContract(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord_Pur-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc_Pur=parseInt(newPageCount);
        TurnToPageUc_Pur_PurContract(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_PurContract(orderColum,orderTip)
{
    if (totalRecord_Pur == 0) 
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
    OrderByUc_PurContract = orderColum+"_"+ordering;
    TurnToPageUc_Pur_PurContract(1);
}
// 获取采购单号后，对相应栏目赋值，包括采购到货及 采购直销
function GetPurContract( managerid,deptid,id , Contractid,providerName,managerName,deptName,SettleType ,SettleTypeName,providerid ,TransPortType,TransPortTypeName,ContractMoney,linkman)
{      
 
        if(isPurSell!="1") // 采购到货单
        {
            ///------------------为采购到货单赋值-----------------///
            $("#txtSourceBillID").val(id);  //采购合同id
            $("#txtSourceBillNo").val(Contractid);  //采购合同编号
            $("#drpSettleType").val(SettleType);    //结算方式
            $("#txtLinkMan").val(linkman);       //联系人
            $("#txtProviderID").val(providerid);
            $("#txtProviderName").val(providerName);
            $("#txtSumMoney").val(ContractMoney);        //总金额
            $("#txtPPersonID").val(managerid);     //采购员id
            $("#txtPPerson").val(managerName);       //采购员姓名
            $("#DeptName").val(deptName);          //部门名称
            $("#hdDeptID").val(deptid);          //部门id
            $("#drpTransPortType").val(TransPortType);  //调运类型
            
            ///---------------------------------------------------///
             var rowsCount=parseInt($("#txtTRLastIndex").val());
             for (var i=0;i<rowsCount;i++)
             {
             var j=i+1;
             $("#dg_Log").find("#"+j).remove();
             }             
            fnGetDetail_Pur(id);
        }
        else // 采购直销单
        {
            $("#txtPurContractID").val(id);
            $("#txtPurContractNo").val(Contractid);
            $("#txtProviderID").val(providerid);    //供应商id
            $("#txtProviderName").val(providerName);   //供应商名称
            $("#txtSupplyAmount").val(ContractMoney);   
            
            fnGetDetail_Zhixiao(id);    
        }          
        document.getElementById('HolidaySpan_PurContract').style.display = "none";
        closeRotoscopingDiv(false,"divJTNameS_PurContract");//关闭遮罩层    
}

// 采购直销时，填充明细栏中采购价格信息
function fnGetDetail_Zhixiao(headId)
{
    var action="SearchPurContract_Zhixiao";
    var orderBy="id";
    var ary = new Array();
    var rowsCount=0;
    
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy+"&headid="+escape(headId)+'',          
        beforeSend: function() {
        },
        error: function() {
          showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {                
                $.each(data.data, function(i, item) {
                for (var i=1;i<=document.getElementById("dg_Log").rows.length;i++)
                {
                  if($("#IdCoalName"+i ).val()==item.cInvCCode)
                  {
                     $("#txtInCost"+i ).val(item.iUnitCost);
                  }
                }   
                    rowsCount++;
                   // FillSignRow_Pur(i,item.cinvccode,item.HeatPower,item.unitid,item.unitname,item.iquantity,item.iunitcost,item.imoney,item.detailsid,item.storageid);
                              
               });
            }
            $("#txtTRLastIndex").val(rowsCount + 1);
        },
       complete:function(){         
        //fnTotalInfo();
         }//接收数据完毕
    });
    
    
}

//采购到货单中，填充明细栏中物品信息
function fnGetDetail_Pur(headid) {
 
    var action="SearchPurContract";
    var orderBy="id";
    var ary = new Array();
    var rowsCount=0;
    $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: '../../../Handler/JTHY/ContractManage/ContractInfo.ashx', //目标地址
        cache: false,
        data: "pageIndex=1&pageCount=1&action=" + action + "&orderby=" + orderBy+"&headid="+escape(headid)+'',          
        beforeSend: function() {

        },
        error: function() {
          showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！");
        },
        success: function(data) {
            if (data.data != null) {
                
                $.each(data.data, function(i, item) {
                    rowsCount++;
                    FillSignRow_Pur(i,item.cinvccode,item.HeatPower,item.unitid,item.unitname,item.iquantity,item.iunitcost,item.imoney,item.detailsid,item.storageid);
                              
            
                });
            }
            $("#txtTRLastIndex").val(rowsCount + 1);
        },
       complete:function(){         
        //fnTotalInfo();
         }//接收数据完毕
    });
} 

// 采购到货单，选择单据时，进行明细值填充
 function FillSignRow_Pur(i,cinvccode,HeatPower,unitid,unitname,iquantity,iunitcost,imoney,detailsid,storageid) { //读取最后一行的行号，存放在txtTRLastIndex文本框中
 //  var txtTRLastIndex = document.getElementById("txtTRLastIndex");
    
   // var rowID = parseInt(txtTRLastIndex.value) + 1;
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
    
    getWareData('drpWare'+rowID,storageid);//加载仓库数据
     

   //加载煤种数据 
    var newFitNametd = newTR.insertCell(m); //添加列:煤种
    newFitNametd.className = "cell";
    //newFitDesctd.innerHTML = "<input id='drpCoalType" + rowID + "' value='" + HeatPower + "' maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    newFitNametd.innerHTML = "<table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td>"+
    "<select disabled='disabled' id='drpCoalType"+rowID+"'   name='drpCoalType"+rowID+"' class='tddropdlist' onChange='getCoalNature(this.value,"+rowID+")' ></select></td></tr></table>"; //添加列内容
    m++;
    
    getCoalData('drpCoalType'+rowID,cinvccode);//加载煤种数据
     
   
    var newFitDesctd = newTR.insertCell(m); //添加列:质量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtSpecialName" + rowID + "' value='"+HeatPower+"' maxlength='10' disabled='disabled' type='text' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:计量单位
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input type='hidden' id='txtUnitID"+rowID+"'  value='"+unitid+"'><input  id='txtUnitName" + rowID + "' value='"+unitname+"'  style=' width:95%;' disabled='disabled' type='text'  class='tdinput' />"; //添加列内容
    m++;   
       
    var newFitDesctd = newTR.insertCell(m); //添加列:数量
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtQuantity" + rowID + "'  onpropertychange=\"getMoney(" + rowID + ");\" value='" + iquantity + "' onkeyup='return ValidateNumber(this,value)'   type='text' class='tdinput' style=' width:90%;'/>"; //添加列内容
    m++;
    
        var newFitDesctd = newTR.insertCell(m); //添加列:单价
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtUnitCost" + rowID + "' onpropertychange=\"getMoney(" + rowID + ");\" maxlength='10' type='text' value='" + Number(iunitcost).toFixed(2) + "'onkeyup='return ValidateNumber(this,value)'  class='tdinput' style=' width:90%;'  />"; //添加列内容
    m++;
    
    var newFitDesctd = newTR.insertCell(m); //添加列:金额
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtMoney" + rowID + "'  onpropertychange=\"getTotalMoney();\"   maxlength='10' type='text' value='" + imoney + "' onkeyup='return ValidateNumber(this,value)'  class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
      
    
    var newFitDesctd = newTR.insertCell(m); //添加列:税率
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtTaxRate" + rowID + "' maxlength='10' type='text' value='17' class='tdinput' style=' width:90%;'  /> "; //添加列内容
    m++;
    
//    var newFitDesctd = newTR.insertCell(m); //添加列:是否质检
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<select style='width:95%' id='txtISQTest"+rowID+"'><option value='1'>是</option><option value='0'>否</option></select>"; //添加列内容
//    m++;   
//       
//    var newFitDesctd = newTR.insertCell(m); //添加列:已报检
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtComQTest" + rowID + "' value='0.00'    type='text' class='tdinput'  disabled='disabled'    style='width:90%;'/>"; //添加列内容
//    m++;
    
     var newFitDesctd = newTR.insertCell(m); //添加列:已入库
    newFitDesctd.className = "cell";
    newFitDesctd.innerHTML = "<input id='txtComInWare" + rowID + "'  value='0.00'  type='text' class='tdinput'  disabled='disabled'   style='width:90%;'/>"; //添加列内容
    m++;

//    var newFitDesctd = newTR.insertCell(m); //添加列:已入库
//    newFitDesctd.className = "cell";
//    newFitDesctd.innerHTML = "<input id='txtComInWare" + rowID + "'  value='0.00'  type='text' class='tdinput'  disabled='disabled'   style='width:90%;'/>"; //添加列内容
//    m++;
  //  txtTRLastIndex.value = rowID; //将行号推进下一行  
     $("#txtTRLastIndex").val((rowID + 1).toString()); //将行号推进下一行
    
    }
//主表单验证
function CheckJTName_PurContract()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var PurContractId = document.getElementById('PurContractId').value;//编号
    var PurContractName = document.getElementById('PurContractName').value;//名称
    

    if(PurContractId.length>0 && PurContractId.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "编号输入不正确|";
    }    
    if(PurContractName.length>0 && ValidText(PurContractName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "名称输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivJTNameClose_PurContract()
{

    document.getElementById("PurContractId").value = "";
    document.getElementById("PurContractName").value = "";

    closeRotoscopingDiv(false,"divJTNameS_PurContract");//关闭遮罩层
    document.getElementById('HolidaySpan_PurContract').style.display='none'; 
}

function JTClear_PurContract()
{
    try
    {
      if(isPurSell!="1")
      { 
            document.getElementById("txtSourceBillID").value ="";  
            document.getElementById("txtSourceBillNo").value ="";
            document.getElementById("drpSettleType").value ="";
            document.getElementById("txtProviderID").value ="";
            document.getElementById("txtProviderName").value ="";
            document.getElementById("txtLinkMan").value ="";
            document.getElementById("drpTransPortType").value ="";
            document.getElementById("txtSumMoney").value ="";
       }
       else
       {
            document.getElementById("txtPurContractID").value ="";
            document.getElementById("txtPurContractNo").value ="";
            document.getElementById("txtProviderID").value ="";
            document.getElementById("txtProviderName").value ="";
            document.getElementById("txtSupplyAmount").value ="";
       }
         
    }
    catch(e)
    { }
    
    DivJTNameClose_PurContract();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchJTData_PurContract();" id="txtUcJTName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divJTNameS_PurContract" style="display:none">
<iframe id="ifmJTNameS_PurContract" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_PurContract" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivJTNameClose_PurContract();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="JTClear_PurContract();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 合同号</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="PurContractId" id="PurContractId"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">供应商</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="PurContractName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                </td>
            <td bgcolor="#FFFFFF" style="width: 24%">
               <input id="Text1"  class="tdinput"  type="text"  style="width:95%" /> </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='fnSelectContract()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_PurContract" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" >合同号<span id="oPurContractId" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" >供应商<span id="oPurContractName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" >结算方式<span id="oPurContractStd" class="orderTip"></span></div></th>                         
        <th align="center" class="td_list_fields"><div class="orderClick" >调运类型<span id="oPurContractClassName" class="orderTip"></span></div></th>
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
            <td height="28"  background="../../../images/Main/PageList_bg.jpg" width="40%"  ><div id="pageCountUc_Pur"></div></td>
            <td height="28"  align="right"><div id="pageDataListUc_PurContract_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_PurContract_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowpageCountUc_Pur"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc_Pur" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_PurContract($('#ShowpageCountUc_Pur').val(),$('#ToPageUc_Pur').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfJTID" type="hidden"  />
<input id="hfJTID_Ser" type="hidden" runat="server" />
<input id="hfJTNo" type="hidden"  /></td>
        </tr>
    </table>
</div>


