var isStart=true;
var nn;
var tt;
var bPlay=new Image;
bPlay.src = "";
var bPause=new Image;
bPause.src = "";
nn=1;
function resetPlay(){
isStart=true;
var e = document.getElementById("top_slider");
var a = e.getElementsByTagName("img");
for(i=0;i<a.length;i++){
 if(a[i].src == bPlay.src) a[i].src = bPause.src;
}
}
function playorpau(e){
if(e.src == ""){
e.src = bPlay.src ;
isStart = false;
}else{
e.src = bPause.src;
isStart = true;
}
}
function pre_img(){
resetPlay();
nn--;
if(nn < 1) nn=2;
setFocus(nn);
}
function next_img(){
resetPlay();
nn++;
if(nn > 2) nn=1;
setFocus(nn);
}
function change_img()
{
if(isStart){
 nn++;
 if(nn>2) nn=1;
 setFocus(nn);
 }else{
 }
}
function setFocus(i)
{
 if(tt) clearTimeout(tt);
 nn = i;
 selectLayer1(i);
}
function selectLayer1(i)
{
 switch(i)
 {
 case 1:
document.getElementById("bbs_s1").style.display="block";
document.getElementById("bbs_s2").style.display="none";
 break;
case 2:
document.getElementById("bbs_s1").style.display="none";
document.getElementById("bbs_s2").style.display="block";
 break;

 }
}
