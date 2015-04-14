<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JtPurContractTrack.aspx.cs" Inherits="ReportPage_JtPurContractTrack" %>
<%@ Register src="../UserControl/CustOrProvider_Per.ascx" tagname="CustOrProvider_Per" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>采购合同执行表</title>
    <link href="css/grid.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript" src="js/prototype.js"></script>  <%-- 这个必须添加 js Prototype JavaScript framework--%>
    <script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>   
    <script src="../js/Calendar/WdatePicker.js" type="text/javascript"></script>
    <script src="../js/common/UserOrDeptSelect_new.js" type="text/javascript"></script>
      
	<link rel="stylesheet" type="text/css" href="css/flexigrid.css" media="all" />
	<script type="text/javascript" src="js/flexigrid.js"></script>
    <script src="JtPurContractTrack.js" type="text/javascript"></script>
    <script src="js/CheckActivX.js" type="text/javascript"></script>    
    <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=0 height=0> </object>
</head>
<body>
<uc3:CustOrProvider_Per ID="CustOrProvider_Per" runat="server" />
    <form id="sform" runat="server">
    <input  type="hidden" runat="server"  ID="txtCompanyCD" value="jthy4" />
       <table id="headerTable">
          <tr>
            <td class="td_right">供应商名称：</td>
            <td class="td_input"><input type="text" runat="server"  ID="txtCustName" /></td>
            <td class="td_right">合同编号：</td>
            <td  class="td_input"><input type="text" runat="server"  ID="txtContractid" /></td>
            <td style="display:none;" class="td_right">所属部门：</td>
            <td style="display:none;"  class="td_input"><input id="hdDeptID" type="hidden" runat="server" /><!-- hdDeptID 是部门id ,txtDept是部门名称--->
              <input type="text" runat="server" onclick="alertdiv('txtDept,hdDeptID')" ID="txtDept" /></td>
            <td class="td_right">时间:</td>
            <td  class="td_input"><input type="text" ID="txtStartTime" runat="server"   onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtStartTime')})"/>
            &nbsp;&nbsp;至&nbsp;&nbsp;
                <input type="text" ID="txtEndTime" runat="server"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',el:$dp.$('txtEndTime')})"/>
            </td>
          </tr>
          <tr>
            <td colspan="8" align="center"><input type="button" runat="server" ID="btnLoad" value="检索" />
                <input type="button" runat="server" onclick="javascript:PrintMytable();" ID="btnPrintAll" value="打印整张表" />
            </td>
          </tr>
        </table>        
    <table id="Report">  

    </table>
    </form>
</body>
</html>
