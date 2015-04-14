<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PaperPlateSelControl.ascx.cs" Inherits="UserControl_PaperPlateSelControl" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />


<input id="hf_TypeID" type="hidden" />

<div id="divPaperPlate" style="display:none">
<iframe id="ifmPaperPlate" frameborder="0" width="100%" ></iframe>
</div>
<div id="PaperPlateSel" style="border: solid 5px #999999; background: #fff;width: 750px; z-index: 21; position: absolute;display: none;top: 50%; left: 60%; margin: 5px 0px 0px -400px">
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
      <tr bgcolor="#E7E7E7">
       <td  style="width:10%">
        <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="divPaperPlateClose();" /> 
      </td>
      </tr>
 </table>
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF">
            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
            <tr class="table-item">
                <td width="10%" height="20" class="td_list_fields" align="right"> 物品编号</td>
                <td width="23%" bgcolor="#FFFFFF"><input id="txtName"  class="tdinput"  type="utext" style="width:95%" /></td>
                
                <td class="td_list_fields" align="right" width="10%">物品名称</td>
                <td width="23%" bgcolor="#FFFFFF"><input id="txtRemark"  class="tdinput"  type="text"  style="width:95%" /></td>            
                <td class="td_list_fields" align="right" width="10%"></td>
                <td bgcolor="#FFFFFF" style="width: 24%"></td>
            </tr>
            <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
               <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchPaperPlate()'  /> 
            </td>
         </tr>
       </table>
       </td>
     </tr>
        </table>
        
 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="paperpageDataList1" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields" >选择</th>
        <th align="center" class="td_list_fields" ><div class="orderClick" onclick="OrderBy('Type','oType');return false;">类型<span id="oType" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields" ><div class="orderClick" onclick="OrderBy('Name','oName');return false;">物品编号<span id="oName" class="orderTip"></span></div></th>       
        <th align="center" class="td_list_fields" ><div class="orderClick" onclick="OrderBy('Remark','oRemark');return false;">物品名称<span id="oRemark" class="orderTip"></span></div></th>
    </tr>
    </tbody>
    </table>
    <br/>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="paperpagecount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="paperpageDataList1_PagerList" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="paperdivpage">
                                            <input name="text" type="text" id="Text6" style="display: none" />
                                            <span id="paperpageDataList1_Total"></span>每页显示
                                            <input name="text" style="width:20px" type="text" id="paperShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" style="width:20px" id="paperToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: hand;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="paperChangePageCountIndex($('#paperShowPageCount').val(),$('#paperToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
</div>

 <script type="text/javascript">
  
   var paperpageCount = 10;//每页计数
    var papertotalRecord = 0;
    var pagerStyle = "flickr";//jPagerBar样式
    
    var currentPageIndex = 1;
    var paperaction = "";//操作
    var paperorderBy = "";//排序字段  
    var TypeID="";//判断选择类型依据 
    lmIfDel = "0";
    var floorname="";
    var floorid="";
    function getFloor(type,name,id)
    {
        
        floorname=name;
        floorid=id;
        PaperTurnToPage(1,type);
        openRotoscopingDiv(false,"divPaperPlate","ifmPaperPlate");//弹遮罩层
        document.getElementById("PaperPlateSel").style.display= "block";
    }

 function PaperTurnToPage(pageIndex,Id)
    {
           currentPageIndex = pageIndex;
           if(Id!=""&&Id!=null&&Id!=undefined)
           {
                TypeID=Id;
           }
           document.getElementById("hf_TypeID").value=Id;

           var Name=document.getElementById("txtName").value;
           var Remark=document.getElementById("txtRemark").value;
           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/PaperPlateSel.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+paperpageCount+"&orderby="+paperorderBy+
                  "&TypeID="+escape(TypeID)+"&Name="+escape(Name)+"&Remark="+escape(Remark)+"&action=getfloor",//数据
           beforeSend:function(){AddPop();$("#PaperpageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#paperpageDataList1 tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID!= "")  
                          var type="";
                          switch(item.BigType)
                          {
                            case "9 ":
                              type="纸号";
                              break;
                            case "10":
                               type="耐磨纸";
                               break;
                            case "11":
                                type="平衡纸";
                                break;
                            case "12":
                                type="基材";
                                break;                              
                          }                    
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"GetPaperPlate('"+item.ID+"','"+item.ProductName+"','"+item.BigType+"')\" id='getpaperplate' value="+item.id+"  type='radio'/>"+"</td>"+                                                                                                       
                        "<td height='22' align='center'>" + type + "</td>"+
                        "<td height='22' align='center'>" + item.ProdNo + "</td>"+
                        "<td height='22' align='center'>" + item.ProductName+ "</td>").appendTo($("#paperpageDataList1 tbody"));
                   });
                    //页码
                   ShowPageBar("paperpageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:paperpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"PaperTurnToPage({pageindex});return false;"}//[attr]
                    );
                  papertotalRecord = msg.totalCount;
                 // $("#pageDataList1_Total").html(msg.totalCount);//记录总条数
                  document.getElementById("Text6").value=msg.totalCount;
                  $("#paperShowPageCount").val(paperpageCount);
                  ShowTotalPage(msg.totalCount,paperpageCount,pageIndex,$("#paperpagecount"));
                  $("#paperToPage").val(pageIndex);
                  },
           error: function() 
           {
                showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
           }, 
           complete:function(){if(lmIfDel=="0"){hidePopup();}$("#paperpageDataList1_PagerList").show();lmIfshow(document.getElementById("Text6").value);pageDataList1("paperpageDataList1","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出信息
function SearchPaperPlate()
{
    lmIfDel = "0";
    var id=document.getElementById("hf_TypeID").value;
     PaperTurnToPage(1,id);
    openRotoscopingDiv(false,"divPaperPlate","ifmPaperPlate")//弹遮罩层
    document.getElementById("PaperPlateSel").style.display= "block";
}

function lmIfshow(count)
{
    if(count=="0")
    {
        document.getElementById("paperdivpage").style.display = "none";
        document.getElementById("paperpagecount").style.display = "none";
    }
    else
    {
        document.getElementById("paperdivpage").style.display = "block";
        document.getElementById("paperpagecount").style.display = "block";
    }
}
//改变每页记录数及跳至页数
function paperChangePageCountIndex(newPageCount,newPageIndex)
{
   var id=document.getElementById("hf_TypeID").value; 
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

    if(newPageCount <=0 || newPageIndex <= 0 ||  newPageIndex  > ((papertotalRecord-1)/newPageCount)+1 )
    {
        showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","转到页数超出查询范围！");
        return false;
    }
    else
    {
        lmIfDel = "0";
        this.paperpageCount=parseInt(newPageCount);
        PaperTurnToPage(parseInt(newPageIndex),id);
    }
}
//排序
function OrderBy(orderColum,orderTip)
{
    var id=document.getElementById("hf_TypeID").value; 
    if (papertotalRecord == 0) 
     {
        return;
     }
    lmIfDel = "0";
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
    paperorderBy = orderColum+"_"+ordering;
    PaperTurnToPage(1,id);
}
function GetPaperPlate(ID,Name,Type)
{
   if(floorid!="")
    {
    document.getElementById(floorid).value=ID;
    document.getElementById(floorname).value=Name;
    }
    if(floorid=="")
    {
        switch(Type)
        {
          case "9 ":
            document.getElementById("txt_Pnumber").value = Name;              
            document.getElementById("hf_txt_Pnumber").value = ID; 
            break;
          case "10":
            document.getElementById("txt_AbrasionResist").value=Name;
            document.getElementById("hf_txt_AbrasionResist").value = ID; 
            break;
          case "11":
             document.getElementById("txt_BalancePaper").value=Name;
             document.getElementById("hf_txt_BalancePaper").value = ID;
             break;
          case "12":
               document.getElementById("txt_BaseMaterial").value = Name; 
               document.getElementById("hf_txt_BaseMaterial").value = ID;
               break;
        }
    }
 
    document.getElementById('PaperPlateSel').style.display = "none";
    closeRotoscopingDiv(false,"divPaperPlate");//关闭遮罩层
}

function divPaperPlateClose()
{
    
    document.getElementById("txtName").value = "";
    document.getElementById("txtRemark").value = "";
    
    document.getElementById('PaperPlateSel').style.display='none';    
    closeRotoscopingDiv(false,"divPaperPlate");//关闭遮罩层
}
 </script>