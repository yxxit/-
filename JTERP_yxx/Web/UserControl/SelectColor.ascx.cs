using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using XBase.Business.Office.StorageManager;
using XBase.Business.Office.SystemManager;
using XBase.Business.Office.SupplyChain;

public partial class UserControl_selectColor : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
         /// <summary>
    /// 绑定所有下拉框
    /// </summary>

       string TypeFlag = "5";
      
       string  Code = "3";
       string htmlDomStr="";
        DataTable dt=new DataTable();
        dt = CodePublicTypeBus.GetCodePublicByCode(TypeFlag, Code);//绑定颜色
        if (dt.Rows.Count > 0)
        {
            //this.sel_ColorID.DataTextField = "TypeName";
            //sel_ColorID.DataValueField = "ID";
            //sel_ColorID.DataSource = dt;
            //sel_ColorID.DataBind();
           // htmlDomStr = "<select id='colorSelect'>";
           // htmlDomStr = "<tr>";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                htmlDomStr += "<tr><td style='width:30%'><input id='color" + i + "' type='radio' name='1' value= " + dt.Rows[i]["TypeName"] + " title=" + dt.Rows[i]["ID"] + " onclick='hideSelColor(" + i + ")'/></td><td>" + dt.Rows[i]["TypeName"] + "</td></tr>";
            }      
            //htmlDomStr+="</select>";
           // htmlDomStr += "</tr>";
            Lit_color.Text=htmlDomStr;
           
        }
       // sel_ColorID.Items.Insert(0, new ListItem("--请选择--", "0"));

    
    }
}
