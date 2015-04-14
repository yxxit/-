using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using XBase.Common;
using XBase.Business.Common;


public partial class Pages_Personal_FileManage_FileAdd : System.Web.UI.Page
{
    private UserInfoUtil UserInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];

        if (!this.IsPostBack)
        {
            this.txtMemoDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.memoUserID.Value = UserInfo.DeptID.ToString();
            this.txtMemoUserID.Text = UserInfo.DeptName;
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string companyCD = string.Empty;
        //获取公司代码
        try
        {
            companyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
        }
        catch
        {
            companyCD = "AAAAAA";
        }
        //获取公司文件相关信息
        DataTable dtFileInfo = UploadFileBus.GetCompanyUploadFileInfo();



        string TItle = Server.HtmlEncode(this.txtName.Text);
        string Content = Server.HtmlEncode(this.txtContent.Text);
        string UserCanViewUserName = this.UserCanViewUserName.ToString();//2012-10-25 添加 判断页面上是否指定可查看的人员
        string deptid = this.memoUserID.Value;
        DateTime pubDate = DateTime.Parse(this.txtMemoDate.Text);
        pubDate = pubDate.AddHours(DateTime.Now.Hour);
        pubDate = pubDate.AddMinutes(DateTime.Now.Minute);
        pubDate = pubDate.AddSeconds(DateTime.Now.Second);

        XBase.Model.Personal.Culture.CultureDocs model = new XBase.Model.Personal.Culture.CultureDocs();
        model.CultureTypeID = int.Parse(this.inputCompuny.Value);
        model.CreateDeptID = int.Parse(deptid);
        model.CreateDate = pubDate;

        model.Title = TItle;
        model.Culturetent = Content;

        model.CompanyCD = UserInfo.CompanyCD;
        model.Creator = UserInfo.EmployeeID;

        model.ModifiedDate = DateTime.Now; ;
        model.ModifiedUserID = UserInfo.UserID;
        if (UserCanViewUserName != "")//2012-10-25 添加 判断页面上是否指定可查看的人员
        {
            model.UserCanViewUserName = this.UserCanViewUserName.Text;
        }
        else
        {
            model.UserCanViewUserName = "";
        }

        if (txtFile.HasFile)
         {
             //String savePath = Server.MapPath("../../../upload/fileStorage/");

           

             //文件保存路径
             string savePath = GetSafeData.ValidateDataRow_String(dtFileInfo.Rows[0], "DocSavePath");
            
             string newsavepath = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
             savePath += "\\" + newsavepath;

             DirectoryInfo folder = new DirectoryInfo(savePath);

             //目录不存在，则创建新的目录
             if (!folder.Exists)
             {
                 //创建目录
                 folder.Create();
             }

             System.IO.FileInfo finfo = new System.IO.FileInfo(txtFile.PostedFile.FileName);
             string filename = finfo.Name;
             model.Attachment = filename;
             savePath = savePath + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()+
                 DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString() + filename;
             model.IFileName = filename;//附件名
             model.IFileAddr = savePath;//附件上传路径
             if (System.IO.File.Exists(savePath))
             {
                 System.IO.File.Delete(savePath);
             }
             txtFile.PostedFile.SaveAs(savePath);
         }
         else
         {
             model.Attachment = "";
         }

         XBase.Business.Personal.Culture.CultureDocs bll = new XBase.Business.Personal.Culture.CultureDocs();
         int DocsID = bll.Add(model); 
         bll.UpdateMainID(DocsID);

         StringBuilder sb = new StringBuilder();
         sb.Append("<script language='javascript'>");
         sb.Append("if (!confirm('文件保存成功,需要继续添加吗？')) {");
         sb.Append("window.location.href='FileList.aspx';");
         sb.Append("}else{");
         sb.Append("window.location.href='FileAdd.aspx';");
         sb.Append("}");
         sb.Append("</script>");
         ClientScript.RegisterStartupScript(this.GetType(), "LoadScript", sb.ToString());

    }
}
