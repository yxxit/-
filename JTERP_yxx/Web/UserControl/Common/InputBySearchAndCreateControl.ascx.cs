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

public partial class UserControl_Common_InputBySearchAndCreateControl : System.Web.UI.UserControl
{
    public string pageID;

    #region 给搜索按钮设置方法

    /// <summary>
    /// 给搜索按钮设置方法

    /// </summary>
    public string SetSearchFunction
    {
        get
        {
            return hidFunction1.Value;
        }
        set
        {
            hidFunction1.Value = value;
        }
    }

    #endregion

    #region 给新建按钮设置方法

    /// <summary>
    /// 给新建按钮设置方法

    /// </summary>
    public string SetCreateFunction
    {
        get
        {
            return hidFunction2.Value;
        }
        set
        {
            hidFunction2.Value = value;
        }
    }
    #endregion

    #region 给新建按钮设置方法

    /// <summary>
    /// 给新建按钮设置方法

    /// </summary>
    public string SetDeleteFunction
    {
        get
        {
            return hidFunction3.Value;
        }
        set
        {
            hidFunction3.Value = value;
        }
    }
    #endregion
    #region 设置宽度
    public string Width
    {
        get
        {
            return hidWidth.Value;
        }
        set
        {
            hidWidth.Value = value;
        }
    }
    #endregion

    #region 设置搜索按钮获取焦点时显示的文字
    public string AltAtrributWordBySearchControl
    {
        get
        {
            return hidsearchAltWord.Value;
        }
        set
        {
            hidsearchAltWord.Value = value;
        }
    }
    #endregion

    #region 设置新建按钮获取焦点时显示的文字
    public string AltAtrributWordByCreateControl
    {
        get
        {
            return hidCreateAltWord.Value;
        }
        set
        {
            hidCreateAltWord.Value = value;
        }
    }
    #endregion

    #region 获取页面设置的控件id
    public string pagesSetID
    {
        get
        {
            return pageID;
        }
        set
        {
            pageID = value;
        }
    }
    #endregion
    #region
    public string Value
    {
        get
        {
            return txtControl.Value;
        }
        set
        {
            txtControl.Value = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
