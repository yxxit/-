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

using System.Text;



public partial class UserControl_LeftMenu : System.Web.UI.UserControl
{
    public string str = "";
    private LeftMenuNode _rootNode;
    public LeftMenuNode RootNode
    {
        get
        {
            return _rootNode;
        }
        set
        {
            _rootNode = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void BuildNodes()
    {
        StringBuilder sb = new StringBuilder();
        foreach (LeftMenuNode node in RootNode.SubNodes)
        {
            str = node.CompanyCD + "," + node.UserID + ",";
            BuildSubNodes(node, sb);
        }

        this.Controls.Add(new LiteralControl(sb.ToString()));
    }

    protected void BuildSubNodes(LeftMenuNode node, StringBuilder sb)
    {
        DoNodesub(node, sb);

        foreach (LeftMenuNode node2 in node.SubNodes)
        {
            BuildSubNodes(node2, sb);
            // DoNode(node2, sb);
            //  foreach (LeftMenuNode node3 in node2.SubNodes)
            //{
            //  //BuildSubNodes(node2,sb);
            //  DoNodesub(node3, sb);
            // }
        }

        if (node.SubNodes.Count > 0)
        {
            sb.Append("</td></tr></table>");
        }
    }

    protected void DoNode(LeftMenuNode node, StringBuilder sb)
    {
        sb.Append("<table ");

        if (node.Level == 0)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"0\"");
        }
        if (node.Level == 1)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"3\"");
        }
        if (node.Level >= 2)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"3\"");
        }


        sb.Append("border=0><tr><td style=\"cursor:pointer;\" level=\"" + node.Level.ToString() + "\" onmouseover=\"highLight(event,0);\"  onmouseout=\"highLight(event,1);\" ");
        if (node.SubNodes.Count > 0)
        {
            if (node.Level == 0)
            {
                sb.Append(" onclick=\"expandSubMenu(event,'" + node.NodeUrl + "');\" ");
                node.NodeUrl = "";
            }
            else
            {
                sb.Append(" onclick=\"expandSubMenu(event);\" ");
            }
        }


        sb.Append(">");

        if (node.NodeIcon != string.Empty)
        {
            sb.Append("<img src=\"" + node.NodeIcon + "\">");
        }


        if (node.NodeUrl != string.Empty)
        {
            string url = node.NodeUrl;
            if (url.IndexOf("?") == -1)
            {
                url += "?ModuleID=" + node.Data;
            }
            else
            {
                url += "&ModuleID=" + node.Data;
            }

            sb.Append("<span dataLink='" + url + "' onclick='parent.addTab(this);' tabid='" + node.Data + "' tabTitle='" + node.NodeName + "'>" + node.NodeName + "</span>");
            
            //sb.Append("<a target=\"Main\" href=\"" + url + "\">" + node.NodeName + "</a>");

        }
        else
        {
            sb.Append(node.NodeName);
        }

        //if (node.Level == 0)
        //{
        //    sb.Append("<img src=\"images/left_frame/Arrow_open.jpg\">");
        //}

        sb.Append("</td></tr>");

        if (node.SubNodes.Count > 0)
        {
            int paddingLeft = 30;
            sb.Append("<tr><td style=\"display:none;padding-left:" + paddingLeft.ToString() + "px;\">");

        }
        else
        {
            sb.AppendLine("</table>");
        }
    }

    protected void DoNodesub(LeftMenuNode node, StringBuilder sb)
    {
        sb.Append("<table ");

        if (node.Level == 0)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"0\"");
        }
        if (node.Level == 1)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"3\"");
        }
        if (node.Level >= 2)
        {
            sb.Append("cellspacing=\"0\" cellpadding=\"3\"");
        }


        sb.Append("border=0><tr><td style=\"cursor:pointer;\" level=\"" + node.Level.ToString() + "\" onmouseover=\"highLight(event,0);\"  onmouseout=\"highLight(event,1);\" ");
        if (node.SubNodes.Count > 0)
        {
            if (node.Level == 0)
            {
                sb.Append(" onclick=\"expandSubMenu(event,'" + node.NodeUrl + "');\" ");
                node.NodeUrl = "";
            }
            else
            {
                sb.Append(" onclick=\"expandSubMenu(event);\" ");
            }
        }


        sb.Append(">");

        if (node.NodeIcon != string.Empty)
        {
            sb.Append("<img src=\"" + node.NodeIcon + "\">");
        }


        if (node.NodeUrl != string.Empty)
        {
            string url = node.NodeUrl;
            if (url.IndexOf("?") == -1)
            {
                url += "?ModuleID=" + node.Data;
            }
            else
            {
                url += "&ModuleID=" + node.Data;
            }
            //sb.Append("<span style=\"font-size:14px; \"dataLink='" + url + "' onclick='parent.addTab(this);' tabid='" + node.Data + "' tabTitle='" + node.NodeName + "'>" + node.NodeName + "</span><a style=\"cursor:pointer;\"   title=\"帮助\" onclick=\"SetWindowOpen(" + node.Data + ")\"><img src=\"Images/Menu/help.gif\" title=\"帮助\" border=\"0\"></a>");
            sb.Append("<span style=\"font-size:14px; \"dataLink='" + url + "' onclick='parent.addTab(this);' tabid='" + node.Data + "' tabTitle='" + node.NodeName + "'>" + node.NodeName + "</span>");
            
            //sb.Append("<a target=\"Main\" href=\"" + url + "\">" + node.NodeName + "</a><a style=\"cursor:pointer;\"   title=\"帮助\" onclick=\"SetWindowOpen(" + node.Data + ")\"><img src=\"Images\\Menu\\help.gif\" title=\"帮助\" border=\"0\"></a>");
        }
        else
        {
            if (node.Data.Length > 3 && node.Data != "10590")
            {
                sb.Append(node.NodeName + "<a style=\"cursor:pointer;\"   title=\"帮助\" onclick=\"SetWindowOpen(" + node.Data + ")\"><img src=\"Images/Menu/help.gif\" title=\"帮助\" border=\"0\"></a>");
            }
            else
            {
                sb.Append(node.NodeName);
            }
        }

        //if (node.Level == 0)
        //{
        //    //sb.Append("<img src=\"images/left_frame/Arrow_open.jpg\">");
        //    if (node.Data.Length == 3 || node.Data == "10590")
        //    {

        //        sb.Append("</td><td style=\"cursor:pointer; font-size:14px;\"   onclick=\"SetWindowOpen(" + node.Data + ")\" ><img src=\"Images/Menu/help1.gif\" title=\"帮助\" border=\"0\"></a>");
        //    }
        //}
        //sb.Append("</td></tr>"); //此处添加增加的按钮代码
        if (!string.IsNullOrEmpty(node.ModuleType))
        {
            string str1 = str + node.Data;
            if (node.ModuleType.Trim().ToUpper() == "M")
            {

                sb.Append("</td><td id=\"'" + str1 + "' \" style=\"cursor:pointer; font-size:13px; \" level=\"1\" onmouseover=\"highLight(event,0);\"     onmouseout=\"highLight(event,1);\" title=\"添加为快捷菜单\" onclick=\"qukmenu('" + str1 + "');\"><img src=\"Images/Menu/icon_list_close.gif\">");
            }
            //if (node.Data.Length > 3 && node.Data != "10590")
            //{
            //    sb.Append("</td><td style=\"cursor:pointer;\"   title=\"帮助\" onclick=\"SetWindowOpen(" + node.Data + ")\" ><img src=\"Images\\Menu\\help.gif\" title=\"帮助\" border=\"0\"></td></tr>");
            //}

        }
        if (node.SubNodes.Count > 0)
        {
            int paddingLeft = 14;//子菜单左边距
            sb.Append("<tr><td style=\"display:none;padding-left:" + paddingLeft.ToString() + "px; font-size:14px; \">");

        }
        else
        {
            sb.AppendLine("</table>");
        }
    }
}
