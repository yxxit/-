
function CloseHidenDiv(){
		var deletediv =  document.getElementById("div_ZCDiv");
		var Mydiv = document.getElementById("DivSel");
		Mydiv.style.display="none";
		deletediv.style.display="none";
	}

    

function getsubcompany(ControlID)	
{
          var Array = ControlID.split(",");
          if(Array.length==2)
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=1&Subflag=sub";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
             {
             var splitInfo=returnValue.split("|");
              window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =splitInfo[0].toString();
              window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =splitInfo[1].toString();
             }
          else if(returnValue=="ClearInfo")
          {
             window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
             window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=""; 
          } 
           }
            else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&Subflag=sub";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             if(window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
   
                       window.parent.window.frames["salaryPage"][0].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else 
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                   if(Tempvalue.length>0)
                    {
                      window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                   window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value+=  window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value =ID;  
             } 
          }
          else if(returnValue=="ClearInfo")
          {
              window.parent.window.frames["salaryPage"].document.getElementById(Array[0]).value="";
              window.parent.window.frames["salaryPage"].document.getElementById(Array[1]).value=""; 
          } 
        }   
        
}

function SelectUserOrDept(ControlID)
{
      var Array = ControlID.split(",");
      if(Array[0].indexOf("Dept") >= 0)
      {
          if(Array.length==3)
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
             {
             var splitInfo=returnValue.split("|");
              window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =splitInfo[0].toString();
              window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =splitInfo[1].toString();
             }
          else if(returnValue=="ClearInfo")
          {
             window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value="";
             window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value=""; 
          } 
           }
          else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
           //  window.parent.document.getElementById(Array[1]).value =ID;
             if(window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
   
                       window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else 
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                   if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
               // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
             }
             else
             {
                   window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value!="")
             {
                  // window.parent.window.frames["Main"].document.getElementById(Array[1]).value+=  window.parent.window.frames["Main"].document.getElementById(Array[1]).value =","+ID;
                var Oldvalue=window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =ID;  
             } 
          }
          else if(returnValue=="ClearInfo")
          {
              window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value="";
              window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value=""; 
          } 
        }   
      }
        if(Array[0].indexOf("User") >= 0) 
         {
                if(Array.length==2)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                        
                       
                         window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value=splitInfo[0].toString();
                         window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value=splitInfo[1].toString();
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value="";
                         window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
          else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             //window.parent.document.getElementById(Array[1]).value =ID; 
             if(window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {                  
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else
                {
                     
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value =Name;   
             }
             if(window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value+=  window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
            {
                 window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value =ID;   
             }
           }
           else if(returnValue=="ClearInfo")
           {
               window.parent.window.frames[Array[2]].document.getElementById(Array[0]).value="";
               window.parent.window.frames[Array[2]].document.getElementById(Array[1]).value=""; 
           } 
         }        
       }
         
}



function alertdiv(ControlID) {
      var Array = ControlID.split(",");
      if(Array[0].indexOf("Dept") >= 0)
      {
          if(Array.length==2)
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
             {
             var splitInfo=returnValue.split("|");
//              window.parent.window.frames["Main"].document.getElementById(Array[1]).value =splitInfo[0].toString();
//              window.parent.window.frames["Main"].document.getElementById(Array[0]).value =splitInfo[1].toString();
               document.getElementById(Array[1]).value =splitInfo[0].toString();
             document.getElementById(Array[0]).value =splitInfo[1].toString();  
             }
          else if(returnValue=="ClearInfo")
          {
             document.getElementById(Array[0]).value="";
             document.getElementById(Array[1]).value=""; 
          } 
           }
          else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
           //  window.parent.document.getElementById(Array[1]).value =ID;
             if(document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
   
                       document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else 
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                   if(Tempvalue.length>0)
                    {
                      document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
               // window.parent.document.getElementById(Array[0]).value+=window.parent.document.getElementById(Array[0]).value =","+Name;
             }
             else
             {
                   document.getElementById(Array[0]).value =Name;   
             }
             if(document.getElementById(Array[1]).value!="")
             {
                  // window.parent.window.frames["Main"].document.getElementById(Array[1]).value+=  window.parent.window.frames["Main"].document.getElementById(Array[1]).value =","+ID;
                var Oldvalue=document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 document.getElementById(Array[1]).value =ID;  
             } 
          }
          else if(returnValue=="ClearInfo")
          {
              document.getElementById(Array[0]).value="";
              document.getElementById(Array[1]).value=""; 
          } 
        }   
      }
      //shjp添加
        if(Array[0].indexOf("TD_Text_") >= 0) 
         {
             if(Array.length==2)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                         document.getElementById(Array[1]).value=splitInfo[0].toString();
                         document.getElementById(Array[0]).value=splitInfo[1].toString();
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.getElementById(Array[1]).value="";
                         document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
            }
            
         //bsd add-提交审批时选择接收人 2014-04-01
         if(Array[0].indexOf("txtNextPersonName") >= 0) 
         {
          
             if(Array.length==2)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                       // window.opener.document.getElementById("txtNextPersonName").value="";
                       // window.parent.document.getElementById("txtNextPersonName").value="";
                      //  document.getElementById("txtNextPersonName").value=splitInfo[1].toString();
                        document.all("FlowApply1_txtNextPersonName").value=splitInfo[1].toString();
                        document.all("FlowApply1_txtNextPersonID").value=splitInfo[0].toString();
                        
                      
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.all("FlowApply1_txtNextPersonName").value="";
                         document.all("FlowApply1_txtNextPersonID").value="";
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
            }
         if(Array[0].indexOf("txtPPerson") >= 0) 
         {
        
             if(Array.length>0)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                        
                        document.all("txtPPerson").value=splitInfo[1].toString();
                        document.all("txtPPersonID").value=splitInfo[0].toString();
                        $("#hdDeptID").val(splitInfo[2].toString());
                        $("#DeptName").val(splitInfo[3].toString());
                        
                      
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.all("txtPPerson").value="";
                         document.all("txtPPersonID").value="";
                       
                   } 
                }
            }
            if(Array[0].indexOf("txtOprName") >= 0)//2014-04-12 Bsd Add
            { 
                 var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                 var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                 if(returnValue!="" &&  returnValue!=null && returnValue!="ClearInfo")
                 {
                     var splitInfo=returnValue.split("|");
                     document.all("txtOprName").value=splitInfo[1].toString();
                     document.all("txtOprID").value=splitInfo[0].toString();
                 }
            }
            //zyy添加-start
            if (Array[0].indexOf("Cashier") >= 0) {
                if (Array.length == 2) {
                    var url = "../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                        var splitInfo = returnValue.split("|");
                        document.getElementById(Array[1]).value = splitInfo[0].toString();
                        document.getElementById(Array[0]).value = splitInfo[1].toString();
                        try {
                            document.getElementById("txtDeptName").value = splitInfo[3].toString();
                            document.getElementById("hidDeptID").value = splitInfo[2].toString();
                        }
                        catch (Error) {
                        }
                    }
                    else if (returnValue == "ClearInfo") {
                        document.getElementById(Array[1]).value = "";
                        document.getElementById(Array[0]).value = "";
                        try {
                            document.getElementById("txtDeptName").value = "";
                            document.getElementById("hidDeptID").value = "";
                        }
                        catch (Error) {
                        }
                    }
                }
            }
            
            if (Array[0].indexOf("Compuny") >= 0) {
                if (Array.length == 2) {
                    var url = "../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=3";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                        var splitInfo = returnValue.split("|");
                        document.getElementById(Array[1]).value = splitInfo[0].toString();
                        document.getElementById(Array[0]).value = splitInfo[1].toString();
                    }
                    else if (returnValue == "ClearInfo") {
                        document.getElementById(Array[1]).value = "";
                        document.getElementById(Array[0]).value = "";
                    }
                }
            }
             if (Array[0].indexOf("Class") >= 0) {
                if (Array.length == 2) {
                    var url = "../Pages/Office/MedicineManager/MedicineClassSel.aspx";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                        var splitInfo = returnValue.split("|");
                        document.getElementById(Array[1]).value = splitInfo[0].toString();
                        document.getElementById(Array[0]).value = splitInfo[1].toString();
                    }
                    else if (returnValue == "ClearInfo") {
                        document.getElementById(Array[1]).value = "";
                        document.getElementById(Array[0]).value = "";
                    }
                }
            }
              if (Array[0].indexOf("Employee") >= 0) {
                if (Array.length == 2) 
                {
                   var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1&EmpType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if (returnValue != "" && returnValue != null && returnValue != "ClearInfo") {
                        var splitInfo = returnValue.split("|");
                        document.getElementById(Array[1]).value = splitInfo[0].toString();
                        document.getElementById(Array[0]).value = splitInfo[1].toString();
                    }
                    else if (returnValue == "ClearInfo") {
                        document.getElementById(Array[1]).value = "";
                        document.getElementById(Array[0]).value = "";
                    }
                }
            }
            
            //zyy添加--end
        if(Array[0].indexOf("User") >= 0) 
         {
          
                if(Array.length==2)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");

//                         document.getElementById(Array[1]).value=splitInfo[0].toString();
//                         document.getElementById(Array[0]).value=splitInfo[1].toString();
                         document.getElementById(Array[1]).value=splitInfo[0].toString();
                         document.getElementById(Array[0]).value = splitInfo[1].toString();$("#cashier").val(splitInfo[1].toString());//当选择业务员时，收款员也自动被选择了，值与业务员一样
                         try
                         {
                           document.getElementById("DeptId").value=splitInfo[3].toString();
                            document.getElementById("SellDeptId").value=splitInfo[2].toString();
                         }
                         catch (Error)
                         {
                         }
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.getElementById(Array[1]).value="";
                         document.getElementById(Array[0]).value = "";
                         try{
                         document.getElementById("DeptId").value = "";
                         document.getElementById("SellDeptId").value =""
                     }
                         catch(Error)
                         {}
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
          else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             //window.parent.document.getElementById(Array[1]).value =ID; 
             if(document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {                  
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else
                {
                     
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 document.getElementById(Array[0]).value =Name;   
             }
             if(document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 document.getElementById(Array[1]).value =ID;   
             }
           }
           else if(returnValue=="ClearInfo")
           {
               document.getElementById(Array[0]).value="";
               document.getElementById(Array[1]).value=""; 
           } 
         }        
       }
       
        if(Array[0].indexOf("cashier") >= 0) 
         {
          
                if(Array.length==2)
                {
                    var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");

//                         document.getElementById(Array[1]).value=splitInfo[0].toString();
//                         document.getElementById(Array[0]).value=splitInfo[1].toString();
                         document.getElementById(Array[1]).value=splitInfo[0].toString();
                         document.getElementById(Array[0]).value = splitInfo[1].toString();$("#cashier").val(splitInfo[1].toString());//当选择业务员时，收款员也自动被选择了，值与业务员一样
                         try
                         {
                           document.getElementById("DeptId").value=splitInfo[3].toString();
                            document.getElementById("SellDeptId").value=splitInfo[2].toString();
                         }
                         catch (Error)
                         {
                         }
                       // window.parent.document.getElementById(Array[1]).value =
                        //window.parent.document.getElementById(Array[0]).value =splitInfo[1].toString();
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.getElementById(Array[1]).value="";
                         document.getElementById(Array[0]).value = "";
                         try{
                         document.getElementById("DeptId").value = "";
                         document.getElementById("SellDeptId").value =""
                     }
                         catch(Error)
                         {}
                       //window.parent.document.getElementById(Array[0]).value="";
                       //window.parent.document.getElementById(Array[1]).value=""; 
                   } 
                }
          else
          {
             var url="../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
             var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
             if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
             {
                 var ID="";
                 var Name="";
                 var getinfo = returnValue.split(",");
                  for(var i=0; i < getinfo.length; i++)
                  {
                      var c = getinfo[i].toString();
                      ID+=c.substring(0,c.indexOf("|"))+",";
                      Name+=c.substring(c.indexOf("|")+1,c.length)+",";
                  }
              ID = ID.substring(0, ID.length-1);
              Name=Name.substring(0,Name.length-1);
             //window.parent.document.getElementById(Array[1]).value =ID; 
             if(document.getElementById(Array[0]).value!="")
             {
                var Oldvalue=document.getElementById(Array[0]).value;
                var Newvalue=Name;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {                  
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
                else
                {
                     
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      document.getElementById(Array[0]).value+=  document.getElementById(Array[0]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 document.getElementById(Array[0]).value =Name;   
             }
             if(document.getElementById(Array[1]).value!="")
             {
                var Oldvalue=document.getElementById(Array[1]).value;
                var Newvalue=ID;
                var Tempvalue="";
                if(Newvalue.indexOf(Oldvalue)>=0)
                {
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+",";
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                       document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
                else
                {
                    Oldvalue=Oldvalue.replace(/,/g,"");
                    var Splitinfo=Newvalue.split(',');
                    for(var i=0;i<Splitinfo.length;i++)
                    {
                        if(Oldvalue.indexOf(Splitinfo[i].toString())<0)
                        {                          
                           Tempvalue+=Splitinfo[i].toString()+","; 
                        }
                    }
                    Tempvalue=Tempvalue.substring(0,Tempvalue.length-1);
                    if(Tempvalue.length>0)
                    {
                      document.getElementById(Array[1]).value+=  document.getElementById(Array[1]).value =","+Tempvalue;
                    }
                }
             }
             else
             {
                 document.getElementById(Array[1]).value =ID;   
             }
           }
           else if(returnValue=="ClearInfo")
           {
               document.getElementById(Array[0]).value="";
               document.getElementById(Array[1]).value=""; 
           } 
         }        
       }
         
}

//输出遮罩层
function alertHidenDiv()
{
  /**第一步：创建DIV遮罩层。*/
	var sWidth,sHeight;
	sWidth = window.screen.availWidth;
	if(window.screen.availHeight > document.body.scrollHeight){  //当高度少于一屏
		sHeight = window.screen.availHeight;  
	}else{//当高度大于一屏
		sHeight = document.body.scrollHeight;   
	}
   var str="";

  str="<div id='div_ZCDiv'  style='position:absolute;top:0;left:0;background:#777;filter:Alpha(opacity=30);opacity:0.3;height:"+sHeight+";width:"+sWidth+";zIndex:900;' >";
  str+="<iframe id='div_ZCDiv_aaaa' style='position: absolute; z-index:-1; width:"+sWidth+"; height:"+sHeight+";' frameborder='0'>  </iframe>";
//  str+="<div  style='position:absolute; top:0; left:0; width:"+sWidth+"; height:"+sHeight+"; background:#FDF3D9; border:1px solid #EEAC53'></div>";
//str+="<iframe src='javascript:false' style='Z-INDEX:-1; FILTER:progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0); LEFT:0px; VISIBILITY:inherit; WIDTH:"+sWidth+"; POSITION:absolute; TOP:0px; HEIGHT:"+sHeight+"'>";
  str+="</div>";
  insertHtml("afterBegin",document.body,str);
}






//--关闭div
function hide()
{
    document.getElementById( "DivSel").style.display = "none";
}