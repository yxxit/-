using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text;

/// <summary>
///Print 的摘要说明
/// </summary>
public class PrintHelp
{
    public int m_currentPageIndex;
    public IList<Stream> m_streams;
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="reportPath">報表路徑</param>
    ///// <param name="printerName">打印機名稱（使用默認打印機，不賦值）</param>
    ///// <param name="dt1">報表數據源1</param>
    ///// <param name="dt1SourceName">報表中數據源1對應名稱</param>
    ///// <param name="dt2">報表數據源2</param>
    ///// <param name="dt2SourceName">報表中數據源2對應名稱</param>
    //public void Run(string reportPath, string printerName, DataTable dt1, string dt1SourceName, DataTable dt2, string dt2SourceName,bool isHindeLogo)
    //{
    //    LocalReport report = new LocalReport();
    //    report.ReportPath = reportPath;//加上报表的路径
    //    report.DataSources.Add(new ReportDataSource(dt1SourceName, dt1));
    //    report.DataSources.Add(new ReportDataSource(dt2SourceName, dt2));
    //    report.EnableExternalImages = true;
    //    //这里我在报表里弄的参数
    //    ReportParameter rp = new ReportParameter("isHindeLogoImg", isHindeLogo.ToString());
    //    report.SetParameters(rp);
    //    Export(report);
    //    m_currentPageIndex = 0;
    //    Print(printerName);
    //}

    public void Export(LocalReport report)
    {
        //这里是设置打印的格式 边距什么的
        string deviceInfo =
          "<DeviceInfo>" +
          "  <OutputFormat>EMF</OutputFormat>" +
          "  <PageWidth>210mm</PageWidth>" +
          "  <PageHeight>297mm</PageHeight>" +
          "  <MarginTop>20mm</MarginTop>" +
          "  <MarginLeft>16mm</MarginLeft>" +
          "  <MarginRight>0mm</MarginRight>" +
          "  <MarginBottom>20mm</MarginBottom>" +
          "</DeviceInfo>";
        //PageCountMode pageCountMode;
        Warning[] warnings;
        m_streams = new List<Stream>();
        try
        {
            //一般情况这里会出错的  使用catch得到错误原因  一般都是简单错误
            report.Render("Image", deviceInfo, CreateStream, out warnings);
        }
        catch (Exception ex)
        {
            //取内异常。因为内异常的信息才有用，才能排除问题。
            Exception innerEx = ex.InnerException;
            while (innerEx != null)
            {
                //MessageBox.Show(innerEx.Message);
                string errmessage = innerEx.Message;
                innerEx = innerEx.InnerException;
            }
        }
        foreach (Stream stream in m_streams)
        {
            stream.Position = 0;
        }
    }

    public Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
    {
        //name 需要进一步处理
        Stream stream = new FileStream(name + DateTime.Now.Millisecond + "." + fileNameExtension, FileMode.Create);//为文件名加上时间
        m_streams.Add(stream);
        return stream;
    }

    public void Print(string printerName)
    {
        // "傳送至 OneNote 2007";
        //string printerName = this.TextBox1.Text.Trim();
        if (m_streams == null || m_streams.Count == 0)
        {
            return;
        }
        PrintDocument printDoc = new PrintDocument();
        // string aa = printDoc.PrinterSettings.PrinterName;
        if (printerName.Length > 0)
        {
            printDoc.PrinterSettings.PrinterName = printerName;
        }
        foreach (PaperSize ps in printDoc.PrinterSettings.PaperSizes)
        {
            if (ps.PaperName == "A4")
            {
                printDoc.PrinterSettings.DefaultPageSettings.PaperSize = ps;
                printDoc.DefaultPageSettings.PaperSize = ps;
                //知道是否是预设定的打印机
                // printDoc.PrinterSettings.IsDefaultPrinter;
            }
        }
        if (!printDoc.PrinterSettings.IsValid)
        {
            string msg = String.Format("Can't find printer " + printerName);
            System.Diagnostics.Debug.WriteLine(msg);
            return;
        }
        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        //打印
        printDoc.Print();
    }
    public void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
        //設置打印尺寸 单位是像素
        ev.Graphics.DrawImage(pageImage, 0, 0, 827, 1169);
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }
}