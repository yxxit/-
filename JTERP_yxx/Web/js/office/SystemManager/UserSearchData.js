function GetModuleInfo(ModuleID)
{
    document.getElementById("hidModuleID").value=ModuleID;  
   var aa=document.getElementById("DrpUserName").value;
   alert(aa);
    
//    //执行查询数据
//    $.ajax({ 
//        type: "POST",
//        url: "../Handler/ModuleInfo.ashx?Action=List&ModuleID=" + ModuleID,
//        dataType:'string',//
//        cache:false,
//        beforeSend:function()
//        {
//        }, 
//        error: function()
//        {
//           alert("请求发生错误！");
//        }, 
//        success:function(data) 
//        {

//        
//         var result = null;
//         eval("result = "+data);
//         if (result.data[0].ID!="")
//         {
//         document.getElementById("hidFlag").value="Update";           
//         var oEditor = FCKeditorAPI.GetInstance("FCKeditor1");
//         oEditor.SetHTML(result.data[0].Description) ;
//         }
//         else
//         {
//         document.getElementById("hidFlag").value="Insert";   
//          var oEditor = FCKeditorAPI.GetInstance("FCKeditor1");
//         oEditor.SetHTML("") ;
//         }     
//        
//        },
//    });  
}
