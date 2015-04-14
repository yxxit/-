<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YYCurrentStock.ascx.cs" Inherits="UserControl_Common_YYCurrentStock" %>
    <!--提示信息弹出详情start-->
<div id="divzhezhao2" style="filter: Alpha(opacity=0); width: 93%; padding: 1px;height: 500px; z-index: 201; bottom:0px; left:0px;position: absolute; display: none;">
</div>
    <div  id="divCurrentStock" style="border: solid 10px #93BCDD; background: #fff; padding: 10px; width: 1000px;
        z-index: 1001; position: absolute; display:none; top: 30%; left: 40%; margin: 5px 0 0 -400px;" onmousedown="javascript:moveStart(event,this.id);">        
        <div align="right"> 
        <a onclick="closeCurrentStock()" style="text-align: right; cursor: pointer" class="Title">
            关闭</a>
        </div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" id="CurrentStock"
            bgcolor="#999999">  
            <tbody>
                <tr class="table-item">
                    <td align="center" class="td_list_fields">                       
                        仓库编号<span id="StorageNo" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">                       
                        仓库名称<span id="StorageName" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">                       
                        药品编码<span id="ProductNo" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">                        
                        物品名称<span id="ProductName" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">                       
                        规格型号<span id="Specification" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">
                        主计量单位<span id="UnitName" class="orderTip"></span>
                    </td>
                     <td align="center" class="td_list_fields">
                        计量单位组<span id="cGroupName" class="orderTip"></span>
                    </td>
                     <td align="center" class="td_list_fields">                       
                        结存数量<span id="QuantityNum" class="orderTip"></span>
                    </td>
                     <td align="center" class="td_list_fields">                       
                        可用数量<span id="KyQuantity" class="orderTip"></span>
                    </td>
                     <td align="center" class="td_list_fields">                       
                        可用件数<span id="KyNum" class="orderTip"></span>
                    </td>
                    <td align="center" class="td_list_fields">                       
                        批次<span id="cBatch" class="orderTip"></span>
                    </td>
                   
                </tr>
            </tbody>
        </table>
       
        <br />
    </div>
    <!--提示信息弹出详情end-->
    
    <script type="text/javascript" language="javascript">
    
    // 关闭询价历史层

function closeCurrentStock()
{
    document.getElementById("divCurrentStock").style.display = "none";
    var ProductBigDiv = document.getElementById("ProductBigDiv");
    document.body.removeChild(ProductBigDiv);
    document.getElementById('divzhezhao2').style.display = 'none';
    
}

function AlertProductMsg() {
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
    maskObj.setAttribute('id', 'ProductBigDiv');
    maskObj.style.position = "absolute";
    maskObj.style.top = "0";
    maskObj.style.left = "0";
    maskObj.style.background = "#fff";
    maskObj.style.filter = "Alpha(opacity=70);";
    maskObj.style.opacity = "0.3";
    maskObj.style.width = sWidth + "px";
    maskObj.style.height = sHeight + "px";
    maskObj.style.zIndex = "100";
    document.body.appendChild(maskObj);
}

//获取报价单历史记录
function fnGetCurrentStockByNo(prodno) {
    $.ajax({
        type: "POST",
        url: "../../../Handler/Office/MedicineManager/Medicinelist.ashx",
        data: "action=getCurrentStock&ProdNo=" + escape(prodno),
        dataType: 'json', //返回json格式数据
        cache: false,
        beforeSend: function() {
            AddPop(); 
        },
        error: function() {
            popMsgObj.ShowMsg('请求发生错误！');
        },
        success: function(msg) {
            $("#CurrentStock tbody").find("tr.newrow").remove();
            if(msg.data!=undefined)
            {
            $.each(msg.data, function(i, item) {
                if (item.StorageNo != null && item.StorageNo != "") {
                    $("<tr class='newrow'></tr>").append(
                    "<td height='22' align='center'>" + item.StorageNo + "</td>" +
                 "<td height='22' align='center'>" + item.StorageName + "</td>" +
                 "<td height='22' align='center'>" + item.ProductNo + "</td>" +
                 "<td height='22' align='center'>" + item.ProductName + "</td>" +
                  "<td height='22' align='center'>" + item.Specification + "</td>" +
                  "<td height='22' align='center'>" + item.UnitName + "</td>" +
                   "<td height='22' align='center'>" + item.cGroupName + "</td>" +
                  "<td height='22' align='center'>" + item.QuantityNum + "</td>" +
                   "<td height='22' align='center'>" + item.KyNum + "</td>" +
                 "<td height='22' align='center'>" + item.KyRateNum + "</td>" +
                 "<td height='22' align='center'>" + item.cBatch + "</td>").appendTo($("#CurrentStock tbody"));
                }
            });
            }
        },
        complete: function() { hidePopup(); pageDataListProduct("CurrentStock", "#E7E7E7", "#FFFFFF", "#cfc", "cfc");} //接收数据完毕
    });
}
    //table行颜色
    function pageDataListProduct(o, a, b, c, d) 
    {
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
    
    </script>
