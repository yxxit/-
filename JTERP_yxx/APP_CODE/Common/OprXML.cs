using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
namespace XBase.Common
{
    public class OprXML
    {
        public string ReadXML(string dbname, string tbname,string kyevalue)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/JTHYSet.xml"));
            //XmlNodeList xmlNodeList = xmlDoc.SelectNodes("smallfoolsRoot/poems[contains(author,'" + Request.QueryString["Xid"].ToString() + "')]");//糊模查找记录
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes(""+dbname+"/"+tbname+"[id='" + kyevalue + "']");//查找
            XmlNode xmlNode = xmlNodeList.Item(0);
            return xmlNode["content"].InnerText;
            
        }
    }
}
