function showInfo(msg)
{
    showPopup("../../../Images/Pic/Close.gif","../../../Images/Pic/note.gif",msg);
}


function dateAdd(date,val)
{    
    var b=Date.parse(date)+val ;
    b   = new   Date(b)   ;
    return b;
}

function date2str(obj,fmt)
{
    var y = obj.getYear();
    var M = obj.getMonth()+1;
    var d = obj.getDate();
    
    var h = obj.getHours();
    var m = obj.getMinutes();
    var s = obj.getSeconds();
    
    //var f = obj.getMilliseconds();
    
    if(M<10)
        M="0"+M;
    if(d<10)
        d="0"+d;
    if(h<10)
        h="0"+h;
    if(m<10)
        m="0"+m;
    if(s<10)
        s="0"+s;
         
    //fmt:yyyyMMddHHmmss fff
    
    fmt = fmt.replace("yyyy",y);
    fmt = fmt.replace("MM",M);
    fmt = fmt.replace("dd",d);
    fmt = fmt.replace("HH",h);
    fmt = fmt.replace("mm",m);
    fmt = fmt.replace("ss",s);
    
    return fmt;
   
}

function str2date(str)
 {
    if(str + "" == "")
    {
        return null;
    }
 
    // 2009-04-05 13:07:12 999
    if(str.indexOf("-") == -1)
    {
        var y=str.substring(0,4);
        var M=str.substring(4,6);
        var d=str.substring(6,8);
        M--;
        
        if(str.length == 8)
        {
            return new Date(y,M,d);
        }
        
        var h=str.substring(8,10);
        var m=str.substring(10,12);
        var s=str.substring(12,14);
        
        
        //alert(y+"-"+M+"-"+d+"-"+h+"-"+m+"-"+s);
        return new Date(y,M,d,h,m,s);
    }
    
    var secs = str.split(' ');
    var datesec = secs[0];
    
    
    var dates = datesec.split('-');
    dates[1] --;
    
    if(secs.length == 1)
    {
        return new Date(dates[0],dates[1],dates[2]);
    }
    
    var timesec = secs[1];
    
    var times = timesec.split(':');   
     if(times.length == 1)
    {
        return new Date(dates[0],dates[1],dates[2]);
    }
     
    return new Date(dates[0],dates[1],dates[2],times[0],times[1],times[2]);
    
 }
 
 
 

function switchAll(obj)
        {
            var tb = obj.parentNode.parentNode.parentNode.parentNode;
            for(var i=1;i<tb.rows.length;i++)
            {
                var cell = tb.rows[i].cells[0];
                var chk = cell.firstChild;
                chk.checked = obj.checked;
                //return;
            }
        }


function IsValChar(strIn) 
{ 
	return strIn.match(/^[\u4e00-\u9fa5A-Za-z0-9]+$/); 
}


function  IsNumber(strIn) 
{ 
	return strIn.match(/^[0-9]+$/); 
} 

//
function closeMsgBox()
{
    if(window.msgDiv)
    {
        window.msgDiv.style.display = "none";    
    }
}
function MsgBox(msg)
{

showInfo(msg);

return;

    var div = null;
    if(window.msgDiv == null)
    {
        var div = document.createElement("DIV");    
        var csstxt = "display:none;position:absolute;z-index:99;left:300px;top:200px;";
        csstxt += "background-color:#999999;border:solid 1px #000000;";
                
        div.style.cssText = csstxt;
        
        //var h = document.documentElement.offsetHeight;
        //var w = document.documentElement.offsetWidth;
  
        
        var html = "<span>系统提示</span>";
        html += "<img style=\"margin-left:100px;margin-top:3px;cursor:pointer;\" title=\"关闭\" onclick=\"closeMsgBox()\" src=\"../../Images/Pic/Close.gif\">";
        var titlediv = document.createElement("DIV");
        titlediv.style.cssText = "width:180px;padding-bottom:2px;padding-left:4px;border:solid 1px #f1f1f1;background-color:#3366cc;color:#ffffff;";
        titlediv.innerHTML = html;        
        div.appendChild(titlediv);
        
        var contentdiv = document.createElement("DIV");
        contentdiv.style.cssText =  "width:180px;background:#ffffff;padding:3px;text-align:center; padding-top:12px;padding-bottom:20px;";
                
        div.appendChild(contentdiv);        
        div.content = contentdiv;
        
        document.body.appendChild(div);
                
        window.msgDiv = div;
    }else{
        div = window.msgDiv;
    }
    
    div.content.innerHTML = msg;
        
    div.style.display = "";    
    
    div.style.left = document.documentElement.offsetWidth/2-div.offsetWidth/2;
    div.style.top = document.documentElement.offsetHeight/2-div.offsetHeight/2;
        
}




//$(document).ready(function(){
//    MsgBox("hah");
//});



function elePos(et) {
   
    var left=0;
	var top=0;
	while(et.offsetParent){
	left+=et.offsetLeft;
	top+=et.offsetTop;
	et=et.offsetParent;
	}
	left+=et.offsetLeft;
	top+=et.offsetTop;
	return {x:left,y:top}; 
};


function UrlEncode(str)
{ 

//return escape(str);

    var ret=""; 
    var strSpecial="!\"#$%&()*+,/:;<=>?[]^`{|}~%"; var tt="";
    for(var i=0;i<str.length;i++)
    { 
        var chr = str.charAt(i); 
        if( chr.match(/^[\u4e00-\u9fa5]+$/) )
        {            
            ret += escape(chr);
            continue;
        }
        var c=str2asc(chr); 
        //tt += chr+":"+c+"n"; 
        if(parseInt("0x"+c) > 0x7f)
        { 
            //alert(chr+":"+parseInt("0x"+c) );
            //ret+="%"+c.slice(0,2)+"%"+c.slice(-2); 
           
          // ret += chr;            
            ret += encodeURI(chr);
            //ret+="%"+chr.toString(16); 
             //ret+="%"+c; 
        }
        else
        { 
            if(chr==" ") 
                ret+="+"; 
            else if(strSpecial.indexOf(chr)!=-1) 
                ret+="%"+c.toString(16); 
            else 
                ret+=chr; 
        } 
    } 

    return ret; 
} 


function UrlDecode(str){ 
    var ret=""; 
    for(var i=0;i<str.length;i++)
    { 
        var chr = str.charAt(i); 
        if(chr == "+")
        { 
            ret+=" "; 
        }
        else if(chr=="%")
        { 
            var asc = str.substring(i+1,i+3); 
            if(parseInt("0x"+asc)>0x7f)
            { 
                ret+=asc2str(parseInt("0x"+asc+str.substring(i+4,i+6))); 
                i+=5; 
            }
            else
            { 
                ret+=asc2str(parseInt("0x"+asc)); 
                i+=2; 
            } 
        }
        else
        { 
            ret+= chr; 
        } 
    } 
    return ret; 
} 


function str2asc(strstr) 
{
    var code = strstr.charCodeAt(0) ;
    return code.toString(16);
}

function asc2str(ascasc) 
{
    return String.fromCharCode(ascasc) 
}




function showLoading(tbEle)
{
   
    var div = document.createElement("DIV");    
    var csstxt = "width:100%;height:100px;padding-top:30px;text-align:center;";
    csstxt += "background-color:#F4F0ED;border:solid 1px #f1f1f1;";
            
    div.style.cssText = csstxt;
    
    div.innerHTML = "Loading ...";
   
    //
    var cell = tbEle.insertRow(-1).insertCell(-1);
    cell.colSpan = "99";
    cell.appendChild(div);
           
}


function hideLoading(tbEle)
{
    tbEle.deleteRow(-1);
}




function SearchEvent()
{    
    if(document.all)
        return event;

    func=SearchEvent.caller;
    while(func!=null)
    {
        var arg0=func.arguments[0];             
        if(arg0)
        {
            if(arg0.constructor==MouseEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==KeyboardEvent) // 如果就是event 对象
                return arg0;
            if(arg0.constructor==Event) // 如果就是event 对象
                return arg0;
        }
        func=func.caller;
    }
    return null;
}

function GetEventSource(evt)
{        
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
           
    return evt.target||evt.srcElement;
}

function GetEventCode(evt)
{
    if(typeof evt == "undefined" || evt == null)
    { 
        evt = SearchEvent();        
        if(typeof evt == "undefined" || evt == null)
        { 
            return null;
        }
    }
    
    return evt.keyCode || evt.charCode;
}

//eventName,without 'on' for: click
function AddEvent(obj,eventName,handler)
{    
	if(document.all)
	{	    
		obj.attachEvent("on"+eventName,handler);
	}else{	    
		obj.addEventListener(eventName,handler,false);
	}
}


//----------------------------------------------------------------------------

function my$(id)
{
    return document.getElementById(id);
}