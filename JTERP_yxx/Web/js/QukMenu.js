 function qukmenu(str)
{
    var companyCD = str.split(',')[0];
    var UserID=str.split(',')[1];
    var ModuleID=str.split(',')[2];
    
    
    $.ajax({ 
              type: "POST",
             url: "../Handler/QukMenu.ashx?companyCD="+companyCD+"&UserID="+UserID+"&ModuleID="+ModuleID,
                //url: "../../Web/Handler/QukMenu.ashx",
                //data: '&companyCD='+companyCD+'&UserID='+UserID+'&ModuleID='+ModuleID,           
//              dataType:'json',//返回json格式数据
              cache:false,
              beforeSend:function()
              { 
              
                  //AddPop();
              }, 
            //complete :function(){hidePopup();},
            error: function() 
            {
                alert("添加快捷菜单失败！");
//             showPopup("../Images/Pic/Close.gif","../../../Images/Pic/note.gif","添加快捷菜单失败！");
           }, 
            success:function(data) 
            { 
            
                //alert("添加快捷菜单成功！");
//                showPopup("../Images/Pic/Close.gif","../../../Images/Pic/note.gif","添加快捷菜单成功！");
                parent.frames.Top.document.location.reload();

               

            } 
            });
            
     
      }
    

      

            
          

