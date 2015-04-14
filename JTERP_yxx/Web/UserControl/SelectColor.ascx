<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectColor.ascx.cs" Inherits="UserControl_selectColor" %>
<div id="showColorSelect" style="border: solid 5px #93BCDD; background: #fff; padding: 3px;
    width: 120px; z-index:3000; position: absolute;  top: 40%; left: 80%;
    margin: 5px 0 0 -400px;height: 190px; display:none;">
     <div style="overflow-y: scroll; height: 170px; text-align: left;">
     <input type="hidden" value="" id="hideRowID"/>
     <table width="100%" cellpadding="0" cellspacing="0"  border="1" bordercolor="#999999" style="border-collapse: collapse; background-color:#dfebf8;text-align:center;">
     <thead>
       <tr>
         <th style="width:30%">选择</th><th style="width:70%">颜色</th>
       </tr>
     </thead>
     </table>

     <table id="colorBody" width="100%" cellpadding="0" cellspacing="0"  border="1" bordercolor="#999999" style="border-collapse: collapse; background-color:#fffff;text-align:center;">       
          <asp:Literal ID="Lit_color" runat="server"></asp:Literal>
     </table>

</div>
<div style="text-align:center;"><img  src="../../../Images/Button/Bottom_btn_close.jpg" id="hideColorForm" onclick="closeColorForm()"/></div>
</div>