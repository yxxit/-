   $(document).ready(function()
{
   getmonthchecklist();
  
});

   function getmonthchecklist()
    {
        //Start验证输入
        var num = 0;
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: '../../../Handler/Office/StorageManager/MonthCheck.ashx', //目标地址
            cache: false,
            data: "Action=monthcheck", //数据
            beforeSend: function() { $("#pageDataList1_Pager").hide(); }, //发送数据之前

            success: function(msg) {
                //数据获取完毕，填充页面据显示
                //数据列表
                $("#pageDataList1 tbody").find("tr.newrow").remove();
                $.each(msg.data, function(i, item) {
                   
                    if (item.id != null && item.id != "" && item.id != "undefined" && item.billtype.toString() == "0") {

                        if (num == 0) {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor='#FFFFFF'>" + getCheckBox(item) + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.years + "</td>" +
                               "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.months + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"stime" + item.id + "\" style=\"width:95%\" readonly value=\""+item.Stime+"\"></td>" +
                                 "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"etime" + item.id + "\" style=\"width:95%\" readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SellDate1')})\"></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.types + "</td>").appendTo($("#pageDataList1 tbody"));
                        } else {
                            $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor='#FFFFFF'>" + getCheckBox(item) + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.years + "</td>" +
                               "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.months + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"stime" + item.id + "\" style=\"width:95%\" readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SellDate')})\"></td>" +
                                 "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"etime" + item.id + "\" style=\"width:95%\" readonly onclick=\"WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('SellDate1')})\"></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.types + "</td>").appendTo($("#pageDataList1 tbody"));
                        }
                        num++;
                    } else {
                        $("<tr class='newrow'></tr>").append("<td height='22' align='center' bgcolor='#FFFFFF'>" + getCheckBox(item) + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.years + "</td>" +

                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.months + "</td>" +
                                 "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"stime" + item.id + "\" style=\"width:95%\" readonly value=\"" + item.Stime + "\"></td>" +
                                 "<td height='22' align='center' bgcolor='#FFFFFF'><input type=\"text\" class=\"tdinput\" id=\"etime" + item.id + "\" style=\"width:95%\" readonly value=\"" + item.Etime + "\"></td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.man + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.checkdate + "</td>" +
                                "<td height='22' align='center' bgcolor='#FFFFFF'>" + item.types + "</td>").appendTo($("#pageDataList1 tbody"));
                    }
                    //                         }
                });




            },
            error: function() { }, /*E4F4FD,C0E8FF*/
            complete: function() { $("#pageDataList1_Pager").show(); } //接收数据完毕
        });
           

    }
    
    function getCheckBox(item)
{

             return "<input type=\"checkbox\" id=\"chk_"+item.id+"\" name=\"chk\" value=\""+item.years+","+item.months+","+item.billtype+","+item.id+"\"    />";
    
}

//结算
function monthcheck()
{
    var aa=document.getElementsByName("chk");
    var num=0;
    var chid="";
    for(var i=0;i<aa.length;i++)
    {
        if(aa[i].checked==true)
        {
            chid=aa[i].id;
            num++
        }
    }
    if(num==0)
    {
        popMsgObj.ShowMsg('请选择要结算的月');
        return;
    }
    if(num>1)
    {
        popMsgObj.ShowMsg('不可同时选择多个月');
        return;
    }
    var str=document.getElementById(chid).value;
    var arry=str.split(',');
    if(arry[2]=="1")
    {
        popMsgObj.ShowMsg('请选择未结算月');
        return;
    }
    if(parseInt(arry[3])-1>0)
    {
        var id=parseInt(arry[3])-1
        var ch=document.getElementById("chk_"+id).value;
        var arr=ch.split(',');
        {
            if(arr[2]=="0")
            {
                popMsgObj.ShowMsg('请先结算上月');
                return;
            }
        }
    }
//    var myDate = new Date(); 
//    var year= myDate.getFullYear(); 
//    var month=myDate.getMonth(); 
//    month++;
//    if(parseInt(arry[0])==year&&parseInt(arry[1])==month)
//    {
//        popMsgObj.ShowMsg('请在本月结束后结算');
//        return;
//    }
    var price = 0;
    var date1 = "";
    var date2 = "";
    date1 = document.getElementById("stime" + arry[3]).value;
    date2 = document.getElementById("etime" + arry[3]).value;
    if (date1 == "" && date2 == "") {
        popMsgObj.ShowMsg('请选择要结算的日期范围！');
        return;
    }
    else if (date1 == "" && date2 != "") {
    popMsgObj.ShowMsg('请选择要结算的起始日期！');
    return;
    }
    else if (date1 != "" && date2 == "") {
    popMsgObj.ShowMsg('请选择要结算的截止日期！');
    return;
    }
    if (date1 != "" && date2 != "") {
        if (date1 > date2) {
            popMsgObj.ShowMsg('日期范围输入错误！');
            return;
        }
    }
    var today = new Date();
    var newtoday="";
    var year = today.getFullYear();
    var month = today.getMonth()+1;
    var day = today.getDate();
    if (parseInt(month) < 10) {
        month = "0" + month;
    }
    if (parseInt(day) < 10) {
        day = "0" + day;
    }
    newtoday = year + "-" + month + "-" + day;
    if(date2>=newtoday) {
        popMsgObj.ShowMsg('只能结算当天以前的数据！');
         return;
    }

    var count=0;
//    getdiv("divjindu");
         $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/StorageManager/MonthCheck.ashx',//目标地址
               cache: false,
               data: "Action=checkit&year=" + escape(arry[0]) + "&month=" + escape(arry[1]) + "&sdate=" + escape(date1) + "&edate=" + escape(date2), //数据
               beforeSend: function() { $("#pageDataList1_Pager").hide(); getdiv1("divjindu"); }, //发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#Table1 tbody").find("tr.newrow").remove();
                        
                        $.each(msg.data,function(i,item){
                            if(item.id != null && item.id != "" && item.id !="undefined")
                            {
                                count++;
                                
                            }
                            
                       });
                        if(count==0)
                        {
                           
                            $.ajax({
                           type: "POST",//用POST方式传输
                           dataType:"json",//数据格式:JSON
                           url:  '../../../Handler/Office/StorageManager/MonthCheck.ashx',//目标地址
                           cache:false,
                           data: "Action=close&year=" + escape(arry[0]) + "&month=" + escape(arry[1]) + "&price=" + escape(price) + "&sdate=" + escape(date1) + "&edate=" + escape(date2), //数据
                           beforeSend:function(){$("#pageDataList1_Pager").hide();},//发送数据之前
                           
                           success: function(data){
                                    if(data.sta>0) 
                                    {
                                        popMsgObj.ShowMsg(data.info);
                                        
                                        getmonthchecklist();
                                        
                                    }else
                                    {
                                         popMsgObj.ShowMsg(data.info);
                                    }
                              },
                           error: function() {popMsgObj.ShowMsg('请求发生错误');document.getElementById("divjindu").style.display="none";}, /*E4F4FD,C0E8FF*/
                           complete:function(){document.getElementById("divjindu").style.display="none";}//接收数据完毕
                           });
                        }else
                        {
                            getdiv("divBillChoose");
                            document.getElementById("divjindu").style.display = "none";
                            $.each(msg.data,function(i,item){
                            if(item.id != null && item.id != "" && item.id !="undefined")
                            {
                                $("<tr class='newrow'></tr>").append(
                                "<td height='22' align='center' bgcolor='#FFFFFF'>"+item.billname+"</td>"+
                                "<td height='22' align='center' bgcolor='#FFFFFF'>"+item.billno+"</td>").appendTo($("#Table1 tbody"));
                                count++; 
                            }
                            
                       });
                        }
                  },
               error: function() {popMsgObj.ShowMsg('请求发生错误');document.getElementById("divjindu").style.display="none";}, /*E4F4FD,C0E8FF*/
               complete: function() { $("#Table1_Pager").show(); } //接收数据完毕
               });
}
//取消
function uncheck()
{
    var aa=document.getElementsByName("chk");
    var num=0;
    var chid="";
    for(var i=0;i<aa.length;i++)
    {
        if(aa[i].checked==true)
        {
            chid=aa[i].id;
            num++
        }
    }
    if(num==0)
    {
        popMsgObj.ShowMsg('请选择要取消结算的月');
        return;
    }
    if(num>1)
    {
        popMsgObj.ShowMsg('不可同时选择多个月');
        return;
    }
    var str=document.getElementById(chid).value;
    var arry=str.split(',');
    if(arry[2]=="0")
    {
        popMsgObj.ShowMsg('请选择已结算月');
        return;
    }

    var id = parseInt(arry[3]) + 1;
    var obj = document.getElementById("chk_" + id);
    if (obj) {
        var ch = document.getElementById("chk_" + id).value;
        var arr = ch.split(',');
        {
            if (arr[2] == "1") {
                popMsgObj.ShowMsg('请先取消下月');
                return;
            }
        }
    }
    var date1 = document.getElementById("stime" + arry[3]).value;
    var date2 = document.getElementById("etime" + arry[3]).value;
    
      $.ajax({
           type: "POST",//用POST方式传输
           dataType:"json",//数据格式:JSON
           url:  '../../../Handler/Office/StorageManager/MonthCheck.ashx',//目标地址
           cache: false,
           data: "Action=unclose&year="+escape(arry[0])+"&month="+escape(arry[1])+"&sdate="+escape(date1)+"&edate="+escape(date2),//数据
           beforeSend:function(){$("#pageDataList1_Pager").hide();getdiv1("divjindu");},//发送数据之前
           
           success: function(data){
                    if(data.sta>0) 
                    {
                        popMsgObj.ShowMsg(data.info);
                        getmonthchecklist();
                        
                    }else
                    {
                         popMsgObj.ShowMsg(data.info);
                    }
              },
           error: function() {popMsgObj.ShowMsg('请求发生错误');document.getElementById("divjindu").style.display="none";}, /*E4F4FD,C0E8FF*/
           complete:function(){document.getElementById("divjindu").style.display="none";}//接收数据完毕
           });
}
//检查
function checkbill()
{
    var aa=document.getElementsByName("chk");
    var num=0;
    var chid="";
    for(var i=0;i<aa.length;i++)
    {
        if(aa[i].checked==true)
        {
            chid=aa[i].id;
            num++
        }
    }
    if(num==0)
    {
        popMsgObj.ShowMsg('请选择要检查的月');
        return;
    }
    if(num>1)
    {
        popMsgObj.ShowMsg('不可同时选择多个月');
        return;
    }
    var str=document.getElementById(chid).value;
    var arry=str.split(',');
    if(arry[2]=="1")
    {
        popMsgObj.ShowMsg('请选择未结算月');
        return;
    }
    var date1 = "";
    var date2 = "";
    date1 = document.getElementById("stime" + arry[3]).value;
    date2 = document.getElementById("etime" + arry[3]).value;
    if (date1 == "" && date2 == "") {
        popMsgObj.ShowMsg('请选择要结算的日期范围！');
        return;
    }
    else if (date1 == "" && date2 != "") {
    popMsgObj.ShowMsg('请选择要结算的起始日期！');
    return;
    }
    else if (date1 != "" && date2 == "") {
    popMsgObj.ShowMsg('请选择要结算的截止日期！');
    return;
    }
    if (date1 != "" && date2 != "") {
        if (date1 > date2) {
            popMsgObj.ShowMsg('日期范围输入错误！');
            return;
        }
    }
    var count=0;
         $.ajax({
               type: "POST",//用POST方式传输
               dataType:"json",//数据格式:JSON
               url:  '../../../Handler/Office/StorageManager/MonthCheck.ashx',//目标地址
               cache:false,
               data: "Action=checkit&year=" + escape(arry[0]) + "&month=" + escape(arry[1]) + "&sdate=" + escape(date1) + "&edate=" + escape(date2), //数据
               beforeSend:function(){$("#pageDataList1_Pager").hide();getdiv1("divjindu"); },//发送数据之前
               
               success: function(msg){
                        //数据获取完毕，填充页面据显示
                        //数据列表
                        $("#Table1 tbody").find("tr.newrow").remove();
                        
                        $.each(msg.data,function(i,item){
                            if(item.id != null && item.id != "" && item.id !="undefined")
                            {
                                count++;
                                
                            }
                            
                       });
                        if(count==0)
                        {
                            popMsgObj.ShowMsg('当前月可以结算'); 
                        }else
                        {
                            document.getElementById("divjindu").style.display="none";
                            
                            getdiv("divBillChoose");
                            $.each(msg.data,function(i,item){
                            if(item.id != null && item.id != "" && item.id !="undefined")
                            {
                                $("<tr class='newrow'></tr>").append(
                                "<td height='22' align='center' bgcolor='#FFFFFF'>"+item.billname+"</td>"+
                                "<td height='22' align='center' bgcolor='#FFFFFF'>"+item.billno+"</td>").appendTo($("#Table1 tbody"));
                                count++; 
                            }
                            
                       });
                        }
                  },
               error: function() {popMsgObj.ShowMsg('请求发生错误');document.getElementById("divjindu").style.display="none";}, /*E4F4FD,C0E8FF*/
               complete:function(){$("#Table1_Pager").show(); document.getElementById("divjindu").style.display="none";
               }//接收数据完毕
               });
           
}

function getdiv(id)
{
    var h=parseFloat(document.documentElement.clientHeight);
    var w=parseFloat(document.documentElement.clientWidth);
    
    var oObj = document.getElementById(id);
    var divw=parseFloat(oObj.style.width);
    var divh=parseFloat(oObj.style.height);
    w=(w-divw)/2;
    h=(h-divh)/2;
    oObj.style.left = 280+parseInt(w)+"px";
    oObj.style.top = parseInt(h)+"px";
    document.getElementById(id).style.display="block";
}
function getdiv1(id)
{
    var h=parseFloat(document.documentElement.clientHeight);
    var w=parseFloat(document.documentElement.clientWidth);
    
    var oObj = document.getElementById(id);
    var divw=parseFloat(oObj.style.width);
    var divh=parseFloat(oObj.style.height);
    w=(w-divw)/2;
    h=(h-divh)/2;
    oObj.style.left = parseInt(w)+"px";
    oObj.style.top = parseInt(h)+"px";
    document.getElementById(id).style.display="block";
}
//关闭层
function closeBilldiv()
{
    document.getElementById("divBillChoose").style.display="none";
}
//shuju
function checkthis(str,count)
{
    
    if(str!=null)
    {
        if(parseFloat(str.value).toString()=="NaN")
        {
            str.value="0.00";
        }else
        {
            str.value=parseFloat(str.value).toFixed(count);
        }
    }
}


