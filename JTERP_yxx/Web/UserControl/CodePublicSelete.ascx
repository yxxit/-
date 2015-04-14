<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CodePublicSelete.ascx.cs" Inherits="UserControl_CodePublicSelete" %>
<script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>
<link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />


<input id="hf_TypeID" type="hidden" />

<div id="divPaperPlate1" style="display:none">
<iframe id="ifmPaperPlate1" frameborder="0" width="100%" ></iframe>
</div>
<div id="PaperPlateSel1" style="border: solid 5px #999999; background: #fff;width: 750px; z-index: 21; position: absolute;display: none;top: 50%; left: 60%; margin: 5px 0px 0px -400px">
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
      <tr bgcolor="#E7E7E7">
       <td  style="width:10%">
        <img id="btn_cancel" alt="关闭" src="../../../Images/Button/Bottom_btn_close.jpg" style='cursor:hand;' onclick="divPaperPlateClose1();" /> 
      </td>
      </tr>
 </table>
<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#CCCCCC">
      <tr>
        <td bgcolor="#FFFFFF">
            <table width="100%" border="0"  cellpadding="2" cellspacing="1" bgcolor="#CCCCCC" class="table">
        
            <tr>
            <td colspan="6" align="center" bgcolor="#FFFFFF">
               <img id="btn_search" alt="检索" src="../../../Images/Button/Bottom_btn_search.jpg" style='cursor:hand;' onclick='SearchPaperPlate1()'  /> 
            </td>
         </tr>
       </table>
       </td>
     </tr>
        </table>
        
 <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="paperpageDataList12" bgcolor="#999999">
    <tbody>
      <tr>
        <th height="20" align="center" class="td_list_fields" >选择</th>
        <th align="center" class="td_list_fields" ><div class="orderClick" onclick="OrderBy1('typecode','otypecode');return false;">分类类别<span id="otypecode" class="orderTip"></span></div></th>
        <th align="center" class="td_list_fields" ><div class="orderClick" onclick="OrderBy1('typename','otypename');return false;">分类名称<span id="otypename" class="orderTip"></span></div></th>       
        
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
                                                width="36" height="28" align="absmiddle" onclick="paperChangePageCountIndex1($('#paperShowPageCount').val(),$('#paperToPage').val());" />
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
    function getCodePublic(type,name,id)
    {
        
        floorname=name;
        floorid=id;
        PaperTurnToPage1(1,type);
        openRotoscopingDiv(false,"divPaperPlate1","ifmPaperPlate1");//弹遮罩层
        document.getElementById("PaperPlateSel1").style.display= "block";
    }

 function PaperTurnToPage1(pageIndex,Id)
    {
           currentPageIndex = pageIndex;
           if(Id!=""&&Id!=null&&Id!=undefined)
           {
                TypeID=Id;
           }
           document.getElementById("hf_TypeID").value=Id;

           $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/SupplyChain/PaperPlateSel.ashx',//目标地址
           cache:false,
           data: "pageIndex="+pageIndex+"&pageCount="+paperpageCount+"&orderby="+paperorderBy+
                  "&TypeID="+escape(TypeID)+"&action=codepublic",//数据
           beforeSend:function(){AddPop();$("#PaperpageDataList1_PagerList").hide();},//发送数据之前
           
           success: function(msg){
                    //数据获取完毕，填充页面据显示
                    //数据列表
                    $("#paperpageDataList12 tbody").find("tr.newrow").remove();                   
                    $.each(msg.data,function(i,item){
                      if(item.ID != null && item.ID!= "")  
                          var type="";
                                           
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center'>"+"<input onclick=\"GetPaperPlate1('"+item.ID+"','"+item.typename+"')\" id='getpaperplate' value="+item.ID+"  type='radio'/>"+"</td>"+                                                                                                       
                       
                        "<td height='22' align='center'>" + item.typecode + "</td>"+
                        "<td height='22' align='center'>" + item.typename+ "</td>").appendTo($("#paperpageDataList12 tbody"));
                   });
                    //页码
                   ShowPageBar("paperpageDataList1_PagerList",//[containerId]提供装载页码栏的容器标签的客户端ID
                   "<%= Request.Url.AbsolutePath %>",//[url]
                    {style:pagerStyle,mark:"pageDataList1Mark",
                    totalCount:msg.totalCount,showPageNumber:3,pageCount:paperpageCount,currentPageIndex:pageIndex,noRecordTip:"没有符合条件的记录",preWord:"上一页",nextWord:"下一页",First:"首页",End:"末页",
                    onclick:"PaperTurnToPage1({pageindex});return false;"}//[attr]
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
           complete:function(){if(lmIfDel=="0"){hidePopup();}$("#paperpageDataList1_PagerList").show();lmIfshow1(document.getElementById("Text6").value);pageDataList1("paperpageDataList12","#E7E7E7","#FFFFFF","#cfc","cfc");}//接收数据完毕
           });
    }
//弹出信息
function SearchPaperPlate1()
{
    lmIfDel = "0";
    var id=document.getElementById("hf_TypeID").value;
     PaperTurnToPage1(1,id);
    openRotoscopingDiv(false,"divPaperPlate1","ifmPaperPlate1")//弹遮罩层
    document.getElementById("PaperPlateSel1").style.display= "block";
}

function lmIfshow1(count)
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
function paperChangePageCountIndex1(newPageCount,newPageIndex)
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
        PaperTurnToPage1(parseInt(newPageIndex),id);
    }
}
//排序
function OrderBy1(orderColum,orderTip)
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
    PaperTurnToPage1(1,id);
}
function GetPaperPlate1(ID,Name)
{
   
    document.getElementById(floorid).value=ID;
    document.getElementById(floorname).value=Name;
    document.getElementById('PaperPlateSel1').style.display = "none";
    closeRotoscopingDiv(false,"divPaperPlate1");//关闭遮罩层
}

function divPaperPlateClose1()
{
    document.getElementById("txtName").value = "";
    document.getElementById("txtRemark").value = "";
    
    document.getElementById('PaperPlateSel1').style.display='none';    
    closeRotoscopingDiv(false,"divPaperPlate1");//关闭遮罩层
}
 </script>