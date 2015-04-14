
//var UserListItem = {text:"",value:"",groupid:""};
/*
    Tree UI
*/

var treenodes = [];
var curLevel = 0;

function BuildTree(nodes)
{
    treenodes = [];
    
    treeview_selnode = null;
    treeview_selnodeindex = -1;
      
    treeview_selNodes = [];
    curLevel = 0;

    var container = document.getElementById("userList");
    
    container.innerHTML = "";    
         
    var tb = document.createElement("TABLE");
    //tb.border="1";
    tb.cellSpacing="0";
    tb.cellPadding="0";
    
    container.appendChild(tb);
    
    for(var i=0;i<nodes.length;i++)
    {
        nodes[i].pIndex = -1;
        BuildSubNodes(nodes[i],tb);
    }
    
}

function BuildSubNodes(node,tb)
{
    if(curFlag == 0)
    {
        for(var i=0;i<treenodes.length;i++)
        {
            if(treenodes[i].value == node.value)
            {
                return;
            }
        }
    }

    node.index = treenodes.length;
    treenodes.push(node);
    
    
    

    var tr = tb.insertRow(-1);
   
    if(node.SubNodes.length>0)
    {
       tr.insertCell(-1).innerHTML = "<img onclick=\"treeview_expand(this)\" src=\"/images/treeimg/WebResource6.gif\">";
    }else{
       tr.insertCell(-1).innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;";
    }    
    tr.insertCell(-1).innerHTML = "<input id=\"treeview_checkbox"+node.index+"\" type=checkbox onclick=\"treeview_onselnode("+node.index+")\" value=\""+node.value+"\"><a onmouseover=\"treeview_onmove("+node.index+",1)\" onmouseout=\"treeview_onmove("+node.index+",0)\" id=\"treeview_node"+node.index+"\" style=\"color:black;\" href=\"javascript:treeview_onselnode("+node.index+");\">"+node.text+"</a>";
    
    curLevel++;
    if(node.SubNodes.length > 0)
    {
        var subTb = document.createElement("TABLE");
        //subTb.border="1";
        subTb.cellSpacing="0";
        subTb.cellPadding="0";
        subTb.style.display="";
    
        var ttr = tb.insertRow(-1);
        ttr.insertCell(-1);
        ttr.insertCell(-1).appendChild(subTb);
    
        for(var i=0;i<node.SubNodes.length;i++)
        {            
            node.SubNodes[i].pIndex = node.index;
            
            BuildSubNodes(node.SubNodes[i],subTb);
        }
        
    }
    curLevel--;
}


//----------------------
function treeview_expand(obj)
{
    var tr = obj.parentNode.parentNode;
    var ntr = tr.nextSibling.cells[1].firstChild;
    
    if(ntr.style.display == "none")
    {
        ntr.style.display = "";
        obj.src = "/images/treeimg/WebResource6.gif";
    }else{
        ntr.style.display = "none";
        obj.src = "/images/treeimg/WebResource5.gif";
    }
}

var treeview_selnode = null;
var treeview_selnodeindex = -1;
var treeview_selNodes = [];
function treeview_onselnode(idx)
{   
    var obj = document.getElementById("treeview_node"+idx);
    if(treeview_selnode != null)
    {
        treeview_selnode.style.color = "#000000";
    }
        
    obj.style.color = "#ff0000";
    treeview_selnode = obj;
    treeview_selnodeindex = idx;
    
    
    var srcEle = GetEventSource();
    if( srcEle == null)//is A
    {
        obj = obj.previousSibling;           
    }
    
    obj.checked = !obj.checked ;    
    
 
    treeview_addSel(idx);
    
    treeview_allchks(idx);//checkbox 连动
    treeview_allchks2(idx);//checkbox 连动
    
    
    //get seluseridlist
    var userlist = "";
    var useridlist = "";
    for(var i in treeview_selNodes)
    {
        if(treenodes[treeview_selNodes[i]].isUser != true)
            continue;
        
        var tid = treenodes[treeview_selNodes[i]].value;
        
        
        if((","+useridlist+",").indexOf(","+tid+",") != -1)
        {
            continue;
        }
       
       if(userlist != "")
       {
        userlist+= ",";
        useridlist += ",";
       }         
        userlist += treenodes[treeview_selNodes[i]].text;
        useridlist += tid;
    }
    
    
    document.getElementById("txtSender").value = userlist;
    document.getElementById("txtSenderHidden").value = useridlist
                
}


function treeview_addSel(idx)
{
//    //只获取叶子节点
//    if( treenodes[idx].SubNodes.length != 0 )
//        return;
        
    var obj = document.getElementById("treeview_checkbox"+idx);
     if(obj.checked)
    {
        for(var m in treeview_selNodes)
        {
            if(treeview_selNodes[m] == idx)
                break;
        }
        treeview_selNodes.push(idx);
    }else{
        for(var m in treeview_selNodes)
        {
            if(treeview_selNodes[m] == idx)
            {
                var i = m;
                var arr = treeview_selNodes;
                i=parseInt(i);
                for(var j=i;j<arr.length-1;j++)
                {        
                    arr[j] = arr[j+1];
                }
                arr.length--;                       
                break;
            }
        }
    }
}

function treeview_allchks(idx)
{
    var chk = document.getElementById("treeview_checkbox"+idx);
    
    //有子节点
    if(treenodes[idx].SubNodes.length>0)
    {       
            for(var i in treenodes[idx].SubNodes)
            {
                var chk2 = document.getElementById("treeview_checkbox"+treenodes[idx].SubNodes[i].index);
                chk2.checked = chk.checked;
                treeview_addSel(treenodes[idx].SubNodes[i].index);
                
                treeview_allchks(treenodes[idx].SubNodes[i].index);
                
            }       
   } 

}

function treeview_allchks2(idx)  
{
    var chk = document.getElementById("treeview_checkbox"+idx);
    
    //有父节点
    if(treenodes[idx].pIndex != -1)
    {
        if(!chk.checked )
        {
            var selCountOfPNode = 0;
            for(var i in treenodes[treenodes[idx].pIndex].SubNodes)
            {
                var tchildIndex = treenodes[treenodes[idx].pIndex].SubNodes[i].index;
                if(document.getElementById("treeview_checkbox"+tchildIndex).checked)
                {
                    selCountOfPNode++;
                }
            }
            
            if(selCountOfPNode > 0)
                return;
        }
    
        var chk2 = document.getElementById("treeview_checkbox"+treenodes[idx].pIndex);
        chk2.checked = chk.checked;
         treeview_addSel(treenodes[idx].pIndex);
         
        treeview_allchks2(treenodes[idx].pIndex);
        return;
    }
}

function treeview_onmove(idx,flag)
{
    if(treeview_selnodeindex == idx)
        return;
        
    var obj = document.getElementById("treeview_node"+idx);
    if(flag == 1)
    {
        obj.style.color = "#ff0000";
    }else{
        obj.style.color = "#000000";
    }
}


function treeview_unsel()
{
    if(treeview_selnodeindex != -1)
    {
         var obj = document.getElementById("treeview_node"+treeview_selnodeindex);
        obj.style.color = "#000000";   
    }

   for(var i=0;i<treeview_selNodes.length;i++)
   {
        var chk2 = document.getElementById("treeview_checkbox"+treeview_selNodes[i]);
        chk2.checked = false;
   }
   treeview_selNodes.length = 0;
   
  treeview_selnode = null;
  treeview_selnodeindex = -1;
  try{
    closeRotoscopingDiv(false,'divPBackShadow');
   }catch(e){}
}

var selalled = false;
function treeview_selall()
{
    if(selalled)
    {
        treeview_unsel();
        selalled = false;
        return;
    }

    treeview_selNodes = [];
    for(var i=0;i<treenodes.length;i++)
    {
        treeview_selNodes.push(i);        
        document.getElementById("treeview_checkbox"+i).checked = true;
    }
    
    getsellist();
    
    selalled = true;
}