<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DelAndSearchUC.ascx.cs" Inherits="UserControl_Common_DelAndSearchUC" %>

<div id="divDelAndSearch"  class="ctrlInput" runat="server"  >

 <input class="tdinput" type="text" id="txtDelSearchUC" runat="server"  style=" float:left ; border:0; width:60%;  " readonly="readonly"  />
 <img src="~/Images/default/search.gif"  style="float:right;  cursor:pointer" id="imgSearchDSUC" runat="server"  />
 <img src="~/Images/default/del.gif"  style="float:right; cursor:pointer "   id="imgDelDSUC"   runat="server" />

</div>
<input id="hidFunction1" type="hidden" runat="server" value="" /> 
<input id="hidFunction2" type="hidden" runat="server"  value=""/>
<input id="hidWidth" type="hidden" runat="server" value="195px" />
<input id="hidsearchAltWord" type="hidden" runat="server"   value="搜索"/>
<input id="hidCreateAltWord" type="hidden" runat="server" value="删除" /> 
  <script language="javascript" type="text/javascript">
  /*引用此控件，例子如下：    控件中属性设置:
    uc1:DelAndSearchUC ID="DelAndSearchUC1" runat="server"    SetSearchFunction="searchFun()"  SetDelFunction="delFun()"  Width="100px" AltAtrributWordBySearchControl="搜索XXX"
    AltAtrributWordByCreateControl="删除XXX"     pagesSetID="'DelAndSearchUC1'"  Value="123"
     
 解释：
 SetSearchFunction  点击搜索图片执行的方法   //showPhoto(this.id)
 SetDelFunction  点击添加图片执行的方法        //其中searchFun();delFun()各自实现
 Width                        控件的宽度（支持px或者%）
 AltAtrributWordBySearchControl 设置搜索按钮获取焦点时显示的文字
 AltAtrributWordByCreateControl 设置删除按钮获取焦点时显示的文字
 pagesSetID                  页面设置的控件id（注意在控件值外围添加 ''，如上面的'code1'--------------------------------------------------------------------------------->此项必须填写
 Value                          初始此控件的值，不写也可以（cs程序获取该控件的值，直接code1.Value）*/
 
  //给搜索按钮设置方法
  var pageID=<%=pagesSetID %>;
 var  function1=pageID+"_hidFunction1";
 var function2=pageID+"_hidFunction2";
  var imSearch=pageID+"_imgSearchDSUC";
  var imAdd=pageID+"_imgDelDSUC";
  var imDivx=pageID+"_divDelAndSearch"; 
  var imWidth=pageID+"_hidWidth"; 
  var imsearchalt=pageID +"_hidsearchAltWord";
  var imAddWord=pageID +"_hidCreateAltWord";
  var imControl=pageID +"_txtDelSearchUC"; 
//  
  var functEvent1=document .getElementById (function1).value;
  if (functEvent1 ==="")
  {
  document.getElementById (imSearch).style.display="none";
  }
 else
 {
  document.getElementById(imSearch).onclick =new Function(  functEvent1);
  }
  //给新建按钮设置方法
 var functEvent2=document .getElementById (function2).value;
   if (functEvent2 ==="")
  {
  document.getElementById (imAdd).style.display="none";
  }
 else
 {
  document.getElementById(imAdd).onclick =new Function(  functEvent2);
}
 //宽度控制**********开始
  var temp=document .getElementById (imWidth).value;
  var sdf=temp .indexOf("px");
  var sdfg=temp .indexOf("%");
  if (sdf!=-1)
  {
    document.getElementById (imControl).style.width=document .getElementById (imWidth).value;
  var temp1=temp.replace("px","");
  
  var sd=   (parseInt(temp1,10) +35 ).toString()+"px";
    document.getElementById (imDivx).style.width=sd;
    
    }
   if (sdfg!=-1)
   {
     document.getElementById (imDivx).style.width=document .getElementById (imWidth).value;
   }
   //宽度控制********结束
   
   //设置搜索按钮获取焦点时显示的文字
     var searAltWords=document .getElementById (imsearchalt).value;
  document.getElementById(imSearch).alt = searAltWords;
   
   //设置新建按钮获取焦点时显示的文字
   var addAltWords=document .getElementById (imAddWord).value;
  document.getElementById(imAdd).alt = addAltWords;
//  }
  </script>
