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
using System.Text.RegularExpressions;

using XBase.Business.Office.SupplyChain;
using XBase.Common;
using System.IO;
public partial class Pages_Office_SupplyChain_ProductInfoInsertList : BasePage
{
    UserInfoUtil userinfo = new UserInfoUtil();
    public static DataSet ds = new DataSet();
    public string errorstr = string.Empty; //错误串
    public DataTable dtCopy;
    public static int right = 0;
    public static int wrong = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
                
           
        
    }
    protected void initvalidate()
    {
        this.lbl_result.Text = string.Empty; errorstr = string.Empty;
        setup1.Enabled = false; setup2.Enabled = false; setup3.Enabled = false; setup4.Enabled = false; setup5.Enabled = false; setup6.Enabled = false; this.btn_input.Enabled = false;
        tr_result.Visible = false; tab_end.Visible = false; lbl_end.Visible = false;

        if (Session["newfile"] != null)
        {
            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        }
    }

    /// <summary>
    /// 上传excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_excel_Click(object sender, EventArgs e)
    {
        right = 0;
        wrong = 0;
        this.tab_end.Visible = false;
        if (upExcelFile.PostedFile.FileName.Length < 1)
        {
            this.lbl_result.Text = "请选择要上传的Excel文件!";
            return;
        }
        else
        {
            this.initvalidate();
        }
        string filename = upExcelFile.PostedFile.FileName;
        string subfile = filename.Substring(filename.LastIndexOf(".") + 1);
        if (subfile.ToUpper() != "XLS" && subfile.ToUpper() != "XLSX")
        {
            this.lbl_result.Text = "导入文件格式错误,必须为XLS,XLSX格式!";
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
        }
        else
        {
            try
            {
                //获取企业上传文件路径 
                string upfilepath = ProductInfoBus.GetCompanyUpFilePath(userinfo.CompanyCD);//得到格式如:"D:\zhou"
                //获取企业并构造企业上传文件名称
                Session["newfile"] = DateTime.Now.ToString("yyyyMMddhhmmss") + filename.Substring(filename.LastIndexOf("\\") + 1);
                //Session["newfile"] = "product1111.xls";
                upExcelFile.PostedFile.SaveAs(upfilepath + @"\" + Session["newfile"].ToString());
                this.lbl_result.Text = "Excel文件上传成功，请点击“批量导入”导入文件！";
                this.btn_input.Enabled = true;
                this.setup1.Enabled = true;
                //将excel中的数据读取到ds中
                //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "文件上传成功");
                try
                {
                    ds = ProductInfoBus.ReadExcel_ds(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    //ds = ProductInfoBus.ReadEexcel(upfilepath + @"\" + Session["newfile"].ToString(), userinfo.CompanyCD);
                    if (ds != null)
                    {
                        

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string RetVal = ds.Tables[0].Rows[0]["流水号"].ToString() +
                                                                           ds.Tables[0].Rows[0]["物品编号"].ToString() +
                                                                           ds.Tables[0].Rows[0]["物品名称"].ToString() + ds.Tables[0].Rows[0]["名称简称"].ToString() +
                                                                           ds.Tables[0].Rows[0]["物品分类"].ToString() + ds.Tables[0].Rows[0]["条码"].ToString() +
                                                                           ds.Tables[0].Rows[0]["单位"].ToString() + ds.Tables[0].Rows[0]["规格型号"].ToString() +
                                                                           ds.Tables[0].Rows[0]["颜色"].ToString() +
                                                                           ds.Tables[0].Rows[0]["产地"].ToString() + ds.Tables[0].Rows[0]["主放仓库"].ToString() +
                                                                           ds.Tables[0].Rows[0]["最低库存"].ToString() + ds.Tables[0].Rows[0]["最高库存"].ToString() +
                                                                           ds.Tables[0].Rows[0]["安全库存"].ToString() +
                                                                           ds.Tables[0].Rows[0]["去税售价"].ToString() +
                                                                           ds.Tables[0].Rows[0]["销项税率"].ToString() +
                                                                           ds.Tables[0].Rows[0]["进项税率"].ToString() + ds.Tables[0].Rows[0]["含税售价"].ToString() +
                                                                           ds.Tables[0].Rows[0]["含税进价"].ToString() +
                                                                           ds.Tables[0].Rows[0]["去税进价"].ToString() + ds.Tables[0].Rows[0]["标准成本"].ToString()+
                                                                            ds.Tables[0].Rows[0]["厂家"].ToString() + ds.Tables[0].Rows[0]["尺寸"].ToString()+
                                                                            ds.Tables[0].Rows[0]["密度"].ToString() + ds.Tables[0].Rows[0]["每件片数"].ToString()+
                                                                            ds.Tables[0].Rows[0]["销售"].ToString() + ds.Tables[0].Rows[0]["采购"].ToString()+
                                                                            ds.Tables[0].Rows[0]["生产耗用"].ToString() + ds.Tables[0].Rows[0]["委外"].ToString()+
                                                                            ds.Tables[0].Rows[0]["自制"].ToString() + ds.Tables[0].Rows[0]["在制"].ToString()+
                                                                            ds.Tables[0].Rows[0]["应税劳务"].ToString()+
                                                                            ds.Tables[0].Rows[0]["最高进货价"].ToString() + ds.Tables[0].Rows[0]["最低销售价"].ToString()+
                                                                            ds.Tables[0].Rows[0]["纸号"].ToString() + ds.Tables[0].Rows[0]["耐磨纸"].ToString()+
                                                                            ds.Tables[0].Rows[0]["平衡纸"].ToString() + ds.Tables[0].Rows[0]["基材"].ToString()+
                                                                            ds.Tables[0].Rows[0]["表面处理工艺"].ToString() + ds.Tables[0].Rows[0]["背面底钢板"].ToString()+
                                                                            ds.Tables[0].Rows[0]["扣型"].ToString()+
                                                                              ds.Tables[0].Rows[0]["扩展项一"].ToString()+
                                                                            ds.Tables[0].Rows[0]["扩展项二"].ToString() + ds.Tables[0].Rows[0]["扩展项三"].ToString() +
                                                                            ds.Tables[0].Rows[0]["扩展项四"].ToString() + ds.Tables[0].Rows[0]["扩展项五"].ToString() +
                                                                            ds.Tables[0].Rows[0]["扩展项六"].ToString() + ds.Tables[0].Rows[0]["扩展项七"].ToString() +
                                                                            ds.Tables[0].Rows[0]["扩展项八"].ToString() + ds.Tables[0].Rows[0]["扩展项九"].ToString() +
                                                                            ds.Tables[0].Rows[0]["扩展项十"].ToString();

                            DataTable dt = new DataTable();
                            dt = ds.Tables[0].Clone();


                            try
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    
                                    //dt.Columns["流水号"] = ds.Tables[0].Rows[i]["流水号"].ToString().Trim();
                                    DataRow dr = ds.Tables[0].NewRow();
                                    string tem = StrReplace(ds.Tables[0].Rows[i]["流水号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["流水号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["物品编号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["物品编号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["物品名称"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["物品名称"] = StrReplacee(tem, 34, "”");//替换物品名称中的半角字符"为全角
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["名称简称"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["名称简称"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["物品分类"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["物品分类"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["条码"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["条码"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["单位"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["单位"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["规格型号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["规格型号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["颜色"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["颜色"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["产地"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["产地"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["主放仓库"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["主放仓库"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["最低库存"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["最高库存"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["安全库存"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["安全库存"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["去税售价"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["去税售价"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["销项税率"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["销项税率"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["进项税率"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["进项税率"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["含税售价"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["含税售价"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["去税进价"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["去税进价"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["标准成本"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["标准成本"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["厂家"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["厂家"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["尺寸"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["尺寸"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["密度"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["密度"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["每件片数"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["每件片数"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["销售"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["销售"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["采购"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["采购"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["生产耗用"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["生产耗用"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["委外"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["委外"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["自制"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["自制"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["在制"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["在制"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["应税劳务"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["应税劳务"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["最高进货价"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["最高进货价"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["最低销售价"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["最低销售价"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["纸号"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["纸号"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["耐磨纸"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["耐磨纸"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["平衡纸"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["平衡纸"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["基材"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["基材"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["表面处理工艺"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["表面处理工艺"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["背面底钢板"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["背面底钢板"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扣型"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扣型"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项一"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项一"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项二"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项二"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项三"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项三"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项四"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项四"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项五"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项五"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项六"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项六"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项七"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项七"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项八"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项八"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项九"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项九"] = tem;
                                    }
                                    tem = StrReplace(ds.Tables[0].Rows[i]["扩展项十"].ToString().Trim(), 127);
                                    if (tem != "")
                                    {
                                        dr["扩展项十"] = tem;
                                    }
                                    //dr["流水号"] =StrReplace( ds.Tables[0].Rows[i]["流水号"].ToString().Trim(),127);
                                    //dr["物品编号"] = StrReplace(ds.Tables[0].Rows[i]["物品编号"].ToString().Trim(),127);
                                    //dr["物品名称"] = StrReplace(ds.Tables[0].Rows[i]["物品名称"].ToString().Trim(),127);
                                    //dr["名称简称"] = StrReplace(ds.Tables[0].Rows[i]["名称简称"].ToString().Trim(),127);
                                    //dr["物品分类"] = StrReplace(ds.Tables[0].Rows[i]["物品分类"].ToString().Trim(),127);
                                    //dr["条码"] = StrReplace(ds.Tables[0].Rows[i]["条码"].ToString().Trim(),127);
                                    //dr["单位"] = StrReplace(ds.Tables[0].Rows[i]["单位"].ToString().Trim(),127);
                                    //dr["规格型号"] =StrReplace( ds.Tables[0].Rows[i]["规格型号"].ToString().Trim(),127);
                                    //dr["颜色"] = StrReplace(ds.Tables[0].Rows[i]["颜色"].ToString().Trim(),127);
                                    //dr["产地"] = StrReplace(ds.Tables[0].Rows[i]["产地"].ToString().Trim(),127);
                                    //dr["主放仓库"] = StrReplace(ds.Tables[0].Rows[i]["主放仓库"].ToString().Trim(),127);
                                    //dr["最低库存"] = StrReplace(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim(),127);
                                    //dr["最高库存"] = StrReplace(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim(),127);
                                    //dr["安全库存"] =StrReplace( ds.Tables[0].Rows[i]["安全库存"].ToString().Trim(),127);
                                    //dr["去税售价"] = StrReplace(ds.Tables[0].Rows[i]["去税售价"].ToString().Trim(),127);
                                    //dr["销项税率"] = StrReplace(ds.Tables[0].Rows[i]["销项税率"].ToString().Trim(),127);
                                    //dr["进项税率"] = StrReplace(ds.Tables[0].Rows[i]["进项税率"].ToString().Trim(),127);
                                    //dr["含税售价"] = StrReplace(ds.Tables[0].Rows[i]["含税售价"].ToString().Trim(),127);
                                    //dr["含税进价"] = StrReplace(ds.Tables[0].Rows[i]["含税进价"].ToString().Trim(), 127);
                                    //dr["去税进价"] =StrReplace( ds.Tables[0].Rows[i]["去税进价"].ToString().Trim(),127);
                                    //dr["标准成本"] = StrReplace(ds.Tables[0].Rows[i]["标准成本"].ToString().Trim(),127);
                                    
                                    
                                    //string trim = Regex.Replace(str, @"\s", "");
                                    dt.Rows.Add(dr.ItemArray);



                                }
                            }
                            catch (Exception ex)
                            { }
                                    
                                    
                                
                               
                            
                            
                            ds.Tables.RemoveAt(0);
                            ds.Tables.Add(dt.DefaultView.ToTable());
                            
                            for (int i = ds.Tables[0].Rows.Count-1; i >0; i--)
                            {
                                bool flag = false;
                                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                                {
                                    if (ds.Tables[0].Rows[i][j].ToString() != "" && ds.Tables[0].Rows[i][j].ToString() != null)
                                    {
                                        flag = true;
                                    }

                                }
                                int a = 0;
                                if (!flag)
                                {
                                    ds.Tables[0].Rows.RemoveAt(i);
                                    

                                }
                                

                            }
                           
                            

                        }
                        else
                        {
                            initvalidate();
                            this.lbl_result.Text = "您导入的Excel表没有数据!";
                            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, "", 0, "数据读取失败");
                    initvalidate();
                    this.lbl_result.Text = "数据读取失败,原因：" + ex.Message.ToString();
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                }
            }
            catch (Exception ex)
            {
                this.setup1.Enabled = false;
                this.lbl_result.Text = "数据读取失败,原因：" + ex.Message;
                ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            }
        }

    }

    //去掉字符串中特定ASC码字符
    /// <summary>
    /// 去掉字符串中特定ASC码字符
    /// </summary>
    /// <param name="stri">字符串</param>
    /// <param name="asc">除去字符asc码</param>
    /// <returns></returns>
    public string StrReplace(string stri,int asc)
    {
        string temp="";
        CharEnumerator CEnumerator = stri.GetEnumerator();

        while (CEnumerator.MoveNext())
        {

            byte[] array = new byte[1];

            array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());

            int asciicode = (short)(array[0]);

            if (asciicode != asc)
            {

                temp += CEnumerator.Current.ToString();

            }
            
        }
        return temp;
    }
    /// <summary>
    /// 替换特殊字符
    /// </summary>
    /// <param name="stri">字符串</param>
    /// <param name="asc">源字符asc码</param>
    /// <param name="strm">目的字符</param>
    /// <returns></returns>
    public string StrReplacee(string stri,int asc,string strm)
    {
        string temp = "";
        CharEnumerator CEnumerator = stri.GetEnumerator();

        while (CEnumerator.MoveNext())
        {

            byte[] array = new byte[1];

            array = System.Text.Encoding.ASCII.GetBytes(CEnumerator.Current.ToString());

            int asciicode = (short)(array[0]);

            if (asciicode == asc)
            {

                temp += strm;

            }
            else
            {
                temp += CEnumerator.Current.ToString();
            }

        }
        return temp;
    }

    #region 空值验证
    //空值验证
    public string IsNullCheck(DataRow dr)
    {
        string strErr = string.Empty;

        string[] code = { "物品编号", "物品名称", "主放仓库", "单位" };

        for (int k = 0; k < code.Length; k++)
        {
            if (dr[code[k]].ToString().Trim().Length < 1)
            {
                strErr += ""+code[k] +" 值不能为空值,导入操作失败!";
            }
        }



        //if (Session["newfile"] != null)
        //{
        //    ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        //}
        //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        return strErr;
    }
    #endregion
    #region 长度判断
    public string LengthCheck(DataRow dr)
    {
        //3.长度判断
        string suberrorstr = string.Empty;
        string strErr = string.Empty;
        string[] code = { "物品编号", "物品名称", "名称简称", "条码", "规格型号", "颜色", "产地", "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价","厂家" };
        int[] codelen = { 50, 100, 50, 50, 100, 12, 100, 12, 12, 12, 12, 12, 12, 12, 12, 12,256 };

        for (int k = 0; k < code.Length; k++)
        {
            if (dr[code[k]].ToString().Trim().Length > codelen[k])
            {
                strErr += ""+code[k] + " 数据长度过长,导入操作失败!";
            }
        }



        //if (Session["newfile"] != null)
        //{
        //    ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        //}
        //ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        return strErr;

    }
    #endregion
    #region 判断是否为实数
    /// 判断是否为实数
    public string RealNumCheck(DataRow dr)
    {
        //4.判断是否为实数
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        string strErr = string.Empty;
        string[] code = { "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价", "标准成本" };

        for (int k = 0; k < code.Length; k++)
        {
            try
            {
                if (dr[code[k]].ToString().Trim().Length > 0)
                {
                    
                    Single num = Single.Parse(dr[code[k]].ToString().Trim());
                }
            }
            catch(Exception e)
            {
                //string strErr = "";
                strErr += "数值格式验证:" + dr[1];
                strErr += code[k] + " 格式应该为数值,导入操作失败!";

            }
        }




        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        try
        {
            string maxstr = dr["最高库存"].ToString().Trim();
            string minstr = dr["最低库存"].ToString().Trim();
            string safestr = dr["安全库存"].ToString().Trim();
            string StandardCost = dr["标准成本"].ToString().Trim();
            decimal maxNum, minNum, safeNum, StandardNum;
            if (StandardCost.Length > 0)
            {
                StandardNum = Convert.ToDecimal(StandardCost);
                if (StandardNum <= 0)
                {
                    strErr += "标准成本必须大于零,导入操作失败!";
                }
            }
            else
            {
                strErr += "标准成本不能为空,导入操作失败!";
            }
            if (maxstr.Length > 0 && minstr.Length > 0)
            {
                maxNum = Convert.ToDecimal(dr["最高库存"].ToString().Trim());
                minNum = Convert.ToDecimal(dr["最低库存"].ToString().Trim());
                if (maxNum <= minNum)
                {
                    strErr += "最高库存数值低于或者等于最低库存,导入操作失败!";
                }
            }
            if (maxstr.Length > 0 && minstr.Length > 0 && safestr.Length > 0)
            {
                maxNum = Convert.ToDecimal(dr["最高库存"].ToString().Trim());
                minNum = Convert.ToDecimal(dr["最低库存"].ToString().Trim());
                safeNum = Convert.ToDecimal(dr["安全库存"].ToString().Trim());
                if (safeNum < minNum || safeNum > maxNum)
                {
                    strErr += "安全库存的数据不在最高库存和最低库存之间,导入操作失败!";

                }
            }
        }
        catch { }
        //}
        return strErr;

    }
    #endregion

    #region 判断是否存在物品编号 数据重复校验
    public string DuplicateCheck(DataRow dr)
    {
        string strErr = "";
        if (dr["物品编号"].ToString().Length > 0)
        {
            string[] arr = new string[] { "CustNo", "CompanyCD" };
            string[] CustNos = new string[] { dr["物品编号"].ToString().Trim(), userinfo.CompanyCD };

            bool NoHas = ProductInfoBus.ChargeProduct("ProdNo", dr["物品编号"].ToString().Trim(), userinfo.CompanyCD);
            if (NoHas)
            {
                if (strErr == "")
                {
                    strErr = " 数据重复校验：";
                    strErr += "物品编号与数据库中记录重复！ ";
                }
                else
                {
                    strErr += "物品编号与数据库中记录重复！";
                }

            }
        }



        return strErr;

    }
    #endregion

    #region 数据存在校验
    public string DataExistCheck(DataRow dr)
    {
        string strErr = "";
        try
        {
            if (dr["主放仓库"].ToString().Length > 0)
            {


                bool flag = ProductInfoBus.ChargeStorage(dr["主放仓库"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    if (strErr == "")
                    {
                        strErr = " 数据存在校验：";
                        strErr += "主放仓库不存在！ ";
                    }
                    else
                    {
                        strErr += "主放仓库不存在！";
                    }

                }
            }
            if (dr["物品分类"].ToString().Length > 0)
            {


                bool flag = ProductInfoBus.ChargeCodeType(dr["物品分类"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    if (strErr == "")
                    {
                        strErr = " 数据存在校验：";
                        strErr += "物品分类不存在！ ";
                    }
                    else
                    {
                        strErr += "物品分类不存在！";
                    }

                }
            }
            if (dr["单位"].ToString().Length > 0)
            {


                bool flag = ProductInfoBus.ChargeCodeUnit(dr["单位"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    if (strErr == "")
                    {
                        strErr = " 数据存在校验：";
                        strErr += "单位不存在！ ";
                    }
                    else
                    {
                        strErr += "单位不存在！";
                    }

                }
            }
            if (dr["条码"].ToString().Length > 0)
            {

                bool flag = ProductInfoBus.ChargeCodeUnit(dr["条码"].ToString().Trim(), userinfo.CompanyCD);
                if (flag)
                {
                    if (strErr == "")
                    {
                        strErr = " 数据存在校验：";
                        strErr += "该条码已经存在！ ";
                    }
                    else
                    {
                        strErr += "该条码已经存在！";
                    }

                }
            }
            if (dr["颜色"].ToString().Length > 0)
            {


                bool flag = ProductInfoBus.ValidateProductColor(dr["颜色"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    if (strErr == "")
                    {
                        strErr = " 数据存在校验：";
                        strErr += "颜色不存在！ ";
                    }
                    else
                    {
                        strErr += "颜色不存在！";
                    }

                }
            }
        }
        catch (Exception e)
        {
            string a = e.Message;
        }
        return strErr;

    }
    #endregion

    #region old 数据验证

    /// <summary>
    /// excel内部重复验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup1_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;

        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;

        #region/*excel内部重复判断*/
        //1.物品编号重复判断
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["物品编号"].ToString().Trim() == ds.Tables[0].Rows[k]["物品编号"].ToString().Trim())
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的物品编号与第" + (k + 2).ToString() + "行中的物品编号重复,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr = "<strong>重复值校验错误列表(物品编号)</strong><br>";
            errorstr += suberrorstr;
        }
        suberrorstr = string.Empty;

        ////2.物品名称重复判断
        //j = 0;
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
        //    {
        //        if (ds.Tables[0].Rows[i]["物品名称"].ToString().Trim() == ds.Tables[0].Rows[k]["物品名称"].ToString().Trim())
        //        {
        //            suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的物品名称与第" + (k + 2).ToString() + "行中的物品名称重复,导入操作失败!<br>";
        //        }
        //    }
        //}
        //if (j > 0)
        //{
        //    total += j;
        //    errorstr += "<strong>重复值校验错误列表(物品名称)</strong><br>";
        //    errorstr += suberrorstr;
        //    suberrorstr = string.Empty;
        //}

        //3、条码重复判断
        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = i + 1; k < ds.Tables[0].Rows.Count; k++)
            {
                if (ds.Tables[0].Rows[i]["条码"].ToString().Trim().Length > 0 && ds.Tables[0].Rows[k]["条码"].ToString().Trim().Length > 0)
                {
                    if (ds.Tables[0].Rows[i]["条码"].ToString().Trim() == ds.Tables[0].Rows[k]["条码"].ToString().Trim())
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行的条码与第" + (k + 2).ToString() + "行中的条码重复,导入操作失败!<br>";
                    }
                }
            }
        }
        if (j > 0)
        {
            total += j;
            errorstr += "<strong>重复值校验错误列表(条码)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        #endregion

        if ((total += j) < 1)
        {
            this.tr_result.Visible = false;
            this.setup1.Enabled = false;
            this.setup2.Enabled = true;
        }
        else
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
    }
    
   
    /// <summary>
    /// 空值验证
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup2_Click(object sender, EventArgs e)
    {
        //3.物品非空判断
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号

        errorstr = string.Empty;

        string[] code = { "物品编号", "物品名称", "主放仓库", "单位" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length < 1)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的  " + code[k] + "  值不能为空值,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            errorstr = "<strong>空值校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup2.Enabled = false;
            this.setup3.Enabled = true;
        }
    }
   
    /// <summary>
    /// 长度判断
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup3_Click(object sender, EventArgs e)
    {
        //3.长度判断
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        string[] code = { "物品编号", "物品名称", "名称简称", "条码", "规格型号", "颜色","产地", "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价" };
        int[] codelen = { 50, 100, 50, 50, 100, 12,100, 12, 12, 12, 12, 12, 12, 12, 12, 12 };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length > codelen[k])
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 数据长度过长,导入操作失败!<br>";
                }
            }
        }
        if (j > 0)
        {
            errorstr = "<strong>数据长度校验错误列表</strong><br>";
            errorstr += suberrorstr;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup3.Enabled = false;
            this.setup4.Enabled = true;
        }
    }

  
    /// <summary>
    /// 判断是否为实数
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void setup4_Click(object sender, EventArgs e)
    {
        //4.判断是否为实数
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;
        string[] code = { "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价","标准成本" };
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int k = 0; k < code.Length; k++)
            {
                try
                {
                    if (ds.Tables[0].Rows[i][code[k]].ToString().Trim().Length > 0)
                    {
                        Single num = Single.Parse(ds.Tables[0].Rows[i][code[k]].ToString().Trim());
                    }
                }
                catch
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的 " + code[k] + " 格式应该为数值,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            errorstr = "<strong>数据格式校验错误列表（数据类型）</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }
        total += j;
        j = 0;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            try
            {
                string maxstr = ds.Tables[0].Rows[i]["最高库存"].ToString().Trim();
                string minstr = ds.Tables[0].Rows[i]["最低库存"].ToString().Trim();
                string safestr = ds.Tables[0].Rows[i]["安全库存"].ToString().Trim();
                decimal maxNum, minNum, safeNum;
                if (maxstr.Length > 0 && minstr.Length > 0)
                {
                    maxNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim());
                    minNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim());
                    if (maxNum <= minNum)
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的最高库存数值低于或者等于最低库存,导入操作失败!<br>";
                    }
                }
                if (maxstr.Length > 0 && minstr.Length > 0 && safestr.Length > 0)
                {
                    maxNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最高库存"].ToString().Trim());
                    minNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["最低库存"].ToString().Trim());
                    safeNum = Convert.ToDecimal(ds.Tables[0].Rows[i]["安全库存"].ToString().Trim());
                    if (safeNum < minNum || safeNum > maxNum)
                    {
                        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的安全库存的数据不在最高库存和最低库存之间,导入操作失败!<br>";
                    }
                }
            }
            catch { }
        }

        if (j > 0) //有错
        {
            errorstr += "<strong>数据格式校验错误列表（库存范围）</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
            return;
        }
        if (total > 0)
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            return;
        }

        this.tr_result.Visible = false;
        this.setup4.Enabled = false;
        this.setup5.Enabled = true;
    }



    //已修改此方法
    protected void setup5_Click(object sender, EventArgs e)
    {
        string suberrorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录
        errorstr = string.Empty;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            bool flag = ProductInfoBus.ChargeProduct("ProdNo", ds.Tables[0].Rows[i]["物品编号"].ToString().Trim(), userinfo.CompanyCD);
            //1、判断物品编号是否存在
            if (flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品编号已经存在,导入操作失败!<br>";
            }
        }
        if (j > 0)
        {
            total += j;
            errorstr = "<strong>数据存在校验(物品编号)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        //2、判断物品名称是否存在
        //j = 0;
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    bool flag = ProductInfoBus.ChargeProduct("ProductName", ds.Tables[0].Rows[i]["物品名称"].ToString().Trim(), userinfo.CompanyCD);
        //    if (flag)
        //    {
        //        suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品名称已经存在,导入操作失败!<br>";
        //    }
        //}

        //if (j > 0)
        //{
        //    total = +j;
        //    errorstr += "<strong>数据存在校验(物品名称)</strong><br>";
        //    errorstr += suberrorstr;
        //    suberrorstr = string.Empty;
        //}

        if (total > 0)
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup5.Enabled = false;
            this.setup6.Enabled = true;
        }
    }
    //已修改此方法
    protected void setup6_Click(object sender, EventArgs e)
    {
        userinfo = (UserInfoUtil)SessionUtil.Session["UserInfo"];
        string suberrorstr = string.Empty;
        errorstr = string.Empty;
        int j = 0;//定义错误列表序号
        int total = 0;//总错误记录

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //判断仓库是否存在
            bool flag = ProductInfoBus.ChargeStorage(ds.Tables[0].Rows[i]["主放仓库"].ToString().Trim(), userinfo.CompanyCD);
            if (!flag)
            {
                suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的仓库不存在,导入操作失败!<br>";
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr = "<strong>数据存在校验错误列表(主放仓库)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["单位"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeCodeUnit(ds.Tables[0].Rows[i]["单位"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品单位不存在,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品单位)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["物品分类"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeCodeType(ds.Tables[0].Rows[i]["物品分类"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的物品分类不存在,导入操作失败!<br>";
                }
            }
        }

        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品分类)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }

        j = 0;


        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["条码"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ChargeBarCode(ds.Tables[0].Rows[i]["条码"].ToString().Trim(), userinfo.CompanyCD);
                if (flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的条码已经存在,导入操作失败!<br>";
                }
            }
        }



        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品分类)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }



        //验证颜色是否存在
        j = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string unitstr = ds.Tables[0].Rows[i]["颜色"].ToString().Trim();
            if (unitstr.Length > 0)
            {
                bool flag = ProductInfoBus.ValidateProductColor(ds.Tables[0].Rows[i]["颜色"].ToString().Trim(), userinfo.CompanyCD);
                if (!flag)
                {
                    suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的颜色在系统中不存在,导入操作失败!<br>";
                }
            }
        }


        if (j > 0)
        {
            total += j;
            errorstr += "<strong>数据存在校验错误列表(物品颜色)</strong><br>";
            errorstr += suberrorstr;
            suberrorstr = string.Empty;
        }



        if ((total += j) > 0)
        {
            this.tr_result.Visible = true;
            if (Session["newfile"] != null)
            {
                ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            }
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, errorstr);
        }
        else
        {
            this.tr_result.Visible = false;
            this.setup5.Enabled = false;
            this.setup6.Enabled = false;
            lbl_end.Visible = true;
            this.btn_input.Enabled = true;
        }
    }
    #endregion

    #region 批量导入
    protected void btn_input_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    if (userinfo.EmployeeID.ToString().Length < 1)
        //    {
        //        this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";
        //    }
        //    else
        //    {
        //        int num = ProductInfoBus.GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
        //        this.lbl_jg.Text = "Excel数据导入成功";
        //        if (Session["newfile"] != null)
        //        {
        //            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
        //        }
        //        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "成功导入" + ds.Tables[0].Rows.Count.ToString() + "条数据");
        //    }
        //    this.tab_end.Visible = true;
        //    btn_input.Enabled = false;
        //}
        //catch (Exception ex)
        //{
        //    btn_input.Enabled = true;
        //    this.tab_end.Visible = true;
        //    this.lbl_jg.Text = ex.Message;
        //    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_jg.Text);
        //}


        btn_input.Enabled = false;
        string ErrorSum = "";
        try
        {
           
                wrong = 0;
                if (userinfo.EmployeeID.ToString().Length < 1)
                {
                    ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
                    this.lbl_jg.Text = "该用户没有分配为雇员,导入失败!";

                }
                else
                {

                    if (!ds.Tables[0].Columns.Contains("del"))
                    {
                        ds.Tables[0].Columns.Add("del");
                    }
                    if (!ds.Tables[0].Columns.Contains("错误"))
                    {
                        ds.Tables[0].Columns.Add("错误");
                    }
                    DataTable dt = ds.Tables[0];

                    dt.Columns.Add("大类");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string strErr = DetermineLegality(dt.Rows[i]);
                        if (string.IsNullOrEmpty(strErr))
                        {
                            DataTable dt1 = ProductInfoBus.GetCodeType(dt.Rows[i]["物品分类"].ToString().Trim(), userinfo.CompanyCD);
                           
                            dt.Rows[i]["大类"] = dt1.Rows[0]["TypeFlag"].ToString();
                            
                            //GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
                            ProductInfoBus.dr_to_proinfo(dt.Rows[i], userinfo.CompanyCD);
                            int num = ProductInfoBus.GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
                            right++;
                            //int num = ProductInfoBus.GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
                                dt.Rows[i]["del"] = "1";
                            
                        }
                        else
                        {
                            if (i % 2 == 1)
                            {
                                errorstr += "<font color='#000093'>";
                             }
                            if (i % 2 == 0)
                            {
                                errorstr += "<font color='#003D79'>";
                            }
                            dt.Rows[i]["del"] = "0";
                            dt.Rows[i]["错误"] = strErr;
                            errorstr += "物品编号：";
                            errorstr += dt.Rows[i]["物品编号"];
                            errorstr += "&nbsp;&nbsp;&nbsp;&nbsp;";
                            errorstr += "物品名称：";
                            errorstr += dt.Rows[i]["物品名称"];
                            errorstr += "&nbsp;&nbsp;&nbsp;&nbsp;";
                            
                            
                            errorstr += "错误信息：";
                            errorstr += strErr;
                            errorstr += "<br>";
                            errorstr += "</font>";
                        }
                    }
                    //int num = ProductInfoBus.GetExcelToProductInfo(userinfo.CompanyCD, userinfo.EmployeeID.ToString());
                    
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i]["del"].ToString() == "1")
                        {
                            dt.Rows.RemoveAt(i);
                        }
                    }
                    ds.Tables.RemoveAt(0);
                    ds.Tables.Add(dt.DefaultView.ToTable());

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        wrong = Convert.ToInt32(ds.Tables[0].Rows.Count);
                        
                        if (wrong > 0)
                        {
                            Err_Excel.Visible = true;
                            Err_Excel.Enabled = true;
                            this.lbl_info.Visible = true;
                            this.tr_result.Visible = true;
                            
                        }
                       
                        this.lbl_jg.Text = "<font  color='#CE0000' size='+2'  >Excel数据导入成功" + right + "条，导入失败" + wrong + "条！</font>";
                        this.lbl_info.Text = "<font  color='#CE0000' size='+1'  >请点击‘失败数据导出’查看相关错误，修改后再导入！</font>";
                        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), right, 0, "物品档案导入成功" + right + "条,导入失败" + wrong + "条！<br>错误信息：<br>" + errorstr + "");
                    }
                    else
                    {
                        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), right, 1, "物品档案导入成功" + right + "条！");
                        this.lbl_jg.Text = "<font  color='#CE0000' size='+2'  >Excel数据" + right + "条全部导入成功!</font>";
                        this.lbl_info.Visible = false;
                        this.Err_Excel.Enabled = false;
                        if (Session["newfile"] != null)
                        {
                            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
                        }
                    }
                
                this.tab_end.Visible = true;

            }
            //else
            //{

            //    DataTable dt = dtCopy.DefaultView.ToTable();
                

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (string.IsNullOrEmpty(DetermineLegality(dt.Rows[i])))
            //        {
            //            if (CustInfoBus.InsertCustInfoRecord(dt.Rows[i], userinfo.BranchID.ToString(), userinfo.CompanyCD, userinfo.EmployeeID.ToString(), userinfo.UserID))
            //            {
            //                dt.Rows[i]["del"] = "1";

            //            }
            //            else
            //            {
            //                dt.Rows[i]["del"] = "0";
            //            }

            //        }
            //    }
            //    for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        if (dt.Rows[i]["del"].ToString() == "1")
            //        {
            //            dt.Rows.RemoveAt(i);
            //        }
            //    }
            //    ds.Tables.RemoveAt(0);
            //    ds.Tables.Add(dt.DefaultView.ToTable());
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {

            //    }
            //    else
            //    {
            //        ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), ds.Tables[0].Rows.Count, 1, "物品档案导入成功！");
            //        this.lbl_jg.Text = "Excel数据导入成功" + right + "条！";
            //        if (Session["newfile"] != null)
            //        {
            //            ProductInfoBus.DeleteFile(userinfo.CompanyCD, Session["newfile"].ToString());
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            ProductInfoBus.LogInsert(userinfo.CompanyCD, userinfo.DeptID, userinfo.UserID, Request.QueryString["ModuleID"].ToString(), 0, 0, this.lbl_result.Text);
            btn_input.Enabled = true;
            this.tab_end.Visible = true;
            this.lbl_jg.Text = "格式错误，导入失败！";
        }




    }
    #endregion
    #region 判断数据是否合法
    public string DetermineLegality(DataRow dr)
    {
        bool bSpecial = true;
        string strErr = "";
        string strErrs = "";
        strErr = CheckSpecialCharacters(dr);//特殊字符校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
            bSpecial = false;
        }
        strErr = IsNullCheck(dr);//空值校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        if (bSpecial)
        {
            strErr = DuplicateCheck(dr);//重复校验
            if (!string.IsNullOrEmpty(strErr))
            {
                strErrs += strErr;
            }
        }
        strErr = LengthCheck(dr);//信息项长度校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        strErr = RealNumCheck(dr);//实数校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        //strErr = DuplicateCheck(dr);
        //if (!string.IsNullOrEmpty(strErr))
        //{
        //    strErrs += strErr;
        //}
        strErr = DataExistCheck(dr);//数据存在校验
        if (!string.IsNullOrEmpty(strErr))
        {
            strErrs += strErr;
        }
        return strErrs;
    }

    #endregion

    #region 特殊字符校验
    public string CheckSpecialCharacters(DataRow dr)
    {
        string strErr = "";
        if (checkString(dr["物品名称"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["物品名称"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：<br>";
                strErr += "物品名称含有特殊字符(" + StrInfo + ") <br> ";
            }
            else
            {
                strErr += "物品名称含有特殊字符(" + StrInfo + ") <br>";
            }
            //suberrorstr += (++j).ToString() + "： " + "第" + (i + 2).ToString() + "行中的往来单位名称含有特殊字符(" + StrInfo + "),导入操作失败!<br>";
        }
        if (checkString(dr["名称简称"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["名称简称"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：<br>";
                strErr += "名称简称含有特殊字符(" + StrInfo + ") <br> ";
            }
            else
            {
                strErr += "名称简称含有特殊字符(" + StrInfo + ") <br>";
            }
        }
        if (checkString(dr["物品分类"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["物品分类"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：<br>";
                strErr += "物品分类含有特殊字符(" + StrInfo + ") <br> ";
            }
            else
            {
                strErr += "物品分类含有特殊字符(" + StrInfo + ") <br>";
            }
        }
        if (checkString(dr["产地"].ToString().Trim()))
        {
            string StrInfo = ShowCheckString(dr["产地"].ToString().Trim());
            if (strErr == "")
            {
                strErr = " 特殊字符校验：<br>";
                strErr += "产地含有特殊字符(" + StrInfo + ") <br> ";
            }
            else
            {
                strErr += "产地含有特殊字符(" + StrInfo + ") <br>";
            }
        }
        return strErr;
    }

    #endregion

    #region 字符串特殊字符显示

    public static string ShowCheckString(string source)
    {
        string StrInfo = "";
        string[] Strcheck = { "^", "'", "\\", "<", ">", "%", "?", "/", "+", "[", "]", "*", "$" };
        foreach (string str in Strcheck)
        {
            if (source.Contains(str))
            {
                StrInfo += str + ",";
            }
        }
        if (StrInfo.Length > 0)
        {
            StrInfo = StrInfo.Substring(0, StrInfo.Length - 1);
        }
        return StrInfo;
    }

    #endregion
    #region 验证特殊字符
    public static bool checkString(string source)
    {
        //Regex regExp = new Regex("[/^[a-zA-Z0-9_-()[]{}.]+$/g]");
        Regex regExp = new Regex(@"^[^'\\<>%?/+]*$");
        return !regExp.IsMatch(source);
    }

    public static bool chenkHave(string source)
    {
        Regex regExp = new Regex(@"^[a-zA-Z0-9-()_\[\]{}\.\\]+$");
        return !regExp.IsMatch(source);
    }
    #endregion

    

    protected void Err_Excel_Click(object sender, EventArgs e)
    {
        Err_Excel.Enabled = false;

        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        //方法一 
        
        OutputToExecl.ExportToTable_Err(this, dt,
                new string[] { "错误","流水号", "物品编号", "物品名称", "名称简称", "物品分类", "条码", "单位", "规格型号", "颜色", "产地", "主放仓库", "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价", "标准成本","厂家" },
                new string[] { "错误", "流水号", "物品编号", "物品名称", "名称简称", "物品分类", "条码", "单位", "规格型号", "颜色", "产地", "主放仓库", "最低库存", "最高库存", "安全库存", "去税售价", "销项税率", "进项税率", "含税售价", "含税进价", "去税进价", "标准成本", "厂家" },
                "商品档案导入失败列表","操作提示：请按照错误提示编辑好后，另存为Excel 标准格式。注意要在导入前把本行删除！");

        

        //方法二， 此方法需要excel组件 在服务器上安装excel
        //OutputToExecl.ExportDataTableToExcel(dt,
        //    new string[] { "序号", "往来单位编号", "往来单位类型", "往来单位名称", "往来单位简介", "人员编号", "分管业务员", "开户行", "户名", "账号", "联系人", "联系电话", "手机", "传真", "网址", "邮编", "Email", "通讯地址", "单位性质", "关系描述" }
        //    );
        lbl_jg.Text = "导出完成！";
    }
}
