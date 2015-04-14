var addi = 0;



//日期校验
function isdate(str,tip){

var strSeparator = "-"; //日期分隔符
var strDateArray;
var intYear;
var intMonth;
var intDay;
var boolLeapYear;
var strDate;
strDate=str;
if(strDate==""){
return "true";
}
strDateArray = strDate.split(strSeparator); //以“-”为分隔符提取年月日
if(strDateArray.length!=3) {
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";
}
intYear = parseInt(strDateArray[0],10);
intMonth = parseInt(strDateArray[1],10);
intDay = parseInt(strDateArray[2],10);
if(isNaN(intYear)||isNaN(intMonth)||isNaN(intDay)) {
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";}
if(intMonth>12||intMonth<1){
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";}
if((intMonth==1||intMonth==3||intMonth==5||intMonth==7||intMonth==8||intMonth==10||intMonth==12)&&(intDay>31||intDay<1)){
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";
}
if((intMonth==4||intMonth==6||intMonth==9||intMonth==11)&&(intDay>30||intDay<1)) {
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";}
if(intMonth==2){
if(intDay<1){
alert("您输入的"+tip+"日期格式错误,请参照：1900-01-01");
return "false";}
boolLeapYear = false;
if((intYear%400==0)||(intYear%4==0 && intYear%100!=0)) //判断闰年
boolLeapYear = true;
else
boolLeapYear = false;//平年
if(boolLeapYear){
if(intDay>29) {
alert(tip+"日期错误,闰年2月份的日份必须在1—29之间");
return "false";}
}
else{
if(intDay>28) {
alert(tip+"日期错误,2月份的日份必须在1—28之间");
return "false";}
}
}
return "true";
}