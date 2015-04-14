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
using XBase.Data.DBHelper;


public partial class Pages_Personal_FileManage_FileEdit : System.Web.UI.Page
{
    private UserInfoUtil UserInfo = null;
    private XBase.Business.Personal.Culture.CultureDocs bll = new XBase.Business.Personal.Culture.CultureDocs();
    private XBase.Model.Personal.Culture.CultureDocs entity = new XBase.Model.Personal.Culture.CultureDocs();
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        entity = bll.GetModel(int.Parse(Request["ID"].ToString()));
        if (!IsPostBack)
        {
            if (Request["ID"] != null)
            {
                this.hidDocsID.Value = Request["ID"].ToString();
                string typename = Request.QueryString["typename"] == null ? "" : Request.QueryString["typename"].ToString();

                this.txtMemoUserID.Text = XBase.Business.Office.HumanManager.DeptInfoBus.GetDeptNameByID(entity.CreateDeptID.ToString());
                this.memoUserID.Value = entity.CreateDeptID.ToString();
                this.txtName.Text = Server.HtmlDecode(entity.Title);
                this.txtContent.Text = Server.HtmlDecode(entity.Culturetent);
                this.hidDocsFile.Value = entity.Attachment;

                string CompanyCD = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).CompanyCD;
                string tourl = "";
                string seltheurl = "select iFileAddr from officedba.CultureDocs where id='" + this.hidDocsID.Value + "' and companycd='"+CompanyCD+"' ";
                DataTable dtUrl = SqlHelper.ExecuteSql(seltheurl);
                if (dtUrl.Rows.Count > 0)
                {
                    tourl = dtUrl.Rows[0][0].ToString();
                }
                else
                {
                    tourl = "";
                }
                //CurrentFile.NavigateUrl = "../../../upload/fileStorage/" + entity.Attachment;
                CurrentFile.NavigateUrl =tourl;
                CurrentFile.Text = hidDocsFile.Value;
                this.txtMemoDate.Text = entity.CreateDate.ToShortDateString();
                this.UserCanViewUserName.Text = entity.UserCanViewUserName;//2012-10-26 添加 可查看用户
                this.inputCompuny.Value = Convert.ToString(entity.CultureTypeID);
                this.Compuny.Text = typename;
            }
            else
            {
                Response.Redirect("FileList.aspx");
            }
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
        string UserCanViewUserName = this.UserCanViewUserName.Text;//2012-10-25 添加 判断页面上是否指定可查看的人员
        string deptid = this.memoUserID.Value;
        DateTime pubDate = DateTime.Parse(this.txtMemoDate.Text);
        pubDate = pubDate.AddHours(DateTime.Now.Hour);
        pubDate = pubDate.AddMinutes(DateTime.Now.Minute);
        pubDate = pubDate.AddSeconds(DateTime.Now.Second);

       // XBase.Model.Personal.Culture.CultureDocs model = new XBase.Model.Personal.Culture.CultureDocs();
        entity.CultureTypeID = int.Parse(this.inputCompuny.Value);
         entity.CreateDeptID = int.Parse(deptid);
         entity.CreateDate = pubDate;

        entity.Title = TItle;
        entity.Culturetent = Content;

        entity.CompanyCD = UserInfo.CompanyCD;
        entity.Creator = UserInfo.EmployeeID;

        entity.ModifiedDate = DateTime.Now; ;
        entity.ModifiedUserID = UserInfo.UserID;
        if (UserCanViewUserName != "")//2012-10-25 添加 判断页面上是否指定可查看的人员
          {
              entity.UserCanViewUserName = UserCanViewUserName;
          }
          else
          {
              entity.UserCanViewUserName = "";
          }

            if (txtFile.HasFile)
            {
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
                entity.Attachment = filename;
              
                savePath = savePath + "\\" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
                    DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + filename;
                entity.IFileName = filename;//附件名
                entity.IFileAddr = savePath;//附件上传路径

               // savePath = savePath + filename;
                if (System.IO.File.Exists(savePath))
                {
                    System.IO.File.Delete(savePath);
                }
                txtFile.PostedFile.SaveAs(savePath);
            }
            else
            {
                entity.Attachment = this.hidDocsFile.Value;
            }

            XBase.Business.Personal.Culture.CultureDocs bll = new XBase.Business.Personal.Culture.CultureDocs();
            bll.Update(entity);

            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript'>");
            sb.Append("if (!confirm('文件修改成功,需要进行新增吗？')) {");
            sb.Append("window.location.href='FileList.aspx';");
            sb.Append("}else{");
            sb.Append("window.location.href='FileAdd.aspx';");
            sb.Append("}");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "LoadScript", sb.ToString());
    }
}
