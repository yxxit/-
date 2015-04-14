<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyMessage_list.aspx.cs" Inherits="Pages_jthy_PersonCenter_MyMessage_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link rel="stylesheet" type="text/css" href="../../../css/jt_default.css" />
    <link href="../../../css/pagecss.css" type="text/css" rel="Stylesheet" />

    <script src="../../../js/JQuery/jquery_last.js" type="text/javascript"></script>

    <script src="../../../js/Calendar/WdatePicker.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../../js/common/PageBar-1.1.1.js"></script>

    <script src="../../../js/Personal/Common.js" type="text/javascript"></script>

    <script src="../../../js/Personal/MessageBox/SendedInfo.js" type="text/javascript"></script>

    <script src="../../../js/common/check.js" type="text/javascript"></script>

    <script src="../../../js/common/page.js" type="text/javascript"></script>

    <script src="../../../js/common/Common.js" type="text/javascript"></script>
      <!--<script src="../../../js/common/UserOrDeptSelect.js" type="text/javascript"></script>-->
      <script type="text/javascript">
      
      
      

function alertdiv(ControlID)
{
 
      var Array = ControlID.split(",");
      if(Array[0].indexOf("Dept") >= 0)
      {
          if(Array.length==2)
          {
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=1";
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
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=2";
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
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
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
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
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
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
                    var returnValue = window.showModalDialog(url, "", "dialogWidth=350px;dialogHeight=400px;scroll:no;");
                    if(returnValue!="" &&  returnValue!=null  &&  returnValue!="ClearInfo")
                    {
                        var splitInfo=returnValue.split("|");
                        
                        document.all("txtPPerson").value=splitInfo[1].toString();
                        document.all("txtPPersonID").value=splitInfo[0].toString();
//                        document.all("hdDeptID").value=splitInfo[2].toString();
//                        document.all("DeptName").value=splitInfo[3].toString();
                        
                      
                    }
                   else  if(returnValue=="ClearInfo")
                   {
                         document.all("txtPPerson").value="";
                         document.all("txtPPersonID").value="";
                       
                   } 
                }
            }
            //zyy添加-start
            if (Array[0].indexOf("Cashier") >= 0) {
                if (Array.length == 2) {
                    var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
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
                    var url = "../../../Pages/Common/SelectUserOrDept.aspx?ShowType=1&OprtType=3";
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
                    var url = "../../../Pages/Office/MedicineManager/MedicineClassSel.aspx";
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
                   var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1&EmpType=1";
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
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
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
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
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
                    var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=1";
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
             var url="../../../Pages/Common/SelectUserOrDept.aspx?ShowType=2&OprtType=2";
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

      
      
      
      
      
      </script>
      
      

    <style type="text/css">
        #editPanel
        {
            width: 400px;
            background-color: #fefefe;
            position: absolute;
            border: solid 1px #000000;
            padding: 5px;
        }
        .style5
        {
            background-color: #FFFFFF;
            width: 111px;
        }
        .style6
        {
            background-color: #FFFFFF;
            width: 123px;
        }
        .style7
        {
            width: 123px;
        }
    </style>
    <title>已发短信</title>
</head>
<body>
    <input type="hidden" id="isSearched" value="0" />
    <form id="form1" runat="server">
    <a name="pageDataList1Mark"></a><span id="Forms" class="Spantype" name="Forms"></span>
    <table width="98%"  border="0" cellpadding="0" cellspacing="0" class="checktable"
        id="mainindex">
        <tr>
            <td valign="middle" align="center">
                <table width="99%" border="0" border="0" cellpadding="0" cellspacing="0" >

                    <tr class="menutitle1">
                        <td align="left" valign="middle" >
                            &nbsp;&nbsp;检索条件
                        </td>
                        <td align="right" valign="middle" >
                            <div id='searchClick'>
                                <img src="../../../images/Main/Close.jpg" style="cursor: pointer" onclick="oprItem('searchtable','searchClick')" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" id="searchtable" cellspacing="0"
                    bgcolor="#CCCCCC">
                    <tr>
                        <td >
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC"
                                class="table">
                                <tr class="table-item">
                                    
                                   <td class="td_list_fields" >
                                        主题
                                    </td>
                                    <td class="style6">
                                        <input name="txtTitle" id="txtTitle" type="text" class="tdinput" style="width: 180px;" />
                                    </td>
                                    <td class="td_list_fields" >
                                        发送时间
                                    </td>
                                    <td class="style5" colspan="3">
                                        <input onkeydown="return false;" name="createDate" id="createDate1" class="tdinput"
                                            type="text" size="8" onclick="WdatePicker()" />
                                  ~  <input onkeydown="return false;" name="createDate" id="createDate2" class="tdinput"
                                            type="text" size="8" onclick="WdatePicker()" />
                                    </td>
                                    
                                       
                               
                                    
                                    
                                    
                                      <td>短信状态</td>
                                  
                                     <td>
                                       <select id="strau">
                       
                                  <option value="0">未读</option>
                                  <option value="1" >已读</option>
                                    </select>
                                        
                                    </td>
                                   
                              
                                </tr>
              
                                <tr class="table-item">
                                <td  class="td_list_fields" > 接收人</td>
                                  
                                  
                                 <td>
                                 
                            <input id="txtPPersonID" type="hidden" runat="server" /> 
                            <asp:TextBox ID="txtPPerson" runat="server" class="tdinput" style="width: 95%;" onclick="alertdiv('txtPPerson')"></asp:TextBox>
                               </td>
                                 
                                
                                  
                              
                                   <td colspan="4"></td>
                                   
                                    <td colspan="4"></td>
                                   
                                </tr>
                                
                                <tr >
                                    <td colspan="10"  bgcolor="#FFFFFF" align="center">
                                        <img title="检索" src="../../../images/Button/Bottom_btn_search.jpg" style='cursor: pointer;'
                                            onclick='SearchEquipData()' />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="98%" height="57" border="0" cellpadding="0" cellspacing="0" class="maintable"
        id="mainindex">
 
        <tr>
            <td  colspan="2" align="center" valign="top" class="Title">
                <table width="99%" border="0" cellpadding="0" cellspacing="0" >
                    <tr class="menutitle1">
                        <td  align="left" valign="middle" >
                            &nbsp;&nbsp;已发短信
                        </td>
                        <td align="right" valign="middle">
                            <img src="../../../images/Button/Main_btn_delete.png" title="删除" id="btn_del" runat="server"
                                visible="false" onclick="DelMessage()" style='cursor: pointer;' />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" id="pageDataList1"
                     bgcolor="#CCCCCC">
                    <tbody>
                        <tr  class="table-item">
                            <th width="3%" class="td_main_detail">
                                选择
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'SendUserID','oGroup');return false;">
                                    发送人<span id="oGroup" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'CreateDate','oC1');return false;">
                                    发送时间<span id="oC1" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'ReceiveUserID','oC2');return false;">
                                    接收人<span id="oC2" class="orderTip"></span></div>
                            </th>
                            <th  width="10%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'Title','ss');return false;">
                                    主题<span id="ss" class="orderTip"></span></div>
                            </th>
                            <th  width="20%" class="td_main_detail">
                                <div class="orderClick" onclick="OrderBy(this,'Content','oC5');return false;">
                                    内容<span id="oC5" class="orderTip"></span></div>
                            </th>
                            <th width="5%" class="td_main_detail">
                                <div onclick="OrderBy(this,'Status','oC6');return false;">
                                    短信状态<span id="oC6" class="orderTip"></span></div>
                            </th>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="99%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#999999"
                    class="PageList">
                    <tr>
                        <td height="28" background="../../../images/Main/PageList_bg.jpg">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="PageList">
                                <tr>
                                    <td height="28" background="../../../images/Main/PageList_bg.jpg" width="40%">
                                        <div id="PageCount">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="pageDataList1_Pager" class="jPagerBar">
                                        </div>
                                    </td>
                                    <td height="28" align="right">
                                        <div id="divpage">
                                            <input name="text" type="text" id="Text2" style="display: none" />
                                            <span id="pageDataList1_Total" style="display: none"></span>每页显示
                                            <input name="text" type="text" id="ShowPageCount" />
                                            条 转到第
                                            <input name="text" type="text" id="ToPage" />
                                            页
                                            <img src="../../../images/Button/Main_btn_GO.jpg" style='cursor: pointer;' alt="go"
                                                width="36" height="28" align="absmiddle" onclick="ChangePageCountIndex($('#ShowPageCount').val(),$('#ToPage').val());" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
    </form>
    <div id="editPanel" style="display: none;">
        <table id="itemContainer" border="1" width="100%" cellpadding="3" style="border-collapse: collapse;">
            <tr>
                <td style="width: 40px;">
                    主题
                </td>
                <td>
                    <span id="ttTitle"></span>
                </td>
            </tr>
            <tr>
                <td>
                    发送时间
                </td>
                <td>
                    <span id="ttSendDate"></span>
                </td>
            </tr>
            <tr>
                <td>
                    接收人
                </td>
                <td>
                    <span id="ReceiveUserID"></span>
                </td>
            </tr>
            <tr>
                <td>
                    阅读状态
                </td>
                <td>
                    <span id="ttState"></span>
                </td>
            </tr>
            <tr>
                <td>
                    短信内容：
                </td>
                <td align="left" valign="top">
                    <span id="ttContent"></span>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding: 3px;">
                    <a href="#" onclick="hideMsg()">确定</a>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
