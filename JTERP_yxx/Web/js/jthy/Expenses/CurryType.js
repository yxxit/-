

//创建对象
var xmlHttp;
function createXMLHttpRequest()
{
    xmlHttp = false;
    xmlhttpObj = ["MSXML2.XmlHttp.5.0","MSXML2.XmlHttp.4.0","MSXML2.XmlHttp.3.0","MSXML2.XmlHttp","Microsoft.XmlHttp"];
    if(window.XMLHttpRequest)
    {
        xmlHttp = new XMLHttpRequest();
    }
    else if(window.ActiveXObject)
    {
        for(i=0;i<xmlhttpObj.length;i++)    
        {
            xmlHttp = new ActiveXObject(xmlhttpObj[i]);
            if(xmlHttp)
            {
                break;    
            }
        }
    }
    else
    {
        alert("暂时不能创建XMLHttpRequest对象");
    }
}



//选择汇率
function SelectRate(ControlName,ControlCD,ControlExchangeRate)
{    
   var Url = "../../../Handler/Office/FinanceManager/Voucher.ashx?ControlName="+ControlName+"&ControlID="+ControlCD+"&Action=Get"+"&ExchangeRate="+ControlExchangeRate;
   createXMLHttpRequest();
   xmlHttp.onreadystatechange = setRateDiv;
   xmlHttp.open("post", Url, true);
   xmlHttp.send(null);
}




//填充汇率div
function setRateDiv()
{
    if (xmlHttp.readyState == 4){
        if (xmlHttp.status == 200)
        {

           document.getElementById("Container").innerHTML=xmlHttp.responseText;

           document.getElementById("Container").style.display="block";
        }
        else
        {
             document.getElementById("Rate").innerHTML = "数据获取错误！"+xmlHttp.status;
        }
    }
}


function GetRate(controlName,controlidCD,ControlExchangeRate)
{
     var row = document.getElementById("dt_Rate").getElementsByTagName("tr");
       for(var rows = 0; rows < row.length; rows++)
        {
           //获取当前行的列数
            var cols = row[rows].getElementsByTagName("td");
            //遍历所有列，以获取值
            for(var col = 0;col < cols.length; col++)
            {
                //获取列的input控件
                var objs = cols[col].getElementsByTagName('input');
                 for(var i = 0; i <objs.length; i++)
                 {
                    if(objs[i].getAttribute("type") == "checkbox"  && objs[i].checked)
                    {
                        
                        
                        var returnValue=objs[i].value.split('|');
                        
                        
                        document.getElementById(controlidCD).value=returnValue[0].toString();//2
                        document.getElementById(controlName).value=returnValue[2].toString();//1
                        document.getElementById(ControlExchangeRate).value=Numb_round(returnValue[1].toString(),2);
                        document.getElementById("Container").style.display="none";
                    }
                 }
            }
        }
} 


function HideDiv()
{
   document.getElementById("Container").style.display="none";
}


//对输入的数字自动保留两位小数，四舍五入
function Numb_round(number,fractionDigits)
{   
    if(number!=parseInt(number))
    {
          with(Math)
          {   
            return round(number*pow(10,fractionDigits))/pow(10,fractionDigits);
          }   
    }
    else
    {
           return number+".00";
    }

}   