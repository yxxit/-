
function URL(input_url)
 {
  this.url = input_url?input_url:document.location.href;
  this.scriptName = "";
  
  this.params = new Array();
  this.makeParams = function(){
    var tmp_url = this.url;
    var spIdx = tmp_url.indexOf("?");
    if( spIdx == -1){
     this.scriptName = tmp_url;
     return ;
    }
    this.scriptName = tmp_url.substring(0,spIdx-1);
    tmp_url=tmp_url.substring(spIdx+1);
    
    this.params = tmp_url.split("&");
        
   }
  this.makeParams();
 
  this.buildURL = function (){
    var tmp_url = this.scriptName;
    
    tmp_url += "?";
    
    for(var k=0;k<this.params.length;k++)
      tmp_url += this.params[k] +"&";
         
    tmp_url = tmp_url.substring(0,tmp_url.length-1);
    
    this.url = tmp_url;
     
   }
   
  this.addParam = function (key,value){
    for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      this.editParam(key,value);      
      return;
     }
    }
    this.params[this.params.length] = key+"="+value;
    this.buildURL();
   }
   
  this.delParam = function (key){
    var keyIdx=-1;
    for(var i=0;i<this.params.length;i++)
     if(this.params[i].split("=")[0] == key)
      {
       keyIdx = i;
       break;
      }
    if (keyIdx == -1)
     return;
     
    for(var j=keyIdx+1;j<this.params.length;j++)
     this.params[j-1] = this.params[j];
    
    this.params.length--;
    
    this.buildURL();
   }
   
  this.editParam = function(key,keyV){
    for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      this.params[i] = key+"="+keyV;
      this.buildURL();
      return;
      }
    }
    
    this.addParam(key,keyV);
    
   }
   
   
  this.keys = function(key){
   for(var i=0;i<this.params.length;i++){
     if(this.params[i].split("=")[0] == key){
      var tmpObj = new Object();
      tmpObj.key = key;
      tmpObj.value = this.params[i].split("=")[1];
      return tmpObj;
     }
   }
   
   return {key:key,value:""};
  }  
 
   
 }
 
 
//  
// var url=new URL();    //生成 一个URL实例

// url.addParam("X","1");   //添加一个URL参数

// url.addParam("Y","2");   //添加一个URL参数

// url.addParam("Z","3");   //添加一个URL参数


// url.delParam("Y");    //删除一个URL参数
// url.editParam("Z","4")   //修改一个URL参数
// 
// alert(url.keys("X").value)  //查找一个URL参数
//  
// alert(url.url);     //获取完整的URL

