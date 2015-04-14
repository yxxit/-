/**********************************************
 * 类作用：   扫描上传
 * 建立人：   包胜东
 * 建立时间： 2014/03/06
 ***********************************************/
using System;
using System.Web.UI;
using XBase.Common;
using System.IO;
using System.Web;
using System.Data;
using XBase.Business.Common;

public partial class Pages_Common_UploadImage : System.Web.UI.Page
{
    protected static string scanImgPath = string.Empty;
    protected static string imagename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            //string RootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            //scanImgPath = RootDir + "/Pages/Office/FirstBusiness/ScanImage/";
            //scanImgPath = HttpContext.Current.Request.MapPath("ScanImage/");
            string userid = ((UserInfoUtil)SessionUtil.Session["UserInfo"]).UserID;
            scanImgPath = "C:/WINDOWS/Temp/";
            scanImgPath = scanImgPath.Replace(@"\", @"\\");
            imagename = userid+DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString()+
            DateTime.Now.Hour.ToString()+DateTime.Now.Minute.ToString()+DateTime.Now.Second.ToString()+DateTime.Now.Millisecond.ToString();
        }
    }
    protected void btnUpload_Click(object sender, ImageClickEventArgs e)
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
        //
        if (dtFileInfo == null || dtFileInfo.Rows.Count < 1)
        {
        }
        //文件个数
        int docNum = GetSafeData.ValidateDataRow_Int(dtFileInfo.Rows[0], "MaxDocNum");
        //文件总大小
        long totalSize = GetSafeData.ValidateDataRow_Long(dtFileInfo.Rows[0], "MaxDocSize");
        //单个文件大小
        long singleSize = GetSafeData.ValidateDataRow_Long(dtFileInfo.Rows[0], "SingleDocSize");
        //文件保存路径
        string savePath = GetSafeData.ValidateDataRow_String(dtFileInfo.Rows[0], "DocSavePath");

        string newsavepath = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
        savePath += "\\" + newsavepath;
        //获取控件上的文件对象
       // HttpPostedFile hpFile = flLocalFile.PostedFile;
        
        string  AllFileName = scanImgPath + imagename + ".jpg";
        
        
        
        

        //string docName = hpFile.FileName.Substring(hpFile.FileName.LastIndexOf("\\") + 1);
        string docName = AllFileName;

        string docNames = imagename + ".jpg";
        //校验文件大小
        //string checkResult = CheckCompanyFile(hpFile, savePath, totalSize, singleSize, docNum);
        string checkResult = CheckCompanyFile(AllFileName, savePath, totalSize, singleSize, docNum);
        //大小超过允许范围时
        if (!string.IsNullOrEmpty(checkResult))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('" + checkResult + "');</script>");
            return;
        }
        string IsExistFilePath = savePath + "\\" + docName;

        if (File.Exists(IsExistFilePath))//文件已经存在提示
        {
            ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('文件名有重复，请重命名文件在上传！');</script>");
        }
        else
        {
            //上传文件并获取文件相对路径
            //string fileName = HtmlInputFileControl.SaveUploadFile(hpFile, savePath);
            string fileName = docName;
            //上传未成功
            if (string.IsNullOrEmpty(fileName))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "UploadFaild", "<script language=javascript>popMsgObj.ShowMsg('"
                                    + ConstUtil.UPLOAD_FILE_ERROR_TYPE + "');</script>");
                return;
            }
            //隐藏域中写入文件相对路径
            uploadFileUrl.Value = fileName;
            //上传文件名
            uploadDocName.Value = docNames;
            //执行返回函数
            ClientScript.RegisterStartupScript(this.GetType(), "UploadSucc", "<script language=javascript>DoConfirm();</script>");
        }
    }

    #region 判断上传该文件是否超过公司允许的文件大小
    /// <summary>
    /// 判断上传该文件是否超过公司允许的文件大小
    /// </summary>
    /// <param name="hpFile">上传文件对象</param>
    /// <param name="savePath">保存路径</param>
    /// <param name="totalSize">文件总大小</param>
    /// <param name="singleSize">单个文件大小</param>
    /// <param name="docNum">文件个数</param>
    /// 
    private string CheckCompanyFile(string hpFile, string savePath, long totalSize, long singleSize, int docNum)
    {
        //判断文件个数是否达到上限
        if (FileUtil.GetFolderFileCount(savePath) >= docNum)
        {
            return ConstUtil.UPLOAD_FILE_ERROR_MAX_NUM + "(" + docNum.ToString() + ")";
        }
        //获取上传文件大小 UPLOAD_FILE_ERROR_MAX_NUM
        long fileSize = long.Parse(hpFile.Length.ToString()) / (1024 * 1024);
        //判断单个文件大小是否达到上限
        if (singleSize < fileSize)
        {
            return ConstUtil.UPLOAD_FILE_ERROR_SINGLE_SIZE + "(" + singleSize.ToString() + ")";
        }
        //获取文件夹大小
        long nowTotalSize = FileUtil.GetFolderSize(savePath) / (1024 * 1024);
        //判断总大小是否达到上限
        if (totalSize < (nowTotalSize + fileSize))
        {
            return ConstUtil.UPLOAD_FILE_ERROR_MAX_SIZE + "(" + totalSize.ToString() + ")";
        }
        //返回校验结果
        return string.Empty;
    }
    #endregion
    
}
