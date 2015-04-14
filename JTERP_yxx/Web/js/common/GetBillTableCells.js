//---------------------shjp:2011-9-3 明细扩展项---------------------------
//添加表头显示数据
/**************************************************************************
参数说明：
IsAddTitel：true表示需要添加表头，false表示不添加表头
IsAddCells:true表示添加单元格并填充数据，false添加单元格不填充数据
TabName：明细表名称。
data：单据返回的json 数据
Tid：需要添加扩展项的table的ID
DIV_ID:放明细表div的ID
***************************************************************************/
var RowNum=0;//扩展项的数量
var Isfirst=true;//脚本是否为第一次执行
//整个添加
function get_Table_Cell_add(IsAddTitel,IsAddCells,TabName,data,Tid,DIV_ID)
{
    var tname = TabName.split('*')[0];
    
    var table=document.getElementById(Tid);
    //获取行数
     var tablen=table.rows.length;
     var row=0;
     if(data!=null)
     {
        //当前添加明细扩展行的起始行
        row=tablen-data.length;
     }
     var num=1;
    //已得到扩展项数量不用查询数据库
    if(RowNum==0)
    {
        $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: "../../../Handler/Common/GetBillTableCells.ashx", //目标地址
            cache: true,
            data: 'action=all&TableName=' + tname,
            beforeSend: function() { }, //发送数据之前
            success: function(msg) {
                //获取扩展项数量
                RowNum=parseInt(msg.totalCount);
                if(RowNum>0&&DIV_ID!=null&&DIV_ID!="")
                {   //增加table宽度
                    Add_div_width(DIV_ID);
                }
                if (parseInt(msg.totalCount) > 0) {
                    if(IsAddTitel)
                    {
                        $.each(msg.data, function(i, item) {
                        
                             //填充表头
                             table.rows[0].insertCell().innerHTML=item.EFDesc;
                             //复制样式
                             var col=1;
                             if(table.rows.item(0).cells.length-col>=0)
                             {
                                 while(1==1)
                                 {
                                     if(table.rows[0].cells[table.rows.item(0).cells.length-col].style.display!="none")
                                     {
                                         table.rows[0].cells[table.rows.item(0).cells.length-col].className=table.rows[0].cells[0].className;
                                         table.rows[0].cells[table.rows.item(0).cells.length-col].align=table.rows[0].cells[0].align;
                                         break;
                                     }else
                                     {
                                        col++;
                                     }
                                 }
                             }
                        });
                     //第一次执行时为table列重新分配宽度
                     if(Isfirst&&RowNum>0)
                     {
                        if(DIV_ID!=null&&DIV_ID!=undefined)
                        {
                            Cells_width_again(Tid,DIV_ID);
                            Isfirst=false;
                        }
                     }
                    }
                    if(data!=null)
                    {
                        if(IsAddCells)
                        {
                            //填充扩展项数据
                             $.each(data, function(i, item) {
                                num=1;
                                 for(var j=0;j<RowNum;j++)
                                 {
                                    var cell="Custom"+num;
                                    num++;
                                    table.rows[row].insertCell().innerHTML="<input type=\"text\" style=\"width:88%;\" id='"+cell+"_" + row + "' value=\"" + item[cell] + "\" class=\"tdinput\"   />";
                                    
                                     //复制样式
                                     var td_class="";
                                     var td_align="";
                                     for(var cell_count=0;cell_count<table.rows.item(row).cells.length;cell_count++)
                                     {
                                        if(table.rows.item(row).cells[cell_count].className!=""&&table.rows.item(row).cells[cell_count].className!=undefined)
                                        {
                                            td_class=table.rows.item(row).cells[cell_count].className;
                                            td_align=table.rows.item(row).cells[cell_count].align;
                                            break;
                                        }
                                     }
                                     if(table.rows.item(row).cells.length-1>=0)
                                     {
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].className=td_class;
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].align=td_align;
                                     }
                                      table.rows[row].cells[table.rows.item(row).cells.length-1].style.width="60px";
                                 }
                                 row++;
                            });
                        }else
                        {
                             //填充扩展项数据
                             $.each(data, function(i, item) {
                                num=1;
                                 for(var j=0;j<RowNum;j++)
                                 {
                                   var cell="Custom"+num;
                                    num++;
                                    table.rows[row].insertCell().innerHTML="<input type=\"text\" style=\"width:88%;\" id='"+cell+"_" + row + "' value=\"\" class=\"tdinput\"   />";
                                    
                                      //复制样式
                                     var td_class="";
                                     var td_align="";
                                     for(var cell_count=0;cell_count<table.rows.item(row).cells.length;cell_count++)
                                     {
                                        if(table.rows.item(row).cells[cell_count].className!=""&&table.rows.item(row).cells[cell_count].className!=undefined)
                                        {
                                            td_class=table.rows.item(row).cells[cell_count].className;
                                            td_align=table.rows.item(row).cells[cell_count].align;
                                            break;
                                        }
                                     }
                                     if(table.rows.item(row).cells.length-1>=0)
                                     {
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].className=td_class;
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].align=td_align;
                                     }
                                     table.rows[row].cells[table.rows.item(row).cells.length-1].style.width="60px";
                                 }
                                 row++;
                            });
                        }
                    }
                }
            },
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
            complete: function() { } //接收数据完毕
        });
        }else
        {
             if(data!=null)
                    {
                        if(IsAddCells)
                        {
                            //添加扩展项单元格
                             $.each(data, function(i, item) {
                                 num=1;
                                 for(var j=0;j<RowNum;j++)
                                 {
                                    var cell="Custom"+num;
                                    num++;
                                    table.rows[row].insertCell().innerHTML="<input type=\"text\" style=\"width:88%;\" id='"+cell+"_" + row + "' value=\"" + item[cell] + "\" class=\"tdinput\"   />";
                                     
                                      //复制样式
                                     var td_class="";
                                     var td_align="";
                                     for(var cell_count=0;cell_count<table.rows.item(row).cells.length;cell_count++)
                                     {
                                        if(table.rows.item(row).cells[cell_count].className!=""&&table.rows.item(row).cells[cell_count].className!=undefined)
                                        {
                                            td_class=table.rows.item(row).cells[cell_count].className;
                                            td_align=table.rows.item(row).cells[cell_count].align;
                                            break;
                                        }
                                     }
                                     if(table.rows.item(row).cells.length-1>=0)
                                     {
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].className=td_class;
                                         table.rows[row].cells[table.rows.item(row).cells.length-1].align=td_align;
                                     }
                                 }
                                 row++;
                            });
                        }else
                        {
                             //添加扩展项单元格
                            $.each(data, function(i, item) {
                                num = 1;
                                for (var j = 0; j < RowNum; j++) {
                                    var cell = "Custom" + num;

                                    if (TabName.split('*')[1] != "" && TabName.split('*')[1] != undefined) {
                                        var CandI = TabName.split('*')[1].split(',');
                                        for (var k = 0; k < CandI.length; k++) {
                                            var exf = CandI[k].split('|');
                                            var result = 0;
                                            if (parseInt(exf[0]) == num) {
                                                result++;
                                                var ename = "ExtField" + exf[1];
                                                table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"" + item[ename] + "\" class=\"tdinput\"   />";
                                                num++;
                                                break;
                                            }
                                        }
                                        if (result == 0) {
                                            table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"\" class=\"tdinput\"   />";
                                            num++;
                                        }


                                    }
                                    else {
                                        table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"\" class=\"tdinput\"   />";
                                        num++;

                                    }
                                    //复制样式
                                    var td_class = "";
                                    var td_align = "";
                                    for (var cell_count = 0; cell_count < table.rows.item(row).cells.length; cell_count++) {
                                        if (table.rows.item(row).cells[cell_count].className != "" && table.rows.item(row).cells[cell_count].className != undefined) {
                                            td_class = table.rows.item(row).cells[cell_count].className;
                                            td_align = table.rows.item(row).cells[cell_count].align;
                                            break;
                                        }
                                    }
                                    if (table.rows.item(row).cells.length - 1 >= 0) {
                                        table.rows[row].cells[table.rows.item(row).cells.length - 1].className = td_class;
                                        table.rows[row].cells[table.rows.item(row).cells.length - 1].align = td_align;
                                    }
                                }
                                row++;
                            });
                        }
                 }
                
        }
         
        
}

//单行添加
function get_Table_Cell_add_1(IsAddTitel,IsAddCells,TabName,data,Tid,DIV_ID) {
    var tname = TabName.split('*')[0];
    
    var table=document.getElementById(Tid);
    var tablen=table.rows.length;
    var row=tablen-1;
    if(row==0)
    {return;}
     var num=1;
        if(RowNum>0)
        {
            if(!IsAddCells)
            {
                 //添加扩展项单元格
                $.each(data, function(i, item) {
                    num = 1;
                    for (var j = 0; j < RowNum; j++) {
                        var cell = "Custom" + num;
                         if (TabName.split('*')[1] != "" && TabName.split('*')[1] != undefined) {
                             var CandI = TabName.split('*')[1].split(',');
                             for (var k = 0; k < CandI.length; k++) {
                                var exf = CandI[k].split('|');
                                var result = 0;
                                if (parseInt(exf[0]) == num) {
                                                result++;
                                                var ename = "ExtField" + exf[1];
                                                table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"" + item[ename] + "\" class=\"tdinput\"   />";
                                                num++;
                                                break;
                                            }
                                    }
                                    if (result == 0) {
                                        table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"\" class=\"tdinput\"   />";
                                        num++;
                                    }


                                }
                          else {
                                    table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"\" class=\"tdinput\"   />";
                                    num++;

                                }
//                        num++;
//                        table.rows[row].insertCell().innerHTML = "<input type=\"text\" style=\"width:88%;\" id='" + cell + "_" + row + "' value=\"\" class=\"tdinput\"   />";
                        //复制样式
//                        if (table.rows.item(row).cells.length - 1 >= 0) {
//                            var td_class = "";
//                            var td_align = "";
//                            for (var k = table.rows.item(row).cells.length - 1; k >= 0; k--) {
//                                if (table.rows[row].cells[k].style.display != "none") {
//                                    td_class = table.rows[row].cells[k].className;
//                                    td_align = table.rows[row].cells[k].align;
//                                    break;
//                                }
//                            }
//                            table.rows[row].cells[table.rows.item(row).cells.length - 1].className = td_class;
//                            table.rows[row].cells[table.rows.item(row).cells.length - 1].align = td_align;
//                        }
                        //复制样式
                        var td_class = "";
                        var td_align = "";
                        for (var cell_count = 0; cell_count < table.rows.item(row).cells.length; cell_count++) {
                            if (table.rows.item(row).cells[cell_count].className != "" && table.rows.item(row).cells[cell_count].className != undefined) {
                                td_class = table.rows.item(row).cells[cell_count].className;
                                td_align = table.rows.item(row).cells[cell_count].align;
                                break;
                            }
                        }
                        if (table.rows.item(row).cells.length - 1 >= 0) {
                            table.rows[row].cells[table.rows.item(row).cells.length - 1].className = td_class;
                            table.rows[row].cells[table.rows.item(row).cells.length - 1].align = td_align;
                        }
                     }
                });
            }  
        }
 
}
//保存调用方法：Tid明细表id
function GetTableCustom(Tid)
{
    //是否有扩展项
    if(RowNum==0)
    {
        return "&strCustom=&RowNum=0";
    }else
    {
     var signFrame = findObj(Tid, document);
     var count=0;
     var Customsvalue= new Array();
     for (var i = 1; i < signFrame.rows.length; i++) 
     {
      if (signFrame.rows[i].style.display != "none") {
            var rowid = signFrame.rows[i].rowIndex;
            count++;
            var str="";
            for(var j=1;j<=RowNum;j++)
            {
                var id="Custom"+j+"_"+rowid;
                if(j==RowNum)
                {
                    str+=document.getElementById(id).value;
                }else
                {
                     str+=document.getElementById(id).value+",";
                }
            }
            Customsvalue[count]=str;
         }
     }
     return "&RowNum="+escape(RowNum)+"&strCustom="+escape(Customsvalue.join("|"));
     }
}
//为明细table所在DIV增加宽度
function Add_div_width(DIV_ID)
{
//    var get_div=document.getElementById(DIV_ID);
//    //当前宽度百分比
//    var num1=parseInt(get_div.style.width);
//    //当前可用宽度
//    var num2=parseInt(get_div.clientWidth);
//    if(num2==0)
//    {return;}
//    //每列增加宽度100
//    var num3=100*RowNum;
//    //每个百分比对应的宽度
//    var num4=num2/num1;
//    //增加列的百分比
//    var num5=parseInt(num3/num4);
//   
//    //为table重新赋值百分比
//    get_div.style.width=(num1+num5).toString()+"%";
}
//为table列重新分配宽度
function Cells_width_again(Tid,DIV_ID)
{
//    var table=document.getElementById(Tid);
//    var get_div=document.getElementById(DIV_ID);
//    var total=parseInt(get_div.clientWidth);
//    if(total==0)
//    {return;}
//    var Cellcount=0;
//     //计算显示列数
//     for(var j=0;j<table.rows.item(0).cells.length;j++)
//     {
//        if(table.rows[0].cells[j].style.display!="none")
//        {
//            Cellcount++;
//        }
//     }
//     //计算每列平均值
//     var everywidth=parseFloat(total/Cellcount).toFixed(2);
//      //获取表头值
//      var colstr = new Array();
//      //获取表头calss
//      var colclassname=new Array();
//      //获取表头align
//      var colalign=new Array();
//      var tdid=new Array();
//      var noneid=new Array();
//      var nonestr=new Array();
//      var display=new Array();
//     for(var j=0;j<table.rows.item(0).cells.length;j++)
//     {
////         if(table.rows[0].cells[j].style.display!="none")
////        {
//            colstr.push(table.rows[0].cells[j].innerHTML);
//            colclassname.push(table.rows[0].cells[j].className);
//            colalign.push(table.rows[0].cells[j].align);
//            if(table.rows[0].cells[j].id!=""&&table.rows[0].cells[j].id!=undefined)
//            {
//                tdid.push(table.rows[0].cells[j].id);
//            }else
//            {
//                tdid.push("");
//            }
//            display.push(table.rows[0].cells[j].style.display);
////        }else
////        {
////            nonestr.push(table.rows[0].cells[j].innerHTML);
////             if(table.rows[0].cells[j].id!=""&&table.rows[0].cells[j].id!=undefined)
////            {
////                noneid.push(table.rows[0].cells[j].id);
////            }else
////            {
////                noneid.push("");
////            }
////        }
//     }
//     //删除现有表头
//     table.deleteRow(0);
//     //新建行
//     table.insertRow(0);
//     //重新添加表头
//     for(var j=0;j<colstr.length;j++)
//     {
//        table.rows[0].insertCell().innerHTML=colstr[j];
//        table.rows[0].cells[table.rows.item(0).cells.length-1].className=colclassname[j];
//        table.rows[0].cells[table.rows.item(0).cells.length-1].align=colalign[j];
//        if(tdid[j]!="")
//        table.rows[0].cells[table.rows.item(0).cells.length-1].id=tdid[j];
//     }
//     //隐藏列
//      for(var j=0;j<colstr.length;j++)
//     {
//        table.rows[0].cells[j].style.display=display[j];
//     }
//     //添加隐藏列
////       for(var j=0;j<nonestr.length;j++)
////     {
////        table.rows[0].insertCell().innerHTML=nonestr[j];
////        if(noneid[j]!="")
////        table.rows[0].cells[table.rows.item(0).cells.length-1].id=noneid[j];
////     }
//     
////      for(var j=0;j<table.rows.item(0).cells.length;j++)
////      {
////        if(table.rows[0].cells[j].className==""||table.rows[0].cells[j].className==null||table.rows[0].cells[j].className==undefined||table.rows[0].cells[j].className.toString()=="undefined")
////        {
////            table.rows[0].cells[j].style.display="none"
////        }
////      }
//     //重新分配列宽
//     for(var j=0;j<table.rows.item(0).cells.length;j++)
//     {
//        if(table.rows[0].cells[j].style.display!="none")
//        {   
//            //最后一列取剩余宽度
//            if(j==table.rows.item(0).cells.length-1)
//            {
//                table.rows[0].cells[j].width=(total-everywidth*(Cellcount-1))
//            }else
//            {
//                 //平衡列宽，避免最后一列过宽
//                 if(j%2==0)
//                 {
//                    table.rows[0].cells[j].width=parseInt(everywidth)-1;
//                 }else
//                 {
//                    table.rows[0].cells[j].width=parseInt(everywidth)+1;
//                 }
//            }
//            
//        }
//     }
}
//---------------------shjp:2011-9-3 明细扩展项---------------------------


//---------------------shjp:2011-11-24 用户自定义显示功能---------------------------
var jbstr="";
var mxstr="";
var fystr="";
var bzstr="";
var hjstr="";
var ztstr="";
//checkbox
function checkboxischecked()
{
    //清空
    var chk_jiben=document.getElementsByName("chk_jibenxinxi");//基本信息
    for(var i=0;i<chk_jiben.length;i++)
    {
        if(chk_jiben[i].disabled==false)
        {
            chk_jiben[i].checked=false;
        }
        document.getElementById(chk_jiben[i].id+"_txt").value="";
    }
    var chk_jiben=document.getElementsByName("chk_billdetail");//明细信息
    for(var i=0;i<chk_jiben.length;i++)
    {
        if(chk_jiben[i].disabled==false)
        {
            chk_jiben[i].checked=false;
        }
        document.getElementById(chk_jiben[i].id+"_txt").value="";
    }
    //加载
    if(jbstr!="")
        {
            document.getElementById("set_jibenxinxi").checked=true;
            var chks=jbstr.split(',');
            for(var i=0;i<chks.length;i++)
            {
                var id=chks[i].split('|');
                document.getElementById(id[2]).checked=true
                document.getElementById(id[2]+"_txt").value=id[1];
            }
        }
        if(mxstr!="")
        {
            document.getElementById("chk_dindanmingxi").checked=true;
            var chks=mxstr.split(',');
            for(var i=0;i<chks.length;i++)
            {
                var id=chks[i].split('|');
                document.getElementById(id[2]).checked=true
                document.getElementById(id[2]+"_txt").value=id[1];
            }
        }
        if(QY_jibenxinxi==1)
            document.getElementById("chk_qiyong_jibenxinxi").checked=true;
        else
            document.getElementById("chk_qiyong_jibenxinxi").checked=false;
        if(QY_dindanmingxi==1)
            document.getElementById("chk_qiyong_dingdanmingxi").checked=true;
        else
            document.getElementById("chk_qiyong_dingdanmingxi").checked=false;    
        
}
//文本框处理
function Txt_CheckThis(txtid)
{
    if(txtid.value!="")
    {
        if(parseInt(txtid.value).toString()=="NaN")
        {
            txtid.value="";
        }else
        {
             txtid.value=parseInt(txtid.value).toString();
             var id=txtid.id.substring(0,txtid.id.length-4);
             document.getElementById(id).checked=true;
        }
    }
}
var QY_jibenxinxi=0;//是否启用基本信息设置
var QY_dindanmingxi=0;//是否启用明细信息设置
//获取设置信息
function getSetUpload(moduleid)
{
 $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Common/GetBillTableCells.ashx", //目标地址
        cache: true,
        data: 'action=getfield&moduleid=' + moduleid,
        beforeSend: function() { }, //发送数据之前
        success: function(msg) {
        if(parseInt(msg.totalCount)>0)
        {
             $.each(msg.data, function(i, item) {
                if(item.modulartype.toString()=="0")
                {
                    jbstr=item.fieldnum.toString();
                    if(parseInt(item.IsEnable)>0)
                    {
                        QY_jibenxinxi=1;
                    }
                }
                else if(item.modulartype.toString()=="1")
                {
                    mxstr=item.fieldnum.toString();
                    if(parseInt(item.IsEnable)>0)
                    {
                        QY_dindanmingxi=1;
                    }
                }
             }); 
         }
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
        complete: function() {
            
        } //接收数据完毕
    });
 }

//显示页面设置
function PageSetUp_Show(moduleid)
{
    if(jbstr!=""||mxstr!=""||fystr!=""||bzstr!=""||hjstr!=""||ztstr!="")
    {
        checkboxischecked();
    }
    else
    {
        
      $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Common/GetBillTableCells.ashx", //目标地址
        cache: true,
        data: 'action=getfield&moduleid=' + moduleid,
        beforeSend: function() { }, //发送数据之前
        success: function(msg) {
        if(parseInt(msg.totalCount)>0)
        {
             $.each(msg.data, function(i, item) {
                if(item.modulartype.toString()=="0")
                {
                    jbstr=item.fieldnum.toString();
                    if(parseInt(item.IsEnable)>0)
                    {
                        QY_jibenxinxi=1;
                    }
                }
                else if(item.modulartype.toString()=="1")
                {
                    mxstr=item.fieldnum.toString();
                     if(parseInt(item.IsEnable)>0)
                    {
                        QY_dindanmingxi=1;
                    }
                }
             }); 
         }
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
        complete: function() {
        checkboxischecked();
        } //接收数据完毕
    });
    }
}
//页面重载
function GetField(id,moduleid,type)
{
// if(jbstr!=""||mxstr!=""||fystr!=""||bzstr!=""||hjstr!=""||ztstr!="")
// {
//     if(type=="0")
//     {
//         ReSetTable(id,jbstr);
//     }else if(type=="1")
//     {
//        setdetailtable(id,mxstr);
//     }
// 
// }
// else
// {
      $.ajax({
        type: "POST", //用POST方式传输
        dataType: "json", //数据格式:JSON
        url: "../../../Handler/Common/GetBillTableCells.ashx", //目标地址
        cache: true,
        data: 'action=getfield&moduleid=' + moduleid+'&type='+type,
        beforeSend: function() { }, //发送数据之前
        success: function(msg) {
        if(parseInt(msg.totalCount)>0)
        {
             $.each(msg.data, function(i, item) {
                if(item.modulartype.toString()=="0")
                {
                    jbstr=item.fieldnum.toString();
                    if(parseInt(item.IsEnable)>0)
                    {
                        QY_jibenxinxi=1;
                    }
                }
                else if(item.modulartype.toString()=="1")
                {
                    mxstr=item.fieldnum.toString();
                     if(parseInt(item.IsEnable)>0)
                    {
                        QY_dindanmingxi=1;
                    }
                }
             }); 
         }
        },
        error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
        complete: function() {
        if(type==0){if(QY_jibenxinxi==1){ReSetTable(id,jbstr);} }
        else if(type==1){if(QY_dindanmingxi==1){setdetailtable(id,mxstr);}
        pagesettablewidth(id);
        }
        } //接收数据完毕
    });
// }
// 
}


function  ReSetTable(id,str)//主表方法
{
    if(str=="")
    {
        return;
    }
    
    var colstr = new Array();//html
    var cellsname=new Array();//文本
    var cellalign=new Array();//align
    var cellclassname=new Array();//classname
    
   
   
    
    var title=str.split(',');
  
     
    for(var i=0;i<title.length;i++)
    {
        var num=title[i].split('|');
        var min=parseInt(num[1]);
        for(var j=i;j<title.length;j++)
        {
            var num1=title[j].split('|');
            if(parseInt(num1[1])<min)
            {
                min=parseInt(num1[1]);
                var a=title[i];
                title[i]=title[j];
                title[j]=a;
            }
        }
    }
  
    var table=document.getElementById(id);
    for(var i=0;i<table.rows.length;i++)
    {
       
        for(var j=0;j<table.rows.item(i).cells.length;j++)
        {
            colstr.push(table.rows[i].cells[j].innerHTML);
            cellsname.push(table.rows[i].cells[j].innerText);
            cellalign.push(table.rows[i].cells[j].align);
            cellclassname.push(table.rows[i].cells[j].className);
        }
    }
    var len=table.rows.length;
    for(var i=0;i<len;i++)
    {
        table.deleteRow(0);
    }
    var num=0;
    var count=0;
    for(var i=0;i<title.length;i++)
    {
        var name=title[i].split('|');
        
        if(i%3==0)
        {
            table.insertRow(num);
           
            
        }
        for(var j=0;j<cellsname.length;j+=2)
        {
            if(name[0].Trim()==cellsname[j].Trim())
            {
                table.rows[num].insertCell().innerHTML=colstr[j];
                table.rows[num].cells[table.rows.item(num).cells.length-1].className=cellclassname[j];
                table.rows[num].cells[table.rows.item(num).cells.length-1].align=cellalign[j];
                table.rows[num].cells[table.rows.item(num).cells.length-1].width="10%"
                table.rows[num].insertCell().innerHTML=colstr[j+1];
                if(cellclassname[j+1]!="")
                    table.rows[num].cells[table.rows.item(num).cells.length-1].className=cellclassname[j+1];
                else
                     table.rows[num].cells[table.rows.item(num).cells.length-1].className="tdColInput";
                table.rows[num].cells[table.rows.item(num).cells.length-1].align=cellalign[j+1];
                if(count%3==2)
                {
                    table.rows[num].cells[table.rows.item(num).cells.length-1].width="24%"
                }else
                {
                    table.rows[num].cells[table.rows.item(num).cells.length-1].width="23%"
                }
                cellclassname=cellclassname.del(j);
                cellclassname=cellclassname.del(j);
                cellalign=cellalign.del(j);
                cellalign=cellalign.del(j);
                cellsname=cellsname.del(j);
                cellsname=cellsname.del(j);
                colstr=colstr.del(j);
                colstr=colstr.del(j);
                count++;
                break;
            }
        }
        if(count%3==0)
        {
            num++;
        }
        
    }
    
   while(count%3!=0)
   {
        table.rows[num].insertCell().innerHTML="";
        table.rows[num].cells[table.rows.item(num).cells.length-1].className="td_list_fields";
         table.rows[num].cells[table.rows.item(num).cells.length-1].align="right";
        table.rows[num].insertCell().innerHTML="";
        table.rows[num].cells[table.rows.item(num).cells.length-1].className="tdColInput";
         table.rows[num].cells[table.rows.item(num).cells.length-1].align="right";
        count++;
   }
   if(count/3!=num)
   {
        num++;
   }
   //隐藏不显示的列
    for(var j=0;j<cellsname.length;j+=2)
    {
         if(j%3==0)
        {
            table.insertRow(num);
        }
        table.rows[num].style.display="none";
        table.rows[num].insertCell().innerHTML=colstr[j];
        table.rows[num].cells[table.rows.item(num).cells.length-1].className=cellclassname[j];
        table.rows[num].cells[table.rows.item(num).cells.length-1].align=cellalign[j];
        table.rows[num].cells[table.rows.item(num).cells.length-1].width="10%"
        //table.rows[num].cells[table.rows.item(num).cells.length-1].style.display="none";
        table.rows[num].insertCell().innerHTML=colstr[j+1];
        table.rows[num].cells[table.rows.item(num).cells.length-1].className=cellclassname[j+1];
        table.rows[num].cells[table.rows.item(num).cells.length-1].align=cellalign[j+1];
        //table.rows[num].cells[table.rows.item(num).cells.length-1].style.display="none";
        if(count%3==2)
        {
            table.rows[num].cells[table.rows.item(num).cells.length-1].width="24%"
        }else
        {
            table.rows[num].cells[table.rows.item(num).cells.length-1].width="23%"
        }
        count++;
         if(count%3==0)
        {
            num++;
        }
    }
  
}

Array.prototype.del=function(n) {  //n表示第几项，从0开始算起。
//prototype为对象原型，注意这里为对象增加自定义方法的方法。
  if(n<0)  //如果n<0，则不进行任何操作。
    return this;
  else
    return this.slice(0,n).concat(this.slice(n+1,this.length));
    /*
      concat方法：返回一个新数组，这个新数组是由两个或更多数组组合而成的。
      　　　　　　这里就是返回this.slice(0,n)/this.slice(n+1,this.length)
     　　　　　　组成的新数组，这中间，刚好少了第n项。
      slice方法： 返回一个数组的一段，两个参数，分别指定开始和结束的位置。
    */
}

//明细列设置
function setdetailtable(id,str)
{
    if(str=="")
    {
        return;
    }
    var count=0;
    var table=document.getElementById(id);
    var title=str.split(',');
    for(var i=0;i<title.length;i++)
    {
        var num=title[i].split('|');
        var min=parseInt(num[1]);
        for(var j=i;j<title.length;j++)
        {
            var num1=title[j].split('|');
            if(parseInt(num1[1])<min)
            {
                min=parseInt(num1[1]);
                var a=title[i];
                title[i]=title[j];
                title[j]=a;
            }
        }
    }
    var colstr = new Array();//html
    var cellsname=new Array();//文本
    var cellalign=new Array();//align
    var cellclassname=new Array();//classname
    var index =new Array();//索引位置
    var nonestr=new Array();
    var noneid=new Array();
    var noneindex=new Array();
    var tdid=new Array();
    var trid="";//行的id
    //var allstr=$("#all_detail_title").val();
    var allstr=$("#detail_index").val();
    //获取所有明细表头
    if(allstr=="")
    {
        for(var i=0;i<table.rows.length;i++)
        {
            for(var j=0;j<table.rows.item(i).cells.length;j++)
            {
                if(i==0)
                {
                    if(table.rows[i].cells[j].style.display!="none")
                    {
                        colstr.push(table.rows[i].cells[j].innerHTML);
                        cellsname.push(table.rows[i].cells[j].innerText);
                        cellalign.push(table.rows[i].cells[j].align);
                        cellclassname.push(table.rows[i].cells[j].className);
                        if(table.rows[i].cells[j].id!=""&&table.rows[i].cells[j].id!=undefined)
                        {
                            tdid.push(table.rows[i].cells[j].id);
                        }else
                        {
                             tdid.push("");
                        }
                        allstr+=table.rows[i].cells[j].innerText+"|"+j+",";
                    }else
                    {
                        nonestr.push(table.rows[i].cells[j].innerHTML);
                        noneid.push(table.rows[i].cells[j].id);
                    }
                }
            }
        }
        
//        $("#all_detail_title").val(allstr);
//        var detail_title=allstr.split(',');
        //获取索引
        for(var i=0;i<title.length;i++)
        {
            for(var j=0;j<cellsname.length;j++)
            {
                var dstr=title[i].split('|')[0].Trim();
                if(dstr=="选择")
                {
                    if(dstr==cellsname[j].Trim())
                    {
                        index.push(j);
                        break;
                    }
                    dstr="";
                }
                
                if(dstr==cellsname[j].Trim())
                {
                    index.push(j);
                    break;
                }
                
            }
        }
        //获取不显示的列
            for(var j=0;j<colstr.length;j++)
            {
                    var bool=true;
//                     for(var k=0;k<index.length;k++)
//                    {
//                        
//                        if(detail_title[index[k]].split('|')[0].Trim()==detail_title[j].split('|')[0].Trim())
//                        {
//                            bool=false;
//                            break;
//                        }
//                    }
//                    for(var k=0;k<noneindex.length;k++)
//                    {
//                        
//                        if(detail_title[noneindex[k]].split('|')[0].Trim()==detail_title[j].split('|')[0].Trim())
//                        {
//                            bool=false;
//                            break;
//                        }
//                    }
//                   
//                    if(bool)
//                    {
//                        noneindex.push(j);
//                        
//                    }else
//                    {
//                        bool=true;
//                    }
                for(var i=0;i<index.length;i++)
                {
                    if(index[i]==j)
                    {
                        bool=false;
                        break;
                    }
                }
                  if(bool)
                    {
                        noneindex.push(j);
//                        noneid.push(tdid[j]);
//                        nonestr.push(cellsname[j]);
                    }else
                    {
                        bool=true;
                    }
            }
         var indexstr=index.toString();
         $("#detail_index").val(indexstr);
         $("#all_detail_title").val(noneindex.toString());
         //document.getElementById("detail_index").value=indexstr;
        //删除表头
        table.deleteRow(0);
        
        table.insertRow(count);
        count++;
        for(var i=0;i<index.length;i++)
        {
            table.rows[count-1].insertCell().innerHTML=colstr[index[i]];
            table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].className=cellclassname[index[i]];
            table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].align=cellalign[index[i]];
            if(tdid[index[i]]!=""&&tdid[index[i]]!=undefined)
            {
                table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].id=tdid[index[i]];
            }
        }
        
        //加载不显示的列（用户设定不显示）
        for(var i=0;i<noneindex.length;i++)
        {
            
                table.rows[count-1].insertCell().innerHTML=colstr[noneindex[i]];
              
                //table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].style.display="none";
                if(tdid[noneindex[i]]!=""&&tdid[noneindex[i]]!=undefined)
                {
                    table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].id=tdid[noneindex[i]];
                }
            
        }
        //加载不显示的列（页面本身不显示）
         for(var i=0;i<nonestr.length;i++)
        {
            table.rows[count-1].insertCell().innerHTML=nonestr[i];
            //table.rows[count-1].cells[table.rows.item(count-1).cells.length-2].style.display="none";
            if(noneid[i]!=""&&noneid[i]!=undefined)
            table.rows[count-1].cells[table.rows.item(count-1).cells.length-1].id=noneid[i];
        }
        //隐藏不显示的列
        for(var i=0;i<table.rows.length;i++)
        {
            for(var j=0;j<table.rows.item(i).cells.length;j++)
            {
                if(table.rows[i].cells[j].className=="" || table.rows[i].cells[j].className==undefined)
                {
                    table.rows[i].cells[j].style.display="none";
                }
            }
        }
        var a=0;
    
    }else
    {
        if(table.rows.length==1)
        {
            return;
        }
    var rowscount=table.rows.length;
    for(var m=1;m<rowscount;m++)
    {
        //判断是否已重载过
        if(table.rows[m].cells[0].innerText=="OK")
        {
            continue;
        }
        index=$("#detail_index").val().split(',');
        var visible=new Array();
        visible=$("#all_detail_title").val().split(',');
        //清空数组
        colstr.length=0;
        cellalign.length=0;
        cellclassname.length=0;
        var detailnone=new Array();
        var detailid=new Array();
         var nonedetailid=new Array();
         var cellcount=table.rows.item(m).cells.length
          if(table.rows[m].id!=""&&table.rows[m].id!=undefined)
        {
            trid=table.rows[m].id;
        }else
        {
            trid="";
        }
        //获取td数据
         for(var j=0;j<cellcount;j++)
         {
               
                if(table.rows[m].cells[j].style.display!="none")
                {
                    colstr.push(table.rows[m].cells[j].innerHTML);
                    cellalign.push(table.rows[m].cells[j].align);
                    cellclassname.push(table.rows[m].cells[j].className);
                    if(table.rows[m].cells[j].id!=""&&table.rows[m].cells[j].id!=undefined)
                    {
                        detailid.push(table.rows[m].cells[j].id);
                    }else
                    {
                        detailid.push("");
                    }
                }else
                {
                    detailnone.push(table.rows[m].cells[j].innerHTML);
                    if(table.rows[m].cells[j].id!=""&&table.rows[m].cells[j].id!=undefined)
                    {
                        nonedetailid.push(table.rows[m].cells[j].id);
                    }else
                    {
                        nonedetailid.push("");
                    }
                }
           
         }
         //获取隐藏列
         for(var j=0;j<visible.length;j++)
         {
//            var isbool=true;
//            for(var i=0;i<index.length;i++)
//            {
//                if(j==index[i])
//                {
//                    isbool=false;
//                    break;
//                } 
//            }
//            if(isbool)
//            {
                detailnone.push(colstr[visible[j]]);
                nonedetailid.push(detailid[visible[j]]);
//            }
//            else
//            {
//                isbool=true;
//            }
         }
        table.deleteRow(m);//先删除行
        table.insertRow(m);//添加新行
        
        if(trid!="")
            table.rows[m].id=trid;
        //重新加载数据
        for(var i=0;i<index.length;i++)
        {
             if(i==0)
            {
                 table.rows[m].insertCell().innerHTML="OK";
            }
            table.rows[m].insertCell().innerHTML=colstr[index[i]];
            table.rows[m].cells[table.rows.item(m).cells.length-1].className=cellclassname[index[i]];
            table.rows[m].cells[table.rows.item(m).cells.length-1].align=cellalign[index[i]];
            table.rows[m].cells[table.rows.item(m).cells.length-1].id=detailid[index[i]];
        }
        //加载隐藏数据
        for(var i=0;i<detailnone.length;i++)
        {
             table.rows[m].insertCell().innerHTML=detailnone[i];
             table.rows[m].cells[table.rows.item(m).cells.length-1].id=nonedetailid[i];
        }
        //隐藏不用显示的列
        for(var i=0;i<detailnone.length;i++)
        {
             table.rows[m].cells[i+index.length+1].style.display="none";
        }
        table.rows[m].cells[0].style.display="none";//隐藏表示列
        //加载扩展项
        for(var i=index.length+visible.length;i<colstr.length;i++)
        {
            table.rows[m].insertCell().innerHTML=colstr[i];
            if(cellclassname[i]==""||cellclassname[i]==undefined)
            {
                table.rows[m].cells[table.rows.item(m).cells.length-1].className="tdColInputCenter";
            }else
            {
                table.rows[m].cells[table.rows.item(m).cells.length-1].className=cellclassname[i];
            }
            table.rows[m].cells[table.rows.item(m).cells.length-1].align=cellalign[i];
            table.rows[m].cells[table.rows.item(m).cells.length-1].id=detailid[i];
        }
        //重设标题列
       
        for(var i=0;i<table.rows.item(0).cells.length;i++)
        {
              if(table.rows[0].cells[i].className!="")
                {
                    table.rows[0].cells[i].style.display="block";
                }else
                {
                     table.rows[0].cells[i].style.display="none";
                }
              
        }
        for(var l=0;l<table.rows.item(m).cells.length;l++)
        {
            if(table.rows[m].cells[l].style.display!="none")
            {
                if(table.rows[m].cells[l].className==""||table.rows[m].cells[l].className==null||table.rows[m].cells[l].className==undefined)
                {
                    table.rows[m].cells[l].className="cell";
                }
            }
        }
       
       }
      
    }
    
  
}
//加载修改页面
function PageSetUp_alldetail(id)
{
        if($("#detail_index").val()==""||$("#detail_index").val()==null||$("#detail_index").val()==undefined)
        {
            return;
        }
        var table=document.getElementById(id);
        if(table.rows.length==1)
        {
            return;
        }
        var index=$("#detail_index").val().split(',');
        //清空数组
        
         var er_clostr=new Array();
         var er_cellsname=new Array();
         var er_cellalign=new Array();
         var er_cellclassname=new Array();
         var er_detailnone=new Array();
         var er_detailid=new Array();
         var er_nonedetailid=new Array();
        //获取td数据
         for(var i=1;i<table.rows.length;i++)
         {
            var colstr = new Array();//html
            var cellsname=new Array();//文本
            var cellalign=new Array();//align
            var cellclassname=new Array();//classname
            var detailnone=new Array();
            var detailid=new Array();
             var nonedetailid=new Array();
             for(var j=0;j<table.rows.item(i).cells.length;j++)
             {
                  
                    if(table.rows[i].cells[j].style.display!="none")
                    {
                        colstr.push(table.rows[i].cells[j].innerHTML);
                        cellalign.push(table.rows[i].cells[j].align);
                        cellclassname.push(table.rows[i].cells[j].className);
                        if(table.rows[i].cells[j].id!=""&&table.rows[i].cells[j].id!=undefined)
                        {
                            detailid.push(table.rows[i].cells[j].id);
                        }else
                        {
                            detailid.push("");
                        }
                    }else
                    {
                        detailnone.push(table.rows[i].cells[j].innerHTML);
                        if(table.rows[i].cells[j].id!=""&&table.rows[i].cells[j].id!=undefined)
                        {
                            nonedetailid.push(table.rows[i].cells[j].id);
                        }else
                        {
                            nonedetailid.push("");
                        }
                    }
               
             }
           
         
             //获取隐藏列
             for(var j=0;j<colstr.length;j++)
             {
                var isbool=true;
                for(var k=0;k<index.length;k++)
                {
                    if(j==index[k])
                    {
                        isbool=false;
                        break;
                    }
                }
                if(isbool)
                {
                    detailnone.push(colstr[j]);
                    nonedetailid.push(detailid[j]);
                }else
                {
                    isbool=true;
                }
             }
             er_clostr.push(colstr);
             er_cellalign.push(cellalign);
             er_cellclassname.push(cellclassname);
             er_detailid.push(detailid);
             er_detailnone.push(detailnone);
             er_nonedetailid.push(nonedetailid)
          }
         var ce=table.rows.length;
       for(var j=ce-1;j>0;j--)
       {
            table.deleteRow(j);
       }
        for(var j=1;j<er_clostr.length+1;j++)
        {
           
            table.insertRow(j);//添加新行

      
            //重新加载数据
            for(var i=0;i<index.length;i++)
            {
                if(i==0)
                {
                    table.rows[j].insertCell().innerHTML="OK";
                }
                table.rows[j].insertCell().innerHTML=er_clostr[j-1][index[i]];
                table.rows[j].cells[table.rows.item(j).cells.length-1].className=er_cellclassname[j-1][index[i]];
                if(er_cellalign[j-1][index[i]]!=undefined&&er_cellalign[j-1][index[i]]!="")
                table.rows[j].cells[table.rows.item(j).cells.length-1].align=er_cellalign[j-1][index[i]];
                table.rows[j].cells[table.rows.item(j).cells.length-1].id=er_detailid[j-1][index[i]];
            }
            //加载隐藏数据
            for(var i=0;i<detailnone.length;i++)
            {
                 table.rows[j].insertCell().innerHTML=er_detailnone[j-1][i];
                 table.rows[j].cells[table.rows.item(j).cells.length-1].id=er_nonedetailid[j-1][i];
            }
            //隐藏不用显示的列
            for(var i=0;i<detailnone.length;i++)
            {
                 table.rows[j].cells[i+index.length+1].style.display="none";
            }
            table.rows[j].cells[0].style.display="none";//隐藏表示列
             
            //检查样式
            for(var i=index.length+1;i<table.rows.item(j).cells.length;i++)
            {
                if(table.rows[j].cells[i].style.display!="none")
                {
                   if(table.rows[j].cells[i].className==""||table.rows[j].cells[i].className==undefined)
                   {
                        table.rows[j].cells[i].className="tdColInputCenter";
                   }
                }
            }
            //重设标题列
            for(var i=0;i<table.rows.item(0).cells.length;i++)
            {
                  if(table.rows[0].cells[i].className!="")
                    {
                        table.rows[0].cells[i].style.display="block";
                    }else
                    {
                         table.rows[0].cells[i].style.display="none";
                    }
                  
            }
        }
}
function displaymore(id)
{
    if($("#detail_index").val()==""||$("#detail_index").val()==null||$("#detail_index").val()==undefined)
    {
        return;
    }
     index=$("#detail_index").val().split(',');
     var table=document.getElementById(id);
     if(index.length>0)
     {
         for(var i=1;i<table.rows.length;i++){
                 for(var j=0;j<table.rows.item(i).cells.length;j++)
                {
                        if(j>=index.length+RowNum&&table.rows.length-1>0)
                        {
                            table.rows[i].cells[j].style.display="none";
                        }
                }
         }
      }   
}
//打开设置
function showPageSetUp(moduleid)
{
    
    document.getElementById("divPageSetUp").style.display="block";
    PageSetUp_Show(moduleid);
    
}
//关闭设置
function divClosePageSetUp()
{
    document.getElementById("divPageSetUp").style.display="none";
}
//页面设置保存
function Save_PageSetUp(modelid)
{
    var jinbenxinxi=new Array();//基本信息
    var jinbenorder=new Array();//排序
    var feiyongxinxi=new Array();//费用明细
    var hejixinxi=new Array();//合计信息
    var beizhuxinxi=new Array();//备注信息
    var danjuzhuangtai=new Array();//单据状态
    var dindandetail=new Array();//单据明细
    var jiben=document.getElementById("set_jibenxinxi").checked;
    var dindanmingxi=document.getElementById("chk_dindanmingxi").checked;
    var jk="0,0,0,0,0,0";
    var qy="0,0,0,0,0,0";
    var isenable=qy.split(',');
    var isdis=jk.split(',');
    if(document.getElementById("chk_qiyong_jibenxinxi").checked)
    {
        isenable[0]="1";
    }
    if(document.getElementById("chk_qiyong_dingdanmingxi").checked)
    {
        isenable[1]="1";
    }
    if(jiben==true)
    {
        isdis[0]="1";
        var jchk=document.getElementsByName("chk_jibenxinxi");
        for(var i=0;i<jchk.length;i++)
        {
            if(jchk[i].checked==true)
            {
                 jinbenorder.push(document.getElementById(jchk[i].id+"_txt").value);
                 jinbenxinxi.push(jchk[i].value+"|"+document.getElementById(jchk[i].id+"_txt").value+"|"+jchk[i].id);
            }
        }
        if(jinbenorder.length==0)
        {
            alert("请至少选择一项显示列");
            return;
        }
        var err=0;
        for(var i=0;i<jinbenorder.length;i++)
        {
            if(jinbenorder[i]=="")
            {
                err=1;
                break;
            }
            var cou=0;
            for(var j=0;j<jinbenorder.length;j++)
            {
                if(jinbenorder[i]==jinbenorder[j])
                {
                    cou++;
                    
                } 
            }
            if(cou>1)
            {
                err=2;
                break;
            }
        }
        if(err==1)
        {
            alert("请为选中项制定序号");
            return;
        }else if(err==2)
        {
            alert("序号不能重复");
            return;
        }
        
    }
    if(dindanmingxi==true)
    {
        isdis[1]="1";
        jinbenorder.length=0;
        var jchk=document.getElementsByName("chk_billdetail");
        for(var i=0;i<jchk.length;i++)
        {
            if(jchk[i].checked==true)
            {
                 jinbenorder.push(document.getElementById(jchk[i].id+"_txt").value);
                 dindandetail.push(jchk[i].value+"|"+document.getElementById(jchk[i].id+"_txt").value+"|"+jchk[i].id);
            }
        }
        if(jinbenorder.length==0)
        {
            alert("请至少选择一项显示列");
            return;
        }
        var err=0;
        for(var i=0;i<jinbenorder.length;i++)
        {
            if(jinbenorder[i]=="")
            {
                err=1;
                break;
            }
            var cou=0;
            for(var j=0;j<jinbenorder.length;j++)
            {
                if(jinbenorder[i]==jinbenorder[j])
                {
                    cou++;
                    
                } 
            }
            if(cou>1)
            {
                err=2;
                break;
            }
        }
        if(err==1)
        {
            alert("请为选中项制定序号");
            return;
        }else if(err==2)
        {
            alert("序号不能重复");
            return;
        }
    }
     $.ajax({ 
        type: "POST",
        url: "../../../Handler/Common/GetBillTableCells.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:"action=savepagesetup&jinbenxinxi="+escape(jinbenxinxi.toString())+"&dindandetail="+escape(dindandetail.toString())+"&feiyongxinxi="+escape(feiyongxinxi.toString())+"&hejixinxi="+escape(hejixinxi.toString())+"&beizhuxinxi="+escape(beizhuxinxi.toString())+"&danjuzhuangtai="+escape(danjuzhuangtai.toString())+"&isdis="+escape(isdis)+"&moduleid="+escape(modelid)+"&isenable="+escape(isenable),
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                alert(data.info);
            }else
            {
                alert(data.info);
            }
        },
        complete:function(){hidePopup();
        if(confirm("重载页面将丢失当前页面数据，是否继续？"))
        {
            location.reload(true);
        }
        divClosePageSetUp();
        }//接收数据完毕
    });
    
}
//选中复选框，自动编号
function PageSetUp_AddOrderCount(chk)
{
    if(chk.checked)
    {
        var vals=new Array();
        var chks=document.getElementsByName(chk.name);
        for(var i=0;i<chks.length;i++)
        {
            if(document.getElementById(chks[i].id).checked)
            {
                if(parseFloat(document.getElementById(chks[i].id+"_txt").value)!="NaN")
                {
                    vals.push(document.getElementById(chks[i].id+"_txt").value);
                }
            }
        }
        
        if(vals.length>0)
        {
             var max=parseInt(vals[0]);
             if(max.toString()=="NaN")
             {
                max=0;
             }
             for(var i=1;i<vals.length;i++)
             {
                if(parseInt(vals[i]).toString()!="NaN")
                {
                    if(max<parseInt(vals[i]))
                    {
                        max=parseInt(vals[i]);
                    }
                }
             }
            
             document.getElementById(chk.id+"_txt").value=++max;
        }
    }
    else
    {
         document.getElementById(chk.id+"_txt").value="";
    }
}

//清楚设置
function divClearPageSetUp(modelid)
{
      if(!confirm("此操作将清空该页面的所有设置，是否继续？"))
      {
            return;
      }
      $.ajax({ 
        type: "POST",
        url: "../../../Handler/Common/GetBillTableCells.ashx",
        dataType:'json',//返回json格式数据
        cache:false,
        data:"action=clearpagesetup&moduleid="+escape(modelid),
        beforeSend:function()
        {
            AddPop();
        }, 
        error: function()
        {
            showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif","请求发生错误！");
        }, 
        success:function(data) 
        {
            if(data.sta == 1) 
            { 
                alert(data.info);
            }else
            {
                alert(data.info);
            }
        },
        complete:function(){hidePopup();
        if(confirm("重载页面将丢失当前页面数据，是否继续？"))
        {
            location.reload(true);
        }else
        {
            
        }
        divClosePageSetUp();
        }//接收数据完毕
    });
}
//添加明细扩展项到设置页面
function addCustomindiv(BillName,Tid,modelid)
{
    var table=document.getElementById(Tid);
    if(document.getElementById("txtisaddcustom").value!="")
    {
        showPageSetUp(modelid);
        return;
    }
     $.ajax({
            type: "POST", //用POST方式传输
            dataType: "json", //数据格式:JSON
            url: "../../../Handler/Common/GetBillTableCells.ashx", //目标地址
            cache: true,
            data: 'action=all&TableName=' + BillName,
            beforeSend: function() { }, //发送数据之前
            success: function(msg) {
                //获取扩展项数量
              
                if (parseInt(msg.totalCount) > 0) {
//                    if(IsAddTitel)
//                    {
                        $.each(msg.data, function(i, item) {
//                            table.rows[i].cells[j]
                            var null_count=0;
                            for(var j=0;j<5;j++)
                            {
                                if(table.rows[table.rows.length-1].cells[j*2].innerText=="")
                                {   
                                     if(table.rows[table.rows.length-1].cells[j*2].align!="left")
                                     {
                                        table.rows[table.rows.length-1].cells[j*2].align="left";
                                     }
                                      if(table.rows[table.rows.length-1].cells[j*2].className!="tdColInput")
                                     {
                                        table.rows[table.rows.length-1].cells[j*2].className="tdColInput";
                                     }
                                     table.rows[table.rows.length-1].cells[j*2].innerHTML="<input type=\"checkbox\" name=\"chk_billdetail\" onclick=\"PageSetUp_AddOrderCount(this)\" id=\"mx_Custom"+item.EFIndex+"\" value=\""+item.EFDesc+"\" />"+item.EFDesc+"";
                                      if(table.rows[table.rows.length-1].cells[j*2+1].className!="tdColInput")
                                     {
                                        table.rows[table.rows.length-1].cells[j*2+1].className="tdColInput";
                                     }
                                     table.rows[table.rows.length-1].cells[j*2+1].innerHTML=" <input type=\"text\" id=\"mx_Custom"+item.EFIndex+"_txt\" onblur=\"Txt_CheckThis(this)\" size=\"4\" />";
                                     null_count++;
                                     break;
                                }
                            }
                            if(null_count==0)
                            {
                                //添加新行
                                table.insertRow(table.rows.length);
                                for(var j=0;j<5;j++)
                                {
                                    if(j==0)
                                    {    
                                         table.rows[table.rows.length-1].insertCell().innerHTML="<input type=\"checkbox\" name=\"chk_billdetail\" onclick=\"PageSetUp_AddOrderCount(this)\" id=\"mx_Custom"+item.EFIndex+"\" value=\""+item.EFDesc+"\" />"+item.EFDesc+"";
                                         if(table.rows[table.rows.length-1].cells[j*2].align!="left")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2].align="left";
                                         }
                                          if(table.rows[table.rows.length-1].cells[j*2].className!="tdColInput")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2].className="tdColInput";
                                         }
                                         table.rows[table.rows.length-1].insertCell().innerHTML=" <input type=\"text\" id=\"mx_Custom"+item.EFIndex+"_txt\" onblur=\"Txt_CheckThis(this)\" size=\"4\" />";
                                          if(table.rows[table.rows.length-1].cells[j*2+1].className!="tdColInput")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2+1].className="tdColInput";
                                         }
                                        
                                    }
                                    else
                                    {
                                         table.rows[table.rows.length-1].insertCell().innerHTML="";
                                         if(table.rows[table.rows.length-1].cells[j*2].align!="left")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2].align="left";
                                         }
                                          if(table.rows[table.rows.length-1].cells[j*2].className!="tdColInput")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2].className="tdColInput";
                                         }
                                         table.rows[table.rows.length-1].insertCell().innerHTML="";
                                          if(table.rows[table.rows.length-1].cells[j*2+1].className!="tdColInput")
                                         {
                                            table.rows[table.rows.length-1].cells[j*2+1].className="tdColInput";
                                         }
                                        
                                    }
                                }
                            }
                        });
//                     }
                   }
                   },
            error: function() { showPopup("../../../Images/Pic/Close.gif", "../../../Images/Pic/note.gif", "请求发生错误"); },
            complete: function() {document.getElementById("txtisaddcustom").value="1";showPageSetUp(modelid); } //接收数据完毕
        });
}
//设置列宽 
function pagesettablewidth(tableid)
{
    var tw=document.getElementById(tableid);
    var count=0;
    for(var i=0;i<tw.rows[0].cells.length;i++)
    {
        if(tw.rows[0].cells[i].style.display=="none")
        {
            continue;
        }else
        {
            count++;
        }
    }
    if(count>10)
    {
        tw.width=(count*10).toString()+"%";
    }
}
//---------------------shjp:2011-11-24 用户自定义显示功能---------------------------