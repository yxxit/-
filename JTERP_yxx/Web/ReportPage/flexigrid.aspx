<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flexigrid.aspx.cs" Inherits="ReportPage_flexigrid" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>flexigrid测试</title>    
        <script type="text/javascript" src="js/prototype.js"></script>
		<script type="text/javascript" src="js/jquery-1.11.1.min.js"></script>
		<link rel="stylesheet" type="text/css" href="css/flexigrid.css" media="all" />
		<script type="text/javascript" src="js/flexigrid.js"></script>
        <script src="js/CheckActivX.js" type="text/javascript"></script>
        <script src="index.js" type="text/javascript"></script>
        <object id="LODOP" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width=0 height=0> 
</object> 
        <style type="text/css">
           .opea{
	            text-decoration:none;
	            color:#06F;
	        }
        </style>        
</head>
<body>
     <form id="sform" runat="server">
     <a href="javascript:PrintMytable();">预览打印</a>
     <div id="tiaojian">
        客户编号<input id="cCusCode" type="text" /> &nbsp;客户名称<input id="cCusName" type="text" />
     </div>
         <input type="button"  id="btnLoad" runat="server" value="检索" />
        <input type="hidden" value="2015-01-15" id="today" />
        <table id="mytable">  

        </table>
     </form>

     
     <script language="javascript" type="text/javascript">
         function PrintMytable1() {//已经集成在index.js
             LODOP.PRINT_INIT("打印插件功能演示_Lodop功能_打印表格");
             LODOP.ADD_PRINT_TABLE(100, 20, 1000, 800, $("#mytable")[0].outerHTML);
             LODOP.PREVIEW();
         };		
</script>
</body>
</html>
