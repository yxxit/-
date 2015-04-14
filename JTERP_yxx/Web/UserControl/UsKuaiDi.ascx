<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsKuaiDi.ascx.cs" Inherits="UserControl_UsKuaiDi" %>
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

    var popSellKuaiDiObj = new Object();
    popSellKuaiDiObj.SearchModel = 'protion'; //查询的模式，all是取出所有的客户，protion时取出所有启用的客户
    popSellKuaiDiObj.returnName = false;

    popSellKuaiDiObj.ShowList = function(model) {
        if (model != null && typeof (model) != "undefined") {
            popSellKuaiDiObj.SearchModel = model;
        }
        ShowPreventReclickDiv();
        $("#KuaiDiName").val('');
        $("#KuaiDiId").val('');
        document.getElementById('HolidaySpan_KuaiDi').style.display = 'block';
        TurnToPageUc_KuaiDi(currentPageIndexUc);
    }
    var pageCountUc = 10;//每页计数
    var totalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndexUc = 1;
    var action = "";//操作
    var OrderByUc_KuaiDi = "";//排序字段    
    var ifdelUc = "0";

 function TurnToPageUc_KuaiDi(pageIndex)
    {
         
           currentPageIndexUc = pageIndex;
           var KuaiDiName =document.getElementById("KuaiDiName").value;           
           var KuaiDiId =document.getElementById("KuaiDiId").value;
          
           action="SearchKuaiDi";
           
           var free1="物运信息";
           var id="";
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/CustManager/KuaiDiName_New.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+pageCountUc+"&action="+action+'&OrderByUc_KuaiDi='+OrderByUc_KuaiDi+
                    '&free1='+escape(free1)+'&KuaiDiId='+escape(KuaiDiId)+'&KuaiDiName='+escape(KuaiDiName), //数据
           beforeSend:function(){AddPop();$("#pageDataListUc_KuaiDi_Pager").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#pageDataListUc_KuaiDi tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                    
                    if (item.id != null && item.id != "")
                        
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>" + "<input  onclick=\"GetKuaiDi('" + item.id + "','" + item.CompanyID + "','" + item.CompanyName + "','"+item.CompanyType+"','"+item.CompanyAdCode+"','"+item.Personer+"')\" id='Checkbox1' value=" + item.id + "  type='radio'/>" + "</td>" +
       
                        "<td height='22' align='center'>" + item.CompanyID + "</td>"+
                        
                        "<td height='22' align='center'>" + item.CompanyName + "</td>"+
                        
                        "<td height='22' align='center'>" + item.CompanyType + "</td>"+
                        
                         "<td height='22' align='center'>" + item.CompanyAdCode + "</td>"+
                                            
                        "<td height='22' align='center'>"+item.Personer+"</td>").appendTo($("#pageDataListUc_KuaiDi tbody"));
                   });
                   // 
                    //页码
                   ShowPageBar("pageDataListUc_KuaiDi_Pager",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1MarkUc",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:pageCountUc,currentPageIndex:currentPageIndexUc,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"TurnToPageUc_KuaiDi({pageindex});return false;"}//[attr]
                    );
                  totalRecord = msg.totalCount;
                 // $("#pageDataListUc_KuaiDi_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("TextUc2").value=msg.totalCount;
                  $("#ShowPageCountUc").val(pageCountUc);
                  ShowTotalPage(msg.totalCount,pageCountUc,pageIndex,$("#pagecountUc"));
                  $("#ToPageUc").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(ifdelUc=="0"){hidePopup();}$("#pageDataListUc_KuaiDi_Pager").show();IfshowUc_KuaiDi(document.getElementById("TextUc2").value);pageDataList1("pageDataListUc_KuaiDi","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出客户信息
function SearchKuaiDiData_KuaiDi()
{
    if(!CheckKuaiDiName_KuaiDi())
    {
        return;
    }
    ifdelUc = "0";
    search="1";
        
    TurnToPageUc_KuaiDi(1);  
   openRotoscopingDiv(false,"divKuaiDiNameS_KuaiDi","ifmKuaiDiNameS_KuaiDi");//弹遮罩层
    document.getElementById("HolidaySpan_KuaiDi").style.display= "block";
}
    
function IfshowUc_KuaiDi(count)
{
    if(count=="0")
    {
        document.getElementById("divUcpage").style.display = "none";
        document.getElementById("pagecountUc").style.display = "none";
    }
    else
    {
        document.getElementById("divUcpage").style.display = "block";
        document.getElementById("pagecountUc").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function ChangePageCountIndexUc_KuaiDi(newPageCount,newPageIndex)
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((totalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        ifdelUc = "0";
        this.pageCountUc=parseInt(newPageCount);
        TurnToPageUc_KuaiDi(parseInt(newPageIndex));
    }
}
//排序
function OrderByUc_KuaiDi(orderColum,orderTip)
{
    if (totalRecord == 0) 
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
    OrderByUc_KuaiDi = orderColum+"_"+ordering;
    TurnToPageUc_KuaiDi(1);
}







function GetKuaiDi(id,CompanyID,CompanyName,CompanyType,CompanyAdCode,Personer)
{

    var cp_version ="medicine";
    if (cp_version == "medicine")
    {
         try
        {
             // LinkManClear(); //
            if (document.getElementById("txtCompanyID")) {
                document.getElementById("txtCompanyID").value ="";
            }
             if (document.getElementById("txtCompanyName")) {
                document.getElementById("txtCompanyName").value ="";
            }
              
            
           
            
        }
        catch(err)
        {
            
        }
       
      
      
        document.getElementById("txtCompanyID").value = CompanyID;
        document.getElementById("txtCompanyName").value = CompanyName;
        
       
        
        document.getElementById('HolidaySpan_KuaiDi').style.display = "none";
        closeRotoscopingDiv(false,"divKuaiDiNameS_KuaiDi");//关闭遮罩层

    }

    else
    {

    }
}

//主表单验证
function CheckKuaiDiName_KuaiDi()
{

    var fieldText = "";
    var msgText = "";
    var isFlag = true;
    
    var KuaiDiId = document.getElementById('KuaiDiId').value;//药品编号
    var KuaiDiName = document.getElementById('KuaiDiName').value;//药品名称
    

    if(KuaiDiId.length>0 && KuaiDiId.match(/^[A-Za-z0-9_]+$/) == null)
    {
        isFlag = false;       
	    msgText = msgText + "公司编号输入不正确|";
    }    
    if(KuaiDiName.length>0 && ValidText(KuaiDiName) == false)
    {
        isFlag = false;       
	    msgText = msgText + "公司名称输入不正确|";
    }
    
    
    if(!isFlag)
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msgText);        
    }
    return isFlag;
}

function DivKuaiDiNameClose_KuaiDi()
{

    document.getElementById("KuaiDiId").value = "";
    document.getElementById("KuaiDiName").value = "";

    closeRotoscopingDiv(false,"divKuaiDiNameS_KuaiDi");//关闭遮罩层
    document.getElementById('HolidaySpan_KuaiDi').style.display='none'; 
}

function KuaiDiClear_KuaiDi()
{
    try
    {
      
        document.getElementById("txtCompanyID").value = "";
        document.getElementById("txtCompanyName").value = "";
       
         
    }
    catch(e)
    { }
    
    DivKuaiDiNameClose_KuaiDi();
}

function aa()
{
    //document.getElementById("txtUcLinkMan").value = "";
   // LinkManClear();
}

 </script>

<%--<input onclick="SearchKuaiDiData_KuaiDi();" id="txtUcKuaiDiName" style="width:95%"  type="text" class="tdinput" readonly/>--%>

<div id="divKuaiDiNameS_KuaiDi" style="display:none">
<iframe id="ifmKuaiDiNameS_KuaiDi" frameborder="0" width="100%" ></iframe>
</div>
<div id="HolidaySpan_KuaiDi" style="border: solid 5px #999999; background: #fff;
        width: 750px; z-index: 21; position: absolute;  display: none;
        top: 20%; left: 60%; margin: 5px 0 0 -400px"><!--;padding: 10px;class="OfficeThingsListCss"-->
<table width="99%" border="0" align="center" cellpadding="0" id="Table1"  cellspacing="0" >
      <tr bgcolor="#E7E7E7">
      <td  style="width:33%">
       <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="DivKuaiDiNameClose_KuaiDi();" />
        <img id="btn_clear" alt="清除" src="../../../Images/Button/Bottom_btn_del.jpg" style='cursor:hand;' onclick="KuaiDiClear_KuaiDi();" /> 
       </td>      
       </tr>
      </table>
    <table width="99%" border="0" align="center" cellpadding="0" id="searchtable"  cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF"><table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
          <tr class="table-item">
            <td width="10%" height="20" class="td_list_fields" align="right"> 公司编码</td>
            <td width="23%" bgcolor="#FFFFFF"><input name="KuaiDiId" id="KuaiDiId"  class="tdinput"  type="text" style="width:95%" /></td>
            
            <td class="td_list_fields" align="right" width="10%">公司名称</td>
            <td width="23%" bgcolor="#FFFFFF"><input id="KuaiDiName"  class="tdinput"  type="text"  style="width:95%" /></td>            
            <td class="td_list_fields" align="right" width="10%">
                </td>
            <td bgcolor="#FFFFFF" style="width: 24%">
                </td>
          </tr>
          <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
            <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchKuaiDiData_KuaiDi()' /> 
            <span class="redbold">仅显示500条，使用条件检索更多</span>
           
            </td>
          </tr>
        </table></td>
      </tr>
    </table>
      <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataListUc_KuaiDi" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields">选择</th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_KuaiDi('CompanyID','oCompanyID');return false;">公司编码<span id="oCompanyID" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_KuaiDi('CompanyName','oCompanyName');return false;">公司名称<span id="oCompanyName" class="orderTip"></span></div></th> 
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_KuaiDi('CompanyType','oCompanyType');return false;">公司类型<span id="oCompanyType" class="orderTip"></span></div></th>  
          <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_KuaiDi('CompanyAdCode','oCompanyAdCode');return false;">公司代码<span id="oCompanyAdCode" class="orderTip"></span></div></th>                                                
        <th align="center" class="td_list_fields"><div class="orderClick" onclick="OrderByUc_KuaiDi('Personer','oPersoner');return false;">联系人<span id="oPersoner" class="orderTip"></span></div></th>
        
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
            <td height="28"  align="right"><div id="pageDataListUc_KuaiDi_Pager" class="jPagerBar"></div></td>
            <td height="28" align="right"><div id="divUcpage">
              <input name="text" type="text" id="TextUc2" style="display:none" />
              <span id="pageDataListUc_KuaiDi_Total"></span>每页显示
              <input name="text" size="3" type="text" id="ShowPageCountUc"/>
              条  转到第
              <input name="text" type="text" id="ToPageUc" size="3" />
              页 <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor:hand;' alt="go" align="absmiddle" onclick="ChangePageCountIndexUc_KuaiDi($('#ShowPageCountUc').val(),$('#ToPageUc').val());" /> </div></td>
          </tr>
        </table><a name="pageDataList1MarkUc"></a>
        <input id="hfKuaiDiID" type="hidden"  />
<input id="hfKuaiDiID_Ser" type="hidden" runat="server" />
<input id="hfKuaiDiNo" type="hidden"  /></td>
        </tr>
    </table>
</div>





