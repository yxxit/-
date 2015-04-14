<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddLimitArea.ascx.cs"
    Inherits="UserControl_AddLimitArea" %>
<div id="areaSelect" style="border: solid 10px #93BCDD; background: #fff; padding: 10px;
    width: 500px; z-index: 2005; position: absolute; display: none; top: 40%; left: 60%;
    margin: 5px 0 0 -400px; height: 220px;">
    <div>
        <div style="overflow: scroll; height: 200px; text-align: left;">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <style type="text/css">
            .busBtn
            {
                background: url(../../../Images/default/btnbg.gif) 0px -5px;
                border: 1px solid #cccccc;
                padding-top: 2px;
                cursor: pointer;
                width: 100px;
            }
            .busBtnOnclick
            {
                background: url(../../../Images/default/btnbg2-1.png) 0px -5px;
                border: 1px solid #cccccc;
                padding-top: 2px;
                cursor: pointer;
                width: 100px;
            }
        </style>

        <script type="text/javascript" src="../../../js/JQuery/jquery_last.js"></script>

        <script type="text/javascript">
            function aa(){
                 var ck = document.getElementsByName("check");
                var strarr = '';
                var str = "";
                var n=0;

                 for (var i = 0; i < ck.length; i++) {
                    if (ck[i].checked) {
                       n+=i;
                    }
                    }
                    if(n==0)
                    alert("您尚未选择任何数据！");
                    else
                    {
                                      for (var i = 0; i < ck.length; i++) {
                    if (ck[i].checked) {
                        strarr+=ck[i].value+"、";
                    }
                    
                }   var aeraID=$("#aeraId").val();
               
                $(aeraID).val(strarr);
                $("#areaSelect").fadeOut();
                    }
              
            }

            function cancel()
            {
              $("#areaSelect").fadeOut();
            } 
            function clearAll()
            {
            var ck = document.getElementsByName("check");
             
              
                for (var i = 0; i < ck.length; i++) {
                    if (ck[i].checked) {
                       ck[i].checked=false;
                    }
                }
            }
 function showHTML(id)
     {
     var ck=$("#"+id+"");
            if(document.getElementById(id).checked==true)  
       ck.next("span").css('color','red');
       
      else
         ck.next("span").css('color','#000000');
// alert(ck.val());
     
     }
     
     function clearSelect()
{
    var ck = document.getElementsByName("Checkbox");
    var id;
     for (var i = 0; i < ck.length; i++) 
    {
        if (ck[i].checked) 
        {
           ck[i].checked=false;
           id=i*1+1;
//        alert($("#"+id+"").next("span"));
$("#"+id+"").next("span").html("11");
        }
    }

}
        </script>

    </div>
    <div>
        <input id="aeraId" type="hidden" /><input id="Button1" class="busBtn" type="button"
            value="确认" onclick="aa()" onmousedown="this.className='busBtnOnclick'" onmouseout="this.className='busBtn'" />
        <input id="Button2" class="busBtn" type="button" value="取消" onclick="cancel()" onmousedown="this.className='busBtnOnclick'"
            onmouseout="this.className='busBtn'" />
        <input id="Button3" class="busBtn" type="button" value="清除" onclick="clearAll()"
            onmousedown="this.className='busBtnOnclick'" onmouseout="this.className='busBtn'" />
    </div>
</div>
