using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using XBase.Business.Personal.Culture;

public partial class Pages_Personal_PersonalUploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();

        string DocsID, DocType;
        DocsID = Request.Params["docsID"];
        DocType = Request.Params["docType"];
        if (DocsID == "")
        {
            DocsID = "0";
        }

        if (Request.Files.Count > 0)
        {
            HttpPostedFile upPhoto = Request.Files[0];
            int upPhotoLength = upPhoto.ContentLength;
            byte[] PhotoArray = new Byte[upPhotoLength];
            Stream PhotoStream = upPhoto.InputStream;
            PhotoStream.Read(PhotoArray, 0, upPhotoLength); //这些编码是把文件转换成二进制的文件

            //string savePath = Server.MapPath("~/upload/Personal/");
            //System.IO.FileInfo finfo = new System.IO.FileInfo(upPhoto.FileName);
            //string filename = finfo.Name;
          
            //savePath = savePath + filename;
            //if (System.IO.File.Exists(savePath))
            //{
            //    System.IO.File.Delete(savePath);
            //}
            //upPhoto.SaveAs(savePath);
             CultureDocs bll = new CultureDocs();
             bll.addDoctent(DocsID, DocType, PhotoArray);
            Response.Write("true");
            Response.End();
        }
        else
        {
            Response.Write("No File Upload!");
        }
        
    }
}
