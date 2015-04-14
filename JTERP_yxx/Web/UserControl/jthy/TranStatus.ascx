<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TranStatus.ascx.cs" Inherits="UserControl_jthy_TranStatus" %>

<script type="text/javascript">
    function confim() {
        //debugger;

        var mes = confirm("您确定要修改吗？");
        if (mes == true) {
            var action = "upStatus";

            var tranSportNo = document.getElementById("txtdan").value; //单据编号
            var status = document.getElementById("drpUPTranSportState").value;
            var statusId = document.getElementById("txtTranSportsID").value;

            $.ajax({
                type: "POST", //用POST方式传输
                dataType: "json", //数据格式:JSON
                url: '../../../Handler/JTHY/BusinessManage/DealInBus_ADD.ashx', //目标地址
                cache: false,
                data: "action=" + action + "&tranSportNo=" + escape(tranSportNo) + "&status=" + status + "&statusId=" + statusId,
                beforeSend: function() { }, //发送数据之前

                success: function(data) {
                    //数据获取完毕，填充页面据显示
                    //数据列表

                    if (data.sta > 0) {
                        var statuu = $("#drpUPTranSportState").find("option:selected").text();
                        GetValuee(statuu);
                    }
                },
                error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误！"); },
                complete: function() {
                    closeRotoscopingDiv(false, "div_TranSport"); //关闭遮罩层
                    document.getElementById('editPanel').style.display = 'none';
                }
                //接收数据完毕

            });

        }
        
    }
    function ChangeStatus(TranSportNo, TranSportState, tarnId) {
        debugger;
        $("#txtdan").val(TranSportNo);
        $("#current").val(TranSportState);
        $("#txtTranSportsID").val(tarnId);
        $("#editPanel").css("top", document.body.clientHeight - 450 + 'px');
        $("#editPanel").css("left", "60%");
        openRotoscopingDiv(false, "div_TranSport", "ifm_TranSport"); //弹遮罩层
        $("#editPanel").css("display", "block");
        $("#confi").css("display", "block");
          $("#drpUPTranSportState").html("");
        var op = "<option value='10' >制单</option>" +
          " <option value='20' >装车</option> " +
          " <option value='30' >发货</option> " +
          " <option value='40' >在途</option> " +
           " <option value='50' >到货</option> ";
      
        $("#drpUPTranSportState").append(op);
   

        //$("#drpUPTranSportState").val(TranSportState);
        if (TranSportState == "制单") {

            $("#drpUPTranSportState option[value='10']").remove();
             // $("#confi").css("display", "none");
             //  $("#confi").css("display", "blcok");
            //  document.getElementById("confi").display="block";
        }
        else if (TranSportState == "装车") {
            $("#drpUPTranSportState option[value='10']").remove();
            $("#drpUPTranSportState option[value='20']").remove();
          //  $("#confi").css("display", "block");
        }
        else if (TranSportState == "发货") {
            $("#drpUPTranSportState option[value='10']").remove();
            $("#drpUPTranSportState option[value='20']").remove();
            $("#drpUPTranSportState option[value='30']").remove();
          //  $("#confi").css("display", "blcok");
           // document.getElementById("confi").display="block";
        }

        else if (TranSportState == "在途") {
            $("#drpUPTranSportState option[value='10']").remove();
            $("#drpUPTranSportState option[value='20']").remove();

            $("#drpUPTranSportState option[value='30']").remove();
            $("#drpUPTranSportState option[value='40']").remove();
         //   $("#confi").css("display", "block");
         //  document.getElementById("confi").display="block";
            
        }
        else {

            $("#drpUPTranSportState option[value='10']").remove();
            $("#drpUPTranSportState option[value='20']").remove();

            $("#drpUPTranSportState option[value='30']").remove();
            $("#drpUPTranSportState option[value='40']").remove();

            $("#drpUPTranSportState").val(TranSportState);
           $("#confi").css("display", "none");
        }
    }
    function Cancleww() {
        closeRotoscopingDiv(false, "div_TranSport"); //关闭遮罩层
        document.getElementById('editPanel').style.display = 'none';
    } 
</script>

<div id="div_TranSport" style="display: none">
    <iframe id="ifm_TranSport" frameborder="0" width="100%"></iframe>
</div>
<div id="editPanel" style="border: solid 5px #999999; background: #fff; width: 320px;
    z-index: 21; position: absolute; display: none; margin: 5px 0 0 -400px">
    <table bgcolor="#F5F5F5" id="itemContainer" cellspacing="1" cellpadding="1" width="320px">
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 单号：
            </td>
            <td>
                <input id="txtdan" type="text"  disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;当前的状态：
            </td>
            <td>
                <input id="current" type="text"  disabled="disabled" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp; 更改状态：
            </td>
            <td>
                <select id="drpUPTranSportState" name="drpUPTranSportState">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <input type="hidden" id="txtTranSportsID" value="" />
            </td>
            <td>
            </td>
            <td>

  
                <a href="#" id="confi" style=" float:left"  onclick="confim()">确定</a>&nbsp;&nbsp; <a href="#" onclick="Cancleww()">

                    取消</a>&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</div> 
