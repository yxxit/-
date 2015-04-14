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
using System.IO;
using XBase.Common;
using XBase.Business.Common;

public partial class Pages_Personal_PersonalFileDownload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FileName"] != null)
        {
            string fileurl = Request.QueryString["FileName"].ToString();
            try
            {
                // liuch 20120825 从系统指定的目录中下载文件，而不是程序所在目录 upload\personal
                DataTable dtFileInfo = UploadFileBus.GetCompanyUploadFileInfo();
                string savePath = GetSafeData.ValidateDataRow_String(dtFileInfo.Rows[0], "DocSavePath");
                // string newsavepath = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                savePath += "\\personal\\" ;
                FileDownload(savePath + fileurl);

                // FileDownload(AppDomain.CurrentDomain.BaseDirectory + "upload\\personal\\" + fileurl);
            }
            catch {
                //Response.ClearHeaders();
                //Response.Buffer = false;
                //Response.ContentType = "text/html";
                Response.Clear();
                Response.Write("该附件已被移除");
            }
        }else {
            Response.Clear();
            Response.Write("该附件已被移除");
        }
    }
    private void FileDownload(string FullFileName)
    {
        FileInfo DownloadFile = new FileInfo(FullFileName);
        Response.Clear();
        Response.ClearHeaders();
        Response.Buffer = false;
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.UTF8));
        Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
        Response.WriteFile(DownloadFile.FullName);
        Response.Flush();
        Response.End();
    }   

}
